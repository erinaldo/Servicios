<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProveedoresDocumentos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.chkMostrarGuardades = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbMonedaGral = New System.Windows.Forms.ComboBox()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.dgvDocumentos = New System.Windows.Forms.DataGridView()
        Me.colId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coSerie = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFolio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colImporte = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.moneda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tipodecambio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSestado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlDatos = New System.Windows.Forms.Panel()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.lblReferencia = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbMoneda = New System.Windows.Forms.ComboBox()
        Me.txtImporte = New System.Windows.Forms.TextBox()
        Me.lblSerieReferencia = New System.Windows.Forms.Label()
        Me.txtFolioReferencia = New System.Windows.Forms.TextBox()
        Me.txtSerieReferencia = New System.Windows.Forms.TextBox()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblFolioReferencia = New System.Windows.Forms.Label()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.pnlProveedor = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.txtDatosProveedor = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDatosProveedor2 = New System.Windows.Forms.TextBox()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvDocumentos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDatos.SuspendLayout()
        Me.pnlProveedor.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkMostrarGuardades
        '
        Me.chkMostrarGuardades.AutoSize = True
        Me.chkMostrarGuardades.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMostrarGuardades.Location = New System.Drawing.Point(158, 426)
        Me.chkMostrarGuardades.Name = "chkMostrarGuardades"
        Me.chkMostrarGuardades.Size = New System.Drawing.Size(147, 20)
        Me.chkMostrarGuardades.TabIndex = 18
        Me.chkMostrarGuardades.Text = "Mostrar Guardadas"
        Me.chkMostrarGuardades.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(749, 42)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(113, 16)
        Me.Label25.TabIndex = 355
        Me.Label25.Text = "Tipo de Cambio:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblTotal)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.cmbMonedaGral)
        Me.Panel2.Controls.Add(Me.btnGuardar)
        Me.Panel2.Location = New System.Drawing.Point(333, 425)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(562, 78)
        Me.Panel2.TabIndex = 19
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(238, 6)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(166, 22)
        Me.lblTotal.TabIndex = 37
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(168, 6)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 22)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Total:"
        '
        'cmbMonedaGral
        '
        Me.cmbMonedaGral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonedaGral.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMonedaGral.FormattingEnabled = True
        Me.cmbMonedaGral.Location = New System.Drawing.Point(410, 4)
        Me.cmbMonedaGral.Name = "cmbMonedaGral"
        Me.cmbMonedaGral.Size = New System.Drawing.Size(121, 24)
        Me.cmbMonedaGral.TabIndex = 20
        Me.cmbMonedaGral.TabStop = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Enabled = False
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(17, 31)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(174, 43)
        Me.btnGuardar.TabIndex = 21
        Me.btnGuardar.Text = "Guardar (F10)"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoCambio.Location = New System.Drawing.Point(755, 58)
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.Size = New System.Drawing.Size(98, 22)
        Me.txtTipoCambio.TabIndex = 7
        '
        'btnAgregar
        '
        Me.btnAgregar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.Location = New System.Drawing.Point(736, 143)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(155, 34)
        Me.btnAgregar.TabIndex = 15
        Me.btnAgregar.Text = "Agregar Documento"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(14, 15)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(67, 16)
        Me.Label22.TabIndex = 354
        Me.Label22.Text = "Sucursal:"
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(736, 182)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(155, 29)
        Me.btnNuevo.TabIndex = 16
        Me.btnNuevo.Text = "Nuevo Documento"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(83, 11)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(304, 24)
        Me.cmbSucursal.TabIndex = 1
        '
        'dgvDocumentos
        '
        Me.dgvDocumentos.AllowUserToAddRows = False
        Me.dgvDocumentos.AllowUserToDeleteRows = False
        Me.dgvDocumentos.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvDocumentos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDocumentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDocumentos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colId, Me.colTipo, Me.colFecha, Me.coSerie, Me.colFolio, Me.colImporte, Me.moneda, Me.tipodecambio, Me.colSestado})
        Me.dgvDocumentos.Location = New System.Drawing.Point(7, 230)
        Me.dgvDocumentos.Name = "dgvDocumentos"
        Me.dgvDocumentos.ReadOnly = True
        Me.dgvDocumentos.RowHeadersVisible = False
        Me.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDocumentos.Size = New System.Drawing.Size(888, 185)
        Me.dgvDocumentos.TabIndex = 349
        '
        'colId
        '
        Me.colId.DataPropertyName = "iddocumento"
        Me.colId.HeaderText = "iddocumento"
        Me.colId.Name = "colId"
        Me.colId.ReadOnly = True
        Me.colId.Visible = False
        '
        'colTipo
        '
        Me.colTipo.DataPropertyName = "tiposaldostr"
        Me.colTipo.HeaderText = "Tipo"
        Me.colTipo.Name = "colTipo"
        Me.colTipo.ReadOnly = True
        '
        'colFecha
        '
        Me.colFecha.DataPropertyName = "fecha"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colFecha.DefaultCellStyle = DataGridViewCellStyle2
        Me.colFecha.HeaderText = "Fecha"
        Me.colFecha.Name = "colFecha"
        Me.colFecha.ReadOnly = True
        '
        'coSerie
        '
        Me.coSerie.DataPropertyName = "seriereferencia"
        Me.coSerie.HeaderText = "Serie"
        Me.coSerie.Name = "coSerie"
        Me.coSerie.ReadOnly = True
        '
        'colFolio
        '
        Me.colFolio.DataPropertyName = "folioreferencia"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colFolio.DefaultCellStyle = DataGridViewCellStyle3
        Me.colFolio.HeaderText = "Folio"
        Me.colFolio.Name = "colFolio"
        Me.colFolio.ReadOnly = True
        '
        'colImporte
        '
        Me.colImporte.DataPropertyName = "totalapagar"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "C2"
        Me.colImporte.DefaultCellStyle = DataGridViewCellStyle4
        Me.colImporte.HeaderText = "Importe"
        Me.colImporte.Name = "colImporte"
        Me.colImporte.ReadOnly = True
        '
        'moneda
        '
        Me.moneda.DataPropertyName = "moneda"
        Me.moneda.HeaderText = "Moneda"
        Me.moneda.Name = "moneda"
        Me.moneda.ReadOnly = True
        '
        'tipodecambio
        '
        Me.tipodecambio.DataPropertyName = "tipodecambio"
        Me.tipodecambio.HeaderText = "T.C."
        Me.tipodecambio.Name = "tipodecambio"
        Me.tipodecambio.ReadOnly = True
        '
        'colSestado
        '
        Me.colSestado.DataPropertyName = "sestado"
        Me.colSestado.HeaderText = "Estado"
        Me.colSestado.Name = "colSestado"
        Me.colSestado.ReadOnly = True
        '
        'pnlDatos
        '
        Me.pnlDatos.Controls.Add(Me.TextBox4)
        Me.pnlDatos.Controls.Add(Me.Label9)
        Me.pnlDatos.Controls.Add(Me.TextBox3)
        Me.pnlDatos.Controls.Add(Me.Label8)
        Me.pnlDatos.Controls.Add(Me.TextBox2)
        Me.pnlDatos.Controls.Add(Me.Label6)
        Me.pnlDatos.Controls.Add(Me.Label5)
        Me.pnlDatos.Controls.Add(Me.TextBox1)
        Me.pnlDatos.Controls.Add(Me.Label1)
        Me.pnlDatos.Controls.Add(Me.cmbTipo)
        Me.pnlDatos.Controls.Add(Me.lblReferencia)
        Me.pnlDatos.Controls.Add(Me.Label10)
        Me.pnlDatos.Controls.Add(Me.Label7)
        Me.pnlDatos.Controls.Add(Me.cmbMoneda)
        Me.pnlDatos.Controls.Add(Me.txtImporte)
        Me.pnlDatos.Controls.Add(Me.lblSerieReferencia)
        Me.pnlDatos.Controls.Add(Me.txtFolioReferencia)
        Me.pnlDatos.Controls.Add(Me.txtSerieReferencia)
        Me.pnlDatos.Controls.Add(Me.dtpFecha)
        Me.pnlDatos.Controls.Add(Me.Label4)
        Me.pnlDatos.Controls.Add(Me.lblFolioReferencia)
        Me.pnlDatos.Location = New System.Drawing.Point(7, 128)
        Me.pnlDatos.Name = "pnlDatos"
        Me.pnlDatos.Size = New System.Drawing.Size(723, 97)
        Me.pnlDatos.TabIndex = 8
        '
        'TextBox4
        '
        Me.TextBox4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(678, 71)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(39, 22)
        Me.TextBox4.TabIndex = 17
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(606, 73)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 16)
        Me.Label9.TabIndex = 351
        Me.Label9.Text = "% Iva Ret:"
        '
        'TextBox3
        '
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(564, 71)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(39, 22)
        Me.TextBox3.TabIndex = 16
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(506, 74)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 16)
        Me.Label8.TabIndex = 349
        Me.Label8.Text = "% IEPS:"
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(463, 70)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(39, 22)
        Me.TextBox2.TabIndex = 15
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(415, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 16)
        Me.Label6.TabIndex = 347
        Me.Label6.Text = "% Iva:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(39, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 345
        Me.Label5.Text = "Concepto:"
        '
        'TextBox1
        '
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(115, 70)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(297, 22)
        Me.TextBox1.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(69, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 343
        Me.Label1.Text = "Tipo:"
        '
        'cmbTipo
        '
        Me.cmbTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Location = New System.Drawing.Point(33, 42)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(122, 24)
        Me.cmbTipo.TabIndex = 9
        '
        'lblReferencia
        '
        Me.lblReferencia.AutoSize = True
        Me.lblReferencia.BackColor = System.Drawing.Color.Transparent
        Me.lblReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReferencia.Location = New System.Drawing.Point(294, 9)
        Me.lblReferencia.Name = "lblReferencia"
        Me.lblReferencia.Size = New System.Drawing.Size(81, 16)
        Me.lblReferencia.TabIndex = 341
        Me.lblReferencia.Text = "Referencia:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(573, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 16)
        Me.Label10.TabIndex = 33
        Me.Label10.Text = "Moneda:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(465, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(61, 16)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Importe:"
        '
        'cmbMoneda
        '
        Me.cmbMoneda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbMoneda.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMoneda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMoneda.FormattingEnabled = True
        Me.cmbMoneda.Location = New System.Drawing.Point(569, 41)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(90, 24)
        Me.cmbMoneda.TabIndex = 18
        '
        'txtImporte
        '
        Me.txtImporte.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporte.Location = New System.Drawing.Point(462, 42)
        Me.txtImporte.Name = "txtImporte"
        Me.txtImporte.Size = New System.Drawing.Size(100, 22)
        Me.txtImporte.TabIndex = 13
        Me.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSerieReferencia
        '
        Me.lblSerieReferencia.AutoSize = True
        Me.lblSerieReferencia.BackColor = System.Drawing.Color.Transparent
        Me.lblSerieReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerieReferencia.Location = New System.Drawing.Point(258, 25)
        Me.lblSerieReferencia.Name = "lblSerieReferencia"
        Me.lblSerieReferencia.Size = New System.Drawing.Size(46, 16)
        Me.lblSerieReferencia.TabIndex = 340
        Me.lblSerieReferencia.Text = "Serie:"
        '
        'txtFolioReferencia
        '
        Me.txtFolioReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolioReferencia.Location = New System.Drawing.Point(358, 42)
        Me.txtFolioReferencia.Name = "txtFolioReferencia"
        Me.txtFolioReferencia.Size = New System.Drawing.Size(98, 22)
        Me.txtFolioReferencia.TabIndex = 12
        Me.txtFolioReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSerieReferencia
        '
        Me.txtSerieReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSerieReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerieReferencia.Location = New System.Drawing.Point(260, 42)
        Me.txtSerieReferencia.Name = "txtSerieReferencia"
        Me.txtSerieReferencia.Size = New System.Drawing.Size(91, 22)
        Me.txtSerieReferencia.TabIndex = 11
        '
        'dtpFecha
        '
        Me.dtpFecha.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha.Location = New System.Drawing.Point(162, 42)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(88, 22)
        Me.dtpFecha.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(161, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 16)
        Me.Label4.TabIndex = 335
        Me.Label4.Text = "Fecha:"
        '
        'lblFolioReferencia
        '
        Me.lblFolioReferencia.AutoSize = True
        Me.lblFolioReferencia.BackColor = System.Drawing.Color.Transparent
        Me.lblFolioReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolioReferencia.Location = New System.Drawing.Point(359, 26)
        Me.lblFolioReferencia.Name = "lblFolioReferencia"
        Me.lblFolioReferencia.Size = New System.Drawing.Size(44, 16)
        Me.lblFolioReferencia.TabIndex = 336
        Me.lblFolioReferencia.Text = "Folio:"
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(807, 4)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(88, 25)
        Me.btnCerrar.TabIndex = 22
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(7, 421)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(138, 25)
        Me.btnEliminar.TabIndex = 17
        Me.btnEliminar.Text = "Eliminar Documento"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'pnlProveedor
        '
        Me.pnlProveedor.Controls.Add(Me.Label2)
        Me.pnlProveedor.Controls.Add(Me.txtProveedor)
        Me.pnlProveedor.Controls.Add(Me.btnBuscarProveedor)
        Me.pnlProveedor.Controls.Add(Me.txtDatosProveedor)
        Me.pnlProveedor.Controls.Add(Me.Label3)
        Me.pnlProveedor.Controls.Add(Me.txtDatosProveedor2)
        Me.pnlProveedor.Location = New System.Drawing.Point(7, 40)
        Me.pnlProveedor.Name = "pnlProveedor"
        Me.pnlProveedor.Size = New System.Drawing.Size(723, 81)
        Me.pnlProveedor.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 16)
        Me.Label2.TabIndex = 333
        Me.Label2.Text = "Proveedor:"
        '
        'txtProveedor
        '
        Me.txtProveedor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProveedor.Location = New System.Drawing.Point(6, 28)
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.Size = New System.Drawing.Size(107, 22)
        Me.txtProveedor.TabIndex = 3
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(119, 27)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(45, 23)
        Me.btnBuscarProveedor.TabIndex = 4
        Me.btnBuscarProveedor.Text = "..."
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'txtDatosProveedor
        '
        Me.txtDatosProveedor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatosProveedor.Location = New System.Drawing.Point(170, 12)
        Me.txtDatosProveedor.Multiline = True
        Me.txtDatosProveedor.Name = "txtDatosProveedor"
        Me.txtDatosProveedor.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDatosProveedor.Size = New System.Drawing.Size(358, 64)
        Me.txtDatosProveedor.TabIndex = 5
        Me.txtDatosProveedor.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(27, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 16)
        Me.Label3.TabIndex = 334
        Me.Label3.Text = "Datos del Proveedor:"
        '
        'txtDatosProveedor2
        '
        Me.txtDatosProveedor2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatosProveedor2.Location = New System.Drawing.Point(534, 12)
        Me.txtDatosProveedor2.Multiline = True
        Me.txtDatosProveedor2.Name = "txtDatosProveedor2"
        Me.txtDatosProveedor2.Size = New System.Drawing.Size(177, 64)
        Me.txtDatosProveedor2.TabIndex = 6
        Me.txtDatosProveedor2.TabStop = False
        '
        'frmProveedoresDocumentos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(902, 512)
        Me.Controls.Add(Me.chkMostrarGuardades)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtTipoCambio)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.cmbSucursal)
        Me.Controls.Add(Me.dgvDocumentos)
        Me.Controls.Add(Me.pnlDatos)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.pnlProveedor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProveedoresDocumentos"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Captura de saldos de proveedores"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvDocumentos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDatos.ResumeLayout(False)
        Me.pnlDatos.PerformLayout()
        Me.pnlProveedor.ResumeLayout(False)
        Me.pnlProveedor.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkMostrarGuardades As System.Windows.Forms.CheckBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbMonedaGral As System.Windows.Forms.ComboBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents txtTipoCambio As System.Windows.Forms.TextBox
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents dgvDocumentos As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDatos As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents lblReferencia As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents txtImporte As System.Windows.Forms.TextBox
    Friend WithEvents lblSerieReferencia As System.Windows.Forms.Label
    Friend WithEvents txtFolioReferencia As System.Windows.Forms.TextBox
    Friend WithEvents txtSerieReferencia As System.Windows.Forms.TextBox
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblFolioReferencia As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents pnlProveedor As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtProveedor As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarProveedor As System.Windows.Forms.Button
    Friend WithEvents txtDatosProveedor As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDatosProveedor2 As System.Windows.Forms.TextBox
    Friend WithEvents colId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTipo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coSerie As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFolio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colImporte As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents moneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tipodecambio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSestado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label

End Class
