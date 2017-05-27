Public Class frmEmpeniosClasificacion
    Dim datos As New dbEmpeniosClasificacion(MySqlcon)
    Dim ID As Integer

    Private Sub frmEmpeniosClasificacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        txtID.Text = datos.idProximo().ToString("000")
        nuevo()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
     
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
       
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txtBusqueda_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBusqueda.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
        filtro()
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
    Private Sub nuevo()
        filtro()
        txtID.Text = datos.idProximo().ToString("000")
        txtNombre.Text = ""
        btnGuardar.Text = "Guardar"
        btnEliminar.Enabled = False
        txtNombre.Focus()
    End Sub
    Private Sub filtro()
        Dim tabla As DataTable
        tabla = datos.filtroClasificacion(txtBusqueda.Text)
        DataGridView1.DataSource = tabla
        DataGridView1.Columns(0).HeaderText = "ID"
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).HeaderText = "Nombre"
        DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Dim column As DataGridViewColumn = DataGridView1.Columns(0)
        column.Width = 40

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        llenadatos()
        txtNombre.Focus()
    End Sub
    Private Sub llenadatos()
        Try
            ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            txtID.Text = ID.ToString("000")
            btnGuardar.Text = "Modificar"
            txtNombre.Text = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value
            btnEliminar.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnGuardar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""

        If txtNombre.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar un nombre para la clasificación."
            txtNombre.BackColor = Color.FromArgb(250, 150, 150)
        Else
            If datos.esRepetida(txtNombre.Text) And btnGuardar.Text = "Guardar" Then
                NoErrores = False
                MensajeError += vbCrLf + "El nombre indicado ya está en uso, favor de cambiarlo."
                txtNombre.BackColor = Color.FromArgb(250, 150, 150)
            End If
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesAlta, PermisosN.Secciones.Empenios) = False And btnGuardar.Text = "Guardar" Then
            MensajeError += " No tiene permiso para realizar esta operación."
            NoErrores = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesCambios, PermisosN.Secciones.Empenios) = False And btnGuardar.Text <> "Guardar" Then
            MensajeError += " No tiene permiso para realizar esta operación."
            NoErrores = False
        End If

        If NoErrores = True Then

            If btnGuardar.Text = "Guardar" Then
                datos.Guardar(txtNombre.Text)
                ID = datos.idProximo - 1
                btnGuardar.Text = "Modificar"
                btnEliminar.Enabled = True
                nuevo()
                PopUp("Guardado", 90)
            Else


                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    'modificar
                    datos.Modificar(ID, txtNombre.Text)
                    nuevo()
                    PopUp("Modificado", 90)

                End If
            End If
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub

    Private Sub btnEliminar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesBaja, PermisosN.Secciones.Empenios) = False Then
                MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
           
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                datos.Eliminar(ID)
                PopUp("Eliminado", 90)
                nuevo()

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevo_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub

    Private Sub txtNombre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub
End Class