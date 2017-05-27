<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGastosProgramables
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbPagoUnico = New System.Windows.Forms.RadioButton()
        Me.pnlPagoUnico = New System.Windows.Forms.Panel()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.rdbPagoConstante = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlPagoConstante = New System.Windows.Forms.Panel()
        Me.cmbDia = New System.Windows.Forms.ComboBox()
        Me.DGDetalles = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.nmrDias = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkPeriodo = New System.Windows.Forms.CheckBox()
        Me.txtBusqueda = New System.Windows.Forms.TextBox()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.dtpFinal = New System.Windows.Forms.DateTimePicker()
        Me.dtpInicio = New System.Windows.Forms.DateTimePicker()
        Me.chkMantenerDatos = New System.Windows.Forms.CheckBox()
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        Me.grpPeriodo = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.pnlPagoUnico.SuspendLayout()
        Me.pnlPagoConstante.SuspendLayout()
        CType(Me.DGDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nmrDias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPeriodo.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbPagoUnico)
        Me.GroupBox1.Controls.Add(Me.chkEstado)
        Me.GroupBox1.Controls.Add(Me.pnlPagoUnico)
        Me.GroupBox1.Controls.Add(Me.rdbPagoConstante)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDescripcion)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(383, 145)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del gasto"
        '
        'rdbPagoUnico
        '
        Me.rdbPagoUnico.AutoSize = True
        Me.rdbPagoUnico.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.rdbPagoUnico.Location = New System.Drawing.Point(217, 19)
        Me.rdbPagoUnico.Name = "rdbPagoUnico"
        Me.rdbPagoUnico.Size = New System.Drawing.Size(91, 20)
        Me.rdbPagoUnico.TabIndex = 39
        Me.rdbPagoUnico.Text = "Pago único"
        Me.rdbPagoUnico.UseVisualStyleBackColor = True
        '
        'pnlPagoUnico
        '
        Me.pnlPagoUnico.Controls.Add(Me.dtpFecha)
        Me.pnlPagoUnico.Location = New System.Drawing.Point(71, 90)
        Me.pnlPagoUnico.Name = "pnlPagoUnico"
        Me.pnlPagoUnico.Size = New System.Drawing.Size(185, 26)
        Me.pnlPagoUnico.TabIndex = 39
        Me.pnlPagoUnico.Visible = False
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(3, 3)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(115, 20)
        Me.dtpFecha.TabIndex = 38
        '
        'rdbPagoConstante
        '
        Me.rdbPagoConstante.AutoSize = True
        Me.rdbPagoConstante.Checked = True
        Me.rdbPagoConstante.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.rdbPagoConstante.Location = New System.Drawing.Point(100, 19)
        Me.rdbPagoConstante.Name = "rdbPagoConstante"
        Me.rdbPagoConstante.Size = New System.Drawing.Size(117, 20)
        Me.rdbPagoConstante.TabIndex = 38
        Me.rdbPagoConstante.TabStop = True
        Me.rdbPagoConstante.Text = "Pago constante"
        Me.rdbPagoConstante.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 16)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Fecha:"
        '
        'txtDescripcion
        '
        Me.txtDescripcion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescripcion.Location = New System.Drawing.Point(74, 52)
        Me.txtDescripcion.MaxLength = 50
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.Size = New System.Drawing.Size(270, 22)
        Me.txtDescripcion.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.txtDescripcion, "Nombre del gasto")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 53)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 16)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "Nombre:"
        '
        'pnlPagoConstante
        '
        Me.pnlPagoConstante.Controls.Add(Me.cmbDia)
        Me.pnlPagoConstante.Location = New System.Drawing.Point(76, 94)
        Me.pnlPagoConstante.Name = "pnlPagoConstante"
        Me.pnlPagoConstante.Size = New System.Drawing.Size(185, 26)
        Me.pnlPagoConstante.TabIndex = 40
        '
        'cmbDia
        '
        Me.cmbDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDia.FormattingEnabled = True
        Me.cmbDia.Items.AddRange(New Object() {"Último", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.cmbDia.Location = New System.Drawing.Point(2, 3)
        Me.cmbDia.Name = "cmbDia"
        Me.cmbDia.Size = New System.Drawing.Size(115, 21)
        Me.cmbDia.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.cmbDia, "Día del mes")
        '
        'DGDetalles
        '
        Me.DGDetalles.AllowUserToAddRows = False
        Me.DGDetalles.AllowUserToDeleteRows = False
        Me.DGDetalles.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DGDetalles.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDetalles.Location = New System.Drawing.Point(394, 76)
        Me.DGDetalles.Name = "DGDetalles"
        Me.DGDetalles.ReadOnly = True
        Me.DGDetalles.RowHeadersVisible = False
        Me.DGDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGDetalles.Size = New System.Drawing.Size(408, 225)
        Me.DGDetalles.TabIndex = 3
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.nmrDias)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 164)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(383, 68)
        Me.GroupBox2.TabIndex = 37
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Datos la Notificación"
        Me.GroupBox2.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(177, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "días antes."
        '
        'nmrDias
        '
        Me.nmrDias.Location = New System.Drawing.Point(103, 28)
        Me.nmrDias.Name = "nmrDias"
        Me.nmrDias.Size = New System.Drawing.Size(70, 20)
        Me.nmrDias.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.nmrDias, "Días.")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 16)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Avisar desde:"
        '
        'chkPeriodo
        '
        Me.chkPeriodo.AutoSize = True
        Me.chkPeriodo.Checked = True
        Me.chkPeriodo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPeriodo.Location = New System.Drawing.Point(292, 30)
        Me.chkPeriodo.Name = "chkPeriodo"
        Me.chkPeriodo.Size = New System.Drawing.Size(64, 17)
        Me.chkPeriodo.TabIndex = 38
        Me.chkPeriodo.Text = "Siempre"
        Me.ToolTip1.SetToolTip(Me.chkPeriodo, "Aplicar siempre.")
        Me.chkPeriodo.UseVisualStyleBackColor = True
        '
        'txtBusqueda
        '
        Me.txtBusqueda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBusqueda.Location = New System.Drawing.Point(460, 15)
        Me.txtBusqueda.MaxLength = 45
        Me.txtBusqueda.Multiline = True
        Me.txtBusqueda.Name = "txtBusqueda"
        Me.txtBusqueda.Size = New System.Drawing.Size(270, 22)
        Me.txtBusqueda.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.txtBusqueda, "Nombre del gasto")
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Items.AddRange(New Object() {"Todos", "Pago Constante", "Pago único"})
        Me.cmbTipo.Location = New System.Drawing.Point(460, 43)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(161, 21)
        Me.cmbTipo.TabIndex = 41
        Me.ToolTip1.SetToolTip(Me.cmbTipo, "Día del mes")
        '
        'dtpFinal
        '
        Me.dtpFinal.CustomFormat = "yyyy/MM/dd"
        Me.dtpFinal.Enabled = False
        Me.dtpFinal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFinal.Location = New System.Drawing.Point(201, 26)
        Me.dtpFinal.Name = "dtpFinal"
        Me.dtpFinal.Size = New System.Drawing.Size(88, 22)
        Me.dtpFinal.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.dtpFinal, "Fecha final.")
        '
        'dtpInicio
        '
        Me.dtpInicio.CustomFormat = "yyyy/MM/dd"
        Me.dtpInicio.Enabled = False
        Me.dtpInicio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInicio.Location = New System.Drawing.Point(59, 26)
        Me.dtpInicio.Name = "dtpInicio"
        Me.dtpInicio.Size = New System.Drawing.Size(88, 22)
        Me.dtpInicio.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.dtpInicio, "Fecha inicial.")
        '
        'chkMantenerDatos
        '
        Me.chkMantenerDatos.AutoSize = True
        Me.chkMantenerDatos.Location = New System.Drawing.Point(394, 309)
        Me.chkMantenerDatos.Name = "chkMantenerDatos"
        Me.chkMantenerDatos.Size = New System.Drawing.Size(100, 17)
        Me.chkMantenerDatos.TabIndex = 46
        Me.chkMantenerDatos.Text = "Mantener datos"
        Me.ToolTip1.SetToolTip(Me.chkMantenerDatos, "Mantener los datos del formulario.")
        Me.chkMantenerDatos.UseVisualStyleBackColor = True
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Checked = True
        Me.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEstado.Location = New System.Drawing.Point(74, 124)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(68, 17)
        Me.chkEstado.TabIndex = 47
        Me.chkEstado.Text = "Activado"
        Me.ToolTip1.SetToolTip(Me.chkEstado, "Mantener los datos del formulario.")
        Me.chkEstado.UseVisualStyleBackColor = True
        '
        'grpPeriodo
        '
        Me.grpPeriodo.Controls.Add(Me.chkPeriodo)
        Me.grpPeriodo.Controls.Add(Me.dtpFinal)
        Me.grpPeriodo.Controls.Add(Me.dtpInicio)
        Me.grpPeriodo.Controls.Add(Me.Label8)
        Me.grpPeriodo.Controls.Add(Me.Label10)
        Me.grpPeriodo.Location = New System.Drawing.Point(5, 238)
        Me.grpPeriodo.Name = "grpPeriodo"
        Me.grpPeriodo.Size = New System.Drawing.Size(383, 63)
        Me.grpPeriodo.TabIndex = 42
        Me.grpPeriodo.TabStop = False
        Me.grpPeriodo.Text = "Periodo de tiempo"
        Me.grpPeriodo.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(158, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 16)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Fin:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 16)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Inicio:"
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(219, 332)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 43
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(308, 332)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminar.TabIndex = 44
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(393, 332)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(75, 23)
        Me.btnNuevo.TabIndex = 45
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(396, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 16)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Nombre:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(418, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 16)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Tipo:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(482, 332)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 48
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmGastosProgramables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(810, 360)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbTipo)
        Me.Controls.Add(Me.txtBusqueda)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.chkMantenerDatos)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.grpPeriodo)
        Me.Controls.Add(Me.pnlPagoConstante)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DGDetalles)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGastosProgramables"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Programar Gastos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlPagoUnico.ResumeLayout(False)
        Me.pnlPagoConstante.ResumeLayout(False)
        CType(Me.DGDetalles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nmrDias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPeriodo.ResumeLayout(False)
        Me.grpPeriodo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DGDetalles As System.Windows.Forms.DataGridView
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents nmrDias As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdbPagoUnico As System.Windows.Forms.RadioButton
    Friend WithEvents rdbPagoConstante As System.Windows.Forms.RadioButton
    Friend WithEvents pnlPagoUnico As System.Windows.Forms.Panel
    Friend WithEvents pnlPagoConstante As System.Windows.Forms.Panel
    Friend WithEvents cmbDia As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grpPeriodo As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtpFinal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkPeriodo As System.Windows.Forms.CheckBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents chkMantenerDatos As System.Windows.Forms.CheckBox
    Friend WithEvents txtBusqueda As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkEstado As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
