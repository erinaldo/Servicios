<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCodigodeBarras
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
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.btnGaurdarImagen = New System.Windows.Forms.Button()
        Me.btnImprimirCodigo = New System.Windows.Forms.Button()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.cmbTipoCodigo = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnGuardarCodigo = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbCodigoPais = New System.Windows.Forms.ComboBox()
        Me.txtCodigoPais = New System.Windows.Forms.TextBox()
        Me.txtManufactura = New System.Windows.Forms.TextBox()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.cmbEscala = New System.Windows.Forms.ComboBox()
        Me.txtCheckSum = New System.Windows.Forms.TextBox()
        Me.txtProducto = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pnlUPCA = New System.Windows.Forms.Panel()
        Me.pnlEAN13 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnEliminar = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUPCA.SuspendLayout()
        Me.pnlEAN13.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGenerar
        '
        Me.btnGenerar.Location = New System.Drawing.Point(250, 169)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerar.TabIndex = 3
        Me.btnGenerar.Text = "Generar"
        Me.btnGenerar.UseVisualStyleBackColor = True
        '
        'btnGaurdarImagen
        '
        Me.btnGaurdarImagen.Enabled = False
        Me.btnGaurdarImagen.Location = New System.Drawing.Point(181, 390)
        Me.btnGaurdarImagen.Name = "btnGaurdarImagen"
        Me.btnGaurdarImagen.Size = New System.Drawing.Size(67, 37)
        Me.btnGaurdarImagen.TabIndex = 6
        Me.btnGaurdarImagen.Text = "Guardar Imagen"
        Me.btnGaurdarImagen.UseVisualStyleBackColor = True
        '
        'btnImprimirCodigo
        '
        Me.btnImprimirCodigo.Enabled = False
        Me.btnImprimirCodigo.Location = New System.Drawing.Point(256, 390)
        Me.btnImprimirCodigo.Name = "btnImprimirCodigo"
        Me.btnImprimirCodigo.Size = New System.Drawing.Size(67, 37)
        Me.btnImprimirCodigo.TabIndex = 7
        Me.btnImprimirCodigo.Text = "Imprimir Código"
        Me.btnImprimirCodigo.UseVisualStyleBackColor = True
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(221, 45)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(199, 20)
        Me.txtCodigo.TabIndex = 2
        '
        'cmbTipoCodigo
        '
        Me.cmbTipoCodigo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoCodigo.FormattingEnabled = True
        Me.cmbTipoCodigo.Items.AddRange(New Object() {"EAN-13", "UPC-A"})
        Me.cmbTipoCodigo.Location = New System.Drawing.Point(221, 18)
        Me.cmbTipoCodigo.Name = "cmbTipoCodigo"
        Me.cmbTipoCodigo.Size = New System.Drawing.Size(121, 21)
        Me.cmbTipoCodigo.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(111, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 16)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Tipo de código:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(161, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 16)
        Me.Label1.TabIndex = 62
        Me.Label1.Text = "Código:"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(144, 196)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(291, 189)
        Me.PictureBox1.TabIndex = 63
        Me.PictureBox1.TabStop = False
        '
        'btnGuardarCodigo
        '
        Me.btnGuardarCodigo.Enabled = False
        Me.btnGuardarCodigo.Location = New System.Drawing.Point(105, 389)
        Me.btnGuardarCodigo.Name = "btnGuardarCodigo"
        Me.btnGuardarCodigo.Size = New System.Drawing.Size(67, 37)
        Me.btnGuardarCodigo.TabIndex = 5
        Me.btnGuardarCodigo.Text = "Guardar Código"
        Me.btnGuardarCodigo.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Location = New System.Drawing.Point(332, 390)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(67, 37)
        Me.btnNuevo.TabIndex = 8
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'cmbCodigoPais
        '
        Me.cmbCodigoPais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCodigoPais.FormattingEnabled = True
        Me.cmbCodigoPais.Items.AddRange(New Object() {"México", "USA", "Otro"})
        Me.cmbCodigoPais.Location = New System.Drawing.Point(253, 1)
        Me.cmbCodigoPais.Name = "cmbCodigoPais"
        Me.cmbCodigoPais.Size = New System.Drawing.Size(118, 21)
        Me.cmbCodigoPais.TabIndex = 253
        Me.ToolTip1.SetToolTip(Me.cmbCodigoPais, "Tamaño de la imagen.")
        '
        'txtCodigoPais
        '
        Me.txtCodigoPais.Location = New System.Drawing.Point(113, 3)
        Me.txtCodigoPais.MaxLength = 5
        Me.txtCodigoPais.Name = "txtCodigoPais"
        Me.txtCodigoPais.Size = New System.Drawing.Size(118, 20)
        Me.txtCodigoPais.TabIndex = 253
        Me.ToolTip1.SetToolTip(Me.txtCodigoPais, "5 dígitos.")
        '
        'txtManufactura
        '
        Me.txtManufactura.Location = New System.Drawing.Point(171, 29)
        Me.txtManufactura.MaxLength = 5
        Me.txtManufactura.Name = "txtManufactura"
        Me.txtManufactura.Size = New System.Drawing.Size(118, 20)
        Me.txtManufactura.TabIndex = 243
        Me.ToolTip1.SetToolTip(Me.txtManufactura, "4 o 5 dígitos.")
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Items.AddRange(New Object() {"0 - Códigos regulares UPC", "2 - Artículos de peso marcados en la tienda.", "3 - Código nacional para la salud y drogas.", "4 - Formato sin restricción, usar en las tiendas de artículos no alimenticios.", "5 - Cupones", "7 - Códigos regulares UPC"})
        Me.cmbTipo.Location = New System.Drawing.Point(171, 4)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(297, 21)
        Me.cmbTipo.TabIndex = 241
        Me.ToolTip1.SetToolTip(Me.cmbTipo, "Tipo de producto.")
        '
        'cmbEscala
        '
        Me.cmbEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEscala.FormattingEnabled = True
        Me.cmbEscala.Items.AddRange(New Object() {"0.8", "0.9", "1.0", "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0"})
        Me.cmbEscala.Location = New System.Drawing.Point(171, 97)
        Me.cmbEscala.Name = "cmbEscala"
        Me.cmbEscala.Size = New System.Drawing.Size(118, 21)
        Me.cmbEscala.TabIndex = 249
        Me.ToolTip1.SetToolTip(Me.cmbEscala, "Tamaño de la imagen.")
        '
        'txtCheckSum
        '
        Me.txtCheckSum.Enabled = False
        Me.txtCheckSum.Location = New System.Drawing.Point(171, 75)
        Me.txtCheckSum.Name = "txtCheckSum"
        Me.txtCheckSum.Size = New System.Drawing.Size(118, 20)
        Me.txtCheckSum.TabIndex = 247
        Me.ToolTip1.SetToolTip(Me.txtCheckSum, "Se generará automaticamente.")
        '
        'txtProducto
        '
        Me.txtProducto.Location = New System.Drawing.Point(171, 52)
        Me.txtProducto.MaxLength = 5
        Me.txtProducto.Name = "txtProducto"
        Me.txtProducto.Size = New System.Drawing.Size(118, 20)
        Me.txtProducto.TabIndex = 245
        Me.ToolTip1.SetToolTip(Me.txtProducto, "5 dígitos.")
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(423, 46)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(13, 16)
        Me.Label14.TabIndex = 253
        Me.Label14.Text = "*"
        '
        'pnlUPCA
        '
        Me.pnlUPCA.Controls.Add(Me.pnlEAN13)
        Me.pnlUPCA.Controls.Add(Me.Label11)
        Me.pnlUPCA.Controls.Add(Me.Label10)
        Me.pnlUPCA.Controls.Add(Me.txtManufactura)
        Me.pnlUPCA.Controls.Add(Me.Label9)
        Me.pnlUPCA.Controls.Add(Me.cmbTipo)
        Me.pnlUPCA.Controls.Add(Me.cmbEscala)
        Me.pnlUPCA.Controls.Add(Me.Label4)
        Me.pnlUPCA.Controls.Add(Me.Label8)
        Me.pnlUPCA.Controls.Add(Me.Label5)
        Me.pnlUPCA.Controls.Add(Me.txtCheckSum)
        Me.pnlUPCA.Controls.Add(Me.txtProducto)
        Me.pnlUPCA.Controls.Add(Me.Label7)
        Me.pnlUPCA.Location = New System.Drawing.Point(50, 42)
        Me.pnlUPCA.Name = "pnlUPCA"
        Me.pnlUPCA.Size = New System.Drawing.Size(470, 120)
        Me.pnlUPCA.TabIndex = 254
        Me.pnlUPCA.Visible = False
        '
        'pnlEAN13
        '
        Me.pnlEAN13.Controls.Add(Me.Label13)
        Me.pnlEAN13.Controls.Add(Me.cmbCodigoPais)
        Me.pnlEAN13.Controls.Add(Me.Label12)
        Me.pnlEAN13.Controls.Add(Me.txtCodigoPais)
        Me.pnlEAN13.Location = New System.Drawing.Point(58, 3)
        Me.pnlEAN13.Name = "pnlEAN13"
        Me.pnlEAN13.Size = New System.Drawing.Size(410, 25)
        Me.pnlEAN13.TabIndex = 252
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(234, 5)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(13, 16)
        Me.Label13.TabIndex = 253
        Me.Label13.Text = "*"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(108, 16)
        Me.Label12.TabIndex = 253
        Me.Label12.Text = "Código de País:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(291, 54)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(13, 16)
        Me.Label11.TabIndex = 252
        Me.Label11.Text = "*"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(291, 30)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(13, 16)
        Me.Label10.TabIndex = 251
        Me.Label10.Text = "*"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(116, 100)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 16)
        Me.Label9.TabIndex = 250
        Me.Label9.Text = "Escala:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(45, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 16)
        Me.Label4.TabIndex = 242
        Me.Label4.Text = "Tipo de producto:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(92, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 16)
        Me.Label8.TabIndex = 248
        Me.Label8.Text = "Ckecksum:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(161, 16)
        Me.Label5.TabIndex = 244
        Me.Label5.Text = "Código de manufactura:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(27, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(141, 16)
        Me.Label7.TabIndex = 246
        Me.Label7.Text = "Código del producto:"
        '
        'btnEliminar
        '
        Me.btnEliminar.Location = New System.Drawing.Point(407, 390)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(67, 37)
        Me.btnEliminar.TabIndex = 11
        Me.btnEliminar.Text = "Eliminar Código"
        Me.btnEliminar.UseVisualStyleBackColor = True
        Me.btnEliminar.Visible = False
        '
        'frmCodigodeBarras
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(578, 453)
        Me.Controls.Add(Me.pnlUPCA)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnGuardarCodigo)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmbTipoCodigo)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.btnImprimirCodigo)
        Me.Controls.Add(Me.btnGaurdarImagen)
        Me.Controls.Add(Me.btnGenerar)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCodigodeBarras"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generador de Códigos de Barra"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUPCA.ResumeLayout(False)
        Me.pnlUPCA.PerformLayout()
        Me.pnlEAN13.ResumeLayout(False)
        Me.pnlEAN13.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents btnGaurdarImagen As System.Windows.Forms.Button
    Friend WithEvents btnImprimirCodigo As System.Windows.Forms.Button
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents cmbTipoCodigo As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnGuardarCodigo As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnlUPCA As System.Windows.Forms.Panel
    Friend WithEvents pnlEAN13 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents cmbCodigoPais As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCodigoPais As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtManufactura As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Private WithEvents cmbEscala As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCheckSum As System.Windows.Forms.TextBox
    Friend WithEvents txtProducto As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
End Class
