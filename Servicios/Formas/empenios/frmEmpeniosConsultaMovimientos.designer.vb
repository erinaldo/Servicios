<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpeniosConsultaMovimientos
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.txtClienteDatos = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.txtClienteCodigo = New System.Windows.Forms.TextBox()
        Me.btnVerMovimientos = New System.Windows.Forms.Button()
        Me.chkTiempoReal = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
        Me.dtpFechaInicio = New System.Windows.Forms.DateTimePicker()
        Me.DGServicios = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.DGServicios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button4
        '
        Me.Button4.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(539, 477)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(94, 25)
        Me.Button4.TabIndex = 196
        Me.Button4.Text = "&Imprimir"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'txtClienteDatos
        '
        Me.txtClienteDatos.BackColor = System.Drawing.Color.White
        Me.txtClienteDatos.Enabled = False
        Me.txtClienteDatos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClienteDatos.Location = New System.Drawing.Point(175, 60)
        Me.txtClienteDatos.Name = "txtClienteDatos"
        Me.txtClienteDatos.Size = New System.Drawing.Size(363, 22)
        Me.txtClienteDatos.TabIndex = 192
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(6, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 19)
        Me.Label1.TabIndex = 201
        Me.Label1.Text = "Cliente:"
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCliente.Location = New System.Drawing.Point(74, 85)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(97, 22)
        Me.btnBuscarCliente.TabIndex = 191
        Me.btnBuscarCliente.Text = "Buscar &Cliente"
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'txtClienteCodigo
        '
        Me.txtClienteCodigo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClienteCodigo.Location = New System.Drawing.Point(77, 60)
        Me.txtClienteCodigo.Name = "txtClienteCodigo"
        Me.txtClienteCodigo.Size = New System.Drawing.Size(93, 22)
        Me.txtClienteCodigo.TabIndex = 190
        '
        'btnVerMovimientos
        '
        Me.btnVerMovimientos.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.btnVerMovimientos.Location = New System.Drawing.Point(544, 58)
        Me.btnVerMovimientos.Name = "btnVerMovimientos"
        Me.btnVerMovimientos.Size = New System.Drawing.Size(141, 28)
        Me.btnVerMovimientos.TabIndex = 193
        Me.btnVerMovimientos.Text = "Ver Movimientos"
        Me.btnVerMovimientos.UseVisualStyleBackColor = True
        '
        'chkTiempoReal
        '
        Me.chkTiempoReal.AutoSize = True
        Me.chkTiempoReal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTiempoReal.Location = New System.Drawing.Point(571, 32)
        Me.chkTiempoReal.Name = "chkTiempoReal"
        Me.chkTiempoReal.Size = New System.Drawing.Size(81, 18)
        Me.chkTiempoReal.TabIndex = 198
        Me.chkTiempoReal.Text = "Tiempo real"
        Me.chkTiempoReal.UseVisualStyleBackColor = True
        Me.chkTiempoReal.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(4, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(180, 17)
        Me.Label2.TabIndex = 200
        Me.Label2.Text = "Resultados de la consulta:"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(639, 477)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 25)
        Me.Button2.TabIndex = 197
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(239, 476)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 27)
        Me.Button1.TabIndex = 195
        Me.Button1.Text = "Ver documento"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(12, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 19)
        Me.Label3.TabIndex = 199
        Me.Label3.Text = "Fecha:"
        '
        'dtpFechaFinal
        '
        Me.dtpFechaFinal.CustomFormat = "yyyy/MM/dd"
        Me.dtpFechaFinal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFechaFinal.Location = New System.Drawing.Point(252, 29)
        Me.dtpFechaFinal.Name = "dtpFechaFinal"
        Me.dtpFechaFinal.Size = New System.Drawing.Size(107, 25)
        Me.dtpFechaFinal.TabIndex = 189
        '
        'dtpFechaInicio
        '
        Me.dtpFechaInicio.CustomFormat = "yyyy/MM/dd"
        Me.dtpFechaInicio.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFechaInicio.Location = New System.Drawing.Point(77, 29)
        Me.dtpFechaInicio.Name = "dtpFechaInicio"
        Me.dtpFechaInicio.Size = New System.Drawing.Size(109, 25)
        Me.dtpFechaInicio.TabIndex = 188
        '
        'DGServicios
        '
        Me.DGServicios.AllowUserToAddRows = False
        Me.DGServicios.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        Me.DGServicios.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGServicios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGServicios.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGServicios.Location = New System.Drawing.Point(5, 143)
        Me.DGServicios.Name = "DGServicios"
        Me.DGServicios.ReadOnly = True
        Me.DGServicios.RowHeadersVisible = False
        Me.DGServicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGServicios.Size = New System.Drawing.Size(720, 326)
        Me.DGServicios.TabIndex = 194
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(192, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 19)
        Me.Label4.TabIndex = 202
        Me.Label4.Text = "hasta:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnBuscarCliente)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpFechaInicio)
        Me.GroupBox1.Controls.Add(Me.dtpFechaFinal)
        Me.GroupBox1.Controls.Add(Me.txtClienteDatos)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkTiempoReal)
        Me.GroupBox1.Controls.Add(Me.btnVerMovimientos)
        Me.GroupBox1.Controls.Add(Me.txtClienteCodigo)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(5, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(720, 118)
        Me.GroupBox1.TabIndex = 203
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filtro de búsqueda"
        '
        'frmEmpeniosConsultaMovimientos
        '
        Me.AcceptButton = Me.btnVerMovimientos
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(731, 514)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DGServicios)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpeniosConsultaMovimientos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta Movimientos"
        CType(Me.DGServicios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents txtClienteDatos As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents txtClienteCodigo As System.Windows.Forms.TextBox
    Friend WithEvents btnVerMovimientos As System.Windows.Forms.Button
    Friend WithEvents chkTiempoReal As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFechaInicio As System.Windows.Forms.DateTimePicker
    Friend WithEvents DGServicios As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
