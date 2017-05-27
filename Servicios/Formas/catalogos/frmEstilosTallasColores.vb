Public Class frmEstilosTallasColores
    Dim Id As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    'Dim IdsSucursales As New elemento
    Dim Tipo As Byte
    Public Sub New(ByVal pTipo As Byte)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Tipo = pTipo
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Select Case Tipo
                Case 0
                    Me.Text = "Modelos"
                Case 1
                    Me.Text = "Tallas"
                Case 2
                    Me.Text = "Colores"
            End Select
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
            Button5.Visible = False
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            'ComboBox1.SelectedIndex = 0
            Select Case Tipo
                Case 0
                    Dim M As New dbModelos(MySqlcon)
                    TextBox6.Text = Format(M.DaMaximo, "00")
                Case 1
                    Dim M As New dbTallas(MySqlcon)
                    TextBox6.Text = Format(M.DaMaximo, "00")
                Case 2
                    Dim M As New dbColores(MySqlcon)
                    TextBox6.Text = Format(M.DaMaximo, "00")
            End Select
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
                Select Case Tipo
                    Case 0
                        Dim P As New dbModelos(MySqlcon)
                        DataGridView1.DataSource = P.Consulta(TextBox3.Text)
                    Case 1
                        Dim P As New dbTallas(MySqlcon)
                        DataGridView1.DataSource = P.Consulta(TextBox3.Text)
                    Case 2
                        Dim P As New dbColores(MySqlcon)
                        DataGridView1.DataSource = P.Consulta(TextBox3.Text)
                End Select
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Código"
                DataGridView1.Columns(2).HeaderText = "Nombre"
                DataGridView1.Columns(3).HeaderText = "Comentario"
                DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un código."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar una descripción."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesAlta, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesCambio, PermisosN.Secciones.Catalagos) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    Select Case Tipo
                        Case 0
                            GuardarModelos()
                        Case 1
                            GuardarTallas()
                        Case 2
                            GuardarColores()
                    End Select
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        Select Case Tipo
                            Case 0
                                ModificaModelos()
                            Case 1
                                ModificaTallas()
                            Case 2
                                ModificaColores()
                        End Select
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
    Private Sub GuardarModelos()
        Dim P As New dbModelos(MySqlcon)
        If P.ChecaNumeroRepetido(TextBox6.Text) = False Then
            P.Guardar(TextBox1.Text, TextBox6.Text, TextBox2.Text)
            PopUp("Guardado", 90)
            Nuevo()
        Else
            MsgBox("Ya existe un modelo con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
        End If
    End Sub
    Private Sub GuardarTallas()
        Dim P As New dbTallas(MySqlcon)
        If P.ChecaNumeroRepetido(TextBox6.Text) = False Then
            P.Guardar(TextBox1.Text, TextBox6.Text, TextBox2.Text)
            PopUp("Guardado", 90)
            Nuevo()
        Else
            MsgBox("Ya existe una talla con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
        End If
    End Sub
    Private Sub GuardarColores()
        Dim P As New dbColores(MySqlcon)
        If P.ChecaNumeroRepetido(TextBox6.Text) = False Then
            P.Guardar(TextBox1.Text, TextBox6.Text, TextBox2.Text)
            PopUp("Guardado", 90)
            Nuevo()
        Else
            MsgBox("Ya existe un color con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
        End If
    End Sub
    Private Sub ModificaModelos()
        Dim P As New dbModelos(MySqlcon)
        Dim ClaveRepetida As Boolean = False
        If TextBox6.Text <> ClaveAnterior Then
            ClaveRepetida = P.ChecaNumeroRepetido(TextBox6.Text)
        End If
        If ClaveRepetida = False Then
            P.Modificar(Id, TextBox1.Text, TextBox6.Text, TextBox2.Text)
            PopUp("Modificado", 90)
            Nuevo()
        Else
            MsgBox("Ya existe un modelo con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
        End If
    End Sub
    Private Sub ModificaTallas()
        Dim P As New dbTallas(MySqlcon)
        Dim ClaveRepetida As Boolean = False
        If TextBox6.Text <> ClaveAnterior Then
            ClaveRepetida = P.ChecaNumeroRepetido(TextBox6.Text)
        End If
        If ClaveRepetida = False Then
            P.Modificar(Id, TextBox1.Text, TextBox6.Text, TextBox2.Text)
            PopUp("Modificado", 90)
            Nuevo()
        Else
            MsgBox("Ya existe una talla con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
        End If
    End Sub
    Private Sub ModificaColores()
        Dim P As New dbColores(MySqlcon)
        Dim ClaveRepetida As Boolean = False
        If TextBox6.Text <> ClaveAnterior Then
            ClaveRepetida = P.ChecaNumeroRepetido(TextBox6.Text)
        End If
        If ClaveRepetida = False Then
            P.Modificar(Id, TextBox1.Text, TextBox6.Text, TextBox2.Text)
            PopUp("Modificado", 90)
            Nuevo()
        Else
            MsgBox("Ya existe un color con ese código, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox6.BackColor = Color.FromArgb(250, 150, 150)
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Select Case Tipo
                        Case 0
                            Dim P As New dbModelos(MySqlcon)
                            P.Eliminar(Id)
                        Case 1
                            Dim P As New dbTallas(MySqlcon)
                            P.Eliminar(Id)
                        Case 2
                            Dim P As New dbColores(MySqlcon)
                            P.Eliminar(Id)
                    End Select
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox6.Focus()
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
        Select Case Tipo
            Case 0
                LlenaDatosModelos()
            Case 1
                LlenaDatosTallas()
            Case 2
                LlenaDatosColores()
        End Select
    End Sub

    Private Sub LlenaDatosModelos()
        Try
            Id = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbModelos(Id, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = P.Codigo
            TextBox6.Text = P.Codigo
            TextBox1.Text = P.Nombre
            TextBox2.Text = P.Comentario
            Button5.Visible = True
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosTallas()
        Try
            Id = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbTallas(Id, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = P.Codigo
            TextBox6.Text = P.Codigo
            TextBox1.Text = P.Nombre
            TextBox2.Text = P.Comentario
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosColores()
        Try
            Id = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbColores(Id, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = P.Codigo
            TextBox6.Text = P.Codigo
            TextBox1.Text = P.Nombre
            TextBox2.Text = P.Comentario
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            Select Case Tipo
                Case 0
                    LlenaDatosModelos()
                Case 1
                    LlenaDatosTallas()
                Case 2
                    LlenaDatosColores()
            End Select
            TextBox6.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            Select Case Tipo
                Case 0
                    LlenaDatosModelos()
                Case 1
                    LlenaDatosTallas()
                Case 2
                    LlenaDatosColores()
            End Select
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Consulta()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New frmInventarioGeneradorMTC(Id)
        f.ShowDialog()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub
End Class