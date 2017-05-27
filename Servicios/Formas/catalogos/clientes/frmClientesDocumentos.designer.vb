<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClientesDocumentos
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbMonedaGral = New System.Windows.Forms.ComboBox()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtDatosCliente2 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.lblSerieReferencia = New System.Windows.Forms.Label()
        Me.txtSerieReferencia = New System.Windows.Forms.TextBox()
        Me.lblFolioReferencia = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgvDocumentos = New System.Windows.Forms.DataGridView()
        Me.colId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coSerie = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colFolio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colImporte = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Moneda = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Tipodecambio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSestado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlDatos = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.lblReferencia = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbMoneda = New System.Windows.Forms.ComboBox()
        Me.txtImporte = New System.Windows.Forms.TextBox()
        Me.txtFolioReferencia = New System.Windows.Forms.TextBox()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.txtDatosCliente = New System.Windows.Forms.TextBox()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.chkMostrarGuardades = New System.Windows.Forms.CheckBox()
        Me.pnlCliente = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvDocumentos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDatos.SuspendLayout()
        Me.pnlCliente.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblTotal)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.cmbMonedaGral)
        Me.Panel2.Controls.Add(Me.btnGuardar)
        Me.Panel2.Location = New System.Drawing.Point(312, 389)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(590, 78)
        Me.Panel2.TabIndex = 18
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(287, 6)
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
        Me.Label16.Location = New System.Drawing.Point(217, 6)
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
        Me.cmbMonedaGral.Location = New System.Drawing.Point(459, 4)
        Me.cmbMonedaGral.Name = "cmbMonedaGral"
        Me.cmbMonedaGral.Size = New System.Drawing.Size(121, 24)
        Me.cmbMonedaGral.TabIndex = 20
        Me.cmbMonedaGral.TabStop = False
        '
        'btnGuardar
        '
        Me.btnGuardar.Enabled = False
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(67, 31)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(174, 43)
        Me.btnGuardar.TabIndex = 19
        Me.btnGuardar.Text = "Guardar (F10)"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtDatosCliente2
        '
        Me.txtDatosCliente2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatosCliente2.Location = New System.Drawing.Point(534, 12)
        Me.txtDatosCliente2.Multiline = True
        Me.txtDatosCliente2.Name = "txtDatosCliente2"
        Me.txtDatosCliente2.Size = New System.Drawing.Size(177, 64)
        Me.txtDatosCliente2.TabIndex = 7
        Me.txtDatosCliente2.TabStop = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(22, 8)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(67, 16)
        Me.Label22.TabIndex = 342
        Me.Label22.Text = "Sucursal:"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(91, 4)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(304, 24)
        Me.cmbSucursal.TabIndex = 1
        '
        'lblSerieReferencia
        '
        Me.lblSerieReferencia.AutoSize = True
        Me.lblSerieReferencia.BackColor = System.Drawing.Color.Transparent
        Me.lblSerieReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerieReferencia.Location = New System.Drawing.Point(287, 25)
        Me.lblSerieReferencia.Name = "lblSerieReferencia"
        Me.lblSerieReferencia.Size = New System.Drawing.Size(46, 16)
        Me.lblSerieReferencia.TabIndex = 340
        Me.lblSerieReferencia.Text = "Serie:"
        '
        'txtSerieReferencia
        '
        Me.txtSerieReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerieReferencia.Location = New System.Drawing.Point(289, 42)
        Me.txtSerieReferencia.Name = "txtSerieReferencia"
        Me.txtSerieReferencia.Size = New System.Drawing.Size(91, 22)
        Me.txtSerieReferencia.TabIndex = 11
        '
        'lblFolioReferencia
        '
        Me.lblFolioReferencia.AutoSize = True
        Me.lblFolioReferencia.BackColor = System.Drawing.Color.Transparent
        Me.lblFolioReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolioReferencia.Location = New System.Drawing.Point(388, 26)
        Me.lblFolioReferencia.Name = "lblFolioReferencia"
        Me.lblFolioReferencia.Size = New System.Drawing.Size(44, 16)
        Me.lblFolioReferencia.TabIndex = 336
        Me.lblFolioReferencia.Text = "Folio:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(190, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 16)
        Me.Label4.TabIndex = 335
        Me.Label4.Text = "Fecha:"
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
        Me.dgvDocumentos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colId, Me.colTipo, Me.colFecha, Me.coSerie, Me.colFolio, Me.colImporte, Me.Moneda, Me.Tipodecambio, Me.colSestado})
        Me.dgvDocumentos.Location = New System.Drawing.Point(15, 198)
        Me.dgvDocumentos.Name = "dgvDocumentos"
        Me.dgvDocumentos.ReadOnly = True
        Me.dgvDocumentos.RowHeadersVisible = False
        Me.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDocumentos.Size = New System.Drawing.Size(888, 185)
        Me.dgvDocumentos.TabIndex = 6
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
        'Moneda
        '
        Me.Moneda.DataPropertyName = "moneda"
        Me.Moneda.HeaderText = "Moneda"
        Me.Moneda.Name = "Moneda"
        Me.Moneda.ReadOnly = True
        '
        'Tipodecambio
        '
        Me.Tipodecambio.DataPropertyName = "tipodecambio"
        Me.Tipodecambio.HeaderText = "T.C."
        Me.Tipodecambio.Name = "Tipodecambio"
        Me.Tipodecambio.ReadOnly = True
        '
        'colSestado
        '
        Me.colSestado.DataPropertyName = "sestado"
        Me.colSestado.HeaderText = "Estado"
        Me.colSestado.Name = "colSestado"
        Me.colSestado.ReadOnly = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(27, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 16)
        Me.Label3.TabIndex = 334
        Me.Label3.Text = "Datos del Cliente:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 16)
        Me.Label2.TabIndex = 333
        Me.Label2.Text = "Cliente:"
        '
        'pnlDatos
        '
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
        Me.pnlDatos.Enabled = False
        Me.pnlDatos.Location = New System.Drawing.Point(15, 121)
        Me.pnlDatos.Name = "pnlDatos"
        Me.pnlDatos.Size = New System.Drawing.Size(723, 72)
        Me.pnlDatos.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(98, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 16)
        Me.Label1.TabIndex = 343
        Me.Label1.Text = "Tipo:"
        '
        'cmbTipo
        '
        Me.cmbTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTipo.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Location = New System.Drawing.Point(62, 42)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(122, 24)
        Me.cmbTipo.TabIndex = 9
        '
        'lblReferencia
        '
        Me.lblReferencia.AutoSize = True
        Me.lblReferencia.BackColor = System.Drawing.Color.Transparent
        Me.lblReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReferencia.Location = New System.Drawing.Point(323, 9)
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
        Me.Label10.Location = New System.Drawing.Point(602, 23)
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
        Me.Label7.Location = New System.Drawing.Point(494, 25)
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
        Me.cmbMoneda.Location = New System.Drawing.Point(598, 40)
        Me.cmbMoneda.Name = "cmbMoneda"
        Me.cmbMoneda.Size = New System.Drawing.Size(90, 24)
        Me.cmbMoneda.TabIndex = 14
        '
        'txtImporte
        '
        Me.txtImporte.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporte.Location = New System.Drawing.Point(491, 41)
        Me.txtImporte.Name = "txtImporte"
        Me.txtImporte.Size = New System.Drawing.Size(100, 22)
        Me.txtImporte.TabIndex = 13
        Me.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFolioReferencia
        '
        Me.txtFolioReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolioReferencia.Location = New System.Drawing.Point(387, 42)
        Me.txtFolioReferencia.Name = "txtFolioReferencia"
        Me.txtFolioReferencia.Size = New System.Drawing.Size(98, 22)
        Me.txtFolioReferencia.TabIndex = 12
        Me.txtFolioReferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dtpFecha
        '
        Me.dtpFecha.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha.Location = New System.Drawing.Point(191, 42)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(88, 22)
        Me.dtpFecha.TabIndex = 10
        '
        'btnAgregar
        '
        Me.btnAgregar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.Location = New System.Drawing.Point(747, 124)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(155, 34)
        Me.btnAgregar.TabIndex = 15
        Me.btnAgregar.Text = "Agregar Documento"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(746, 164)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(155, 28)
        Me.btnNuevo.TabIndex = 16
        Me.btnNuevo.Text = "Nuevo Documento"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'txtDatosCliente
        '
        Me.txtDatosCliente.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatosCliente.Location = New System.Drawing.Point(170, 12)
        Me.txtDatosCliente.Multiline = True
        Me.txtDatosCliente.Name = "txtDatosCliente"
        Me.txtDatosCliente.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDatosCliente.Size = New System.Drawing.Size(358, 64)
        Me.txtDatosCliente.TabIndex = 6
        Me.txtDatosCliente.TabStop = False
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Location = New System.Drawing.Point(119, 27)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(45, 23)
        Me.btnBuscarCliente.TabIndex = 5
        Me.btnBuscarCliente.Text = "..."
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'txtCliente
        '
        Me.txtCliente.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Location = New System.Drawing.Point(6, 27)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(107, 22)
        Me.txtCliente.TabIndex = 4
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(817, 473)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(88, 25)
        Me.btnCerrar.TabIndex = 21
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(15, 389)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(138, 25)
        Me.btnEliminar.TabIndex = 17
        Me.btnEliminar.Text = "Eliminar Documento"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(752, 37)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(113, 16)
        Me.Label25.TabIndex = 342
        Me.Label25.Text = "Tipo de Cambio:"
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoCambio.Location = New System.Drawing.Point(758, 53)
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.Size = New System.Drawing.Size(98, 22)
        Me.txtTipoCambio.TabIndex = 2
        '
        'chkMostrarGuardades
        '
        Me.chkMostrarGuardades.AutoSize = True
        Me.chkMostrarGuardades.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMostrarGuardades.Location = New System.Drawing.Point(159, 392)
        Me.chkMostrarGuardades.Name = "chkMostrarGuardades"
        Me.chkMostrarGuardades.Size = New System.Drawing.Size(147, 20)
        Me.chkMostrarGuardades.TabIndex = 8
        Me.chkMostrarGuardades.Text = "Mostrar Guardadas"
        Me.chkMostrarGuardades.UseVisualStyleBackColor = True
        '
        'pnlCliente
        '
        Me.pnlCliente.Controls.Add(Me.Label2)
        Me.pnlCliente.Controls.Add(Me.txtCliente)
        Me.pnlCliente.Controls.Add(Me.btnBuscarCliente)
        Me.pnlCliente.Controls.Add(Me.txtDatosCliente)
        Me.pnlCliente.Controls.Add(Me.Label3)
        Me.pnlCliente.Controls.Add(Me.txtDatosCliente2)
        Me.pnlCliente.Location = New System.Drawing.Point(15, 34)
        Me.pnlCliente.Name = "pnlCliente"
        Me.pnlCliente.Size = New System.Drawing.Size(723, 81)
        Me.pnlCliente.TabIndex = 3
        '
        'frmClientesDocumentos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(913, 502)
        Me.Controls.Add(Me.chkMostrarGuardades)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.txtTipoCambio)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.cmbSucursal)
        Me.Controls.Add(Me.dgvDocumentos)
        Me.Controls.Add(Me.pnlDatos)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.pnlCliente)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmClientesDocumentos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Captura de Documentos de Clientes"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvDocumentos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDatos.ResumeLayout(False)
        Me.pnlDatos.PerformLayout()
        Me.pnlCliente.ResumeLayout(False)
        Me.pnlCliente.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbMonedaGral As System.Windows.Forms.ComboBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents txtDatosCliente2 As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents lblSerieReferencia As System.Windows.Forms.Label
    Friend WithEvents txtSerieReferencia As System.Windows.Forms.TextBox
    Friend WithEvents lblFolioReferencia As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dgvDocumentos As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlDatos As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents cmbMoneda As System.Windows.Forms.ComboBox
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents txtImporte As System.Windows.Forms.TextBox
    Friend WithEvents txtDatosCliente As System.Windows.Forms.TextBox
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents txtFolioReferencia As System.Windows.Forms.TextBox
    Friend WithEvents txtCliente As System.Windows.Forms.TextBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtTipoCambio As System.Windows.Forms.TextBox
    Friend WithEvents lblReferencia As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents chkMostrarGuardades As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCliente As System.Windows.Forms.Panel
    Friend WithEvents colId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colTipo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coSerie As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colFolio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colImporte As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Moneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Tipodecambio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSestado As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
