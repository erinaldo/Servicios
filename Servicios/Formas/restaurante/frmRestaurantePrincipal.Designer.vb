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
        Me.panelMesas = New System.Windows.Forms.Panel()
        Me.btnReservar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnLlevar = New System.Windows.Forms.Button()
        Me.btnMeseros = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.panelSecciones = New System.Windows.Forms.Panel()
        Me.PanelObjetos = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PanelObjetos.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelMesas
        '
        Me.panelMesas.AutoScroll = True
        Me.panelMesas.BackColor = System.Drawing.Color.Silver
        Me.panelMesas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panelMesas.Location = New System.Drawing.Point(3, 88)
        Me.panelMesas.Name = "panelMesas"
        Me.panelMesas.Size = New System.Drawing.Size(998, 487)
        Me.panelMesas.TabIndex = 1
        '
        'btnReservar
        '
        Me.btnReservar.BackColor = System.Drawing.Color.White
        Me.btnReservar.BackgroundImage = CType(resources.GetObject("btnReservar.BackgroundImage"), System.Drawing.Image)
        Me.btnReservar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnReservar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReservar.Location = New System.Drawing.Point(3, 3)
        Me.btnReservar.Name = "btnReservar"
        Me.btnReservar.Size = New System.Drawing.Size(108, 68)
        Me.btnReservar.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnReservar, "Reservas")
        Me.btnReservar.UseVisualStyleBackColor = False
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
        'btnLlevar
        '
        Me.btnLlevar.BackColor = System.Drawing.Color.White
        Me.btnLlevar.BackgroundImage = CType(resources.GetObject("btnLlevar.BackgroundImage"), System.Drawing.Image)
        Me.btnLlevar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnLlevar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLlevar.Location = New System.Drawing.Point(3, 77)
        Me.btnLlevar.Name = "btnLlevar"
        Me.btnLlevar.Size = New System.Drawing.Size(108, 64)
        Me.btnLlevar.TabIndex = 5
        Me.btnLlevar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.btnLlevar, "Pedidos para Llevar")
        Me.btnLlevar.UseVisualStyleBackColor = False
        '
        'btnMeseros
        '
        Me.btnMeseros.BackColor = System.Drawing.Color.White
        Me.btnMeseros.BackgroundImage = CType(resources.GetObject("btnMeseros.BackgroundImage"), System.Drawing.Image)
        Me.btnMeseros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMeseros.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMeseros.Location = New System.Drawing.Point(3, 147)
        Me.btnMeseros.Name = "btnMeseros"
        Me.btnMeseros.Size = New System.Drawing.Size(108, 66)
        Me.btnMeseros.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btnMeseros, "Meseros")
        Me.btnMeseros.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button2.Location = New System.Drawing.Point(3, 219)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(108, 67)
        Me.Button2.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.Button2, "Comandas")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(485, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Mesas"
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
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(819, 36)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(125, 46)
        Me.Button3.TabIndex = 12
        Me.Button3.Text = "Entrar"
        Me.Button3.UseVisualStyleBackColor = False
        Me.Button3.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(488, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 45)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Vista de mapa"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(577, 10)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(89, 45)
        Me.Button4.TabIndex = 14
        Me.Button4.Text = "Vista en cuadrícula"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'panelSecciones
        '
        Me.panelSecciones.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelSecciones.Location = New System.Drawing.Point(86, 10)
        Me.panelSecciones.Name = "panelSecciones"
        Me.panelSecciones.Size = New System.Drawing.Size(396, 72)
        Me.panelSecciones.TabIndex = 15
        '
        'PanelObjetos
        '
        Me.PanelObjetos.Controls.Add(Me.Panel1)
        Me.PanelObjetos.Controls.Add(Me.Label1)
        Me.PanelObjetos.Controls.Add(Me.Button3)
        Me.PanelObjetos.Controls.Add(Me.Button1)
        Me.PanelObjetos.Controls.Add(Me.txtPass)
        Me.PanelObjetos.Controls.Add(Me.Button4)
        Me.PanelObjetos.Controls.Add(Me.Label3)
        Me.PanelObjetos.Controls.Add(Me.panelSecciones)
        Me.PanelObjetos.Controls.Add(Me.Label2)
        Me.PanelObjetos.Controls.Add(Me.panelMesas)
        Me.PanelObjetos.Location = New System.Drawing.Point(1, 2)
        Me.PanelObjetos.MinimumSize = New System.Drawing.Size(995, 528)
        Me.PanelObjetos.Name = "PanelObjetos"
        Me.PanelObjetos.Size = New System.Drawing.Size(1004, 578)
        Me.PanelObjetos.TabIndex = 16
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnReservar)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.btnLlevar)
        Me.Panel1.Controls.Add(Me.btnMeseros)
        Me.Panel1.Location = New System.Drawing.Point(872, 145)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(118, 290)
        Me.Panel1.TabIndex = 16
        Me.Panel1.Visible = False
        '
        'Timer1
        '
        '
        'Timer2
        '
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
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelMesas As System.Windows.Forms.Panel
    Friend WithEvents btnReservar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnLlevar As System.Windows.Forms.Button
    Friend WithEvents btnMeseros As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPass As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents panelSecciones As System.Windows.Forms.Panel
    Friend WithEvents PanelObjetos As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
