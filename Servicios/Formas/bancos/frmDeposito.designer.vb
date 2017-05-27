<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeposito
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.chkFecha = New System.Windows.Forms.CheckBox()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtReferencia = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.drpDeposito = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnImprimirDeposito = New System.Windows.Forms.Button()
        Me.txtComentario = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbCuenta = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.grpBusqueda = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnLimpiarBusqueda = New System.Windows.Forms.Button()
        Me.ckbProvedor = New System.Windows.Forms.CheckBox()
        Me.ckbFecha = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtBuscRef = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnBuscarCC = New System.Windows.Forms.Button()
        Me.btnEliminar2 = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.txtCuenta = New System.Windows.Forms.TextBox()
        Me.txtPoliza = New System.Windows.Forms.TextBox()
        Me.btnGuardarPoliza = New System.Windows.Forms.Button()
        Me.lblDiferencia = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblAbonos = New System.Windows.Forms.Label()
        Me.lblCargos = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtRegistra = New System.Windows.Forms.TextBox()
        Me.txtAutoriza = New System.Windows.Forms.TextBox()
        Me.txtElabora = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnLimpiarRenglon = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.drpDeposito.SuspendLayout()
        Me.grpBusqueda.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkFecha
        '
        Me.chkFecha.AutoSize = True
        Me.chkFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFecha.Location = New System.Drawing.Point(183, 77)
        Me.chkFecha.Name = "chkFecha"
        Me.chkFecha.Size = New System.Drawing.Size(15, 14)
        Me.chkFecha.TabIndex = 2
        Me.chkFecha.TabStop = False
        Me.ToolTip1.SetToolTip(Me.chkFecha, "Mantener la fecha estática")
        Me.chkFecha.UseVisualStyleBackColor = True
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFecha.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFecha.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(86, 73)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(85, 22)
        Me.dtpFecha.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(35, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 149
        Me.Label3.Text = "Fecha:"
        '
        'txtReferencia
        '
        Me.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReferencia.Location = New System.Drawing.Point(86, 100)
        Me.txtReferencia.MaxLength = 80
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.Size = New System.Drawing.Size(307, 22)
        Me.txtReferencia.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 16)
        Me.Label4.TabIndex = 153
        Me.Label4.Text = "Referencia:"
        '
        'txtCantidad
        '
        Me.txtCantidad.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.Location = New System.Drawing.Point(86, 130)
        Me.txtCantidad.MaxLength = 20
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(114, 22)
        Me.txtCantidad.TabIndex = 5
        Me.txtCantidad.Text = "0.00"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 16)
        Me.Label6.TabIndex = 157
        Me.Label6.Text = "Cantidad:"
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Location = New System.Drawing.Point(225, 250)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(81, 26)
        Me.btnLimpiar.TabIndex = 9
        Me.btnLimpiar.Text = "Nuevo"
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(124, 250)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(81, 26)
        Me.btnEliminar.TabIndex = 8
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'drpDeposito
        '
        Me.drpDeposito.Controls.Add(Me.Label12)
        Me.drpDeposito.Controls.Add(Me.DateTimePicker1)
        Me.drpDeposito.Controls.Add(Me.Label7)
        Me.drpDeposito.Controls.Add(Me.Button3)
        Me.drpDeposito.Controls.Add(Me.Button1)
        Me.drpDeposito.Controls.Add(Me.btnImprimirDeposito)
        Me.drpDeposito.Controls.Add(Me.txtComentario)
        Me.drpDeposito.Controls.Add(Me.Label5)
        Me.drpDeposito.Controls.Add(Me.cmbCuenta)
        Me.drpDeposito.Controls.Add(Me.Label3)
        Me.drpDeposito.Controls.Add(Me.Label31)
        Me.drpDeposito.Controls.Add(Me.dtpFecha)
        Me.drpDeposito.Controls.Add(Me.chkFecha)
        Me.drpDeposito.Controls.Add(Me.Label4)
        Me.drpDeposito.Controls.Add(Me.txtCantidad)
        Me.drpDeposito.Controls.Add(Me.txtReferencia)
        Me.drpDeposito.Controls.Add(Me.Label6)
        Me.drpDeposito.Location = New System.Drawing.Point(1, 4)
        Me.drpDeposito.Name = "drpDeposito"
        Me.drpDeposito.Size = New System.Drawing.Size(412, 237)
        Me.drpDeposito.TabIndex = 1
        Me.drpDeposito.TabStop = False
        Me.drpDeposito.Text = "Depósito"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(204, 75)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(92, 16)
        Me.Label12.TabIndex = 191
        Me.Label12.Text = "Fecha Conta:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DateTimePicker1.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.DateTimePicker1.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.DateTimePicker1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(299, 73)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(85, 22)
        Me.DateTimePicker1.TabIndex = 3
        Me.DateTimePicker1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(88, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 16)
        Me.Label7.TabIndex = 189
        Me.Label7.Text = "Folio:"
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(210, 202)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(145, 26)
        Me.Button3.TabIndex = 188
        Me.Button3.TabStop = False
        Me.Button3.Text = "Ver Facturas ligadas"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(48, 202)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 26)
        Me.Button1.TabIndex = 183
        Me.Button1.TabStop = False
        Me.Button1.Text = "Seleccionar Facturas"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnImprimirDeposito
        '
        Me.btnImprimirDeposito.Enabled = False
        Me.btnImprimirDeposito.Location = New System.Drawing.Point(378, 190)
        Me.btnImprimirDeposito.Name = "btnImprimirDeposito"
        Me.btnImprimirDeposito.Size = New System.Drawing.Size(28, 24)
        Me.btnImprimirDeposito.TabIndex = 187
        Me.btnImprimirDeposito.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnImprimirDeposito, "Imprimir depósito")
        Me.btnImprimirDeposito.UseVisualStyleBackColor = True
        '
        'txtComentario
        '
        Me.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComentario.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComentario.Location = New System.Drawing.Point(86, 158)
        Me.txtComentario.Multiline = True
        Me.txtComentario.Name = "txtComentario"
        Me.txtComentario.Size = New System.Drawing.Size(269, 37)
        Me.txtComentario.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 16)
        Me.Label5.TabIndex = 185
        Me.Label5.Text = "Comentario:"
        '
        'cmbCuenta
        '
        Me.cmbCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCuenta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCuenta.FormattingEnabled = True
        Me.cmbCuenta.Location = New System.Drawing.Point(86, 44)
        Me.cmbCuenta.Name = "cmbCuenta"
        Me.cmbCuenta.Size = New System.Drawing.Size(320, 24)
        Me.cmbCuenta.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmbCuenta, "Selecciona una cuenta")
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(23, 47)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(57, 16)
        Me.Label31.TabIndex = 183
        Me.Label31.Text = "Cuenta:"
        '
        'grpBusqueda
        '
        Me.grpBusqueda.Controls.Add(Me.Label8)
        Me.grpBusqueda.Controls.Add(Me.TextBox1)
        Me.grpBusqueda.Controls.Add(Me.btnLimpiarBusqueda)
        Me.grpBusqueda.Controls.Add(Me.ckbProvedor)
        Me.grpBusqueda.Controls.Add(Me.ckbFecha)
        Me.grpBusqueda.Controls.Add(Me.DataGridView1)
        Me.grpBusqueda.Controls.Add(Me.txtBuscRef)
        Me.grpBusqueda.Controls.Add(Me.Label2)
        Me.grpBusqueda.Controls.Add(Me.dtpHasta)
        Me.grpBusqueda.Controls.Add(Me.dtpDesde)
        Me.grpBusqueda.Controls.Add(Me.Label10)
        Me.grpBusqueda.Controls.Add(Me.Label9)
        Me.grpBusqueda.Location = New System.Drawing.Point(414, 4)
        Me.grpBusqueda.Name = "grpBusqueda"
        Me.grpBusqueda.Size = New System.Drawing.Size(587, 254)
        Me.grpBusqueda.TabIndex = 36
        Me.grpBusqueda.TabStop = False
        Me.grpBusqueda.Text = "Consulta"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(408, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 16)
        Me.Label8.TabIndex = 134
        Me.Label8.Text = "Folio:"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(405, 37)
        Me.TextBox1.MaxLength = 80
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(61, 22)
        Me.TextBox1.TabIndex = 133
        Me.ToolTip1.SetToolTip(Me.TextBox1, "Busqueda por referencia")
        '
        'btnLimpiarBusqueda
        '
        Me.btnLimpiarBusqueda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiarBusqueda.Location = New System.Drawing.Point(502, 19)
        Me.btnLimpiarBusqueda.Name = "btnLimpiarBusqueda"
        Me.btnLimpiarBusqueda.Size = New System.Drawing.Size(75, 25)
        Me.btnLimpiarBusqueda.TabIndex = 132
        Me.btnLimpiarBusqueda.Text = "Limpiar"
        Me.btnLimpiarBusqueda.UseVisualStyleBackColor = True
        Me.btnLimpiarBusqueda.Visible = False
        '
        'ckbProvedor
        '
        Me.ckbProvedor.AutoSize = True
        Me.ckbProvedor.Checked = True
        Me.ckbProvedor.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbProvedor.Location = New System.Drawing.Point(41, 42)
        Me.ckbProvedor.Name = "ckbProvedor"
        Me.ckbProvedor.Size = New System.Drawing.Size(15, 14)
        Me.ckbProvedor.TabIndex = 4
        Me.ckbProvedor.UseVisualStyleBackColor = True
        Me.ckbProvedor.UseWaitCursor = True
        Me.ckbProvedor.Visible = False
        '
        'ckbFecha
        '
        Me.ckbFecha.AutoSize = True
        Me.ckbFecha.Checked = True
        Me.ckbFecha.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckbFecha.Location = New System.Drawing.Point(41, 17)
        Me.ckbFecha.Name = "ckbFecha"
        Me.ckbFecha.Size = New System.Drawing.Size(15, 14)
        Me.ckbFecha.TabIndex = 1
        Me.ckbFecha.UseVisualStyleBackColor = True
        Me.ckbFecha.UseWaitCursor = True
        Me.ckbFecha.Visible = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.Location = New System.Drawing.Point(4, 68)
        Me.DataGridView1.MultiSelect = false
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = true
        Me.DataGridView1.RowHeadersVisible = false
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(583, 179)
        Me.DataGridView1.TabIndex = 6
        Me.DataGridView1.TabStop = false
        '
        'txtBuscRef
        '
        Me.txtBuscRef.BackColor = System.Drawing.Color.White
        Me.txtBuscRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscRef.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtBuscRef.Location = New System.Drawing.Point(144, 38)
        Me.txtBuscRef.MaxLength = 80
        Me.txtBuscRef.Name = "txtBuscRef"
        Me.txtBuscRef.Size = New System.Drawing.Size(255, 22)
        Me.txtBuscRef.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtBuscRef, "Busqueda por referencia")
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(62, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 16)
        Me.Label2.TabIndex = 127
        Me.Label2.Text = "Referencia:"
        '
        'dtpHasta
        '
        Me.dtpHasta.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.dtpHasta.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpHasta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(296, 13)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(92, 22)
        Me.dtpHasta.TabIndex = 3
        '
        'dtpDesde
        '
        Me.dtpDesde.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(255,Byte),Integer))
        Me.dtpDesde.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpDesde.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesde.Location = New System.Drawing.Point(144, 13)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(92, 22)
        Me.dtpDesde.TabIndex = 2
        '
        'Label10
        '
        Me.Label10.AutoSize = true
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label10.Location = New System.Drawing.Point(242, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(47, 16)
        Me.Label10.TabIndex = 124
        Me.Label10.Text = "Hasta:"
        '
        'Label9
        '
        Me.Label9.AutoSize = true
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label9.Location = New System.Drawing.Point(87, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 16)
        Me.Label9.TabIndex = 122
        Me.Label9.Text = "Fecha:"
        '
        'btnBuscarCC
        '
        Me.btnBuscarCC.Location = New System.Drawing.Point(22, 335)
        Me.btnBuscarCC.Name = "btnBuscarCC"
        Me.btnBuscarCC.Size = New System.Drawing.Size(26, 23)
        Me.btnBuscarCC.TabIndex = 2
        Me.btnBuscarCC.Text = "..."
        Me.ToolTip1.SetToolTip(Me.btnBuscarCC, "Búsqueda de cuenta contable")
        Me.btnBuscarCC.UseVisualStyleBackColor = true
        Me.btnBuscarCC.Visible = false
        '
        'btnEliminar2
        '
        Me.btnEliminar2.Enabled = false
        Me.btnEliminar2.Location = New System.Drawing.Point(907, 339)
        Me.btnEliminar2.Name = "btnEliminar2"
        Me.btnEliminar2.Size = New System.Drawing.Size(75, 23)
        Me.btnEliminar2.TabIndex = 8
        Me.btnEliminar2.Text = "Eliminar"
        Me.btnEliminar2.UseVisualStyleBackColor = true
        Me.btnEliminar2.Visible = false
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(826, 335)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(75, 23)
        Me.btnAgregar.TabIndex = 7
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = true
        Me.btnAgregar.Visible = false
        '
        'Label28
        '
        Me.Label28.AutoSize = true
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label28.Location = New System.Drawing.Point(743, 318)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(49, 16)
        Me.Label28.TabIndex = 163
        Me.Label28.Text = "Abono"
        Me.Label28.Visible = false
        '
        'Label27
        '
        Me.Label27.AutoSize = true
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label27.Location = New System.Drawing.Point(641, 318)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(46, 16)
        Me.Label27.TabIndex = 162
        Me.Label27.Text = "Cargo"
        Me.Label27.Visible = false
        '
        'Label26
        '
        Me.Label26.AutoSize = true
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label26.Location = New System.Drawing.Point(373, 318)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(82, 16)
        Me.Label26.TabIndex = 161
        Me.Label26.Text = "Descripción"
        Me.Label26.Visible = false
        '
        'Label25
        '
        Me.Label25.AutoSize = true
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label25.Location = New System.Drawing.Point(104, 318)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(53, 16)
        Me.Label25.TabIndex = 160
        Me.Label25.Text = "Cuenta"
        Me.Label25.Visible = false
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.White
        Me.txtDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtDesc.Location = New System.Drawing.Point(230, 336)
        Me.txtDesc.MaxLength = 80
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(378, 22)
        Me.txtDesc.TabIndex = 4
        Me.txtDesc.Visible = false
        '
        'txtCuenta
        '
        Me.txtCuenta.BackColor = System.Drawing.Color.White
        Me.txtCuenta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCuenta.Location = New System.Drawing.Point(51, 336)
        Me.txtCuenta.MaxLength = 29
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(174, 22)
        Me.txtCuenta.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCuenta, "Número de cuenta contable")
        Me.txtCuenta.Visible = false
        '
        'txtPoliza
        '
        Me.txtPoliza.BackColor = System.Drawing.Color.White
        Me.txtPoliza.Location = New System.Drawing.Point(90, 579)
        Me.txtPoliza.MaxLength = 5
        Me.txtPoliza.Name = "txtPoliza"
        Me.txtPoliza.Size = New System.Drawing.Size(64, 20)
        Me.txtPoliza.TabIndex = 14
        Me.txtPoliza.Text = "00001"
        Me.txtPoliza.Visible = false
        '
        'btnGuardarPoliza
        '
        Me.btnGuardarPoliza.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnGuardarPoliza.Location = New System.Drawing.Point(22, 250)
        Me.btnGuardarPoliza.Name = "btnGuardarPoliza"
        Me.btnGuardarPoliza.Size = New System.Drawing.Size(82, 26)
        Me.btnGuardarPoliza.TabIndex = 7
        Me.btnGuardarPoliza.Text = "Guardar"
        Me.btnGuardarPoliza.UseVisualStyleBackColor = true
        '
        'lblDiferencia
        '
        Me.lblDiferencia.AutoSize = true
        Me.lblDiferencia.ForeColor = System.Drawing.Color.Black
        Me.lblDiferencia.Location = New System.Drawing.Point(934, 536)
        Me.lblDiferencia.Name = "lblDiferencia"
        Me.lblDiferencia.Size = New System.Drawing.Size(28, 13)
        Me.lblDiferencia.TabIndex = 181
        Me.lblDiferencia.Text = "0.00"
        Me.lblDiferencia.Visible = false
        '
        'Label24
        '
        Me.Label24.AutoSize = true
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label24.Location = New System.Drawing.Point(825, 536)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 16)
        Me.Label24.TabIndex = 180
        Me.Label24.Text = "Diferencia:"
        Me.Label24.Visible = false
        '
        'lblAbonos
        '
        Me.lblAbonos.AutoSize = true
        Me.lblAbonos.Location = New System.Drawing.Point(1000, 516)
        Me.lblAbonos.Name = "lblAbonos"
        Me.lblAbonos.Size = New System.Drawing.Size(28, 13)
        Me.lblAbonos.TabIndex = 179
        Me.lblAbonos.Text = "0.00"
        Me.lblAbonos.Visible = false
        '
        'lblCargos
        '
        Me.lblCargos.AutoSize = true
        Me.lblCargos.Location = New System.Drawing.Point(934, 516)
        Me.lblCargos.Name = "lblCargos"
        Me.lblCargos.Size = New System.Drawing.Size(28, 13)
        Me.lblCargos.TabIndex = 178
        Me.lblCargos.Text = "0.00"
        Me.lblCargos.Visible = false
        '
        'Label23
        '
        Me.Label23.AutoSize = true
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label23.Location = New System.Drawing.Point(795, 516)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(107, 16)
        Me.Label23.TabIndex = 177
        Me.Label23.Text = "Cargos/Abonos:"
        Me.Label23.Visible = false
        '
        'txtRegistra
        '
        Me.txtRegistra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegistra.Location = New System.Drawing.Point(810, 580)
        Me.txtRegistra.MaxLength = 45
        Me.txtRegistra.Name = "txtRegistra"
        Me.txtRegistra.Size = New System.Drawing.Size(197, 20)
        Me.txtRegistra.TabIndex = 17
        Me.txtRegistra.Visible = false
        '
        'txtAutoriza
        '
        Me.txtAutoriza.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAutoriza.Location = New System.Drawing.Point(526, 580)
        Me.txtAutoriza.MaxLength = 45
        Me.txtAutoriza.Name = "txtAutoriza"
        Me.txtAutoriza.Size = New System.Drawing.Size(197, 20)
        Me.txtAutoriza.TabIndex = 16
        Me.txtAutoriza.Visible = false
        '
        'txtElabora
        '
        Me.txtElabora.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtElabora.Location = New System.Drawing.Point(242, 580)
        Me.txtElabora.MaxLength = 45
        Me.txtElabora.Name = "txtElabora"
        Me.txtElabora.Size = New System.Drawing.Size(197, 20)
        Me.txtElabora.TabIndex = 15
        Me.txtElabora.Visible = false
        '
        'Label15
        '
        Me.Label15.AutoSize = true
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label15.Location = New System.Drawing.Point(743, 583)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 16)
        Me.Label15.TabIndex = 176
        Me.Label15.Text = "Registra:"
        Me.Label15.Visible = false
        '
        'Label14
        '
        Me.Label14.AutoSize = true
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label14.Location = New System.Drawing.Point(459, 581)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 16)
        Me.Label14.TabIndex = 175
        Me.Label14.Text = "Autoriza:"
        Me.Label14.Visible = false
        '
        'Label13
        '
        Me.Label13.AutoSize = true
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label13.Location = New System.Drawing.Point(180, 581)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(61, 16)
        Me.Label13.TabIndex = 174
        Me.Label13.Text = "Elabora:"
        Me.Label13.Visible = false
        '
        'Label11
        '
        Me.Label11.AutoSize = true
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label11.Location = New System.Drawing.Point(36, 580)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(52, 16)
        Me.Label11.TabIndex = 172
        Me.Label11.Text = "Poliza:"
        Me.Label11.Visible = false
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = false
        Me.DataGridView2.AllowUserToDeleteRows = false
        Me.DataGridView2.AllowUserToOrderColumns = true
        Me.DataGridView2.AllowUserToResizeColumns = false
        Me.DataGridView2.AllowUserToResizeRows = false
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(20, 369)
        Me.DataGridView2.MultiSelect = false
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = true
        Me.DataGridView2.RowHeadersVisible = false
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(1041, 132)
        Me.DataGridView2.TabIndex = 99
        Me.DataGridView2.TabStop = false
        Me.DataGridView2.Visible = false
        '
        'PrintDocument1
        '
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(324, 250)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(81, 26)
        Me.btnCerrar.TabIndex = 10
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = true
        '
        'btnLimpiarRenglon
        '
        Me.btnLimpiarRenglon.Location = New System.Drawing.Point(988, 340)
        Me.btnLimpiarRenglon.Name = "btnLimpiarRenglon"
        Me.btnLimpiarRenglon.Size = New System.Drawing.Size(75, 23)
        Me.btnLimpiarRenglon.TabIndex = 9
        Me.btnLimpiarRenglon.Text = "Nuevo"
        Me.btnLimpiarRenglon.UseVisualStyleBackColor = true
        Me.btnLimpiarRenglon.Visible = false
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = true
        Me.lblDescripcion.Location = New System.Drawing.Point(20, 509)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(39, 13)
        Me.lblDescripcion.TabIndex = 182
        Me.lblDescripcion.Text = "Label7"
        Me.lblDescripcion.Visible = false
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(426, 266)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(211, 16)
        Me.Label1.TabIndex = 192
        Me.Label1.Text = "Venta contado contra depositos:"
        '
        'frmDeposito
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96!, 96!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(1003, 289)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.btnLimpiarRenglon)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.txtPoliza)
        Me.Controls.Add(Me.btnGuardarPoliza)
        Me.Controls.Add(Me.btnLimpiar)
        Me.Controls.Add(Me.lblDiferencia)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.lblAbonos)
        Me.Controls.Add(Me.lblCargos)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.txtRegistra)
        Me.Controls.Add(Me.txtAutoriza)
        Me.Controls.Add(Me.txtElabora)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.btnBuscarCC)
        Me.Controls.Add(Me.btnEliminar2)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.txtCuenta)
        Me.Controls.Add(Me.grpBusqueda)
        Me.Controls.Add(Me.drpDeposito)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmDeposito"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Depósitos"
        Me.drpDeposito.ResumeLayout(false)
        Me.drpDeposito.PerformLayout
        Me.grpBusqueda.ResumeLayout(false)
        Me.grpBusqueda.PerformLayout
        CType(Me.DataGridView1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DataGridView2,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents chkFecha As System.Windows.Forms.CheckBox
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtReferencia As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnLimpiar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents drpDeposito As System.Windows.Forms.GroupBox
    Friend WithEvents grpBusqueda As System.Windows.Forms.GroupBox
    Friend WithEvents btnLimpiarBusqueda As System.Windows.Forms.Button
    Friend WithEvents ckbProvedor As System.Windows.Forms.CheckBox
    Friend WithEvents ckbFecha As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtBuscRef As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCC As System.Windows.Forms.Button
    Friend WithEvents btnEliminar2 As System.Windows.Forms.Button
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtCuenta As System.Windows.Forms.TextBox
    Friend WithEvents txtPoliza As System.Windows.Forms.TextBox
    Friend WithEvents btnGuardarPoliza As System.Windows.Forms.Button
    Friend WithEvents lblDiferencia As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblAbonos As System.Windows.Forms.Label
    Friend WithEvents lblCargos As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtRegistra As System.Windows.Forms.TextBox
    Friend WithEvents txtAutoriza As System.Windows.Forms.TextBox
    Friend WithEvents txtElabora As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents cmbCuenta As System.Windows.Forms.ComboBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnLimpiarRenglon As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtComentario As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnImprimirDeposito As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
