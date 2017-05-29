<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUbicaciones
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
        Me.dgvDimension3 = New System.Windows.Forms.DataGridView()
        Me.colDimension3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.dgvDimension2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvDimension1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgvDimension3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDimension2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDimension1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvDimension3
        '
        Me.dgvDimension3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDimension3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDimension3.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDimension3})
        Me.dgvDimension3.Location = New System.Drawing.Point(370, 29)
        Me.dgvDimension3.Name = "dgvDimension3"
        Me.dgvDimension3.RowHeadersVisible = False
        Me.dgvDimension3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDimension3.Size = New System.Drawing.Size(173, 293)
        Me.dgvDimension3.TabIndex = 2
        '
        'colDimension3
        '
        Me.colDimension3.HeaderText = "Descripción"
        Me.colDimension3.Name = "colDimension3"
        '
        'btnGenerar
        '
        Me.btnGenerar.Location = New System.Drawing.Point(445, 328)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(97, 25)
        Me.btnGenerar.TabIndex = 3
        Me.btnGenerar.Text = "Generar"
        Me.btnGenerar.UseVisualStyleBackColor = True
        '
        'dgvDimension2
        '
        Me.dgvDimension2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDimension2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDimension2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1})
        Me.dgvDimension2.Location = New System.Drawing.Point(191, 29)
        Me.dgvDimension2.Name = "dgvDimension2"
        Me.dgvDimension2.RowHeadersVisible = False
        Me.dgvDimension2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDimension2.Size = New System.Drawing.Size(173, 293)
        Me.dgvDimension2.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Descripción"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'dgvDimension1
        '
        Me.dgvDimension1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDimension1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDimension1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2})
        Me.dgvDimension1.Location = New System.Drawing.Point(12, 29)
        Me.dgvDimension1.Name = "dgvDimension1"
        Me.dgvDimension1.RowHeadersVisible = False
        Me.dgvDimension1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDimension1.Size = New System.Drawing.Size(173, 293)
        Me.dgvDimension1.TabIndex = 0
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Descripción"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Primera posición"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(188, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Segunda posición"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(367, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Tercera posición"
        '
        'frmUbicaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(554, 365)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvDimension1)
        Me.Controls.Add(Me.dgvDimension2)
        Me.Controls.Add(Me.btnGenerar)
        Me.Controls.Add(Me.dgvDimension3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmUbicaciones"
        Me.Text = "Generar ubicaciones"
        CType(Me.dgvDimension3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDimension2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDimension1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvDimension3 As System.Windows.Forms.DataGridView
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents colDimension3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDimension2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvDimension1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
