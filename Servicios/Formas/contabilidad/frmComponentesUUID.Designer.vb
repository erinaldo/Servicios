<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComponentesUUID
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
        Me.dgvCompro = New System.Windows.Forms.DataGridView()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.txtFolioCliente = New System.Windows.Forms.TextBox()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.Selec = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MontoComp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RFCcomp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.usado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvCompro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvCompro
        '
        Me.dgvCompro.AllowUserToAddRows = False
        Me.dgvCompro.AllowUserToDeleteRows = False
        Me.dgvCompro.AllowUserToResizeColumns = False
        Me.dgvCompro.AllowUserToResizeRows = False
        Me.dgvCompro.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCompro.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCompro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCompro.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Selec, Me.UUID, Me.MontoComp, Me.RFCcomp, Me.Fecha, Me.Fecha2, Me.usado})
        Me.dgvCompro.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvCompro.Location = New System.Drawing.Point(12, 58)
        Me.dgvCompro.MultiSelect = False
        Me.dgvCompro.Name = "dgvCompro"
        Me.dgvCompro.ReadOnly = True
        Me.dgvCompro.RowHeadersVisible = False
        Me.dgvCompro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCompro.Size = New System.Drawing.Size(698, 295)
        Me.dgvCompro.TabIndex = 6
        Me.dgvCompro.TabStop = False
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(311, 359)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(106, 34)
        Me.btnAceptar.TabIndex = 7
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Button5.Location = New System.Drawing.Point(115, 20)
        Me.Button5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(31, 23)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "..."
        Me.Button5.UseVisualStyleBackColor = True
        '
        'txtFolioCliente
        '
        Me.txtFolioCliente.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtFolioCliente.Location = New System.Drawing.Point(16, 20)
        Me.txtFolioCliente.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtFolioCliente.Name = "txtFolioCliente"
        Me.txtFolioCliente.Size = New System.Drawing.Size(93, 25)
        Me.txtFolioCliente.TabIndex = 1
        '
        'txtNombre
        '
        Me.txtNombre.Enabled = False
        Me.txtNombre.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtNombre.Location = New System.Drawing.Point(152, 18)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(406, 25)
        Me.txtNombre.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(564, 20)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Buscar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Location = New System.Drawing.Point(640, 20)
        Me.btnLimpiar.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(70, 23)
        Me.btnLimpiar.TabIndex = 5
        Me.btnLimpiar.Text = "Limpiar"
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'Selec
        '
        Me.Selec.DataPropertyName = "Selec"
        Me.Selec.FalseValue = "0"
        Me.Selec.HeaderText = "Selec."
        Me.Selec.Name = "Selec"
        Me.Selec.ReadOnly = True
        Me.Selec.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Selec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Selec.TrueValue = "1"
        Me.Selec.Width = 40
        '
        'UUID
        '
        Me.UUID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.UUID.DataPropertyName = "UUID"
        Me.UUID.HeaderText = "UUID"
        Me.UUID.Name = "UUID"
        Me.UUID.ReadOnly = True
        '
        'MontoComp
        '
        Me.MontoComp.DataPropertyName = "Monto"
        Me.MontoComp.HeaderText = "Monto"
        Me.MontoComp.Name = "MontoComp"
        Me.MontoComp.ReadOnly = True
        '
        'RFCcomp
        '
        Me.RFCcomp.DataPropertyName = "RFC"
        Me.RFCcomp.HeaderText = "RFC"
        Me.RFCcomp.Name = "RFCcomp"
        Me.RFCcomp.ReadOnly = True
        '
        'Fecha
        '
        Me.Fecha.DataPropertyName = "Fecha"
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        '
        'Fecha2
        '
        Me.Fecha2.DataPropertyName = "Fecha2"
        Me.Fecha2.HeaderText = "Fecha2"
        Me.Fecha2.Name = "Fecha2"
        Me.Fecha2.ReadOnly = True
        Me.Fecha2.Visible = False
        '
        'usado
        '
        Me.usado.DataPropertyName = "usado"
        Me.usado.HeaderText = "Usado"
        Me.usado.Name = "usado"
        Me.usado.ReadOnly = True
        Me.usado.Visible = False
        '
        'frmComponentesUUID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(725, 400)
        Me.Controls.Add(Me.btnLimpiar)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.txtFolioCliente)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.dgvCompro)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmComponentesUUID"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscar UUID"
        CType(Me.dgvCompro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvCompro As System.Windows.Forms.DataGridView
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents txtFolioCliente As System.Windows.Forms.TextBox
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnLimpiar As System.Windows.Forms.Button
    Friend WithEvents Selec As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents UUID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MontoComp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RFCcomp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents usado As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
