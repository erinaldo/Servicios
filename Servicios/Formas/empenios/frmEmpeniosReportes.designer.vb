<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpeniosReportes
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
        Me.lstTipos = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.txtcliente = New System.Windows.Forms.TextBox()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkFecha = New System.Windows.Forms.CheckBox()
        Me.chkCliente = New System.Windows.Forms.CheckBox()
        Me.txtNombeCliente = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbCaja = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbClasificacion = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmbVendedor = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.comboStatus = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lstTipos
        '
        Me.lstTipos.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTipos.FormattingEnabled = True
        Me.lstTipos.ItemHeight = 19
        Me.lstTipos.Items.AddRange(New Object() {"Empeños Pendientes", "Empeños Pendientes Detalles", "Empeños", "Empeños Detalles", "Pagos", "Adjudicaciones", "Reporte Mensual", "Compras", "Compras Detalles", "Pagos Intereses", "Estados empeños"})
        Me.lstTipos.Location = New System.Drawing.Point(0, 1)
        Me.lstTipos.Name = "lstTipos"
        Me.lstTipos.Size = New System.Drawing.Size(237, 289)
        Me.lstTipos.TabIndex = 266
        Me.ToolTip1.SetToolTip(Me.lstTipos, "Seleccione un reporte")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(472, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 19)
        Me.Label1.TabIndex = 282
        Me.Label1.Text = "Hasta:"
        '
        'dtpFechaHasta
        '
        Me.dtpFechaHasta.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFechaHasta.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFechaHasta.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpFechaHasta.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaHasta.Location = New System.Drawing.Point(537, 36)
        Me.dtpFechaHasta.Name = "dtpFechaHasta"
        Me.dtpFechaHasta.Size = New System.Drawing.Size(99, 25)
        Me.dtpFechaHasta.TabIndex = 280
        Me.ToolTip1.SetToolTip(Me.dtpFechaHasta, "Fecha final")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(298, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 19)
        Me.Label3.TabIndex = 281
        Me.Label3.Text = "Desde:"
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFecha.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFecha.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(367, 36)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(98, 25)
        Me.dtpFecha.TabIndex = 279
        Me.ToolTip1.SetToolTip(Me.dtpFecha, "Fehca inicial")
        '
        'btnImprimir
        '
        Me.btnImprimir.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(471, 237)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(128, 36)
        Me.btnImprimir.TabIndex = 283
        Me.btnImprimir.Text = "Imprimir"
        Me.ToolTip1.SetToolTip(Me.btnImprimir, "Imprimir reporte")
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'txtcliente
        '
        Me.txtcliente.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtcliente.Location = New System.Drawing.Point(367, 65)
        Me.txtcliente.Name = "txtcliente"
        Me.txtcliente.Size = New System.Drawing.Size(98, 25)
        Me.txtcliente.TabIndex = 284
        Me.ToolTip1.SetToolTip(Me.txtcliente, "Clave del cliente")
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.btnBuscarCliente.Location = New System.Drawing.Point(706, 65)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(45, 24)
        Me.btnBuscarCliente.TabIndex = 285
        Me.btnBuscarCliente.Text = "..."
        Me.ToolTip1.SetToolTip(Me.btnBuscarCliente, "Buscar cliente")
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(294, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 19)
        Me.Label2.TabIndex = 286
        Me.Label2.Text = "Cliente:"
        '
        'chkFecha
        '
        Me.chkFecha.AutoSize = True
        Me.chkFecha.Checked = True
        Me.chkFecha.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFecha.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.chkFecha.Location = New System.Drawing.Point(657, 40)
        Me.chkFecha.Name = "chkFecha"
        Me.chkFecha.Size = New System.Drawing.Size(15, 14)
        Me.chkFecha.TabIndex = 287
        Me.ToolTip1.SetToolTip(Me.chkFecha, "Habilitar o deshabilitar filtro por fecha")
        Me.chkFecha.UseVisualStyleBackColor = True
        Me.chkFecha.Visible = False
        '
        'chkCliente
        '
        Me.chkCliente.AutoSize = True
        Me.chkCliente.Location = New System.Drawing.Point(763, 71)
        Me.chkCliente.Name = "chkCliente"
        Me.chkCliente.Size = New System.Drawing.Size(15, 14)
        Me.chkCliente.TabIndex = 288
        Me.ToolTip1.SetToolTip(Me.chkCliente, "Habilitar o deshabilitar busqueda por cliente")
        Me.chkCliente.UseVisualStyleBackColor = True
        Me.chkCliente.Visible = False
        '
        'txtNombeCliente
        '
        Me.txtNombeCliente.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtNombeCliente.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtNombeCliente.Location = New System.Drawing.Point(471, 65)
        Me.txtNombeCliente.Name = "txtNombeCliente"
        Me.txtNombeCliente.ReadOnly = True
        Me.txtNombeCliente.Size = New System.Drawing.Size(232, 25)
        Me.txtNombeCliente.TabIndex = 289
        Me.ToolTip1.SetToolTip(Me.txtNombeCliente, "Datos del cliente")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(313, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 19)
        Me.Label4.TabIndex = 330
        Me.Label4.Text = "Caja:"
        '
        'cmbCaja
        '
        Me.cmbCaja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCaja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCaja.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbCaja.FormattingEnabled = True
        Me.cmbCaja.Location = New System.Drawing.Point(368, 122)
        Me.cmbCaja.Name = "cmbCaja"
        Me.cmbCaja.Size = New System.Drawing.Size(152, 25)
        Me.cmbCaja.TabIndex = 328
        Me.ToolTip1.SetToolTip(Me.cmbCaja, "Caja")
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(279, 94)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 19)
        Me.Label22.TabIndex = 329
        Me.Label22.Text = "Sucursal:"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(367, 92)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(336, 25)
        Me.cmbSucursal.TabIndex = 327
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(249, 184)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 19)
        Me.Label5.TabIndex = 332
        Me.Label5.Text = "Clasificación:"
        '
        'cmbClasificacion
        '
        Me.cmbClasificacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbClasificacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbClasificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClasificacion.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbClasificacion.FormattingEnabled = True
        Me.cmbClasificacion.ItemHeight = 17
        Me.cmbClasificacion.Location = New System.Drawing.Point(368, 182)
        Me.cmbClasificacion.Name = "cmbClasificacion"
        Me.cmbClasificacion.Size = New System.Drawing.Size(152, 25)
        Me.cmbClasificacion.TabIndex = 331
        Me.ToolTip1.SetToolTip(Me.cmbClasificacion, "Clasificación")
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label25.Location = New System.Drawing.Point(273, 154)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 19)
        Me.Label25.TabIndex = 334
        Me.Label25.Text = "Vendedor:"
        '
        'cmbVendedor
        '
        Me.cmbVendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbVendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVendedor.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbVendedor.FormattingEnabled = True
        Me.cmbVendedor.ItemHeight = 17
        Me.cmbVendedor.Location = New System.Drawing.Point(368, 152)
        Me.cmbVendedor.Name = "cmbVendedor"
        Me.cmbVendedor.Size = New System.Drawing.Size(335, 25)
        Me.cmbVendedor.TabIndex = 333
        Me.ToolTip1.SetToolTip(Me.cmbVendedor, "Vendedor")
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(710, 257)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(78, 28)
        Me.Button1.TabIndex = 335
        Me.Button1.Text = "Cerrar"
        Me.ToolTip1.SetToolTip(Me.Button1, "Imprimir reporte")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'comboStatus
        '
        Me.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboStatus.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboStatus.FormattingEnabled = True
        Me.comboStatus.Location = New System.Drawing.Point(640, 122)
        Me.comboStatus.Name = "comboStatus"
        Me.comboStatus.Size = New System.Drawing.Size(121, 24)
        Me.comboStatus.TabIndex = 336
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(580, 125)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 16)
        Me.Label6.TabIndex = 337
        Me.Label6.Text = "Estado:"
        '
        'frmEmpeniosReportes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(795, 290)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.comboStatus)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.cmbVendedor)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbClasificacion)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbCaja)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.cmbSucursal)
        Me.Controls.Add(Me.txtNombeCliente)
        Me.Controls.Add(Me.chkCliente)
        Me.Controls.Add(Me.chkFecha)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtcliente)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpFechaHasta)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpFecha)
        Me.Controls.Add(Me.lstTipos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpeniosReportes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reportes"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstTipos As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents txtcliente As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkFecha As System.Windows.Forms.CheckBox
    Friend WithEvents chkCliente As System.Windows.Forms.CheckBox
    Friend WithEvents txtNombeCliente As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbCaja As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbClasificacion As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbVendedor As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents comboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
