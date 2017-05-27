Public Class frmInvnetarioConceptos



    'Private Sub frmInvnetarioConceptos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
   
    '    NuevoConcepto()
    'End Sub

    Dim IdConcepto As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdsSucursales As New elemento
    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            ConsultaOn = False
            LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales)
            ConsultaOn = True
            If ComboBox1.Items.Count = 0 Then
                MsgBox("Se deben registrar primero las sucursales para poder dar de alta conceptos.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = False
            End If
            ComboBox2.Items.Add("Entrada")
            ComboBox2.Items.Add("Salida")
            ComboBox2.Items.Add("Inventario Físico")
            ComboBox2.Items.Add("Traspaso")
            ComboBox2.Items.Add("Inventario Inicial")
            Nuevo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox6.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = "1"
            TextBox4.Text = ""
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ComboBox2.SelectedIndex = 0
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
                Dim P As New dbInventarioConceptos(MySqlcon)
                DataGridView1.DataSource = P.Consulta(TextBox3.Text, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Concepto"
                DataGridView1.Columns(2).HeaderText = "Tipo"
                DataGridView1.Columns(3).HeaderText = "Serie"
                DataGridView1.Columns(4).HeaderText = "Folio"
                DataGridView1.Columns(5).Visible = False
                DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
            Dim P As New dbInventarioConceptos(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al concepto."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox2.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El folio debe ser un valor numérico."
                TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioAlta, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioCambio, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    P.Guardar(TextBox6.Text, ComboBox2.SelectedIndex, TextBox1.Text, CInt(TextBox2.Text), TextBox4.Text, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    PopUp("Guardado", 90)
                    Nuevo()
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        P.Modificar(IdConcepto, TextBox6.Text, ComboBox2.SelectedIndex, TextBox1.Text, CInt(TextBox2.Text), TextBox4.Text)
                        PopUp("Modificado", 90)
                        Nuevo()
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioBaja, PermisosN.Secciones.Catalagos) = False Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbInventarioConceptos(MySqlcon)
                    P.Eliminar(IdConcepto)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox6.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este concepto debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            IdConcepto = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbInventarioConceptos(IdConcepto, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            TextBox6.Text = P.Nombre
            TextBox1.Text = P.Serie
            TextBox2.Text = P.Folio.ToString
            TextBox4.Text = P.Formato
            ComboBox1.SelectedIndex = IdsSucursales.Busca(P.IdSucursal)
            ComboBox2.SelectedIndex = P.Tipo
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
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

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ConsultaOn Then
            Nuevo()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 4 Then
            e.Value = Format(e.Value, DataGridView1.Item(5, e.RowIndex).Value)
        End If
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
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

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub
End Class