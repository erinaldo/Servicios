<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDevolucionesElegir
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
        Me.DGDetalles = New System.Windows.Forms.DataGridView
        Me.Seleccionado = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TipoR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Extra = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Uni = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CantidadM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UniM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Codigo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PrecioU = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Importe = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Moneda = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnSalir = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.DGDetalles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGDetalles
        '
        Me.DGDetalles.AllowUserToAddRows = False
        Me.DGDetalles.AllowUserToDeleteRows = False
        Me.DGDetalles.AllowUserToResizeRows = False
        Me.DGDetalles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGDetalles.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccionado, Me.Id, Me.TipoR, Me.Extra, Me.Cantidad, Me.Uni, Me.CantidadM, Me.UniM, Me.Codigo, Me.Descripcion, Me.PrecioU, Me.Importe, Me.Moneda})
        Me.DGDetalles.Location = New System.Drawing.Point(27, 23)
        Me.DGDetalles.MultiSelect = False
        Me.DGDetalles.Name = "DGDetalles"
        Me.DGDetalles.ReadOnly = True
        Me.DGDetalles.RowHeadersVisible = False
        Me.DGDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGDetalles.Size = New System.Drawing.Size(827, 255)
        Me.DGDetalles.TabIndex = 21
        Me.DGDetalles.TabStop = False
        '
        'Seleccionado
        '
        Me.Seleccionado.DataPropertyName = "selec"
        Me.Seleccionado.FalseValue = "0"
        Me.Seleccionado.HeaderText = " "
        Me.Seleccionado.Name = "Seleccionado"
        Me.Seleccionado.ReadOnly = True
        Me.Seleccionado.TrueValue = "1"
        Me.Seleccionado.Width = 16
        '
        'Id
        '
        Me.Id.DataPropertyName = "Id"
        Me.Id.HeaderText = "Id"
        Me.Id.Name = "Id"
        Me.Id.ReadOnly = True
        Me.Id.Width = 41
        '
        'TipoR
        '
        Me.TipoR.DataPropertyName = "TipoR"
        Me.TipoR.HeaderText = "TipoR"
        Me.TipoR.Name = "TipoR"
        Me.TipoR.ReadOnly = True
        Me.TipoR.Width = 61
        '
        'Extra
        '
        Me.Extra.DataPropertyName = "Extra"
        Me.Extra.HeaderText = "Extra"
        Me.Extra.Name = "Extra"
        Me.Extra.ReadOnly = True
        Me.Extra.Width = 56
        '
        'Cantidad
        '
        Me.Cantidad.DataPropertyName = "Cantidad"
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Width = 74
        '
        'Uni
        '
        Me.Uni.DataPropertyName = "Uni."
        Me.Uni.HeaderText = "Uni"
        Me.Uni.Name = "Uni"
        Me.Uni.ReadOnly = True
        Me.Uni.Width = 48
        '
        'CantidadM
        '
        Me.CantidadM.DataPropertyName = "Cantidad M."
        Me.CantidadM.HeaderText = "CantidadM"
        Me.CantidadM.Name = "CantidadM"
        Me.CantidadM.ReadOnly = True
        Me.CantidadM.Width = 83
        '
        'UniM
        '
        Me.UniM.DataPropertyName = "Uni. M."
        Me.UniM.HeaderText = "UniM"
        Me.UniM.Name = "UniM"
        Me.UniM.ReadOnly = True
        Me.UniM.Width = 57
        '
        'Codigo
        '
        Me.Codigo.DataPropertyName = "Código"
        Me.Codigo.HeaderText = "Codigo"
        Me.Codigo.Name = "Codigo"
        Me.Codigo.ReadOnly = True
        Me.Codigo.Width = 65
        '
        'Descripcion
        '
        Me.Descripcion.DataPropertyName = "Descripción"
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.ReadOnly = True
        Me.Descripcion.Width = 88
        '
        'PrecioU
        '
        Me.PrecioU.DataPropertyName = "Precio U."
        Me.PrecioU.HeaderText = "PrecioU"
        Me.PrecioU.Name = "PrecioU"
        Me.PrecioU.ReadOnly = True
        Me.PrecioU.Width = 70
        '
        'Importe
        '
        Me.Importe.DataPropertyName = "Importe"
        Me.Importe.HeaderText = "Importe"
        Me.Importe.Name = "Importe"
        Me.Importe.ReadOnly = True
        Me.Importe.Width = 67
        '
        'Moneda
        '
        Me.Moneda.DataPropertyName = "Moneda"
        Me.Moneda.HeaderText = "Moneda"
        Me.Moneda.Name = "Moneda"
        Me.Moneda.ReadOnly = True
        Me.Moneda.Width = 71
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAceptar.Location = New System.Drawing.Point(293, 309)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 22
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.Location = New System.Drawing.Point(417, 308)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(75, 23)
        Me.btnSalir.TabIndex = 23
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.Label1.Location = New System.Drawing.Point(24, 281)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(323, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "*Selecciona los productos a los que les deseas aplicar Devolución."
        '
        'frmDevolucionesElegir
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnAceptar
        Me.ClientSize = New System.Drawing.Size(879, 351)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.DGDetalles)
        Me.Name = "frmDevolucionesElegir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Elegir Devoluciones"
        CType(Me.DGDetalles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents DGDetalles As System.Windows.Forms.DataGridView
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Seleccionado As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TipoR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Extra As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Uni As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CantidadM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UniM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Codigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrecioU As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Importe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Moneda As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
