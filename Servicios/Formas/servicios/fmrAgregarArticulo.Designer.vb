<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fmrAgregarArticulo
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
        Me.Button7 = New System.Windows.Forms.Button
        Me.DGInventario = New System.Windows.Forms.DataGridView
        Me.Label15 = New System.Windows.Forms.Label
        Me.Button6 = New System.Windows.Forms.Button
        Me.btnagregar = New System.Windows.Forms.Button
        Me.DGInventario2 = New System.Windows.Forms.DataGridView
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtbusqueda = New System.Windows.Forms.TextBox
        Me.lblTotalArticulos = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCantidad = New System.Windows.Forms.NumericUpDown
        Me.txtNombre = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtCodigo = New System.Windows.Forms.TextBox
        Me.btnNuevo = New System.Windows.Forms.Button
        Me.lblTotal = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPrecio = New System.Windows.Forms.TextBox
        Me.btnconsultar = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGInventario2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(286, 370)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(133, 24)
        Me.Button7.TabIndex = 8
        Me.Button7.Text = "Finalizar"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'DGInventario
        '
        Me.DGInventario.AllowUserToAddRows = False
        Me.DGInventario.AllowUserToDeleteRows = False
        Me.DGInventario.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGInventario.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGInventario.Location = New System.Drawing.Point(72, 223)
        Me.DGInventario.Name = "DGInventario"
        Me.DGInventario.ReadOnly = True
        Me.DGInventario.RowHeadersVisible = False
        Me.DGInventario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGInventario.Size = New System.Drawing.Size(552, 123)
        Me.DGInventario.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(15, 10)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(108, 16)
        Me.Label15.TabIndex = 65
        Me.Label15.Text = "Buscar Artículo:"
        '
        'Button6
        '
        Me.Button6.Enabled = False
        Me.Button6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Location = New System.Drawing.Point(618, 165)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(73, 25)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "Eliminar"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'btnagregar
        '
        Me.btnagregar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnagregar.Location = New System.Drawing.Point(462, 165)
        Me.btnagregar.Name = "btnagregar"
        Me.btnagregar.Size = New System.Drawing.Size(73, 25)
        Me.btnagregar.TabIndex = 5
        Me.btnagregar.Text = "Agregar"
        Me.btnagregar.UseVisualStyleBackColor = True
        '
        'DGInventario2
        '
        Me.DGInventario2.AllowUserToAddRows = False
        Me.DGInventario2.AllowUserToDeleteRows = False
        Me.DGInventario2.AllowUserToResizeRows = False
        Me.DGInventario2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGInventario2.Location = New System.Drawing.Point(18, 57)
        Me.DGInventario2.Name = "DGInventario2"
        Me.DGInventario2.ReadOnly = True
        Me.DGInventario2.RowHeadersVisible = False
        Me.DGInventario2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGInventario2.Size = New System.Drawing.Size(361, 126)
        Me.DGInventario2.TabIndex = 9
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(273, 204)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(146, 16)
        Me.Label13.TabIndex = 64
        Me.Label13.Text = "Artículos consumidos:"
        '
        'txtbusqueda
        '
        Me.txtbusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtbusqueda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbusqueda.Location = New System.Drawing.Point(17, 29)
        Me.txtbusqueda.Name = "txtbusqueda"
        Me.txtbusqueda.Size = New System.Drawing.Size(362, 22)
        Me.txtbusqueda.TabIndex = 0
        '
        'lblTotalArticulos
        '
        Me.lblTotalArticulos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalArticulos.Location = New System.Drawing.Point(522, 352)
        Me.lblTotalArticulos.Name = "lblTotalArticulos"
        Me.lblTotalArticulos.Size = New System.Drawing.Size(60, 15)
        Me.lblTotalArticulos.TabIndex = 74
        Me.lblTotalArticulos.Text = "0"
        Me.lblTotalArticulos.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(472, 352)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 16)
        Me.Label18.TabIndex = 71
        Me.Label18.Text = "Total:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(404, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "Cantidad:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(381, 142)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "Precio unitario:"
        '
        'txtCantidad
        '
        Me.txtCantidad.Location = New System.Drawing.Point(462, 112)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(100, 20)
        Me.txtCantidad.TabIndex = 3
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtCantidad.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtNombre
        '
        Me.txtNombre.BackColor = System.Drawing.Color.White
        Me.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNombre.Location = New System.Drawing.Point(462, 86)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.ReadOnly = True
        Me.txtNombre.Size = New System.Drawing.Size(252, 20)
        Me.txtNombre.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(411, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 81
        Me.Label3.Text = "Nombre:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(415, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 83
        Me.Label4.Text = "Código:"
        '
        'txtCodigo
        '
        Me.txtCodigo.BackColor = System.Drawing.Color.White
        Me.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCodigo.Location = New System.Drawing.Point(462, 60)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(100, 20)
        Me.txtCodigo.TabIndex = 1
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(539, 165)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(73, 25)
        Me.btnNuevo.TabIndex = 6
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(618, 140)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(60, 15)
        Me.lblTotal.TabIndex = 86
        Me.lblTotal.Text = "0"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(568, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 16)
        Me.Label6.TabIndex = 85
        Me.Label6.Text = "Total:"
        '
        'txtPrecio
        '
        Me.txtPrecio.Location = New System.Drawing.Point(462, 140)
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(100, 20)
        Me.txtPrecio.TabIndex = 4
        Me.txtPrecio.Text = "0.00"
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnconsultar
        '
        Me.btnconsultar.Location = New System.Drawing.Point(18, 189)
        Me.btnconsultar.Name = "btnconsultar"
        Me.btnconsultar.Size = New System.Drawing.Size(95, 23)
        Me.btnconsultar.TabIndex = 10
        Me.btnconsultar.Text = "Componentes"
        Me.btnconsultar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(119, 189)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Historial"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(18, 370)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 23)
        Me.btnCerrar.TabIndex = 87
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(462, 34)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(100, 20)
        Me.dtpFecha.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label5.Location = New System.Drawing.Point(417, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 88
        Me.Label5.Text = "Fecha:"
        '
        'fmrAgregarArticulo
        '
        Me.AcceptButton = Me.btnagregar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(741, 399)
        Me.Controls.Add(Me.dtpFecha)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnconsultar)
        Me.Controls.Add(Me.txtPrecio)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblTotalArticulos)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.DGInventario)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.btnagregar)
        Me.Controls.Add(Me.DGInventario2)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtbusqueda)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fmrAgregarArticulo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Artículos"
        CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGInventario2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents DGInventario As System.Windows.Forms.DataGridView
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents btnagregar As System.Windows.Forms.Button
    Friend WithEvents DGInventario2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtbusqueda As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalArticulos As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPrecio As System.Windows.Forms.TextBox
    Friend WithEvents btnconsultar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
