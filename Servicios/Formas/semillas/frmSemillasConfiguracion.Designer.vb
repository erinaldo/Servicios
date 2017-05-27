<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSemillasConfiguracion
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtHumedad = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtImpurezas = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtGranoDanado = New System.Windows.Forms.TextBox()
        Me.txtGranoQuebrado = New System.Windows.Forms.TextBox()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCastigoH = New System.Windows.Forms.TextBox()
        Me.txtCastigoI = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCastigoQ = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCastigoD = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(52, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(171, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "% de humedad permitido:"
        '
        'txtHumedad
        '
        Me.txtHumedad.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHumedad.Location = New System.Drawing.Point(229, 19)
        Me.txtHumedad.Name = "txtHumedad"
        Me.txtHumedad.Size = New System.Drawing.Size(38, 22)
        Me.txtHumedad.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(46, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(177, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "% de impurezas permitido:"
        '
        'txtImpurezas
        '
        Me.txtImpurezas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpurezas.Location = New System.Drawing.Point(229, 51)
        Me.txtImpurezas.Name = "txtImpurezas"
        Me.txtImpurezas.Size = New System.Drawing.Size(38, 22)
        Me.txtImpurezas.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(23, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(200, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "% de grano dañado permitido:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(213, 16)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "% de grano quebrado permitido:"
        '
        'txtGranoDanado
        '
        Me.txtGranoDanado.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGranoDanado.Location = New System.Drawing.Point(229, 113)
        Me.txtGranoDanado.Name = "txtGranoDanado"
        Me.txtGranoDanado.Size = New System.Drawing.Size(38, 22)
        Me.txtGranoDanado.TabIndex = 3
        '
        'txtGranoQuebrado
        '
        Me.txtGranoQuebrado.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGranoQuebrado.Location = New System.Drawing.Point(229, 82)
        Me.txtGranoQuebrado.Name = "txtGranoQuebrado"
        Me.txtGranoQuebrado.Size = New System.Drawing.Size(38, 22)
        Me.txtGranoQuebrado.TabIndex = 2
        '
        'btnGuardar
        '
        Me.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(66, 151)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 26)
        Me.btnGuardar.TabIndex = 10
        Me.btnGuardar.Text = "Guadar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Location = New System.Drawing.Point(148, 151)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 26)
        Me.btnCancelar.TabIndex = 11
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(286, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 16)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Kilos por punto:"
        '
        'txtCastigoH
        '
        Me.txtCastigoH.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCastigoH.Location = New System.Drawing.Point(410, 19)
        Me.txtCastigoH.Name = "txtCastigoH"
        Me.txtCastigoH.Size = New System.Drawing.Size(70, 22)
        Me.txtCastigoH.TabIndex = 13
        Me.txtCastigoH.Text = "0"
        '
        'txtCastigoI
        '
        Me.txtCastigoI.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCastigoI.Location = New System.Drawing.Point(410, 54)
        Me.txtCastigoI.Name = "txtCastigoI"
        Me.txtCastigoI.Size = New System.Drawing.Size(70, 22)
        Me.txtCastigoI.TabIndex = 15
        Me.txtCastigoI.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(286, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 16)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Kilos por punto:"
        '
        'txtCastigoQ
        '
        Me.txtCastigoQ.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCastigoQ.Location = New System.Drawing.Point(410, 85)
        Me.txtCastigoQ.Name = "txtCastigoQ"
        Me.txtCastigoQ.Size = New System.Drawing.Size(70, 22)
        Me.txtCastigoQ.TabIndex = 17
        Me.txtCastigoQ.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(286, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(108, 16)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Kilos por punto:"
        '
        'txtCastigoD
        '
        Me.txtCastigoD.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCastigoD.Location = New System.Drawing.Point(410, 116)
        Me.txtCastigoD.Name = "txtCastigoD"
        Me.txtCastigoD.Size = New System.Drawing.Size(70, 22)
        Me.txtCastigoD.TabIndex = 19
        Me.txtCastigoD.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(286, 119)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(108, 16)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Kilos por punto:"
        '
        'frmSemillasConfiguracion
        '
        Me.AcceptButton = Me.btnGuardar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(502, 200)
        Me.Controls.Add(Me.txtCastigoD)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtCastigoQ)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtCastigoI)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtCastigoH)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtGranoQuebrado)
        Me.Controls.Add(Me.txtGranoDanado)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtImpurezas)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtHumedad)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSemillasConfiguracion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ventana de configuración de castigos."
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtHumedad As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtImpurezas As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtGranoDanado As System.Windows.Forms.TextBox
    Friend WithEvents txtGranoQuebrado As System.Windows.Forms.TextBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCastigoH As System.Windows.Forms.TextBox
    Friend WithEvents txtCastigoI As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCastigoQ As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCastigoD As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
