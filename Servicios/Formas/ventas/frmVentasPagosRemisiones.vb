Public Class frmVentasPagosRemisiones

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
    Dim strClave As String = ""
    Dim Tipo As Byte

    ' Dim Tabla As New DataTable
    Public Sub New(ByVal pFolio As String, ByVal pTipo As Byte, ByVal pidCliente As Integer, pClave As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        strFolio = pFolio
        IdCliente = pidCliente
        Tipo = pTipo
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
            If IdCliente <> 0 Then
                Dim C As New dbClientes(IdCliente, MySqlcon)
                txtcliente.Text = C.Clave
            Else
                If strClave <> "" Then
                    CheckBox2.Checked = True
                    txtcliente.Text = strClave
                End If
            End If
            
            If Tipo = 1 Then
                Button3.Visible = False
                RadioButton1.Visible = False
                RadioButton2.Visible = True
                Me.Text = "Apartados Abonos"
            End If

        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirCambioFechaPagosRemisiones, PermisosN.Secciones.Ventas) = False Then
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
            If IsNumeric(TextBox4.Text) = False Then
                TextBox4.Text = "1"
            Else
                If CDbl(TextBox4.Text) = 0 Then
                    TextBox4.Text = "1"
                End If
            End If
            If consultaOn Then
                If Tipo = 0 Then
                    Dim PrimerCeldaRow As Integer = -1
                    If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
                    Dim V As New dbVentasRemisiones(MySqlcon)
                    DGDetalles.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, TextBox3.Text, 0, CheckBox1.Checked, CheckBox2.Checked, 0, CheckBox3.Checked, CDbl(TextBox4.Text), CheckBox4.Checked, IdsSucursales.Valor(ComboBox2.SelectedIndex))
                    If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                    If ClearSelection Then DGDetalles.ClearSelection()
                Else
                    Dim PrimerCeldaRow As Integer = -1
                    If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
                    Dim V As New dbVentasApartados(MySqlcon)
                    DGDetalles.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, TextBox3.Text, 0, CheckBox1.Checked, CheckBox2.Checked, 0, CheckBox3.Checked, CDbl(TextBox4.Text), CheckBox4.Checked, IdsSucursales.Valor(ComboBox2.SelectedIndex))
                    If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                    If ClearSelection Then DGDetalles.ClearSelection()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ConsultaDeudas2()
        Try
            If IsNumeric(TextBox4.Text) = False Then
                TextBox4.Text = "1"
            Else
                If CDbl(TextBox4.Text) = 0 Then
                    TextBox4.Text = "1"
                End If
            End If
            If consultaOn Then
                Dim V As New dbVentasRemisiones(MySqlcon)
                If RadioButton1.Checked Then
                    DGDetalles2.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, "", 0, False, False, 0, False, CDbl(TextBox4.Text), True, IdsSucursales.Valor(ComboBox2.SelectedIndex))
                Else
                    DGDetalles2.DataSource = V.ConsultaDeudas(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), IdCliente, "", 0, False, False, 1, False, CDbl(TextBox4.Text), True, IdsSucursales.Valor(ComboBox2.SelectedIndex))
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
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If e.RowIndex > -1 Then
            IdVenta = DGDetalles.Item("id", e.RowIndex).Value

            If DGDetalles.Item("Sel", e.RowIndex).Value = 1 Then
                DGDetalles.Item("Sel", e.RowIndex).Value = 0
            Else
                DGDetalles.Item("Sel", e.RowIndex).Value = 1
            End If
            'Dim V As New dbVentas(IdVenta, MySqlcon)
            Dim R As Integer = 0
            Dim T As Double = 0
            While R < DGDetalles.RowCount
                If DGDetalles.Item("Sel", R).Value = 1 Then
                    T += Math.Round(DGDetalles.Item("Restante", R).Value, 2)
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
            If Tipo = 0 Then
                Dim VP As New dbVentasPagosRemisiones(MySqlcon)
                DataGridView1.DataSource = VP.Consulta(IdVenta)
                DataGridView1.Columns(0).Visible = False
                'DataGridView1.Columns(4).Visible = False
                'DataGridView1.Columns(5).Visible = False
                DataGridView1.Columns(1).HeaderText = "Fecha"
                DataGridView1.Columns(2).HeaderText = "Cantidad"
                DataGridView1.Columns(3).HeaderText = "Descripción"
                DataGridView1.Columns(4).HeaderText = "Concepto"
                DataGridView1.Columns(4).Width = 150
                DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            Else
                Dim VP As New dbVentasApartadosPagos(MySqlcon)
                DataGridView1.DataSource = VP.Consulta(IdVenta)
                DataGridView1.Columns(0).Visible = False
                'DataGridView1.Columns(4).Visible = False
                'DataGridView1.Columns(5).Visible = False
                DataGridView1.Columns(1).HeaderText = "Fecha"
                DataGridView1.Columns(2).HeaderText = "Cantidad"
                DataGridView1.Columns(3).HeaderText = "Descripción"
                DataGridView1.Columns(4).HeaderText = "Concepto"
                DataGridView1.Columns(4).Width = 150
                DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            DataGridView1.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Tipo = 0 Then
                PagoRemision()
            Else
                PagoApartado()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub PagoRemision()
        Dim VP As New dbVentasPagosRemisiones(MySqlcon)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemAlta, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
        Else
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosCambios, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
        End If
        If HayErrores = False Then
            If Button1.Text = "Guardar" Then
                Dim TotalaAbonar As Double
                Dim TotalaAbonar2 As Double
                Dim R As Integer = 0
                Dim T As Double = 0
                Dim IdsPagos As Integer() = {0}
                Dim ConP As Integer = 0
                Dim Pagos As Integer = 0
                Dim AbonaTotal As Boolean = False
                Dim HuboAbonos As Boolean = False
                TotalaAbonar = CDbl(TextBox1.Text)
                TotalaAbonar2 = TotalaAbonar
                Dim wherestr As String = ""
                While R < DGDetalles.RowCount
                    If DGDetalles.Item("Sel", R).Value = 1 Then
                        Pagos += 1
                        If TotalaAbonar > 0 Then
                            T = Math.Round(DGDetalles.Item("Restante", R).Value, 2)
                            If Math.Round(TotalaAbonar, 2) >= T Then
                                AbonaTotal = True
                            End If
                            If T >= TotalaAbonar And AbonaTotal = False Then
                                VP.Guardar(DGDetalles.Item("id", R).Value, Math.Round(TotalaAbonar, 2), Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox2.Text, 0)
                                wherestr += " or idpago=" + VP.ID.ToString
                                ConP += 1
                                ReDim Preserve IdsPagos(ConP)
                                IdsPagos(ConP - 1) = VP.ID
                                HuboAbonos = True
                            Else
                                VP.Guardar(DGDetalles.Item("id", R).Value, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox2.Text, 0)
                                HuboAbonos = True
                                wherestr += " or idpago=" + VP.ID.ToString
                                ConP += 1
                                ReDim Preserve IdsPagos(ConP)
                                IdsPagos(ConP - 1) = VP.ID
                                AbonaTotal = False
                            End If
                            TotalaAbonar -= T
                        End If
                    End If
                    R += 1

                End While
                If Pagos > 0 Then
                    'PopUp("Pago Registrado", 90)
                    Dim op As New dbOpciones(MySqlcon)
                    If op.FacturarPagosRemisiones = 1 Then
                        If MsgBox("¿Facturar estos pagos?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            Try
                                Dim V As New frmVentasN(0, 2, CDbl(TextBox1.Text), IdCliente)
                                V.idPagos = IdsPagos
                                V.ShowDialog()
                                V.Dispose()
                            Catch ex As Exception
                                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                            End Try
                        End If
                    End If
                    ConsultaAbonos()
                    ConsultaDeudas(False)
                    NuevoPago()
                    TextBox1.Focus()
                    ImprimeRecibo(wherestr, TotalaAbonar2)
                Else
                    MsgBox("Debe seleccionar al menos un documento.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            Else
                VP.Modificar(IdPago, CDbl(TextBox1.Text), Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, TextBox2.Text)
                PopUp("Pago Modificado", 90)
                ConsultaAbonos()
                ConsultaDeudas(False)
                NuevoPago()
                TextBox1.Focus()
            End If
        End If
    End Sub
    Private Sub PagoApartado()
        Dim VP As New dbVentasApartadosPagos(MySqlcon)
        Dim HayErrores As Boolean = False
        If IsNumeric(TextBox1.Text) = False Then
            HayErrores = True
        Else
            If CDbl(TextBox1.Text) <= 0 Then
                HayErrores = True
            End If
        End If
        If Button1.Text = "Guardar" Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemAlta, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
        Else
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosCambios, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
        End If
        If HayErrores = False Then
            If Button1.Text = "Guardar" Then
                Dim TotalaAbonar As Double
                Dim TotalAAbonar2 As Double
                Dim R As Integer = 0
                Dim T As Double = 0
                Dim Pagos As Integer = 0
                Dim AbonaTotal As Boolean = False
                Dim HuboAbonos As Boolean = False
                TotalaAbonar = CDbl(TextBox1.Text)
                TotalAAbonar2 = TotalaAbonar
                Dim wherestr As String = ""
                While R < DGDetalles.RowCount
                    If DGDetalles.Item("Sel", R).Value = 1 Then
                        Pagos += 1
                        If TotalaAbonar > 0 Then
                            T = Math.Round(DGDetalles.Item("Restante", R).Value, 2)
                            If Math.Round(TotalaAbonar, 2) >= T Then
                                AbonaTotal = True
                            End If
                            If T >= TotalaAbonar And AbonaTotal = False Then
                                VP.Guardar(DGDetalles.Item("id", R).Value, TotalaAbonar, Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox2.Text)
                                
                                HuboAbonos = True

                            Else
                                VP.Guardar(DGDetalles.Item("id", R).Value, DGDetalles.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox2.Text)
                                
                                HuboAbonos = True
                                AbonaTotal = False
                            End If
                            TotalaAbonar -= T
                        End If
                    End If
                    R += 1
                    wherestr += " or idpago=" + VP.ID.ToString
                End While

                'If idNota <> 0 And HuboAbonos Then
                '    Dim NC As New dbNotasDeCredito(MySqlcon)
                '    NC.Aplicar(idNota, CantidadNota - TotalaAbonar, True)
                'End If
                If Pagos > 0 Then
                    'PopUp("Pago Registrado", 90)
                    ConsultaAbonos()
                    ConsultaDeudas(False)
                    NuevoPago()
                    TextBox1.Focus()
                    ImprimeRecibo(wherestr, TotalAAbonar2)
                Else
                    MsgBox("Debe seleccionar al menos un documento.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            Else
                VP.Modificar(IdPago, CDbl(TextBox1.Text), Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, TextBox2.Text)
                PopUp("Pago Modificado", 90)
                ConsultaAbonos()
                ConsultaDeudas(False)
                NuevoPago()
                TextBox1.Focus()
            End If
        End If
    End Sub
    Private Sub NuevoPago()
        idNota = 0
        dtpFechaPago.Value = Now.Date
        TextBox1.Text = "0"
        TextBox2.Text = ""
        Button1.Text = "Guardar"
        Button2.Enabled = False
        Button3.Enabled = True
    End Sub
    Private Sub LlenaDatosPago()
        Try
            If Tipo = 0 Then
                Dim VP As New dbVentasPagosRemisiones(IdPago, MySqlcon)
                dtpFechaPago.Value = VP.Fecha
                TextBox1.Text = VP.Cantidad.ToString
                TextBox2.Text = VP.Tipo
                ComboBox1.SelectedIndex = IdsMonedas.Busca(VP.idMoneda)
                Button1.Text = "Modificar"
                Button2.Enabled = True
                Button3.Enabled = False
                cmbConcepto.SelectedIndex = IdsConceptos.Busca(VP.Idconceptonotaventa)
            Else
                Dim VP As New dbVentasApartadosPagos(IdPago, MySqlcon)
                dtpFechaPago.Value = VP.Fecha
                TextBox1.Text = VP.Cantidad.ToString
                TextBox2.Text = VP.Tipo
                ComboBox1.SelectedIndex = IdsMonedas.Busca(VP.idMoneda)
                Button1.Text = "Modificar"
                Button2.Enabled = True
                Button3.Enabled = False
                cmbConcepto.SelectedIndex = IdsConceptos.Busca(VP.Idconceptonotaventa)
            End If
            
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        IdPago = DataGridView1.Item(0, e.RowIndex).Value
        LlenaDatosPago()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemCancelar, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If MsgBox("¿Cancelar pago?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If Tipo = 0 Then
                    Dim vp As New dbVentasPagosRemisiones(MySqlcon)
                    vp.CancelarPago(IdPago, Estados.Cancelada, IdCliente)
                Else
                    Dim vp As New dbVentasApartadosPagos(MySqlcon)
                    vp.CancelarPago(IdPago, Estados.Cancelada, IdCliente)
                End If
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
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If CDbl(DGDetalles.Item(10, e.RowIndex).Value) <= 0 Then
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

    'Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
    '    If DataGridView1.Item(5, e.RowIndex).Value = 1 Then
    '        Dim F As New frmNotasdeCredito(DataGridView1.Item(4, e.RowIndex).Value)
    '        F.ShowDialog()
    '    End If
    '    If DataGridView1.Item(5, e.RowIndex).Value = 2 Then
    '        Dim Fd As New frmDevoluciones(DataGridView1.Item(4, e.RowIndex).Value, 0, 0, "", 0)
    '        Fd.ShowDialog()
    '    End If
    'End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 2 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PagosRemAlta, PermisosN.Secciones.Ventas) = False Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            Dim VP As New dbVentasPagosRemisiones(MySqlcon)
            ConsultaDeudas2()
            Dim TotalaAbonar As Double
            Dim TotalaAbonar2 As Double
            Dim R As Integer = 0
            Dim T As Double = 0
            Dim wherestr As String = ""
            Dim HuboAbonos As Boolean = False
            Dim Abonatotal As Boolean = False
            TotalaAbonar = CDbl(TextBox1.Text)
            TotalaAbonar2 = TotalaAbonar
            While R < DGDetalles2.RowCount
                If DGDetalles2.Item(10, R).Value > 0 Then
                    If TotalaAbonar > 0 Then
                        T = DGDetalles2.Item(10, R).Value
                        If Math.Round(TotalaAbonar, 2) >= Math.Round(T, 2) Then
                            Abonatotal = True
                        End If
                        If T >= TotalaAbonar And Abonatotal = False Then
                            VP.Guardar(DGDetalles2.Item("id2", R).Value, Math.Round(TotalaAbonar, 2), Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox2.Text, 0)
                            'If DGDetalles2.Item(11, R).Value = 1 Then VP.Guardar(0, TotalaAbonar, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, DGDetalles2.Item(0, R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                            'If DGDetalles2.Item(11, R).Value = 2 Or DGDetalles.Item(10, R).Value = 3 Then VP.Guardar(0, TotalaAbonar, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles2.Item(0, R).Value, DGDetalles2.Item(11, R).Value, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                            HuboAbonos = True
                            wherestr += " or idpago=" + VP.ID.ToString
                        Else
                            VP.Guardar(DGDetalles2.Item("id2", R).Value, DGDetalles2.Item("restante2", R).Value, Format(dtpFechaPago.Value, "yyyy/MM/dd"), IdCliente, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox2.Text, 0)
                            'If DGDetalles2.Item(11, R).Value = 1 Then VP.Guardar(0, T, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, DGDetalles2.Item(0, R).Value, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), 0, 1, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                            'If DGDetalles2.Item(11, R).Value = 2 Or DGDetalles2.Item(10, R).Value = 3 Then VP.Guardar(0, T, Format(dtpFechaPago.Value, "yyyy/MM/dd"), TextBox2.Text, IdCliente, 0, 0, 0, CDbl(TextBox4.Text), IdsMonedas.Valor(ComboBox1.SelectedIndex), DGDetalles2.Item(0, R).Value, DGDetalles2.Item(11, R).Value, IdsConceptos.Valor(cmbConcepto.SelectedIndex))
                            HuboAbonos = True
                            Abonatotal = False
                            wherestr += " or idpago=" + VP.ID.ToString
                        End If
                        TotalaAbonar -= T
                    End If
                End If
                R += 1
            End While
            If HuboAbonos Then
                ImprimeRecibo(wherestr, TotalaAbonar2)
                PopUp("Pagos Registrados", 90)
                ConsultaAbonos()
                ConsultaDeudas(False)
            End If
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

    Private Sub txtdatoscliente_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtdatoscliente.KeyDown, txtcliente.KeyDown, TextBox4.KeyDown, TextBox3.KeyDown, TextBox2.KeyDown, TextBox1.KeyDown, RadioButton2.KeyDown, RadioButton1.KeyDown, dtpFechaPago.KeyDown, dtpFecha2.KeyDown, dtpFecha1.KeyDown, ComboBox1.KeyDown, cmbConcepto.KeyDown, CheckBox4.KeyDown, CheckBox3.KeyDown, CheckBox2.KeyDown, CheckBox1.KeyDown, Button6.KeyDown, Button5.KeyDown, Button3.KeyDown, Button2.KeyDown, Button1.KeyDown, btnBuscarCliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.I And IdPago <> 0 Then
            ImprimeRecibo(" or idpago=" + IdPago.ToString, DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex).Value)
        End If
    End Sub
    Private Sub ImprimeRecibo(ByVal pwhere As String, pTotalRecibo As Double)
        Try
            Dim op As New dbOpciones(MySqlcon)
            Dim DirEmpresa As String
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            DirEmpresa = S.Direccion
            If S.NoExterior <> "" Then DirEmpresa += " " + S.NoExterior
            If S.Colonia <> "" Then DirEmpresa += " " + S.Colonia
            If S.Ciudad <> "" Then DirEmpresa += " " + S.Ciudad
            If S.Estado <> "" Then DirEmpresa += " " + S.Estado
            If S.Telefono <> "" Then DirEmpresa += " " + S.Telefono
            If op.PagosTicket = 0 Then
                If Tipo = 0 Then
                    Dim VP As New dbVentasPagosRemisiones(IdPago, MySqlcon)
                    Dim rep As New repVentaPago
                    rep.SetDataSource(VP.impresion(pwhere))
                    rep.SetParameterValue("tipo", "R")
                    If ComboBox1.SelectedIndex = 0 Then
                        rep.SetParameterValue("moneda", "P")
                    Else
                        rep.SetParameterValue("moneda", "D")
                    End If
                    rep.SetParameterValue("totalrecibo", pTotalRecibo)
                    Dim frm As New frmReportes(rep, False)
                    frm.Show()
                Else
                    Dim VP As New dbVentasApartadosPagos(IdPago, MySqlcon)
                    Dim rep As New repVentaPago
                    rep.SetDataSource(VP.impresion(pwhere))
                    rep.SetParameterValue("tipo", "R")
                    If ComboBox1.SelectedIndex = 0 Then
                        rep.SetParameterValue("moneda", "P")
                    Else
                        rep.SetParameterValue("moneda", "D")
                    End If
                    rep.SetParameterValue("totalrecibo", pTotalRecibo)
                    Dim frm As New frmReportes(rep, False)
                    frm.Show()
                End If
            Else
                If Tipo = 0 Then
                    Dim VP As New dbVentasPagosRemisiones(IdPago, MySqlcon)
                    Dim rep As New repVentaPagoTikect
                    rep.SetDataSource(VP.impresion(pwhere))
                    rep.SetParameterValue("tipo", "R")
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
                    Dim VP As New dbVentasApartadosPagos(IdPago, MySqlcon)
                    Dim rep As New repVentaPagoTikect
                    rep.SetDataSource(VP.impresion(pwhere))
                    rep.SetParameterValue("tipo", "R")
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
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        ConsultaDeudas(True)
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        ConsultaDeudas(True)
    End Sub
End Class