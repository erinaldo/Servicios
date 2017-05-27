<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContabilidadPolizasN
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbClasificacionPoliza = New System.Windows.Forms.ComboBox()
        Me.chkMantenerFecha = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBeneficiario = New System.Windows.Forms.TextBox()
        Me.cmbTipoPolizaN = New System.Windows.Forms.ComboBox()
        Me.txtConcepto = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBeneficiario = New System.Windows.Forms.Label()
        Me.txtNumeroPoliza = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFechaN = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtAbono = New System.Windows.Forms.TextBox()
        Me.txtCargo = New System.Windows.Forms.TextBox()
        Me.btnNuevoDetalle = New System.Windows.Forms.Button()
        Me.btnEliminarDetalle = New System.Windows.Forms.Button()
        Me.btnAgregarDetalle = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.pnlDatosIVA = New System.Windows.Forms.Panel()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.cmbProveedores = New System.Windows.Forms.ComboBox()
        Me.lblValorActos = New System.Windows.Forms.Label()
        Me.txtValorActos = New System.Windows.Forms.TextBox()
        Me.txtCocepFac = New System.Windows.Forms.TextBox()
        Me.txtIVA = New System.Windows.Forms.TextBox()
        Me.txtFolioFactura = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dgvCuentas = New System.Windows.Forms.DataGridView()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnXML = New System.Windows.Forms.Button()
        Me.chkAgregarComprobante = New System.Windows.Forms.CheckBox()
        Me.btnComprobantes = New System.Windows.Forms.Button()
        Me.btnImportar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.lblN5 = New System.Windows.Forms.Label()
        Me.lblN4 = New System.Windows.Forms.Label()
        Me.lblN3 = New System.Windows.Forms.Label()
        Me.lblN2 = New System.Windows.Forms.Label()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.btnGuardarPoliza = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.lblDiferencia = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblAbonos = New System.Windows.Forms.Label()
        Me.lblCargos = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.bcc = New Servicios.BuscadorCuentasContables()
        Me.sc = New Servicios.SelectorCuentas()
        Me.txtCantCheque = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.pnlDatosIVA.SuspendLayout()
        CType(Me.dgvCuentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(204, 10)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(114, 16)
        Me.Label11.TabIndex = 220
        Me.Label11.Text = "Clasif. de póliza:"
        '
        'cmbClasificacionPoliza
        '
        Me.cmbClasificacionPoliza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClasificacionPoliza.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClasificacionPoliza.FormattingEnabled = True
        Me.cmbClasificacionPoliza.Items.AddRange(New Object() {"E  Egreso", "I   Ingreso", "D Diario", "A Apertura"})
        Me.cmbClasificacionPoliza.Location = New System.Drawing.Point(319, 8)
        Me.cmbClasificacionPoliza.Name = "cmbClasificacionPoliza"
        Me.cmbClasificacionPoliza.Size = New System.Drawing.Size(293, 24)
        Me.cmbClasificacionPoliza.TabIndex = 34
        '
        'chkMantenerFecha
        '
        Me.chkMantenerFecha.AutoSize = True
        Me.chkMantenerFecha.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMantenerFecha.Location = New System.Drawing.Point(171, 12)
        Me.chkMantenerFecha.Name = "chkMantenerFecha"
        Me.chkMantenerFecha.Size = New System.Drawing.Size(15, 14)
        Me.chkMantenerFecha.TabIndex = 33
        Me.chkMantenerFecha.TabStop = False
        Me.chkMantenerFecha.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 16)
        Me.Label1.TabIndex = 215
        Me.Label1.Text = "Tipo de póliza:"
        '
        'txtBeneficiario
        '
        Me.txtBeneficiario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBeneficiario.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBeneficiario.Location = New System.Drawing.Point(108, 94)
        Me.txtBeneficiario.MaxLength = 200
        Me.txtBeneficiario.Name = "txtBeneficiario"
        Me.txtBeneficiario.Size = New System.Drawing.Size(510, 22)
        Me.txtBeneficiario.TabIndex = 1
        '
        'cmbTipoPolizaN
        '
        Me.cmbTipoPolizaN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoPolizaN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoPolizaN.FormattingEnabled = True
        Me.cmbTipoPolizaN.Items.AddRange(New Object() {"E  Egreso", "I   Ingreso", "D Diario", "A Apertura"})
        Me.cmbTipoPolizaN.Location = New System.Drawing.Point(108, 38)
        Me.cmbTipoPolizaN.Name = "cmbTipoPolizaN"
        Me.cmbTipoPolizaN.Size = New System.Drawing.Size(97, 24)
        Me.cmbTipoPolizaN.TabIndex = 35
        '
        'txtConcepto
        '
        Me.txtConcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtConcepto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConcepto.Location = New System.Drawing.Point(108, 67)
        Me.txtConcepto.MaxLength = 200
        Me.txtConcepto.Name = "txtConcepto"
        Me.txtConcepto.Size = New System.Drawing.Size(664, 22)
        Me.txtConcepto.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(214, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 16)
        Me.Label2.TabIndex = 216
        Me.Label2.Text = "Número:"
        '
        'lblBeneficiario
        '
        Me.lblBeneficiario.AutoSize = True
        Me.lblBeneficiario.BackColor = System.Drawing.Color.Transparent
        Me.lblBeneficiario.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeneficiario.ForeColor = System.Drawing.Color.Black
        Me.lblBeneficiario.Location = New System.Drawing.Point(17, 97)
        Me.lblBeneficiario.Name = "lblBeneficiario"
        Me.lblBeneficiario.Size = New System.Drawing.Size(89, 16)
        Me.lblBeneficiario.TabIndex = 219
        Me.lblBeneficiario.Text = "Beneficiario:"
        '
        'txtNumeroPoliza
        '
        Me.txtNumeroPoliza.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroPoliza.Location = New System.Drawing.Point(278, 40)
        Me.txtNumeroPoliza.Name = "txtNumeroPoliza"
        Me.txtNumeroPoliza.Size = New System.Drawing.Size(90, 22)
        Me.txtNumeroPoliza.TabIndex = 36
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(34, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 218
        Me.Label4.Text = "Concepto:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(23, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 217
        Me.Label3.Text = "Fecha:"
        '
        'dtpFechaN
        '
        Me.dtpFechaN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFechaN.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaN.Location = New System.Drawing.Point(78, 8)
        Me.dtpFechaN.Name = "dtpFechaN"
        Me.dtpFechaN.Size = New System.Drawing.Size(89, 22)
        Me.dtpFechaN.TabIndex = 32
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button1.Location = New System.Drawing.Point(813, 43)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(151, 45)
        Me.Button1.TabIndex = 30
        Me.Button1.Text = "Buscar Póliza"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button2.Location = New System.Drawing.Point(886, 6)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(82, 25)
        Me.Button2.TabIndex = 31
        Me.Button2.Text = "Cerrar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtAbono
        '
        Me.txtAbono.BackColor = System.Drawing.Color.White
        Me.txtAbono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAbono.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAbono.Location = New System.Drawing.Point(685, 145)
        Me.txtAbono.MaxLength = 80
        Me.txtAbono.Name = "txtAbono"
        Me.txtAbono.Size = New System.Drawing.Size(80, 22)
        Me.txtAbono.TabIndex = 5
        Me.txtAbono.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCargo
        '
        Me.txtCargo.BackColor = System.Drawing.Color.White
        Me.txtCargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCargo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCargo.Location = New System.Drawing.Point(600, 145)
        Me.txtCargo.MaxLength = 80
        Me.txtCargo.Name = "txtCargo"
        Me.txtCargo.Size = New System.Drawing.Size(80, 22)
        Me.txtCargo.TabIndex = 4
        Me.txtCargo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnNuevoDetalle
        '
        Me.btnNuevoDetalle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoDetalle.Location = New System.Drawing.Point(915, 141)
        Me.btnNuevoDetalle.Name = "btnNuevoDetalle"
        Me.btnNuevoDetalle.Size = New System.Drawing.Size(58, 27)
        Me.btnNuevoDetalle.TabIndex = 19
        Me.btnNuevoDetalle.Text = "Nuevo"
        Me.btnNuevoDetalle.UseVisualStyleBackColor = True
        '
        'btnEliminarDetalle
        '
        Me.btnEliminarDetalle.Enabled = False
        Me.btnEliminarDetalle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarDetalle.Location = New System.Drawing.Point(844, 141)
        Me.btnEliminarDetalle.Name = "btnEliminarDetalle"
        Me.btnEliminarDetalle.Size = New System.Drawing.Size(68, 27)
        Me.btnEliminarDetalle.TabIndex = 18
        Me.btnEliminarDetalle.Text = "Eliminar"
        Me.btnEliminarDetalle.UseVisualStyleBackColor = True
        '
        'btnAgregarDetalle
        '
        Me.btnAgregarDetalle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarDetalle.Location = New System.Drawing.Point(773, 141)
        Me.btnAgregarDetalle.Name = "btnAgregarDetalle"
        Me.btnAgregarDetalle.Size = New System.Drawing.Size(68, 27)
        Me.btnAgregarDetalle.TabIndex = 17
        Me.btnAgregarDetalle.Text = "Agregar"
        Me.btnAgregarDetalle.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(700, 126)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(49, 16)
        Me.Label28.TabIndex = 272
        Me.Label28.Text = "Abono"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(619, 126)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(46, 16)
        Me.Label27.TabIndex = 271
        Me.Label27.Text = "Cargo"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(286, 126)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(82, 16)
        Me.Label26.TabIndex = 270
        Me.Label26.Text = "Descripción"
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.White
        Me.txtDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(282, 145)
        Me.txtDesc.MaxLength = 1000
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(313, 22)
        Me.txtDesc.TabIndex = 3
        '
        'pnlDatosIVA
        '
        Me.pnlDatosIVA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDatosIVA.Controls.Add(Me.TextBox3)
        Me.pnlDatosIVA.Controls.Add(Me.Label19)
        Me.pnlDatosIVA.Controls.Add(Me.TextBox2)
        Me.pnlDatosIVA.Controls.Add(Me.Label18)
        Me.pnlDatosIVA.Controls.Add(Me.Label13)
        Me.pnlDatosIVA.Controls.Add(Me.TextBox1)
        Me.pnlDatosIVA.Controls.Add(Me.cmbProveedores)
        Me.pnlDatosIVA.Controls.Add(Me.lblValorActos)
        Me.pnlDatosIVA.Controls.Add(Me.txtValorActos)
        Me.pnlDatosIVA.Controls.Add(Me.txtCocepFac)
        Me.pnlDatosIVA.Controls.Add(Me.txtIVA)
        Me.pnlDatosIVA.Controls.Add(Me.txtFolioFactura)
        Me.pnlDatosIVA.Controls.Add(Me.Label10)
        Me.pnlDatosIVA.Controls.Add(Me.Label9)
        Me.pnlDatosIVA.Controls.Add(Me.Label5)
        Me.pnlDatosIVA.Location = New System.Drawing.Point(4, 170)
        Me.pnlDatosIVA.Name = "pnlDatosIVA"
        Me.pnlDatosIVA.Size = New System.Drawing.Size(970, 44)
        Me.pnlDatosIVA.TabIndex = 9
        '
        'TextBox3
        '
        Me.TextBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(589, 18)
        Me.TextBox3.MaxLength = 2
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(61, 22)
        Me.TextBox3.TabIndex = 14
        Me.TextBox3.Text = "0"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(593, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 16)
        Me.Label19.TabIndex = 229
        Me.Label19.Text = "%IEPS"
        '
        'TextBox2
        '
        Me.TextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(525, 18)
        Me.TextBox2.MaxLength = 2
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(61, 22)
        Me.TextBox2.TabIndex = 13
        Me.TextBox2.Text = "0"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(525, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(68, 16)
        Me.Label18.TabIndex = 227
        Me.Label18.Text = "%IVA RET"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(735, 2)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 16)
        Me.Label13.TabIndex = 226
        Me.Label13.Text = "Referencia"
        '
        'TextBox1
        '
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(733, 18)
        Me.TextBox1.MaxLength = 200
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(234, 22)
        Me.TextBox1.TabIndex = 16
        '
        'cmbProveedores
        '
        Me.cmbProveedores.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbProveedores.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedores.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProveedores.FormattingEnabled = True
        Me.cmbProveedores.Items.AddRange(New Object() {"E  Egreso", "I   Ingreso", "D Diario", "A Apertura"})
        Me.cmbProveedores.Location = New System.Drawing.Point(64, 18)
        Me.cmbProveedores.Name = "cmbProveedores"
        Me.cmbProveedores.Size = New System.Drawing.Size(391, 24)
        Me.cmbProveedores.TabIndex = 11
        '
        'lblValorActos
        '
        Me.lblValorActos.AutoSize = True
        Me.lblValorActos.BackColor = System.Drawing.Color.Transparent
        Me.lblValorActos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValorActos.ForeColor = System.Drawing.Color.Black
        Me.lblValorActos.Location = New System.Drawing.Point(651, 0)
        Me.lblValorActos.Name = "lblValorActos"
        Me.lblValorActos.Size = New System.Drawing.Size(78, 16)
        Me.lblValorActos.TabIndex = 224
        Me.lblValorActos.Text = "Valor actos"
        Me.lblValorActos.Visible = False
        '
        'txtValorActos
        '
        Me.txtValorActos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtValorActos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorActos.Location = New System.Drawing.Point(653, 18)
        Me.txtValorActos.MaxLength = 40
        Me.txtValorActos.Name = "txtValorActos"
        Me.txtValorActos.Size = New System.Drawing.Size(75, 22)
        Me.txtValorActos.TabIndex = 15
        Me.txtValorActos.Text = "0.00"
        Me.txtValorActos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtValorActos.Visible = False
        '
        'txtCocepFac
        '
        Me.txtCocepFac.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCocepFac.Location = New System.Drawing.Point(759, 18)
        Me.txtCocepFac.MaxLength = 60
        Me.txtCocepFac.Name = "txtCocepFac"
        Me.txtCocepFac.Size = New System.Drawing.Size(29, 20)
        Me.txtCocepFac.TabIndex = 222
        Me.txtCocepFac.Visible = False
        '
        'txtIVA
        '
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Location = New System.Drawing.Point(461, 18)
        Me.txtIVA.MaxLength = 2
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(61, 22)
        Me.txtIVA.TabIndex = 12
        Me.txtIVA.Text = "16"
        Me.txtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFolioFactura
        '
        Me.txtFolioFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFolioFactura.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolioFactura.Location = New System.Drawing.Point(2, 18)
        Me.txtFolioFactura.MaxLength = 45
        Me.txtFolioFactura.Name = "txtFolioFactura"
        Me.txtFolioFactura.Size = New System.Drawing.Size(59, 22)
        Me.txtFolioFactura.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(474, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(39, 16)
        Me.Label10.TabIndex = 215
        Me.Label10.Text = "%IVA"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(97, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 16)
        Me.Label9.TabIndex = 214
        Me.Label9.Text = "Proveedor"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(13, -1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 16)
        Me.Label5.TabIndex = 213
        Me.Label5.Text = "Folio"
        '
        'dgvCuentas
        '
        Me.dgvCuentas.AllowUserToAddRows = False
        Me.dgvCuentas.AllowUserToDeleteRows = False
        Me.dgvCuentas.AllowUserToResizeColumns = False
        Me.dgvCuentas.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.dgvCuentas.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCuentas.BackgroundColor = System.Drawing.Color.White
        Me.dgvCuentas.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.dgvCuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCuentas.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCuentas.GridColor = System.Drawing.Color.LightGray
        Me.dgvCuentas.Location = New System.Drawing.Point(4, 170)
        Me.dgvCuentas.MultiSelect = False
        Me.dgvCuentas.Name = "dgvCuentas"
        Me.dgvCuentas.ReadOnly = True
        Me.dgvCuentas.RowHeadersVisible = False
        Me.dgvCuentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCuentas.Size = New System.Drawing.Size(970, 288)
        Me.dgvCuentas.StandardTab = True
        Me.dgvCuentas.TabIndex = 20
        Me.dgvCuentas.TabStop = False
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button3.Location = New System.Drawing.Point(519, 537)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(156, 25)
        Me.Button3.TabIndex = 26
        Me.Button3.Text = "Guardar como nueva."
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnXML
        '
        Me.btnXML.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnXML.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnXML.Location = New System.Drawing.Point(90, 537)
        Me.btnXML.Name = "btnXML"
        Me.btnXML.Size = New System.Drawing.Size(59, 25)
        Me.btnXML.TabIndex = 23
        Me.btnXML.Text = "XML"
        Me.btnXML.UseVisualStyleBackColor = True
        '
        'chkAgregarComprobante
        '
        Me.chkAgregarComprobante.AutoSize = True
        Me.chkAgregarComprobante.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgregarComprobante.ForeColor = System.Drawing.Color.Black
        Me.chkAgregarComprobante.Location = New System.Drawing.Point(333, 541)
        Me.chkAgregarComprobante.Name = "chkAgregarComprobante"
        Me.chkAgregarComprobante.Size = New System.Drawing.Size(150, 18)
        Me.chkAgregarComprobante.TabIndex = 25
        Me.chkAgregarComprobante.Text = "Comprobantes al agregar."
        Me.chkAgregarComprobante.UseVisualStyleBackColor = True
        '
        'btnComprobantes
        '
        Me.btnComprobantes.Enabled = False
        Me.btnComprobantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnComprobantes.Location = New System.Drawing.Point(154, 537)
        Me.btnComprobantes.Name = "btnComprobantes"
        Me.btnComprobantes.Size = New System.Drawing.Size(170, 25)
        Me.btnComprobantes.TabIndex = 24
        Me.btnComprobantes.Text = "Agregar Comprobantes"
        Me.btnComprobantes.UseVisualStyleBackColor = True
        '
        'btnImportar
        '
        Me.btnImportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnImportar.Location = New System.Drawing.Point(9, 537)
        Me.btnImportar.Name = "btnImportar"
        Me.btnImportar.Size = New System.Drawing.Size(75, 25)
        Me.btnImportar.TabIndex = 22
        Me.btnImportar.Text = "Importar"
        Me.btnImportar.UseVisualStyleBackColor = True
        '
        'btnImprimir
        '
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnImprimir.Location = New System.Drawing.Point(897, 537)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(75, 25)
        Me.btnImprimir.TabIndex = 29
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'lblN5
        '
        Me.lblN5.AutoSize = True
        Me.lblN5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN5.ForeColor = System.Drawing.Color.Black
        Me.lblN5.Location = New System.Drawing.Point(37, 521)
        Me.lblN5.Name = "lblN5"
        Me.lblN5.Size = New System.Drawing.Size(39, 14)
        Me.lblN5.TabIndex = 293
        Me.lblN5.Text = "Label5"
        '
        'lblN4
        '
        Me.lblN4.AutoSize = True
        Me.lblN4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN4.ForeColor = System.Drawing.Color.Black
        Me.lblN4.Location = New System.Drawing.Point(30, 506)
        Me.lblN4.Name = "lblN4"
        Me.lblN4.Size = New System.Drawing.Size(39, 14)
        Me.lblN4.TabIndex = 292
        Me.lblN4.Text = "Label5"
        '
        'lblN3
        '
        Me.lblN3.AutoSize = True
        Me.lblN3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN3.ForeColor = System.Drawing.Color.Black
        Me.lblN3.Location = New System.Drawing.Point(22, 490)
        Me.lblN3.Name = "lblN3"
        Me.lblN3.Size = New System.Drawing.Size(39, 14)
        Me.lblN3.TabIndex = 291
        Me.lblN3.Text = "Label5"
        '
        'lblN2
        '
        Me.lblN2.AutoSize = True
        Me.lblN2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN2.ForeColor = System.Drawing.Color.Black
        Me.lblN2.Location = New System.Drawing.Point(15, 475)
        Me.lblN2.Name = "lblN2"
        Me.lblN2.Size = New System.Drawing.Size(39, 14)
        Me.lblN2.TabIndex = 290
        Me.lblN2.Text = "Label5"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcion.ForeColor = System.Drawing.Color.Black
        Me.lblDescripcion.Location = New System.Drawing.Point(8, 460)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(39, 14)
        Me.lblDescripcion.TabIndex = 289
        Me.lblDescripcion.Text = "Label5"
        '
        'btnGuardarPoliza
        '
        Me.btnGuardarPoliza.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardarPoliza.Location = New System.Drawing.Point(389, 475)
        Me.btnGuardarPoliza.Name = "btnGuardarPoliza"
        Me.btnGuardarPoliza.Size = New System.Drawing.Size(200, 51)
        Me.btnGuardarPoliza.TabIndex = 21
        Me.btnGuardarPoliza.Text = "Guardar (F10)"
        Me.btnGuardarPoliza.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnNuevo.Location = New System.Drawing.Point(787, 537)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(104, 25)
        Me.btnNuevo.TabIndex = 28
        Me.btnNuevo.Text = "Nueva póliza"
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnEliminar.Location = New System.Drawing.Point(679, 537)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(104, 25)
        Me.btnEliminar.TabIndex = 27
        Me.btnEliminar.Text = "Eliminar póliza"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'lblDiferencia
        '
        Me.lblDiferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferencia.ForeColor = System.Drawing.Color.Black
        Me.lblDiferencia.Location = New System.Drawing.Point(865, 491)
        Me.lblDiferencia.Name = "lblDiferencia"
        Me.lblDiferencia.Size = New System.Drawing.Size(102, 14)
        Me.lblDiferencia.TabIndex = 288
        Me.lblDiferencia.Text = "0.00"
        Me.lblDiferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(775, 491)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 16)
        Me.Label24.TabIndex = 287
        Me.Label24.Text = "Diferencia:"
        '
        'lblAbonos
        '
        Me.lblAbonos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbonos.ForeColor = System.Drawing.Color.Black
        Me.lblAbonos.Location = New System.Drawing.Point(865, 469)
        Me.lblAbonos.Name = "lblAbonos"
        Me.lblAbonos.Size = New System.Drawing.Size(102, 17)
        Me.lblAbonos.TabIndex = 286
        Me.lblAbonos.Text = "0.00"
        Me.lblAbonos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCargos
        '
        Me.lblCargos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCargos.ForeColor = System.Drawing.Color.Black
        Me.lblCargos.Location = New System.Drawing.Point(760, 469)
        Me.lblCargos.Name = "lblCargos"
        Me.lblCargos.Size = New System.Drawing.Size(103, 16)
        Me.lblCargos.TabIndex = 285
        Me.lblCargos.Text = "0.00"
        Me.lblCargos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(640, 469)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(107, 16)
        Me.Label23.TabIndex = 284
        Me.Label23.Text = "Cargos/Abonos:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PrintDocument1
        '
        '
        'bcc
        '
        Me.bcc.Location = New System.Drawing.Point(3, 169)
        Me.bcc.Name = "bcc"
        Me.bcc.Size = New System.Drawing.Size(971, 289)
        Me.bcc.TabIndex = 294
        Me.bcc.Visible = False
        '
        'sc
        '
        Me.sc.BackColor = System.Drawing.Color.Transparent
        Me.sc.IdCuenta = 0
        Me.sc.Location = New System.Drawing.Point(7, 123)
        Me.sc.Name = "sc"
        Me.sc.Size = New System.Drawing.Size(271, 45)
        Me.sc.TabIndex = 3
        '
        'txtCantCheque
        '
        Me.txtCantCheque.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCantCheque.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantCheque.Location = New System.Drawing.Point(722, 95)
        Me.txtCantCheque.MaxLength = 200
        Me.txtCantCheque.Name = "txtCantCheque"
        Me.txtCantCheque.Size = New System.Drawing.Size(95, 22)
        Me.txtCantCheque.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(623, 97)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 16)
        Me.Label6.TabIndex = 296
        Me.Label6.Text = "Total Cheque:"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBox1.Location = New System.Drawing.Point(822, 97)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(146, 18)
        Me.CheckBox1.TabIndex = 297
        Me.CheckBox1.TabStop = False
        Me.CheckBox1.Text = "A cuenta de beneficiario."
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'frmContabilidadPolizasN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(979, 567)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.txtCantCheque)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.bcc)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.btnXML)
        Me.Controls.Add(Me.chkAgregarComprobante)
        Me.Controls.Add(Me.btnComprobantes)
        Me.Controls.Add(Me.btnImportar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.lblN5)
        Me.Controls.Add(Me.lblN4)
        Me.Controls.Add(Me.lblN3)
        Me.Controls.Add(Me.lblN2)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.btnGuardarPoliza)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.lblDiferencia)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.lblAbonos)
        Me.Controls.Add(Me.lblCargos)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.sc)
        Me.Controls.Add(Me.txtAbono)
        Me.Controls.Add(Me.txtCargo)
        Me.Controls.Add(Me.btnNuevoDetalle)
        Me.Controls.Add(Me.btnEliminarDetalle)
        Me.Controls.Add(Me.btnAgregarDetalle)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.pnlDatosIVA)
        Me.Controls.Add(Me.dgvCuentas)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmbClasificacionPoliza)
        Me.Controls.Add(Me.chkMantenerFecha)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBeneficiario)
        Me.Controls.Add(Me.cmbTipoPolizaN)
        Me.Controls.Add(Me.txtConcepto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblBeneficiario)
        Me.Controls.Add(Me.txtNumeroPoliza)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpFechaN)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmContabilidadPolizasN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pólizas"
        Me.pnlDatosIVA.ResumeLayout(False)
        Me.pnlDatosIVA.PerformLayout()
        CType(Me.dgvCuentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbClasificacionPoliza As System.Windows.Forms.ComboBox
    Friend WithEvents chkMantenerFecha As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBeneficiario As System.Windows.Forms.TextBox
    Friend WithEvents cmbTipoPolizaN As System.Windows.Forms.ComboBox
    Friend WithEvents txtConcepto As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblBeneficiario As System.Windows.Forms.Label
    Friend WithEvents txtNumeroPoliza As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaN As System.Windows.Forms.DateTimePicker
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtAbono As System.Windows.Forms.TextBox
    Friend WithEvents txtCargo As System.Windows.Forms.TextBox
    Friend WithEvents btnNuevoDetalle As System.Windows.Forms.Button
    Friend WithEvents btnEliminarDetalle As System.Windows.Forms.Button
    Friend WithEvents btnAgregarDetalle As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents pnlDatosIVA As System.Windows.Forms.Panel
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents cmbProveedores As System.Windows.Forms.ComboBox
    Friend WithEvents lblValorActos As System.Windows.Forms.Label
    Friend WithEvents txtValorActos As System.Windows.Forms.TextBox
    Friend WithEvents txtCocepFac As System.Windows.Forms.TextBox
    Friend WithEvents txtIVA As System.Windows.Forms.TextBox
    Friend WithEvents txtFolioFactura As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dgvCuentas As System.Windows.Forms.DataGridView
    Friend WithEvents sc As Servicios.SelectorCuentas
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btnXML As System.Windows.Forms.Button
    Friend WithEvents chkAgregarComprobante As System.Windows.Forms.CheckBox
    Friend WithEvents btnComprobantes As System.Windows.Forms.Button
    Friend WithEvents btnImportar As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents lblN5 As System.Windows.Forms.Label
    Friend WithEvents lblN4 As System.Windows.Forms.Label
    Friend WithEvents lblN3 As System.Windows.Forms.Label
    Friend WithEvents lblN2 As System.Windows.Forms.Label
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents btnGuardarPoliza As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents lblDiferencia As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblAbonos As System.Windows.Forms.Label
    Friend WithEvents lblCargos As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents bcc As Servicios.BuscadorCuentasContables
    Friend WithEvents txtCantCheque As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
