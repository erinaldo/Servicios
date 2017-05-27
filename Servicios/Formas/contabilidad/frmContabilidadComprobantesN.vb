Public Class frmContabilidadComprobantesN
    Dim IdsBancosCheque As New elemento
    Dim IdsBancosTransD As New elemento
    Dim IdsBancosTransO As New elemento
    Dim IdsCuentaOTrans As New elemento
    Dim IdsCuentaOChe As New elemento
    Dim IdsMonedaCompNac As New elemento
    Dim IdsMonedaCompNac2 As New elemento
    Dim IdsMonedaCompEx As New elemento
    Dim IdsMonedaCheque As New elemento
    Dim IdsMonedaTra As New elemento
    Dim IdsMonedaOtro As New elemento
    Dim IdsMetodoPago As New elemento
    Dim IDPoliza As Integer
    Dim IDRenglon As Integer
    Dim p As New dbContabilidadPolizas(MySqlcon)
    Dim llenando As Boolean = False
    Dim benef As String
    Dim parFecha As String

    Public Sub New(ByVal pIDPoliza As Integer, ByVal pIDRenglon As Integer, ByVal pBeneficiario As String, pFecha As String, pTipoPoliza As String)
        llenando = True
        InitializeComponent()
        IDPoliza = pIDPoliza
        IDRenglon = pIDRenglon
        benef = pBeneficiario
        txtBeneficiarioCheque.Text = benef
        txtBeneficiaTrans.Text = benef
        txtBeneficiarioOtro.Text = benef
        cmbcuentaOrigenTrans.Text = My.Settings.CuentaTrans
        parFecha = pFecha
        If pTipoPoliza.StartsWith("I") Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If
    End Sub

    Private Sub frmContabilidadComprobantes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            llenando = False
            LlenaCombos("tblbancoscatalogo", cmbBancoCheque, "nombre", "nombret", "clave", IdsBancosCheque, , , "nombre")
            LlenaCombos("tblbancoscatalogo", cmbBancoDestinoTrans, "nombre", "nombret", "clave", IdsBancosTransD, , , "nombre")
            LlenaCombos("tblbancoscatalogo", cmbBancoOrigenTrans, "nombre", "nombret", "clave", IdsBancosTransO, , , "nombre")
            LlenaCombos("tblcuentas", cmbcuentaOrigenTrans, "numero", "nombret", "idCuenta", IdsCuentaOTrans, , , "numero")
            LlenaCombos("tblcuentas", cmbCuentaOrigenCheque, "numero", "nombret", "idCuenta", IdsCuentaOChe, , , "numero")
            LlenaCombos("tblmonedassat", cmbMonedaCompNac, "moneda", "nombret", "id", IdsMonedaCompNac, , , "id")
            LlenaCombos("tblmonedassat", cmbMonedaCompNac2, "moneda", "nombret", "id", IdsMonedaCompNac2, , , "id")
            LlenaCombos("tblmonedassat", cmbMonedaCompEx, "moneda", "nombret", "id", IdsMonedaCompEx, , , "id")
            LlenaCombos("tblmonedassat", cmbMonedaCheque, "moneda", "nombret", "id", IdsMonedaCheque, , , "id")
            LlenaCombos("tblmonedassat", cmbMonedaTrans, "moneda", "nombret", "id", IdsMonedaTra, , , "id")
            LlenaCombos("tblmonedassat", cmbMonedaOtro, "moneda", "nombret", "id", IdsMonedaOtro, , , "id")
            LlenaCombos("tblformasdepagosat", cmbMetodoPago, "concat(lpad(convert(clave using utf8),2,'0'),' ',nombre)", "nombret", "clave", IdsMetodoPago, , , "clave")

            cmbMonedaCompNac.SelectedIndex = 100
            cmbMonedaCompNac2.SelectedIndex = 100
            cmbMonedaCompEx.SelectedIndex = 146
            cmbMonedaCheque.SelectedIndex = 100
            cmbMonedaTrans.SelectedIndex = 100
            cmbMonedaOtro.SelectedIndex = 100
            cmbMetodoPago.SelectedIndex = 0

            cmbBancoCheque.SelectedIndex = 0
            cmbBancoDestinoTrans.SelectedIndex = 0
            cmbBancoOrigenTrans.SelectedIndex = 0
            If cmbcuentaOrigenTrans.Items.Count > 0 Then cmbcuentaOrigenTrans.SelectedIndex = 0
            If cmbCuentaOrigenCheque.Items.Count > 0 Then cmbCuentaOrigenCheque.SelectedIndex = 0
            ' dgvCompro.ClearSelection()
            tbPrincipal.TabIndex = 0
            nuevoCompro()
            nuevoComproN2()
            nuevoComproE()
            nuevoTrans()
            nuevoCheque()
            nuevoOtro()
            txtUUID.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
    End Sub
    
    Private Sub nuevoCheque()
        txtNumeroCheque.Text = ""
        ' cmbCuentaOrigenCheque.SelectedIndex = 0
        txtBancoEmisorExtranjero.Text = ""
        dtpFechaCheque1.Value = CDate(parFecha)
        txtMontoCheque.Text = ""
        txtBeneficiarioCheque.Text = benef
        txtRFCCheque.Text = ""
        cmbBancoCheque.SelectedIndex = 0
        cmbMonedaCheque.BackColor = Color.White
        cmbBancoCheque.BackColor = Color.White
        txtNumeroCheque.BackColor = Color.White
        cmbCuentaOrigenCheque.BackColor = Color.White
        txtMontoCheque.BackColor = Color.White
        txtBeneficiarioCheque.BackColor = Color.White
        txtRFCCheque.BackColor = Color.White
        cmbBancoCheque.SelectedIndex = My.Settings.bancoCheque
        If My.Settings.cuentaCheque = "" And cmbCuentaOrigenCheque.Items.Count > 0 Then
            cmbCuentaOrigenCheque.SelectedIndex = 0
        Else
            If My.Settings.cuentaCheque <> "" Then
                cmbCuentaOrigenCheque.Text = My.Settings.cuentaCheque
            End If
        End If
        btnAgregarCheque.Text = "Agregar"
        btnEliminarCheque.Enabled = False
        Consultacheques()
    End Sub
    Private Sub nuevoTrans()
        txtMontoTrans.Text = ""
        txtCuentaDestinoTrans.Text = ""
        dtpFechaTrans1.Value = CDate(parFecha)
        txtBeneficiaTrans.Text = benef
        txtBancoDesExTrans.Text = ""
        txtBancoOriExTrans.Text = ""
        txtRFCTrans.Text = ""
        cmbBancoDestinoTrans.SelectedIndex = 0
        If My.Settings.cuentaTrans = "" And cmbcuentaOrigenTrans.Items.Count > 0 Then
            cmbcuentaOrigenTrans.SelectedIndex = 0
        Else
            If My.Settings.cuentaTrans <> "" Then
                cmbcuentaOrigenTrans.Text = My.Settings.cuentaTrans
            End If
        End If
        cmbBancoOrigenTrans.SelectedIndex = My.Settings.bancoTrans
        cmbMonedaTrans.BackColor = Color.White
        cmbBancoOrigenTrans.BackColor = Color.White
        cmbBancoDestinoTrans.BackColor = Color.White
        cmbcuentaOrigenTrans.BackColor = Color.White
        txtMontoTrans.BackColor = Color.White
        txtCuentaDestinoTrans.BackColor = Color.White
        txtBeneficiaTrans.BackColor = Color.White
        txtRFCTrans.BackColor = Color.White
        btnAgregarTrans.Text = "Agregar"
        btnEliminarTrans.Enabled = False
        ConsultaTrans()
        cmbcuentaOrigenTrans.Focus()
    End Sub
    Private Sub nuevoCompro()
        txtUUID.Text = ""
        txtMontoCompro.Text = ""
        txtRFCCompro.Text = ""
        txtUUID.BackColor = Color.White
        txtMontoCompro.BackColor = Color.White
        txtRFCCompro.BackColor = Color.White
        cmbMonedaCompNac.BackColor = Color.White
        btnAgregarCompro.Text = "Agregar"
        btnEliminarCompro.Enabled = False
        Consultacomprobantes()
    End Sub
    Private Sub nuevoComproN2()
        txtSerieCompNac2.Text = ""
        txtFolioCompNac2.Text = ""
        txtRFCCompNac2.Text = ""
        txtMontoComNac2.Text = ""
        cmbMonedaCompNac2.BackColor = Color.White
        txtSerieCompNac2.BackColor = Color.White
        txtFolioCompNac2.BackColor = Color.White
        txtRFCCompNac2.BackColor = Color.White
        txtMontoComNac2.BackColor = Color.White
        btnAgregarCompNac2.Text = "Agregar"
        btnEliminarCompNac2.Enabled = False
        Consultacomprobantes2()
    End Sub
    Private Sub nuevoComproE()
        txtNumFacturaCompEx.Text = ""
        txtTaxIDCompEx.Text = ""
        txtMontoComNac2.Text = ""
        txtMontoTotalCompEx.Text = ""
        cmbMonedaCompEx.BackColor = Color.White
        txtNumFacturaCompEx.BackColor = Color.White
        txtTaxIDCompEx.BackColor = Color.White
        txtMontoComNac2.BackColor = Color.White
        txtMontoTotalCompEx.BackColor = Color.White
        btnAgregarCompEx.Text = "Agregar"
        btnEliminarCompEx.Enabled = False
        ConsultacomprobantesE()
    End Sub

    Private Sub txtNumeroCheque_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMontoCheque_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoCheque.KeyPress
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And txtMontoCheque.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
        If e.KeyChar = "-" Then
            If txtMontoCheque.Text <> "" Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub txtMontoCheque_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMontoCheque.Leave
        Dim x As Double
        'Dim txtmontocheque As txtmontocheque = DirectCast(sender, txtmontocheque)
        If txtMontoCheque.Text = "." Then
            txtMontoCheque.Text = "0.00"
        End If
        If txtMontoCheque.Text = "" Then
            ' txtCargo.Text = "0.00"
        Else
            If txtMontoCheque.Text = "-" Then
                txtMontoCheque.Text = "-0.00"
            Else
                If txtMontoCheque.Text = "-." Then
                    txtMontoCheque.Text = "-0.00"
                Else
                    x = Double.Parse(txtMontoCheque.Text)
                    txtMontoCheque.Text = Format(x, "0.00")
                End If
            End If

        End If
    End Sub

    Private Sub txtRFCCheque_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFCCheque.Leave
        If txtRFCCheque.Text <> "" Then
            If ValidarRFC(txtRFCCheque.Text) = 1 Then
                MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRFCCheque.Focus()
                txtRFCCheque.SelectAll()
            End If
        End If
    End Sub
    Public Function ValidarRFC(ByVal cadena As String) As Integer
        Dim i As Integer = 0
        Dim confirmacion As Boolean = True
        If cadena.Length > 11 And cadena.Length < 14 Then
            If cadena.Length = 12 Then
                cadena = "-" + cadena
                i = 1
            End If

            For j As Integer = i To 3
                If Char.IsLetter(cadena(j)) = False Then 'creo que aqui tiene que ser verdadero
                    confirmacion = False
                End If
            Next
            For j As Integer = 4 To 9
                If Char.IsDigit(cadena(j)) = False Then
                    confirmacion = False

                End If
            Next
            For j As Integer = 9 To 12
                If Char.IsLetterOrDigit(cadena(j)) = False Then
                    confirmacion = False

                End If

            Next


        Else
            confirmacion = False
        End If
        If (confirmacion = False) Then
            'no es valido
            Return 1
        Else
            ' si es valido
            Return 0
        End If
    End Function

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtMontoTrans_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoTrans.KeyPress
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And txtMontoTrans.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
        If e.KeyChar = "-" Then
            If txtMontoTrans.Text <> "" Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub txtMontoTrans_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMontoTrans.Leave
        Dim x As Double
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "." Then
            textBox.Text = "0.00"
        End If
        If textBox.Text = "" Then
            ' txtCargo.Text = "0.00"
        Else
            If textBox.Text = "-" Then
                textBox.Text = "-0.00"
            Else
                If textBox.Text = "-." Then
                    textBox.Text = "-0.00"
                Else
                    x = Double.Parse(textBox.Text)
                    textBox.Text = Format(x, "0.00")
                End If
            End If

        End If
    End Sub

    Private Sub txtRFCTrans_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFCTrans.Leave
        If txtRFCTrans.Text <> "" Then
            If ValidarRFC(txtRFCTrans.Text) = 1 Then
                MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRFCTrans.Focus()
                txtRFCTrans.SelectAll()
            End If
        End If
    End Sub

    Private Sub txtMontoCompro_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoCompro.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
        If e.KeyChar = "-" Then
            If txtMontoCompro.Text <> "" Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub txtMontoCompro_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMontoCompro.Leave
        Dim x As Double
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "." Then
            textBox.Text = "0.00"
        End If
        If textBox.Text = "" Then
            ' txtCargo.Text = "0.00"
        Else
            If textBox.Text = "-" Then
                textBox.Text = "-0.00"
            Else
                If textBox.Text = "-." Then
                    textBox.Text = "-0.00"
                Else
                    x = Double.Parse(textBox.Text)
                    textBox.Text = Format(x, "0.00")
                End If
            End If

        End If
    End Sub

    Private Sub txtRFCCompro_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFCCompro.Leave
        If txtRFCCompro.Text <> "" Then
            If ValidarRFC(txtRFCCompro.Text) = 1 Then
                MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRFCCompro.Focus()
                txtRFCCompro.SelectAll()
            End If
        End If
    End Sub

    Private Sub txtNumeroCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            cmbBancoCheque.Focus()
        End If
    End Sub

    Private Sub cmbBancoCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbBancoCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            'txtBancoEmisorExtranjero.Focus()
            cmbCuentaOrigenCheque.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtNumeroCheque.Focus()
        End If
    End Sub

    Private Sub txtCtaOrigenCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            dtpFechaCheque1.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbBancoCheque.Focus()
        End If
    End Sub

    Private Sub dtpFechaCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            txtMontoCheque.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbCuentaOrigenCheque.Focus()
        End If
    End Sub

    Private Sub txtMontoCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMontoCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            'cmbMonedaCheque.Focus()
            AgregarCheque()
        End If
        If e.KeyCode = Keys.Escape Then
            txtRFCCheque.Focus()
        End If
    End Sub

    Private Sub txtBeneficiarioCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBeneficiarioCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRFCCheque.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            dtpFechaCheque1.Focus()
        End If
    End Sub

    Private Sub txtRFCCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRFCCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMontoCheque.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtBeneficiarioCheque.Focus()
        End If
    End Sub

    Private Sub txtcuentaOrigenTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            cmbBancoOrigenTrans.Focus()
        End If
    End Sub

    Private Sub cmbBancoOrigenTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbBancoOrigenTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCuentaDestinoTrans.Focus()
        End If
        'If e.KeyCode = Keys.Escape Then
        '    cmbcuentaOrigenTrans.Focus()
        'End If
    End Sub

    Private Sub txtMontoTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMontoTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            'cmbMonedaTrans.Focus()
            AgregarTransaccion()
        End If
        If e.KeyCode = Keys.Escape Then
            txtRFCTrans.Focus()
        End If
    End Sub

    Private Sub txtCuentaDestinoTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCuentaDestinoTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbBancoDestinoTrans.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbBancoOrigenTrans.Focus()
        End If
    End Sub

    Private Sub cmbBancoDestinoTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbBancoDestinoTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFechaTrans1.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtCuentaDestinoTrans.Focus()
        End If
    End Sub

    Private Sub dtpFechaTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            txtBeneficiaTrans.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbBancoDestinoTrans.Focus()
        End If
    End Sub

    Private Sub txtBeneficiaTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBeneficiaTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRFCTrans.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            dtpFechaTrans1.Focus()
        End If
    End Sub

    Private Sub txtRFCTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRFCTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMontoTrans.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtBeneficiaTrans.Focus()
        End If
    End Sub

    Private Sub txtUUID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUUID.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMontoCompro.Focus()
        End If
    End Sub

    Private Sub txtMontoCompro_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMontoCompro.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRFCCompro.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtUUID.Focus()
        End If
    End Sub

    Private Sub txtRFCCompro_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRFCCompro.KeyDown
        If e.KeyCode = Keys.Enter Then
            'cmbMonedaCompNac.Focus()
            agregarComprobante()
        End If
        If e.KeyCode = Keys.Escape Then
            txtMontoCompro.Focus()
        End If
    End Sub

    Private Sub txtCuentaDestinoTrans_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCuentaDestinoTrans.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnBuscar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnBuscar.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtUUID.Focus()
        End If
    End Sub

    Private Sub btnAgregarCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnAgregarCheque.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtRFCCheque.Focus()
        End If
    End Sub

    Private Sub btnEliminarCheque_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnEliminarCheque.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnAgregarCheque.Focus()
        End If
    End Sub

    Private Sub btnAgregarTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnAgregarTrans.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtRFCTrans.Focus()
        End If
    End Sub

    Private Sub btnEliminarTrans_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnEliminarTrans.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnAgregarTrans.Focus()
        End If
    End Sub

    Private Sub btnAgregarCompro_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnAgregarCompro.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtRFCCompro.Focus()
        End If
    End Sub

    Private Sub btnEliminarCompro_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnEliminarCompro.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnAgregarCompro.Focus()
        End If
    End Sub

    Private Sub btnNuevoCompro_Click(sender As Object, e As EventArgs) Handles btnNuevoCompro.Click
        nuevoCompro()
        txtUUID.Focus()
    End Sub

    Private Sub btnNuevoTrans_Click(sender As Object, e As EventArgs) Handles btnNuevoTrans.Click
        nuevoTrans()
        cmbcuentaOrigenTrans.Focus()
    End Sub

    Private Sub btnNuevoCheque_Click(sender As Object, e As EventArgs) Handles btnNuevoCheque.Click
        nuevoCheque()
        txtNumeroCheque.Focus()
    End Sub

    Private Sub btnAgregarCheque_Click(sender As Object, e As EventArgs) Handles btnAgregarCheque.Click
        AgregarCheque()

    End Sub
    Private Sub AgregarCheque()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If txtNumeroCheque.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere un número de cheque." + vbCrLf
                txtNumeroCheque.BackColor = Color.Tomato
            End If
            If cmbCuentaOrigenCheque.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el número de cuenta origen." + vbCrLf
                cmbCuentaOrigenCheque.BackColor = Color.Tomato
            End If
            If txtMontoCheque.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el monto del cheque." + vbCrLf
                txtMontoCheque.BackColor = Color.Tomato
            End If
            If txtBeneficiarioCheque.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el beneficiario del cheque." + vbCrLf
                txtBeneficiarioCheque.BackColor = Color.Tomato
            End If
            If txtRFCCheque.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el R.F.C. del beneficiario." + vbCrLf
                txtRFCCheque.BackColor = Color.Tomato
            End If
            If cmbBancoCheque.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe seleccionar un banco" + vbCrLf
                cmbBancoCheque.BackColor = Color.Tomato
            End If
            If cmbMonedaCheque.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe especificar un tipo de moneda" + vbCrLf
                cmbMonedaCheque.BackColor = Color.Tomato
            End If

            If NoErrores Then
                If btnAgregarCheque.Text = "Agregar" Then
                    
                    ' dgvCheques.ClearSelection()
                    My.Settings.bancoCheque = cmbBancoCheque.SelectedIndex
                    My.Settings.CuentaCheque = cmbCuentaOrigenCheque.Text
                    p.guardarCheque(IDPoliza, IDRenglon, txtNumeroCheque.Text, IdsBancosCheque.Valor(cmbBancoCheque.SelectedIndex), txtBancoEmisorExtranjero.Text, cmbCuentaOrigenCheque.Text, dtpFechaCheque1.Value.ToString("yyyy/MM/dd"), txtBeneficiarioCheque.Text, txtRFCCheque.Text, CDbl(txtMontoCheque.Text), IdsMonedaCheque.Valor(cmbMonedaCheque.SelectedIndex), CDbl(txtTipoCambioCheque.Text))
                    nuevoCheque()
                    txtNumeroCheque.Focus()
                Else
                    'Modificar Cheque
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        p.ModificarCheque(p.ChequeId, txtNumeroCheque.Text, IdsBancosCheque.Valor(cmbBancoCheque.SelectedIndex), txtBancoEmisorExtranjero.Text, cmbCuentaOrigenCheque.Text, dtpFechaCheque1.Value.ToString("yyyy/MM/dd"), txtBeneficiarioCheque.Text, txtRFCCheque.Text, CDbl(txtMontoCheque.Text), IdsMonedaCheque.Valor(cmbMonedaCheque.SelectedIndex), CDbl(txtTipoCambioCheque.Text))
                        '  dgvCheques.ClearSelection()
                        My.Settings.bancoCheque = cmbBancoCheque.SelectedIndex
                        My.Settings.CuentaCheque = cmbCuentaOrigenCheque.Text
                        nuevoCheque()
                        txtNumeroCheque.Focus()
                    End If
                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    

    Private Sub btnEliminarCheque_Click(sender As Object, e As EventArgs) Handles btnEliminarCheque.Click
        Try
            If MsgBox("¿Deseas eliminar este cheque?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                p.EliminarCheque(p.ChequeId)
                nuevoCheque()
                ' dgvCheques.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnAgregarTrans_Click(sender As Object, e As EventArgs) Handles btnAgregarTrans.Click
        AgregarTransaccion()
    End Sub
    Private Sub AgregarTransaccion()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If cmbcuentaOrigenTrans.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere la cuenta origen." + vbCrLf
                cmbcuentaOrigenTrans.BackColor = Color.Tomato
            End If
            If txtMontoTrans.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el monto de la transacción." + vbCrLf
                txtMontoTrans.BackColor = Color.Tomato
            End If
            If txtCuentaDestinoTrans.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere la cuenta destino." + vbCrLf
                txtCuentaDestinoTrans.BackColor = Color.Tomato
            End If
            If txtBeneficiaTrans.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el beneficiario." + vbCrLf
                txtBeneficiaTrans.BackColor = Color.Tomato
            End If
            If txtRFCTrans.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el R.F.C. del beneficiario." + vbCrLf
                txtRFCTrans.BackColor = Color.Tomato
            End If
            If cmbBancoOrigenTrans.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Se requiere especificar banco origen." + vbCrLf
                cmbBancoOrigenTrans.BackColor = Color.Tomato

            End If
            If cmbBancoDestinoTrans.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Se requiere especificar banco destino." + vbCrLf
                cmbBancoDestinoTrans.BackColor = Color.Tomato
            End If
            If cmbMonedaTrans.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe especificar un tipo de moenda." + vbCrLf
                cmbMonedaTrans.BackColor = Color.Tomato
            End If
            If NoErrores Then
                If btnAgregarTrans.Text = "Agregar" Then
                    My.Settings.CuentaTrans = cmbcuentaOrigenTrans.Text
                    My.Settings.bancoTrans = cmbBancoOrigenTrans.SelectedIndex
                    p.guardarTransferencia(IDPoliza, IDRenglon, cmbcuentaOrigenTrans.Text, IdsBancosTransO.Valor(cmbBancoOrigenTrans.SelectedIndex), txtBancoOriExTrans.Text, txtCuentaDestinoTrans.Text, IdsBancosTransD.Valor(cmbBancoDestinoTrans.SelectedIndex), txtBancoDesExTrans.Text, dtpFechaTrans1.Value.ToString("yyyy/MM/dd"), txtBeneficiaTrans.Text, txtRFCTrans.Text, CDbl(txtMontoTrans.Text), IdsMonedaTra.Valor(cmbMonedaTrans.SelectedIndex), CDbl(txtTipoCambioTran.Text))
                    nuevoTrans()
                    cmbcuentaOrigenTrans.Focus()
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        My.Settings.CuentaTrans = cmbcuentaOrigenTrans.Text
                        My.Settings.bancoTrans = cmbBancoOrigenTrans.SelectedIndex
                        p.ModificarTransaccion(p.TransferenciaId, cmbcuentaOrigenTrans.Text, IdsBancosTransO.Valor(cmbBancoOrigenTrans.SelectedIndex), txtBancoOriExTrans.Text, txtCuentaDestinoTrans.Text, IdsBancosTransD.Valor(cmbBancoDestinoTrans.SelectedIndex), txtBancoDesExTrans.Text, dtpFechaTrans1.Value.ToString("yyyy/MM/dd"), txtBeneficiaTrans.Text, txtRFCTrans.Text, CDbl(txtMontoTrans.Text), IdsMonedaTra.Valor(cmbMonedaTrans.SelectedIndex), CDbl(txtTipoCambioTran.Text))
                        nuevoTrans()
                        cmbcuentaOrigenTrans.Focus()
                    End If
                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    

    Private Sub btnEliminarTrans_Click(sender As Object, e As EventArgs) Handles btnEliminarTrans.Click
        Try
            If MsgBox("¿Deseas eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                p.EliminarTrans(p.TransferenciaId)
                nuevoTrans()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        Dim R As String
        Dim T As String
        If RadioButton1.Checked Then
            R = p.RutaXMLIngresos
            T = "I"
        Else
            R = p.rutaUUID
            T = "E"
        End If
        Dim v As New frmComponentesUUID(R, T)
        v.ShowDialog()
        If v.DialogResult = Windows.Forms.DialogResult.OK Then
            For i As Integer = 0 To v.tabla.Rows.Count - 1
                If v.tabla.Rows(i)(0) = 1 Then
                    txtUUID.Text = v.tabla.Rows(i)(1).ToString
                    txtMontoCompro.Text = v.tabla.Rows(i)(2).ToString
                    txtRFCCompro.Text = v.tabla.Rows(i)(3).ToString
                    For j As Integer = 0 To dgvCompro.RowCount - 1
                        If txtUUID.Text = dgvCompro.Item(2, j).Value Then
                            NoErrores = False
                            MensajeError += "UUID repetido: " + txtUUID.Text + vbCrLf
                        End If
                    Next
                    If NoErrores Then
                        agregarComprobante()
                    End If
                End If
            Next
            If MensajeError <> "" Then
                MsgBox(MensajeError, MsgBoxStyle.OkOnly, GlobalNombreApp)
            End If
            'txtCuenta.Text = C.N1.PadLeft(p.NNiv1, "0")
        End If
    End Sub

    Private Sub btnAgregarCompro_Click(sender As Object, e As EventArgs) Handles btnAgregarCompro.Click
        agregarComprobante()
    End Sub
    Private Sub agregarComprobante()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If txtUUID.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el UUID." + vbCrLf
                txtUUID.BackColor = Color.Tomato
            Else
                For i As Integer = 0 To dgvCompro.RowCount - 1
                    If txtUUID.Text = dgvCompro.Item(1, i).Value Then
                        If btnEliminarCompro.Enabled = False Then
                            NoErrores = False
                            MensajeError += "UUID repetido: " + txtUUID.Text + vbCrLf
                        End If

                    End If
                Next

            End If
            If txtMontoCompro.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el monto del comprobante." + vbCrLf
                txtMontoCompro.BackColor = Color.Tomato
            End If
            If txtRFCCompro.Text = "" Then
                NoErrores = False
                MensajeError += "Se R.F.C. del comprobante." + vbCrLf
                txtRFCCompro.BackColor = Color.Tomato
            End If
            If cmbMonedaCompNac.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe especificar un tipo de moneda" + vbCrLf
                cmbMonedaCompNac.BackColor = Color.Tomato
            End If

            If NoErrores Then
                If btnAgregarCompro.Text = "Agregar" Then
                    p.guardarComprobante(IDPoliza, IDRenglon, txtUUID.Text, CDbl(txtMontoCompro.Text), txtRFCCompro.Text, IdsMonedaCompNac.Valor(cmbMonedaCompNac.SelectedIndex), CDbl(txtTipoCambioCompNac.Text))
                    nuevoCompro()
                    txtUUID.Focus()
                Else
                    'Modificar Cheque
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        '  dgvCompro.ClearSelection()
                        p.ModificarComprobante(p.ComprobanteId, txtUUID.Text, CDbl(txtMontoCompro.Text), txtRFCCompro.Text, IdsMonedaCompNac.Valor(cmbMonedaCompNac.SelectedIndex), CDbl(txtTipoCambioCompNac.Text))
                        nuevoCompro()
                        txtUUID.Focus()
                    End If
                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Consultacomprobantes()
        dgvCompro.DataSource = p.ConsultaComprobantes(IDRenglon)
        'id,fecha,uuid,monto,rfc
        dgvCompro.Columns(1).HeaderText = "UUID"
        dgvCompro.Columns(2).HeaderText = "Monto"
        dgvCompro.Columns(3).HeaderText = "RFC"
        dgvCompro.Columns(0).Visible = False
        dgvCompro.Columns(3).Width = 150
        dgvCompro.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        lblTotalComprobante.Text = p.TotalComprobantes(IDRenglon).ToString("C2")
    End Sub
    Private Sub Consultacomprobantes2()
        dgvComproNac2.DataSource = p.ConsultaComprobantesNa2(IDRenglon)
        'd,serie,folio,rfc,monto
        dgvComproNac2.Columns(1).HeaderText = "Serie"
        dgvComproNac2.Columns(2).HeaderText = "Folio"
        dgvComproNac2.Columns(3).HeaderText = "RFC"
        dgvComproNac2.Columns(4).HeaderText = "Monto"
        dgvComproNac2.Columns(0).Visible = False
        dgvComproNac2.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        lblTotalCompNac2.Text = p.TotalComprobantesNa2(IDRenglon).ToString("C2")
    End Sub
    Private Sub ConsultacomprobantesE()
        'numfactura,taxid,monto,(select tblmonedassat.moneda from tblmonedassat where tblmonedassat.id=tblcontabilidadcomproe.moneda ) Moneda,tipocambio
        dgvComproE.DataSource = p.ConsultaComprobantesE(IDRenglon)
        dgvComproE.Columns(1).HeaderText = "Num."
        dgvComproE.Columns(2).HeaderText = "Tax Id"
        dgvComproE.Columns(3).HeaderText = "Monto"
        dgvComproE.Columns(4).HeaderText = "Moneda"
        dgvComproE.Columns(5).HeaderText = "T. Cambio"
        dgvComproE.Columns(0).Visible = False
        dgvComproE.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        lbltotalCompE.Text = p.TotalComprobantesE(IDRenglon).ToString("C2")
    End Sub
    Private Sub Consultacheques()
        ',numero,(select nombre from tblbancoscatalogo where idbanco=banco),ctaOri,fecha,monto
        dgvCheques.DataSource = p.ConsultaCheques(IDRenglon)
        dgvCheques.Columns(1).HeaderText = "Num."
        dgvCheques.Columns(2).HeaderText = "Banco"
        dgvCheques.Columns(3).HeaderText = "Cuenta"
        dgvCheques.Columns(4).HeaderText = "Fecha"
        dgvCheques.Columns(5).HeaderText = "Monto"
        dgvCheques.Columns(0).Visible = False
        dgvCheques.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        lblTotalCheque.Text = p.TotalCheques(IDRenglon).ToString("C2")
    End Sub
    Private Sub ConsultaTrans()
        dgvTrans.DataSource = p.ConsultaTrans(IDRenglon)
        dgvTrans.Columns(1).HeaderText = "Fecha"
        dgvTrans.Columns(2).HeaderText = "Origen"
        dgvTrans.Columns(3).HeaderText = "Destino"
        dgvTrans.Columns(4).HeaderText = "Monto"
        dgvTrans.Columns(0).Visible = False
        dgvTrans.Columns(3).Width = 250
        dgvTrans.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        lblTotalTrans.Text = p.TotalTrans(IDRenglon).ToString("C2")
    End Sub
    Private Sub ConsultaOtros()
        dgvOtros.DataSource = p.ConsultaOtro(IDRenglon)
        dgvOtros.Columns(1).HeaderText = "Método"
        dgvOtros.Columns(2).HeaderText = "Fecha"
        dgvOtros.Columns(3).HeaderText = "Beneficiario"
        dgvOtros.Columns(4).HeaderText = "Monto"
        dgvOtros.Columns(0).Visible = False
        dgvOtros.Columns(1).Width = 200
        dgvOtros.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        lblTotalOtro.Text = p.TotalOtros(IDRenglon).ToString("C2")
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Mensajeerror As String = ""
        Dim NoErrores As Boolean = True
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = True
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim xmldoc As New Xml.XmlDocument
                Dim s As String
                For Each s In OpenFileDialog1.FileNames
                    xmldoc.Load(s)
                    NoErrores = True
                    txtUUID.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                    txtMontoCompro.Text = xmldoc.Item("cfdi:Comprobante").Attributes("total").Value
                    txtRFCCompro.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                    For i As Integer = 0 To dgvCompro.RowCount - 1
                        If txtUUID.Text = dgvCompro.Item(1, i).Value Then
                            NoErrores = False
                            Mensajeerror += "UUID repetido: " + txtUUID.Text + vbCrLf
                        End If
                    Next
                    If NoErrores Then
                        agregarComprobante()
                    End If

                Next
                If Mensajeerror <> "" Then
                    MsgBox(Mensajeerror, MsgBoxStyle.OkOnly, GlobalNombreApp)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            End Try
        End If
        nuevoCompro()
    End Sub

    Private Sub btnEliminarCompro_Click(sender As Object, e As EventArgs) Handles btnEliminarCompro.Click
        Try
            If MsgBox("¿Deseas eliminar este comprobante?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                p.EliminarComprobante(p.ComprobanteId)
                nuevoCompro()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub dtpFechaTrans1_CloseUp(sender As Object, e As EventArgs) Handles dtpFechaTrans1.CloseUp
        txtBeneficiarioCheque.Focus()
    End Sub

    Private Sub dtpFechaTrans1_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFechaTrans1.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbBancoDestinoTrans.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            txtBeneficiaTrans.Focus()
        End If
    End Sub

    Private Sub dtpFechaCheque1_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFechaCheque1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBeneficiarioCheque.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbCuentaOrigenCheque.Focus()
        End If
    End Sub

    Private Sub cmbBancoCheque_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbBancoCheque.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub cmbBancoOrigenTrans_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbBancoOrigenTrans.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub cmbBancoDestinoTrans_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbBancoDestinoTrans.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub cmbNumeroCheque_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            cmbBancoCheque.Focus()
        End If
    End Sub

    Private Sub cmbNumeroCheque_KeyPress(sender As Object, e As KeyPressEventArgs)

        e.KeyChar = UCase(e.KeyChar)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbcuentaOrigenTrans_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbcuentaOrigenTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBancoOriExTrans.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtMontoTrans.Focus()
        End If
    End Sub

    Private Sub cmbcuentaOrigenTrans_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbcuentaOrigenTrans.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmContabilidadComprobantes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If My.Settings.cuentaCheque <> "" Then
            cmbCuentaOrigenCheque.Text = My.Settings.cuentaCheque
        End If
        If My.Settings.cuentaTrans <> "" Then
            cmbcuentaOrigenTrans.Text = My.Settings.cuentaTrans
        End If
        tbComprobante.Focus()
        txtUUID.Focus()
    End Sub

    Private Sub cmbCuentaOrigenCheque_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbCuentaOrigenCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFechaCheque1.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtBancoEmisorExtranjero.Focus()
        End If
    End Sub

    Private Sub txtNumeroCheque_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles txtNumeroCheque.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNumeroCheque_KeyDown_1(sender As Object, e As KeyEventArgs) Handles txtNumeroCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbBancoCheque.Focus()
        End If
    End Sub

    Private Sub txtTipoCambioCompNac_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipoCambioCompNac.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
    End Sub

    Private Sub cmbMonedaCompNac_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedaCompNac.SelectedIndexChanged
        If cmbMonedaCompNac.SelectedIndex = 100 Then
            lblTipoCambioCompNac.Enabled = False
            txtTipoCambioCompNac.Enabled = False
        Else
            lblTipoCambioCompNac.Enabled = True
            txtTipoCambioCompNac.Enabled = True
        End If
    End Sub

    Private Sub txtFolioCompNac2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFolioCompNac2.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtRFCCompNac2_Leave(sender As Object, e As EventArgs) Handles txtRFCCompNac2.Leave
        If txtRFCCompNac2.Text <> "" Then
            If ValidarRFC(txtRFCCompNac2.Text) = 1 Then
                MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRFCCompNac2.Focus()
                txtRFCCompNac2.SelectAll()
            End If
        End If
    End Sub

    Private Sub txtMontoComNac2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMontoComNac2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
    End Sub

    Private Sub txtMontoComNac2_Leave(sender As Object, e As EventArgs) Handles txtMontoComNac2.Leave
        Dim x As Double
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "." Then
            textBox.Text = "0.00"
        End If
        If textBox.Text = "" Then
            ' txtCargo.Text = "0.00"
        Else
            If textBox.Text = "-" Then
                textBox.Text = "-0.00"
            Else
                If textBox.Text = "-." Then
                    textBox.Text = "-0.00"
                Else
                    x = Double.Parse(textBox.Text)
                    textBox.Text = Format(x, "0.00")
                End If
            End If

        End If
    End Sub

    Private Sub cmbMonedaCompNac2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedaCompNac2.SelectedIndexChanged
        If cmbMonedaCompNac2.SelectedIndex = 100 Then
            lblTipoCambioCompNac2.Enabled = False
            txtTipoCambioCompNac2.Enabled = False
        Else
            lblTipoCambioCompNac2.Enabled = True
            txtTipoCambioCompNac2.Enabled = True
        End If
    End Sub

    Private Sub cmbMonedaCompEx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedaCompEx.SelectedIndexChanged
        If cmbMonedaCompEx.SelectedIndex = 100 Then
            lblTipoCambioCompEx.Enabled = False
            txtTipoCambioCompEx.Enabled = False
        Else
            lblTipoCambioCompEx.Enabled = True
            txtTipoCambioCompEx.Enabled = True
        End If
    End Sub

    Private Sub cmbMonedaCheque_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedaCheque.SelectedIndexChanged
        If cmbMonedaCheque.SelectedIndex = 100 Then
            lblTipoCambioCheque.Enabled = False
            txtTipoCambioCheque.Enabled = False
        Else
            lblTipoCambioCheque.Enabled = True
            txtTipoCambioCheque.Enabled = True
        End If
    End Sub

    Private Sub cmbMonedaTrans_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedaTrans.SelectedIndexChanged
        If cmbMonedaTrans.SelectedIndex = 100 Then
            lblTipoCambioTra.Enabled = False
            txtTipoCambioTran.Enabled = False
        Else
            lblTipoCambioTra.Enabled = True
            txtTipoCambioTran.Enabled = True
        End If
    End Sub

    Private Sub txtRFCOtros_Leave(sender As Object, e As EventArgs) Handles txtRFCOtros.Leave
        If txtRFCOtros.Text <> "" Then
            If ValidarRFC(txtRFCOtros.Text) = 1 Then
                MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRFCOtros.Focus()
                txtRFCOtros.SelectAll()
            End If
        End If
    End Sub

    Private Sub cmbMonedaOtro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonedaOtro.SelectedIndexChanged
        If cmbMonedaOtro.SelectedIndex = 100 Then
            lblTipoCambioOtro.Enabled = False
            txtTipoCambioOtro.Enabled = False
        Else
            lblTipoCambioOtro.Enabled = True
            txtTipoCambioOtro.Enabled = True
        End If
    End Sub


    Private Sub cmbMonedaCompNac_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMonedaCompNac.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtTipoCambioCompNac.Enabled = True Then
                txtTipoCambioCompNac.Focus()
            Else
                btnAgregarCompro.Focus()
            End If

        End If
        If e.KeyCode = Keys.Escape Then
            txtRFCCompro.Focus()
        End If
    End Sub

    Private Sub txtTipoCambioCompNac_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambioCompNac.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtTipoCambioCompNac.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbMonedaCompNac.Focus()
        End If
    End Sub

    Private Sub txtSerieCompNac2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSerieCompNac2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFolioCompNac2.Focus()
        End If
    End Sub

    Private Sub txtFolioCompNac2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFolioCompNac2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRFCCompNac2.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtSerieCompNac2.Focus()
        End If
    End Sub

    Private Sub txtRFCCompNac2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRFCCompNac2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMontoComNac2.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtFolioCompNac2.Focus()
        End If
    End Sub

    Private Sub txtMontoComNac2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMontoComNac2.KeyDown
        If e.KeyCode = Keys.Enter Then
            'cmbMonedaCompNac2.Focus()
            agregarComprobanteNac()
        End If
        If e.KeyCode = Keys.Escape Then
            txtRFCCompNac2.Focus()
        End If
    End Sub

    Private Sub cmbMonedaCompNac2_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMonedaCompNac2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtTipoCambioCompNac2.Enabled = True Then
                txtTipoCambioCompNac2.Focus()
            Else
                btnAgregarCompNac2.Focus()
            End If

        End If
        If e.KeyCode = Keys.Escape Then
            txtMontoComNac2.Focus()
        End If
    End Sub

    Private Sub txtNumFacturaCompEx_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNumFacturaCompEx.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtTaxIDCompEx.Focus()
        End If
    End Sub

    Private Sub txtTaxIDCompEx_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTaxIDCompEx.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMontoTotalCompEx.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtNumFacturaCompEx.Focus()
        End If
    End Sub

    Private Sub txtMontoTotalCompEx_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMontoTotalCompEx.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbMonedaCompEx.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtTaxIDCompEx.Focus()
        End If
    End Sub

    Private Sub cmbMonedaCompEx_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMonedaCompEx.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtTipoCambioCompEx.Enabled = True Then
                txtTipoCambioCompEx.Focus()
            Else
                btnAgregarCompEx.Focus()
            End If

        End If
        If e.KeyCode = Keys.Escape Then
            txtMontoTotalCompEx.Focus()
        End If
    End Sub

    Private Sub txtBancoOriExTrans_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBancoOriExTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBancoDesExTrans.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbBancoOrigenTrans.Focus()
        End If
    End Sub

    Private Sub txtBancoDesExTrans_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBancoDesExTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbMonedaTrans.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtBancoOriExTrans.Focus()
        End If
    End Sub

    Private Sub cmbMonedaTrans_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMonedaTrans.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtMontoTrans.Enabled = True Then
                txtTipoCambioTran.Focus()
            Else
                AgregarTransaccion()
            End If

        End If
        If e.KeyCode = Keys.Escape Then
            txtBancoDesExTrans.Focus()
        End If
    End Sub

    Private Sub txtTipoCambioTran_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambioTran.KeyDown
        If e.KeyCode = Keys.Enter Then
            AgregarTransaccion()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbMonedaTrans.Focus()
        End If
    End Sub

    Private Sub btnAgregarCompNac2_Click(sender As Object, e As EventArgs) Handles btnAgregarCompNac2.Click
        agregarComprobanteNac()
    End Sub
    Private Sub agregarComprobanteNac()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            'If txtSerieCompNac2.Text = "" Then
            '    NoErrores = False
            '    MensajeError += "Se requiere la Serie del compronate." + vbCrLf
            '    txtSerieCompNac2.BackColor = Color.Tomato
            'End If
            If txtFolioCompNac2.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el Folio del comprobante." + vbCrLf
                txtFolioCompNac2.BackColor = Color.Tomato
            End If
            If txtRFCCompNac2.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere R.F.C. del comprobante." + vbCrLf
                txtRFCCompNac2.BackColor = Color.Tomato
            End If
            If cmbMonedaCompNac2.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe especificar un tipo de moneda" + vbCrLf
                cmbMonedaCompNac2.BackColor = Color.Tomato
            End If
            If txtSerieCompNac2.Text <> "" And txtFolioCompNac2.Text <> "" Then

                For i As Integer = 0 To dgvComproNac2.RowCount - 1
                    If txtSerieCompNac2.Text = dgvComproNac2.Item(3, i).Value And txtFolioCompNac2.Text = dgvComproNac2.Item(4, i).Value Then
                        If btnEliminarCompNac2.Enabled = False Then
                            NoErrores = False
                            MensajeError += "Serie y Folio coinciden con uno ya existente." + vbCrLf
                        End If

                    End If
                Next

            End If

            If NoErrores Then
                If btnAgregarCompNac2.Text = "Agregar" Then
                    p.guardarComprobanteNac2(IDPoliza, IDRenglon, txtSerieCompNac2.Text, CInt(txtFolioCompNac2.Text), txtRFCCompNac2.Text, txtMontoComNac2.Text, IdsMonedaCompNac2.Valor(cmbMonedaCompNac2.SelectedIndex), txtTipoCambioCompNac2.Text)
                    nuevoComproN2()
                    txtSerieCompNac2.Focus()
                Else
                    'Modificar
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        p.ModificarComprobanteNac2(p.Comprobante2Id, txtSerieCompNac2.Text, CInt(txtFolioCompNac2.Text), txtRFCCompNac2.Text, txtMontoComNac2.Text, IdsMonedaCompNac2.Valor(cmbMonedaCompNac2.SelectedIndex), txtTipoCambioCompNac2.Text)
                        nuevoComproN2()
                        txtSerieCompNac2.Focus()
                    End If

                End If

            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnAgregarCompEx_Click(sender As Object, e As EventArgs) Handles btnAgregarCompEx.Click
        agregarComprobanteE()
    End Sub

    Private Sub btnEliminarCompNac2_Click(sender As Object, e As EventArgs) Handles btnEliminarCompNac2.Click
        Try
            If MsgBox("¿Deseas eliminar este comprobante?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                p.EliminarComproNac2(p.Comprobante2Id)
                nuevoComproN2()
                txtSerieCompNac2.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevoCompNac2_Click(sender As Object, e As EventArgs) Handles btnNuevoCompNac2.Click
        nuevoComproN2()
    End Sub

    Private Sub txtTipoCambioCompNac2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambioCompNac2.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnAgregarCompNac2.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbMonedaCompNac2.Focus()
        End If
    End Sub

    Private Sub txtTipoCambioCompNac2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipoCambioCompNac2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
    End Sub

    Private Sub txtTipoCambioCompEx_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambioCompEx.KeyDown
        If e.KeyCode = Keys.Enter Then
            agregarComprobanteE()
        End If
    End Sub

    Private Sub txtTipoCambioCompEx_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipoCambioCompEx.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
    End Sub

    Private Sub txtTipoCambioCheque_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipoCambioCheque.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
    End Sub

    Private Sub txtTipoCambioTran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipoCambioTran.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtTipoCambioOtro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipoCambioOtro.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
    End Sub
    Private Sub agregarComprobanteE()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If txtNumFacturaCompEx.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el número de factura." + vbCrLf
                txtNumFacturaCompEx.BackColor = Color.Tomato
            End If
            'If txtTaxIDCompEx.Text = "" Then
            '    NoErrores = False
            '    MensajeError += "Se requiere el tax ID de la factura." + vbCrLf
            '    txtTaxIDCompEx.BackColor = Color.Tomato
            'End If
            If txtMontoTotalCompEx.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere el monto total de la factura." + vbCrLf
                txtMontoTotalCompEx.BackColor = Color.Tomato
            End If
            If cmbMonedaCompEx.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe especificar un tipo de moneda" + vbCrLf
                cmbMonedaCompEx.BackColor = Color.Tomato
            End If

            If txtNumFacturaCompEx.Text <> "" And txtMontoTotalCompEx.Text <> "" Then

                For i As Integer = 0 To dgvComproE.RowCount - 1
                    If txtNumFacturaCompEx.Text = dgvComproE.Item(3, i).Value And txtMontoTotalCompEx.Text = dgvComproE.Item(5, i).Value Then
                        If btnEliminarCompEx.Enabled = False Then
                            NoErrores = False
                            MensajeError += "El Número de factura y el monto coinciden con uno ya existente." + vbCrLf
                            i = dgvComproE.RowCount - 1

                        End If

                    End If
                Next

            End If

            If NoErrores Then
                If btnAgregarCompEx.Text = "Agregar" Then
                    
                    p.guardarComprobanteE(IDPoliza, IDRenglon, txtNumFacturaCompEx.Text, txtTaxIDCompEx.Text, CDbl(txtMontoTotalCompEx.Text), IdsMonedaCompEx.Valor(cmbMonedaCompEx.SelectedIndex), CDbl(txtTipoCambioCompEx.Text))
                    nuevoComproE()
                    txtNumFacturaCompEx.Focus()
                Else
                    'Modificar Cheque
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        p.modificarComprobanteE(p.ComprobanteEId, txtNumFacturaCompEx.Text, txtTaxIDCompEx.Text, CDbl(txtMontoTotalCompEx.Text), IdsMonedaCompEx.Valor(cmbMonedaCompEx.SelectedIndex), CDbl(txtTipoCambioCompEx.Text))
                        nuevoComproE()
                        txtNumFacturaCompEx.Focus()
                    End If
                End If

            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub btnEliminarCompEx_Click(sender As Object, e As EventArgs) Handles btnEliminarCompEx.Click
        Try
            If MsgBox("¿Deseas eliminar este comprobante?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                p.EliminarComproE(p.ComprobanteEId)
                nuevoComproE()
                '    dgvCompro.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevoCompEx_Click(sender As Object, e As EventArgs) Handles btnNuevoCompEx.Click
        nuevoComproE()
    End Sub

    Private Sub txtBancoEmisorExtranjero_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBancoEmisorExtranjero.KeyDown
        If e.KeyCode = Keys.Enter Then
            'cmbCuentaOrigenCheque.Focus()
            AgregarCheque()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbBancoCheque.Focus()
        End If
    End Sub

    Private Sub cmbMonedaCheque_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMonedaCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtTipoCambioCheque.Enabled = True Then
                txtTipoCambioCheque.Focus()
            Else
                txtBancoEmisorExtranjero.Focus()
            End If
        End If
        If e.KeyCode = Keys.Escape Then
            txtMontoCheque.Focus()
        End If
    End Sub

    Private Sub txtTipoCambioCheque_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambioCheque.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBancoEmisorExtranjero.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbMonedaCheque.Focus()
        End If
    End Sub

    Private Sub btnAgregarOtro_Click(sender As Object, e As EventArgs) Handles btnAgregarOtro.Click
        AgregarOtros()
    End Sub
    Private Sub AgregarOtros()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If cmbMetodoPago.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe seleccionar un método de pago válido." + vbCrLf
                cmbMetodoPago.BackColor = Color.Tomato
            End If
            'If txtTaxIDCompEx.Text = "" Then
            '    NoErrores = False
            '    MensajeError += "Se requiere el tax ID de la factura." + vbCrLf
            '    txtTaxIDCompEx.BackColor = Color.Tomato
            'End If
            If txtBeneficiarioOtro.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere un beneficiario para el método." + vbCrLf
                txtBeneficiarioOtro.BackColor = Color.Tomato
            End If
            If txtRFCOtros.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere un RFC para el método." + vbCrLf
                txtRFCOtros.BackColor = Color.Tomato
            End If
            If cmbMonedaOtro.SelectedIndex < 0 Then
                NoErrores = False
                MensajeError += "Debe especificar un tipo de moneda" + vbCrLf
                cmbMonedaOtro.BackColor = Color.Tomato
            End If

            If NoErrores Then
                If btnAgregarOtro.Text = "Agregar" Then
                    p.guardarOtro(IDPoliza, IDRenglon, IdsMetodoPago.Valor(cmbMetodoPago.SelectedIndex), dtpFechaOtro.Value.ToString("yyyy/MM/dd"), txtBeneficiarioOtro.Text, txtRFCOtros.Text, CDbl(txtMontoOtro.Text), IdsMonedaOtro.Valor(cmbMonedaOtro.SelectedIndex), CDbl(txtTipoCambioOtro.Text))
                    nuevoOtro()
                    cmbMetodoPago.Focus()
                Else
                    'Modificar Cheque
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        p.modificarOtro(p.OtrosId, IdsMetodoPago.Valor(cmbMetodoPago.SelectedIndex), dtpFechaOtro.Value.ToString("yyyy/MM/dd"), txtBeneficiarioOtro.Text, txtRFCOtros.Text, CDbl(txtMontoOtro.Text), IdsMonedaOtro.Valor(cmbMonedaOtro.SelectedIndex), CDbl(txtTipoCambioOtro.Text))
                        nuevoOtro()
                        cmbMetodoPago.Focus()
                    End If
                End If

            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub btnNuevoOtro_Click(sender As Object, e As EventArgs) Handles btnNuevoOtro.Click
        nuevoOtro()
    End Sub
    Private Sub nuevoOtro()
        cmbMetodoPago.SelectedIndex = 0
        dtpFechaOtro.Value = CDate(parFecha)
        txtBeneficiarioOtro.Text = benef
        txtRFCOtros.Text = ""
        txtMontoOtro.Text = ""
        cmbMonedaOtro.BackColor = Color.White
        cmbMetodoPago.BackColor = Color.White
        txtBeneficiarioOtro.BackColor = Color.White
        txtRFCOtros.BackColor = Color.White
        txtMontoOtro.BackColor = Color.White
        btnAgregarOtro.Text = "Agregar"
        btnEliminarOtro.Enabled = False

        ConsultaOtros()
        cmbMetodoPago.Focus()
    End Sub

    Private Sub btnEliminarOtro_Click(sender As Object, e As EventArgs) Handles btnEliminarOtro.Click
        Try
            If MsgBox("¿Deseas eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                p.EliminarOtro(p.OtrosId)
                nuevoOtro()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    

    Private Sub cmbMetodoPago_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMetodoPago.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFechaOtro.Focus()
        End If

    End Sub

    Private Sub dtpFechaOtro_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFechaOtro.KeyDown
         If e.KeyCode = Keys.Enter Then
            txtBeneficiarioOtro.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbMetodoPago.Focus()
        End If
    End Sub

    Private Sub txtBeneficiarioOtro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBeneficiarioOtro.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRFCOtros.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            dtpFechaOtro.Focus()
        End If
    End Sub

    Private Sub txtRFCOtros_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRFCOtros.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMontoOtro.Focus()

        End If
        If e.KeyCode = Keys.Escape Then
            txtBeneficiarioOtro.Focus()
        End If
    End Sub

    Private Sub txtMontoOtro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMontoOtro.KeyDown
        If e.KeyCode = Keys.Enter Then
            AgregarOtros()

        End If
        If e.KeyCode = Keys.Escape Then
            txtRFCOtros.Focus()
        End If
    End Sub

    Private Sub cmbMonedaOtro_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbMonedaOtro.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtTipoCambioOtro.Enabled = True Then
                txtTipoCambioOtro.Focus()
            Else
                btnAgregarOtro.Focus()
            End If


        End If
        If e.KeyCode = Keys.Escape Then
            txtMontoOtro.Focus()
        End If
    End Sub

    Private Sub txtTipoCambioOtro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTipoCambioOtro.KeyDown
        If e.KeyCode = Keys.Enter Then
            AgregarOtros()
        End If
        If e.KeyCode = Keys.Escape Then
            cmbMonedaOtro.Focus()
        End If
    End Sub

    Private Sub txtTipoCambioCompNac_Leave(sender As Object, e As EventArgs) Handles txtTipoCambioCompNac.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            textBox.Text = Double.Parse(textBox.Text).ToString("0.00")
        End If
    End Sub

    Private Sub txtTipoCambioCompNac2_Leave(sender As Object, e As EventArgs) Handles txtTipoCambioCompNac2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            textBox.Text = Double.Parse(textBox.Text).ToString("0.00")
        End If
    End Sub

    Private Sub txtTipoCambioCompEx_Leave(sender As Object, e As EventArgs) Handles txtTipoCambioCompEx.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            textBox.Text = Double.Parse(textBox.Text).ToString("0.00")
        End If
    End Sub

    Private Sub txtTipoCambioCheque_Leave(sender As Object, e As EventArgs) Handles txtTipoCambioCheque.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            textBox.Text = Double.Parse(textBox.Text).ToString("0.00")
        End If
    End Sub

    Private Sub txtTipoCambioTran_Leave(sender As Object, e As EventArgs) Handles txtTipoCambioTran.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            textBox.Text = Double.Parse(textBox.Text).ToString("0.00")
        End If
    End Sub

    Private Sub txtTipoCambioOtro_Leave(sender As Object, e As EventArgs) Handles txtTipoCambioOtro.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If textBox.Text = "" Then
            textBox.Text = "0.00"
        Else
            textBox.Text = Double.Parse(textBox.Text).ToString("0.00")
        End If
    End Sub

    Private Sub frmContabilidadComprobantes_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim fo As New frmContabilidadAyudaCompro(tbPrincipal.SelectedIndex)
            fo.ShowDialog()
        End If
    End Sub

    Private Sub frmContabilidadComprobantes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress

    End Sub

    Private Sub txtCapturador_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCapturador.KeyDown
        If e.KeyCode = Keys.Enter And txtCapturador.Text <> "" Then
            Dim codigo As String = txtCapturador.Text
            Dim rfc As String = "", cantidad As String = "", uuid As String = ""
            Dim bandera = False
            'RFC
            Try


                For i As Integer = 2 To codigo.Length() - 1
                    If codigo.Chars(i) = "/" And i <> 2 Then
                        i = codigo.Length() - 1
                    Else
                        If bandera = True Then
                            rfc += codigo.Chars(i)
                        End If
                        If codigo.Chars(i) = "¿" Then
                            bandera = True
                        End If
                    End If
                Next
                'cantidad
                bandera = False
                For i As Integer = 35 To codigo.Length() - 1
                    If codigo.Chars(i) = "/" And i <> 35 Then
                        i = codigo.Length() - 1
                    Else
                        If bandera = True Then
                            If (codigo.Chars(i) >= "0" And codigo.Chars(i) <= "9") Or codigo.Chars(i) = "." Then
                                cantidad += codigo.Chars(i)
                            End If
                        End If
                        If codigo.Chars(i) = "¿" Then
                            bandera = True
                        End If
                    End If
                Next
                'UUID
                bandera = False
                For i As Integer = codigo.Length() - 40 To codigo.Length() - 1
                    'If codigo.Chars(i) = "/" And i <> codigo.Length() - 40 Then
                    '    Return
                    'Else
                    If bandera = True Then
                        If codigo.Chars(i) = "'" Then
                            uuid += "-"
                        Else
                            uuid += codigo.Chars(i)
                        End If

                    End If
                    If codigo.Chars(i) = "¿" Then
                        bandera = True
                    End If
                    'End If
                Next
                '_/re¿SARA771001FA0/rr¿PSS100211P40/tt¿$1,160.00/id¿5919262B'2F67'4AB1'8C9D'2861D68B6ED9

                agregar(rfc, cantidad, uuid)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
            End Try
        End If
    End Sub

    Private Sub agregar(ByVal pRFC As String, ByVal pCantidad As Double, ByVal pUUId As String)
        Dim NoErrores As Boolean = True
        Dim mensajeError As String = ""

        For j As Integer = 0 To dgvCompro.RowCount - 1
            If pUUId = dgvCompro.Item(3, j).Value Then
                NoErrores = False
                mensajeError += "UUID repetido: " + pUUId + vbCrLf
            End If
        Next
        If p.contadorUUID(pUUId) = False Then
            NoErrores = False
            mensajeError += "UUID repetido: " + pUUId + vbCrLf
        End If

        If NoErrores Then
            'Dim dr As DataRow

            'dr = tablaCompro.NewRow()
            'dr("id") = contadorCompro
            'dr("idPoliza") = IDPoliza
            'dr("idRenglon") = IDRenglon
            'dr("UUID") = pUUId
            'dr("RFC") = pRFC
            'dr("idMoneda") = IdsMonedaCompNac.Valor(cmbMonedaCompNac.SelectedIndex)
            'dr("Moneda") = cmbMonedaCompNac.Text
            'dr("Tipo_Cambio") = txtTipoCambioCompNac.Text
            'dr("Monto") = pCantidad
            'tablaCompro.Rows.Add(dr)

            'contadorCompro += 1
            'dgvCompro.DataSource = tablaCompro
            txtCapturador.Focus()
            txtCapturador.Text = ""
        Else
            MsgBox(mensajeError, MsgBoxStyle.OkOnly, GlobalNombreApp)
            txtCapturador.Focus()
            txtCapturador.SelectAll()
        End If

    End Sub

    Private Sub txtMontoTotalCompEx_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMontoTotalCompEx.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
        If e.KeyChar = "-" Then
            If txtMontoCompro.Text <> "" Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub txtMontoOtro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMontoOtro.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
        If e.KeyChar = "-" Then
            If txtMontoCompro.Text <> "" Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub dgvCompro_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompro.CellClick
        Try
            If e.RowIndex >= 0 Then
                p.ComprobanteId = dgvCompro.Item(0, e.RowIndex).Value
                p.LlenaDatosComprobante(p.ComprobanteId)
                txtUUID.Text = p.ComprobanteUUID
                txtMontoCompro.Text = p.ComprobanteMonto.ToString
                txtRFCCompro.Text = p.ComprobanteRFC
                cmbMonedaCompNac.SelectedIndex = IdsMonedaCompNac.Busca(p.ComprobanteMoneda)
                txtTipoCambioCompNac.Text = p.ComprobanteTipodeCambio.ToString
                btnAgregarCompro.Text = "Modificar"
                btnEliminarCompro.Enabled = True
                txtUUID.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

   

   
    Private Sub dgvCompro_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvCompro.CellFormatting
        If e.ColumnIndex = 2 Then
            e.Value = Format(Double.Parse(e.Value), "###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComproNac2.CellClick
        Try
            If e.RowIndex >= 0 Then
                p.Comprobante2Id = dgvComproNac2.Item(0, e.RowIndex).Value
                p.LlenaDatosComprobanteNa2(p.Comprobante2Id)
                txtSerieCompNac2.Text = p.Comprobante2Serie
                txtFolioCompNac2.Text = p.Comprobante2Folio.ToString
                txtRFCCompNac2.Text = p.Comprobante2RFC
                txtMontoComNac2.Text = p.Comprobante2Monto
                cmbMonedaCompNac2.SelectedIndex = IdsMonedaCompNac2.Busca(p.Comprobante2Moneda)
                txtTipoCambioCompNac2.Text = p.Comprobante2TipodeCambio.ToString
                btnAgregarCompNac2.Text = "Modificar"
                btnEliminarCompNac2.Enabled = True
                txtSerieCompNac2.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub dgvComproNac2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvComproNac2.CellFormatting
        If e.ColumnIndex = 4 Then
            e.Value = Format(Double.Parse(e.Value), "###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

   

    Private Sub dgvComproE_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvComproE.CellFormatting
        If e.ColumnIndex = 3 Or e.ColumnIndex = 5 Then
            e.Value = Format(Double.Parse(e.Value), "###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub dgvCheques_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvCheques.CellFormatting
        If e.ColumnIndex = 5 Then
            e.Value = Format(Double.Parse(e.Value), "###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub dgvCheques_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCheques.CellClick
        Try
            If e.RowIndex >= 0 Then
                p.ChequeId = dgvCheques.Item(0, e.RowIndex).Value
                p.LlenaDatosCheque(p.ChequeId)
                txtNumeroCheque.Text = p.ChequeNunero
                cmbBancoCheque.SelectedIndex = IdsBancosCheque.Busca(p.ChequeBanco)
                cmbCuentaOrigenCheque.Text = p.ChequeCtaOri
                dtpFechaCheque1.Value = p.ChequeFehca
                txtBeneficiarioCheque.Text = p.ChequeBenef
                txtMontoCheque.Text = p.ChequeMonto.ToString
                txtRFCCheque.Text = p.ChequeRFC
                cmbMonedaCheque.SelectedIndex = IdsMonedaCheque.Busca(p.ChequeMoneda)
                txtTipoCambioCheque.Text = p.ChequeTipoCambio.ToString
                txtBancoEmisorExtranjero.Text = p.ChequeBancoEx
                btnAgregarCheque.Text = "Modificar"
                btnEliminarCheque.Enabled = True
                txtNumeroCheque.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub dgvTrans_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvTrans.CellFormatting
        If e.ColumnIndex = 4 Then
            e.Value = Format(Double.Parse(e.Value), "###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub dgvTrans_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTrans.CellClick
        Try
            If e.RowIndex >= 0 Then
                p.TransferenciaId = dgvTrans.Item(0, e.RowIndex).Value
                p.LlenaDatosTransferencia(p.TransferenciaId)
                cmbBancoOrigenTrans.SelectedIndex = IdsBancosTransO.Busca(p.TransBancoOri)
                txtCuentaDestinoTrans.Text = p.TransCtaDest
                cmbBancoDestinoTrans.SelectedIndex = IdsBancosTransD.Busca(p.TransBancoDest)
                dtpFechaTrans1.Value = p.TransFecha
                txtBeneficiaTrans.Text = p.TransBenefi
                txtRFCTrans.Text = p.TransRFC
                txtMontoTrans.Text = p.TransMonto.ToString
                cmbcuentaOrigenTrans.Text = p.TransCaOri
                txtBancoOriExTrans.Text = p.TransBancoOriE
                txtBancoDesExTrans.Text = p.TransBancoDestE
                cmbMonedaTrans.SelectedIndex = IdsMonedaTra.Busca(p.TransMoneda)
                txtTipoCambioTran.Text = p.TransTipoCambio.ToString
                btnAgregarTrans.Text = "Modificar"
                btnEliminarTrans.Enabled = True
                cmbBancoOrigenTrans.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub dgvOtros_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvOtros.CellFormatting
        If e.ColumnIndex = 4 Then
            e.Value = Format(Double.Parse(e.Value), "###,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub dgvOtros_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOtros.CellClick
        Try
            If e.RowIndex >= 0 Then
                p.OtrosId = dgvOtros.Item(0, e.RowIndex).Value
                p.LlenaDatosOtros(p.OtrosId)
                cmbMetodoPago.SelectedIndex = IdsMetodoPago.Busca(p.OtrosMetodoPago)
                dtpFechaOtro.Value = p.OtrosFecha
                txtBeneficiarioOtro.Text = p.OtrosBenef
                txtMontoOtro.Text = p.OtrosMonto.ToString
                txtRFCOtros.Text = p.OtrosRFC
                txtTipoCambioOtro.Text = p.OtrosTipoCambio.ToString
                cmbMonedaOtro.SelectedIndex = IdsMonedaOtro.Busca(p.OtrosMoneda)
                btnAgregarOtro.Text = "Modificar"
                btnEliminarOtro.Enabled = True
                cmbMetodoPago.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

   
    Private Sub dgvComproE_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComproE.CellClick
        Try
            If e.RowIndex >= 0 Then
                p.ComprobanteEId = dgvComproE.Item(0, e.RowIndex).Value
                p.LlenaDatosComprobanteE(p.ComprobanteEId)
                txtNumFacturaCompEx.Text = p.ComprobanteEFolio
                txtTaxIDCompEx.Text = p.ComprobanteETax
                txtMontoTotalCompEx.Text = p.ComprobanteEMonto.ToString
                cmbMonedaCompEx.SelectedIndex = IdsMonedaCompEx.Busca(p.ComprobanteEMoneda)
                txtTipoCambioCompEx.Text = p.ComprobanteETipodeCambio.ToString
                btnAgregarCompEx.Text = "Modificar"
                btnEliminarCompEx.Enabled = True
                txtNumeroCheque.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(OpenFileDialog1.FileName)
                txtUUID.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                txtMontoCompro.Text = xmldoc.Item("cfdi:Comprobante").Attributes("total").Value
                txtRFCCompro.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Emisor").Attributes("rfc").Value
                agregarComprobante()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            End Try
        End If
    End Sub
End Class