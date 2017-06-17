<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestaurantePrincipal
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRestaurantePrincipal))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPedidos = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.btnEntrar = New System.Windows.Forms.Button()
        Me.PanelObjetos = New System.Windows.Forms.Panel()
        Me.rdbOrdenar = New System.Windows.Forms.RadioButton()
        Me.rdbCambiarMesa = New System.Windows.Forms.RadioButton()
        Me.rdbReservar = New System.Windows.Forms.RadioButton()
        Me.pnlSecciones = New System.Windows.Forms.FlowLayoutPanel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabMapa = New System.Windows.Forms.TabPage()
        Me.pnlMesasFijo = New System.Windows.Forms.Panel()
        Me.tabCuadricula = New System.Windows.Forms.TabPage()
        Me.pnlMesasFlujo = New System.Windows.Forms.FlowLayoutPanel()
        Me.tabPedidos = New System.Windows.Forms.TabPage()
        Me.pnlPedidos = New System.Windows.Forms.FlowLayoutPanel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.rdbPagar = New System.Windows.Forms.RadioButton()
        Me.PanelObjetos.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabMapa.SuspendLayout()
        Me.tabCuadricula.SuspendLayout()
        Me.tabPedidos.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Sección:"
        '
        'btnPedidos
        '
        Me.btnPedidos.BackColor = System.Drawing.Color.White
        Me.btnPedidos.BackgroundImage = CType(resources.GetObject("btnPedidos.BackgroundImage"), System.Drawing.Image)
        Me.btnPedidos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnPedidos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPedidos.Location = New System.Drawing.Point(887, 110)
        Me.btnPedidos.Name = "btnPedidos"
        Me.btnPedidos.Size = New System.Drawing.Size(108, 68)
        Me.btnPedidos.TabIndex = 5
        Me.btnPedidos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnPedidos, "Agregar pedido")
        Me.btnPedidos.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(712, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Mesero:"
        '
        'txtPass
        '
        Me.txtPass.Enabled = False
        Me.txtPass.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPass.Location = New System.Drawing.Point(776, 8)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(214, 22)
        Me.txtPass.TabIndex = 11
        '
        'btnEntrar
        '
        Me.btnEntrar.BackColor = System.Drawing.Color.White
        Me.btnEntrar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEntrar.Location = New System.Drawing.Point(819, 36)
        Me.btnEntrar.Name = "btnEntrar"
        Me.btnEntrar.Size = New System.Drawing.Size(125, 46)
        Me.btnEntrar.TabIndex = 12
        Me.btnEntrar.Text = "Entrar"
        Me.btnEntrar.UseVisualStyleBackColor = False
        Me.btnEntrar.Visible = False
        '
        'PanelObjetos
        '
        Me.PanelObjetos.Controls.Add(Me.rdbPagar)
        Me.PanelObjetos.Controls.Add(Me.rdbOrdenar)
        Me.PanelObjetos.Controls.Add(Me.rdbCambiarMesa)
        Me.PanelObjetos.Controls.Add(Me.rdbReservar)
        Me.PanelObjetos.Controls.Add(Me.btnPedidos)
        Me.PanelObjetos.Controls.Add(Me.pnlSecciones)
        Me.PanelObjetos.Controls.Add(Me.Label1)
        Me.PanelObjetos.Controls.Add(Me.btnEntrar)
        Me.PanelObjetos.Controls.Add(Me.txtPass)
        Me.PanelObjetos.Controls.Add(Me.Label3)
        Me.PanelObjetos.Controls.Add(Me.TabControl1)
        Me.PanelObjetos.Location = New System.Drawing.Point(1, 2)
        Me.PanelObjetos.MinimumSize = New System.Drawing.Size(995, 528)
        Me.PanelObjetos.Name = "PanelObjetos"
        Me.PanelObjetos.Size = New System.Drawing.Size(1004, 578)
        Me.PanelObjetos.TabIndex = 16
        '
        'rdbOrdenar
        '
        Me.rdbOrdenar.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdbOrdenar.Checked = True
        Me.rdbOrdenar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbOrdenar.Location = New System.Drawing.Point(887, 184)
        Me.rdbOrdenar.Name = "rdbOrdenar"
        Me.rdbOrdenar.Size = New System.Drawing.Size(108, 68)
        Me.rdbOrdenar.TabIndex = 21
        Me.rdbOrdenar.TabStop = True
        Me.rdbOrdenar.Text = "Ordenar"
        Me.rdbOrdenar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.rdbOrdenar, "Vender")
        Me.rdbOrdenar.UseVisualStyleBackColor = True
        '
        'rdbCambiarMesa
        '
        Me.rdbCambiarMesa.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdbCambiarMesa.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbCambiarMesa.Location = New System.Drawing.Point(887, 406)
        Me.rdbCambiarMesa.Name = "rdbCambiarMesa"
        Me.rdbCambiarMesa.Size = New System.Drawing.Size(108, 68)
        Me.rdbCambiarMesa.TabIndex = 20
        Me.rdbCambiarMesa.Text = "Cambiar de mesa"
        Me.rdbCambiarMesa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.rdbCambiarMesa, "Cambiar de mesa")
        Me.rdbCambiarMesa.UseVisualStyleBackColor = True
        '
        'rdbReservar
        '
        Me.rdbReservar.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdbReservar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbReservar.Location = New System.Drawing.Point(887, 332)
        Me.rdbReservar.Name = "rdbReservar"
        Me.rdbReservar.Size = New System.Drawing.Size(108, 68)
        Me.rdbReservar.TabIndex = 19
        Me.rdbReservar.Text = "Reservar"
        Me.rdbReservar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.rdbReservar, "Reservar")
        Me.rdbReservar.UseVisualStyleBackColor = True
        '
        'pnlSecciones
        '
        Me.pnlSecciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSecciones.Location = New System.Drawing.Point(90, 10)
        Me.pnlSecciones.Name = "pnlSecciones"
        Me.pnlSecciones.Size = New System.Drawing.Size(616, 72)
        Me.pnlSecciones.TabIndex = 18
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabMapa)
        Me.TabControl1.Controls.Add(Me.tabCuadricula)
        Me.TabControl1.Controls.Add(Me.tabPedidos)
        Me.TabControl1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.ItemSize = New System.Drawing.Size(103, 30)
        Me.TabControl1.Location = New System.Drawing.Point(11, 85)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(870, 483)
        Me.TabControl1.TabIndex = 17
        '
        'tabMapa
        '
        Me.tabMapa.Controls.Add(Me.pnlMesasFijo)
        Me.tabMapa.Location = New System.Drawing.Point(4, 34)
        Me.tabMapa.Name = "tabMapa"
        Me.tabMapa.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMapa.Size = New System.Drawing.Size(862, 445)
        Me.tabMapa.TabIndex = 0
        Me.tabMapa.Text = "Vista de mapa"
        Me.tabMapa.UseVisualStyleBackColor = True
        '
        'pnlMesasFijo
        '
        Me.pnlMesasFijo.AutoScroll = True
        Me.pnlMesasFijo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMesasFijo.Location = New System.Drawing.Point(3, 3)
        Me.pnlMesasFijo.Name = "pnlMesasFijo"
        Me.pnlMesasFijo.Size = New System.Drawing.Size(856, 439)
        Me.pnlMesasFijo.TabIndex = 0
        '
        'tabCuadricula
        '
        Me.tabCuadricula.Controls.Add(Me.pnlMesasFlujo)
        Me.tabCuadricula.Location = New System.Drawing.Point(4, 34)
        Me.tabCuadricula.Name = "tabCuadricula"
        Me.tabCuadricula.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCuadricula.Size = New System.Drawing.Size(862, 445)
        Me.tabCuadricula.TabIndex = 1
        Me.tabCuadricula.Text = "Vista de cuadricula"
        Me.tabCuadricula.UseVisualStyleBackColor = True
        '
        'pnlMesasFlujo
        '
        Me.pnlMesasFlujo.AutoScroll = True
        Me.pnlMesasFlujo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMesasFlujo.Location = New System.Drawing.Point(3, 3)
        Me.pnlMesasFlujo.Name = "pnlMesasFlujo"
        Me.pnlMesasFlujo.Size = New System.Drawing.Size(856, 439)
        Me.pnlMesasFlujo.TabIndex = 0
        '
        'tabPedidos
        '
        Me.tabPedidos.Controls.Add(Me.pnlPedidos)
        Me.tabPedidos.Location = New System.Drawing.Point(4, 34)
        Me.tabPedidos.Name = "tabPedidos"
        Me.tabPedidos.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPedidos.Size = New System.Drawing.Size(862, 445)
        Me.tabPedidos.TabIndex = 2
        Me.tabPedidos.Text = "Pedidos registrados"
        Me.tabPedidos.UseVisualStyleBackColor = True
        '
        'pnlPedidos
        '
        Me.pnlPedidos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPedidos.Location = New System.Drawing.Point(3, 3)
        Me.pnlPedidos.Name = "pnlPedidos"
        Me.pnlPedidos.Size = New System.Drawing.Size(856, 439)
        Me.pnlPedidos.TabIndex = 0
        '
        'rdbPagar
        '
        Me.rdbPagar.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdbPagar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbPagar.Location = New System.Drawing.Point(887, 258)
        Me.rdbPagar.Name = "rdbPagar"
        Me.rdbPagar.Size = New System.Drawing.Size(108, 68)
        Me.rdbPagar.TabIndex = 22
        Me.rdbPagar.Text = "Pagar"
        Me.rdbPagar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.rdbPagar, "Vender")
        Me.rdbPagar.UseVisualStyleBackColor = True
        '
        'frmRestaurantePrincipal
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(1008, 582)
        Me.Controls.Add(Me.PanelObjetos)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1024, 568)
        Me.Name = "frmRestaurantePrincipal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PanelObjetos.ResumeLayout(False)
        Me.PanelObjetos.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tabMapa.ResumeLayout(False)
        Me.tabCuadricula.ResumeLayout(False)
        Me.tabPedidos.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnPedidos As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents btnEntrar As System.Windows.Forms.Button
    Friend WithEvents PanelObjetos As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabMapa As System.Windows.Forms.TabPage
    Friend WithEvents pnlMesasFijo As System.Windows.Forms.Panel
    Friend WithEvents tabCuadricula As System.Windows.Forms.TabPage
    Friend WithEvents pnlMesasFlujo As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlSecciones As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents rdbCambiarMesa As System.Windows.Forms.RadioButton
    Friend WithEvents rdbReservar As System.Windows.Forms.RadioButton
    Friend WithEvents rdbOrdenar As System.Windows.Forms.RadioButton
    Friend WithEvents tabPedidos As System.Windows.Forms.TabPage
    Friend WithEvents pnlPedidos As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents rdbPagar As System.Windows.Forms.RadioButton
End Class
