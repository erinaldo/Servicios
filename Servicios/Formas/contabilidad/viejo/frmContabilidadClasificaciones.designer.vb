<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContabilidadClasificaciones
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpBuscar = New System.Windows.Forms.GroupBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nivel1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tipo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtBuscar = New System.Windows.Forms.TextBox()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.cmbNaturaleza1 = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbTipo1 = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.cmbNivel1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbNivel2 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbNivel3 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbNivel4 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbNivel5 = New System.Windows.Forms.ComboBox()
        Me.grpNiveles = New System.Windows.Forms.GroupBox()
        Me.cmbNomNivel1 = New System.Windows.Forms.ComboBox()
        Me.cmbNomNivel5 = New System.Windows.Forms.ComboBox()
        Me.cmbNomNivel2 = New System.Windows.Forms.ComboBox()
        Me.cmbNomNivel4 = New System.Windows.Forms.ComboBox()
        Me.cmbNomNivel3 = New System.Windows.Forms.ComboBox()
        Me.grpCuenta = New System.Windows.Forms.GroupBox()
        Me.chkDIOT = New System.Windows.Forms.CheckBox()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNuevoTodo = New System.Windows.Forms.Button()
        Me.cmbSubTipo = New System.Windows.Forms.ComboBox()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnXML = New System.Windows.Forms.Button()
        Me.cmbCuenta = New System.Windows.Forms.ComboBox()
        Me.btnModificar = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ckAplicarSubcuentas = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.grpBuscar.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpNiveles.SuspendLayout()
        Me.grpCuenta.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpBuscar
        '
        Me.grpBuscar.Controls.Add(Me.CheckBox2)
        Me.grpBuscar.Controls.Add(Me.Label9)
        Me.grpBuscar.Controls.Add(Me.DataGridView1)
        Me.grpBuscar.Controls.Add(Me.txtBuscar)
        Me.grpBuscar.ForeColor = System.Drawing.Color.Black
        Me.grpBuscar.Location = New System.Drawing.Point(485, 12)
        Me.grpBuscar.Name = "grpBuscar"
        Me.grpBuscar.Size = New System.Drawing.Size(518, 469)
        Me.grpBuscar.TabIndex = 10
        Me.grpBuscar.TabStop = False
        Me.grpBuscar.Text = "Buscar"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox2.Location = New System.Drawing.Point(344, 443)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(166, 20)
        Me.CheckBox2.TabIndex = 116
        Me.CheckBox2.Text = "Mostrar descontinuadas"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label9.Location = New System.Drawing.Point(6, 442)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(172, 13)
        Me.Label9.TabIndex = 104
        Me.Label9.Text = "* Seleccione para cargar los datos."
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.Cta, Me.Desc, Me.nivel1, Me.tipo, Me.Nat, Me.cod})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.Location = New System.Drawing.Point(7, 42)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.RowHeadersVisible = False
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(510, 397)
        Me.DataGridView1.TabIndex = 5
        Me.DataGridView1.TabStop = False
        '
        'id
        '
        Me.id.DataPropertyName = "idCContable"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.id.DefaultCellStyle = DataGridViewCellStyle3
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        Me.id.Width = 21
        '
        'Cta
        '
        Me.Cta.DataPropertyName = "cpncat_cuenta"
        Me.Cta.HeaderText = "Cuenta"
        Me.Cta.Name = "Cta"
        Me.Cta.ReadOnly = True
        Me.Cta.Width = 66
        '
        'Desc
        '
        Me.Desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Desc.DataPropertyName = "Descripcion"
        Me.Desc.HeaderText = "Descripción"
        Me.Desc.Name = "Desc"
        Me.Desc.ReadOnly = True
        '
        'nivel1
        '
        Me.nivel1.DataPropertyName = "nivel"
        Me.nivel1.HeaderText = "Nivel"
        Me.nivel1.Name = "nivel1"
        Me.nivel1.ReadOnly = True
        Me.nivel1.Visible = False
        Me.nivel1.Width = 56
        '
        'tipo
        '
        Me.tipo.DataPropertyName = "TIPO"
        Me.tipo.HeaderText = "Tipo"
        Me.tipo.Name = "tipo"
        Me.tipo.ReadOnly = True
        Me.tipo.Width = 53
        '
        'Nat
        '
        Me.Nat.DataPropertyName = "natiraleza"
        Me.Nat.HeaderText = "Naturaleza"
        Me.Nat.Name = "Nat"
        Me.Nat.ReadOnly = True
        Me.Nat.Width = 83
        '
        'cod
        '
        Me.cod.DataPropertyName = "codigo"
        Me.cod.HeaderText = "Agrupador SAT"
        Me.cod.Name = "cod"
        Me.cod.ReadOnly = True
        Me.cod.Width = 96
        '
        'txtBuscar
        '
        Me.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuscar.Location = New System.Drawing.Point(7, 19)
        Me.txtBuscar.MaxLength = 100
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(510, 22)
        Me.txtBuscar.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtBuscar, "Buscar por nombre")
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnCerrar.Location = New System.Drawing.Point(894, 487)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 25)
        Me.btnCerrar.TabIndex = 112
        Me.btnCerrar.Text = "Cerrar"
        Me.ToolTip1.SetToolTip(Me.btnCerrar, "Cerrar ventana")
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'cmbNaturaleza1
        '
        Me.cmbNaturaleza1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNaturaleza1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNaturaleza1.FormattingEnabled = True
        Me.cmbNaturaleza1.Items.AddRange(New Object() {"DEUDORA", "ACREEDORA"})
        Me.cmbNaturaleza1.Location = New System.Drawing.Point(221, 26)
        Me.cmbNaturaleza1.Name = "cmbNaturaleza1"
        Me.cmbNaturaleza1.Size = New System.Drawing.Size(100, 24)
        Me.cmbNaturaleza1.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cmbNaturaleza1, "Naturaleza de la cuenta")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(137, 29)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 16)
        Me.Label15.TabIndex = 102
        Me.Label15.Text = "Naturaleza:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(236, 449)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(0, 16)
        Me.Label14.TabIndex = 101
        '
        'cmbTipo1
        '
        Me.cmbTipo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipo1.FormattingEnabled = True
        Me.cmbTipo1.Items.AddRange(New Object() {"ACTÍVO", "PASÍVO", "CAPITAL", "INGRESOS", "COSTOS", "EGRESOS", "ORDEN"})
        Me.cmbTipo1.Location = New System.Drawing.Point(50, 25)
        Me.cmbTipo1.Name = "cmbTipo1"
        Me.cmbTipo1.Size = New System.Drawing.Size(84, 24)
        Me.cmbTipo1.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbTipo1, "Tipo de cuenta")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(7, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 16)
        Me.Label10.TabIndex = 99
        Me.Label10.Text = "Tipo:"
        '
        'btnEliminar
        '
        Me.btnEliminar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(253, 442)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(75, 25)
        Me.btnEliminar.TabIndex = 6
        Me.btnEliminar.Text = "Eliminar"
        Me.ToolTip1.SetToolTip(Me.btnEliminar, "Eliminar cuenta")
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnGuardar.Enabled = False
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(91, 442)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 25)
        Me.btnGuardar.TabIndex = 4
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'cmbNivel1
        '
        Me.cmbNivel1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNivel1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNivel1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNivel1.FormattingEnabled = True
        Me.cmbNivel1.Location = New System.Drawing.Point(114, 20)
        Me.cmbNivel1.MaxLength = 5
        Me.cmbNivel1.Name = "cmbNivel1"
        Me.cmbNivel1.Size = New System.Drawing.Size(99, 24)
        Me.cmbNivel1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbNivel1, "Cuenta de Nivel 1")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(53, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 16)
        Me.Label1.TabIndex = 103
        Me.Label1.Text = "Nivel 1:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(53, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 16)
        Me.Label2.TabIndex = 115
        Me.Label2.Text = "Nivel 2:"
        '
        'cmbNivel2
        '
        Me.cmbNivel2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNivel2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNivel2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNivel2.FormattingEnabled = True
        Me.cmbNivel2.Location = New System.Drawing.Point(114, 47)
        Me.cmbNivel2.MaxLength = 5
        Me.cmbNivel2.Name = "cmbNivel2"
        Me.cmbNivel2.Size = New System.Drawing.Size(99, 24)
        Me.cmbNivel2.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cmbNivel2, "Cuenta de Nivel 2")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(53, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 16)
        Me.Label3.TabIndex = 117
        Me.Label3.Text = "Nivel 3:"
        '
        'cmbNivel3
        '
        Me.cmbNivel3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNivel3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNivel3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNivel3.FormattingEnabled = True
        Me.cmbNivel3.Location = New System.Drawing.Point(114, 74)
        Me.cmbNivel3.MaxLength = 5
        Me.cmbNivel3.Name = "cmbNivel3"
        Me.cmbNivel3.Size = New System.Drawing.Size(99, 24)
        Me.cmbNivel3.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.cmbNivel3, "Cuenta de Nivel 3")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(53, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 16)
        Me.Label4.TabIndex = 119
        Me.Label4.Text = "Nivel 4:"
        '
        'cmbNivel4
        '
        Me.cmbNivel4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNivel4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNivel4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNivel4.FormattingEnabled = True
        Me.cmbNivel4.Location = New System.Drawing.Point(114, 101)
        Me.cmbNivel4.MaxLength = 5
        Me.cmbNivel4.Name = "cmbNivel4"
        Me.cmbNivel4.Size = New System.Drawing.Size(99, 24)
        Me.cmbNivel4.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.cmbNivel4, "Cuenta de Nivel 4")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(53, 130)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 16)
        Me.Label7.TabIndex = 121
        Me.Label7.Text = "Nivel 5:"
        '
        'cmbNivel5
        '
        Me.cmbNivel5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNivel5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNivel5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNivel5.FormattingEnabled = True
        Me.cmbNivel5.Location = New System.Drawing.Point(114, 128)
        Me.cmbNivel5.MaxLength = 5
        Me.cmbNivel5.Name = "cmbNivel5"
        Me.cmbNivel5.Size = New System.Drawing.Size(99, 24)
        Me.cmbNivel5.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.cmbNivel5, "Cuenta de Nivel 5")
        '
        'grpNiveles
        '
        Me.grpNiveles.Controls.Add(Me.cmbNomNivel1)
        Me.grpNiveles.Controls.Add(Me.cmbNomNivel5)
        Me.grpNiveles.Controls.Add(Me.cmbNomNivel2)
        Me.grpNiveles.Controls.Add(Me.cmbNomNivel4)
        Me.grpNiveles.Controls.Add(Me.cmbNomNivel3)
        Me.grpNiveles.Controls.Add(Me.Label7)
        Me.grpNiveles.Controls.Add(Me.cmbNivel1)
        Me.grpNiveles.Controls.Add(Me.cmbNivel5)
        Me.grpNiveles.Controls.Add(Me.Label1)
        Me.grpNiveles.Controls.Add(Me.Label4)
        Me.grpNiveles.Controls.Add(Me.cmbNivel2)
        Me.grpNiveles.Controls.Add(Me.cmbNivel4)
        Me.grpNiveles.Controls.Add(Me.Label2)
        Me.grpNiveles.Controls.Add(Me.Label3)
        Me.grpNiveles.Controls.Add(Me.cmbNivel3)
        Me.grpNiveles.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpNiveles.ForeColor = System.Drawing.Color.Black
        Me.grpNiveles.Location = New System.Drawing.Point(8, 12)
        Me.grpNiveles.Name = "grpNiveles"
        Me.grpNiveles.Size = New System.Drawing.Size(474, 160)
        Me.grpNiveles.TabIndex = 1
        Me.grpNiveles.TabStop = False
        Me.grpNiveles.Text = "Niveles"
        '
        'cmbNomNivel1
        '
        Me.cmbNomNivel1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNomNivel1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNomNivel1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNomNivel1.FormattingEnabled = True
        Me.cmbNomNivel1.Location = New System.Drawing.Point(219, 20)
        Me.cmbNomNivel1.Name = "cmbNomNivel1"
        Me.cmbNomNivel1.Size = New System.Drawing.Size(232, 24)
        Me.cmbNomNivel1.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbNomNivel1, "Cuenta de Nivel 1")
        '
        'cmbNomNivel5
        '
        Me.cmbNomNivel5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNomNivel5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNomNivel5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNomNivel5.FormattingEnabled = True
        Me.cmbNomNivel5.Location = New System.Drawing.Point(219, 128)
        Me.cmbNomNivel5.Name = "cmbNomNivel5"
        Me.cmbNomNivel5.Size = New System.Drawing.Size(232, 24)
        Me.cmbNomNivel5.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.cmbNomNivel5, "Cuenta de Nivel 5")
        '
        'cmbNomNivel2
        '
        Me.cmbNomNivel2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNomNivel2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNomNivel2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNomNivel2.FormattingEnabled = True
        Me.cmbNomNivel2.Location = New System.Drawing.Point(219, 47)
        Me.cmbNomNivel2.Name = "cmbNomNivel2"
        Me.cmbNomNivel2.Size = New System.Drawing.Size(232, 24)
        Me.cmbNomNivel2.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbNomNivel2, "Cuenta de Nivel 2")
        '
        'cmbNomNivel4
        '
        Me.cmbNomNivel4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNomNivel4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNomNivel4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNomNivel4.FormattingEnabled = True
        Me.cmbNomNivel4.Location = New System.Drawing.Point(219, 101)
        Me.cmbNomNivel4.Name = "cmbNomNivel4"
        Me.cmbNomNivel4.Size = New System.Drawing.Size(232, 24)
        Me.cmbNomNivel4.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.cmbNomNivel4, "Cuenta de Nivel 4")
        '
        'cmbNomNivel3
        '
        Me.cmbNomNivel3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbNomNivel3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbNomNivel3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbNomNivel3.FormattingEnabled = True
        Me.cmbNomNivel3.Location = New System.Drawing.Point(219, 74)
        Me.cmbNomNivel3.Name = "cmbNomNivel3"
        Me.cmbNomNivel3.Size = New System.Drawing.Size(232, 24)
        Me.cmbNomNivel3.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.cmbNomNivel3, "Cuenta de Nivel 3")
        '
        'grpCuenta
        '
        Me.grpCuenta.Controls.Add(Me.chkDIOT)
        Me.grpCuenta.Controls.Add(Me.dtpFecha)
        Me.grpCuenta.Controls.Add(Me.Label8)
        Me.grpCuenta.Controls.Add(Me.Label10)
        Me.grpCuenta.Controls.Add(Me.cmbNaturaleza1)
        Me.grpCuenta.Controls.Add(Me.cmbTipo1)
        Me.grpCuenta.Controls.Add(Me.Label15)
        Me.grpCuenta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCuenta.ForeColor = System.Drawing.Color.Black
        Me.grpCuenta.Location = New System.Drawing.Point(8, 178)
        Me.grpCuenta.Name = "grpCuenta"
        Me.grpCuenta.Size = New System.Drawing.Size(474, 81)
        Me.grpCuenta.TabIndex = 2
        Me.grpCuenta.TabStop = False
        Me.grpCuenta.Text = "Datos de cuenta"
        '
        'chkDIOT
        '
        Me.chkDIOT.AutoSize = True
        Me.chkDIOT.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDIOT.Location = New System.Drawing.Point(56, 55)
        Me.chkDIOT.Name = "chkDIOT"
        Me.chkDIOT.Size = New System.Drawing.Size(148, 20)
        Me.chkDIOT.TabIndex = 110
        Me.chkDIOT.Text = "Para verificador DIOT"
        Me.chkDIOT.UseVisualStyleBackColor = True
        '
        'dtpFecha
        '
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(379, 27)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(86, 22)
        Me.dtpFecha.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.dtpFecha, "Fecha de creación de la cuenta")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(325, 30)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 16)
        Me.Label8.TabIndex = 104
        Me.Label8.Text = "Fecha:"
        '
        'btnNuevoTodo
        '
        Me.btnNuevoTodo.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNuevoTodo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoTodo.Location = New System.Drawing.Point(334, 442)
        Me.btnNuevoTodo.Name = "btnNuevoTodo"
        Me.btnNuevoTodo.Size = New System.Drawing.Size(75, 25)
        Me.btnNuevoTodo.TabIndex = 7
        Me.btnNuevoTodo.Text = "Limpiar"
        Me.ToolTip1.SetToolTip(Me.btnNuevoTodo, "Nueva cuenta")
        Me.btnNuevoTodo.UseVisualStyleBackColor = True
        '
        'cmbSubTipo
        '
        Me.cmbSubTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSubTipo.Enabled = False
        Me.cmbSubTipo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSubTipo.FormattingEnabled = True
        Me.cmbSubTipo.Items.AddRange(New Object() {"ACTÍVO", "PASÍVO", "CAPITAL", "INGRESOS", "EGRESOS", "ORDEN"})
        Me.cmbSubTipo.Location = New System.Drawing.Point(86, 52)
        Me.cmbSubTipo.Name = "cmbSubTipo"
        Me.cmbSubTipo.Size = New System.Drawing.Size(190, 24)
        Me.cmbSubTipo.TabIndex = 105
        Me.ToolTip1.SetToolTip(Me.cmbSubTipo, "Tipo de cuenta")
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Items.AddRange(New Object() {"ACTÍVO", "PASÍVO", "CAPITAL CONTABLE", "INGRESOS", "COSTOS", "GASTOS", "RESULTADO INTEGRAL DE FINANCIAMIENTO", "CUENTAS DE ORDEN", "OTROS"})
        Me.cmbTipo.Location = New System.Drawing.Point(86, 23)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(190, 24)
        Me.cmbTipo.TabIndex = 105
        Me.ToolTip1.SetToolTip(Me.cmbTipo, "Tipo de cuenta")
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button1.Location = New System.Drawing.Point(718, 487)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 25)
        Me.Button1.TabIndex = 113
        Me.Button1.Text = "Importar"
        Me.ToolTip1.SetToolTip(Me.Button1, "Importar")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button2.Location = New System.Drawing.Point(799, 487)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(89, 25)
        Me.Button2.TabIndex = 115
        Me.Button2.Text = "Información"
        Me.ToolTip1.SetToolTip(Me.Button2, "Información importante antes de importar un archivo.")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(637, 487)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(75, 25)
        Me.btnImprimir.TabIndex = 112
        Me.btnImprimir.Text = "Imprimir"
        Me.ToolTip1.SetToolTip(Me.btnImprimir, "Importar")
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnXML
        '
        Me.btnXML.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnXML.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnXML.Location = New System.Drawing.Point(556, 487)
        Me.btnXML.Name = "btnXML"
        Me.btnXML.Size = New System.Drawing.Size(75, 25)
        Me.btnXML.TabIndex = 8
        Me.btnXML.Text = "XML"
        Me.ToolTip1.SetToolTip(Me.btnXML, "Nueva cuenta")
        Me.btnXML.UseVisualStyleBackColor = True
        '
        'cmbCuenta
        '
        Me.cmbCuenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCuenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCuenta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCuenta.FormattingEnabled = True
        Me.cmbCuenta.Location = New System.Drawing.Point(86, 81)
        Me.cmbCuenta.Name = "cmbCuenta"
        Me.cmbCuenta.Size = New System.Drawing.Size(365, 24)
        Me.cmbCuenta.TabIndex = 106
        Me.ToolTip1.SetToolTip(Me.cmbCuenta, "Agrupadores")
        '
        'btnModificar
        '
        Me.btnModificar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnModificar.Enabled = False
        Me.btnModificar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModificar.Location = New System.Drawing.Point(172, 442)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(75, 25)
        Me.btnModificar.TabIndex = 5
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 16)
        Me.Label11.TabIndex = 106
        Me.Label11.Text = "Sub Tipo:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(5, 85)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 16)
        Me.Label12.TabIndex = 108
        Me.Label12.Text = "Agrupador:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbCuenta)
        Me.GroupBox1.Controls.Add(Me.ckAplicarSubcuentas)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cmbTipo)
        Me.GroupBox1.Controls.Add(Me.cmbSubTipo)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(8, 267)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(474, 136)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Equivalencia"
        '
        'ckAplicarSubcuentas
        '
        Me.ckAplicarSubcuentas.AutoSize = True
        Me.ckAplicarSubcuentas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckAplicarSubcuentas.Location = New System.Drawing.Point(86, 106)
        Me.ckAplicarSubcuentas.Name = "ckAplicarSubcuentas"
        Me.ckAplicarSubcuentas.Size = New System.Drawing.Size(292, 20)
        Me.ckAplicarSubcuentas.TabIndex = 109
        Me.ckAplicarSubcuentas.Text = "Aplicar este agrupador SAT a las subcuentas."
        Me.ckAplicarSubcuentas.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(44, 25)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(40, 16)
        Me.Label13.TabIndex = 106
        Me.Label13.Text = "Tipo:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(11, 408)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(113, 20)
        Me.CheckBox1.TabIndex = 111
        Me.CheckBox1.Text = "Descontinuada"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'frmContabilidadClasificaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(1004, 518)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.btnXML)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnModificar)
        Me.Controls.Add(Me.btnNuevoTodo)
        Me.Controls.Add(Me.grpCuenta)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.grpNiveles)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.grpBuscar)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmContabilidadClasificaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Contabilidad - Catálogo de cuentas"
        Me.grpBuscar.ResumeLayout(false)
        Me.grpBuscar.PerformLayout
        CType(Me.DataGridView1,System.ComponentModel.ISupportInitialize).EndInit
        Me.grpNiveles.ResumeLayout(false)
        Me.grpNiveles.PerformLayout
        Me.grpCuenta.ResumeLayout(false)
        Me.grpCuenta.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents grpBuscar As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents cmbNaturaleza1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbTipo1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents cmbNivel1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbNivel2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbNivel3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbNivel4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbNivel5 As System.Windows.Forms.ComboBox
    Friend WithEvents grpNiveles As System.Windows.Forms.GroupBox
    Friend WithEvents grpCuenta As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnNuevoTodo As System.Windows.Forms.Button
    Friend WithEvents cmbNomNivel1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbNomNivel5 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbNomNivel2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbNomNivel4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbNomNivel3 As System.Windows.Forms.ComboBox
    Friend WithEvents btnModificar As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbSubTipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents ckAplicarSubcuentas As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents chkDIOT As System.Windows.Forms.CheckBox
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnXML As System.Windows.Forms.Button
    Friend WithEvents cmbCuenta As System.Windows.Forms.ComboBox
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents nivel1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tipo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
End Class
