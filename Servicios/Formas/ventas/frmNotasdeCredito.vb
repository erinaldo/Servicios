﻿Public Class frmNotasdeCredito
    Dim idNota As Integer
    Dim IVaDefault As Double
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idCliente As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsTipos As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim Estado As Byte
    Dim FolioAnt As Integer
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
    Dim iTipoFacturacion As Byte
    Dim LimitedeFolios As Boolean = False
    Dim EsElectronica As Byte
    Dim CertificadoCaduco As Boolean = False
    Dim SinTimbres As Boolean = False
    Dim ImpDoc As ImprimirDocumento
    Dim Op As dbOpciones
    Public Sub New(Optional ByVal pidNota As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idNota = pidNota
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nota de crédito no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim C As New dbNotasDeCredito(idNota, MySqlcon)
                'C.RegresaInventario(idNota)
                C.Eliminar(idNota)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
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
        ImpDoc = New ImprimirDocumento
        Op = New dbOpciones(MySqlcon)

        Tabla.Columns.Add("Id", I.GetType)
        'Tabla.Columns.Add("TipoR", S.GetType)
        'Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        'Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Serie", S.GetType)
        Tabla.Columns.Add("Folio", I.GetType)
        Tabla.Columns.Add("Importe", D.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        'LlenaCombos("tblformasdepago", ComboBox4, "nombre", "nombret", "idforma", idsFormasDePago, , , "idforma")
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        LlenaCombos("tblconceptosnotasventas", cmbConcepto, "nombre", "nombret", "idconceptonotaventa", IdsTipos, " tipo=1 and idconceptonotaventa>0")

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
                Dim V As New dbNotasDeCredito(MySqlcon)
                T = V.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)

                Iva = V.TotalIva
                Label12.Text = Format(V.Subtotal - V.TotalIva + V.TotalIvaRetenida + V.TotalISR, "#,##0.00")
                Label13.Text = Format(V.TotalIva - V.TotalIvaRetenida - V.TotalISR, "#,##0.00")
                Label14.Text = Format(V.TotalNota, "#,##0.00")

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        DateTimePicker1.Value = Date.Now
        iTipoFacturacion = GlobalTipoFacturacion
        TextBox1.Text = ""
        FolioAnt = 0
        idNota = 0
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button35.Enabled = False
        Button14.Enabled = False
        TextBox14.Text = ""
        CheckBox1.Checked = False
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'TextBox11.Text = S.Serie
        IVaDefault = S.Impuesto
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, GlobalTipoFacturacion)
        TextBox11.Text = Sf.Serie
        EsElectronica = GlobalTipoFacturacion
        Dim V As New dbNotasDeCredito(MySqlcon)
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
        Isr = 0
        IvaRetenido = 0
        CheckBox1.Checked = False
        Panel1.Enabled = True
        Panel2.Enabled = True
        SerieAnt = ""
        Button2.Enabled = True
        Label24.Visible = False
        Button20.Enabled = False
        'ComboBox4.SelectedIndex = 0
        NuevoConcepto()
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        Else
            ComboBox3.Enabled = True
        End If
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
        If GlobalTipoVersion = 3 Then
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button3.Enabled = True
            Button10.Enabled = True
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
                Else
                    TextBox7.Text = ""
                    TextBox13.Text = ""
                    idCliente = 0
                    Isr = 0
                    IvaRetenido = 0
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
            Dim C As New dbNotasDeCredito(MySqlcon)
            Dim Desglozar As Byte
            If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."

            If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                If pEstado = Estados.Guardada Then TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            End If

            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoCancelar, PermisosN.Secciones.Ventas) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If MensajeError = "" Then
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                Dim O As New dbOpciones(MySqlcon)

                Dim CM As New dbMonedasConversiones(MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, GlobalTipoFacturacion)
                Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                C.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Modificar(idNota, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, pEstado, C.Subtotal, C.TotalNota, idCliente, TextBox11.Text, CDbl(TextBox10.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), EsElectronica, TextBox14.Text, IvaRetenido, Isr)
                Estado = pEstado
                If pEstado = Estados.Cancelada Then
                    Dim VP As New dbVentasPagos(MySqlcon)
                    VP.CancelarPagosxDocumento(idNota, 1, idCliente, Estados.Cancelada)
                    
                End If
                If pEstado = Estados.Guardada Then
                    Select Case EsElectronica
                        Case 0
                            Imprimir(idNota)
                        Case 1
                            CadenaOriginal()
                        Case 2
                            CadenaOriginali(pEstado)
                        Case 3
                            CadenaOriginali33(pEstado)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas) = True Then
                If idCliente <> 0 Then
                    Dim C As New dbNotasDeCredito(MySqlcon)
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
                    Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, GlobalTipoFacturacion)
                    Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                    C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text, CDbl(TextBox10.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, 1, IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), Isr, IvaRetenido)
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
    
    Private Sub LlenaDatosVenta()
        Dim C As New dbNotasDeCredito(idNota, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio
        iTipoFacturacion = C.EsElectronica
        TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        TextBox8.Text = C.Iva.ToString
        TextBox11.Text = C.Serie
        EsElectronica = C.EsElectronica
        TextBox10.Text = C.TipodeCambio.ToString
        TextBox14.Text = C.ComentarioNC
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        Isr = C.ISR
        IvaRetenido = C.IvaRetenido
        DateTimePicker1.Value = C.Fecha
        'ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdMoneda)
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
                Button20.Enabled = True
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
                Button20.Enabled = True
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button20.Enabled = False
                Button14.Enabled = True
                Button2.Enabled = True
                Button35.Enabled = False
        End Select
        If GlobalTipoVersion = 3 Then
            For Each cc As Control In Me.Controls
                cc.Enabled = False
            Next
            Button3.Enabled = True
            Button10.Enabled = True
            Button15.Enabled = True
            Button20.Enabled = True
            If Estado = Estados.Guardada Then Button13.Enabled = True
        End If
    End Sub
    Private Sub LlenaDatosDetalles()
        Panel1.Visible = True
        ConsultaDetalles()
    End Sub
    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbNotasDeCreditoDetalles(MySqlcon)
            T = CD.ConsultaReader(idNota)
            While T.Read
                'If T("idinventario") > 1 Then
                Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("serieventa"), T("folioventa"), T("precio"), T("abreviatura"))
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
        
        TextBox12.Text = "0"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "1"
        TextBox9.Text = "0"
        TextBox6.Text = "0"
        TextBox8.Text = IVaDefault.ToString
        PrecioBase = 0
        Button4.Enabled = False
        Button12.Visible = False
        Button9.Enabled = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        TextBox4.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbNotasDeCreditoDetalles(MySqlcon)
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
                    CD.Guardar(idNota, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), 0, 0, 0, "80141629", "ACT")
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
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), 0, 0, "80141629", "ACT")
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


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas) = True Then
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

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try

            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbNotasDeCreditoDetalles(IdDetalle, MySqlcon)
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            TextBox5.Text = CD.Cantidad.ToString
            TextBox8.Text = CD.Iva.ToString
            TextBox9.Text = CD.Descuento.ToString
            IdInventario = CD.Idinventario
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
            CantAnt = CD.Cantidad
            'IdAlmacen = CD.IdAlmacen
            TextBox6.Text = CD.Precio.ToString
            TextBox4.Text = CD.Descripcion
            Button4.Text = "Modificar Concepto"
            Button4.Enabled = True
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            'cmbtipoarticulo.Text = "A"



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nota de crédito no ha sido guardada. ¿Desea iniciar una nueva factura? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbNotasDeCredito(MySqlcon)
                c.Eliminar(idNota)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas) = True Then
                If MsgBox("¿Desea eliminar este concepto de la nota de crédito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbNotasDeCreditoDetalles(MySqlcon)
                    CD.Eliminar(IdDetalle)
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

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
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
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            TextBox5.Focus()
        End If
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
        Dim f As New frmNotasDeCreditoConsulta(ModosDeBusqueda.Secundario, "")
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idNota = f.IdVenta
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta nota de crédito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas) = True Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim VP As New dbVentasPagos(MySqlcon)
                VP.CancelarPagosxDocumento(idNota, 1, idCliente, Estados.Cancelada)
                Dim C As New dbNotasDeCredito(idNota, MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idNota)
                C.Eliminar(idNota)
                PopUp("Factura Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CDbl(TextBox5.Text))
        F.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoCancelar, PermisosN.Secciones.Ventas) = True Then
            If MsgBox("¿Cancelar esta nota de crédito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'If EsElectronica = 2 And GlobalConector = 1 Then
                Dim V As New dbNotasDeCredito(idNota, MySqlcon)
                V.DaDatosTimbrado(idNota)
                'Dim op As New dbOpciones(MySqlcon)
                If EsElectronica = 2 And GlobalConector = 1 And GlobalPacCFDI = 1 Then
                    V.DaDatosTimbrado(idNota)
                    Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                    If V.CancelarTimbrado(S.RFC, V.uuid) = 1 Then
                        Modificar(Estados.Cancelada)
                    Else
                        MsgBox("Error en la cancelación de la devolución. Intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If EsElectronica = 2 And GlobalPacCFDI = 2 Then
                        V.DaDatosTimbrado(idNota)
                        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                        If V.CancelarTimbrado2(S.RFC, V.uuid, op._ApiKey) = 1 Then
                            Modificar(Estados.Cancelada)
                        Else
                            MsgBox("Error en la cancelación de la devolución. Intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
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
        Dim V As New dbNotasDeCredito(idNota, MySqlcon)
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
        RutaXML = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
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
        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\"
        RutaXML = RutaXML + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\"
        'en.GuardaArchivo(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNC-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        en.GuardaArchivoTexto(RutaXML + "\NC-" + V.Serie + V.Folio.ToString + ".xml", xmldoc, System.Text.Encoding.UTF8)
        Imprimir(idNota)
        If V.Cliente.Email <> "" Then
            Try
                If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If V.Cliente.Email <> "" Then
                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        Dim O As New dbOpciones(MySqlcon)
                        Dim C As String
                        C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                        M.send("Comprobante fiscal digital Nota de crédito " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\NC-" + V.Serie + V.Folio.ToString + ".pdf", RutaXML + "\NC-" + V.Serie + V.Folio.ToString + ".xml")
                    End If
                End If
            Catch ex As Exception
                MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
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
        Dim V As New dbNotasDeCredito(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        If Sf.EsElectronica > 0 Then
            CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        'If IdInventario <> 0 Or IdVariante <> 0 Then
        If IsNumeric(TextBox5.Text) Then
            TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
        End If
        'End If
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
    
    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click

        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Select Case EsElectronica
            Case 0
                Imprimir(idNota)
            Case 1
                CadenaOriginal()
            Case 2
                CadenaOriginali(Estado)
            Case 3
                CadenaOriginali33(Estado)
        End Select
        
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

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        If ConsultaOn Then
            If IsNumeric(TextBox12.Text) And IsNumeric(TextBox9.Text) Then
                If PrecioBase <> 0 Then
                    TextBox12.Text = CStr(PrecioBase - (PrecioBase * CDbl(TextBox9.Text) / 100))
                Else
                    TextBox12.Text = CStr(PrecioU - (PrecioU * CDbl(TextBox9.Text) / 100))
                End If
                'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
            End If
        End If
    End Sub

    Private Sub CadenaOriginali(ByVal pEstado As Byte)
        Dim en As New Encriptador
        Dim V As New dbNotasDeCredito(idNota, MySqlcon)
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        'Cadena = V.CreaCadenaOriginali(idNota, GlobalIdMoneda)
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            Cadena = V.CreaCadenaOriginali32(idNota, GlobalIdMoneda)
        Else
            Cadena = V.CreaCadenaOriginali(idNota, GlobalIdMoneda)
        End If
        Dim op As New dbOpciones(MySqlcon)
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
        If Op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        End If
        'RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")

        Dim Enc As New System.Text.UTF8Encoding
        'Dim strXML As String = V.CreaXMLi(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)


        Dim strXML As String
        If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
            strXML = V.CreaXMLi32(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        Else
            strXML = V.CreaXMLi(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
        End If
        en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
        Dim Bytes() As Byte = Enc.GetBytes(strXML)
        'Dim Os As New dbOpciones(MySqlcon)
        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        'en.GuardaArchivo(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        V.DaDatosTimbrado(idNota)
        If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
            If GlobalPacCFDI = 0 Then
                'en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                Dim sa As New dbSucursalesArchivos
                sa.DaOpciones(GlobalIdEmpresa, True)
                en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado, True)
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

                If V.Timbrar2(Os._UsuarioFacCom, Os._passFacCom, S.RFC, RutaXmlTemp, RutaXml, GlobalConector, True) = 0 Then
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
            'Crear XML Timbrado
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
            Imprimir(idNota)

            If V.Cliente.Email <> "" Then
                Try
                    If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If V.Cliente.Email <> "" Then
                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            'Dim O As New dbOpciones(MySqlcon)
                            Dim C As String
                            Dim rpdf As String
                            'If GlobalTipoVersion <> 3 Then
                            rpdf = RutaPDF + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".pdf"
                            'Else
                            '   rpdf = RutaPDF + "\NC-" + V.Serie + V.Folio.ToString + ".pdf"
                            'End If
                            C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                            C += op.CorreoContenido

                            If GlobalConector = 0 Then
                                M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, rpdf, RutaXml)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, rpdf, RutaXmlTimbrado)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End Try
            End If
        Else
            MsgBox("Ha ocurrido un error en el timbrado del la nota de crédito, intente mas tarde. " + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardarla como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idNota, Estados.Pendiente)
                Nuevo()
            Else
                'Dim Se As New dbInventarioSeries(MySqlcon)
                'Se.QuitaSeriesAVenta(idNota)
                'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idNota)
                PopUp("Nota de Crédito Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
        End If
    End Sub

    Private Sub CadenaOriginali33(ByVal pEstado As Byte)
        Dim en As New Encriptador
        Dim V As New dbNotasDeCredito(idNota, MySqlcon)
        Dim RutaXmlTemp As String
        Dim RutaXml As String
        Dim RutaXmlTimbrado As String
        Dim RutaPDF As String
        Dim MsgError As String = ""
        
        Cadena = V.CreaCadenaOriginali33(idNota, GlobalIdMoneda, "", GlobalIdEmpresa, "", 0, "")
        Dim Archivos As New dbSucursalesArchivos
        Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
        RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
        Archivos.CierraDB()
        Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
        RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
        If op._NoRutas = "0" Then
            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        End If
        'RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")

        Dim Enc As New System.Text.UTF8Encoding
        'Dim strXML As String = V.CreaXMLi(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)


        Dim strXML As String
        strXML = V.CreaXMLi33(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", 0)  
        en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
        Dim Bytes() As Byte = Enc.GetBytes(strXML)
        'Dim Os As New dbOpciones(MySqlcon)
        Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        'en.GuardaArchivo(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        V.DaDatosTimbrado(idNota)
        If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
            If GlobalPacCFDI = 2 Then
                en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                Dim Timbre As String
                Dim sa As New dbSucursalesArchivos
                sa.DaOpciones(GlobalIdEmpresa, True)
                Timbre = Timbrar33(S.RFC, strXML, "", Op._ApiKey, True, V.Folio, V.Serie, "NotadeCredito", V.ID)
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
            'Crear XML Timbrado
            Dim ExisteArchivo As Boolean = False
            If GlobalConector = 0 Then
                If IO.File.Exists(RutaXml) Then ExisteArchivo = True
            Else
                If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
            End If


            If pEstado = Estados.Guardada And ExisteArchivo = False Then
                Dim strTimbrado As String
                strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ SelloCFD=""" + V.SelloCFD + """ NoCertificadoSAT=""" + V.NoCertificadoSAT + """ SelloSAT=""" + V.SelloSAT + """ />" + vbCrLf
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
            Imprimir(idNota)

            If V.Cliente.Email <> "" Then
                Try
                    If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If V.Cliente.Email <> "" Then
                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            'Dim O As New dbOpciones(MySqlcon)
                            Dim C As String
                            Dim rpdf As String
                            'If GlobalTipoVersion <> 3 Then
                            rpdf = RutaPDF + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".pdf"
                            'Else
                            '   rpdf = RutaPDF + "\NC-" + V.Serie + V.Folio.ToString + ".pdf"
                            'End If
                            C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                            C += op.CorreoContenido

                            If GlobalConector = 0 Then
                                M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, rpdf, RutaXml)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, rpdf, RutaXmlTimbrado)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End Try
            End If
        Else
            MsgBox("Ha ocurrido un error en el timbrado del la nota de crédito, intente mas tarde. " + MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardarla como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idNota, Estados.Pendiente)
                Nuevo()
            Else
                'Dim Se As New dbInventarioSeries(MySqlcon)
                'Se.QuitaSeriesAVenta(idNota)
                'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idNota)
                PopUp("Nota de Crédito Eliminada", 90)
                Nuevo()
            End If
            'Error en timbrado
        End If
    End Sub

    Private Sub LlenaNodosImpresion()
        'Dim O As New dbOpciones(MySqlcon)
        Dim V As New dbNotasDeCredito(idNota, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
        V.DaDatosTimbrado(idNota)
        ImpDoc.ImpND.Clear()

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
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nocuenta", "", 0), "nocuenta")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")


        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
        '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'Else
        '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'End If

        Sucursal.LlenaExp(V.ID, 1)
        If Sucursal.HayExp Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.LugarExp, 0), "lugarexp")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.CalleExp, 0), "callerexp")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NumExp, 0), "numeroexp")
            If Op._IgualarFechaTimbrado = 0 Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.LugarExp + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugar")
            End If
        Else
            If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
                ImpDoc.ImpND.Add(New NodoImpresionN("", "lugarexp", Sucursal.Ciudad2, 0), "lugarexp")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "callerexp", Sucursal.Direccion2, 0), "callerexp")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "numeroexp", Sucursal.NoExterior2, 0), "numeroexp")
                If Op._IgualarFechaTimbrado = 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugar")
                End If
            Else
                ImpDoc.ImpND.Add(New NodoImpresionN("", "lugarexp", "", 0), "lugarexp")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "callerexp", "", 0), "callerexp")
                ImpDoc.ImpND.Add(New NodoImpresionN("", "numeroexp", "", 0), "numeroexp")
                If Op._IgualarFechaTimbrado = 0 Then
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
                Else
                    ImpDoc.ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.FechaTimbrado, "T", " "), 0), "lugar")
                End If
                End If
            End If
        Dim CadenaCFDI As String
        CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasDeCreditoDetalles(MySqlcon)
        DR = VI.ConsultaReader(idNota)
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            'If DR("idinventario") = 0 Then
            '    ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Factura: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            'Else
            '    ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Nota de Cargo: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            'End If
            If DR("idinventario") = 0 Then ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Factura: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            If DR("idinventario") = 1 Then ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Nota de Cargo: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            If DR("idinventario") = 2 Then ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Saldo Inicial: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            If DR("idinventario") = 3 Then ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion") + " | Documento: " + DR("serieventa") + DR("folioventa").ToString, 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8) + " " + DR("tipocantidad"), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format((DR("precio") / (1 + ((DR("iva") - V.ISR - V.IvaRetenido) / 100))) / DR("cantidad"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format((DR("precio") / (1 + ((DR("iva") - V.ISR - V.IvaRetenido) / 100))), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()


        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.ComentarioNC, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal - V.TotalIva + V.TotalISR + V.TotalIvaRetenida, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")

        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idNota)
        ImpDoc.ImpNDDi.Clear()
        Dim IAnt As Double
        Dim Desglose As Double
        '(Precio / (1 + (iIva / 100)))
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                Desglose = (DR("precio") / (1 + (((DR("iva") - V.ISR - V.IvaRetenido) / 100))))
                IvasImporte.Add(Desglose * DR("iva") / 100, DR("iva").ToString)
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                Desglose = (DR("precio") / (1 + (((DR("iva") - V.ISR - V.IvaRetenido) / 100))))
                IvasImporte.Add(IAnt + (Desglose * DR("iva") / 100), DR("iva").ToString)
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
        If V.ISR <> 0 Then
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
            'ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "isr")
            Cont += 1
        Else
            'ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "isr")
        End If
        If V.IvaRetenido <> 0 Then
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(V.TotalIvaRetenida, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
            'ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "ivaret")
            Cont += 1
        Else
            'ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenida, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "ivaret")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalNota, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "total")

        Dim f As New StringFunctions
        Dim CL As New CLetras
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda), 0), "totalletra")
        If V.TotalNota >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalNota, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalNota * -1, V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And Op._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        ImpDoc.Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        ImpDoc.CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalNota, "0000000000.000000") + "&id=" + V.uuid, System.Text.Encoding.Default)
        ImpDoc.NumeroPagina = 1
    End Sub

    Private Sub DateTimePicker1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown, TextBox8.KeyDown, TextBox7.KeyDown, TextBox6.KeyDown, TextBox5.KeyDown, TextBox4.KeyDown, TextBox3.KeyDown, TextBox2.KeyDown, TextBox12.KeyDown, TextBox11.KeyDown, TextBox10.KeyDown, TextBox1.KeyDown, DateTimePicker1.KeyDown, ComboBox8.KeyDown, ComboBox4.KeyDown, ComboBox3.KeyDown, ComboBox2.KeyDown, ComboBox1.KeyDown, cmbConcepto.KeyDown, CheckBox1.KeyDown, Button9.KeyDown, Button8.KeyDown, Button7.KeyDown, Button6.KeyDown, Button5.KeyDown, Button4.KeyDown, Button3.KeyDown, Button2.KeyDown, Button15.KeyDown, Button14.KeyDown, Button13.KeyDown, Button12.KeyDown, Button11.KeyDown, Button10.KeyDown, Button1.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{Tab}")
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
                Dim V As New dbNotasDeCredito(MySqlcon)
                V.ActualizaComentario(idNota, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim V As New dbNotasDeCredito(MySqlcon)
        V.DaDatosTimbrado(idNota)
        Dim FDT As New frmVentasDatosTimbrado(V.uuid, V.FechaTimbrado, V.NoCertificadoSAT, V.SelloCFD, V.SelloSAT)
        FDT.ShowDialog()
        FDT.Dispose()
    End Sub

    Private Sub GeneraPoliza()
        Try
            Dim Op As New dbOpciones(MySqlcon)
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbNotasDeCredito(idNota, MySqlcon)
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
                cuantas = M.CuantasHay(6, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(6, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 6)
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
                    GP.GeneraPolizaGeneral(V.ID, V.IdCliente, 0, 0, 0, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    Else
                        PopUp("Pólizada generada.", 90)
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
            Dim V As New dbNotasDeCredito(idNota, MySqlcon)
            V.DaDatosTimbrado(idNota)
            V.DaTotal(idNota, V.IdMoneda)
            Dim RutaXml As String
            Dim ImpOp As Boolean = False
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
            Archivos.CierraDB()
            'Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(FacturaGlobal.Fecha), "yyyy"))
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSACUSECANNCR-" + V.Serie + V.Folio.ToString + ".xml"
            If IO.File.Exists(RutaXml) = False Then
                Dim Suc As New dbSucursales(V.IdSucursal, MySqlcon)
                XmlAcuse = AcuseCancelacion(V.uuid, op._ApiKey, Suc.RFC, "Notas de crédito", idNota)
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
                    AddErrorTimbrado(XmlAcuse, "Nota de crédito - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
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
                    Rep.SetParameterValue("Documento", "NOTA DE CRÉDITO")
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
                AddError(ex.Message, "Nota de crédito - Acuse Impresión", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        Catch ex As Exception
            AddErrorTimbrado(ex.Message, "Nota de crédito - Acuse", Date.Now.ToString("yyyy/MM/dd"), Date.Now.ToString("HH:mm:ss"), idNota)
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Imprimir(pIdNota As Integer)
        ImprimirNota(idNota, False, "1")
        If Op._ActivarPDF = "1" Then
            ImprimirNota(idNota, True, Op._MostrarPDF)
        End If
    End Sub
    Private Sub ImprimirNota(pIdNota As Integer, pEsPDF As Boolean, pMostrarPDF As String)
        Try
            Dim Cot As New dbNotasDeCredito(pIdNota, MySqlcon)
            Dim S As New dbSucursales(Cot.IdSucursal, MySqlcon)
            ImpDoc.IdSucursal = Cot.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.VentaNotadeCredito
            ImpDoc.TipoDocumentoT = TiposDocumentos.VentaNotadeCredito + 1000
            If pEsPDF = False Then
                ImpDoc.TipoDocumentoImp = TiposDocumentos.VentaNotadeCredito
            Else
                ImpDoc.TipoDocumentoImp = TiposDocumentos.PDF
            End If
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF

            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF, pMostrarPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "PSSNOTADECREDITO-" + Cot.Serie + Cot.Folio.ToString
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class