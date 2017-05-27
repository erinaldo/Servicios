Public Class frmVentasPagos
    Dim consultaOn As Boolean
    Dim IdCliente As Integer
    Dim IdVenta As Integer
    Dim IdCargo As Integer
    Dim IdPago As Integer
    Dim strFolio As String
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim idNota As Integer
    Dim CantidadNota As Double
    Dim IdsMonedas As New elemento
    Dim IdDocumento As Integer
    Dim IdsConceptos As New elemento
    Dim IdsSucursales As New elemento
    Dim FacturasAfectadas As String
    Dim ConceptoParaBancos As String
    Dim NombreProveedor As String = ""
    Dim op As dbOpciones
    Dim CantidadPago As Double = 0
    Dim whereStrG As String
    Dim strClave As String = ""
    ' Dim Tabla As New DataTable
    Public Sub New(ByVal pFolio As String, pClave As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        strFolio = pFolio
        strClave = pClave
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasPagos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
                Button4.Enabled = False
            Next
        Else
            IdCliente = 0
            consultaOn = False
            dtpFecha1.Value = Date.Now
            dtpFecha2.Value = Date.Now
            LlenaCombos("tblsucursales", ComboBox2, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
            LlenaCombos("tblconceptosnotasventas", cmbConcepto, "nombre", "nombret", "idconceptonotaventa", IdsConceptos, " tipo=2")
            consultaOn = True
            If GlobaltpBanxico <> "Error" Then
                TextBox4.Text = GlobaltpBanxico
            Else
                Dim CM As New dbMonedasConversiones(1, MySqlcon)
                TextBox4.Text = CM.Cantidad.ToString
            End If
            LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1")
            If strFolio <> "" Then
                TextBox3.Text = strFolio
            End If
            If strClave <> "" Then
                CheckBox2.Checked = True
                txtcliente.Text = strClave
            End If
            op = New dbOpciones(MySqlcon)
            If op.IntegrarBancosVentasPagos = 0 Then
                CheckBox5.Checked = False
            End If
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirCambioFechaPagos, PermisosN.Secciones.Ventas) = False Then
            dtpFechaPago.Enabled = False
        End If
    End Sub



    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtdatoscliente.Text = B.Cliente.Nombre + " | " + B.Cliente.RFC
            IdCliente = B.Cliente.ID
            ConsultaOn = False
            txtcliente.Text = B.Cliente.Clave
            consultaOn = True
            ConsultaDeudas(True)
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub ConsultaDeudas(ByVal ClearSelection As Boolean)
        Try
            If consultaOn Then
                If IsNumeric(TextBox4.Text) = False Then
                    TextBox4.Text = "1"
                Else
                    If CDbl(TextBox4.Text) = 0 Then
                        TextBox4.Text = "1"
                    End If
                End If
                Dim PrimerCeldaRow As Integer = -1
                If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
                Dim V As New dbVentas(MySqlcon)
                DGDetalles.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, TextBox3.Text, 0, CheckBox1.Checked, CheckBox2.Checked, 0, CheckBox3.Checked, CDbl(TextBox4.Text), CheckBox4.Checked, IdsSucursales.Valor(ComboBox2.SelectedIndex), False)
                If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                If ClearSelection Then DGDetalles.ClearSelection()

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ConsultaDeudas2()
        Try
            If consultaOn Then
                If IsNumeric(TextBox4.Text) = False Then
                    TextBox4.Text = "1"
                Else
                    If CDbl(TextBox4.Text) = 0 Then
                        TextBox4.Text = "1"
                    End If
                End If
                Dim V As New dbVentas(MySqlcon)
                If RadioButton1.Checked Then
                    DGDetalles2.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, "", 0, False, False, 0, False, CDbl(TextBox4.Text), True, IdsSucursales.Valor(ComboBox2.SelectedIndex), False)
                Else
                    DGDetalles2.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, "", 0, False, False, 1, False, CDbl(TextBox4.Text), True, IdsSucursales.Valor(ComboBox2.SelectedIndex), False)
                End If
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
                    ConsultaDeudas(True)
                    DataGridView1.DataSource = Nothing
                Else
                    txtdatoscliente.Text = ""
                    IdCliente = 0
                    ConsultaDeudas(True)
                    DataGridView1.DataSource = Nothing
                    'DGDetalles.Rows.Clear()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If e.RowIndex > -1 Then
            Select Case DGDetalles.Item(11, e.RowIndex).Value
                Case 0
                    IdVenta = DGDetalles.Item(0, e.RowIndex).Value
                    IdCargo = 0
                    IdDocumento = 0
                Case 1
                    IdCargo = DGDetalles.Item(0, e.RowIndex).Value
                    IdVenta = 0
                    IdDocumento = 0
                Case 2
                    IdCargo = 0
                    IdVenta = 0
                    IdDocumento = DGDetalles.Item(0, e.RowIndex).Value
                Case 3
                    IdCargo = 0
                    IdVenta = 0
                    IdDocumento = DGDetalles.Item(0, e.RowIndex).Value
                Case 4
                    IdVenta = DGDetalles.Item(0, e.RowIndex).Value
                    IdCargo = 0
                    IdDocumento = 0
            End Select
           

            If DGDetalles.Item(1, e.RowIndex).Value = 1 Then
                DGDetalles.Item(1, e.RowIndex).Value = 0
            Else
                DGDetalles.Item(1, e.RowIndex).Value = 1
            End If
            'Dim V As New dbVentas(IdVenta, MySqlcon)
            Dim R As Integer = 0
            Dim T As Double = 0
            While R < DGDetalles.RowCount
                If DGDetalles.Item(1, R).Value = 1 Then
                    T += Math.Round(DGDetalles.Item(10, R).Value, 2)
                End If
                R += 1
            End While
            NuevoPago()
            ConsultaAbonos()
            TextBox1.Text = Format(T, "#0.00")
            TextBox1.Focus()

        End If
    End Sub

    Private Sub ConsultaAbonos()
        Try
            If IdVenta <> 0 Or IdCargo <> 0 Or IdDocumento <> 0 Then
                Dim VP As New dbVentasPagos(MySqlcon)
                DataGridView1.DataSource = VP.Consulta(IdVenta, IdCargo, IdDocumento)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(4).Visible = False
                DataGridView1.Columns(5).Visible = False
                DataGridView1.Columns(1).HeaderText = "Fecha"
                DataGridView1.Columns(2).HeaderText = "Cantidad"
                DataGridView1.Columns(3).HeaderText = "Descripción"
                DataGridView1.Columns(6).HeaderText = "Concepto"
                DataGridView1.Columns(6).Width = 150
                DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DataGridView1.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        Try
            Dim VP As New dbVentasPagos(MySqlcon)
            Dim HayErrores As Boolean = False
            If IsNumeric(TextBox1.Text) = False Then
                HayErrores = True
            Else
                If CDbl(TextBox1.Text) <= 0 Then
                    HayErrores = True
                End If
            End If
            If IsNumeric(TextBox4.Text) = False Then
                TextBox4.Text = "1"
            Else
                If CDbl(TextBox4.Text) = 0 Then
                    TextBox4.Text = "1"
                End If
            End If
            If Button1.Text = "Guardar" Then
                If FacturasAfectadas = "" Then FacturasAfectadas = "Facturas: "
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosAlta, PermisosN.Secciones.Ventas) = False Then
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                    HayErrores = True
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosCambios, PermisosN.Secciones.Ventas) = False Then
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                    HayErrores = True
                End If
            End If
            If HayErrores = False Then
                If Button1.Text = "Guardar" Then
                    Dim TotalaAbonar As Double
                    Dim TotalaAbonar2 As Double
                    Dim R As Integer = 0
                    Dim T As Double = 0
                    Dim Pagos As Integer = 0
                    Dim AbonaTotal As Boolean = False
                    Dim HuboAbonos As Boolean = False
                    TotalaAbonar = CDbl(TextBox1.Text)
                    TotalaAbonar2 = TotalaAbonar
                    Dim wherestr As String = ""
                    While R < DGDetalles.RowCount
                        If DGDetalles.Item(1, R).Value = 1 Then
                            Pagos += 1
                            If TotalaAbonar > 0 Then
                                T = Math.Round(DGDetalles.Item(10, R).Value, 2)
                                If Math.Round(TotalaAbonar, 2) >= T Then
                                    AbonaTotal = True
                                End If
                                If T >= TotalaAbonar And AbonaTotal = False Then
                                    If DGDetalles.Item(11, R).Value = 0 Then
                                        FacturasAfectadas += Trim(DGDetalles.Item(6, R).Value + DGDetalles.Item(7, R).Value.ToString) + " | "
                                        VP.Guardar(DGDetalles.Item(0, R).Value, TotalaAbonar, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                    End If
                                    If DGDetalles.Item(11, R).Value = 1 Then
                                        FacturasAfectadas += Trim(DGDetalles.Item(6, R).Value + DGDetalles.Item(7, R).Value.ToString) + " | "
                                        VP.Guardar(0, TotalaAbonar, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, DGDetalles.Item(0, R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                    End If
                                    If DGDetalles.Item(11, R).Value = 2 Or DGDetalles.Item(11, R).Value = 3 Then
                                        FacturasAfectadas += Trim(DGDetalles.Item(6, R).Value + DGDetalles.Item(7, R).Value.ToString) + " | "
                                        VP.Guardar(0, TotalaAbonar, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item(0, R).Value, DGDetalles.Item(11, R).Value, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                    End If

                                    If DGDetalles.Item(11, R).Value = 4 Then
                                        VP.Guardar(DGDetalles.Item(0, R).Value, TotalaAbonar, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                        Dim V As New frmVentasN(DGDetalles.Item(0, R).Value, 1, TotalaAbonar, IdCliente)
                                        V.ShowDialog()
                                        V.Dispose()
                                    End If
                                    HuboAbonos = True

                                Else
                                    If DGDetalles.Item(11, R).Value = 0 Then
                                        FacturasAfectadas += Trim(DGDetalles.Item(6, R).Value + DGDetalles.Item(7, R).Value.ToString) + " | "
                                        VP.Guardar(DGDetalles.Item(0, R).Value, DGDetalles.Item(12, R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                    End If

                                    If DGDetalles.Item(11, R).Value = 1 Then
                                        FacturasAfectadas += Trim(DGDetalles.Item(6, R).Value + DGDetalles.Item(7, R).Value.ToString) + " | "
                                        VP.Guardar(0, DGDetalles.Item(12, R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, DGDetalles.Item(0, R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                    End If
                                    If DGDetalles.Item(11, R).Value = 2 Or DGDetalles.Item(11, R).Value = 3 Then
                                        VP.Guardar(0, DGDetalles.Item(12, R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles.Item(0, R).Value, DGDetalles.Item(11, R).Value, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                        FacturasAfectadas += Trim(DGDetalles.Item(6, R).Value + DGDetalles.Item(7, R).Value.ToString) + " | "
                                    End If

                                    If DGDetalles.Item(11, R).Value = 4 Then
                                        VP.Guardar(DGDetalles.Item(0, R).Value, DGDetalles.Item(12, R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                        Dim V As New frmVentasN(DGDetalles.Item(0, R).Value, 1, DGDetalles.Item(12, R).Value, IdCliente)
                                        V.ShowDialog()
                                        V.Dispose()
                                    End If
                                    HuboAbonos = True
                                    AbonaTotal = False
                                End If
                                TotalaAbonar -= T
                            End If
                            wherestr += " or idpago=" + VP.ID.ToString
                        End If
                        R += 1
                    End While

                    'If idNota <> 0 And HuboAbonos Then
                    '    Dim NC As New dbNotasDeCredito(MySqlcon)
                    '    NC.Aplicar(idNota, CantidadNota - TotalaAbonar, True)
                    'End If
                    whereStrG += wherestr
                    If Pagos > 0 Then

                        'liga a bancos aqui
                        If op.IntegrarBancosVentasPagos = 1 And FacturasAfectadas <> "Facturas: " And GlobalConBancos Then
                            CantidadPago += CDbl(TextBox1.Text)
                            ConceptoParaBancos = TextBox2.Text
                            If CheckBox5.Checked = True Then
                                If MsgBox("¿Aplicar a bancos?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                    FacturasAfectadas = FacturasAfectadas.Substring(0, FacturasAfectadas.Length - 3)
                                    Dim PP As New frmDeposito(op.IntegrarBancosVentasPagos, FacturasAfectadas, ConceptoParaBancos, NombreProveedor, CantidadPago, IdCliente, whereStrG, Format(dtpFechaPago.Value, "dd/MM/yyyy"), 1)
                                    'TextBox1.Text=Total pagado
                                    If PP.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                                        MsgBox("No se completo el ligado a bancos. Necesita eliminar los pagos generados o realizar manualmente el depósito a bancos.", MsgBoxStyle.Information, GlobalNombreApp)
                                        ConceptoParaBancos = ""
                                        CantidadPago = 0
                                        FacturasAfectadas = ""
                                        whereStrG = ""
                                    End If
                                    PP.Dispose()
                                End If
                            Else
                                FacturasAfectadas = FacturasAfectadas.Substring(0, FacturasAfectadas.Length - 3)
                                Dim PP As New frmDeposito(op.IntegrarBancosVentasPagos, FacturasAfectadas, ConceptoParaBancos, NombreProveedor, CantidadPago, IdCliente, wherestr, Format(dtpFechaPago.Value, "dd/MM/yyyy"), 1)
                                'TextBox1.Text=Total pagado
                                If PP.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                                    MsgBox("No se completo el ligado a bancos. Necesita eliminar los pagos generados o realizar manualmente el depósito a bancos.", MsgBoxStyle.Information, GlobalNombreApp)
                                    ConceptoParaBancos = ""
                                    CantidadPago = 0
                                    FacturasAfectadas = ""
                                    whereStrG = ""
                                End If
                                PP.Dispose()
                            End If
                        End If
                        ' sigue lo normal

                        ConsultaAbonos()
                        ConsultaDeudas(False)
                        NuevoPago()
                        TextBox1.Focus()
                        ImprimeRecibo(wherestr, TotalaAbonar2)
                        'Dim rep As New repVentaPago
                        'rep.SetDataSource(VP.impresion(wherestr))
                        'rep.SetParameterValue("tipo", "V")
                        'If ComboBox1.SelectedIndex = 0 Then
                        '    rep.SetParameterValue("moneda", "P")
                        'Else
                        '    rep.SetParameterValue("moneda", "D")
                        'End If
                        'Dim f As New frmReportes(rep, False)
                        'f.Show()

                    Else
                        MsgBox("Debe seleccionar al menos un documento.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If
                Else
                    If VP.LigadoABancos(IdPago) = False Then
                        VP.Modificar(IdPago, CDbl(TextBox1.Text), Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente)
                        PopUp("Pago Modificado", 90)
                        ConsultaAbonos()
                        ConsultaDeudas(False)
                        NuevoPago()
                        TextBox1.Focus()
                    Else
                        MsgBox("No se puede modificar este movimiento ya que esta ligado a una entrada a bancos.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub NuevoPago()
        idNota = 0
        dtpFechaPago.Value = Now.Date
        TextBox1.Text = "0"
        Button1.Text = "Guardar"
        Button2.Enabled = False
        Button3.Enabled = True
        If CheckBox5.Checked = False Then
            FacturasAfectadas = ""
            ConceptoParaBancos = ""
            CantidadPago = 0
            whereStrG = ""
            TextBox2.Text = ""
        End If
    End Sub
    Private Sub LlenaDatosPago()
        Try
            Dim VP As New dbVentasPagos(IdPago, MySqlcon)
            dtpFechaPago.Value = VP.Fecha
            TextBox1.Text = VP.Cantidad.ToString
            TextBox2.Text = VP.Tipo
            Button1.Text = "Modificar"
            ComboBox1.SelectedIndex = IdsMonedas.Busca(VP.idMoneda)
            Button2.Enabled = True
            Button3.Enabled = False
            cmbConcepto.SelectedIndex = IdsConceptos.Busca(VP.Idconceptonotaventa)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            IdPago = DataGridView1.Item(0, e.RowIndex).Value
            LlenaDatosPago()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosCancelar, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If MsgBox("¿Cancelar pago?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim vp As New dbVentasPagos(MySqlcon)
                vp.CancelarPago(IdPago, Estados.Cancelada, IdCliente)
                NuevoPago()
                ConsultaAbonos()
                ConsultaDeudas(False)
                PopUp("Pago Cancelado", 80)
                TextBox1.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub DGDetalles_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGDetalles.CellFormatting
        
        If e.ColumnIndex = 8 Or e.ColumnIndex = 9 Or e.ColumnIndex = 10 Then
            e.Value = Format(e.Value, "#,###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If Math.Round(CDbl(DGDetalles.Item(10, e.RowIndex).Value), 2) <= 0 Then
            e.CellStyle.BackColor = ColorVerde
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
        If e.RowIndex >= 0 Then
            If DataGridView1.Item(5, e.RowIndex).Value = 1 Then
                Dim F As New frmNotasdeCredito(DataGridView1.Item(4, e.RowIndex).Value)
                F.ShowDialog()
            End If
            If DataGridView1.Item(5, e.RowIndex).Value = 2 Then
                Dim Fd As New frmDevoluciones(DataGridView1.Item(4, e.RowIndex).Value, 0, 0, "", 0)
                Fd.ShowDialog()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 2 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosAlta, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            Dim VP As New dbVentasPagos(MySqlcon)
            ConsultaDeudas2()
            Dim TotalaAbonar As Double
            Dim TotalaAbonar2 As Double
            Dim R As Integer = 0
            Dim T As Double = 0
            Dim Pagos As Integer = 0
            Dim wherestr As String = ""
            Dim HuboAbonos As Boolean = False
            Dim AbonaTotal As Boolean = False
            TotalaAbonar = CDbl(TextBox1.Text)
            TotalaAbonar2 = TotalaAbonar
            If FacturasAfectadas = "" Then FacturasAfectadas = "Facturas: "
            While R < DGDetalles2.RowCount
                If DGDetalles2.Item(10, R).Value > 0 Then
                    If TotalaAbonar > 0 Then
                        T = DGDetalles2.Item(10, R).Value
                        If Math.Round(TotalaAbonar, 2) >= Math.Round(T, 2) Then
                            AbonaTotal = True
                        End If
                        If T >= TotalaAbonar And AbonaTotal = False Then
                            If DGDetalles2.Item(11, R).Value = 0 Then
                                FacturasAfectadas += Trim(DGDetalles2.Item(6, R).Value + DGDetalles2.Item(7, R).Value.ToString) + " | "
                                VP.Guardar(DGDetalles2.Item(0, R).Value, Math.Round(TotalaAbonar, 2), Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                Pagos += 1
                            End If

                            If DGDetalles2.Item(11, R).Value = 1 Then
                                FacturasAfectadas += Trim(DGDetalles2.Item(6, R).Value + DGDetalles2.Item(7, R).Value.ToString) + " | "
                                VP.Guardar(0, Math.Round(TotalaAbonar, 2), Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, DGDetalles2.Item(0, R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                Pagos += 1
                            End If

                            If DGDetalles2.Item(11, R).Value = 2 Or DGDetalles.Item(10, R).Value = 3 Then
                                FacturasAfectadas += Trim(DGDetalles2.Item(6, R).Value + DGDetalles2.Item(7, R).Value.ToString) + " | "
                                VP.Guardar(0, Math.Round(TotalaAbonar, 2), Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles2.Item(0, R).Value, DGDetalles2.Item(11, R).Value, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                Pagos += 1
                            End If

                            HuboAbonos = True
                        Else
                            If DGDetalles2.Item(11, R).Value = 0 Then
                                FacturasAfectadas += Trim(DGDetalles2.Item(6, R).Value + DGDetalles2.Item(7, R).Value.ToString) + " | "
                                VP.Guardar(DGDetalles2.Item(0, R).Value, DGDetalles2.Item(12, R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 0, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                Pagos += 1
                            End If

                            If DGDetalles2.Item(11, R).Value = 1 Then
                                FacturasAfectadas += Trim(DGDetalles2.Item(6, R).Value + DGDetalles2.Item(7, R).Value.ToString) + " | "
                                VP.Guardar(0, DGDetalles2.Item(12, R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, DGDetalles2.Item(0, R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                                Pagos += 1
                            End If

                            If DGDetalles2.Item(11, R).Value = 2 Or DGDetalles2.Item(10, R).Value = 3 Then
                                FacturasAfectadas += Trim(DGDetalles2.Item(6, R).Value + DGDetalles2.Item(7, R).Value.ToString) + " | "
                                Pagos += 1
                                VP.Guardar(0, DGDetalles.Item(12, R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles2.Item(0, R).Value, DGDetalles2.Item(11, R).Value, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                            End If
                            HuboAbonos = True
                            AbonaTotal = False
                        End If
                        TotalaAbonar -= T
                        wherestr += " or idpago=" + VP.ID.ToString
                    End If

                End If
                R += 1
            End While
            whereStrG += wherestr

            If Pagos > 0 Then

                'liga a bancos aqui
                If op.IntegrarBancosVentasPagos = 1 And FacturasAfectadas <> "Facturas: " And GlobalConBancos Then
                    CantidadPago += CDbl(TextBox1.Text)
                    ConceptoParaBancos = TextBox2.Text
                    If CheckBox5.Checked = True Then
                        If MsgBox("¿Aplicar a bancos?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            FacturasAfectadas = FacturasAfectadas.Substring(0, FacturasAfectadas.Length - 3)
                            Dim PP As New frmDeposito(op.IntegrarBancosVentasPagos, FacturasAfectadas, ConceptoParaBancos, NombreProveedor, CantidadPago, IdCliente, whereStrG, Format(dtpFechaPago.Value, "dd/MM/yyyy"), 1)
                            'TextBox1.Text=Total pagado
                            If PP.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                                MsgBox("", MsgBoxStyle.Information, GlobalNombreApp)
                                ConceptoParaBancos = ""
                                CantidadPago = 0
                                FacturasAfectadas = ""
                                whereStrG = ""
                            End If
                        End If
                    Else
                        FacturasAfectadas = FacturasAfectadas.Substring(0, FacturasAfectadas.Length - 3)
                        Dim PP As New frmDeposito(op.IntegrarBancosVentasPagos, FacturasAfectadas, ConceptoParaBancos, NombreProveedor, CantidadPago, IdCliente, wherestr, Format(dtpFechaPago.Value, "dd/MM/yyyy"), 1)
                        'TextBox1.Text=Total pagado
                        If PP.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                            MsgBox("No se completo el ligado a bancos. Necesita eliminar los pagos generados o realizar manualmente el depósito a bancos.", MsgBoxStyle.Information, GlobalNombreApp)
                            ConceptoParaBancos = ""
                            CantidadPago = 0
                            FacturasAfectadas = ""
                            whereStrG = ""
                        End If
                    End If
                End If
                ImprimeRecibo(wherestr, TotalaAbonar2)
            End If
            PopUp("Pagos Registrados", 90)
            ConsultaAbonos()
            ConsultaDeudas(False)
            NuevoPago()
            TextBox1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
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

    'Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
    '    Dim f As New frmNotasDeCreditoConsulta(ModosDeBusqueda.Secundario, txtcliente.Text)
    '    f.ShowDialog()
    '    If f.DialogResult = Windows.Forms.DialogResult.OK Then

    '        idNota = f.IdVenta
    '        Dim NC As New dbNotasDeCredito(idNota, MySqlcon)
    '        CantidadNota = NC.TotalaPagar - NC.Aplicado
    '        TextBox1.Text = Format(CantidadNota, "#0.00")
    '        TextBox2.Text = "Nota de crédito:" + NC.Serie + NC.Folio
    '        'LlenaDatosVenta()
    '        'NuevoConcepto()
    '    End If
    'End Sub

    Private Sub frmVentasPagos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
       
    End Sub

   

    Private Sub ImprimeRecibo(ByVal pwhere As String, pTotalRecibo As Double)
        Dim op As New dbOpciones(MySqlcon)

        Dim VP As New dbVentasPagos(IdPago, MySqlcon)
        If op.PagosTicket = 1 Then
            Dim rep As New repVentaPagoTikect
            Dim DirEmpresa As String
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            DirEmpresa = S.Direccion
            If S.NoExterior <> "" Then DirEmpresa += " " + S.NoExterior
            If S.Colonia <> "" Then DirEmpresa += " " + S.Colonia
            If S.Ciudad <> "" Then DirEmpresa += " " + S.Ciudad
            If S.Estado <> "" Then DirEmpresa += " " + S.Estado
            If S.CP <> "" Then DirEmpresa += " " + S.CP
            If S.Telefono <> "" Then DirEmpresa += " " + S.Telefono
            rep.SetDataSource(VP.impresion(pwhere))
            rep.SetParameterValue("tipo", "V")

            If ComboBox1.SelectedIndex = 0 Then
                rep.SetParameterValue("moneda", "P")
            Else
                rep.SetParameterValue("moneda", "D")
            End If
            rep.SetParameterValue("EmpresaNombre", S.Nombre)
            rep.SetParameterValue("DireccionEmpresa", DirEmpresa)
            rep.SetParameterValue("RFCempresa", S.RFC)
            rep.SetParameterValue("totalrecibo", pTotalRecibo)
            Dim frm As New frmReportes(rep, False)
            frm.Show()
        Else
            Dim rep As New repVentaPago
            rep.SetDataSource(VP.impresion(pwhere))
            rep.SetParameterValue("tipo", "V")
            If ComboBox1.SelectedIndex = 0 Then
                rep.SetParameterValue("moneda", "P")
            Else
                rep.SetParameterValue("moneda", "D")
            End If
            rep.SetParameterValue("totalrecibo", pTotalRecibo)
            Dim frm As New frmReportes(rep, False)
            frm.Show()
        End If


        'Dim VP As New dbVentasPagos(IdPago, MySqlcon)
        'Dim rep As New repVentaPago
        ''Dim DirEmpresa As String
        ''Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox2.SelectedIndex), MySqlcon)
        ''DirEmpresa = S.Direccion
        ''If S.NoExterior <> "" Then DirEmpresa += " " + S.NoExterior
        ''If S.Colonia <> "" Then DirEmpresa += " " + S.Colonia
        ''If S.Ciudad <> "" Then DirEmpresa += " " + S.Ciudad
        ''If S.Estado <> "" Then DirEmpresa += " " + S.Estado
        'rep.SetDataSource(VP.impresion(" or idpago=" + IdPago.ToString))
        'rep.SetParameterValue("tipo", "V")
        ''rep.SetParameterValue("NombreEmpresa", S.Nombre)
        ''rep.SetParameterValue("DireccionEmpresa", DirEmpresa)
        ''rep.SetParameterValue("RFCEmpresa", S.RFC)
        'If ComboBox1.SelectedIndex = 0 Then
        '    rep.SetParameterValue("moneda", "P")
        'Else
        '    rep.SetParameterValue("moneda", "D")
        'End If
        'Dim frm As New frmReportes(rep, False)
        'frm.Show()

    End Sub
    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.I And IdPago <> 0 Then
            Try
                ImprimeRecibo(" or idpago=" + IdPago.ToString, DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex).Value)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        ConsultaDeudas(True)
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub dtpFechaPago_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpFechaPago.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub dtpFechaPago_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaPago.ValueChanged

    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        ConsultaDeudas(True)
    End Sub
End Class