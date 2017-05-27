<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEquiposHistorial
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DGEventos = New System.Windows.Forms.DataGridView
        Me.cmbEvento = New System.Windows.Forms.ComboBox
        Me.lblEventos = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.DGInventario = New System.Windows.Forms.DataGridView
        Me.btnCerrar = New System.Windows.Forms.Button
        CType(Me.DGEventos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGEventos
        '
        Me.DGEventos.AllowUserToAddRows = False
        Me.DGEventos.AllowUserToDeleteRows = False
        Me.DGEventos.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        Me.DGEventos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGEventos.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGEventos.Location = New System.Drawing.Point(12, 77)
        Me.DGEventos.Name = "DGEventos"
        Me.DGEventos.ReadOnly = True
        Me.DGEventos.RowHeadersVisible = False
        Me.DGEventos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGEventos.Size = New System.Drawing.Size(800, 170)
        Me.DGEventos.TabIndex = 17
        '
        'cmbEvento
        '
        Me.cmbEvento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEvento.FormattingEnabled = True
        Me.cmbEvento.Location = New System.Drawing.Point(69, 22)
        Me.cmbEvento.Name = "cmbEvento"
        Me.cmbEvento.Size = New System.Drawing.Size(121, 21)
        Me.cmbEvento.TabIndex = 18
        '
        'lblEventos
        '
        Me.lblEventos.AutoSize = True
        Me.lblEventos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEventos.Location = New System.Drawing.Point(12, 25)
        Me.lblEventos.Name = "lblEventos"
        Me.lblEventos.Size = New System.Drawing.Size(51, 13)
        Me.lblEventos.TabIndex = 19
        Me.lblEventos.Text = "Evento:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Eventos:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 260)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Componentes:"
        '
        'DGInventario
        '
        Me.DGInventario.AllowUserToAddRows = False
        Me.DGInventario.AllowUserToDeleteRows = False
        Me.DGInventario.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.DGInventario.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGInventario.DefaultCellStyle = DataGridViewCellStyle4
        Me.DGInventario.Location = New System.Drawing.Point(12, 278)
        Me.DGInventario.Name = "DGInventario"
        Me.DGInventario.ReadOnly = True
        Me.DGInventario.RowHeadersVisible = False
        Me.DGInventario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGInventario.Size = New System.Drawing.Size(556, 170)
        Me.DGInventario.TabIndex = 21
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(377, 470)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(70, 22)
        Me.btnCerrar.TabIndex = 23
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'frmEquiposHistorial
        '
        Me.AcceptButton = Me.btnCerrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(837, 504)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DGInventario)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblEventos)
        Me.Controls.Add(Me.cmbEvento)
        Me.Controls.Add(Me.DGEventos)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEquiposHistorial"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Historial de Equipos"
        CType(Me.DGEventos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGInventario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGEventos As System.Windows.Forms.DataGridView
    Friend WithEvents cmbEvento As System.Windows.Forms.ComboBox
    Friend WithEvents lblEventos As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DGInventario As System.Windows.Forms.DataGridView
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
End Class
