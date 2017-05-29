<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConsultaUbicaciones
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.colAlmacen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colUbicacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colExistencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtArticulo = New System.Windows.Forms.TextBox()
        Me.txtUbicacion = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cmbAlmacen = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colAlmacen, Me.colUbicacion, Me.colExistencia})
        Me.DataGridView1.Location = New System.Drawing.Point(17, 125)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(385, 348)
        Me.DataGridView1.TabIndex = 5
        '
        'colAlmacen
        '
        Me.colAlmacen.DataPropertyName = "nombre"
        Me.colAlmacen.FillWeight = 200.0!
        Me.colAlmacen.HeaderText = "Almacén"
        Me.colAlmacen.Name = "colAlmacen"
        Me.colAlmacen.ReadOnly = True
        '
        'colUbicacion
        '
        Me.colUbicacion.DataPropertyName = "ubicacion"
        Me.colUbicacion.FillWeight = 200.0!
        Me.colUbicacion.HeaderText = "Ubicación"
        Me.colUbicacion.Name = "colUbicacion"
        Me.colUbicacion.ReadOnly = True
        '
        'colExistencia
        '
        Me.colExistencia.DataPropertyName = "cantidad"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        Me.colExistencia.DefaultCellStyle = DataGridViewCellStyle3
        Me.colExistencia.HeaderText = "Existencia"
        Me.colExistencia.Name = "colExistencia"
        Me.colExistencia.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Artículo:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Ubicación"
        '
        'txtArticulo
        '
        Me.txtArticulo.Location = New System.Drawing.Point(82, 7)
        Me.txtArticulo.Name = "txtArticulo"
        Me.txtArticulo.ReadOnly = True
        Me.txtArticulo.Size = New System.Drawing.Size(320, 21)
        Me.txtArticulo.TabIndex = 0
        '
        'txtUbicacion
        '
        Me.txtUbicacion.Location = New System.Drawing.Point(82, 64)
        Me.txtUbicacion.Name = "txtUbicacion"
        Me.txtUbicacion.Size = New System.Drawing.Size(320, 21)
        Me.txtUbicacion.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(82, 91)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 28)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Imprimir"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'cmbAlmacen
        '
        Me.cmbAlmacen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAlmacen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAlmacen.DisplayMember = "nombre"
        Me.cmbAlmacen.FormattingEnabled = True
        Me.cmbAlmacen.Location = New System.Drawing.Point(82, 35)
        Me.cmbAlmacen.Name = "cmbAlmacen"
        Me.cmbAlmacen.Size = New System.Drawing.Size(320, 23)
        Me.cmbAlmacen.TabIndex = 1
        Me.cmbAlmacen.ValueMember = "idalmacen"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Almacén:"
        '
        'frmConsultaUbicaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 485)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbAlmacen)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtUbicacion)
        Me.Controls.Add(Me.txtArticulo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmConsultaUbicaciones"
        Me.Text = "Consultar ubicaciones"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtArticulo As System.Windows.Forms.TextBox
    Friend WithEvents txtUbicacion As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cmbAlmacen As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents colAlmacen As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colUbicacion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colExistencia As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
