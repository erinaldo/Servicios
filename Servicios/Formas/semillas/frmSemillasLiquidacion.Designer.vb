<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSemillasLiquidacion
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
        Me.panelProductor = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtSerie = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtProductor = New System.Windows.Forms.TextBox()
        Me.btnLiquidacion = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.btnBoletas = New System.Windows.Forms.Button()
        Me.panelBoletas = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnVer = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtTotal = New System.Windows.Forms.Label()
        Me.txtFacturas = New System.Windows.Forms.Label()
        Me.txtAnticipos = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTotalBoletas = New System.Windows.Forms.Label()
        Me.txtPesoBruto = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPesoFinal = New System.Windows.Forms.TextBox()
        Me.dgvFacturas = New System.Windows.Forms.DataGridView()
        Me.btnAnticipo = New System.Windows.Forms.Button()
        Me.txtImporte = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMedio = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnFactura = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dgvAnticipos = New System.Windows.Forms.DataGridView()
        Me.dgvLiqudadas = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblDomicilio = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblRFC = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblCosecha = New System.Windows.Forms.Label()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.panelProductor.SuspendLayout()
        Me.panelBoletas.SuspendLayout()
        CType(Me.dgvFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvAnticipos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvLiqudadas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelProductor
        '
        Me.panelProductor.Controls.Add(Me.Label20)
        Me.panelProductor.Controls.Add(Me.Label18)
        Me.panelProductor.Controls.Add(Me.btnBuscar)
        Me.panelProductor.Controls.Add(Me.ComboBox3)
        Me.panelProductor.Controls.Add(Me.Label16)
        Me.panelProductor.Controls.Add(Me.txtSerie)
        Me.panelProductor.Controls.Add(Me.Label17)
        Me.panelProductor.Controls.Add(Me.txtProductor)
        Me.panelProductor.Controls.Add(Me.btnLiquidacion)
        Me.panelProductor.Controls.Add(Me.DateTimePicker1)
        Me.panelProductor.Controls.Add(Me.Label2)
        Me.panelProductor.Controls.Add(Me.Label1)
        Me.panelProductor.Controls.Add(Me.txtFolio)
        Me.panelProductor.Controls.Add(Me.btnBoletas)
        Me.panelProductor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelProductor.Location = New System.Drawing.Point(9, 5)
        Me.panelProductor.Name = "panelProductor"
        Me.panelProductor.Size = New System.Drawing.Size(902, 127)
        Me.panelProductor.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(715, 87)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(51, 16)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "Fecha:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(373, 62)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 16)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "Sucursal:"
        '
        'btnBuscar
        '
        Me.btnBuscar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.Location = New System.Drawing.Point(699, 13)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(175, 42)
        Me.btnBuscar.TabIndex = 21
        Me.btnBuscar.Text = "Buscar liquidaciones"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Location = New System.Drawing.Point(250, 82)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(254, 24)
        Me.ComboBox3.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(112, 2)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(218, 16)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "Elija un productor para empezar:"
        '
        'txtSerie
        '
        Me.txtSerie.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerie.Location = New System.Drawing.Point(558, 84)
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(52, 22)
        Me.txtSerie.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(510, 86)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(46, 16)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "Serie:"
        '
        'txtProductor
        '
        Me.txtProductor.Enabled = False
        Me.txtProductor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductor.Location = New System.Drawing.Point(79, 24)
        Me.txtProductor.Name = "txtProductor"
        Me.txtProductor.Size = New System.Drawing.Size(308, 22)
        Me.txtProductor.TabIndex = 6
        '
        'btnLiquidacion
        '
        Me.btnLiquidacion.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLiquidacion.Location = New System.Drawing.Point(393, 21)
        Me.btnLiquidacion.Name = "btnLiquidacion"
        Me.btnLiquidacion.Size = New System.Drawing.Size(44, 28)
        Me.btnLiquidacion.TabIndex = 0
        Me.btnLiquidacion.Text = "..."
        Me.btnLiquidacion.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "yyyy/MMM/dd"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(772, 84)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(114, 22)
        Me.DateTimePicker1.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(616, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Folio:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(5, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Productor:"
        '
        'txtFolio
        '
        Me.txtFolio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolio.Location = New System.Drawing.Point(662, 84)
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.Size = New System.Drawing.Size(47, 22)
        Me.txtFolio.TabIndex = 3
        '
        'btnBoletas
        '
        Me.btnBoletas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBoletas.Location = New System.Drawing.Point(79, 53)
        Me.btnBoletas.Name = "btnBoletas"
        Me.btnBoletas.Size = New System.Drawing.Size(169, 23)
        Me.btnBoletas.TabIndex = 1
        Me.btnBoletas.Text = "Ver boletas pendientes"
        Me.btnBoletas.UseVisualStyleBackColor = True
        '
        'panelBoletas
        '
        Me.panelBoletas.Controls.Add(Me.Button2)
        Me.panelBoletas.Controls.Add(Me.txtPrecio)
        Me.panelBoletas.Controls.Add(Me.Label21)
        Me.panelBoletas.Controls.Add(Me.btnVer)
        Me.panelBoletas.Controls.Add(Me.Label19)
        Me.panelBoletas.Controls.Add(Me.Button1)
        Me.panelBoletas.Controls.Add(Me.btnNuevo)
        Me.panelBoletas.Controls.Add(Me.btnImprimir)
        Me.panelBoletas.Controls.Add(Me.btnGuardar)
        Me.panelBoletas.Controls.Add(Me.txtTotal)
        Me.panelBoletas.Controls.Add(Me.txtFacturas)
        Me.panelBoletas.Controls.Add(Me.txtAnticipos)
        Me.panelBoletas.Controls.Add(Me.Label7)
        Me.panelBoletas.Controls.Add(Me.txtTotalBoletas)
        Me.panelBoletas.Controls.Add(Me.txtPesoBruto)
        Me.panelBoletas.Controls.Add(Me.Label15)
        Me.panelBoletas.Controls.Add(Me.Label6)
        Me.panelBoletas.Controls.Add(Me.Label5)
        Me.panelBoletas.Controls.Add(Me.txtPesoFinal)
        Me.panelBoletas.Controls.Add(Me.dgvFacturas)
        Me.panelBoletas.Controls.Add(Me.btnAnticipo)
        Me.panelBoletas.Controls.Add(Me.txtImporte)
        Me.panelBoletas.Controls.Add(Me.Label14)
        Me.panelBoletas.Controls.Add(Me.txtMedio)
        Me.panelBoletas.Controls.Add(Me.Label13)
        Me.panelBoletas.Controls.Add(Me.Label12)
        Me.panelBoletas.Controls.Add(Me.btnFactura)
        Me.panelBoletas.Controls.Add(Me.Label11)
        Me.panelBoletas.Controls.Add(Me.Label10)
        Me.panelBoletas.Controls.Add(Me.dgvAnticipos)
        Me.panelBoletas.Controls.Add(Me.dgvLiqudadas)
        Me.panelBoletas.Controls.Add(Me.Label4)
        Me.panelBoletas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelBoletas.Location = New System.Drawing.Point(9, 184)
        Me.panelBoletas.Name = "panelBoletas"
        Me.panelBoletas.Size = New System.Drawing.Size(905, 374)
        Me.panelBoletas.TabIndex = 2
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(529, 1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(113, 24)
        Me.Button2.TabIndex = 112
        Me.Button2.Text = "Cambiar Precio"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtPrecio
        '
        Me.txtPrecio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrecio.Location = New System.Drawing.Point(452, 2)
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(72, 22)
        Me.txtPrecio.TabIndex = 111
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(334, 5)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(115, 16)
        Me.Label21.TabIndex = 110
        Me.Label21.Text = "Modificar precio:"
        '
        'btnVer
        '
        Me.btnVer.Location = New System.Drawing.Point(16, 2)
        Me.btnVer.Name = "btnVer"
        Me.btnVer.Size = New System.Drawing.Size(151, 23)
        Me.btnVer.TabIndex = 109
        Me.btnVer.Text = "Ver Boleta"
        Me.btnVer.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(71, 200)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(66, 16)
        Me.Label19.TabIndex = 108
        Me.Label19.Text = "Facturas:"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(736, 324)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 27)
        Me.Button1.TabIndex = 107
        Me.Button1.Text = "Cerrar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.Location = New System.Drawing.Point(736, 258)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(118, 23)
        Me.btnNuevo.TabIndex = 6
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.Location = New System.Drawing.Point(736, 291)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(118, 27)
        Me.btnImprimir.TabIndex = 22
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(733, 210)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(121, 41)
        Me.btnGuardar.TabIndex = 5
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(662, 129)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(183, 16)
        Me.txtTotal.TabIndex = 106
        Me.txtTotal.Text = "LabelL"
        Me.txtTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.txtTotal.Visible = False
        '
        'txtFacturas
        '
        Me.txtFacturas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFacturas.Location = New System.Drawing.Point(662, 95)
        Me.txtFacturas.Name = "txtFacturas"
        Me.txtFacturas.Size = New System.Drawing.Size(183, 16)
        Me.txtFacturas.TabIndex = 105
        Me.txtFacturas.Text = "LabelF"
        Me.txtFacturas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.txtFacturas.Visible = False
        '
        'txtAnticipos
        '
        Me.txtAnticipos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnticipos.Location = New System.Drawing.Point(662, 61)
        Me.txtAnticipos.Name = "txtAnticipos"
        Me.txtAnticipos.Size = New System.Drawing.Size(183, 16)
        Me.txtAnticipos.TabIndex = 104
        Me.txtAnticipos.Text = "labelA"
        Me.txtAnticipos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.txtAnticipos.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(313, 169)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(108, 16)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Peso a liquidar:"
        '
        'txtTotalBoletas
        '
        Me.txtTotalBoletas.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalBoletas.Location = New System.Drawing.Point(662, 27)
        Me.txtTotalBoletas.Name = "txtTotalBoletas"
        Me.txtTotalBoletas.Size = New System.Drawing.Size(183, 16)
        Me.txtTotalBoletas.TabIndex = 103
        Me.txtTotalBoletas.Text = "Label19"
        Me.txtTotalBoletas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.txtTotalBoletas.Visible = False
        '
        'txtPesoBruto
        '
        Me.txtPesoBruto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesoBruto.Location = New System.Drawing.Point(185, 167)
        Me.txtPesoBruto.Name = "txtPesoBruto"
        Me.txtPesoBruto.Size = New System.Drawing.Size(100, 22)
        Me.txtPesoBruto.TabIndex = 1
        Me.txtPesoBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(532, 129)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(122, 16)
        Me.Label15.TabIndex = 102
        Me.Label15.Text = "Total Liquidación:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(71, 170)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 16)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Peso bruto total:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(553, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 16)
        Me.Label5.TabIndex = 100
        Me.Label5.Text = "Total Facturas:"
        '
        'txtPesoFinal
        '
        Me.txtPesoFinal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPesoFinal.Location = New System.Drawing.Point(424, 167)
        Me.txtPesoFinal.Name = "txtPesoFinal"
        Me.txtPesoFinal.Size = New System.Drawing.Size(100, 22)
        Me.txtPesoFinal.TabIndex = 2
        Me.txtPesoFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'dgvFacturas
        '
        Me.dgvFacturas.AllowUserToAddRows = False
        Me.dgvFacturas.AllowUserToDeleteRows = False
        Me.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFacturas.Location = New System.Drawing.Point(16, 219)
        Me.dgvFacturas.MultiSelect = False
        Me.dgvFacturas.Name = "dgvFacturas"
        Me.dgvFacturas.ReadOnly = True
        Me.dgvFacturas.RowHeadersVisible = False
        Me.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvFacturas.Size = New System.Drawing.Size(197, 112)
        Me.dgvFacturas.TabIndex = 14
        '
        'btnAnticipo
        '
        Me.btnAnticipo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnticipo.Location = New System.Drawing.Point(321, 313)
        Me.btnAnticipo.Name = "btnAnticipo"
        Me.btnAnticipo.Size = New System.Drawing.Size(125, 29)
        Me.btnAnticipo.TabIndex = 5
        Me.btnAnticipo.Text = "Agregar Anticipo"
        Me.btnAnticipo.UseVisualStyleBackColor = True
        '
        'txtImporte
        '
        Me.txtImporte.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImporte.Location = New System.Drawing.Point(338, 285)
        Me.txtImporte.Name = "txtImporte"
        Me.txtImporte.Size = New System.Drawing.Size(100, 22)
        Me.txtImporte.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(357, 266)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 16)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "Importe:"
        '
        'txtMedio
        '
        Me.txtMedio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMedio.Location = New System.Drawing.Point(338, 242)
        Me.txtMedio.Name = "txtMedio"
        Me.txtMedio.Size = New System.Drawing.Size(100, 22)
        Me.txtMedio.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(330, 224)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(115, 16)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "Método de pago:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(550, 61)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(104, 16)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Total anticipos:"
        '
        'btnFactura
        '
        Me.btnFactura.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFactura.Location = New System.Drawing.Point(12, 337)
        Me.btnFactura.Name = "btnFactura"
        Me.btnFactura.Size = New System.Drawing.Size(197, 31)
        Me.btnFactura.TabIndex = 4
        Me.btnFactura.Text = "Agregar Factura"
        Me.btnFactura.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(524, 205)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 16)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Anticipos:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(187, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(125, 16)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Boletas liquidadas"
        '
        'dgvAnticipos
        '
        Me.dgvAnticipos.AllowUserToAddRows = False
        Me.dgvAnticipos.AllowUserToDeleteRows = False
        Me.dgvAnticipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAnticipos.Location = New System.Drawing.Point(452, 224)
        Me.dgvAnticipos.Name = "dgvAnticipos"
        Me.dgvAnticipos.ReadOnly = True
        Me.dgvAnticipos.RowHeadersVisible = False
        Me.dgvAnticipos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAnticipos.Size = New System.Drawing.Size(207, 107)
        Me.dgvAnticipos.TabIndex = 99
        '
        'dgvLiqudadas
        '
        Me.dgvLiqudadas.AllowUserToAddRows = False
        Me.dgvLiqudadas.AllowUserToDeleteRows = False
        Me.dgvLiqudadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLiqudadas.Location = New System.Drawing.Point(15, 27)
        Me.dgvLiqudadas.Name = "dgvLiqudadas"
        Me.dgvLiqudadas.ReadOnly = True
        Me.dgvLiqudadas.RowHeadersVisible = False
        Me.dgvLiqudadas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLiqudadas.Size = New System.Drawing.Size(511, 129)
        Me.dgvLiqudadas.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(560, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Total Boletas:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Domicilio:"
        '
        'lblDomicilio
        '
        Me.lblDomicilio.AutoSize = True
        Me.lblDomicilio.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDomicilio.Location = New System.Drawing.Point(72, 135)
        Me.lblDomicilio.Name = "lblDomicilio"
        Me.lblDomicilio.Size = New System.Drawing.Size(0, 16)
        Me.lblDomicilio.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(34, 163)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 16)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "RFC:"
        '
        'lblRFC
        '
        Me.lblRFC.AutoSize = True
        Me.lblRFC.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRFC.Location = New System.Drawing.Point(72, 162)
        Me.lblRFC.Name = "lblRFC"
        Me.lblRFC.Size = New System.Drawing.Size(0, 16)
        Me.lblRFC.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(383, 161)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 16)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Cosecha:"
        Me.Label9.Visible = False
        '
        'lblCosecha
        '
        Me.lblCosecha.AutoSize = True
        Me.lblCosecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCosecha.Location = New System.Drawing.Point(414, 135)
        Me.lblCosecha.Name = "lblCosecha"
        Me.lblCosecha.Size = New System.Drawing.Size(0, 16)
        Me.lblCosecha.TabIndex = 19
        Me.lblCosecha.Visible = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'PrintDocument1
        '
        '
        'frmSemillasLiquidacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(921, 560)
        Me.Controls.Add(Me.lblCosecha)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblRFC)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblDomicilio)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.panelBoletas)
        Me.Controls.Add(Me.panelProductor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSemillasLiquidacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidacion"
        Me.panelProductor.ResumeLayout(False)
        Me.panelProductor.PerformLayout()
        Me.panelBoletas.ResumeLayout(False)
        Me.panelBoletas.PerformLayout()
        CType(Me.dgvFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvAnticipos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvLiqudadas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents panelProductor As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFolio As System.Windows.Forms.TextBox
    Friend WithEvents panelBoletas As System.Windows.Forms.Panel
    Friend WithEvents dgvLiqudadas As System.Windows.Forms.DataGridView
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPesoBruto As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPesoFinal As System.Windows.Forms.TextBox
    Friend WithEvents btnBoletas As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblDomicilio As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblRFC As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblCosecha As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dgvAnticipos As System.Windows.Forms.DataGridView
    Friend WithEvents btnFactura As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnAnticipo As System.Windows.Forms.Button
    Friend WithEvents txtImporte As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMedio As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnLiquidacion As System.Windows.Forms.Button
    Friend WithEvents dgvFacturas As System.Windows.Forms.DataGridView
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents txtProductor As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.Label
    Friend WithEvents txtFacturas As System.Windows.Forms.Label
    Friend WithEvents txtAnticipos As System.Windows.Forms.Label
    Friend WithEvents txtTotalBoletas As System.Windows.Forms.Label
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btnVer As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtPrecio As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
End Class
