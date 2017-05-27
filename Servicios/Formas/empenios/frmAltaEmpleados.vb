Imports System.Text.RegularExpressions

Public Class frmAltaEmpleados
    Dim IdEmpleado As Integer
    Dim P As New dbAltaEmpleados(MySqlcon)
    Dim tabla As New DataTable

    Private Sub frmAltaEmpleados_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        txtNombre.Focus()
        tabla.Columns.Add("idEmpleado")
        tabla.Columns.Add("Nombre")
        tabla.Columns.Add("RFC")
        tabla.Columns.Add("Telefono")
        tabla.Columns.Add("Salario")
        nuevo()
    End Sub
    Private Sub txtNombre_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNombre.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsSeparator(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Public Function validar_Mail(ByVal sMail As String) As Boolean
        ' retorna true o false   
        Return Regex.IsMatch(sMail, "^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$")
    End Function
    Private Sub txtEmail_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.Leave

        If (validar_Mail(LCase(txtEmail.Text)) = False And txtEmail.Text <> "") Then
            MessageBox.Show("Dirección de correo electronico no valida, el correo debe tener el formato: nombre@dominio.com, por favor seleccione un correo valido", "Validación de correo electronico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtEmail.Focus()
            txtEmail.SelectAll()
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
    Private Sub txtCURP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCURP.KeyPress

        If Char.IsLower(e.KeyChar) Then
            txtCURP.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub
    Private Sub txtSalario_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSalario.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtSalario_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalario.Leave
        Dim x As Double
        If txtSalario.Text = "." Then
            txtSalario.Text = "0.00"
        End If
        If txtSalario.Text = "" Then
            txtSalario.Text = "0.00"
        Else
            x = Double.Parse(txtSalario.Text)
            txtSalario.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub nuevo()
        idEmpleado = -1
        txtNombre.Text = ""
        txtCalle.Text = ""
        txtCURP.Text = ""
        txtEmail.Text = ""
        txtNoExterior.Text = ""
        txtNoInterior.Text = ""
        txtRFC.Text = "XXXXXXXXXXXXX"
        txtSalario.Text = "0.00"
        txtTelefono.Text = ""
        txtCalle.Text = ""
        txtColonia.Text = ""
        txtMunicipio.Text = ""
        txtCiudad.Text = ""
        txtReferencia.Text = ""
        txtCP.Text = "00000"
        txtEstado.Text = ""
        btnGuardar.Text = "Guardar"
        txtNombre.Focus()
        filtrar()
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub


    Private Sub txtRFC_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRFC.KeyPress
        If Char.IsLower(e.KeyChar) And txtRFC.Text.Length <= 12 Then
            txtRFC.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

  

    Private Sub txtRFC_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFC.Leave
        If ValidarRFC(txtRFC.Text) = 1 And txtRFC.Text <> "" And txtRFC.Text <> "XXXXXXXXXXXXX" Then
            MessageBox.Show("RFC no es válido, por favor seleccione un RFC válido", "Validación de RFC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtRFC.Focus()
            txtRFC.SelectAll()
        End If
        If txtRFC.Text = "" Then
            txtRFC.Text = "XXXXXXXXXXXXX"
        End If
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try

            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            txtNombre.BackColor = Color.White
            txtSalario.BackColor = Color.White

            If txtNombre.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al empleado."
                txtNombre.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtSalario.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un salario al empleado."
                txtSalario.BackColor = Color.FromArgb(250, 150, 150)
            End If

            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosAlta, PermisosN.Secciones.Gastos) = False And btnGuardar.Text = "Guardar" Then
                MensajeError += " No tiene permiso para realizar esta operación."
                NoErrores = False
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosCambios, PermisosN.Secciones.Gastos) = False And btnGuardar.Text <> "Guardar" Then
                NoErrores = False
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
      
            If NoErrores Then
                If btnGuardar.Text = "Guardar" Then


                    P.Guardar(txtNombre.Text, txtTelefono.Text, txtEmail.Text, txtRFC.Text, txtCURP.Text, txtSalario.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtCiudad.Text, txtReferencia.Text, txtEstado.Text, txtCP.Text, txtPais.Text)
                    PopUp("Guardado", 90)
                    nuevo()
                   
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                    
                        'P.Modificar(IdCliente, TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox2.Text, TextBox6.Text, TextBox8.Text, TextBox7.Text, TextBox12.Text, TextBox9.Text, TextBox10.Text, TextBox11.Text, TextBox17.Text, TextBox16.Text, TextBox13.Text, TextBox14.Text, TextBox15.Text)
                        P.Modificar(IdEmpleado, txtNombre.Text, txtTelefono.Text, txtEmail.Text, txtRFC.Text, txtCURP.Text, txtSalario.Text, txtCalle.Text, txtNoExterior.Text, txtNoInterior.Text, txtColonia.Text, txtMunicipio.Text, txtCiudad.Text, txtReferencia.Text, txtEstado.Text, txtCP.Text, txtPais.Text)
                        PopUp("Modificado", 90)
                        nuevo()

                    End If
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosBaja, PermisosN.Secciones.Gastos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                    P.Eliminar(IdEmpleado)
                    PopUp("Eliminado", 90)
                    nuevo()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
    Private Sub filtrar()
        tabla = P.Consulta(txtBuscarNombre.Text)
        dgvEmpleados.DataSource = tabla
        dgvEmpleados.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvEmpleados.Columns(0).Visible = False
    End Sub

    Private Sub dgvEmpleados_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvEmpleados.CellClick
        IdEmpleado = dgvEmpleados.Item(0, dgvEmpleados.CurrentCell.RowIndex).Value
        LlenaDatos()
    End Sub
    Private Sub llenaDatos()
        P.LlenaDatos(IdEmpleado)
        txtNombre.Text = P.nombre
        txtCalle.Text = P.calle
        txtCURP.Text = P.curp
        txtEmail.Text = P.eMail
        txtNoExterior.Text = P.nExterior
        txtNoInterior.Text = P.nInterior
        If P.RFC = "" Then
            txtRFC.Text = "XXXXXXXXXXXXX"
        Else
            txtRFC.Text = P.RFC
        End If

        txtSalario.Text = P.salario
        txtTelefono.Text = P.telefono
        txtCalle.Text = P.calle
        txtColonia.Text = P.colonia
        txtMunicipio.Text = P.municipio
        txtCiudad.Text = P.ciudad
        txtReferencia.Text = P.referencia
        txtCP.Text = P.CP
        txtEstado.Text = P.estado
        btnGuardar.Text = "Modificar"
        txtNombre.Focus()
    End Sub

    Private Sub txtCP_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCP.Leave
        If txtCP.Text = "" Then
            txtCP.Text = "00000"
        End If
    End Sub

    Private Sub txtNombre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtTelefono_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTelefono.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtRFC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRFC.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtCURP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCURP.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtCalle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCalle.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtNoExterior_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoExterior.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtNoInterior_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoInterior.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtColonia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtColonia.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtMunicipio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMunicipio.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtCiudad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCiudad.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtReferencia_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtReferencia.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtEstado_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEstado.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtPais_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPais.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub txtBuscarNombre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuscarNombre.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub
End Class