<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestauranteAcomodarMesas
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
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.comboSucursal = New System.Windows.Forms.ComboBox()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlMesasFijo = New Servicios.PanelDoubleBuffer()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.nudLeft = New System.Windows.Forms.NumericUpDown()
        Me.nudTop = New System.Windows.Forms.NumericUpDown()
        Me.nudHeight = New System.Windows.Forms.NumericUpDown()
        Me.nudWidth = New System.Windows.Forms.NumericUpDown()
        Me.nudCapacidad = New System.Windows.Forms.NumericUpDown()
        Me.nudNumero = New System.Windows.Forms.NumericUpDown()
        Me.pnlSecciones = New System.Windows.Forms.FlowLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.nudLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCapacidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudNumero, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.Color.White
        Me.btnAgregar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.Location = New System.Drawing.Point(646, 75)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(131, 47)
        Me.btnAgregar.TabIndex = 1
        Me.btnAgregar.Text = "Nueva Mesa"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(532, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Número:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(301, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Sección:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Sucursal:"
        '
        'comboSucursal
        '
        Me.comboSucursal.DisplayMember = "nombre"
        Me.comboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboSucursal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboSucursal.FormattingEnabled = True
        Me.comboSucursal.Location = New System.Drawing.Point(84, 17)
        Me.comboSucursal.Name = "comboSucursal"
        Me.comboSucursal.Size = New System.Drawing.Size(207, 24)
        Me.comboSucursal.TabIndex = 7
        Me.comboSucursal.ValueMember = "idsucursal"
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.Color.White
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(725, 496)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(146, 43)
        Me.btnGuardar.TabIndex = 9
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(231, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Alto:"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(324, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 16)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Ancho:"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(410, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 16)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Capacidad:"
        '
        'pnlMesasFijo
        '
        Me.pnlMesasFijo.AutoScroll = True
        Me.pnlMesasFijo.BackColor = System.Drawing.Color.White
        Me.pnlMesasFijo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMesasFijo.Location = New System.Drawing.Point(14, 128)
        Me.pnlMesasFijo.Name = "pnlMesasFijo"
        Me.pnlMesasFijo.Size = New System.Drawing.Size(857, 362)
        Me.pnlMesasFijo.TabIndex = 8
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(14, 496)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(124, 43)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Eliminar Mesa"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.nudLeft, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.nudTop, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.nudHeight, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.nudWidth, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.nudCapacidad, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.nudNumero, 5, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(16, 75)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(624, 47)
        Me.TableLayoutPanel1.TabIndex = 18
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(119, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 16)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Coord. Y"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(18, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 16)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Coord. X"
        '
        'nudLeft
        '
        Me.nudLeft.Location = New System.Drawing.Point(3, 26)
        Me.nudLeft.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nudLeft.Name = "nudLeft"
        Me.nudLeft.Size = New System.Drawing.Size(94, 21)
        Me.nudLeft.TabIndex = 21
        '
        'nudTop
        '
        Me.nudTop.Location = New System.Drawing.Point(103, 26)
        Me.nudTop.Maximum = New Decimal(New Integer() {9999, 0, 0, 0})
        Me.nudTop.Name = "nudTop"
        Me.nudTop.Size = New System.Drawing.Size(94, 21)
        Me.nudTop.TabIndex = 22
        '
        'nudHeight
        '
        Me.nudHeight.Location = New System.Drawing.Point(203, 26)
        Me.nudHeight.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nudHeight.Name = "nudHeight"
        Me.nudHeight.Size = New System.Drawing.Size(94, 21)
        Me.nudHeight.TabIndex = 23
        '
        'nudWidth
        '
        Me.nudWidth.Location = New System.Drawing.Point(303, 26)
        Me.nudWidth.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.nudWidth.Name = "nudWidth"
        Me.nudWidth.Size = New System.Drawing.Size(94, 21)
        Me.nudWidth.TabIndex = 24
        '
        'nudCapacidad
        '
        Me.nudCapacidad.Location = New System.Drawing.Point(403, 26)
        Me.nudCapacidad.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nudCapacidad.Name = "nudCapacidad"
        Me.nudCapacidad.Size = New System.Drawing.Size(94, 21)
        Me.nudCapacidad.TabIndex = 25
        '
        'nudNumero
        '
        Me.nudNumero.Location = New System.Drawing.Point(503, 26)
        Me.nudNumero.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nudNumero.Name = "nudNumero"
        Me.nudNumero.Size = New System.Drawing.Size(120, 21)
        Me.nudNumero.TabIndex = 26
        '
        'pnlSecciones
        '
        Me.pnlSecciones.Location = New System.Drawing.Point(370, 17)
        Me.pnlSecciones.Name = "pnlSecciones"
        Me.pnlSecciones.Size = New System.Drawing.Size(474, 52)
        Me.pnlSecciones.TabIndex = 19
        '
        'frmRestauranteAcomodarMesas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(885, 545)
        Me.Controls.Add(Me.pnlSecciones)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.pnlMesasFijo)
        Me.Controls.Add(Me.comboSucursal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnAgregar)
        Me.Font = New System.Drawing.Font("Arial", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmRestauranteAcomodarMesas"
        Me.Text = "Acomodo de mesas."
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.nudLeft,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudTop,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudHeight,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudWidth,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudCapacidad,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.nudNumero,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents comboSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents pnlMesasFijo As Servicios.PanelDoubleBuffer
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents nudLeft As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudTop As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudCapacidad As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudNumero As System.Windows.Forms.NumericUpDown
    Friend WithEvents pnlSecciones As System.Windows.Forms.FlowLayoutPanel
End Class
