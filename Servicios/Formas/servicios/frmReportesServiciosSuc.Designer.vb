<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportesServiciosSuc
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
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.cmbSubClasificacion = New System.Windows.Forms.ComboBox()
        Me.cmbClasificacion = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbTecnico = New System.Windows.Forms.ComboBox()
        Me.dtpFecha2 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.cmbEquipo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstTipos = New System.Windows.Forms.ListBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(470, 187)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(77, 24)
        Me.btnCerrar.TabIndex = 305
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'cmbSubClasificacion
        '
        Me.cmbSubClasificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSubClasificacion.Enabled = False
        Me.cmbSubClasificacion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSubClasificacion.FormattingEnabled = True
        Me.cmbSubClasificacion.Location = New System.Drawing.Point(337, 154)
        Me.cmbSubClasificacion.Name = "cmbSubClasificacion"
        Me.cmbSubClasificacion.Size = New System.Drawing.Size(259, 24)
        Me.cmbSubClasificacion.TabIndex = 304
        '
        'cmbClasificacion
        '
        Me.cmbClasificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClasificacion.Enabled = False
        Me.cmbClasificacion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClasificacion.FormattingEnabled = True
        Me.cmbClasificacion.Location = New System.Drawing.Point(337, 126)
        Me.cmbClasificacion.Name = "cmbClasificacion"
        Me.cmbClasificacion.Size = New System.Drawing.Size(259, 24)
        Me.cmbClasificacion.TabIndex = 303
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(212, 156)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(116, 16)
        Me.Label9.TabIndex = 302
        Me.Label9.Text = "Subclasificacion:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(235, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(93, 16)
        Me.Label8.TabIndex = 301
        Me.Label8.Text = "Clasificacion:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(271, 102)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 16)
        Me.Label5.TabIndex = 300
        Me.Label5.Text = "Técnico:"
        '
        'cmbTecnico
        '
        Me.cmbTecnico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTecnico.Enabled = False
        Me.cmbTecnico.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTecnico.FormattingEnabled = True
        Me.cmbTecnico.Location = New System.Drawing.Point(337, 98)
        Me.cmbTecnico.Name = "cmbTecnico"
        Me.cmbTecnico.Size = New System.Drawing.Size(259, 24)
        Me.cmbTecnico.TabIndex = 299
        '
        'dtpFecha2
        '
        Me.dtpFecha2.Enabled = False
        Me.dtpFecha2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha2.Location = New System.Drawing.Point(453, 14)
        Me.dtpFecha2.Name = "dtpFecha2"
        Me.dtpFecha2.Size = New System.Drawing.Size(96, 22)
        Me.dtpFecha2.TabIndex = 289
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(282, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 297
        Me.Label3.Text = "Fecha:"
        '
        'dtpFecha
        '
        Me.dtpFecha.Enabled = False
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(337, 14)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(96, 22)
        Me.dtpFecha.TabIndex = 288
        '
        'btnImprimir
        '
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(387, 187)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(77, 24)
        Me.btnImprimir.TabIndex = 293
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'cmbEquipo
        '
        Me.cmbEquipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEquipo.Enabled = False
        Me.cmbEquipo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEquipo.FormattingEnabled = True
        Me.cmbEquipo.Location = New System.Drawing.Point(337, 70)
        Me.cmbEquipo.Name = "cmbEquipo"
        Me.cmbEquipo.Size = New System.Drawing.Size(259, 24)
        Me.cmbEquipo.TabIndex = 292
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(278, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 296
        Me.Label2.Text = "Equipo:"
        '
        'lstTipos
        '
        Me.lstTipos.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTipos.FormattingEnabled = True
        Me.lstTipos.ItemHeight = 18
        Me.lstTipos.Items.AddRange(New Object() {"Servicios", "Sucursales - Equipos", "Componentes de equipo", "Historial de equipos", "Técnicos"})
        Me.lstTipos.Location = New System.Drawing.Point(1, 0)
        Me.lstTipos.Name = "lstTipos"
        Me.lstTipos.Size = New System.Drawing.Size(200, 220)
        Me.lstTipos.TabIndex = 287
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(266, 45)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(67, 16)
        Me.Label22.TabIndex = 331
        Me.Label22.Text = "Sucursal:"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(337, 41)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(259, 24)
        Me.cmbSucursal.TabIndex = 330
        '
        'frmReportesServiciosSuc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(653, 221)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.cmbSucursal)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.cmbSubClasificacion)
        Me.Controls.Add(Me.cmbClasificacion)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbTecnico)
        Me.Controls.Add(Me.dtpFecha2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpFecha)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.cmbEquipo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lstTipos)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportesServiciosSuc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reportes - Servicios"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents cmbSubClasificacion As System.Windows.Forms.ComboBox
    Friend WithEvents cmbClasificacion As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbTecnico As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFecha2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents cmbEquipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstTipos As System.Windows.Forms.ListBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
End Class
