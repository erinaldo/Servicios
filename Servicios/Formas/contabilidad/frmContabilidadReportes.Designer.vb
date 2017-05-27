<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContabilidadReportes
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
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.dtpdesde = New System.Windows.Forms.DateTimePicker()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtProveedorNombre = New System.Windows.Forms.TextBox()
        Me.txtProveedorClave = New System.Windows.Forms.TextBox()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.pnlProveedor = New System.Windows.Forms.Panel()
        Me.lblTipoCuenta = New System.Windows.Forms.Label()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.pnlRango = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRangoHasta = New System.Windows.Forms.TextBox()
        Me.txtRangoDesde = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTipoPoliza = New System.Windows.Forms.Label()
        Me.cmbTipoPoliza = New System.Windows.Forms.ComboBox()
        Me.lstTipos = New System.Windows.Forms.ListBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnXML = New System.Windows.Forms.Button()
        Me.cmbClasificacionPoliza = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pnlRangoCuenta = New System.Windows.Forms.Panel()
        Me.SelectorCuentas1 = New Servicios.SelectorCuentas()
        Me.SelectorCuentas2 = New Servicios.SelectorCuentas()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.pnlClasificacion = New System.Windows.Forms.Panel()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.pnlProveedor.SuspendLayout()
        Me.pnlRango.SuspendLayout()
        Me.pnlRangoCuenta.SuspendLayout()
        Me.pnlClasificacion.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(1, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 16)
        Me.Label9.TabIndex = 286
        Me.Label9.Text = "Proveedor:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(441, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 16)
        Me.Label1.TabIndex = 285
        Me.Label1.Text = "Hasta:"
        '
        'dtpHasta
        '
        Me.dtpHasta.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpHasta.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpHasta.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpHasta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(492, 39)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(89, 22)
        Me.dtpHasta.TabIndex = 282
        Me.ToolTip1.SetToolTip(Me.dtpHasta, "Fecha final")
        '
        'dtpdesde
        '
        Me.dtpdesde.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpdesde.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpdesde.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpdesde.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpdesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpdesde.Location = New System.Drawing.Point(346, 39)
        Me.dtpdesde.Name = "dtpdesde"
        Me.dtpdesde.Size = New System.Drawing.Size(88, 22)
        Me.dtpdesde.TabIndex = 281
        Me.ToolTip1.SetToolTip(Me.dtpdesde, "Fecha inicio")
        '
        'btnImprimir
        '
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(406, 408)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(137, 40)
        Me.btnImprimir.TabIndex = 280
        Me.btnImprimir.Text = "Imprimir"
        Me.ToolTip1.SetToolTip(Me.btnImprimir, "Imprimir reporte")
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(293, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 284
        Me.Label3.Text = "Desde:"
        '
        'txtProveedorNombre
        '
        Me.txtProveedorNombre.BackColor = System.Drawing.Color.White
        Me.txtProveedorNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtProveedorNombre.Enabled = False
        Me.txtProveedorNombre.Location = New System.Drawing.Point(173, 3)
        Me.txtProveedorNombre.Name = "txtProveedorNombre"
        Me.txtProveedorNombre.Size = New System.Drawing.Size(256, 20)
        Me.txtProveedorNombre.TabIndex = 288
        '
        'txtProveedorClave
        '
        Me.txtProveedorClave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtProveedorClave.Location = New System.Drawing.Point(81, 3)
        Me.txtProveedorClave.MaxLength = 45
        Me.txtProveedorClave.Name = "txtProveedorClave"
        Me.txtProveedorClave.Size = New System.Drawing.Size(88, 20)
        Me.txtProveedorClave.TabIndex = 287
        Me.ToolTip1.SetToolTip(Me.txtProveedorClave, "Clave de proveedor")
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(432, 1)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(32, 23)
        Me.btnBuscarProveedor.TabIndex = 289
        Me.btnBuscarProveedor.Text = "..."
        Me.ToolTip1.SetToolTip(Me.btnBuscarProveedor, "Buscar proveedor")
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'pnlProveedor
        '
        Me.pnlProveedor.Controls.Add(Me.txtProveedorClave)
        Me.pnlProveedor.Controls.Add(Me.btnBuscarProveedor)
        Me.pnlProveedor.Controls.Add(Me.Label9)
        Me.pnlProveedor.Controls.Add(Me.txtProveedorNombre)
        Me.pnlProveedor.Enabled = False
        Me.pnlProveedor.Location = New System.Drawing.Point(264, 65)
        Me.pnlProveedor.Name = "pnlProveedor"
        Me.pnlProveedor.Size = New System.Drawing.Size(481, 28)
        Me.pnlProveedor.TabIndex = 290
        '
        'lblTipoCuenta
        '
        Me.lblTipoCuenta.AutoSize = True
        Me.lblTipoCuenta.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoCuenta.Enabled = False
        Me.lblTipoCuenta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoCuenta.ForeColor = System.Drawing.Color.Black
        Me.lblTipoCuenta.Location = New System.Drawing.Point(256, 95)
        Me.lblTipoCuenta.Name = "lblTipoCuenta"
        Me.lblTipoCuenta.Size = New System.Drawing.Size(87, 16)
        Me.lblTipoCuenta.TabIndex = 292
        Me.lblTipoCuenta.Text = "Tipo cuenta:"
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.Enabled = False
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Items.AddRange(New Object() {"TODOS", "ACTÍVO", "PASÍVO", "CAPITAL", "INGRESOS", "COSTOS", "EGRESOS", "ORDEN"})
        Me.cmbTipo.Location = New System.Drawing.Point(346, 94)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(88, 21)
        Me.cmbTipo.TabIndex = 291
        Me.ToolTip1.SetToolTip(Me.cmbTipo, "Tipo de cuenta")
        '
        'pnlRango
        '
        Me.pnlRango.Controls.Add(Me.Label4)
        Me.pnlRango.Controls.Add(Me.txtRangoHasta)
        Me.pnlRango.Controls.Add(Me.txtRangoDesde)
        Me.pnlRango.Controls.Add(Me.Label2)
        Me.pnlRango.Enabled = False
        Me.pnlRango.Location = New System.Drawing.Point(259, 147)
        Me.pnlRango.Name = "pnlRango"
        Me.pnlRango.Size = New System.Drawing.Size(322, 27)
        Me.pnlRango.TabIndex = 293
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(191, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 16)
        Me.Label4.TabIndex = 296
        Me.Label4.Text = "a"
        '
        'txtRangoHasta
        '
        Me.txtRangoHasta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRangoHasta.Location = New System.Drawing.Point(209, 4)
        Me.txtRangoHasta.MaxLength = 45
        Me.txtRangoHasta.Name = "txtRangoHasta"
        Me.txtRangoHasta.Size = New System.Drawing.Size(88, 20)
        Me.txtRangoHasta.TabIndex = 295
        Me.ToolTip1.SetToolTip(Me.txtRangoHasta, "Rango de numero de póliza")
        '
        'txtRangoDesde
        '
        Me.txtRangoDesde.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRangoDesde.Location = New System.Drawing.Point(101, 4)
        Me.txtRangoDesde.MaxLength = 45
        Me.txtRangoDesde.Name = "txtRangoDesde"
        Me.txtRangoDesde.Size = New System.Drawing.Size(88, 20)
        Me.txtRangoDesde.TabIndex = 290
        Me.ToolTip1.SetToolTip(Me.txtRangoDesde, "Rango de numero de póliza")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(10, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 16)
        Me.Label2.TabIndex = 294
        Me.Label2.Text = "Rango num.:"
        '
        'lblTipoPoliza
        '
        Me.lblTipoPoliza.AutoSize = True
        Me.lblTipoPoliza.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoPoliza.Enabled = False
        Me.lblTipoPoliza.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoPoliza.ForeColor = System.Drawing.Color.Black
        Me.lblTipoPoliza.Location = New System.Drawing.Point(261, 119)
        Me.lblTipoPoliza.Name = "lblTipoPoliza"
        Me.lblTipoPoliza.Size = New System.Drawing.Size(83, 16)
        Me.lblTipoPoliza.TabIndex = 295
        Me.lblTipoPoliza.Text = "Tipo póliza:"
        '
        'cmbTipoPoliza
        '
        Me.cmbTipoPoliza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoPoliza.Enabled = False
        Me.cmbTipoPoliza.FormattingEnabled = True
        Me.cmbTipoPoliza.Items.AddRange(New Object() {"TODOS", "E  EGRESO", "I   INGRESO", "D DIARIO", "A APERTURA"})
        Me.cmbTipoPoliza.Location = New System.Drawing.Point(346, 118)
        Me.cmbTipoPoliza.Name = "cmbTipoPoliza"
        Me.cmbTipoPoliza.Size = New System.Drawing.Size(87, 21)
        Me.cmbTipoPoliza.TabIndex = 296
        Me.ToolTip1.SetToolTip(Me.cmbTipoPoliza, "Tipo de póliza")
        '
        'lstTipos
        '
        Me.lstTipos.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTipos.FormattingEnabled = True
        Me.lstTipos.ItemHeight = 18
        Me.lstTipos.Items.AddRange(New Object() {"Verificador DIOT", "Balanza", "Relaciones Analíticas", "Relación de pólizas", "Auxiliar de Cuentas", "Estado de posición financiera", "Estado de resultados", "Libro Diario", "Libro Mayor", "Impresión pólizas"})
        Me.lstTipos.Location = New System.Drawing.Point(1, 1)
        Me.lstTipos.Name = "lstTipos"
        Me.lstTipos.Size = New System.Drawing.Size(227, 454)
        Me.lstTipos.TabIndex = 297
        Me.ToolTip1.SetToolTip(Me.lstTipos, "Seleccionar reporte")
        '
        'btnXML
        '
        Me.btnXML.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnXML.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnXML.Location = New System.Drawing.Point(618, 418)
        Me.btnXML.Name = "btnXML"
        Me.btnXML.Size = New System.Drawing.Size(75, 25)
        Me.btnXML.TabIndex = 303
        Me.btnXML.Text = "XML"
        Me.ToolTip1.SetToolTip(Me.btnXML, "Nueva cuenta")
        Me.btnXML.UseVisualStyleBackColor = True
        '
        'cmbClasificacionPoliza
        '
        Me.cmbClasificacionPoliza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClasificacionPoliza.FormattingEnabled = True
        Me.cmbClasificacionPoliza.Items.AddRange(New Object() {"E  Egreso", "I   Ingreso", "D Diario", "A Apertura"})
        Me.cmbClasificacionPoliza.Location = New System.Drawing.Point(117, 7)
        Me.cmbClasificacionPoliza.Name = "cmbClasificacionPoliza"
        Me.cmbClasificacionPoliza.Size = New System.Drawing.Size(234, 21)
        Me.cmbClasificacionPoliza.TabIndex = 304
        Me.ToolTip1.SetToolTip(Me.cmbClasificacionPoliza, "Tipo de póliza.")
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button1.Location = New System.Drawing.Point(785, 423)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(78, 25)
        Me.Button1.TabIndex = 308
        Me.Button1.Text = "Cerrar"
        Me.ToolTip1.SetToolTip(Me.Button1, "Nueva cuenta")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'pnlRangoCuenta
        '
        Me.pnlRangoCuenta.Controls.Add(Me.SelectorCuentas1)
        Me.pnlRangoCuenta.Controls.Add(Me.SelectorCuentas2)
        Me.pnlRangoCuenta.Enabled = False
        Me.pnlRangoCuenta.Location = New System.Drawing.Point(250, 183)
        Me.pnlRangoCuenta.Name = "pnlRangoCuenta"
        Me.pnlRangoCuenta.Size = New System.Drawing.Size(343, 120)
        Me.pnlRangoCuenta.TabIndex = 301
        '
        'SelectorCuentas1
        '
        Me.SelectorCuentas1.BackColor = System.Drawing.Color.Transparent
        Me.SelectorCuentas1.IdCuenta = 0
        Me.SelectorCuentas1.Location = New System.Drawing.Point(6, 3)
        Me.SelectorCuentas1.Name = "SelectorCuentas1"
        Me.SelectorCuentas1.Size = New System.Drawing.Size(323, 46)
        Me.SelectorCuentas1.TabIndex = 312
        '
        'SelectorCuentas2
        '
        Me.SelectorCuentas2.BackColor = System.Drawing.Color.Transparent
        Me.SelectorCuentas2.IdCuenta = 0
        Me.SelectorCuentas2.Location = New System.Drawing.Point(6, 55)
        Me.SelectorCuentas2.Name = "SelectorCuentas2"
        Me.SelectorCuentas2.Size = New System.Drawing.Size(323, 43)
        Me.SelectorCuentas2.TabIndex = 313
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Enabled = False
        Me.CheckBox7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox7.Location = New System.Drawing.Point(608, 248)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(261, 20)
        Me.CheckBox7.TabIndex = 302
        Me.CheckBox7.Text = "No mostrar importes tasa 0 y Exentos"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(2, 9)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(114, 16)
        Me.Label13.TabIndex = 305
        Me.Label13.Text = "Clasif. de póliza:"
        '
        'pnlClasificacion
        '
        Me.pnlClasificacion.Controls.Add(Me.cmbClasificacionPoliza)
        Me.pnlClasificacion.Controls.Add(Me.Label13)
        Me.pnlClasificacion.Enabled = False
        Me.pnlClasificacion.Location = New System.Drawing.Point(230, 1)
        Me.pnlClasificacion.Name = "pnlClasificacion"
        Me.pnlClasificacion.Size = New System.Drawing.Size(506, 35)
        Me.pnlClasificacion.TabIndex = 306
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Enabled = False
        Me.CheckBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(608, 148)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(196, 20)
        Me.CheckBox1.TabIndex = 307
        Me.CheckBox1.Text = "No mostrar saldos en cero."
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Enabled = False
        Me.CheckBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox2.Location = New System.Drawing.Point(608, 171)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(148, 20)
        Me.CheckBox2.TabIndex = 309
        Me.CheckBox2.Text = "Agrupado por mes."
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Enabled = False
        Me.CheckBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox3.Location = New System.Drawing.Point(608, 197)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(252, 20)
        Me.CheckBox3.TabIndex = 310
        Me.CheckBox3.Text = "Solo cuentas de mayor en balanza."
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Enabled = False
        Me.CheckBox4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox4.Location = New System.Drawing.Point(608, 222)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(214, 20)
        Me.CheckBox4.TabIndex = 311
        Me.CheckBox4.Text = "Todos los niveles en balanza."
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'frmContabilidadReportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(875, 456)
        Me.Controls.Add(Me.CheckBox4)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.pnlClasificacion)
        Me.Controls.Add(Me.btnXML)
        Me.Controls.Add(Me.CheckBox7)
        Me.Controls.Add(Me.pnlRangoCuenta)
        Me.Controls.Add(Me.lstTipos)
        Me.Controls.Add(Me.cmbTipoPoliza)
        Me.Controls.Add(Me.lblTipoPoliza)
        Me.Controls.Add(Me.pnlRango)
        Me.Controls.Add(Me.lblTipoCuenta)
        Me.Controls.Add(Me.cmbTipo)
        Me.Controls.Add(Me.pnlProveedor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpHasta)
        Me.Controls.Add(Me.dtpdesde)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.Label3)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmContabilidadReportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reportes"
        Me.pnlProveedor.ResumeLayout(false)
        Me.pnlProveedor.PerformLayout
        Me.pnlRango.ResumeLayout(false)
        Me.pnlRango.PerformLayout
        Me.pnlRangoCuenta.ResumeLayout(false)
        Me.pnlClasificacion.ResumeLayout(false)
        Me.pnlClasificacion.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpdesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtProveedorNombre As System.Windows.Forms.TextBox
    Friend WithEvents txtProveedorClave As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarProveedor As System.Windows.Forms.Button
    Friend WithEvents pnlProveedor As System.Windows.Forms.Panel
    Friend WithEvents lblTipoCuenta As System.Windows.Forms.Label
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents pnlRango As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRangoHasta As System.Windows.Forms.TextBox
    Friend WithEvents txtRangoDesde As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblTipoPoliza As System.Windows.Forms.Label
    Friend WithEvents cmbTipoPoliza As System.Windows.Forms.ComboBox
    Friend WithEvents lstTipos As System.Windows.Forms.ListBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlRangoCuenta As System.Windows.Forms.Panel
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents btnXML As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbClasificacionPoliza As System.Windows.Forms.ComboBox
    Friend WithEvents pnlClasificacion As System.Windows.Forms.Panel
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents SelectorCuentas1 As Servicios.SelectorCuentas
    Friend WithEvents SelectorCuentas2 As Servicios.SelectorCuentas
End Class
