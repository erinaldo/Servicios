<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGastosSalarioEmpleados
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvEmpleados = New System.Windows.Forms.DataGridView()
        Me.x = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.idEmpleado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sueldo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dias = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.nmrDiasAPagar = New System.Windows.Forms.NumericUpDown()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnGaurdar = New System.Windows.Forms.Button()
        Me.Eliminar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.Chck_separar = New System.Windows.Forms.CheckBox()
        CType(Me.dgvEmpleados, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nmrDiasAPagar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvEmpleados
        '
        Me.dgvEmpleados.AllowUserToAddRows = False
        Me.dgvEmpleados.AllowUserToDeleteRows = False
        Me.dgvEmpleados.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvEmpleados.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEmpleados.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.x, Me.idEmpleado, Me.Nombre, Me.Sueldo, Me.Dias, Me.Total})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvEmpleados.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvEmpleados.Location = New System.Drawing.Point(17, 50)
        Me.dgvEmpleados.Name = "dgvEmpleados"
        Me.dgvEmpleados.RowHeadersVisible = False
        Me.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEmpleados.Size = New System.Drawing.Size(673, 217)
        Me.dgvEmpleados.TabIndex = 21
        '
        'x
        '
        Me.x.DataPropertyName = "x"
        Me.x.HeaderText = "x"
        Me.x.Name = "x"
        Me.x.Width = 25
        '
        'idEmpleado
        '
        Me.idEmpleado.DataPropertyName = "idEmpleado"
        Me.idEmpleado.HeaderText = "idEmpleado"
        Me.idEmpleado.Name = "idEmpleado"
        Me.idEmpleado.Visible = False
        '
        'Nombre
        '
        Me.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Nombre.DataPropertyName = "nombre"
        Me.Nombre.HeaderText = "Nombre"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Sueldo
        '
        Me.Sueldo.DataPropertyName = "salario"
        Me.Sueldo.HeaderText = "Sueldo"
        Me.Sueldo.Name = "Sueldo"
        '
        'Dias
        '
        Me.Dias.DataPropertyName = "dias"
        Me.Dias.HeaderText = "Días trabajados"
        Me.Dias.Name = "Dias"
        '
        'Total
        '
        Me.Total.DataPropertyName = "salario2"
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(19, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 236
        Me.Label6.Text = "Días a pagar: "
        '
        'nmrDiasAPagar
        '
        Me.nmrDiasAPagar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nmrDiasAPagar.Location = New System.Drawing.Point(119, 17)
        Me.nmrDiasAPagar.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        Me.nmrDiasAPagar.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nmrDiasAPagar.Name = "nmrDiasAPagar"
        Me.nmrDiasAPagar.Size = New System.Drawing.Size(79, 22)
        Me.nmrDiasAPagar.TabIndex = 237
        Me.nmrDiasAPagar.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(524, 279)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(158, 22)
        Me.lblTotal.TabIndex = 238
        Me.lblTotal.Text = "$0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(454, 279)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 22)
        Me.Label16.TabIndex = 239
        Me.Label16.Text = "Total:"
        '
        'btnGaurdar
        '
        Me.btnGaurdar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGaurdar.Location = New System.Drawing.Point(66, 276)
        Me.btnGaurdar.Name = "btnGaurdar"
        Me.btnGaurdar.Size = New System.Drawing.Size(82, 27)
        Me.btnGaurdar.TabIndex = 240
        Me.btnGaurdar.Text = "Guardar"
        Me.btnGaurdar.UseVisualStyleBackColor = True
        '
        'Eliminar
        '
        Me.Eliminar.Enabled = False
        Me.Eliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Eliminar.Location = New System.Drawing.Point(164, 276)
        Me.Eliminar.Name = "Eliminar"
        Me.Eliminar.Size = New System.Drawing.Size(82, 27)
        Me.Eliminar.TabIndex = 241
        Me.Eliminar.Text = "Eliminar"
        Me.Eliminar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnSalir.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Location = New System.Drawing.Point(262, 276)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(82, 27)
        Me.btnSalir.TabIndex = 242
        Me.btnSalir.Text = "Cerrar"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'Chck_separar
        '
        Me.Chck_separar.AutoSize = True
        Me.Chck_separar.Location = New System.Drawing.Point(205, 20)
        Me.Chck_separar.Name = "Chck_separar"
        Me.Chck_separar.Size = New System.Drawing.Size(130, 17)
        Me.Chck_separar.TabIndex = 243
        Me.Chck_separar.Text = "Separar por empleado"
        Me.Chck_separar.UseVisualStyleBackColor = True
        '
        'frmGastosSalarioEmpleados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnSalir
        Me.ClientSize = New System.Drawing.Size(702, 311)
        Me.Controls.Add(Me.Chck_separar)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.Eliminar)
        Me.Controls.Add(Me.btnGaurdar)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.nmrDiasAPagar)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dgvEmpleados)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGastosSalarioEmpleados"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Salario de empleados"
        CType(Me.dgvEmpleados, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nmrDiasAPagar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents nmrDiasAPagar As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents x As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents idEmpleado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Nombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sueldo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dias As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents dgvEmpleados As System.Windows.Forms.DataGridView
    Friend WithEvents btnGaurdar As System.Windows.Forms.Button
    Friend WithEvents Eliminar As System.Windows.Forms.Button
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents Chck_separar As System.Windows.Forms.CheckBox
End Class
