Public Class frmServiciosAltaEstado
    Dim P As New dbServicioAltaEstado(MySqlcon)
    Dim tabla As New DataTable
    Dim ID As Integer

    Private Sub frmServiciosAltaEstado_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        nuevo()
    End Sub
    Private Sub nuevo()
        txtID.Text = P.buscaCodigo()
        txtNombre.Text = ""
        btnGuardar.Text = "Guardar"
        ckEsFinal.Checked = False
        consulta()
        txtNombre.Focus()
    End Sub
    Private Sub consulta()
        tabla = P.Consulta(txtBusqueda.Text)
        DataGridView1.DataSource = tabla
        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).Visible = False

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        Dim pFinal As String
        If txtNombre.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar un nombre para la clasificación."
            txtNombre.BackColor = Color.FromArgb(250, 150, 150)
            'Else
            'If datos.esRepetida(txtNombre.Text) And btnGuardar.Text = "Guardar" Then
            '    NoErrores = False
            '    MensajeError += vbCrLf + "El nombre indicado ya está en uso, favor de cambiarlo."
            '    txtNombre.BackColor = Color.FromArgb(250, 150, 150)
            'End If
        End If
        If ckEsFinal.Checked = True Then
            pFinal = "1"
        Else
            pFinal = "0"
        End If

        If NoErrores = True Then

            If btnGuardar.Text = "Guardar" Then
                ID = P.Guardar(txtNombre.Text, pFinal)
                btnGuardar.Text = "Modificar"
                btnEliminar.Enabled = True
                nuevo()
                PopUp("Guardado", 90)
            Else


                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    'modificar
                    P.Modificar(ID, txtNombre.Text, pFinal)
                    nuevo()
                    PopUp("Modificado", 90)

                End If
            End If
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try

            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                P.Eliminar(ID)
                PopUp("Eliminado", 90)
                nuevo()

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub

    Private Sub txtBusqueda_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBusqueda.TextChanged
        consulta()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            ID = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            P.LlenaDatos(ID)
            txtID.Text = ID.ToString
            btnGuardar.Text = "Modificar"
            txtNombre.Text = P.nombre
            ckEsFinal.Checked = P.final
            btnEliminar.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class