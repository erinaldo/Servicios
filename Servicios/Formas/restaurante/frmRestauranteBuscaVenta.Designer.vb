<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestauranteBuscaVenta
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
        Me.panelObjetos = New System.Windows.Forms.Panel()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.hora2 = New System.Windows.Forms.DateTimePicker()
        Me.hora1 = New System.Windows.Forms.DateTimePicker()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.comboSucursal = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.dtp2 = New System.Windows.Forms.DateTimePicker()
        Me.dtp1 = New System.Windows.Forms.DateTimePicker()
        Me.dgvVentas = New System.Windows.Forms.DataGridView()
        Me.panelObjetos.SuspendLayout()
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelObjetos
        '
        Me.panelObjetos.Controls.Add(Me.btnGuardar)
        Me.panelObjetos.Controls.Add(Me.btnCerrar)
        Me.panelObjetos.Controls.Add(Me.Label4)
        Me.panelObjetos.Controls.Add(Me.Label3)
        Me.panelObjetos.Controls.Add(Me.hora2)
        Me.panelObjetos.Controls.Add(Me.hora1)
        Me.panelObjetos.Controls.Add(Me.btnBuscar)
        Me.panelObjetos.Controls.Add(Me.Label2)
        Me.panelObjetos.Controls.Add(Me.comboSucursal)
        Me.panelObjetos.Controls.Add(Me.CheckBox1)
        Me.panelObjetos.Controls.Add(Me.lblTipo)
        Me.panelObjetos.Controls.Add(Me.txtFolio)
        Me.panelObjetos.Controls.Add(Me.dtp2)
        Me.panelObjetos.Controls.Add(Me.dtp1)
        Me.panelObjetos.Controls.Add(Me.dgvVentas)
        Me.panelObjetos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelObjetos.Location = New System.Drawing.Point(1, 5)
        Me.panelObjetos.Name = "panelObjetos"
        Me.panelObjetos.Size = New System.Drawing.Size(726, 605)
        Me.panelObjetos.TabIndex = 0
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(455, 75)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 49)
        Me.btnGuardar.TabIndex = 14
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        Me.btnGuardar.Visible = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(455, 130)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 49)
        Me.btnCerrar.TabIndex = 13
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(430, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(17, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "A"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(164, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(20, 16)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Al"
        '
        'hora2
        '
        Me.hora2.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.hora2.Location = New System.Drawing.Point(453, 47)
        Me.hora2.Name = "hora2"
        Me.hora2.Size = New System.Drawing.Size(106, 22)
        Me.hora2.TabIndex = 10
        '
        'hora1
        '
        Me.hora1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.hora1.Location = New System.Drawing.Point(318, 47)
        Me.hora1.Name = "hora1"
        Me.hora1.Size = New System.Drawing.Size(106, 22)
        Me.hora1.TabIndex = 9
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(430, 6)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(106, 24)
        Me.btnBuscar.TabIndex = 8
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(164, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Sucursal"
        '
        'comboSucursal
        '
        Me.comboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboSucursal.FormattingEnabled = True
        Me.comboSucursal.Location = New System.Drawing.Point(229, 7)
        Me.comboSucursal.Name = "comboSucursal"
        Me.comboSucursal.Size = New System.Drawing.Size(195, 24)
        Me.comboSucursal.TabIndex = 6
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(542, 6)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(99, 20)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Tiempo Real"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'lblTipo
        '
        Me.lblTipo.AutoSize = True
        Me.lblTipo.Location = New System.Drawing.Point(5, 10)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(36, 16)
        Me.lblTipo.TabIndex = 4
        Me.lblTipo.Text = "Folio"
        '
        'txtFolio
        '
        Me.txtFolio.Location = New System.Drawing.Point(47, 7)
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.Size = New System.Drawing.Size(106, 22)
        Me.txtFolio.TabIndex = 3
        '
        'dtp2
        '
        Me.dtp2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtp2.Location = New System.Drawing.Point(190, 47)
        Me.dtp2.Name = "dtp2"
        Me.dtp2.Size = New System.Drawing.Size(103, 22)
        Me.dtp2.TabIndex = 2
        '
        'dtp1
        '
        Me.dtp1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp1.Location = New System.Drawing.Point(47, 46)
        Me.dtp1.Name = "dtp1"
        Me.dtp1.Size = New System.Drawing.Size(106, 22)
        Me.dtp1.TabIndex = 1
        '
        'dgvVentas
        '
        Me.dgvVentas.AllowUserToAddRows = False
        Me.dgvVentas.AllowUserToDeleteRows = False
        Me.dgvVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVentas.Location = New System.Drawing.Point(18, 75)
        Me.dgvVentas.MultiSelect = False
        Me.dgvVentas.Name = "dgvVentas"
        Me.dgvVentas.ReadOnly = True
        Me.dgvVentas.RowHeadersVisible = False
        Me.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvVentas.Size = New System.Drawing.Size(431, 415)
        Me.dgvVentas.TabIndex = 0
        '
        'frmRestauranteBuscaVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(739, 612)
        Me.Controls.Add(Me.panelObjetos)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRestauranteBuscaVenta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Buscador de ventas"
        Me.panelObjetos.ResumeLayout(False)
        Me.panelObjetos.PerformLayout()
        CType(Me.dgvVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelObjetos As System.Windows.Forms.Panel
    Friend WithEvents dgvVentas As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents comboSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    Friend WithEvents txtFolio As System.Windows.Forms.TextBox
    Friend WithEvents dtp2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtp1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents hora2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents hora1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
End Class
