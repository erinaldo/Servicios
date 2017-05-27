<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestaurantePuntoVenta
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRestaurantePuntoVenta))
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtClave = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.btnEnter = New System.Windows.Forms.Button()
        Me.btnDecimal = New System.Windows.Forms.Button()
        Me.btn0 = New System.Windows.Forms.Button()
        Me.btnCajon = New System.Windows.Forms.Button()
        Me.btnPor = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btn3 = New System.Windows.Forms.Button()
        Me.btn2 = New System.Windows.Forms.Button()
        Me.btn1 = New System.Windows.Forms.Button()
        Me.btnCE = New System.Windows.Forms.Button()
        Me.btnMeseros = New System.Windows.Forms.Button()
        Me.btnMesas = New System.Windows.Forms.Button()
        Me.btn6 = New System.Windows.Forms.Button()
        Me.btn5 = New System.Windows.Forms.Button()
        Me.btn4 = New System.Windows.Forms.Button()
        Me.btnBorrar = New System.Windows.Forms.Button()
        Me.btn9 = New System.Windows.Forms.Button()
        Me.btn8 = New System.Windows.Forms.Button()
        Me.btn7 = New System.Windows.Forms.Button()
        Me.btnPedidos = New System.Windows.Forms.Button()
        Me.btnMenu = New System.Windows.Forms.Button()
        Me.btnLlevar = New System.Windows.Forms.Button()
        Me.btnReservaciones = New System.Windows.Forms.Button()
        Me.btnClientes = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.txtRecibido = New System.Windows.Forms.TextBox()
        Me.panelMetodos = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblNotificacion = New System.Windows.Forms.Label()
        Me.dgvMetodos = New System.Windows.Forms.DataGridView()
        Me.panelObjetos = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnVer = New System.Windows.Forms.Button()
        Me.panelPedidos = New System.Windows.Forms.Panel()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblMesero = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblMesa = New System.Windows.Forms.Label()
        Me.label = New System.Windows.Forms.Label()
        Me.lblCajero = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        CType(Me.dgvMetodos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelObjetos.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(98, 77)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(90, 72)
        Me.Button2.TabIndex = 2
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Button2, "Descuento")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 646)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Folio:"
        Me.Label2.Visible = False
        '
        'txtClave
        '
        Me.txtClave.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClave.Location = New System.Drawing.Point(71, 643)
        Me.txtClave.Name = "txtClave"
        Me.txtClave.Size = New System.Drawing.Size(124, 22)
        Me.txtClave.TabIndex = 6
        Me.txtClave.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(156, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Total:"
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(233, 111)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(181, 32)
        Me.lblTotal.TabIndex = 21
        Me.lblTotal.Text = "$999,999,999"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(3, 79)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(93, 72)
        Me.Button1.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.Button1, "Guardar (F2)")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.btnAgregar)
        Me.Panel1.Controls.Add(Me.btnQuitar)
        Me.Panel1.Controls.Add(Me.btnEnter)
        Me.Panel1.Controls.Add(Me.btnDecimal)
        Me.Panel1.Controls.Add(Me.btn0)
        Me.Panel1.Controls.Add(Me.btnCajon)
        Me.Panel1.Controls.Add(Me.btnPor)
        Me.Panel1.Controls.Add(Me.btnImprimir)
        Me.Panel1.Controls.Add(Me.btn3)
        Me.Panel1.Controls.Add(Me.btn2)
        Me.Panel1.Controls.Add(Me.btn1)
        Me.Panel1.Controls.Add(Me.btnCE)
        Me.Panel1.Controls.Add(Me.btnMeseros)
        Me.Panel1.Controls.Add(Me.btnMesas)
        Me.Panel1.Controls.Add(Me.btn6)
        Me.Panel1.Controls.Add(Me.btn5)
        Me.Panel1.Controls.Add(Me.btn4)
        Me.Panel1.Controls.Add(Me.btnBorrar)
        Me.Panel1.Controls.Add(Me.btn9)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btn8)
        Me.Panel1.Controls.Add(Me.btn7)
        Me.Panel1.Controls.Add(Me.btnPedidos)
        Me.Panel1.Controls.Add(Me.btnMenu)
        Me.Panel1.Controls.Add(Me.btnLlevar)
        Me.Panel1.Controls.Add(Me.btnReservaciones)
        Me.Panel1.Controls.Add(Me.btnClientes)
        Me.Panel1.Controls.Add(Me.btnNuevo)
        Me.Panel1.Location = New System.Drawing.Point(471, 78)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(476, 462)
        Me.Panel1.TabIndex = 23
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(4, 385)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(90, 72)
        Me.Button4.TabIndex = 26
        Me.Button4.Text = "Buscar Pedido"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.Color.White
        Me.btnAgregar.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.Location = New System.Drawing.Point(380, 307)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(90, 72)
        Me.btnAgregar.TabIndex = 25
        Me.btnAgregar.Text = "+"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'btnQuitar
        '
        Me.btnQuitar.BackColor = System.Drawing.Color.White
        Me.btnQuitar.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuitar.Location = New System.Drawing.Point(380, 383)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(90, 72)
        Me.btnQuitar.TabIndex = 24
        Me.btnQuitar.Text = "-"
        Me.btnQuitar.UseVisualStyleBackColor = False
        '
        'btnEnter
        '
        Me.btnEnter.BackColor = System.Drawing.Color.White
        Me.btnEnter.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnter.Location = New System.Drawing.Point(286, 383)
        Me.btnEnter.Name = "btnEnter"
        Me.btnEnter.Size = New System.Drawing.Size(90, 72)
        Me.btnEnter.TabIndex = 23
        Me.btnEnter.Text = "Enter"
        Me.btnEnter.UseVisualStyleBackColor = False
        '
        'btnDecimal
        '
        Me.btnDecimal.BackColor = System.Drawing.Color.White
        Me.btnDecimal.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDecimal.Location = New System.Drawing.Point(192, 383)
        Me.btnDecimal.Name = "btnDecimal"
        Me.btnDecimal.Size = New System.Drawing.Size(90, 72)
        Me.btnDecimal.TabIndex = 22
        Me.btnDecimal.Text = "."
        Me.btnDecimal.UseVisualStyleBackColor = False
        '
        'btn0
        '
        Me.btn0.BackColor = System.Drawing.Color.White
        Me.btn0.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn0.Location = New System.Drawing.Point(98, 383)
        Me.btn0.Name = "btn0"
        Me.btn0.Size = New System.Drawing.Size(90, 72)
        Me.btn0.TabIndex = 21
        Me.btn0.Text = "0"
        Me.btn0.UseVisualStyleBackColor = False
        '
        'btnCajon
        '
        Me.btnCajon.BackColor = System.Drawing.Color.White
        Me.btnCajon.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCajon.Location = New System.Drawing.Point(4, 307)
        Me.btnCajon.Name = "btnCajon"
        Me.btnCajon.Size = New System.Drawing.Size(90, 72)
        Me.btnCajon.TabIndex = 25
        Me.btnCajon.Text = "Buscar Venta"
        Me.btnCajon.UseVisualStyleBackColor = False
        '
        'btnPor
        '
        Me.btnPor.BackColor = System.Drawing.Color.White
        Me.btnPor.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPor.Location = New System.Drawing.Point(380, 231)
        Me.btnPor.Name = "btnPor"
        Me.btnPor.Size = New System.Drawing.Size(90, 72)
        Me.btnPor.TabIndex = 20
        Me.btnPor.Text = "x"
        Me.btnPor.UseVisualStyleBackColor = False
        '
        'btnImprimir
        '
        Me.btnImprimir.BackColor = System.Drawing.Color.White
        Me.btnImprimir.BackgroundImage = CType(resources.GetObject("btnImprimir.BackgroundImage"), System.Drawing.Image)
        Me.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnImprimir.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(4, 231)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(90, 72)
        Me.btnImprimir.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.btnImprimir, "Imprimir")
        Me.btnImprimir.UseVisualStyleBackColor = False
        '
        'btn3
        '
        Me.btn3.BackColor = System.Drawing.Color.White
        Me.btn3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn3.Location = New System.Drawing.Point(286, 307)
        Me.btn3.Name = "btn3"
        Me.btn3.Size = New System.Drawing.Size(90, 72)
        Me.btn3.TabIndex = 18
        Me.btn3.Text = "3"
        Me.btn3.UseVisualStyleBackColor = False
        '
        'btn2
        '
        Me.btn2.BackColor = System.Drawing.Color.White
        Me.btn2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn2.Location = New System.Drawing.Point(192, 307)
        Me.btn2.Name = "btn2"
        Me.btn2.Size = New System.Drawing.Size(90, 72)
        Me.btn2.TabIndex = 17
        Me.btn2.Text = "2"
        Me.btn2.UseVisualStyleBackColor = False
        '
        'btn1
        '
        Me.btn1.BackColor = System.Drawing.Color.White
        Me.btn1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn1.Location = New System.Drawing.Point(98, 307)
        Me.btn1.Name = "btn1"
        Me.btn1.Size = New System.Drawing.Size(90, 72)
        Me.btn1.TabIndex = 16
        Me.btn1.Text = "1"
        Me.btn1.UseVisualStyleBackColor = False
        '
        'btnCE
        '
        Me.btnCE.BackColor = System.Drawing.Color.White
        Me.btnCE.Enabled = False
        Me.btnCE.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCE.Location = New System.Drawing.Point(4, 155)
        Me.btnCE.Name = "btnCE"
        Me.btnCE.Size = New System.Drawing.Size(90, 72)
        Me.btnCE.TabIndex = 15
        Me.btnCE.Text = "Imprimir cocina"
        Me.btnCE.UseVisualStyleBackColor = False
        '
        'btnMeseros
        '
        Me.btnMeseros.BackColor = System.Drawing.Color.White
        Me.btnMeseros.BackgroundImage = CType(resources.GetObject("btnMeseros.BackgroundImage"), System.Drawing.Image)
        Me.btnMeseros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMeseros.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMeseros.Location = New System.Drawing.Point(380, 79)
        Me.btnMeseros.Name = "btnMeseros"
        Me.btnMeseros.Size = New System.Drawing.Size(90, 72)
        Me.btnMeseros.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.btnMeseros, "Meseros")
        Me.btnMeseros.UseVisualStyleBackColor = False
        '
        'btnMesas
        '
        Me.btnMesas.BackColor = System.Drawing.Color.White
        Me.btnMesas.BackgroundImage = CType(resources.GetObject("btnMesas.BackgroundImage"), System.Drawing.Image)
        Me.btnMesas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMesas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMesas.Location = New System.Drawing.Point(286, 79)
        Me.btnMesas.Name = "btnMesas"
        Me.btnMesas.Size = New System.Drawing.Size(90, 72)
        Me.btnMesas.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.btnMesas, "Mesas")
        Me.btnMesas.UseVisualStyleBackColor = False
        '
        'btn6
        '
        Me.btn6.BackColor = System.Drawing.Color.White
        Me.btn6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn6.Location = New System.Drawing.Point(286, 231)
        Me.btn6.Name = "btn6"
        Me.btn6.Size = New System.Drawing.Size(90, 72)
        Me.btn6.TabIndex = 13
        Me.btn6.Text = "6"
        Me.btn6.UseVisualStyleBackColor = False
        '
        'btn5
        '
        Me.btn5.BackColor = System.Drawing.Color.White
        Me.btn5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn5.Location = New System.Drawing.Point(192, 231)
        Me.btn5.Name = "btn5"
        Me.btn5.Size = New System.Drawing.Size(90, 72)
        Me.btn5.TabIndex = 12
        Me.btn5.Text = "5"
        Me.btn5.UseVisualStyleBackColor = False
        '
        'btn4
        '
        Me.btn4.BackColor = System.Drawing.Color.White
        Me.btn4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn4.Location = New System.Drawing.Point(98, 231)
        Me.btn4.Name = "btn4"
        Me.btn4.Size = New System.Drawing.Size(90, 72)
        Me.btn4.TabIndex = 11
        Me.btn4.Text = "4"
        Me.btn4.UseVisualStyleBackColor = False
        '
        'btnBorrar
        '
        Me.btnBorrar.BackColor = System.Drawing.Color.White
        Me.btnBorrar.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBorrar.Location = New System.Drawing.Point(380, 155)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = New System.Drawing.Size(90, 72)
        Me.btnBorrar.TabIndex = 10
        Me.btnBorrar.Text = "Borrar"
        Me.btnBorrar.UseVisualStyleBackColor = False
        '
        'btn9
        '
        Me.btn9.BackColor = System.Drawing.Color.White
        Me.btn9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn9.Location = New System.Drawing.Point(286, 155)
        Me.btn9.Name = "btn9"
        Me.btn9.Size = New System.Drawing.Size(90, 72)
        Me.btn9.TabIndex = 8
        Me.btn9.Text = "9"
        Me.btn9.UseVisualStyleBackColor = False
        '
        'btn8
        '
        Me.btn8.BackColor = System.Drawing.Color.White
        Me.btn8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn8.Location = New System.Drawing.Point(192, 155)
        Me.btn8.Name = "btn8"
        Me.btn8.Size = New System.Drawing.Size(90, 72)
        Me.btn8.TabIndex = 7
        Me.btn8.Text = "8"
        Me.btn8.UseVisualStyleBackColor = False
        '
        'btn7
        '
        Me.btn7.BackColor = System.Drawing.Color.White
        Me.btn7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn7.Location = New System.Drawing.Point(98, 155)
        Me.btn7.Name = "btn7"
        Me.btn7.Size = New System.Drawing.Size(90, 72)
        Me.btn7.TabIndex = 6
        Me.btn7.Text = "7"
        Me.btn7.UseVisualStyleBackColor = False
        '
        'btnPedidos
        '
        Me.btnPedidos.BackColor = System.Drawing.Color.White
        Me.btnPedidos.BackgroundImage = CType(resources.GetObject("btnPedidos.BackgroundImage"), System.Drawing.Image)
        Me.btnPedidos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnPedidos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPedidos.Location = New System.Drawing.Point(190, 79)
        Me.btnPedidos.Name = "btnPedidos"
        Me.btnPedidos.Size = New System.Drawing.Size(90, 72)
        Me.btnPedidos.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnPedidos, "Pedidos")
        Me.btnPedidos.UseVisualStyleBackColor = False
        '
        'btnMenu
        '
        Me.btnMenu.BackColor = System.Drawing.Color.White
        Me.btnMenu.BackgroundImage = CType(resources.GetObject("btnMenu.BackgroundImage"), System.Drawing.Image)
        Me.btnMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMenu.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMenu.Location = New System.Drawing.Point(380, 3)
        Me.btnMenu.Name = "btnMenu"
        Me.btnMenu.Size = New System.Drawing.Size(90, 72)
        Me.btnMenu.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnMenu, "Menú")
        Me.btnMenu.UseVisualStyleBackColor = False
        '
        'btnLlevar
        '
        Me.btnLlevar.BackColor = System.Drawing.Color.White
        Me.btnLlevar.BackgroundImage = CType(resources.GetObject("btnLlevar.BackgroundImage"), System.Drawing.Image)
        Me.btnLlevar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnLlevar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLlevar.Location = New System.Drawing.Point(286, 3)
        Me.btnLlevar.Name = "btnLlevar"
        Me.btnLlevar.Size = New System.Drawing.Size(90, 72)
        Me.btnLlevar.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btnLlevar, "Orden Para Llevar")
        Me.btnLlevar.UseVisualStyleBackColor = False
        '
        'btnReservaciones
        '
        Me.btnReservaciones.BackColor = System.Drawing.Color.White
        Me.btnReservaciones.BackgroundImage = CType(resources.GetObject("btnReservaciones.BackgroundImage"), System.Drawing.Image)
        Me.btnReservaciones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnReservaciones.CausesValidation = False
        Me.btnReservaciones.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReservaciones.Location = New System.Drawing.Point(192, 3)
        Me.btnReservaciones.Name = "btnReservaciones"
        Me.btnReservaciones.Size = New System.Drawing.Size(90, 72)
        Me.btnReservaciones.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnReservaciones, "Reservas")
        Me.btnReservaciones.UseVisualStyleBackColor = False
        '
        'btnClientes
        '
        Me.btnClientes.BackColor = System.Drawing.Color.White
        Me.btnClientes.BackgroundImage = CType(resources.GetObject("btnClientes.BackgroundImage"), System.Drawing.Image)
        Me.btnClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClientes.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClientes.Location = New System.Drawing.Point(98, 3)
        Me.btnClientes.Name = "btnClientes"
        Me.btnClientes.Size = New System.Drawing.Size(90, 72)
        Me.btnClientes.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnClientes, "Clientes")
        Me.btnClientes.UseVisualStyleBackColor = False
        '
        'btnNuevo
        '
        Me.btnNuevo.BackColor = System.Drawing.Color.White
        Me.btnNuevo.BackgroundImage = CType(resources.GetObject("btnNuevo.BackgroundImage"), System.Drawing.Image)
        Me.btnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(4, 3)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(90, 72)
        Me.btnNuevo.TabIndex = 0
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnNuevo, "Nueva Venta")
        Me.btnNuevo.UseVisualStyleBackColor = False
        '
        'txtRecibido
        '
        Me.txtRecibido.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecibido.Location = New System.Drawing.Point(242, 160)
        Me.txtRecibido.Name = "txtRecibido"
        Me.txtRecibido.Size = New System.Drawing.Size(161, 39)
        Me.txtRecibido.TabIndex = 27
        Me.txtRecibido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'panelMetodos
        '
        Me.panelMetodos.AutoScroll = True
        Me.panelMetodos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelMetodos.Location = New System.Drawing.Point(6, 71)
        Me.panelMetodos.Name = "panelMetodos"
        Me.panelMetodos.Size = New System.Drawing.Size(138, 321)
        Me.panelMetodos.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 18)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Métodos de pago:"
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.Color.White
        Me.btnBuscar.BackgroundImage = CType(resources.GetObject("btnBuscar.BackgroundImage"), System.Drawing.Image)
        Me.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBuscar.Location = New System.Drawing.Point(10, 557)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(69, 68)
        Me.btnBuscar.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.btnBuscar, "Buscar")
        Me.btnBuscar.UseVisualStyleBackColor = False
        Me.btnBuscar.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(156, 257)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 19)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Cambio:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(156, 169)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 19)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Efectivo:"
        '
        'lblNotificacion
        '
        Me.lblNotificacion.AutoSize = True
        Me.lblNotificacion.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotificacion.Location = New System.Drawing.Point(158, 217)
        Me.lblNotificacion.Name = "lblNotificacion"
        Me.lblNotificacion.Size = New System.Drawing.Size(60, 19)
        Me.lblNotificacion.TabIndex = 34
        Me.lblNotificacion.Text = "Label6"
        Me.lblNotificacion.Visible = False
        '
        'dgvMetodos
        '
        Me.dgvMetodos.AllowUserToAddRows = False
        Me.dgvMetodos.AllowUserToDeleteRows = False
        Me.dgvMetodos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMetodos.Location = New System.Drawing.Point(6, 399)
        Me.dgvMetodos.Name = "dgvMetodos"
        Me.dgvMetodos.ReadOnly = True
        Me.dgvMetodos.RowHeadersVisible = False
        Me.dgvMetodos.Size = New System.Drawing.Size(138, 131)
        Me.dgvMetodos.TabIndex = 35
        '
        'panelObjetos
        '
        Me.panelObjetos.BackColor = System.Drawing.Color.Lavender
        Me.panelObjetos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panelObjetos.Controls.Add(Me.Label8)
        Me.panelObjetos.Controls.Add(Me.btnVer)
        Me.panelObjetos.Controls.Add(Me.panelPedidos)
        Me.panelObjetos.Controls.Add(Me.lblDescuento)
        Me.panelObjetos.Controls.Add(Me.Label7)
        Me.panelObjetos.Controls.Add(Me.lblMesero)
        Me.panelObjetos.Controls.Add(Me.Label11)
        Me.panelObjetos.Controls.Add(Me.lblMesa)
        Me.panelObjetos.Controls.Add(Me.label)
        Me.panelObjetos.Controls.Add(Me.lblCajero)
        Me.panelObjetos.Controls.Add(Me.Label6)
        Me.panelObjetos.Controls.Add(Me.panelMetodos)
        Me.panelObjetos.Controls.Add(Me.btnBuscar)
        Me.panelObjetos.Controls.Add(Me.dgvMetodos)
        Me.panelObjetos.Controls.Add(Me.Label3)
        Me.panelObjetos.Controls.Add(Me.Label2)
        Me.panelObjetos.Controls.Add(Me.txtClave)
        Me.panelObjetos.Controls.Add(Me.lblNotificacion)
        Me.panelObjetos.Controls.Add(Me.lblTotal)
        Me.panelObjetos.Controls.Add(Me.Panel1)
        Me.panelObjetos.Controls.Add(Me.Label5)
        Me.panelObjetos.Controls.Add(Me.Label1)
        Me.panelObjetos.Controls.Add(Me.Label4)
        Me.panelObjetos.Controls.Add(Me.txtRecibido)
        Me.panelObjetos.Location = New System.Drawing.Point(2, 2)
        Me.panelObjetos.Name = "panelObjetos"
        Me.panelObjetos.Size = New System.Drawing.Size(950, 676)
        Me.panelObjetos.TabIndex = 36
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(236, 248)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(181, 32)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "$999,999,999"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnVer
        '
        Me.btnVer.BackColor = System.Drawing.Color.White
        Me.btnVer.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVer.Location = New System.Drawing.Point(135, 555)
        Me.btnVer.Name = "btnVer"
        Me.btnVer.Size = New System.Drawing.Size(90, 72)
        Me.btnVer.TabIndex = 27
        Me.btnVer.Text = "Ver pedido"
        Me.btnVer.UseVisualStyleBackColor = False
        '
        'panelPedidos
        '
        Me.panelPedidos.AutoScroll = True
        Me.panelPedidos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelPedidos.Location = New System.Drawing.Point(228, 545)
        Me.panelPedidos.Name = "panelPedidos"
        Me.panelPedidos.Size = New System.Drawing.Size(719, 120)
        Me.panelPedidos.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.panelPedidos, "Pedidos para llevar")
        '
        'lblDescuento
        '
        Me.lblDescuento.AutoSize = True
        Me.lblDescuento.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescuento.Location = New System.Drawing.Point(253, 75)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(40, 19)
        Me.lblDescuento.TabIndex = 43
        Me.lblDescuento.Text = "0.00"
        Me.lblDescuento.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(152, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 19)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Descuento:"
        '
        'lblMesero
        '
        Me.lblMesero.AutoSize = True
        Me.lblMesero.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMesero.Location = New System.Drawing.Point(279, 9)
        Me.lblMesero.Name = "lblMesero"
        Me.lblMesero.Size = New System.Drawing.Size(118, 18)
        Me.lblMesero.TabIndex = 41
        Me.lblMesero.Text = "nombre mesero"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(189, 6)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 22)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "Mesero:"
        '
        'lblMesa
        '
        Me.lblMesa.AutoSize = True
        Me.lblMesa.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMesa.Location = New System.Drawing.Point(79, 8)
        Me.lblMesa.Name = "lblMesa"
        Me.lblMesa.Size = New System.Drawing.Size(95, 18)
        Me.lblMesa.TabIndex = 39
        Me.lblMesa.Text = "nmesa Num."
        '
        'label
        '
        Me.label.AutoSize = True
        Me.label.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label.Location = New System.Drawing.Point(9, 5)
        Me.label.Name = "label"
        Me.label.Size = New System.Drawing.Size(67, 22)
        Me.label.TabIndex = 38
        Me.label.Text = "Mesa:"
        '
        'lblCajero
        '
        Me.lblCajero.AutoSize = True
        Me.lblCajero.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCajero.Location = New System.Drawing.Point(606, 10)
        Me.lblCajero.Name = "lblCajero"
        Me.lblCajero.Size = New System.Drawing.Size(108, 18)
        Me.lblCajero.TabIndex = 37
        Me.lblCajero.Text = "nombre cajero"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(527, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 22)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "Cajero:"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'frmRestaurantePuntoVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.LavenderBlush
        Me.ClientSize = New System.Drawing.Size(954, 679)
        Me.Controls.Add(Me.panelObjetos)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(970, 590)
        Me.Name = "frmRestaurantePuntoVenta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pago de cuenta"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvMetodos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelObjetos.ResumeLayout(False)
        Me.panelObjetos.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents btnQuitar As System.Windows.Forms.Button
    Friend WithEvents btnEnter As System.Windows.Forms.Button
    Friend WithEvents btnDecimal As System.Windows.Forms.Button
    Friend WithEvents btn0 As System.Windows.Forms.Button
    Friend WithEvents btnPor As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents btnCE As System.Windows.Forms.Button
    Friend WithEvents btnMeseros As System.Windows.Forms.Button
    Friend WithEvents btn6 As System.Windows.Forms.Button
    Friend WithEvents btn5 As System.Windows.Forms.Button
    Friend WithEvents btn4 As System.Windows.Forms.Button
    Friend WithEvents btnBorrar As System.Windows.Forms.Button
    Friend WithEvents btnMesas As System.Windows.Forms.Button
    Friend WithEvents btn9 As System.Windows.Forms.Button
    Friend WithEvents btn8 As System.Windows.Forms.Button
    Friend WithEvents btn7 As System.Windows.Forms.Button
    Friend WithEvents btnPedidos As System.Windows.Forms.Button
    Friend WithEvents btnMenu As System.Windows.Forms.Button
    Friend WithEvents btnLlevar As System.Windows.Forms.Button
    Friend WithEvents btnReservaciones As System.Windows.Forms.Button
    Friend WithEvents btnClientes As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnCajon As System.Windows.Forms.Button
    Friend WithEvents txtRecibido As System.Windows.Forms.TextBox
    Friend WithEvents panelMetodos As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblNotificacion As System.Windows.Forms.Label
    Friend WithEvents dgvMetodos As System.Windows.Forms.DataGridView
    Friend WithEvents panelObjetos As System.Windows.Forms.Panel
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents lblMesero As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblMesa As System.Windows.Forms.Label
    Friend WithEvents label As System.Windows.Forms.Label
    Friend WithEvents lblCajero As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents panelPedidos As System.Windows.Forms.Panel
    Friend WithEvents btnVer As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
