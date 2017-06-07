Public Class frmAlmacenes
    Dim IdAlmacen As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdsSucursales As New elemento
    Dim IdCuenta As Integer
    Dim IdCuenta2 As Integer
    Dim IdCuenta3 As Integer
    Dim IdCuenta4 As Integer
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales)
            ComboBox2.Items.Add("Estacionario")
            ComboBox2.Items.Add("Móvil")
            If ComboBox1.Items.Count = 0 Then
                MsgBox("Se deben registrar primero las sucursales para poder dar de alta almacenes.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button1.Enabled = False
                Button2.Enabled = False
                Button3.Enabled = False
            End If
            If GlobalTipoUsuario = 0 Then Button5.Visible = True
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
            TextBox2.Text = ""
            TextBox4.Text = "0.0"
            ComboBox2.SelectedIndex = 0
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ComboBox1.SelectedIndex = 0
            ComboBox1.Enabled = True
            ConsultaOn = True
            IdCuenta = 0
            IdCuenta2 = 0
            IdCuenta3 = 0
            IdCuenta4 = 0
            Button9.BackColor = ColorRojo
            Button10.BackColor = ColorRojo
            Button11.BackColor = ColorRojo
            Button12.BackColor = ColorRojo
            Button5.Enabled = False
            Dim p As New dbAlmacenes(MySqlcon)
            cmbUbicaciones.DataSource = p.Ubicaciones(0)
            cmbUbicaciones.Text = ""
            cmbUbicaciones.Enabled = False
            btnEliminarU.Enabled = False
            btnGuardarU.Enabled = False
            btnGenerarU.Enabled = False
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
                Dim P As New dbAlmacenes(MySqlcon)
                DataGridView1.DataSource = P.Consulta(TextBox3.Text)
                'DataGridView1.Columns(0).Visible = False
                'DataGridView1.Columns(1).HeaderText = "Número"
                'DataGridView1.Columns(2).HeaderText = "Nombre"
                'DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
            Dim P As New dbAlmacenes(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un número al almacen."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al almacen."
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
            If IsNumeric(TextBox4.Text) = False Then TextBox4.Text = "0"
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    If P.ChecaNumeroRepetido(TextBox6.Text) = False Then
                        P.Guardar(TextBox1.Text, TextBox2.Text, TextBox6.Text, IdsSucursales.Valor(ComboBox1.SelectedIndex), CDbl(TextBox4.Text), ComboBox2.SelectedIndex, 0, IdCuenta, IdCuenta2, IdCuenta3, IdCuenta4)
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("Ya existe un almacen con ese número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        TextBox6.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        Dim ClaveRepetida As Boolean = False
                        If TextBox6.Text <> ClaveAnterior Then
                            ClaveRepetida = P.ChecaNumeroRepetido(TextBox6.Text)
                        End If
                        If ClaveRepetida = False Then
                            P.Modificar(IdAlmacen, TextBox1.Text, TextBox2.Text, TextBox6.Text, IdsSucursales.Valor(ComboBox1.SelectedIndex), CDbl(TextBox4.Text), ComboBox2.SelectedIndex, IdCuenta, IdCuenta2, IdCuenta3, IdCuenta4)
                            PopUp("Modificado", 90)
                            Nuevo()
                        Else
                            MsgBox("Ya existe un almacen con ese número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbAlmacenes(MySqlcon)
                    P.Eliminar(IdAlmacen)
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
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            IdAlmacen = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbAlmacenes(IdAlmacen, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = P.Numero
            TextBox6.Text = P.Numero
            TextBox1.Text = P.Nombre
            TextBox2.Text = P.Direccion
            TextBox4.Text = P.Peso
            IdCuenta = P.idCuenta
            IdCuenta2 = P.idCuenta2
            IdCuenta3 = P.idCuenta3
            IdCuenta4 = P.idCuenta4
            If IdCuenta <> 0 Then
                Button9.BackColor = ColorVerde
            Else
                Button9.BackColor = ColorRojo
            End If
            If IdCuenta2 <> 0 Then
                Button10.BackColor = ColorVerde
            Else
                Button10.BackColor = ColorRojo
            End If
            If IdCuenta3 <> 0 Then
                Button11.BackColor = ColorVerde
            Else
                Button11.BackColor = ColorRojo
            End If
            If IdCuenta4 <> 0 Then
                Button12.BackColor = ColorVerde
            Else
                Button12.BackColor = ColorRojo
            End If
            ComboBox2.SelectedIndex = P.Tipo
            ComboBox1.SelectedIndex = IdsSucursales.Busca(P.IdSucursal)
            ComboBox1.Enabled = False

            cmbUbicaciones.DataSource = P.Ubicaciones(P.ID)
            cmbUbicaciones.Text = ""
            cmbUbicaciones.Enabled = True
            btnEliminarU.Enabled = True
            btnGuardarU.Enabled = True
            btnGenerarU.Enabled = True
            Button5.Enabled = True
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta = fsc.IdCuenta
            If IdCuenta <> 0 Then
                Button9.BackColor = ColorVerde
            Else
                Button9.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta2)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta2 = fsc.IdCuenta
            If IdCuenta2 <> 0 Then
                Button10.BackColor = ColorVerde
            Else
                Button10.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta3)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta3 = fsc.IdCuenta
            If IdCuenta3 <> 0 Then
                Button11.BackColor = ColorVerde
            Else
                Button11.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta4)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta4 = fsc.IdCuenta
            If IdCuenta4 <> 0 Then
                Button12.BackColor = ColorVerde
            Else
                Button12.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub btnGuardarU_Click(sender As Object, e As EventArgs) Handles btnGuardarU.Click
        If cmbUbicaciones.Text = "" Then
            MsgBox("Indique una ubicación.")
        Else
            Dim db As New dbAlmacenes(MySqlcon)
            If cmbUbicaciones.SelectedIndex = -1 Then
                db.AgregarUbicacion(IdAlmacen, cmbUbicaciones.Text)
            Else
                db.ModificarUbicacion(cmbUbicaciones.SelectedValue, cmbUbicaciones.Text)
            End If
            cmbUbicaciones.DataSource = db.Ubicaciones(IdAlmacen)
            cmbUbicaciones.Text = ""
            cmbUbicaciones.Enabled = True
            btnEliminarU.Enabled = True
            btnGuardarU.Enabled = True
            PopUp("Guardado.", 100)
        End If
    End Sub

    Private Sub btnEliminarU_Click(sender As Object, e As EventArgs) Handles btnEliminarU.Click
        Dim db As New dbAlmacenes(MySqlcon)
        If cmbUbicaciones.SelectedIndex = -1 Then
            MsgBox("Seleccione una ubicación.")
        Else
            If db.EliminarUbicacion(cmbUbicaciones.SelectedValue) Then
                cmbUbicaciones.DataSource = db.Ubicaciones(IdAlmacen)
                cmbUbicaciones.Text = ""
                cmbUbicaciones.Enabled = True
                btnEliminarU.Enabled = True
                btnGuardarU.Enabled = True
                PopUp("Eliminado.", 100)
            Else
                MsgBox("La ubicación tiene movimientos.")
            End If
        End If
    End Sub

    Private Sub btnGenerarU_Click(sender As Object, e As EventArgs) Handles btnGenerarU.Click
        Dim f As New frmUbicaciones(IdAlmacen)
        f.ShowDialog()
        Dim db As New dbAlmacenes(MySqlcon)
        cmbUbicaciones.DataSource = db.Ubicaciones(IdAlmacen)
        cmbUbicaciones.Text = ""
        cmbUbicaciones.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If IdAlmacen <> 0 Then
            Dim fap As New frmAlmacenesPermisos(IdAlmacen, TextBox1.Text, ComboBox1.Text)
            fap.ShowDialog()
            fap.Dispose()
        End If
    End Sub
End Class