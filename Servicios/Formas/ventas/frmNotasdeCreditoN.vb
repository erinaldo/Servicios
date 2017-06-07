Public Class frmNotasdeCreditoN
    Dim consultaOn As Boolean
    Dim IdCliente As Integer
    Dim IdVenta As Integer
    Dim IdPago As Integer
    Dim strFolio As String
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim idNota As Integer
    Dim CantidadNota As Double
    Dim IdsMonedas As New elemento
    Dim IdsSucursales As New elemento
    Dim Cadena As String
    Dim Sello As String
    Dim IvaDefault As Double
    Dim ImpDoc As ImprimirDocumento
    Dim CadenaCFDI As String
    Dim Op As dbOpciones
    Dim IdsTipos As New elemento
    Dim NC2Pasos As Byte
    Dim CertificadoCaduco As Boolean = False
    Dim SinTimbres As Boolean = False
    Dim Isr As Double
    Dim IvaRetenido As Double
    ' Dim Tabla As New DataTable
    Public Sub New(ByVal pFolio As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        strFolio = pFolio
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasPagos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        IdCliente = 0
        consultaOn = False
        dtpFecha1.Value = Date.Now
        dtpFecha2.Value = Date.Now
        consultaOn = True
        If GlobalTipoFacturacion > 1 Then
            SinTimbres = ChecaTimbres()
        End If
        If GlobaltpBanxico <> "Error" Then
            TextBox4.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox4.Text = CM.Cantidad.ToString
        End If
        Op = New dbOpciones(MySqlcon)
        ImpDoc = New ImprimirDocumento
        NC2Pasos = Op._NCDosPasos
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1")
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblconceptosnotasventas", cmbConcepto, "nombre", "nombret", "idconceptonotaventa", IdsTipos, " idconceptonotaventa>0 and tipo=1")
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        IvaDefault = 16
        If strFolio <> "" Then
            TextBox3.Text = strFolio
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        End If
        NuevoPago()
    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtdatoscliente.Text = B.Cliente.Nombre + " | " + B.Cliente.RFC
            IdCliente = B.Cliente.ID
            consultaOn = False
            txtcliente.Text = B.Cliente.Clave
            If B.Cliente.SobreescribeIVA = 0 Then
                TextBox5.Text = IvaDefault.ToString
            Else
                TextBox5.Text = B.Cliente.IVA.ToString
            End If
            Isr = B.Cliente.ISR
            IvaRetenido = B.Cliente.IvaRetenido
            consultaOn = True
            ConsultaDeudas(True)
            ConsultaAbonos()
        End If
    End Sub
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
    Private Sub ConsultaDeudas(ByVal ClearSelection As Boolean)
        Try
            If consultaOn Then
                Dim V As New dbVentas(MySqlcon)
                DGDetalles.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, TextBox3.Text, 0, CheckBox1.Checked, CheckBox2.Checked, 0, CheckBox3.Checked, CDbl(TextBox4.Text), True, IdsSucursales.Valor(ComboBox3.SelectedIndex), CheckBox4.Checked)
                If ClearSelection Then DGDetalles.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub dtpFecha1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        ConsultaDeudas(True)
    End Sub

    Private Sub dtpFecha2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        ConsultaDeudas(True)
    End Sub

    Private Sub txtcliente_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcliente.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(txtcliente.Text) Then
                    txtdatoscliente.Text = c.Nombre + " | " + c.RFC
                    IdCliente = c.ID
                    If c.SobreescribeIVA = 0 Then
                        TextBox5.Text = IvaDefault.ToString
                    Else
                        TextBox5.Text = c.IVA.ToString
                    End If
                    Isr = c.ISR
                    IvaRetenido = c.IvaRetenido
                    ConsultaDeudas(True)
                    ConsultaAbonos()
                Else
                    txtdatoscliente.Text = ""
                    IdCliente = 0
                    Isr = 0
                    IvaRetenido = 0
                    TextBox5.Text = IvaDefault.ToString
                    ConsultaDeudas(True)
                    ConsultaAbonos()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If e.RowIndex > -1 Then
            'Label9.Text = DGDetalles.Item(0, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(1, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(2, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(3, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(4, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(5, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(6, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(7, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(8, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(9, e.RowIndex).Value.ToString + "|" + DGDetalles.Item(10, e.RowIndex).Value.ToString
            'IdVenta = DGDetalles.Item(0, e.RowIndex).Value
            'If DGDetalles.Item(1, e.RowIndex).Value = 1 Then
            '    DGDetalles.Item(1, e.RowIndex).Value = 0
            'Else
            '    DGDetalles.Item(1, e.RowIndex).Value = 1
            'End If

            ''Dim V As New dbVentas(IdVenta, MySqlcon)
            'Dim R As Integer = 0
            'Dim T As Double = 0
            'While R < DGDetalles.RowCount
            '    If DGDetalles.Item(1, R).Value = 1 Then
            '        T += DGDetalles.Item(8, R).Value
            '    End If
            '    R += 1
            'End While
            'NuevoPago()
            'ConsultaAbonos()
            'TextBox1.Text = Format(T, "#0.00")
            'TextBox1.Focus()

        End If
    End Sub

    Private Sub ConsultaAbonos()
        Try
            Dim VP As New dbNotasDeCredito(MySqlcon)
            DataGridView1.DataSource = VP.ConsultaxCliente(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, False, "", True)
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(7).Visible = False
            DataGridView1.Columns(8).Visible = False
            DataGridView1.Columns(1).HeaderText = "Fecha"
            DataGridView1.Columns(2).HeaderText = "Serie"
            DataGridView1.Columns(3).HeaderText = "Folio"
            DataGridView1.Columns(4).HeaderText = "C. Cliente"
            DataGridView1.Columns(5).HeaderText = "Cliente"
            DataGridView1.Columns(6).HeaderText = "Importe"
            DataGridView1.Columns(7).HeaderText = "Aplicado"
            DataGridView1.Columns(8).HeaderText = "Estado"
            DataGridView1.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.Columns(2).Width = "60"
            DataGridView1.Columns(3).Width = "60"
            DataGridView1.Columns(6).Width = "100"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim VP As New dbVentasPagos(MySqlcon)
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Dim Sc As New dbSucursalesCertificados(MySqlcon)
            Dim Eselectronica As Byte
            If CDbl(TextBox1.Text) = 0 Then
                MsgBox("Debe indicar una cantidad", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If TextBox2.Text = "" Then
                If MsgBox("No se ha indicado ninguna descripción de la nota de crédito. ¿Continuar de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                    Exit Sub
                Else
                    TextBox2.Text = "NC"
                End If
            End If
            If TextBox5.Text = "" Then TextBox5.Text = "0"
            If IsNumeric(TextBox5.Text) = False Then
                MsgBox("El Iva debe ser un valor numérico", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            'Dim IdNota As Integer
            Dim Folio As Integer
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, GlobalTipoFacturacion)
            Sc.BuscaCertificado(IdsSucursales.Valor(ComboBox3.SelectedIndex))
            Dim NC As New dbNotasDeCredito(MySqlcon)
            If Button1.Text = "Guardar" Then
                Dim TotalaAbonar As Double
                Dim R As Integer = 0
                Dim T As Double = 0
                Dim AbonaTotal As Boolean = False
                Dim HuboAbonos As Boolean = False
                Dim Errores As Boolean = False
                TotalaAbonar = CDbl(TextBox1.Text)
                Eselectronica = Sf.EsElectronica
                Folio = NC.DaNuevoFolio(Sf.Serie, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                If Folio < Sf.FolioInicial Then Folio = Sf.FolioInicial
                If Folio > Sf.FolioFinal Then
                    Throw New Exception("Se a llegado al límite de folios")
                End If
                If Sf.EsElectronica > 0 Then
                    If ChecaCertificado(Sf.IdCertificado) = True Then
                        Throw New Exception("El certificado de sello digital está vencido.")
                    End If
                    If SinTimbres And Sf.EsElectronica > 1 Then
                        Throw New Exception("Timbres agotados o caducados.")
                    End If
                End If
                If Errores = False Then
                    NC.Guardar(IdCliente, Format(dtpFechaPago.Value, "yyyy/MM/dd"), Folio, 0, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), Sf.Serie, CDbl(TextBox4.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, 1, IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), Isr, IvaRetenido)
                    idNota = NC.ID
                    Dim NCD As New dbNotasDeCreditoDetalles(MySqlcon)
                    While R < DGDetalles.RowCount
                        If CDbl(DGDetalles.Item("Cantidad", R).Value) > 0 Then
                            'If TotalaAbonar > 0 Then
                            T = DGDetalles.Item("restante", R).Value
                            If CDbl(DGDetalles.Item("Cantidad", R).Value) >= Math.Round(T, 2) Then
                                AbonaTotal = True
                            End If
                            If AbonaTotal = False Then
                                If DGDetalles.Item("tipo", R).Value = 0 Then
                                    NCD.Guardar(idNota, 0, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    If CheckBox4.Checked = False Then VP.Guardar(DGDetalles.Item("idv", R).Value, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 1 Then
                                    NCD.Guardar(idNota, 1, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    VP.Guardar(0, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, DGDetalles.Item("idv", R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 2 Then
                                    NCD.Guardar(idNota, 2, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    VP.Guardar(0, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 2, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 3 Then
                                    NCD.Guardar(idNota, 3, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    VP.Guardar(0, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 3, 4)
                                End If
                                HuboAbonos = True
                            Else
                                If DGDetalles.Item("tipo", R).Value = 0 Then
                                    NCD.Guardar(idNota, 0, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    If CheckBox4.Checked = False Then VP.Guardar(DGDetalles.Item("idv", R).Value, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 1 Then
                                    NCD.Guardar(idNota, 1, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    VP.Guardar(0, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, DGDetalles.Item("idv", R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 2 Then
                                    NCD.Guardar(idNota, 2, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    VP.Guardar(0, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 2, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 3 Then
                                    NCD.Guardar(idNota, 3, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value, 0, 0, "80141629", "ACT")
                                    VP.Guardar(0, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, Sf.Formato), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 3, 4)
                                End If
                                HuboAbonos = True
                                AbonaTotal = False
                            End If
                            'TotalaAbonar -= T
                            'End If
                        End If
                        R += 1
                    End While
                    NC.DaTotal(idNota, IdsMonedas.Valor(ComboBox1.SelectedIndex))
                    If NC2Pasos = 1 Then
                        NC.Modificar(idNota, Format(dtpFechaPago.Value, "yyyy/MM/dd"), Folio, 0, 0, Estados.Pendiente, NC.Total, NC.TotalNota, IdCliente, Sf.Serie, CDbl(TextBox4.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), Eselectronica, TextBox14.Text, IvaRetenido, Isr)
                        Dim FNC As New frmNotasdeCredito(idNota)
                        FNC.ShowDialog()
                    Else
                        NC.Modificar(idNota, Format(dtpFechaPago.Value, "yyyy/MM/dd"), Folio, 0, 0, Estados.Guardada, NC.Total, NC.TotalNota, IdCliente, Sf.Serie, CDbl(TextBox4.Text), Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), Eselectronica, TextBox14.Text, IvaRetenido, Isr)
                        NC.Aplicar(idNota, NC.TotalNota, True)
                        Select Case GlobalTipoFacturacion
                            Case 0
                                Imprimir(Sf.Serie + Folio.ToString)
                            Case 1
                                CadenaOriginal()
                            Case 2
                                CadenaOriginali(Estados.Guardada)
                        End Select
                        GeneraPoliza()
                    End If
                    'If idNota <> 0 And HuboAbonos Then
                    ' Dim NC As New dbNotasDeCredito(MySqlcon)
                    ' NC.Aplicar(idNota, CantidadNota - TotalaAbonar, True)
                    'End I2f

                    ConsultaAbonos()
                    ConsultaDeudas(False)
                    NuevoPago()
                End If
                'Else
                '    VP.Modificar(IdPago, CDbl(TextBox1.Text), Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, 0, IdCliente)
                '    PopUp("Pago Modificado", 90)
                '    ConsultaAbonos()
                '    ConsultaDeudas(False)
                '    NuevoPago()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub NuevoPago()
        idNota = 0
        dtpFechaPago.Value = Now.Date
        TextBox1.Text = "0"
        cmbConcepto.SelectedIndex = 0
        TextBox2.Text = ""
        TextBox5.Text = IvaDefault.ToString
        Button1.Text = "Guardar"
        TextBox14.Text = ""
        Button2.Enabled = False
    End Sub
    Private Sub LlenaDatosPago()
        Try
            Dim VP As New dbVentasPagos(IdPago, MySqlcon)
            dtpFechaPago.Value = VP.Fecha
            TextBox1.Text = VP.Cantidad.ToString
            TextBox2.Text = VP.Tipo
            Button1.Text = "Modificar"
            Button2.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'IdPago = DataGridView1.Item(0, e.RowIndex).Value
        'LlenaDatosPago()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Try
        '    If MsgBox("¿Cancelar nota de crédito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '        Dim vp As New dbNotasDeCredito(MySqlcon)
        '        vp.Guardar(IdPago, Estados.Cancelada, IdCliente)
        '        NuevoPago()
        '        ConsultaAbonos()
        '        ConsultaDeudas(False)
        '        PopUp("Pago Cancelado", 80)
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        'End Try
    End Sub

    Private Sub DGDetalles_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellDoubleClick
        DGDetalles.Item("Cantidad", e.RowIndex).Value = DGDetalles.Item("Restante", e.RowIndex).Value
        Dim R As Integer = 0
        Dim T As Double = 0
        While R < DGDetalles.RowCount
            If DGDetalles.Item("Cantidad", R).Value Is DBNull.Value Then
                R += 1
            Else
                If DGDetalles.Item("Cantidad", R).Value > 0 Then
                    T += DGDetalles.Item("Cantidad", R).Value
                End If
                R += 1
            End If
        End While
        TextBox1.Text = Format(T, "#0.00")
    End Sub

    Private Sub DGDetalles_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellEndEdit
        Dim R As Integer = 0
        Dim T As Double = 0
        While R < DGDetalles.RowCount
            If DGDetalles.Item("Cantidad", R).Value Is DBNull.Value Then
                R += 1
            Else
                If DGDetalles.Item("Cantidad", R).Value > 0 Then
                    T += DGDetalles.Item("Cantidad", R).Value
                End If
                R += 1
            End If
        End While
        TextBox1.Text = Format(T, "#0.00")
        'TextBox1.Focus()
    End Sub

    Private Sub DGDetalles_CellErrorTextChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellErrorTextChanged

    End Sub

    Private Sub DGDetalles_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGDetalles.CellFormatting
        If CDbl(DGDetalles.Item(10, e.RowIndex).Value) <= 0 Then
            e.CellStyle.BackColor = ColorVerde
        End If
        If e.ColumnIndex = 8 Or e.ColumnIndex = 9 Or e.ColumnIndex = 10 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If e.ColumnIndex = 7 Then
            e.Value = Format(e.Value, "00000")
        End If
        If e.ColumnIndex = 6 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If DGDetalles.Item(5, e.RowIndex).Value <> "A" Then
            e.CellStyle.BackColor = ColorRojo
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim F As New frmNotasdeCredito(DataGridView1.Item(0, e.RowIndex).Value)
        F.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 6 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub


    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        consultaOn = False
        If CheckBox2.Checked Then CheckBox4.Checked = False
        consultaOn = True
        ConsultaDeudas(True)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        ConsultaDeudas(True)
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        ConsultaDeudas(True)
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        ConsultaDeudas(True)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        NuevoPago()
    End Sub




    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        'If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
        'e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        'End If
        e.HasMorePages = ImpDoc.MasPaginas
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
        Imprimir(V.ID)
        If V.Cliente.Email <> "" Then
            Try
                If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If V.Cliente.Email <> "" Then
                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        Dim C As String
                        C = "Eviado por: " + Op._NombreEmpresa + vbNewLine + "RFC: " + Op._RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                        M.send("Comprobante fiscal digital Nota de crédito " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\NC-" + V.Serie + V.Folio.ToString + ".pdf", RutaXML + "\NC-" + V.Serie + V.Folio.ToString + ".xml")
                    End If
                End If
            Catch ex As Exception
                MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
    End Sub

    'Private Sub CadenaOriginali(ByVal pEstado As Byte)
    '    Dim en As New Encriptador
    '    Dim V As New dbNotasDeCredito(idNota, MySqlcon)
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
    '    RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
    '    RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
    '    Archivos.CierraDB()
    '    Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"))
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCCFDIb" + V.Serie + V.Folio.ToString + ".xml"
    '    RutaXMLTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCCFDI" + V.Serie + V.Folio.ToString + "_TIMBRADO.xml"
    '    RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCCFDI" + V.Serie + V.Folio.ToString + ".xml"
    '    RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")

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
    '    'en.GuardaArchivo(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
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
    '                If xmldoc.DocumentElement.Name = "ERROR" Then
    '                    V.NoCertificadoSAT = "Error"
    '                    MsgError = xmldoc.InnerText
    '                End If
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
    '            If V.NoCertificadoSAT <> "Error" Then
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
    '    Else
    '        'crear xml timbrado
    '    End If
    '    If V.NoCertificadoSAT <> "Error" Then
    '        CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
    '        'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
    '        'IO.Directory.CreateDirectory(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '        'IO.Directory.CreateDirectory(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '        'IO.Directory.CreateDirectory(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '        'IO.Directory.CreateDirectory(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '        'Dim strTimbrado As String
    '        'strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
    '        'strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
    '        'strTimbrado += "</cfdi:Complemento>" + vbCrLf
    '        'strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
    '        'Bytes = Enc.GetBytes(strXML)
    '        'en.GuardaArchivo(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFacCFDI-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
    '        'en.GuardaArchivoTexto(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNCCFDI" + V.Serie + V.Folio.ToString + ".xml", strXML, System.Text.Encoding.UTF8)
    '        PrintDocument1.DocumentName = "PDFNCCFDI" + V.Serie + V.Folio.ToString
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
    '        'LlenaNodosImpresion()
    '        'LlenaNodos(V.IdSucursal, TiposDocumentos.Venta)
    '        LlenaNodosImpresion()
    '        LlenaNodos(V.IdSucursal, TiposDocumentos.VentaNotadeCredito)
    '        PrintDocument1.Print()
    '        If My.Settings.impresoraPDF = "Bullzip PDF Printer" Then Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\PDFNCCFDI" + V.Serie + V.Folio.ToString + ".pdf", 1000)

    '        If V.Cliente.Email <> "" Then
    '            Try
    '                If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '                    If V.Cliente.Email <> "" Then
    '                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
    '                        'Dim O As New dbOpciones(MySqlcon)
    '                        Dim C As String
    '                        C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.uuid + vbNewLine + "Comprobante fiscal digital por internet enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
    '                        If GlobalConector = 0 Then
    '                            M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PDFNCCFDI" + V.Serie + V.Folio.ToString + ".pdf", RutaXml)
    '                        Else
    '                            M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PDFNCCFDI" + V.Serie + V.Folio.ToString + ".pdf", RutaXMLTimbrado)
    '                        End If
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '            End Try
    '        End If
    '    Else
    '        MsgBox("Ha ocurrido un error en el timbrado del la nota de crédito, intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
    '        If MsgBox("Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '            'V.ModificaEstado(idNota, Estados.Pendiente)
    '            'Else
    '            'Dim Se As New dbInventarioSeries(MySqlcon)
    '            'Se.QuitaSeriesAVenta(idNota)
    '            'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
    '            Dim VP As New dbVentasPagos(MySqlcon)
    '            VP.CancelarPagosxDocumento(idNota, 1, IdCliente, Estados.Cancelada)
    '            V.Eliminar(idNota)
    '            PopUp("Nota de Crédito Eliminada", 90)

    '        End If
    '        'Error en timbrado
    '    End If
    'End Sub

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
        Dim Op As New dbOpciones(MySqlcon)
        If op._NoRutas = "0" Then
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
        'Dim op As New dbOpciones(MySqlcon)
        If V.NoCertificadoSAT <> "Error" Then
            Try
                CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
                PrintDocument1.DocumentName = "PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString
                Imprimir(V.ID)
            Catch ex As Exception
                MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try

            If V.Cliente.Email <> "" Then
                Try
                    If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If V.Cliente.Email <> "" Then
                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            'Dim O As New dbOpciones(MySqlcon)
                            Dim C As String
                            C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.uuid + vbNewLine + vbNewLine
                            C += Op.CorreoContenido
                            If GlobalConector = 0 Then
                                M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".pdf", RutaXml)
                            Else
                                M.send("Comprobante Fiscal Digital por Internet Nota de Crédito: " + V.uuid, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".pdf", RutaXmlTimbrado)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End Try
            End If
        Else
            MsgBox("Ha ocurrido un error en el timbrado del la nota de crédito, intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
            If MsgBox("¿Guardarla como pendiente? Si elige no; ésta se eliminará.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                V.ModificaEstado(idNota, Estados.Pendiente)
                'Nuevo()
            Else
                'Dim Se As New dbInventarioSeries(MySqlcon)
                'Se.QuitaSeriesAVenta(idNota)
                'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
                V.Eliminar(idNota)
                PopUp("Nota de Crédito Eliminada", 90)
                'Nuevo()
            End If

            'MsgBox("Ha ocurrido un error en el timbrado del la nota de crédito, intente mas tarde.", MsgBoxStyle.Critical, GlobalNombreApp)
            '        If MsgBox("Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '            'V.ModificaEstado(idNota, Estados.Pendiente)
            '            'Else
            '            'Dim Se As New dbInventarioSeries(MySqlcon)
            '            'Se.QuitaSeriesAVenta(idNota)
            '            'If V.Estado = Estados.Guardada Then V.RegresaInventario(idVenta)
            '            Dim VP As New dbVentasPagos(MySqlcon)
            '            VP.CancelarPagosxDocumento(idNota, 1, IdCliente, Estados.Cancelada)
            '            V.Eliminar(idNota)
            '            PopUp("Nota de Crédito Eliminada", 90)

            '        End If
            '        'Error en timbrado


            'Error en timbrado
        End If
    End Sub
    Private Sub LlenaNodosImpresion()
        Dim V As New dbNotasDeCredito(idNota, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idNota, IdsMonedas.Valor(ComboBox1.SelectedIndex))
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
        ImpDoc.ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nocuenta", "", 0), "nocuenta")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
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
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal - V.TotalIva + V.TotalIvaRetenida + V.TotalISR, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")

        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idNota)
        ImpDoc.ImpNDDi.Clear()
        Dim IAnt As Double
        '(Precio / (1 + (iIva / 100)))
        Dim Desglose As Double
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
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And Op._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
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
        ImpDoc.Posicion = 0
        Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        ImpDoc.CodigoBidimensional = CB.Encode("?re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalNota, "0000000000.000000") + "&id=" + V.uuid, System.Text.Encoding.Default)
        ImpDoc.NumeroPagina = 1
    End Sub

    

    Private Sub DGDetalles_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DGDetalles.DataError
        PopUp("Debe indicar una cantidad numérica.", 90)
    End Sub

    Private Sub cmbConcepto_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdatoscliente.KeyDown, txtcliente.KeyDown, TextBox5.KeyDown, TextBox4.KeyDown, TextBox3.KeyDown, TextBox2.KeyDown, TextBox1.KeyDown, dtpFechaPago.KeyDown, dtpFecha2.KeyDown, dtpFecha1.KeyDown, ComboBox3.KeyDown, ComboBox1.KeyDown, cmbConcepto.KeyDown, CheckBox3.KeyDown, CheckBox2.KeyDown, CheckBox1.KeyDown, Button5.KeyDown, Button4.KeyDown, Button2.KeyDown, Button1.KeyDown, btnBuscarCliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        ConsultaDeudas(True)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim f As New frmNotasDeCreditoConsulta(ModosDeBusqueda.Principal, "")
        'f.MdiParent = Me.MdiParent
        f.ShowDialog()
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        consultaOn = False
        If CheckBox4.Checked Then CheckBox2.Checked = False
        consultaOn = True
        ConsultaDeudas(True)
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
                        PopUp("Póliza generada.", 90)
                    End If
                End If
            End If
        Catch ex As Exception
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
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Cot.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(CDate(Cot.Fecha), "yyyy") + "\" + Format(CDate(Cot.Fecha), "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(CDate(Cot.Fecha), "yyyy") + "\" + Format(CDate(Cot.Fecha), "MM")
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