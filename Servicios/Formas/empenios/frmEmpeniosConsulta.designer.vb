<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpeniosConsulta
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbEstado = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbCaja = New System.Windows.Forms.ComboBox
        Me.Label61 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.cmbSucursal = New System.Windows.Forms.ComboBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.chkTiempoReal = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtBusqueda = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnSeleccionar = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtpFecha2 = New System.Windows.Forms.DateTimePicker
        Me.dtpFecha1 = New System.Windows.Forms.DateTimePicker
        Me.DGServicios = New System.Windows.Forms.DataGridView
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        CType(Me.DGServicios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(355, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.TabIndex = 339
        Me.Label4.Text = "Folio:"
        '
        'cmbEstado
        '
        Me.cmbEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbEstado.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEstado.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbEstado.FormattingEnabled = True
        Me.cmbEstado.Location = New System.Drawing.Point(614, 84)
        Me.cmbEstado.Name = "cmbEstado"
        Me.cmbEstado.Size = New System.Drawing.Size(159, 25)
        Me.cmbEstado.TabIndex = 337
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(542, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 19)
        Me.Label1.TabIndex = 338
        Me.Label1.Text = "Estado:"
        '
        'cmbCaja
        '
        Me.cmbCaja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCaja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCaja.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbCaja.FormattingEnabled = True
        Me.cmbCaja.Location = New System.Drawing.Point(614, 46)
        Me.cmbCaja.Name = "cmbCaja"
        Me.cmbCaja.Size = New System.Drawing.Size(159, 25)
        Me.cmbCaja.TabIndex = 326
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.BackColor = System.Drawing.Color.Transparent
        Me.Label61.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label61.Location = New System.Drawing.Point(560, 48)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(49, 19)
        Me.Label61.TabIndex = 336
        Me.Label61.Text = "Caja:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(23, 48)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 19)
        Me.Label22.TabIndex = 335
        Me.Label22.Text = "Sucursal:"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(109, 46)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(424, 25)
        Me.cmbSucursal.TabIndex = 325
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Button3.Location = New System.Drawing.Point(784, 81)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 28)
        Me.Button3.TabIndex = 328
        Me.Button3.Text = "Buscar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'chkTiempoReal
        '
        Me.chkTiempoReal.AutoSize = True
        Me.chkTiempoReal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.chkTiempoReal.Location = New System.Drawing.Point(784, 54)
        Me.chkTiempoReal.Name = "chkTiempoReal"
        Me.chkTiempoReal.Size = New System.Drawing.Size(103, 21)
        Me.chkTiempoReal.TabIndex = 334
        Me.chkTiempoReal.Text = "Tiempo real"
        Me.chkTiempoReal.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(27, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(180, 17)
        Me.Label2.TabIndex = 333
        Me.Label2.Text = "Resultados de la consulta:"
        '
        'txtBusqueda
        '
        Me.txtBusqueda.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtBusqueda.Location = New System.Drawing.Point(410, 84)
        Me.txtBusqueda.Name = "txtBusqueda"
        Me.txtBusqueda.Size = New System.Drawing.Size(123, 25)
        Me.txtBusqueda.TabIndex = 327
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(821, 355)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 25)
        Me.Button2.TabIndex = 330
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.btnSeleccionar.Location = New System.Drawing.Point(401, 355)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(107, 34)
        Me.btnSeleccionar.TabIndex = 329
        Me.btnSeleccionar.Text = "Seleccionar"
        Me.btnSeleccionar.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(39, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 19)
        Me.Label3.TabIndex = 332
        Me.Label3.Text = "Fechas:"
        '
        'dtpFecha2
        '
        Me.dtpFecha2.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha2.Location = New System.Drawing.Point(226, 14)
        Me.dtpFecha2.Name = "dtpFecha2"
        Me.dtpFecha2.Size = New System.Drawing.Size(109, 25)
        Me.dtpFecha2.TabIndex = 324
        '
        'dtpFecha1
        '
        Me.dtpFecha1.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha1.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha1.Location = New System.Drawing.Point(113, 14)
        Me.dtpFecha1.Name = "dtpFecha1"
        Me.dtpFecha1.Size = New System.Drawing.Size(94, 25)
        Me.dtpFecha1.TabIndex = 323
        '
        'DGServicios
        '
        Me.DGServicios.AllowUserToAddRows = False
        Me.DGServicios.AllowUserToDeleteRows = False
        Me.DGServicios.AllowUserToResizeColumns = False
        Me.DGServicios.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DGServicios.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGServicios.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGServicios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGServicios.DefaultCellStyle = DataGridViewCellStyle3
        Me.DGServicios.Location = New System.Drawing.Point(28, 132)
        Me.DGServicios.Name = "DGServicios"
        Me.DGServicios.ReadOnly = True
        Me.DGServicios.RowHeadersVisible = False
        Me.DGServicios.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGServicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGServicios.Size = New System.Drawing.Size(868, 217)
        Me.DGServicios.TabIndex = 331
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(38, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 19)
        Me.Label5.TabIndex = 341
        Me.Label5.Text = "Cliente:"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TextBox1.Location = New System.Drawing.Point(109, 84)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(242, 25)
        Me.TextBox1.TabIndex = 340
        '
        'frmEmpeniosConsulta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(922, 403)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbEstado)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbCaja)
        Me.Controls.Add(Me.Label61)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.cmbSucursal)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.chkTiempoReal)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBusqueda)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpFecha2)
        Me.Controls.Add(Me.dtpFecha1)
        Me.Controls.Add(Me.DGServicios)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpeniosConsulta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Búsqueda de Empeños"
        CType(Me.DGServicios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbEstado As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCaja As System.Windows.Forms.ComboBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents chkTiempoReal As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBusqueda As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFecha1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DGServicios As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
