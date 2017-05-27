<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNominaOtrosPagos
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button25 = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtSubsidio = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtRemanente = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtAnhos = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtSaldo = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.dgvPagos = New System.Windows.Forms.DataGridView()
        Me.txtImporte = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtClave = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.comboTipo = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvPagos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Button25)
        Me.GroupBox1.Controls.Add(Me.Label41)
        Me.GroupBox1.Controls.Add(Me.Label40)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.Label39)
        Me.GroupBox1.Controls.Add(Me.txtSubsidio)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtRemanente)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.txtAnhos)
        Me.GroupBox1.Controls.Add(Me.Label37)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.txtSaldo)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.btnAgregar)
        Me.GroupBox1.Controls.Add(Me.dgvPagos)
        Me.GroupBox1.Controls.Add(Me.txtImporte)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtClave)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.comboTipo)
        Me.GroupBox1.Location = New System.Drawing.Point(3, -3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(673, 407)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Otros Pagos"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(361, 225)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(99, 27)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Eliminar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Location = New System.Drawing.Point(573, 376)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 27)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button25
        '
        Me.Button25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button25.Location = New System.Drawing.Point(264, 225)
        Me.Button25.Name = "Button25"
        Me.Button25.Size = New System.Drawing.Size(88, 27)
        Me.Button25.TabIndex = 8
        Me.Button25.Text = "Nuevo"
        Me.Button25.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(282, 195)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(13, 16)
        Me.Label41.TabIndex = 19
        Me.Label41.Text = "*"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(282, 169)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(13, 16)
        Me.Label40.TabIndex = 18
        Me.Label40.Text = "*"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(282, 117)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(13, 16)
        Me.Label38.TabIndex = 16
        Me.Label38.Text = "*"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(282, 145)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(13, 16)
        Me.Label39.TabIndex = 17
        Me.Label39.Text = "*"
        '
        'txtSubsidio
        '
        Me.txtSubsidio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSubsidio.Location = New System.Drawing.Point(183, 114)
        Me.txtSubsidio.MaxLength = 15
        Me.txtSubsidio.Name = "txtSubsidio"
        Me.txtSubsidio.Size = New System.Drawing.Size(96, 22)
        Me.txtSubsidio.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(56, 117)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(124, 16)
        Me.Label20.TabIndex = 1006
        Me.Label20.Text = "Subsidio causado:"
        '
        'txtRemanente
        '
        Me.txtRemanente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemanente.Location = New System.Drawing.Point(183, 192)
        Me.txtRemanente.MaxLength = 15
        Me.txtRemanente.Name = "txtRemanente"
        Me.txtRemanente.Size = New System.Drawing.Size(96, 22)
        Me.txtRemanente.TabIndex = 6
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(9, 195)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(171, 16)
        Me.Label23.TabIndex = 1009
        Me.Label23.Text = "Remanente saldo a favor:"
        '
        'txtAnhos
        '
        Me.txtAnhos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAnhos.Location = New System.Drawing.Point(183, 166)
        Me.txtAnhos.MaxLength = 15
        Me.txtAnhos.Name = "txtAnhos"
        Me.txtAnhos.Size = New System.Drawing.Size(96, 22)
        Me.txtAnhos.TabIndex = 5
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(282, 89)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(13, 16)
        Me.Label37.TabIndex = 15
        Me.Label37.Text = "*"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(137, 170)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(43, 16)
        Me.Label22.TabIndex = 1008
        Me.Label22.Text = "Años:"
        '
        'txtSaldo
        '
        Me.txtSaldo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSaldo.Location = New System.Drawing.Point(183, 141)
        Me.txtSaldo.MaxLength = 15
        Me.txtSaldo.Name = "txtSaldo"
        Me.txtSaldo.Size = New System.Drawing.Size(96, 22)
        Me.txtSaldo.TabIndex = 4
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(336, 62)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(13, 16)
        Me.Label35.TabIndex = 13
        Me.Label35.Text = "*"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(83, 145)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(97, 16)
        Me.Label21.TabIndex = 1007
        Me.Label21.Text = "Saldo a favor:"
        '
        'btnAgregar
        '
        Me.btnAgregar.Location = New System.Drawing.Point(162, 225)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(93, 28)
        Me.btnAgregar.TabIndex = 7
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'dgvPagos
        '
        Me.dgvPagos.AllowUserToAddRows = False
        Me.dgvPagos.AllowUserToDeleteRows = False
        Me.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPagos.Location = New System.Drawing.Point(9, 265)
        Me.dgvPagos.Name = "dgvPagos"
        Me.dgvPagos.ReadOnly = True
        Me.dgvPagos.RowHeadersVisible = False
        Me.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPagos.Size = New System.Drawing.Size(658, 105)
        Me.dgvPagos.TabIndex = 10
        '
        'txtImporte
        '
        Me.txtImporte.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtImporte.Location = New System.Drawing.Point(183, 86)
        Me.txtImporte.MaxLength = 15
        Me.txtImporte.Name = "txtImporte"
        Me.txtImporte.Size = New System.Drawing.Size(96, 22)
        Me.txtImporte.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(119, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 16)
        Me.Label5.TabIndex = 1005
        Me.Label5.Text = "Importe:"
        '
        'txtClave
        '
        Me.txtClave.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtClave.Location = New System.Drawing.Point(183, 58)
        Me.txtClave.MaxLength = 15
        Me.txtClave.Name = "txtClave"
        Me.txtClave.Size = New System.Drawing.Size(150, 22)
        Me.txtClave.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(132, 61)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 16)
        Me.Label3.TabIndex = 1003
        Me.Label3.Text = "Clave:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 1002
        Me.Label2.Text = "Tipo:"
        '
        'comboTipo
        '
        Me.comboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboTipo.FormattingEnabled = True
        Me.comboTipo.Location = New System.Drawing.Point(63, 21)
        Me.comboTipo.Name = "comboTipo"
        Me.comboTipo.Size = New System.Drawing.Size(581, 24)
        Me.comboTipo.TabIndex = 0
        '
        'frmNominaOtrosPagos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(682, 407)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNominaOtrosPagos"
        Me.Text = "Nomina - Otros pagos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvPagos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents dgvPagos As System.Windows.Forms.DataGridView
    Friend WithEvents txtImporte As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents comboTipo As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtRemanente As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtAnhos As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtSaldo As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents txtSubsidio As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button25 As System.Windows.Forms.Button
End Class
