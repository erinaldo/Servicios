﻿Public Class frmNominas
    Dim IdsVariantes As New elemento
    Dim idNota As Integer
    Dim IVaDefault As Double
    Dim IDsMonedas As New elemento

    Dim IDsMonedas2 As New elemento
    Dim idCliente As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim IdsTipos As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    Dim IdVariante As Integer
    Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim Tabla As New DataTable
    Dim IdsSucursales As New elemento
    Dim idsFormasDePago As New elemento
    Dim SerieAnt As String
    Dim Cadena As String
    Dim Sello As String
    Dim Isr As Double
    Dim IvaRetenido As Double
    Dim PrecioBase As Double
    Dim LimitedeFolios As Boolean = False
    
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim ImpDoc As ImprimirDocumento
    Dim SObre As Byte
    Dim SIVA As Double
    Dim EsElectronica As Byte
    Dim CertificadoCaduco As Boolean = False
    Dim SinTimbres As Boolean = False
    Dim CLABE As String
    Dim Banco As Integer
    Dim IdDetalleI As Integer
    Dim IdDetalleH As Integer
    Dim IDsnominas As New elemento
    Dim TotalVenta As Double
    Dim OrigenRecurso As String
    Dim MontoRecurso As Double
    Dim ValorMercado As Double
    Dim PrecioalOtorgarse As Double
    Dim MetodosDePago As dbVentasAddMetodos
    Dim Op As dbOpciones
    Dim Nom As dbNominas
    Dim Cp As dbContabilidadPolizas
    Dim FechaInicioLaboral As Date
    'Private origenes() As String = {"Ingresos propios", "Ingresos Federales", "Ingresos Mixtos"}
    'Private clavesRecursos() As String = {"IP", "IF", "IM"}
    Public Sub New(Optional ByVal pidNota As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idNota = pidNota
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Settings.nominacheck = CheckBox7.Checked
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nomina no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim C As New dbNominas(idNota, MySqlcon)
                'C.RegresaInventario(idNota)
                C.Eliminar(idNota)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmNotasdeCargo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled = True Then
                Modificar(Estados.Guardada, False)
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled = True Then
                Modificar(Estados.Pendiente, False)
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
        If e.KeyCode = Keys.F6 And Estado >= 3 Then
            Dim fmp As New frmVentasSelectorMetodosPago(2, idNota, TotalVenta, 1, True)
            fmp.ShowDialog()
            fmp.Dispose()
        End If
    End Sub
    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConsultaOn = False
        'Button12.Visible = True
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ImpDoc = New ImprimirDocumento()
        Op = New dbOpciones(MySqlcon)
        Nom = New dbNominas(MySqlcon)
        Cp = New dbContabilidadPolizas(MySqlcon)
        CheckBox7.Checked = My.Settings.nominacheck
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Me.Icon = GlobalIcono
        If GlobalTipoFacturacion > 1 Then
            SinTimbres = ChecaTimbres()
        End If
        MetodosDePago = New dbVentasAddMetodos(MySqlcon)
        Tabla.Columns.Add("Id", I.GetType)
        'Tabla.Columns.Add("TipoR", S.GetType)
        'Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        'Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Importe", D.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblformasdepago", ComboBox6, "concat(lpad(convert(clavesat using utf8),2,'0'),' ',nombre)", "nombret", "idforma", idsFormasDePago, "tipo=1", , "idforma")
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        cmbVariante.Items.Add("Percepción")
        cmbVariante.Items.Add("Deducción")
        ComboBox4.Items.Add("01 Riesgo de trabajo")
        ComboBox4.Items.Add("02 Enfermedad en general")
        ComboBox4.Items.Add("03 Maternidad")
        ComboBox5.Items.Add("Dobles")
        ComboBox5.Items.Add("Triples")
        comboTipo.Items.Add("Ordinaria")
        comboTipo.Items.Add("Extraordinaria")
        comboTipo.SelectedIndex = 0

        ConsultaOn = True
        If idNota = 0 Then
            Nuevo()
        Else
            LlenaDatosVenta()
            NuevoConcepto()
        End If

    End Sub
    Private Function ChecaCertificado(ByVal pIdCertificado As Integer) As Boolean
        Dim SC As New dbSucursalesCertificados(pIdCertificado, MySqlcon)
        If SC.FechaVencimiento <= Format(Date.Now, "yyyy/MM/dd") Then
            MsgBox("El certificado del sello digital a caducado.", MsgBoxStyle.Critical, GlobalNombreApp)
            Return True
        Else
            If Format(Date.Now, "yyyy/MM/dd") > DateAdd(DateInterval.Day, SC.Aviso * -1, CDate(SC.FechaVencimiento)) Then
                MsgBox("El certificado del sello digital esta por caducar.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
            Return False
        End If
    End Function
    Private Function ChecaTimbres() As Boolean
        Dim TTimbres As Integer
        TTimbres = CuentaTimbres()
        Dim Ops As New dbOpciones(MySqlcon)
        If Ops.FechaVen <= Format(Date.Now, "yyyy/MM/dd") Then
            MsgBox("Los timbres han caducado.", MsgBoxStyle.Critical, GlobalNombreApp)
            Return True
        Else
            If TTimbres >= Ops.Timbres Then
                MsgBox("Los timbres se han terminado.", MsgBoxStyle.Critical, GlobalNombreApp)
                Return True
            Else
                If Format(Date.Now, "yyyy/MM/dd") > DateAdd(DateInterval.Day, Ops.AvisoDias * -1, CDate(Ops.FechaVen)) Then
                    MsgBox("Los timbres estan por caducar.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
                If Ops.Timbres - TTimbres <= Ops.AvisoTimbres Then
                    MsgBox("Los timbres estan por terminarse.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
                Return False
            End If
        End If
    End Function
    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim T As Double
                Dim Iva As Double
                Dim V As New dbNominas(MySqlcon)
                T = V.DaTotal(idNota, 2)
                'Dim O As New dbOpciones(MySqlcon)

                Iva = V.TotalIva
                Label12.Text = Format(V.Subtotal, "#,##0.00")
                Label13.Text = Format(V.TotalIva, "#,##0.00")
                Label14.Text = Format(V.TotalNota, "#,##0.00")
                TotalVenta = V.TotalNota
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        DateTimePicker1.Value = Date.Now
        TextBox1.Text = ""
        FolioAnt = 0
        idNota = 0
        Button27.Enabled = True
        Button35.Enabled = False
        DGDetalles.DataSource = Nothing
        DGIncapacidades.DataSource = Nothing
        DGHorasextra.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        TextBox18.Text = "0"
        txtantiguedad.Text = "0"
        txtdiaspagados.Text = "0"
        Button26.Enabled = False
        Button14.Enabled = False
        CheckBox1.Checked = False
        cmbVariante.SelectedIndex = 0
        TextBox14.Text = ""
        Button30.Visible = False
        ComboBox6.Enabled = True
        comboTipo.SelectedIndex = 0
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        TotalVenta = 0
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        Estado = Estados.Inicio
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'TextBox11.Text = S.Serie
        IVaDefault = S.Impuesto
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Nominas, GlobalTipoFacturacion)
        TextBox11.Text = Sf.Serie
        EsElectronica = GlobalTipoFacturacion
        Dim V As New dbNominas(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        TextBox10.Text = CM.Cantidad.ToString
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        CheckBox1.Checked = False
        ComboBox6.SelectedIndex = 0
        Panel1.Enabled = True
        Panel2.Enabled = True
        Panel3.Enabled = True
        Panel4.Enabled = True
        SerieAnt = ""
        OrigenRecurso = "NA"
        MontoRecurso = 0
        Label24.Visible = False
        DateTimePicker2.Value = Date.Now
        DateTimePicker3.Value = Date.Now
        DateTimePicker4.Value = Date.Now
        DateTimePicker2.Enabled = True
        DateTimePicker3.Enabled = True
        DateTimePicker4.Enabled = True
        txtantiguedad.Enabled = True
        txtdiaspagados.Enabled = True
        'Button25.Enabled = True
        TextBox18.Enabled = True
        'Label38.Text = "0"
        'ComboBox4.SelectedIndex = 0
        NuevoConcepto()
        NuevoHorasExtra()
        NuevoIncapacidad()
        TextBox1.Focus()
        If CInt(TextBox2.Text) > Sf.FolioFinal Then
            LimitedeFolios = True
            MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
        Else
            LimitedeFolios = False
        End If
        If Sf.EsElectronica > 0 Then
            CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.CambiarSucursal, PermisosN.Secciones.Nomina) = False Then
            ComboBox3.Enabled = False
        Else
            ComboBox3.Enabled = True
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker2.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonCliente()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbTrabajadores(MySqlcon)
                If c.BuscaTrabajador(TextBox1.Text) Then
                    'If c.DireccionFiscal = 0 Then
                    TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    'Else
                    '   TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    'End If
                    'TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00") + vbCrLf + "A Favor: " + Format(c.DaSaldoAFavor(c.ID), "#,##0.00")

                    'If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Then
                    'If c.Credito > 0 Then
                    '    ComboBox4.SelectedIndex = 1
                    'Else
                    '    ComboBox4.SelectedIndex = 0
                    'End If
                    'End If
                    idCliente = c.ID
                    Banco = c.Banco
                    CLABE = c.CLABE
                    FechaInicioLaboral = CDate(c.FechaInicioLaboral)
                    txtantiguedad.Text = CStr(Math.Truncate((DateDiff(DateInterval.Day, CDate(c.FechaInicioLaboral), DateTimePicker4.Value) + 1) / 7))
                    'Isr = c.ISR
                    'IvaRetenido = c.IvaRetenido
                    'SIVA = c.IVA
                    'SObre = c.SobreescribeIVA
                    'If SObre <> 0 Then TextBox8.Text = SIVA.ToString
                Else
                    TextBox7.Text = ""
                    'TextBox13.Text = ""
                    idCliente = 0
                    Isr = 0
                    txtantiguedad.Text = "0"
                    IvaRetenido = 0
                    SObre = 0
                    SIVA = 0
                    Banco = 0
                    CLABE = ""
                    'TextBox8.Text = IVaDefault.ToString
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente, False)
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte, ByVal SinImprimir As Boolean)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbNominas(MySqlcon)
            Dim Desglozar As Byte
            If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            'If FolioAnt <> TextBox2.Text Then
            If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                If pEstado = Estados.Guardada Then TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            End If
            'End If
            If IsNumeric(TextBox18.Text) = False Then
                MensajeError += " El importe debe ser un valor numérico."
            End If
            If IsNumeric(txtdiaspagados.Text) Then
                If CInt(txtdiaspagados.Text) <= 0 Then
                    MensajeError += " Los días pagados deben ser mayor a cero."
                End If
            Else
                MensajeError += " Los días pagados deben ser un valor numérico."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = False And pEstado = Estados.Guardada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NomminaCancelar, PermisosN.Secciones.Nomina) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            Dim TotalAgregado As Double = MetodosDePago.TotalAgregado(2, idNota)
            If pEstado = Estados.Guardada And Math.Round(TotalAgregado, 2) <> Math.Round(TotalVenta, 2) And TotalAgregado > 0 Then
                MensajeError += " Los métodos de pago no estan agregados correctamente."
            End If
            If DateTimePicker1.Value.ToString("yyyy/MM/dd") < DateAdd(DateInterval.Day, -3, Date.Now).ToString("yyyy/MM/dd") And pEstado = Estados.Guardada Then
                MensajeError += "Fecha no válida. No puede ser mayor a 3 días atras."
            End If
            If MensajeError = "" Then
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                Dim O As New dbOpciones(MySqlcon)
                'Dim FP As New dbFormasdePago(idsFormasDePago.Valor(ComboBox4.SelectedIndex), MySqlcon)
                'Dim CM As New dbMonedasConversiones(MySqlcon)
                'CM.Modificar(1, CDbl(TextBox10.Text))
                'Dim Credito As Byte
                'Credito = FP.Tipo
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Nominas, GlobalTipoFacturacion)
                Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                C.DaTotal(idNota, 2)
                C.Modificar(idNota, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, pEstado, CDbl(TextBox18.Text), CDbl(TextBox18.Text), idCliente, TextBox11.Text, CDbl(TextBox10.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, 2, IdsTipos.Valor(ComboBox8.SelectedIndex), EsElectronica, TextBox14.Text, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), Format(DateTimePicker4.Value, "yyyy/MM/dd"), CDbl(txtdiaspagados.Text), CInt(txtantiguedad.Text), idsFormasDePago.Valor(ComboBox6.SelectedIndex), If(comboTipo.SelectedIndex = 0, "O", "E"), OrigenRecurso, MontoRecurso)
                Estado = pEstado
                'If pEstado = Estados.Cancelada Then
                'Dim VP As New dbVentasPagos(MySqlcon)
                'VP.CancelarPagosxDocumento(idNota, 1, idCliente, Estados.Cancelada)
                '    Dim S As New dbInventarioSeries(MySqlcon)
                '    S.QuitaSeriesAVenta(idNota)
                'If Estado = Estados.Guardada Then
                'Dim Cliente As New dbClientes(MySqlcon)
                'Cliente.ModificaSaldo(idCliente, C.TotalNota, 1)
                'End If
                'C.RegresaInventario(idNota)
                'End If
                If pEstado = Estados.Guardada Then
                    'If FP.Tipo = 0 Then
                    'Dim Cliente As New dbClientes(MySqlcon)
                    'Cliente.ModificaSaldo(idCliente, C.TotalNota, 0)
                    'End If
                    'If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    If TotalAgregado = 0 Then
                        MetodosDePago.Guardar(2, idsFormasDePago.Valor(ComboBox6.SelectedIndex), C.TotalNota, idNota)
                    End If
                    Select Case EsElectronica
                        Case 0
                            Imprimir(idNota)
                        Case 1
                            Imprimir(idNota)
                        Case 2
                            CadenaOriginali(pEstado, SinImprimir)
                        Case 3
                            CadenaOriginali33(pEstado, SinImprimir)
                    End Select

                End If
                'End If
                'CadenaOriginal()
                If Estado = Estados.Guardada Or Estado = Estados.Cancelada Then
                    GeneraPoliza()
                End If
                Nuevo()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Guardar()
        Try
            If LimitedeFolios = True Then
                MsgBox("Se ha alcanzado el limite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If CertificadoCaduco = True Then
                MsgBox("El certificado del sello digital esta vencido.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If SinTimbres = True Then
                MsgBox("Los timbres se acabaron o se han caducado.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If IsNumeric(TextBox18.Text) = False Then
                MsgBox("El importe debe ser un valor numérico.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            'If Button1.Text = "Guardar" Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
                If idCliente <> 0 Then
                    Dim C As New dbNominas(MySqlcon)
                    Dim Desglozar As Byte
                    If CheckBox1.Checked Then
                        Desglozar = 1
                    Else
                        Desglozar = 0
                    End If
                    If IsNumeric(TextBox2.Text) = False Then
                        MsgBox("El folio debe ser un valor numérico", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                        TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                        Label17.Visible = True
                        FolioAnt = ""
                    Else
                        FolioAnt = TextBox2.Text
                    End If
                    Dim O As New dbOpciones(MySqlcon)
                    Dim CM As New dbMonedasConversiones(MySqlcon)
                    CM.Modificar(1, CDbl(TextBox10.Text))
                    'ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(ComboBox1.SelectedIndex))
                    C.DaTotal(idNota, 2)
                    Dim Sf As New dbSucursalesFolios(MySqlcon)
                    Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Nominas, GlobalTipoFacturacion)
                    Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                    C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text, CDbl(TextBox10.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, 1, 2, Isr, IvaRetenido, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), Format(DateTimePicker4.Value, "yyyy/MM/dd"), Banco, CLABE, CDbl(txtdiaspagados.Text), CInt(txtantiguedad.Text), 0, idsFormasDePago.Valor(ComboBox6.SelectedIndex), If(comboTipo.SelectedIndex = 0, "O", "E"), "NA", 0)
                    idNota = C.ID
                    Estado = 1
                    'Button1.Text = "Modificar"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    Button14.Enabled = True
                    'LlenaDatosDetalles()
                Else
                    MsgBox("Debe indicar un trabajador", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        'BuscaArticulo()
    End Sub
    'Private Sub BuscaArticulo()
    '    Try
    '        If ConsultaOn Then
    '            Dim p As New dbInventario(MySqlcon)
    '            If p.BuscaArticulo(TextBox3.Text, 0) Then
    '                LlenaDatosArticulo(p)
    '            Else
    '                IdInventario = 0
    '                Dim ps As New dbProductos(MySqlcon)
    '                If ps.BuscaProducto(TextBox3.Text) Then
    '                    'LlenaDatosProducto(ps)
    '                Else
    '                    TextBox4.Text = ""
    '                    TextBox6.Text = "0"
    '                    TextBox8.Text = "0"
    '                    TextBox9.Text = "0"
    '                    PrecioU = 0
    '                    IdVariante = 0
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub
    Private Sub LlenaDatosVenta()
        Dim C As New dbNominas(idNota, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio
        TextBox1.Text = C.Trabajador.NumeroEmpleado
        Estado = C.Estado
        EsElectronica = C.EsElectronica
        TextBox8.Text = C.Iva.ToString
        TextBox11.Text = C.Serie
        txtdiaspagados.Text = C.DiasPagados.ToString
        TextBox10.Text = C.TipodeCambio.ToString
        TextBox14.Text = C.Comentario
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        DateTimePicker2.Value = C.FechaPago
        DateTimePicker3.Value = C.FechaInicialPago
        DateTimePicker4.Value = C.FechaFinalPAgo
        txtantiguedad.Text = C.Antiguedad.ToString
        TextBox18.Text = C.TotalaPagar.ToString
        MontoRecurso = C.montoRecurso
        OrigenRecurso = C.origenRecurso
        ComboBox6.SelectedIndex = idsFormasDePago.Busca(C.Idforma)
        ComboBox3.Enabled = False
        'ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        'ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.Id)
        'cmbConcepto.SelectedIndex = IdsTipos.Busca(C.IdConcepto)
        LlenaDatosDetalles()
        Button27.Enabled = False
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Panel3.Enabled = False
                Panel4.Enabled = False
                Button2.Enabled = False
                Button26.Enabled = True
                DateTimePicker2.Enabled = False
                DateTimePicker3.Enabled = False
                DateTimePicker4.Enabled = False
                txtantiguedad.Enabled = False
                txtdiaspagados.Enabled = False
                'Button25.Enabled = False
                TextBox18.Enabled = False
                Button35.Enabled = True
            Case Estados.Guardada
                Button35.Enabled = False
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Button2.Enabled = False
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
                Panel3.Enabled = False
                Panel4.Enabled = False
                Button26.Enabled = True
                DateTimePicker2.Enabled = False
                DateTimePicker3.Enabled = False
                DateTimePicker4.Enabled = False
                txtantiguedad.Enabled = False
                txtdiaspagados.Enabled = False
                'Button25.Enabled = False
                TextBox18.Enabled = False
            Case Else
                Button35.Enabled = False
                Label24.Visible = False
                Button13.Enabled = False
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Panel3.Enabled = True
                Panel4.Enabled = True
                DateTimePicker2.Enabled = True
                DateTimePicker3.Enabled = True
                DateTimePicker4.Enabled = True
                txtantiguedad.Enabled = True
                txtdiaspagados.Enabled = True
                'Button25.Enabled = True
                TextBox18.Enabled = True
                Button26.Enabled = False
        End Select
    End Sub
    Private Sub LlenaDatosDetalles()
        Panel1.Visible = True
        ConsultaDetalles()
        ConsultaDetallesHorasExtra()
        ConsultaDetallesIncapacidad()
    End Sub
    Private Sub ConsultaDetalles()
        Try

            'Tabla.Rows.Clear()
            'Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbNominasDetalles(MySqlcon)
            'T = CD.Consulta(idNota)
            'While T.Read
            '    'If T("idinventario") > 1 Then
            '    Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("precio"), T("abreviatura"))
            '    'Else
            '    'Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("precio"), T("abreviatura"))
            '    'End If
            'End While
            'T.Close()
            DGDetalles.DataSource = CD.Consulta(idNota)
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).HeaderText = "Mov."
            DGDetalles.Columns(2).HeaderText = "Tipo P/D"
            DGDetalles.Columns(3).HeaderText = "Clave"
            DGDetalles.Columns(4).HeaderText = "Concepto"
            DGDetalles.Columns(5).HeaderText = "Imp. Gravado"
            DGDetalles.Columns(6).HeaderText = "Imp. Exento"
            'DGDetalles.Columns(1).Visible = False
            'DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub ConsultaDetallesIncapacidad()
        Try

            'Tabla.Rows.Clear()
            'Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbNominasIncapacidades(MySqlcon)
            'T = CD.Consulta(idNota)
            'While T.Read
            '    'If T("idinventario") > 1 Then
            '    Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("precio"), T("abreviatura"))
            '    'Else
            '    'Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("precio"), T("abreviatura"))
            '    'End If
            'End While
            'T.Close()
            DGIncapacidades.DataSource = CD.Consulta(idNota)
            DGIncapacidades.Columns(0).Visible = False
            DGIncapacidades.Columns(1).HeaderText = "Tipo"
            DGIncapacidades.Columns(2).HeaderText = "Dias"
            DGIncapacidades.Columns(3).HeaderText = "Descuento"
            'DGDetalles.Columns(1).Visible = False
            'DGDetalles.Columns(2).Visible = False
            DGIncapacidades.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DGIncapacidades.RowCount > DGIncapacidades.DisplayedRowCount(False) Then DGIncapacidades.FirstDisplayedScrollingRowIndex = DGIncapacidades.RowCount - DGIncapacidades.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub ConsultaDetallesHorasExtra()
        Try
            Dim CD As New dbNoominaHorasExtra(MySqlcon)
            
            DGHorasextra.DataSource = CD.Consulta(idNota)
            DGHorasextra.Columns(0).Visible = False
            DGHorasextra.Columns(1).HeaderText = "Tipo"
            DGHorasextra.Columns(2).HeaderText = "Dias"
            DGHorasextra.Columns(3).HeaderText = "Horas"
            DGHorasextra.Columns(4).HeaderText = "Importe"
            DGHorasextra.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DGHorasextra.RowCount > DGHorasextra.DisplayedRowCount(False) Then DGHorasextra.FirstDisplayedScrollingRowIndex = DGHorasextra.RowCount - DGHorasextra.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        IdVariante = 0
        IdServicio = 0
        'cmbVariante.Visible = False
        TextBox12.Text = "0"
        TextBox3.Text = ""
        Button6.Visible = False
        Button12.Visible = False
        Button31.Visible = False
        Button32.Visible = False
        TextBox5.Text = ""
        If CheckBox7.Checked Then
            Dim IdCuenta As Integer
            IdCuenta = Nom.DaIdCuentaConcepto(IdsTipos.Valor(ComboBox8.SelectedIndex), cmbVariante.SelectedIndex)
            If IdCuenta = 0 Then
                TextBox5.Text = TextBox4.Text.Substring(0, 3)
            Else
                TextBox5.Text = Cp.DaCuentaTxt(IdCuenta).Trim.Replace(" ", "")
            End If
        End If
        TextBox9.Text = "0"
        TextBox6.Text = "0"
        TextBox8.Text = "0"
        PrecioBase = 0
        'cmbVariante.Visible = False
        ComboBox8.SelectedIndex = 0
        TextBox4.Text = ComboBox8.Text
        'Button12.Visible = False
        Button9.Enabled = False
        ValorMercado = 0
        PrecioalOtorgarse = 0
        Button6.Visible = False
        Button12.Visible = False
        Button31.Visible = False
        Button32.Visible = False
        'ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        ComboBox8.Focus()
    End Sub
    Private Sub NuevoIncapacidad()
        ComboBox4.SelectedIndex = 0
        TextBox19.Text = "0"
        TextBox16.Text = "0"
        Button16.Text = "Agregar Incapacidad"
        Button20.Enabled = False

    End Sub
    Private Sub NuevoHorasExtra()
        ComboBox5.SelectedIndex = 0
        TextBox15.Text = "0"
        TextBox13.Text = "0"
        TextBox17.Text = "0"
        Button23.Text = "Agregar Horas Extra"
        Button22.Enabled = False

    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbNominasDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            Dim ClavePer As Integer = 0
            If IsNumeric(TextBox8.Text) = False Or IsNumeric(TextBox9.Text) = False Then
                MsgError += vbCrLf + "Los importe deben ser un valor numérico."
                HayError = True

            End If

            If TextBox5.Text.Length < 3 Then
                MsgError += vbCrLf + "La clave debe contener mínimo 3 caracteres."
                HayError = True
            End If
            If TextBox4.Text = "" Then
                MsgError += vbCrLf + "Debe indicar un concepto."
                HayError = True
            End If
            ClavePer = CInt(TextBox4.Text.Substring(0, 3))
            If DateTimePicker1.Value.ToString("yyyy/MM/dd") >= "2017/01/01" Then
                If cmbVariante.SelectedIndex = 0 And ClavePer = 45 Then
                    Dim Fmd As New frmNominaMasDatos(Estado)
                    Fmd.PrecioOtorgarse = PrecioalOtorgarse
                    Fmd.ValorMercado = ValorMercado
                    Fmd.Tipo = 1
                    If Fmd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        PrecioalOtorgarse = Fmd.PrecioOtorgarse
                        ValorMercado = Fmd.ValorMercado
                    End If
                    Fmd.Dispose()
                End If
            End If
            If HayError = False Then
                If Button4.Text = "Agregar Concepto" Then
                    CD.Guardar(idNota, IdsTipos.Valor(ComboBox8.SelectedIndex), cmbVariante.SelectedIndex, TextBox5.Text, TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), ValorMercado, PrecioalOtorgarse)
                Else
                    CD.Modificar(IdDetalle, IdsTipos.Valor(ComboBox8.SelectedIndex), cmbVariante.SelectedIndex, TextBox5.Text, TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), ValorMercado, PrecioalOtorgarse)
                End If
                If DateTimePicker1.Value.ToString("yyyy/MM/dd") >= "2017/01/01" Then
                    If cmbVariante.SelectedIndex = 0 And ClavePer = 19 Then
                        Dim Fmd As New frmNominaMasDatos(Estado)
                        Fmd.IdDetalle = CD.ID
                        Fmd.Tipo = 2
                        Fmd.ShowDialog()
                        Fmd.Dispose()
                    End If
                    If cmbVariante.SelectedIndex = 0 And (ClavePer = 22 Or ClavePer = 23 Or ClavePer = 25) Then
                        Dim Fmd As New frmNominaMasDatos(Estado)
                        Fmd.IdNomina = idNota
                        Fmd.Tipo = 3
                        Fmd.ShowDialog()
                        Fmd.Dispose()
                    End If
                    If cmbVariante.SelectedIndex = 0 And ClavePer = 39 Then
                        Dim Fmd As New frmNominaMasDatos(Estado)
                        Fmd.IdNomina = idNota
                        Fmd.Tipo = 4
                        Fmd.ShowDialog()
                        Fmd.Dispose()
                    End If
                    If cmbVariante.SelectedIndex = 0 And ClavePer = 44 Then
                        Dim Fmd As New frmNominaMasDatos(Estado)
                        Fmd.IdNomina = idNota
                        Fmd.Tipo = 5
                        Fmd.ShowDialog()
                        Fmd.Dispose()
                    End If
                    
                End If
                NuevoConcepto()
                ConsultaDetalles()
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub AgregaIncapacidad()
        Try
            Dim CD As New dbNominasIncapacidades(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            If IsNumeric(TextBox19.Text) = False Or IsNumeric(TextBox16.Text) = False Then
                MsgError += vbCrLf + "El descuento y los dias deben ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox19.Text) = 0 Or CDbl(TextBox16.Text) = 0 Then
                    MsgError += vbCrLf + "Las cantidades deben ser diferente de cero."
                    HayError = True
                End If
            End If
            If HayError = False Then
                If Button16.Text = "Agregar Incapacidad" Then
                    CD.Guardar(idNota, ComboBox4.SelectedIndex, CInt(TextBox19.Text), CDbl(TextBox16.Text))
                    ConsultaDetallesIncapacidad()
                    NuevoIncapacidad()
                Else
                    CD.Modificar(IdDetalleI, ComboBox4.SelectedIndex, CInt(TextBox19.Text), CDbl(TextBox16.Text))
                    ConsultaDetallesIncapacidad()
                    NuevoIncapacidad()
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub AgregaHorasExtra()
        Try
            Dim CD As New dbNoominaHorasExtra(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            'If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            'If IsNumeric(TextBox5.Text) Then
            '    If CDbl(TextBox5.Text) <= 0 Then
            '        MsgError += "La cantidad debe ser un valor mayor a 0."
            '        HayError = True
            '    End If
            'Else
            '    MsgError += "La cantidad debe ser un valor numérico."
            '    HayError = True
            'End If
            If IsNumeric(TextBox15.Text) = False Or IsNumeric(TextBox13.Text) = False Or IsNumeric(TextBox17.Text) = False Then
                MsgError += vbCrLf + "El descuento, los dias y el importe deben ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox15.Text) = 0 Or CDbl(TextBox13.Text) = 0 Or CDbl(TextBox17.Text) = 0 Then
                    MsgError += vbCrLf + "Las cantidades deben ser diferente de cero."
                    HayError = True
                End If
            End If
            If HayError = False Then
                If Button23.Text = "Agregar Horas Extra" Then
                    CD.Guardar(idNota, CInt(TextBox15.Text), ComboBox5.Text, CInt(TextBox13.Text), CDbl(TextBox17.Text))
                    ConsultaDetallesHorasExtra()
                    NuevoHorasExtra()
                Else
                    CD.Modificar(IdDetalleH, CInt(TextBox15.Text), ComboBox5.Text, CInt(TextBox13.Text), CDbl(TextBox17.Text))
                    ConsultaDetallesHorasExtra()
                    NuevoHorasExtra()
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub BotonGuardarArriba()
        If MsgBox("¿Guardar Recibo de Nómina? Al guardarlo no se podrá modificar.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
                If Estado = 0 Then
                    Guardar()
                End If
                If Estado <> 0 Then
                    Modificar(Estados.Guardada, False)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                TextBox5.Focus()
            End If
        End If
    End Sub
    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaArticulo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox5.Focus()
        End If
    End Sub
    Private Sub BotonAgregarIncapacidad()
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaIncapacidad()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'TextBox5.Focus()
        End If
    End Sub
    Private Sub BotonAgregarHorasExtra()
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaHorasExtra()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'TextBox5.Focus()
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try
            Button6.Visible = False
            Button12.Visible = False
            Button31.Visible = False
            Button32.Visible = False
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbNominasDetalles(IdDetalle, MySqlcon)
            'ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)

            TextBox8.Text = CD.ImporteGravado
            TextBox9.Text = CD.ImporteExento
            PrecioalOtorgarse = CD.precioAlOtorgarse
            ValorMercado = CD.valorMercado
            cmbVariante.SelectedIndex = CD.TipoPercepcionDeduccion
            ComboBox8.SelectedIndex = IdsTipos.Busca(CD.Tipo)
            TextBox5.Text = CD.Clave
            TextBox4.Text = CD.Concepto
            Dim ClavePer As Integer
            ClavePer = CInt(TextBox4.Text.Substring(0, 3))
            If DateTimePicker1.Value.ToString("yyyy/MM/dd") >= "2017/01/01" Then
                If ClavePer = 45 And cmbVariante.SelectedIndex = 0 Then Button6.Visible = True
                If ClavePer = 19 And cmbVariante.SelectedIndex = 0 Then Button12.Visible = True
                If (ClavePer = 22 Or ClavePer = 23 Or ClavePer = 25) And cmbVariante.SelectedIndex = 0 Then Button31.Visible = True
                If (ClavePer = 39 Or ClavePer = 44) And cmbVariante.SelectedIndex = 0 Then Button32.Visible = True
            End If
            Button4.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nomina no ha sido guardada. ¿Desea iniciar un nuevo recibo de nomina? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbNominas(MySqlcon)
                c.Eliminar(idNota)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        BotonNuevo()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
                If MsgBox("¿Desea eliminar este concepto de la nomina?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbNominasDetalles(MySqlcon)
                    CD.Eliminar(IdDetalle)
                    'Dim S As New dbInventarioSeries(MySqlcon)
                    'S.QuitarSeriesAventaxArticulo(IdInventario, idNota)
                    'If IdInventario <> 0 Then
                    '    Dim I As New dbInventario(MySqlcon)
                    '    I.MovimientoDeInventario(IdInventario, CantAnt, CantAnt, dbInventario.TipoMovimiento.Alta, IdAlmacen)
                    'End If
                    'If IdVariante <> 0 Then
                    '    Dim PV As New dbProductosVariantes(MySqlcon)
                    '    PV.ModificaInventario(IdVariante, CantAnt * -1, IdAlmacen)
                    'End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Concepto Eliminado", 90)
                End If

            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub BotonCliente()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Trabajador, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox7.Text = B.Trabajador.Nombre + vbCrLf + B.Trabajador.RFC + vbCrLf + B.Trabajador.Direccion + " " + B.Trabajador.NoExterior + " " + B.Trabajador.Ciudad + " " + B.Trabajador.CP
            idCliente = B.Trabajador.ID
            Banco = B.Trabajador.Banco
            CLABE = B.Trabajador.CLABE
            FechaInicioLaboral = CDate(B.Trabajador.FechaInicioLaboral)
            txtantiguedad.Text = CStr(Math.Truncate((DateDiff(DateInterval.Day, CDate(B.Trabajador.FechaInicioLaboral), Date.Now) + 1) / 7))
            ConsultaOn = False
            TextBox1.Text = B.Trabajador.NumeroEmpleado
            ConsultaOn = True
            DateTimePicker2.Focus()
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BotonCliente()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SacaTotal()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        SacaTotal()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmNominasConsulta(ModosDeBusqueda.Secundario, "")
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idNota = f.IdVenta
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta nomina?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim C As New dbNominas(MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idNota)
                C.Eliminar(idNota)
                PopUp("Nomina eliminada.", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub



    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada, False)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NomminaCancelar, PermisosN.Secciones.Nomina) = True Then
            If MsgBox("¿Cancelar esta nomina?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'If EsElectronica = 2 And GlobalConector = 1 Then
                Dim V As New dbNominas(idNota, MySqlcon)
                V.DaDatosTimbrado(idNota)
                Dim op As New dbOpciones(MySqlcon)
                If EsElectronica = 2 And GlobalConector = 1 And GlobalPacCFDI = 1 Then
                    V.DaDatosTimbrado(idNota)
                    Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                    If V.CancelarTimbrado(S.RFC, V.uuid) = 1 Then
                        Modificar(Estados.Cancelada, False)
                    Else
                        MsgBox("Error en la cancelación. Intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If EsElectronica = 2 And GlobalPacCFDI = 2 Then
                        V.DaDatosTimbrado(idNota)
                        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                        If V.CancelarTimbrado2(S.RFC, V.uuid, op._ApiKey) = 1 Then
                            Modificar(Estados.Cancelada, False)
                        Else
                            MsgBox("Error en la cancelación. Intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        Modificar(Estados.Cancelada, False)
                    End If
                End If
                'Else
                '   Modificar(Estados.Cancelada)
                'End If

            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    'Private Sub CadenaOriginal()
    '    Dim en As New Encriptador
    '    Dim V As New dbNominas(idNota, MySqlcon)
    '    Dim RutaXML As String
    '    Dim RutaPDF As String
    '    'TextBox9.Text = 
    '    'TextBox10.Text = 
    '    'en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
    '    If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
    '        Cadena = V.CreaCadenaOriginal22(idNota, GlobalIdMoneda)
    '    Else
    '        Cadena = V.CreaCadenaOriginal(idNota, GlobalIdMoneda)
    '    End If

    '    Dim Archivos As New dbSucursalesArchivos
    '    Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
    '    RutaXML = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoXML, False)
    '    RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoPDF, False)
    '    Archivos.CierraDB()
    '    Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"))
    '    Dim Enc As New System.Text.UTF8Encoding
    '    Dim xmldoc As String
    '    If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
    '        xmldoc = V.CreaXML22(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
    '    Else
    '        xmldoc = V.CreaXML(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
    '    End If
    '    Dim Bytes() As Byte = Enc.GetBytes(xmldoc)
    '    'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    IO.Directory.CreateDirectory(RutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
    '    RutaXML = RutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
    '    'en.GuardaArchivo(My.Settings.rutaxmlnca + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCA-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
    '    en.GuardaArchivoTexto(RutaXML + "\NCA-" + V.Serie + V.Folio.ToString + ".xml", xmldoc, System.Text.Encoding.UTF8)
    '    PrintDocument1.DocumentName = "NCA-" + V.Serie + V.Folio.ToString
    '    Dim SA As New dbSucursalesArchivos
    '    Dim Impresora As String
    '    Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.VentaNotadeCargo)
    '    TipoImpresora = SA.TipoImpresora
    '    If Impresora = "Bullzip PDF Printer" Then
    '        Dim obj As New Bullzip.PdfWriter.PdfSettings
    '        obj.Init()
    '        obj.PrinterName = Impresora
    '        'obj.WriteSettings()

    '        obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
    '        obj.SetValue("ShowSettings", "never")
    '        obj.SetValue("ShowPDF", "yes")
    '        obj.SetValue("ShowSaveAS", "nofile")
    '        obj.SetValue("ConfirmOverwrite", "no")
    '        obj.SetValue("Target", "printer")
    '        obj.WriteSettings()
    '    End If
    '    PrintDocument1.PrinterSettings.PrinterName = Impresora
    '    LlenaNodosImpresion()
    '    If TipoImpresora = 0 Then
    '        LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo)
    '    Else
    '        LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargoTicket)
    '    End If
    '    PrintDocument1.Print()
    '    'Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\NCA-" + V.Serie + V.Folio.ToString + ".pdf", 1000)
    '    If V.Cliente.Email <> "" Then
    '        Try
    '            If MsgBox("¿Enviar nota de cargo por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '                If V.Cliente.Email <> "" Then
    '                    Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
    '                    Dim O As New dbOpciones(MySqlcon)
    '                    Dim C As String
    '                    C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "NOTA DE CARGO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
    '                    M.send("Comprobante fiscal digital Nota de cargo " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\NCA-" + V.Serie + V.Folio.ToString + ".pdf", RutaXML + "\NCA-" + V.Serie + V.Folio.ToString + ".xml")
    '                End If
    '            End If
    '        Catch ex As Exception
    '            MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '        End Try
    '    End If
    'End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' cmbConcepto.Focus()
        End If
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        IVaDefault = S.Impuesto
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Nominas, GlobalTipoFacturacion)
        TextBox11.Text = Sf.Serie
        If Sf.EsElectronica > 0 Then
            CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
        End If
        Dim V As New dbNominas(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        'If IdInventario <> 0 Or IdVariante <> 0 Then
        If IsNumeric(TextBox5.Text) Then
            TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
        End If
        'End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaImportar, PermisosN.Secciones.Nomina) = True Then

            OpenFileDialog1.Filter = "*.xls|*.xlsx"
            If MsgBox("¿Importar nominas? Este proceso puede tardar unos minutos y no puede detenerse. ¿Desea continuar?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Dim I As New Importador(OpenFileDialog1.FileName, MySqlcon)
                    Try
                        ''TextBox26.Text = OpenFileDialog1.FileName
                        'If GlobalTipoVersion = 3 Then
                        For Each c As Control In Me.Controls
                            c.Enabled = False
                        Next
                        'End If

                        Dim Sf As New dbSucursalesFolios(MySqlcon)
                        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Nominas, GlobalTipoFacturacion)
                        Dim SC As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                        ''DataGridView1.DataSource = I.ConsultaExcel()
                        I.ImportarNominas2(IdsSucursales.Valor(ComboBox3.SelectedIndex), SC.NoSerie, Sf.Serie, TextBox5.Text)
                        I.CierraConexiones()

                        Dim Nom As New dbNominas(MySqlcon)
                        Nom.DaIdsPorTimbrar()

                        For Each Id As Integer In Nom.IdsPorTimbrar
                            idNota = Id
                            LlenaDatosVenta()
                            Modificar(Estados.Guardada, True)
                            Nuevo()
                        Next
                        If I.Fallos = "" Then
                            MsgBox("Listo", MsgBoxStyle.OkOnly, GlobalNombreApp)
                        Else
                            MsgBox("Algunas nóminas no pudieron procesarse:" + vbCrLf + I.Fallos, MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                        'If GlobalTipoVersion = 3 Then
                        For Each c As Control In Me.Controls
                            c.Enabled = True
                        Next
                        Nuevo()
                        ' End If
                        'Label40.Text = I.ConsultaExcelReader
                    Catch ex As Exception
                        I.CierraConexiones()
                        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                        For Each c As Control In Me.Controls
                            c.Enabled = True
                        Next
                        Nuevo()
                    End Try

                End If
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    'Version Normal
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = ImpDoc.MasPaginas

    End Sub
    '---------------Termina Version Normal
    '--------------------------------------------------------------------
    
    
    Private Sub Imprimir(pIdNomina As Integer)
        Try
            Dim Nomina As New dbNominas(pIdNomina, MySqlcon)
            ImpDoc.IdSucursal = Nomina.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.Nominas
            ImpDoc.TipoDocumentoT = TiposDocumentos.Nominas + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.Nominas
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.NominasPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "PSSNOMINA-" + Nomina.Serie + Nomina.Folio.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click

        Select Case EsElectronica
            Case 0
                Imprimir(idNota)
            Case 1
                Imprimir(idNota)
            Case 2
                CadenaOriginali(Estado, False)
            Case 3
                CadenaOriginali33(Estado, False)
        End Select
        
    End Sub

   
    

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub


    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        'If IdInventario <> 0 Or IdVariante <> 0 Then
        If IsNumeric(TextBox12.Text) Then
            PrecioU = CDbl(TextBox12.Text)
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
        'End If
    End Sub

    

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub CadenaOriginali(ByVal pEstado As Byte, ByVal SinImprimir As Boolean)
        Dim en As New Encriptador
        Dim V As New dbNominas(idNota, MySqlcon)
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        'If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
        If V.Fecha >= "2017/01/01" And Op.ActNom12 = 1 Then
            Cadena = V.CreaCadenaOriginali32n12(idNota, GlobalIdMoneda)
        Else
            Cadena = V.CreaCadenaOriginali32(idNota, GlobalIdMoneda)
        End If
        'Else
        'Cadena = V.CreaCadenaOriginali(idNota, GlobalIdMoneda)
        'End If
        'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        'RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        If op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        End If
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".xml"


        'RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        Dim strXML As String
        'If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
        If V.Fecha >= "2017/01/01" And Op.ActNom12 = 1 Then
            strXML = V.CreaXMLi32n12(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        Else
            strXML = V.CreaXMLi32(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        End If
        'Else
        'strXML = V.CreaXMLi(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        'End If

        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        V.NoCertificadoSAT = ""
        V.DaDatosTimbrado(idNota)
        If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
            If GlobalPacCFDI = 0 Then
                'en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                Dim sa As New dbSucursalesArchivos
                sa.DaOpciones(GlobalIdEmpresa, True)
                Timbre = V.Timbrar(S.RFC, V.Trabajador.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado)

                V.NoCertificadoSAT = Timbre.noCertificadoSAT
                If V.NoCertificadoSAT <> "Error" Then
                    V.uuid = Timbre.UUID
                    V.FechaTimbrado = Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss")
                    V.SelloCFD = Timbre.selloCFD
                    V.NoCertificadoSAT = Timbre.noCertificadoSAT
                    V.SelloSAT = Timbre.selloSAT
                    V.GuardaDatosTimbrado(idNota, Timbre.UUID, Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss"), Timbre.selloCFD, Timbre.noCertificadoSAT, Timbre.selloSAT)
                    Dim strTimbrado As String
                    strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                    strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                    strTimbrado += "</cfdi:Complemento>" + vbCrLf
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), strTimbrado)
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If
            End If
            If GlobalPacCFDI = 1 Then
                Dim Os As New dbOpciones(MySqlcon)
                If GlobalConector = 0 Then
                    en.GuardaArchivoTexto(RutaXmlTemp, strXML, System.Text.Encoding.UTF8)
                    If IO.File.Exists(RutaXml) Then
                        IO.File.Delete(RutaXml)
                    End If
                Else
                    If IO.File.Exists(RutaXmlTimbrado) Then
                        IO.File.Delete(RutaXmlTimbrado)
                    End If
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If

                If V.Timbrar2(Os._UsuarioFacCom, Os._passFacCom, S.RFC, RutaXmlTemp, RutaXml, GlobalConector) = 0 Then
                    V.NoCertificadoSAT = "Error"
                End If
                Dim xmldoc As New Xml.XmlDocument
                'Dim xmldoc2 As New Xml.XmlDocument
                If GlobalConector = 0 Then
                    xmldoc.Load(RutaXml)
                    If xmldoc.DocumentElement.Name = "ERROR" Then
                        V.NoCertificadoSAT = "Error"
                        MsgError = xmldoc.InnerText
                    End If
                Else
                    Dim ChecarXML As String
                    ChecarXML = en.LeeArchivoTexto(RutaXmlTimbrado)
                    If ChecarXML.StartsWith("ERROR") Then
                        MsgError = ChecarXML
                        V.NoCertificadoSAT = "Error"
                    Else
                        If ChecarXML.StartsWith("<?xml version=""1.0"" encoding=""utf-8""?>" + vbCrLf + "<?xml version=""1.0"" encoding=""utf-8""?>") Then
                            ChecarXML = ChecarXML.Substring(40, ChecarXML.Length - 40)
                            en.GuardaArchivoTexto(RutaXmlTimbrado, ChecarXML, System.Text.Encoding.UTF8)
                        End If
                        xmldoc.Load(RutaXmlTimbrado)
                    End If

                    'xmldoc2.Load(RutaXmlTimbrado)
                End If
                If V.NoCertificadoSAT <> "Error" Then
                    '    V.NoCertificadoSAT = "Error"
                    '    MsgError = xmldoc.InnerText
                    'Else
                    V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                    V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                    V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                    V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value

                    V.GuardaDatosTimbrado(idNota, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                    If IO.File.Exists(RutaXmlTemp) Then
                        IO.File.Delete(RutaXmlTemp)
                    End If
                    If IO.File.Exists(RutaXml) And GlobalConector = 1 Then
                        IO.File.Delete(RutaXml)
                    End If
                End If
            End If
            If GlobalPacCFDI = 2 Then
                Dim Os As New dbOpciones(MySqlcon)
                en.GuardaArchivoTexto(Application.StartupPath + "\temp.xml", strXML, System.Text.Encoding.UTF8)
                Dim Timbre As String
                Dim sa As New dbSucursalesArchivos
                sa.DaOpciones(GlobalIdEmpresa, True)
                Timbre = V.Timbrar3(S.RFC, strXML, "", Os._ApiKey, V.Serie, V.Folio)
                If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                    Dim xmldoc As New Xml.XmlDocument
                    en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                    xmldoc.Load(RutaXmlTimbrado)
                    V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                    V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                    V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                    V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                    V.GuardaDatosTimbrado(idNota, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                Else
                    MsgError = Timbre
                    V.NoCertificadoSAT = "Error"
                End If
            End If
        Else
            'Crear xml timbrado
            Dim ExisteArchivo As Boolean = False
            If GlobalConector = 0 Then
                If IO.File.Exists(RutaXml) Then ExisteArchivo = True
            Else
                If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
            End If


            If pEstado = Estados.Guardada And ExisteArchivo = False Then
                Dim strTimbrado As String
                strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                strTimbrado += "</cfdi:Complemento>" + vbCrLf
                strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), strTimbrado)
                If GlobalConector = 0 Then
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                Else
                    en.GuardaArchivoTexto(RutaXmlTimbrado, strXML, System.Text.Encoding.UTF8)
                End If
            End If

        End If
        If V.NoCertificadoSAT <> "Error" Then
                CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
            Try
                Imprimir(idNota)
            Catch ex As Exception

            End Try
            'If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".pdf", 1000)

            If V.Trabajador.Email <> "" Then
                Try
                    If MsgBox("¿Enviar recibo de nómina por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If V.Trabajador.Email <> "" Then
                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            Dim C As String
                            C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "RECIBO DE NÓMINA" + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                            C += Op.CorreoContenido
                            'If GlobalConector = 0 Then
                            'M.send("Comprobante Fiscal Digital por Internet Nómina: " + V.uuid, C, V.Trabajador.Email, V.Cliente.Nombre, RutaPDF + "\PDFNCACFDI" + V.Serie + V.Folio.ToString + ".pdf", RutaXml)
                            'Else
                            If Estado = Estados.Guardada Or Estado = Estados.Cancelada Then
                                M.send("Comprobante Fiscal Digital por Internet Nómina: " + V.uuid, C, V.Trabajador.Email, V.Trabajador.Nombre, RutaPDF + "\PSSNOMINA-" + V.Serie + V.Folio.ToString("0000") + ".pdf", RutaXmlTimbrado)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet Nómina: " + V.uuid, C, V.Trabajador.Email, V.Trabajador.Nombre, RutaPDF + "\PSSNOMINA-" + V.Serie + V.Folio.ToString("0000") + ".pdf", "")
                            End If
                            PopUp("Correo Enviado", 90)
                            'End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End Try
            End If
            'Dim Ss As New dbInventarioSeries(MySqlcon)
            'If Ss.CantidadDeSeriesAgregadasaVenta(idVenta, 0) > 0 Then
            '    If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        ImprimirSeries()
            '    End If
            'End If
        Else
            MsgBox("Ha ocurrido un error en el timbrado del recibo de nómina, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardar nómina como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idNota, Estados.Pendiente)
                Nuevo()
            Else
                'Dim Se As New dbInventarioSeries(MySqlcon)
                'Se.QuitaSeriesAVenta(idVenta)
                'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idNota)
                PopUp("Nómina Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
        End If
    End Sub

    Private Sub CadenaOriginali33(ByVal pEstado As Byte, ByVal SinImprimir As Boolean)
        Dim en As New Encriptador
        Dim V As New dbNominas(idNota, MySqlcon)
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        Cadena = V.CreaCadenaOriginali33(idNota, GlobalIdMoneda, "", GlobalIdEmpresa, "", 0, "")
        en.GuardaArchivoTexto(Application.StartupPath + "\co.txt", Cadena, System.Text.Encoding.UTF8)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        'RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        If Op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        End If
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".xml"



        Dim strXML As String
        strXML = V.CreaXMLi33(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", 0)
        
        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        V.NoCertificadoSAT = ""
        V.DaDatosTimbrado(idNota)
        If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
            If GlobalPacCFDI = 2 Then
                Dim Os As New dbOpciones(MySqlcon)
                en.GuardaArchivoTexto(Application.StartupPath + "\temp.xml", strXML, System.Text.Encoding.UTF8)
                Dim Timbre As String
                Dim sa As New dbSucursalesArchivos
                sa.DaOpciones(GlobalIdEmpresa, True)
                Timbre = Timbrar33(S.RFC, strXML, "", Os._ApiKey, True, V.Folio, V.Serie, "Nomina", V.ID)
                If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                    Dim xmldoc As New Xml.XmlDocument
                    en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                    xmldoc.Load(RutaXmlTimbrado)
                    V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloCFD").Value
                    V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("NoCertificadoSAT").Value
                    V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                    V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("SelloSAT").Value
                    V.GuardaDatosTimbrado(idNota, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                Else
                    MsgError = Timbre
                    V.NoCertificadoSAT = "Error"
                End If
            End If
        Else
            'Crear xml timbrado
            Dim ExisteArchivo As Boolean = False
            If GlobalConector = 0 Then
                If IO.File.Exists(RutaXml) Then ExisteArchivo = True
            Else
                If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
            End If


            If pEstado = Estados.Guardada And ExisteArchivo = False Then
                Dim strTimbrado As String
                strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                strTimbrado += "<tfd:TimbreFiscalDigital Version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ SelloCFD=""" + V.SelloCFD + """ NoCertificadoSAT=""" + V.NoCertificadoSAT + """ SelloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                strTimbrado += "</cfdi:Complemento>" + vbCrLf
                strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>"), strTimbrado)
                If GlobalConector = 0 Then
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                Else
                    en.GuardaArchivoTexto(RutaXmlTimbrado, strXML, System.Text.Encoding.UTF8)
                End If
            End If

        End If
        If V.NoCertificadoSAT <> "Error" Then
            CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
            Try
                Imprimir(idNota)
            Catch ex As Exception

            End Try
            'If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\PSSNOMINA-" + V.Serie + V.Folio.ToString + ".pdf", 1000)

            If V.Trabajador.Email <> "" Then
                Try
                    If MsgBox("¿Enviar recibo de nómina por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If V.Trabajador.Email <> "" Then
                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            Dim C As String
                            C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "RECIBO DE NÓMINA" + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                            C += Op.CorreoContenido
                            'If GlobalConector = 0 Then
                            'M.send("Comprobante Fiscal Digital por Internet Nómina: " + V.uuid, C, V.Trabajador.Email, V.Cliente.Nombre, RutaPDF + "\PDFNCACFDI" + V.Serie + V.Folio.ToString + ".pdf", RutaXml)
                            'Else
                            If Estado = Estados.Guardada Or Estado = Estados.Cancelada Then
                                M.send("Comprobante Fiscal Digital por Internet Nómina: " + V.uuid, C, V.Trabajador.Email, V.Trabajador.Nombre, RutaPDF + "\PSSNOMINA-" + V.Serie + V.Folio.ToString("0000") + ".pdf", RutaXmlTimbrado)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet Nómina: " + V.uuid, C, V.Trabajador.Email, V.Trabajador.Nombre, RutaPDF + "\PSSNOMINA-" + V.Serie + V.Folio.ToString("0000") + ".pdf", "")
                            End If
                            PopUp("Correo Enviado", 90)
                            'End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End Try
            End If
            'Dim Ss As New dbInventarioSeries(MySqlcon)
            'If Ss.CantidadDeSeriesAgregadasaVenta(idVenta, 0) > 0 Then
            '    If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        ImprimirSeries()
            '    End If
            'End If
        Else
            MsgBox("Ha ocurrido un error en el timbrado del recibo de nómina, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardar nómina como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idNota, Estados.Pendiente)
                Nuevo()
            Else
                'Dim Se As New dbInventarioSeries(MySqlcon)
                'Se.QuitaSeriesAVenta(idVenta)
                'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idNota)
                PopUp("Nómina Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
        End If
    End Sub

    Private Sub LlenaNodosImpresion()

        Dim V As New dbNominas(idNota, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        'Dim O As New dbOpciones(MySqlcon)
        V.DaTotal(idNota, 2)
        V.DaDatosTimbrado(idNota)


        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombretrabajador", V.Trabajador.Nombre, 0), "nombretrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "numtrabajador", V.Trabajador.NumeroEmpleado, 0), "numtrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "regtrabajador", V.Trabajador.RegistroPatronal, 0), "regtrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direcciontrabajador", V.Trabajador.Direccion, 0), "direcciontrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriortrabajador", V.Trabajador.NoExterior, 0), "noexteriortrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriortrabajador", V.Trabajador.NoInterior, 0), "nointeriortrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniatrabajador", V.Trabajador.Colonia, 0), "coloniatrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cptrabajador", V.Trabajador.CP, 0), "cptrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadtrabajador", V.Trabajador.Ciudad, 0), "ciudadtrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estadotrabajador", V.Trabajador.Estado, 0), "estadotrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiotrabajador", V.Trabajador.Municipio, 0), "municipiotrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "reftrabajador", V.Trabajador.ReferenciaDomicilio, 0), "reftrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfctrabajador", V.Trabajador.RFC, 0), "rfctrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curptrabajador", V.Trabajador.Curp, 0), "curptrabajador")
        If V.Trabajador.sindicalizado = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sindicalizado", "No", 0), "sindicalizado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sindicalizado", "Sí", 0), "sindicalizado")
        End If
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfctercero", V.Trabajador.RFCPatronOrigen, 0), "rfctercero")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estadolabora", V.Trabajador.EstadoLabora, 0), "estadolabora")
        Dim Subcontratacion As String = ""
        Dim Subc As New dbnominasubcontratacion(MySqlcon)
        DR = Subc.ConsultaReader(V.IdTrabajador)
        While DR.Read
            If Subcontratacion <> "" Then Subcontratacion += vbCrLf
            Subcontratacion += "RFC:" + DR("rfclaboral") + " % Tiempo=" + DR("porcentaje").ToString
        End While
        DR.Close()
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subcontratacion", Subcontratacion, 0), "subcontratacion")
        Dim FP As New dbFormasdePago(V.Idforma, MySqlcon)
        Dim strMetodos As String = ""
        Dim MeP As New dbVentasAddMetodos(MySqlcon)
        DR = MeP.ConsultaReader(2, V.ID)
        While DR.Read()
            If strMetodos <> "" Then strMetodos += vbNewLine
            If DR("clavesat") < 1000 Then
                strMetodos += Format(DR("clavesat"), "00") + " " + DR("nombre")
            Else
                strMetodos += DR("nombre")
            End If
        End While
        DR.Close()
        If V.Fecha < "2017/01/01" Then
            If strMetodos <> "" Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "metododepago", strMetodos, 0), "metododepago")
            Else
                If FP.clavesat < 1000 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "metododepago", Format(FP.clavesat, "00") + " " + FP.Nombre, 0), "metododepago")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "metododepago", FP.Nombre, 0), "metododepago")
                End If
            End If
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "metododepago", "NA", 0), "metododepago")
        End If
        'nuevos
        Dim Regimen As String = ""
        Select Case V.Trabajador.TipoRegimen
            Case 1
                Regimen = "Asimilado a salariados"
            Case 2
                Regimen = "Sueldos y salarios"
            Case 3
                Regimen = "Jubilados"
            Case 4
                Regimen = "Pensionados"
        End Select

        ImpDoc.ImpND.Add(New NodoImpresionN("", "tiporegimentrabajador", Regimen, 0), "tiporegimentrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nsstrabajador", V.Trabajador.NumeroSeguroSocial, 0), "nsstrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "finiciotrabajador", V.Trabajador.FechaInicioLaboral, 0), "finiciotrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "anttrabajador", V.Antiguedad.ToString, 0), "anttrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "deptrabajador", V.Trabajador.Departamento, 0), "deptrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "puestotrabajador", V.Trabajador.Puesto, 0), "puestotrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tipojtrabajador", V.Trabajador.TipoJornada, 0), "tipojtrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tipoctrabajador", V.Trabajador.TipoContrato, 0), "tipoctrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "periodotrabajador", V.Trabajador.Periodicidad, 0), "periodotrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sbasetrabajador", Format(V.Trabajador.SalarioBaseCotApor, "#,###,##0.00"), 0), "sbasetrabajador")
        Dim Riesgo As String = ""
        Select Case V.Trabajador.RiesgoPuesto
            Case 1
                Riesgo = "Clase I"
            Case 2
                Riesgo = "Clase II"
            Case 3
                Riesgo = "Clase III"
            Case 4
                Riesgo = "Clase IV"
            Case 5
                Riesgo = "Clase V"

        End Select
        ImpDoc.ImpND.Add(New NodoImpresionN("", "riesgotrabajador", Riesgo, 0), "riesgotrabajador")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sdiariotrabajador", Format(V.Trabajador.SalarioDiarioIntegrado, "#,###,##0.00"), 0), "sdiariotrabajador")

        'ComboBox1.Items.Add("Clase I")
        'ComboBox1.Items.Add("Clase II")
        'ComboBox1.Items.Add("Clase III")
        'ComboBox1.Items.Add("Clase IV")
        'ComboBox1.Items.Add("Clase V")


        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        'ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fechapago", Replace(V.FechaPago, "/", "-"), 0), "fechapago")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "fechainicial", Replace(V.FechaInicialPago, "/", "-"), 0), "fechainicial")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "fechafinal", Replace(V.FechaFinalPAgo, "/", "-"), 0), "fechafinal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clabe", V.Clabe, 0), "clabe")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "banco", V.DaNombreBanco(V.Banco), 0), "banco")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "diaspagados", V.DiasPagados.ToString, 0), "diaspagados")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")

        If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        End If
        CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")



        Dim VI As New dbNominasDetalles(MySqlcon)
        DR = VI.ConsultaReader(idNota, 3)
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipoconcepto", DR("tipod"), 0), "tipoconcepto" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("concepto"), 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impgravado", Format(DR("importegravado"), Op._formatoPrecioU).PadLeft(Op.EspacioImporte), 0), "impgravado" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impexento", Format(DR("importeexento"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "impexento" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imptotal", Format(DR("importeexento") + DR("importegravado"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imptotal" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "dias", "", 0), "dias" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "horasextra", "", 0), "horasextra" + Format(Cont, "000"))

            'Percepción','Deducción'
            If DR("tipod") = "Percepción" Then
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imppercepcion", Format(DR("importeexento") + DR("importegravado"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imppercepcion" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impdeduccion", "", 0), "impdeduccion" + Format(Cont, "000"))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imppercepcion", "", 0), "imppercepcion" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impdeduccion", Format(DR("importeexento") + DR("importegravado"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "impdeduccion" + Format(Cont, "000"))
            End If


            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        Dim VII As New dbNominasIncapacidades(MySqlcon)
        DR = VII.ConsultaReader(idNota)
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", "", 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipoconcepto", "Incapacidad", 0), "tipoconcepto" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("tipod"), 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impgravado", "", 0), "impgravado" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impexento", "", 0), "impexento" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imptotal", Format(DR("descuento"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imptotal" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "dias", Format(DR("dias"), "#00"), 0), "dias" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "horasextra", "", 0), "horasextra" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imppercepcion", "", 0), "imppercepcion" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impdeduccion", Format(DR("descuento"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "impdeduccion" + Format(Cont, "000"))
            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        Dim VIH As New dbNoominaHorasExtra(MySqlcon)
        DR = VIH.ConsultaReader(idNota)
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", "", 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipoconcepto", "Horas Extra", 0), "tipoconcepto" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("tipohoras"), 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impgravado", "", 0), "impgravado" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impexento", "", 0), "impexento" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imptotal", Format(DR("importepagado"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imptotal" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "dias", Format(DR("dias"), "#00"), 0), "dias" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "horasextra", Format(DR("horasextra"), Op._formatocantidad).PadLeft(6), 0), "horasextra" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imppercepcion", Format(DR("importepagado"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imppercepcion" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impdeduccion", "", 0), "impdeduccion" + Format(Cont, "000"))
            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()


        Dim VOP As New dbNominaOtrosPagos(MySqlcon)
        DR = VOP.consultaPagos(idNota)
        Dim Concepto As String = ""
        Dim TipoOP As String
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipoconcepto", "Otro Pago", 0), "tipoconcepto" + Format(Cont, "000"))
            Concepto = DR("concepto")
            If DR("tipopago") < 4 Then
                TipoOP = Format(DR("tipopago") + 1, "000")
            Else
                TipoOP = "99"
            End If
            If TipoOP = "002" Then
                Concepto += vbCrLf + "Subsicio Causado: " + Format(DR("subsidio"), "0.00")
            End If
            If TipoOP = "004" Then
                Concepto += vbCrLf + "Saldo a Favor: " + Format(DR("saldoafavor"), "0.00")
                Concepto += vbCrLf + "Años de servicio: " + Format(DR("anhos"), "0.00")
                Concepto += vbCrLf + "Remanente saldo a favor: " + Format(DR("remanente"), "0.00")
            End If
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", Concepto, 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impgravado", "", 0), "impgravado" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impexento", "", 0), "impexento" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imptotal", Format(DR("importe"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imptotal" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "dias", "", 0), "dias" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "horasextra", "", 0), "horasextra" + Format(Cont, "000"))

            'Percepción','Deducción'

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "imppercepcion", Format(DR("importe"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "imppercepcion" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "impdeduccion", "", 0), "impdeduccion" + Format(Cont, "000"))

            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        Dim NEx As New dbNominaTRabajador(idNota, MySqlcon)
        If V.HayJubilacionPE Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "jubtotalunae", Format(NEx.JtotalUnaExhibicion, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "jubtotalunae")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "jubtotalparcialidad", "", 0), "jubtotalparcialidad")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "jubmontodiario", "", 0), "jubmontodiario")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "jubingacumulable", Format(NEx.Jacumulable, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "jubingacumulable")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "jubingnoacumulable", Format(NEx.JnoAcumulable, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "jubingnoacumulable")
        Else
            If V.HayJubilacionPP Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubtotalunae", "", 0), "jubtotalunae")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubtotalparcialidad", Format(NEx.JtotalParcialidad, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "jubtotalparcialidad")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubmontodiario", Format(NEx.JmontoDiario, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "jubmontodiario")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubingacumulable", Format(NEx.Jacumulable, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "jubingacumulable")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubingnoacumulable", Format(NEx.JnoAcumulable, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "jubingnoacumulable")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubtotalunae", "", 0), "jubtotalunae")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubtotalparcialidad", "", 0), "jubtotalparcialidad")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubmontodiario", "", 0), "jubmontodiario")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubingacumulable", "", 0), "jubingacumulable")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "jubingnoacumulable", "", 0), "jubingnoacumulable")
            End If
        End If
        If V.HaySeparacionP Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "septotalpagado", Format(NEx.StotalPagado, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "septotalpagado")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepaniosser", Format(NEx.SanhosServicio, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "sepaniosser")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepultimosueldo", Format(NEx.SsueldoMensual, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "sepultimosueldo")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepingacumulable", Format(NEx.Sacumulable, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "sepingacumulable")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepingnoacumulable", Format(NEx.SnoAcumulable, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "sepingnoacumulable")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "septotalpagado", "", 0), "septotalpagado")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepaniosser", "", 0), "sepaniosser")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepultimosueldo", "", 0), "sepultimosueldo")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepingacumulable", "", 0), "sepingacumulable")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sepingnoacumulable", "", 0), "sepingnoacumulable")
        End If
        If V.origenRecurso <> "NA" Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sncforigen", V.origenRecurso, 0), "sncforigen")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sncfmonto", Format(V.montoRecurso, Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "sncfmonto")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sncforigen", "", 0), "sncforigen")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "sncfmonto", "", 0), "sncfmonto")
        End If
        'ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalaPagar, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "total")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalNota, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalcon")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total Percepciones:", Format(V.TotalExentoP + V.TotalGravadoP, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalper")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total Deducciones:", Format(V.TotalExentoD + V.TotalGravadoD, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "totalded")

        Dim f As New StringFunctions
        Dim CL As New CLetras
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda), 0), "totalletra")
        If V.TotalaPagar >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalaPagar, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalaPagar * -1, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        End If
        If V.TotalNota >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletracon", CL.LetrasM(V.TotalNota, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletracon")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletracon", "MENOS " + CL.LetrasM(V.TotalNota * -1, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletracon")
        End If
        'ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        'ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        ImpDoc.CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + V.Trabajador.RFC + "&tt=" + Format(V.TotalNota, "0000000000.000000") + "&id=" + V.uuid, System.Text.Encoding.Default)
        ImpDoc.NumeroPagina = 1
    End Sub

    

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmTrabajadores(1, idCliente)
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.NumeroEmpleado
        End If
    End Sub

    Private Sub cmbConcepto_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub cmbConcepto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboTipo.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox8.Focus()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox9.Focus()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbNominas(MySqlcon)
                V.ActualizaComentario(idNota, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub cmbVariante_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbVariante.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox8.Focus()
        End If
    End Sub

    
    Private Sub cmbVariante_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVariante.SelectedIndexChanged
        If cmbVariante.SelectedIndex = 0 Then
            LlenaCombos("tblpercepciones", ComboBox8, "concat(clave,' ',descripcion)", "con", "idpercepcion", IdsTipos, "visible=1", , "clave")

        Else
            LlenaCombos("tbldeducciones", ComboBox8, "concat(clave,' ',descripcion)", "con", "iddeduccion", IdsTipos, , , "clave")
        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        NuevoIncapacidad()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
                If MsgBox("¿Desea eliminar esta incapacidad de la nomina?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbNominasIncapacidades(MySqlcon)
                    CD.Eliminar(IdDetalle)
                    ConsultaDetallesIncapacidad()
                    NuevoIncapacidad()
                End If

            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        BotonAgregarIncapacidad()
    End Sub
    Private Sub LlenaDatosDetallesIncapacidad()
        Try

            IdDetalleI = DGIncapacidades.Item(0, DGIncapacidades.CurrentCell.RowIndex).Value
            Dim CD As New dbNominasIncapacidades(IdDetalleI, MySqlcon)
            'ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            ComboBox4.SelectedIndex = CD.TipoIncapacidad
            TextBox19.Text = CD.Dias.ToString
            TextBox16.Text = CD.Descuento.ToString
            Button16.Text = "Modificar Incapacidad"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button20.Enabled = True
            'cmbtipoarticulo.Text = "A"



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosDetallesHorasExtra()
        Try

            IdDetalleH = DGHorasextra.Item(0, DGHorasextra.CurrentCell.RowIndex).Value
            Dim CD As New dbNoominaHorasExtra(IdDetalleH, 0, MySqlcon)
            'ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            ComboBox5.Text = CD.TipoHoras
            TextBox15.Text = CD.Dias.ToString
            TextBox13.Text = CD.HorasExtra.ToString
            TextBox17.Text = CD.ImportePagado.ToString
            Button23.Text = "Modificar Hora Extra"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button22.Enabled = True
            'cmbtipoarticulo.Text = "A"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub DGIncapacidades_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGIncapacidades.CellClick
        LlenaDatosDetallesIncapacidad()
    End Sub

    Private Sub DGIncapacidades_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGIncapacidades.CellContentClick

    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        NuevoHorasExtra()
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
                If MsgBox("¿Desea eliminar esta hora extra de la nomina?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbNoominaHorasExtra(MySqlcon)
                    CD.Eliminar(IdDetalleH)
                    ConsultaDetallesHorasExtra()
                    NuevoHorasExtra()
                End If

            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGHorasextra_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGHorasextra.CellClick
        LlenaDatosDetallesHorasExtra()
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        BotonAgregarHorasExtra()
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Dim Fmd As New frmNominaMasDatos(Estado)
        Fmd.Monto = MontoRecurso
        Fmd.OrigenRecurso = OrigenRecurso
        Fmd.Tipo = 0
        If Fmd.ShowDialog = Windows.Forms.DialogResult.OK Then
            MontoRecurso = Fmd.Monto
            OrigenRecurso = Fmd.OrigenRecurso
        End If
        Fmd.Dispose()
    End Sub

    Private Sub ComboBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox5.Focus()
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged
        TextBox4.Text = ComboBox8.Text
        If CheckBox7.Checked Then
            Dim IdCuenta As Integer
            IdCuenta = Nom.DaIdCuentaConcepto(IdsTipos.Valor(ComboBox8.SelectedIndex), cmbVariante.SelectedIndex)
            If IdCuenta = 0 Then
                TextBox5.Text = TextBox4.Text.Substring(0, 3)
            Else
                TextBox5.Text = Cp.DaCuentaTxt(IdCuenta).Trim.Replace(" ", "")
            End If
        End If
    End Sub

    Private Sub DateTimePicker4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker4.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtdiaspagados.Focus()
        End If
    End Sub

    Private Sub DateTimePicker4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker4.ValueChanged
        'Label38.Text = CStr(DateDiff(DateInterval.Day, DateTimePicker3.Value, DateTimePicker4.Value) + 1) + " días."
        txtantiguedad.Text = CStr(Math.Truncate((DateDiff(DateInterval.Day, CDate(FechaInicioLaboral), DateTimePicker4.Value) + 1) / 7)) '+ " " + DateDiff(DateInterval.Day, CDate(FechaInicioLaboral), DateTimePicker4.Value).ToString
    End Sub

    Private Sub DateTimePicker3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker3.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker4.Focus()
        End If
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        'Label38.Text = CStr(DateDiff(DateInterval.Day, DateTimePicker3.Value, DateTimePicker4.Value) + 1) + " días."
    End Sub

    Private Sub DateTimePicker2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker2.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker3.Focus()
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged

    End Sub

    Private Sub TextBox18_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox18.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbVariante.Focus()
        End If
    End Sub

    Private Sub TextBox18_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox18.TextChanged

    End Sub

    Private Sub ComboBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox19.Focus()
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged

    End Sub

    Private Sub TextBox19_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox19.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox16.Focus()
        End If
    End Sub

    Private Sub TextBox19_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox19.TextChanged

    End Sub

    Private Sub TextBox16_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox16.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregarIncapacidad()
        End If
    End Sub

    Private Sub TextBox16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox16.TextChanged

    End Sub

    Private Sub ComboBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox15.Focus()
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged

    End Sub

    Private Sub TextBox15_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox15.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox13.Focus()
        End If
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox15.TextChanged

    End Sub

    Private Sub TextBox13_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox13.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox17.Focus()
        End If
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged

    End Sub

    Private Sub TextBox17_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox17.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregarHorasExtra()
        End If
    End Sub

    Private Sub TextBox17_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox17.TextChanged

    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        Dim V As New dbNominas(MySqlcon)
        V.DaDatosTimbrado(idNota)
        Dim FDT As New frmVentasDatosTimbrado(V.uuid, V.FechaTimbrado, V.NoCertificadoSAT, V.SelloCFD, V.SelloSAT)
        FDT.ShowDialog()
        FDT.Dispose()
    End Sub

    Private Sub txtdiaspagados_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdiaspagados.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtantiguedad.Focus()
        End If
    End Sub

  
    Private Sub txtdiaspagados_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdiaspagados.TextChanged

    End Sub

    Private Sub txtantiguedad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtantiguedad.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox18.Focus()
        End If
    End Sub

    Private Sub txtantiguedad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtantiguedad.TextChanged

    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        Dim f As New frmNominasConsulta(ModosDeBusqueda.Secundario, "")
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            Dim Ni As New dbNominas(f.IdVenta, MySqlcon)
            Dim Nn As New dbNominas(MySqlcon)

            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Nominas, GlobalTipoFacturacion)
            TextBox11.Text = Sf.Serie
            If Sf.EsElectronica > 0 Then
                CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
            End If
            'Dim V As New dbNominas(MySqlcon)
            TextBox2.Text = Nn.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
            Dim Cer As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
            Nn.Guardar(Ni.IdTrabajador, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), 0, 0, Ni.IdSucursal, TextBox11.Text, 0, Sf.NoAprobacion, Sf.YearAprobacion, Cer.Certificado, EsElectronica, 2, 0, 0, Ni.FechaPago, Ni.FechaInicialPago, Ni.FechaFinalPAgo, Ni.Banco, Ni.Clabe, Ni.DiasPagados, Ni.Trabajador.DaAntiguedad(Ni.Trabajador.FechaInicioLaboral), 0, Ni.Idforma, If(comboTipo.SelectedIndex = 0, "O", "E"), "", 0)
            idNota = Nn.ID
            Nn.AgregarDetallesReferencia(idNota, Ni.ID)
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub

    Private Sub Button28_BackgroundImageLayoutChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button28.BackgroundImageLayoutChanged

    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        Dim en As New Encriptador
        Dim V As New dbNominas(idNota, MySqlcon)
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaXMLTimbradob As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        'If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
        'Cadena = V.CreaCadenaOriginali32(idNota, GlobalIdMoneda)
        'Else
        'Cadena = V.CreaCadenaOriginali(idNota, GlobalIdMoneda)
        'End If
        'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNOMINACFDIb-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNOMINACFDI-" + V.Serie + V.Folio.ToString + "_TIMBRADO.xml"
        RutaXMLTimbradob = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNOMINACFDI-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNOMINACFDI-" + V.Serie + V.Folio.ToString + ".xml"
        'RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNOMINACFDIb-" + V.Serie + V.Folio.ToString + ".xml"
        'RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNOMINACFDI-" + V.Serie + V.Folio.ToString + "_TIMBRADO.xml"
        'RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNOMINACFDI-" + V.Serie + V.Folio.ToString + ".xml"
        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        'Dim strXML As String
        'If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
        'strXML = V.CreaXMLi32(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        'Else
        'strXML = V.CreaXMLi(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        'End If

        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        V.NoCertificadoSAT = ""
        V.DaDatosTimbrado(idNota)
        If GlobalPacCFDI = 2 Then
            'en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
            Dim Timbre As String
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, True)
            Timbre = V.Recuperar(S.RFC, op._ApiKey, V.Serie, V.Folio, True)
            If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                Dim xmldoc As New Xml.XmlDocument
                en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                xmldoc.Load(RutaXmlTimbrado)
                If V.uuid = "**No Timbrado**" Then
                    V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                    V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                    V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                    V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                    V.GuardaDatosTimbrado(idNota, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                End If
                en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
            Else
                MsgError = Timbre
                V.NoCertificadoSAT = "Error"
            End If
        End If
        If V.NoCertificadoSAT <> "Error" Then
            V.ModificaEstado(idNota, Estados.Guardada)
            Imprimir(idNota)
        Else
            MsgBox("Ha ocurrido un error en el timbrado del la óomina, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardar nómina como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idNota, Estados.Pendiente)
                Nuevo()
            Else
                'Dim Se As New dbInventarioSeries(MySqlcon)
                'Se.QuitaSeriesAVenta(idVenta)
                'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idNota)
                PopUp("Nota Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
        End If
    End Sub
    Private Sub GeneraPoliza()
        Try
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbNominas(idNota, MySqlcon)
                Dim Canceladas As Byte = 0
                Dim credito As Byte = 2
                Dim cuantas As Integer
                If V.Estado = Estados.Cancelada Then
                    Canceladas = 1
                End If
                'Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
                'If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                '    credito = 0
                'Else
                '    credito = 1
                'End If
                cuantas = M.CuantasHay(10, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(10, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 10)
                        f.ShowDialog()
                        If f.DialogResult = Windows.Forms.DialogResult.OK Then
                            M.ID = f.IdMascara
                        Else
                            Exit Sub
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    If Canceladas = 0 Then
                        GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    Else
                        GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    End If
                    GP.GeneraPolizaGeneral(V.ID, V.IdTrabajador, 2, 0, 0, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Try
            Dim En As New Encriptador()
            Dim op As New dbOpciones(MySqlcon)
            Dim XmlAcuse As String
            Dim V As New dbNominas(idNota, MySqlcon)
            V.DaDatosTimbrado(idNota)
            V.DaTotal(idNota, V.IdMoneda)
            Dim RutaXml As String
            Dim ImpOp As Boolean = False
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NominasXML, False)
            Archivos.CierraDB()
            'Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(FacturaGlobal.Fecha), "yyyy"))
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSACUSECANNOM-" + V.Serie + V.Folio.ToString + ".xml"
            If IO.File.Exists(RutaXml) = False Then
                Dim Suc As New dbSucursales(V.IdSucursal, MySqlcon)
                XmlAcuse = AcuseCancelacion(V.uuid, op._ApiKey, Suc.RFC, "Nómina", idNota)
                If XmlAcuse.ToUpper.Contains("ERROR") = False Then
                    If XmlAcuse.ToUpper.Contains("NOENCONTRADA") = True Then
                        MsgBox("El SAT aun no ha notificado al pac de este acuse de cancelación, vuelva a intentarlo mas tarde.", MsgBoxStyle.Information, GlobalNombreApp)
                    Else
                        En.GuardaArchivoTexto(RutaXml, XmlAcuse, System.Text.Encoding.UTF8)
                        ImpOp = True
                        PopUp("Acuse Obtenido", 70)
                    End If
                Else
                    MsgBox("A ocurrido un error al tratar de obtener el acuse de cancelación. " + XmlAcuse, MsgBoxStyle.Information, GlobalNombreApp)
                    AddErrorTimbrado(XmlAcuse, "Nómina - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
                End If
            Else
                ImpOp = True
            End If
            Try
                'Dim S As String
                'Generar reporte de acuse
                If ImpOp = True Then
                    Dim xmldoc As New Xml.XmlDocument
                    xmldoc.Load(RutaXml)
                    ''S = xmldoc.Item("Acuse").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    'S = xmldoc.Item("Acuse").Attributes("Fecha").Value
                    'S = xmldoc.Item("Acuse").Attributes("RfcEmisor").Value
                    'S = xmldoc.Item("Acuse").Item("Folios").Item("UUID").InnerText
                    'S = xmldoc.Item("Acuse").Item("Signature").Item("SignatureValue").InnerText
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                    Rep = New repAcuseCancelacion
                    Rep.SetParameterValue("empresa", suc.Nombre)
                    Rep.SetParameterValue("rfcemisor", suc.RFC)
                    Rep.SetParameterValue("rfccliente", V.Trabajador.RFC)
                    Rep.SetParameterValue("Documento", "NÓMINA")
                    Rep.SetParameterValue("folio", V.Serie + V.Folio.ToString("00000"))
                    Rep.SetParameterValue("fechac", xmldoc.Item("Acuse").Attributes("Fecha").Value)
                    Rep.SetParameterValue("uuid", xmldoc.Item("Acuse").Item("Folios").Item("UUID").InnerText)
                    Rep.SetParameterValue("monto", V.TotalaPagar.ToString("$#,###,###,##0.00"))
                    Rep.SetParameterValue("sello", xmldoc.Item("Acuse").Item("Signature").Item("SignatureValue").InnerText)
                    Rep.SetParameterValue("nombrecliente", V.Trabajador.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                End If
            Catch ex As Exception
                AddError(ex.Message, "Nómina - Acuse Impresión", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        Catch ex As Exception
            AddErrorTimbrado(ex.Message, "Nómina - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        If TotalVenta > 0 Then
            Dim fmp As New frmVentasSelectorMetodosPago(2, idNota, TotalVenta, 1, False)
                fmp.ShowDialog()
                fmp.Dispose()
            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            DR = MetodosDePago.ConsultaReader(2, idNota)
            If DR.Read() Then
                ComboBox6.SelectedIndex = idsFormasDePago.Busca(DR("idforma"))
            End If
            DR.Close()
            Button30.Visible = True
            ComboBox6.Enabled = False
        Else
            MsgBox("El recibo debe ser mayor a cero.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If MsgBox("¿Remover los métodos de pago agregados?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            MetodosDePago.RemoverTodo(2, idNota)
            ComboBox6.Enabled = True
            Button30.Visible = False
            PopUp("Métodos removidos.", 60)
        End If
    End Sub

    Private Sub comboTipo_KeyDown(sender As Object, e As KeyEventArgs) Handles comboTipo.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

   
    Private Sub DGHorasextra_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGHorasextra.CellContentClick

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim Fmd As New frmNominaMasDatos(Estado)
        Fmd.PrecioOtorgarse = PrecioalOtorgarse
        Fmd.ValorMercado = ValorMercado
        Fmd.Tipo = 1
        If Fmd.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrecioalOtorgarse = Fmd.PrecioOtorgarse
            ValorMercado = Fmd.ValorMercado
        End If
        Fmd.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim Fmd As New frmNominaMasDatos(Estado)
        Fmd.IdDetalle = IdDetalle
        Fmd.Tipo = 2
        Fmd.ShowDialog()
        Fmd.Dispose()
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Dim Fmd As New frmNominaMasDatos(Estado)
        Fmd.IdNomina = idNota
        Fmd.Tipo = 3
        Fmd.ShowDialog()
        Fmd.Dispose()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim Fmd As New frmNominaMasDatos(Estado)
        If IdsTipos.Valor(ComboBox8.SelectedIndex) = 39 Then
            Fmd.Tipo = 4
        Else
            Fmd.Tipo = 5
        End If
        Fmd.IdNomina = idNota
        Fmd.ShowDialog()
        Fmd.Dispose()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If DateTimePicker1.Value.ToString("yyyy/MM/dd") >= "2017/01/01" And Op.ActNom12 = 1 Then
            Panel4.Visible = False
            Button25.Visible = True
            Button33.Visible = True
            Label38.Visible = False
            ComboBox6.Visible = False
            Button29.Visible = False
            comboTipo.Visible = True
            Label41.Visible = True
        Else
            comboTipo.Visible = False
            Label41.Visible = False
            Panel4.Visible = True
            Button6.Visible = False
            Button12.Visible = False
            Button31.Visible = False
            Button32.Visible = False
            Button25.Visible = False
            Button33.Visible = False
            Label38.Visible = True
            ComboBox6.Visible = True
            Button29.Visible = True
        End If
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If Estado = 0 Then
            Guardar()
        End If
        If Estado <> 0 Then
            Dim opa As New frmNominaOtrosPagos(idNota, Estado)
            opa.ShowDialog()
            opa.Dispose()
            SacaTotal()
        End If
    End Sub
End Class