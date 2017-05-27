Public Class frmProveedores
    Public IdProveedor As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Public CodigoProveedor As String
    Public Nombre As String
    Public RFC As String
    Public Calle As String
    Public Ciudad As String
    Public Municipio As String
    Public NoInterior As String
    Public NoExterior As String
    Public Colonia As String
    Public Estado As String
    Public CP As String
    Public Pais As String
    Dim TipoAlta As Byte
    Dim IdCuenta As Integer
    Dim IdCuenta2 As Integer
    Dim IdCuenta3 As Integer
    Dim IdCuenta4 As Integer
    Dim IdsTipos As New elemento
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Public Sub New(ByVal pTipoalta As Byte, ByVal pIdProveedor As Integer, ByVal pCodigoProv As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TipoAlta = pTipoalta
        IdProveedor = pIdProveedor
        CodigoProveedor = pCodigoProv
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub frmProveedores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        txtTipoConsulta.SelectedIndex = 0
        LlenaCombos("tblproveedorestipos", cmbTipo, "nombre", "nombret", "idtipo", IdsTipos)
        If IdProveedor = 0 Then
            Nuevo()
            If TipoAlta = 2 Then
                txtNombre.Text = Nombre
                txtPais.Text = Pais
                txtCiudad.Text = Ciudad
                txtCalle.Text = Calle
                txtNoInterior.Text = NoInterior
                txtNoExterior.Text = NoExterior
                txtRfc.Text = RFC
                txtMunicipio.Text = Municipio
                txtColonia.Text = Colonia
                txtRfc.Text = RFC
                txtEstado.Text = Estado
                txtCp.Text = CP

                cmbParaDiot.SelectedIndex = 1
            End If
        Else
            LlenaDatos()
        End If
    End Sub


    Private Sub Nuevo()
        Try

            ConsultaOn = False
            IdCuenta = 0
            IdCuenta2 = 0
            IdCuenta3 = 0
            IdCuenta4 = 0
            btnCuentas1.BackColor = ColorRojo
            btnCuentas2.BackColor = ColorRojo
            btnCuentas3.BackColor = ColorRojo
            btnCuentas4.BackColor = ColorRojo
            txtCodigo.Text = ""
            txtNombre.Text = ""
            txtContacto.Text = ""
            txtTelefono.Text = ""
            txtEmail.Text = ""
            txtRfc.Text = ""
            txtGiro.Text = ""
            txtCalle.Text = ""
            txtNoExterior.Text = ""
            txtNoInterior.Text = ""
            txtColonia.Text = ""
            txtMunicipio.Text = ""
            txtCiudad.Text = ""
            txtReferencia.Text = ""
            txtEstado.Text = ""
            txtCp.Text = ""
            txtPais.Text = "MÉXICO"
            btnManejarCuentas.Visible = False
            txtDiasCredito.Text = "0"
            txtLimiteCredito.Text = "0"
            txtRepresentante.Text = ""
            txtIeps.Text = "0"
            txtIvaRetenido.Text = "0"
            txtIsr.Text = "0"
            txtNombre.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtCodigo.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtRfc.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtDiasCredito.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtLimiteCredito.BackColor = Color.FromKnownColor(KnownColor.Window)

            cmbParaDiot.SelectedIndex = 0
            cmbTipo.SelectedIndex = 0
            btnGuardar.Text = "Guardar"
            btnEliminar.Enabled = False
            ConsultaOn = True
            Consulta()
            Dim C As New dbproveedores(MySqlcon)
            ConsultaOn = False
            txtCodigo.Text = Format(C.DaMaximo, "0000")
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbproveedores(MySqlcon)
                DataGridView1.DataSource = P.Consulta(txtConsulta.Text, txtTipoConsulta.SelectedIndex)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Código"
                DataGridView1.Columns(2).HeaderText = "Nombre"
                DataGridView1.Columns(3).HeaderText = "RFC"
                DataGridView1.Columns(2).Width = 200
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Nuevo()
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged
        'Consulta()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged
        'Consulta()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        guardar()
    End Sub
    Private Sub guardar()
        Try
            txtNombre.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtCodigo.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtRfc.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtDiasCredito.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtLimiteCredito.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtRepresentante.BackColor = Color.FromKnownColor(KnownColor.Window)
            Dim P As New dbproveedores(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If P.BuscaProveedorDIOT(txtCodigo.Text) And btnGuardar.Text = "Guardar" Then
                NoErrores = False
                MensajeError += vbCrLf + "El código proporcionado ya existe, elija otro."
                txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If P.BuscaProveedorDIOTModif(txtCodigo.Text, IdProveedor) And btnGuardar.Text <> "Guardar" Then
                NoErrores = False
                MensajeError += vbCrLf + "El código proporcionado ya existe, elija otro."
                txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtIeps.Text = "" Then txtIeps.Text = "0"
            If txtIvaRetenido.Text = "" Then txtIvaRetenido.Text = "0"
            If txtIsr.Text = "" Then txtIsr.Text = "0"
            If txtNombre.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al proveedor."
                txtNombre.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtCodigo.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un código al proveedor."
                txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtRfc.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un RFC al proveedor."
                txtRfc.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(txtDiasCredito.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " Los días de crédito deben ser un valor numérico."
                txtDiasCredito.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(txtLimiteCredito.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El límite de crédito debe ser un valor numérico."
                txtLimiteCredito.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(txtIvaRetenido.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " IVA Retenido debe ser un valor numérico."
                txtDiasCredito.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(txtIsr.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " ISR debe ser un valor numérico."
                txtDiasCredito.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(txtIeps.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " IEPS debe ser un valor numérico."
                txtDiasCredito.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If btnGuardar.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ProveedoresAlta, PermisosN.Secciones.Catalagos) = False Then
                    MensajeError += "No tiene permiso para realizar esta operación."
                    NoErrores = False
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ProveedoresCambio, PermisosN.Secciones.Catalagos) = False Then
                    MensajeError += "No tiene permiso para realizar esta operación."
                    NoErrores = False
                End If
            End If
            If NoErrores Then
                If btnGuardar.Text = "Guardar" Then

                    If P.ChecaClaveRepetida(txtCodigo.Text) = False Then
                        'P.Guardar(TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox2.Text, TextBox6.Text, TextBox8.Text, TextBox7.Text, TextBox12.Text, TextBox9.Text, TextBox10.Text, TextBox11.Text)
                        P.Guardar(txtNombre.Text, txtCalle.Text, txtTelefono.Text, txtEmail.Text, txtContacto.Text, txtCodigo.Text, txtRfc.Text, txtGiro.Text, txtCiudad.Text, txtCp.Text, txtEstado.Text, txtPais.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtReferencia.Text, CDbl(txtDiasCredito.Text), CDbl(txtLimiteCredito.Text), txtCurp.Text, cmbParaDiot.SelectedIndex, IdCuenta, txtRepresentante.Text, CDbl(txtIvaRetenido.Text), CDbl(txtIeps.Text), IdsTipos.Valor(cmbTipo.SelectedIndex), IdCuenta2, IdCuenta3, IdCuenta4, CDbl(txtIsr.Text))
                        PopUp("Guardado", 90)
                        If TipoAlta = 1 Or TipoAlta = 2 Then
                            CodigoProveedor = txtCodigo.Text
                            Nombre = txtNombre.Text
                            RFC = txtRfc.Text
                            IdProveedor = P.ID
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                        End If
                        Nuevo()
                    Else
                        MsgBox("Ya existe un proveedor con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
                    End If

                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim ClaveRepetida As Boolean = False
                        If txtCodigo.Text <> ClaveAnterior Then
                            ClaveRepetida = P.ChecaClaveRepetida(txtCodigo.Text)
                        End If
                        If ClaveRepetida = False Then
                            'P.Modificar(IdProveedor, TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox2.Text, TextBox6.Text, TextBox8.Text, TextBox7.Text, TextBox12.Text, TextBox9.Text, TextBox10.Text, TextBox11.Text)
                            P.Modificar(IdProveedor, txtNombre.Text, txtCalle.Text, txtTelefono.Text, txtEmail.Text, txtContacto.Text, txtCodigo.Text, txtRfc.Text, txtGiro.Text, txtCiudad.Text, txtCp.Text, txtEstado.Text, txtPais.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtReferencia.Text, CDbl(txtDiasCredito.Text), CDbl(txtLimiteCredito.Text), txtCurp.Text, cmbParaDiot.SelectedIndex, IdCuenta, txtRepresentante.Text, CDbl(txtIvaRetenido.Text), CDbl(txtIeps.Text), IdsTipos.Valor(cmbTipo.SelectedIndex), IdCuenta2, IdCuenta3, IdCuenta4, CDbl(txtIsr.Text))
                            PopUp("Modificado", 90)
                            If TipoAlta = 1 Or TipoAlta = 2 Then
                                CodigoProveedor = txtCodigo.Text
                                Nombre = txtNombre.Text
                                RFC = txtRfc.Text
                                Me.DialogResult = Windows.Forms.DialogResult.OK
                            End If
                            Nuevo()
                        Else
                            MsgBox("Ya existe un proveedor con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                End If
                txtCodigo.Focus()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ProveedoresBaja, PermisosN.Secciones.Catalagos) = True Then


                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbproveedores(MySqlcon)
                    If P.contadorPolizas(IdProveedor) > 0 Then
                        MsgBox("El Proveedor no se puede borrar ya que está siendo utilizado.", MsgBoxStyle.OkOnly, GlobalNombreApp)
                    Else
                        P.Eliminar(IdProveedor)
                        PopUp("Eliminado", 90)
                        Nuevo()
                        txtCodigo.Focus()
                    End If

                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este proveedor debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        IdProveedor = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try

            Dim P As New dbproveedores(IdProveedor, MySqlcon)
            btnGuardar.Text = "Modificar"
            btnEliminar.Enabled = True
            ConsultaOn = False
            btnManejarCuentas.Visible = True
            txtCodigo.Text = P.Clave
            txtNombre.Text = P.Nombre
            txtContacto.Text = P.Contacto
            txtTelefono.Text = P.Telefono
            txtEmail.Text = P.Email
            txtRfc.Text = P.RFC
            txtGiro.Text = P.Giro
            txtCalle.Text = P.Direccion
            txtNoExterior.Text = P.NoExterior
            txtNoInterior.Text = P.NoInterior
            txtColonia.Text = P.Colonia
            txtMunicipio.Text = P.Municipio
            txtCiudad.Text = P.Ciudad
            txtReferencia.Text = P.ReferenciaDomicilio
            txtEstado.Text = P.Estado
            txtCp.Text = P.CP
            txtPais.Text = P.Pais
            txtDiasCredito.Text = P.DiasCredito.ToString
            txtLimiteCredito.Text = P.LimiteCredito.ToString
            txtRepresentante.Text = P.representateLegal
            txtIvaRetenido.Text = P.IvaRet.ToString
            txtIeps.Text = P.Ieps.ToString
            txtIsr.Text = P.ISR.ToString
            ClaveAnterior = P.Clave
            txtCurp.Text = P.curp
            cmbParaDiot.SelectedIndex = P.tipo
            IdCuenta = P.idCuenta
            IdCuenta2 = P.IdCuenta2
            IdCuenta3 = P.IdCuenta3
            IdCuenta4 = P.IdCuenta4
            If IdCuenta <> 0 Then
                btnCuentas1.BackColor = ColorVerde
            Else
                btnCuentas1.BackColor = ColorRojo
            End If
            If IdCuenta2 <> 0 Then
                btnCuentas2.BackColor = ColorVerde
            Else
                btnCuentas2.BackColor = ColorRojo
            End If
            If IdCuenta3 <> 0 Then
                btnCuentas3.BackColor = ColorVerde
            Else
                btnCuentas3.BackColor = ColorRojo
            End If
            If IdCuenta4 <> 0 Then
                btnCuentas4.BackColor = ColorVerde
            Else
                btnCuentas4.BackColor = ColorRojo
            End If
            cmbTipo.SelectedIndex = IdsTipos.Busca(P.IdTipo)
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        IdProveedor = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            txtCodigo.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConsulta.TextChanged
        Consulta()
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNombre.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContacto.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTelefono.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        'If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
        '    textBox.SelectedText = Char.ToUpper(e.KeyChar)
        '    e.Handled = True
        'End If
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRfc.KeyPress

        If Not Char.IsNumber(e.KeyChar) And Not Char.IsLetter(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGiro.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox18_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCalle.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox11_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNoExterior.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNoInterior.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox12_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtColonia.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMunicipio.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCiudad.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox21_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtReferencia.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox20_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEstado.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox22_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPais.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox19_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCp.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox13_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConsulta.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox6_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCodigo.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNombre.Focus()
        End If

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNombre.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtContacto.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtContacto.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRfc.Focus()
        End If
    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRfc.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtTelefono.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTelefono.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtEmail.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmail.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCurp.Focus()
        End If
    End Sub

    Private Sub txtCURP_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCurp.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtGiro.Focus()
        End If
    End Sub

    Private Sub TextBox7_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGiro.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCalle.Focus()
        End If
    End Sub

    Private Sub TextBox18_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCalle.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNoExterior.Focus()
        End If
    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNoExterior.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNoInterior.Focus()
        End If
    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNoInterior.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtColonia.Focus()
        End If
    End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtColonia.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMunicipio.Focus()
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMunicipio.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCiudad.Focus()
        End If
    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCiudad.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtReferencia.Focus()
        End If
    End Sub

    Private Sub TextBox21_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReferencia.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtEstado.Focus()
        End If
    End Sub

    Private Sub TextBox20_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEstado.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCp.Focus()
        End If
    End Sub

    Private Sub TextBox19_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCp.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPais.Focus()
        End If
    End Sub

    Private Sub TextBox22_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPais.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDiasCredito.Focus()
        End If
    End Sub

    Private Sub TextBox14_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDiasCredito.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtLimiteCredito.Focus()
        End If
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLimiteCredito.TextChanged

    End Sub

    Private Sub TextBox15_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLimiteCredito.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRepresentante.Focus()
        End If
    End Sub

    Private Sub cmbTipo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbParaDiot.KeyDown
        If e.KeyCode = Keys.Enter Then
            guardar()
        End If
    End Sub

    Private Sub TextBox8_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRfc.Leave
        If txtRfc.Text <> "" Then
            If ValidarRFC(txtRfc.Text) = 1 Then
                MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtRfc.Focus()
                txtRfc.SelectAll()
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

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTipoConsulta.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnManejarCuentas.Click
        Dim f As New frmProveedoresCuentas(IdProveedor)
        f.ShowDialog()
        f.Dispose()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles btnCuentas1.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta = fsc.IdCuenta
            If IdCuenta <> 0 Then
                btnCuentas1.BackColor = ColorVerde
            Else
                btnCuentas1.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub TextBox16_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRepresentante.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbParaDiot.Focus()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btnCuentas2.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta2)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta2 = fsc.IdCuenta
            If IdCuenta2 <> 0 Then
                btnCuentas2.BackColor = ColorVerde
            Else
                btnCuentas2.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnCuentas3.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta3)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta3 = fsc.IdCuenta
            If IdCuenta3 <> 0 Then
                btnCuentas3.BackColor = ColorVerde
            Else
                btnCuentas3.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnCuentas4.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta4)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta4 = fsc.IdCuenta
            If IdCuenta4 <> 0 Then
                btnCuentas4.BackColor = ColorVerde
            Else
                btnCuentas4.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub
End Class