<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContabilidadXML
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
        Me.lstTipo = New System.Windows.Forms.ListBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlTipoSolicitud = New System.Windows.Forms.Panel()
        Me.txtNumTramite = New System.Windows.Forms.TextBox()
        Me.txtNumOrden = New System.Windows.Forms.TextBox()
        Me.lblNumTramite = New System.Windows.Forms.Label()
        Me.lblNumOrden = New System.Windows.Forms.Label()
        Me.cmbTipoSolicitud = New System.Windows.Forms.ComboBox()
        Me.pnlBalanza = New System.Windows.Forms.Panel()
        Me.dtpFechaModificacion = New System.Windows.Forms.DateTimePicker()
        Me.lblFechaBalanza = New System.Windows.Forms.Label()
        Me.cmbTipoEnvio = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnVerificar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.pnlTipoSolicitud.SuspendLayout()
        Me.pnlBalanza.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstTipo
        '
        Me.lstTipo.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstTipo.FormattingEnabled = True
        Me.lstTipo.ItemHeight = 18
        Me.lstTipo.Items.AddRange(New Object() {"Catalogo", "Balanza", "Polizas", "Auxiliar de Folios", "Auxiliar de Cuentas"})
        Me.lstTipo.Location = New System.Drawing.Point(1, 0)
        Me.lstTipo.Name = "lstTipo"
        Me.lstTipo.Size = New System.Drawing.Size(169, 238)
        Me.lstTipo.TabIndex = 291
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(405, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 16)
        Me.Label8.TabIndex = 295
        Me.Label8.Text = "Hasta:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(255, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 16)
        Me.Label7.TabIndex = 294
        Me.Label7.Text = "Desde:"
        '
        'dtpDesde
        '
        Me.dtpDesde.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesde.Location = New System.Drawing.Point(309, 5)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(86, 22)
        Me.dtpDesde.TabIndex = 292
        '
        'dtpHasta
        '
        Me.dtpHasta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(453, 5)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(85, 22)
        Me.dtpHasta.TabIndex = 293
        '
        'btnImprimir
        '
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(394, 193)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(128, 33)
        Me.btnImprimir.TabIndex = 296
        Me.btnImprimir.Text = "Generar"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(18, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 16)
        Me.Label1.TabIndex = 297
        Me.Label1.Text = "Tipo de solicitud:"
        '
        'pnlTipoSolicitud
        '
        Me.pnlTipoSolicitud.Controls.Add(Me.txtNumTramite)
        Me.pnlTipoSolicitud.Controls.Add(Me.txtNumOrden)
        Me.pnlTipoSolicitud.Controls.Add(Me.lblNumTramite)
        Me.pnlTipoSolicitud.Controls.Add(Me.lblNumOrden)
        Me.pnlTipoSolicitud.Controls.Add(Me.cmbTipoSolicitud)
        Me.pnlTipoSolicitud.Controls.Add(Me.Label1)
        Me.pnlTipoSolicitud.Enabled = False
        Me.pnlTipoSolicitud.Location = New System.Drawing.Point(187, 32)
        Me.pnlTipoSolicitud.Name = "pnlTipoSolicitud"
        Me.pnlTipoSolicitud.Size = New System.Drawing.Size(429, 85)
        Me.pnlTipoSolicitud.TabIndex = 298
        '
        'txtNumTramite
        '
        Me.txtNumTramite.Enabled = False
        Me.txtNumTramite.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumTramite.Location = New System.Drawing.Point(135, 62)
        Me.txtNumTramite.MaxLength = 10
        Me.txtNumTramite.Name = "txtNumTramite"
        Me.txtNumTramite.Size = New System.Drawing.Size(146, 22)
        Me.txtNumTramite.TabIndex = 302
        '
        'txtNumOrden
        '
        Me.txtNumOrden.Enabled = False
        Me.txtNumOrden.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumOrden.Location = New System.Drawing.Point(135, 34)
        Me.txtNumOrden.MaxLength = 13
        Me.txtNumOrden.Name = "txtNumOrden"
        Me.txtNumOrden.Size = New System.Drawing.Size(146, 22)
        Me.txtNumOrden.TabIndex = 301
        '
        'lblNumTramite
        '
        Me.lblNumTramite.AutoSize = True
        Me.lblNumTramite.BackColor = System.Drawing.Color.Transparent
        Me.lblNumTramite.Enabled = False
        Me.lblNumTramite.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumTramite.ForeColor = System.Drawing.Color.Black
        Me.lblNumTramite.Location = New System.Drawing.Point(3, 63)
        Me.lblNumTramite.Name = "lblNumTramite"
        Me.lblNumTramite.Size = New System.Drawing.Size(131, 16)
        Me.lblNumTramite.TabIndex = 300
        Me.lblNumTramite.Text = "Número de trámite:"
        '
        'lblNumOrden
        '
        Me.lblNumOrden.AutoSize = True
        Me.lblNumOrden.BackColor = System.Drawing.Color.Transparent
        Me.lblNumOrden.Enabled = False
        Me.lblNumOrden.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumOrden.ForeColor = System.Drawing.Color.Black
        Me.lblNumOrden.Location = New System.Drawing.Point(12, 35)
        Me.lblNumOrden.Name = "lblNumOrden"
        Me.lblNumOrden.Size = New System.Drawing.Size(123, 16)
        Me.lblNumOrden.TabIndex = 299
        Me.lblNumOrden.Text = "Número de orden:"
        '
        'cmbTipoSolicitud
        '
        Me.cmbTipoSolicitud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoSolicitud.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoSolicitud.FormattingEnabled = True
        Me.cmbTipoSolicitud.Items.AddRange(New Object() {"AF - ACTO DE FISCALIZACIÓN", "FC - FISCALIZACIÓN COMPULSA", "DE - DEVOLUCIÓN", "CO - COMPENSACIÓN"})
        Me.cmbTipoSolicitud.Location = New System.Drawing.Point(135, 5)
        Me.cmbTipoSolicitud.Name = "cmbTipoSolicitud"
        Me.cmbTipoSolicitud.Size = New System.Drawing.Size(244, 24)
        Me.cmbTipoSolicitud.TabIndex = 298
        '
        'pnlBalanza
        '
        Me.pnlBalanza.Controls.Add(Me.CheckBox1)
        Me.pnlBalanza.Controls.Add(Me.dtpFechaModificacion)
        Me.pnlBalanza.Controls.Add(Me.lblFechaBalanza)
        Me.pnlBalanza.Controls.Add(Me.cmbTipoEnvio)
        Me.pnlBalanza.Controls.Add(Me.Label4)
        Me.pnlBalanza.Enabled = False
        Me.pnlBalanza.Location = New System.Drawing.Point(189, 119)
        Me.pnlBalanza.Name = "pnlBalanza"
        Me.pnlBalanza.Size = New System.Drawing.Size(427, 61)
        Me.pnlBalanza.TabIndex = 299
        '
        'dtpFechaModificacion
        '
        Me.dtpFechaModificacion.Enabled = False
        Me.dtpFechaModificacion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaModificacion.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaModificacion.Location = New System.Drawing.Point(140, 34)
        Me.dtpFechaModificacion.Name = "dtpFechaModificacion"
        Me.dtpFechaModificacion.Size = New System.Drawing.Size(85, 22)
        Me.dtpFechaModificacion.TabIndex = 300
        '
        'lblFechaBalanza
        '
        Me.lblFechaBalanza.AutoSize = True
        Me.lblFechaBalanza.BackColor = System.Drawing.Color.Transparent
        Me.lblFechaBalanza.Enabled = False
        Me.lblFechaBalanza.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFechaBalanza.ForeColor = System.Drawing.Color.Black
        Me.lblFechaBalanza.Location = New System.Drawing.Point(2, 36)
        Me.lblFechaBalanza.Name = "lblFechaBalanza"
        Me.lblFechaBalanza.Size = New System.Drawing.Size(137, 16)
        Me.lblFechaBalanza.TabIndex = 299
        Me.lblFechaBalanza.Text = "Fecha modificación:"
        '
        'cmbTipoEnvio
        '
        Me.cmbTipoEnvio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoEnvio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoEnvio.FormattingEnabled = True
        Me.cmbTipoEnvio.Items.AddRange(New Object() {"N - NORMAL", "C - COMPLEMENTARIA"})
        Me.cmbTipoEnvio.Location = New System.Drawing.Point(137, 5)
        Me.cmbTipoEnvio.Name = "cmbTipoEnvio"
        Me.cmbTipoEnvio.Size = New System.Drawing.Size(244, 24)
        Me.cmbTipoEnvio.TabIndex = 298
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(36, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 16)
        Me.Label4.TabIndex = 297
        Me.Label4.Text = "Tipo de envío:"
        '
        'btnVerificar
        '
        Me.btnVerificar.Enabled = False
        Me.btnVerificar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVerificar.Location = New System.Drawing.Point(250, 193)
        Me.btnVerificar.Name = "btnVerificar"
        Me.btnVerificar.Size = New System.Drawing.Size(128, 33)
        Me.btnVerificar.TabIndex = 300
        Me.btnVerificar.Text = "Verificar cuentas"
        Me.btnVerificar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(565, 202)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(70, 31)
        Me.Button1.TabIndex = 301
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(304, 37)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(97, 20)
        Me.CheckBox1.TabIndex = 303
        Me.CheckBox1.Text = "Balanza 13"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'frmContabilidadXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button1
        Me.ClientSize = New System.Drawing.Size(645, 238)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnVerificar)
        Me.Controls.Add(Me.pnlBalanza)
        Me.Controls.Add(Me.pnlTipoSolicitud)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.dtpHasta)
        Me.Controls.Add(Me.lstTipo)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmContabilidadXML"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "XML"
        Me.pnlTipoSolicitud.ResumeLayout(False)
        Me.pnlTipoSolicitud.PerformLayout()
        Me.pnlBalanza.ResumeLayout(False)
        Me.pnlBalanza.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstTipo As System.Windows.Forms.ListBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlTipoSolicitud As System.Windows.Forms.Panel
    Friend WithEvents txtNumTramite As System.Windows.Forms.TextBox
    Friend WithEvents txtNumOrden As System.Windows.Forms.TextBox
    Friend WithEvents lblNumTramite As System.Windows.Forms.Label
    Friend WithEvents lblNumOrden As System.Windows.Forms.Label
    Friend WithEvents cmbTipoSolicitud As System.Windows.Forms.ComboBox
    Friend WithEvents pnlBalanza As System.Windows.Forms.Panel
    Friend WithEvents dtpFechaModificacion As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFechaBalanza As System.Windows.Forms.Label
    Friend WithEvents cmbTipoEnvio As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnVerificar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
