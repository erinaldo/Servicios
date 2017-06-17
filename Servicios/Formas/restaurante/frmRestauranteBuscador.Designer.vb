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
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnComidaCorrida = New System.Windows.Forms.Button()
        Me.lstAgregados = New System.Windows.Forms.ListBox()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.panelCategorias = New System.Windows.Forms.FlowLayoutPanel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.panelPlatillos = New System.Windows.Forms.FlowLayoutPanel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCerrar.BackColor = System.Drawing.Color.White
        Me.btnCerrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(496, 68)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(119, 59)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "Terminar de añadir"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'btnComidaCorrida
        '
        Me.btnComidaCorrida.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnComidaCorrida.BackColor = System.Drawing.Color.White
        Me.btnComidaCorrida.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnComidaCorrida.Location = New System.Drawing.Point(241, 3)
        Me.btnComidaCorrida.Name = "btnComidaCorrida"
        Me.btnComidaCorrida.Size = New System.Drawing.Size(182, 78)
        Me.btnComidaCorrida.TabIndex = 6
        Me.btnComidaCorrida.Text = "COMIDA CORRIDA"
        Me.btnComidaCorrida.UseVisualStyleBackColor = False
        '
        'lstAgregados
        '
        Me.lstAgregados.DisplayMember = "descripcion"
        Me.lstAgregados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstAgregados.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstAgregados.FormattingEnabled = True
        Me.lstAgregados.ItemHeight = 15
        Me.lstAgregados.Location = New System.Drawing.Point(0, 0)
        Me.lstAgregados.Margin = New System.Windows.Forms.Padding(6)
        Me.lstAgregados.Name = "lstAgregados"
        Me.lstAgregados.Size = New System.Drawing.Size(200, 171)
        Me.lstAgregados.TabIndex = 7
        Me.lstAgregados.ValueMember = "iddetalle"
        '
        'btnEliminar
        '
        Me.btnEliminar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEliminar.BackColor = System.Drawing.Color.White
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminar.Location = New System.Drawing.Point(3, 68)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(127, 59)
        Me.btnEliminar.TabIndex = 8
        Me.btnEliminar.Text = "Eliminar platillo"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(822, 461)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.TabIndex = 9
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.panelCategorias)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.lstAgregados)
        Me.SplitContainer3.Size = New System.Drawing.Size(200, 461)
        Me.SplitContainer3.SplitterDistance = 286
        Me.SplitContainer3.TabIndex = 8
        '
        'panelCategorias
        '
        Me.panelCategorias.AutoScroll = True
        Me.panelCategorias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelCategorias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelCategorias.Location = New System.Drawing.Point(0, 0)
        Me.panelCategorias.Name = "panelCategorias"
        Me.panelCategorias.Size = New System.Drawing.Size(200, 286)
        Me.panelCategorias.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.panelPlatillos)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnEliminar)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnCerrar)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnComidaCorrida)
        Me.SplitContainer2.Size = New System.Drawing.Size(618, 461)
        Me.SplitContainer2.SplitterDistance = 327
        Me.SplitContainer2.TabIndex = 0
        '
        'panelPlatillos
        '
        Me.panelPlatillos.AutoScroll = True
        Me.panelPlatillos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelPlatillos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelPlatillos.Location = New System.Drawing.Point(0, 0)
        Me.panelPlatillos.Name = "panelPlatillos"
        Me.panelPlatillos.Size = New System.Drawing.Size(618, 327)
        Me.panelPlatillos.TabIndex = 0
        '
        'frmRestauranteBuscador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(822, 461)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimizeBox = False
        Me.Name = "frmRestauranteBuscador"
        Me.Text = "Menú"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnComidaCorrida As System.Windows.Forms.Button
    Friend WithEvents lstAgregados As System.Windows.Forms.ListBox
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents panelPlatillos As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents panelCategorias As System.Windows.Forms.FlowLayoutPanel
End Class
