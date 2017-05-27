<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNominaDatosN
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
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.btnRfc = New System.Windows.Forms.Button()
        Me.txtPorcentaje = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtRfc = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.dgvRFC = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvRFC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(203, 251)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 28)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Text = "Cerrar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(263, 47)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(13, 16)
        Me.Label46.TabIndex = 13
        Me.Label46.Text = "*"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(263, 14)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(13, 16)
        Me.Label45.TabIndex = 12
        Me.Label45.Text = "*"
        '
        'btnRfc
        '
        Me.btnRfc.Location = New System.Drawing.Point(99, 80)
        Me.btnRfc.Name = "btnRfc"
        Me.btnRfc.Size = New System.Drawing.Size(85, 30)
        Me.btnRfc.TabIndex = 3
        Me.btnRfc.Text = "Agregar"
        Me.btnRfc.UseVisualStyleBackColor = True
        '
        'txtPorcentaje
        '
        Me.txtPorcentaje.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPorcentaje.Location = New System.Drawing.Point(160, 44)
        Me.txtPorcentaje.Name = "txtPorcentaje"
        Me.txtPorcentaje.Size = New System.Drawing.Size(100, 22)
        Me.txtPorcentaje.TabIndex = 2
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(15, 47)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(142, 16)
        Me.Label44.TabIndex = 1026
        Me.Label44.Text = "% Tiempo Trabajado:"
        '
        'txtRfc
        '
        Me.txtRfc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRfc.Location = New System.Drawing.Point(108, 11)
        Me.txtRfc.Name = "txtRfc"
        Me.txtRfc.Size = New System.Drawing.Size(152, 22)
        Me.txtRfc.TabIndex = 1
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(18, 14)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(87, 16)
        Me.Label43.TabIndex = 1025
        Me.Label43.Text = "RFC laboral:"
        '
        'dgvRFC
        '
        Me.dgvRFC.AllowUserToAddRows = False
        Me.dgvRFC.AllowUserToDeleteRows = False
        Me.dgvRFC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRFC.Location = New System.Drawing.Point(14, 116)
        Me.dgvRFC.Name = "dgvRFC"
        Me.dgvRFC.ReadOnly = True
        Me.dgvRFC.RowHeadersVisible = False
        Me.dgvRFC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRFC.Size = New System.Drawing.Size(264, 115)
        Me.dgvRFC.TabIndex = 6
        Me.dgvRFC.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(190, 80)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 30)
        Me.Button1.TabIndex = 1027
        Me.Button1.Text = "Nuevo"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 234)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(205, 15)
        Me.Label1.TabIndex = 1028
        Me.Label1.Text = "Doble click para eliminar un registro"
        '
        'frmNominaDatosN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(287, 287)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label46)
        Me.Controls.Add(Me.Label45)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnRfc)
        Me.Controls.Add(Me.txtPorcentaje)
        Me.Controls.Add(Me.dgvRFC)
        Me.Controls.Add(Me.Label44)
        Me.Controls.Add(Me.Label43)
        Me.Controls.Add(Me.txtRfc)
        Me.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNominaDatosN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Subcontratación"
        CType(Me.dgvRFC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents txtPorcentaje As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtRfc As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents btnRfc As System.Windows.Forms.Button
    Friend WithEvents dgvRFC As System.Windows.Forms.DataGridView
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
