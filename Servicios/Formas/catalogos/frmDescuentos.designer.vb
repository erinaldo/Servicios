<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDescuentos
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlPromocion = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.nmrInicio = New System.Windows.Forms.NumericUpDown()
        Me.nmrFinal = New System.Windows.Forms.NumericUpDown()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rdbPromocion = New System.Windows.Forms.RadioButton()
        Me.rdbEfectivo = New System.Windows.Forms.RadioButton()
        Me.rdbPorcentaje = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlEfectivo = New System.Windows.Forms.Panel()
        Me.txtEfectivo = New System.Windows.Forms.TextBox()
        Me.dtpHoraf = New System.Windows.Forms.DateTimePicker()
        Me.pnlPorcentaje = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.dtpHorai = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaf = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechai = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBusqueda = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.Seleccionar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Código = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripción = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.Seleccionado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.id2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantidad2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnAgregarTodos = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlPromocion.SuspendLayout()
        CType(Me.nmrInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmrFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlEfectivo.SuspendLayout()
        Me.pnlPorcentaje.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 16)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Activado:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadioButton2)
        Me.Panel1.Controls.Add(Me.RadioButton1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(107, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(174, 27)
        Me.Panel1.TabIndex = 54
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(123, 4)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(43, 20)
        Me.RadioButton2.TabIndex = 24
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "No"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(77, 5)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(39, 20)
        Me.RadioButton1.TabIndex = 23
        Me.RadioButton1.Text = "Sí"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 16)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Descuento:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Descripción:"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Location = New System.Drawing.Point(97, 120)
        Me.txtDescripcion.MaxLength = 50
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(273, 22)
        Me.txtDescripcion.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(41, 147)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 16)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Desde:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(46, 174)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 16)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Hasta:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbSucursal)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.pnlPromocion)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.pnlEfectivo)
        Me.GroupBox1.Controls.Add(Me.dtpHoraf)
        Me.GroupBox1.Controls.Add(Me.pnlPorcentaje)
        Me.GroupBox1.Controls.Add(Me.dtpHorai)
        Me.GroupBox1.Controls.Add(Me.dtpFechaf)
        Me.GroupBox1.Controls.Add(Me.dtpFechai)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.txtDescripcion)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(403, 227)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del descuento"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(80, 201)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(304, 24)
        Me.cmbSucursal.TabIndex = 230
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(10, 203)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 16)
        Me.Label12.TabIndex = 229
        Me.Label12.Text = "Sucursal:"
        '
        'pnlPromocion
        '
        Me.pnlPromocion.Controls.Add(Me.Label11)
        Me.pnlPromocion.Controls.Add(Me.nmrInicio)
        Me.pnlPromocion.Controls.Add(Me.nmrFinal)
        Me.pnlPromocion.Location = New System.Drawing.Point(93, 91)
        Me.pnlPromocion.Name = "pnlPromocion"
        Me.pnlPromocion.Size = New System.Drawing.Size(173, 25)
        Me.pnlPromocion.TabIndex = 1
        Me.pnlPromocion.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(62, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 16)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "X"
        '
        'nmrInicio
        '
        Me.nmrInicio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmrInicio.Location = New System.Drawing.Point(3, 3)
        Me.nmrInicio.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmrInicio.Name = "nmrInicio"
        Me.nmrInicio.Size = New System.Drawing.Size(57, 22)
        Me.nmrInicio.TabIndex = 230
        Me.nmrInicio.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nmrFinal
        '
        Me.nmrFinal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmrFinal.Location = New System.Drawing.Point(80, 2)
        Me.nmrFinal.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmrFinal.Name = "nmrFinal"
        Me.nmrFinal.Size = New System.Drawing.Size(57, 22)
        Me.nmrFinal.TabIndex = 231
        Me.nmrFinal.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rdbPromocion)
        Me.Panel2.Controls.Add(Me.rdbEfectivo)
        Me.Panel2.Controls.Add(Me.rdbPorcentaje)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Location = New System.Drawing.Point(18, 41)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(352, 48)
        Me.Panel2.TabIndex = 64
        '
        'rdbPromocion
        '
        Me.rdbPromocion.AutoSize = True
        Me.rdbPromocion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbPromocion.Location = New System.Drawing.Point(218, 23)
        Me.rdbPromocion.Name = "rdbPromocion"
        Me.rdbPromocion.Size = New System.Drawing.Size(95, 20)
        Me.rdbPromocion.TabIndex = 57
        Me.rdbPromocion.Text = "Promoción"
        Me.rdbPromocion.UseVisualStyleBackColor = True
        '
        'rdbEfectivo
        '
        Me.rdbEfectivo.AutoSize = True
        Me.rdbEfectivo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbEfectivo.Location = New System.Drawing.Point(140, 23)
        Me.rdbEfectivo.Name = "rdbEfectivo"
        Me.rdbEfectivo.Size = New System.Drawing.Size(76, 20)
        Me.rdbEfectivo.TabIndex = 56
        Me.rdbEfectivo.Text = "Efectivo"
        Me.rdbEfectivo.UseVisualStyleBackColor = True
        '
        'rdbPorcentaje
        '
        Me.rdbPorcentaje.AutoSize = True
        Me.rdbPorcentaje.Checked = True
        Me.rdbPorcentaje.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbPorcentaje.Location = New System.Drawing.Point(48, 23)
        Me.rdbPorcentaje.Name = "rdbPorcentaje"
        Me.rdbPorcentaje.Size = New System.Drawing.Size(95, 20)
        Me.rdbPorcentaje.TabIndex = 55
        Me.rdbPorcentaje.TabStop = True
        Me.rdbPorcentaje.Text = "Porcentaje"
        Me.rdbPorcentaje.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(116, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(125, 16)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Tipo de descuento"
        '
        'pnlEfectivo
        '
        Me.pnlEfectivo.Controls.Add(Me.txtEfectivo)
        Me.pnlEfectivo.Location = New System.Drawing.Point(96, 92)
        Me.pnlEfectivo.Name = "pnlEfectivo"
        Me.pnlEfectivo.Size = New System.Drawing.Size(108, 25)
        Me.pnlEfectivo.TabIndex = 228
        Me.pnlEfectivo.Visible = False
        '
        'txtEfectivo
        '
        Me.txtEfectivo.Location = New System.Drawing.Point(1, 3)
        Me.txtEfectivo.Name = "txtEfectivo"
        Me.txtEfectivo.Size = New System.Drawing.Size(63, 20)
        Me.txtEfectivo.TabIndex = 0
        '
        'dtpHoraf
        '
        Me.dtpHoraf.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHoraf.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHoraf.Location = New System.Drawing.Point(191, 173)
        Me.dtpHoraf.Name = "dtpHoraf"
        Me.dtpHoraf.ShowUpDown = True
        Me.dtpHoraf.Size = New System.Drawing.Size(103, 22)
        Me.dtpHoraf.TabIndex = 6
        '
        'pnlPorcentaje
        '
        Me.pnlPorcentaje.Controls.Add(Me.Label10)
        Me.pnlPorcentaje.Controls.Add(Me.txtDescuento)
        Me.pnlPorcentaje.Location = New System.Drawing.Point(96, 92)
        Me.pnlPorcentaje.Name = "pnlPorcentaje"
        Me.pnlPorcentaje.Size = New System.Drawing.Size(108, 25)
        Me.pnlPorcentaje.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(66, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(18, 16)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "%"
        '
        'txtDescuento
        '
        Me.txtDescuento.Location = New System.Drawing.Point(1, 3)
        Me.txtDescuento.MaxLength = 3
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(63, 20)
        Me.txtDescuento.TabIndex = 1
        '
        'dtpHorai
        '
        Me.dtpHorai.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHorai.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpHorai.Location = New System.Drawing.Point(191, 146)
        Me.dtpHorai.Name = "dtpHorai"
        Me.dtpHorai.ShowUpDown = True
        Me.dtpHorai.Size = New System.Drawing.Size(104, 22)
        Me.dtpHorai.TabIndex = 4
        '
        'dtpFechaf
        '
        Me.dtpFechaf.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaf.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaf.Location = New System.Drawing.Point(97, 173)
        Me.dtpFechaf.Name = "dtpFechaf"
        Me.dtpFechaf.Size = New System.Drawing.Size(84, 22)
        Me.dtpFechaf.TabIndex = 5
        '
        'dtpFechai
        '
        Me.dtpFechai.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechai.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechai.Location = New System.Drawing.Point(96, 146)
        Me.dtpFechai.Name = "dtpFechai"
        Me.dtpFechai.Size = New System.Drawing.Size(86, 22)
        Me.dtpFechai.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtBusqueda)
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(415, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(563, 224)
        Me.GroupBox2.TabIndex = 70
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Busqueda"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(100, 46)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(314, 24)
        Me.ComboBox1.TabIndex = 232
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(30, 48)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 16)
        Me.Label13.TabIndex = 231
        Me.Label13.Text = "Sucursal:"
        '
        'txtBusqueda
        '
        Me.txtBusqueda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBusqueda.Location = New System.Drawing.Point(100, 20)
        Me.txtBusqueda.Name = "txtBusqueda"
        Me.txtBusqueda.Size = New System.Drawing.Size(446, 22)
        Me.txtBusqueda.TabIndex = 21
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(17, 74)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(530, 139)
        Me.DataGridView1.TabIndex = 22
        Me.DataGridView1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 16)
        Me.Label6.TabIndex = 70
        Me.Label6.Text = "Descripción:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 326)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 16)
        Me.Label9.TabIndex = 208
        Me.Label9.Text = "Artículo:"
        '
        'TextBox5
        '
        Me.TextBox5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.Location = New System.Drawing.Point(73, 323)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(298, 22)
        Me.TextBox5.TabIndex = 16
        Me.TextBox5.TabStop = False
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccionar, Me.id, Me.Código, Me.Descripción, Me.Cantidad})
        Me.DataGridView2.Location = New System.Drawing.Point(12, 348)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(435, 205)
        Me.DataGridView2.TabIndex = 17
        Me.DataGridView2.TabStop = False
        '
        'Seleccionar
        '
        Me.Seleccionar.DataPropertyName = "selec"
        Me.Seleccionar.FalseValue = "0"
        Me.Seleccionar.HeaderText = "X"
        Me.Seleccionar.Name = "Seleccionar"
        Me.Seleccionar.ReadOnly = True
        Me.Seleccionar.TrueValue = "1"
        Me.Seleccionar.Width = 20
        '
        'id
        '
        Me.id.DataPropertyName = "idinventario"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        Me.id.Width = 40
        '
        'Código
        '
        Me.Código.DataPropertyName = "clave"
        Me.Código.HeaderText = "Código"
        Me.Código.Name = "Código"
        Me.Código.ReadOnly = True
        Me.Código.Width = 65
        '
        'Descripción
        '
        Me.Descripción.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Descripción.DataPropertyName = "nombre"
        Me.Descripción.HeaderText = "Descripción"
        Me.Descripción.Name = "Descripción"
        Me.Descripción.ReadOnly = True
        '
        'Cantidad
        '
        Me.Cantidad.DataPropertyName = "spdainventario(idinventario,0,0)"
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 74
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AllowUserToDeleteRows = False
        Me.DataGridView3.AllowUserToResizeRows = False
        Me.DataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccionado, Me.id2, Me.codigo, Me.descripcion, Me.cantidad2})
        Me.DataGridView3.Location = New System.Drawing.Point(528, 348)
        Me.DataGridView3.MultiSelect = False
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.ReadOnly = True
        Me.DataGridView3.RowHeadersVisible = False
        Me.DataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView3.Size = New System.Drawing.Size(431, 205)
        Me.DataGridView3.TabIndex = 20
        Me.DataGridView3.TabStop = False
        '
        'Seleccionado
        '
        Me.Seleccionado.DataPropertyName = "selec"
        Me.Seleccionado.FalseValue = "0"
        Me.Seleccionado.HeaderText = " "
        Me.Seleccionado.Name = "Seleccionado"
        Me.Seleccionado.ReadOnly = True
        Me.Seleccionado.TrueValue = "1"
        Me.Seleccionado.Width = 16
        '
        'id2
        '
        Me.id2.DataPropertyName = "id"
        Me.id2.HeaderText = "id"
        Me.id2.Name = "id2"
        Me.id2.ReadOnly = True
        Me.id2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.id2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.id2.Visible = False
        Me.id2.Width = 21
        '
        'codigo
        '
        Me.codigo.DataPropertyName = "clave"
        Me.codigo.HeaderText = "Código"
        Me.codigo.Name = "codigo"
        Me.codigo.ReadOnly = True
        Me.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.codigo.Width = 46
        '
        'descripcion
        '
        Me.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.descripcion.DataPropertyName = "nombre"
        Me.descripcion.HeaderText = "Descripción"
        Me.descripcion.Name = "descripcion"
        Me.descripcion.ReadOnly = True
        Me.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'cantidad2
        '
        Me.cantidad2.DataPropertyName = "cantidad"
        Me.cantidad2.HeaderText = "Cantidad"
        Me.cantidad2.Name = "cantidad2"
        Me.cantidad2.ReadOnly = True
        Me.cantidad2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cantidad2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cantidad2.Width = 55
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(468, 369)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(44, 23)
        Me.btnAgregar.TabIndex = 18
        Me.btnAgregar.Text = ">"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'btnQuitar
        '
        Me.btnQuitar.Location = New System.Drawing.Point(468, 441)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(44, 23)
        Me.btnQuitar.TabIndex = 19
        Me.btnQuitar.Text = "<"
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'ComboBox7
        '
        Me.ComboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Location = New System.Drawing.Point(190, 294)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(250, 24)
        Me.ComboBox7.TabIndex = 15
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(59, 297)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 16)
        Me.Label16.TabIndex = 224
        Me.Label16.Text = "Nivel 3:"
        '
        'ComboBox6
        '
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(168, 264)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(250, 24)
        Me.ComboBox6.TabIndex = 13
        '
        'TextBox13
        '
        Me.TextBox13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox13.Location = New System.Drawing.Point(120, 295)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(64, 22)
        Me.TextBox13.TabIndex = 14
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(37, 267)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 16)
        Me.Label15.TabIndex = 223
        Me.Label15.Text = "Nivel 2:"
        '
        'TextBox12
        '
        Me.TextBox12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox12.Location = New System.Drawing.Point(98, 265)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(64, 22)
        Me.TextBox12.TabIndex = 12
        '
        'TextBox10
        '
        Me.TextBox10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox10.Location = New System.Drawing.Point(77, 235)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(66, 22)
        Me.TextBox10.TabIndex = 10
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(149, 235)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(250, 24)
        Me.ComboBox3.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(18, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 16)
        Me.Label8.TabIndex = 222
        Me.Label8.Text = "Nivel 1:"
        '
        'btnAgregarTodos
        '
        Me.btnAgregarTodos.Location = New System.Drawing.Point(468, 393)
        Me.btnAgregarTodos.Name = "btnAgregarTodos"
        Me.btnAgregarTodos.Size = New System.Drawing.Size(44, 23)
        Me.btnAgregarTodos.TabIndex = 225
        Me.btnAgregarTodos.Text = ">>"
        Me.btnAgregarTodos.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(468, 465)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(44, 23)
        Me.Button2.TabIndex = 226
        Me.Button2.Text = "<<"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(372, 322)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 66
        Me.Button3.Text = "Limpiar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(494, 561)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(80, 30)
        Me.btnNuevo.TabIndex = 229
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(402, 561)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(80, 30)
        Me.btnEliminar.TabIndex = 228
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(310, 561)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(80, 30)
        Me.btnGuardar.TabIndex = 227
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(589, 561)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 30)
        Me.Button1.TabIndex = 230
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmDescuentos
        '
        Me.AcceptButton = Me.btnGuardar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(990, 597)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnAgregarTodos)
        Me.Controls.Add(Me.ComboBox7)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.ComboBox6)
        Me.Controls.Add(Me.TextBox13)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.TextBox12)
        Me.Controls.Add(Me.TextBox10)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.DataGridView3)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDescuentos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ofertas"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlPromocion.ResumeLayout(False)
        Me.pnlPromocion.PerformLayout()
        CType(Me.nmrInicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmrFinal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlEfectivo.ResumeLayout(False)
        Me.pnlEfectivo.PerformLayout()
        Me.pnlPorcentaje.ResumeLayout(False)
        Me.pnlPorcentaje.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFechaf As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFechai As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBusqueda As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents btnQuitar As System.Windows.Forms.Button
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpHoraf As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHorai As System.Windows.Forms.DateTimePicker
    Public WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Public WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents Seleccionado As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents id2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents codigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantidad2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnAgregarTodos As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents rdbPromocion As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEfectivo As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPorcentaje As System.Windows.Forms.RadioButton
    Friend WithEvents pnlPorcentaje As System.Windows.Forms.Panel
    Friend WithEvents pnlEfectivo As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents txtEfectivo As System.Windows.Forms.TextBox
    Friend WithEvents pnlPromocion As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents nmrInicio As System.Windows.Forms.NumericUpDown
    Friend WithEvents nmrFinal As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Seleccionar As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Código As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripción As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
End Class
