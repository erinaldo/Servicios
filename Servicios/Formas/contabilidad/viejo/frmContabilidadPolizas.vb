Public Class frmContabilidadPolizas
    Dim p As New dbContabilidadPolizas(MySqlcon)
    Dim C As New dbContabilidadClasificacion(MySqlcon)
    Dim prov As New dbproveedores(MySqlcon)
    Dim Tabla As New DataTable
    Dim Tabla2 As DataTable
    Dim idCuenta As Integer = 0
    Dim idTabla As Integer = 0
    Dim idPoliza As Integer = 0
    Dim auxNiv5 As Boolean
    Dim volverVer As Boolean = False
    Dim pregunta As Boolean = False
    Dim bandera As Boolean = False
    Dim TipoImpresora As Byte
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim inicial As Integer = 1
    Dim fechaAux As String = ""
    Dim tipoFecha As Integer = 0
    Dim bDay As Boolean
    Dim bMonth As Boolean
    Dim bYear As Boolean
    Dim fechaNum As Integer = 0
    Dim primera As Boolean = False
    Dim periodo As String
    Dim nombreProv As String
    Dim tablaImpres As New DataTable
    Dim n1 As String
    Dim n2 As String
    Dim n3 As String
    Dim n4 As String
    Dim n5 As String
    Dim nombreConcepto As String
    Dim parcial As Double
    Dim cargo As Double
    Dim abono As Double
    Dim descripcion As String
    Dim UltimaFila As Integer
    Dim totalCargo As Double
    Dim totalAbono As Double
    Dim llenando As Boolean
    Dim contadorFilas As Integer
    Dim tablaChequesAUX As New DataTable
    Dim tablaTransAUX As New DataTable
    Dim tablaComproAUX As New DataTable
    Dim tblCheques As New DataTable
    Dim tblTrans As New DataTable
    Dim tblCompro As New DataTable
    Dim tablaComproNac2Aux As New DataTable
    Dim tablaComproNac2 As New DataTable
    Dim tablaComproEAux As New DataTable
    Dim tablaComproE As New DataTable
    Dim tablaOtrosAux As New DataTable
    Dim tablaOtros As New DataTable
    Dim IdsClasificacionPoliza As New elemento
    Dim IdsClasificacionPolizaBusqeda As New elemento
    Dim mayorCheque As Integer
    Dim mayorTrans As Integer
    Dim mayorCompro As Integer
    Dim mayorcomproN As Integer
    Dim mayorcomproE As Integer
    Dim mayorOtro As Integer
    Dim idRenglon As Integer
    Dim rellenando As Boolean = False
    Dim PreguntarCuadrada As Byte
    'Dim idProveedor As Integer
    Dim IdsProveedores As New elemento
    Dim noFiltro As Boolean
    Dim IdPolizaconsuta As Integer
    Dim FechaLimite As String
    Dim ConsultaOn As Boolean = True
    Dim SalirAlImprimir As Boolean


    Public Sub New(pIdPoliza As Integer, pSalirAlImprimir As Boolean)
        llenando = True
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdPolizaconsuta = pIdPoliza
        SalirAlImprimir = pSalirAlImprimir
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmContabilidadPolizas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
            PictureBox1.Image = Image.FromFile(Application.StartupPath + "\iconos\imgtextBox.png")
        Catch ex As Exception

        End Try
        Try
            LlenaCombos("tblproveedores", cmbProveedores, "nombre", "nombrep", "idproveedor", IdsProveedores, "tipo=1")
            llenando = False
            noFiltro = False
            cmbTipoPolizaX.Items.Add("E Egreso")
            cmbTipoPolizaX.Items.Add("I Ingreso")
            cmbTipoPolizaX.Items.Add("D Diario")
            cmbTipoPolizaX.Items.Add("A Apertura")
            cmbTipoBuscar.Items.Add("Todos")
            cmbTipoBuscar.Items.Add("E Egreso")
            cmbTipoBuscar.Items.Add("I Ingreso")
            cmbTipoBuscar.Items.Add("D Diario")
            cmbTipoBuscar.Items.Add("A Apertura")
            'e Egreso
            'I Ingreso
            'D Diario
            'A Apertura
            cmbTipoPolizaX.SelectedIndex = 0
            cmbTipoBuscar.SelectedIndex = 0
            periodo = p.buscarPeriodo()

            'dtpHasta.Value = Date.Now.Day.ToString + "/" + Date.Now.Month.ToString + "/" + periodo
            LlenaCombos("tblcontabilidadclas", cmbClasificacionPoliza, "nombre", "nombret", "id", IdsClasificacionPoliza, , , "nombre")
            LlenaCombos("tblcontabilidadclas", cmbClasBusqueda, "nombre", "nombret", "id", IdsClasificacionPolizaBusqeda, , "TODOS", "nombre")
            cmbTipoPolizaX.Focus()
            tablaImpres.Columns.Add("N1")
            tablaImpres.Columns.Add("N2")
            tablaImpres.Columns.Add("N3")
            tablaImpres.Columns.Add("N4")
            tablaImpres.Columns.Add("N5")
            tablaImpres.Columns.Add("desCuenta")
            tablaImpres.Columns.Add("descripcion")
            tablaImpres.Columns.Add("parcial")
            tablaImpres.Columns.Add("Cargo")
            tablaImpres.Columns.Add("Abono")

            Tabla.Columns.Add("id")
            Tabla.Columns.Add("idCuenta")
            Tabla.Columns.Add("Cuenta")
            Tabla.Columns.Add("descripcion")
            Tabla.Columns.Add("Cargo")
            Tabla.Columns.Add("Abono")
            Tabla.Columns.Add("factura")
            Tabla.Columns.Add("idProveedor")
            Tabla.Columns.Add("iva")
            Tabla.Columns.Add("concepto")
            Tabla.Columns.Add("esDIOT")
            Tabla.Columns.Add("ValorActos")
            Tabla.Columns.Add("referencia")
            Tabla.Columns.Add("ivaret")
            Tabla.Columns.Add("ieps")
            dgvCuentas.DataSource = Tabla
            dgvCuentas.Columns(0).Visible = False
            dgvCuentas.Columns(1).Visible = False
            dgvCuentas.Columns(6).Visible = False
            dgvCuentas.Columns(7).Visible = False
            dgvCuentas.Columns(8).Visible = False
            dgvCuentas.Columns(9).Visible = False
            dgvCuentas.Columns(10).Visible = False
            dgvCuentas.Columns(11).Visible = False
            dgvCuentas.Columns(12).Visible = False
            dgvCuentas.Columns(13).Visible = False
            dgvCuentas.Columns(14).Visible = False
            dgvCuentas.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            dgvCuentas.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            dgvCuentas.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            dgvCuentas.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            dgvCuentas.Columns(2).HeaderText = "Cuenta"
            dgvCuentas.Columns(3).HeaderText = "Descripción"
            dgvCuentas.Columns(4).HeaderText = "Cargo"
            dgvCuentas.Columns(5).HeaderText = "Abono"

            'dgvCuentas.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            'dgvCuentas.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            'dgvCuentas.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            'dgvCuentas.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            'dgvCuentas.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            dgvCuentas.Columns(2).Width = 170
            dgvCuentas.Columns(4).Width = 85
            dgvCuentas.Columns(5).Width = 85
            dgvCuentas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            dgvCuentas.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            dgvCuentas.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


            p.llenaDatosConfig()
            txtCuenta.MaxLength = p.NNiv1
            txtN2.MaxLength = p.NNiv2
            txtN3.MaxLength = p.NNiv3
            txtN4.MaxLength = p.NNiv4
            txtN5.MaxLength = p.NNiv5

            'dtpFecha.MinDate = periodo + "/01/01"
            'dtpFecha.MaxDate = periodo + "/12/31"
            FechaLimite = periodo
            'txtFecha1.Text = dtpFecha.Value.ToString("dd/MM/yyyy")
            Try
                If p.ActivarFechaTrabajo = 0 Then
                    dtpDesde.Value = "01/" + Date.Now.Month.ToString + "/" + periodo
                Else
                    dtpDesde.Value = Format(CDate(p.FechaTRabajo), "yyyy/MM/01")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

            'dtpDesde.MinDate = periodo + "/01/01"
            'dtpDesde.MaxDate = periodo + "/12/31"

            'dtpHasta.MinDate = periodo + "/01/01"
            'dtpHasta.MaxDate = periodo + "/12/31"

            llenarTablas()
            nuevo()
            'dtpFecha.Value = Date.Now.Day.ToString("00") + "/" + Date.Now.Month.ToString("00") + "/" + periodo
            noFiltro = True
            Consulta()
            grpDatosPoliza.Focus()
            chkMantenerFecha.Focus()

            cmbTipoPolizaX.Focus()

            If cmbClasificacionPoliza.Items.Count = 0 Then
                MsgBox("Es necesario dar de alta al menos una clasificación de pólizas.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                grpDatosPoliza.Enabled = False
                btnGuardarPoliza.Enabled = False
            End If

            If IdPolizaconsuta <> 0 Then
                LlenaDatosPoliza(IdPolizaconsuta)
                If SalirAlImprimir Then Imprimir(True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub llenarTablas()
        'llenaTablaCheques
        tablaChequesAUX.Columns.Add("id")
        tablaChequesAUX.Columns.Add("idPoliza")
        tablaChequesAUX.Columns.Add("idRenglon")
        tablaChequesAUX.Columns.Add("Numero")
        tablaChequesAUX.Columns.Add("BancoCod")
        tablaChequesAUX.Columns.Add("Banco")
        tablaChequesAUX.Columns.Add("Banco_extranjero")
        tablaChequesAUX.Columns.Add("CuentaOri")
        tablaChequesAUX.Columns.Add("Fecha")
        tablaChequesAUX.Columns.Add("Beneficiario")
        tablaChequesAUX.Columns.Add("RFC")
        tablaChequesAUX.Columns.Add("Monto")
        tablaChequesAUX.Columns.Add("idMoneda")
        tablaChequesAUX.Columns.Add("Moneda")
        tablaChequesAUX.Columns.Add("Tipo_cambio")

        tblCheques.Columns.Add("id")
        tblCheques.Columns.Add("idPoliza")
        tblCheques.Columns.Add("idRenglon")
        tblCheques.Columns.Add("Numero")
        tblCheques.Columns.Add("BancoCod")
        tblCheques.Columns.Add("Banco")
        tblCheques.Columns.Add("Banco_extranjero")
        tblCheques.Columns.Add("CuentaOri")
        tblCheques.Columns.Add("Fecha")
        tblCheques.Columns.Add("Beneficiario")
        tblCheques.Columns.Add("RFC")
        tblCheques.Columns.Add("Monto")
        tblCheques.Columns.Add("idMoneda")
        tblCheques.Columns.Add("Moneda")
        tblCheques.Columns.Add("Tipo_cambio")
        tblCheques.Clear()
        'llenaTablaTrans

        tablaTransAUX.Columns.Add("id")
        tablaTransAUX.Columns.Add("idPoliza")
        tablaTransAUX.Columns.Add("idRenglon")
        tablaTransAUX.Columns.Add("CuentaOri")
        tablaTransAUX.Columns.Add("BancoCodO")
        tablaTransAUX.Columns.Add("BancoOri")
        tablaTransAUX.Columns.Add("Banco_extranjero_O")
        tablaTransAUX.Columns.Add("CuentaDest")
        tablaTransAUX.Columns.Add("BancoCodD")
        tablaTransAUX.Columns.Add("BancoDest")
        tablaTransAUX.Columns.Add("Banco_extranjero_D")
        tablaTransAUX.Columns.Add("Fecha")
        tablaTransAUX.Columns.Add("Beneficiario")
        tablaTransAUX.Columns.Add("RFC")
        tablaTransAUX.Columns.Add("Monto")
        tablaTransAUX.Columns.Add("idMoneda")
        tablaTransAUX.Columns.Add("Moneda")
        tablaTransAUX.Columns.Add("tipo_Cambio")

        tblTrans.Columns.Add("id")
        tblTrans.Columns.Add("idPoliza")
        tblTrans.Columns.Add("idRenglon")
        tblTrans.Columns.Add("CuentaOri")
        tblTrans.Columns.Add("BancoCodO")
        tblTrans.Columns.Add("BancoOri")
        tblTrans.Columns.Add("Banco_extranjero_O")
        tblTrans.Columns.Add("CuentaDest")
        tblTrans.Columns.Add("BancoCodD")
        tblTrans.Columns.Add("BancoDest")
        tblTrans.Columns.Add("Banco_extranjero_D")
        tblTrans.Columns.Add("Fecha")
        tblTrans.Columns.Add("Beneficiario")
        tblTrans.Columns.Add("RFC")
        tblTrans.Columns.Add("Monto")
        tblTrans.Columns.Add("idMoneda")
        tblTrans.Columns.Add("Moneda")
        tblTrans.Columns.Add("tipo_Cambio")
        tblTrans.Clear()
        'llenaTablaCompro

        tablaComproAUX.Columns.Add("id")
        tablaComproAUX.Columns.Add("idPoliza")
        tablaComproAUX.Columns.Add("idRenglon")
        tablaComproAUX.Columns.Add("UUID")
        tablaComproAUX.Columns.Add("RFC")
        tablaComproAUX.Columns.Add("idMoneda")
        tablaComproAUX.Columns.Add("Moneda")
        tablaComproAUX.Columns.Add("Tipo_Cambio")
        tablaComproAUX.Columns.Add("Monto")


        tblCompro.Columns.Add("id")
        tblCompro.Columns.Add("idPoliza")
        tblCompro.Columns.Add("idRenglon")
        tblCompro.Columns.Add("UUID")
        tblCompro.Columns.Add("RFC")
        tblCompro.Columns.Add("idMoneda")
        tblCompro.Columns.Add("Moneda")
        tblCompro.Columns.Add("Tipo_Cambio")
        tblCompro.Columns.Add("Monto")
        tblCompro.Clear()

        'Comprobantes nacionales CFD/CBB
        tablaComproNac2Aux.Columns.Add("id")
        tablaComproNac2Aux.Columns.Add("idPoliza")
        tablaComproNac2Aux.Columns.Add("idRenglon")
        tablaComproNac2Aux.Columns.Add("Serie")
        tablaComproNac2Aux.Columns.Add("Folio")
        tablaComproNac2Aux.Columns.Add("RFC")
        tablaComproNac2Aux.Columns.Add("Monto")
        tablaComproNac2Aux.Columns.Add("idMoneda")
        tablaComproNac2Aux.Columns.Add("Moneda")
        tablaComproNac2Aux.Columns.Add("Tipo_Cambio")

        tablaComproNac2.Columns.Add("id")
        tablaComproNac2.Columns.Add("idPoliza")
        tablaComproNac2.Columns.Add("idRenglon")
        tablaComproNac2.Columns.Add("Serie")
        tablaComproNac2.Columns.Add("Folio")
        tablaComproNac2.Columns.Add("RFC")
        tablaComproNac2.Columns.Add("Monto")
        tablaComproNac2.Columns.Add("idMoneda")
        tablaComproNac2.Columns.Add("Moneda")
        tablaComproNac2.Columns.Add("Tipo_Cambio")
        tablaComproNac2.Clear()

        'Comprobantes extranjeros
        tablaComproEAux.Columns.Add("id")
        tablaComproEAux.Columns.Add("idPoliza")
        tablaComproEAux.Columns.Add("idRenglon")
        tablaComproEAux.Columns.Add("Num_Factura")
        tablaComproEAux.Columns.Add("taxID")
        tablaComproEAux.Columns.Add("Monto")
        tablaComproEAux.Columns.Add("idMoneda")
        tablaComproEAux.Columns.Add("Moneda")
        tablaComproEAux.Columns.Add("Tipo_Cambio")

        tablaComproE.Columns.Add("id")
        tablaComproE.Columns.Add("idPoliza")
        tablaComproE.Columns.Add("idRenglon")
        tablaComproE.Columns.Add("Num_Factura")
        tablaComproE.Columns.Add("taxID")
        tablaComproE.Columns.Add("Monto")
        tablaComproE.Columns.Add("idMoneda")
        tablaComproE.Columns.Add("Moneda")
        tablaComproE.Columns.Add("Tipo_Cambio")
        tablaComproE.Clear()

        'tablaOtros
        tablaOtrosAux.Columns.Add("id")
        tablaOtrosAux.Columns.Add("idPoliza")
        tablaOtrosAux.Columns.Add("idRenglon")
        tablaOtrosAux.Columns.Add("idMetodoPago")
        tablaOtrosAux.Columns.Add("Metodo_Pago")
        tablaOtrosAux.Columns.Add("Fecha")
        tablaOtrosAux.Columns.Add("Beneficiario")
        tablaOtrosAux.Columns.Add("RFC")
        tablaOtrosAux.Columns.Add("Monto")
        tablaOtrosAux.Columns.Add("idMoneda")
        tablaOtrosAux.Columns.Add("Moneda")
        tablaOtrosAux.Columns.Add("Tipo_Cambio")

        tablaOtros.Columns.Add("id")
        tablaOtros.Columns.Add("idPoliza")
        tablaOtros.Columns.Add("idRenglon")
        tablaOtros.Columns.Add("idMetodoPago")
        tablaOtros.Columns.Add("Metodo_Pago")
        tablaOtros.Columns.Add("Fecha")
        tablaOtros.Columns.Add("Beneficiario")
        tablaOtros.Columns.Add("RFC")
        tablaOtros.Columns.Add("Monto")
        tablaOtros.Columns.Add("idMoneda")
        tablaOtros.Columns.Add("Moneda")
        tablaOtros.Columns.Add("Tipo_Cambio")
        tablaOtros.Clear()


    End Sub
    Private Sub txtNumeroPoliza_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumeroPoliza.KeyPress
        ' Dim textBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = "+" Then
            txtNumeroPoliza.Text = ""
            txtNumeroPoliza.Text = p.bucarNumero(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbTipoPolizaX.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
            e.Handled = True
            'txtFecha1.Focus()
            'txtFecha1.Select(0, 0)
            'txtFecha1.SelectionStart = 0
            dtpFecha.Focus()
        Else
            If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
                dtpFecha.Focus()
            Else
                If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
                    e.Handled = True
                End If
            End If

        End If


    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        llenando = True
        'txtFecha1.Text = dtpFecha.Value.ToString("dd/MM/yyyy")
        txtNumeroPoliza.Text = p.bucarNumero(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbTipoPolizaX.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
        ' txtConcepto.Focus()
        llenando = False
    End Sub

    Private Sub cmbTipoPoliza_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cmbtipopolizax.SelectedIndex = 0 Then
            lblBeneficiario.Visible = True
            txtBeneficiario.Visible = True
        Else
            lblBeneficiario.Visible = False
            txtBeneficiario.Visible = False
        End If
        txtNumeroPoliza.Text = p.bucarNumero(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbtipopolizax.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
        ' txtConcepto.Focus()
    End Sub

    Private Sub txtBeneficiario_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBeneficiario.TextChanged

    End Sub

    Private Sub cmbTipoPoliza_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '  Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            If dtpFecha.Enabled = True Then
                dtpFecha.Focus()
            Else
                txtConcepto.Focus()
            End If

        End If
    End Sub

    Private Sub txtConcepto_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConcepto.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            If txtBeneficiario.Visible = True Then
                txtBeneficiario.Focus()
            Else
                txtCuenta.Focus()
            End If

        End If
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Escape) Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub txtBeneficiario_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBeneficiario.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            txtCuenta.Focus()
        End If
    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        NuevoDetalle2()
        nuevo()
    End Sub
    Private Sub nuevo()
        nuevoDetalle(False)
        txtConcepto.Text = ""
        txtBeneficiario.Text = ""
        txtCuenta.Text = ""
        txtDesc.Text = ""
        lblDescripcion.Text = ""
        lblN2.Text = ""
        lblN3.Text = ""
        lblN4.Text = ""
        lblN5.Text = ""
        TextBox1.Text = ""
        nombreConcepto = ""
        Tabla.Clear()
        If chkMantenerFecha.Checked = False Then
            If p.ActivarFechaTrabajo = 1 Then
                dtpFecha.Value = CDate(p.FechaTRabajo)
                'txtFecha1.Text = dtpFecha.Value.ToString("dd/MM/yyyy")
            Else
                'dtpFecha.Value = Date.Now.ToString("dd/MM/" + periodo)
                'txtFecha1.Text = dtpFecha.Value.ToString("dd/MM/yyyy")
            End If
        End If

        txtNumeroPoliza.Text = p.bucarNumero(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbTipoPolizaX.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
        btnImprimir.Enabled = False
        txtNumeroPoliza.BackColor = Color.White
        txtConcepto.BackColor = Color.White
        txtBeneficiario.BackColor = Color.White

        dgvCuentas.DataSource = Tabla
        dgvCuentas.Columns(0).Visible = False
        dgvCuentas.Columns(1).Visible = False
        dgvCuentas.Columns(6).Visible = False
        dgvCuentas.Columns(7).Visible = False
        dgvCuentas.Columns(8).Visible = False
        dgvCuentas.Columns(9).Visible = False
        dgvCuentas.Columns(10).Visible = False
        dgvCuentas.Columns(11).Visible = False
        dgvCuentas.Columns(12).Visible = False
        dgvCuentas.Columns(13).Visible = False
        dgvCuentas.Columns(14).Visible = False
        dgvCuentas.Columns(2).Width = 170
        dgvCuentas.Columns(4).Width = 85
        dgvCuentas.Columns(5).Width = 85
        dgvCuentas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        dgvCuentas.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        dgvCuentas.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvCuentas.Columns(2).HeaderText = "Cuenta"
        dgvCuentas.Columns(3).HeaderText = "Descripción"
        dgvCuentas.Columns(4).HeaderText = "Cargo"
        dgvCuentas.Columns(5).HeaderText = "Abono"
        ' nuevoDetalle()
        'filtro()
        If cmbClasificacionPoliza.Items.Count > 0 Then
            cmbClasificacionPoliza.SelectedIndex = 0
        End If
        calculos()
        btnGuardarPoliza.Text = "Guardar (F10)"
        btnEliminar.Enabled = False
        grpDatosPoliza.Focus()
        If chkMantenerFecha.Checked = False Then
            'chkMantenerFecha.Focus()
            dtpFecha.Focus()
        Else
            'chkMantenerFecha.Focus()
            txtConcepto.Focus()
        End If
        idPoliza = -1
        ' If tablaChequesAUX.Rows.Count <= 0 Then
        tablaChequesAUX.Clear()
        ' End If
        ' If tablaComproAUX.Rows.Count <= 0 Then
        tablaComproAUX.Clear()
        ' End If
        ' If tablaTransAUX.Rows.Count <= 0 Then
        tablaTransAUX.Clear()
        tblCheques.Clear()
        tblCompro.Clear()
        tblTrans.Clear()
        ' End If
        dtpFecha.Focus()
        contadorFilas = 0
    End Sub

    Private Sub txtCargo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCargo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If txtCargo.Text = "" Then
                txtAbono.Focus()
            Else
                botonAgregar()
            End If

        Else
            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0) Or (e.KeyChar = "-" And textBox.Text.IndexOf("-") < 0)) Then
                e.Handled = True
            End If
            'If e.KeyChar = "-" Then
            '    If txtCargo.Text <> "" Then
            '        e.Handled = True
            '    Else
            '        e.Handled = False
            '    End If
            'End If

        End If

    End Sub

    Private Sub txtCargo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCargo.Leave
        Dim x As Double
        If txtCargo.Text = "." Then
            txtCargo.Text = "0.00"
        End If
        If txtCargo.Text = "" Then
            ' txtCargo.Text = "0.00"
        Else
            If txtCargo.Text = "-" Then
                txtCargo.Text = "-0.00"
            Else
                If txtCargo.Text = "-." Then
                    txtCargo.Text = "-0.00"
                Else
                    x = Double.Parse(txtCargo.Text)
                    txtCargo.Text = Format(x, "0.00")
                End If
            End If

        End If
        If txtCargo.Text = "-999999999" Then
            MsgBox("Cantidad no válida", MsgBoxStyle.Information, "Pull System Soft")
            txtCargo.Text = ""
        End If
    End Sub

    Private Sub txtAbono_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAbono.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Dim x As Double
            If txtAbono.Text = "." Then
                txtAbono.Text = "0.00"
            End If
            If txtAbono.Text = "" Then
                '   txtAbono.Text = "0.00"
            Else
                x = Double.Parse(txtAbono.Text)
                txtAbono.Text = Format(x, "0.00")
            End If
            botonAgregar()
        Else

            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0) Or (e.KeyChar = "-" And textBox.Text.IndexOf("-") < 0)) Then
                e.Handled = True
            End If
            'If e.KeyChar = "-" Then
            '    If txtCargo.Text <> "" Then
            '        e.Handled = True
            '    Else
            '        e.Handled = False
            '    End If
            'End If

        End If
        If txtAbono.Text = "-999999999" Then
            MsgBox("Cantidad no válida", MsgBoxStyle.Information, "Pull System Soft")
            txtAbono.Text = ""
        End If
    End Sub

    Private Sub txtAbono_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbono.Leave
        Dim x As Double
        If txtAbono.Text = "." Then
            txtAbono.Text = "0.00"
        End If
        If txtAbono.Text = "" Then
            '   txtAbono.Text = "0.00"
        Else
            If txtAbono.Text = "-" Then
                txtAbono.Text = "-0.00"
            Else
                If txtAbono.Text = "-." Then
                    txtAbono.Text = "-0.00"
                Else
                    x = Double.Parse(txtAbono.Text)
                    txtAbono.Text = Format(x, "0.00")
                End If
            End If


        End If
    End Sub

    Private Sub btnBuscarCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim B As New frmBuscadorCC()
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtCuenta.Text = B.Cuenta.ToString()
            ' txtDesc.Text = B.descripcion.ToString()
            txtDesc.Focus()
            lblDescripcion.Text = B.descripcion
            idCuenta = B.ID
        End If
    End Sub
    Private Sub verificaCuenta(ByVal verificar As Boolean, pDonde As Byte)
        'If rellenando = False Then


        '    auxNiv5 = True
        '    Dim N1 As String = ""
        '    Dim N2 As String = ""
        '    Dim N3 As String = ""
        '    Dim N4 As String = ""
        '    Dim N5 As String = ""
        '    Dim niv As Integer = 1
        '    Dim aux As String = ""
        '    lblDescripcion.Text = ""
        '    lblN2.Text = ""
        '    lblN3.Text = ""
        '    lblN4.Text = ""
        '    lblN5.Text = ""
        '    N1 = quitarCeros(txtCuenta.Text)
        '    idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
        '    lblDescripcion.Text = p.descripcionCuenta
        '    If txtN2.Text <> "" Then
        '        N2 = quitarCeros(txtN2.Text)
        '        niv += 1
        '        idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
        '        lblN2.Text = p.descripcionCuenta
        '    End If
        '    If txtN3.Text <> "" Then
        '        N3 = quitarCeros(txtN3.Text)
        '        niv += 1
        '        idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
        '        lblN3.Text = p.descripcionCuenta
        '    End If
        '    If txtN4.Text <> "" Then
        '        N4 = quitarCeros(txtN4.Text)
        '        niv += 1
        '        idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
        '        lblN4.Text = p.descripcionCuenta
        '    End If
        '    If txtN5.Text <> "" Then
        '        N5 = quitarCeros(txtN5.Text)
        '        niv += 1
        '        idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
        '        lblN5.Text = p.descripcionCuenta
        '    End If
        '    ' idCuenta = p.BuscarIdCuenta(niv, N1, N2, N4, N4, N5)
        '    volverVer = False
        '    If verificar Then
        '        If p.ultimoNivel(N1, N2, N3, N4, N5, niv) = 0 And idCuenta <> 0 Then
        '            'lblDescripcion.Text = p.descripcionCuenta
        '            'txtDesc.Focus()
        '        Else
        '            MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
        '            pregunta = True
        '            If ConsultaOn Then
        '                If niv = 1 Then
        '                    txtCuenta.Focus()
        '                Else
        '                    If niv = 2 Then
        '                        txtN3.Focus()
        '                    Else
        '                        If niv = 3 Then
        '                            txtN4.Focus()
        '                        Else
        '                            If niv = 4 Then
        '                                txtN5.Focus()
        '                            Else
        '                                txtN5.Focus()
        '                            End If
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        End If
        '    Else
        '        If idCuenta = 0 Then

        '            If MsgBox("La cuenta proporcionada no existe. ¿Desea Registrarla?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '                altaCuenta(N1, N2, N3, N4, N5)

        '            Else


        '                pregunta = True
        '                volverVer = True
        '                If ConsultaOn Then
        '                    If niv = 1 Then
        '                        txtCuenta.Focus()
        '                        txtCuenta.SelectAll()
        '                    Else
        '                        If niv = 2 Then
        '                            txtN2.Focus()
        '                            txtN2.SelectAll()
        '                        Else
        '                            If niv = 3 Then
        '                                txtN3.Focus()
        '                                txtN3.SelectAll()
        '                            Else
        '                                If niv = 4 Then
        '                                    txtN4.Focus()
        '                                    txtN4.SelectAll()
        '                                Else
        '                                    txtN5.Focus()
        '                                    txtN5.SelectAll()
        '                                    auxNiv5 = False
        '                                End If
        '                            End If
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        Else
        '            Dim UL As Integer
        '            UL = p.ultimoNivel(N1, N2, N3, N4, N5, niv)
        '            If UL = 0 And idCuenta <> 0 Then
        '                If p.esDIOT(idCuenta) Then
        '                    'dgvCuentas.Size = New Size(1016, 198)
        '                    'dgvCuentas.Location = New Point(14, 279)
        '                    pnlDatosIVA.Visible = True
        '                    If ConsultaOn Then txtFolioFactura.Focus()
        '                Else
        '                    'dgvCuentas.Size = New Size(1016, 212)
        '                    'dgvCuentas.Location = New Point(14, 265)
        '                    pnlDatosIVA.Visible = False
        '                    If ConsultaOn Then
        '                        If pDonde = 1 And N2 <> "" Then
        '                            txtN2.Focus()
        '                        Else
        '                            If pDonde = 2 And N3 <> "" Then
        '                                txtN3.Focus()
        '                            Else
        '                                If pDonde = 3 And N4 <> "" Then
        '                                    txtN4.Focus()
        '                                Else
        '                                    If pDonde = 4 And N5 <> "" Then
        '                                        txtN5.Focus()
        '                                    Else
        '                                        If txtDesc.Text <> "" Then
        '                                            txtDesc.Focus()
        '                                            txtDesc.SelectAll()
        '                                        Else
        '                                            txtDesc.Focus()
        '                                        End If
        '                                    End If
        '                                End If
        '                            End If
        '                        End If
        '                    End If
        '                    txtFolioFactura.Text = ""
        '                    cmbProveedores.Text = ""
        '                    txtCocepFac.Text = ""
        '                    txtIVA.Text = ""

        '                End If
        '            Else
        '                pnlDatosIVA.Visible = False
        '            End If
        '        End If

        '    End If


        'End If

        If rellenando = False Then


            auxNiv5 = True
            Dim N1 As String = ""
            Dim N2 As String = ""
            Dim N3 As String = ""
            Dim N4 As String = ""
            Dim N5 As String = ""
            Dim niv As Integer = 1
            Dim aux As String = ""
            lblDescripcion.Text = ""
            lblN2.Text = ""
            lblN3.Text = ""
            lblN4.Text = ""
            lblN5.Text = ""
            N1 = quitarCeros(txtCuenta.Text)
            idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
            lblDescripcion.Text = p.descripcionCuenta
            If txtN2.Text <> "" Then
                N2 = quitarCeros(txtN2.Text)
                niv += 1
                idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
                lblN2.Text = p.descripcionCuenta
            End If
            If txtN3.Text <> "" Then
                N3 = quitarCeros(txtN3.Text)
                niv += 1
                idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
                lblN3.Text = p.descripcionCuenta
            End If
            If txtN4.Text <> "" Then
                N4 = quitarCeros(txtN4.Text)
                niv += 1
                idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
                lblN4.Text = p.descripcionCuenta
            End If
            If txtN5.Text <> "" Then
                N5 = quitarCeros(txtN5.Text)
                niv += 1
                idCuenta = p.BuscarIdCuenta(niv, N1, N2, N3, N4, N5)
                lblN5.Text = p.descripcionCuenta
            End If
            ' idCuenta = p.BuscarIdCuenta(niv, N1, N2, N4, N4, N5)
            volverVer = False
            If verificar Then
                If p.ultimoNivel(N1, N2, N3, N4, N5, niv) = 0 And idCuenta <> 0 Then
                    ' lblDescripcion.Text = p.descripcionCuenta
                    ' txtDesc.Focus()
                    'txtFolioFactura.Text = ""
                    'cmbProveedores.Text = ""
                    'txtCocepFac.Text = ""
                    'txtIVA.Text = ""
                Else
                    MsgBox("Debe seleccionar cuentas de último nivel.", MsgBoxStyle.Critical, GlobalNombreApp)
                    pregunta = True
                    If niv = 1 Then
                        txtCuenta.Focus()
                    Else
                        If niv = 2 Then
                            txtN3.Focus()
                        Else
                            If niv = 3 Then
                                txtN4.Focus()
                            Else
                                If niv = 4 Then
                                    txtN5.Focus()
                                Else
                                    txtN5.Focus()
                                End If
                            End If
                        End If
                    End If

                End If
            Else
                If idCuenta = 0 Then

                    If MsgBox("La cuenta proporcionada no existe.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        altaCuenta(N1, N2, N3, N4, N5)

                    Else
                        pregunta = True
                        volverVer = True
                        If niv = 1 Then
                            txtCuenta.Focus()
                            txtCuenta.SelectAll()
                        Else
                            If niv = 2 Then
                                txtN2.Focus()
                                txtN2.SelectAll()
                            Else
                                If niv = 3 Then
                                    txtN3.Focus()
                                    txtN3.SelectAll()
                                Else
                                    If niv = 4 Then
                                        txtN4.Focus()
                                        txtN4.SelectAll()
                                    Else
                                        txtN5.Focus()
                                        txtN5.SelectAll()
                                        auxNiv5 = False
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    Dim UL As Integer
                    UL = p.ultimoNivel(N1, N2, N3, N4, N5, niv)
                    If UL = 0 And idCuenta <> 0 Then
                        If p.esDIOT(idCuenta) Then
                            'dgvCuentas.Size = New Size(1016, 198)
                            'dgvCuentas.Location = New Point(14, 279)
                            pnlDatosIVA.Visible = True
                            txtFolioFactura.Focus()
                        Else
                            'dgvCuentas.Size = New Size(1016, 212)
                            'dgvCuentas.Location = New Point(14, 265)
                            pnlDatosIVA.Visible = False
                            If pDonde = 1 And N2 <> "" Then
                                txtN2.Focus()
                            Else
                                If pDonde = 2 And N3 <> "" Then
                                    txtN3.Focus()
                                Else
                                    If pDonde = 3 And N4 <> "" Then
                                        txtN4.Focus()
                                    Else
                                        If pDonde = 4 And N5 <> "" Then
                                            txtN5.Focus()
                                        Else
                                            If txtDesc.Text <> "" Then
                                                txtDesc.Focus()
                                                txtDesc.SelectAll()
                                            Else
                                                txtDesc.Focus()
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            txtFolioFactura.Text = ""
                            cmbProveedores.Text = ""
                            cmbProveedores.SelectedIndex = -1
                            txtCocepFac.Text = ""
                            txtIVA.Text = ""
                        End If
                    Else
                        pnlDatosIVA.Visible = False
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub altaCuenta(ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String)
        Dim B As New frmContabilidadCuentas(pN1, pN2, pN3, pN4, pN5)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            If pN1 <> "" And pN2 = "" And pN3 = "" And pN4 = "" And pN5 = "" Then
                verificaCuenta(False, 1)
            End If
            If pN1 <> "" And pN2 <> "" And pN3 = "" And pN4 = "" And pN5 = "" Then
                verificaCuenta(False, 2)
            End If
            If pN1 <> "" And pN2 <> "" And pN3 <> "" And pN4 = "" And pN5 = "" Then
                verificaCuenta(False, 3)
            End If
            If pN1 <> "" And pN2 <> "" And pN3 <> "" And pN4 <> "" And pN5 = "" Then
                verificaCuenta(False, 4)
            End If
            If pN1 <> "" And pN2 <> "" And pN3 <> "" And pN4 <> "" And pN5 <> "" Then
                verificaCuenta(False, 5)
            End If
        End If
    End Sub

    Private Function quitarCeros(ByVal pTexto As String)
        Dim texto As String = ""
        If pTexto <> "" Then
            If pTexto.Chars(0) = "0" Then
                For i As Integer = 0 To pTexto.Length.ToString - 1
                    If pTexto.Chars(i) <> "0" Then
                        For j As Integer = i To pTexto.Length - 1
                            texto += pTexto.Chars(j)
                        Next
                        If texto = "" Then
                            texto = "0"
                        End If
                        Return texto
                    End If
                Next
            Else
                texto = pTexto
            End If
            If texto = "" Then
                texto = "0"
            End If

        End If

        Return texto
    End Function

    Private Sub txtCuenta_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCuenta.KeyDown
        'If e.KeyCode = Keys.F10 Then
        '    guardar()
        'End If
        If e.KeyCode = Keys.F1 Then
            buscar()
        End If
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Then
            If txtBeneficiario.Visible = True Then
                txtBeneficiario.Focus()
            Else
                txtConcepto.Focus()
            End If
        End If
    End Sub
    Private Sub buscar()
        Dim buscador As String = ""
        rellenando = True
        If txtCuenta.Text <> "" Then
            buscador += txtCuenta.Text
        End If
        If txtN2.Text <> "" Then
            buscador += " " + txtN2.Text
        End If
        If txtN3.Text <> "" Then
            buscador += " " + txtN3.Text
        End If
        If txtN4.Text <> "" Then
            buscador += " " + txtN4.Text
        End If
        If txtN5.Text <> "" Then
            buscador += " " + txtN5.Text
        End If

        Dim B As New frmBuscadorCC(buscador)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'txtCuenta.Text = B.Cuenta.ToString()
            '' txtDesc.Text = B.descripcion.ToString()

            'lblDescripcion.Text = B.descripcion
            txtN2.Text = ""
            txtN2.Enabled = False
            txtN3.Text = ""
            txtN3.Enabled = False
            txtN4.Text = ""
            txtN4.Enabled = False
            txtN5.Text = ""
            txtN5.Enabled = False

            idCuenta = B.ID
            C.busquedaRegistro(idCuenta)
            txtCuenta.Text = C.N1.PadLeft(p.NNiv1, "0")
            If C.Nivel >= 2 Then
                txtN2.Text = C.N2.PadLeft(p.NNiv2, "0")
                txtN2.Enabled = True
            End If
            If C.Nivel >= 3 Then
                txtN3.Text = C.N3.PadLeft(p.NNiv3, "0")
                txtN3.Enabled = True

            End If
            If C.Nivel >= 4 Then
                txtN4.Text = C.N4.PadLeft(p.NNiv4, "0")
                txtN4.Enabled = True

            End If
            If C.Nivel >= 5 Then
                txtN5.Text = C.N5.PadLeft(p.NNiv5, "0")
                txtN5.Enabled = True
            End If

            If p.esDIOT(idCuenta) Then
                pnlDatosIVA.Visible = True
                'dgvCuentas.Size = New Size(1016, 198)
                'dgvCuentas.Location = New Point(14, 279)
                txtFolioFactura.Focus()
            Else
                pnlDatosIVA.Visible = False
                'dgvCuentas.Size = New Size(1016, 212)
                'dgvCuentas.Location = New Point(14, 265)
                txtFolioFactura.Text = ""
                cmbProveedores.Text = ""
                cmbProveedores.SelectedIndex = -1
                txtCocepFac.Text = ""
                txtIVA.Text = ""
                txtDesc.Focus()
            End If
        End If
        rellenando = False
    End Sub

    Private Sub txtCuenta_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCuenta.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtCuenta.Text <> "" Then

            If txtCuenta.Text = "+" Then

            Else
                If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtCuenta.Text = "" Then
                    If txtDesc.Text <> "" Then
                        txtDesc.Focus()
                        txtDesc.SelectAll()
                    Else
                        txtDesc.Focus()
                    End If
                Else
                    pregunta = False
                    txtCuenta.Text = txtCuenta.Text.PadLeft(p.NNiv1, "0")

                    'txtN2.Text = ""
                    'txtN3.Text = ""
                    'txtN4.Text = ""
                    'txtN5.Text = ""
                    'txtN2.Enabled = True
                    'txtN3.Enabled = False
                    'txtN4.Enabled = False
                    'txtN5.Enabled = False
                    'txtN2.Focus()
                    txtN2.Enabled = True
                    txtN2.Focus()
                    If volverVer = False Then
                        verificaCuenta(False, 1)
                    End If
                    If pregunta Then
                        txtCuenta.Focus()
                    End If
                End If

            End If

        Else

            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtCuenta.Text = "" Then
                If txtDesc.Text <> "" Then
                    txtDesc.Focus()
                    txtDesc.SelectAll()
                Else
                    txtDesc.Focus()
                End If
            End If

            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = " ") Or (e.KeyChar = "+")) Then
                e.Handled = True
            End If

        End If

    End Sub

    Private Sub btnAgregarDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarDetalle.Click
        botonAgregar()
    End Sub

    Private Sub botonAgregar()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim IdProveedor As Integer
            txtCuenta.BackColor = Color.White
            txtDesc.BackColor = Color.White
            txtCargo.BackColor = Color.White
            txtAbono.BackColor = Color.White
            txtFolioFactura.BackColor = Color.White
            cmbProveedores.BackColor = Color.White


            If txtCuenta.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere un número de cuenta." + vbCrLf
                txtCuenta.BackColor = Color.Tomato
            End If
            'If txtDesc.Text = "" Then
            '    NoErrores = False
            '    MensajeError += "Se requiere una descripción." + vbCrLf
            '    txtDesc.BackColor = Color.Tomato
            'End If
            If txtCargo.Text = "" And txtAbono.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere un cargo o un abono." + vbCrLf
                txtCargo.BackColor = Color.Tomato
                txtAbono.BackColor = Color.Tomato
            End If
            If cmbProveedores.SelectedIndex < 0 Then
                IdProveedor = 0
                If cmbProveedores.Text <> "" Then
                    IdProveedor = -1
                End If
            Else
                IdProveedor = IdsProveedores.Valor(cmbProveedores.SelectedIndex)
            End If

            If pnlDatosIVA.Visible = True And IdProveedor < 0 Then
                NoErrores = False
                MensajeError += "Se requiere proporcionar los datos DIOT." + vbCrLf
                'txtFolioFactura.BackColor = Color.Tomato
                cmbProveedores.BackColor = Color.Tomato
            End If

            If NoErrores = True Then
                If btnAgregarDetalle.Text = "Agregar" Then
                    Dim tabAux As DataTable = Tabla.Copy
                    If UltimaFila <> dgvCuentas.RowCount - 1 And dgvCuentas.RowCount > 0 Then
                        'si no está seleccionada la última fila
                        Tabla.Rows(idTabla).Delete()
                        Tabla.Clear()
                        For i As Integer = 0 To tabAux.Rows.Count - 1
                            If i <= UltimaFila Then
                                Dim dr2 As DataRow

                                dr2 = Tabla.NewRow()
                                dr2("id") = tabAux.Rows(i)(0)
                                dr2("idCuenta") = tabAux.Rows(i)(1)
                                dr2("Cuenta") = tabAux.Rows(i)(2)
                                dr2("descripcion") = tabAux.Rows(i)(3)
                                dr2("Cargo") = tabAux.Rows(i)(4)
                                dr2("Abono") = tabAux.Rows(i)(5)
                                dr2("factura") = tabAux.Rows(i)(6)
                                dr2("idProveedor") = tabAux.Rows(i)(7)
                                dr2("iva") = tabAux.Rows(i)(8)
                                dr2("concepto") = tabAux.Rows(i)(9)
                                dr2("esDIOT") = tabAux.Rows(i)(10)
                                dr2("ValorActos") = tabAux.Rows(i)(11)
                                dr2("referencia") = tabAux.Rows(i)(12)
                                dr2("ivaret") = tabAux.Rows(i)(13)
                                dr2("ieps") = tabAux.Rows(i)(14)
                                Tabla.Rows.Add(dr2)
                            End If
                        Next
                    End If

                    Dim dr As DataRow

                    dr = Tabla.NewRow()
                    dr("id") = contadorFilas
                    dr("idCuenta") = idCuenta
                    dr("Cuenta") = p.bucarCuenta(idCuenta)
                    dr("descripcion") = txtDesc.Text
                    If txtCargo.Text <> "" Then
                        dr("Cargo") = txtCargo.Text
                    Else
                        dr("Cargo") = ""
                    End If
                    If txtAbono.Text <> "" Then
                        dr("Abono") = txtAbono.Text
                    Else
                        dr("Abono") = ""
                    End If

                    dr("factura") = txtFolioFactura.Text
                    If IdProveedor <= 0 Then
                        dr("idProveedor") = 0
                    Else
                        dr("idProveedor") = IdProveedor
                    End If


                    dr("iva") = txtIVA.Text
                    dr("concepto") = cmbProveedores.Text
                    If txtCargo.Text <> "" And IdProveedor > 0 Then
                        If txtCargo.Text <> "0.00" Then
                            dr("esDIOT") = 1
                        Else
                            dr("esDIOT") = 0
                        End If
                    Else
                        dr("esDIOT") = 0
                    End If
                    dr("ValorActos") = txtValorActos.Text
                    nombreConcepto = txtDesc.Text
                    dr("referencia") = TextBox1.Text
                    dr("ivaret") = TextBox2.Text
                    dr("ieps") = TextBox3.Text
                    Tabla.Rows.Add(dr)

                    If UltimaFila <> dgvCuentas.RowCount - 1 And dgvCuentas.RowCount > 0 Then
                        'si no está seleccionada la última fila
                        For i As Integer = UltimaFila + 1 To tabAux.Rows.Count - 1
                            'If i < UltimaFila Then
                            Dim dr2 As DataRow

                            dr2 = Tabla.NewRow()
                            dr2("id") = tabAux.Rows(i)(0)
                            dr2("idCuenta") = tabAux.Rows(i)(1)
                            dr2("Cuenta") = tabAux.Rows(i)(2)
                            dr2("descripcion") = tabAux.Rows(i)(3)
                            dr2("Cargo") = tabAux.Rows(i)(4)
                            dr2("Abono") = tabAux.Rows(i)(5)
                            dr2("factura") = tabAux.Rows(i)(6)
                            dr2("idProveedor") = tabAux.Rows(i)(7)
                            dr2("iva") = tabAux.Rows(i)(8)
                            dr2("concepto") = tabAux.Rows(i)(9)
                            dr2("esDIOT") = tabAux.Rows(i)(10)
                            dr2("ValorActos") = tabAux.Rows(i)(11)
                            dr2("referencia") = tabAux.Rows(i)(12)
                            dr2("ivaret") = tabAux.Rows(i)(13)
                            dr2("ieps") = tabAux.Rows(i)(14)
                            Tabla.Rows.Add(dr2)
                            'End If

                        Next
                    End If

                    contadorFilas += 1

                    dgvCuentas.DataSource = Tabla
                    dgvCuentas.Columns(0).Visible = False
                    'dgvCuentas.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                    'dgvCuentas.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                    'dgvCuentas.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                    'dgvCuentas.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
                    'dgvCuentas.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                    dgvCuentas.Columns(1).Visible = False
                    dgvCuentas.Columns(6).Visible = False
                    dgvCuentas.Columns(7).Visible = False
                    dgvCuentas.Columns(8).Visible = False
                    dgvCuentas.Columns(9).Visible = False
                    dgvCuentas.Columns(10).Visible = False
                    dgvCuentas.Columns(11).Visible = False
                    dgvCuentas.Columns(12).Visible = False
                    dgvCuentas.Columns(13).Visible = False
                    dgvCuentas.Columns(14).Visible = False
                    dgvCuentas.Columns(2).Width = 170
                    dgvCuentas.Columns(4).Width = 85
                    dgvCuentas.Columns(5).Width = 85
                    dgvCuentas.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                    dgvCuentas.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                    dgvCuentas.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
                    dgvCuentas.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
                    dgvCuentas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
                    dgvCuentas.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
                    dgvCuentas.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    dgvCuentas.Columns(2).HeaderText = "Cuenta"
                    dgvCuentas.Columns(3).HeaderText = "Descripción"
                    dgvCuentas.Columns(4).HeaderText = "Cargo"
                    dgvCuentas.Columns(5).HeaderText = "Abono"
                    UltimaFila = dgvCuentas.Rows.Count - 1
                    dgvCuentas.Rows(UltimaFila).Selected = True
                    dgvCuentas.CurrentCell = dgvCuentas.Rows(dgvCuentas.Rows.Count - 1).Cells(2)
                    UltimaFila = dgvCuentas.Rows.Count - 1

                    If chkAgregarComprobante.Checked = True Then
                        If MsgBox("¿Desea agregar comprobantes?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            idRenglon = contadorFilas - 1
                            agregarComprobantes()
                            idRenglon = contadorFilas + 1
                        End If
                    End If


                    nuevoDetalleb()
                    ' PopUp("Agregado", 60)
                Else
                    'Modificar
                    'If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    'For i As Integer = 0 To Tabla.Rows.Count - 1
                    '  If Tabla.Rows(idTabla)(0) = idTabla Then
                    Tabla.Rows(idTabla)(1) = idCuenta
                    Tabla.Rows(idTabla)(2) = p.bucarCuenta(idCuenta)
                    Tabla.Rows(idTabla)(3) = txtDesc.Text
                    If txtCargo.Text <> "" Then
                        Tabla.Rows(idTabla)(4) = txtCargo.Text
                    Else
                        Tabla.Rows(idTabla)(4) = ""
                    End If
                    If txtAbono.Text <> "" Then
                        Tabla.Rows(idTabla)(5) = txtAbono.Text
                    Else
                        Tabla.Rows(idTabla)(5) = ""
                    End If


                    Tabla.Rows(idTabla)(6) = txtFolioFactura.Text
                    If IdProveedor <= 0 Then
                        Tabla.Rows(idTabla)(7) = 0
                    Else
                        Tabla.Rows(idTabla)(7) = IdProveedor
                    End If

                    Tabla.Rows(idTabla)(8) = txtIVA.Text
                    Tabla.Rows(idTabla)(9) = cmbProveedores.Text


                    If txtCargo.Text <> "" And IdProveedor > 0 Then
                        If txtCargo.Text <> "0.00" Then
                            Tabla.Rows(idTabla)(10) = 1
                        Else
                            Tabla.Rows(idTabla)(10) = 0
                        End If
                    Else
                        Tabla.Rows(idTabla)(10) = 0
                    End If
                    Tabla.Rows(idTabla)(11) = txtValorActos.Text
                    Tabla.Rows(idTabla)(12) = TextBox1.Text
                    Tabla.Rows(idTabla)(13) = TextBox2.Text
                    Tabla.Rows(idTabla)(14) = TextBox3.Text
                    nuevoDetalleb()
                    dgvCuentas.DataSource = Tabla
                End If
                If p.PreguntarCuadrar = 1 And lblDiferencia.Text = "$0.00" Then
                    Dim Fp As New frmSioNo("¿Guardar póliza?", "¿Guardar póliza?")
                    If Fp.ShowDialog = MsgBoxResult.Ok Then
                        guardar()
                    End If
                    Fp.Dispose()
                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub nuevoDetalle(pFocus As Boolean)
        idCuenta = -1
        txtCuenta.BackColor = Color.White
        txtN2.BackColor = Color.White
        txtN3.BackColor = Color.White
        txtN4.BackColor = Color.White
        txtN5.BackColor = Color.White
        txtCargo.BackColor = Color.White
        txtAbono.BackColor = Color.White
        rellenando = False
        ' txtCuenta.Text = ""
        txtDesc.Text = nombreConcepto
        ' txtN2.Text = ""
        'txtN3.Text = ""
        ' txtN4.Text = ""
        ' txtN5.Text = ""
        ' txtN2.Enabled = False
        ' txtN3.Enabled = False
        ' txtN4.Enabled = False
        ' txtN5.Enabled = False
        'lblDescripcion.Text = ""
        ' lblN2.Text = ""
        ' lblN3.Text = ""
        ' lblN4.Text = ""
        ' lblN5.Text = ""
        txtCargo.Text = ""
        txtAbono.Text = ""
        txtFolioFactura.Text = ""
        cmbProveedores.Text = ""
        cmbProveedores.SelectedIndex = -1
        txtCocepFac.Text = ""
        txtIVA.Text = "16"
        btnAgregarDetalle.Text = "Agregar"
        calculos()
        btnEliminarDetalle.Enabled = False
        ' grpDatosPoliza.Visible = False
        pnlDatosIVA.Visible = False
        btnComprobantes.Enabled = False

        idRenglon = 0
        txtValorActos.Text = "0.00"
        If pFocus Then txtCuenta.Focus()
    End Sub
    Private Sub nuevoDetalleb()
        'idCuenta = -1
        txtCuenta.BackColor = Color.White
        txtN2.BackColor = Color.White
        txtN3.BackColor = Color.White
        txtN4.BackColor = Color.White
        txtN5.BackColor = Color.White
        txtCargo.BackColor = Color.White
        txtAbono.BackColor = Color.White
        rellenando = False
        ' txtCuenta.Text = ""
        txtDesc.Text = nombreConcepto
        ' txtN2.Text = ""
        'txtN3.Text = ""
        ' txtN4.Text = ""
        ' txtN5.Text = ""
        ' txtN2.Enabled = False
        ' txtN3.Enabled = False
        ' txtN4.Enabled = False
        ' txtN5.Enabled = False
        'lblDescripcion.Text = ""
        ' lblN2.Text = ""
        ' lblN3.Text = ""
        ' lblN4.Text = ""
        ' lblN5.Text = ""
        'txtCargo.Text = ""
        'txtAbono.Text = ""
        txtFolioFactura.Text = ""
        cmbProveedores.Text = ""
        cmbProveedores.SelectedIndex = -1

        txtCocepFac.Text = ""
        txtIVA.Text = "16"
        TextBox2.Text = "0"
        TextBox3.Text = "0"
        btnAgregarDetalle.Text = "Agregar"
        calculos()
        btnEliminarDetalle.Enabled = False
        ' grpDatosPoliza.Visible = False
        pnlDatosIVA.Visible = False
        btnComprobantes.Enabled = False
        'idProveedor = 0
        idRenglon = 0
        txtValorActos.Text = "0.00"
        'If txtN5.Text <> "" Then
        'TextBox1.Text = ""
        'End If
        txtCuenta.Focus()
    End Sub
    Private Sub NuevoDetalle2()
        txtCuenta.Text = ""
        txtN2.Text = ""
        txtN3.Text = ""
        txtN4.Text = ""
        txtN5.Text = ""
        txtN2.Enabled = False
        txtN3.Enabled = False
        txtN4.Enabled = False
        txtN5.Enabled = False
        lblDescripcion.Text = ""
        lblN2.Text = ""
        lblN3.Text = ""
        lblN4.Text = ""
        lblN5.Text = ""
        idRenglon = 0
    End Sub



    Private Sub txtCuenta_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Leave, txtCuenta.Leave
        'If txtN2.Focus = False Or txtN3.Focus = False Or txtN4.Focus = False Or txtN5.Focus = False Then
        '    validarCuenta()
        'End If

    End Sub
    Private Sub validarCuenta()
        If rellenando = False Then
            txtCuenta.BackColor = Color.White
            txtN2.BackColor = Color.White
            txtN3.BackColor = Color.White
            txtN4.BackColor = Color.White
            txtN5.BackColor = Color.White
            verificaCuenta(True, 0)

            If txtCuenta.Text <> "" Then
                If p.existeCuenta(idCuenta) = False Then
                    MsgBox("La cuenta proporcionada no exite.", MsgBoxStyle.OkOnly, "Pull System Soft")
                    txtCuenta.BackColor = Color.Tomato
                    If txtN2.Text <> "" Then
                        txtN2.BackColor = Color.Tomato
                    End If
                    If txtN3.Text <> "" Then
                        txtN3.BackColor = Color.Tomato
                    End If
                    If txtN4.Text <> "" Then
                        txtN4.BackColor = Color.Tomato
                    End If
                    If txtN5.Text <> "" Then
                        txtN5.BackColor = Color.Tomato
                    End If
                    txtCuenta.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub calculos()
        If dgvCuentas.RowCount > 0 Then
            Dim abono As Double = 0
            Dim cargo As Double = 0
            Dim dif As Double = 0
            'Dim aux As String
            'Dim auxD As Double

            For i As Integer = 0 To dgvCuentas.RowCount - 1
                'aux = 
                'Double.TryParse(aux, auxD)
                If dgvCuentas.Item(4, i).Value.ToString <> "" Then
                    'cargo += Math.Round(Double.Parse(dgvCuentas.Item(4, i).Value.ToString), 2)
                    cargo += Math.Round(CDbl(dgvCuentas.Item(4, i).Value), 2)
                Else
                    cargo += 0
                End If
                If dgvCuentas.Item(5, i).Value.ToString <> "" Then
                    'abono += Math.Round(Double.Parse(dgvCuentas.Item(5, i).Value.ToString), 2)
                    abono += Math.Round(CDbl(dgvCuentas.Item(5, i).Value), 2)
                Else
                    abono += 0
                End If

            Next
            If cargo > abono Then
                dif = cargo - abono
            Else
                dif = abono - cargo
            End If
            lblAbonos.Text = abono.ToString("c2")
            lblCargos.Text = cargo.ToString("c2")
            lblDiferencia.Text = dif.ToString("c2")
            If dif > 0.0001 Then
                lblDiferencia.ForeColor = Color.Red
                'Dim f As New System.Drawing.Font("Arial", 10, FontStyle.Bold)
                'lblDiferencia.Font = f
                'lblDiferencia.Font.Bold = True
            Else
                lblDiferencia.ForeColor = Color.Black
                'Dim f As New System.Drawing.Font("Arial", 10, FontStyle.Bold)
                'lblDiferencia.Font = f
            End If
        Else
            lblAbonos.Text = "$0.00"
            lblCargos.Text = "$0.00"
            lblDiferencia.Text = "$0.00"
            lblDiferencia.ForeColor = Color.Black
            'Dim f As New System.Drawing.Font("Arial", 10, FontStyle.Bold)
            'lblDiferencia.Font = f
        End If
    End Sub

    Private Sub btnNuevoDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevoDetalle.Click
        nuevoDetalle(True)
    End Sub

    Private Sub dgvCuentas_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCuentas.CellClick
        llenaDatos()
    End Sub
    Private Sub llenaDatos(Optional ByVal prow As Integer = -1)

        If dgvCuentas.RowCount > 0 Then


            If dgvCuentas.CurrentCell.RowIndex >= 0 Then
                nuevoDetalle(False)
                If prow = -1 Then
                    idRenglon = dgvCuentas.Item(0, dgvCuentas.CurrentCell.RowIndex).Value
                    idTabla = dgvCuentas.CurrentCell.RowIndex
                    idCuenta = dgvCuentas.Item(1, dgvCuentas.CurrentCell.RowIndex).Value
                    UltimaFila = dgvCuentas.CurrentCell.RowIndex
                Else
                    idTabla = prow
                    idCuenta = dgvCuentas.Item(1, prow).Value
                    UltimaFila = prow
                End If
                btnComprobantes.Enabled = True


                C.busquedaRegistro(idCuenta)
                ConsultaOn = False
                txtCuenta.Text = C.N1.PadLeft(p.NNiv1, "0")
                If C.Nivel >= 2 Then
                    txtN2.Text = C.N2.PadLeft(p.NNiv2, "0")
                    txtN2.Enabled = True
                End If
                If C.Nivel >= 3 Then
                    txtN3.Text = C.N3.PadLeft(p.NNiv3, "0")
                    txtN3.Enabled = True
                End If
                If C.Nivel >= 4 Then
                    txtN4.Text = C.N4.PadLeft(p.NNiv4, "0")
                    txtN4.Enabled = True
                End If
                If C.Nivel >= 5 Then
                    txtN5.Text = C.N5.PadLeft(p.NNiv5, "0")
                    txtN5.Enabled = True
                End If
                ConsultaOn = True
                If prow = -1 Then
                    txtDesc.Text = dgvCuentas.Item(3, dgvCuentas.CurrentCell.RowIndex).Value
                    txtCargo.Text = dgvCuentas.Item(4, dgvCuentas.CurrentCell.RowIndex).Value
                    txtAbono.Text = dgvCuentas.Item(5, dgvCuentas.CurrentCell.RowIndex).Value
                    txtFolioFactura.Text = dgvCuentas.Item(6, dgvCuentas.CurrentCell.RowIndex).Value
                    'idProveedor = dgvCuentas.Item(7, dgvCuentas.CurrentCell.RowIndex).Value
                    'prov.LlenaDatos(dgvCuentas.Item(7, dgvCuentas.CurrentCell.RowIndex).Value)
                    'If idProveedor = 0 Then
                    '    txtProveedorClave.Text = ""
                    '    txtProveedorNombre.Text = ""
                    'Else
                    '    txtProveedorClave.Text = prov.Clave
                    '    txtProveedorNombre.Text = prov.Nombre
                    'End If
                    If cmbProveedores.Items.Count > 0 Then
                        If dgvCuentas.Item(7, dgvCuentas.CurrentCell.RowIndex).Value <> 0 Then
                            cmbProveedores.SelectedIndex = IdsProveedores.Busca(dgvCuentas.Item(7, dgvCuentas.CurrentCell.RowIndex).Value)
                            cmbProveedores.Text = cmbProveedores.Items(cmbProveedores.SelectedIndex)
                        Else
                            cmbProveedores.Text = ""
                            cmbProveedores.SelectedIndex = -1
                        End If
                    End If
                    txtIVA.Text = dgvCuentas.Item(8, dgvCuentas.CurrentCell.RowIndex).Value
                    If txtIVA.Text = "0" Or txtIVA.Text = "E" Then
                        txtValorActos.Visible = True
                        txtValorActos.Text = dgvCuentas.Item(11, dgvCuentas.CurrentCell.RowIndex).Value.ToString()

                    Else
                        txtValorActos.Text = "0.00"
                        txtValorActos.Visible = False
                    End If
                    TextBox1.Text = dgvCuentas.Item(12, dgvCuentas.CurrentCell.RowIndex).Value
                    If IsDBNull(dgvCuentas.Item(13, dgvCuentas.CurrentCell.RowIndex).Value) = False Then
                        TextBox2.Text = dgvCuentas.Item(13, dgvCuentas.CurrentCell.RowIndex).Value
                    Else
                        TextBox2.Text = "0"
                    End If
                    If IsDBNull(dgvCuentas.Item(14, dgvCuentas.CurrentCell.RowIndex).Value) = False Then
                        TextBox3.Text = dgvCuentas.Item(14, dgvCuentas.CurrentCell.RowIndex).Value
                    Else
                        TextBox3.Text = "0"
                    End If
                Else
                    txtDesc.Text = dgvCuentas.Item(3, prow).Value
                    txtCargo.Text = dgvCuentas.Item(4, prow).Value
                    txtAbono.Text = dgvCuentas.Item(5, prow).Value
                    txtFolioFactura.Text = dgvCuentas.Item(6, prow).Value
                    'idProveedor = dgvCuentas.Item(7, prow).Value
                    'prov.LlenaDatos(dgvCuentas.Item(7, prow).Value)
                    'If dgvCuentas.Item(7, prow).Value = "0" Then

                    '    txtProveedorClave.Text = ""
                    '    txtProveedorNombre.Text = ""
                    'Else

                    '    txtProveedorClave.Text = prov.Clave
                    '    txtProveedorNombre.Text = prov.Nombre
                    'End If

                    If cmbProveedores.Items.Count > 0 Then
                        If dgvCuentas.Item(7, prow).Value <> 0 Then
                            cmbProveedores.SelectedIndex = IdsProveedores.Busca(dgvCuentas.Item(7, prow).Value)
                        Else
                            cmbProveedores.Text = ""
                            cmbProveedores.SelectedIndex = -1
                        End If
                    End If



                    txtIVA.Text = dgvCuentas.Item(8, prow).Value
                    TextBox1.Text = dgvCuentas.Item(12, prow).Value
                    TextBox2.Text = dgvCuentas.Item(13, prow).Value
                    TextBox3.Text = dgvCuentas.Item(14, prow).Value
                End If

                btnAgregarDetalle.Text = "Modif."
                btnEliminarDetalle.Enabled = True
                '   verificaCuenta(True)
                pnlDatosIVA.Visible = False
                txtCuenta.Focus()
                If C.Nivel = 2 Then
                    txtN2.Focus()
                Else
                    If C.Nivel = 3 Then
                        txtN3.Focus()
                    Else
                        If C.Nivel = 4 Then
                            txtN4.Focus()
                        Else
                            If C.Nivel = 5 Then
                                txtN5.Focus()

                            End If
                        End If
                    End If
                End If

            End If
        End If
    End Sub
    Private Sub btnEliminarDetalle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarDetalle.Click
        Try
            If MsgBox("¿Deseas eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim tabAux As DataTable = Tabla.Copy
                Tabla.Rows(idTabla).Delete()
                Tabla.Clear()
                For i As Integer = 0 To tabAux.Rows.Count - 1
                    If i <> idTabla Then
                        Dim dr As DataRow

                        dr = Tabla.NewRow()
                        dr("id") = tabAux.Rows(i)(0)
                        dr("idCuenta") = tabAux.Rows(i)(1)
                        dr("Cuenta") = tabAux.Rows(i)(2)
                        dr("descripcion") = tabAux.Rows(i)(3)
                        dr("Cargo") = tabAux.Rows(i)(4)
                        dr("Abono") = tabAux.Rows(i)(5)
                        dr("factura") = tabAux.Rows(i)(6)
                        dr("idProveedor") = tabAux.Rows(i)(7)
                        dr("iva") = tabAux.Rows(i)(8)
                        dr("concepto") = tabAux.Rows(i)(9)
                        dr("esDIOT") = tabAux.Rows(i)(10)
                        dr("ValorActos") = tabAux.Rows(i)(11)
                        dr("referencia") = tabAux.Rows(i)(12)
                        dr("ivaret") = tabAux.Rows(i)(13)
                        dr("ieps") = tabAux.Rows(i)(14)
                        Tabla.Rows.Add(dr)
                    End If

                Next
                If dgvCuentas.RowCount > 0 Then
                    dgvCuentas.Rows(dgvCuentas.Rows.Count - 1).Selected = True
                    dgvCuentas.CurrentCell = dgvCuentas.Rows(dgvCuentas.Rows.Count - 1).Cells(2)
                    UltimaFila = dgvCuentas.Rows.Count - 1
                End If

                nuevoDetalleb()
                'PopUp("Eliminado", 60)
                dgvCuentas.DataSource = Tabla

                If p.PreguntarCuadrar = 1 And lblDiferencia.Text = "$0.00" Then
                    Dim Fp As New frmSioNo("¿Guardar póliza?", "¿Guardar póliza?")
                    If Fp.ShowDialog = MsgBoxResult.Ok Then
                        guardar()
                    End If
                    Fp.Dispose()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnGuardarPoliza_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarPoliza.Click
        guardar()
    End Sub
    Private Sub guardar()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim tipoPoli As Integer
            txtNumeroPoliza.BackColor = Color.White
            txtConcepto.BackColor = Color.White
            txtBeneficiario.BackColor = Color.White

            'If Not dtpFecha.MinDate <= dtpFecha.Then Then
            '    NoErrores = False
            '    MensajeError += "La fecha se encuentra en un rango incorrecto." + vbCrLf
            'Else
            '    If Not dtpFecha.MaxDate >= Date.Parse(txtFecha1.Text) Then
            '        NoErrores = False
            '        MensajeError += "La fecha se encuentra en un rango incorrecto." + vbCrLf
            '    End If

            'End If

            If txtNumeroPoliza.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere un número de póliza." + vbCrLf
                txtNumeroPoliza.BackColor = Color.Tomato
            Else
                If btnGuardarPoliza.Text = "Guardar (F10)" Then
                    'txtNumeroPoliza.Text = p.bucarNumero(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbTipoPoliza.Text.Chars(0))
                    If p.folioRepetido(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbTipoPolizaX.Text.Chars(0), CInt(txtNumeroPoliza.Text), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)) <> 0 Then
                        NoErrores = False
                        MensajeError += " Número de póliza repetido."
                    End If

                End If
            End If
            If dtpFecha.Value.Year.ToString < periodo Or dtpFecha.Value.Year.ToString > periodo Then
                NoErrores = False
                MensajeError += " No puede guardar pólizas que no esten en el periodo de trabajo."
            End If
            If txtConcepto.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere indicar el concepto." + vbCrLf
                txtConcepto.BackColor = Color.Tomato
            End If
            If cmbTipoPolizaX.SelectedIndex = 0 And txtBeneficiario.Text = "" Then
                NoErrores = False
                MensajeError += "Se requiere indicar el nombre del beneficiario." + vbCrLf
                txtBeneficiario.BackColor = Color.Tomato
            End If
            If lblDiferencia.Text <> "$0.00" Then
                If MsgBox("La póliza no está balanceada. ¿Desea guardarla de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                    Exit Sub
                End If
                'NoErrores = False
                'MensajeError += "La póliza no está balanceada." + vbCrLf
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasAlta, PermisosN.Secciones.Contabilidad) = False Then
                MensajeError += "No tiene permiso para realizar esta operación"
                NoErrores = False
            End If
            If tipoPoli <> 3 Then
                tipoPoli = cmbTipoPolizaX.SelectedIndex
            Else
                tipoPoli = 4
            End If
            If NoErrores = True Then
                If btnGuardarPoliza.Text = "Guardar (F10)" Then
                    p.Comm.Transaction = p.Comm.Connection.BeginTransaction
                    p.guardarPoliza(cmbTipoPolizaX.Text.Chars(0), Integer.Parse(txtNumeroPoliza.Text), dtpFecha.Value.ToString("yyyy/MM/dd"), txtConcepto.Text, txtBeneficiario.Text, lblCargos.Text, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex), 3)
                    p.DetallesOrden = 1
                    NuevoDetalle2()
                    nuevoDetalle(False)
                    txtDesc.Text = ""
                    idPoliza = p.IDPoliza
                    For i As Integer = 0 To Tabla.Rows.Count - 1

                        p.guardarDetalles(Tabla.Rows(i)(2), Tabla.Rows(i)(3), Tabla.Rows(i)(4), Tabla.Rows(i)(5), Tabla.Rows(i)(1), Tabla.Rows(i)(6), Tabla.Rows(i)(7), Tabla.Rows(i)(8), Tabla.Rows(i)(9), Tabla.Rows(i)(10), Tabla.Rows(i)(11), dtpFecha.Value.ToString("yyyy/MM/dd"), Tabla.Rows(i)(12), Tabla.Rows(i)(13), Tabla.Rows(i)(14), p.DetallesOrden)
                        'AgregarComprobantesCheques
                        For j As Integer = 0 To tablaChequesAUX.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaChequesAUX.Rows(j)(2) Then
                                p.guardarCheque(idPoliza, p.IDRenglon, tablaChequesAUX.Rows(j)(3), tablaChequesAUX.Rows(j)(4), tablaChequesAUX.Rows(j)(6), tablaChequesAUX.Rows(j)(7), tablaChequesAUX.Rows(j)(8), tablaChequesAUX.Rows(j)(9), tablaChequesAUX.Rows(j)(10), tablaChequesAUX.Rows(j)(11), tablaChequesAUX.Rows(j)(12), tablaChequesAUX.Rows(j)(14))
                            End If
                        Next
                        'Agregar comprobantes transaccion
                        For j As Integer = 0 To tablaTransAUX.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaTransAUX.Rows(j)(2) Then
                                p.guardarTransferencia(idPoliza, p.IDRenglon, tablaTransAUX.Rows(j)(3), tablaTransAUX.Rows(j)(4), tablaTransAUX.Rows(j)(6), tablaTransAUX.Rows(j)(7), tablaTransAUX.Rows(j)(8), tablaTransAUX.Rows(j)(10), tablaTransAUX.Rows(j)(11), tablaTransAUX.Rows(j)(12), tablaTransAUX.Rows(j)(13), tablaTransAUX.Rows(j)(14), tablaTransAUX.Rows(j)(15), tablaTransAUX.Rows(j)(17))
                            End If
                        Next
                        'agregar comprobantes
                        For j As Integer = 0 To tablaComproAUX.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaComproAUX.Rows(j)(2) Then
                                p.guardarComprobante(idPoliza, p.IDRenglon, tablaComproAUX.Rows(j)(3), tablaComproAUX.Rows(j)(8), tablaComproAUX.Rows(j)(4), tablaComproAUX.Rows(j)(5), tablaComproAUX.Rows(j)(7))
                            End If
                        Next
                        'agregar comprobantes nacinale 2
                        For j As Integer = 0 To tablaComproNac2Aux.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaComproNac2Aux.Rows(j)(2) Then
                                p.guardarComprobanteNac2(idPoliza, p.IDRenglon, tablaComproNac2Aux.Rows(j)(3), tablaComproNac2Aux.Rows(j)(4), tablaComproNac2Aux.Rows(j)(5), tablaComproNac2Aux.Rows(j)(6), tablaComproNac2Aux.Rows(j)(7), tablaComproNac2Aux.Rows(j)(9))
                            End If
                        Next
                        'agregar comprobantes extranjeros
                        For j As Integer = 0 To tablaComproEAux.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaComproEAux.Rows(j)(2) Then
                                p.guardarComprobanteE(idPoliza, p.IDRenglon, tablaComproEAux.Rows(j)(3), tablaComproEAux.Rows(j)(4), tablaComproEAux.Rows(j)(5), tablaComproEAux.Rows(j)(6), tablaComproEAux.Rows(j)(8))
                            End If
                        Next
                        'agregar otros
                        For j As Integer = 0 To tablaOtrosAux.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaOtrosAux.Rows(j)(2) Then
                                p.guardarOtro(idPoliza, p.IDRenglon, tablaOtrosAux.Rows(j)(3), tablaOtrosAux.Rows(j)(5), tablaOtrosAux.Rows(j)(6), tablaOtrosAux.Rows(j)(7), tablaOtrosAux.Rows(j)(8), tablaOtrosAux.Rows(j)(9), tablaOtrosAux.Rows(j)(11))
                            End If
                        Next
                    Next
                    p.Comm.Transaction.Commit()
                    nuevo()
                    Consulta()
                    PopUp("Póliza guardada", 60)
                Else
                    'Modificar
                    'If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    p.Comm.Transaction = p.Comm.Connection.BeginTransaction
                    p.modificarPoliza(idPoliza, cmbTipoPolizaX.Text.Chars(0), Integer.Parse(txtNumeroPoliza.Text), dtpFecha.Value.Year.ToString + "/" + dtpFecha.Value.Month.ToString("00") + "/" + dtpFecha.Value.Day.ToString("00"), txtConcepto.Text, txtBeneficiario.Text, lblCargos.Text, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
                    p.DetallesOrden = 1
                    p.eliminarDetalles()
                    NuevoDetalle2()
                    nuevoDetalle(False)
                    txtDesc.Text = ""
                    'p.EliminarCheque(idPoliza)
                    'p.EliminarCompro(idPoliza)
                    'p.EliminarTrans(idPoliza)
                    'p.EliminarComproNac2(idPoliza)
                    'p.EliminarComproE(idPoliza)
                    'p.EliminarOtro(idPoliza)
                    For i As Integer = 0 To Tabla.Rows.Count - 1
                        p.guardarDetalles(Tabla.Rows(i)(2), Tabla.Rows(i)(3), Tabla.Rows(i)(4), Tabla.Rows(i)(5), Tabla.Rows(i)(1), Tabla.Rows(i)(6), Tabla.Rows(i)(7), Tabla.Rows(i)(8), Tabla.Rows(i)(9), Tabla.Rows(i)(10), Tabla.Rows(i)(11), dtpFecha.Value.ToString("yyyy/MM/dd"), Tabla.Rows(i)(12), Tabla.Rows(i)(13), Tabla.Rows(i)(14), p.DetallesOrden)
                        'AgregarComprobantesCheques
                        For j As Integer = 0 To tablaChequesAUX.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaChequesAUX.Rows(j)(2) Then
                                p.guardarCheque(idPoliza, p.IDRenglon, tablaChequesAUX.Rows(j)(3), tablaChequesAUX.Rows(j)(4), tablaChequesAUX.Rows(j)(6), tablaChequesAUX.Rows(j)(7), tablaChequesAUX.Rows(j)(8), tablaChequesAUX.Rows(j)(9), tablaChequesAUX.Rows(j)(10), tablaChequesAUX.Rows(j)(11), tablaChequesAUX.Rows(j)(12), tablaChequesAUX.Rows(j)(14))
                            End If
                        Next
                        'Agregar comprobantes transaccion
                        For j As Integer = 0 To tablaTransAUX.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaTransAUX.Rows(j)(2) Then
                                p.guardarTransferencia(idPoliza, p.IDRenglon, tablaTransAUX.Rows(j)(3), tablaTransAUX.Rows(j)(4), tablaTransAUX.Rows(j)(6), tablaTransAUX.Rows(j)(7), tablaTransAUX.Rows(j)(8), tablaTransAUX.Rows(j)(10), tablaTransAUX.Rows(j)(11), tablaTransAUX.Rows(j)(12), tablaTransAUX.Rows(j)(13), tablaTransAUX.Rows(j)(14), tablaTransAUX.Rows(j)(15), tablaTransAUX.Rows(j)(17))
                            End If
                        Next
                        'agregar comprobantes
                        For j As Integer = 0 To tablaComproAUX.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaComproAUX.Rows(j)(2) Then
                                p.guardarComprobante(idPoliza, p.IDRenglon, tablaComproAUX.Rows(j)(3), tablaComproAUX.Rows(j)(8), tablaComproAUX.Rows(j)(4), tablaComproAUX.Rows(j)(5), tablaComproAUX.Rows(j)(7))
                            End If
                        Next
                        'agregar comprobantes nacinale 2
                        For j As Integer = 0 To tablaComproNac2Aux.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaComproNac2Aux.Rows(j)(2) Then
                                p.guardarComprobanteNac2(idPoliza, p.IDRenglon, tablaComproNac2Aux.Rows(j)(3), tablaComproNac2Aux.Rows(j)(4), tablaComproNac2Aux.Rows(j)(5), tablaComproNac2Aux.Rows(j)(6), tablaComproNac2Aux.Rows(j)(7), tablaComproNac2Aux.Rows(j)(9))
                            End If
                        Next
                        'agregar comprobantes extranjeros
                        For j As Integer = 0 To tablaComproEAux.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaComproEAux.Rows(j)(2) Then
                                p.guardarComprobanteE(idPoliza, p.IDRenglon, tablaComproEAux.Rows(j)(3), tablaComproEAux.Rows(j)(4), tablaComproEAux.Rows(j)(5), tablaComproEAux.Rows(j)(6), tablaComproEAux.Rows(j)(8))
                            End If
                        Next
                        'agregar otros
                        For j As Integer = 0 To tablaOtrosAux.Rows.Count - 1
                            If Tabla.Rows(i)(0) = tablaOtrosAux.Rows(j)(2) Then
                                p.guardarOtro(idPoliza, p.IDRenglon, tablaOtrosAux.Rows(j)(3), tablaOtrosAux.Rows(j)(5), tablaOtrosAux.Rows(j)(6), tablaOtrosAux.Rows(j)(7), tablaOtrosAux.Rows(j)(8), tablaOtrosAux.Rows(j)(9), tablaOtrosAux.Rows(j)(11))
                            End If
                        Next
                    Next
                    p.Comm.Transaction.Commit()
                    nuevo()
                    Consulta()
                    PopUp("Póliza Modificada", 60)
                    'End If

                End If
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Consulta()
        Try
            If noFiltro Then


                Dim tipo As Integer
                If cmbTipoBuscar.SelectedIndex = 4 Then
                    tipo = 5
                Else
                    tipo = cmbTipoBuscar.SelectedIndex
                End If
                Dim clas As Integer

                If cmbClasBusqueda.Items.Count = 0 Then
                    clas = 0
                Else
                    clas = IdsClasificacionPolizaBusqeda.Valor(cmbClasBusqueda.SelectedIndex)
                End If
                DataGridView1.DataSource = p.reporte(dtpDesde.Value.Year.ToString + "/" + dtpDesde.Value.Month.ToString("00") + "/" + dtpDesde.Value.Day.ToString("00"), dtpHasta.Value.Year.ToString + "/" + dtpHasta.Value.Month.ToString("00") + "/" + dtpHasta.Value.Day.ToString("00"), txtBuscarConecpto.Text, tipo, clas, CheckBox1.Checked, TextBox4.Text)

                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(5).Visible = False
                DataGridView1.Columns(1).HeaderText = "Póliza"
                DataGridView1.Columns(1).Width = 1
                DataGridView1.Columns(4).Width = 100
                DataGridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
                DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub txtBuscarConecpto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscarConecpto.TextChanged
        Consulta()
    End Sub

    Private Sub dtpDesde_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDesde.ValueChanged
        Dim fecha As Date
        fecha = CDate(Format(dtpDesde.Value, "yyyy/MM/01"))
        fecha = DateAdd(DateInterval.Month, 1, fecha)
        fecha = DateAdd(DateInterval.Day, -1, fecha)
        ConsultaOn = False
        dtpHasta.Value = fecha
        Consulta()
    End Sub

    Private Sub dtpHasta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpHasta.ValueChanged
        Consulta()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.RowCount > 0 Then
            LlenaDatosPoliza(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
        End If
    End Sub
    Private Sub LlenaDatosPoliza(pIdPoliza As Integer)
        llenando = True
        Dim idPolizaAux As Integer
        idPolizaAux = pIdPoliza
        NuevoDetalle2()
        nuevo()
        idPoliza = idPolizaAux
        Tabla = p.llenaDatosPoliza(idPoliza)

        tablaChequesAUX = p.tablaCheque
        tablaComproAUX = p.tablaCompro
        tablaTransAUX = p.tablaTrans
        tablaComproNac2Aux = p.tablaComproNac2
        tablaComproEAux = p.tablaComproE
        tablaOtrosAux = p.tablaOtros



        cmbTipoPolizaX.SelectedIndex = p.tipo
        dtpFecha.Value = p.fecha
        txtNumeroPoliza.Text = p.Numero

        txtConcepto.Text = p.concepto
        txtBeneficiario.Text = p.beneficiario
        dgvCuentas.DataSource = Nothing

        dgvCuentas.DataSource = Tabla
        dgvCuentas.Columns(0).Visible = False
        dgvCuentas.Columns(1).Visible = False
        dgvCuentas.Columns(6).Visible = False
        dgvCuentas.Columns(7).Visible = False
        dgvCuentas.Columns(8).Visible = False
        dgvCuentas.Columns(9).Visible = False
        dgvCuentas.Columns(10).Visible = False
        dgvCuentas.Columns(11).Visible = False
        dgvCuentas.Columns(12).Visible = False
        dgvCuentas.Columns(13).Visible = False
        dgvCuentas.Columns(14).Visible = False
        'dgvCuentas.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
        'dgvCuentas.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
        'dgvCuentas.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
        'dgvCuentas.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
        'dgvCuentas.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(2).Width = 170
        dgvCuentas.Columns(4).Width = 85
        dgvCuentas.Columns(5).Width = 85
        dgvCuentas.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        dgvCuentas.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        dgvCuentas.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvCuentas.Columns(2).HeaderText = "Cuenta"
        dgvCuentas.Columns(3).HeaderText = "Descripción"
        dgvCuentas.Columns(4).HeaderText = "Cargo"
        dgvCuentas.Columns(5).HeaderText = "Abono"
        calculos()
        btnGuardarPoliza.Text = "Modificar (F10)"
        btnEliminar.Enabled = True
        btnImprimir.Enabled = True

        If dgvCuentas.RowCount > 0 Then
            UltimaFila = dgvCuentas.Rows.Count - 1
            dgvCuentas.Rows(UltimaFila).Selected = True
        Else
            UltimaFila = 0
        End If

        nuevoDetalle(True)
        llenando = False
        contadorFilas = p.MayorID
        'cmbTipoPoliza.Focus()
        'DataGridView1.Focus()
    End Sub
    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasBaja, PermisosN.Secciones.Contabilidad) = False Then
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Try
            If MsgBox("¿Desea eliminar esta poliza?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'p.EliminarCheque(idPoliza)
                'p.EliminarCompro(idPoliza)
                'p.EliminarTrans(idPoliza)
                p.eliminarPoliza(idPoliza)
                p.eliminarDetalles()
                nuevo()
                Consulta()
                PopUp("Poliza Eliminada", 60)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub dtpFecha_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpFecha.KeyPress

    End Sub

    Private Sub chkMantenerFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMantenerFecha.CheckedChanged
        If chkMantenerFecha.Checked = True Then
            dtpFecha.Enabled = False
            'txtFecha1.Enabled = False
        Else
            dtpFecha.Enabled = True
            'txtFecha1.Enabled = True
        End If
    End Sub

    Private Sub txtN2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtN2.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN2.Text <> "" Then
            If txtN2.Text = "+" Then
            Else
                If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN2.Text = "" Then
                    If txtDesc.Text <> "" Then
                        txtDesc.Focus()
                        txtDesc.SelectAll()
                    Else
                        txtDesc.Focus()
                    End If
                    validarCuenta()
                Else
                    pregunta = False
                    txtN2.Text = txtN2.Text.PadLeft(p.NNiv2, "0")

                    'txtN3.Text = ""
                    'txtN4.Text = ""
                    'txtN5.Text = ""
                    ''txtN2.Enabled = True
                    txtN3.Enabled = True
                    'txtN4.Enabled = False
                    'txtN5.Enabled = False
                    txtN3.Focus()
                    If volverVer = False Then
                        verificaCuenta(False, 2)
                    End If
                    If pregunta Then
                        txtN2.Focus()
                    End If
                End If

            End If

        Else
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN2.Text = "" Then
                txtDesc.Focus()
                ' validarCuenta()
            End If


            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = " ") Or (e.KeyChar = "+")) Then
                e.Handled = True
            End If



        End If

    End Sub

    Private Sub txtN3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtN3.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN3.Text <> "" Then
            If txtN3.Text = "+" Then

            Else
                If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN3.Text = "" Then
                    If txtDesc.Text <> "" Then
                        txtDesc.Focus()
                        txtDesc.SelectAll()
                    Else
                        txtDesc.Focus()
                    End If
                    validarCuenta()
                Else
                    pregunta = False
                    txtN3.Text = txtN3.Text.PadLeft(p.NNiv3, "0")

                    'txtN4.Text = ""
                    'txtN5.Text = ""
                    txtN4.Enabled = True
                    'txtN5.Enabled = False
                    txtN4.Focus()
                    If volverVer = False Then
                        verificaCuenta(False, 3)
                    End If
                    If pregunta Then
                        txtN3.Focus()
                    End If
                End If

            End If

        Else
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN3.Text = "" Then
                txtDesc.Focus()
                ' validarCuenta()
            End If

            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = " ") Or (e.KeyChar = "+")) Then
                e.Handled = True
            End If


        End If
    End Sub

    Private Sub txtN4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtN4.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN4.Text <> "" Then
            If txtN4.Text = "+" Then

            Else
                If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN4.Text = "" Then
                    If txtDesc.Text <> "" Then
                        txtDesc.Focus()
                        txtDesc.SelectAll()
                    Else
                        txtDesc.Focus()
                    End If
                    validarCuenta()
                Else
                    pregunta = False
                    txtN4.Text = txtN4.Text.PadLeft(p.NNiv4, "0")

                    'txtN5.Text = ""

                    txtN5.Enabled = True
                    txtN5.Focus()
                    If volverVer = False Then
                        verificaCuenta(False, 4)
                    End If
                    If pregunta Then
                        txtN4.Focus()
                    End If
                End If

            End If

        Else
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN4.Text = "" Then
                txtDesc.Focus()
                '  validarCuenta()
            End If

            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = " ") Or (e.KeyChar = "+")) Then
                e.Handled = True
            End If


        End If
    End Sub

    Private Sub txtN5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtN5.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN5.Text <> "" Then
            If txtN5.Text = "+" Then

            Else
                If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN5.Text = "" Then
                    pregunta = False
                    verificaCuenta(False, 5)
                    If auxNiv5 Then
                        If txtDesc.Text <> "" Then
                            txtDesc.Focus()
                            txtDesc.SelectAll()
                        Else
                            txtDesc.Focus()
                        End If

                        validarCuenta()
                    End If
                    If pregunta Then
                        txtN5.Focus()
                    End If

                Else
                    txtN5.Text = txtN5.Text.PadLeft(p.NNiv5, "0")
                    verificaCuenta(False, 5)
                    If auxNiv5 Then
                        txtDesc.Focus()
                        validarCuenta()
                    End If
                    ' txtDesc.Focus()
                End If

            End If

        Else
            If e.KeyChar = Microsoft.VisualBasic.ChrW(13) And txtN5.Text = "" Then
                txtDesc.Focus()
                ' validarCuenta()
            End If

            Dim textBox As TextBox = DirectCast(sender, TextBox)
            If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = " ") Or (e.KeyChar = "+")) Then
                e.Handled = True
            End If


        End If
    End Sub


    Private Sub txtDesc_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDesc.Enter
        If txtCuenta.Text <> "" Then
            validarCuenta()
        End If
        If pnlDatosIVA.Visible = True Then
            txtFolioFactura.Focus()
        End If

    End Sub

    Private Sub txtCargo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCargo.Enter
        If txtCuenta.Text <> "" Then
            validarCuenta()
        End If

    End Sub

    Private Sub txtAbono_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbono.Enter
        If txtCuenta.Text <> "" Then
            validarCuenta()
        End If

    End Sub

    Private Sub btnAgregarDetalle_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarDetalle.Enter
        If txtCuenta.Text <> "" Then
            validarCuenta()
        End If

    End Sub

    Private Sub txtCuenta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCuenta.TextChanged
        If txtCuenta.Text.Length = txtCuenta.MaxLength Then
            txtCuenta.Text = txtCuenta.Text.PadLeft(p.NNiv1, "0")

            txtN2.Text = ""
            txtN3.Text = ""
            txtN4.Text = ""
            txtN5.Text = ""
            txtN2.Enabled = True
            txtN3.Enabled = False
            txtN4.Enabled = False
            txtN5.Enabled = False
            txtN2.Enabled = True
            txtN2.Focus()
            verificaCuenta(False, 1)
        End If

    End Sub

    Private Sub txtN2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN2.TextChanged
        If txtN2.Text.Length = txtN2.MaxLength Then
            txtN2.Text = txtN2.Text.PadLeft(p.NNiv2, "0")
            txtN3.Text = ""
            txtN4.Text = ""
            txtN5.Text = ""
            txtN3.Enabled = True
            txtN4.Enabled = False
            txtN5.Enabled = False
            txtN3.Focus()
            verificaCuenta(False, 2)
        End If

    End Sub

    Private Sub txtN3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN3.TextChanged
        If txtN3.Text.Length = txtN3.MaxLength Then
            txtN3.Text = txtN3.Text.PadLeft(p.NNiv3, "0")
            txtN4.Text = ""
            txtN5.Text = ""
            txtN4.Enabled = True
            txtN5.Enabled = False
            txtN4.Focus()
            verificaCuenta(False, 3)
        End If

    End Sub

    Private Sub txtN4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN4.TextChanged
        If txtN4.Text.Length = txtN4.MaxLength Then
            txtN4.Text = txtN4.Text.PadLeft(p.NNiv4, "0")
            txtN5.Text = ""
            txtN5.Enabled = True
            txtN5.Focus()
            verificaCuenta(False, 4)
        End If

    End Sub

    Private Sub txtN5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN5.TextChanged
        If txtN5.Text.Length = txtN5.MaxLength Then
            verificaCuenta(False, 5)
            If auxNiv5 Then
                txtDesc.Focus()
                validarCuenta()
            End If
        End If

    End Sub

    Private Sub frmContabilidadPolizas_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F10 Then
            guardar()
        End If
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If

    End Sub

    Private Sub txtN2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtN2.KeyDown
        If e.KeyCode = Keys.Back And txtN2.Text = "" Then
            txtCuenta.Focus()
            txtCuenta.Select(txtCuenta.Text.Length, 0)
        End If
        If e.KeyCode = Keys.F1 Then
            buscar()
        End If
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            txtCuenta.Focus()
        End If
    End Sub

    Private Sub txtN3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtN3.KeyDown
        If e.KeyCode = Keys.Back And txtN3.Text = "" Then
            txtN2.Focus()
            txtN2.Select(txtN2.Text.Length, 0)
        End If
        If e.KeyCode = Keys.F1 Then
            buscar()
        End If
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            txtN2.Focus()
        End If
    End Sub

    Private Sub txtN4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtN4.KeyDown
        If e.KeyCode = Keys.Back And txtN4.Text = "" Then
            txtN3.Focus()
            txtN3.Select(txtN3.Text.Length, 0)
        End If
        If e.KeyCode = Keys.F1 Then
            buscar()
        End If
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            txtN3.Focus()
        End If
    End Sub

    Private Sub txtN5_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtN5.KeyDown
        If e.KeyCode = Keys.Back And txtN5.Text = "" Then
            txtN4.Focus()
            txtN4.Select(txtN4.Text.Length, 0)
        End If
        If e.KeyCode = Keys.F1 Then
            buscar()
        End If
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            txtN4.Focus()
        End If
    End Sub

    Private Sub txtN2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN2.Enter
        If txtCuenta.Text = "" Then
            txtCuenta.Focus()
        End If
    End Sub

    Private Sub txtN3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN3.Enter
        If txtN2.Text = "" Then
            txtN2.Focus()
        End If

    End Sub

    Private Sub txtN4_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN4.Enter
        If txtN3.Text = "" Then
            txtN3.Focus()
        End If
    End Sub

    Private Sub txtN5_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN5.Enter
        If txtN4.Text = "" Then
            txtN4.Focus()
        End If
    End Sub

    Private Sub txtCargo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCargo.KeyDown
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            Dim aux As String = txtCargo.Text
            txtCargo.Text = ""
            txtAbono.Text = aux

            txtAbono.Focus()

        End If
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            txtDesc.Focus()
        End If
        If e.KeyCode = Keys.Tab Then
            If txtCargo.Text <> "" Then
                botonAgregar()
            End If

        End If


    End Sub

    Private Sub txtAbono_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAbono.KeyDown
        If e.KeyCode = Keys.Space Then
            e.Handled = True
            Dim aux As String = txtAbono.Text
            txtAbono.Text = txtCargo.Text
            txtCargo.Text = aux
            txtCargo.Focus()
        End If
        'If e.KeyCode = Keys.F5 Then
        '    nuevo()
        'End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            txtCargo.Focus()
        End If
    End Sub

    Private Sub txtNumeroPoliza_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNumeroPoliza.KeyDown
        'If e.KeyCode = Keys.F5 Then
        '    nuevo()
        'End If
        If e.KeyCode = Keys.Escape Then
            cmbTipoPolizaX.Focus()
        End If
    End Sub

    Private Sub dtpFecha_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpFecha.KeyDown
        'If e.KeyCode = Keys.F5 Then
        '    nuevo()
        'End If
        If e.KeyCode = Keys.Enter Then
            txtConcepto.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtNumeroPoliza.Focus()
        End If
    End Sub

    Private Sub txtConcepto_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtConcepto.KeyDown
        'If e.KeyCode = Keys.F5 Then
        '    nuevo()
        'End If
        If e.KeyCode = Keys.Escape Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub txtBeneficiario_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBeneficiario.KeyDown
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Then
            txtConcepto.Focus()
        End If
    End Sub

    Private Sub txtDesc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDesc.KeyDown
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Enter Then
            If txtCargo.Text <> "" Then
                txtCargo.Focus()
            Else
                If txtAbono.Text <> "" Then
                    txtAbono.Focus()
                Else
                    txtCargo.Focus()
                End If
            End If
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            'If txtN5.Enabled = True Then
            '    txtN5.Focus()
            'Else
            '    If txtN4.Enabled = True Then
            '        txtN4.Focus()
            '    Else
            '        If txtN3.Enabled = True Then
            '            txtN3.Focus()
            '        Else

            '            If txtN2.Enabled = True Then
            '                txtN2.Focus()
            '            Else
            '                txtCuenta.Focus()
            '            End If
            '        End If

            '    End If
            'End If
            txtCuenta.Focus()
        End If
    End Sub

    Private Sub txtBuscarConecpto_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscarConecpto.KeyDown
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Then
            btnImprimir.Focus()
        End If
    End Sub

    Private Sub cmbTipoPoliza_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F5 Then
            nuevo()
        End If
        If e.KeyCode = Keys.Escape Then
            DataGridView1.Focus()
        End If
    End Sub

    Private Sub cmbTipoBuscar_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoBuscar.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Imprimir()
    End Sub
    Private Sub llenarTabla(ByVal pId As String, ByVal pN1 As String, ByVal pN2 As String, ByVal pN3 As String, ByVal pN4 As String, ByVal pN5 As String, ByVal pDescripcionCuenta As String, ByVal pDescripcion As String, ByVal pParcial As Double, ByVal pCargo As Double, ByVal pAbono As Double)

        Dim dr As DataRow
        dr = tablaImpres.NewRow()
        dr("N1") = pN1
        dr("N2") = pN2
        dr("N3") = pN3
        dr("N4") = pN4
        dr("N5") = pN5
        dr("desCuenta") = pDescripcionCuenta
        dr("descripcion") = pDescripcion
        If pParcial = 0 Then
            dr("parcial") = ""
        Else
            dr("parcial") = pParcial.ToString("C2").PadLeft("13")
        End If

        If pCargo = 0 Then
            dr("Cargo") = ""
        Else
            dr("Cargo") = pCargo.ToString("C2").PadLeft("13")
        End If
        If pAbono = 0 Then
            dr("Abono") = ""
        Else
            dr("Abono") = pAbono.ToString("C2").PadLeft("13")
        End If

        tablaImpres.Rows.Add(dr)
    End Sub
    Private Function sacarCuentas(ByVal pCuenta As String, ByVal pRenglon As Integer)
        totalAbono = 0
        totalCargo = 0
        Dim HuboCambio As Boolean = False
        Dim Temp As Byte = 0
        For i As Integer = pRenglon To Tabla2.Rows.Count - 1
            If pRenglon <= Tabla2.Rows.Count - 1 Then
                If Tabla2.Rows(i)(1).ToString = pCuenta And HuboCambio = False Then
                    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                        totalCargo += Double.Parse(Tabla2.Rows(i)(7).ToString)
                    Else
                        If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                            totalAbono += Double.Parse(Tabla2.Rows(i)(8).ToString)
                        End If
                    End If
                    If totalAbono <> 0 And Temp = 0 Then
                        Temp = 1
                    Else
                        If Temp = 2 And totalAbono <> 0 Then
                            HuboCambio = True
                            totalAbono = 0
                        End If
                    End If
                    If totalCargo <> 0 And Temp = 0 Then
                        Temp = 2
                    Else
                        If Temp = 1 And totalCargo <> 0 Then
                            HuboCambio = True
                            totalCargo = 0
                        End If
                    End If

                Else
                    If totalAbono = 0 And totalCargo = 0 Then
                        Return 0
                    Else
                        'If Tabla2.Rows(pRenglon)(7).ToString <> "" And Tabla2.Rows(pRenglon)(7).ToString <> "0" Then
                        '    totalCargo += Double.Parse(Tabla2.Rows(pRenglon)(7).ToString)
                        'Else
                        '    If Tabla2.Rows(pRenglon)(8).ToString <> "" And Tabla2.Rows(pRenglon)(8).ToString <> "0" Then
                        '        totalAbono += Double.Parse(Tabla2.Rows(pRenglon)(8).ToString)
                        '    End If
                        'End If
                        Return 1
                    End If
                End If
            Else
                If totalAbono = 0 And totalCargo = 0 Then
                    Return 0
                Else
                    'If Tabla2.Rows(pRenglon)(7).ToString <> "" And Tabla2.Rows(pRenglon)(7).ToString <> "0" Then
                    '    totalCargo += Double.Parse(Tabla2.Rows(pRenglon)(7).ToString)
                    'Else
                    '    If Tabla2.Rows(pRenglon)(8).ToString <> "" And Tabla2.Rows(pRenglon)(8).ToString <> "0" Then
                    '        totalAbono += Double.Parse(Tabla2.Rows(pRenglon)(8).ToString)
                    '    End If
                    'End If
                    Return 1
                End If
            End If
        Next
        'If totalAbono = 0 And totalCargo = 0 Then
        '    Return 0
        'Else
        '    If Tabla2.Rows(pRenglon)(7).ToString <> "" And Tabla2.Rows(pRenglon)(7).ToString <> "0" Then
        '        totalCargo += Double.Parse(Tabla2.Rows(pRenglon)(7).ToString)
        '    Else
        '        If Tabla2.Rows(pRenglon)(8).ToString <> "" And Tabla2.Rows(pRenglon)(8).ToString <> "0" Then
        '            totalAbono += Double.Parse(Tabla2.Rows(pRenglon)(8).ToString)
        '        End If
        '    End If
        '    Return 1
        'End If
        Return 1
    End Function


    Private Function sacarCuentasbk(ByVal pCuenta As String, ByVal pRenglon As Integer)
        totalAbono = 0
        totalCargo = 0
        For i As Integer = pRenglon + 1 To Tabla2.Rows.Count - 1
            If pRenglon <= Tabla2.Rows.Count - 1 Then
                If Tabla2.Rows(i)(1).ToString = pCuenta Then
                    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                        totalCargo += Double.Parse(Tabla2.Rows(i)(7).ToString)
                    Else
                        If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                            totalAbono += Double.Parse(Tabla2.Rows(i)(8).ToString)
                        End If
                    End If
                Else
                    If totalAbono = 0 And totalCargo = 0 Then
                        Return 0
                    Else
                        If Tabla2.Rows(pRenglon)(7).ToString <> "" And Tabla2.Rows(pRenglon)(7).ToString <> "0" Then
                            totalCargo += Double.Parse(Tabla2.Rows(pRenglon)(7).ToString)
                        Else
                            If Tabla2.Rows(pRenglon)(8).ToString <> "" And Tabla2.Rows(pRenglon)(8).ToString <> "0" Then
                                totalAbono += Double.Parse(Tabla2.Rows(pRenglon)(8).ToString)
                            End If
                        End If
                        Return 1
                    End If
                End If
            Else
                If totalAbono = 0 And totalCargo = 0 Then
                    Return 0
                Else
                    If Tabla2.Rows(pRenglon)(7).ToString <> "" And Tabla2.Rows(pRenglon)(7).ToString <> "0" Then
                        totalCargo += Double.Parse(Tabla2.Rows(pRenglon)(7).ToString)
                    Else
                        If Tabla2.Rows(pRenglon)(8).ToString <> "" And Tabla2.Rows(pRenglon)(8).ToString <> "0" Then
                            totalAbono += Double.Parse(Tabla2.Rows(pRenglon)(8).ToString)
                        End If
                    End If
                    Return 1
                End If
            End If
        Next
        If totalAbono = 0 And totalCargo = 0 Then
            Return 0
        Else
            If Tabla2.Rows(pRenglon)(7).ToString <> "" And Tabla2.Rows(pRenglon)(7).ToString <> "0" Then
                totalCargo += Double.Parse(Tabla2.Rows(pRenglon)(7).ToString)
            Else
                If Tabla2.Rows(pRenglon)(8).ToString <> "" And Tabla2.Rows(pRenglon)(8).ToString <> "0" Then
                    totalAbono += Double.Parse(Tabla2.Rows(pRenglon)(8).ToString)
                End If
            End If
            Return 1
        End If

    End Function

    Private Sub desglosar()
        tablaImpres.Clear()
        n1 = ""
        n2 = ""
        n3 = ""
        n4 = ""
        n5 = ""
        Dim HuboCambio As Boolean = False
        Dim Temp As Integer = 0
        Dim ParcialAnt As Double = 0
        For i As Integer = 0 To Tabla2.Rows.Count - 1

            For j As Integer = 1 To Integer.Parse(Tabla2.Rows(i)(9))
                If j = 1 Then
                    'NIVEL 1
                    ' n1 = Tabla2.Rows(i)(1).ToString.PadLeft(p.NNiv1, "0")
                    'Aqui esta el rollo haber si le atino :p
                    If Tabla2.Rows(i)(1).ToString <> n1 Or HuboCambio = True Then
                        'es repetido
                        n1 = Tabla2.Rows(i)(1).ToString
                        n2 = ""
                        n3 = ""
                        n4 = ""
                        n5 = ""
                        If sacarCuentas(n1, i) = 0 Then
                            If Tabla2.Rows(i)(7).ToString <> "0" And Tabla2.Rows(i)(7).ToString <> "" Then
                                cargo = Tabla2.Rows(i)(7).ToString
                            Else
                                cargo = "0"
                            End If
                            If Tabla2.Rows(i)(8).ToString <> "0" And Tabla2.Rows(i)(8).ToString <> "" Then
                                abono = Tabla2.Rows(i)(8).ToString
                            Else
                                abono = "0"
                            End If

                        Else
                            cargo = totalCargo
                            abono = totalAbono
                            HuboCambio = False
                            Temp = 0
                            ParcialAnt = 0
                        End If
                        C.buscarID(n1, "", "", "", "", 1)
                        parcial = 0

                        If i < Tabla2.Rows.Count - 1 Then
                            If Tabla2.Rows(i + 1)(1).ToString = n1 And Tabla2.Rows(i)(2).ToString = "" Then
                                Dim NoPon As Boolean = False
                                If i > 0 Then
                                    If Tabla2.Rows(i - 1)(1).ToString = n1 Then
                                        NoPon = True
                                    End If
                                End If
                                Dim ConParcial As Boolean = True
                                If NoPon = False Then

                                    If Tabla2.Rows(i)(7).ToString <> "0" And Tabla2.Rows(i)(7).ToString <> "" Then
                                        parcial = Tabla2.Rows(i)(7).ToString
                                        If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                            If Temp = 0 Then parcial = 0
                                            HuboCambio = True
                                            ConParcial = False
                                        End If
                                        Temp += 1
                                    Else
                                        If Tabla2.Rows(i)(8).ToString <> "0" And Tabla2.Rows(i)(8).ToString <> "" Then
                                            parcial = Tabla2.Rows(i)(8).ToString
                                            If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                                If Temp = 0 Then parcial = 0
                                                HuboCambio = True
                                                ConParcial = False
                                            End If
                                            Temp += 1
                                        Else
                                            parcial = "0"
                                        End If
                                    End If
                                    If ConParcial = False Then descripcion = Tabla2.Rows(i)(6).ToString()
                                    llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(p.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, cargo, abono)
                                End If

                                'agrega parcial
                                If Tabla2.Rows(i)(9) = 1 Then
                                    descripcion = Tabla2.Rows(i)(6).ToString()
                                Else
                                    descripcion = ""
                                End If

                                If Tabla2.Rows(i)(7).ToString <> "0" And Tabla2.Rows(i)(7).ToString <> "" Then
                                    parcial = Tabla2.Rows(i)(7).ToString
                                    If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                        If Temp = 0 Then parcial = 0
                                        HuboCambio = True
                                    End If
                                    Temp += 1
                                Else
                                    If Tabla2.Rows(i)(8).ToString <> "0" And Tabla2.Rows(i)(8).ToString <> "" Then
                                        parcial = Tabla2.Rows(i)(8).ToString
                                        If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                            If Temp = 0 Then parcial = 0
                                            HuboCambio = True
                                        End If
                                        Temp += 1
                                    Else
                                        parcial = "0"
                                    End If
                                End If
                                If ConParcial Then llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(p.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, 0, 0)
                            Else
                                If Tabla2.Rows(i)(9) = 1 Then
                                    descripcion = Tabla2.Rows(i)(6).ToString()
                                Else
                                    descripcion = ""
                                End If
                                llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(p.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, cargo, abono)
                            End If

                        Else
                            'Es el último renglon
                            If Tabla2.Rows(i)(9) = 1 Then
                                descripcion = Tabla2.Rows(i)(6).ToString()
                            Else
                                descripcion = ""
                            End If
                            llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(p.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, cargo, abono)
                        End If
                    Else
                        If Tabla2.Rows(i)(2).ToString = "" Then
                            'agrega parcial
                            If Tabla2.Rows(i)(9) = 1 Then
                                descripcion = Tabla2.Rows(i)(6).ToString()
                            Else
                                descripcion = ""
                            End If
                            If i < Tabla2.Rows.Count - 1 Then
                                If Tabla2.Rows(i)(7).ToString <> "0" And Tabla2.Rows(i)(7).ToString <> "" Then
                                    parcial = Tabla2.Rows(i)(7).ToString
                                    If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                        If Temp = 0 Then parcial = 0
                                        HuboCambio = True
                                    End If
                                    Temp += 1
                                Else
                                    If Tabla2.Rows(i)(8).ToString <> "0" And Tabla2.Rows(i)(8).ToString <> "" Then
                                        parcial = Tabla2.Rows(i)(8).ToString
                                        If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                            If Temp = 0 Then parcial = 0
                                            HuboCambio = True
                                        End If
                                        Temp += 1
                                    Else
                                        parcial = "0"
                                    End If
                                End If
                            Else
                                If Tabla2.Rows(i)(7).ToString <> "0" And Tabla2.Rows(i)(7).ToString <> "" Then
                                    parcial = Tabla2.Rows(i)(7).ToString
                                Else
                                    If Tabla2.Rows(i)(8).ToString <> "0" And Tabla2.Rows(i)(8).ToString <> "" Then
                                        parcial = Tabla2.Rows(i)(8).ToString
                                    Else
                                        parcial = "0"
                                    End If
                                End If
                            End If
                            llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(p.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, 0, 0)
                        End If
                    End If
                Else
                    If j = 2 Then
                        'NIVEL 2
                        If n2 <> Tabla2.Rows(i)(2).ToString Or Tabla2.Rows(i)(3).ToString = "" Then
                            n2 = Tabla2.Rows(i)(2).ToString
                            n3 = ""
                            n4 = ""
                            n5 = ""
                            C.buscarID(n1, n2, "", "", "", 2)
                            If (i + 1) <= Tabla2.Rows.Count - 1 And Tabla2.Rows(i)(3).ToString = "" Then
                                If Tabla2.Rows(i + 1)(1).ToString = n1 And HuboCambio = False Then
                                    ' For k As Integer = i To Tabla2.Rows.Count - 1
                                    If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                        If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                            parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                            If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                If Temp = 0 Then parcial = 0
                                                HuboCambio = True
                                            End If
                                            Temp += 1
                                        Else
                                            If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                                    If Temp = 0 Then parcial = 0
                                                    HuboCambio = True
                                                End If
                                            End If
                                            Temp += 1
                                        End If

                                        'Else
                                        '    k = Tabla2.Rows.Count + 1
                                    End If
                                    ' Next
                                    cargo = 0
                                    abono = 0
                                Else
                                    If (i - 1) >= 0 Then

                                        If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                            ' For k As Integer = i To Tabla2.Rows.Count - 1
                                            If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                                If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                                    parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                                    'If i + 1 < Tabla2.Rows.Count Then
                                                    If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                        If Temp = 0 Then parcial = 0
                                                        HuboCambio = True
                                                    End If
                                                    Temp += 1
                                                    'End If
                                                Else
                                                    If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                        parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                        If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                                            If Temp = 0 Then parcial = 0
                                                            HuboCambio = True
                                                        End If
                                                        Temp += 1
                                                    End If
                                                End If
                                                'Else
                                                '    k = Tabla2.Rows.Count + 1
                                            End If
                                            'Next
                                            cargo = 0
                                            abono = 0
                                        Else
                                            parcial = 0
                                            cargo = 0
                                            abono = 0
                                        End If

                                    Else
                                        parcial = 0
                                        cargo = 0
                                        abono = 0
                                    End If
                                End If
                            Else
                                If (i - 1) >= 0 And Tabla2.Rows(i)(3).ToString = "" Then

                                    If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                        For k As Integer = i To Tabla2.Rows.Count - 1
                                            If Tabla2.Rows(k)(1).ToString = n1 And HuboCambio = False Then
                                                If Tabla2.Rows(k)(7).ToString <> "" And Tabla2.Rows(k)(7).ToString <> "0" Then
                                                    parcial = Double.Parse(Tabla2.Rows(k)(7).ToString)
                                                    If (k + 1) < Tabla2.Rows.Count Then
                                                        If Tabla2.Rows(k + 1)(8).ToString <> "" And Tabla2.Rows(k + 1)(8).ToString <> "0" Then
                                                            If Temp = 0 Then parcial = 0
                                                            HuboCambio = True
                                                        End If
                                                    Else
                                                        If Tabla2.Rows(k - 1)(8).ToString <> "" And Tabla2.Rows(k - 1)(8).ToString <> "0" Then
                                                            If Temp = 0 Then parcial = 0
                                                            HuboCambio = True
                                                        End If
                                                    End If
                                                    Temp += 1
                                                Else
                                                    If Tabla2.Rows(k)(8).ToString <> "" And Tabla2.Rows(k)(8).ToString <> "0" Then
                                                        If Tabla2.Rows(k)(8).ToString <> "" And Tabla2.Rows(k)(8).ToString <> "0" Then
                                                            parcial = Double.Parse(Tabla2.Rows(k)(8).ToString)
                                                            If (k + 1) < Tabla2.Rows.Count Then
                                                                If Tabla2.Rows(k + 1)(7).ToString <> "" And Tabla2.Rows(k + 1)(7).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            Else
                                                                If Tabla2.Rows(k - 1)(7).ToString <> "" And Tabla2.Rows(k - 1)(7).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            Else
                                                k = Tabla2.Rows.Count + 1
                                            End If
                                        Next
                                        cargo = 0
                                        abono = 0
                                    Else
                                        parcial = 0
                                        cargo = 0
                                        abono = 0
                                    End If

                                Else
                                    parcial = 0
                                    cargo = 0
                                    abono = 0
                                End If
                            End If

                            If Tabla2.Rows(i)(9) = 2 Then
                                descripcion = Tabla2.Rows(i)(6).ToString()
                            Else
                                descripcion = ""
                            End If
                            llenarTabla("", "", n2.PadLeft(p.NNiv2, "0"), "", "", "", C.Descripcion2, descripcion, parcial, cargo, abono)
                        End If

                    Else
                        If j = 3 Then
                            'NIVEL 3
                            If n3 <> Tabla2.Rows(i)(3).ToString Or Tabla2.Rows(i)(4).ToString = "" Then
                                n3 = Tabla2.Rows(i)(3).ToString
                                n4 = ""
                                n5 = ""
                                C.buscarID(n1, n2, n3, "", "", 3)
                                'If p.contadorCuentas(n1, idPoliza) > 1 Then
                                '    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '        parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '    Else
                                '        parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '    End If
                                '    cargo = 0
                                '    abono = 0
                                '    'Else
                                '    parcial = 0
                                '    cargo = 0
                                '    abono = 0
                                'End If
                                'If (i + 1) <= Tabla2.Rows.Count - 1 And Tabla2.Rows(i)(3).ToString = "" Then
                                If (i + 1) <= Tabla2.Rows.Count - 1 And Tabla2.Rows(i)(4).ToString = "" Then
                                    If Tabla2.Rows(i + 1)(1).ToString = n1 And HuboCambio = False Then
                                        ' For k As Integer = i To Tabla2.Rows.Count - 1
                                        If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                            If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                                parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                                If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                    If Temp = 0 Then parcial = 0
                                                    HuboCambio = True
                                                End If
                                                Temp += 1
                                            Else
                                                If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                    parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                    If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                                        If Temp = 0 Then parcial = 0
                                                        HuboCambio = True
                                                    End If
                                                    Temp += 1
                                                End If
                                            End If


                                            'Else
                                            '    k = Tabla2.Rows.Count + 1
                                        End If
                                        ' Next
                                        cargo = 0
                                        abono = 0
                                    Else
                                        If (i - 1) >= 0 Then

                                            If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                                '  For k As Integer = i To Tabla2.Rows.Count - 1
                                                If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                                    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                                        parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                                        If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                            If Temp = 0 Then parcial = 0
                                                            HuboCambio = True
                                                        End If
                                                        Temp += 1
                                                    Else
                                                        If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                            parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                            If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                                If Temp = 0 Then parcial = 0
                                                                HuboCambio = True
                                                            End If
                                                            Temp += 1
                                                        End If
                                                    End If
                                                    'Else
                                                    '    k = Tabla2.Rows.Count + 1
                                                End If
                                                '  Next
                                                cargo = 0
                                                abono = 0
                                            Else
                                                parcial = 0
                                                cargo = 0
                                                abono = 0
                                            End If

                                        Else
                                            parcial = 0
                                            cargo = 0
                                            abono = 0
                                        End If
                                    End If
                                Else
                                    If (i - 1) >= 0 And Tabla2.Rows(i)(4).ToString = "" Then

                                        If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                            For k As Integer = i To Tabla2.Rows.Count - 1
                                                If Tabla2.Rows(k)(1).ToString = n1 And HuboCambio = False Then
                                                    If Tabla2.Rows(k)(7).ToString <> "" And Tabla2.Rows(k)(7).ToString <> "0" Then
                                                        parcial = Double.Parse(Tabla2.Rows(k)(7).ToString)
                                                        If (k + 1) < Tabla2.Rows.Count Then
                                                            If Tabla2.Rows(k + 1)(8).ToString <> "" And Tabla2.Rows(k + 1)(8).ToString <> "0" Then
                                                                If Temp = 0 Then parcial = 0
                                                                HuboCambio = True
                                                            End If
                                                            Temp += 1
                                                        Else
                                                            If Tabla2.Rows(k - 1)(8).ToString <> "" And Tabla2.Rows(k - 1)(8).ToString <> "0" Then
                                                                If Temp = 0 Then parcial = 0
                                                                HuboCambio = True
                                                            End If
                                                            Temp += 1
                                                        End If

                                                    Else
                                                        If Tabla2.Rows(k)(8).ToString <> "" And Tabla2.Rows(k)(8).ToString <> "0" Then
                                                            parcial = Double.Parse(Tabla2.Rows(k)(8).ToString)
                                                            If (k + 1) < Tabla2.Rows.Count Then
                                                                If Tabla2.Rows(k + 1)(7).ToString <> "" And Tabla2.Rows(k + 1)(7).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            Else
                                                                If Tabla2.Rows(k - 1)(7).ToString <> "" And Tabla2.Rows(k - 1)(7).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            End If

                                                        End If
                                                    End If
                                                Else
                                                    k = Tabla2.Rows.Count + 1
                                                End If
                                            Next
                                            cargo = 0
                                            abono = 0
                                        Else
                                            parcial = 0
                                            cargo = 0
                                            abono = 0
                                        End If

                                    Else
                                        parcial = 0
                                        cargo = 0
                                        abono = 0
                                    End If
                                End If

                                If Tabla2.Rows(i)(9) = 3 Then
                                    descripcion = Tabla2.Rows(i)(6).ToString()
                                Else
                                    descripcion = ""
                                End If
                                llenarTabla("", "", "", n3.PadLeft(p.NNiv3, "0"), "", "", C.Descripcion3, descripcion, parcial, cargo, abono)
                            End If

                        Else
                            If j = 4 Then
                                'NIVEL 4

                                If n4 <> Tabla2.Rows(i)(4).ToString Or Tabla2.Rows(i)(5).ToString = "" Then
                                    n4 = Tabla2.Rows(i)(4).ToString
                                    n5 = ""
                                    C.buscarID(n1, n2, n3, n4, "", 4)
                                    If (i + 1) <= Tabla2.Rows.Count - 1 And Tabla2.Rows(i)(5).ToString = "" Then
                                        If Tabla2.Rows(i + 1)(1).ToString = n1 And HuboCambio = False Then
                                            ' For k As Integer = i To Tabla2.Rows.Count - 1
                                            If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                                If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                                    parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                                    If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                        If Temp = 0 Then parcial = 0
                                                        HuboCambio = True
                                                    End If
                                                    Temp += 1
                                                Else
                                                    If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                        parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                        If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                                            If Temp = 0 Then parcial = 0
                                                            HuboCambio = True
                                                        End If
                                                        Temp += 1
                                                    End If
                                                End If


                                                'Else
                                                '    k = Tabla2.Rows.Count + 1
                                            End If
                                            ' Next
                                            cargo = 0
                                            abono = 0
                                        Else
                                            If (i - 1) >= 0 Then

                                                If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                                    '  For k As Integer = i To Tabla2.Rows.Count - 1
                                                    If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                                        If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                                            parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                                            If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                                If Temp = 0 Then parcial = 0
                                                                HuboCambio = True
                                                            End If
                                                            Temp += 1
                                                        Else
                                                            If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                                parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                                If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            End If
                                                        End If
                                                        'Else
                                                        '    k = Tabla2.Rows.Count + 1
                                                    End If
                                                    '  Next
                                                    cargo = 0
                                                    abono = 0
                                                Else
                                                    parcial = 0
                                                    cargo = 0
                                                    abono = 0
                                                End If

                                            Else
                                                parcial = 0
                                                cargo = 0
                                                abono = 0
                                            End If
                                        End If
                                    Else
                                        If (i - 1) >= 0 And Tabla2.Rows(i)(5).ToString = "" Then

                                            If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                                For k As Integer = i To Tabla2.Rows.Count - 1
                                                    If Tabla2.Rows(k)(1).ToString = n1 And HuboCambio = False Then
                                                        If Tabla2.Rows(k)(7).ToString <> "" And Tabla2.Rows(k)(7).ToString <> "0" Then
                                                            parcial = Double.Parse(Tabla2.Rows(k)(7).ToString)
                                                            If (k + 1) < Tabla2.Rows.Count Then
                                                                If Tabla2.Rows(k + 1)(8).ToString <> "" And Tabla2.Rows(k + 1)(8).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            Else
                                                                If Tabla2.Rows(k - 1)(8).ToString <> "" And Tabla2.Rows(k - 1)(8).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            End If

                                                        Else
                                                            If Tabla2.Rows(k)(8).ToString <> "" And Tabla2.Rows(k)(8).ToString <> "0" Then
                                                                parcial = Double.Parse(Tabla2.Rows(k)(8).ToString)
                                                                If (k + 1) < Tabla2.Rows.Count Then
                                                                    If Tabla2.Rows(k + 1)(7).ToString <> "" And Tabla2.Rows(k + 1)(7).ToString <> "0" Then
                                                                        If Temp = 0 Then parcial = 0
                                                                        HuboCambio = True
                                                                    End If
                                                                    Temp += 1
                                                                Else
                                                                    If Tabla2.Rows(k - 1)(7).ToString <> "" And Tabla2.Rows(k - 1)(7).ToString <> "0" Then
                                                                        If Temp = 0 Then parcial = 0
                                                                        HuboCambio = True
                                                                    End If
                                                                    Temp += 1
                                                                End If

                                                            End If
                                                        End If
                                                    Else
                                                        k = Tabla2.Rows.Count + 1
                                                    End If
                                                Next
                                                cargo = 0
                                                abono = 0
                                            Else
                                                parcial = 0
                                                cargo = 0
                                                abono = 0
                                            End If

                                        Else
                                            parcial = 0
                                            cargo = 0
                                            abono = 0
                                        End If
                                    End If

                                    If Tabla2.Rows(i)(9) = 4 Then
                                        descripcion = Tabla2.Rows(i)(6).ToString()
                                    Else
                                        descripcion = ""
                                    End If
                                    llenarTabla("", "", "", "", n4.PadLeft(p.NNiv4, "0"), "", C.Descripcion4, descripcion, parcial, cargo, abono)
                                End If



                                'If n4 <> Tabla2.Rows(i)(4).ToString Or Tabla2.Rows(i)(5).ToString = "" Then
                                '    n4 = Tabla2.Rows(i)(4).ToString
                                '    n5 = ""
                                '    C.buscarID(n1, n2, n3, n4, "", 4)
                                '    'If p.contadorCuentas(n1, idPoliza) > 1 Then
                                '    '    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '    '        parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '    '    Else
                                '    '        parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '    '    End If
                                '    '    cargo = 0
                                '    '    abono = 0
                                '    'Else
                                '    '    parcial = 0
                                '    '    cargo = 0
                                '    '    abono = 0
                                '    'End If
                                '    If (i + 1) <= Tabla2.Rows.Count - 1 And Tabla2.Rows(i)(5).ToString = "" Then
                                '        If Tabla2.Rows(i + 1)(1).ToString = n1 And HuboCambio = False Then
                                '            ' For k As Integer = i To Tabla2.Rows.Count - 1
                                '            If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                '                If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '                    parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '                    If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                '                        If Temp = 0 Then parcial = 0
                                '                        HuboCambio = True
                                '                    End If
                                '                    Temp += 1
                                '                Else
                                '                    If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                '                        parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '                        If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                '                            If Temp = 0 Then parcial = 0
                                '                            HuboCambio = True
                                '                        End If
                                '                        Temp += 1
                                '                    End If
                                '                End If


                                '                'Else
                                '                '   k = Tabla2.Rows.Count + 1
                                '            End If
                                '            ''  Next
                                '            cargo = 0
                                '            abono = 0
                                '        Else
                                '            If (i - 1) >= 0 Then

                                '                If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                '                    ' For k As Integer = i To Tabla2.Rows.Count - 1
                                '                    If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                '                        If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '                            parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '                            If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                '                                If Temp = 0 Then parcial = 0
                                '                                HuboCambio = True
                                '                            End If
                                '                            Temp += 1
                                '                        Else
                                '                            If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                '                                parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '                                If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                '                                    If Temp = 0 Then parcial = 0
                                '                                    HuboCambio = True
                                '                                End If
                                '                                Temp += 1
                                '                            End If
                                '                        End If
                                '                        'Else
                                '                        '   k = Tabla2.Rows.Count + 1
                                '                    End If
                                '                    ' Next
                                '                    cargo = 0
                                '                    abono = 0
                                '                Else
                                '                    parcial = 0
                                '                    cargo = 0
                                '                    abono = 0
                                '                End If

                                '            Else
                                '                parcial = 0
                                '                cargo = 0
                                '                abono = 0
                                '            End If
                                '        End If
                                '    Else
                                '        If (i - 1) >= 0 And Tabla2.Rows(i)(5).ToString = "" Then

                                '            If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                '                ' For k As Integer = i To Tabla2.Rows.Count - 1
                                '                If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                '                        If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '                            parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '                            If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                '                            If Temp = 0 Then parcial = 0
                                '                            HuboCambio = True
                                '                        End If
                                '                        Temp += 1
                                '                        Else
                                '                            If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                '                                parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '                            If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                '                                If Temp = 0 Then parcial = 0
                                '                                HuboCambio = True
                                '                            End If
                                '                            Temp += 1
                                '                            End If
                                '                        End If

                                '                    End If
                                '                    'Else
                                '                    '  k = Tabla2.Rows.Count + 1
                                '                ' Next
                                '                cargo = 0
                                '                abono = 0
                                '            Else
                                '                parcial = 0
                                '                cargo = 0
                                '                abono = 0
                                '            End If

                                '        Else
                                '            parcial = 0
                                '            cargo = 0
                                '            abono = 0
                                '        End If
                                '    End If

                                '    If Tabla2.Rows(i)(9) = 4 Then
                                '        descripcion = Tabla2.Rows(i)(6).ToString()
                                '    Else
                                '        descripcion = ""
                                '    End If
                                '    llenarTabla("", "", "", "", n4.PadLeft(p.NNiv4, "0"), "", C.Descripcion, descripcion, parcial, cargo, abono)
                                'End If


                            Else
                                'NIVEL 5

                                'If n5 <> Tabla2.Rows(i)(5).ToString Then
                                n5 = Tabla2.Rows(i)(5).ToString
                                C.buscarID(n1, n2, n3, n4, n5, 5)
                                If (i + 1) <= Tabla2.Rows.Count - 1 Then
                                    If Tabla2.Rows(i + 1)(1).ToString = n1 And HuboCambio = False Then
                                        ' For k As Integer = i To Tabla2.Rows.Count - 1
                                        If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                            If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                                parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                                If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                    If Temp = 0 Then parcial = 0
                                                    HuboCambio = True
                                                End If
                                                Temp += 1
                                            Else
                                                If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                    parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                    If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                                        If Temp = 0 Then parcial = 0
                                                        HuboCambio = True
                                                    End If
                                                    Temp += 1
                                                End If
                                            End If


                                            'Else
                                            '    k = Tabla2.Rows.Count + 1
                                        End If
                                        ' Next
                                        cargo = 0
                                        abono = 0
                                    Else
                                        If (i - 1) >= 0 Then

                                            If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                                '  For k As Integer = i To Tabla2.Rows.Count - 1
                                                If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                                    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                                        parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                                        If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                            If Temp = 0 Then parcial = 0
                                                            HuboCambio = True
                                                        End If
                                                        Temp += 1
                                                    Else
                                                        If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                                            parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                                            If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                                                If Temp = 0 Then parcial = 0
                                                                HuboCambio = True
                                                            End If
                                                            Temp += 1
                                                        End If
                                                    End If
                                                    'Else
                                                    '    k = Tabla2.Rows.Count + 1
                                                End If
                                                '  Next
                                                cargo = 0
                                                abono = 0
                                            Else
                                                parcial = 0
                                                cargo = 0
                                                abono = 0
                                            End If

                                        Else
                                            parcial = 0
                                            cargo = 0
                                            abono = 0
                                        End If
                                    End If
                                Else
                                    If (i - 1) >= 0 Then

                                        If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                            For k As Integer = i To Tabla2.Rows.Count - 1
                                                If Tabla2.Rows(k)(1).ToString = n1 And HuboCambio = False Then
                                                    If Tabla2.Rows(k)(7).ToString <> "" And Tabla2.Rows(k)(7).ToString <> "0" Then
                                                        parcial = Double.Parse(Tabla2.Rows(k)(7).ToString)
                                                        If (k + 1) < Tabla2.Rows.Count Then
                                                            If Tabla2.Rows(k + 1)(8).ToString <> "" And Tabla2.Rows(k + 1)(8).ToString <> "0" Then
                                                                If Temp = 0 Then parcial = 0
                                                                HuboCambio = True
                                                            End If
                                                            Temp += 1
                                                        Else
                                                            If Tabla2.Rows(k - 1)(8).ToString <> "" And Tabla2.Rows(k - 1)(8).ToString <> "0" Then
                                                                If Temp = 0 Then parcial = 0
                                                                HuboCambio = True
                                                            End If
                                                            Temp += 1
                                                        End If

                                                    Else
                                                        If Tabla2.Rows(k)(8).ToString <> "" And Tabla2.Rows(k)(8).ToString <> "0" Then
                                                            parcial = Double.Parse(Tabla2.Rows(k)(8).ToString)
                                                            If (k + 1) < Tabla2.Rows.Count Then
                                                                If Tabla2.Rows(k + 1)(7).ToString <> "" And Tabla2.Rows(k + 1)(7).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            Else
                                                                If Tabla2.Rows(k - 1)(7).ToString <> "" And Tabla2.Rows(k - 1)(7).ToString <> "0" Then
                                                                    If Temp = 0 Then parcial = 0
                                                                    HuboCambio = True
                                                                End If
                                                                Temp += 1
                                                            End If

                                                        End If
                                                    End If
                                                Else
                                                    k = Tabla2.Rows.Count + 1
                                                End If
                                            Next
                                            cargo = 0
                                            abono = 0
                                        Else
                                            parcial = 0
                                            cargo = 0
                                            abono = 0
                                        End If

                                    Else
                                        parcial = 0
                                        cargo = 0
                                        abono = 0
                                    End If
                                End If

                                If Tabla2.Rows(i)(9) = 5 Then
                                    descripcion = Tabla2.Rows(i)(6).ToString()
                                Else
                                    descripcion = ""
                                End If
                                llenarTabla("", "", "", "", "", n5.PadLeft(p.NNiv5, "0"), C.Descripcion5, descripcion, parcial, cargo, abono)
                                'End If


                                ''  If n5 <> Tabla2.Rows(i)(5).ToString Or Tabla2.Rows(i)(3).ToString = "" Then
                                'n5 = Tabla2.Rows(i)(5).ToString
                                'C.buscarID(n1, n2, n3, n4, n5, 5)
                                ''If p.contadorCuentas(n1, idPoliza) > 1 Then
                                ''    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                ''        parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                ''    Else
                                ''        parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                ''    End If
                                ''    cargo = 0
                                ''    abono = 0
                                ''Else
                                ''    parcial = 0
                                ''    cargo = 0
                                ''    abono = 0
                                ''End If
                                'If (i + 1) <= Tabla2.Rows.Count - 1 Then
                                '    If Tabla2.Rows(i + 1)(1).ToString = n1 And HuboCambio = False Then
                                '        '    For k As Integer = i To Tabla2.Rows.Count - 1
                                '        If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                '            If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '                parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '                If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                '                    If Temp = 0 Then parcial = 0
                                '                    HuboCambio = True
                                '                End If
                                '                Temp += 1
                                '            Else
                                '                If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                '                    parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '                    If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                '                        If Temp = 0 Then parcial = 0
                                '                        HuboCambio = True
                                '                    End If
                                '                    Temp += 1
                                '                End If
                                '            End If


                                '            'Else
                                '            ' k = Tabla2.Rows.Count + 1
                                '        End If
                                '        'Next
                                '        cargo = 0
                                '        abono = 0
                                '    Else
                                '        If (i - 1) >= 0 Then

                                '            If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                '                '  For k As Integer = i To Tabla2.Rows.Count - 1
                                '                If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                '                    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '                        parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '                        If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                '                            If Temp = 0 Then parcial = 0
                                '                            HuboCambio = True
                                '                        End If
                                '                        Temp += 1
                                '                    Else
                                '                        If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                '                            parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '                            If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                '                                If Temp = 0 Then parcial = 0
                                '                                HuboCambio = True
                                '                            End If
                                '                            Temp += 1
                                '                        End If
                                '                    End If
                                '                    '   Else
                                '                    'k = Tabla2.Rows.Count + 1
                                '                End If
                                '                ' Next
                                '                cargo = 0
                                '                abono = 0
                                '            Else
                                '                parcial = 0
                                '                cargo = 0
                                '                abono = 0
                                '            End If

                                '        Else
                                '            parcial = 0
                                '            cargo = 0
                                '            abono = 0
                                '        End If
                                '    End If
                                'Else
                                '    If (i - 1) >= 0 Then

                                '        If Tabla2.Rows(i - 1)(1).ToString = n1 And HuboCambio = False Then
                                '            ' For k As Integer = i To Tabla2.Rows.Count - 1
                                '            If Tabla2.Rows(i)(1).ToString = n1 And HuboCambio = False Then
                                '                    If Tabla2.Rows(i)(7).ToString <> "" And Tabla2.Rows(i)(7).ToString <> "0" Then
                                '                        parcial = Double.Parse(Tabla2.Rows(i)(7).ToString)
                                '                        If Tabla2.Rows(i + 1)(8).ToString <> "" And Tabla2.Rows(i + 1)(8).ToString <> "0" Then
                                '                        If Temp = 0 Then parcial = 0
                                '                        HuboCambio = True
                                '                    End If
                                '                    Temp += 1
                                '                    Else
                                '                        If Tabla2.Rows(i)(8).ToString <> "" And Tabla2.Rows(i)(8).ToString <> "0" Then
                                '                            parcial = Double.Parse(Tabla2.Rows(i)(8).ToString)
                                '                        If Tabla2.Rows(i + 1)(7).ToString <> "" And Tabla2.Rows(i + 1)(7).ToString <> "0" Then
                                '                            If Temp = 0 Then parcial = 0
                                '                            HuboCambio = True
                                '                        End If
                                '                        Temp += 1
                                '                        End If
                                '                    End If
                                '                'Else
                                '                ' k = Tabla2.Rows.Count + 1
                                '            End If
                                '            '  Next
                                '            cargo = 0
                                '            abono = 0
                                '        Else
                                '            parcial = 0
                                '            cargo = 0
                                '            abono = 0
                                '        End If

                                '    Else
                                '        parcial = 0
                                '        cargo = 0
                                '        abono = 0
                                '    End If
                                'End If

                                'If Tabla2.Rows(i)(9) = 5 Then
                                '    descripcion = Tabla2.Rows(i)(6).ToString()
                                'Else
                                '    descripcion = ""
                                'End If
                                'llenarTabla("", "", "", "", "", n5.PadLeft(p.NNiv5, "0"), C.Descripcion, descripcion, parcial, cargo, abono)
                                ''End If

                            End If
                        End If
                    End If
                End If
            Next
        Next
    End Sub
    Private Sub Imprimir(Optional ByVal pSalir As Boolean = False)
        'Dim en As New Encriptador

        '  Tabla = p.llenaDatosPoliza(idPoliza)
        Tabla2 = p.llenaDatosPolizaImpresion(idPoliza)
        desglosar()
        dgvCuentas.DataSource = p.llenaDatosPoliza(idPoliza)
        cmbTipoPolizaX.SelectedIndex = p.tipo
        txtNumeroPoliza.Text = p.Numero
        dtpFecha.Value = p.fecha
        txtConcepto.Text = p.concepto
        txtBeneficiario.Text = p.beneficiario

        Dim RutaPDF As String

        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Date.Now.Year.ToString + "\") 'Modificado
        IO.Directory.CreateDirectory(RutaPDF + "\" + Date.Now.Year.ToString + "\" + Date.Now.Month.ToString("00")) 'Modificado
        RutaPDF = RutaPDF + "\" + Date.Now.Year.ToString + "-" + Date.Now.Month.ToString("00") 'Modificado

        PrintDocument1.DocumentName = "Contabilidad Poliza - " + cmbTipoPolizaX.Text.Chars(0) + txtNumeroPoliza.Text + " " + dtpFecha.Value.ToString("dd/MM/yyyy") '+ " " + txtConcepto.Text     'Modificado
        ' End If

        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.ContaPoliza) 'Modificado
        TipoImpresora = SA.TipoImpresora

        'obj.WriteSettings()
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
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
        If p.tipo <> 0 Then
            If TipoImpresora = 0 Then
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.ContaPoliza) 'Modificado
            Else
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.ContaPolizaTicket) 'Modificado
            End If
        Else
            If TipoImpresora = 0 Then
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.ContabilidadPolizaEgresos) 'Modificado
            Else
                LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.ContabilidadPolizaEgresosTicket) 'Modificado
            End If
        End If
        PrintDocument1.Print()
        If pSalir Then Me.Close()
    End Sub
    Private Sub LlenaNodosImpresion()
        ImpND.Clear()
        ImpNDD.Clear()

        ImpND.Add(New NodoImpresionN("", "tipoPoliza", cmbTipoPolizaX.Text.Chars(0) + txtNumeroPoliza.Text, 0), "tipoPoliza")
        ImpND.Add(New NodoImpresionN("", "fechaPoliza", dtpFecha.Value.ToString("dd/MM/yyy"), 0), "fechaPoliza")
        ImpND.Add(New NodoImpresionN("", "conceptoPoliza", txtConcepto.Text, 0), "conceptoPoliza")
        ImpND.Add(New NodoImpresionN("", "BeneficiarioPoliza", txtBeneficiario.Text, 0), "BeneficiarioPoliza")
        ImpND.Add(New NodoImpresionN("", "totalCargos", Format(CDbl(lblCargos.Text), "c2").PadLeft(13), 0), "totalCargos")
        ImpND.Add(New NodoImpresionN("", "totalAbonos", Format(CDbl(lblAbonos.Text), "c2").PadLeft(13), 0), "totalAbonos")

        Dim nFilas As Integer
        nFilas = tablaImpres.Rows.Count
        CuantosRenglones = 0
        For j As Integer = 0 To nfilas - 1
            ImpNDD.Add(New NodoImpresionN("", "N1", tablaImpres.Rows(j)(0).ToString, 0), "N1" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "N2", tablaImpres.Rows(j)(1).ToString, 0), "N2" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "N3", tablaImpres.Rows(j)(2).ToString, 0), "N3" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "N4", tablaImpres.Rows(j)(3).ToString, 0), "N4" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "N5", tablaImpres.Rows(j)(4).ToString, 0), "N5" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "desCuenta", tablaImpres.Rows(j)(5).ToString, 0), "desCuenta" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "DescripcionPoliza", tablaImpres.Rows(j)(6).ToString, 0), "DescripcionPoliza" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "parcial", tablaImpres.Rows(j)(7).ToString(), 0), "parcial" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cargoPolizaa", tablaImpres.Rows(j)(8).ToString(), 0), "cargoPoliza" + Format(j, "000"))
            ImpNDD.Add(New NodoImpresionN("", "abonoPoliza", tablaImpres.Rows(j)(9).ToString(), 0), "abonoPoliza" + Format(j, "000"))
            CuantosRenglones += 1
        Next

        Posicion = 0
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
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If

        e.HasMorePages = MasPaginas
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
    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.ContaPoliza, GlobalIdSucursalDefault)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.ContaPolizaTicket, GlobalIdSucursalDefault)
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
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.ContaPoliza, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.ContaPolizaTicket, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer


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

            '***********************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))

                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
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

                            'If n.ConEtiqueta = 1 Then
                            '    If n.TipoDato = 0 Then
                            '        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                            '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                            '        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                            '        If YExtra < YExtra2 Then YExtra = YExtra2
                            '    Else
                            '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                            '    End If
                            'Else
                            '    If n.TipoDato = 0 Then
                            '        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                            '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                            '        YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                            '        If YExtra < YExtra2 Then YExtra = YExtra2
                            '    Else
                            '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                            '    End If
                            'End If
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
            '***********************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n

                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)

                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 *, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                    'If n.TipoDato = 0 Then
                                    '    If n.ConEtiqueta = 1 Then
                                    '        Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                    '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    '        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    '    Else
                                    '        Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                    '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    '    End If
                                    'Else
                                    '    If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                    '    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    'End If

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

            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
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

            End If
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim YExtra2 As Integer = 0
        Dim YExtra As Integer = 0
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.ContaPoliza, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.ContaPolizaTicket, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    'If n.TipoDato = 0 Then
                                    '    If n.ConEtiqueta = 1 Then
                                    '        Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                    '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    '        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    '    Else
                                    '        Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                    '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    '    End If
                                    'Else
                                    '    If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                    '    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    'End If

                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 *, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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

            End If
            'If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
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
        'Dim YExtra As Integer = 0
        YExtra = 0
        YExtra2 = 0
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
                            'If n.ConEtiqueta = 1 Then
                            '    If n.TipoDato = 0 Then
                            '        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                            '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                            '        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                            '        If YExtra < YExtra2 Then YExtra = YExtra2
                            '    Else
                            '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                            '    End If
                            'Else
                            '    If n.TipoDato = 0 Then
                            '        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                            '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                            '        YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                            '        If YExtra < YExtra2 Then YExtra = YExtra2
                            '    Else
                            '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                            '    End If
                            'End If
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
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
            '*****************************************
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
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
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
                            'If n.ConEtiqueta = 1 Then
                            '    If n.TipoDato = 0 Then
                            '        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                            '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                            '        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                            '        If YExtra < YExtra2 Then YExtra = YExtra2
                            '    Else
                            '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                            '    End If
                            'Else
                            '    If n.TipoDato = 0 Then
                            '        Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                            '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                            '        YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                            '        If YExtra < YExtra2 Then YExtra = YExtra2
                            '    Else
                            '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                            '    End If
                            'End If
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
            '********************************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
        If MasPaginas = True And (pModo = 2 Or pModo = 3) Then
            NumeroPagina += 1
            Exit Sub
        End If
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
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
                                    'If n.TipoDato = 0 Then
                                    '    If n.ConEtiqueta = 1 Then
                                    '        Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    '        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    '        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    '    Else
                                    '        Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    '        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    '    End If
                                    'Else
                                    '    If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    '    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    'End If

                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 *, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
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

            End If

            'If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub chkMantenerFecha_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles chkMantenerFecha.KeyDown
        If e.KeyCode = Keys.Enter Then
            If chkMantenerFecha.Checked = True Then
                chkMantenerFecha.Checked = False
            Else
                chkMantenerFecha.Checked = True
            End If
        End If
    End Sub
    Private Sub chkMantenerFecha_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMantenerFecha.Enter
        chkMantenerFecha.BackColor = Color.SkyBlue
        If primera = False Then
            cmbTipoPolizaX.Focus()
            primera = True
            chkMantenerFecha.BackColor = Color.Transparent
        End If
    End Sub
    Private Sub chkMantenerFecha_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMantenerFecha.Leave
        chkMantenerFecha.BackColor = Color.Transparent
    End Sub

    Private Sub dtpFecha_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.Enter
        fechaAux = ""
        tipoFecha = 0
        bDay = False
        bMonth = False
        bYear = False
        ' SendKeys.Send("{RIGHT 0}")
        fechaNum = 0
    End Sub

    Private Sub dtpFecha_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpFecha.KeyUp

    End Sub

    Private Sub frmContabilidadPolizas_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Enter

    End Sub

    Private Sub pnlDatosIVA_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlDatosIVA.VisibleChanged
        If pnlDatosIVA.Visible = False Then
            dgvCuentas.Size = New Size(1016, 210)
            dgvCuentas.Location = New Point(14, 232)
        Else
            dgvCuentas.Size = New Size(1016, 175)
            dgvCuentas.Location = New Point(14, 270)
        End If
    End Sub

    'Private Sub txtProveedorClave_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProveedorClave.KeyDown
    '    If e.KeyCode = Keys.F1 Then
    '        Dim B As New frmBuscador(9, GlobalIdAlmacen)
    '        B.ShowDialog()
    '        If B.DialogResult = Windows.Forms.DialogResult.OK Then
    '            txtProveedorClave.Text = B.Proveedor.Clave
    '            nombreProv = B.Proveedor.Nombre
    '            txtIVA.Focus()
    '            idProveedor = B.ID
    '        End If
    '    End If
    '    If e.KeyCode = Keys.Enter Then
    '        'If txtProveedorNombre.Text = "" Then
    '        '    If MsgBox("El cliente solicitado no existe. ¿Desea guardalo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '        '        guardarProv()
    '        '    End If

    '        'Else
    '        txtIVA.Focus()
    '    End If

    '    'End If
    '    If e.KeyCode = Keys.N Then
    '        txtProveedorClave.Text = prov.DaMaximoCodigo()
    '    End If
    '    If e.KeyCode = Keys.Escape Then
    '        txtFolioFactura.Focus()
    '    End If
    'End Sub
    'Private Sub guardarProv()
    '    Dim B As New frmProveedores(2, 0, txtProveedorClave.Text)
    '    B.ShowDialog()
    '    If B.DialogResult = Windows.Forms.DialogResult.OK Then
    '        txtProveedorClave.Text = B.CodigoProveedor
    '        nombreProv = B.Nombre
    '        txtProveedorNombre.Text = nombreProv
    '        txtIVA.Focus()
    '    End If
    'End Sub


    'Private Sub txtProveedorClave_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProveedorClave.TextChanged
    '    prov.BuscaProveedorDIOT(txtProveedorClave.Text)
    '    nombreProv = prov.Nombre
    '    txtProveedorNombre.Text = prov.Nombre
    '    idProveedor = prov.ID
    'End Sub



    Private Sub txtIVA_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIVA.KeyPress
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or e.KeyChar = "E" Or e.KeyChar = "e") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtIVA_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVA.Leave
        If txtIVA.Text <> "" Then
            If txtIVA.Text <> "E" Then
                If txtIVA.Text <> "0" Then
                    If txtIVA.Text <> "16" Then
                        If txtIVA.Text <> "10" Then
                            If txtIVA.Text <> "11" Then
                                MsgBox("Solo se permiten los siguientes IVAs:" + vbCrLf + "0" + vbCrLf + "10" + vbCrLf + "11" + vbCrLf + "16" + vbCrLf + "E", MsgBoxStyle.Exclamation, "Pull System Soft")
                                txtIVA.Focus()
                                txtIVA.SelectAll()
                            End If

                        End If

                    End If
                End If

            End If
        End If
        If txtIVA.Text = "0" Or txtIVA.Text = "E" Then
            lblValorActos.Visible = True
            txtValorActos.Visible = True
            'txtValorActos.Focus()
        Else
            lblValorActos.Visible = False
            txtValorActos.Visible = False
        End If
    End Sub



    Private Sub txtFolioFactura_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolioFactura.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbProveedores.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            If txtN5.Enabled = True Then
                txtN5.Focus()
            Else
                If txtN4.Enabled = True Then
                    txtN4.Focus()
                Else
                    If txtN3.Enabled = True Then
                        txtN3.Focus()
                    Else
                        If txtN2.Enabled = True Then
                            txtN2.Focus()
                        Else
                            txtCuenta.Focus()
                        End If
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub txtIVA_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtIVA.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtIVA.Text = "0" Or txtIVA.Text = "E" Then
                txtValorActos.Visible = True
            Else
                txtValorActos.Visible = False
            End If
            '
            'txtValorActos.Focus()
            'Else
            'guardarDIOT()
            TextBox2.Focus()
            'End If

        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            cmbProveedores.Focus()
        End If
    End Sub

    Private Sub txtCocepFac_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCocepFac.KeyDown

    End Sub
    Private Sub guardarDIOT()
        Dim noErrores As Boolean = True
        Dim erroresText As String = ""

        txtFolioFactura.BackColor = Color.White
        cmbProveedores.BackColor = Color.White
        txtIVA.BackColor = Color.White
        'If txtFolioFactura.Text = "" Then
        '    noErrores = False
        '    erroresText += "Se requiere un folio de factura." + vbCrLf
        '    txtFolioFactura.BackColor = Color.Tomato
        'End If

        If cmbProveedores.SelectedIndex < 0 And cmbProveedores.Text <> "" Then
            noErrores = False
            erroresText += "El proveedor indicado no esta dado de alta." + vbCrLf
            cmbProveedores.BackColor = Color.Tomato
        End If
        If txtIVA.Text = "" Then
            noErrores = False
            erroresText += "Se requiere indicar el IVA." + vbCrLf
            txtIVA.BackColor = Color.Tomato
        End If

        'If cmbProveedores.SelectedIndex >= 0 And txtFolioFactura.Text <> "" Then
        '    If prov.validarDIOT(txtFolioFactura.Text, IdsProveedores.Valor(cmbProveedores.SelectedIndex), idRenglon) = False Then
        '        noErrores = False
        '        erroresText += "El folio de factura y proveedor, ya existen registrados en otra póliza." + vbCrLf
        '        txtFolioFactura.BackColor = Color.Tomato
        '        cmbProveedores.BackColor = Color.Tomato
        '    End If
        'End If


        If noErrores Then
            'If prov.BuscaProveedorDIOT(txtProveedorClave.Text) = False Then
            '    MsgBox("El cliente solicitado no existe. Favor de guardarlo o cambiarlo.", MsgBoxStyle.OkOnly, GlobalNombreApp)
            '    txtProveedorClave.BackColor = Color.Tomato
            '    txtProveedorClave.Focus()
            'Else

            'If txtCocepFac.Text = "" Then
            '    txtCocepFac.Text = nombreProv
            'End If

            txtDesc.Text = Trim(txtFolioFactura.Text + "  " + cmbProveedores.Text + "  " + txtIVA.Text)
            pnlDatosIVA.Visible = False

            If txtCargo.Text <> "" Then
                If txtIVA.Text = "0" And TextBox2.Text = "0" And TextBox3.Text = "0" Then
                    txtCargo.Text = "0"
                End If
                txtCargo.Focus()
            Else
                If txtAbono.Text <> "" Then
                    If txtIVA.Text = "0" And TextBox2.Text = "0" And TextBox3.Text = "0" Then
                        txtAbono.Text = "0"
                    End If
                    txtAbono.Focus()
                Else
                    If txtIVA.Text = "0" And TextBox2.Text = "0" And TextBox3.Text = "0" Then
                        txtCargo.Text = "0"
                    End If
                    txtCargo.Focus()
                End If
            End If
            'End If

        Else
            MsgBox(erroresText, MsgBoxStyle.OkOnly, GlobalNombreApp)

        End If

    End Sub

    'Private Sub txtProveedorNombre_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProveedorNombre.KeyDown
    'End Sub

    'Private Sub txtProveedorClave_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If txtProveedorClave.Text <> "" Then
    '        If txtProveedorNombre.Text = "" Then
    '            If MsgBox("El Proveedor solicitado no existe. ¿Desea guardalo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '                guardarProv()
    '            End If

    '        End If
    '    End If

    'End Sub


    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub btnNuevoDetalle_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnNuevoDetalle.KeyDown
        If e.KeyCode = Keys.Escape Then
            If btnEliminarDetalle.Enabled = True Then
                btnEliminarDetalle.Focus()
            Else
                btnAgregarDetalle.Focus()
            End If

        End If
    End Sub

    Private Sub btnEliminarDetalle_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnEliminarDetalle.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnAgregarDetalle.Focus()
        End If
    End Sub

    Private Sub btnAgregarDetalle_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnAgregarDetalle.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtAbono.Focus()
        End If
    End Sub

    Private Sub btnCerrar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnCerrar.KeyDown
        If e.KeyCode = Keys.Escape Then
            dgvCuentas.Focus()
        End If
    End Sub

    Private Sub btnImprimir_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnImprimir.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnNuevo.Focus()
        End If
    End Sub

    Private Sub btnNuevo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnNuevo.KeyDown
        If e.KeyCode = Keys.Escape Then
            If btnEliminar.Enabled = True Then
                btnEliminar.Focus()
            Else
                btnGuardarPoliza.Focus()
            End If

        End If
    End Sub

    Private Sub btnEliminar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnEliminar.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnGuardarPoliza.Focus()
        End If
    End Sub

    Private Sub btnGuardarPoliza_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnGuardarPoliza.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnImportar.Focus()
        End If
    End Sub

    Private Sub dgvCuentas_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvCuentas.KeyDown
        Dim selec As Integer
        If e.KeyCode = Keys.Escape Then
            btnNuevoDetalle.Focus()
        End If
        If dgvCuentas.RowCount > 0 Then
            selec = dgvCuentas.CurrentCell.RowIndex
            dgvCuentas.ClearSelection()
            If e.KeyCode = Keys.Up Then
                If selec <> 0 Then
                    dgvCuentas.Rows(selec - 1).Selected = True

                    llenaDatos(selec - 1)

                Else
                    dgvCuentas.Rows(selec).Selected = True
                End If
            End If
            If e.KeyCode = Keys.Down Then
                If selec <> dgvCuentas.RowCount - 1 Then
                    dgvCuentas.Rows(selec + 1).Selected = True
                    llenaDatos(selec + 1)
                Else
                    dgvCuentas.Rows(selec).Selected = True
                End If
            End If
        End If

    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            dtpHasta.Focus()
        End If
    End Sub

    Private Sub dtpHasta_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpHasta.KeyDown
        If e.KeyCode = Keys.Escape Then
            dtpDesde.Focus()
        End If
    End Sub

    Private Sub dtpDesde_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpDesde.KeyDown
        If e.KeyCode = Keys.Escape Then
            cmbTipoBuscar.Focus()
        End If
    End Sub

    Private Sub cmbTipoBuscar_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cmbTipoBuscar.KeyPress

    End Sub

    Private Sub cmbTipoBuscar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTipoBuscar.KeyDown
        If e.KeyCode = Keys.Escape Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub frmContabilidadPolizas_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If dgvCuentas.RowCount > 0 And btnGuardarPoliza.Text = "Guardar" Then
            If MsgBox("Esta póliza no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
        'My.Settings.ultimotipopoliza = cmbTipoPoliza.SelectedIndex
        'My.Settings.Save()
    End Sub

    Private Sub txtConcepto_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConcepto.Leave
        If dgvCuentas.RowCount <= 0 Then
            txtDesc.Text = txtConcepto.Text
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportar.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasAlta, PermisosN.Secciones.Contabilidad) = False Then
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Try
            Dim B As New frmInstruccionesPolizas()
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                OpenFileDialog1.FileName = "POLIZAS"
                OpenFileDialog1.Filter = "*.xls|*.xlsx"
                If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    Dim I As New Importador(OpenFileDialog1.FileName, MySqlcon)
                    ' I.ImportaArticulos(CheckBox1.Checked, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex))
                    I.ImportaPolizas()
                    I.CierraConexiones()
                    Consulta()
                    cmbTipoPolizaX.Focus()
                    If MsgBox("Importación exitosa", MsgBoxStyle.OkOnly, GlobalNombreApp) = MsgBoxResult.Ok Then
                        cmbTipoPolizaX.Focus()
                    End If




                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnImportar_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnImportar.KeyDown
        If e.KeyCode = Keys.Escape Then
            btnCerrar.Focus()
        End If
    End Sub

    Private Sub DataGridView1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DataGridView1.Scroll

    End Sub


    Private Sub dgvCuentas_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvCuentas.CellFormatting
        If e.ColumnIndex = 4 Or e.ColumnIndex = 5 Then
            If e.Value <> "" Then
                e.Value = Double.Parse(e.Value).ToString("###,##0.00")

            End If

        End If
    End Sub

    Private Sub dgvCuentas_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvCuentas.SizeChanged

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        txtCuenta.Focus()
    End Sub

    Private Sub txtN5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN5.Click
        If txtN5.Enabled = False Then
            If txtN4.Enabled = True Then
                txtN4.Focus()
            Else
                If txtN3.Enabled = True Then
                    txtN3.Focus()
                Else
                    If txtN2.Enabled = True Then
                        txtN2.Focus()
                    Else
                        txtCuenta.Focus()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtN4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN4.Click

        If txtN4.Enabled = True Then
            txtN4.Focus()
        Else
            If txtN3.Enabled = True Then
                txtN3.Focus()
            Else
                If txtN2.Enabled = True Then
                    txtN2.Focus()
                Else
                    txtCuenta.Focus()
                End If
            End If
        End If

    End Sub

    Private Sub txtN3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN3.Click

        If txtN3.Enabled = True Then
            txtN3.Focus()
        Else
            If txtN2.Enabled = True Then
                txtN2.Focus()
            Else
                txtCuenta.Focus()
            End If
        End If

    End Sub

    Private Sub txtN2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtN2.Click

        If txtN2.Enabled = True Then
            txtN2.Focus()
        Else
            txtCuenta.Focus()
        End If

    End Sub

    Private Sub dgvCuentas_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCuentas.CellContentClick

    End Sub

    Private Sub txtCargo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCargo.TextChanged
        If txtAbono.Text <> "" Then
            txtAbono.Text = ""
        End If
    End Sub

    Private Sub txtAbono_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAbono.TextChanged
        If txtCargo.Text <> "" Then
            txtCargo.Text = ""
        End If
    End Sub

    Private Sub btnUUID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComprobantes.Click
        agregarComprobantes()
    End Sub
    Private Sub agregarComprobantes()
        coincidenciasTabla()
        Dim ven As New frmContabilidadComprobantes(idPoliza, idRenglon, tblCheques, tblTrans, tblCompro, mayorCheque, mayorTrans, mayorCompro, mayorcomproN, mayorcomproE, mayorOtro, tablaComproNac2, tablaComproE, tablaOtros, txtBeneficiario.Text, dtpFecha.Value.ToString("yyyy/MM/dd"), cmbTipoPolizaX.Text)
        ven.ShowDialog()
        If ven.DialogResult = Windows.Forms.DialogResult.OK Then
            guardarCambiosTabla(ven.tablaCheques, ven.tablaTrans, ven.tablaCompro, ven.tablaComproNac2, ven.tablaComproE, ven.tablaOtros)
            'txtCuenta.Text = C.N1.PadLeft(p.NNiv1, "0")
        End If
    End Sub
    Private Sub coincidenciasTabla()
        tblCheques.Clear()
        mayorCheque = 0
        mayorCompro = 0
        mayorTrans = 0
        mayorcomproN = 0
        mayorcomproE = 0
        mayorOtro = 0

        If tablaChequesAUX.Rows.Count > 0 Then
            'llenar coincidencias.
            For i As Integer = 0 To tablaChequesAUX.Rows.Count - 1
                If tablaChequesAUX.Rows(i)(2).ToString = idRenglon.ToString Then
                    Dim dr As DataRow

                    dr = tblCheques.NewRow()
                    dr("id") = tablaChequesAUX.Rows(i)(0)
                    dr("idPoliza") = tablaChequesAUX.Rows(i)(1)
                    dr("idRenglon") = tablaChequesAUX.Rows(i)(2)
                    dr("Numero") = tablaChequesAUX.Rows(i)(3)
                    dr("BancoCod") = tablaChequesAUX.Rows(i)(4)
                    dr("Banco") = tablaChequesAUX.Rows(i)(5)
                    dr("Banco_extranjero") = tablaChequesAUX.Rows(i)(6)
                    dr("CuentaOri") = tablaChequesAUX.Rows(i)(7)
                    dr("Fecha") = tablaChequesAUX.Rows(i)(8)
                    dr("Beneficiario") = tablaChequesAUX.Rows(i)(9)
                    dr("RFC") = tablaChequesAUX.Rows(i)(10)
                    dr("Monto") = tablaChequesAUX.Rows(i)(11)
                    dr("idMoneda") = tablaChequesAUX.Rows(i)(12)
                    dr("Moneda") = tablaChequesAUX.Rows(i)(13)
                    dr("Tipo_cambio") = tablaChequesAUX.Rows(i)(14)
                    tblCheques.Rows.Add(dr)


                    If Integer.Parse(tablaChequesAUX.Rows(i)(0)) > mayorCheque Then
                        mayorCheque = Integer.Parse(tablaChequesAUX.Rows(i)(0))
                    End If

                End If
            Next

        End If

        tblCompro.Clear()
        If tablaComproAUX.Rows.Count > 0 Then
            'llenar coincidencias.
            For i As Integer = 0 To tablaComproAUX.Rows.Count - 1
                If tablaComproAUX.Rows(i)(2).ToString = idRenglon.ToString Then
                    Dim dr As DataRow

                    dr = tblCompro.NewRow()
                    dr("id") = tablaComproAUX.Rows(i)(0)
                    dr("idPoliza") = tablaComproAUX.Rows(i)(1)
                    dr("idRenglon") = tablaComproAUX.Rows(i)(2)
                    dr("UUID") = tablaComproAUX.Rows(i)(3)

                    dr("RFC") = tablaComproAUX.Rows(i)(4)
                    dr("idMoneda") = tablaComproAUX.Rows(i)(5)
                    dr("Moneda") = tablaComproAUX.Rows(i)(6)
                    dr("Tipo_Cambio") = tablaComproAUX.Rows(i)(7)
                    dr("Monto") = tablaComproAUX.Rows(i)(8)

                    tblCompro.Rows.Add(dr)
                    If Integer.Parse(tablaComproAUX.Rows(i)(0)) > mayorCheque Then
                        mayorCompro = Integer.Parse(tablaComproAUX.Rows(i)(0))
                    End If
                End If
            Next

        End If

        tblTrans.Clear()
        If tablaTransAUX.Rows.Count > 0 Then
            'llenar coincidencias.
            For i As Integer = 0 To tablaTransAUX.Rows.Count - 1
                If tablaTransAUX.Rows(i)(2).ToString = idRenglon.ToString Then
                    Dim dr As DataRow

                    dr = tblTrans.NewRow()
                    dr("id") = tablaTransAUX.Rows(i)(0)
                    dr("idPoliza") = tablaTransAUX.Rows(i)(1)
                    dr("idRenglon") = tablaTransAUX.Rows(i)(2)
                    dr("CuentaOri") = tablaTransAUX.Rows(i)(3)
                    dr("BancoCodO") = tablaTransAUX.Rows(i)(4)
                    dr("BancoOri") = tablaTransAUX.Rows(i)(5)
                    dr("Banco_extranjero_O") = tablaTransAUX.Rows(i)(6)
                    dr("CuentaDest") = tablaTransAUX.Rows(i)(7)
                    dr("BancoCodD") = tablaTransAUX.Rows(i)(8)
                    dr("BancoDest") = tablaTransAUX.Rows(i)(9)
                    dr("Banco_extranjero_D") = tablaTransAUX.Rows(i)(10)
                    dr("Fecha") = tablaTransAUX.Rows(i)(11)
                    dr("Beneficiario") = tablaTransAUX.Rows(i)(12)
                    dr("RFC") = tablaTransAUX.Rows(i)(13)
                    dr("Monto") = tablaTransAUX.Rows(i)(14)
                    dr("idMoneda") = tablaTransAUX.Rows(i)(15)
                    dr("Moneda") = tablaTransAUX.Rows(i)(16)
                    dr("tipo_Cambio") = tablaTransAUX.Rows(i)(17)

                    tblTrans.Rows.Add(dr)
                    If Integer.Parse(tablaTransAUX.Rows(i)(0)) > mayorCheque Then
                        mayorTrans = Integer.Parse(tablaTransAUX.Rows(i)(0))
                    End If
                End If
            Next

        End If

        'COMPRO NAC2
        tablaComproNac2.Clear()
        If tablaComproNac2Aux.Rows.Count > 0 Then
            'llenar coincidencias.
            For i As Integer = 0 To tablaComproNac2Aux.Rows.Count - 1
                If tablaComproNac2Aux.Rows(i)(2).ToString = idRenglon.ToString Then
                    Dim dr As DataRow

                    dr = tablaComproNac2.NewRow()
                    dr("id") = tablaComproNac2Aux.Rows(i)(0)
                    dr("idPoliza") = tablaComproNac2Aux.Rows(i)(1)
                    dr("idRenglon") = tablaComproNac2Aux.Rows(i)(2)
                    dr("Serie") = tablaComproNac2Aux.Rows(i)(3)
                    dr("Folio") = tablaComproNac2Aux.Rows(i)(4)
                    dr("RFC") = tablaComproNac2Aux.Rows(i)(5)
                    dr("Monto") = tablaComproNac2Aux.Rows(i)(6)
                    dr("idMoneda") = tablaComproNac2Aux.Rows(i)(7)
                    dr("Moneda") = tablaComproNac2Aux.Rows(i)(8)
                    dr("Tipo_Cambio") = tablaComproNac2Aux.Rows(i)(9)
                    tablaComproNac2.Rows.Add(dr)
                    If Integer.Parse(tablaComproNac2Aux.Rows(i)(0)) > mayorcomproN Then
                        mayorcomproN = Integer.Parse(tablaComproNac2Aux.Rows(i)(0))
                    End If
                End If
            Next

        End If

        'compro extra
        tablaComproE.Clear()
        If tablaComproEAux.Rows.Count > 0 Then
            'llenar coincidencias.
            For i As Integer = 0 To tablaComproEAux.Rows.Count - 1
                If tablaComproEAux.Rows(i)(2).ToString = idRenglon.ToString Then
                    Dim dr As DataRow

                    dr = tablaComproE.NewRow()
                    dr("id") = tablaComproEAux.Rows(i)(0)
                    dr("idPoliza") = tablaComproEAux.Rows(i)(1)
                    dr("idRenglon") = tablaComproEAux.Rows(i)(2)
                    dr("Num_Factura") = tablaComproEAux.Rows(i)(3)
                    dr("taxID") = tablaComproEAux.Rows(i)(4)
                    dr("Monto") = tablaComproEAux.Rows(i)(5)
                    dr("idMoneda") = tablaComproEAux.Rows(i)(6)
                    dr("Moneda") = tablaComproEAux.Rows(i)(7)
                    dr("Tipo_Cambio") = tablaComproEAux.Rows(i)(8)
                    tablaComproE.Rows.Add(dr)
                    If Integer.Parse(tablaComproEAux.Rows(i)(0)) > mayorcomproE Then
                        mayorcomproE = Integer.Parse(tablaComproEAux.Rows(i)(0))
                    End If
                End If
            Next

        End If
        'coprobantes otros
        tablaOtros.Clear()
        If tablaOtrosAux.Rows.Count > 0 Then
            'llenar coincidencias.
            For i As Integer = 0 To tablaOtrosAux.Rows.Count - 1
                If tablaOtrosAux.Rows(i)(2).ToString = idRenglon.ToString Then
                    Dim dr As DataRow
                    dr = tablaOtros.NewRow()
                    dr("id") = tablaOtrosAux.Rows(i)(0)
                    dr("idPoliza") = tablaOtrosAux.Rows(i)(1)
                    dr("idRenglon") = tablaOtrosAux.Rows(i)(2)
                    dr("idMetodoPago") = tablaOtrosAux.Rows(i)(3)
                    dr("Metodo_Pago") = tablaOtrosAux.Rows(i)(4)
                    dr("Fecha") = tablaOtrosAux.Rows(i)(5)
                    dr("Beneficiario") = tablaOtrosAux.Rows(i)(6)
                    dr("RFC") = tablaOtrosAux.Rows(i)(7)
                    dr("Monto") = tablaOtrosAux.Rows(i)(8)
                    dr("idMoneda") = tablaOtrosAux.Rows(i)(9)
                    dr("Moneda") = tablaOtrosAux.Rows(i)(10)
                    dr("Tipo_Cambio") = tablaOtrosAux.Rows(i)(11)

                    tablaOtros.Rows.Add(dr)
                    If Integer.Parse(tablaOtrosAux.Rows(i)(0)) > mayorOtro Then
                        mayorOtro = Integer.Parse(tablaOtrosAux.Rows(i)(0))
                    End If
                End If
            Next

        End If

    End Sub
    Private Sub guardarCambiosTabla(ByVal tC As DataTable, ByVal tt As DataTable, ByVal tcom As DataTable, ByVal tcN As DataTable, ByVal tcE As DataTable, ByVal tOtros As DataTable)
        'CHEQUES
        Dim tabAuxChe As DataTable = tablaChequesAUX.Copy
        tablaChequesAUX.Clear()

        For i As Integer = 0 To tabAuxChe.Rows.Count - 1
            If tabAuxChe.Rows(i)(2).ToString <> idRenglon.ToString Then
                Dim dr As DataRow

                dr = tablaChequesAUX.NewRow()
                dr("id") = tabAuxChe.Rows(i)(0)
                dr("idPoliza") = tabAuxChe.Rows(i)(1)
                dr("idRenglon") = tabAuxChe.Rows(i)(2)
                dr("Numero") = tabAuxChe.Rows(i)(3)
                dr("BancoCod") = tabAuxChe.Rows(i)(4)
                dr("Banco") = tabAuxChe.Rows(i)(5)
                dr("Banco_extranjero") = tabAuxChe.Rows(i)(6)
                dr("CuentaOri") = tabAuxChe.Rows(i)(7)
                dr("Fecha") = tabAuxChe.Rows(i)(8)
                dr("Beneficiario") = tabAuxChe.Rows(i)(9)
                dr("RFC") = tabAuxChe.Rows(i)(10)
                dr("Monto") = tabAuxChe.Rows(i)(11)
                dr("idMoneda") = tabAuxChe.Rows(i)(12)
                dr("Moneda") = tabAuxChe.Rows(i)(13)
                dr("Tipo_cambio") = tabAuxChe.Rows(i)(14)
                tablaChequesAUX.Rows.Add(dr)


            End If

        Next
        For i As Integer = 0 To tC.Rows.Count - 1
            Dim dr As DataRow

            dr = tablaChequesAUX.NewRow()
            dr("id") = tC.Rows(i)(0)
            dr("idPoliza") = tC.Rows(i)(1)
            dr("idRenglon") = tC.Rows(i)(2)
            dr("Numero") = tC.Rows(i)(3)
            dr("BancoCod") = tC.Rows(i)(4)
            dr("Banco") = tC.Rows(i)(5)
            dr("Banco_extranjero") = tC.Rows(i)(6)
            dr("CuentaOri") = tC.Rows(i)(7)
            dr("Fecha") = tC.Rows(i)(8)
            dr("Beneficiario") = tC.Rows(i)(9)
            dr("RFC") = tC.Rows(i)(10)
            dr("Monto") = tC.Rows(i)(11)
            dr("idMoneda") = tC.Rows(i)(12)
            dr("Moneda") = tC.Rows(i)(13)
            dr("Tipo_cambio") = tC.Rows(i)(14)
            tablaChequesAUX.Rows.Add(dr)
        Next
        'TRANSACCION
        '------------------------------------------------------------------------------------------------------------------------------------------------------
        Dim tabAuxTra As DataTable = tablaTransAUX.Copy
        tablaTransAUX.Clear()

        For i As Integer = 0 To tabAuxTra.Rows.Count - 1
            If tabAuxTra.Rows(i)(2).ToString <> idRenglon.ToString Then
                Dim dr As DataRow

                dr = tablaTransAUX.NewRow()
                dr("id") = tabAuxTra.Rows(i)(0)
                dr("idPoliza") = tabAuxTra.Rows(i)(1)
                dr("idRenglon") = tabAuxTra.Rows(i)(2)
                dr("CuentaOri") = tabAuxTra.Rows(i)(3)
                dr("BancoCodO") = tabAuxTra.Rows(i)(4)
                dr("BancoOri") = tabAuxTra.Rows(i)(5)
                dr("Banco_extranjero_O") = tabAuxTra.Rows(i)(6)
                dr("CuentaDest") = tabAuxTra.Rows(i)(7)
                dr("BancoCodD") = tabAuxTra.Rows(i)(8)
                dr("BancoDest") = tabAuxTra.Rows(i)(9)
                dr("Banco_extranjero_D") = tabAuxTra.Rows(i)(10)
                dr("Fecha") = tabAuxTra.Rows(i)(11)
                dr("Beneficiario") = tabAuxTra.Rows(i)(12)
                dr("RFC") = tabAuxTra.Rows(i)(13)
                dr("Monto") = tabAuxTra.Rows(i)(14)
                dr("idMoneda") = tabAuxTra.Rows(i)(15)
                dr("Moneda") = tabAuxTra.Rows(i)(16)
                dr("tipo_Cambio") = tabAuxTra.Rows(i)(17)
                tablaTransAUX.Rows.Add(dr)
            End If
        Next
        For i As Integer = 0 To tt.Rows.Count - 1
            Dim dr As DataRow

            dr = tablaTransAUX.NewRow()
            dr("id") = tt.Rows(i)(0)
            dr("idPoliza") = tt.Rows(i)(1)
            dr("idRenglon") = tt.Rows(i)(2)
            dr("CuentaOri") = tt.Rows(i)(3)
            dr("BancoCodO") = tt.Rows(i)(4)
            dr("BancoOri") = tt.Rows(i)(5)
            dr("Banco_extranjero_O") = tt.Rows(i)(6)
            dr("CuentaDest") = tt.Rows(i)(7)
            dr("BancoCodD") = tt.Rows(i)(8)
            dr("BancoDest") = tt.Rows(i)(9)
            dr("Banco_extranjero_D") = tt.Rows(i)(10)
            dr("Fecha") = tt.Rows(i)(11)
            dr("Beneficiario") = tt.Rows(i)(12)
            dr("RFC") = tt.Rows(i)(13)
            dr("Monto") = tt.Rows(i)(14)
            dr("idMoneda") = tt.Rows(i)(15)
            dr("Moneda") = tt.Rows(i)(16)
            dr("tipo_Cambio") = tt.Rows(i)(17)
            tablaTransAUX.Rows.Add(dr)
        Next

        'COMPROBANTe
        '--------------------------------------------------------------------------------------
        Dim tabAuxCo As DataTable = tablaComproAUX.Copy
        tablaComproAUX.Clear()

        For i As Integer = 0 To tabAuxCo.Rows.Count - 1
            If tabAuxCo.Rows(i)(2).ToString <> idRenglon.ToString Then
                Dim dr As DataRow

                dr = tablaComproAUX.NewRow()
                dr("id") = tabAuxCo.Rows(i)(0)
                dr("idPoliza") = tabAuxCo.Rows(i)(1)
                dr("idRenglon") = tabAuxCo.Rows(i)(2)
                dr("UUID") = tabAuxCo.Rows(i)(3)
                dr("RFC") = tabAuxCo.Rows(i)(4)
                dr("idMoneda") = tabAuxCo.Rows(i)(5)
                dr("Moneda") = tabAuxCo.Rows(i)(6)
                dr("Tipo_Cambio") = tabAuxCo.Rows(i)(7)
                dr("Monto") = tabAuxCo.Rows(i)(8)
                tablaComproAUX.Rows.Add(dr)
            End If

        Next
        For i As Integer = 0 To tcom.Rows.Count - 1
            Dim dr As DataRow
            dr = tablaComproAUX.NewRow()
            dr("id") = tcom.Rows(i)(0)
            dr("idPoliza") = tcom.Rows(i)(1)
            dr("idRenglon") = tcom.Rows(i)(2)
            dr("UUID") = tcom.Rows(i)(3)
            dr("RFC") = tcom.Rows(i)(4)
            dr("idMoneda") = tcom.Rows(i)(5)
            dr("Moneda") = tcom.Rows(i)(6)
            dr("Tipo_Cambio") = tcom.Rows(i)(7)
            dr("Monto") = tcom.Rows(i)(8)
            tablaComproAUX.Rows.Add(dr)
        Next

        'Componentes Nacionales 2
        Dim tabAuxCoN2 As DataTable = tablaComproNac2Aux.Copy
        tablaComproNac2Aux.Clear()

        For i As Integer = 0 To tabAuxCoN2.Rows.Count - 1
            If tabAuxCoN2.Rows(i)(2).ToString <> idRenglon.ToString Then
                Dim dr As DataRow

                dr = tablaComproNac2Aux.NewRow()
                dr("id") = tabAuxCoN2.Rows(i)(0)
                dr("idPoliza") = tabAuxCoN2.Rows(i)(1)
                dr("idRenglon") = tabAuxCoN2.Rows(i)(2)
                dr("Serie") = tabAuxCoN2.Rows(i)(3)
                dr("Folio") = tabAuxCoN2.Rows(i)(4)
                dr("RFC") = tabAuxCoN2.Rows(i)(5)
                dr("Monto") = tabAuxCoN2.Rows(i)(6)
                dr("idMoneda") = tabAuxCoN2.Rows(i)(7)
                dr("Moneda") = tabAuxCoN2.Rows(i)(8)
                dr("Tipo_Cambio") = tabAuxCoN2.Rows(i)(9)

                tablaComproNac2Aux.Rows.Add(dr)
            End If

        Next
        For i As Integer = 0 To tcN.Rows.Count - 1
            Dim dr As DataRow

            dr = tablaComproNac2Aux.NewRow()

            dr("id") = tcN.Rows(i)(0)
            dr("idPoliza") = tcN.Rows(i)(1)
            dr("idRenglon") = tcN.Rows(i)(2)
            dr("Serie") = tcN.Rows(i)(3)
            dr("Folio") = tcN.Rows(i)(4)
            dr("RFC") = tcN.Rows(i)(5)
            dr("Monto") = tcN.Rows(i)(6)
            dr("idMoneda") = tcN.Rows(i)(7)
            dr("Moneda") = tcN.Rows(i)(8)
            dr("Tipo_Cambio") = tcN.Rows(i)(9)

            tablaComproNac2Aux.Rows.Add(dr)
        Next

        'Comprobantes extranjeros
        Dim tabAuxCoE As DataTable = tablaComproEAux.Copy
        tablaComproEAux.Clear()

        For i As Integer = 0 To tablaComproEAux.Rows.Count - 1
            If tablaComproEAux.Rows(i)(2).ToString <> idRenglon.ToString Then
                Dim dr As DataRow

                dr = tablaComproEAux.NewRow()

                dr("id") = tabAuxCoE.Rows(i)(0)
                dr("idPoliza") = tabAuxCoE.Rows(i)(1)
                dr("idRenglon") = tabAuxCoE.Rows(i)(2)
                dr("Num_Factura") = tabAuxCoE.Rows(i)(3)
                dr("taxID") = tabAuxCoE.Rows(i)(4)
                dr("Monto") = tabAuxCoE.Rows(i)(5)
                dr("idMoneda") = tabAuxCoE.Rows(i)(6)
                dr("Moneda") = tabAuxCoE.Rows(i)(7)
                dr("Tipo_Cambio") = tabAuxCoE.Rows(i)(8)

                tablaComproEAux.Rows.Add(dr)
            End If

        Next
        For i As Integer = 0 To tcE.Rows.Count - 1
            Dim dr As DataRow

            dr = tablaComproEAux.NewRow()

            dr("id") = tcE.Rows(i)(0)
            dr("idPoliza") = tcE.Rows(i)(1)
            dr("idRenglon") = tcE.Rows(i)(2)
            dr("Num_Factura") = tcE.Rows(i)(3)
            dr("taxID") = tcE.Rows(i)(4)
            dr("Monto") = tcE.Rows(i)(5)
            dr("idMoneda") = tcE.Rows(i)(6)
            dr("Moneda") = tcE.Rows(i)(7)
            dr("Tipo_Cambio") = tcE.Rows(i)(8)
            tablaComproEAux.Rows.Add(dr)
        Next

        'Comprobante Otros
        Dim tabAuxOtros As DataTable = tablaOtrosAux.Copy
        tablaOtrosAux.Clear()

        For i As Integer = 0 To tablaOtrosAux.Rows.Count - 1
            If tablaOtrosAux.Rows(i)(2).ToString <> idRenglon.ToString Then
                Dim dr As DataRow

                dr = tablaOtrosAux.NewRow()

                dr("id") = tabAuxOtros.Rows(i)(0)
                dr("idPoliza") = tabAuxOtros.Rows(i)(1)
                dr("idRenglon") = tabAuxOtros.Rows(i)(2)
                dr("idMetodoPago") = tabAuxOtros.Rows(i)(3)
                dr("Metodo_Pago") = tabAuxOtros.Rows(i)(4)
                dr("Fecha") = tabAuxOtros.Rows(i)(5)
                dr("Beneficiario") = tabAuxOtros.Rows(i)(6)
                dr("RFC") = tabAuxOtros.Rows(i)(7)
                dr("Monto") = tabAuxOtros.Rows(i)(8)
                dr("idMoneda") = tabAuxOtros.Rows(i)(9)
                dr("Moneda") = tabAuxOtros.Rows(i)(10)
                dr("Tipo_Cambio") = tabAuxOtros.Rows(i)(11)

                tablaOtrosAux.Rows.Add(dr)
            End If

        Next
        For i As Integer = 0 To tOtros.Rows.Count - 1
            Dim dr As DataRow

            dr = tablaOtrosAux.NewRow()

            dr("id") = tOtros.Rows(i)(0)
            dr("idPoliza") = tOtros.Rows(i)(1)
            dr("idRenglon") = tOtros.Rows(i)(2)
            dr("idMetodoPago") = tOtros.Rows(i)(3)
            dr("Metodo_Pago") = tOtros.Rows(i)(4)
            dr("Fecha") = tOtros.Rows(i)(5)
            dr("Beneficiario") = tOtros.Rows(i)(6)
            dr("RFC") = tOtros.Rows(i)(7)
            dr("Monto") = tOtros.Rows(i)(8)
            dr("idMoneda") = tOtros.Rows(i)(9)
            dr("Moneda") = tOtros.Rows(i)(10)
            dr("Tipo_Cambio") = tOtros.Rows(i)(11)

            tablaOtrosAux.Rows.Add(dr)
        Next
    End Sub
    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    'Private Sub txtFecha1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFecha1.KeyPress
    '    'txtFecha1.Select(txtFecha1.SelectionStart, txtFecha1.SelectionStart)
    '    Dim textBox As TextBox = DirectCast(sender, TextBox)
    '    If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
    '        e.Handled = True
    '    Else
    '        If e.KeyChar = ControlChars.Back Then
    '            e.Handled = True
    '        End If
    '    End If
    '    If Char.IsNumber(e.KeyChar) Then
    '        'If txtFecha1.SelectionStart = 2 Or txtFecha1.SelectionStart = 5 Then
    '        '    txtFecha1.SelectionStart += 1
    '        'End If
    '        Dim selec As Integer = txtFecha1.SelectionStart
    '        Dim aux As String = ""
    '        If selec = 2 Or selec = 5 Then
    '            selec += 1
    '        End If
    '        For i As Integer = 0 To txtFecha1.Text.Length - 1
    '            If i = selec Then
    '                aux += e.KeyChar.ToString

    '            Else
    '                aux += txtFecha1.Text.Chars(i)
    '            End If
    '        Next
    '        txtFecha1.Text = aux

    '        'llenando = True
    '        txtFecha1.SelectionStart = selec + 1

    '        'If selec <> 1 And selec <> 4 Then
    '        '    txtFecha1.SelectionStart = selec + 1
    '        'Else

    '        '    txtFecha1.SelectionStart = selec + 2

    '        'End If
    '        'llenando = False

    '    End If
    'End Sub

    'Private Sub txtFecha1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFecha1.KeyDown
    '    If e.KeyCode = Keys.Enter Then
    '        txtConcepto.Focus()
    '    End If
    'End Sub

    'Private Sub txtFecha1_TextChanged(sender As Object, e As EventArgs) Handles txtFecha1.TextChanged
    '    If txtFecha1.Text.Length = 10 And llenando = False Then
    '        If IsDate(txtFecha1.Text) And llenando = False Then
    '            If dtpFecha.MinDate <= Date.Parse(txtFecha1.Text) And dtpFecha.MaxDate >= Date.Parse(txtFecha1.Text) Then
    '                dtpFecha.Value = txtFecha1.Text
    '            End If

    '        End If
    '    End If
    'End Sub

    'Private Sub txtFecha1_Leave(sender As Object, e As EventArgs) Handles txtFecha1.Leave
    '    If IsDate(txtFecha1.Text) = False Then
    '        txtFecha1.ForeColor = Color.Red
    '        txtFecha1.Focus()
    '        txtFecha1.Select(0, 0)
    '        txtFecha1.SelectionStart = 0
    '    Else
    '        If Not dtpFecha.MinDate <= Date.Parse(txtFecha1.Text) Then
    '            txtFecha1.ForeColor = Color.Red
    '            txtFecha1.Focus()
    '            txtFecha1.Select(0, 0)
    '            txtFecha1.SelectionStart = 0
    '        Else
    '            If Not dtpFecha.MaxDate >= Date.Parse(txtFecha1.Text) Then
    '                txtFecha1.ForeColor = Color.Red
    '                txtFecha1.Focus()
    '                txtFecha1.Select(0, 0)
    '                txtFecha1.SelectionStart = 0
    '            Else
    '                txtFecha1.ForeColor = Color.Black
    '            End If

    '        End If

    '    End If
    'End Sub

    Private Sub dtpFecha_CloseUp(sender As Object, e As EventArgs) Handles dtpFecha.CloseUp
        txtConcepto.Focus()
    End Sub


    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.RowCount > 0 Then
            If e.ColumnIndex = 4 Then
                If e.Value <> "" Then
                    e.Value = Format(CDbl(e.Value), "$###,##0.00")
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If

            End If
            If DataGridView1.Item(5, e.RowIndex).Value >= 0.01 Or DataGridView1.Item(5, e.RowIndex).Value <= -0.01 Then
                e.CellStyle.BackColor = Color.Yellow
            End If
        End If

    End Sub

    Private Sub btnXML_Click(sender As Object, e As EventArgs) Handles btnXML.Click
        Dim B As New frmContabilidadXML()
        B.ShowDialog()
    End Sub

    Private Sub txtValorActos_KeyDown(sender As Object, e As KeyEventArgs) Handles txtValorActos.KeyDown
        If e.KeyCode = Keys.Enter Then
            'guardarDIOT()
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub txtValorActos_Leave(sender As Object, e As EventArgs) Handles txtValorActos.Leave
        Dim x As Double
        If txtValorActos.Text = "." Then
            txtValorActos.Text = "0.00"
        End If
        If txtValorActos.Text = "" Then
            txtValorActos.Text = "0.00"
        Else
            If txtValorActos.Text = "-" Then
                txtValorActos.Text = "-0.00"
            Else
                If txtValorActos.Text = "-." Then
                    txtValorActos.Text = "-0.00"
                Else
                    x = Double.Parse(txtValorActos.Text)
                    txtValorActos.Text = Format(x, "0.00")
                End If
            End If

        End If
    End Sub

    Private Sub txtValorActos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValorActos.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then

            e.Handled = True
        End If
        If e.KeyChar = "-" Then
            If txtCargo.Text <> "" Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End If

    End Sub

    Private Sub cmbClasBusqueda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClasBusqueda.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub txtDesc_TextChanged(sender As Object, e As EventArgs) Handles txtDesc.TextChanged

    End Sub

    Private Sub txtProveedorNombre_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmbProveedores_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProveedores.KeyDown
        If e.KeyCode = Keys.Enter Then

            If cmbProveedores.SelectedIndex < 0 And cmbProveedores.Text <> "" Then
                'Dim FSino As New frmSioNo("El proveedor indicado no existe. ¿Desea guardarlo?", "¿Guardar proveedor?")
                'If FSino.ShowDialog = MsgBoxResult.Ok Then
                If MsgBox("El proveedor indicado no existe. ¿Desea Guardarlo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ProveedoresAlta, PermisosN.Secciones.Catalagos) = True Then
                        Dim B As New frmProveedores(2, 0, cmbProveedores.Text)
                        B.ShowDialog()
                        If B.DialogResult = Windows.Forms.DialogResult.OK Then
                            LlenaCombos("tblproveedores", cmbProveedores, "nombre", "nombrep", "idproveedor", IdsProveedores, "tipo=1")
                            cmbProveedores.SelectedIndex = IdsProveedores.Busca(B.IdProveedor)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If
                End If
                'FSino.Dispose()
            End If
            'If txtProveedorNombre.Text = "" Then
            '        '    If MsgBox("El cliente solicitado no existe. ¿Desea guardalo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            '        '        guardarProv()
            '        '    End If

            '        'Else
            If cmbProveedores.SelectedIndex >= 0 Then
                Dim Pro As New dbproveedores(IdsProveedores.Valor(cmbProveedores.SelectedIndex), MySqlcon)
                If Pro.IvaRet <> 0 Then TextBox2.Text = Pro.IvaRet.ToString
                If Pro.Ieps <> 0 Then TextBox3.Text = Pro.Ieps.ToString
            Else
                TextBox2.Text = "0"
                TextBox3.Text = "0"
            End If
            txtIVA.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            txtFolioFactura.Focus()
        End If

    End Sub
    Private Sub cmbClasificacionPoliza_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbClasificacionPoliza.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbTipoPolizaX.Focus()
        End If
    End Sub

    Private Sub cmbClasificacionPoliza_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClasificacionPoliza.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            If txtValorActos.Visible = True Then
                txtValorActos.Focus()
            Else
                TextBox3.Focus()
            End If

        End If
        If e.KeyCode = Keys.Enter Then
            guardarDIOT()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
            If TextBox2.Text <> "0" And TextBox2.Text <> "" Then
                txtIVA.Text = "0"
                TextBox3.Text = "0"
                txtValorActos.Visible = False
                lblValorActos.Visible = False
            End If
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            txtIVA.Focus()
        End If
    End Sub

    'Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
    '    txtNumeroPoliza.Text = p.bucarNumero(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbTipoPoliza.Text.Chars(0))
    'End Sub


    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text <> "0" And TextBox3.Text <> "" Then
                txtIVA.Text = "0"
                TextBox2.Text = "0"
                txtValorActos.Visible = False
                lblValorActos.Visible = False
            End If
            If txtValorActos.Visible Then
                txtValorActos.Focus()
            Else
                TextBox1.Focus()
            End If
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Consulta()
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Escape Then
            txtBuscarConecpto.Focus()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Consulta()
    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim PC As New frmContabilidadPolizasConsulta()
        If MsgBox("¿Guardar esta póliza como una nueva?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            txtNumeroPoliza.Text = p.bucarNumero(dtpFecha.Value.Month.ToString("00"), dtpFecha.Value.Year.ToString, cmbTipoPolizaX.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
            btnGuardarPoliza.Text = "Guardar (F10)"
            guardar()
        End If
    End Sub

    Private Sub txtNumeroPoliza_TextChanged(sender As Object, e As EventArgs) Handles txtNumeroPoliza.TextChanged

    End Sub

    Private Sub txtIVA_TextChanged(sender As Object, e As EventArgs) Handles txtIVA.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub txtValorActos_TextChanged(sender As Object, e As EventArgs) Handles txtValorActos.TextChanged

    End Sub

    Private Sub cmbProveedores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProveedores.SelectedIndexChanged

    End Sub
End Class