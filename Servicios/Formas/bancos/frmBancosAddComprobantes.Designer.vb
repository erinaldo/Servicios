<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBancosAddComprobantes
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtCapturador = New System.Windows.Forms.TextBox()
        Me.txtTipoCambioCompNac = New System.Windows.Forms.TextBox()
        Me.cmbMonedaCompNac = New System.Windows.Forms.ComboBox()
        Me.lblTipoCambioCompNac = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnNuevoCompro = New System.Windows.Forms.Button()
        Me.btnEliminarCompro = New System.Windows.Forms.Button()
        Me.btnAgregarCompro = New System.Windows.Forms.Button()
        Me.txtMontoCompro = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.txtUUID = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.BackColor = System.Drawing.Color.Transparent
        Me.Label73.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.Color.Black
        Me.Label73.Location = New System.Drawing.Point(317, 37)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(17, 22)
        Me.Label73.TabIndex = 318
        Me.Label73.Text = "*"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.BackColor = System.Drawing.Color.Transparent
        Me.Label72.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.Color.Black
        Me.Label72.Location = New System.Drawing.Point(459, 9)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(17, 22)
        Me.Label72.TabIndex = 317
        Me.Label72.Text = "*"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.Black
        Me.Label44.Location = New System.Drawing.Point(134, 343)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(83, 16)
        Me.Label44.TabIndex = 316
        Me.Label44.Text = "Capturador:"
        '
        'txtCapturador
        '
        Me.txtCapturador.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapturador.Location = New System.Drawing.Point(219, 342)
        Me.txtCapturador.MaxLength = 200
        Me.txtCapturador.Name = "txtCapturador"
        Me.txtCapturador.Size = New System.Drawing.Size(301, 22)
        Me.txtCapturador.TabIndex = 9
        '
        'txtTipoCambioCompNac
        '
        Me.txtTipoCambioCompNac.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTipoCambioCompNac.Location = New System.Drawing.Point(156, 92)
        Me.txtTipoCambioCompNac.MaxLength = 19
        Me.txtTipoCambioCompNac.Name = "txtTipoCambioCompNac"
        Me.txtTipoCambioCompNac.Size = New System.Drawing.Size(158, 22)
        Me.txtTipoCambioCompNac.TabIndex = 3
        Me.txtTipoCambioCompNac.Text = "0.00"
        '
        'cmbMonedaCompNac
        '
        Me.cmbMonedaCompNac.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbMonedaCompNac.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMonedaCompNac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonedaCompNac.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMonedaCompNac.FormattingEnabled = True
        Me.cmbMonedaCompNac.Location = New System.Drawing.Point(156, 62)
        Me.cmbMonedaCompNac.Name = "cmbMonedaCompNac"
        Me.cmbMonedaCompNac.Size = New System.Drawing.Size(301, 24)
        Me.cmbMonedaCompNac.TabIndex = 2
        '
        'lblTipoCambioCompNac
        '
        Me.lblTipoCambioCompNac.AutoSize = True
        Me.lblTipoCambioCompNac.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoCambioCompNac.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoCambioCompNac.ForeColor = System.Drawing.Color.Black
        Me.lblTipoCambioCompNac.Location = New System.Drawing.Point(42, 95)
        Me.lblTipoCambioCompNac.Name = "lblTipoCambioCompNac"
        Me.lblTipoCambioCompNac.Size = New System.Drawing.Size(111, 16)
        Me.lblTipoCambioCompNac.TabIndex = 315
        Me.lblTipoCambioCompNac.Text = "Tipo de cambio:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(90, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 16)
        Me.Label11.TabIndex = 314
        Me.Label11.Text = "Moneda:"
        '
        'btnNuevoCompro
        '
        Me.btnNuevoCompro.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoCompro.Location = New System.Drawing.Point(335, 157)
        Me.btnNuevoCompro.Name = "btnNuevoCompro"
        Me.btnNuevoCompro.Size = New System.Drawing.Size(75, 26)
        Me.btnNuevoCompro.TabIndex = 7
        Me.btnNuevoCompro.Text = "Nuevo"
        Me.btnNuevoCompro.UseVisualStyleBackColor = True
        '
        'btnEliminarCompro
        '
        Me.btnEliminarCompro.Enabled = False
        Me.btnEliminarCompro.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarCompro.Location = New System.Drawing.Point(251, 157)
        Me.btnEliminarCompro.Name = "btnEliminarCompro"
        Me.btnEliminarCompro.Size = New System.Drawing.Size(75, 26)
        Me.btnEliminarCompro.TabIndex = 6
        Me.btnEliminarCompro.Text = "Eliminar"
        Me.btnEliminarCompro.UseVisualStyleBackColor = True
        '
        'btnAgregarCompro
        '
        Me.btnAgregarCompro.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarCompro.Location = New System.Drawing.Point(170, 157)
        Me.btnAgregarCompro.Name = "btnAgregarCompro"
        Me.btnAgregarCompro.Size = New System.Drawing.Size(75, 26)
        Me.btnAgregarCompro.TabIndex = 5
        Me.btnAgregarCompro.Text = "Agregar"
        Me.btnAgregarCompro.UseVisualStyleBackColor = True
        '
        'txtMontoCompro
        '
        Me.txtMontoCompro.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMontoCompro.Location = New System.Drawing.Point(156, 34)
        Me.txtMontoCompro.MaxLength = 20
        Me.txtMontoCompro.Name = "txtMontoCompro"
        Me.txtMontoCompro.Size = New System.Drawing.Size(158, 22)
        Me.txtMontoCompro.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(102, 37)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(51, 16)
        Me.Label21.TabIndex = 310
        Me.Label21.Text = "Monto:"
        '
        'btnBuscar
        '
        Me.btnBuscar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.Location = New System.Drawing.Point(483, 5)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(75, 26)
        Me.btnBuscar.TabIndex = 4
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'txtUUID
        '
        Me.txtUUID.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUUID.Location = New System.Drawing.Point(156, 8)
        Me.txtUUID.MaxLength = 36
        Me.txtUUID.Name = "txtUUID"
        Me.txtUUID.Size = New System.Drawing.Size(301, 22)
        Me.txtUUID.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(110, 11)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 16)
        Me.Label18.TabIndex = 309
        Me.Label18.Text = "UUID:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(12, 206)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(648, 125)
        Me.DataGridView1.TabIndex = 322
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(416, 157)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 26)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Archivos XML |*.xml"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(103, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 16)
        Me.Label1.TabIndex = 323
        Me.Label1.Text = "Fecha:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "yyyy/MM/dd"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(157, 122)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(88, 22)
        Me.DateTimePicker1.TabIndex = 324
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 187)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(175, 16)
        Me.Label2.TabIndex = 325
        Me.Label2.Text = "Comprobantes agregados:"
        '
        'frmBancosAddComprobantes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(670, 369)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label73)
        Me.Controls.Add(Me.Label72)
        Me.Controls.Add(Me.Label44)
        Me.Controls.Add(Me.txtCapturador)
        Me.Controls.Add(Me.txtTipoCambioCompNac)
        Me.Controls.Add(Me.cmbMonedaCompNac)
        Me.Controls.Add(Me.lblTipoCambioCompNac)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnNuevoCompro)
        Me.Controls.Add(Me.btnEliminarCompro)
        Me.Controls.Add(Me.btnAgregarCompro)
        Me.Controls.Add(Me.txtMontoCompro)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.txtUUID)
        Me.Controls.Add(Me.Label18)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBancosAddComprobantes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agregar Comprobantes"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtCapturador As System.Windows.Forms.TextBox
    Friend WithEvents txtTipoCambioCompNac As System.Windows.Forms.TextBox
    Friend WithEvents cmbMonedaCompNac As System.Windows.Forms.ComboBox
    Friend WithEvents lblTipoCambioCompNac As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnNuevoCompro As System.Windows.Forms.Button
    Friend WithEvents btnEliminarCompro As System.Windows.Forms.Button
    Friend WithEvents btnAgregarCompro As System.Windows.Forms.Button
    Friend WithEvents txtMontoCompro As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents txtUUID As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
