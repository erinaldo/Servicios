<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmContabilidadDIOT
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvCompro = New System.Windows.Forms.DataGridView()
        Me.Selec = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RFC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Prov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Valor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.iva1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IVA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IVAret = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ivaretc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IEPSpor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IEPScan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TipoPoliza = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Referencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaDiot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ivaretLabel20 = New System.Windows.Forms.Label()
        Me.ivaretLabel19 = New System.Windows.Forms.Label()
        Me.ivaLabel18 = New System.Windows.Forms.Label()
        Me.ivaLabel17 = New System.Windows.Forms.Label()
        Me.ivaretLabel15 = New System.Windows.Forms.Label()
        Me.ivaLabel14 = New System.Windows.Forms.Label()
        Me.vaLabel13 = New System.Windows.Forms.Label()
        Me.vaLabel12 = New System.Windows.Forms.Label()
        Me.vaLabel11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.iepsLabel22 = New System.Windows.Forms.Label()
        Me.iepsLabel21 = New System.Windows.Forms.Label()
        Me.iepsLabel16 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.btnCambiar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.dgvCompro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvCompro
        '
        Me.dgvCompro.AllowUserToAddRows = False
        Me.dgvCompro.AllowUserToDeleteRows = False
        Me.dgvCompro.AllowUserToResizeColumns = False
        Me.dgvCompro.AllowUserToResizeRows = False
        Me.dgvCompro.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCompro.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvCompro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCompro.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Selec, Me.ID, Me.Cod, Me.RFC, Me.Prov, Me.Valor, Me.iva1, Me.IVA, Me.IVAret, Me.ivaretc, Me.IEPSpor, Me.IEPScan, Me.TipoPoliza, Me.No, Me.Referencia, Me.Fecha, Me.FechaDiot})
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCompro.DefaultCellStyle = DataGridViewCellStyle18
        Me.dgvCompro.GridColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.dgvCompro.Location = New System.Drawing.Point(1, 50)
        Me.dgvCompro.MultiSelect = False
        Me.dgvCompro.Name = "dgvCompro"
        Me.dgvCompro.ReadOnly = True
        Me.dgvCompro.RowHeadersVisible = False
        Me.dgvCompro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCompro.Size = New System.Drawing.Size(1002, 295)
        Me.dgvCompro.TabIndex = 252
        Me.dgvCompro.TabStop = False
        '
        'Selec
        '
        Me.Selec.DataPropertyName = "Selec"
        Me.Selec.FalseValue = "0"
        Me.Selec.HeaderText = "Selec."
        Me.Selec.Name = "Selec"
        Me.Selec.ReadOnly = True
        Me.Selec.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Selec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Selec.TrueValue = "1"
        Me.Selec.Width = 40
        '
        'ID
        '
        Me.ID.DataPropertyName = "ID"
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Visible = False
        '
        'Cod
        '
        Me.Cod.DataPropertyName = "Cod"
        Me.Cod.HeaderText = "Código"
        Me.Cod.Name = "Cod"
        Me.Cod.ReadOnly = True
        Me.Cod.Width = 65
        '
        'RFC
        '
        Me.RFC.DataPropertyName = "RFC"
        Me.RFC.HeaderText = "RFC"
        Me.RFC.Name = "RFC"
        Me.RFC.ReadOnly = True
        Me.RFC.Width = 130
        '
        'Prov
        '
        Me.Prov.DataPropertyName = "Prov"
        Me.Prov.HeaderText = "Proveedor"
        Me.Prov.Name = "Prov"
        Me.Prov.ReadOnly = True
        Me.Prov.Width = 200
        '
        'Valor
        '
        Me.Valor.DataPropertyName = "Valor"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        DataGridViewCellStyle11.Format = "N2"
        DataGridViewCellStyle11.NullValue = Nothing
        Me.Valor.DefaultCellStyle = DataGridViewCellStyle11
        Me.Valor.HeaderText = "Valor Actos"
        Me.Valor.Name = "Valor"
        Me.Valor.ReadOnly = True
        '
        'iva1
        '
        Me.iva1.DataPropertyName = "ivaP"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.iva1.DefaultCellStyle = DataGridViewCellStyle12
        Me.iva1.HeaderText = "%IVA"
        Me.iva1.Name = "iva1"
        Me.iva1.ReadOnly = True
        Me.iva1.Width = 50
        '
        'IVA
        '
        Me.IVA.DataPropertyName = "iva"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        DataGridViewCellStyle13.Format = "N2"
        DataGridViewCellStyle13.NullValue = Nothing
        Me.IVA.DefaultCellStyle = DataGridViewCellStyle13
        Me.IVA.HeaderText = "IVA"
        Me.IVA.Name = "IVA"
        Me.IVA.ReadOnly = True
        Me.IVA.Width = 80
        '
        'IVAret
        '
        Me.IVAret.DataPropertyName = "ivaret"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.IVAret.DefaultCellStyle = DataGridViewCellStyle14
        Me.IVAret.HeaderText = "%IVARet"
        Me.IVAret.Name = "IVAret"
        Me.IVAret.ReadOnly = True
        Me.IVAret.Width = 70
        '
        'ivaretc
        '
        Me.ivaretc.DataPropertyName = "ivaretc"
        Me.ivaretc.HeaderText = "IVA Ret"
        Me.ivaretc.Name = "ivaretc"
        Me.ivaretc.ReadOnly = True
        Me.ivaretc.Width = 80
        '
        'IEPSpor
        '
        Me.IEPSpor.DataPropertyName = "ieps"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.IEPSpor.DefaultCellStyle = DataGridViewCellStyle15
        Me.IEPSpor.HeaderText = "%IEPS"
        Me.IEPSpor.Name = "IEPSpor"
        Me.IEPSpor.ReadOnly = True
        Me.IEPSpor.Visible = False
        Me.IEPSpor.Width = 50
        '
        'IEPScan
        '
        Me.IEPScan.DataPropertyName = "iepsc"
        Me.IEPScan.HeaderText = "IEPS"
        Me.IEPScan.Name = "IEPScan"
        Me.IEPScan.ReadOnly = True
        Me.IEPScan.Visible = False
        Me.IEPScan.Width = 80
        '
        'TipoPoliza
        '
        Me.TipoPoliza.DataPropertyName = "tPoliza"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.TipoPoliza.DefaultCellStyle = DataGridViewCellStyle16
        Me.TipoPoliza.HeaderText = "Tipo"
        Me.TipoPoliza.Name = "TipoPoliza"
        Me.TipoPoliza.ReadOnly = True
        Me.TipoPoliza.Width = 50
        '
        'No
        '
        Me.No.DataPropertyName = "nPoliza"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        Me.No.DefaultCellStyle = DataGridViewCellStyle17
        Me.No.HeaderText = "No."
        Me.No.Name = "No"
        Me.No.ReadOnly = True
        Me.No.Width = 50
        '
        'Referencia
        '
        Me.Referencia.DataPropertyName = "referencia"
        Me.Referencia.HeaderText = "Ref"
        Me.Referencia.Name = "Referencia"
        Me.Referencia.ReadOnly = True
        '
        'Fecha
        '
        Me.Fecha.DataPropertyName = "Fecha"
        Me.Fecha.HeaderText = "Fecha"
        Me.Fecha.Name = "Fecha"
        Me.Fecha.ReadOnly = True
        Me.Fecha.Width = 80
        '
        'FechaDiot
        '
        Me.FechaDiot.DataPropertyName = "fechadiot"
        Me.FechaDiot.HeaderText = "Column1"
        Me.FechaDiot.Name = "FechaDiot"
        Me.FechaDiot.ReadOnly = True
        Me.FechaDiot.Visible = False
        '
        'dtpDesde
        '
        Me.dtpDesde.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDesde.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesde.Location = New System.Drawing.Point(90, 22)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(85, 22)
        Me.dtpDesde.TabIndex = 253
        '
        'dtpHasta
        '
        Me.dtpHasta.CalendarFont = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHasta.Enabled = False
        Me.dtpHasta.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(236, 22)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(83, 22)
        Me.dtpHasta.TabIndex = 254
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(36, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 16)
        Me.Label2.TabIndex = 257
        Me.Label2.Text = "Desde:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(186, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 16)
        Me.Label3.TabIndex = 258
        Me.Label3.Text = "Hasta:"
        '
        'btnGuardar
        '
        Me.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnGuardar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(895, 15)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(79, 29)
        Me.btnGuardar.TabIndex = 259
        Me.btnGuardar.Text = "Cerrar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(33, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(173, 16)
        Me.Label4.TabIndex = 260
        Me.Label4.Text = "TOTAL DE OPERACIONES:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(35, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(171, 16)
        Me.Label6.TabIndex = 266
        Me.Label6.Text = "( - )  OPER. EN TRÁNSITO:"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.ivaretLabel20)
        Me.Panel1.Controls.Add(Me.ivaretLabel19)
        Me.Panel1.Controls.Add(Me.ivaLabel18)
        Me.Panel1.Controls.Add(Me.ivaLabel17)
        Me.Panel1.Controls.Add(Me.ivaretLabel15)
        Me.Panel1.Controls.Add(Me.ivaLabel14)
        Me.Panel1.Controls.Add(Me.vaLabel13)
        Me.Panel1.Controls.Add(Me.vaLabel12)
        Me.Panel1.Controls.Add(Me.vaLabel11)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(390, 357)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(584, 123)
        Me.Panel1.TabIndex = 269
        '
        'ivaretLabel20
        '
        Me.ivaretLabel20.BackColor = System.Drawing.Color.Transparent
        Me.ivaretLabel20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ivaretLabel20.ForeColor = System.Drawing.Color.Black
        Me.ivaretLabel20.Location = New System.Drawing.Point(428, 30)
        Me.ivaretLabel20.Name = "ivaretLabel20"
        Me.ivaretLabel20.Size = New System.Drawing.Size(105, 15)
        Me.ivaretLabel20.TabIndex = 295
        Me.ivaretLabel20.Text = "000,000,000.00"
        Me.ivaretLabel20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ivaretLabel19
        '
        Me.ivaretLabel19.BackColor = System.Drawing.Color.Transparent
        Me.ivaretLabel19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ivaretLabel19.ForeColor = System.Drawing.Color.Black
        Me.ivaretLabel19.Location = New System.Drawing.Point(428, 54)
        Me.ivaretLabel19.Name = "ivaretLabel19"
        Me.ivaretLabel19.Size = New System.Drawing.Size(105, 15)
        Me.ivaretLabel19.TabIndex = 294
        Me.ivaretLabel19.Text = "000,000,000.00"
        Me.ivaretLabel19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ivaLabel18
        '
        Me.ivaLabel18.BackColor = System.Drawing.Color.Transparent
        Me.ivaLabel18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ivaLabel18.ForeColor = System.Drawing.Color.Black
        Me.ivaLabel18.Location = New System.Drawing.Point(320, 30)
        Me.ivaLabel18.Name = "ivaLabel18"
        Me.ivaLabel18.Size = New System.Drawing.Size(105, 15)
        Me.ivaLabel18.TabIndex = 293
        Me.ivaLabel18.Text = "000,000,000.00"
        Me.ivaLabel18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ivaLabel17
        '
        Me.ivaLabel17.BackColor = System.Drawing.Color.Transparent
        Me.ivaLabel17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ivaLabel17.ForeColor = System.Drawing.Color.Black
        Me.ivaLabel17.Location = New System.Drawing.Point(320, 54)
        Me.ivaLabel17.Name = "ivaLabel17"
        Me.ivaLabel17.Size = New System.Drawing.Size(105, 15)
        Me.ivaLabel17.TabIndex = 292
        Me.ivaLabel17.Text = "000,000,000.00"
        Me.ivaLabel17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ivaretLabel15
        '
        Me.ivaretLabel15.BackColor = System.Drawing.Color.Transparent
        Me.ivaretLabel15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ivaretLabel15.ForeColor = System.Drawing.Color.Black
        Me.ivaretLabel15.Location = New System.Drawing.Point(428, 86)
        Me.ivaretLabel15.Name = "ivaretLabel15"
        Me.ivaretLabel15.Size = New System.Drawing.Size(105, 15)
        Me.ivaretLabel15.TabIndex = 290
        Me.ivaretLabel15.Text = "000,000,000.00"
        Me.ivaretLabel15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ivaLabel14
        '
        Me.ivaLabel14.BackColor = System.Drawing.Color.Transparent
        Me.ivaLabel14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ivaLabel14.ForeColor = System.Drawing.Color.Black
        Me.ivaLabel14.Location = New System.Drawing.Point(320, 86)
        Me.ivaLabel14.Name = "ivaLabel14"
        Me.ivaLabel14.Size = New System.Drawing.Size(105, 15)
        Me.ivaLabel14.TabIndex = 289
        Me.ivaLabel14.Text = "000,000,000.00"
        Me.ivaLabel14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'vaLabel13
        '
        Me.vaLabel13.BackColor = System.Drawing.Color.Transparent
        Me.vaLabel13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vaLabel13.ForeColor = System.Drawing.Color.Black
        Me.vaLabel13.Location = New System.Drawing.Point(209, 30)
        Me.vaLabel13.Name = "vaLabel13"
        Me.vaLabel13.Size = New System.Drawing.Size(105, 15)
        Me.vaLabel13.TabIndex = 288
        Me.vaLabel13.Text = "000,000,000.00"
        Me.vaLabel13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'vaLabel12
        '
        Me.vaLabel12.BackColor = System.Drawing.Color.Transparent
        Me.vaLabel12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vaLabel12.ForeColor = System.Drawing.Color.Black
        Me.vaLabel12.Location = New System.Drawing.Point(209, 54)
        Me.vaLabel12.Name = "vaLabel12"
        Me.vaLabel12.Size = New System.Drawing.Size(105, 15)
        Me.vaLabel12.TabIndex = 287
        Me.vaLabel12.Text = "000,000,000.00"
        Me.vaLabel12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'vaLabel11
        '
        Me.vaLabel11.BackColor = System.Drawing.Color.Transparent
        Me.vaLabel11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vaLabel11.ForeColor = System.Drawing.Color.Black
        Me.vaLabel11.Location = New System.Drawing.Point(209, 86)
        Me.vaLabel11.Name = "vaLabel11"
        Me.vaLabel11.Size = New System.Drawing.Size(105, 15)
        Me.vaLabel11.TabIndex = 286
        Me.vaLabel11.Text = "000,000,000.00"
        Me.vaLabel11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(455, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 16)
        Me.Label1.TabIndex = 281
        Me.Label1.Text = "IVA Ret"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(61, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(145, 16)
        Me.Label5.TabIndex = 279
        Me.Label5.Text = "TOTAL OPER. P/DIOT:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(209, 69)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(337, 16)
        Me.Label9.TabIndex = 272
        Me.Label9.Text = "_______________________________________________"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(360, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 16)
        Me.Label8.TabIndex = 271
        Me.Label8.Text = "IVA"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(218, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 16)
        Me.Label7.TabIndex = 270
        Me.Label7.Text = "VALOR ACTOS"
        '
        'iepsLabel22
        '
        Me.iepsLabel22.BackColor = System.Drawing.Color.Transparent
        Me.iepsLabel22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.iepsLabel22.ForeColor = System.Drawing.Color.Black
        Me.iepsLabel22.Location = New System.Drawing.Point(264, 478)
        Me.iepsLabel22.Name = "iepsLabel22"
        Me.iepsLabel22.Size = New System.Drawing.Size(105, 15)
        Me.iepsLabel22.TabIndex = 297
        Me.iepsLabel22.Text = "000,000,000.00"
        Me.iepsLabel22.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.iepsLabel22.Visible = False
        '
        'iepsLabel21
        '
        Me.iepsLabel21.BackColor = System.Drawing.Color.Transparent
        Me.iepsLabel21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.iepsLabel21.ForeColor = System.Drawing.Color.Black
        Me.iepsLabel21.Location = New System.Drawing.Point(264, 444)
        Me.iepsLabel21.Name = "iepsLabel21"
        Me.iepsLabel21.Size = New System.Drawing.Size(105, 15)
        Me.iepsLabel21.TabIndex = 296
        Me.iepsLabel21.Text = "000,000,000.00"
        Me.iepsLabel21.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.iepsLabel21.Visible = False
        '
        'iepsLabel16
        '
        Me.iepsLabel16.BackColor = System.Drawing.Color.Transparent
        Me.iepsLabel16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.iepsLabel16.ForeColor = System.Drawing.Color.Black
        Me.iepsLabel16.Location = New System.Drawing.Point(264, 420)
        Me.iepsLabel16.Name = "iepsLabel16"
        Me.iepsLabel16.Size = New System.Drawing.Size(105, 15)
        Me.iepsLabel16.TabIndex = 291
        Me.iepsLabel16.Text = "000,000,000.00"
        Me.iepsLabel16.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.iepsLabel16.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(300, 395)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 16)
        Me.Label10.TabIndex = 285
        Me.Label10.Text = "IEPS"
        Me.Label10.Visible = False
        '
        'btnGenerar
        '
        Me.btnGenerar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerar.Location = New System.Drawing.Point(22, 455)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(153, 38)
        Me.btnGenerar.TabIndex = 270
        Me.btnGenerar.Text = "Generar DIOT"
        Me.btnGenerar.UseVisualStyleBackColor = True
        '
        'btnCambiar
        '
        Me.btnCambiar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCambiar.Location = New System.Drawing.Point(202, 357)
        Me.btnCambiar.Name = "btnCambiar"
        Me.btnCambiar.Size = New System.Drawing.Size(157, 38)
        Me.btnCambiar.TabIndex = 271
        Me.btnCambiar.Text = "Enviar a siguiente mes"
        Me.btnCambiar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(21, 357)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(170, 38)
        Me.Button1.TabIndex = 272
        Me.Button1.Text = "Enviar a mes anterior"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmContabilidadDIOT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.btnGuardar
        Me.ClientSize = New System.Drawing.Size(1004, 516)
        Me.Controls.Add(Me.iepsLabel22)
        Me.Controls.Add(Me.iepsLabel21)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCambiar)
        Me.Controls.Add(Me.btnGenerar)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.iepsLabel16)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpHasta)
        Me.Controls.Add(Me.dtpDesde)
        Me.Controls.Add(Me.dgvCompro)
        Me.Controls.Add(Me.Label10)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmContabilidadDIOT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Conciliación DIOT"
        CType(Me.dgvCompro,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents dgvCompro As System.Windows.Forms.DataGridView
    Friend WithEvents dtpDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents btnCambiar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents iepsLabel22 As System.Windows.Forms.Label
    Friend WithEvents iepsLabel21 As System.Windows.Forms.Label
    Friend WithEvents ivaretLabel20 As System.Windows.Forms.Label
    Friend WithEvents ivaretLabel19 As System.Windows.Forms.Label
    Friend WithEvents ivaLabel18 As System.Windows.Forms.Label
    Friend WithEvents ivaLabel17 As System.Windows.Forms.Label
    Friend WithEvents iepsLabel16 As System.Windows.Forms.Label
    Friend WithEvents ivaretLabel15 As System.Windows.Forms.Label
    Friend WithEvents ivaLabel14 As System.Windows.Forms.Label
    Friend WithEvents vaLabel13 As System.Windows.Forms.Label
    Friend WithEvents vaLabel12 As System.Windows.Forms.Label
    Friend WithEvents vaLabel11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Selec As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RFC As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Prov As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Valor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents iva1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IVA As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IVAret As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ivaretc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IEPSpor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IEPScan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TipoPoliza As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Referencia As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaDiot As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
