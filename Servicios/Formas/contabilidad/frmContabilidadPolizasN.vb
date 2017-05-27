Public Class frmContabilidadPolizasN
    Dim Poliza As dbContabilidadPolizas
    Dim IdsProveedores As New elemento
    Dim IdsClasificacionPoliza As New elemento
    Dim MensajeError As String
    Dim ConsultaOn As Boolean
    Dim IdPolizaG As Integer
    Dim ImpDoc As ImprimirDocumento
    Dim Tabla2 As DataTable
    Dim tablaImpres As New DataTable
    Dim totalCargo As Double
    Dim totalAbono As Double
    Dim n1 As String
    Dim n2 As String
    Dim n3 As String
    Dim n4 As String
    Dim n5 As String
    Dim parcial As Double
    Dim cargo As Double
    Dim abono As Double
    Dim descripcion As String
    Dim C As dbContabilidadClasificacion
    Public Sub New(pIdPoliza As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        IdPolizaG = pIdPoliza
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmContabilidadPolizasN_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Poliza.Estado > 0 And Poliza.Estado <> 3 Then
            If MsgBox("Esta póliza no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Poliza.eliminarPoliza(Poliza.IDPoliza)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
        My.Settings.ultimotipopoliza = cmbTipoPolizaN.SelectedIndex
        My.Settings.Save()
    End Sub
    Private Sub frmContabilidadPolizasN_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If btnGuardarPoliza.Enabled = True Then
                Modificar()
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            Botonnuevo()
        End If
    End Sub
    Private Sub Botonnuevo()
        If Poliza.Estado > 0 And Poliza.Estado <> 3 Then
            If MsgBox("Esta póliza no ha sido guardada. ¿Empezar una nueva de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Poliza.eliminarPoliza(Poliza.IDPoliza)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub
    Private Sub frmContabilidadPolizasN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            ImpDoc = New ImprimirDocumento()
            C = New dbContabilidadClasificacion(MySqlcon)
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
            lblDescripcion.Text = ""
            lblN2.Text = ""
            lblN3.Text = ""
            lblN4.Text = ""
            lblN5.Text = ""
            ConsultaOn = False
            Poliza = New dbContabilidadPolizas(MySqlcon)
            sc.C = New dbContabilidadClasificacion(MySqlcon)
            sc.P = New dbContabilidadPolizas(MySqlcon)
            sc.Inicializar()
            bcc.P = New dbContabilidadClasificacion(MySqlcon)
            LlenaCombos("tblproveedores", cmbProveedores, "nombre", "nombrep", "idproveedor", IdsProveedores, "tipo=1")
            LlenaCombos("tblcontabilidadclas", cmbClasificacionPoliza, "nombre", "nombret", "id", IdsClasificacionPoliza, , , "nombre")
            If My.Settings.ultimotipopoliza < 0 Then
                cmbTipoPolizaN.SelectedIndex = 0
            Else
                cmbTipoPolizaN.SelectedIndex = My.Settings.ultimotipopoliza
            End If

            If cmbClasificacionPoliza.Items.Count = 0 Then
                MsgBox("Es necesario dar de alta al menos una clasificación de pólizas.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                btnGuardarPoliza.Enabled = False
            End If
            ConsultaOn = True
            Nuevo()
            If IdPolizaG <> 0 Then
                Poliza.IDPoliza = IdPolizaG
                LlenaDatosPoliza()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Nuevo()
        sc.Vacia()
        txtConcepto.Text = ""
        txtDesc.Text = ""
        txtAbono.Text = ""
        txtCargo.Text = ""
        txtBeneficiario.Text = ""
        txtCantCheque.Text = "0"
        CheckBox1.Checked = False
        Poliza.Estado = 0
        Poliza.IDPoliza = 0
        btnEliminar.Enabled = False
        btnGuardarPoliza.Enabled = False
        Button3.Enabled = False
        btnImprimir.Enabled = False
        ConsultaOn = False
        If chkMantenerFecha.Checked = False Then
            If Poliza.ActivarFechaTrabajo = 1 Then
                dtpFechaN.Value = CDate(Poliza.FechaTRabajo)
            Else
                dtpFechaN.Value = Date.Now.ToString("dd/MM/" + Poliza.anio)
            End If
        End If
        ConsultaOn = True
        txtNumeroPoliza.Text = Poliza.bucarNumero(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
        txtDesc.Text = ""
        txtAbono.Text = ""
        txtCargo.Text = ""
        NuevaDetalle()
    End Sub

    Private Sub NuevaDetalle()
        txtFolioFactura.Text = ""
        cmbProveedores.Text = ""
        txtIVA.Text = "16"
        TextBox2.Text = "0"
        TextBox3.Text = "0"
        txtValorActos.Text = "0"
        TextBox1.Text = ""
        Poliza.DetalleId = 0
        cmbProveedores.SelectedIndex = -1
        txtDesc.Text = txtConcepto.Text
        btnAgregarDetalle.Text = "Agregar"
        btnEliminarDetalle.Enabled = False
        pnlDatosIVA.Visible = False
        btnComprobantes.Enabled = False
        ConsultaDetalles()
        sc.txtCuenta.Focus()
        'Select Case sc.Nivel
        '    Case 0
        '        sc.txtCuenta.Focus()
        '    Case 1
        '        sc.txtCuenta.Focus()
        '    Case 2
        '        sc.txtN2.Focus()
        '    Case 3
        '        sc.txtN3.Focus()
        '    Case 4
        '        sc.txtN4.Focus()
        '    Case 5
        '        sc.txtN5.Focus()
        'End Select
    End Sub

    Private Sub cmbTipoPoliza_Enter(sender As Object, e As EventArgs) Handles cmbTipoPolizaN.Enter, txtNumeroPoliza.Enter, dtpFechaN.Enter, chkMantenerFecha.Enter, cmbClasificacionPoliza.Enter, txtConcepto.Enter, txtBeneficiario.Enter, txtDesc.Enter, txtCargo.Enter, txtAbono.Enter, Button1.Enter, Button2.Enter, btnAgregarDetalle.Enter, btnEliminarDetalle.Enter, btnNuevoDetalle.Enter, btnGuardarPoliza.Enter, btnImportar.Enter, btnXML.Enter, btnComprobantes.Enter, Button3.Enter, btnEliminar.Enter, btnNuevo.Enter, btnImprimir.Enter
        If sc.ActivaBuscador And bcc.Visible = True Then
            bcc.Visible = False
            sc.ActivaBuscador = False
        End If
    End Sub

    Private Sub cmbTipoPoliza_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmbTipoPolizaN.KeyDown
        If e.KeyCode = Keys.Enter Then txtNumeroPoliza.Focus()
        If e.KeyCode = Keys.Escape Then cmbClasificacionPoliza.Focus()
    End Sub
    Private Sub cmbTipoPoliza_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoPolizaN.SelectedIndexChanged
        If cmbTipoPolizaN.SelectedIndex = 0 Then
            lblBeneficiario.Visible = True
            txtBeneficiario.Visible = True
            txtCantCheque.Visible = True
            CheckBox1.Visible = True
            Label6.Visible = True
        Else
            lblBeneficiario.Visible = False
            txtBeneficiario.Visible = False
            txtCantCheque.Visible = False
            CheckBox1.Visible = False
            Label6.Visible = False
        End If
        If ConsultaOn Then txtNumeroPoliza.Text = Poliza.bucarNumero(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
    End Sub

    Private Sub dtpFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFechaN.KeyDown
        If e.KeyCode = Keys.Enter Then cmbClasificacionPoliza.Focus()
    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaN.ValueChanged
        If ConsultaOn And Poliza.Estado <> 3 Then txtNumeroPoliza.Text = Poliza.bucarNumero(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
    End Sub

    Private Sub btnAgregarDetalle_Click(sender As Object, e As EventArgs) Handles btnAgregarDetalle.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        If Poliza.Estado = 0 Then Guardar()
        If Poliza.Estado <> 0 Then Agregar()
    End Sub
    Private Sub Agregar()
        Try
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim IdProveedor As Integer
            txtDesc.BackColor = Color.White
            txtCargo.BackColor = Color.White
            txtAbono.BackColor = Color.White
            txtFolioFactura.BackColor = Color.White
            cmbProveedores.BackColor = Color.White
            Dim EsDiot As Integer = 0

            If sc.IdCuenta = 0 Then
                NoErrores = False
                MensajeError += "Se requiere un número de cuenta." + vbCrLf
            Else
                Dim Cc As New dbCContables(MySqlcon)
                If Cc.UtlimoNivel(sc.IdCuenta, sc.Nivel) <> 0 Then
                    NoErrores = False
                    MensajeError += "Debe indicar una cuenta de último nivel." + vbCrLf
                End If
            End If
            If txtIVA.Text = "" And (TextBox2.Text = "" Or TextBox2.Text = "0") And (TextBox3.Text = "" Or TextBox3.Text = "0") Then
                txtIVA.Text = "0"
            End If
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
            If IdProveedor > 0 Then EsDiot = 1
            If sc.EsDiot = 0 Then
                IdProveedor = 0
                EsDiot = 0
            End If

            If pnlDatosIVA.Visible = True And IdProveedor < 0 Then
                NoErrores = False
                MensajeError += "Se requiere proporcionar los datos DIOT." + vbCrLf
                cmbProveedores.BackColor = Color.Tomato
            End If

            If NoErrores = True Then
                If btnAgregarDetalle.Text = "Agregar" Then
                    If chkAgregarComprobante.Checked = True Then
                        If MsgBox("¿Desea agregar comprobantes?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                        End If
                    End If
                    If Poliza.DetallesOrden < Poliza.UltimoOrden Then
                        Poliza.CambiaOrden(Poliza.IDPoliza, Poliza.DetallesOrden)
                    End If
                    sc.C.LlenaIdsExtra(sc.IdCuenta)
                    Poliza.guardarDetalles(sc.DaCuentatxt, txtDesc.Text, txtCargo.Text, txtAbono.Text, sc.IdCuenta, txtFolioFactura.Text, IdProveedor, txtIVA.Text, cmbProveedores.Text, EsDiot, txtValorActos.Text, Poliza.fecha, TextBox1.Text, CDbl(TextBox2.Text), CDbl(TextBox3.Text), Poliza.DetallesOrden, sc.C.IdCuentaMayor, sc.C.idNivel2, sc.C.idNivel3, sc.C.idNivel4, sc.C.idNivel5)
                    NuevaDetalle()
                Else
                    'Modificar
                    sc.C.LlenaIdsExtra(sc.IdCuenta)
                    Poliza.ModificaDetalle(Poliza.DetalleId, sc.DaCuentatxt, txtDesc.Text, txtCargo.Text, txtAbono.Text, sc.IdCuenta, txtFolioFactura.Text, IdProveedor, txtIVA.Text, cmbProveedores.Text, EsDiot, txtValorActos.Text, Poliza.fecha, TextBox1.Text, CDbl(TextBox2.Text), CDbl(TextBox3.Text), sc.C.IdCuentaMayor, sc.C.idNivel2, sc.C.idNivel3, sc.C.idNivel4, sc.C.idNivel5)
                    NuevaDetalle()
                End If
                If chkAgregarComprobante.Checked = True Then
                    If MsgBox("¿Desea agregar comprobantes?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim ven As New frmContabilidadComprobantesN(Poliza.IDPoliza, Poliza.IDRenglon, txtBeneficiario.Text, dtpFechaN.Value.ToString("yyyy/MM/dd"), cmbTipoPolizaN.Text)
                        ven.ShowDialog()
                        ven.Dispose()
                    End If
                End If
                If Poliza.PreguntarCuadrar = 1 And lblDiferencia.Text = "$0.00" Then
                    Dim Fp As New frmSioNo("¿Guardar póliza?", "¿Guardar póliza?")
                    If Fp.ShowDialog = MsgBoxResult.Ok Then
                        Modificar()
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
    Private Sub ConsultaDetalles()
        dgvCuentas.DataSource = Poliza.ConsultaDetalles(Poliza.IDPoliza)
        dgvCuentas.Columns(0).Visible = False
        dgvCuentas.Columns(1).Visible = False
        dgvCuentas.Columns(2).HeaderText = "Cuenta"
        dgvCuentas.Columns(3).HeaderText = "Descripción"
        dgvCuentas.Columns(4).HeaderText = "Cargo"
        dgvCuentas.Columns(5).HeaderText = "Abono"
        dgvCuentas.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvCuentas.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
        dgvCuentas.Columns(2).Width = 190
        dgvCuentas.Columns(4).Width = 130
        dgvCuentas.Columns(5).Width = 130
        dgvCuentas.ClearSelection()
        Dim Cargo As Double = Poliza.DaTotalAbono(Poliza.IDPoliza)
        Dim Abono As Double = Poliza.DaTotalCargo(Poliza.IDPoliza)
        Dim Dif As Double = Cargo - Abono
        If Dif < 0 Then Dif = Dif * -1
        lblAbonos.Text = Cargo.ToString("c2")
        lblCargos.Text = Abono.ToString("c2")
        lblDiferencia.Text = Dif.ToString("c2")
        If Dif > 0.0001 Then
            lblDiferencia.ForeColor = Color.Red
        Else
            lblDiferencia.ForeColor = Color.Black
        End If
    End Sub
    Private Sub LlenaDatosDetalles()
        Poliza.DetalleId = dgvCuentas.Item(0, dgvCuentas.CurrentCell.RowIndex).Value
        Poliza.LlenaDatosDetalle(Poliza.DetalleId)
        sc.LlenaCuenta(Poliza.DetalleidCuenta)
        txtDesc.Text = Poliza.Detalledescripcion
        If Poliza.DetalleAbono = "-999999999" Then
            txtAbono.Text = ""
        Else
            txtAbono.Text = Poliza.DetalleAbono.ToString()
        End If
        If Poliza.Detallecargo = "-999999999" Then
            txtCargo.Text = ""
        Else
            txtCargo.Text = Poliza.Detallecargo.ToString()
        End If
        txtFolioFactura.Text = Poliza.Detallefactura
        If Poliza.Detalleidproveedor > 0 And cmbProveedores.Items.Count > 0 Then
            cmbProveedores.SelectedIndex = IdsProveedores.Busca(Poliza.Detalleidproveedor)
        Else
            cmbProveedores.SelectedIndex = -1
            cmbProveedores.Text = ""
        End If
        txtIVA.Text = Poliza.Detalleiva
        txtValorActos.Text = Poliza.DetallevalorActos
        TextBox1.Text = Poliza.Detallereferencia
        TextBox2.Text = Poliza.Detalleivaret.ToString
        TextBox3.Text = Poliza.Detalleieps.ToString
        btnAgregarDetalle.Text = "Modif."
        btnEliminarDetalle.Enabled = True
        btnComprobantes.Enabled = True
        'Select Case sc.Nivel
        '    Case 0
        '        sc.txtCuenta.Focus()
        '    Case 1
        '        sc.txtCuenta.Focus()
        '    Case 2
        '        sc.txtN2.Focus()
        '    Case 3
        '        sc.txtN3.Focus()
        '    Case 4
        '        sc.txtN4.Focus()
        '    Case 5
        '        sc.txtN5.Focus()
        'End Select
    End Sub
    Private Sub Guardar()

        If ChecaErrores(False) = True Then
            Dim L As Byte = 0
            If CheckBox1.Checked Then L = 1
            Poliza.guardarPoliza(cmbTipoPolizaN.Text.Chars(0), CInt(txtNumeroPoliza.Text), dtpFechaN.Value.ToString("yyyy/MM/dd"), txtConcepto.Text, txtBeneficiario.Text, lblCargos.Text, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex), 1, CDbl(txtCantCheque.Text), L)
            Poliza.fecha = dtpFechaN.Value.ToString("yyyy/MM/dd")
            Poliza.Numero = CInt(txtNumeroPoliza.Text)
            Poliza.Estado = 1
            Poliza.DetallesOrden = 1
            btnGuardarPoliza.Enabled = True
            btnImprimir.Enabled = True
        Else
            MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Modificar()
        If lblDiferencia.Text <> "$0.00" Then
            If MsgBox("La póliza no está balanceada. ¿Desea guardarla de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If
        If ChecaErrores(True) Then
            Poliza.Estado = 3
            Dim L As Byte = 0
            If CheckBox1.Checked Then L = 1
            Poliza.modificarPoliza(Poliza.IDPoliza, cmbTipoPolizaN.Text.Chars(0), CInt(txtNumeroPoliza.Text), dtpFechaN.Value.Year.ToString + "/" + dtpFechaN.Value.Month.ToString("00") + "/" + dtpFechaN.Value.Day.ToString("00"), txtConcepto.Text, txtBeneficiario.Text, lblCargos.Text, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex), CDbl(txtCantCheque.Text), L)
            Nuevo()
        Else
            MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Function ChecaErrores(pModificar As Boolean) As Boolean
        Dim NoErrores As Boolean = True
        txtNumeroPoliza.BackColor = Color.White
        txtConcepto.BackColor = Color.White
        txtBeneficiario.BackColor = Color.White

        If txtNumeroPoliza.Text = "" Then
            NoErrores = False
            MensajeError += "Se requiere un número de póliza." + vbCrLf
            txtNumeroPoliza.BackColor = Color.Tomato
        Else
            If pModificar Then
                If Poliza.Numero <> CInt(txtNumeroPoliza.Text) Then
                    If Poliza.folioRepetido(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), CInt(txtNumeroPoliza.Text), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)) <> 0 Then
                        NoErrores = False
                        MensajeError += " Número de póliza repetido."
                    End If
                End If
            Else
                If Poliza.folioRepetido(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), CInt(txtNumeroPoliza.Text), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)) <> 0 Then
                    NoErrores = False
                    MensajeError += " Número de póliza repetido."
                End If
            End If

        End If
        If dtpFechaN.Value.Year.ToString < Poliza.anio Or dtpFechaN.Value.Year.ToString > Poliza.anio Then
            NoErrores = False
            MensajeError += " No puede guardar pólizas que no esten en el periodo de trabajo."
        End If
        If txtConcepto.Text = "" Then
            NoErrores = False
            MensajeError += "Se requiere indicar el concepto." + vbCrLf
            txtConcepto.BackColor = Color.Tomato
        End If
        If cmbTipoPolizaN.SelectedIndex = 0 And txtBeneficiario.Text = "" Then
            NoErrores = False
            MensajeError += "Se requiere indicar el nombre del beneficiario." + vbCrLf
            txtBeneficiario.BackColor = Color.Tomato
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.PolizasAlta, PermisosN.Secciones.Contabilidad) = False Then
            MensajeError += "No tiene permiso para realizar esta operación"
            NoErrores = False
        End If
        If txtCantCheque.Text = "" Then
            txtCantCheque.Text = "0"
        Else
            If IsNumeric(txtCantCheque.Text) = False Then
                MensajeError += "Cantidad del cheque debe ser un valor numérico."
                NoErrores = False
            End If
        End If

        Return NoErrores
    End Function
    Private Sub LlenaDatosPoliza()
        Poliza.llenaDatosPolizaN(Poliza.IDPoliza)
        Poliza.Estado = 3
        ConsultaOn = False
        dtpFechaN.Value = Poliza.fecha
        cmbClasificacionPoliza.SelectedIndex = IdsClasificacionPoliza.Busca(Poliza.IdClasificacion)
        cmbTipoPolizaN.SelectedIndex = Poliza.tipo
        txtNumeroPoliza.Text = Poliza.Numero
        txtConcepto.Text = Poliza.concepto
        txtBeneficiario.Text = Poliza.beneficiario
        txtCantCheque.Text = Poliza.CantidadCheque
        If Poliza.Leyenda = 0 Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
        ConsultaOn = True
        btnEliminar.Enabled = True
        Button3.Enabled = True
        btnImprimir.Enabled = True
        btnGuardarPoliza.Enabled = True
        ConsultaDetalles()
        sc.txtCuenta.Focus()
    End Sub

    Private Sub sc_CambiaID() Handles sc.CambiaID
        If sc.IdCuenta <> 0 Then
            lblDescripcion.Text = sc.P.DNiv1
            lblN2.Text = sc.P.DNiv2
            lblN3.Text = sc.P.DNiv3
            lblN4.Text = sc.P.DNiv4
            lblN5.Text = sc.P.DNiv5
        Else
            lblDescripcion.Text = ""
            lblN2.Text = ""
            lblN3.Text = ""
            lblN4.Text = ""
            lblN3.Text = ""
        End If
    End Sub

    Private Sub sc_KeyDown(sender As Object, e As KeyEventArgs) Handles sc.KeyDown
        If (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down) And sc.ActivaBuscador = False Then
            If sc.IdCuenta <> 0 Then
                Dim Cc As New dbCContables(MySqlcon)
                If Cc.UtlimoNivel(sc.IdCuenta, sc.Nivel) = 0 Then
                    If sc.EsDiot = 0 Then
                        Select Case sc.Nivel
                            Case 1
                                txtDesc.Focus()
                            Case 2
                                If sc.txtN3.Focused Then txtDesc.Focus()
                            Case 3
                                If sc.txtN4.Focused Then txtDesc.Focus()
                            Case 4
                                If sc.txtN5.Focused Then txtDesc.Focus()
                            Case 5
                                txtDesc.Focus()
                        End Select

                    Else
                        Select Case sc.Nivel
                            Case 1
                                pnlDatosIVA.Visible = True
                                txtFolioFactura.Focus()
                            Case 2
                                If sc.txtN3.Focused Then
                                    pnlDatosIVA.Visible = True
                                    txtFolioFactura.Focus()
                                End If
                            Case 3
                                If sc.txtN4.Focused Then
                                    pnlDatosIVA.Visible = True
                                    txtFolioFactura.Focus()
                                End If
                            Case 4
                                If sc.txtN5.Focused Then
                                    pnlDatosIVA.Visible = True
                                    txtFolioFactura.Focus()
                                End If
                            Case 5
                                pnlDatosIVA.Visible = True
                                txtFolioFactura.Focus()
                        End Select

                    End If
                End If
            Else
                MsgBox("No existe esa cuenta", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        End If

        If e.KeyCode = Keys.Enter And sc.ActivaBuscador Then
            sc.IdCuenta = bcc.DaId
            sc.LlenaCuenta(sc.IdCuenta)
            If sc.IdCuenta <> 0 Then
                Dim Cc As New dbCContables(MySqlcon)
                If Cc.UtlimoNivel(sc.IdCuenta, sc.Nivel) = 0 Then
                    If sc.EsDiot = 0 Then
                        txtDesc.Focus()
                    Else
                        pnlDatosIVA.Visible = True
                        txtFolioFactura.Focus()
                    End If
                End If
            End If
        End If
        If (e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up) And sc.ActivaBuscador = False Then
            If sc.txtCuenta.Focused Then
                If txtCantCheque.Visible Then
                    txtCantCheque.Focus()
                Else
                    txtConcepto.Focus()
                End If
            End If
        End If
        If sc.ActivaBuscador Then
            Select Case e.KeyCode
                Case Keys.Up
                    bcc.PresionaArriba()
                Case Keys.Down
                    bcc.PresionaAbajo()
                Case Keys.Left
                Case Keys.Right
                Case Else
                    bcc.Visible = True
                    bcc.filtroCuenta = Trim(sc.txtCuenta.Text.Trim + " " + sc.txtN2.Text.Trim + " " + sc.txtN3.Text.Trim + " " + sc.txtN4.Text.Trim + " " + sc.txtN5.Text.Trim)
                    bcc.Consulta()
            End Select
        Else
            bcc.Visible = False
        End If
        'PresionaArribaAbajo(e.KeyCode)
    End Sub

    Private Sub txtDesc_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDesc.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then
            If txtAbono.Text <> "" Then
                txtAbono.Focus()
            Else
                txtCargo.Focus()
            End If
        End If
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then
            Select Case sc.Nivel
                Case 1
                    sc.txtCuenta.Focus()
                Case 2
                    sc.txtN2.Focus()
                Case 3
                    sc.txtN3.Focus()
                Case 4
                    sc.txtN4.Focus()
                Case 5
                    sc.txtN5.Focus()
            End Select
        End If
        'PresionaArribaAbajo(e.KeyCode)
    End Sub

    Private Sub txtDesc_TextChanged(sender As Object, e As EventArgs) Handles txtDesc.TextChanged

    End Sub

    Private Sub txtCargo_GotFocus(sender As Object, e As EventArgs) Handles txtCargo.GotFocus
        txtCargo.SelectAll()
    End Sub

    Private Sub txtCargo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCargo.KeyDown
        If e.KeyCode = Keys.Space Then
            txtAbono.Text = txtCargo.Text
            txtCargo.Text = ""
            txtAbono.Focus()
        End If
        If e.KeyCode = Keys.Enter Then BotonAgregar()
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then txtDesc.Focus()
        'PresionaArribaAbajo(e.KeyCode)
    End Sub

    Private Sub txtCargo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCargo.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And txtCargo.Text.IndexOf(".") < 0) Or (e.KeyChar = "-" And txtCargo.Text.IndexOf("-") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCargo_TextChanged(sender As Object, e As EventArgs) Handles txtCargo.TextChanged

    End Sub

    Private Sub txtAbono_GotFocus(sender As Object, e As EventArgs) Handles txtAbono.GotFocus
        txtAbono.SelectAll()
    End Sub

    Private Sub txtAbono_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAbono.KeyDown
        If e.KeyCode = Keys.Space Then
            txtCargo.Text = txtAbono.Text
            txtAbono.Text = ""
            txtCargo.Focus()
        End If
        If e.KeyCode = Keys.Enter Then BotonAgregar()
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then txtDesc.Focus()
        'PresionaArribaAbajo(e.KeyCode)
    End Sub

    Private Sub txtAbono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAbono.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And txtAbono.Text.IndexOf(".") < 0) Or (e.KeyChar = "-" And txtAbono.Text.IndexOf("-") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAbono_TextChanged(sender As Object, e As EventArgs) Handles txtAbono.TextChanged

    End Sub

    Private Sub dgvCuentas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCuentas.CellClick
        LlenaDatosDetalles()
    End Sub

    Private Sub dgvCuentas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCuentas.CellContentClick

    End Sub

    Private Sub txtNumeroPoliza_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNumeroPoliza.KeyDown
        If e.KeyCode = Keys.Oemplus Then
            txtNumeroPoliza.Text = ""
            txtNumeroPoliza.Text = Poliza.bucarNumero(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
            e.Handled = True
            dtpFechaN.Focus()
        End If
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then txtConcepto.Focus()
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then cmbTipoPolizaN.Focus()
    End Sub

    Private Sub txtNumeroPoliza_TextChanged(sender As Object, e As EventArgs) Handles txtNumeroPoliza.TextChanged

    End Sub

    Private Sub btnGuardarPoliza_Click(sender As Object, e As EventArgs) Handles btnGuardarPoliza.Click
        Modificar()
    End Sub

    Private Sub txtConcepto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtConcepto.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then
            If txtBeneficiario.Visible Then
                txtBeneficiario.Focus()
            Else
                sc.txtCuenta.Focus()
            End If
        End If

        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then txtNumeroPoliza.Focus()
    End Sub

    Private Sub txtConcepto_TextChanged(sender As Object, e As EventArgs) Handles txtConcepto.TextChanged
        txtDesc.Text = txtConcepto.Text
    End Sub

    Private Sub btnNuevoDetalle_Click(sender As Object, e As EventArgs) Handles btnNuevoDetalle.Click
        NuevaDetalle()
    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
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
                    If MsgBox("Importación exitosa", MsgBoxStyle.OkOnly, GlobalNombreApp) = MsgBoxResult.Ok Then
                        cmbTipoPolizaN.Focus()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnXML_Click(sender As Object, e As EventArgs) Handles btnXML.Click
        Dim B As New frmContabilidadXML()
        B.ShowDialog()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Botonnuevo()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If MsgBox("¿Eliminar esta póliza?.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Poliza.eliminarPoliza(Poliza.IDPoliza)
            Nuevo()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim PC As New frmContabilidadPolizasConsulta()
        If MsgBox("¿Guardar esta póliza como una nueva?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim IdAnt As Integer
            IdAnt = Poliza.IDPoliza
            txtNumeroPoliza.Text = Poliza.bucarNumero(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
            Dim L As Byte = 0
            If CheckBox1.Checked Then L = 1
            Poliza.guardarPoliza(cmbTipoPolizaN.Text.Chars(0), CInt(txtNumeroPoliza.Text), dtpFechaN.Value.ToString("yyyy/MM/dd"), txtConcepto.Text, txtBeneficiario.Text, lblCargos.Text, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex), 3, CDbl(txtCantCheque.Text), L)
            Poliza.fecha = dtpFechaN.Value.ToString("yyyy/MM/dd")
            Poliza.Numero = CInt(txtNumeroPoliza.Text)
            Poliza.Estado = 3
            Poliza.CrearDesde(IdAnt, Poliza.IDPoliza)
            LlenaDatosPoliza()
        End If
    End Sub

    Private Sub btnComprobantes_Click(sender As Object, e As EventArgs) Handles btnComprobantes.Click
        Dim ven As New frmContabilidadComprobantesN(Poliza.IDPoliza, Poliza.DetalleId, txtBeneficiario.Text, dtpFechaN.Value.ToString("yyyy/MM/dd"), cmbTipoPolizaN.Text)
        ven.ShowDialog()
        ven.Dispose()
    End Sub
    Private Sub cmbClasificacionPoliza_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles cmbClasificacionPoliza.KeyDown
        If e.KeyCode = Keys.Enter Then cmbTipoPolizaN.Focus()
        If e.KeyCode = Keys.Escape Then dtpFechaN.Focus()
    End Sub

    Private Sub cmbClasificacionPoliza_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClasificacionPoliza.SelectedIndexChanged
        If ConsultaOn Then txtNumeroPoliza.Text = Poliza.bucarNumero(dtpFechaN.Value.Month.ToString("00"), dtpFechaN.Value.Year.ToString, cmbTipoPolizaN.Text.Chars(0), IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex))
    End Sub

    Private Sub txtBeneficiario_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBeneficiario.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then txtCantCheque.Focus()
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then txtConcepto.Focus()
    End Sub

    Private Sub txtBeneficiario_TextChanged(sender As Object, e As EventArgs) Handles txtBeneficiario.TextChanged

    End Sub

    Private Sub txtFolioFactura_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFolioFactura.KeyDown
        If e.KeyCode = Keys.Enter Then cmbProveedores.Focus()
        If e.KeyCode = Keys.Escape Then txtDesc.Focus()
    End Sub

    Private Sub txtFolioFactura_TextChanged(sender As Object, e As EventArgs) Handles txtFolioFactura.TextChanged

    End Sub

    Private Sub cmbProveedores_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbProveedores.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbProveedores.SelectedIndex < 0 And cmbProveedores.Text <> "" Then
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
            End If
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

        If e.KeyCode = Keys.Escape Then txtFolioFactura.Focus()
    End Sub

    Private Function VerificaDatosDiot() As Boolean
        cmbProveedores.BackColor = Color.White
        If cmbProveedores.SelectedIndex < 0 And cmbProveedores.Text <> "" Then
            MsgBox("El proveedor indicado no esta dado de alta.", MsgBoxStyle.Information, GlobalNombreApp)
            cmbProveedores.BackColor = Color.Tomato
            Return False
        End If
        If txtIVA.Text = "" And (TextBox2.Text = "" Or TextBox2.Text = "0") And (TextBox3.Text = "" Or TextBox3.Text = "0") Then
            txtIVA.Text = "0"
        End If
        txtDesc.Text = Trim(txtFolioFactura.Text + "  " + cmbProveedores.Text + "  " + txtIVA.Text)
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
        Return True
    End Function

    Private Sub txtIVA_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIVA.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Poliza.SoloIvaEnDiot = 1 Then
                If txtIVA.Text = "0" Or txtIVA.Text = "E" Then
                    txtValorActos.Visible = True
                    lblValorActos.Visible = True
                    txtValorActos.Focus()
                Else
                    txtValorActos.Visible = False
                    lblValorActos.Visible = False
                    TextBox1.Focus()
                End If
            Else
                TextBox2.Focus()
            End If
        End If
        If e.KeyCode = Keys.Escape Then cmbProveedores.Focus()
    End Sub

    Private Sub txtIVA_TextChanged(sender As Object, e As EventArgs) Handles txtIVA.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text <> "0" And TextBox2.Text <> "" Then
                txtIVA.Text = "0"
                TextBox3.Text = "0"
                txtValorActos.Visible = False
                lblValorActos.Visible = False
            End If
            TextBox3.Focus()
        End If
        If e.KeyCode = Keys.Escape Then txtIVA.Focus()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text <> "0" And TextBox3.Text <> "" Then
                txtIVA.Text = "0"
                TextBox2.Text = "0"
                txtValorActos.Visible = False
                lblValorActos.Visible = False
                TextBox1.Focus()
            Else
                If txtIVA.Text = "0" Or txtIVA.Text = "E" Then
                    txtValorActos.Visible = True
                    lblValorActos.Visible = True
                    txtValorActos.Focus()
                Else
                    txtValorActos.Visible = False
                    lblValorActos.Visible = False
                    TextBox1.Focus()
                End If
            End If
        End If
        If e.KeyCode = Keys.Escape Then TextBox2.Focus()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub txtValorActos_KeyDown(sender As Object, e As KeyEventArgs) Handles txtValorActos.KeyDown
        If e.KeyCode = Keys.Enter Then TextBox1.Focus()
        If e.KeyCode = Keys.Escape Then
            If Poliza.SoloIvaEnDiot = 0 Then
                TextBox3.Focus()
            Else
                txtIVA.Focus()
            End If
        End If
    End Sub

    Private Sub txtValorActos_TextChanged(sender As Object, e As EventArgs) Handles txtValorActos.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then VerificaDatosDiot()
        If e.KeyCode = Keys.Escape Then
            If Poliza.SoloIvaEnDiot = 0 Then
                If txtValorActos.Visible = True Then
                    txtValorActos.Focus()
                Else
                    TextBox3.Focus()
                End If
            Else
                If txtValorActos.Visible = True Then
                    txtValorActos.Focus()
                Else
                    txtIVA.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Poliza.Estado > 0 And Poliza.Estado <> 3 Then
            If MsgBox("Esta póliza no ha sido guardada. Si continua los datos se perderán. ¿Desea continuar?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Poliza.eliminarPoliza(Poliza.IDPoliza)
            Else
                Exit Sub
            End If
        End If
        Dim F As New frmContabilidadPolizasConsulta()
        If F.ShowDialog = Windows.Forms.DialogResult.OK Then
            Poliza.IDPoliza = F.IdPoliza
            LlenaDatosPoliza()
        End If
        F.Dispose()
    End Sub

    Private Sub dgvCuentas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvCuentas.CellFormatting
        If e.ColumnIndex = 4 Or e.ColumnIndex = 5 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            If e.Value <> "" Then
                e.Value = Format(CDbl(e.Value), "#,###,###,##0.00")
            End If

        End If
    End Sub
    'Private Sub PresionaArribaAbajo(Key As Keys)
    '    If Key = Keys.Down Then
    '        If dgvCuentas.RowCount > 1 Then
    '            If dgvCuentas.CurrentRow.Index < dgvCuentas.RowCount - 1 Then dgvCuentas.CurrentCell = dgvCuentas.Item(2, dgvCuentas.CurrentCell.RowIndex + 1)
    '        End If
    '    End If
    '    If Key = Keys.Up Then
    '        If dgvCuentas.RowCount > 1 Then
    '            If dgvCuentas.CurrentRow.Index > 0 Then dgvCuentas.CurrentCell = dgvCuentas.Item(2, dgvCuentas.CurrentCell.RowIndex - 1)
    '        End If
    '    End If
    'End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub dgvCuentas_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvCuentas.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            sc.txtCuenta.Focus()
            'Select Case sc.Nivel
            '    Case 1
            '        sc.txtCuenta.Focus()
            '    Case 2
            '        sc.txtN2.Focus()
            '    Case 3
            '        sc.txtN3.Focus()
            '    Case 4
            '        sc.txtN4.Focus()
            '    Case 5
            '        sc.txtN5.Focus()
            'End Select
        End If
    End Sub

    Private Sub bcc_Click(sender As Object, e As EventArgs) Handles bcc.Click
        If bcc.ID <> 0 Then
            sc.ActivaBuscador = False
            sc.LlenaCuenta(bcc.ID)
            bcc.Visible = False
            Select Case sc.Nivel
                Case 1
                    sc.txtCuenta.Focus()
                Case 2
                    sc.txtN2.Focus()
                Case 3
                    sc.txtN3.Focus()
                Case 4
                    sc.txtN4.Focus()
                Case 5
                    sc.txtN5.Focus()
            End Select
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If Poliza.IDPoliza <> 0 Then
            Imprimir()
        End If
    End Sub
    Private Sub Imprimir()
        Try
            'Dim Compra As New dbCompras(pIdCompra, MySqlcon)
            Tabla2 = Poliza.llenaDatosPolizaImpresion(Poliza.IDPoliza)
            ImpDoc.IdSucursal = GlobalIdSucursalDefault
            If cmbTipoPolizaN.SelectedIndex <> 0 Then
                ImpDoc.TipoDocumento = TiposDocumentos.ContaPoliza
                ImpDoc.TipoDocumentoT = TiposDocumentos.ContaPoliza + 1000
            Else
                ImpDoc.TipoDocumento = TiposDocumentos.ContabilidadPolizaEgresos
                ImpDoc.TipoDocumentoT = TiposDocumentos.ContabilidadPolizaEgresos + 1000
            End If
            ImpDoc.TipoDocumentoImp = TiposDocumentos.ContaPoliza
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(dtpFechaN.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(dtpFechaN.Value, "yyyy") + "\" + Format(dtpFechaN.Value, "MM") + "\")
            Dim Op As New dbOpciones(MySqlcon)
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(dtpFechaN.Value, "yyyy") + "\" + Format(dtpFechaN.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "POLIZA-" + cmbTipoPolizaN.Text.Chars(0) + " " + txtNumeroPoliza.Text + " " + dtpFechaN.Value.ToString("dd/MM/yyyy")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaNodosImpresion()
        ImpDoc.ImpND.Clear()
        ImpDoc.ImpNDD.Clear()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "tipoPoliza", cmbTipoPolizaN.Text.Chars(0) + txtNumeroPoliza.Text, 0), "tipoPoliza")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "fechaPoliza", dtpFechaN.Value.ToString("dd/MM/yyy"), 0), "fechaPoliza")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "conceptoPoliza", txtConcepto.Text, 0), "conceptoPoliza")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "BeneficiarioPoliza", txtBeneficiario.Text, 0), "BeneficiarioPoliza")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalCargos", Format(CDbl(lblCargos.Text), "c2").PadLeft(13), 0), "totalCargos")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalAbonos", Format(CDbl(lblAbonos.Text), "c2").PadLeft(13), 0), "totalAbonos")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cantcheque", Format(CDbl(txtCantCheque.Text), "c2").PadLeft(13), 0), "cantcheque")
        Dim CL As New CLetras
        If CDbl(txtCantCheque.Text) >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletrac", CL.LetrasM(CDbl(txtCantCheque.Text), 2, GlobalIdiomaLetras), 0), "totalletrac")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletrac", "MENOS " + CL.LetrasM(CDbl(txtCantCheque.Text) * -1, 2, GlobalIdiomaLetras), 0), "totalletrac")
        End If
        If CheckBox1.Checked Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "leyenda", "PARA ABONO EN CUENTA DE BENEFICIARIO", 0), "leyenda")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "leyenda", "", 0), "leyenda")
        End If

        desglosar()
        Dim nFilas As Integer
        nFilas = tablaImpres.Rows.Count
        ImpDoc.CuantosRenglones = 0
        For j As Integer = 0 To nfilas - 1
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "N1", tablaImpres.Rows(j)(0).ToString, 0), "N1" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "N2", tablaImpres.Rows(j)(1).ToString, 0), "N2" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "N3", tablaImpres.Rows(j)(2).ToString, 0), "N3" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "N4", tablaImpres.Rows(j)(3).ToString, 0), "N4" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "N5", tablaImpres.Rows(j)(4).ToString, 0), "N5" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "desCuenta", tablaImpres.Rows(j)(5).ToString, 0), "desCuenta" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "DescripcionPoliza", tablaImpres.Rows(j)(6).ToString, 0), "DescripcionPoliza" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "parcial", tablaImpres.Rows(j)(7).ToString(), 0), "parcial" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cargoPolizaa", tablaImpres.Rows(j)(8).ToString(), 0), "cargoPoliza" + Format(j, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "abonoPoliza", tablaImpres.Rows(j)(9).ToString(), 0), "abonoPoliza" + Format(j, "000"))
            ImpDoc.CuantosRenglones += 1
        Next
        ImpDoc.Posicion = 0
        ImpDoc.NumeroPagina = 1
    End Sub
    Private Sub dgvCuentas_KeyUp(sender As Object, e As KeyEventArgs) Handles dgvCuentas.KeyUp
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatosDetalles()
        End If
    End Sub

    Private Sub btnEliminarDetalle_Click(sender As Object, e As EventArgs) Handles btnEliminarDetalle.Click
        If MsgBox("¿Eliminar este renglon?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Poliza.EliminarDetalle(Poliza.DetalleId)
            NuevaDetalle()
        End If
    End Sub

    Private Sub sc_Load(sender As Object, e As EventArgs) Handles sc.Load

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        'If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
        ' e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        'End If
        e.HasMorePages = ImpDoc.MasPaginas
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
                    ' n1 = Tabla2.Rows(i)(1).ToString.PadLeft(poliza.NNiv1, "0")
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
                                    llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(Poliza.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, cargo, abono)
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
                                If ConParcial Then llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(Poliza.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, 0, 0)
                            Else
                                If Tabla2.Rows(i)(9) = 1 Then
                                    descripcion = Tabla2.Rows(i)(6).ToString()
                                Else
                                    descripcion = ""
                                End If
                                llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(Poliza.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, cargo, abono)
                            End If

                        Else
                            'Es el último renglon
                            If Tabla2.Rows(i)(9) = 1 Then
                                descripcion = Tabla2.Rows(i)(6).ToString()
                            Else
                                descripcion = ""
                            End If
                            llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(Poliza.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, cargo, abono)
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
                            llenarTabla(Tabla2.Rows(i)(0).ToString(), n1.PadLeft(Poliza.NNiv1, "0"), "", "", "", "", C.Descripcion, descripcion, parcial, 0, 0)
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
                            llenarTabla("", "", n2.PadLeft(Poliza.NNiv2, "0"), "", "", "", C.Descripcion2, descripcion, parcial, cargo, abono)
                        End If

                    Else
                        If j = 3 Then
                            'NIVEL 3
                            If n3 <> Tabla2.Rows(i)(3).ToString Or Tabla2.Rows(i)(4).ToString = "" Then
                                n3 = Tabla2.Rows(i)(3).ToString
                                n4 = ""
                                n5 = ""
                                C.buscarID(n1, n2, n3, "", "", 3)
                                'If poliza.contadorCuentas(n1, idPoliza) > 1 Then
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
                                llenarTabla("", "", "", n3.PadLeft(Poliza.NNiv3, "0"), "", "", C.Descripcion3, descripcion, parcial, cargo, abono)
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
                                    llenarTabla("", "", "", "", n4.PadLeft(Poliza.NNiv4, "0"), "", C.Descripcion4, descripcion, parcial, cargo, abono)
                                End If
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
                                llenarTabla("", "", "", "", "", n5.PadLeft(Poliza.NNiv5, "0"), C.Descripcion5, descripcion, parcial, cargo, abono)
                                'End If
                            End If
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantCheque.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Down Then sc.txtCuenta.Focus()
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Up Then txtBeneficiario.Focus()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles txtCantCheque.TextChanged

    End Sub

    Private Sub bcc_Load(sender As Object, e As EventArgs) Handles bcc.Load

    End Sub
End Class