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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnAgregarComensal = New System.Windows.Forms.Button()
        Me.btnCancelarVenta = New System.Windows.Forms.Button()
        Me.btnCarta = New System.Windows.Forms.Button()
        Me.btnDirecto1 = New System.Windows.Forms.Button()
        Me.btnDirecto2 = New System.Windows.Forms.Button()
        Me.btnDirecto3 = New System.Windows.Forms.Button()
        Me.btnDirecto4 = New System.Windows.Forms.Button()
        Me.btnDirecto5 = New System.Windows.Forms.Button()
        Me.lblMesa = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnImprimirComanda = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.panelObjetos = New System.Windows.Forms.Panel()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.dgvComensales = New System.Windows.Forms.DataGridView()
        Me.colComensal1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvPlatillos = New System.Windows.Forms.DataGridView()
        Me.colId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colComensal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colPrecio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEnviado = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnRemover = New System.Windows.Forms.Button()
        Me.btnRepetir = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelObjetos.SuspendLayout()
        CType(Me.dgvComensales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvPlatillos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAgregarComensal
        '
        Me.btnAgregarComensal.BackColor = System.Drawing.Color.White
        Me.btnAgregarComensal.BackgroundImage = CType(resources.GetObject("btnAgregarComensal.BackgroundImage"), System.Drawing.Image)
        Me.btnAgregarComensal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAgregarComensal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarComensal.Location = New System.Drawing.Point(3, 485)
        Me.btnAgregarComensal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnAgregarComensal.Name = "btnAgregarComensal"
        Me.btnAgregarComensal.Size = New System.Drawing.Size(102, 69)
        Me.btnAgregarComensal.TabIndex = 2
        Me.btnAgregarComensal.Text = "Agregar"
        Me.btnAgregarComensal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnAgregarComensal, "Agregar comensal")
        Me.btnAgregarComensal.UseVisualStyleBackColor = False
        '
        'btnCancelarVenta
        '
        Me.btnCancelarVenta.BackColor = System.Drawing.Color.White
        Me.btnCancelarVenta.BackgroundImage = CType(resources.GetObject("btnCancelarVenta.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelarVenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCancelarVenta.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCancelarVenta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelarVenta.Location = New System.Drawing.Point(345, 485)
        Me.btnCancelarVenta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCancelarVenta.Name = "btnCancelarVenta"
        Me.btnCancelarVenta.Size = New System.Drawing.Size(102, 69)
        Me.btnCancelarVenta.TabIndex = 3
        Me.btnCancelarVenta.Text = "Cancelar"
        Me.btnCancelarVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnCancelarVenta, "Cancelar Venta")
        Me.btnCancelarVenta.UseVisualStyleBackColor = False
        '
        'btnCarta
        '
        Me.btnCarta.BackColor = System.Drawing.Color.White
        Me.btnCarta.BackgroundImage = CType(resources.GetObject("btnCarta.BackgroundImage"), System.Drawing.Image)
        Me.btnCarta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCarta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCarta.Location = New System.Drawing.Point(129, 485)
        Me.btnCarta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCarta.Name = "btnCarta"
        Me.btnCarta.Size = New System.Drawing.Size(102, 69)
        Me.btnCarta.TabIndex = 5
        Me.btnCarta.Text = "Ordenar"
        Me.btnCarta.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnCarta, "Carta")
        Me.btnCarta.UseVisualStyleBackColor = False
        '
        'btnDirecto1
        '
        Me.btnDirecto1.BackColor = System.Drawing.Color.White
        Me.btnDirecto1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto1.Location = New System.Drawing.Point(258, 13)
        Me.btnDirecto1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto1.Name = "btnDirecto1"
        Me.btnDirecto1.Size = New System.Drawing.Size(107, 69)
        Me.btnDirecto1.TabIndex = 6
        Me.btnDirecto1.Text = "No asig."
        Me.btnDirecto1.UseVisualStyleBackColor = False
        '
        'btnDirecto2
        '
        Me.btnDirecto2.BackColor = System.Drawing.Color.White
        Me.btnDirecto2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto2.Location = New System.Drawing.Point(371, 13)
        Me.btnDirecto2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto2.Name = "btnDirecto2"
        Me.btnDirecto2.Size = New System.Drawing.Size(107, 69)
        Me.btnDirecto2.TabIndex = 7
        Me.btnDirecto2.Text = "No asig."
        Me.btnDirecto2.UseVisualStyleBackColor = False
        '
        'btnDirecto3
        '
        Me.btnDirecto3.BackColor = System.Drawing.Color.White
        Me.btnDirecto3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto3.Location = New System.Drawing.Point(484, 13)
        Me.btnDirecto3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto3.Name = "btnDirecto3"
        Me.btnDirecto3.Size = New System.Drawing.Size(107, 69)
        Me.btnDirecto3.TabIndex = 8
        Me.btnDirecto3.Text = "No asig."
        Me.btnDirecto3.UseVisualStyleBackColor = False
        '
        'btnDirecto4
        '
        Me.btnDirecto4.BackColor = System.Drawing.Color.White
        Me.btnDirecto4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto4.Location = New System.Drawing.Point(597, 12)
        Me.btnDirecto4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto4.Name = "btnDirecto4"
        Me.btnDirecto4.Size = New System.Drawing.Size(107, 69)
        Me.btnDirecto4.TabIndex = 9
        Me.btnDirecto4.Text = "No asig."
        Me.btnDirecto4.UseVisualStyleBackColor = False
        '
        'btnDirecto5
        '
        Me.btnDirecto5.BackColor = System.Drawing.Color.White
        Me.btnDirecto5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirecto5.Location = New System.Drawing.Point(710, 12)
        Me.btnDirecto5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnDirecto5.Name = "btnDirecto5"
        Me.btnDirecto5.Size = New System.Drawing.Size(108, 69)
        Me.btnDirecto5.TabIndex = 10
        Me.btnDirecto5.Text = "No asig."
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
        'btnImprimirComanda
        '
        Me.btnImprimirComanda.BackColor = System.Drawing.Color.White
        Me.btnImprimirComanda.BackgroundImage = CType(resources.GetObject("btnImprimirComanda.BackgroundImage"), System.Drawing.Image)
        Me.btnImprimirComanda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnImprimirComanda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimirComanda.Location = New System.Drawing.Point(237, 485)
        Me.btnImprimirComanda.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnImprimirComanda.Name = "btnImprimirComanda"
        Me.btnImprimirComanda.Size = New System.Drawing.Size(102, 69)
        Me.btnImprimirComanda.TabIndex = 16
        Me.btnImprimirComanda.Text = "Comanda"
        Me.btnImprimirComanda.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnImprimirComanda, "Imprimir comanda")
        Me.btnImprimirComanda.UseVisualStyleBackColor = False
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
        Me.panelObjetos.Controls.Add(Me.btnCerrar)
        Me.panelObjetos.Controls.Add(Me.dgvComensales)
        Me.panelObjetos.Controls.Add(Me.dgvPlatillos)
        Me.panelObjetos.Controls.Add(Me.btnRemover)
        Me.panelObjetos.Controls.Add(Me.btnRepetir)
        Me.panelObjetos.Controls.Add(Me.btnCancelarVenta)
        Me.panelObjetos.Controls.Add(Me.btnCarta)
        Me.panelObjetos.Controls.Add(Me.btnImprimirComanda)
        Me.panelObjetos.Controls.Add(Me.lblMesa)
        Me.panelObjetos.Controls.Add(Me.btnDirecto5)
        Me.panelObjetos.Controls.Add(Me.Label4)
        Me.panelObjetos.Controls.Add(Me.btnDirecto4)
        Me.panelObjetos.Controls.Add(Me.btnAgregarComensal)
        Me.panelObjetos.Controls.Add(Me.Label1)
        Me.panelObjetos.Controls.Add(Me.btnDirecto3)
        Me.panelObjetos.Controls.Add(Me.btnDirecto2)
        Me.panelObjetos.Controls.Add(Me.btnDirecto1)
        Me.panelObjetos.Location = New System.Drawing.Point(2, 1)
        Me.panelObjetos.Name = "panelObjetos"
        Me.panelObjetos.Size = New System.Drawing.Size(911, 560)
        Me.panelObjetos.TabIndex = 22
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.Color.White
        Me.btnCerrar.BackgroundImage = CType(resources.GetObject("btnCerrar.BackgroundImage"), System.Drawing.Image)
        Me.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCerrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(726, 485)
        Me.btnCerrar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(102, 69)
        Me.btnCerrar.TabIndex = 26
        Me.btnCerrar.Text = "Regresar"
        Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnCerrar, "Regresar")
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'dgvComensales
        '
        Me.dgvComensales.AllowDrop = True
        Me.dgvComensales.AllowUserToAddRows = False
        Me.dgvComensales.AllowUserToDeleteRows = False
        Me.dgvComensales.AllowUserToResizeRows = False
        Me.dgvComensales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvComensales.BackgroundColor = System.Drawing.Color.White
        Me.dgvComensales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvComensales.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colComensal1})
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
        Me.dgvComensales.Size = New System.Drawing.Size(102, 381)
        Me.dgvComensales.TabIndex = 25
        '
        'colComensal1
        '
        Me.colComensal1.DataPropertyName = "comensal"
        Me.colComensal1.HeaderText = "Comensal"
        Me.colComensal1.Name = "colComensal1"
        Me.colComensal1.ReadOnly = True
        '
        'dgvPlatillos
        '
        Me.dgvPlatillos.AllowDrop = True
        Me.dgvPlatillos.AllowUserToAddRows = False
        Me.dgvPlatillos.AllowUserToDeleteRows = False
        Me.dgvPlatillos.AllowUserToResizeRows = False
        Me.dgvPlatillos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPlatillos.BackgroundColor = System.Drawing.Color.White
        Me.dgvPlatillos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPlatillos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colId, Me.colCant, Me.colDescripcion, Me.colComensal, Me.colPrecio, Me.colEnviado})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPlatillos.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPlatillos.Location = New System.Drawing.Point(124, 92)
        Me.dgvPlatillos.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvPlatillos.MultiSelect = False
        Me.dgvPlatillos.Name = "dgvPlatillos"
        Me.dgvPlatillos.ReadOnly = True
        Me.dgvPlatillos.RowHeadersVisible = False
        Me.dgvPlatillos.RowTemplate.Height = 25
        Me.dgvPlatillos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPlatillos.Size = New System.Drawing.Size(704, 381)
        Me.dgvPlatillos.TabIndex = 24
        '
        'colId
        '
        Me.colId.DataPropertyName = "iddetalle"
        Me.colId.HeaderText = "Id"
        Me.colId.Name = "colId"
        Me.colId.ReadOnly = True
        Me.colId.Visible = False
        '
        'colCant
        '
        Me.colCant.DataPropertyName = "cantidad"
        Me.colCant.HeaderText = "Cant."
        Me.colCant.Name = "colCant"
        Me.colCant.ReadOnly = True
        '
        'colDescripcion
        '
        Me.colDescripcion.DataPropertyName = "descripcion"
        Me.colDescripcion.FillWeight = 300.0!
        Me.colDescripcion.HeaderText = "Descripción"
        Me.colDescripcion.Name = "colDescripcion"
        Me.colDescripcion.ReadOnly = True
        '
        'colComensal
        '
        Me.colComensal.DataPropertyName = "comensal"
        Me.colComensal.HeaderText = "Comensal"
        Me.colComensal.Name = "colComensal"
        Me.colComensal.ReadOnly = True
        '
        'colPrecio
        '
        Me.colPrecio.DataPropertyName = "precio"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "C2"
        Me.colPrecio.DefaultCellStyle = DataGridViewCellStyle2
        Me.colPrecio.HeaderText = "Precio"
        Me.colPrecio.Name = "colPrecio"
        Me.colPrecio.ReadOnly = True
        '
        'colEnviado
        '
        Me.colEnviado.DataPropertyName = "enviado"
        Me.colEnviado.HeaderText = "Enviado"
        Me.colEnviado.Name = "colEnviado"
        Me.colEnviado.ReadOnly = True
        '
        'btnRemover
        '
        Me.btnRemover.BackColor = System.Drawing.Color.White
        Me.btnRemover.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemover.Location = New System.Drawing.Point(834, 173)
        Me.btnRemover.Name = "btnRemover"
        Me.btnRemover.Size = New System.Drawing.Size(75, 75)
        Me.btnRemover.TabIndex = 23
        Me.btnRemover.Text = "Eliminar"
        Me.ToolTip1.SetToolTip(Me.btnRemover, "Eliminar platillo")
        Me.btnRemover.UseVisualStyleBackColor = False
        '
        'btnRepetir
        '
        Me.btnRepetir.BackColor = System.Drawing.Color.White
        Me.btnRepetir.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRepetir.Location = New System.Drawing.Point(834, 92)
        Me.btnRepetir.Name = "btnRepetir"
        Me.btnRepetir.Size = New System.Drawing.Size(75, 75)
        Me.btnRepetir.TabIndex = 22
        Me.btnRepetir.Text = "Repetir"
        Me.ToolTip1.SetToolTip(Me.btnRepetir, "Duplicar platillo")
        Me.btnRepetir.UseVisualStyleBackColor = False
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
    Friend WithEvents btnAgregarComensal As System.Windows.Forms.Button
    Friend WithEvents btnCancelarVenta As System.Windows.Forms.Button
    Friend WithEvents btnCarta As System.Windows.Forms.Button
    Friend WithEvents btnDirecto1 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto2 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto3 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto4 As System.Windows.Forms.Button
    Friend WithEvents btnDirecto5 As System.Windows.Forms.Button
    Friend WithEvents lblMesa As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnImprimirComanda As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents panelObjetos As System.Windows.Forms.Panel
    Friend WithEvents btnRemover As System.Windows.Forms.Button
    Friend WithEvents btnRepetir As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents dgvPlatillos As System.Windows.Forms.DataGridView
    Friend WithEvents dgvComensales As System.Windows.Forms.DataGridView
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents colComensal1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCant As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colComensal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPrecio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEnviado As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
