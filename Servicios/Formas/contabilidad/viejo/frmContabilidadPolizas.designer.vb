<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContabilidadPolizas
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
        Me.lblDiferencia = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblAbonos = New System.Windows.Forms.Label()
        Me.lblCargos = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.dgvCuentas = New System.Windows.Forms.DataGridView()
        Me.btnAgregarDetalle = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNumeroPoliza = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblBeneficiario = New System.Windows.Forms.Label()
        Me.txtConcepto = New System.Windows.Forms.TextBox()
        Me.txtBeneficiario = New System.Windows.Forms.TextBox()
        Me.grpDatosPoliza = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbClasificacionPoliza = New System.Windows.Forms.ComboBox()
        Me.chkMantenerFecha = New System.Windows.Forms.CheckBox()
        Me.cmbProveedores = New System.Windows.Forms.ComboBox()
        Me.grbBusqueda = New System.Windows.Forms.GroupBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbClasBusqueda = New System.Windows.Forms.ComboBox()
        Me.lbltipo2 = New System.Windows.Forms.Label()
        Me.cmbTipoBuscar = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.txtBuscarConecpto = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.btnGuardarPoliza = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnEliminarDetalle = New System.Windows.Forms.Button()
        Me.btnNuevoDetalle = New System.Windows.Forms.Button()
        Me.txtCargo = New System.Windows.Forms.TextBox()
        Me.txtAbono = New System.Windows.Forms.TextBox()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtN5 = New System.Windows.Forms.TextBox()
        Me.txtN4 = New System.Windows.Forms.TextBox()
        Me.txtN3 = New System.Windows.Forms.TextBox()
        Me.txtN2 = New System.Windows.Forms.TextBox()
        Me.txtCuenta = New System.Windows.Forms.TextBox()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.txtFolioFactura = New System.Windows.Forms.TextBox()
        Me.txtIVA = New System.Windows.Forms.TextBox()
        Me.txtCocepFac = New System.Windows.Forms.TextBox()
        Me.btnImportar = New System.Windows.Forms.Button()
        Me.chkAgregarComprobante = New System.Windows.Forms.CheckBox()
        Me.btnXML = New System.Windows.Forms.Button()
        Me.txtValorActos = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblN2 = New System.Windows.Forms.Label()
        Me.lblN3 = New System.Windows.Forms.Label()
        Me.lblN4 = New System.Windows.Forms.Label()
        Me.lblN5 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.pnlDatosIVA = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblValorActos = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnComprobantes = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbtipopolizax = New System.Windows.Forms.ComboBox()
        CType(Me.dgvCuentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDatosPoliza.SuspendLayout()
        Me.grbBusqueda.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDatosIVA.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDiferencia
        '
        Me.lblDiferencia.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferencia.ForeColor = System.Drawing.Color.Black
        Me.lblDiferencia.Location = New System.Drawing.Point(922, 479)
        Me.lblDiferencia.Name = "lblDiferencia"
        Me.lblDiferencia.Size = New System.Drawing.Size(102, 14)
        Me.lblDiferencia.TabIndex = 197
        Me.lblDiferencia.Text = "0.00"
        Me.lblDiferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.lblDiferencia, "Diferencia entre cargos y abonos.")
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(832, 479)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(77, 16)
        Me.Label24.TabIndex = 196
        Me.Label24.Text = "Diferencia:"
        '
        'lblAbonos
        '
        Me.lblAbonos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbonos.ForeColor = System.Drawing.Color.Black
        Me.lblAbonos.Location = New System.Drawing.Point(922, 457)
        Me.lblAbonos.Name = "lblAbonos"
        Me.lblAbonos.Size = New System.Drawing.Size(102, 17)
        Me.lblAbonos.TabIndex = 195
        Me.lblAbonos.Text = "0.00"
        Me.lblAbonos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.lblAbonos, "Total de abonos.")
        '
        'lblCargos
        '
        Me.lblCargos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCargos.ForeColor = System.Drawing.Color.Black
        Me.lblCargos.Location = New System.Drawing.Point(817, 457)
        Me.lblCargos.Name = "lblCargos"
        Me.lblCargos.Size = New System.Drawing.Size(103, 16)
        Me.lblCargos.TabIndex = 194
        Me.lblCargos.Text = "0.00"
        Me.lblCargos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.lblCargos, "Total de cargos.")
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(704, 457)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(107, 16)
        Me.Label23.TabIndex = 193
        Me.Label23.Text = "Cargos/Abonos:"
        '
        'dgvCuentas
        '
        Me.dgvCuentas.AllowUserToAddRows = False
        Me.dgvCuentas.AllowUserToDeleteRows = False
        Me.dgvCuentas.AllowUserToResizeColumns = False
        Me.dgvCuentas.AllowUserToResizeRows = False
        Me.dgvCuentas.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvCuentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCuentas.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvCuentas.Location = New System.Drawing.Point(6, 234)
        Me.dgvCuentas.MultiSelect = False
        Me.dgvCuentas.Name = "dgvCuentas"
        Me.dgvCuentas.ReadOnly = True
        Me.dgvCuentas.RowHeadersVisible = False
        Me.dgvCuentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCuentas.Size = New System.Drawing.Size(1026, 208)
        Me.dgvCuentas.TabIndex = 21
        Me.dgvCuentas.TabStop = False
        '
        'btnAgregarDetalle
        '
        Me.btnAgregarDetalle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregarDetalle.Location = New System.Drawing.Point(819, 202)
        Me.btnAgregarDetalle.Name = "btnAgregarDetalle"
        Me.btnAgregarDetalle.Size = New System.Drawing.Size(68, 27)
        Me.btnAgregarDetalle.TabIndex = 18
        Me.btnAgregarDetalle.Text = "Agregar"
        Me.ToolTip1.SetToolTip(Me.btnAgregarDetalle, "Agregar/Modificar renglón.")
        Me.btnAgregarDetalle.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(746, 187)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(49, 16)
        Me.Label28.TabIndex = 192
        Me.Label28.Text = "Abono"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(665, 187)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(46, 16)
        Me.Label27.TabIndex = 191
        Me.Label27.Text = "Cargo"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(410, 187)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(82, 16)
        Me.Label26.TabIndex = 190
        Me.Label26.Text = "Descripción"
        '
        'txtDesc
        '
        Me.txtDesc.BackColor = System.Drawing.Color.White
        Me.txtDesc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDesc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(275, 206)
        Me.txtDesc.MaxLength = 1000
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(366, 22)
        Me.txtDesc.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtDesc, "Descripción del concepto a registrar.")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 16)
        Me.Label1.TabIndex = 199
        Me.Label1.Text = "Tipo de póliza:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(51, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 16)
        Me.Label2.TabIndex = 200
        Me.Label2.Text = "Número:"
        '
        'txtNumeroPoliza
        '
        Me.txtNumeroPoliza.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumeroPoliza.Location = New System.Drawing.Point(115, 76)
        Me.txtNumeroPoliza.Name = "txtNumeroPoliza"
        Me.txtNumeroPoliza.Size = New System.Drawing.Size(90, 22)
        Me.txtNumeroPoliza.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtNumeroPoliza, "¿Quiere el siguiente número consecutivo? presione '+'.")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(62, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 16)
        Me.Label3.TabIndex = 202
        Me.Label3.Text = "Fecha:"
        '
        'dtpFecha
        '
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(117, 101)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(89, 22)
        Me.dtpFecha.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFecha, "Fecha de emisión de la póliza.")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(41, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 204
        Me.Label4.Text = "Concepto:"
        '
        'lblBeneficiario
        '
        Me.lblBeneficiario.AutoSize = True
        Me.lblBeneficiario.BackColor = System.Drawing.Color.Transparent
        Me.lblBeneficiario.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBeneficiario.ForeColor = System.Drawing.Color.Black
        Me.lblBeneficiario.Location = New System.Drawing.Point(24, 152)
        Me.lblBeneficiario.Name = "lblBeneficiario"
        Me.lblBeneficiario.Size = New System.Drawing.Size(89, 16)
        Me.lblBeneficiario.TabIndex = 205
        Me.lblBeneficiario.Text = "Beneficiario:"
        '
        'txtConcepto
        '
        Me.txtConcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtConcepto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConcepto.Location = New System.Drawing.Point(115, 126)
        Me.txtConcepto.MaxLength = 200
        Me.txtConcepto.Name = "txtConcepto"
        Me.txtConcepto.Size = New System.Drawing.Size(293, 22)
        Me.txtConcepto.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtConcepto, "Concepto general de la póliza.")
        '
        'txtBeneficiario
        '
        Me.txtBeneficiario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBeneficiario.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBeneficiario.Location = New System.Drawing.Point(115, 150)
        Me.txtBeneficiario.MaxLength = 200
        Me.txtBeneficiario.Name = "txtBeneficiario"
        Me.txtBeneficiario.Size = New System.Drawing.Size(293, 22)
        Me.txtBeneficiario.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtBeneficiario, "Beneficiario de la póliza.")
        '
        'grpDatosPoliza
        '
        Me.grpDatosPoliza.Controls.Add(Me.cmbtipopolizax)
        Me.grpDatosPoliza.Controls.Add(Me.Label11)
        Me.grpDatosPoliza.Controls.Add(Me.cmbClasificacionPoliza)
        Me.grpDatosPoliza.Controls.Add(Me.chkMantenerFecha)
        Me.grpDatosPoliza.Controls.Add(Me.Label1)
        Me.grpDatosPoliza.Controls.Add(Me.txtBeneficiario)
        Me.grpDatosPoliza.Controls.Add(Me.txtConcepto)
        Me.grpDatosPoliza.Controls.Add(Me.Label2)
        Me.grpDatosPoliza.Controls.Add(Me.lblBeneficiario)
        Me.grpDatosPoliza.Controls.Add(Me.txtNumeroPoliza)
        Me.grpDatosPoliza.Controls.Add(Me.Label4)
        Me.grpDatosPoliza.Controls.Add(Me.Label3)
        Me.grpDatosPoliza.Controls.Add(Me.dtpFecha)
        Me.grpDatosPoliza.ForeColor = System.Drawing.Color.Black
        Me.grpDatosPoliza.Location = New System.Drawing.Point(4, 5)
        Me.grpDatosPoliza.Name = "grpDatosPoliza"
        Me.grpDatosPoliza.Size = New System.Drawing.Size(421, 181)
        Me.grpDatosPoliza.TabIndex = 0
        Me.grpDatosPoliza.TabStop = False
        Me.grpDatosPoliza.Text = "Datos de la póliza"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(0, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(114, 16)
        Me.Label11.TabIndex = 207
        Me.Label11.Text = "Clasif. de póliza:"
        '
        'cmbClasificacionPoliza
        '
        Me.cmbClasificacionPoliza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClasificacionPoliza.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClasificacionPoliza.FormattingEnabled = True
        Me.cmbClasificacionPoliza.Items.AddRange(New Object() {"E  Egreso", "I   Ingreso", "D Diario", "A Apertura"})
        Me.cmbClasificacionPoliza.Location = New System.Drawing.Point(115, 21)
        Me.cmbClasificacionPoliza.Name = "cmbClasificacionPoliza"
        Me.cmbClasificacionPoliza.Size = New System.Drawing.Size(293, 24)
        Me.cmbClasificacionPoliza.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmbClasificacionPoliza, "Tipo de póliza.")
        '
        'chkMantenerFecha
        '
        Me.chkMantenerFecha.AutoSize = True
        Me.chkMantenerFecha.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMantenerFecha.Location = New System.Drawing.Point(210, 105)
        Me.chkMantenerFecha.Name = "chkMantenerFecha"
        Me.chkMantenerFecha.Size = New System.Drawing.Size(15, 14)
        Me.chkMantenerFecha.TabIndex = 6
        Me.chkMantenerFecha.TabStop = False
        Me.ToolTip1.SetToolTip(Me.chkMantenerFecha, "Mantener fecha")
        Me.chkMantenerFecha.UseVisualStyleBackColor = True
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
        Me.cmbProveedores.Size = New System.Drawing.Size(356, 24)
        Me.cmbProveedores.TabIndex = 219
        Me.ToolTip1.SetToolTip(Me.cmbProveedores, "Lista de proveedores.")
        '
        'grbBusqueda
        '
        Me.grbBusqueda.Controls.Add(Me.TextBox4)
        Me.grbBusqueda.Controls.Add(Me.Label20)
        Me.grbBusqueda.Controls.Add(Me.CheckBox1)
        Me.grbBusqueda.Controls.Add(Me.Label12)
        Me.grbBusqueda.Controls.Add(Me.cmbClasBusqueda)
        Me.grbBusqueda.Controls.Add(Me.lbltipo2)
        Me.grbBusqueda.Controls.Add(Me.cmbTipoBuscar)
        Me.grbBusqueda.Controls.Add(Me.Label8)
        Me.grbBusqueda.Controls.Add(Me.Label7)
        Me.grbBusqueda.Controls.Add(Me.dtpDesde)
        Me.grbBusqueda.Controls.Add(Me.dtpHasta)
        Me.grbBusqueda.Controls.Add(Me.txtBuscarConecpto)
        Me.grbBusqueda.Controls.Add(Me.Label6)
        Me.grbBusqueda.Controls.Add(Me.DataGridView1)
        Me.grbBusqueda.ForeColor = System.Drawing.Color.Black
        Me.grbBusqueda.Location = New System.Drawing.Point(428, 5)
        Me.grbBusqueda.Name = "grbBusqueda"
        Me.grbBusqueda.Size = New System.Drawing.Size(603, 181)
        Me.grbBusqueda.TabIndex = 32
        Me.grbBusqueda.TabStop = False
        Me.grbBusqueda.Text = "Búsqueda"
        '
        'TextBox4
        '
        Me.TextBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(72, 36)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(99, 22)
        Me.TextBox4.TabIndex = 216
        Me.ToolTip1.SetToolTip(Me.TextBox4, "Concepto general de la póliza a buscar.")
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(76, 17)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(68, 16)
        Me.Label20.TabIndex = 217
        Me.Label20.Text = "Concepto"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckBox1.Location = New System.Drawing.Point(499, 12)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(104, 18)
        Me.CheckBox1.TabIndex = 208
        Me.CheckBox1.TabStop = False
        Me.CheckBox1.Text = "Descuadradas"
        Me.ToolTip1.SetToolTip(Me.CheckBox1, "Mostrar solo descuadradas")
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(438, 13)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 16)
        Me.Label12.TabIndex = 215
        Me.Label12.Text = "Clasif."
        '
        'cmbClasBusqueda
        '
        Me.cmbClasBusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClasBusqueda.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbClasBusqueda.FormattingEnabled = True
        Me.cmbClasBusqueda.Items.AddRange(New Object() {"E  Egreso", "I   Ingreso", "D Diario", "A Apertura"})
        Me.cmbClasBusqueda.Location = New System.Drawing.Point(434, 34)
        Me.cmbClasBusqueda.Name = "cmbClasBusqueda"
        Me.cmbClasBusqueda.Size = New System.Drawing.Size(161, 24)
        Me.cmbClasBusqueda.TabIndex = 208
        Me.ToolTip1.SetToolTip(Me.cmbClasBusqueda, "Tipo de póliza.")
        '
        'lbltipo2
        '
        Me.lbltipo2.AutoSize = True
        Me.lbltipo2.BackColor = System.Drawing.Color.Transparent
        Me.lbltipo2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltipo2.ForeColor = System.Drawing.Color.Black
        Me.lbltipo2.Location = New System.Drawing.Point(180, 17)
        Me.lbltipo2.Name = "lbltipo2"
        Me.lbltipo2.Size = New System.Drawing.Size(36, 16)
        Me.lbltipo2.TabIndex = 214
        Me.lbltipo2.Text = "Tipo"
        '
        'cmbTipoBuscar
        '
        Me.cmbTipoBuscar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoBuscar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbTipoBuscar.FormattingEnabled = True
        Me.cmbTipoBuscar.Location = New System.Drawing.Point(176, 35)
        Me.cmbTipoBuscar.Name = "cmbTipoBuscar"
        Me.cmbTipoBuscar.Size = New System.Drawing.Size(76, 24)
        Me.cmbTipoBuscar.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbTipoBuscar, "Tipo de póliza.")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(349, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 16)
        Me.Label8.TabIndex = 213
        Me.Label8.Text = "Hasta"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(261, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 16)
        Me.Label7.TabIndex = 212
        Me.Label7.Text = "Desde"
        '
        'dtpDesde
        '
        Me.dtpDesde.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesde.Location = New System.Drawing.Point(257, 35)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(84, 22)
        Me.dtpDesde.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpDesde, "Fecha inicial de búsqueda.")
        '
        'dtpHasta
        '
        Me.dtpHasta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(345, 35)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(85, 22)
        Me.dtpHasta.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpHasta, "Fecha final de búsqueda.")
        '
        'txtBuscarConecpto
        '
        Me.txtBuscarConecpto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBuscarConecpto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuscarConecpto.Location = New System.Drawing.Point(9, 36)
        Me.txtBuscarConecpto.Name = "txtBuscarConecpto"
        Me.txtBuscarConecpto.Size = New System.Drawing.Size(59, 22)
        Me.txtBuscarConecpto.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtBuscarConecpto, "Concepto general de la póliza a buscar.")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(10, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 16)
        Me.Label6.TabIndex = 208
        Me.Label6.Text = "Número"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridView1.Location = New System.Drawing.Point(9, 60)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(590, 111)
        Me.DataGridView1.TabIndex = 10
        Me.DataGridView1.TabStop = False
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnCerrar.Location = New System.Drawing.Point(10, 525)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(75, 25)
        Me.btnCerrar.TabIndex = 24
        Me.btnCerrar.Text = "Cerrar"
        Me.ToolTip1.SetToolTip(Me.btnCerrar, "Cerrar ventana.")
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'btnGuardarPoliza
        '
        Me.btnGuardarPoliza.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardarPoliza.Location = New System.Drawing.Point(426, 469)
        Me.btnGuardarPoliza.Name = "btnGuardarPoliza"
        Me.btnGuardarPoliza.Size = New System.Drawing.Size(200, 51)
        Me.btnGuardarPoliza.TabIndex = 22
        Me.btnGuardarPoliza.Text = "Guardar (F10)"
        Me.ToolTip1.SetToolTip(Me.btnGuardarPoliza, "Guardar póliza.")
        Me.btnGuardarPoliza.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnNuevo.Location = New System.Drawing.Point(844, 525)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(104, 25)
        Me.btnNuevo.TabIndex = 30
        Me.btnNuevo.Text = "Nueva póliza"
        Me.ToolTip1.SetToolTip(Me.btnNuevo, "Nueva póliza.")
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.Enabled = False
        Me.btnEliminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnEliminar.Location = New System.Drawing.Point(736, 525)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(104, 25)
        Me.btnEliminar.TabIndex = 29
        Me.btnEliminar.Text = "Eliminar póliza"
        Me.ToolTip1.SetToolTip(Me.btnEliminar, "Eliminar póliza.")
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnEliminarDetalle
        '
        Me.btnEliminarDetalle.Enabled = False
        Me.btnEliminarDetalle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarDetalle.Location = New System.Drawing.Point(892, 202)
        Me.btnEliminarDetalle.Name = "btnEliminarDetalle"
        Me.btnEliminarDetalle.Size = New System.Drawing.Size(68, 27)
        Me.btnEliminarDetalle.TabIndex = 19
        Me.btnEliminarDetalle.Text = "Eliminar"
        Me.ToolTip1.SetToolTip(Me.btnEliminarDetalle, "Eliminar renglón.")
        Me.btnEliminarDetalle.UseVisualStyleBackColor = True
        '
        'btnNuevoDetalle
        '
        Me.btnNuevoDetalle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevoDetalle.Location = New System.Drawing.Point(964, 202)
        Me.btnNuevoDetalle.Name = "btnNuevoDetalle"
        Me.btnNuevoDetalle.Size = New System.Drawing.Size(68, 27)
        Me.btnNuevoDetalle.TabIndex = 20
        Me.btnNuevoDetalle.Text = "Nuevo"
        Me.ToolTip1.SetToolTip(Me.btnNuevoDetalle, "Nuevo renglón.")
        Me.btnNuevoDetalle.UseVisualStyleBackColor = True
        '
        'txtCargo
        '
        Me.txtCargo.BackColor = System.Drawing.Color.White
        Me.txtCargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCargo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCargo.Location = New System.Drawing.Point(646, 206)
        Me.txtCargo.MaxLength = 80
        Me.txtCargo.Name = "txtCargo"
        Me.txtCargo.Size = New System.Drawing.Size(80, 22)
        Me.txtCargo.TabIndex = 16
        Me.txtCargo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtCargo, "Cargo.")
        '
        'txtAbono
        '
        Me.txtAbono.BackColor = System.Drawing.Color.White
        Me.txtAbono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAbono.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAbono.Location = New System.Drawing.Point(731, 206)
        Me.txtAbono.MaxLength = 80
        Me.txtAbono.Name = "txtAbono"
        Me.txtAbono.Size = New System.Drawing.Size(80, 22)
        Me.txtAbono.TabIndex = 17
        Me.txtAbono.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtAbono, "Abono.")
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcion.ForeColor = System.Drawing.Color.Black
        Me.lblDescripcion.Location = New System.Drawing.Point(9, 445)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(39, 14)
        Me.lblDescripcion.TabIndex = 200
        Me.lblDescripcion.Text = "Label5"
        '
        'txtN5
        '
        Me.txtN5.BackColor = System.Drawing.Color.White
        Me.txtN5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtN5.Enabled = False
        Me.txtN5.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.txtN5.Location = New System.Drawing.Point(223, 209)
        Me.txtN5.MaxLength = 29
        Me.txtN5.Name = "txtN5"
        Me.txtN5.Size = New System.Drawing.Size(45, 15)
        Me.txtN5.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtN5, "¿Quiéres buscar una Cuenta? Pulsa F1 .")
        '
        'txtN4
        '
        Me.txtN4.BackColor = System.Drawing.Color.White
        Me.txtN4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtN4.Enabled = False
        Me.txtN4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.txtN4.Location = New System.Drawing.Point(172, 209)
        Me.txtN4.MaxLength = 29
        Me.txtN4.Name = "txtN4"
        Me.txtN4.Size = New System.Drawing.Size(45, 15)
        Me.txtN4.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtN4, "¿Quiéres buscar una Cuenta? Pulsa F1 .")
        '
        'txtN3
        '
        Me.txtN3.BackColor = System.Drawing.Color.White
        Me.txtN3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtN3.Enabled = False
        Me.txtN3.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.txtN3.Location = New System.Drawing.Point(121, 209)
        Me.txtN3.MaxLength = 29
        Me.txtN3.Name = "txtN3"
        Me.txtN3.Size = New System.Drawing.Size(45, 15)
        Me.txtN3.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtN3, "¿Quiéres buscar una Cuenta? Pulsa F1 .")
        '
        'txtN2
        '
        Me.txtN2.BackColor = System.Drawing.Color.White
        Me.txtN2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtN2.Enabled = False
        Me.txtN2.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.txtN2.Location = New System.Drawing.Point(67, 209)
        Me.txtN2.MaxLength = 29
        Me.txtN2.Name = "txtN2"
        Me.txtN2.Size = New System.Drawing.Size(45, 15)
        Me.txtN2.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtN2, "¿Quiéres buscar una Cuenta? Pulsa F1 .")
        '
        'txtCuenta
        '
        Me.txtCuenta.BackColor = System.Drawing.Color.White
        Me.txtCuenta.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCuenta.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.txtCuenta.Location = New System.Drawing.Point(16, 209)
        Me.txtCuenta.MaxLength = 29
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(45, 15)
        Me.txtCuenta.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtCuenta, "¿Quiéres buscar una Cuenta? Pulsa F1 .")
        '
        'btnImprimir
        '
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnImprimir.Location = New System.Drawing.Point(954, 525)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(75, 25)
        Me.btnImprimir.TabIndex = 31
        Me.btnImprimir.Text = "Imprimir"
        Me.ToolTip1.SetToolTip(Me.btnImprimir, "Imprimir póliza.")
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'txtFolioFactura
        '
        Me.txtFolioFactura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFolioFactura.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolioFactura.Location = New System.Drawing.Point(2, 18)
        Me.txtFolioFactura.MaxLength = 45
        Me.txtFolioFactura.Name = "txtFolioFactura"
        Me.txtFolioFactura.Size = New System.Drawing.Size(59, 22)
        Me.txtFolioFactura.TabIndex = 218
        Me.ToolTip1.SetToolTip(Me.txtFolioFactura, "Folio de la factura.")
        '
        'txtIVA
        '
        Me.txtIVA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIVA.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIVA.Location = New System.Drawing.Point(425, 18)
        Me.txtIVA.MaxLength = 2
        Me.txtIVA.Name = "txtIVA"
        Me.txtIVA.Size = New System.Drawing.Size(61, 22)
        Me.txtIVA.TabIndex = 220
        Me.txtIVA.Text = "16"
        Me.txtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtIVA, "IVA")
        '
        'txtCocepFac
        '
        Me.txtCocepFac.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCocepFac.Location = New System.Drawing.Point(723, 18)
        Me.txtCocepFac.MaxLength = 60
        Me.txtCocepFac.Name = "txtCocepFac"
        Me.txtCocepFac.Size = New System.Drawing.Size(29, 20)
        Me.txtCocepFac.TabIndex = 222
        Me.ToolTip1.SetToolTip(Me.txtCocepFac, "Nombre de la Factura.")
        Me.txtCocepFac.Visible = False
        '
        'btnImportar
        '
        Me.btnImportar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnImportar.Location = New System.Drawing.Point(91, 525)
        Me.btnImportar.Name = "btnImportar"
        Me.btnImportar.Size = New System.Drawing.Size(75, 25)
        Me.btnImportar.TabIndex = 25
        Me.btnImportar.Text = "Importar"
        Me.ToolTip1.SetToolTip(Me.btnImportar, "Importar")
        Me.btnImportar.UseVisualStyleBackColor = True
        '
        'chkAgregarComprobante
        '
        Me.chkAgregarComprobante.AutoSize = True
        Me.chkAgregarComprobante.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgregarComprobante.ForeColor = System.Drawing.Color.Black
        Me.chkAgregarComprobante.Location = New System.Drawing.Point(424, 529)
        Me.chkAgregarComprobante.Name = "chkAgregarComprobante"
        Me.chkAgregarComprobante.Size = New System.Drawing.Size(150, 18)
        Me.chkAgregarComprobante.TabIndex = 28
        Me.chkAgregarComprobante.Text = "Comprobantes al agregar."
        Me.ToolTip1.SetToolTip(Me.chkAgregarComprobante, "Active si desea abrir la ventana de agregar comprobantes de manera automática al " & _
        "terminar un renglón.")
        Me.chkAgregarComprobante.UseVisualStyleBackColor = True
        '
        'btnXML
        '
        Me.btnXML.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnXML.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnXML.Location = New System.Drawing.Point(345, 525)
        Me.btnXML.Name = "btnXML"
        Me.btnXML.Size = New System.Drawing.Size(75, 25)
        Me.btnXML.TabIndex = 27
        Me.btnXML.Text = "XML"
        Me.ToolTip1.SetToolTip(Me.btnXML, "Nueva cuenta")
        Me.btnXML.UseVisualStyleBackColor = True
        '
        'txtValorActos
        '
        Me.txtValorActos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtValorActos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorActos.Location = New System.Drawing.Point(617, 18)
        Me.txtValorActos.MaxLength = 40
        Me.txtValorActos.Name = "txtValorActos"
        Me.txtValorActos.Size = New System.Drawing.Size(75, 22)
        Me.txtValorActos.TabIndex = 223
        Me.txtValorActos.Text = "0.00"
        Me.txtValorActos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtValorActos, "Valor Actos")
        Me.txtValorActos.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(697, 18)
        Me.TextBox1.MaxLength = 200
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(319, 22)
        Me.TextBox1.TabIndex = 224
        Me.ToolTip1.SetToolTip(Me.TextBox1, "Referencia")
        '
        'TextBox2
        '
        Me.TextBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(489, 18)
        Me.TextBox2.MaxLength = 2
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(61, 22)
        Me.TextBox2.TabIndex = 221
        Me.TextBox2.Text = "0"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.TextBox2, "IVA")
        '
        'TextBox3
        '
        Me.TextBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(553, 18)
        Me.TextBox3.MaxLength = 2
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(61, 22)
        Me.TextBox3.TabIndex = 222
        Me.TextBox3.Text = "0"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.TextBox3, "IVA")
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Button1.Location = New System.Drawing.Point(577, 525)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(156, 25)
        Me.Button1.TabIndex = 260
        Me.Button1.Text = "Guardar como nueva."
        Me.ToolTip1.SetToolTip(Me.Button1, "Crear desde...")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblN2
        '
        Me.lblN2.AutoSize = True
        Me.lblN2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN2.ForeColor = System.Drawing.Color.Black
        Me.lblN2.Location = New System.Drawing.Point(16, 460)
        Me.lblN2.Name = "lblN2"
        Me.lblN2.Size = New System.Drawing.Size(39, 14)
        Me.lblN2.TabIndex = 201
        Me.lblN2.Text = "Label5"
        '
        'lblN3
        '
        Me.lblN3.AutoSize = True
        Me.lblN3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN3.ForeColor = System.Drawing.Color.Black
        Me.lblN3.Location = New System.Drawing.Point(23, 475)
        Me.lblN3.Name = "lblN3"
        Me.lblN3.Size = New System.Drawing.Size(39, 14)
        Me.lblN3.TabIndex = 202
        Me.lblN3.Text = "Label5"
        '
        'lblN4
        '
        Me.lblN4.AutoSize = True
        Me.lblN4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN4.ForeColor = System.Drawing.Color.Black
        Me.lblN4.Location = New System.Drawing.Point(31, 491)
        Me.lblN4.Name = "lblN4"
        Me.lblN4.Size = New System.Drawing.Size(39, 14)
        Me.lblN4.TabIndex = 203
        Me.lblN4.Text = "Label5"
        '
        'lblN5
        '
        Me.lblN5.AutoSize = True
        Me.lblN5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblN5.ForeColor = System.Drawing.Color.Black
        Me.lblN5.Location = New System.Drawing.Point(38, 506)
        Me.lblN5.Name = "lblN5"
        Me.lblN5.Size = New System.Drawing.Size(39, 14)
        Me.lblN5.TabIndex = 204
        Me.lblN5.Text = "Label5"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(96, 186)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(53, 16)
        Me.Label25.TabIndex = 212
        Me.Label25.Text = "Cuenta"
        '
        'PrintDocument1
        '
        '
        'pnlDatosIVA
        '
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
        Me.pnlDatosIVA.Location = New System.Drawing.Point(10, 229)
        Me.pnlDatosIVA.Name = "pnlDatosIVA"
        Me.pnlDatosIVA.Size = New System.Drawing.Size(1021, 42)
        Me.pnlDatosIVA.TabIndex = 213
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(557, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 16)
        Me.Label19.TabIndex = 229
        Me.Label19.Text = "%IEPS"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(489, 0)
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
        Me.Label13.Location = New System.Drawing.Point(699, 2)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 16)
        Me.Label13.TabIndex = 226
        Me.Label13.Text = "Referencia"
        '
        'lblValorActos
        '
        Me.lblValorActos.AutoSize = True
        Me.lblValorActos.BackColor = System.Drawing.Color.Transparent
        Me.lblValorActos.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValorActos.ForeColor = System.Drawing.Color.Black
        Me.lblValorActos.Location = New System.Drawing.Point(615, 0)
        Me.lblValorActos.Name = "lblValorActos"
        Me.lblValorActos.Size = New System.Drawing.Size(78, 16)
        Me.lblValorActos.TabIndex = 224
        Me.lblValorActos.Text = "Valor actos"
        Me.lblValorActos.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(438, 0)
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
        'btnComprobantes
        '
        Me.btnComprobantes.Enabled = False
        Me.btnComprobantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.btnComprobantes.Location = New System.Drawing.Point(171, 525)
        Me.btnComprobantes.Name = "btnComprobantes"
        Me.btnComprobantes.Size = New System.Drawing.Size(170, 25)
        Me.btnComprobantes.TabIndex = 26
        Me.btnComprobantes.Text = "Agregar Comprobantes"
        Me.btnComprobantes.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "Archivos XML |*.xml"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(12, 205)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(260, 25)
        Me.PictureBox1.TabIndex = 206
        Me.PictureBox1.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Black
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Location = New System.Drawing.Point(62, 209)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(2, 15)
        Me.Label14.TabIndex = 256
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Black
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Location = New System.Drawing.Point(117, 209)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(2, 15)
        Me.Label15.TabIndex = 257
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Black
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Location = New System.Drawing.Point(168, 209)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(2, 15)
        Me.Label16.TabIndex = 258
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Black
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Location = New System.Drawing.Point(219, 209)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(2, 15)
        Me.Label17.TabIndex = 259
        '
        'cmbtipopolizax
        '
        Me.cmbtipopolizax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtipopolizax.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbtipopolizax.FormattingEnabled = True
        Me.cmbtipopolizax.Location = New System.Drawing.Point(115, 47)
        Me.cmbtipopolizax.Name = "cmbtipopolizax"
        Me.cmbtipopolizax.Size = New System.Drawing.Size(76, 24)
        Me.cmbtipopolizax.TabIndex = 218
        Me.ToolTip1.SetToolTip(Me.cmbtipopolizax, "Tipo de póliza.")
        '
        'frmContabilidadPolizas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1036, 562)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtN5)
        Me.Controls.Add(Me.txtN4)
        Me.Controls.Add(Me.txtN3)
        Me.Controls.Add(Me.txtN2)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.txtCuenta)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtAbono)
        Me.Controls.Add(Me.txtCargo)
        Me.Controls.Add(Me.btnNuevoDetalle)
        Me.Controls.Add(Me.btnEliminarDetalle)
        Me.Controls.Add(Me.btnAgregarDetalle)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.btnXML)
        Me.Controls.Add(Me.chkAgregarComprobante)
        Me.Controls.Add(Me.btnComprobantes)
        Me.Controls.Add(Me.btnImportar)
        Me.Controls.Add(Me.pnlDatosIVA)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.lblN5)
        Me.Controls.Add(Me.lblN4)
        Me.Controls.Add(Me.lblN3)
        Me.Controls.Add(Me.lblN2)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.grbBusqueda)
        Me.Controls.Add(Me.btnGuardarPoliza)
        Me.Controls.Add(Me.grpDatosPoliza)
        Me.Controls.Add(Me.btnNuevo)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.lblDiferencia)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.lblAbonos)
        Me.Controls.Add(Me.lblCargos)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.dgvCuentas)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmContabilidadPolizas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Polizas"
        CType(Me.dgvCuentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDatosPoliza.ResumeLayout(False)
        Me.grpDatosPoliza.PerformLayout()
        Me.grbBusqueda.ResumeLayout(False)
        Me.grbBusqueda.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDatosIVA.ResumeLayout(False)
        Me.pnlDatosIVA.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDiferencia As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblAbonos As System.Windows.Forms.Label
    Friend WithEvents lblCargos As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents dgvCuentas As System.Windows.Forms.DataGridView
    Friend WithEvents btnAgregarDetalle As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNumeroPoliza As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblBeneficiario As System.Windows.Forms.Label
    Friend WithEvents txtConcepto As System.Windows.Forms.TextBox
    Friend WithEvents txtBeneficiario As System.Windows.Forms.TextBox
    Friend WithEvents grpDatosPoliza As System.Windows.Forms.GroupBox
    Friend WithEvents grbBusqueda As System.Windows.Forms.GroupBox
    Friend WithEvents txtBuscarConecpto As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents btnGuardarPoliza As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnEliminarDetalle As System.Windows.Forms.Button
    Friend WithEvents btnNuevoDetalle As System.Windows.Forms.Button
    Friend WithEvents txtCargo As System.Windows.Forms.TextBox
    Friend WithEvents txtAbono As System.Windows.Forms.TextBox
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkMantenerFecha As System.Windows.Forms.CheckBox
    Friend WithEvents lblN2 As System.Windows.Forms.Label
    Friend WithEvents lblN3 As System.Windows.Forms.Label
    Friend WithEvents lblN4 As System.Windows.Forms.Label
    Friend WithEvents lblN5 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents txtN5 As System.Windows.Forms.TextBox
    Friend WithEvents txtN4 As System.Windows.Forms.TextBox
    Friend WithEvents txtN3 As System.Windows.Forms.TextBox
    Friend WithEvents txtN2 As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtCuenta As System.Windows.Forms.TextBox
    Friend WithEvents lbltipo2 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoBuscar As System.Windows.Forms.ComboBox
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents pnlDatosIVA As System.Windows.Forms.Panel
    Friend WithEvents txtFolioFactura As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCocepFac As System.Windows.Forms.TextBox
    Friend WithEvents txtIVA As System.Windows.Forms.TextBox
    Friend WithEvents btnImportar As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Private WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnComprobantes As System.Windows.Forms.Button
    Friend WithEvents chkAgregarComprobante As System.Windows.Forms.CheckBox
    Friend WithEvents btnXML As System.Windows.Forms.Button
    Friend WithEvents lblValorActos As System.Windows.Forms.Label
    Friend WithEvents txtValorActos As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbClasificacionPoliza As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbClasBusqueda As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProveedores As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbtipopolizax As System.Windows.Forms.ComboBox
End Class
