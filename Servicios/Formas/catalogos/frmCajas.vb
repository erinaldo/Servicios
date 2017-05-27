Public Class frmCajas
    Dim IdCaja As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdsSucursales As New elemento
    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales)
            If ComboBox1.Items.Count = 0 Then
                MsgBox("Se deben registrar primero las sucursales para poder dar de alta cajas.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = False
            End If
            Nuevo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = "0"
            ComboBox1.Enabled = True
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ComboBox1.SelectedIndex = 0
            ConsultaOn = True
            Consulta()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbCajas(MySqlcon)
                DataGridView1.DataSource = P.Consulta(TextBox3.Text)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Nombre"
                DataGridView1.Columns(2).HeaderText = "Sucursal"
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbCajas(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre a la caja."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox6.Text <> "" And IsNumeric(TextBox6.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un valor numérico."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            Else
                If TextBox6.Text = "" Then TextBox6.Text = "0"
            End If
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.CajasAlta, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.CajasCambio, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    If P.ChecaNombreRepetido(TextBox1.Text, IdsSucursales.Valor(ComboBox1.SelectedIndex)) = False Then
                        P.Guardar(TextBox1.Text, IdsSucursales.Valor(ComboBox1.SelectedIndex), TextBox2.Text, TextBox4.Text, TextBox5.Text, CDbl(TextBox6.Text))
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("Ya existe una caja con ese nombre, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        TextBox1.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        Dim ClaveRepetida As Boolean = False
                        If TextBox1.Text <> ClaveAnterior Then
                            ClaveRepetida = P.ChecaNombreRepetido(TextBox1.Text, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                        End If
                        If ClaveRepetida = False Then
                            P.Modificar(IdCaja, TextBox1.Text, TextBox2.Text, TextBox4.Text, TextBox5.Text, CDbl(TextBox6.Text))
                            PopUp("Modificado", 90)
                            Nuevo()
                        Else
                            MsgBox("Ya existe una caja con ese nombre, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            TextBox1.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                End If
            TextBox1.Focus()
            Else
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.CajasBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbCajas(MySqlcon)
                    P.Eliminar(IdCaja)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox1.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este almacen debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            IdCaja = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbCajas(IdCaja, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = P.Nombre
            TextBox1.Text = P.Nombre
            ComboBox1.SelectedIndex = IdsSucursales.Busca(P.IdSucursal)
            TextBox2.Text = P.Serie
            TextBox4.Text = P.SerieCot
            TextBox5.Text = P.SeriePed
            TextBox6.Text = P.Maximo.ToString
            ComboBox1.Enabled = False
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub




    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Consulta()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
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

    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
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
End Class