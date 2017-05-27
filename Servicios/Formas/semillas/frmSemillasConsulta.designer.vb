<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSemillasConsulta
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
        Me.dgvBoletas = New System.Windows.Forms.DataGridView()
        Me.seleccion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnExportar = New System.Windows.Forms.Button()
        Me.btnVer = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.txtFiltro = New System.Windows.Forms.TextBox()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtClave = New System.Windows.Forms.TextBox()
        Me.txtClaveProducto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBuscarProducto = New System.Windows.Forms.Button()
        Me.txtNombreProducto = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.chckTiempo = New System.Windows.Forms.CheckBox()
        Me.btnBusqueda = New System.Windows.Forms.Button()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.dtpFecha1 = New System.Windows.Forms.DateTimePicker()
        Me.dtpFecha2 = New System.Windows.Forms.DateTimePicker()
        Me.comboTipo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.dgvBoletas,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'dgvBoletas
        '
        Me.dgvBoletas.AllowUserToAddRows = false
        Me.dgvBoletas.AllowUserToDeleteRows = false
        Me.dgvBoletas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvBoletas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBoletas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.seleccion})
        Me.dgvBoletas.Location = New System.Drawing.Point(14, 148)
        Me.dgvBoletas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.dgvBoletas.Name = "dgvBoletas"
        Me.dgvBoletas.RowHeadersVisible = false
        Me.dgvBoletas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBoletas.Size = New System.Drawing.Size(703, 320)
        Me.dgvBoletas.TabIndex = 0
        '
        'seleccion
        '
        Me.seleccion.HeaderText = "sel."
        Me.seleccion.Name = "seleccion"
        '
        'btnExportar
        '
        Me.btnExportar.Location = New System.Drawing.Point(14, 481)
        Me.btnExportar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnExportar.Name = "btnExportar"
        Me.btnExportar.Size = New System.Drawing.Size(87, 28)
        Me.btnExportar.TabIndex = 1
        Me.btnExportar.Text = "Exportar"
        Me.btnExportar.UseVisualStyleBackColor = true
        Me.btnExportar.Visible = false
        '
        'btnVer
        '
        Me.btnVer.Location = New System.Drawing.Point(110, 481)
        Me.btnVer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnVer.Name = "btnVer"
        Me.btnVer.Size = New System.Drawing.Size(87, 28)
        Me.btnVer.TabIndex = 2
        Me.btnVer.Text = "Ver"
        Me.btnVer.UseVisualStyleBackColor = true
        '
        'btnEliminar
        '
        Me.btnEliminar.Location = New System.Drawing.Point(216, 481)
        Me.btnEliminar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(132, 28)
        Me.btnEliminar.TabIndex = 3
        Me.btnEliminar.Text = "Eliminar seleccion"
        Me.btnEliminar.UseVisualStyleBackColor = true
        '
        'txtFiltro
        '
        Me.txtFiltro.Enabled = false
        Me.txtFiltro.Location = New System.Drawing.Point(218, 63)
        Me.txtFiltro.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFiltro.Name = "txtFiltro"
        Me.txtFiltro.Size = New System.Drawing.Size(306, 22)
        Me.txtFiltro.TabIndex = 6
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(527, 60)
        Me.btnBuscar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(47, 25)
        Me.btnBuscar.TabIndex = 7
        Me.btnBuscar.Text = "..."
        Me.btnBuscar.UseVisualStyleBackColor = true
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Productor:"
        '
        'txtClave
        '
        Me.txtClave.Location = New System.Drawing.Point(94, 63)
        Me.txtClave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtClave.Name = "txtClave"
        Me.txtClave.Size = New System.Drawing.Size(116, 22)
        Me.txtClave.TabIndex = 10
        '
        'txtClaveProducto
        '
        Me.txtClaveProducto.Location = New System.Drawing.Point(94, 114)
        Me.txtClaveProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtClaveProducto.Name = "txtClaveProducto"
        Me.txtClaveProducto.Size = New System.Drawing.Size(116, 22)
        Me.txtClaveProducto.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(128, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 16)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Código:"
        '
        'btnBuscarProducto
        '
        Me.btnBuscarProducto.Location = New System.Drawing.Point(527, 112)
        Me.btnBuscarProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnBuscarProducto.Name = "btnBuscarProducto"
        Me.btnBuscarProducto.Size = New System.Drawing.Size(48, 25)
        Me.btnBuscarProducto.TabIndex = 12
        Me.btnBuscarProducto.Text = "..."
        Me.btnBuscarProducto.UseVisualStyleBackColor = true
        '
        'txtNombreProducto
        '
        Me.txtNombreProducto.Enabled = false
        Me.txtNombreProducto.Location = New System.Drawing.Point(216, 114)
        Me.txtNombreProducto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNombreProducto.Name = "txtNombreProducto"
        Me.txtNombreProducto.Size = New System.Drawing.Size(306, 22)
        Me.txtNombreProducto.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.Location = New System.Drawing.Point(128, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Código:"
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 116)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 16)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Producto:"
        '
        'Label7
        '
        Me.Label7.AutoSize = true
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label7.Location = New System.Drawing.Point(47, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 16)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Folio:"
        '
        'txtFolio
        '
        Me.txtFolio.Location = New System.Drawing.Point(94, 13)
        Me.txtFolio.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.Size = New System.Drawing.Size(116, 22)
        Me.txtFolio.TabIndex = 19
        '
        'chckTiempo
        '
        Me.chckTiempo.AutoSize = true
        Me.chckTiempo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.chckTiempo.Location = New System.Drawing.Point(606, 57)
        Me.chckTiempo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chckTiempo.Name = "chckTiempo"
        Me.chckTiempo.Size = New System.Drawing.Size(94, 18)
        Me.chckTiempo.TabIndex = 20
        Me.chckTiempo.Text = "Tiempo Real"
        Me.chckTiempo.UseVisualStyleBackColor = true
        '
        'btnBusqueda
        '
        Me.btnBusqueda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.btnBusqueda.Location = New System.Drawing.Point(613, 83)
        Me.btnBusqueda.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnBusqueda.Name = "btnBusqueda"
        Me.btnBusqueda.Size = New System.Drawing.Size(87, 28)
        Me.btnBusqueda.TabIndex = 21
        Me.btnBusqueda.Text = "Buscar"
        Me.btnBusqueda.UseVisualStyleBackColor = true
        '
        'Label36
        '
        Me.Label36.AutoSize = true
        Me.Label36.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label36.Location = New System.Drawing.Point(219, 13)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(57, 16)
        Me.Label36.TabIndex = 51
        Me.Label36.Text = "Fechas:"
        '
        'dtpFecha1
        '
        Me.dtpFecha1.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha1.Location = New System.Drawing.Point(280, 10)
        Me.dtpFecha1.Name = "dtpFecha1"
        Me.dtpFecha1.Size = New System.Drawing.Size(87, 22)
        Me.dtpFecha1.TabIndex = 50
        '
        'dtpFecha2
        '
        Me.dtpFecha2.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha2.Location = New System.Drawing.Point(373, 10)
        Me.dtpFecha2.Name = "dtpFecha2"
        Me.dtpFecha2.Size = New System.Drawing.Size(87, 22)
        Me.dtpFecha2.TabIndex = 52
        '
        'comboTipo
        '
        Me.comboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboTipo.Enabled = false
        Me.comboTipo.FormattingEnabled = true
        Me.comboTipo.Location = New System.Drawing.Point(579, 8)
        Me.comboTipo.Name = "comboTipo"
        Me.comboTipo.Size = New System.Drawing.Size(121, 24)
        Me.comboTipo.TabIndex = 53
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(480, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 16)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Tipo de boleta:"
        '
        'frmSemillasConsulta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7!, 16!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230,Byte),Integer), CType(CType(255,Byte),Integer), CType(CType(230,Byte),Integer))
        Me.ClientSize = New System.Drawing.Size(733, 513)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.comboTipo)
        Me.Controls.Add(Me.dtpFecha2)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.dtpFecha1)
        Me.Controls.Add(Me.btnBusqueda)
        Me.Controls.Add(Me.chckTiempo)
        Me.Controls.Add(Me.txtFolio)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtClaveProducto)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBuscarProducto)
        Me.Controls.Add(Me.txtNombreProducto)
        Me.Controls.Add(Me.txtClave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.txtFiltro)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnVer)
        Me.Controls.Add(Me.btnExportar)
        Me.Controls.Add(Me.dgvBoletas)
        Me.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmSemillasConsulta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta de boletas registradas"
        CType(Me.dgvBoletas,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents dgvBoletas As System.Windows.Forms.DataGridView
    Friend WithEvents btnExportar As System.Windows.Forms.Button
    Friend WithEvents btnVer As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents seleccion As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents txtFiltro As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents txtClaveProducto As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarProducto As System.Windows.Forms.Button
    Friend WithEvents txtNombreProducto As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFolio As System.Windows.Forms.TextBox
    Friend WithEvents chckTiempo As System.Windows.Forms.CheckBox
    Friend WithEvents btnBusqueda As System.Windows.Forms.Button
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFecha2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents comboTipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
