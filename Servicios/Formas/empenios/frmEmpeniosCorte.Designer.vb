<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpeniosCorte
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
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFechaHasta = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbCaja = New System.Windows.Forms.ComboBox()
        Me.grpBusqueda = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblSaldoInicial = New System.Windows.Forms.Label()
        Me.lblEmpenios = New System.Windows.Forms.Label()
        Me.lblCapital = New System.Windows.Forms.Label()
        Me.lblInteres = New System.Windows.Forms.Label()
        Me.lblCompras = New System.Windows.Forms.Label()
        Me.lblSueldos = New System.Windows.Forms.Label()
        Me.lblVarios = New System.Windows.Forms.Label()
        Me.lblIngresos = New System.Windows.Forms.Label()
        Me.lblDepositos = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblCanEmpenios = New System.Windows.Forms.Label()
        Me.lblCanCapital = New System.Windows.Forms.Label()
        Me.lblCanInteres = New System.Windows.Forms.Label()
        Me.lblCanCompras = New System.Windows.Forms.Label()
        Me.lblCanSueldos = New System.Windows.Forms.Label()
        Me.lblCanVarios = New System.Windows.Forms.Label()
        Me.lblCanIngresos = New System.Windows.Forms.Label()
        Me.lblCanDepositos = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.grpBusqueda.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(331, 25)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 19)
        Me.Label22.TabIndex = 336
        Me.Label22.Text = "Sucursal:"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(254, 47)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(229, 25)
        Me.cmbSucursal.TabIndex = 335
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(159, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 19)
        Me.Label1.TabIndex = 333
        Me.Label1.Text = "Hasta:"
        '
        'dtpFechaHasta
        '
        Me.dtpFechaHasta.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFechaHasta.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFechaHasta.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpFechaHasta.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaHasta.Location = New System.Drawing.Point(134, 47)
        Me.dtpFechaHasta.Name = "dtpFechaHasta"
        Me.dtpFechaHasta.Size = New System.Drawing.Size(110, 25)
        Me.dtpFechaHasta.TabIndex = 331
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(41, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 19)
        Me.Label3.TabIndex = 332
        Me.Label3.Text = "Desde:"
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dtpFecha.CalendarTitleForeColor = System.Drawing.SystemColors.ControlText
        Me.dtpFecha.CalendarTrailingForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.dtpFecha.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(17, 47)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(111, 25)
        Me.dtpFecha.TabIndex = 330
        '
        'btnBuscar
        '
        Me.btnBuscar.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.Location = New System.Drawing.Point(324, 94)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(90, 30)
        Me.btnBuscar.TabIndex = 337
        Me.btnBuscar.Text = "Consultar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(90, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 19)
        Me.Label4.TabIndex = 339
        Me.Label4.Text = "Caja:"
        '
        'cmbCaja
        '
        Me.cmbCaja.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCaja.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCaja.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbCaja.FormattingEnabled = True
        Me.cmbCaja.Location = New System.Drawing.Point(34, 99)
        Me.cmbCaja.Name = "cmbCaja"
        Me.cmbCaja.Size = New System.Drawing.Size(152, 25)
        Me.cmbCaja.TabIndex = 338
        '
        'grpBusqueda
        '
        Me.grpBusqueda.Controls.Add(Me.dtpFecha)
        Me.grpBusqueda.Controls.Add(Me.Label4)
        Me.grpBusqueda.Controls.Add(Me.Label3)
        Me.grpBusqueda.Controls.Add(Me.cmbCaja)
        Me.grpBusqueda.Controls.Add(Me.dtpFechaHasta)
        Me.grpBusqueda.Controls.Add(Me.btnBuscar)
        Me.grpBusqueda.Controls.Add(Me.Label1)
        Me.grpBusqueda.Controls.Add(Me.Label22)
        Me.grpBusqueda.Controls.Add(Me.cmbSucursal)
        Me.grpBusqueda.Location = New System.Drawing.Point(21, 12)
        Me.grpBusqueda.Name = "grpBusqueda"
        Me.grpBusqueda.Size = New System.Drawing.Size(527, 130)
        Me.grpBusqueda.TabIndex = 340
        Me.grpBusqueda.TabStop = False
        Me.grpBusqueda.Text = "Busqueda"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 224)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(156, 27)
        Me.Label2.TabIndex = 341
        Me.Label2.Text = "Saldo Inicial: "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(55, 254)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(128, 27)
        Me.Label5.TabIndex = 342
        Me.Label5.Text = "Empeños: "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(69, 374)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(114, 27)
        Me.Label6.TabIndex = 343
        Me.Label6.Text = "Sueldos: "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(107, 494)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 29)
        Me.Label7.TabIndex = 344
        Me.Label7.Text = "Total: "
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(82, 284)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 27)
        Me.Label8.TabIndex = 345
        Me.Label8.Text = "Capital: "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(84, 314)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 27)
        Me.Label9.TabIndex = 346
        Me.Label9.Text = "Interés: "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(60, 344)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(123, 27)
        Me.Label10.TabIndex = 347
        Me.Label10.Text = "Compras: "
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(93, 404)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 27)
        Me.Label11.TabIndex = 348
        Me.Label11.Text = "Varios: "
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(65, 434)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(118, 27)
        Me.Label12.TabIndex = 349
        Me.Label12.Text = "Ingresos: "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(51, 464)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(132, 27)
        Me.Label13.TabIndex = 350
        Me.Label13.Text = "Depósitos: "
        '
        'lblSaldoInicial
        '
        Me.lblSaldoInicial.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaldoInicial.Location = New System.Drawing.Point(279, 224)
        Me.lblSaldoInicial.Name = "lblSaldoInicial"
        Me.lblSaldoInicial.Size = New System.Drawing.Size(225, 26)
        Me.lblSaldoInicial.TabIndex = 351
        Me.lblSaldoInicial.Text = "$0.00"
        Me.lblSaldoInicial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEmpenios
        '
        Me.lblEmpenios.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpenios.Location = New System.Drawing.Point(279, 254)
        Me.lblEmpenios.Name = "lblEmpenios"
        Me.lblEmpenios.Size = New System.Drawing.Size(225, 26)
        Me.lblEmpenios.TabIndex = 352
        Me.lblEmpenios.Text = "$0.00"
        Me.lblEmpenios.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCapital
        '
        Me.lblCapital.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCapital.Location = New System.Drawing.Point(279, 284)
        Me.lblCapital.Name = "lblCapital"
        Me.lblCapital.Size = New System.Drawing.Size(225, 26)
        Me.lblCapital.TabIndex = 353
        Me.lblCapital.Text = "$0.00"
        Me.lblCapital.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInteres
        '
        Me.lblInteres.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInteres.Location = New System.Drawing.Point(279, 314)
        Me.lblInteres.Name = "lblInteres"
        Me.lblInteres.Size = New System.Drawing.Size(225, 26)
        Me.lblInteres.TabIndex = 354
        Me.lblInteres.Text = "$0.00"
        Me.lblInteres.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompras
        '
        Me.lblCompras.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompras.Location = New System.Drawing.Point(279, 344)
        Me.lblCompras.Name = "lblCompras"
        Me.lblCompras.Size = New System.Drawing.Size(225, 26)
        Me.lblCompras.TabIndex = 355
        Me.lblCompras.Text = "$0.00"
        Me.lblCompras.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSueldos
        '
        Me.lblSueldos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSueldos.Location = New System.Drawing.Point(279, 374)
        Me.lblSueldos.Name = "lblSueldos"
        Me.lblSueldos.Size = New System.Drawing.Size(225, 26)
        Me.lblSueldos.TabIndex = 356
        Me.lblSueldos.Text = "$0.00"
        Me.lblSueldos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVarios
        '
        Me.lblVarios.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVarios.Location = New System.Drawing.Point(279, 404)
        Me.lblVarios.Name = "lblVarios"
        Me.lblVarios.Size = New System.Drawing.Size(225, 26)
        Me.lblVarios.TabIndex = 357
        Me.lblVarios.Text = "$0.00"
        Me.lblVarios.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIngresos
        '
        Me.lblIngresos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIngresos.Location = New System.Drawing.Point(279, 434)
        Me.lblIngresos.Name = "lblIngresos"
        Me.lblIngresos.Size = New System.Drawing.Size(225, 26)
        Me.lblIngresos.TabIndex = 358
        Me.lblIngresos.Text = "$0.00"
        Me.lblIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDepositos
        '
        Me.lblDepositos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepositos.Location = New System.Drawing.Point(279, 464)
        Me.lblDepositos.Name = "lblDepositos"
        Me.lblDepositos.Size = New System.Drawing.Size(225, 26)
        Me.lblDepositos.TabIndex = 359
        Me.lblDepositos.Text = "$0.00"
        Me.lblDepositos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(279, 494)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(226, 27)
        Me.lblTotal.TabIndex = 360
        Me.lblTotal.Text = "$0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanEmpenios
        '
        Me.lblCanEmpenios.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanEmpenios.Location = New System.Drawing.Point(197, 255)
        Me.lblCanEmpenios.Name = "lblCanEmpenios"
        Me.lblCanEmpenios.Size = New System.Drawing.Size(59, 26)
        Me.lblCanEmpenios.TabIndex = 361
        Me.lblCanEmpenios.Text = "0"
        Me.lblCanEmpenios.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanCapital
        '
        Me.lblCanCapital.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanCapital.Location = New System.Drawing.Point(197, 285)
        Me.lblCanCapital.Name = "lblCanCapital"
        Me.lblCanCapital.Size = New System.Drawing.Size(59, 26)
        Me.lblCanCapital.TabIndex = 362
        Me.lblCanCapital.Text = "0"
        Me.lblCanCapital.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanInteres
        '
        Me.lblCanInteres.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanInteres.Location = New System.Drawing.Point(197, 315)
        Me.lblCanInteres.Name = "lblCanInteres"
        Me.lblCanInteres.Size = New System.Drawing.Size(59, 26)
        Me.lblCanInteres.TabIndex = 363
        Me.lblCanInteres.Text = "0"
        Me.lblCanInteres.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanCompras
        '
        Me.lblCanCompras.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanCompras.Location = New System.Drawing.Point(197, 345)
        Me.lblCanCompras.Name = "lblCanCompras"
        Me.lblCanCompras.Size = New System.Drawing.Size(59, 26)
        Me.lblCanCompras.TabIndex = 364
        Me.lblCanCompras.Text = "0"
        Me.lblCanCompras.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanSueldos
        '
        Me.lblCanSueldos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanSueldos.Location = New System.Drawing.Point(197, 375)
        Me.lblCanSueldos.Name = "lblCanSueldos"
        Me.lblCanSueldos.Size = New System.Drawing.Size(59, 26)
        Me.lblCanSueldos.TabIndex = 365
        Me.lblCanSueldos.Text = "0"
        Me.lblCanSueldos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanVarios
        '
        Me.lblCanVarios.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanVarios.Location = New System.Drawing.Point(197, 405)
        Me.lblCanVarios.Name = "lblCanVarios"
        Me.lblCanVarios.Size = New System.Drawing.Size(59, 26)
        Me.lblCanVarios.TabIndex = 366
        Me.lblCanVarios.Text = "0"
        Me.lblCanVarios.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanIngresos
        '
        Me.lblCanIngresos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanIngresos.Location = New System.Drawing.Point(197, 435)
        Me.lblCanIngresos.Name = "lblCanIngresos"
        Me.lblCanIngresos.Size = New System.Drawing.Size(59, 26)
        Me.lblCanIngresos.TabIndex = 367
        Me.lblCanIngresos.Text = "0"
        Me.lblCanIngresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCanDepositos
        '
        Me.lblCanDepositos.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCanDepositos.Location = New System.Drawing.Point(196, 465)
        Me.lblCanDepositos.Name = "lblCanDepositos"
        Me.lblCanDepositos.Size = New System.Drawing.Size(59, 26)
        Me.lblCanDepositos.TabIndex = 368
        Me.lblCanDepositos.Text = "0"
        Me.lblCanDepositos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(186, 204)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(319, 13)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "____________________________________________________"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(410, 189)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(80, 24)
        Me.Label15.TabIndex = 371
        Me.Label15.Text = "Importe"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(197, 189)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(93, 24)
        Me.Label14.TabIndex = 372
        Me.Label14.Text = "Cantidad"
        '
        'btnImprimir
        '
        Me.btnImprimir.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(227, 544)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(90, 30)
        Me.btnImprimir.TabIndex = 340
        Me.btnImprimir.Text = "Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'frmEmpeniosCorte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(575, 586)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblCanDepositos)
        Me.Controls.Add(Me.lblCanIngresos)
        Me.Controls.Add(Me.lblCanVarios)
        Me.Controls.Add(Me.lblCanSueldos)
        Me.Controls.Add(Me.lblCanCompras)
        Me.Controls.Add(Me.lblCanInteres)
        Me.Controls.Add(Me.lblCanCapital)
        Me.Controls.Add(Me.lblCanEmpenios)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lblDepositos)
        Me.Controls.Add(Me.lblIngresos)
        Me.Controls.Add(Me.lblVarios)
        Me.Controls.Add(Me.lblSueldos)
        Me.Controls.Add(Me.lblCompras)
        Me.Controls.Add(Me.lblInteres)
        Me.Controls.Add(Me.lblCapital)
        Me.Controls.Add(Me.lblEmpenios)
        Me.Controls.Add(Me.lblSaldoInicial)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grpBusqueda)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpeniosCorte"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta"
        Me.grpBusqueda.ResumeLayout(False)
        Me.grpBusqueda.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbCaja As System.Windows.Forms.ComboBox
    Friend WithEvents grpBusqueda As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblSaldoInicial As System.Windows.Forms.Label
    Friend WithEvents lblEmpenios As System.Windows.Forms.Label
    Friend WithEvents lblCapital As System.Windows.Forms.Label
    Friend WithEvents lblInteres As System.Windows.Forms.Label
    Friend WithEvents lblCompras As System.Windows.Forms.Label
    Friend WithEvents lblSueldos As System.Windows.Forms.Label
    Friend WithEvents lblVarios As System.Windows.Forms.Label
    Friend WithEvents lblIngresos As System.Windows.Forms.Label
    Friend WithEvents lblDepositos As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblCanEmpenios As System.Windows.Forms.Label
    Friend WithEvents lblCanCapital As System.Windows.Forms.Label
    Friend WithEvents lblCanInteres As System.Windows.Forms.Label
    Friend WithEvents lblCanCompras As System.Windows.Forms.Label
    Friend WithEvents lblCanSueldos As System.Windows.Forms.Label
    Friend WithEvents lblCanVarios As System.Windows.Forms.Label
    Friend WithEvents lblCanIngresos As System.Windows.Forms.Label
    Friend WithEvents lblCanDepositos As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
End Class
