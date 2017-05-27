Public Class frmZona
    Dim P As New dbZona(MySqlcon)
    Dim ID As Integer

    Private Sub frmZona_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        txtCodigo.Text = P.Folio
        FiltroTodos()
    End Sub

    Private Sub FiltroTodos()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex

            DataGridView1.DataSource = P.reporte()
            DataGridView1.Columns(0).HeaderText = "Código"
            DataGridView1.Columns(1).HeaderText = "Nombre"
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            Dim P As New dbZona(MySqlcon)
            DataGridView1.DataSource = P.Consultar(txtConsulta.Text)
            DataGridView1.Columns(0).HeaderText = "Código"
            DataGridView1.Columns(1).HeaderText = "Nombre"
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub txtConsulta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConsulta.TextChanged
        Consulta()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        txtZona.BackColor = Color.White

        
        If txtZona.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar el nombre del la zona."
            txtZona.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ZonasAlta, PermisosN.Secciones.Catalagos2) = False And btnGuardar.Text = "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ZonasCambio, PermisosN.Secciones.Catalagos2) = False And btnGuardar.Text <> "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        If NoErrores = True Then
            If btnGuardar.Text = "Guardar" Then

                P.Guardar(txtZona.Text)
                PopUp("Guardado", 90)
                Nuevo()
                ' btnEliminar.Enabled = True
                txtZona.Focus()

            Else
                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim ClaveRepetida As Boolean = False
                    P.Modificar(Integer.Parse(txtCodigo.Text), txtZona.Text)
                    PopUp("Modificado", 90)
                    Nuevo()
                    ' btnEliminar.Enabled = True
                    txtZona.Focus()
                End If
            End If
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub
    Private Sub Nuevo()
        txtCodigo.Text = P.Folio
        txtZona.Text = ""
        txtZona.Focus()
        txtConsulta.Text = ""
        FiltroTodos()
        btnGuardar.Text = "Guardar"
        btnEliminar.Enabled = False
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.ZonasBaja, PermisosN.Secciones.Catalagos2) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    'Dim P As New dbBanco(MySqlcon)
                    P.Eliminar(Integer.Parse(txtCodigo.Text))
                    PopUp("Eliminado", 90)
                    Nuevo()
                    txtZona.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        llenaDatos()
        '   btnDiseno.Enabled = True
        btnEliminar.Enabled = True
        txtZona.Focus()

    End Sub
    Private Sub llenaDatos()

        Try
            ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim Q As New dbZona(ID, MySqlcon)
            btnGuardar.Text = "Modificar"
            txtCodigo.Text = Q.ID
            txtZona.Text = Q.zona
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Nuevo()
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub txtZona_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZona.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub
End Class