<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestauranteOrden
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRestauranteOrden))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnPagar = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnDirecto1 = New System.Windows.Forms.Button()
        Me.btnDirecto2 = New System.Windows.Forms.Button()
        Me.btnDirecto3 = New System.Windows.Forms.Button()
        Me.btnDirecto4 = New System.Windows.Forms.Button()
        Me.btnDirecto5 = New System.Windows.Forms.Button()
        Me.lblMesa = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.panelObjetos = New System.Windows.Forms.Panel()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.dgvComensales = New System.Windows.Forms.DataGridView()
        Me.Comensal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvPlatillos = New System.Windows.Forms.DataGridView()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelObjetos.SuspendLayout()
        CType(Me.dgvComensales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPlatillos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(8, 481)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 69)
        Me.Button1.TabIndex = 2
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.Button1, "Nuevo Comensal")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(688, 485)
        Me.Button2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(102, 69)
        Me.Button2.TabIndex = 3
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.Button2, "Cancelar Venta")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'btnPagar
        '
        Me.btnPagar.BackColor = System.Drawing.Color.White
        Me.btnPagar.BackgroundImage = CType(resources.GetObject("btnPagar.BackgroundImage"), System.Drawing.Image)
        Me.btnPagar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnPagar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPagar.Location = New System.Drawing.Point(327, 485)
        Me.btnPagar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnPagar.Name = "btnPagar"
        Me.btnPagar.Size = New System.Drawing.Size(102, 69)
        Me.btnPagar.TabIndex = 4
        Me.btnPagar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnPagar, "Comanda")
        Me.btnPagar.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.Font = New System.Drawing.Font("Arial", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(201, 485)
        Me.Button4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(102, 69)
        Me.Button4.TabIndex = 5
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.Button4, "Menú")
        Me.Button4.UseVisualStyleBackColor = False
        '
        'btnDirecto1
        '
        Me.btnDirecto1.BackColor = System.Drawing.Color.White
        Me.btnDirecto1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto1.Location = New System.Drawing.Point(245, 5)
        Me.btnDirecto1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto1.Name = "btnDirecto1"
        Me.btnDirecto1.Size = New System.Drawing.Size(120, 59)
        Me.btnDirecto1.TabIndex = 6
        Me.btnDirecto1.Text = "producto 1"
        Me.btnDirecto1.UseVisualStyleBackColor = False
        '
        'btnDirecto2
        '
        Me.btnDirecto2.BackColor = System.Drawing.Color.White
        Me.btnDirecto2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto2.Location = New System.Drawing.Point(371, 5)
        Me.btnDirecto2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto2.Name = "btnDirecto2"
        Me.btnDirecto2.Size = New System.Drawing.Size(120, 59)
        Me.btnDirecto2.TabIndex = 7
        Me.btnDirecto2.Text = "producto 2"
        Me.btnDirecto2.UseVisualStyleBackColor = False
        '
        'btnDirecto3
        '
        Me.btnDirecto3.BackColor = System.Drawing.Color.White
        Me.btnDirecto3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto3.Location = New System.Drawing.Point(497, 5)
        Me.btnDirecto3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto3.Name = "btnDirecto3"
        Me.btnDirecto3.Size = New System.Drawing.Size(120, 59)
        Me.btnDirecto3.TabIndex = 8
        Me.btnDirecto3.Text = "producto 3"
        Me.btnDirecto3.UseVisualStyleBackColor = False
        '
        'btnDirecto4
        '
        Me.btnDirecto4.BackColor = System.Drawing.Color.White
        Me.btnDirecto4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto4.Location = New System.Drawing.Point(623, 4)
        Me.btnDirecto4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto4.Name = "btnDirecto4"
        Me.btnDirecto4.Size = New System.Drawing.Size(120, 59)
        Me.btnDirecto4.TabIndex = 9
        Me.btnDirecto4.Text = "producto 4"
        Me.btnDirecto4.UseVisualStyleBackColor = False
        '
        'btnDirecto5
        '
        Me.btnDirecto5.BackColor = System.Drawing.Color.White
        Me.btnDirecto5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto5.Location = New System.Drawing.Point(749, 4)
        Me.btnDirecto5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto5.Name = "btnDirecto5"
        Me.btnDirecto5.Size = New System.Drawing.Size(121, 59)
        Me.btnDirecto5.TabIndex = 10
        Me.btnDirecto5.Text = "producto 5"
        Me.btnDirecto5.UseVisualStyleBackColor = False
        '
        'lblMesa
        '
        Me.lblMesa.AutoSize = True
        Me.lblMesa.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMesa.Location = New System.Drawing.Point(5, 4)
        Me.lblMesa.Name = "lblMesa"
        Me.lblMesa.Size = New System.Drawing.Size(91, 32)
        Me.lblMesa.TabIndex = 11
        Me.lblMesa.Text = "Mesa:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 19)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Mesero:"
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.White
        Me.Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), System.Drawing.Image)
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(573, 485)
        Me.Button6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(102, 69)
        Me.Button6.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.Button6, "Cambiar de Mesa")
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.White
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"), System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(447, 485)
        Me.Button7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(102, 69)
        Me.Button7.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.Button7, "Imprimir")
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 16)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Folio:"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'panelObjetos
        '
        Me.panelObjetos.Controls.Add(Me.Button8)
        Me.panelObjetos.Controls.Add(Me.dgvComensales)
        Me.panelObjetos.Controls.Add(Me.dgvPlatillos)
        Me.panelObjetos.Controls.Add(Me.Button5)
        Me.panelObjetos.Controls.Add(Me.Button3)
        Me.panelObjetos.Controls.Add(Me.Button2)
        Me.panelObjetos.Controls.Add(Me.Button4)
        Me.panelObjetos.Controls.Add(Me.Button7)
        Me.panelObjetos.Controls.Add(Me.btnPagar)
        Me.panelObjetos.Controls.Add(Me.Button6)
        Me.panelObjetos.Controls.Add(Me.lblMesa)
        Me.panelObjetos.Controls.Add(Me.btnDirecto5)
        Me.panelObjetos.Controls.Add(Me.Label4)
        Me.panelObjetos.Controls.Add(Me.btnDirecto4)
        Me.panelObjetos.Controls.Add(Me.Button1)
        Me.panelObjetos.Controls.Add(Me.Label1)
        Me.panelObjetos.Controls.Add(Me.btnDirecto3)
        Me.panelObjetos.Controls.Add(Me.btnDirecto2)
        Me.panelObjetos.Controls.Add(Me.btnDirecto1)
        Me.panelObjetos.Location = New System.Drawing.Point(2, 1)
        Me.panelObjetos.Name = "panelObjetos"
        Me.panelObjetos.Size = New System.Drawing.Size(911, 560)
        Me.panelObjetos.TabIndex = 22
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.White
        Me.Button8.BackgroundImage = CType(resources.GetObject("Button8.BackgroundImage"), System.Drawing.Image)
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Location = New System.Drawing.Point(796, 486)
        Me.Button8.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(102, 69)
        Me.Button8.TabIndex = 26
        Me.Button8.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.Button8, "Cerrar")
        Me.Button8.UseVisualStyleBackColor = False
        '
        'dgvComensales
        '
        Me.dgvComensales.AllowUserToAddRows = False
        Me.dgvComensales.AllowUserToDeleteRows = False
        Me.dgvComensales.AllowUserToResizeRows = False
        Me.dgvComensales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvComensales.BackgroundColor = System.Drawing.Color.White
        Me.dgvComensales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvComensales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Comensal})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvComensales.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvComensales.Location = New System.Drawing.Point(3, 92)
        Me.dgvComensales.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvComensales.MultiSelect = False
        Me.dgvComensales.Name = "dgvComensales"
        Me.dgvComensales.ReadOnly = True
        Me.dgvComensales.RowHeadersVisible = False
        Me.dgvComensales.RowTemplate.Height = 32
        Me.dgvComensales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvComensales.Size = New System.Drawing.Size(96, 381)
        Me.dgvComensales.TabIndex = 25
        '
        'Comensal
        '
        Me.Comensal.HeaderText = "Comensal"
        Me.Comensal.Name = "Comensal"
        Me.Comensal.ReadOnly = True
        '
        'dgvPlatillos
        '
        Me.dgvPlatillos.AllowUserToAddRows = False
        Me.dgvPlatillos.AllowUserToDeleteRows = False
        Me.dgvPlatillos.AllowUserToResizeRows = False
        Me.dgvPlatillos.BackgroundColor = System.Drawing.Color.White
        Me.dgvPlatillos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPlatillos.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPlatillos.Location = New System.Drawing.Point(196, 92)
        Me.dgvPlatillos.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvPlatillos.Name = "dgvPlatillos"
        Me.dgvPlatillos.RowHeadersVisible = False
        Me.dgvPlatillos.RowTemplate.Height = 25
        Me.dgvPlatillos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPlatillos.Size = New System.Drawing.Size(704, 381)
        Me.dgvPlatillos.TabIndex = 24
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.White
        Me.Button5.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(105, 181)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 49)
        Me.Button5.TabIndex = 23
        Me.Button5.Text = "-"
        Me.ToolTip1.SetToolTip(Me.Button5, "Quitar")
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.Font = New System.Drawing.Font("Arial", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(105, 125)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 50)
        Me.Button3.TabIndex = 22
        Me.Button3.Text = "+"
        Me.ToolTip1.SetToolTip(Me.Button3, "Agregar")
        Me.Button3.UseVisualStyleBackColor = False
        '
        'frmRestauranteOrden
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(914, 563)
        Me.Controls.Add(Me.panelObjetos)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(900, 500)
        Me.Name = "frmRestauranteOrden"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comanda"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panelObjetos.ResumeLayout(False)
        Me.panelObjetos.PerformLayout()
        CType(Me.dgvComensales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvPlatillos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnPagar As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto1 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto2 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto3 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto4 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto5 As System.Windows.Forms.Button
    Friend WithEvents lblMesa As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents panelObjetos As System.Windows.Forms.Panel
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents dgvPlatillos As System.Windows.Forms.DataGridView
    Friend WithEvents dgvComensales As System.Windows.Forms.DataGridView
    Friend WithEvents Comensal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button8 As System.Windows.Forms.Button
End Class
