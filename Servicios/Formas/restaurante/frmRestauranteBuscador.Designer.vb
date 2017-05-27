<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestauranteBuscador
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
        Me.panelPlatillos = New System.Windows.Forms.Panel()
        Me.panelCategorias = New System.Windows.Forms.Panel()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnAtras = New System.Windows.Forms.Button()
        Me.btnSiguiente = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtAgregados = New System.Windows.Forms.ListBox()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'panelPlatillos
        '
        Me.panelPlatillos.AutoScroll = True
        Me.panelPlatillos.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.panelPlatillos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelPlatillos.Location = New System.Drawing.Point(196, 12)
        Me.panelPlatillos.Name = "panelPlatillos"
        Me.panelPlatillos.Size = New System.Drawing.Size(614, 524)
        Me.panelPlatillos.TabIndex = 0
        '
        'panelCategorias
        '
        Me.panelCategorias.AutoScroll = True
        Me.panelCategorias.Location = New System.Drawing.Point(6, 12)
        Me.panelCategorias.Name = "panelCategorias"
        Me.panelCategorias.Size = New System.Drawing.Size(184, 445)
        Me.panelCategorias.TabIndex = 1
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.Color.White
        Me.btnCerrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(712, 542)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(99, 59)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "Terminar de añadir"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'btnAtras
        '
        Me.btnAtras.BackColor = System.Drawing.Color.White
        Me.btnAtras.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAtras.Location = New System.Drawing.Point(196, 543)
        Me.btnAtras.Name = "btnAtras"
        Me.btnAtras.Size = New System.Drawing.Size(75, 58)
        Me.btnAtras.TabIndex = 4
        Me.btnAtras.Text = "<<"
        Me.btnAtras.UseVisualStyleBackColor = False
        Me.btnAtras.Visible = False
        '
        'btnSiguiente
        '
        Me.btnSiguiente.BackColor = System.Drawing.Color.White
        Me.btnSiguiente.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSiguiente.Location = New System.Drawing.Point(277, 543)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.Size = New System.Drawing.Size(75, 58)
        Me.btnSiguiente.TabIndex = 5
        Me.btnSiguiente.Text = ">>"
        Me.btnSiguiente.UseVisualStyleBackColor = False
        Me.btnSiguiente.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(398, 542)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(182, 78)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "COMIDA CORRIDA"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'txtAgregados
        '
        Me.txtAgregados.FormattingEnabled = True
        Me.txtAgregados.Location = New System.Drawing.Point(6, 463)
        Me.txtAgregados.Name = "txtAgregados"
        Me.txtAgregados.Size = New System.Drawing.Size(184, 199)
        Me.txtAgregados.TabIndex = 7
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.Color.White
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(196, 604)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(117, 58)
        Me.btnEliminar.TabIndex = 8
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        Me.btnEliminar.Visible = False
        '
        'frmRestauranteBuscador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(822, 670)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.txtAgregados)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSiguiente)
        Me.Controls.Add(Me.btnAtras)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.panelCategorias)
        Me.Controls.Add(Me.panelPlatillos)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRestauranteBuscador"
        Me.Text = "Menú"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelPlatillos As System.Windows.Forms.Panel
    Friend WithEvents panelCategorias As System.Windows.Forms.Panel
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnAtras As System.Windows.Forms.Button
    Friend WithEvents btnSiguiente As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtAgregados As System.Windows.Forms.ListBox
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
End Class
