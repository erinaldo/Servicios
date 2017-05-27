Public Class frmCantidades
    Dim idsTipos As New elemento
    Dim idTipo As Integer = -1
    Private Sub frmCantidades_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Nuevo()
    End Sub
    Private Sub Nuevo()
        Try
            ComboBox1.Text = ""
            Button1.Text = "Guardar"
            Button2.Enabled = False
            TextBox1.Text = ""
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            LlenaCombos("tbltiposcantidades", ComboBox1, "nombre", "nombret", "idtipocantidad", idsTipos, "idtipocantidad>1")
            idTipo = -1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim UsaBascula As Byte = 0
            If CheckBox1.Checked Then UsaBascula = 1
            Dim ParaRemision As Byte = 0
            If CheckBox2.Checked Then ParaRemision = 1
            Dim TC As New dbTiposCantidades(MySqlcon)
            If ComboBox1.Text <> "" Then
                If Button1.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.MedidasAlta, PermisosN.Secciones.Catalagos) = True Then
                        TC.Guardar(ComboBox1.Text.Trim, TextBox1.Text.Trim, UsaBascula, ParaRemision)
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.MedidasCambio, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            TC.Modificar(idTipo, ComboBox1.Text.Trim, TextBox1.Text.Trim, UsaBascula, ParaRemision)
                            PopUp("Modificado", 90)
                            Nuevo()
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
                ComboBox1.Focus()
            Else
                MsgBox("Debe de indicar un nombre a la medida.", MsgBoxStyle.Exclamation, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim TC As New dbTiposCantidades(MySqlcon)
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.MedidasBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Eliminar(idTipo)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    ComboBox1.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta medida debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            idTipo = idsTipos.Valor(ComboBox1.SelectedIndex)
            Dim TC As New dbTiposCantidades(idTipo, MySqlcon)
            TextBox1.Text = TC.Abreviatura
            If TC.UsaBascula = 1 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            If TC.ParaRemision = 1 Then
                CheckBox2.Checked = True
            Else
                CheckBox2.Checked = False
            End If
            Button1.Text = "Modificar"
            Button2.Enabled = True
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If Char.IsLower(e.KeyChar) Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub
End Class