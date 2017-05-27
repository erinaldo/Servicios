Public Class frmNotasdeCargo
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
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDi2 As New Collection
    Dim TipoImpresora As Byte
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim SObre As Byte
    Dim SIVA As Double
    Dim EsElectronica As Byte
    Dim CertificadoCaduco As Boolean = False
    Dim SinTimbres As Boolean = False
    Dim CuantaY As Integer = 0
    Public Sub New(Optional ByVal pidNota As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idNota = pidNota
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nota de cargo no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim C As New dbNotasdeCargo(idNota, MySqlcon)
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
                Modificar(Estados.Guardada)
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled = True Then
                Modificar(Estados.Pendiente)
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
    End Sub
    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        If GlobalTipoFacturacion > 1 Then
            SinTimbres = ChecaTimbres()
        End If
        Tabla.Columns.Add("Id", I.GetType)
        'Tabla.Columns.Add("TipoR", S.GetType)
        'Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        'Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Importe", D.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        'LlenaCombos("tblformasdepago", ComboBox4, "nombre", "nombret", "idforma", idsFormasDePago, , , "idforma")
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        LlenaCombos("tblconceptosnotasventas", cmbConcepto, "nombre", "nombret", "idconceptonotaventa", IdsTipos, " tipo=0 and idconceptonotaventa>0")

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
                Dim V As New dbNotasdeCargo(MySqlcon)
                T = V.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)

                Iva = V.TotalIva
                Label12.Text = Format(V.Subtotal, "#,##0.00")
                Label13.Text = Format(V.TotalIva, "#,##0.00")
                Label14.Text = Format(V.TotalNota, "#,##0.00")

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
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button35.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        CheckBox1.Checked = False
        TextBox14.Text = ""
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'TextBox11.Text = S.Serie
        IVaDefault = S.Impuesto
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotasDeCargo, GlobalTipoFacturacion)
        TextBox11.Text = Sf.Serie
        EsElectronica = Sf.EsElectronica
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        Dim V As New dbNotasdeCargo(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        If GlobaltpBanxico <> "Error" Then
            TextBox10.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox10.Text = CM.Cantidad.ToString
        End If
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        CheckBox1.Checked = False
        Panel1.Enabled = True
        Panel2.Enabled = True
        SerieAnt = ""
        Button2.Enabled = True
        Label24.Visible = False
        'ComboBox4.SelectedIndex = 0
        NuevoConcepto()
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
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        Else
            ComboBox3.Enabled = True
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox5.Focus()
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
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(TextBox1.Text) Then
                    If c.DireccionFiscal = 0 Then
                        TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    Else
                        TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    End If
                    If c.NoChecarCr = 0 Then
                        TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00") + vbCrLf + "A Favor: " + Format(c.DaSaldoAFavor(c.ID), "#,##0.00")
                    Else
                        TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00")
                    End If
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Then
                        'If c.Credito > 0 Then
                        '    ComboBox4.SelectedIndex = 1
                        'Else
                        '    ComboBox4.SelectedIndex = 0
                        'End If
                    End If
                    idCliente = c.ID
                    Isr = c.ISR
                    IvaRetenido = c.IvaRetenido
                    SIVA = c.IVA
                    SObre = c.SobreescribeIVA
                    If SObre <> 0 Then TextBox8.Text = SIVA.ToString
                Else
                    TextBox7.Text = ""
                    TextBox13.Text = ""
                    idCliente = 0
                    Isr = 0
                    IvaRetenido = 0
                    SObre = 0
                    SIVA = 0
                    TextBox8.Text = "16"
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbNotasdeCargo(MySqlcon)
            Dim Desglozar As Byte
            If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            'If FolioAnt <> TextBox2.Text Then
            If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                If pEstado = Estados.Guardada Then TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            End If
            'End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoAlta, PermisosN.Secciones.Ventas) = False And pEstado = Estados.Guardada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoCancelar, PermisosN.Secciones.Ventas) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If MensajeError = "" Then
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                Dim O As New dbOpciones(MySqlcon)
                'Dim FP As New dbFormasdePago(idsFormasDePago.Valor(ComboBox4.SelectedIndex), MySqlcon)
                Dim CM As New dbMonedasConversiones(MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                'Dim Credito As Byte
                'Credito = FP.Tipo
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotasDeCargo, GlobalTipoFacturacion)
                Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                C.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Modificar(idNota, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, pEstado, C.Subtotal, C.TotalNota, idCliente, TextBox11.Text, CDbl(TextBox10.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), EsElectronica, TextBox14.Text)
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
                    Select Case EsElectronica
                        Case 0
                            Imprimir()
                        Case 1
                            CadenaOriginal()
                        Case 2
                            CadenaOriginali(pEstado)
                    End Select
                    'End If
                End If
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
            'If Button1.Text = "Guardar" Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoAlta, PermisosN.Secciones.Ventas) = True Then
                If idCliente <> 0 Then
                    Dim C As New dbNotasdeCargo(MySqlcon)
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
                    ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(ComboBox1.SelectedIndex))
                    C.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                    Dim Sf As New dbSucursalesFolios(MySqlcon)
                    Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotasDeCargo, GlobalTipoFacturacion)
                    Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                    C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text, CDbl(TextBox10.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, 1, IDsMonedas2.Valor(ComboBox2.SelectedIndex), Isr, IvaRetenido, IdsTipos.Valor(cmbConcepto.SelectedIndex))
                    idNota = C.ID
                    Estado = 1
                    'Button1.Text = "Modificar"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    Button14.Enabled = True
                    'LlenaDatosDetalles()
                Else
                    MsgBox("Debe indicar un cliente", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    'Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
    '    BuscaArticulo()
    'End Sub
    'Private Sub BuscaArticulo()
    '    Try
    '        If ConsultaOn Then
    '            Dim p As New dbInventario(MySqlcon)
    '            If p.BuscaArticulo(TextBox3.Text, 0) Then
    '                LlenaDatosArticulo(p)
    '            Else
    '                IdInventario = 0
    '                    TextBox4.Text = ""
    '                    TextBox6.Text = "0"
    '                    TextBox8.Text = "0"
    '                    TextBox9.Text = "0"
    '                    PrecioU = 0
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub
    Private Sub LlenaDatosVenta()
        Dim C As New dbNotasdeCargo(idNota, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio
        TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        EsElectronica = C.EsElectronica
        TextBox8.Text = C.Iva.ToString
        TextBox11.Text = C.Serie
        TextBox10.Text = C.TipodeCambio.ToString
        TextBox14.Text = C.Comentario
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        'ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        'ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.Id)
        ComboBox3.Enabled = False
        cmbConcepto.SelectedIndex = IdsTipos.Busca(C.IdConcepto)
        LlenaDatosDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
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
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Button35.Enabled = False
        End Select
    End Sub
    Private Sub LlenaDatosDetalles()
        Panel1.Visible = True
        ConsultaDetalles()
    End Sub
    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbNotasdeCargoDetalles(MySqlcon)
            T = CD.ConsultaReader(idNota)
            While T.Read
                'If T("idinventario") > 1 Then
                Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("precio"), T("abreviatura"))
                'Else
                'Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("precio"), T("abreviatura"))
                'End If
            End While
            T.Close()
            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            'DGDetalles.Columns(1).Visible = False
            'DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        IdVariante = 0
        IdServicio = 0
        cmbVariante.Visible = False
        TextBox12.Text = "0"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "1"
        TextBox9.Text = "0"
        TextBox6.Text = "0"
        If SObre = 0 Then
            TextBox8.Text = "16"
        Else
            TextBox8.Text = SIVA.ToString
        End If
        PrecioBase = 0
        cmbVariante.Visible = False
        Button12.Visible = False
        Button9.Enabled = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        TextBox4.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbNotasdeCargoDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            'If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox6.Text) = False Then
                MsgError += vbCrLf + "El precio debe ser un valor numérico."
                HayError = True
            Else
                If CInt(TextBox6.Text) <= 0 Then
                    MsgError += " El precio debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If HayError = False Then
                If Button4.Text = "Agregar Concepto" Then
                    CD.Guardar(idNota, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text))
                    'If IdInventario <> 0 Then
                    '    If ManejaSeries <> 0 Then
                    '        If CD.NuevoConcepto Then
                    '            Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CInt(TextBox5.Text))
                    '            F.ShowDialog()
                    '        Else
                    '            Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CD.Cantidad)
                    '            F.ShowDialog()
                    '        End If
                    '    End If
                    '    Dim I As New dbInventario(MySqlcon)
                    '    I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Baja, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If
                    'If IdVariante <> 0 Then
                    '    Dim PV As New dbProductosVariantes(MySqlcon)
                    '    PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If

                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text))
                    'If IdInventario <> 0 Then
                    '    If ManejaSeries <> 0 Then
                    '        Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CDbl(TextBox5.Text))
                    '        F.ShowDialog()
                    '    End If
                    '    Dim I As New dbInventario(MySqlcon)
                    '    I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    'End If
                    'If IdVariante <> 0 Then
                    '    Dim PV As New dbProductosVariantes(MySqlcon)
                    '    PV.ModificaInventario(IdVariante, CantAnt * -1, IdAlmacen)
                    '    PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdAlmacen)
                    'End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo modificado", 90)
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoAlta, PermisosN.Secciones.Ventas) = True Then
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
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try

            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbNotasdeCargoDetalles(IdDetalle, MySqlcon)
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            TextBox5.Text = CD.Cantidad.ToString
            TextBox8.Text = CD.Iva.ToString
            TextBox9.Text = CD.Descuento.ToString
            IdInventario = CD.Idinventario
            'IdVariante = CD.idVariante
            'If IdInventario > 1 Then
            '    TextBox3.Text = CD.Inventario.Clave
            '    IdVariante = 0
            '    cmbVariante.Visible = False
            'End If
            'If IdVariante > 1 Then
            '    Dim P As New dbProductos(CD.Producto.IdProducto, MySqlcon)
            '    TextBox3.Text = P.Clave
            '    IdInventario = 0
            '    cmbVariante.Visible = True
            'End If

            PrecioU = CD.Precio / CD.Cantidad
            PrecioBase = PrecioU
            TextBox12.Text = PrecioU.ToString
            cmbVariante.Visible = False
            CantAnt = CD.Cantidad
            'IdAlmacen = CD.IdAlmacen
            TextBox6.Text = CD.Precio.ToString
            TextBox4.Text = CD.Descripcion
            Button4.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            'cmbtipoarticulo.Text = "A"



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nota de crédito no ha sido guardada. ¿Desea iniciar una nueva factura? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbNotasdeCargo(MySqlcon)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoAlta, PermisosN.Secciones.Ventas) = True Then
                If MsgBox("¿Desea eliminar este concepto de la nota de cargo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbNotasdeCargoDetalles(MySqlcon)
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
                    PopUp("Concepto Eliminado", 90)
                End If

            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub BotonCliente()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            If B.Cliente.DireccionFiscal = 0 Then
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            Else
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            End If
            'TextBox13.Text = "Límite: " + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Días: " + B.Cliente.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(B.Cliente.Saldo, "#,##0.00")
            If B.Cliente.NoChecarCr = 0 Then
                TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00") + vbCrLf + "A Favor: " + Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "#,##0.00")
            Else
                TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00")
            End If
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Then
                'If B.Cliente.Credito > 0 Then
                '    ComboBox4.SelectedIndex = 1
                'Else
                '    ComboBox4.SelectedIndex = 0
                'End If
            End If
            idCliente = B.Cliente.ID
            Isr = B.Cliente.ISR
            IvaRetenido = B.Cliente.IvaRetenido
            SIVA = B.Cliente.IVA
            SObre = B.Cliente.SobreescribeIVA
            If SObre = 0 Then
                TextBox8.Text = "16"
            Else
                TextBox8.Text = SIVA.ToString
            End If
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            TextBox5.Focus()
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
        Dim f As New frmNotasdeCargoConsulta(ModosDeBusqueda.Secundario, "")
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idNota = f.IdVenta
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta nota de cargo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoAlta, PermisosN.Secciones.Ventas) = True Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim C As New dbNotasdeCargo(idNota, MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idNota)
                C.Eliminar(idNota)
                PopUp("Nota de cargo eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub



    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoCancelar, PermisosN.Secciones.Ventas) = True Then
            If MsgBox("¿Cancelar esta nota de cargo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'If EsElectronica = 2 And GlobalConector = 1 Then
                Dim V As New dbNotasdeCargo(idNota, MySqlcon)
                V.DaDatosTimbrado(idNota)
                Dim op As New dbOpciones(MySqlcon)
                If EsElectronica = 2 And GlobalConector = 1 And GlobalPacCFDI = 1 Then
                    V.DaDatosTimbrado(idNota)
                    Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                    If V.CancelarTimbrado(S.RFC, V.uuid) = 1 Then
                        Modificar(Estados.Cancelada)
                    Else
                        MsgBox("Error en la cancelación de la nota de carggo. Intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If EsElectronica = 2 And GlobalPacCFDI = 2 Then
                        V.DaDatosTimbrado(idNota)
                        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                        If V.CancelarTimbrado2(S.RFC, V.uuid, op._ApiKey) = 1 Then
                            Modificar(Estados.Cancelada)
                        Else
                            MsgBox("Error en la cancelación de la nota de cargo. Intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        Modificar(Estados.Cancelada)
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

    Private Sub CadenaOriginal()
        Dim en As New Encriptador
        Dim V As New dbNotasdeCargo(idNota, MySqlcon)
        Dim RutaXML As String
        Dim RutaPDF As String
        'TextBox9.Text = 
        'TextBox10.Text = 
        'en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            Cadena = V.CreaCadenaOriginal22(idNota, GlobalIdMoneda)
        Else
            Cadena = V.CreaCadenaOriginal(idNota, GlobalIdMoneda)
        End If

        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXML = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
        Dim Enc As New System.Text.UTF8Encoding
        Dim xmldoc As String
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            xmldoc = V.CreaXML22(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        Else
            xmldoc = V.CreaXML(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        End If
        Dim Bytes() As Byte = Enc.GetBytes(xmldoc)
        'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        RutaXML = RutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        'en.GuardaArchivo(My.Settings.rutaxmlnca + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCA-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        en.GuardaArchivoTexto(RutaXML + "\NCA-" + V.Serie + V.Folio.ToString + ".xml", xmldoc, System.Text.Encoding.UTF8)
        PrintDocument1.DocumentName = "NCA-" + V.Serie + V.Folio.ToString
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.VentaNotadeCargo)
        TipoImpresora = SA.TipoImpresora
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            'obj.WriteSettings()

            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion()
        If TipoImpresora = 0 Then
            LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo)
        Else
            LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo + 1000)
        End If
        PrintDocument1.Print()
        'Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\NCA-" + V.Serie + V.Folio.ToString + ".pdf", 1000)
        If V.Cliente.Email <> "" Then
            Try
                If MsgBox("¿Enviar nota de cargo por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If V.Cliente.Email <> "" Then
                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        Dim O As New dbOpciones(MySqlcon)
                        Dim C As String
                        C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "NOTA DE CARGO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                        M.send("Comprobante fiscal digital Nota de cargo " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\NCA-" + V.Serie + V.Folio.ToString + ".pdf", RutaXML + "\NCA-" + V.Serie + V.Folio.ToString + ".xml")
                    End If
                End If
            Catch ex As Exception
                MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbConcepto.Focus()
        End If
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        IVaDefault = S.Impuesto
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, GlobalTipoFacturacion)
        TextBox11.Text = Sf.Serie
        If Sf.EsElectronica > 0 Then
            CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
        End If
        Dim V As New dbNotasdeCargo(MySqlcon)
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
        'Try
        '    Dim Forma As New frmBuscaDocumentoVenta(1, False)
        '    If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '        Dim V As New dbVentas(Forma.Id, MySqlcon)
        '        If Estado = 0 Then
        '            '0 cotizacion
        '            '1 pedido
        '            '2 remision
        '            '3 ventas
        '            Select Case Forma.Tipo
        '                Case 0
        '                    Dim Co As New dbVentasCotizaciones(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Co.Cliente.Clave
        '                Case 1
        '                    Dim Cp As New dbVentasPedidos(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Cp.Cliente.Clave
        '                Case 2
        '                    Dim Cr As New dbVentasRemisiones(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Cr.Cliente.Clave
        '                Case 3
        '                    Dim Cv As New dbVentas(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Cv.Cliente.Clave
        '            End Select

        '            Guardar()
        '            If Estado <> 0 Then
        '                V.AgregarDetallesReferencia(idNota, Forma.Id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
        '                ConsultaDetalles()
        '            End If
        '        Else
        '            V.AgregarDetallesReferencia(idNota, Forma.Id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
        '            ConsultaDetalles()
        '        End If
        '        NuevoConcepto()
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        'End Try
    End Sub
    'Version Normal
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If
        'If Estado = Estados.Cancelada Then
        '    e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
        'End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = MasPaginas

        'Dim O As New dbOpciones(MySqlcon)
        'Dim en As New Encriptador
        ''Dim XMLDoc As String
        'Dim V As New dbNotasdeCargo(idNota, MySqlcon)
        'Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
        'Dim Pluma As New Pen(Color.Black, 0.2)
        'Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)

        'en.Leex509(My.Settings.rutacer)

        'V.DaTotal(idNota, V.IdMoneda)
        ''CI = TI * (V.Iva / 100) 'Pendiente

        ''Try
        ''    e.Graphics.DrawImage(Image.FromFile(My.Settings.rutancfondo), 20, 1, 810, 1100)
        ''Catch ex As Exception

        ''End Try

        'e.Graphics.PageUnit = GraphicsUnit.Millimeter

        ''e.Graphics.DrawLine(Pluma, 10, 10, 10, 30)
        ''e.Graphics.DrawLine(Pluma, 10, 30, 100, 30)
        'Dim XEncabezado As Integer = 5
        'e.Graphics.DrawString(Sucursal.NombreFiscal, FuenteArialC, Brushes.Black, XEncabezado, 4)
        'e.Graphics.DrawString(Sucursal.Direccion + " " + Sucursal.NoExterior + " " + Sucursal.NoInterior, FuenteArialC, Brushes.Black, XEncabezado, 8)
        'e.Graphics.DrawString("Col: " + Sucursal.Colonia + " " + Sucursal.Ciudad + " " + Sucursal.Estado, FuenteArialC, Brushes.Black, XEncabezado, 12)
        'e.Graphics.DrawString("CP: " + Sucursal.CP + " RFC:" + Sucursal.RFC, FuenteArialC, Brushes.Black, XEncabezado, 16)
        'e.Graphics.DrawString(If(Sucursal.Telefono <> "", "Tel: " + Sucursal.Telefono, "") + If(Sucursal.Email <> "", " E-mail:" + Sucursal.Email, ""), FuenteArialC, Brushes.Black, XEncabezado, 20)
        'e.Graphics.DrawString(Sucursal.ReferenciaDomicilio, FuenteArialC, Brushes.Black, XEncabezado, 24)


        ''e.Graphics.DrawLine(Pluma, 10, 55, 100, 55)
        ''e.Graphics.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)

        'Dim strF As New StringFormat
        'strF.Alignment = StringAlignment.Near
        'strF.LineAlignment = StringAlignment.Near
        'Dim Rec As RectangleF
        'Rec = New RectangleF(5, 40, 100, 100)
        ''e.Graphics.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)
        'e.Graphics.DrawString("Datos del Cliente:", FuenteArialC, Brushes.Black, 5, 30)
        'e.Graphics.DrawString("Nombre: " + V.Cliente.Nombre, FuenteArialC, Brushes.Black, 5, 34)
        'If V.Cliente.DireccionFiscal = 0 Then
        '    e.Graphics.DrawString("Dom: " + V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteArialC, Brushes.Black, 5, 38)
        '    e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteArialC, Brushes.Black, 5, 42)
        '    e.Graphics.DrawString("Ciudad: " + V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteArialC, Brushes.Black, 5, 46)
        'Else
        '    e.Graphics.DrawString("Dom: " + V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteArialC, Brushes.Black, 5, 38)
        '    e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteArialC, Brushes.Black, 5, 42)
        '    e.Graphics.DrawString("Ciudad: " + V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteArialC, Brushes.Black, 5, 46)
        'End If
        'e.Graphics.DrawString("RFC: " + V.Cliente.RFC, FuenteArialC, Brushes.Black, 5, 50)
        'Dim SF As New dbSucursalesFolios(MySqlcon)
        'SF.BuscaFolios(V.IdSucursal, dbSucursalesFolios.TipoDocumentos.NotasDeCargo, 1)
        'e.Graphics.DrawString("NOTA DE CARGO", Fuente, Brushes.Black, 155, 4)
        'e.Graphics.DrawString("Folio:", Fuente, Brushes.Black, 155, 8)
        'e.Graphics.DrawString(V.Serie + Format(V.Folio, SF.Formato), Fuente, Brushes.Black, 155, 12)
        'e.Graphics.DrawString("No. y año de aprobación:", Fuente, Brushes.Black, 155, 16)
        'e.Graphics.DrawString(V.NoAprobacion + " " + V.YearAprobacion, Fuente, Brushes.Black, 155, 20)
        ''e.Graphics.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
        ''e.Graphics.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
        'e.Graphics.DrawString("No. Certificado: ", Fuente, Brushes.Black, 155, 24)
        'e.Graphics.DrawString(V.NoCertificado, Fuente, Brushes.Black, 155, 28)
        'e.Graphics.DrawString("Fecha:", Fuente, Brushes.Black, 155, 32)
        'e.Graphics.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 155, 36)
        ''e.Graphics.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)
        'e.Graphics.DrawString("CONCEPTOS", FuenteB, Brushes.Black, 10, 56)
        'e.Graphics.DrawLine(Pluma, 5, 61, 210, 61)

        ''e.Graphics.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
        ''e.Graphics.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
        ''e.Graphics.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
        ''e.Graphics.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
        ''e.Graphics.DrawString("Importe", Fuente, Brushes.Black, 170, 60)


        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim VI As New dbNotasdeCargoDetalles(MySqlcon)
        'DR = VI.ConsultaReader(idNota)
        'Dim CadenaB As String
        'Dim Y As Integer = 64
        'Dim YB As Integer
        'While DR.Read
        '    'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        '    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

        '    YB = Y
        '    'Dim Venta As New dbVentas(DR("idventa"), MySqlcon)
        '    CadenaB = DR("descripcion") '+ " Factura: " + DR("serieventa") + DR("folioventa").ToString

        '    Y = InsertaEnters(CadenaB, 60, Y, 4)
        '    Rec = New RectangleF(5, YB, 110, 200)
        '    e.Graphics.DrawString(DR("descripcion"), Fuente, Brushes.Black, Rec, strF)
        '    'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        '    'e.Graphics.DrawString(, Fuente, Brushes.Black, 130, YB)
        '    'e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 119, YB)
        '    'e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        '    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        '    Y += 6
        'End While
        'DR.Close()

        ''Dim VP As New dbVentasProductos(MySqlcon)
        ''DR = VP.ConsultaReader(idVenta)
        ''While DR.Read
        ''    e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        ''    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        ''    YB = Y
        ''    CadenaB = DR("descripcion")
        ''    Y = InsertaEnters(CadenaB, 34, Y, 4)
        ''    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        ''    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        ''    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        ''    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        ''    Y += 6
        ''End While
        ''DR.Close()


        ''Dim VS As New dbVentasServicios(MySqlcon)
        ''DR = VS.ConsultaReader(idVenta)
        ''While DR.Read
        ''    'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        ''    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        ''    YB = Y
        ''    CadenaB = DR("descripcion")
        ''    Y = InsertaEnters(CadenaB, 34, Y, 4)
        ''    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        ''    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        ''    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        ''    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        ''    Y += 6
        ''End While
        ''DR.Close()
        'e.Graphics.DrawLine(Pluma, 5, Y, 210, Y)
        'Y += 4
        'e.Graphics.DrawString("Sub total:", FuenteC, Brushes.Black, 153, Y)
        'e.Graphics.DrawString(Format(V.Subtotal - V.TotalIva, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        ''If V.IdMoneda <> 2 Then
        ''e.Graphics.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
        ''End If
        ''“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
        'Dim Ivas As New Collection
        'Dim IvasImporte As New Collection
        'DR = V.DaIvas(idNota)
        'Dim IAnt As Double
        'While DR.Read
        '    If Ivas.Contains(DR("iva").ToString) = False Then
        '        Ivas.Add(DR("iva"), DR("iva").ToString)
        '    End If
        '    If IvasImporte.Contains(DR("iva").ToString) = False Then
        '        IvasImporte.Add(DR("precio") - (DR("precio") / (1 + DR("iva") / 100)), DR("iva").ToString)
        '    Else
        '        IAnt = IvasImporte(DR("iva").ToString)
        '        IvasImporte.Remove(DR("iva").ToString)
        '        IvasImporte.Add(IAnt + (DR("precio") - (DR("precio") / (1 + DR("iva") / 100))), DR("iva").ToString)
        '    End If
        'End While
        'DR.Close()
        'Y += 4
        'For Each I As Double In Ivas
        '    'If I <> 0 Then
        '    e.Graphics.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.Black, 153, Y)
        '    e.Graphics.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        '    Y += 4
        '    'End If
        'Next
        ''If V.ISR <> 0 Then
        ''    e.Graphics.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
        ''    e.Graphics.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        ''    Y += 4
        ''End If
        ''If V.IvaRetenido <> 0 Then
        ''    e.Graphics.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
        ''    e.Graphics.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        ''    Y += 4
        ''End If
        'e.Graphics.DrawString("Total: ", Fuente, Brushes.Black, 153, Y)
        'e.Graphics.DrawString(Format(V.TotalNota, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

        'Dim f As New StringFunctions

        ''        Y = 175

        'If V.IdMoneda = 2 Then
        '    CadenaB = f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda)
        'Else
        '    CadenaB = f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda)
        'End If

        'YB = Y
        'Y = InsertaEnters(CadenaB, 63, Y, 4)
        'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 10, YB)



        'Y += 10
        'e.Graphics.DrawString("Cadena Original:", FuenteC, Brushes.Black, 10, Y)
        'Y += 4
        'YB = Y
        'CadenaB = Cadena
        'Y = InsertaEnters(CadenaB, 105, Y, 4)
        'e.Graphics.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

        ''e.Graphics.DrawString("Sello:", Fuente, Brushes.Black, 10, Y + 6)

        'Dim SelloB As String
        'Y += 10
        'e.Graphics.DrawString("Sello Digital:", FuenteC, Brushes.Black, 10, Y)
        'Y += 4
        'SelloB = Sello
        'YB = Y
        'Y = InsertaEnters(SelloB, 105, Y, 4)

        'e.Graphics.DrawString(SelloB, FuenteC, Brushes.Black, 10, YB)
        ''e.Graphics.DrawString("Pago en una sola exhibición", Fuente, Brushes.Black, 10, Y + 10)
        'e.Graphics.DrawString("Este documento es una representación impresa de un CFD", Fuente, Brushes.Black, 10, Y + 16)

    End Sub
    '---------------Termina Version Normal
    '--------------------------------------------------------------------
    'Version Daniel
    'Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage



    '    Dim O As New dbOpciones(MySqlcon)
    '    Dim en As New Encriptador
    '    'Dim XMLDoc As String
    '    Dim V As New dbVentas(idVenta, MySqlcon)
    '    Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim Pluma As New Pen(Color.Black, 0.5)
    '    'Dim Fondo As New Image
    '    'XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
    '    ' XMLDoc += "<Comprobante " + vbCrLf
    '    en.Leex509(My.Settings.rutacer)
    '    'Dim TI As Double
    '    'Dim CI As Double
    '    V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
    '    'CI = TI * (V.Iva / 100) 'Pendiente

    '    Try
    '        e.Graphics.DrawImage(Image.FromFile(My.Settings.rutafacturafondo), 20, 1, 810, 1100)
    '    Catch ex As Exception

    '    End Try

    '    e.Graphics.PageUnit = GraphicsUnit.Millimeter

    '    'e.Graphics.DrawLine(Pluma, 10, 10, 10, 30)
    '    'e.Graphics.DrawLine(Pluma, 10, 30, 100, 30)
    '    Dim XEncabezado As Integer = 65
    '    e.Graphics.DrawString(O._NombreEmpresa, FuenteArialB, Brushes.Black, XEncabezado, 4)
    '    e.Graphics.DrawString(O._Calle + " " + O._noExterior + " " + O._noInterior, FuenteArial, Brushes.Black, XEncabezado, 10)
    '    e.Graphics.DrawString("Col: " + O._Colonia + " " + O._Localidad + " " + O._Estado, FuenteArial, Brushes.Black, XEncabezado, 15)
    '    e.Graphics.DrawString("CP: " + O._CodigoPostal + " RFC:" + O._RFC, FuenteArial, Brushes.Black, XEncabezado, 20)
    '    e.Graphics.DrawString(O._ReferenciaDomicilio, FuenteArial, Brushes.Black, XEncabezado, 24)

    '    'e.Graphics.DrawLine(Pluma, 10, 34, 10, 55)
    '    'e.Graphics.DrawLine(Pluma, 10, 55, 100, 55)
    '    'e.Graphics.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)


    '    'Dim Nenter As String = V.Cliente.Nombre
    '    'InsertaEnters(Nenter, 60, 0, 0)
    '    'e.Graphics.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)

    '    Dim strF As New StringFormat
    '    strF.Alignment = StringAlignment.Near
    '    strF.LineAlignment = StringAlignment.Near
    '    Dim Rec As RectangleF
    '    Rec = New RectangleF(12, 40, 100, 100)
    '    e.Graphics.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)

    '    If V.Cliente.DireccionFiscal = 0 Then
    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
    '    Else
    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
    '    End If
    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)






    '    '    e.Graphics.DrawString(V.Cliente.Nombre, Fuente, Brushes.Black, 12, 42)
    '    '    If V.Cliente.DireccionFiscal = 0 Then
    '    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, Fuente, Brushes.Black, 12, 46)
    '    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, Fuente, Brushes.Black, 12, 50)
    '    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, Fuente, Brushes.Black, 12, 54)
    '    '    Else
    '    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, Fuente, Brushes.Black, 12, 46)
    '    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, Fuente, Brushes.Black, 12, 50)
    '    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, Fuente, Brushes.Black, 12, 54)
    '    '    End If
    '    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


    '    e.Graphics.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
    '    'e.Graphics.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
    '    e.Graphics.DrawString(O._noAprobacion + " " + O._yearAprobacion, Fuente, Brushes.Black, 163, 37)
    '    'e.Graphics.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
    '    'e.Graphics.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
    '    'e.Graphics.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
    '    e.Graphics.DrawString(en.Seriex509, Fuente, Brushes.Black, 163, 44)
    '    'e.Graphics.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
    '    e.Graphics.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
    '    'e.Graphics.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


    '    'e.Graphics.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
    '    'e.Graphics.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
    '    'e.Graphics.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
    '    'e.Graphics.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
    '    'e.Graphics.DrawString("Importe", Fuente, Brushes.Black, 170, 60)
    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(idVenta)
    '    Dim CadenaB As String
    '    Dim Y As Integer = 70
    '    Dim YB As Integer
    '    While DR.Read
    '        'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    '        'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

    '        YB = Y
    '        CadenaB = DR("descripcion")
    '        Y = InsertaEnters(CadenaB, 50, Y, 4)
    '        Rec = New RectangleF(32, YB, 120, 200)
    '        e.Graphics.DrawString(DR("descripcion"), Fuente, Brushes.Black, Rec, strF)
    '        'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 32, YB)
    '        e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 10, YB)
    '        e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 146, YB)
    '        e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    '        Y += 6
    '    End While
    '    DR.Close()

    '    Y = 170
    '    e.Graphics.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.Subtototal, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '    If V.IdConversion <> 2 Then
    '        e.Graphics.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
    '    End If
    '    '“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
    '    Dim Ivas As New Collection
    '    Dim IvasImporte As New Collection
    '    DR = V.DaIvas(idVenta)
    '    Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '        End If
    '    End While
    '    DR.Close()
    '    Y += 4
    '    For Each I As Double In Ivas
    '        'If I <> 0 Then
    '        e.Graphics.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '        'End If
    '    Next
    '    If V.ISR <> 0 Then
    '        e.Graphics.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    If V.IvaRetenido <> 0 Then
    '        e.Graphics.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    e.Graphics.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

    '    Dim f As New PaseLetras

    '    Y = 175

    '    If V.IdConversion = 2 Then
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.MN) + " M.N."
    '    Else
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.USD) + " U.S.D"
    '    End If

    '    YB = Y
    '    Y = InsertaEnters(CadenaB, 63, Y, 4)
    '    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 10, 175)

    '    Y = 198
    '    YB = Y
    '    CadenaB = Cadena
    '    Y = InsertaEnters(CadenaB, 105, Y, 4)
    '    e.Graphics.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

    '    Dim SelloB As String
    '    Y = 238
    '    SelloB = Sello
    '    YB = Y
    '    Y = InsertaEnters(SelloB, 105, Y, 4)

    '    e.Graphics.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)

    'End Sub
    '----------------Termina Version DAniel---------------------------------------------------------------
    '-------------------------------------------------------------------------------------------------------
    Private Sub Imprimir()
        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        RutaPDF = Archivos.DaRutaArchivos(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
        Dim op As New dbOpciones(MySqlcon)
        If Op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
        End If
        'RutaPDF = RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
        PrintDocument1.DocumentName = "Nota de cargo " + TextBox11.Text + TextBox2.Text
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.VentaNotadeCargo)
        TipoImpresora = SA.TipoImpresora
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            'obj.WriteSettings()

            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion()
        If TipoImpresora = 0 Then
            LlenaNodos(IdsSucursales.Valor(ComboBox3.SelectedIndex), TiposDocumentos.VentaNotadeCargo)
        Else
            LlenaNodos(IdsSucursales.Valor(ComboBox3.SelectedIndex), TiposDocumentos.VentaNotadeCargo + 1000)
        End If
        PrintDocument1.Print()
    End Sub
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        'If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        Dim Op As New dbOpciones(MySqlcon)
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Select Case EsElectronica
            Case 0
                Imprimir()
            Case 1
                CadenaOriginal()
            Case 2
                CadenaOriginali(Estado)
        End Select
        'PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
        'PrintDocument1.Print()
        'End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ConsultaOn Then
        '    If IsNumeric(TextBox6.Text) And IsNumeric(TextBox10.Text) Then
        '        If IDsMonedas.Valor(ComboBox1.SelectedIndex) = 2 Then
        '            TextBox12.Text = CStr(PrecioU * CDbl(TextBox10.Text))
        '            'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
        '        Else
        '            TextBox12.Text = CStr(PrecioU / CDbl(TextBox10.Text))
        '            'TextBox6.Text = CStr(CDbl(TextBox6.Text) / CDbl(TextBox10.Text))
        '        End If
        '    End If
        'End If
    End Sub
    Private Function InsertaEnters(ByRef Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Then
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function

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

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        'If ConsultaOn Then
        '    If IsNumeric(TextBox12.Text) And IsNumeric(TextBox9.Text) Then
        '        If PrecioBase <> 0 Then
        '            TextBox12.Text = CStr(PrecioBase - (PrecioBase * CDbl(TextBox9.Text) / 100))
        '        Else
        '            TextBox12.Text = CStr(PrecioU - (PrecioU * CDbl(TextBox9.Text) / 100))
        '        End If
        '        'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
        '    End If
        'End If
    End Sub

    'Private Sub CadenaOriginali(ByVal pEstado As Integer)
    '    Dim en As New Encriptador
    '    Dim V As New dbNotasdeCargo(idNota, MySqlcon)
    '    Dim RutaXmlTemp As String
    '    Dim RutaXml As String
    '    Dim RutaXMLTimbrado As String
    '    Dim RutaPDF As String
    '    Dim MsgError As String = ""
    '    'Cadena = V.CreaCadenaOriginali(idNota, GlobalIdMoneda)

    '    If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
    '        Cadena = V.CreaCadenaOriginali32(idNota, GlobalIdMoneda)
    '    Else
    '        Cadena = V.CreaCadenaOriginali(idNota, GlobalIdMoneda)
    '    End If

    '    Dim Archivos As New dbSucursalesArchivos
    '    Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
    '    RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoXML, False)
    '    RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoPDF, False)
    '    Archivos.CierraDB()
    '    Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"))
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
    '    RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCACFDIb-" + V.Serie + V.Folio.ToString + ".xml"
    '    RutaXMLTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCACFDI-" + V.Serie + V.Folio.ToString + "_TIMBRADO.xml"
    '    RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCACFDI-" + V.Serie + V.Folio.ToString + ".xml"


    '    Dim Enc As New System.Text.UTF8Encoding

    '    'Dim strXML As String = V.CreaXMLi(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
    '    Dim strXML As String
    '    If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
    '        strXML = V.CreaXMLi32(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
    '    Else
    '        strXML = V.CreaXMLi(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
    '    End If

    '    Dim Bytes() As Byte = Enc.GetBytes(strXML)
    '    'Dim Os As New dbOpciones(MySqlcon)
    '    Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
    '    'en.GuardaArchivo(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
    '    V.DaDatosTimbrado(idNota)
    '    If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
    '        If GlobalPacCFDI = 0 Then
    '            'en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
    '            Dim Timbre As TimbreFiscal.TimbreFiscalDigital
    '            Dim sa As New dbSucursalesArchivos
    '            sa.DaOpciones(GlobalIdEmpresa, True)
    '            Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado)
    '            V.NoCertificadoSAT = Timbre.noCertificadoSAT
    '            If V.NoCertificadoSAT <> "Error" Then
    '                V.uuid = Timbre.UUID
    '                V.FechaTimbrado = Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss")
    '                V.SelloCFD = Timbre.selloCFD
    '                V.NoCertificadoSAT = Timbre.noCertificadoSAT
    '                V.SelloSAT = Timbre.selloSAT
    '                V.GuardaDatosTimbrado(idNota, Timbre.UUID, Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss"), Timbre.selloCFD, Timbre.noCertificadoSAT, Timbre.selloSAT)
    '                Dim strTimbrado As String
    '                strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
    '                strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
    '                strTimbrado += "</cfdi:Complemento>" + vbCrLf
    '                strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
    '                en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
    '            End If
    '        Else
    '            Dim Os As New dbOpciones(MySqlcon)
    '            If GlobalConector = 0 Then
    '                en.GuardaArchivoTexto(RutaXmlTemp, strXML, System.Text.Encoding.UTF8)
    '                If IO.File.Exists(RutaXml) Then
    '                    IO.File.Delete(RutaXml)
    '                End If
    '            Else
    '                If IO.File.Exists(RutaXMLTimbrado) Then
    '                    IO.File.Delete(RutaXMLTimbrado)
    '                End If
    '                en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
    '            End If

    '            If V.Timbrar2(Os._UsuarioFacCom, Os._passFacCom, S.RFC, RutaXmlTemp, RutaXml, GlobalConector) = 0 Then
    '                V.NoCertificadoSAT = "Error"
    '            End If
    '            Dim xmldoc As New Xml.XmlDocument
    '            'Dim xmldoc2 As New Xml.XmlDocument
    '            If GlobalConector = 0 Then
    '                xmldoc.Load(RutaXml)
    '            Else
    '                Dim ChecarXML As String
    '                ChecarXML = en.LeeArchivoTexto(RutaXMLTimbrado)
    '                If ChecarXML.StartsWith("ERROR") Then
    '                    MsgError = ChecarXML
    '                    V.NoCertificadoSAT = "Error"
    '                Else
    '                    If ChecarXML.StartsWith("<?xml version=""1.0"" encoding=""utf-8""?>" + vbCrLf + "<?xml version=""1.0"" encoding=""utf-8""?>") Then
    '                        ChecarXML = ChecarXML.Substring(40, ChecarXML.Length - 40)
    '                        en.GuardaArchivoTexto(RutaXMLTimbrado, ChecarXML, System.Text.Encoding.UTF8)
    '                    End If
    '                    xmldoc.Load(RutaXMLTimbrado)
    '                End If

    '                'xmldoc2.Load(RutaXmlTimbrado)
    '            End If
    '            If V.NoCertificadoSAT <> "Error" And GlobalConector = 0 Then
    '                If xmldoc.DocumentElement.Name = "ERROR" Then
    '                    V.NoCertificadoSAT = "Error"
    '                    MsgError = xmldoc.InnerText
    '                End If
    '            End If
    '            If V.NoCertificadoSAT <> "Error" Then
    '                '    V.NoCertificadoSAT = "Error"
    '                '    MsgError = xmldoc.InnerText
    '                'Else
    '                V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
    '                V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
    '                V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
    '                V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
    '                V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value

    '                V.GuardaDatosTimbrado(idNota, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
    '                If IO.File.Exists(RutaXmlTemp) Then
    '                    IO.File.Delete(RutaXmlTemp)
    '                End If
    '                If IO.File.Exists(RutaXml) And GlobalConector = 1 Then
    '                    IO.File.Delete(RutaXml)
    '                End If
    '            End If
    '        End If
    '    End If
    '    Else
    '    'crear xmls si esta timbrado y xml no existe
    '    End If
    '    If V.NoCertificadoSAT <> "Error" Then
    '        CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
    '        'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
    '        'IO.Directory.CreateDirectory(My.Settings.rutapdfnca + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '        'IO.Directory.CreateDirectory(My.Settings.rutapdfnca + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '        'IO.Directory.CreateDirectory(My.Settings.rutaxmlnca + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '        'IO.Directory.CreateDirectory(My.Settings.rutaxmlnca + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '        'Dim strTimbrado As String
    '        'strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
    '        'strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
    '        'strTimbrado += "</cfdi:Complemento>" + vbCrLf
    '        'strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
    '        'Bytes = Enc.GetBytes(strXML)
    '        ''en.GuardaArchivo(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
    '        'en.GuardaArchivoTexto(My.Settings.rutaxmlnca + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCACFDI-" + V.Serie + V.Folio.ToString + ".xml", strXML, System.Text.Encoding.UTF8)
    '        PrintDocument1.DocumentName = "PDFNCACFDI" + V.Serie + V.Folio.ToString
    '        If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then
    '            Dim obj As New Bullzip.PdfWriter.PdfSettings
    '            obj.Init()
    '            obj.PrinterName = My.Settings.impresoraPDF
    '            obj.WriteSettings()
    '            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
    '            obj.SetValue("ShowSettings", "never")
    '            obj.SetValue("ShowPDF", "yes")
    '            obj.SetValue("ShowSaveAS", "nofile")
    '            obj.SetValue("ConfirmOverwrite", "no")
    '            obj.SetValue("Target", "printer")
    '            obj.WriteSettings()
    '        End If
    '        PrintDocument1.PrinterSettings.PrinterName = My.Settings.impresoraPDF
    '        LlenaNodosImpresion()
    '        LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo)
    '        PrintDocument1.Print()
    '        If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\PDFFacCFDI" + V.Serie + V.Folio.ToString + ".pdf", 1000)

    '        If V.Cliente.Email <> "" Then
    '            Try
    '                If MsgBox("¿Enviar nota de cargo por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '                    If V.Cliente.Email <> "" Then
    '                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
    '                        'Dim O As New dbOpciones(MySqlcon)
    '                        Dim C As String
    '                        C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "NOTA DE CARGO" + vbNewLine + "Folio: " + V.uuid + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
    '                        If GlobalConector = 0 Then
    '                            M.send("Comprobante Fiscal Digital por Internet Nota de Cargo: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PDFNCACFDI" + V.Serie + V.Folio.ToString + ".pdf", RutaXml)
    '                        Else
    '                            M.send("Comprobante Fiscal Digital por Internet Nota de Cargo: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PDFNCACFDI" + V.Serie + V.Folio.ToString + ".pdf", RutaXMLTimbrado)
    '                        End If
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '            End Try
    '        End If
    '    Else
    '        MsgBox("Ha ocurrido un error en el timbrado del la nota de cargo, intente mas tarde." + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
    '        If MsgBox("¿Guardar nota de cargo como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '            V.ModificaEstado(idNota, Estados.Pendiente)
    '            Nuevo()
    '        Else
    '            V.Eliminar(idNota)
    '            PopUp("Nota de Cargo Eliminada", 90)
    '            Nuevo()
    '        End If
    '        'Error en timbrado
    '    End If
    'End Sub



    Private Sub CadenaOriginali(ByVal pEstado As Byte)
        Dim en As New Encriptador
        Dim V As New dbNotasdeCargo(idNota, MySqlcon)
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            Cadena = V.CreaCadenaOriginali32(idNota, GlobalIdMoneda)
        Else
            Cadena = V.CreaCadenaOriginali(idNota, GlobalIdMoneda)
        End If
        'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECARGO-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECARGO-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECARGO-" + V.Serie + V.Folio.ToString + ".xml"


        'RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        Dim strXML As String
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            strXML = V.CreaXMLi32(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        Else
            strXML = V.CreaXMLi(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        End If

        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        V.NoCertificadoSAT = ""
        V.DaDatosTimbrado(idNota)



        If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
            If GlobalPacCFDI = 0 Then
                'en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                Dim sa As New dbSucursalesArchivos
                sa.DaOpciones(GlobalIdEmpresa, True)
                Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado)

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
                en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                Dim Timbre As String
                Dim sa As New dbSucursalesArchivos
                sa.DaOpciones(GlobalIdEmpresa, True)
                Timbre = V.Timbrar3(S.RFC, strXML, "", Os._ApiKey, True, V.Serie, V.Folio)
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
        Dim op As New dbOpciones(MySqlcon)
        If V.NoCertificadoSAT <> "Error" Then
            
            Try
                CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
                PrintDocument1.DocumentName = "PSSNOTADECARGO-" + V.Serie + V.Folio.ToString
                Dim SA As New dbSucursalesArchivos
                Dim Impresora As String
                Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.VentaNotadeCargo)
                TipoImpresora = SA.TipoImpresora
                If Impresora = "Bullzip PDF Printer" Then
                    Dim obj As New Bullzip.PdfWriter.PdfSettings
                    obj.Init()
                    obj.PrinterName = Impresora
                    'obj.WriteSettings()
                    obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                    obj.SetValue("ShowSettings", "never")
                    obj.SetValue("ShowPDF", "yes")
                    obj.SetValue("ShowSaveAS", "nofile")
                    obj.SetValue("ConfirmOverwrite", "no")
                    obj.SetValue("Target", "printer")
                    obj.WriteSettings()
                End If
                PrintDocument1.PrinterSettings.PrinterName = Impresora
                LlenaNodosImpresion()
                If TipoImpresora = 0 Then
                    LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo)
                Else
                    LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo + 1000)
                End If
                PrintDocument1.Print()
                'If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\PDFFacCFDI" + V.Serie + V.Folio.ToString + ".pdf", 1000)

                If op._ActivarPDF = "1" Then
                    LlenaNodosImpresion()
                    If TipoImpresora = 0 Then
                        LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo)
                    Else
                        LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCargo + 1000)
                    End If
                    'PrintDocument1.DocumentName = "FAC-" + V.Serie + V.Folio.ToString
                    Dim SA2 As New dbSucursalesArchivos
                    Impresora = SA2.DaImpresoraActiva(V.IdSucursal, GlobalIdEmpresa, True, 0, TiposDocumentos.PDF)
                    'TipoImpresora = SA.TipoImpresora
                    If Impresora = "Bullzip PDF Printer" Then
                        Dim obj As New Bullzip.PdfWriter.PdfSettings
                        obj.Init()
                        obj.PrinterName = Impresora
                        obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
                        obj.SetValue("ShowSettings", "never")
                        If op._MostrarPDF = "0" Then
                            obj.SetValue("ShowPDF", "no")
                        Else
                            obj.SetValue("ShowPDF", "yes")
                        End If
                        obj.SetValue("ShowSaveAS", "nofile")
                        obj.SetValue("ConfirmOverwrite", "no")
                        obj.SetValue("Target", "printer")
                        obj.WriteSettings()
                    End If
                    PrintDocument1.PrinterSettings.PrinterName = Impresora
                    'LlenaNodosImpresion(Op.TituloOriginalFactura)
                    PrintDocument1.Print()
                End If
            Catch ex As Exception
                MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try

            If V.Cliente.Email <> "" Then
                Try
                    If MsgBox("¿Enviar nota de cargo por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If V.Cliente.Email <> "" Then
                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            'Dim O As New dbOpciones(MySqlcon)
                            Dim C As String
                            C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "NOTA DE CARGO" + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                            C += op.CorreoContenido
                            If GlobalConector = 0 Then
                                M.send("Comprobante Fiscal Digital por Internet Nota de Cargo: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PSSNOTADECARGO-" + V.Serie + V.Folio.ToString + ".pdf", RutaXml)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet Nota de Cargo: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PSSNOTADECARGO-" + V.Serie + V.Folio.ToString + ".pdf", RutaXmlTimbrado)
                            End If
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
            MsgBox("Ha ocurrido un error en el timbrado del la nota de cargo, intente mas tarde." + vbCrLf + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardar nota de cargo como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idNota, Estados.Pendiente)
                Nuevo()
            Else
                'Dim Se As New dbInventarioSeries(MySqlcon)
                'Se.QuitaSeriesAVenta(idVenta)
                'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idNota)
                PopUp("Nota de Cargo Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
        End If
    End Sub



    Private Sub LlenaNodosImpresion()

        Dim V As New dbNotasdeCargo(idNota, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        Dim O As New dbOpciones(MySqlcon)
        V.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
        V.DaDatosTimbrado(idNota)
        ImpND.Clear()

        ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
        ImpND.Add(New NodoImpresionN("", "nocuenta", "", 0), "nocuenta")
        ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
            ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
            ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
            ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
            ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
            ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
            ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
            ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")


        ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        If O._IgualarFechaTimbrado = 0 Then
            If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
            Else
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
            End If
        Else
            If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugar")
            Else
                ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugar")
            End If
        End If
        CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
        ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasdeCargoDetalles(MySqlcon)
        DR = VI.ConsultaReader(idNota)
        ImpNDD.Clear()
        CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()


        ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
        ImpND.Add(New NodoImpresionN("", "subtotalsinret", Format(V.Subtotal + V.TotalIva, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsinret")

        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idNota)
        ImpNDDi.Clear()
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
        If V.ISR <> 0 Then
            ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        End If
        If V.IvaRetenido <> 0 Then
            ImpNDDi.Add(New NodoImpresionN("IVA Ret. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVA Ret. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        End If
        ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalNota, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")

        Dim f As New StringFunctions
        Dim CL As New CLetras
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda), 0), "totalletra")
        If V.TotalNota >= 0 Then
            ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalNota, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalNota * -1, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        End If
        ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        If V.Estado = Estados.Cancelada Then
            ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If

        Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalNota, "0000000000.000000") + "&id=" + V.uuid, System.Text.Encoding.Default)
        NumeroPagina = 1
    End Sub

    Private Sub LlenaNodos(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
        Dim I As New dbImpresionesN(MySqlcon)
        Dim Fs As FontStyle
        ImpNDi.Clear()
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Try
            dr = I.DaNodos(pDocumento, pidSucursal)
            While dr.Read
                Select Case dr("fuentestilo")
                    Case 1
                        Fs = FontStyle.Bold
                    Case 2
                        Fs = FontStyle.Italic
                    Case 0
                        Fs = FontStyle.Regular
                    Case 8
                        Fs = FontStyle.Strikeout
                    Case 4
                        Fs = FontStyle.Underline
                End Select
                ImpNDi.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs, GraphicsUnit.Point), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")))
            End While
            dr.Close()
        Catch ex As Exception

        End Try

    End Sub
    'Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
    '    Dim Nimp As NodoImpresionN
    '    Dim strF As New StringFormat
    '    Dim SA As New dbSucursalesArchivos

    '    MasPáginas = False
    '    Dim niva As New NodoImpresionN("iva", "0", "0", 0)
    '    Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
    '    strF.Alignment = StringAlignment.Near
    '    strF.LineAlignment = StringAlignment.Near
    '    e.PageUnit = GraphicsUnit.Millimeter
    '    Try
    '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCargo, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
    '    Catch ex As Exception

    '    End Try
    '    Dim Rec As RectangleF
    '    'Nodos fijos
    '    Try
    '        For Each n As NodoImpresionN In ImpNDi
    '            If n.DataPropertyName = "iva" Then niva = n
    '            If n.DataPropertyName = "codigobi" Then ncb = n
    '            If n.DataPropertyName <> "iva" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "isr" And n.DataPropertyName <> "ivaretenido" And n.DataPropertyName <> "codigobi" Then
    '                If n.Visible = 1 And n.Tipo = 0 Then
    '                    Select Case n.TipoNodo
    '                        Case 0
    '                            'normal
    '                            Nimp = ImpND(n.DataPropertyName)
    '                            If n.TipoDato = 0 Then
    '                                If n.ConEtiqueta = 1 Then
    '                                    Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
    '                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
    '                                    'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
    '                                Else
    '                                    Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
    '                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
    '                                End If
    '                            Else
    '                                If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
    '                                e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
    '                            End If

    '                        Case 1
    '                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
    '                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
    '                            'lineas
    '                        Case 2
    '                            'etiquetas
    '                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
    '                    End Select
    '                End If
    '            End If
    '        Next


    '        'IVAS
    '        Dim XCoord As Integer = 0
    '        Dim YCoord As Integer = 0
    '        Dim C As Integer


    '        C = 0
    '        While C < ImpNDDi.Count
    '            'For Each n As NodoImpresionN In ImpNDi
    '            Nimp = ImpNDDi("iva" + Format(C, "00"))

    '            'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
    '            If YCoord = 0 Then YCoord = niva.Y / 40 * 10

    '            If niva.ConEtiqueta = 1 Then
    '                e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
    '                e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
    '            Else
    '                e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
    '            End If
    '            'End If
    '            'Next
    '            YCoord += 4
    '            C += 1
    '        End While

    '        'Nodos Detalles            



    '        C = Posicion
    '        YCoord = 0

    '        Dim YExtra As Integer = 0
    '        Dim YExtra2 As Integer = 0
    '        While C < CuantosRenglones
    '            YExtra = 0
    '            YExtra2 = 0
    '            If YCoord >= 166 And Posicion > 0 Then
    '                MasPáginas = True
    '                Exit While
    '            End If
    '            For Each n As NodoImpresionN In ImpNDi
    '                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" Then
    '                    If YCoord = 0 Then YCoord = n.Y / 40 * 10
    '                    Select Case n.TipoNodo
    '                        Case 0
    '                            'normal
    '                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
    '                            If n.ConEtiqueta = 1 Then
    '                                If n.TipoDato = 0 Then
    '                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
    '                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
    '                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, 66, YCoord, 4)
    '                                    If YExtra < YExtra2 Then YExtra = YExtra2
    '                                Else
    '                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
    '                                End If

    '                            Else
    '                                If n.TipoDato = 0 Then
    '                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
    '                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
    '                                    YExtra2 = InsertaEnters(Nimp.Valor, 66, YCoord, 4)
    '                                    If YExtra < YExtra2 Then YExtra = YExtra2
    '                                Else
    '                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
    '                                End If
    '                            End If
    '                        Case 1
    '                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
    '                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
    '                            'YCoord += n.y/40L + 1
    '                            'lineas
    '                        Case 2
    '                            'etiquetas
    '                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
    '                    End Select
    '                End If
    '            Next
    '            YCoord = YCoord + 4 + YExtra
    '            Posicion += 1
    '            C += 1
    '        End While
    '        If ncb.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
    '        NumeroPagina += 1
    '    Catch ex As Exception
    '        MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try


    'End Sub

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.VentaNotadeCargo, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.VentaNotadeCargo + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            ' If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCargo, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCargo + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos Detalles            

        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '******************************
            Dim Hayrenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            'If YCoord >= LimY And Posicion > 0 Then
            '    MasPaginas = True
            '    Exit While
            'End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    Hayrenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If Hayrenglon Then YCoord = YCoord + 4 + YExtra

            '********************************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
                End If
            Next
            If ncb.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        'Dim ImpDb As New dbImpresionesN(MySqlcon)
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCargo, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaNotadeCargo + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos fijos principio
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 And niva.Tipo = 0 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 0 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        For Each n As NodoImpresionN In ImpNDi
            If n.DataPropertyName.Contains("cancelado") Then
                e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
            End If
        Next

        'Nodos Detalles            
        XCoord = 0
        YCoord = 0
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra
            '******************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon Then YCoord = YCoord + 4 + YExtra


            '*********************************
            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 2 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(n.XL / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS
            Dim Ycoord2 As Integer
            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                Ycoord2 = 0
                If niva.Visible = 1 And niva.Tipo = 2 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

                Ycoord2 = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 2 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva2.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva2.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

            End If

            If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmClientes(1, idCliente)
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.CodigoCliente
        End If
    End Sub

    Private Sub cmbConcepto_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbConcepto.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub cmbConcepto_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbConcepto.SelectedIndexChanged

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
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox12.Focus()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
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
                Dim V As New dbNotasdeCargo(MySqlcon)
                V.ActualizaComentario(idNota, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub GeneraPoliza()
        Try
            Dim Op As New dbOpciones(MySqlcon)
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbNotasdeCargo(idNota, MySqlcon)
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
                cuantas = M.CuantasHay(8, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(8, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 8)
                        f.ShowDialog()
                        If f.DialogResult = Windows.Forms.DialogResult.OK Then
                            M.ID = f.IdMascara
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    If Canceladas = 0 Then
                        GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    Else
                        GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    End If
                    GP.GeneraPolizaGeneral(V.ID, V.IdCliente, 0, 0, 0, 0, 0)
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
            Dim V As New dbNotasdeCargo(idNota, MySqlcon)
            V.DaDatosTimbrado(idNota)
            V.DaTotal(idNota, V.IdMoneda)
            Dim RutaXml As String
            Dim ImpOp As Boolean = False
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCargoXML, False)
            Archivos.CierraDB()
            'Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(FacturaGlobal.Fecha), "yyyy"))
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSACUSECANNCA-" + V.Serie + V.Folio.ToString + ".xml"
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
                    AddErrorTimbrado(XmlAcuse, "Nata de cargo - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
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
                    Rep.SetParameterValue("rfccliente", V.Cliente.RFC)
                    Rep.SetParameterValue("Documento", "NOTA DE CARGO")
                    Rep.SetParameterValue("folio", V.Serie + V.Folio.ToString("00000"))
                    Rep.SetParameterValue("fechac", xmldoc.Item("Acuse").Attributes("Fecha").Value)
                    Rep.SetParameterValue("uuid", xmldoc.Item("Acuse").Item("Folios").Item("UUID").InnerText)
                    Rep.SetParameterValue("monto", V.TotalaPagar.ToString("$#,###,###,##0.00"))
                    Rep.SetParameterValue("sello", xmldoc.Item("Acuse").Item("Signature").Item("SignatureValue").InnerText)
                    Rep.SetParameterValue("nombrecliente", V.Cliente.Nombre)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                End If
            Catch ex As Exception
                AddError(ex.Message, "Nota de cargo - Acuse Impresión", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        Catch ex As Exception
            AddErrorTimbrado(ex.Message, "Nota de cargo - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class