Public Class frmDistribuidores
    Dim ID As Integer
    Dim Lic As Licencia
    Private Sub frmDistribuidores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Lic = New Licencia("servidor", True)
        nuevo()
        consultar()
    End Sub
    Private Sub nuevo()
        consultar()
        txtNombre.Text = ""
        txtContacto.Text = ""
        txtTelefono.Text = ""
        txtCelular.Text = ""
        txtEmail.Text = ""
        txtDireccion.Text = ""
        txtComentario.Text = ""
        btnGuardar.Text = "Guardar"
        ID = -1
        btnEliminar.Enabled = False
        txtNombre.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            ' Dim CD As New dbDistribuidores(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""

            If txtNombre.Text = "" Then
                MsgError += " Es necesario indicar un Nombre para el distribuidor."
                HayError = True
            End If
            If txtTelefono.Text = "" Then
                MsgError += " Es necesario indicar un Teléfono para el distribuidor."
                HayError = True
            End If

            If HayError = False Then
                If btnGuardar.Text = "Guardar" Then

                    Lic.Guardar(txtNombre.Text, txtContacto.Text, txtTelefono.Text, txtCelular.Text, txtEmail.Text, txtDireccion.Text, txtComentario.Text)
                    PopUp("Guardado", 90)
                    nuevo()

                Else

                    Lic.Modificar(ID, txtNombre.Text, txtContacto.Text, txtTelefono.Text, txtCelular.Text, txtEmail.Text, txtDireccion.Text, txtComentario.Text)
                    PopUp("Modificado", 90)
                    nuevo()

                End If

            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                ' Dim CD As New dbDistribuidores(MySqlcon)
                Lic.Eliminar(ID)
                PopUp("Eliminado", 90)
                nuevo()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub consultar()
        'Dim CD As New dbDistribuidores(MySqlcon)
        dgvDistribuidores.DataSource = Lic.Consulta(txtConsulta.Text)
        dgvDistribuidores.Columns(0).Visible = False
        dgvDistribuidores.Columns(6).Visible = False
        dgvDistribuidores.Columns(7).Visible = False
        dgvDistribuidores.Columns(4).Visible = False
        dgvDistribuidores.Columns(5).Visible = False

        dgvDistribuidores.Columns(1).HeaderText = "Nombre"
        dgvDistribuidores.Columns(2).HeaderText = "Contacto"
        dgvDistribuidores.Columns(3).HeaderText = "Teléfono"
        dgvDistribuidores.Columns(4).HeaderText = "Celular"
        dgvDistribuidores.Columns(5).HeaderText = "Email"
        dgvDistribuidores.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
       
    End Sub

    Private Sub txtConsulta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConsulta.TextChanged
        consultar()
    End Sub

    Private Sub dgvDistribuidores_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDistribuidores.CellClick
        llenaDatos()
    End Sub
    Private Sub llenaDatos()
        Try

            ID = dgvDistribuidores.Item(0, dgvDistribuidores.CurrentCell.RowIndex).Value
            'Dim CD As New dbDistribuidores(ID, MySqlcon)
            Lic.LlenaDatos(ID)
            txtNombre.Text = Lic.nombre
            txtContacto.Text = Lic.contacto
            txtTelefono.Text = Lic.telefono
            txtCelular.Text = Lic.celular
            txtEmail.Text = Lic.email
            txtDireccion.Text = Lic.direccion
            txtComentario.Text = Lic.Comentario
            btnGuardar.Text = "Modificar"
            btnEliminar.Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub frmDistribuidores_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Lic.CierraConexion()
    End Sub
End Class