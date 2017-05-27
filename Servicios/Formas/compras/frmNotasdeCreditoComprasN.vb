Public Class frmNotasdeCreditoComprasN

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
    Dim IdsTipos As New elemento
    Dim NC2Pasos As Byte
    Dim Rep As New DibujaReportes()
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
        If GlobaltpBanxico <> "Error" Then
            TextBox4.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox4.Text = CM.Cantidad.ToString
        End If
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1")
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblconceptosnotascompras", cmbConcepto, "nombre", "nombret", "idconceptonotacompra", IdsTipos, " idconceptonotacompra>0 and tipo=1")
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)

        Dim Op As New dbOpciones(MySqlcon)
        Op.RutaXMLEgresos = Op.DaRutaXMLCompras
        If Op.RutaXMLEgresos <> "" Then OpenFileDialog1.InitialDirectory = Op.RutaXMLEgresos
        NC2Pasos = Op._NCDosPasos
        If strFolio <> "" Then
            TextBox3.Text = strFolio
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            ComboBox3.Enabled = False
        End If
        NuevoPago()
    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtdatoscliente.Text = B.Proveedor.Nombre + " | " + B.Proveedor.RFC
            IdCliente = B.Proveedor.ID
            consultaOn = False
            txtcliente.Text = B.Proveedor.Clave
            consultaOn = True
            ConsultaDeudas(True)
            ConsultaAbonos()
        End If
    End Sub

    Private Sub ConsultaDeudas(ByVal ClearSelection As Boolean)
        Try
            If consultaOn Then
                Dim V As New dbCompras(MySqlcon)
                DGDetalles.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, TextBox3.Text, 0, CheckBox1.Checked, CheckBox2.Checked, 0, CheckBox3.Checked, CDbl(TextBox4.Text), True, IdsSucursales.Valor(ComboBox3.SelectedIndex))
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
                Dim c As New dbproveedores(MySqlcon)
                If c.BuscaProveedor(txtcliente.Text) Then
                    txtdatoscliente.Text = c.Nombre + " | " + c.RFC
                    IdCliente = c.ID
                    ConsultaDeudas(True)
                    ConsultaAbonos()
                Else
                    txtdatoscliente.Text = ""
                    IdCliente = 0
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
            Dim VP As New dbNotasdeCreditoCompras(MySqlcon)
            DataGridView1.DataSource = VP.ConsultaxProveedor(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, False, "", True)
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(6).Visible = False
            DataGridView1.Columns(7).Visible = False
            DataGridView1.Columns(1).HeaderText = "Fecha"
            'DataGridView1.Columns(2).HeaderText = "Serie"
            DataGridView1.Columns(2).HeaderText = "Folio"
            DataGridView1.Columns(3).HeaderText = "C. Prov."
            DataGridView1.Columns(4).HeaderText = "Proveedor"
            DataGridView1.Columns(5).HeaderText = "Importe"
            DataGridView1.Columns(6).HeaderText = "Aplicado"
            DataGridView1.Columns(7).HeaderText = "Estado"
            DataGridView1.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DataGridView1.Columns(2).Width = "40"
            DataGridView1.Columns(2).Width = "60"
            DataGridView1.Columns(5).Width = "100"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            'If TextBox6.Text = "" Then
            'MsgBox("Debe indicar un folio", MsgBoxStyle.Information, GlobalNombreApp)
            ' Exit Sub
            'Else
            Dim NCC As New dbNotasdeCreditoCompras(MySqlcon)
            'If NCC.ChecaFolioRepetido(TextBox6.Text, IdCliente) Then
            '    MsgBox("Folio repetido", MsgBoxStyle.Information, GlobalNombreApp)
            '    Exit Sub
            'End If
            'End If
            If CDbl(TextBox1.Text) = 0 Then
                MsgBox("Debe indicar una cantidad", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If TextBox5.Text = "" Then TextBox5.Text = "0"
            If IsNumeric(TextBox5.Text) = False Then
                MsgBox("Le Iva debe ser un valor numérico", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            Dim VP As New dbComprasPagos(MySqlcon)
            'Dim Sf As New dbSucursalesFolios(MySqlcon)
            'Dim Sc As New dbSucursalesCertificados(MySqlcon)
            'Dim IdNota As Integer
            Dim Folio As Integer
            'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, 1)
            'Sc.BuscaCertificado(IdsSucursales.Valor(ComboBox3.SelectedIndex))
            Dim NC As New dbNotasdeCreditoCompras(MySqlcon)
            If Button1.Text = "Guardar" Then
                Dim TotalaAbonar As Double
                Dim R As Integer = 0
                Dim T As Double = 0
                Dim AbonaTotal As Boolean = False
                Dim HuboAbonos As Boolean = False
                TotalaAbonar = CDbl(TextBox1.Text)
                Dim Errores As Boolean = False
                If Errores = False Then

                    Dim Sf As New dbSucursalesFolios(MySqlcon)
                    Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasNotasCredito, 0)
                    'Dim V As New dbNotasdeCreditoCompras(MySqlcon)
                    Folio = NCC.DaNuevoFolio(Sf.Serie, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                    If Folio < Sf.FolioInicial Then Folio = Sf.FolioInicial
                    NC.Guardar(IdCliente, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox6.Text, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), Sf.Serie, Folio, TextBox7.Text)
                    idNota = NC.ID
                    Dim NCD As New dbNotasdeCreditoComprasDetalles(MySqlcon)
                    While R < DGDetalles.RowCount
                        If DGDetalles.Item("Cantidad", R).Value > 0 Then
                            'If TotalaAbonar > 0 Then
                            T = DGDetalles.Item("restante", R).Value
                            If DGDetalles.Item("Cantidad", R).Value = Math.Round(T, 2) Then
                                AbonaTotal = True
                            End If
                            If AbonaTotal = False Then
                                If DGDetalles.Item("tipo", R).Value = 0 Then
                                    NCD.Guardar(idNota, 0, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(DGDetalles.Item("idv", R).Value, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 1 Then
                                    NCD.Guardar(idNota, 1, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(0, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, DGDetalles.Item("idv", R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 2 Then
                                    NCD.Guardar(idNota, 2, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(0, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 2, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 3 Then
                                    NCD.Guardar(idNota, 3, 1, DGDetalles.Item("Cantidad", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(0, DGDetalles.Item("Cantidad", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 3, 4)
                                End If
                                HuboAbonos = True
                            Else
                                If DGDetalles.Item("tipo", R).Value = 0 Then
                                    NCD.Guardar(idNota, 0, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(DGDetalles.Item("idv", R).Value, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 1 Then
                                    NCD.Guardar(idNota, 1, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(0, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, DGDetalles.Item("idv", R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 2 Then
                                    NCD.Guardar(idNota, 2, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(0, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 2, 4)
                                End If
                                If DGDetalles.Item("tipo", R).Value = 3 Then
                                    NCD.Guardar(idNota, 3, 1, DGDetalles.Item("restante2", R).Value, IdsMonedas.Valor(ComboBox1.SelectedIndex), TextBox2.Text, CDbl(TextBox5.Text), 0, DGDetalles.Item("idv", R).Value)
                                    VP.Guardar(0, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text + " Nota de Crédito-" + Sf.Serie + Format(Folio, "0000"), IdCliente, idNota, 1, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item("idv", R).Value, 3, 4)
                                End If
                                HuboAbonos = True
                                AbonaTotal = False
                            End If
                            '    TotalaAbonar -= T
                            'End If
                        End If
                        R += 1
                    End While
                    NC.DaTotal(idNota, IdsMonedas.Valor(ComboBox1.SelectedIndex))
                    If NC2Pasos = 1 Then
                        NC.Modificar(idNota, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox6.Text, 0, Estados.Pendiente, NC.Total, NC.TotalNota, IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), TextBox14.Text, Sf.Serie, Folio, TextBox7.Text)
                        Dim FNC As New frmNotasdeCreditoCompras(idNota)
                        FNC.ShowDialog()
                    Else
                        NC.Modificar(idNota, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox6.Text, 0, Estados.Guardada, NC.Total, NC.TotalNota, IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), TextBox14.Text, Sf.Serie, Folio, TextBox7.Text)
                        NC.Aplicar(idNota, NC.TotalNota, True)
                        CadenaOriginal()
                        GeneraPoliza()
                        PopUp("Guardado", 90)
                    End If
                    ConsultaAbonos()
                    ConsultaDeudas(False)
                    NuevoPago()
                    'If idNota <> 0 And HuboAbonos Then
                    ' Dim NC As New dbNotasDeCredito(MySqlcon)
                    ' NC.Aplicar(idNota, CantidadNota - TotalaAbonar, True)
                    'End If

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
        TextBox2.Text = ""
        TextBox14.Text = ""
        TextBox7.Text = ""
        TextBox6.Text = ""
        TextBox5.Text = "16"
        Button1.Text = "Guardar"
        Button2.Enabled = False
    End Sub
    Private Sub LlenaDatosPago()
        Try
            Dim VP As New dbComprasPagos(IdPago, MySqlcon)
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
        Try
            If MsgBox("¿Cancelar pago?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim vp As New dbComprasPagos(MySqlcon)
                vp.CancelarPago(IdPago, Estados.Cancelada, IdCliente)
                NuevoPago()
                ConsultaAbonos()
                ConsultaDeudas(False)
                PopUp("Pago Cancelado", 80)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

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
        'TextBox1.Focus()
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
        'If e.ColumnIndex = 6 Then
        '    e.Value = Format(e.Value, "00000")
        'End If
        'If e.ColumnIndex = 5 Then
        '    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'End If
        If DGDetalles.Item(5, e.RowIndex).Value <> "A" Then
            e.CellStyle.BackColor = ColorRojo
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim F As New frmNotasdeCreditoCompras(DataGridView1.Item(0, e.RowIndex).Value)
        F.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 6 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub


    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
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
        Rep.DibujaPaginaN(e.Graphics)
        If Rep.MasPaginas = True Or Rep.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(Rep.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If

        e.HasMorePages = Rep.MasPaginas
    End Sub
    Private Function InsertaEnters(ByRef Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        Dim SigLimite As Integer
        C = 0
        SigLimite = CadaCuantos
        While C < Cadena.Length
            If C >= SigLimite Then
                Cadena = Cadena.Insert(SigLimite, vbCrLf)
                SigLimite += CadaCuantos
                Y += AumentoY
            End If
            C += CadaCuantos
        End While
        Return Y
    End Function
    'Private Sub CadenaOriginal()
    '    'Dim en As New Encriptador
    '    Dim V As New dbNotasdeCreditoCompras(idNota, MySqlcon)
    '    'TextBox9.Text = 
    '    'TextBox10.Text = 
    '    'en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
    '    'Cadena = V.CreaCadenaOriginal(idNota, IdMonedaG)
    '    'Sello = en.GeneraSello(Cadena, My.Settings.rutacer, Format(CDate(V.Fecha), "yyyy"))
    '    'Dim Enc As New System.Text.UTF8Encoding
    '    'Dim Bytes() As Byte = Enc.GetBytes(V.CreaXML(idNota, IdMonedaG, Sello))
    '    'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
    '    'IO.Directory.CreateDirectory(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    'IO.Directory.CreateDirectory(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    'IO.Directory.CreateDirectory(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    'IO.Directory.CreateDirectory(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    'en.GuardaArchivo(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNC-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
    '    Dim RutaPDF As String
    '    Dim Archivos As New dbSucursalesArchivos
    '    RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, False)

    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
    '    RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")

    '    PrintDocument1.DocumentName = "PDFNCC-" + V.Folio
    '    Dim Impresora As String
    '    'Dim TipoImpresora
    '    Impresora = Archivos.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.CompraNotadeCredito)
    '    'TipoImpresora = Archivos.TipoImpresora
    '    If Impresora = "Bullzip PDF Printer" Then


    '        Dim obj As New Bullzip.PdfWriter.PdfSettings
    '        obj.Init()
    '        obj.PrinterName = My.Settings.impresoraPDF
    '        obj.WriteSettings()
    '        obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
    '        obj.SetValue("ShowSettings", "never")
    '        obj.SetValue("ShowPDF", "yes")
    '        obj.SetValue("ShowSaveAS", "nofile")
    '        obj.SetValue("ConfirmOverwrite", "no")
    '        obj.SetValue("Target", "printer")
    '        obj.WriteSettings()
    '    End If
    '    PrintDocument1.PrinterSettings.PrinterName = Impresora
    '    PrintDocument1.Print()
    '    'Bullzip.PdfWriter.PdfUtil.WaitForFile(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PDFNC" + V.Serie + V.Folio.ToString + ".pdf", 1000)
    '    'If V.Cliente.Email <> "" Then
    '    '    Try
    '    '        If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, NombreApp) = MsgBoxResult.Yes Then
    '    '            If V.Cliente.Email <> "" Then
    '    '                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto)
    '    '                Dim O As New dbOpciones(MySqlcon)
    '    '                Dim C As String
    '    '                C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
    '    '                M.send("Comprobante fiscal digital Nota de crédito " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PDFNC" + V.Serie + V.Folio.ToString + ".pdf", My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNC-" + V.Serie + V.Folio.ToString + ".xml")
    '    '            End If
    '    '        End If
    '    '    Catch ex As Exception
    '    '        MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, NombreApp)
    '    '    End Try
    '    'End If
    'End Sub
    Private Sub CadenaOriginal()

        Dim en As New Encriptador
        Dim V As New dbNotasdeCreditoCompras(idNota, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        Dim CadenaB As String
        Dim CL As New CLetras
        Dim Tabla As New DataTable
        Tabla.Columns.Add("Descripcion")
        Tabla.Columns.Add("Cantidad")
        Tabla.Columns.Add("PU")
        Tabla.Columns.Add("Total")
        ' Tabla.Columns.Add("Codigo")
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasdeCreditoComprasDetalles(MySqlcon)
        DR = VI.ConsultaReader(idNota)
        While DR.Read
            Dim dr2 As DataRow

            dr2 = Tabla.NewRow()
            dr2("Descripcion") = DR("descripcion")
            dr2("Cantidad") = Format(DR("cantidad"), "#,##0.00").PadLeft(8)
            dr2("PU") = Format((DR("precio") / DR("cantidad")), "$#,###,##0.00").PadLeft(13)
            dr2("Total") = Format(DR("precio"), "$#,###,##0.00").PadLeft(13)
            ' dr2("Codigo") = Format(DR("clave"))
            Tabla.Rows.Add(dr2)


        End While
        DR.Close()

        PrintDocument1.DocumentName = "NotasCredito - " + V.Folio.ToString() + " - " + V.Proveedor.Nombre.ToString()  'Modificado ESTO SE NECESITA PONER AFUERA
        PrintDocument1.PrinterSettings.PrinterName = Rep.Imprimir("NotasCredito", "NotasCredito - " + V.Folio.ToString() + " - " + V.Proveedor.Nombre.ToString(), Date.Today.ToString("dd-MM-yyyy"))

        V.DaTotal(idNota, V.IdMoneda)
        If V.TotalaPagar >= 0 Then
            CadenaB = CL.LetrasM(V.TotalaPagar, V.IdMoneda, GlobalIdiomaLetras)
        Else
            CadenaB = "MENOS " + CL.LetrasM(V.TotalaPagar * -1, V.IdMoneda, GlobalIdiomaLetras)
        End If
        Rep.Posicion = 0
        Rep.LlenaNodosImpresionNotasCredito(Format(V.Folio, "0000"), Replace(V.Fecha, "/", "-") + " " + V.Hora, Format(V.Subtotal, "$#,###,##0.00"), Format(V.TotalIva, "$#,###,##0.00"), Format(V.TotalaPagar, "$#,###,##0.00"), "00", CadenaB, " ", Sucursal.NombreFiscal, Sucursal.Direccion + " " + Sucursal.NoExterior, Sucursal.Colonia + ", " + Sucursal.Ciudad, Sucursal.CP, Sucursal.RFC, Sucursal.Telefono, Sucursal.Telefono, Sucursal.Email, V.Proveedor.Nombre, V.Proveedor.Direccion, V.Proveedor.Colonia, V.Proveedor.CP, V.Proveedor.Ciudad, V.Proveedor.RFC, Tabla, idNota, V.Proveedor.NoInterior, V.Proveedor.NoExterior, V.Proveedor.Estado, V.Proveedor.Pais, V.Comentario)
        PrintDocument1.Print()



    End Sub
    Private Sub DGDetalles_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellLeave

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        IvaDefault = S.Impuesto
    End Sub

    Private Sub DGDetalles_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DGDetalles.DataError
        PopUp("Debe indicar una cantidad numérica.", 90)
    End Sub

    Private Sub cmbConcepto_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdatoscliente.KeyDown, txtcliente.KeyDown, TextBox6.KeyDown, TextBox5.KeyDown, TextBox4.KeyDown, TextBox3.KeyDown, TextBox2.KeyDown, TextBox1.KeyDown, dtpFechaPago.KeyDown, dtpFecha2.KeyDown, dtpFecha1.KeyDown, ComboBox3.KeyDown, ComboBox1.KeyDown, cmbConcepto.KeyDown, CheckBox3.KeyDown, CheckBox2.KeyDown, CheckBox1.KeyDown, Button5.KeyDown, Button4.KeyDown, Button2.KeyDown, Button1.KeyDown, btnBuscarCliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim f As New frmNotasdeCreditoComprasConsulta(ModosDeBusqueda.Principal, "")
        f.MdiParent = Me.MdiParent
        f.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(OpenFileDialog1.FileName)
                TextBox7.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            End Try
        End If
    End Sub
    Private Sub GeneraPoliza()
        Try
            Dim Op As New dbOpciones(MySqlcon)
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbNotasdeCreditoCompras(idNota, MySqlcon)
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
                cuantas = M.CuantasHay(7, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(7, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 7)
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
                    GP.GeneraPolizaGeneral(V.ID, V.IdProveedor, 1, 0, 0, 0, 0)
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
End Class