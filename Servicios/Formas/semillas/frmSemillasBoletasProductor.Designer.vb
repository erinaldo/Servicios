﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSemillasBoletasProductor
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
        Me.btnLiquidar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.dgvBoletas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvBoletas
        '
        Me.dgvBoletas.AllowUserToAddRows = False
        Me.dgvBoletas.AllowUserToDeleteRows = False
        Me.dgvBoletas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBoletas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.seleccion})
        Me.dgvBoletas.Location = New System.Drawing.Point(12, 12)
        Me.dgvBoletas.Name = "dgvBoletas"
        Me.dgvBoletas.ReadOnly = True
        Me.dgvBoletas.RowHeadersVisible = False
        Me.dgvBoletas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvBoletas.Size = New System.Drawing.Size(622, 250)
        Me.dgvBoletas.TabIndex = 0
        '
        'seleccion
        '
        Me.seleccion.HeaderText = "sel."
        Me.seleccion.Name = "seleccion"
        Me.seleccion.ReadOnly = True
        '
        'btnLiquidar
        '
        Me.btnLiquidar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLiquidar.Location = New System.Drawing.Point(12, 277)
        Me.btnLiquidar.Name = "btnLiquidar"
        Me.btnLiquidar.Size = New System.Drawing.Size(75, 31)
        Me.btnLiquidar.TabIndex = 9
        Me.btnLiquidar.Text = "Liquidar"
        Me.btnLiquidar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(559, 277)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 31)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmSemillasBoletasProductor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(646, 312)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnLiquidar)
        Me.Controls.Add(Me.dgvBoletas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSemillasBoletasProductor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Boletas del productor"
        CType(Me.dgvBoletas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvBoletas As System.Windows.Forms.DataGridView
    Friend WithEvents btnLiquidar As System.Windows.Forms.Button
    Friend WithEvents seleccion As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
