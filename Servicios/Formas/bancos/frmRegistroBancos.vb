Public Class frmRegistroBancos
    Dim IdBanco As Integer

    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        FiltroTodos()
        Dim P As New dbBanco(MySqlcon)
        btnDiseno.Enabled = False
        txtCodigo.Text = P.Folio()
        txtNombre.Focus()

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim P As New dbBanco(MySqlcon)
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        txtCodigo.BackColor = Color.White
        txtNombre.BackColor = Color.White

        If txtCodigo.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar el código del banco."
            txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtNombre.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar el nombre del banco."
            txtNombre.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesAlta, PermisosN.Secciones.Catalagos) = False Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If

        If NoErrores = True Then
            If btnGuardar.Text = "Guardar" Then
                If P.ClaveRepetida(txtCodigo.Text) = False Then ' no es cuenta repetida
                    P.Guardar(txtCodigo.Text, txtNombre.Text)
                    PopUp("Guardado", 90)
                    Nuevo()
                    btnDiseno.Enabled = True
                    txtNombre.Focus()
                Else
                    MsgBox("Ya existe un banco con el mismo código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                    txtCodigo.BackColor = Color.FromArgb(250, 150, 150)
                End If
            Else
                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim ClaveRepetida As Boolean = False
                    P.Modificar(IdBanco, txtCodigo.Text, txtNombre.Text)
                    PopUp("Modificado", 90)
                    Nuevo()
                    btnDiseno.Enabled = True
                    txtNombre.Focus()
                End If
            End If
        End If


            If NoErrores = False Then
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

            End If

    End Sub

    Private Sub Nuevo()

        Try
            btnDiseno.Enabled = False
            txtCodigo.Text = ""
            txtNombre.Text = ""
            txtConsulta.Text = ""
            txtCodigo.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtNombre.BackColor = Color.FromKnownColor(KnownColor.Window)
            btnGuardar.Text = "Guardar"
            IdBanco = 0
            FiltroTodos()
            btnEliminar.Enabled = False
            Dim P As New dbBanco(MySqlcon)
            txtCodigo.Text = P.Folio()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub FiltroTodos()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            Dim P As New dbBanco(MySqlcon)
            DataGridView1.DataSource = P.reporte()
            DataGridView1.Columns(0).HeaderText = "Código"
            DataGridView1.Columns(1).HeaderText = "Nombre"
            DataGridView1.Columns(2).Visible = False
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            Dim P As New dbBanco(MySqlcon)
            DataGridView1.DataSource = P.Consultar(txtConsulta.Text)
            DataGridView1.Columns(0).HeaderText = "código"
            DataGridView1.Columns(1).HeaderText = "Nombre"
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub txtConsulta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConsulta.TextChanged

        If txtConsulta.Text = "" Then
            FiltroTodos()
        Else
            Consulta()
        End If

    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        llenaDatos()
        btnDiseno.Enabled = True
        btnEliminar.Enabled = True
        txtNombre.Focus()

    End Sub

    Private Sub llenaDatos()

        Try
            IdBanco = DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbBanco(IdBanco, MySqlcon)
            btnGuardar.Text = "Modificar"
            txtCodigo.Text = P.Codigo
            txtNombre.Text = P.Nombre
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbBanco(MySqlcon)
                    P.Eliminar(IdBanco)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    txtCodigo.Focus()
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

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click

        Nuevo()
        txtNombre.Focus()

    End Sub

    Private Sub txtCodigo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If


    End Sub

    Private Sub btnDiseno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiseno.Click

        If btnGuardar.Text = "Guardar" Then
            MsgBox("Seleccione un registro.", MsgBoxStyle.Critical, GlobalNombreApp)
        Else
            'Diseño cheque
        End If

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click

        Me.Close()

    End Sub

    Private Sub txtNombre_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNombre.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtConsulta_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConsulta.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub
End Class

'Falta diseño de cheque