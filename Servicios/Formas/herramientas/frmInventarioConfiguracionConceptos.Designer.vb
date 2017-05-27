<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventarioConfiguracionConceptos
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
        Me.comboMermas = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboObsequios = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboDevolucion = New System.Windows.Forms.ComboBox()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.comboSucursales = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.comboCarga = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.comboCliente = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.comboPlanta = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.comboBuenas = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.comboObsequios2 = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.comboMermas2 = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'comboMermas
        '
        Me.comboMermas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboMermas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboMermas.FormattingEnabled = True
        Me.comboMermas.Location = New System.Drawing.Point(450, 159)
        Me.comboMermas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboMermas.Name = "comboMermas"
        Me.comboMermas.Size = New System.Drawing.Size(342, 24)
        Me.comboMermas.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(447, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Mermas:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(447, 188)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Obsequios:"
        '
        'ComboObsequios
        '
        Me.ComboObsequios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboObsequios.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboObsequios.FormattingEnabled = True
        Me.ComboObsequios.Location = New System.Drawing.Point(450, 208)
        Me.ComboObsequios.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboObsequios.Name = "ComboObsequios"
        Me.ComboObsequios.Size = New System.Drawing.Size(342, 24)
        Me.ComboObsequios.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(447, 243)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Dev. a planta:"
        '
        'ComboDevolucion
        '
        Me.ComboDevolucion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboDevolucion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboDevolucion.FormattingEnabled = True
        Me.ComboDevolucion.Location = New System.Drawing.Point(450, 263)
        Me.ComboDevolucion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ComboDevolucion.Name = "ComboDevolucion"
        Me.ComboDevolucion.Size = New System.Drawing.Size(342, 24)
        Me.ComboDevolucion.TabIndex = 4
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(303, 379)
        Me.btnGuardar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(90, 28)
        Me.btnGuardar.TabIndex = 6
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Location = New System.Drawing.Point(411, 379)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(90, 28)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(195, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 16)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Sucursal:"
        '
        'comboSucursales
        '
        Me.comboSucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboSucursales.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboSucursales.FormattingEnabled = True
        Me.comboSucursales.Location = New System.Drawing.Point(266, 12)
        Me.comboSucursales.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboSucursales.Name = "comboSucursales"
        Me.comboSucursales.Size = New System.Drawing.Size(344, 24)
        Me.comboSucursales.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 242)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Carga:"
        Me.Label5.Visible = False
        '
        'comboCarga
        '
        Me.comboCarga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboCarga.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboCarga.FormattingEnabled = True
        Me.comboCarga.Location = New System.Drawing.Point(21, 263)
        Me.comboCarga.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboCarga.Name = "comboCarga"
        Me.comboCarga.Size = New System.Drawing.Size(342, 24)
        Me.comboCarga.TabIndex = 10
        Me.comboCarga.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(447, 306)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 16)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Dev. de Cliente:"
        '
        'comboCliente
        '
        Me.comboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboCliente.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboCliente.FormattingEnabled = True
        Me.comboCliente.Location = New System.Drawing.Point(450, 329)
        Me.comboCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboCliente.Name = "comboCliente"
        Me.comboCliente.Size = New System.Drawing.Size(342, 24)
        Me.comboCliente.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(222, 16)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Control de entrega de mercancía:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(491, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(228, 16)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Control de inventario y mercancía:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(448, 87)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(117, 16)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Envíos de planta:"
        '
        'comboPlanta
        '
        Me.comboPlanta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboPlanta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboPlanta.FormattingEnabled = True
        Me.comboPlanta.Location = New System.Drawing.Point(450, 107)
        Me.comboPlanta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboPlanta.Name = "comboPlanta"
        Me.comboPlanta.Size = New System.Drawing.Size(342, 24)
        Me.comboPlanta.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(20, 86)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 16)
        Me.Label10.TabIndex = 19
        Me.Label10.Text = "Buenas:"
        '
        'comboBuenas
        '
        Me.comboBuenas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBuenas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBuenas.FormattingEnabled = True
        Me.comboBuenas.Location = New System.Drawing.Point(23, 107)
        Me.comboBuenas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboBuenas.Name = "comboBuenas"
        Me.comboBuenas.Size = New System.Drawing.Size(342, 24)
        Me.comboBuenas.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(20, 138)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 16)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Obsequios:"
        '
        'comboObsequios2
        '
        Me.comboObsequios2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboObsequios2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboObsequios2.FormattingEnabled = True
        Me.comboObsequios2.Location = New System.Drawing.Point(23, 159)
        Me.comboObsequios2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboObsequios2.Name = "comboObsequios2"
        Me.comboObsequios2.Size = New System.Drawing.Size(342, 24)
        Me.comboObsequios2.TabIndex = 20
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(20, 187)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 16)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Mermas:"
        '
        'comboMermas2
        '
        Me.comboMermas2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboMermas2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboMermas2.FormattingEnabled = True
        Me.comboMermas2.Location = New System.Drawing.Point(23, 208)
        Me.comboMermas2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboMermas2.Name = "comboMermas2"
        Me.comboMermas2.Size = New System.Drawing.Size(342, 24)
        Me.comboMermas2.TabIndex = 22
        '
        'frmInventarioConfiguracionConceptos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(805, 426)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.comboMermas2)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.comboObsequios2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.comboBuenas)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.comboPlanta)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.comboCliente)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.comboCarga)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.comboSucursales)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboDevolucion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboObsequios)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.comboMermas)
        Me.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInventarioConfiguracionConceptos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Relación de conceptos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents comboMermas As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboObsequios As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboDevolucion As System.Windows.Forms.ComboBox
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents comboSucursales As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents comboCarga As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents comboCliente As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents comboPlanta As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents comboBuenas As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents comboObsequios2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents comboMermas2 As System.Windows.Forms.ComboBox
End Class
