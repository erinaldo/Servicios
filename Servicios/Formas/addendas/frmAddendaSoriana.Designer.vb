<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddendaSoriana
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.grpRemision = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRemision = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbTipoBulto = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbEntregaMercancia = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCantidadBultos = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSubtotal = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtIEPS = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtIVA = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtOtrosImpuestos = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtpFechaEntrega = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCita = New System.Windows.Forms.TextBox()
        Me.cmbMoneda = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFolioNotaEntrada = New System.Windows.Forms.TextBox()
        Me.txtTienda = New System.Windows.Forms.TextBox()
        Me.dgvPedidos = New System.Windows.Forms.DataGridView()
        Me.colFolio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTienda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvArticulos = New System.Windows.Forms.DataGridView()
        Me.colCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCantidadArt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCosto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIEPS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabGeneral = New System.Windows.Forms.TabPage()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tabPedimento = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtPedimento = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtAduana = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtAgenteAduanal = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbTipoPedimento = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.dtpFechaPedimento = New System.Windows.Forms.DateTimePicker()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.dtpFechaRecibo = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.dtpFechaBillOflanding = New System.Windows.Forms.DateTimePicker()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.grpRemision.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgvPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvArticulos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tabGeneral.SuspendLayout()
        Me.tabPedimento.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpRemision
        '
        Me.grpRemision.Controls.Add(Me.TableLayoutPanel1)
        Me.grpRemision.Location = New System.Drawing.Point(6, 6)
        Me.grpRemision.Name = "grpRemision"
        Me.grpRemision.Size = New System.Drawing.Size(346, 579)
        Me.grpRemision.TabIndex = 0
        Me.grpRemision.TabStop = False
        Me.grpRemision.Text = "Remisión"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.35294!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.64706!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtProveedor, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtRemision, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.dtpFecha, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbTipoBulto, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbEntregaMercancia, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCantidadBultos, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.txtSubtotal, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.Label11, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.txtIEPS, 1, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.Label12, 0, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.txtIVA, 1, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.Label13, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.txtOtrosImpuestos, 1, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.Label14, 0, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.txtTotal, 1, 12)
        Me.TableLayoutPanel1.Controls.Add(Me.Label15, 0, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.dtpFechaEntrega, 1, 13)
        Me.TableLayoutPanel1.Controls.Add(Me.Label16, 0, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.txtCita, 1, 14)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbMoneda, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.txtFolioNotaEntrada, 1, 15)
        Me.TableLayoutPanel1.Controls.Add(Me.txtTienda, 1, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 17
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(340, 559)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 27)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Proveedor"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtProveedor
        '
        Me.txtProveedor.Location = New System.Drawing.Point(129, 3)
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.Size = New System.Drawing.Size(180, 21)
        Me.txtProveedor.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(3, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 27)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Remisión"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemision
        '
        Me.txtRemision.Location = New System.Drawing.Point(129, 30)
        Me.txtRemision.Name = "txtRemision"
        Me.txtRemision.ReadOnly = True
        Me.txtRemision.Size = New System.Drawing.Size(180, 21)
        Me.txtRemision.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Location = New System.Drawing.Point(3, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 27)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Fecha"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFecha
        '
        Me.dtpFecha.Enabled = False
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(129, 57)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(180, 21)
        Me.dtpFecha.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Location = New System.Drawing.Point(3, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 27)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Tienda"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Location = New System.Drawing.Point(3, 108)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 29)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Moneda"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Location = New System.Drawing.Point(3, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 29)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Tipo bulto"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTipoBulto
        '
        Me.cmbTipoBulto.FormattingEnabled = True
        Me.cmbTipoBulto.Items.AddRange(New Object() {"", "CAJAS", "BOLSAS"})
        Me.cmbTipoBulto.Location = New System.Drawing.Point(129, 140)
        Me.cmbTipoBulto.Name = "cmbTipoBulto"
        Me.cmbTipoBulto.Size = New System.Drawing.Size(180, 23)
        Me.cmbTipoBulto.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(3, 166)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 29)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Entrega mercancia"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbEntregaMercancia
        '
        Me.cmbEntregaMercancia.FormattingEnabled = True
        Me.cmbEntregaMercancia.Location = New System.Drawing.Point(129, 169)
        Me.cmbEntregaMercancia.Name = "cmbEntregaMercancia"
        Me.cmbEntregaMercancia.Size = New System.Drawing.Size(180, 23)
        Me.cmbEntregaMercancia.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Location = New System.Drawing.Point(3, 195)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 27)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Cantidad de bultos"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCantidadBultos
        '
        Me.txtCantidadBultos.Location = New System.Drawing.Point(129, 198)
        Me.txtCantidadBultos.Name = "txtCantidadBultos"
        Me.txtCantidadBultos.Size = New System.Drawing.Size(180, 21)
        Me.txtCantidadBultos.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.Location = New System.Drawing.Point(3, 222)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 27)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Subtotal"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSubtotal
        '
        Me.txtSubtotal.Location = New System.Drawing.Point(129, 225)
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.ReadOnly = True
        Me.txtSubtotal.Size = New System.Drawing.Size(180, 21)
        Me.txtSubtotal.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Location = New System.Drawing.Point(3, 249)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 27)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "IEPS"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIEPS
        '
        Me.txtIEPS.Location = New System.Drawing.Point(129, 252)
        Me.txtIEPS.Name = "txtIEPS"
        Me.txtIEPS.ReadOnly = True
        Me.txtIEPS.Size = New System.Drawing.Size(180, 21)
        Me.txtIEPS.TabIndex = 9
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Location = New System.Drawing.Point(3, 276)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(120, 27)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "IVA"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIVA
        '
        Me.txtIVA.Location = New System.Drawing.Point(129, 279)
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.ReadOnly = True
        Me.txtIVA.Size = New System.Drawing.Size(180, 21)
        Me.txtIVA.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label13.Location = New System.Drawing.Point(3, 303)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 27)
        Me.Label13.TabIndex = 24
        Me.Label13.Text = "Otros impuestos"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOtrosImpuestos
        '
        Me.txtOtrosImpuestos.Location = New System.Drawing.Point(129, 306)
        Me.txtOtrosImpuestos.Name = "txtOtrosImpuestos"
        Me.txtOtrosImpuestos.ReadOnly = True
        Me.txtOtrosImpuestos.Size = New System.Drawing.Size(180, 21)
        Me.txtOtrosImpuestos.TabIndex = 11
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Location = New System.Drawing.Point(3, 330)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 27)
        Me.Label14.TabIndex = 26
        Me.Label14.Text = "Total"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(129, 333)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(180, 21)
        Me.txtTotal.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Location = New System.Drawing.Point(3, 357)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 27)
        Me.Label15.TabIndex = 28
        Me.Label15.Text = "Fecha de entrega"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFechaEntrega
        '
        Me.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaEntrega.Location = New System.Drawing.Point(129, 360)
        Me.dtpFechaEntrega.Name = "dtpFechaEntrega"
        Me.dtpFechaEntrega.Size = New System.Drawing.Size(180, 21)
        Me.dtpFechaEntrega.TabIndex = 13
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label16.Location = New System.Drawing.Point(3, 384)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 27)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "Cita"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCita
        '
        Me.txtCita.Location = New System.Drawing.Point(129, 387)
        Me.txtCita.Name = "txtCita"
        Me.txtCita.Size = New System.Drawing.Size(180, 21)
        Me.txtCita.TabIndex = 14
        '
        'cmbMoneda
        '
        Me.cmbMoneda.Enabled = False
        Me.cmbMoneda.FormattingEnabled = True
        Me.cmbMoneda.Items.AddRange(New Object() {"", "MXN", "USD", "EUR"})
        Me.cmbMoneda.Location = New System.Drawing.Point(129, 111)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(180, 23)
        Me.cmbMoneda.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 411)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 27)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Folio entrada"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFolioNotaEntrada
        '
        Me.txtFolioNotaEntrada.Location = New System.Drawing.Point(129, 414)
        Me.txtFolioNotaEntrada.Name = "txtFolioNotaEntrada"
        Me.txtFolioNotaEntrada.Size = New System.Drawing.Size(180, 21)
        Me.txtFolioNotaEntrada.TabIndex = 15
        '
        'txtTienda
        '
        Me.txtTienda.Location = New System.Drawing.Point(129, 84)
        Me.txtTienda.Name = "txtTienda"
        Me.txtTienda.Size = New System.Drawing.Size(180, 21)
        Me.txtTienda.TabIndex = 3
        '
        'dgvPedidos
        '
        Me.dgvPedidos.AllowUserToResizeRows = False
        Me.dgvPedidos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPedidos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colFolio, Me.colTienda})
        Me.dgvPedidos.Location = New System.Drawing.Point(358, 32)
        Me.dgvPedidos.MultiSelect = False
        Me.dgvPedidos.Name = "dgvPedidos"
        Me.dgvPedidos.RowHeadersVisible = False
        Me.dgvPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPedidos.Size = New System.Drawing.Size(509, 155)
        Me.dgvPedidos.TabIndex = 1
        '
        'colFolio
        '
        Me.colFolio.DataPropertyName = "FolioPedido"
        Me.colFolio.HeaderText = "Folio"
        Me.colFolio.Name = "colFolio"
        '
        'colTienda
        '
        Me.colTienda.DataPropertyName = "Tienda"
        Me.colTienda.FillWeight = 200.0!
        Me.colTienda.HeaderText = "Tienda"
        Me.colTienda.Name = "colTienda"
        '
        'dgvArticulos
        '
        Me.dgvArticulos.AllowUserToResizeRows = False
        Me.dgvArticulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvArticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvArticulos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colCodigo, Me.colCantidadArt, Me.colCosto, Me.colIVA, Me.colIEPS})
        Me.dgvArticulos.Location = New System.Drawing.Point(358, 216)
        Me.dgvArticulos.MultiSelect = False
        Me.dgvArticulos.Name = "dgvArticulos"
        Me.dgvArticulos.RowHeadersVisible = False
        Me.dgvArticulos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvArticulos.Size = New System.Drawing.Size(509, 245)
        Me.dgvArticulos.TabIndex = 2
        '
        'colCodigo
        '
        Me.colCodigo.DataPropertyName = "Codigo"
        Me.colCodigo.HeaderText = "Código"
        Me.colCodigo.Name = "colCodigo"
        '
        'colCantidadArt
        '
        Me.colCantidadArt.DataPropertyName = "CantidadUnidadCompra"
        Me.colCantidadArt.HeaderText = "Cantidad"
        Me.colCantidadArt.Name = "colCantidadArt"
        '
        'colCosto
        '
        Me.colCosto.DataPropertyName = "CostoNetoUnidadCompra"
        Me.colCosto.HeaderText = "Costo"
        Me.colCosto.Name = "colCosto"
        '
        'colIVA
        '
        Me.colIVA.DataPropertyName = "PorcentajeIVA"
        Me.colIVA.HeaderText = "Porc. IVA"
        Me.colIVA.Name = "colIVA"
        '
        'colIEPS
        '
        Me.colIEPS.DataPropertyName = "PorcentajeIEPS"
        Me.colIEPS.HeaderText = "Porc. IEPS"
        Me.colIEPS.Name = "colIEPS"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabGeneral)
        Me.TabControl1.Controls.Add(Me.tabPedimento)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(890, 493)
        Me.TabControl1.TabIndex = 1
        '
        'tabGeneral
        '
        Me.tabGeneral.Controls.Add(Me.Label18)
        Me.tabGeneral.Controls.Add(Me.Label17)
        Me.tabGeneral.Controls.Add(Me.grpRemision)
        Me.tabGeneral.Controls.Add(Me.dgvArticulos)
        Me.tabGeneral.Controls.Add(Me.dgvPedidos)
        Me.tabGeneral.Location = New System.Drawing.Point(4, 24)
        Me.tabGeneral.Name = "tabGeneral"
        Me.tabGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tabGeneral.Size = New System.Drawing.Size(882, 465)
        Me.tabGeneral.TabIndex = 0
        Me.tabGeneral.Text = "Información general"
        Me.tabGeneral.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(359, 194)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(58, 15)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Artículos"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(359, 13)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 15)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "Pedidos"
        '
        'tabPedimento
        '
        Me.tabPedimento.Controls.Add(Me.TableLayoutPanel2)
        Me.tabPedimento.Location = New System.Drawing.Point(4, 24)
        Me.tabPedimento.Name = "tabPedimento"
        Me.tabPedimento.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPedimento.Size = New System.Drawing.Size(882, 465)
        Me.tabPedimento.TabIndex = 1
        Me.tabPedimento.Text = "Pedimento"
        Me.tabPedimento.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.35294!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.64706!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label19, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.txtPedimento, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label20, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.txtAduana, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label21, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.txtAgenteAduanal, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label22, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.cmbTipoPedimento, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label23, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.dtpFechaPedimento, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label24, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.dtpFechaRecibo, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label25, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.dtpFechaBillOflanding, 1, 6)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(8, 6)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 8
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(340, 229)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label19.Location = New System.Drawing.Point(3, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(120, 27)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Número"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPedimento
        '
        Me.txtPedimento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPedimento.Location = New System.Drawing.Point(129, 3)
        Me.txtPedimento.Name = "txtPedimento"
        Me.txtPedimento.Size = New System.Drawing.Size(208, 21)
        Me.txtPedimento.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label20.Location = New System.Drawing.Point(3, 27)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(120, 27)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Aduana"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAduana
        '
        Me.txtAduana.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAduana.Location = New System.Drawing.Point(129, 30)
        Me.txtAduana.Name = "txtAduana"
        Me.txtAduana.Size = New System.Drawing.Size(208, 21)
        Me.txtAduana.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label21.Location = New System.Drawing.Point(3, 54)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(120, 27)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "Agente aduanal"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAgenteAduanal
        '
        Me.txtAgenteAduanal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAgenteAduanal.Location = New System.Drawing.Point(129, 57)
        Me.txtAgenteAduanal.Name = "txtAgenteAduanal"
        Me.txtAgenteAduanal.Size = New System.Drawing.Size(208, 21)
        Me.txtAgenteAduanal.TabIndex = 3
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label22.Location = New System.Drawing.Point(3, 81)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(120, 29)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "Tipo pedimento"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbTipoPedimento
        '
        Me.cmbTipoPedimento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbTipoPedimento.FormattingEnabled = True
        Me.cmbTipoPedimento.Location = New System.Drawing.Point(129, 84)
        Me.cmbTipoPedimento.Name = "cmbTipoPedimento"
        Me.cmbTipoPedimento.Size = New System.Drawing.Size(208, 23)
        Me.cmbTipoPedimento.TabIndex = 4
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label23.Location = New System.Drawing.Point(3, 110)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 27)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "Fecha pedimento"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFechaPedimento
        '
        Me.dtpFechaPedimento.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFechaPedimento.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaPedimento.Location = New System.Drawing.Point(129, 113)
        Me.dtpFechaPedimento.Name = "dtpFechaPedimento"
        Me.dtpFechaPedimento.Size = New System.Drawing.Size(208, 21)
        Me.dtpFechaPedimento.TabIndex = 5
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label24.Location = New System.Drawing.Point(3, 137)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(120, 27)
        Me.Label24.TabIndex = 10
        Me.Label24.Text = "Fecha recibo"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFechaRecibo
        '
        Me.dtpFechaRecibo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFechaRecibo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaRecibo.Location = New System.Drawing.Point(129, 140)
        Me.dtpFechaRecibo.Name = "dtpFechaRecibo"
        Me.dtpFechaRecibo.Size = New System.Drawing.Size(208, 21)
        Me.dtpFechaRecibo.TabIndex = 6
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label25.Location = New System.Drawing.Point(3, 164)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(120, 27)
        Me.Label25.TabIndex = 12
        Me.Label25.Text = "Fecha bill of landing"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFechaBillOflanding
        '
        Me.dtpFechaBillOflanding.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtpFechaBillOflanding.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaBillOflanding.Location = New System.Drawing.Point(129, 167)
        Me.dtpFechaBillOflanding.Name = "dtpFechaBillOflanding"
        Me.dtpFechaBillOflanding.Size = New System.Drawing.Size(208, 21)
        Me.dtpFechaBillOflanding.TabIndex = 7
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(632, 499)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(124, 31)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(762, 499)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(124, 31)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmAddendaSoriana
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(890, 538)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmAddendaSoriana"
        Me.Text = "Addenda Soriana"
        Me.grpRemision.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.dgvPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvArticulos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tabGeneral.ResumeLayout(False)
        Me.tabGeneral.PerformLayout()
        Me.tabPedimento.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpRemision As System.Windows.Forms.GroupBox
    Friend WithEvents dgvPedidos As System.Windows.Forms.DataGridView
    Friend WithEvents dgvArticulos As System.Windows.Forms.DataGridView
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtProveedor As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoBulto As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbEntregaMercancia As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtIEPS As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtIVA As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtOtrosImpuestos As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaEntrega As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCita As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabGeneral As System.Windows.Forms.TabPage
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents tabPedimento As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtPedimento As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtAduana As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtAgenteAduanal As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoPedimento As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaPedimento As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaRecibo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaBillOflanding As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCantidadBultos As System.Windows.Forms.TextBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRemision As System.Windows.Forms.TextBox
    Friend WithEvents cmbMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFolioNotaEntrada As System.Windows.Forms.TextBox
    Friend WithEvents txtTienda As System.Windows.Forms.TextBox
    Friend WithEvents colFolio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTienda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCodigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCantidadArt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCosto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIVA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIEPS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
