Public Class frmCompras
    Dim idCompra As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idProveedor As Integer
    Dim idPedido As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As String
    Dim FolioAntB As Integer
    Dim SerieAnt As String
    Dim IdsSucursales As New elemento
    Dim IdsFormasdePago As New elemento
    Dim PrecioU As Double
    Dim PrecioBase As Double
    Dim IdRemision As Integer
    Dim Tabla As New DataTable
    Dim CosteotiempoREal As Byte
    Dim Op As dbOpciones
    Dim PorLotes As Byte
    Dim Aduana As Byte
    Dim IvaAnterir As Double
    Dim IepsAnterior As Double
    Dim DescuentoAterior As Double
    Dim IvaRetAnterior As Double
    Dim Almacen As dbAlmacenes
    'Dim Rep As New DibujaReportes()
    Dim ImpDoc As ImprimirDocumento
    Public Sub New(Optional ByVal pidCompra As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idCompra = pidCompra
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmCompras_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta compra no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesACompra(idCompra)
                Dim c As New dbCompras(MySqlcon)
                c.Eliminar(idCompra, 0, CDbl(TextBox10.Text), Estado, CosteotiempoREal)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmCompras_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled = True Then
                Modificar(Estados.Guardada)
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled = True Then
                Modificar(Estados.Pendiente)
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
        If e.KeyCode = Keys.F7 Then
            Dim f As New frmInventario
            f.ShowDialog()
            f.Dispose()
        End If
       
    End Sub

    Private Sub frmCompras_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button3.Enabled = True
        Else
            ConsultaOn = False
            CheckScroll.Checked = My.Settings.comprasscroll
            CheckBox2.Checked = My.Settings.comprasdatosestatico
            Dim I As Integer = 0
            Dim S As String = ""
            Dim D As Double = 0

            Op = New dbOpciones(MySqlcon)
            Almacen = New dbAlmacenes(MySqlcon)
            Almacen.AlmacenesSinPermiso(GlobalIdUsuario)
            CosteotiempoREal = Op.CostoTiempoReal
            Tabla.Columns.Add("Id", I.GetType)
            Tabla.Columns.Add("TipoR", S.GetType)
            Tabla.Columns.Add("Extra", S.GetType)
            Tabla.Columns.Add("Cantidad", D.GetType)
            Tabla.Columns.Add("Código", S.GetType)
            Tabla.Columns.Add("Descripción", S.GetType)
            Tabla.Columns.Add("Costo U.", S.GetType)
            Tabla.Columns.Add("Importe", S.GetType)
            Tabla.Columns.Add("Moneda", S.GetType)
            LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
            LlenaCombos("tblmonedas", cmbMoneda, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
            LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
            'LlenaCombos("tblformasdepago", ComboBox4, "nombre", "nombret", "idforma", IdsFormasdePago, , , "idforma")
            LlenaCombos("tblformasdepago", ComboBox4, "concat(convert(if(tipo=0,'CRÉDITO','CONTADO') using utf8),'-',nombre)", "nombret", "idforma", IdsFormasdePago, , , "idforma")
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1")
            Op.RutaXMLEgresos = Op.DaRutaXMLCompras
            If Op.RutaXMLEgresos <> "" Then OpenFileDialog1.InitialDirectory = Op.RutaXMLEgresos
            ConsultaOn = True
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(GlobalIdAlmacen)
            ImpDoc = New ImprimirDocumento()
            If idCompra = 0 Then
                Nuevo()
            Else
                LlenaDatosCompra()
            End If
        End If

    End Sub

    Private Sub SacaTotal(ByVal AgregaC As Boolean)
        Try
            If ConsultaOn Then
                Dim T As Double
                Dim Iva As Double
                Dim V As New dbCompras(MySqlcon)
                T = V.DaTotal(idCompra, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                Iva = V.TotalIva
                Label12.Text = Format(V.Subtotal, "#,##0.00")
                Label13.Text = Format(V.TotalIva - V.TotalISR - V.TotalIvaRetenido, "#,##0.00")
                If AgregaC Then
                    Label14.Text = Format(V.TotalVenta + CDbl(TextBox12.Text), "#,##0.00")
                Else
                    Label14.Text = Format(V.TotalVenta, "#,##0.00")
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        DateTimePicker1.Value = Date.Now
        Button16.Enabled = False
        Label29.Visible = False
        txtReferencia.Text = ""
        TextBox1.Text = ""
        txtSerie.Text = ""
        txtFolioi.Enabled = True
        txtSerie.Enabled = True
        FolioAnt = ""
        TextBox14.Text = ""
        TextBox13.Text = ""
        FolioAntB = 0
        SerieAnt = ""
        Button11.Enabled = True
        idCompra = 0
        IdRemision = 0
        idPedido = 0

        If GlobaltpBanxico <> "Error" Then
            TextBox10.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox10.Text = CM.Cantidad.ToString
        End If
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Compras, 0)
        txtSerie.Text = Sf.Serie
        Dim C As New dbCompras(MySqlcon)
        txtFolioi.Text = C.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        If CInt(txtFolioi.Text) < Sf.FolioInicial Then
            txtFolioi.Text = Sf.FolioInicial.ToString
        End If
        DGDetalles.DataSource = Nothing
        Panel1.Enabled = True
        ComboBox8.Enabled = True
        Panel2.Enabled = True
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        ComboBox3.Enabled = True
        txtReferencia.BackColor = Color.FromKnownColor(KnownColor.Window)
        txtFolioi.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        lblCancelada.Visible = False
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        TextBox12.Text = "0"
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        If Op._TipoSelAlmacen = "0" Then
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Else
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
        End If
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        If Op._TipoSelAlmacen = "0" Then
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.IdAlmacenC)
        Else
            ComboBox8.SelectedIndex = 0
        End If
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(GlobalIdMoneda)
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            ComboBox3.Enabled = False
        End If
        NuevoArticulo()
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        txtReferencia.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                ComboBox8.Focus()
                'End If
            Else
                txtCantidad.Focus()
            End If
        End If
        If e.KeyCode = Keys.F1 Then
            BotonProveedor()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaProveedor()
    End Sub
    Private Sub BuscaProveedor()
        Try
            If ConsultaOn Then
                Dim p As New dbproveedores(MySqlcon)
                If p.BuscaProveedor(TextBox1.Text) Then
                    TextBox7.Text = p.RFC + " " + p.Nombre ' vbCrLf + p.Direccion + vbCrLf + p.Telefono
                    TextBox7.Text += vbCrLf + "Límite: " + Format(p.LimiteCredito, "#,##0.00") + " Días: " + p.DiasCredito.ToString + " Saldo: " + Format(p.DaSaldoAFecha(p.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    TextBox7.Text += vbCrLf + p.Direccion + " " + p.NoExterior + " " + p.Ciudad + " " + p.Estado + " " + p.CP
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        If p.DiasCredito > 0 Or p.LimiteCredito > 0 Then
                            ComboBox4.SelectedIndex = 1
                        Else
                            ComboBox4.SelectedIndex = 0
                        End If
                    End If
                    idProveedor = p.ID
                Else
                    TextBox7.Text = ""
                    idProveedor = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Button14.Enabled = False
        Try
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            Dim MensajeError As String = ""
            Dim C As New dbCompras(MySqlcon)
            Dim Desglozar As Byte
            If C.RevisaConceptos(idCompra) = False Then
                MensajeError = "Hay conceptos en pesos y en dolares solo se pueden hacer comprar con un mismo tipo de moneda."
                'If MsgBox("Hay conceptos en pesos y en dolares solo se pueden hacer comprar con un mismo tipo de moneda.", MsgBoxStyle.Information) = MsgBoxResult.Cancel Then

                '    Exit Sub
                'End If
            End If
            'If TextBox2.Text = "" Then MensajeError = "Debe indicar una referencia."
            If IsNumeric(txtFolioi.Text) = False Then
                MensajeError += "Folio debe ser un valor numérico."
            Else
                If FolioAntB <> CInt(txtFolioi.Text) Or SerieAnt <> txtSerie.Text Then
                    If C.ChecaFolioRepetidoi(txtSerie.Text, CInt(txtFolioi.Text), IdsSucursales.Valor(ComboBox3.SelectedIndex)) Then
                        MensajeError += " Folio repetido."
                        txtFolioi.BackColor = Color.FromArgb(250, 150, 150)
                        'Label17.Visible = True
                    End If
                End If
            End If

            If pEstado = Estados.Guardada And pEstado <> Estados.Cancelada Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasModificar, PermisosN.Secciones.Compras) = False Then
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            If pEstado = Estados.Cancelada Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasCancelar, PermisosN.Secciones.Compras) = False Then
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            If IsNumeric(TextBox12.Text) = False Then MensajeError = "El costo indirecto debe ser un valor numérico"
            If pEstado = Estados.Guardada And txtReferencia.Text <> "" And FolioAnt <> txtReferencia.Text Then
                If C.ChecaFolioRepetido(txtReferencia.Text, idProveedor) Then
                    MensajeError += " Ya existe una compra con este folio de referencia."
                    txtReferencia.BackColor = Color.FromArgb(250, 150, 150)
                    Label17.Visible = True
                End If
            End If
            If MensajeError = "" Then
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                C.DaTotal(idCompra, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                Estado = pEstado
                C.Modificar(idCompra, Format(DateTimePicker1.Value, "yyyy/MM/dd"), txtReferencia.Text, Desglozar, pEstado, CDbl(TextBox10.Text), C.Subtotal, C.TotalVenta, IdsFormasdePago.Valor(ComboBox4.SelectedIndex), IDsMonedas2.Valor(ComboBox2.SelectedIndex), CDbl(TextBox12.Text), txtSerie.Text, CInt(txtFolioi.Text), TextBox14.Text, TextBox13.Text)
                Dim S As New dbInventarioSeries(MySqlcon)
                If pEstado = Estados.Cancelada Then
                    S.QuitaSeriesACompra(idCompra)
                    If C.VienedeRemision(idCompra) = 0 Then
                        C.RegresaInventario(idCompra, GlobalTipoCosteo, CDbl(TextBox10.Text), CosteotiempoREal)
                    End If
                    C.DesligaRemisiones(idCompra)
                    C.ReCalculaCostos(idCompra, GlobalTipoCosteo, CosteotiempoREal, CDbl(TextBox10.Text))
                End If
                If pEstado = Estados.Guardada Then
                    C.AsignaCostoIndirecto(idCompra, Op.TipoProrrateo)
                    C.ModificaInventario(idCompra, GlobalTipoCosteo, CDbl(TextBox10.Text))
                    C.ReCalculaCostos(idCompra, GlobalTipoCosteo, CosteotiempoREal, CDbl(TextBox10.Text))
                    If IdRemision <> 0 Then
                        Dim CR As New dbComprasRemisiones(MySqlcon)
                        CR.Usar(IdRemision, idCompra)
                    End If
                    Dim FP As New dbFormasdePago(IdsFormasdePago.Valor(ComboBox4.SelectedIndex), MySqlcon)
                    If Op.IntegrarBancosComprasContado = 1 And (FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad) And GlobalConBancos And GlobalPermisos.ChecaPermiso(PermisosN.Bancos.PagoProveedoresVer, PermisosN.Secciones.Bancos) = True Then
                        Dim Prov As New dbproveedores(idProveedor, MySqlcon)
                        Dim PP As New frmPagosProveedores(Op.IntegrarBancosComprasContado, "COMPRA: " + txtSerie.Text + txtFolioi.Text + " " + txtReferencia.Text, ComboBox4.Text.Replace("CONTADO-", ""), Prov.Nombre, C.TotalVenta, idProveedor, idCompra.ToString, Format(DateTimePicker1.Value, "dd/MM/yyyy"), 2)
                        'TextBox1.Text=Total pagado
                        If PP.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                            MsgBox("No se completo el ligado a bancos. Para ligar esta factura a bancos lo puede hacer después en un depósito.", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                        PP.Dispose()
                    End If
                    Imprimir(idCompra)
                    If S.CantidadDeSeriesAgregadasaCompra(idCompra, 0) > 0 Then
                        If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            ImprimirSeries()
                        End If
                    End If
                End If
                If pEstado = Estados.Guardada Or pEstado = Estados.Cancelada Then
                    GeneraPoliza()
                End If
                Nuevo()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
                Button14.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Dim CE As New dbCompras(MySqlcon)
            If CE.VienedeRemision(idCompra) = 0 Then
                CE.RegresaInventario(idCompra, GlobalTipoCosteo, CDbl(TextBox10.Text), CosteotiempoREal)
            End If
            Button14.Enabled = True
        End Try

    End Sub
    Private Sub Guardar()
        Try

            'If Button1.Text = "Guardar" Then

            If idProveedor <> 0 Then
                Dim C As New dbCompras(MySqlcon)
                Dim Desglozar As Byte
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                'TextBox8.Text = CStr(CDbl(TextBox8.Text) - (CDbl(TextBox8.Text) * CDbl(TextBox9.Text) / 100))
                If IsNumeric(txtFolioi.Text) = False Then
                    MsgBox("Debe indicar un folio.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                'If C.ChecaFolioRepetido(TextBox2.Text, idProveedor) Then
                '    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                '    Label17.Visible = True
                '    FolioAnt = ""
                'Else
                '    FolioAnt = TextBox2.Text
                'End If
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(cmbMoneda.SelectedIndex))
                C.Guardar(idProveedor, Format(DateTimePicker1.Value, "yyyy/MM/dd"), txtReferencia.Text, Desglozar, IdsSucursales.Valor(ComboBox3.SelectedIndex), CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsFormasdePago.Valor(ComboBox4.SelectedIndex), CInt(txtFolioi.Text), txtSerie.Text, TextBox13.Text, idPedido)
                idCompra = C.ID
                Estado = 1
                'Button1.Text = "Modificar"
                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                ComboBox3.Enabled = False
                'LlenaDatosDetalles()
            Else
                MsgBox("Debe indicar un proveedor", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodigo.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCosto.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonBuscar()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged
        BuscaArticulo()
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(txtCodigo.Text, 0, False, False, True) Then
                    LlenaDatosArticulo(p)
                Else
                    txtDescripcion.Text = ""
                    txtImporte.Text = "0"
                    TextBox11.Text = "0"
                    IdInventario = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub LlenaDatosCompra()
        IdRemision = 0
        Button11.Enabled = False
        Dim C As New dbCompras(idCompra, MySqlcon)
        ConsultaOn = False
        TextBox12.Text = C.CostoIndirecto.ToString
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.idSucursal)
        ConsultaOn = True
        Button16.Enabled = True
        txtReferencia.Text = C.Referencia
        txtSerie.Text = C.Serie
        txtFolioi.Text = C.Folioi.ToString
        FolioAnt = C.Referencia
        FolioAntB = C.Folioi
        SerieAnt = C.Serie
        txtIEPS.Text = C.IEPS
        txtIVARetenido.Text = C.ivaRetenido
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdMoneda)
        TextBox1.Text = C.Proveedor.Clave

        If Op._TipoSelAlmacen = "0" Then
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Else
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
        End If
        Estado = C.Estado
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        Button2.Enabled = True
        TextBox10.Text = C.TipodeCambio
        TextBox14.Text = C.Comentario
        TextBox13.Text = C.FolioCFDI
        ComboBox4.SelectedIndex = IdsFormasdePago.Busca(C.Idforma)
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdMoneda)
        DateTimePicker1.Value = C.Fecha
        ComboBox3.Enabled = False
        Button13.Enabled = True
        Dim KM As New dbkardexdocumentos(MySqlcon)
        If KM.MovimientosCompraCant(idCompra) > 0 Then
            Label29.Visible = True
        Else
            Label29.Visible = False
        End If
        ConsultaDetalles()
        If Estado = Estados.Cancelada Or Estado = Estados.Guardada Then
            'if permiso no cambiar guardadas
            'Panel1.Enabled = False
            'Panel2.Enabled = False
            'Button2.Enabled = False
            'else
            Panel1.Enabled = True
            ComboBox8.Enabled = True
            Panel2.Enabled = True
            Button1.Enabled = False
            Button14.Enabled = True
            Button2.Enabled = True

            lblCancelada.Visible = True
            If Estado = Estados.Guardada Then
                lblCancelada.Text = "GUARDADA"
                lblCancelada.ForeColor = Color.FromArgb(50, 255, 50)
            End If
        Else
            Panel1.Enabled = True
            Panel2.Enabled = True
            Button1.Enabled = True
            Button14.Enabled = True
            Button2.Enabled = True
            lblCancelada.Visible = False
        End If
        If Estado = Estados.Cancelada Then
            Button2.Enabled = False
            lblCancelada.Text = "CANCELADA"
            lblCancelada.ForeColor = Color.Red
            Button14.Enabled = False
            Panel1.Enabled = False
            ComboBox8.Enabled = False
        End If
    End Sub


    'Private Sub LlenaDatosDetalles()
    '    Panel1.Visible = True
    '    ConsultaDetalles()
    'End Sub

    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbComprasDetalles(MySqlcon)
            T = CD.ConsultaReader(idCompra, 0)
            While T.Read
                If T("cantidad") <> 0 Then
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                Else
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                End If
            End While
            T.Close()
            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(4).Width = 80
            DGDetalles.Columns(8).Width = 80
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal(False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    'Private Sub ConsultaDetalles()
    '    Try
    '        Dim CD As New dbComprasDetalles(MySqlcon)
    '        DGDetalles.DataSource = CD.Consulta(idCompra)
    '        Dim C As New dbCompras(MySqlcon)
    '        DGDetalles.Columns(0).Visible = False
    '        'DGDetalles.Columns(2).Width = 300
    '        DGDetalles.Columns(1).HeaderText = "Cantidad"
    '        DGDetalles.Columns(2).HeaderText = "Código"
    '        DGDetalles.Columns(3).HeaderText = "Descripción"
    '        DGDetalles.Columns(4).HeaderText = "Precio U."
    '        DGDetalles.Columns(4).HeaderText = "Importe"
    '        DGDetalles.Columns(5).HeaderText = "Moneda"
    '        DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    '        If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
    '        SacaTotal(False)
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub
    Private Sub NuevoArticulo()
        IdInventario = 0
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        txtCantidad.Text = "0"
        txtImporte.Text = "0"
        txtCosto.Text = "0"
        TextBox11.Text = "0"
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox9.Text = "0"
        Button20.Enabled = False
        Button23.Enabled = False
        'ComboBox1.SelectedIndex = 0
        Button6.Enabled = True
        txtCodigo.Enabled = True
        PrecioU = 0
        PorLotes = 0
        Aduana = 0
        Button9.Enabled = False
        Button12.Enabled = False
        If CheckBox2.Checked Then
            TextBox11.Text = Format(IvaAnterir, "0.00")
            txtIEPS.Text = Format(IepsAnterior, "0.00")
            txtIVARetenido.Text = Format(IvaRetAnterior, "0.00")
            TextBox9.Text = Format(DescuentoAterior, "0.00")
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambiodeAlmacen, PermisosN.Secciones.Compras) = False Then
            ComboBox8.Enabled = False
        Else
            ComboBox8.Enabled = True
        End If
        Button4.Text = "Agregar artículo"
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbComprasDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            If Op._TipoSelAlmacen = "1" Then
                If ComboBox8.SelectedIndex <= 0 Then
                    MsgError = "Debe seleccionar un almacen."
                    HayError = True
                End If
            End If
            If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            If IsNumeric(txtCantidad.Text) Then
                If CDbl(txtCantidad.Text) <= 0 Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(txtCosto.Text) = False Then
                MsgError += vbCrLf + "El costo debe ser un valor numérico."
                HayError = True
                'Else
                'If CDbl(TextBox8.Text) <= 0 Then
                '    MsgError += " El costo debe ser un valor mayor a 0."
                '    HayError = True
                'End If

            End If
            If IsNumeric(txtIEPS.Text) = False Then
                MsgError += vbCrLf + "El IEPS debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(txtIVARetenido.Text) = False Then
                MsgError += vbCrLf + "El IVA Retenido debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox9.Text) = False Then
                MsgError += vbCrLf + "El descuento debe ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox9.Text) <> 0 Then
                    txtCosto.Text = CStr(CDbl(txtCosto.Text) - (CDbl(txtCosto.Text) * CDbl(TextBox9.Text) / 100))
                End If
            End If
            If Almacen.TienePermiso(IdsAlmacenes.Valor(ComboBox8.SelectedIndex)) = False Then
                HayError = True
                MsgError += vbCrLf + " No tiene permiso para realizar operaciones en el almacén seleccionado."
            End If
            If HayError = False Then
                IvaAnterir = CDbl(TextBox11.Text)
                IepsAnterior = CDbl(txtIEPS.Text)
                DescuentoAterior = CDbl(TextBox9.Text)
                IvaRetAnterior = CDbl(txtIVARetenido.Text)
                If Button4.Text = "Agregar artículo" Then

                    CD.Guardar(idCompra, IdInventario, CDbl(txtCantidad.Text), CDbl(txtImporte.Text), IDsMonedas.Valor(cmbMoneda.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox11.Text), CDbl(TextBox9.Text), True, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
                    If ManejaSeries <> 0 Then
                        If CD.NuevoConcepto Then
                            Dim F As New frmInventarioSeries(IdInventario, idCompra, 0, CInt(txtCantidad.Text), DateTimePicker1.Value, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        Else
                            Dim F As New frmInventarioSeries(IdInventario, idCompra, 0, CD.Cantidad, DateTimePicker1.Value, 0, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                    End If
                    If PorLotes = 1 Then
                        Dim F As New frmInventarioLotes(0, 0, CD.ID, 0, CDbl(txtCantidad.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    If Aduana = 1 Then
                        Dim F As New frmInventarioAduana(0, 0, CD.ID, 0, CDbl(txtCantidad.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    'Dim I As New dbInventario(MySqlcon)
                    'Dim IK As New dbiKardex(MySqlcon)
                    'Dim EAnt As Double
                    'EAnt = I.DaInventarioTodos(IdInventario)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Alta, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'IK.Guardar(IdInventario, TiposDeMovimientos.CompraAlta, idCompra, CDbl(TextBox5.Text), EAnt, I.DaInventarioTodos(IdInventario), Format(Date.Today, "yyyy/MM/dd"), Format(Date.Today, "HH:mm:ss"))
                    ConsultaDetalles()
                    NuevoArticulo()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(txtCantidad.Text), CDbl(txtImporte.Text), IDsMonedas.Valor(cmbMoneda.SelectedIndex), CDbl(TextBox11.Text), CDbl(TextBox9.Text), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
                    If ManejaSeries <> 0 Then
                        Dim F As New frmInventarioSeries(IdInventario, idCompra, 0, CDbl(txtCantidad.Text), DateTimePicker1.Value, 0, 0)
                        F.ShowDialog()
                    End If
                    If PorLotes = 1 Then
                        Dim F As New frmInventarioLotes(0, 0, CD.ID, 0, CDbl(txtCantidad.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    If Aduana = 1 Then
                        Dim F As New frmInventarioAduana(0, 0, CD.ID, 0, CDbl(txtCantidad.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    'Dim I As New dbInventario(MySqlcon)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.Cambio, IdAlmacen)
                    ConsultaDetalles()
                    NuevoArticulo()
                    'PopUp("Artículo modificado", 90)
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        'If Op._TipoSelAlmacen = "1" Then
        '    If ComboBox8.SelectedIndex <= 0 Then
        '        MsgBox("Debe seleccionar un almacen.", MsgBoxStyle.Information, GlobalNombreApp)
        '        Exit Sub
        '    End If
        'End If
        If IdInventario <> 0 Then
            If Estado = Estados.Inicio Then
                ComboBox2.SelectedIndex = cmbMoneda.SelectedIndex
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasAlta, PermisosN.Secciones.Compras) = False Then
                    MsgBox("no tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                Guardar()
            End If
            If Estado <> Estados.Inicio Then
                If Estado = Estados.Guardada Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasModificar, PermisosN.Secciones.Compras) = False Then
                        MsgBox("no tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                        Exit Sub
                    End If
                End If
                AgregaArticulo()

            End If

            txtCantidad.Focus()
        Else
            MsgBox("Seleccione un artículo.", MsgBoxStyle.Critical, GlobalNombreApp)
            txtCodigo.Focus()
        End If
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbComprasDetalles(IdDetalle, MySqlcon)
            txtCodigo.Text = CD.Inventario.Clave
            txtCantidad.Text = CD.Cantidad.ToString
            IdInventario = CD.Idinventario
            IdAlmacen = CD.IdAlmacen
            CantAnt = CD.Cantidad
            txtIEPS.Text = CD.IEPS
            txtIVARetenido.Text = CD.ivaRetenido
            TextBox11.Text = CD.Iva.ToString
            TextBox9.Text = CD.Descuento.ToString
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(IdAlmacen)
            If CD.Descuento = 0 Then
                'If PrecioNeto = 0 Then
                PrecioU = Math.Round(CD.Precio / CD.Cantidad, 2)
                'Else
                '   PrecioU = CD.Precio / CD.Cantidad * (1 + CD.Iva / 100)
                'End If
            Else
                Dim Val As Double = (CD.Precio / (1 - CD.Descuento / 100))
                'If PrecioNeto = 0 Then
                PrecioU = Math.Round(Val / CD.Cantidad, 2)
                'Else
                '   PrecioU = Val / CD.Cantidad * (1 + CD.Iva / 100)
                'End If
            End If

            txtCosto.Text = PrecioU.ToString("0.00")

            If CD.Descuento = 0 Then
                txtImporte.Text = CD.Precio.ToString("0.00")
            Else
                txtImporte.Text = Format(PrecioU * CD.Cantidad, "0.00")
            End If
            If CD.Inventario.ManejaSeries = 1 Then
                Button12.Enabled = True
            Else
                Button12.Enabled = False
            End If
            PorLotes = CD.Inventario.PorLotes
            Aduana = CD.Inventario.Aduana
            If PorLotes = 1 Then
                Button20.Enabled = True
            Else
                Button20.Enabled = False
            End If
            If Aduana = 1 Then
                Button23.Enabled = True
            Else
                Button23.Enabled = False
            End If
            Button4.Text = "Modificar Artículo"
            If Estado <> Estados.Cancelada Then Button9.Enabled = True
            cmbMoneda.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            Button6.Enabled = False
            txtCodigo.Enabled = False
            ComboBox8.Enabled = False
            If CheckScroll.Checked Then txtCantidad.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosCompra()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta compra no ha sido guardada. ¿Empezar una compra nueva? Los datos no guardados se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesACompra(idCompra)
                Dim c As New dbCompras(MySqlcon)
                c.Eliminar(idCompra, 1, CDbl(TextBox10.Text), Estado, CosteotiempoREal)
                Nuevo()
            End If
        Else
            Nuevo()
        End If

    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        BotonNuevo()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoArticulo()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasModificar, PermisosN.Secciones.Compras) = False And Estado = Estados.Guardada Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim CD As New dbComprasDetalles(IdDetalle, MySqlcon)
                If Estado = Estados.Guardada Then
                    CD.QuitaInventarioLotesAduana(IdDetalle)
                    Dim I As New dbInventario(MySqlcon)
                    I.MovimientoDeInventario(CD.Idinventario, CD.Surtido, CD.Surtido, dbInventario.TipoMovimiento.Baja, CD.IdAlmacen)
                End If
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitarSeriesACompraxArticulo(IdInventario, idCompra)
                CD.Eliminar(IdDetalle)
                ConsultaDetalles()
                NuevoArticulo()
                PopUp("Articulo Eliminado", 90)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub BotonProveedor()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox7.Text = B.Proveedor.RFC + " " + B.Proveedor.Nombre 'B.Proveedor.Direccion + vbCrLf + B.Proveedor.Telefono
            TextBox7.Text += vbCrLf + "Límite: " + Format(B.Proveedor.LimiteCredito, "#,##0.00") + " Días: " + B.Proveedor.DiasCredito.ToString + " Saldo: " + Format(B.Proveedor.DaSaldoAFecha(B.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
            TextBox7.Text += vbCrLf + B.Proveedor.Direccion + " " + B.Proveedor.NoExterior + " " + B.Proveedor.Ciudad + " " + B.Proveedor.Estado + " " + B.Proveedor.CP
            idProveedor = B.Proveedor.ID
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                If B.Proveedor.DiasCredito > 0 Or B.Proveedor.LimiteCredito > 0 Then
                    ComboBox4.SelectedIndex = 1
                Else
                    ComboBox4.SelectedIndex = 0
                End If
            End If
            ConsultaOn = False
            TextBox1.Text = B.Proveedor.Clave
            ConsultaOn = True
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                ComboBox8.Focus()
                'End If
            Else
                txtCantidad.Focus()
            End If
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BotonProveedor()
    End Sub
    Private Sub BotonBuscar()
        If Op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Articulo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                LlenaDatosArticulo(B.Inventario)
                txtCosto.Focus()
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(frmBuscador.TipoDeBusqueda.Articulo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                LlenaDatosArticulo(B.Inventario)
                txtCosto.Focus()
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        BotonBuscar()
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        'Dim a As dbInventarioPrecios
        Dim Cant As Double
        txtDescripcion.Text = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        If IsNumeric(txtCantidad.Text) Then
            Cant = CDbl(txtCantidad.Text)
        Else
            txtCantidad.Text = "1"
            Cant = 1
        End If
        Articulo.DaUltimaidMoneda(Articulo.ID)
        Articulo.DaUltimoCosto(Articulo.ID)
        cmbMoneda.SelectedIndex = IDsMonedas.Busca(Articulo.idMonedaCompra)
        PrecioU = Math.Round(Articulo.UltimoPrecioCompra, 2)
        PrecioBase = PrecioU
        TextBox11.Text = Articulo.Iva.ToString
        ManejaSeries = Articulo.ManejaSeries
        txtCosto.Text = PrecioU.ToString("0.00")
        txtImporte.Text = Format(Cant * PrecioU, "0.00")
        txtIEPS.Text = Articulo.ieps.ToString
        txtIVARetenido.Text = Articulo.ivaRetenido.ToString
        If CheckBox1.Checked Then cmbMoneda.SelectedIndex = IDsMonedas.Busca(Articulo.idMonedaCompra)
        IdInventario = Articulo.ID
        PorLotes = Articulo.PorLotes
        Aduana = Articulo.Aduana
        'IdVariante = 0
        'If ManejaSeries = 0 Then
        '    Button12.Visible = False
        'Else
        '    Button12.Visible = True
        'End If
        ConsultaOn = False
        txtCodigo.Text = Articulo.Clave
        txtCodigo.Select(txtCodigo.Text.Length, 0)
        ConsultaOn = True
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SacaTotal(False)
    End Sub

    'Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
    '    SacaTotal()
    'End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmComprasConsulta(ModosDeBusqueda.Secundario, 0, idPedido)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idCompra = f.IdCompra
            LlenaDatosCompra()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasBaja, PermisosN.Secciones.Compras) = True Then
            Dim VP As New dbComprasPagos(MySqlcon)
            If VP.HayPagosCompras(idCompra) = 0 Then
                If MsgBox("¿Eliminar esta compra?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim S As New dbInventarioSeries(MySqlcon)
                    S.QuitaSeriesACompra(idCompra)
                    Dim C As New dbCompras(MySqlcon)
                    C.Eliminar(idCompra, GlobalTipoCosteo, CDbl(TextBox10.Text), Estado, CosteotiempoREal)
                    PopUp("Compra Eliminada", 90)
                    Nuevo()
                End If
            Else
                MsgBox("Para poder eliminar esta compra primero debe cancelar todos los pagos.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmInventarioSeries(IdInventario, idCompra, 0, CDbl(txtCantidad.Text), DateTimePicker1.Value, 0, 0)
        F.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim Dep As New dbPagosProveedores(MySqlcon)
        If Dep.TieneRetiros(idCompra) Then
            MsgBox("Esta compra tiene retiros ligados, necesita eliminar primero los retiros para poder cancelarla.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If MsgBox("¿Cancelar compra?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim VP As New dbComprasPagos(MySqlcon)
            If VP.HayPagosCompras(idCompra) = 0 Then
                Modificar(Estados.Cancelada)
                If MsgBox("¿Desea imprimir la compra?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Imprimir(idCompra)
                End If
            Else
                MsgBox("Para poder cancelar esta compra primero debe cancelar todos los pagos.", MsgBoxStyle.Information, GlobalNombreApp)
            End If


        End If

    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtSerie.Focus()
        End If
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ConsultaOn Then
            'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            If Op._TipoSelAlmacen = "0" Then
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            Else
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
            End If
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            If ComboBox8.Items.Count > 0 Then
                If Op._TipoSelAlmacen = "0" Then
                    ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.IdAlmacenC)
                Else
                    ComboBox8.SelectedIndex = 0
                End If
            Else
                MsgBox("Esta sucursal no cuenta con almacenes.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Compras, 0)
            txtSerie.Text = Sf.Serie
            Dim V As New dbCompras(MySqlcon)
            txtFolioi.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolioi.Text) < Sf.FolioInicial Then
                txtFolioi.Text = Sf.FolioInicial.ToString
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReferencia.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReferencia.TextChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCosto.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCosto.TextChanged
        If IdInventario <> 0 Then
            If IsNumeric(txtCosto.Text) Then
                PrecioU = CDbl(txtCosto.Text)
                If IsNumeric(txtCantidad.Text) Then
                    txtImporte.Text = CDbl(PrecioU * CDbl(txtCantidad.Text))
                End If
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCodigo.Enabled Then
                txtCodigo.Focus()
            Else
                txtCosto.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged
        If IdInventario <> 0 Then
            If IsNumeric(txtCantidad.Text) Then
                txtImporte.Text = CDbl(PrecioU * CDbl(txtCantidad.Text))
            End If
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'Rep.DibujaPaginaN(e.Graphics)
        'If Rep.MasPaginas = True Or Rep.NumeroPagina > 2 Then
        '    e.Graphics.DrawString("Página: " + Format(Rep.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        'End If

        'e.HasMorePages = Rep.MasPaginas

        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub



    Private Sub Imprimir(pIdCompra As Integer)
        Try
            Dim Compra As New dbCompras(pIdCompra, MySqlcon)
            ImpDoc.IdSucursal = Compra.idSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.Compra
            ImpDoc.TipoDocumentoT = TiposDocumentos.Compra + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.Compra
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion(pIdCompra)
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "COMPRA-" + Compra.Serie + Compra.Folioi.ToString("0000") + " " + Compra.Referencia
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaNodosImpresion(PIdCompra As Integer)
        Dim Compra As New dbCompras(PIdCompra, MySqlcon)
        Dim O As New dbOpciones(MySqlcon)
        Dim Suc As New dbSucursales(Compra.idSucursal, MySqlcon)
        Dim Prov As New dbproveedores(Compra.IdProveedor, MySqlcon)
        Compra.DaTotal(PIdCompra, Compra.IdMoneda)
        'Dim firmaChequeRecibido As String = "Firma cheque recibido"
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFolio", Compra.Folioi.ToString("00000"), 0), "docFolio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docSerie", Compra.Serie, 0), "docSerie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docref", Compra.Referencia, 0), "docref")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFecha", Compra.Fecha, 0), "docFecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docSubTotal", Format(Compra.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "docSubTotal")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "docTotal", Format(Compra.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "docTotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalieps", Format(Compra.TotalIeps, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "doctotalieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalivaret", Format(Compra.TotalIvaRetenido, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "doctotalivaret")
        Dim TotalconLetra As String
        Dim CL As New CLetras
        If Compra.TotalVenta >= 0 Then
            TotalconLetra = CL.LetrasM(Compra.TotalVenta, Compra.IdMoneda, GlobalIdiomaLetras)
        Else
            TotalconLetra = "MENOS " + CL.LetrasM(Compra.TotalVenta * -1, Compra.IdMoneda, GlobalIdiomaLetras)
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "usuario", GlobalUsuario, 0), "usuario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docCantLetra", TotalconLetra, 0), "docCantLetra")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docCostoIndirecto", Format(Compra.CostoIndirecto, O._formatoTotal).PadLeft(O.EspacioSubtotal), 0), "docCostoIndirecto")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaNombre", Suc.Nombre, 0), "empresaNombre")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaCalle", Suc.Direccion + " " + Suc.NoExterior, 0), "empresaCalle")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaColonia", Suc.Colonia, 0), "empresaColonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaCP", Suc.CP, 0), "empresaCP")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaRFC", Suc.RFC, 0), "empresaRFC")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaTel", Suc.Telefono, 0), "empresaTel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaTel2", Suc.Telefono, 0), "empresaTel2")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaEMail", Suc.Email, 0), "empresaEMail")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", Compra.Comentario, 0), "comentario")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "provNombre", Prov.Nombre, 0), "provNombre")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provDom", Prov.Direccion + " " + Prov.NoExterior + " " + Prov.NoInterior, 0), "provDom")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCol", Prov.Colonia, 0), "provCol")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCP", Prov.CP, 0), "provCP")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCiudad", Prov.Ciudad, 0), "provCiudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provRFC", Prov.RFC, 0), "provRFC")
        Dim Cont As Integer = 0
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Detalles As New dbComprasDetalles(MySqlcon)
        Dim TotalDescuento As Double = 0
        DR = Detalles.ConsultaReader(PIdCompra, 1)
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artCant", Format(DR("cantidad"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "artCant" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artPrecioU", Format((DR("precio") / DR("cantidad")), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "artPrecioU" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "provImporte", Format(DR("precio"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "provImporte" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artCodigo", DR("clave"), 0), "artCodigo" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "medida", DR("tipocantidad"), 0), "medida" + Format(ImpDoc.CuantosRenglones, "000"))

            If DR("cantidad") <> 0 And DR("descuento") <> 0 Then
                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(Desc, O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "importesindesc" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", Format((Desc / DR("cantidad")) * (DR("descuento") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocantuni" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", "", 0), "descuentocantuni" + Format(Cont, "000"))
                If DR("cantidad") <> 0 Then
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importesindesc", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importesindesc" + Format(Cont, "000"))
                Else
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                    ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importesindesc", "", 0), "importesindesc" + Format(Cont, "000"))
                End If
            End If
            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()
        'Dim V As New dbCompras(PIdCompra, MySqlcon)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoTotal).PadLeft(O.Espaciototal), 0), "totaldesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "foliocfdi", Compra.FolioCFDI, 0), "foliocfdi")
        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        Dim Aux As Integer = 0
        DR = Compra.DaIvas(PIdCompra)
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
        '  Y += 4

        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If

        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Aux, "00"))
            Aux += 1
        Next
        If Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
    End Sub


    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If idCompra <> 0 Then
            Imprimir(idCompra)
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim Forma As New frmBuscaDocumentoVenta(0, True, 2, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, False, True, True, 0, False, "", False)
            If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim V As New dbCompras(MySqlcon)
                If Estado = 0 Then
                    Select Case Forma.Tipo
                        Case 0
                            Dim Co As New dbComprasCotizacionesb(Forma.id(0), MySqlcon)
                            TextBox1.Text = Co.Proveedor.Clave
                        Case 1
                            Dim Cp As New dbComprasPedidos(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cp.Proveedor.Clave
                            idPedido = Cp.ID
                        Case 2
                            Dim Cr As New dbComprasRemisiones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cr.Proveedor.Clave
                            IdRemision = Forma.id(0)
                            idPedido = Cr.IdPedido
                        Case 3
                            Dim Cv As New dbCompras(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cv.Proveedor.Clave
                    End Select
                    Guardar()
                    If Estado <> 0 Then
                        V.AgregarDetallesReferencia(idCompra, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                        ConsultaDetalles()
                    End If
                Else
                    V.AgregarDetallesReferencia(idCompra, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    ConsultaDetalles()
                End If
                Button11.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ImprimirSeries()
        Dim V As New dbCompras(idCompra, MySqlcon)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repComprasSeries
        'V.ReporteVentasSeries(idCotizacion)
        Rep.SetDataSource(V.ReporteVentasSeries(idCompra))
        'Rep.SetParameterValue("Encabezado", O._NombreEmpresa)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        ImprimirSeries()
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        'If IsNumeric(TextBox12.Text) Then
        '    SacaTotal(True)
        'End If
    End Sub

    Private Sub DateTimePicker1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtReferencia.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbMoneda.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMoneda.SelectedIndexChanged

    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub


    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub ComboBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCantidad.Focus()
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged

    End Sub

    Private Sub txtSerie_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSerie.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFolioi.Focus()
        End If
    End Sub

    Private Sub txtSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerie.TextChanged

    End Sub

    Private Sub txtFolioi_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolioi.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtReferencia.Focus()
        End If
    End Sub

    Private Sub txtFolioi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolioi.TextChanged

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim C As New dbCompras(MySqlcon)
        If C.ChecaFolioRepetido(txtReferencia.Text, idProveedor) Then
            PopUp(" Ya existe una compra con este folio de referencia.", 90)
        Else
            C.ModificaReferencia(idCompra, txtReferencia.Text)
            PopUp("Referencia modificada.", 90)
        End If
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmProveedores(1, idProveedor, "")
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.CodigoProveedor
        End If
    End Sub

    Private Sub CheckScroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckScroll.CheckedChanged
        If CheckScroll.Checked Then
            My.Settings.comprasscroll = True
        Else
            My.Settings.comprasscroll = False
        End If
    End Sub


    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbCompras(MySqlcon)
                V.ActualizaComentario(idCompra, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button20_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        If PorLotes = 1 Then
            Dim F As New frmInventarioLotes(0, 0, IdDetalle, 0, CDbl(txtCantidad.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub txtIEPS_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIEPS.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtIVARetenido.Focus()
        End If
    End Sub

    Private Sub txtIEPS_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIEPS.Leave
        If txtIEPS.Text = "" Then
            txtIEPS.Text = "0"
        End If
    End Sub

    Private Sub txtIVARetenido_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIVARetenido.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox9.Focus()
        End If
    End Sub

    Private Sub txtIVARetenido_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVARetenido.Leave
        If txtIVARetenido.Text = "" Then
            txtIVARetenido.Text = "0"
        End If
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Try
            OpenFileDialog1.FileName = ""
            Dim p As New dbContabilidadPolizas(MySqlcon)
            If p.rutaUUID <> "" Then
                OpenFileDialog1.InitialDirectory = p.rutaUUID
            End If
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim xml As New Xml.XmlDocument
                Dim emisor As New dbproveedores(MySqlcon)
                Dim inventario As New dbInventario(MySqlcon)
                'Dim monedas As New dbMonedas(MySqlcon)
                Dim fp As New dbFormasdePago(MySqlcon)
                xml.Load(OpenFileDialog1.FileName)
                Dim Fecha As String = Replace(xml.Item("cfdi:Comprobante").Attributes("fecha").Value, "-", "/").Substring(0, 10)
                TextBox13.Text = xml.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                Dim rfc = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value.ToString
                DateTimePicker1.Value = Fecha
                'busca al emisor
                If emisor.BuscaProveedor("", rfc) Then
                    idProveedor = emisor.ID
                    'TextBox7.Text = emisor.RFC + " " + emisor.Nombre 'B.Proveedor.Direccion + vbCrLf + B.Proveedor.Telefono
                    'TextBox7.Text += vbCrLf + "Límite: " + Format(emisor.LimiteCredito, "#,##0.00") + " Días: " + emisor.DiasCredito.ToString + " Saldo: " + Format(emisor.DaSaldoAFecha(emisor.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    'TextBox7.Text += vbCrLf + emisor.Direccion + " " + emisor.NoExterior + " " + emisor.Ciudad + " " + emisor.Estado + " " + emisor.CP
                    'ConsultaOn = False
                    TextBox1.Text = emisor.Clave
                    'ConsultaOn = True
                Else
                    If MsgBox("Este proveedor no esta registrado en el sistema, ¿desea darlo de alta?", MsgBoxStyle.YesNo) = DialogResult.Yes Then
                        Dim frm As New frmProveedores(2, 0, "")
                        frm.RFC = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value.ToString
                        frm.Nombre = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("nombre").Value.ToString
                        If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal") IsNot Nothing Then
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("calle") IsNot Nothing Then frm.Calle = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("calle").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("localidad") IsNot Nothing Then frm.Ciudad = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("localidad").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("municipio") IsNot Nothing Then frm.Municipio = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("municipio").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("noExterior") IsNot Nothing Then frm.NoExterior = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("noExterior").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("noInterior") IsNot Nothing Then frm.NoInterior = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("noInterior").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("colonia") IsNot Nothing Then frm.Colonia = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("colonia").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("estado") IsNot Nothing Then frm.Estado = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("estado").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("pais") IsNot Nothing Then frm.Pais = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("pais").Value.ToString
                            If xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("codigoPostal") IsNot Nothing Then frm.CP = xml.Item("cfdi:Comprobante").Item("cfdi:Emisor").Item("cfdi:DomicilioFiscal").Attributes("codigoPostal").Value.ToString

                        End If
                        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                            idProveedor = frm.IdProveedor
                            emisor = New dbproveedores(frm.IdProveedor, MySqlcon)
                            'TextBox7.Text = emisor.RFC + " " + emisor.Nombre 'B.Proveedor.Direccion + vbCrLf + B.Proveedor.Telefono
                            'TextBox7.Text += vbCrLf + "Límite: " + Format(emisor.LimiteCredito, "#,##0.00") + " Días: " + emisor.DiasCredito.ToString + " Saldo: " + Format(emisor.DaSaldoAFecha(emisor.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                            'TextBox7.Text += vbCrLf + emisor.Direccion + " " + emisor.NoExterior + " " + emisor.Ciudad + " " + emisor.Estado + " " + emisor.CP
                            'ConsultaOn = False
                            TextBox1.Text = frm.CodigoProveedor
                            'ConsultaOn = True
                        End If
                    End If
                End If

                'agrega los datos generales del comprobante
                Dim Serie As String = ""
                Dim Folio As String = ""
                If xml.Item("cfdi:Comprobante").Attributes("serie") IsNot Nothing Then Serie = xml.Item("cfdi:Comprobante").Attributes("serie").Value
                If xml.Item("cfdi:Comprobante").Attributes("folio") IsNot Nothing Then Folio = xml.Item("cfdi:Comprobante").Attributes("folio").Value.ToString
                txtReferencia.Text = Serie + Folio
                Dim forma As String = xml.Item("cfdi:Comprobante").Attributes("formaDePago").Value.ToString
                If fp.BuscaForma(forma, 0) Then ComboBox4.SelectedIndex = IdsFormasdePago.Busca(fp.ID)
                Dim subtotal As Double = CDbl(xml.Item("cfdi:Comprobante").Attributes("subTotal").Value.ToString)
                Dim total As Double = CDbl(xml.Item("cfdi:Comprobante").Attributes("total").Value.ToString)
                Dim iva As Double = total - subtotal
                Label12.Text = subtotal
                Label13.Text = iva
                Label14.Text = total
                If xml.Item("cfdi:Comprobante").Attributes("TipoCambio") IsNot Nothing Then TextBox10.Text = xml.Item("cfdi:Comprobante").Attributes("TipoCambio").Value.ToString
                Dim moneda As String = "Pesos"
                If xml.Item("cfdi:Comprobante").Attributes("Moneda") IsNot Nothing Then xml.Item("cfdi:Comprobante").Attributes("Moneda").Value.ToString()
                If moneda.Contains("Pesos") Or moneda.Contains("MXN") Or moneda.Contains("PESOS") Then
                    moneda = "2"
                Else
                    moneda = "3"
                End If

                'agrega los conceptos
                For Each n As Xml.XmlElement In xml.Item("cfdi:Comprobante").Item("cfdi:Conceptos").ChildNodes
                    Dim noIdentificacion As String = ""
                    If n.Attributes("noIdentificacion") IsNot Nothing Then noIdentificacion = n.Attributes("noIdentificacion").Value
                    Dim descripcion As String = n.Attributes("descripcion").Value
                    If inventario.BuscaArticuloXML("", 0, , descripcion) = False Then
                        Dim res As DialogResult = MsgBox("No se encuentra el concepto " + descripcion.ToUpper + ". ¿Dar de alta árticulo?", MsgBoxStyle.YesNoCancel)
                        Select Case res
                            Case Windows.Forms.DialogResult.No
                                'seleccionar articulo de la consulta 
                                Dim f As New frmBuscador(frmBuscador.TipoDeBusqueda.Articulo, 0, False, False, False)

                                If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                    inventario.ID = f.ID
                                    inventario.LlenaDatos()
                                    IdInventario = inventario.ID
                                    txtCodigo.Text = inventario.Clave
                                    txtCantidad.Text = n.Attributes("cantidad").Value.ToString
                                    txtCosto.Text = n.Attributes("valorUnitario").Value.ToString
                                    txtImporte.Text = n.Attributes("importe").Value.ToString
                                    BotonAgregar()
                                End If
                            Case Windows.Forms.DialogResult.Yes
                                'dar de alta articulo
                                Dim f As New frmInventario(1)
                                f.Codigo = noIdentificacion
                                f.Descripcion = descripcion
                                'f.Costo = CDbl(n.Attributes("valorUnitario").Value.ToString)

                                If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                                    IdInventario = f.IdInventario
                                    txtCodigo.Text = f.Codigo
                                    txtCantidad.Text = n.Attributes("cantidad").Value.ToString
                                    txtCosto.Text = n.Attributes("valorUnitario").Value.ToString
                                    txtImporte.Text = n.Attributes("importe").Value.ToString
                                    BotonAgregar()
                                End If
                        End Select
                    Else
                        'ya existe el articulo
                        IdInventario = inventario.ID
                        txtCodigo.Text = inventario.Clave
                        txtCantidad.Text = n.Attributes("cantidad").Value.ToString
                        txtCosto.Text = n.Attributes("valorUnitario").Value.ToString
                        txtImporte.Text = n.Attributes("importe").Value.ToString
                        BotonAgregar()
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("Ocurrió un problema al cargar el XML.")
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            My.Settings.comprasdatosestatico = True
        Else
            My.Settings.comprasdatosestatico = False
        End If
    End Sub

    Private Sub txtIEPS_TextChanged(sender As Object, e As EventArgs) Handles txtIEPS.TextChanged

    End Sub

    Private Sub txtIVARetenido_TextChanged(sender As Object, e As EventArgs) Handles txtIVARetenido.TextChanged

    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If Aduana = 1 Then
            Dim F As New frmInventarioAduana(0, 0, IdDetalle, 0, CDbl(txtCantidad.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), ComboBox8.Text, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub GeneraPoliza()
        Try
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbCompras(idCompra, MySqlcon)
                Dim Canceladas As Byte = 0
                Dim credito As Byte
                Dim cuantas As Integer
                If V.Estado = Estados.Cancelada Then
                    Canceladas = 1
                End If
                Dim FP As New dbFormasdePago(V.Idforma, MySqlcon)
                If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                    credito = 0
                Else
                    credito = 1
                End If
                cuantas = M.CuantasHay(1, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(1, Canceladas, credito)
                    Else
                        cuantas = M.CuantasHay(1, Canceladas, 3)
                        If cuantas = 1 Then
                            M.ID = M.DaMascaraActiva(1, Canceladas, 3)
                        Else
                            Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 1)
                            f.ShowDialog()
                            If f.DialogResult = Windows.Forms.DialogResult.OK Then
                                M.ID = f.IdMascara
                            Else
                                Exit Sub
                            End If
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    If Canceladas = 0 Then
                        GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    Else
                        GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    End If
                    GP.GeneraPolizaGeneral(V.ID, V.IdProveedor, 1, 0, 0, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        If idCompra > 0 Then
            Dim frmK As New FrmDocKardex(idCompra, 2, txtReferencia.Text + txtSerie.Text + txtFolioi.Text, TextBox1.Text)
            frmK.ShowDialog()
            frmK.Dispose()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(OpenFileDialog1.FileName)
                TextBox13.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            End Try
        End If
    End Sub
End Class