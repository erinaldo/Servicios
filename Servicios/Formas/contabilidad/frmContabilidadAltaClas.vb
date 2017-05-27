Public Class frmContabilidadAltaClas
    Dim clas As New dbContabilidadClasificacionesPolizas(MySqlcon)
    Dim idClas As Integer = -1
    Private Sub frmContabilidadAltaClas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Nuevo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        buscar()
        txtNombre.Text = ""
        btnGuardar.Text = "Guardar"
        txtNombre.BackColor = Color.White
        btnEliminar.Enabled = False
        txtNombre.Focus()
    End Sub
    Private Sub buscar()
        dgvClasificaciones.DataSource = clas.buscar(txtBusqueda.Text)
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Dim errores As String = ""
            Dim Noerrores As Boolean = True

            If txtNombre.Text = "" Then
                errores += "Debe indicar el nombre de la clasificación." + vbCrLf
                Noerrores = False
                txtNombre.BackColor = Color.Coral
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasAlta, PermisosN.Secciones.Contabilidad) = False And btnGuardar.Text = "Guardar" Then
                errores += "No tiene permiso para realizar esta operación"
                Noerrores = False
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasCambios, PermisosN.Secciones.Contabilidad) = False And btnGuardar.Text <> "Guardar" Then
                errores += "No tiene permiso para realizar esta operación"
                Noerrores = False
            End If
            If Noerrores Then
                If btnGuardar.Text = "Guardar" Then
                    'Guardar
                    clas.guardar(txtNombre.Text)
                    Nuevo()
                    PopUp("Guardado", 90)
                Else
                    'Modificacr
                    If MsgBox("¿Desea modificar esta clasificación?", MsgBoxStyle.OkCancel, GlobalNombreApp) = MsgBoxResult.Ok Then
                        clas.modificar(idClas, txtNombre.Text)
                        Nuevo()
                        PopUp("Modificado", 90)
                    End If
                   
                End If


            Else
                MsgBox(errores, MsgBoxStyle.OkOnly, GlobalNombreApp)
                txtNombre.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
    End Sub

    Private Sub dgvClasificaciones_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClasificaciones.CellClick
        Try
            idClas = dgvClasificaciones.Item(0, dgvClasificaciones.CurrentCell.RowIndex).Value
            txtNombre.Text = dgvClasificaciones.Item(1, dgvClasificaciones.CurrentCell.RowIndex).Value
            btnGuardar.Text = "Modificar"
            btnEliminar.Enabled = True
            txtNombre.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasBaja, PermisosN.Secciones.Contabilidad) = False Then
                MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If idClas = 1 Then
                MsgBox("Esta clasificación no se puede eliminar.", MsgBoxStyle.OkOnly, GlobalNombreApp)
            Else
                If MsgBox("¿Desea eliminar esta clasificación?", MsgBoxStyle.OkCancel, GlobalNombreApp) = MsgBoxResult.Ok Then
                    clas.Eliminar(idClas)
                    Nuevo()
                    PopUp("Eliminado", 90)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Nuevo()
    End Sub

    Private Sub txtBusqueda_TextChanged(sender As Object, e As EventArgs) Handles txtBusqueda.TextChanged
        buscar()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dispose()
    End Sub
End Class