Public Class frmEmpresas
    Dim Em As New dbEmpresas
    Dim IdEmpresa As Integer
    Dim NombreAnt As String
    Private Sub frmEmpresas_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Em.MySqlconE.Close()
    End Sub

    Private Sub frmEmpresas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Em.IniciarMySQLE(My.Settings.BasedeDatos, My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
        Consulta()
    End Sub
    Private Sub Nuevo()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        NombreAnt = ""
        TextBox6.Focus()
        CheckBox1.Checked = False
        Button1.Text = "Guardar"
        Button2.Enabled = False
        Consulta()
    End Sub
    Private Sub Guardar()
        Try
            Dim IdI As Integer
            Dim EsDefault As Byte
            If CheckBox1.Checked Then
                EsDefault = 1
            Else
                EsDefault = 0
            End If
            Dim HayError As String = ""
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
                HayError = "Debe llenar todos los datos para agregar una empresa."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.EmpresasAlta, PermisosN.Secciones.Catalagos2) = False Then
                HayError += " No tiene permiso para realizar esta operación."
            End If
            If Em.ChecaEmpresaRepetida(TextBox6.Text) Then HayError += " Ya existe una empresa con ese nombre."
            If HayError = "" Then
                IdI = Em.Guardar(TextBox6.Text, TextBox1.Text, TextBox2.Text, TextBox4.Text, TextBox5.Text, EsDefault)
                If EsDefault = 1 Then
                    My.Settings.empresadefault = IdI
                    My.Settings.Save()
                End If
                PopUp("Empresa Agregada", 90)
                Nuevo()
            Else
                MsgBox(HayError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Modificar()
        Try
            Dim EsDefault As Byte
            If CheckBox1.Checked Then
                EsDefault = 1
            Else
                EsDefault = 0
            End If
            Dim HayError As String = ""
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
                HayError = "Debe llenar todos los datos para agregar una empresa."
            End If
            If TextBox6.Text <> NombreAnt.ToUpper Then
                If Em.ChecaEmpresaRepetida(TextBox6.Text) Then HayError += " Ya existe una empresa con ese nombre."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.EmpresasCambio, PermisosN.Secciones.Catalagos2) = False Then
                HayError += " No tiene permiso para realizar esta operación."
            End If
            If HayError = "" Then
                If MsgBox("¿Guardar cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Em.Modificar(IdEmpresa, TextBox6.Text, TextBox1.Text, TextBox2.Text, TextBox4.Text, TextBox5.Text, EsDefault)
                    If EsDefault = 1 Then
                        My.Settings.empresadefault = IdEmpresa
                        My.Settings.Save()
                    End If
                    If EsDefault = 0 And My.Settings.empresadefault = IdEmpresa Then
                        My.Settings.empresadefault = 0
                        My.Settings.Save()
                    End If
                    PopUp("Empresa Modificada", 90)
                    Nuevo()
                End If
            Else
                MsgBox(HayError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatos()
        Em.LlenaDatos(IdEmpresa)
        TextBox6.Text = Em.NombreEmpresa
        NombreAnt = Em.NombreEmpresa
        TextBox5.Text = Em.Servidor
        TextBox1.Text = Em.NombreDB
        TextBox2.Text = Em.Usuario
        TextBox4.Text = Em.PasswordS
        If IdEmpresa = My.Settings.empresadefault Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        Button1.Text = "Modificar"
        Button2.Enabled = True
    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            ' If ConsultaOn Then
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            DataGridView1.DataSource = Em.Consulta(TextBox3.Text, My.Settings.empresadefault)
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Nombre Empresa"
            DataGridView1.Columns(2).HeaderText = "Default"
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            ' End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        IdEmpresa = DataGridView1.Item(0, e.RowIndex).Value
        LlenaDatos()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Consulta()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Guardar" Then
            Guardar()
        Else
            Modificar()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.EmpresasBaja, PermisosN.Secciones.Catalagos2) = True Then
            If MsgBox("¿Eliminar esta empresa?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Em.Eliminar(IdEmpresa)
                If My.Settings.empresadefault = IdEmpresa Then
                    My.Settings.empresadefault = 0
                    My.Settings.Save()
                End If
                PopUp("Empresa Eliminada", 90)
                Nuevo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub
End Class