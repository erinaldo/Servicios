<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBancosConciliacion
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.dgvSistema = New System.Windows.Forms.DataGridView()
        Me.colSeleccion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFolio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colProveedor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCobro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colReferencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colBanco = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNoCuenta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCargo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAbono = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colIVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colLeyenda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCheque = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTotalPagosSelec = New System.Windows.Forms.TextBox()
        Me.txtTotalpagosUnSelec = New System.Windows.Forms.TextBox()
        Me.txtTotalDepUnSelec = New System.Windows.Forms.TextBox()
        Me.txtTotalDepSelec = New System.Windows.Forms.TextBox()
        Me.txtSaldoActual = New System.Windows.Forms.TextBox()
        Me.btnSeleccionar = New System.Windows.Forms.Button()
        Me.txtSaldoInicial = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbCuenta = New System.Windows.Forms.ComboBox()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.btnImportar = New System.Windows.Forms.Button()
        Me.dgvXML = New System.Windows.Forms.DataGridView()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.colFecha2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colConcepto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colReferenciaAmp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCargo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAbono2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnReporte = New System.Windows.Forms.Button()
        CType(Me.dgvSistema, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dgvXML, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(35, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 151
        Me.Label3.Text = "Desde:"
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFecha.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFecha.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(88, 7)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(84, 22)
        Me.dtpFecha.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(185, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 16)
        Me.Label1.TabIndex = 153
        Me.Label1.Text = "Hasta:"
        '
        'dtpFechaHasta
        '
        Me.dtpFechaHasta.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFechaHasta.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFechaHasta.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpFechaHasta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaHasta.Location = New System.Drawing.Point(235, 6)
        Me.dtpFechaHasta.Name = "dtpFechaHasta"
        Me.dtpFechaHasta.Size = New System.Drawing.Size(89, 22)
        Me.dtpFechaHasta.TabIndex = 2
        '
        'dgvSistema
        '
        Me.dgvSistema.AllowUserToAddRows = False
        Me.dgvSistema.AllowUserToDeleteRows = False
        Me.dgvSistema.AllowUserToResizeRows = False
        Me.dgvSistema.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSistema.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSistema.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSistema.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colSeleccion, Me.colId, Me.colFecha, Me.colFolio, Me.colProveedor, Me.colCobro, Me.colReferencia, Me.colBanco, Me.colNoCuenta, Me.colCargo, Me.colAbono, Me.colTipo, Me.colIVA, Me.colLeyenda, Me.colCheque, Me.colEstado})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSistema.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvSistema.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSistema.Location = New System.Drawing.Point(0, 0)
        Me.dgvSistema.MultiSelect = False
        Me.dgvSistema.Name = "dgvSistema"
        Me.dgvSistema.ReadOnly = True
        Me.dgvSistema.RowHeadersVisible = False
        Me.dgvSistema.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSistema.Size = New System.Drawing.Size(960, 362)
        Me.dgvSistema.TabIndex = 174
        Me.dgvSistema.TabStop = False
        '
        'colSeleccion
        '
        Me.colSeleccion.DataPropertyName = "seleccion"
        Me.colSeleccion.FalseValue = "0"
        Me.colSeleccion.HeaderText = "Sel"
        Me.colSeleccion.Name = "colSeleccion"
        Me.colSeleccion.ReadOnly = True
        Me.colSeleccion.TrueValue = "1"
        Me.colSeleccion.Width = 28
        '
        'colId
        '
        Me.colId.DataPropertyName = "id"
        Me.colId.HeaderText = "ID"
        Me.colId.Name = "colId"
        Me.colId.ReadOnly = True
        Me.colId.Visible = False
        Me.colId.Width = 43
        '
        'colFecha
        '
        Me.colFecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colFecha.DataPropertyName = "Fecha"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colFecha.DefaultCellStyle = DataGridViewCellStyle2
        Me.colFecha.HeaderText = "Fecha"
        Me.colFecha.Name = "colFecha"
        Me.colFecha.ReadOnly = True
        '
        'colFolio
        '
        Me.colFolio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colFolio.DataPropertyName = "Folio"
        Me.colFolio.HeaderText = "Folio"
        Me.colFolio.Name = "colFolio"
        Me.colFolio.ReadOnly = True
        Me.colFolio.Visible = False
        '
        'colProveedor
        '
        Me.colProveedor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colProveedor.DataPropertyName = "Proveedor"
        Me.colProveedor.HeaderText = "Proveedor"
        Me.colProveedor.Name = "colProveedor"
        Me.colProveedor.ReadOnly = True
        Me.colProveedor.Visible = False
        '
        'colCobro
        '
        Me.colCobro.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCobro.DataPropertyName = "Cobro"
        Me.colCobro.HeaderText = "Cobro"
        Me.colCobro.Name = "colCobro"
        Me.colCobro.ReadOnly = True
        Me.colCobro.Visible = False
        '
        'colReferencia
        '
        Me.colReferencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colReferencia.DataPropertyName = "Referencia"
        Me.colReferencia.FillWeight = 300.0!
        Me.colReferencia.HeaderText = "Referencia"
        Me.colReferencia.Name = "colReferencia"
        Me.colReferencia.ReadOnly = True
        '
        'colBanco
        '
        Me.colBanco.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colBanco.DataPropertyName = "Banco"
        Me.colBanco.HeaderText = "Banco"
        Me.colBanco.Name = "colBanco"
        Me.colBanco.ReadOnly = True
        '
        'colNoCuenta
        '
        Me.colNoCuenta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colNoCuenta.DataPropertyName = "NoCuenta"
        Me.colNoCuenta.HeaderText = "No. Cuenta"
        Me.colNoCuenta.Name = "colNoCuenta"
        Me.colNoCuenta.ReadOnly = True
        '
        'colCargo
        '
        Me.colCargo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colCargo.DataPropertyName = "Cargo"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        Me.colCargo.DefaultCellStyle = DataGridViewCellStyle3
        Me.colCargo.HeaderText = "Cargo"
        Me.colCargo.Name = "colCargo"
        Me.colCargo.ReadOnly = True
        Me.colCargo.Width = 120
        '
        'colAbono
        '
        Me.colAbono.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colAbono.DataPropertyName = "Abono"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "C2"
        Me.colAbono.DefaultCellStyle = DataGridViewCellStyle4
        Me.colAbono.HeaderText = "Abono"
        Me.colAbono.Name = "colAbono"
        Me.colAbono.ReadOnly = True
        Me.colAbono.Width = 120
        '
        'colTipo
        '
        Me.colTipo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colTipo.DataPropertyName = "Tipo"
        Me.colTipo.HeaderText = "Tipo"
        Me.colTipo.Name = "colTipo"
        Me.colTipo.ReadOnly = True
        Me.colTipo.Visible = False
        '
        'colIVA
        '
        Me.colIVA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colIVA.DataPropertyName = "IVA"
        Me.colIVA.HeaderText = "IVA"
        Me.colIVA.Name = "colIVA"
        Me.colIVA.ReadOnly = True
        Me.colIVA.Visible = False
        '
        'colLeyenda
        '
        Me.colLeyenda.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colLeyenda.DataPropertyName = "Leyenda"
        Me.colLeyenda.HeaderText = "Leyenda"
        Me.colLeyenda.Name = "colLeyenda"
        Me.colLeyenda.ReadOnly = True
        Me.colLeyenda.Visible = False
        '
        'colCheque
        '
        Me.colCheque.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colCheque.DataPropertyName = "EsCheque"
        Me.colCheque.HeaderText = "Cheque"
        Me.colCheque.Name = "colCheque"
        Me.colCheque.ReadOnly = True
        Me.colCheque.Visible = False
        '
        'colEstado
        '
        Me.colEstado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEstado.DataPropertyName = "Estado"
        Me.colEstado.HeaderText = "Estado"
        Me.colEstado.Name = "colEstado"
        Me.colEstado.ReadOnly = True
        Me.colEstado.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(289, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 19)
        Me.Label5.TabIndex = 158
        Me.Label5.Text = "Saldo actual:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(587, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 16)
        Me.Label2.TabIndex = 157
        Me.Label2.Text = "Total sin seleccionar:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(598, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 16)
        Me.Label4.TabIndex = 156
        Me.Label4.Text = "Total seleccionado:"
        '
        'txtTotalPagosSelec
        '
        Me.txtTotalPagosSelec.BackColor = System.Drawing.Color.White
        Me.txtTotalPagosSelec.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPagosSelec.Location = New System.Drawing.Point(723, 5)
        Me.txtTotalPagosSelec.Name = "txtTotalPagosSelec"
        Me.txtTotalPagosSelec.ReadOnly = True
        Me.txtTotalPagosSelec.Size = New System.Drawing.Size(103, 22)
        Me.txtTotalPagosSelec.TabIndex = 9
        Me.txtTotalPagosSelec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalpagosUnSelec
        '
        Me.txtTotalpagosUnSelec.BackColor = System.Drawing.Color.White
        Me.txtTotalpagosUnSelec.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalpagosUnSelec.Location = New System.Drawing.Point(723, 33)
        Me.txtTotalpagosUnSelec.Name = "txtTotalpagosUnSelec"
        Me.txtTotalpagosUnSelec.ReadOnly = True
        Me.txtTotalpagosUnSelec.Size = New System.Drawing.Size(103, 22)
        Me.txtTotalpagosUnSelec.TabIndex = 10
        Me.txtTotalpagosUnSelec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalDepUnSelec
        '
        Me.txtTotalDepUnSelec.BackColor = System.Drawing.Color.White
        Me.txtTotalDepUnSelec.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDepUnSelec.Location = New System.Drawing.Point(832, 33)
        Me.txtTotalDepUnSelec.Name = "txtTotalDepUnSelec"
        Me.txtTotalDepUnSelec.ReadOnly = True
        Me.txtTotalDepUnSelec.Size = New System.Drawing.Size(108, 22)
        Me.txtTotalDepUnSelec.TabIndex = 12
        Me.txtTotalDepUnSelec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalDepSelec
        '
        Me.txtTotalDepSelec.BackColor = System.Drawing.Color.White
        Me.txtTotalDepSelec.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDepSelec.Location = New System.Drawing.Point(832, 5)
        Me.txtTotalDepSelec.Name = "txtTotalDepSelec"
        Me.txtTotalDepSelec.ReadOnly = True
        Me.txtTotalDepSelec.Size = New System.Drawing.Size(108, 22)
        Me.txtTotalDepSelec.TabIndex = 11
        Me.txtTotalDepSelec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSaldoActual
        '
        Me.txtSaldoActual.BackColor = System.Drawing.Color.White
        Me.txtSaldoActual.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaldoActual.ForeColor = System.Drawing.Color.Black
        Me.txtSaldoActual.Location = New System.Drawing.Point(404, 10)
        Me.txtSaldoActual.Name = "txtSaldoActual"
        Me.txtSaldoActual.ReadOnly = True
        Me.txtSaldoActual.Size = New System.Drawing.Size(163, 26)
        Me.txtSaldoActual.TabIndex = 14
        Me.txtSaldoActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnSeleccionar.BackColor = System.Drawing.Color.FromArgb(CType(CType(189, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(189, Byte), Integer))
        Me.btnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSeleccionar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSeleccionar.Location = New System.Drawing.Point(501, 34)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(135, 23)
        Me.btnSeleccionar.TabIndex = 8
        Me.btnSeleccionar.Text = "Seleccionar todo"
        Me.btnSeleccionar.UseVisualStyleBackColor = False
        '
        'txtSaldoInicial
        '
        Me.txtSaldoInicial.BackColor = System.Drawing.Color.White
        Me.txtSaldoInicial.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaldoInicial.Location = New System.Drawing.Point(115, 9)
        Me.txtSaldoInicial.Name = "txtSaldoInicial"
        Me.txtSaldoInicial.ReadOnly = True
        Me.txtSaldoInicial.Size = New System.Drawing.Size(157, 26)
        Me.txtSaldoInicial.TabIndex = 13
        Me.txtSaldoInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 18)
        Me.Label8.TabIndex = 165
        Me.Label8.Text = "Saldo inicial:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(29, 35)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 16)
        Me.Label9.TabIndex = 169
        Me.Label9.Text = "Cuenta:"
        '
        'cmbCuenta
        '
        Me.cmbCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCuenta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCuenta.FormattingEnabled = True
        Me.cmbCuenta.Location = New System.Drawing.Point(92, 34)
        Me.cmbCuenta.Name = "cmbCuenta"
        Me.cmbCuenta.Size = New System.Drawing.Size(313, 24)
        Me.cmbCuenta.TabIndex = 5
        '
        'btnBuscar
        '
        Me.btnBuscar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.Location = New System.Drawing.Point(411, 34)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(75, 25)
        Me.btnBuscar.TabIndex = 170
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 65)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(962, 515)
        Me.SplitContainer1.SplitterDistance = 433
        Me.SplitContainer1.TabIndex = 175
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.dgvSistema)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtTotalDepSelec)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtTotalDepUnSelec)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtSaldoActual)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtTotalpagosUnSelec)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtSaldoInicial)
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtTotalPagosSelec)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label5)
        Me.SplitContainer2.Size = New System.Drawing.Size(960, 431)
        Me.SplitContainer2.SplitterDistance = 362
        Me.SplitContainer2.TabIndex = 175
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.IsSplitterFixed = True
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnImportar)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.dgvXML)
        Me.SplitContainer3.Size = New System.Drawing.Size(960, 76)
        Me.SplitContainer3.SplitterDistance = 30
        Me.SplitContainer3.TabIndex = 0
        '
        'btnImportar
        '
        Me.btnImportar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImportar.Location = New System.Drawing.Point(3, 3)
        Me.btnImportar.Name = "btnImportar"
        Me.btnImportar.Size = New System.Drawing.Size(106, 25)
        Me.btnImportar.TabIndex = 171
        Me.btnImportar.Text = "Importar XML"
        Me.btnImportar.UseVisualStyleBackColor = True
        '
        'dgvXML
        '
        Me.dgvXML.AllowUserToAddRows = False
        Me.dgvXML.AllowUserToDeleteRows = False
        Me.dgvXML.AllowUserToResizeRows = False
        Me.dgvXML.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvXML.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvXML.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvXML.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.colFecha2, Me.colConcepto, Me.DataGridViewTextBoxColumn6, Me.colReferenciaAmp, Me.colCargo2, Me.colAbono2})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvXML.DefaultCellStyle = DataGridViewCellStyle10
        Me.dgvXML.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvXML.Location = New System.Drawing.Point(0, 0)
        Me.dgvXML.MultiSelect = False
        Me.dgvXML.Name = "dgvXML"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvXML.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvXML.RowHeadersVisible = False
        Me.dgvXML.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvXML.Size = New System.Drawing.Size(960, 42)
        Me.dgvXML.TabIndex = 175
        Me.dgvXML.TabStop = False
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "seleccion"
        Me.DataGridViewCheckBoxColumn1.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Sel"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.TrueValue = "1"
        Me.DataGridViewCheckBoxColumn1.Width = 28
        '
        'colFecha2
        '
        Me.colFecha2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colFecha2.DataPropertyName = "Fecha"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "d/MM/yyyy"
        Me.colFecha2.DefaultCellStyle = DataGridViewCellStyle7
        Me.colFecha2.HeaderText = "Fecha"
        Me.colFecha2.Name = "colFecha2"
        Me.colFecha2.ReadOnly = True
        Me.colFecha2.Width = 120
        '
        'colConcepto
        '
        Me.colConcepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colConcepto.DataPropertyName = "Concepto"
        Me.colConcepto.HeaderText = "Concepto"
        Me.colConcepto.Name = "colConcepto"
        Me.colConcepto.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Referencia"
        Me.DataGridViewTextBoxColumn6.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn6.HeaderText = "Referencia"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'colReferenciaAmp
        '
        Me.colReferenciaAmp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colReferenciaAmp.DataPropertyName = "ReferenciaAmpliada"
        Me.colReferenciaAmp.FillWeight = 300.0!
        Me.colReferenciaAmp.HeaderText = "Referencia ampliada"
        Me.colReferenciaAmp.Name = "colReferenciaAmp"
        Me.colReferenciaAmp.ReadOnly = True
        '
        'colCargo2
        '
        Me.colCargo2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colCargo2.DataPropertyName = "Cargo"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "C2"
        Me.colCargo2.DefaultCellStyle = DataGridViewCellStyle8
        Me.colCargo2.HeaderText = "Cargo"
        Me.colCargo2.Name = "colCargo2"
        Me.colCargo2.ReadOnly = True
        Me.colCargo2.Width = 120
        '
        'colAbono2
        '
        Me.colAbono2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.colAbono2.DataPropertyName = "Abono"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "C2"
        Me.colAbono2.DefaultCellStyle = DataGridViewCellStyle9
        Me.colAbono2.HeaderText = "Abono"
        Me.colAbono2.Name = "colAbono2"
        Me.colAbono2.ReadOnly = True
        Me.colAbono2.Width = 120
        '
        'btnReporte
        '
        Me.btnReporte.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReporte.Location = New System.Drawing.Point(655, 33)
        Me.btnReporte.Name = "btnReporte"
        Me.btnReporte.Size = New System.Drawing.Size(152, 25)
        Me.btnReporte.TabIndex = 176
        Me.btnReporte.Text = "Reporte no sel."
        Me.btnReporte.UseVisualStyleBackColor = True
        '
        'frmBancosConciliacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(986, 592)
        Me.Controls.Add(Me.btnReporte)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.cmbCuenta)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpFechaHasta)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpFecha)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBancosConciliacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Conciliación"
        CType(Me.dgvSistema, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dgvXML, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPagosSelec As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalpagosUnSelec As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalDepUnSelec As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalDepSelec As System.Windows.Forms.TextBox
    Friend WithEvents txtSaldoActual As System.Windows.Forms.TextBox
    Public WithEvents dgvSistema As System.Windows.Forms.DataGridView
    Friend WithEvents btnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents txtSaldoInicial As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbCuenta As System.Windows.Forms.ComboBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnImportar As System.Windows.Forms.Button
    Public WithEvents dgvXML As System.Windows.Forms.DataGridView
    Friend WithEvents colSeleccion As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFolio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colProveedor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCobro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colReferencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBanco As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colNoCuenta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCargo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAbono As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTipo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colIVA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLeyenda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCheque As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEstado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colFecha2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colConcepto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colReferenciaAmp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCargo2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colAbono2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnReporte As System.Windows.Forms.Button
End Class
