Public Class frmVendedores

    Dim IdVendedor As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdsVendedores As New elemento
    Dim IdsUsuario As New elemento
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox6.Text = ""
            TextBox1.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox10.Text = ""
            TextBox9.Text = ""
            TextBox8.Text = ""
            TextBox7.Text = "MÉXICO"
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            cmbZona.SelectedIndex = 0
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ConsultaOn = True
            Consulta()
            Dim C As New dbVendedores(MySqlcon)
            ConsultaOn = False
            TextBox6.Text = Format(C.DaMaximo, "0000")
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
                Dim P As New dbVendedores(MySqlcon)
                DataGridView1.DataSource = P.Consulta(0, TextBox2.Text)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Código"
                DataGridView1.Columns(2).HeaderText = "Nombre"
                DataGridView1.Columns(3).HeaderText = "Teléfono"
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        'Consulta()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'Consulta()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbVendedores(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim idClas As Integer
            Dim esMesero As Byte = 0
            Dim esCajero As Byte = 0
            If CheckBox1.Checked Then esMesero = 1
            If CheckBox2.Checked Then esCajero = 1
            If cmbZona.SelectedIndex <= 0 Then
                idClas = 0
            Else
                idClas = IdsVendedores.Valor(cmbZona.SelectedIndex)
            End If


            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al vendedor."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un código al vendedor."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.VendedoresAlta, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.VendedoresCambio, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then

                    If P.ChecaClaveRepetida(TextBox6.Text) = False Then
                        P.Guardar(TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox6.Text, TextBox10.Text, TextBox9.Text, TextBox8.Text, TextBox7.Text, idClas, esMesero, esCajero)
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("Ya existe un vendedor con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                
                Else

                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        Dim ClaveRepetida As Boolean = False
                        If TextBox6.Text <> ClaveAnterior Then
                            ClaveRepetida = P.ChecaClaveRepetida(TextBox6.Text)
                        End If
                        If ClaveRepetida = False Then
                            P.Modificar(IdVendedor, TextBox1.Text, TextBox5.Text, TextBox3.Text, TextBox4.Text, TextBox6.Text, TextBox10.Text, TextBox9.Text, TextBox8.Text, TextBox7.Text, idClas, esCajero, esMesero)
                            PopUp("Modificado", 90)
                            Nuevo()
                        Else
                            MsgBox("Ya existe un vendedor con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                   
                End If
            TextBox6.Focus()
            Else
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.VendedoresBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbVendedores(MySqlcon)
                    P.Eliminar(IdVendedor)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox6.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este vendedor debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            IdVendedor = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbVendedores(IdVendedor, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            TextBox6.Text = P.Clave
            TextBox1.Text = P.Nombre
            TextBox3.Text = P.Telefono
            TextBox4.Text = P.Email
            TextBox5.Text = P.Direccion
            ClaveAnterior = P.Clave
            TextBox10.Text = P.Ciudad
            TextBox9.Text = P.CP
            TextBox8.Text = P.Estado
            TextBox7.Text = P.Pais
            If P.EsMesero = 0 Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If
            If P.EsCajero = 0 Then
                CheckBox2.Checked = False
            Else
                CheckBox2.Checked = True
            End If
            ' ComboBox1.SelectedIndex = IDsMonedas.Busca(a.IdMoneda)
            cmbZona.SelectedIndex = IdsVendedores.Busca(P.zona)
            ' txtZona.Text = P.zona
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub frmTecnicos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblzona", cmbZona, "zona", "zonat", "idZona", IdsVendedores, , "Todos")
        Nuevo()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            TextBox6.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Consulta()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        'Dim textBox As TextBox = DirectCast(sender, TextBox)
        'If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
        '    textBox.SelectedText = Char.ToUpper(e.KeyChar)
        '    e.Handled = True
        'End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox10_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox9_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub
End Class