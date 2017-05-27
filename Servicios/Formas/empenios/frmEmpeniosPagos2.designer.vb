<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpeniosPagos2
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblPagado = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSerie = New System.Windows.Forms.TextBox()
        Me.lblRefrendo = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAbono = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblRestante = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dgvPagosRealizados = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.ckDescuento = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.cmbCaja = New System.Windows.Forms.ComboBox()
        Me.btnEliminarConcepto = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpFechaPago = New System.Windows.Forms.DateTimePicker()
        Me.btnGuardarConcepto = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCantidadPago = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.txtcliente = New System.Windows.Forms.TextBox()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dgvTodosPagos = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.chkCanceladas = New System.Windows.Forms.CheckBox()
        Me.chkPagadas = New System.Windows.Forms.CheckBox()
        Me.chkFecha = New System.Windows.Forms.CheckBox()
        Me.dtpFecha2 = New System.Windows.Forms.DateTimePicker()
        Me.dtpFecha1 = New System.Windows.Forms.DateTimePicker()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.chkTiempoReal = New System.Windows.Forms.CheckBox()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmbVendedor = New System.Windows.Forms.ComboBox()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        CType(Me.dgvPagosRealizados, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvTodosPagos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblPagado
        '
        Me.lblPagado.AutoSize = True
        Me.lblPagado.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPagado.ForeColor = System.Drawing.Color.Red
        Me.lblPagado.Location = New System.Drawing.Point(39, 615)
        Me.lblPagado.Name = "lblPagado"
        Me.lblPagado.Size = New System.Drawing.Size(114, 32)
        Me.lblPagado.TabIndex = 394
        Me.lblPagado.Text = "Pagado"
        Me.lblPagado.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(311, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 19)
        Me.Label3.TabIndex = 393
        Me.Label3.Text = "Serie:"
        '
        'txtSerie
        '
        Me.txtSerie.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtSerie.Location = New System.Drawing.Point(289, 95)
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(89, 25)
        Me.txtSerie.TabIndex = 392
        '
        'lblRefrendo
        '
        Me.lblRefrendo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblRefrendo.Location = New System.Drawing.Point(688, 527)
        Me.lblRefrendo.Name = "lblRefrendo"
        Me.lblRefrendo.Size = New System.Drawing.Size(94, 22)
        Me.lblRefrendo.TabIndex = 390
        Me.lblRefrendo.Text = "$ 0.00"
        Me.lblRefrendo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRefrendo.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(595, 529)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(97, 18)
        Me.Label9.TabIndex = 391
        Me.Label9.Text = "Desempeño:"
        Me.Label9.Visible = False
        '
        'lblAbono
        '
        Me.lblAbono.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblAbono.Location = New System.Drawing.Point(687, 505)
        Me.lblAbono.Name = "lblAbono"
        Me.lblAbono.Size = New System.Drawing.Size(94, 22)
        Me.lblAbono.TabIndex = 388
        Me.lblAbono.Text = "$ 0.00"
        Me.lblAbono.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblAbono.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(597, 507)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 18)
        Me.Label5.TabIndex = 389
        Me.Label5.Text = "Total abono:"
        Me.Label5.Visible = False
        '
        'lblRestante
        '
        Me.lblRestante.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRestante.Location = New System.Drawing.Point(630, 560)
        Me.lblRestante.Name = "lblRestante"
        Me.lblRestante.Size = New System.Drawing.Size(158, 22)
        Me.lblRestante.TabIndex = 386
        Me.lblRestante.Text = "$ 0.00"
        Me.lblRestante.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblRestante.Visible = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(526, 560)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(98, 22)
        Me.Label16.TabIndex = 387
        Me.Label16.Text = "Restante:"
        Me.Label16.Visible = False
        '
        'dgvPagosRealizados
        '
        Me.dgvPagosRealizados.AllowUserToAddRows = False
        Me.dgvPagosRealizados.AllowUserToDeleteRows = False
        Me.dgvPagosRealizados.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        Me.dgvPagosRealizados.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPagosRealizados.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvPagosRealizados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPagosRealizados.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvPagosRealizados.Location = New System.Drawing.Point(117, 440)
        Me.dgvPagosRealizados.Name = "dgvPagosRealizados"
        Me.dgvPagosRealizados.ReadOnly = True
        Me.dgvPagosRealizados.RowHeadersVisible = False
        Me.dgvPagosRealizados.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvPagosRealizados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPagosRealizados.Size = New System.Drawing.Size(743, 141)
        Me.dgvPagosRealizados.TabIndex = 385
        Me.dgvPagosRealizados.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblDescuento)
        Me.GroupBox2.Controls.Add(Me.txtDescuento)
        Me.GroupBox2.Controls.Add(Me.ckDescuento)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.btnNuevo)
        Me.GroupBox2.Controls.Add(Me.cmbCaja)
        Me.GroupBox2.Controls.Add(Me.btnEliminarConcepto)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.dtpFechaPago)
        Me.GroupBox2.Controls.Add(Me.btnGuardarConcepto)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtCantidadPago)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.GroupBox2.Location = New System.Drawing.Point(117, 353)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(743, 85)
        Me.GroupBox2.TabIndex = 384
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Abono de empeño"
        '
        'lblDescuento
        '
        Me.lblDescuento.AutoSize = True
        Me.lblDescuento.BackColor = System.Drawing.Color.Transparent
        Me.lblDescuento.Enabled = False
        Me.lblDescuento.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblDescuento.Location = New System.Drawing.Point(374, 14)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(98, 19)
        Me.lblDescuento.TabIndex = 402
        Me.lblDescuento.Text = "Descuento:"
        '
        'txtDescuento
        '
        Me.txtDescuento.Enabled = False
        Me.txtDescuento.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtDescuento.Location = New System.Drawing.Point(372, 36)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(105, 25)
        Me.txtDescuento.TabIndex = 4
        Me.txtDescuento.Text = "0.00"
        '
        'ckDescuento
        '
        Me.ckDescuento.AutoSize = True
        Me.ckDescuento.Location = New System.Drawing.Point(372, 64)
        Me.ckDescuento.Name = "ckDescuento"
        Me.ckDescuento.Size = New System.Drawing.Size(176, 21)
        Me.ckDescuento.TabIndex = 400
        Me.ckDescuento.Text = "Cobrar con descuento."
        Me.ckDescuento.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(18, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(49, 19)
        Me.Label10.TabIndex = 399
        Me.Label10.Text = "Caja:"
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.btnNuevo.Location = New System.Drawing.Point(580, 36)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(78, 24)
        Me.btnNuevo.TabIndex = 6
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'cmbCaja
        '
        Me.cmbCaja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCaja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCaja.Enabled = False
        Me.cmbCaja.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbCaja.FormattingEnabled = True
        Me.cmbCaja.Location = New System.Drawing.Point(16, 37)
        Me.cmbCaja.Name = "cmbCaja"
        Me.cmbCaja.Size = New System.Drawing.Size(128, 25)
        Me.cmbCaja.TabIndex = 1
        '
        'btnEliminarConcepto
        '
        Me.btnEliminarConcepto.Enabled = False
        Me.btnEliminarConcepto.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.btnEliminarConcepto.Location = New System.Drawing.Point(662, 36)
        Me.btnEliminarConcepto.Name = "btnEliminarConcepto"
        Me.btnEliminarConcepto.Size = New System.Drawing.Size(78, 24)
        Me.btnEliminarConcepto.TabIndex = 7
        Me.btnEliminarConcepto.Text = "Eliminar"
        Me.btnEliminarConcepto.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(168, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 19)
        Me.Label6.TabIndex = 364
        Me.Label6.Text = "Fecha:"
        '
        'dtpFechaPago
        '
        Me.dtpFechaPago.CustomFormat = "yyyy/MM/dd"
        Me.dtpFechaPago.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFechaPago.Location = New System.Drawing.Point(150, 37)
        Me.dtpFechaPago.Name = "dtpFechaPago"
        Me.dtpFechaPago.Size = New System.Drawing.Size(105, 25)
        Me.dtpFechaPago.TabIndex = 2
        '
        'btnGuardarConcepto
        '
        Me.btnGuardarConcepto.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.btnGuardarConcepto.Location = New System.Drawing.Point(497, 36)
        Me.btnGuardarConcepto.Name = "btnGuardarConcepto"
        Me.btnGuardarConcepto.Size = New System.Drawing.Size(78, 25)
        Me.btnGuardarConcepto.TabIndex = 5
        Me.btnGuardarConcepto.Text = "Guardar"
        Me.btnGuardarConcepto.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(273, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 19)
        Me.Label1.TabIndex = 363
        Me.Label1.Text = "Cantidad:"
        '
        'txtCantidadPago
        '
        Me.txtCantidadPago.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtCantidadPago.Location = New System.Drawing.Point(261, 37)
        Me.txtCantidadPago.Name = "txtCantidadPago"
        Me.txtCantidadPago.Size = New System.Drawing.Size(105, 25)
        Me.txtCantidadPago.TabIndex = 3
        Me.txtCantidadPago.Text = "0.00"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox7)
        Me.GroupBox1.Controls.Add(Me.txtcliente)
        Me.GroupBox1.Controls.Add(Me.btnBuscarCliente)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(955, 70)
        Me.GroupBox1.TabIndex = 383
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del cliente"
        '
        'TextBox7
        '
        Me.TextBox7.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TextBox7.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TextBox7.Location = New System.Drawing.Point(213, 37)
        Me.TextBox7.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.TextBox7.Multiline = True
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.ReadOnly = True
        Me.TextBox7.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox7.Size = New System.Drawing.Size(727, 22)
        Me.TextBox7.TabIndex = 339
        Me.TextBox7.TabStop = False
        '
        'txtcliente
        '
        Me.txtcliente.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtcliente.Location = New System.Drawing.Point(24, 37)
        Me.txtcliente.Name = "txtcliente"
        Me.txtcliente.Size = New System.Drawing.Size(132, 25)
        Me.txtcliente.TabIndex = 50
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Location = New System.Drawing.Point(162, 36)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(45, 24)
        Me.btnBuscarCliente.TabIndex = 51
        Me.btnBuscarCliente.Text = "..."
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(25, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 19)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "Código cliente:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(213, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(144, 19)
        Me.Label7.TabIndex = 340
        Me.Label7.Text = "Datos del Cliente:"
        '
        'dgvTodosPagos
        '
        Me.dgvTodosPagos.AllowUserToAddRows = False
        Me.dgvTodosPagos.AllowUserToDeleteRows = False
        Me.dgvTodosPagos.AllowUserToResizeRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.White
        Me.dgvTodosPagos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTodosPagos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvTodosPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvTodosPagos.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvTodosPagos.Location = New System.Drawing.Point(20, 148)
        Me.dgvTodosPagos.Name = "dgvTodosPagos"
        Me.dgvTodosPagos.ReadOnly = True
        Me.dgvTodosPagos.RowHeadersVisible = False
        Me.dgvTodosPagos.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTodosPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTodosPagos.Size = New System.Drawing.Size(951, 146)
        Me.dgvTodosPagos.TabIndex = 382
        Me.dgvTodosPagos.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(407, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.TabIndex = 381
        Me.Label4.Text = "Folio:"
        '
        'txtFolio
        '
        Me.txtFolio.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtFolio.Location = New System.Drawing.Point(384, 96)
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.Size = New System.Drawing.Size(92, 25)
        Me.txtFolio.TabIndex = 378
        '
        'chkCanceladas
        '
        Me.chkCanceladas.AutoSize = True
        Me.chkCanceladas.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.chkCanceladas.Location = New System.Drawing.Point(783, 104)
        Me.chkCanceladas.Name = "chkCanceladas"
        Me.chkCanceladas.Size = New System.Drawing.Size(177, 23)
        Me.chkCanceladas.TabIndex = 380
        Me.chkCanceladas.Text = "Mostrar canceladas"
        Me.chkCanceladas.UseVisualStyleBackColor = True
        '
        'chkPagadas
        '
        Me.chkPagadas.AutoSize = True
        Me.chkPagadas.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.chkPagadas.Location = New System.Drawing.Point(783, 77)
        Me.chkPagadas.Name = "chkPagadas"
        Me.chkPagadas.Size = New System.Drawing.Size(156, 23)
        Me.chkPagadas.TabIndex = 379
        Me.chkPagadas.Text = "Mostrar pagadas"
        Me.chkPagadas.UseVisualStyleBackColor = True
        '
        'chkFecha
        '
        Me.chkFecha.AutoSize = True
        Me.chkFecha.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.chkFecha.Location = New System.Drawing.Point(123, 72)
        Me.chkFecha.Name = "chkFecha"
        Me.chkFecha.Size = New System.Drawing.Size(81, 23)
        Me.chkFecha.TabIndex = 375
        Me.chkFecha.Text = "Fecha:"
        Me.chkFecha.UseVisualStyleBackColor = True
        '
        'dtpFecha2
        '
        Me.dtpFecha2.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha2.Location = New System.Drawing.Point(168, 95)
        Me.dtpFecha2.Name = "dtpFecha2"
        Me.dtpFecha2.Size = New System.Drawing.Size(115, 25)
        Me.dtpFecha2.TabIndex = 377
        '
        'dtpFecha1
        '
        Me.dtpFecha1.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha1.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha1.Location = New System.Drawing.Point(40, 95)
        Me.dtpFecha1.Name = "dtpFecha1"
        Me.dtpFecha1.Size = New System.Drawing.Size(110, 25)
        Me.dtpFecha1.TabIndex = 376
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(577, 76)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 19)
        Me.Label22.TabIndex = 374
        Me.Label22.Text = "Sucursal:"
        '
        'ComboBox2
        '
        Me.ComboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.ComboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(503, 95)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(239, 25)
        Me.ComboBox2.TabIndex = 373
        '
        'chkTiempoReal
        '
        Me.chkTiempoReal.AutoSize = True
        Me.chkTiempoReal.Checked = True
        Me.chkTiempoReal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTiempoReal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.chkTiempoReal.Location = New System.Drawing.Point(451, 126)
        Me.chkTiempoReal.Name = "chkTiempoReal"
        Me.chkTiempoReal.Size = New System.Drawing.Size(109, 21)
        Me.chkTiempoReal.TabIndex = 396
        Me.chkTiempoReal.Text = "Tiempo Real"
        Me.chkTiempoReal.UseVisualStyleBackColor = True
        '
        'btnBuscar
        '
        Me.btnBuscar.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.btnBuscar.Location = New System.Drawing.Point(363, 122)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(82, 23)
        Me.btnBuscar.TabIndex = 395
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(20, 300)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(101, 25)
        Me.Button1.TabIndex = 367
        Me.Button1.Text = "Ver Empeño"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Arial", 16.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(640, 306)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(299, 22)
        Me.Label8.TabIndex = 397
        Me.Label8.Text = "Saldo: $0.00"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label25.Location = New System.Drawing.Point(6, 409)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 19)
        Me.Label25.TabIndex = 401
        Me.Label25.Text = "Vendedor:"
        Me.Label25.Visible = False
        '
        'cmbVendedor
        '
        Me.cmbVendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbVendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVendedor.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbVendedor.FormattingEnabled = True
        Me.cmbVendedor.ItemHeight = 17
        Me.cmbVendedor.Location = New System.Drawing.Point(20, 431)
        Me.cmbVendedor.Name = "cmbVendedor"
        Me.cmbVendedor.Size = New System.Drawing.Size(39, 25)
        Me.cmbVendedor.TabIndex = 400
        Me.cmbVendedor.Visible = False
        '
        'lblDescripcion
        '
        Me.lblDescripcion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcion.Location = New System.Drawing.Point(129, 306)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(545, 44)
        Me.lblDescripcion.TabIndex = 402
        Me.lblDescripcion.Text = "Label11"
        '
        'frmEmpeniosPagos2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(983, 582)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.cmbVendedor)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkTiempoReal)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.lblPagado)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSerie)
        Me.Controls.Add(Me.lblRefrendo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblAbono)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblRestante)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.dgvPagosRealizados)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvTodosPagos)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtFolio)
        Me.Controls.Add(Me.chkCanceladas)
        Me.Controls.Add(Me.chkPagadas)
        Me.Controls.Add(Me.chkFecha)
        Me.Controls.Add(Me.dtpFecha2)
        Me.Controls.Add(Me.dtpFecha1)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.ComboBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpeniosPagos2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Empeños - Pagos"
        CType(Me.dgvPagosRealizados, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvTodosPagos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPagado As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents lblRefrendo As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblAbono As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblRestante As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dgvPagosRealizados As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnEliminarConcepto As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaPago As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGuardarConcepto As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCantidadPago As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents txtcliente As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dgvTodosPagos As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFolio As System.Windows.Forms.TextBox
    Friend WithEvents chkCanceladas As System.Windows.Forms.CheckBox
    Friend WithEvents chkPagadas As System.Windows.Forms.CheckBox
    Friend WithEvents chkFecha As System.Windows.Forms.CheckBox
    Friend WithEvents dtpFecha2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFecha1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents chkTiempoReal As System.Windows.Forms.CheckBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbCaja As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbVendedor As System.Windows.Forms.ComboBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents ckDescuento As System.Windows.Forms.CheckBox
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
End Class
