Public Class frmComprasRemisiones
    
    Dim idRemision As Integer
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
    Dim FolioAnt As Integer
    'Dim IdVariante As Integer
    'Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim PrecioBase As Double
    Dim IdsSucursales As New elemento
    Dim Tabla As New DataTable
    Dim PorLotes As Byte
    Dim Aduana As Byte
    Dim ImpDoc As ImprimirDocumento
    Dim Op As dbOpciones
    Dim Almacen As dbAlmacenes
    Public Sub New(Optional ByVal pidRemision As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idRemision = pidRemision
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta remisión no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesARemisionCompra(idRemision)
                Dim c As New dbComprasRemisiones(MySqlcon)
                c.Eliminar(idRemision)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmComprasRemisiones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled Then
                Modificar(Estados.Guardada)
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled Then
                Modificar(Estados.Pendiente)
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
        If e.KeyCode = Keys.F7 Then
            Dim f As New frmInventario
            f.ShowDialog()
        End If

    End Sub

    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConsultaOn = False
        'Label5.Text = CStr(CSng("1,000"))
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Tabla.Columns.Add("Id", I.GetType)
        Tabla.Columns.Add("TipoR", S.GetType)
        Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Precio U.", S.GetType)
        Tabla.Columns.Add("Importe", S.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        'ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(IdAlmacenG)
        Op = New dbOpciones(MySqlcon)
        Almacen = New dbAlmacenes(MySqlcon)
        ImpDoc = New ImprimirDocumento()
        Almacen.AlmacenesSinPermiso(GlobalIdUsuario)
        ConsultaOn = True
        If idRemision = 0 Then
            Nuevo()
            NuevoConcepto()
        Else
            ConsultaOn = False
            LlenaDatosVenta()
            ConsultaOn = True
        End If
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim T As Double
                Dim Iva As Double
                Dim V As New dbComprasRemisiones(MySqlcon)
                T = V.DaTotal(idRemision, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                Iva = V.TotalIva
                Label12.Text = Format(V.Subtotal, "#,##0.00")
                Label13.Text = Format(V.TotalIva - V.TotalISR - V.TotalIvaRetenido, "#,##0.00")
                Label14.Text = Format(V.TotalVenta, "#,##0.00")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        Panel1.Enabled = True
        Panel2.Enabled = True
        If GlobaltpBanxico <> "Error" Then
            txtTipodeCambio.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            txtTipodeCambio.Text = CM.Cantidad.ToString
        End If
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        DateTimePicker1.Value = Date.Now
        TextBox1.Text = ""
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        txtFolio.Enabled = True
        txtSerie.Enabled = True
        txtFolioi.Enabled = True
        Button11.Enabled = True
        Label27.Text = ""
        txtFolio.Text = ""
        FolioAnt = 0
        txtFolio.BackColor = Color.FromKnownColor(KnownColor.Window)
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        If Op._TipoSelAlmacen = "0" Then
            LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Else
            LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
        End If
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'TextBox11.Text = S.Serie
        If Op._TipoSelAlmacen = "0" Then
            cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(S.IdAlmacenC)
        Else
            cmbAlmacen.SelectedIndex = 0
        End If
        'Dim Sf As New dbSucursalesFolios(MySqlcon)
        'If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.VentasCotizaciones, 0) Then
        ' TextBox11.Text = Sf.Serie
        ' Else
        'TextBox11.Text = ""
        'End If
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasRemisiones, 0)
        txtSerie.Text = Sf.Serie
        Dim V As New dbComprasRemisiones(MySqlcon)
        txtFolioi.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(txtFolioi.Text) < Sf.FolioInicial Then
            txtFolioi.Text = Sf.FolioInicial.ToString
        End If
        Label24.Visible = False
        idRemision = 0
        idPedido = 0
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        TextBox14.Text = ""
        txtFolio.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        'Dim o As New dbOpciones(MySqlcon)
        'TextBox8.Text = o.Imp.ToString
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        CheckBox1.Checked = False
        Button2.Enabled = False
        Button15.Enabled = False
        Button18.Enabled = False
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            ComboBox3.Enabled = False
        End If
        NuevoConcepto()
        txtFolio.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                cmbAlmacen.Focus()
                'End If
            Else
                TextBox5.Focus()
            End If
        End If
        If e.KeyCode = Keys.F1 Then
            BotonProveedores()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaProveedor()
    End Sub
    Private Sub BuscaProveedor()
        Try
            If ConsultaOn Then
                Dim c As New dbproveedores(MySqlcon)
                If c.BuscaProveedor(TextBox1.Text) Then
                    'If c.DireccionFiscal = 0 Then
                    'TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    TextBox7.Text = c.RFC + " " + c.Nombre ' vbCrLf + p.Direccion + vbCrLf + p.Telefono
                    TextBox7.Text += vbCrLf + "Límite: " + Format(c.LimiteCredito, "#,##0.00") + " Días: " + c.DiasCredito.ToString + " Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    TextBox7.Text += vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.Estado + " " + c.CP
                    'TextBox13.Text = "Límite: " + Format(c.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + c.DiasCredito.ToString + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    'Else
                    '   TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    'End If
                    'TextBox13.Text = "Límite: " + Format(c.Credito, "#,##0.00") + vbCrLf + "Días: " + c.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    idProveedor = c.ID
                Else
                    TextBox7.Text = ""
                    'TextBox13.Text = ""
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
        Try
            Dim MensajeError As String = ""
            Dim C As New dbComprasRemisiones(idRemision, MySqlcon)
            Dim Desglozar As Byte
            'If txtFolio.Text = "" Then MensajeError = "Debe indicar un folio."
            'If FolioAnt <> TextBox2.Text Then
            '    If C.ChecaFolioRepetido(TextBox2.Text) Then
            '        MensajeError += " Ya existe una compra con este folio."
            '        TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            '        Label17.Visible = True
            '    End If
            'End If
            'If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            If C.idComprar <> 0 And pEstado = Estados.Cancelada Then
                MensajeError += " No se puede cancelar una remisión ya facturada necesita cancelar primero la factura."
            End If

            If C.RevisaConceptos(idRemision) = False Then
                MensajeError = "Hay conceptos en pesos y en dolares solo se pueden hacer comprar con un mismo tipo de moneda."
                'If MsgBox("Hay conceptos en pesos y en dolares solo se pueden hacer comprar con un mismo tipo de moneda.", MsgBoxStyle.Information) = MsgBoxResult.Cancel Then

                '    Exit Sub
                'End If
            End If

            If txtFolio.Text <> "" Then
                If C.ChecaFolioRepetido(txtFolio.Text, idProveedor) And pEstado = Estados.Guardada Then
                    MensajeError += " Folio Referencia Repetido"
                End If
            End If

            If IsNumeric(txtTipodeCambio.Text) = False Then MensajeError += " Tipo de cambio debe ser un valor numérico."
            If IsNumeric(txtFolioi.Text) Then
                If C.ChecaFolioRepetidoi(txtSerie.Text, CInt(txtFolioi.Text), IdsSucursales.Valor(ComboBox3.SelectedIndex)) And pEstado = Estados.Guardada Then
                    MensajeError += " Folio Repetido"
                End If
            Else
                MensajeError += " El Folio debe ser un valor numérico"
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesAlta, PermisosN.Secciones.Compras) = False And pEstado = Estados.Guardada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesCancelar, PermisosN.Secciones.Compras) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If MensajeError = "" Then
                '
                'If FolioAnt <> CInt(TextBox2.Text) Then

                'End If
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                'Dim O As New dbOpciones(MySqlcon)
                C.DaTotal(idRemision, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Modificar(idRemision, Format(DateTimePicker1.Value, "yyyy/MM/dd"), txtFolio.Text, IDsMonedas2.Valor(ComboBox2.SelectedIndex), 0, pEstado, C.Subtotal, C.TotalVenta, idProveedor, txtSerie.Text, CInt(txtFolioi.Text), CDbl(txtTipodeCambio.Text), TextBox14.Text)

                Dim S As New dbInventarioSeries(MySqlcon)
                If pEstado = Estados.Guardada Then
                    C.ModificaInventario(idRemision)
                    Imprimir(idRemision)
                    If S.CantidadDeSeriesAgregadasaRemisionCompra(idRemision, 0) > 0 Then
                        If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            ImprimirSeries()
                        End If
                    End If
                End If
                If pEstado = Estados.Cancelada Then
                    S.QuitaSeriesARemisionCompra(idRemision)
                    C.RegresaInventario(idRemision)
                End If
                Nuevo()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Dim RE As New dbComprasRemisiones(MySqlcon)
            RE.RegresaInventario(idRemision)
        End Try
    End Sub
    Private Sub ImprimirSeries()
        Dim V As New dbComprasRemisiones(idRemision, MySqlcon)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repComprasSeriesR
        'V.ReporteVentasSeries(idRemision)
        Rep.SetDataSource(V.ReporteVentasSeries(idRemision))
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub
    Private Sub Guardar()
        Try

            'If Button1.Text = "Guardar" Then
            'If (PermisosCompras And CULng((Math.Pow(2, perCompras.Remisiones + 1)))) <> 0 Then
            If idProveedor <> 0 Then
                Dim C As New dbComprasRemisiones(MySqlcon)
                'Dim Desglozar As Byte
                'If CheckBox1.Checked Then
                '    Desglozar = 1
                'Else
                '    Desglozar = 0
                'End If

                'If C.ChecaFolioRepetido(TextBox2.Text) Then
                '    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                '    Label17.Visible = True
                '    FolioAnt = 0
                'Else
                '    FolioAnt = TextBox2.Text
                'End If
                'If C.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox11.Text) Then
                '    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                '    Label17.Visible = True
                '    FolioAnt = 0
                'Else
                '    FolioAnt = CInt(TextBox2.Text)
                'End If
                'Dim O As New dbOpciones(MySqlcon)

                C.Guardar(idProveedor, Format(DateTimePicker1.Value, "yyyy/MM/dd"), txtFolio.Text, IDsMonedas2.Valor(ComboBox2.SelectedIndex), 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), txtSerie.Text, CInt(txtFolioi.Text), CDbl(txtTipodeCambio.Text), idPedido)
                idRemision = C.ID
                Estado = 1
                'Button1.Text = "Modificar"
                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Button15.Enabled = True
                Button18.Enabled = True
                'LlenaDatosDetalles()
            Else
                MsgBox("Debe indicar un proveedor", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox12.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonArticulos()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0, False, False, True, False) Then
                    LlenaDatosArticulo(p)
                Else
                    'IdInventario = 0
                    'Dim ps As New dbProductos(MySqlcon)
                    'If ps.BuscaProducto(TextBox3.Text) Then
                    '    LlenaDatosProducto(ps)
                    'Else
                    TextBox4.Text = ""
                    TextBox6.Text = "0"
                    TextBox8.Text = "0"
                    TextBox9.Text = "0"
                    PrecioU = 0
                    'IdVariante = 0
                    'End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosVenta()
        Dim C As New dbComprasRemisiones(idRemision, MySqlcon)

        If ConsultaOn = False Then
            ConsultaOn = True
            ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
            ConsultaOn = False
        Else
            ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        End If

        txtFolio.Text = C.Folio
        txtSerie.Text = C.Serie
        txtFolioi.Text = C.Folioi.ToString
        txtTipodeCambio.Text = C.TipodeCambio.ToString
        'TextBox11.Text = C.Serie


        'FolioAnt = C.Folio
        TextBox1.Text = C.Proveedor.Clave
        Estado = C.Estado
        TextBox8.Text = C.Iva.ToString
        TextBox14.Text = C.Comentario
        Label27.Text = C.FolioRef
        'If C.Desglosar = 1 Then
        '    CheckBox1.Checked = True
        'Else
        '    CheckBox1.Checked = False
        'End If
        Button2.Enabled = True
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.idMoneda)
        DateTimePicker1.Value = C.Fecha


        ConsultaDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                Button15.Enabled = True
                Button18.Enabled = False
                txtSerie.Enabled = False
                txtFolioi.Enabled = False
                Button9.Enabled = False
                txtFolio.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                Button15.Enabled = True
                Button18.Enabled = True
                txtSerie.Enabled = False
                txtFolioi.Enabled = False
                txtFolio.Enabled = False
                Button9.Enabled = False
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel1.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Button15.Enabled = True
                Button18.Enabled = True
                Button9.Enabled = False
                txtSerie.Enabled = True
                txtFolioi.Enabled = True
                txtFolio.Enabled = True
        End Select


    End Sub

    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbComprasRemisionesDetalles(MySqlcon)
            T = CD.ConsultaReader(idRemision, 0)
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
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        'IdVariante = 0
        PrecioBase = 0
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        TextBox9.Text = "0"
        TextBox12.Text = "0"
        PorLotes = 0
        Aduana = 0
        Button16.Enabled = False
        Button12.Visible = False
        Button9.Enabled = False
        Button6.Enabled = True
        TextBox3.Enabled = True
        Button20.Enabled = False
        pnlUbicacion.Visible = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambiodeAlmacen, PermisosN.Secciones.Compras) = False Then
            cmbAlmacen.Enabled = False
        Else
            cmbAlmacen.Enabled = True
        End If
        TextBox5.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbComprasRemisionesDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            If Op._TipoSelAlmacen = "1" Then
                If cmbAlmacen.SelectedIndex <= 0 Then
                    MsgError = "Debe seleccionar un almacen."
                    HayError = True
                End If
            End If
            If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox6.Text) = False Then
                MsgError += vbCrLf + "El costo debe ser un valor numérico."
                HayError = True
                'Else
                '    If CDbl(TextBox6.Text) <= 0 Then
                '        MsgError += " El costo debe ser un valor mayor a 0."
                '        HayError = True
                '    End If
            End If
            If Almacen.TienePermiso(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex)) = False Then
                HayError = True
                MsgError += vbCrLf + " No tiene permiso para realizar operaciones en el almacén seleccionado."
            End If
            If IsNumeric(TextBox9.Text) = False Then
                MsgError += vbCrLf + "El descuento debe ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox9.Text) <> 0 Then
                    TextBox12.Text = CStr(CDbl(TextBox12.Text) - (CDbl(TextBox12.Text) * CDbl(TextBox9.Text) / 100))
                End If
            End If

            If IsNumeric(txtIEPS.Text) = False Then
                MsgError += vbCrLf + "El IEPS debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(txtIVARetenido.Text) = False Then
                MsgError += vbCrLf + "El IVA Retenido debe ser un valor numérico."
                HayError = True
            End If
            If HayError = False Then
                If Button4.Text = "Agregar Concepto" Then

                    CD.Guardar(idRemision, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
                    If ManejaSeries <> 0 Then
                        If CD.NuevoConcepto Then
                            Dim F As New frmInventarioSeries(IdInventario, 0, 0, CInt(TextBox5.Text), DateTimePicker1.Value, 0, idRemision)
                            F.ShowDialog()
                        Else
                            Dim F As New frmInventarioSeries(IdInventario, 0, 0, CD.Cantidad, DateTimePicker1.Value, 0, idRemision)
                            F.ShowDialog()
                        End If
                    End If
                    If PorLotes = 1 Then
                        Dim F As New frmInventarioLotes(0, 0, 0, CD.ID, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    If Aduana = 1 Then
                        Dim F As New frmInventarioAduana(0, 0, 0, CD.ID, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    'Dim I As New dbInventario(MySqlcon)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Baja, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    ConsultaDetalles()
                    NuevoConcepto()
                    SacaTotal()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), If(pnlUbicacion.Visible, cmbUbicacion.SelectedValue, ""), txtTarima.Text)
                    If ManejaSeries <> 0 Then
                        Dim F As New frmInventarioSeries(IdInventario, 0, 0, CDbl(TextBox5.Text), DateTimePicker1.Value, 0, idRemision)
                        F.ShowDialog()
                    End If
                    If PorLotes = 1 Then
                        Dim F As New frmInventarioLotes(0, 0, 0, IdDetalle, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    If Aduana = 1 Then
                        Dim F As New frmInventarioAduana(0, 0, 0, IdDetalle, CDbl(TextBox5.Text), IdInventario, 0, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    'Dim I As New dbInventario(MySqlcon)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    ConsultaDetalles()
                    NuevoConcepto()
                    SacaTotal()
                    'PopUp("Artículo modificado", 90)

                End If
                TextBox5.Focus()
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub



    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesAlta, PermisosN.Secciones.Compras) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                If IdInventario <> 0 Then AgregaArticulo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try
            Button6.Enabled = False
            TextBox3.Enabled = False
            cmbAlmacen.Enabled = False
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            'Tipo = DGDetalles.Item(1, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbComprasRemisionesDetalles(IdDetalle, MySqlcon)
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            TextBox5.Text = CD.Cantidad.ToString
            txtIEPS.Text = CD.IEPS
            txtIVARetenido.Text = CD.ivaRetenido
            IdInventario = CD.Idinventario
            'IdVariante = CD.IdVariante
            'If IdInventario > 1 Then
            TextBox3.Text = CD.Inventario.Clave
            PorLotes = CD.Inventario.PorLotes
            Aduana = CD.Inventario.Aduana
            If PorLotes = 1 Then
                Button20.Enabled = True
            Else
                Button20.Enabled = False
            End If
            If Aduana = 1 Then
                Button16.Enabled = True
            Else
                Button16.Enabled = False
            End If
            '   IdVariante = 0
            'End If
            'If IdVariante > 1 Then
            'Dim P As New dbProductos(CD.Producto.IdProducto, MySqlcon)
            'TextBox3.Text = P.Clave
            'IdInventario = 0
            'End If
            If CD.Inventario.ManejaSeries = 1 Then
                Button12.Enabled = True
            Else
                Button12.Enabled = False
            End If
            TextBox8.Text = CD.Iva
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
            TextBox12.Text = PrecioU.ToString("0.00")
            PrecioBase = PrecioU
            TextBox9.Text = CD.Descuento
            CantAnt = CD.Cantidad
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
                Button9.Enabled = True
            End If
            IdAlmacen = CD.IdAlmacen
            cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(CD.IdAlmacen)

            If CD.Descuento = 0 Then
                TextBox6.Text = CD.Precio.ToString("0.00")
            Else
                TextBox6.Text = Format(PrecioU * CD.Cantidad, "0.00")
            End If
            Button4.Text = "Modificar Concepto"
            TextBox4.Text = CD.Descripcion

            pnlUbicacion.Visible = CD.Inventario.UsaUbicacion
            cmbUbicacion.DataSource = CD.Inventario.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), IdInventario)
            cmbUbicacion.SelectedValue = CD.Ubicacion
            txtTarima.Text = CD.Tarima
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta remisión no ha sido guardada. ¿Desea iniciar una nueva remisión? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesARemisionCompra(idRemision)
                Dim c As New dbComprasRemisiones(MySqlcon)
                c.Eliminar(idRemision)
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
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesAlta, PermisosN.Secciones.Compras) = True Then
                If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim S As New dbInventarioSeries(MySqlcon)
                    S.QuitarSeriesARemisionCompraxArticulo(IdInventario, idRemision)
                    Dim CD As New dbComprasRemisionesDetalles(MySqlcon)
                    CD.Eliminar(IdDetalle)
                    ConsultaDetalles()
                    NuevoConcepto()
                    PopUp("Concepto Eliminado", 90)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub BotonProveedores()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'If B.Cliente.DireccionFiscal = 0 Then
            'TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            'Else
            'TextBox7.Text = B.Proveedor.Nombre + vbCrLf + B.Proveedor.RFC + vbCrLf + B.Proveedor.Direccion + " " + B.Proveedor.NoExterior + " " + B.Proveedor.Ciudad + " " + B.Proveedor.CP
            TextBox7.Text = B.Proveedor.RFC + " " + B.Proveedor.Nombre ' vbCrLf + p.Direccion + vbCrLf + p.Telefono
            TextBox7.Text += vbCrLf + "Límite: " + Format(B.Proveedor.LimiteCredito, "#,##0.00") + " Días: " + B.Proveedor.DiasCredito.ToString + " Saldo: " + Format(B.Proveedor.DaSaldoAFecha(B.Proveedor.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
            TextBox7.Text += vbCrLf + B.Proveedor.Direccion + " " + B.Proveedor.NoExterior + " " + B.Proveedor.Ciudad + " " + B.Proveedor.Estado + " " + B.Proveedor.CP
            'End If
            'TextBox13.Text = "Límite: " + Format(B.Proveedor.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + B.Proveedor.DiasCredito.ToString + vbCrLf + "Saldo: " + Format(B.Proveedor.DaSaldoAFecha(B.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
            idProveedor = B.Proveedor.ID
            ConsultaOn = False
            TextBox1.Text = B.Proveedor.Clave
            ConsultaOn = True
            If Op._TipoSelAlmacen <> "0" Then
                'If ComboBox8.SelectedIndex <= 0 Then
                cmbAlmacen.Focus()
                'End If
            Else
                TextBox5.Focus()
            End If
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BotonProveedores()
    End Sub
    Private Sub BotonArticulos()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
        Dim op As New dbOpciones(MySqlcon)
        If op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                LlenaDatosArticulo(B.Inventario)
                TextBox12.Focus()
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                LlenaDatosArticulo(B.Inventario)
                TextBox12.Focus()
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        BotonArticulos()
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        'Dim a As dbInventarioPrecios
        Dim Cant As Double
        TextBox4.Text = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If
        Articulo.DaUltimaidMoneda(Articulo.ID)
        Articulo.DaUltimoCosto(Articulo.ID)
        PrecioU = Math.Round(Articulo.UltimoPrecioCompra, 2)
        PrecioBase = PrecioU
        TextBox8.Text = Articulo.Iva.ToString
        ManejaSeries = Articulo.ManejaSeries
        TextBox12.Text = PrecioU.ToString("0.00")
        TextBox6.Text = Format(Cant * PrecioU, "0.00")
        ComboBox1.SelectedIndex = IDsMonedas.Busca(Articulo.idMonedaCompra)
        IdInventario = Articulo.ID
        PorLotes = Articulo.PorLotes
        Aduana = Articulo.Aduana
        txtIEPS.Text = Articulo.ieps.ToString
        txtIVARetenido.Text = Articulo.ivaRetenido.ToString
        pnlUbicacion.Visible = Articulo.UsaUbicacion
        cmbUbicacion.DataSource = Articulo.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), IdInventario)

        'IdVariante = 0
        If ManejaSeries = 0 Then
            Button12.Visible = False
        Else
            Button12.Visible = True
        End If
        ConsultaOn = False
        TextBox3.Text = Articulo.Clave
        TextBox3.Select(TextBox3.Text.Length, 0)
        ConsultaOn = True
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SacaTotal()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        SacaTotal()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmComprasRemisionesConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idRemision = f.IdRemision
            LlenaDatosVenta()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este remision?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesAlta, PermisosN.Secciones.Compras) = True Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesARemisionCompra(idRemision)
                Dim C As New dbComprasRemisiones(MySqlcon)
                C.Eliminar(idRemision)
                PopUp("Remision Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub



    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MsgBox("¿Cancelar esta remisión?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada)
            If MsgBox("¿Desea imprimir la remisión de compra?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Imprimir(idRemision)
            End If
        End If
    End Sub

    'Private Sub CadenaOriginal()
    '    Dim en As New Encriptador
    '    Dim V As New dbVentas(MySqlcon)
    '    'TextBox9.Text = 
    '    'TextBox10.Text = 
    '    'en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
    '    Dim Enc As New System.Text.UTF8Encoding
    '    Dim Bytes() As Byte = Enc.GetBytes(V.CreaXML(idCotizacion, IdMonedaG, en.GeneraSello(V.CreaCadenaOriginal(idCotizacion, IdMonedaG), My.Settings.rutakey, My.Settings.passwordkey, My.Settings.rutacer)))
    '    'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
    '    en.GuardaArchivo("XMLFac-" + V.Folio.ToString + ".xml", Bytes)
    'End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Enabled Then
                TextBox3.Focus()
            Else
                TextBox12.Focus()
            End If
        End If
    End Sub



    'Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
    '    LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
    'End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IdInventario <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim Forma As New frmBuscaDocumentoVenta(0, True, 2, IdsSucursales.Valor(ComboBox3.SelectedIndex), 2, False, True, True, 0, False, "", False)
            If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim V As New dbComprasRemisiones(MySqlcon)
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
                        Case 3
                            Dim Cv As New dbCompras(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cv.Proveedor.Clave
                    End Select
                    Guardar()
                    If Estado <> 0 Then
                        V.AgregarDetallesReferencia(idRemision, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex))
                        ConsultaDetalles()
                    End If
                Else
                    V.AgregarDetallesReferencia(idRemision, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex))
                    ConsultaDetalles()
                End If
                Button11.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub


    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If IdInventario <> 0 Then
            If IsNumeric(TextBox12.Text) Then
                PrecioU = CDbl(TextBox12.Text)
                If IsNumeric(TextBox5.Text) Then
                    TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
                End If
            End If
        End If
    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox8.Focus()
        End If
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        'If ConsultaOn Then
        '    If IsNumeric(TextBox12.Text) And IsNumeric(TextBox9.Text) Then
        '        If PrecioBase <> 0 Then
        '            TextBox12.Text = CStr(PrecioBase - (PrecioBase * CDbl(TextBox9.Text) / 100))
        '        Else
        '            TextBox12.Text = CStr(PrecioU - (PrecioU * CDbl(TextBox9.Text) / 100))
        '        End If
        '        'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
        '    End If
        'End If
    End Sub

    Private Sub txtSerie_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSerie.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFolioi.Focus()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub

    Private Sub Imprimir(pIdRemision As Integer)
        Try
            Dim Compra As New dbComprasRemisiones(pIdRemision, MySqlcon)
            ImpDoc.IdSucursal = Compra.idSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.CompraRemision
            ImpDoc.TipoDocumentoT = TiposDocumentos.CompraRemision + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.CompraRemision
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion(pIdRemision)
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "REM DE COMPRA " + Compra.Serie + Compra.Folioi.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Public Sub LlenaNodosImpresion(pIdRemision As Integer)
        Dim Cot As New dbComprasRemisiones(pIdRemision, MySqlcon)
        Dim Suc As New dbSucursales(Cot.IdSucursal, MySqlcon)
        Dim Prov As New dbproveedores(Cot.Idproveedor, MySqlcon)
        'Dim firmaChequeRecibido As String = "Firma cheque recibido"
        Cot.DaTotal(pIdRemision, Cot.idMoneda)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Cot.Folio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFolio", Cot.Serie + Cot.Folioi.ToString("0000"), 0), "docFolio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFecha", Cot.Fecha, 0), "docFecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docSubTotal", Format(Cot.Subtotal, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "docSubTotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docTotal", Format(Cot.TotalVenta, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "docTotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalieps", Format(Cot.TotalIEPS, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "doctotalieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalivaret", Format(Cot.TotalIvaRetenido, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "doctotalivaret")
        Dim TotalconLetra As String
        Dim CL As New CLetras
        If Cot.TotalVenta >= 0 Then
            TotalconLetra = CL.LetrasM(Cot.TotalVenta, Cot.idMoneda, GlobalIdiomaLetras)
        Else
            TotalconLetra = "MENOS " + CL.LetrasM(Cot.TotalVenta * -1, Cot.idMoneda, GlobalIdiomaLetras)
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docCantLetra", TotalconLetra, 0), "docCantLetra")
        If Cot.Estado = 4 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADO", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaNombre", Suc.Nombre, 0), "empresaNombre")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaCalle", Suc.Direccion, 0), "empresaCalle")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaColonia", Suc.Colonia, 0), "empresaColonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaCP", Suc.CP, 0), "empresaCP")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaRFC", Suc.RFC, 0), "empresaRFC")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaTel", Suc.Telefono, 0), "empresaTel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaTel2", Suc.Telefono, 0), "empresaTel2")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaEMail", Suc.Email, 0), "empresaEMail")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", Cot.Comentario, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provNombre", Prov.Nombre, 0), "provNombre")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provDom", Prov.Direccion, 0), "provDom")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCol", Prov.Colonia, 0), "provCol")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCP", Prov.CP, 0), "provCP")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCiudad", Prov.Ciudad, 0), "provCiudad") 'no
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provRFC", Prov.RFC, 0), "provRFC")
        ImpDoc.CuantosRenglones = 0
        Dim T As MySql.Data.MySqlClient.MySqlDataReader
        Dim CD As New dbComprasRemisionesDetalles(MySqlcon)
        T = CD.ConsultaReader(pIdRemision, 1)
        While T.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artCant", Format(T("cantidad"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "artCant" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", T("descripcion"), 0), "descripcion" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artPrecioU", Format(T("precio") / T("cantidad"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "artPrecioU" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "provImporte", Format(T("precio"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "provImporte" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artCod", T("clave"), 0), "artCod" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "medida", T("tipocantidad"), 0), "medida" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.CuantosRenglones += 1
        End While
        T.Close()
        ImpDoc.ImpND.Add(New NodoImpresionN("", "privnoInterior", Prov.NoInterior, 0), "privnoInterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provnoExterior", Prov.NoExterior, 0), "provnoExterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provEstado", Prov.Estado, 0), "provEstado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provPais", Prov.Pais, 0), "provPais")

        Dim Ivas As New Collection
        Dim Cont As Integer = 0

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim IvasImporte As New Collection
        DR = Cot.DaIvas(pIdRemision)
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

        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And Op._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If

        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
    End Sub
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Imprimir(idRemision)
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
                LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            Else
                LlenaCombos("tblalmacenes", cmbAlmacen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
            End If
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            If cmbAlmacen.Items.Count > 0 Then
                If Op._TipoSelAlmacen = "0" Then
                    cmbAlmacen.SelectedIndex = IdsAlmacenes.Busca(S.IdAlmacenC)
                Else
                    cmbAlmacen.SelectedIndex = 0
                End If
            Else
                MsgBox("Esta sucursal no cuenta con almacenes.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.IdAlmacenC)
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasRemisiones, 0)
            txtSerie.Text = Sf.Serie
            Dim V As New dbComprasRemisiones(MySqlcon)
            txtFolioi.Text = V.DaNuevoFolio(txtSerie.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(txtFolioi.Text) < Sf.FolioInicial Then
                txtFolioi.Text = Sf.FolioInicial.ToString
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmInventarioSeries(IdInventario, 0, 0, CDbl(TextBox5.Text), DateTimePicker1.Value, 0, idRemision)
        F.ShowDialog()
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        ImprimirSeries()
    End Sub

    Private Sub DateTimePicker1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox3.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolio.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged

    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolioi.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFolio.Focus()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolioi.TextChanged

    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox12.Focus()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbAlmacen.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox9.Focus()
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAlmacen.SelectedIndexChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmProveedores(1, idProveedor, "")
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.CodigoProveedor
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbComprasRemisiones(MySqlcon)
                V.ActualizaComentario(idRemision, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
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

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        If PorLotes = 1 Then
            Dim F As New frmInventarioLotes(0, 0, 0, IdDetalle, CDbl(TextBox5.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub txtIEPS_TextChanged(sender As Object, e As EventArgs) Handles txtIEPS.TextChanged

    End Sub

    Private Sub txtIVARetenido_TextChanged(sender As Object, e As EventArgs) Handles txtIVARetenido.TextChanged

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If Aduana = 1 Then
            Dim F As New frmInventarioAduana(0, 0, 0, IdDetalle, CDbl(TextBox5.Text), IdInventario, 1, 0, 0, 0, IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbAlmacen.Text, 0, 0, 0)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub cmbUbicacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUbicacion.SelectedIndexChanged
        If cmbUbicacion.SelectedIndex = -1 Then
            txtTarima.Text = ""
        Else
            Dim db As New dbAlmacenes(MySqlcon)
            txtTarima.Text = db.Tarima(IdsAlmacenes.Valor(cmbAlmacen.SelectedIndex), cmbUbicacion.SelectedValue)
        End If
        txtTarima.Enabled = txtTarima.Text = ""

    End Sub
End Class