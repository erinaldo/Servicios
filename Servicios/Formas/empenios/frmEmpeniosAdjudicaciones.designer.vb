<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpeniosAdjudicaciones
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.chkTiempoReal = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnSeleccionar = New System.Windows.Forms.Button()
        Me.dtpFecha2 = New System.Windows.Forms.DateTimePicker()
        Me.dtpFecha1 = New System.Windows.Forms.DateTimePicker()
        Me.DGServicios = New System.Windows.Forms.DataGridView()
        Me.Seleccion = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.idMovimiento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Folio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FInicio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FechaRe = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Refrend = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Restante = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fUltimoPago = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantUltPago = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSerie = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtClienteDatos = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.txtClienteCodigo = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnimprimir = New System.Windows.Forms.Button()
        Me.grpFiltro = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.DGServicios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpFiltro.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(489, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 19)
        Me.Label4.TabIndex = 339
        Me.Label4.Text = "Folio:"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(643, 19)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(83, 19)
        Me.Label22.TabIndex = 335
        Me.Label22.Text = "Sucursal:"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSucursal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(731, 16)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(352, 25)
        Me.cmbSucursal.TabIndex = 325
        Me.ToolTip1.SetToolTip(Me.cmbSucursal, "Seleccional la sucursal.")
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Button3.Location = New System.Drawing.Point(382, 81)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 28)
        Me.Button3.TabIndex = 328
        Me.Button3.Text = "Buscar"
        Me.ToolTip1.SetToolTip(Me.Button3, "Buscar empeños.")
        Me.Button3.UseVisualStyleBackColor = True
        '
        'chkTiempoReal
        '
        Me.chkTiempoReal.AutoSize = True
        Me.chkTiempoReal.Checked = True
        Me.chkTiempoReal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTiempoReal.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.chkTiempoReal.Location = New System.Drawing.Point(498, 85)
        Me.chkTiempoReal.Name = "chkTiempoReal"
        Me.chkTiempoReal.Size = New System.Drawing.Size(103, 21)
        Me.chkTiempoReal.TabIndex = 334
        Me.chkTiempoReal.Text = "Tiempo real"
        Me.ToolTip1.SetToolTip(Me.chkTiempoReal, "Habilíta o deshabilíta la busqueda en tiempo real.")
        Me.chkTiempoReal.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Label2.Location = New System.Drawing.Point(13, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(180, 17)
        Me.Label2.TabIndex = 333
        Me.Label2.Text = "Resultados de la consulta:"
        '
        'txtFolio
        '
        Me.txtFolio.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtFolio.Location = New System.Drawing.Point(546, 19)
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.Size = New System.Drawing.Size(90, 25)
        Me.txtFolio.TabIndex = 327
        Me.ToolTip1.SetToolTip(Me.txtFolio, "Folio.")
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.Button2.Location = New System.Drawing.Point(1058, 519)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(64, 25)
        Me.Button2.TabIndex = 330
        Me.Button2.Text = "Cerrar"
        Me.ToolTip1.SetToolTip(Me.Button2, "Cerrar.")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Enabled = False
        Me.btnSeleccionar.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSeleccionar.Location = New System.Drawing.Point(463, 529)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(115, 37)
        Me.btnSeleccionar.TabIndex = 329
        Me.btnSeleccionar.Text = "Adjudicar"
        Me.ToolTip1.SetToolTip(Me.btnSeleccionar, "Adjudicar los empeños seleccionados.")
        Me.btnSeleccionar.UseVisualStyleBackColor = True
        '
        'dtpFecha2
        '
        Me.dtpFecha2.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha2.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha2.Location = New System.Drawing.Point(245, 19)
        Me.dtpFecha2.Name = "dtpFecha2"
        Me.dtpFecha2.Size = New System.Drawing.Size(108, 25)
        Me.dtpFecha2.TabIndex = 324
        Me.ToolTip1.SetToolTip(Me.dtpFecha2, "Fecha final.")
        '
        'dtpFecha1
        '
        Me.dtpFecha1.CustomFormat = "yyyy/MM/dd"
        Me.dtpFecha1.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFecha1.Location = New System.Drawing.Point(75, 19)
        Me.dtpFecha1.Name = "dtpFecha1"
        Me.dtpFecha1.Size = New System.Drawing.Size(108, 25)
        Me.dtpFecha1.TabIndex = 323
        Me.ToolTip1.SetToolTip(Me.dtpFecha1, "Fecha inicio.")
        '
        'DGServicios
        '
        Me.DGServicios.AllowUserToAddRows = False
        Me.DGServicios.AllowUserToDeleteRows = False
        Me.DGServicios.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.DGServicios.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial Narrow", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGServicios.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGServicios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGServicios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Seleccion, Me.idMovimiento, Me.Folio, Me.FInicio, Me.FechaRe, Me.Descripcion, Me.Cliente, Me.Total, Me.Refrend, Me.Restante, Me.fUltimoPago, Me.cantUltPago})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGServicios.DefaultCellStyle = DataGridViewCellStyle10
        Me.DGServicios.Location = New System.Drawing.Point(15, 149)
        Me.DGServicios.Name = "DGServicios"
        Me.DGServicios.ReadOnly = True
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGServicios.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DGServicios.RowHeadersVisible = False
        Me.DGServicios.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DGServicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGServicios.Size = New System.Drawing.Size(1107, 364)
        Me.DGServicios.TabIndex = 331
        Me.ToolTip1.SetToolTip(Me.DGServicios, "Selecciona los empeños a adjudicar.")
        '
        'Seleccion
        '
        Me.Seleccion.DataPropertyName = "Selec"
        Me.Seleccion.FalseValue = "0"
        Me.Seleccion.HeaderText = ""
        Me.Seleccion.Name = "Seleccion"
        Me.Seleccion.ReadOnly = True
        Me.Seleccion.TrueValue = "1"
        Me.Seleccion.Width = 25
        '
        'idMovimiento
        '
        Me.idMovimiento.DataPropertyName = "idMovimiento"
        Me.idMovimiento.HeaderText = "idMovimiento"
        Me.idMovimiento.Name = "idMovimiento"
        Me.idMovimiento.ReadOnly = True
        Me.idMovimiento.Visible = False
        '
        'Folio
        '
        Me.Folio.DataPropertyName = "Folio"
        Me.Folio.HeaderText = "Folio"
        Me.Folio.Name = "Folio"
        Me.Folio.ReadOnly = True
        Me.Folio.Width = 50
        '
        'FInicio
        '
        Me.FInicio.DataPropertyName = "Fecha"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.FInicio.DefaultCellStyle = DataGridViewCellStyle3
        Me.FInicio.HeaderText = "F. Inicio"
        Me.FInicio.Name = "FInicio"
        Me.FInicio.ReadOnly = True
        Me.FInicio.Width = 90
        '
        'FechaRe
        '
        Me.FechaRe.DataPropertyName = "FechaContrato"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        Me.FechaRe.DefaultCellStyle = DataGridViewCellStyle4
        Me.FechaRe.HeaderText = "Fecha Ren."
        Me.FechaRe.Name = "FechaRe"
        Me.FechaRe.ReadOnly = True
        '
        'Descripcion
        '
        Me.Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Descripcion.DataPropertyName = "Descripcion"
        Me.Descripcion.HeaderText = "Descripción"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.ReadOnly = True
        '
        'Cliente
        '
        Me.Cliente.DataPropertyName = "Cliente"
        Me.Cliente.HeaderText = "Cliente"
        Me.Cliente.Name = "Cliente"
        Me.Cliente.ReadOnly = True
        Me.Cliente.Width = 350
        '
        'Total
        '
        Me.Total.DataPropertyName = "Total"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Total.DefaultCellStyle = DataGridViewCellStyle5
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        '
        'Refrend
        '
        Me.Refrend.DataPropertyName = "Refrendo"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.Refrend.DefaultCellStyle = DataGridViewCellStyle6
        Me.Refrend.HeaderText = "Refrendo"
        Me.Refrend.Name = "Refrend"
        Me.Refrend.ReadOnly = True
        Me.Refrend.Visible = False
        Me.Refrend.Width = 85
        '
        'Restante
        '
        Me.Restante.DataPropertyName = "Restante"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.Restante.DefaultCellStyle = DataGridViewCellStyle7
        Me.Restante.HeaderText = "Restante"
        Me.Restante.Name = "Restante"
        Me.Restante.ReadOnly = True
        Me.Restante.Visible = False
        Me.Restante.Width = 85
        '
        'fUltimoPago
        '
        Me.fUltimoPago.DataPropertyName = "FechaUltimoPago"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.fUltimoPago.DefaultCellStyle = DataGridViewCellStyle8
        Me.fUltimoPago.HeaderText = "Último Pago"
        Me.fUltimoPago.Name = "fUltimoPago"
        Me.fUltimoPago.ReadOnly = True
        Me.fUltimoPago.Width = 160
        '
        'cantUltPago
        '
        Me.cantUltPago.DataPropertyName = "CantidadUltimoPago"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.cantUltPago.DefaultCellStyle = DataGridViewCellStyle9
        Me.cantUltPago.HeaderText = "Cant. último Pago"
        Me.cantUltPago.Name = "cantUltPago"
        Me.cantUltPago.ReadOnly = True
        Me.cantUltPago.Visible = False
        Me.cantUltPago.Width = 85
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(361, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 19)
        Me.Label1.TabIndex = 341
        Me.Label1.Text = "Serie:"
        '
        'txtSerie
        '
        Me.txtSerie.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtSerie.Location = New System.Drawing.Point(417, 19)
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(65, 25)
        Me.txtSerie.TabIndex = 340
        Me.ToolTip1.SetToolTip(Me.txtSerie, "N° de serie")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(16, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 19)
        Me.Label3.TabIndex = 343
        Me.Label3.Text = "Inicio:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(190, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 19)
        Me.Label5.TabIndex = 344
        Me.Label5.Text = "Final:"
        '
        'txtClienteDatos
        '
        Me.txtClienteDatos.BackColor = System.Drawing.Color.White
        Me.txtClienteDatos.Enabled = False
        Me.txtClienteDatos.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtClienteDatos.Location = New System.Drawing.Point(189, 50)
        Me.txtClienteDatos.Name = "txtClienteDatos"
        Me.txtClienteDatos.Size = New System.Drawing.Size(473, 25)
        Me.txtClienteDatos.TabIndex = 347
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(4, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 19)
        Me.Label6.TabIndex = 348
        Me.Label6.Text = "Cliente:"
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCliente.Location = New System.Drawing.Point(75, 81)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(108, 22)
        Me.btnBuscarCliente.TabIndex = 346
        Me.btnBuscarCliente.Text = "Buscar &Cliente"
        Me.ToolTip1.SetToolTip(Me.btnBuscarCliente, "Buscar cliente.")
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'txtClienteCodigo
        '
        Me.txtClienteCodigo.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.txtClienteCodigo.Location = New System.Drawing.Point(75, 50)
        Me.txtClienteCodigo.Name = "txtClienteCodigo"
        Me.txtClienteCodigo.Size = New System.Drawing.Size(108, 25)
        Me.txtClienteCodigo.TabIndex = 345
        Me.ToolTip1.SetToolTip(Me.txtClienteCodigo, "Clave del cliente")
        '
        'btnimprimir
        '
        Me.btnimprimir.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnimprimir.Location = New System.Drawing.Point(584, 529)
        Me.btnimprimir.Name = "btnimprimir"
        Me.btnimprimir.Size = New System.Drawing.Size(115, 37)
        Me.btnimprimir.TabIndex = 350
        Me.btnimprimir.Text = "Imprimir"
        Me.ToolTip1.SetToolTip(Me.btnimprimir, "Adjudicar los empeños seleccionados.")
        Me.btnimprimir.UseVisualStyleBackColor = True
        '
        'grpFiltro
        '
        Me.grpFiltro.Controls.Add(Me.txtClienteDatos)
        Me.grpFiltro.Controls.Add(Me.dtpFecha1)
        Me.grpFiltro.Controls.Add(Me.Label6)
        Me.grpFiltro.Controls.Add(Me.dtpFecha2)
        Me.grpFiltro.Controls.Add(Me.btnBuscarCliente)
        Me.grpFiltro.Controls.Add(Me.txtFolio)
        Me.grpFiltro.Controls.Add(Me.txtClienteCodigo)
        Me.grpFiltro.Controls.Add(Me.chkTiempoReal)
        Me.grpFiltro.Controls.Add(Me.Label5)
        Me.grpFiltro.Controls.Add(Me.Button3)
        Me.grpFiltro.Controls.Add(Me.Label3)
        Me.grpFiltro.Controls.Add(Me.cmbSucursal)
        Me.grpFiltro.Controls.Add(Me.Label22)
        Me.grpFiltro.Controls.Add(Me.Label1)
        Me.grpFiltro.Controls.Add(Me.Label4)
        Me.grpFiltro.Controls.Add(Me.txtSerie)
        Me.grpFiltro.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpFiltro.Location = New System.Drawing.Point(12, 4)
        Me.grpFiltro.Name = "grpFiltro"
        Me.grpFiltro.Size = New System.Drawing.Size(1110, 121)
        Me.grpFiltro.TabIndex = 349
        Me.grpFiltro.TabStop = False
        Me.grpFiltro.Text = "Filtro de búsqueda"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(217, 519)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 19)
        Me.Label7.TabIndex = 349
        Me.Label7.Text = "Peso total:"
        '
        'frmEmpeniosAdjudicaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(1134, 578)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnimprimir)
        Me.Controls.Add(Me.grpFiltro)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.DGServicios)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpeniosAdjudicaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Adjudicaciones"
        CType(Me.DGServicios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpFiltro.ResumeLayout(False)
        Me.grpFiltro.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents chkTiempoReal As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFolio As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents dtpFecha2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFecha1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DGServicios As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtClienteDatos As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents txtClienteCodigo As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents grpFiltro As System.Windows.Forms.GroupBox
    Friend WithEvents btnimprimir As System.Windows.Forms.Button
    Friend WithEvents Seleccion As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents idMovimiento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Folio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FInicio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FechaRe As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cliente As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Refrend As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Restante As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fUltimoPago As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantUltPago As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
