<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCatalogos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rdbClientes = New System.Windows.Forms.RadioButton()
        Me.rdbClientesDirectorio = New System.Windows.Forms.RadioButton()
        Me.rdbProveedores = New System.Windows.Forms.RadioButton()
        Me.rdbProveedoresDirectorio = New System.Windows.Forms.RadioButton()
        Me.rdbAlmacenes = New System.Windows.Forms.RadioButton()
        Me.rdbSucursales = New System.Windows.Forms.RadioButton()
        Me.rdbVendedores = New System.Windows.Forms.RadioButton()
        Me.rdbArticulos = New System.Windows.Forms.RadioButton()
        Me.rdbClasificacionesArticulos = New System.Windows.Forms.RadioButton()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cmbZonacliente = New System.Windows.Forms.ComboBox()
        Me.lblZonaCliente = New System.Windows.Forms.Label()
        Me.lblZonaVendedor = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'rdbClientes
        '
        Me.rdbClientes.AutoSize = True
        Me.rdbClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbClientes.Location = New System.Drawing.Point(21, 99)
        Me.rdbClientes.Name = "rdbClientes"
        Me.rdbClientes.Size = New System.Drawing.Size(70, 17)
        Me.rdbClientes.TabIndex = 5
        Me.rdbClientes.Text = "Clientes"
        Me.rdbClientes.UseVisualStyleBackColor = True
        '
        'rdbClientesDirectorio
        '
        Me.rdbClientesDirectorio.AutoSize = True
        Me.rdbClientesDirectorio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbClientesDirectorio.Location = New System.Drawing.Point(21, 122)
        Me.rdbClientesDirectorio.Name = "rdbClientesDirectorio"
        Me.rdbClientesDirectorio.Size = New System.Drawing.Size(137, 17)
        Me.rdbClientesDirectorio.TabIndex = 6
        Me.rdbClientesDirectorio.Text = "Clientes (Directorio)"
        Me.rdbClientesDirectorio.UseVisualStyleBackColor = True
        '
        'rdbProveedores
        '
        Me.rdbProveedores.AutoSize = True
        Me.rdbProveedores.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbProveedores.Location = New System.Drawing.Point(21, 148)
        Me.rdbProveedores.Name = "rdbProveedores"
        Me.rdbProveedores.Size = New System.Drawing.Size(96, 17)
        Me.rdbProveedores.TabIndex = 8
        Me.rdbProveedores.Text = "Proveedores"
        Me.rdbProveedores.UseVisualStyleBackColor = True
        '
        'rdbProveedoresDirectorio
        '
        Me.rdbProveedoresDirectorio.AutoSize = True
        Me.rdbProveedoresDirectorio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbProveedoresDirectorio.Location = New System.Drawing.Point(21, 171)
        Me.rdbProveedoresDirectorio.Name = "rdbProveedoresDirectorio"
        Me.rdbProveedoresDirectorio.Size = New System.Drawing.Size(163, 17)
        Me.rdbProveedoresDirectorio.TabIndex = 9
        Me.rdbProveedoresDirectorio.Text = "Proveedores (Directorio)"
        Me.rdbProveedoresDirectorio.UseVisualStyleBackColor = True
        '
        'rdbAlmacenes
        '
        Me.rdbAlmacenes.AutoSize = True
        Me.rdbAlmacenes.Checked = True
        Me.rdbAlmacenes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbAlmacenes.Location = New System.Drawing.Point(21, 30)
        Me.rdbAlmacenes.Name = "rdbAlmacenes"
        Me.rdbAlmacenes.Size = New System.Drawing.Size(86, 17)
        Me.rdbAlmacenes.TabIndex = 1
        Me.rdbAlmacenes.TabStop = True
        Me.rdbAlmacenes.Text = "Almacenes"
        Me.rdbAlmacenes.UseVisualStyleBackColor = True
        '
        'rdbSucursales
        '
        Me.rdbSucursales.AutoSize = True
        Me.rdbSucursales.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbSucursales.Location = New System.Drawing.Point(21, 194)
        Me.rdbSucursales.Name = "rdbSucursales"
        Me.rdbSucursales.Size = New System.Drawing.Size(87, 17)
        Me.rdbSucursales.TabIndex = 10
        Me.rdbSucursales.Text = "Sucursales"
        Me.rdbSucursales.UseVisualStyleBackColor = True
        '
        'rdbVendedores
        '
        Me.rdbVendedores.AutoSize = True
        Me.rdbVendedores.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbVendedores.Location = New System.Drawing.Point(21, 217)
        Me.rdbVendedores.Name = "rdbVendedores"
        Me.rdbVendedores.Size = New System.Drawing.Size(92, 17)
        Me.rdbVendedores.TabIndex = 11
        Me.rdbVendedores.Text = "Vendedores"
        Me.rdbVendedores.UseVisualStyleBackColor = True
        '
        'rdbArticulos
        '
        Me.rdbArticulos.AutoSize = True
        Me.rdbArticulos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbArticulos.Location = New System.Drawing.Point(21, 53)
        Me.rdbArticulos.Name = "rdbArticulos"
        Me.rdbArticulos.Size = New System.Drawing.Size(74, 17)
        Me.rdbArticulos.TabIndex = 2
        Me.rdbArticulos.Text = "Articulos"
        Me.rdbArticulos.UseVisualStyleBackColor = True
        '
        'rdbClasificacionesArticulos
        '
        Me.rdbClasificacionesArticulos.AutoSize = True
        Me.rdbClasificacionesArticulos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbClasificacionesArticulos.Location = New System.Drawing.Point(21, 76)
        Me.rdbClasificacionesArticulos.Name = "rdbClasificacionesArticulos"
        Me.rdbClasificacionesArticulos.Size = New System.Drawing.Size(181, 17)
        Me.rdbClasificacionesArticulos.TabIndex = 3
        Me.rdbClasificacionesArticulos.Text = "Clasificaciones de Articulos"
        Me.rdbClasificacionesArticulos.UseVisualStyleBackColor = True
        '
        'ComboBox7
        '
        Me.ComboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Location = New System.Drawing.Point(396, 110)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(250, 24)
        Me.ComboBox7.TabIndex = 17
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(265, 113)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 16)
        Me.Label16.TabIndex = 208
        Me.Label16.Text = "Nivel 3:"
        '
        'ComboBox6
        '
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(374, 78)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(250, 24)
        Me.ComboBox6.TabIndex = 15
        '
        'TextBox13
        '
        Me.TextBox13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox13.Location = New System.Drawing.Point(326, 111)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(64, 22)
        Me.TextBox13.TabIndex = 16
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(243, 81)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 16)
        Me.Label15.TabIndex = 207
        Me.Label15.Text = "Nivel 2:"
        '
        'TextBox12
        '
        Me.TextBox12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox12.Location = New System.Drawing.Point(304, 79)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(64, 22)
        Me.TextBox12.TabIndex = 14
        '
        'TextBox10
        '
        Me.TextBox10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox10.Location = New System.Drawing.Point(283, 46)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(66, 22)
        Me.TextBox10.TabIndex = 12
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(355, 46)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(250, 24)
        Me.ComboBox3.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(224, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 16)
        Me.Label7.TabIndex = 204
        Me.Label7.Text = "Nivel 1:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(371, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(93, 16)
        Me.Label17.TabIndex = 209
        Me.Label17.Text = "Clasificacion:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(426, 216)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(99, 32)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Imprimir"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(531, 217)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(93, 29)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cmbZonacliente
        '
        Me.cmbZonacliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbZonacliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbZonacliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbZonacliente.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbZonacliente.FormattingEnabled = True
        Me.cmbZonacliente.Location = New System.Drawing.Point(396, 145)
        Me.cmbZonacliente.Name = "cmbZonacliente"
        Me.cmbZonacliente.Size = New System.Drawing.Size(125, 24)
        Me.cmbZonacliente.TabIndex = 295
        Me.cmbZonacliente.Visible = False
        '
        'lblZonaCliente
        '
        Me.lblZonaCliente.AutoSize = True
        Me.lblZonaCliente.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZonaCliente.Location = New System.Drawing.Point(301, 149)
        Me.lblZonaCliente.Name = "lblZonaCliente"
        Me.lblZonaCliente.Size = New System.Drawing.Size(92, 16)
        Me.lblZonaCliente.TabIndex = 294
        Me.lblZonaCliente.Text = "Zona Cliente:"
        Me.lblZonaCliente.Visible = False
        '
        'lblZonaVendedor
        '
        Me.lblZonaVendedor.AutoSize = True
        Me.lblZonaVendedor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZonaVendedor.Location = New System.Drawing.Point(285, 149)
        Me.lblZonaVendedor.Name = "lblZonaVendedor"
        Me.lblZonaVendedor.Size = New System.Drawing.Size(108, 16)
        Me.lblZonaVendedor.TabIndex = 296
        Me.lblZonaVendedor.Text = "Zona Vendedor:"
        Me.lblZonaVendedor.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(290, 177)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 16)
        Me.Label1.TabIndex = 298
        Me.Label1.Text = "Código cliente:"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(396, 175)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(64, 22)
        Me.TextBox1.TabIndex = 297
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(227, 211)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(188, 20)
        Me.CheckBox1.TabIndex = 299
        Me.CheckBox1.Text = "Solo clientes con crédito."
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox2.Location = New System.Drawing.Point(227, 235)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(163, 20)
        Me.CheckBox2.TabIndex = 300
        Me.CheckBox2.Text = "Solo descontinuados."
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'frmCatalogos
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(673, 263)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.lblZonaVendedor)
        Me.Controls.Add(Me.cmbZonacliente)
        Me.Controls.Add(Me.lblZonaCliente)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.ComboBox7)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.ComboBox6)
        Me.Controls.Add(Me.TextBox13)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.TextBox12)
        Me.Controls.Add(Me.TextBox10)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.rdbClasificacionesArticulos)
        Me.Controls.Add(Me.rdbArticulos)
        Me.Controls.Add(Me.rdbVendedores)
        Me.Controls.Add(Me.rdbSucursales)
        Me.Controls.Add(Me.rdbAlmacenes)
        Me.Controls.Add(Me.rdbProveedoresDirectorio)
        Me.Controls.Add(Me.rdbProveedores)
        Me.Controls.Add(Me.rdbClientesDirectorio)
        Me.Controls.Add(Me.rdbClientes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCatalogos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Impresión de catalogos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdbClientes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbClientesDirectorio As System.Windows.Forms.RadioButton
    Friend WithEvents rdbProveedores As System.Windows.Forms.RadioButton
    Friend WithEvents rdbProveedoresDirectorio As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAlmacenes As System.Windows.Forms.RadioButton
    Friend WithEvents rdbSucursales As System.Windows.Forms.RadioButton
    Friend WithEvents rdbVendedores As System.Windows.Forms.RadioButton
    Friend WithEvents rdbArticulos As System.Windows.Forms.RadioButton
    Friend WithEvents rdbClasificacionesArticulos As System.Windows.Forms.RadioButton
    Friend WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox13 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cmbZonacliente As System.Windows.Forms.ComboBox
    Friend WithEvents lblZonaCliente As System.Windows.Forms.Label
    Friend WithEvents lblZonaVendedor As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox

End Class
