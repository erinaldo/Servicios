Public Class frmServiciosClasificaciones
    Dim IdClasificacion As Integer
    Dim IdClasificacion2 As Integer
    Dim IdsClas1 As New elemento
    Dim IdsClas2 As New elemento
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub frmServiciosClasificaciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        NuevoClasificacion()
    End Sub
    Private Sub NuevoClasificacion()
        Try
            Button1.Text = "Guardar"
            Button2.Enabled = False
            LlenaCombos("tblserviciosclasificaciones", ComboBox1, "nombre", "nombrec", "idclasificacion", IdsClas1)
            ComboBox2.Items.Clear()
            ComboBox2.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            TextBox1.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub NuevoClasificacion2()
        Try
            Button5.Text = "Guardar"
            Button6.Enabled = False
            LlenaCombos("tblserviciosclasificaciones2", ComboBox2, "nombre", "nombrec", "idclasificacion2", IdsClas2, "idclasificacion=" + IdClasificacion.ToString)
            Button5.Enabled = True
            Button7.Enabled = True
            TextBox1.Text = "0"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            If ComboBox1.SelectedIndex >= 0 Then
                IdClasificacion = IdsClas1.Valor(ComboBox1.SelectedIndex)
                ComboBox2.Enabled = True
                LlenaCombos("tblserviciosclasificaciones2", ComboBox2, "nombre", "nombrec", "idclasificacion2", IdsClas2, "idclasificacion=" + IdClasificacion.ToString)
                Button5.Enabled = True
                Button7.Enabled = True
                Button1.Text = "Modificar"
                Button2.Enabled = True
                TextBox1.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            ComboBox1.BackColor = Color.White
            If ComboBox1.Text <> "" Then
                Dim SC As New dbServiciosClasificaciones(MySqlcon)
                If Button1.Text = "Guardar" Then
                    If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClasGuardar, PermisosN.Secciones.Servicios) = True) Then
                        SC.Guardar(ComboBox1.Text)
                        PopUp("Guardado", 90)
                        NuevoClasificacion()
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClasModificar, PermisosN.Secciones.Servicios) = True) Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            SC.Modificar(IdClasificacion, ComboBox1.Text)
                            PopUp("Modificado", 90)
                            NuevoClasificacion()
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If

                Else
                    MsgBox("Debe indicar un nombre a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    ComboBox1.BackColor = Color.FromArgb(250, 150, 150)
                End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClasEliminar, PermisosN.Secciones.Servicios) = True) Then
                If MsgBox("¿Desea eliminar el registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim SC As New dbServiciosClasificaciones(MySqlcon)
                    SC.Eliminar(IdClasificacion)
                    PopUp("Eliminado", 90)
                    NuevoClasificacion()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        NuevoClasificacion()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            ComboBox2.BackColor = Color.White
            TextBox1.BackColor = Color.White
            If ComboBox2.Text <> "" Then
                If IsNumeric(TextBox1.Text) Then
                    Dim SC As New dbServiciosClasificaciones2(MySqlcon)
                    If Button5.Text = "Guardar" Then
                        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClasGuardar, PermisosN.Secciones.Servicios) = True) Then
                            SC.Guardar(ComboBox2.Text, IdClasificacion, CDbl(TextBox1.Text))
                            PopUp("Guardado", 90)
                            NuevoClasificacion2()
                        Else
                            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClasModificar, PermisosN.Secciones.Servicios) = True) Then
                            If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                SC.Modificar(IdClasificacion2, ComboBox2.Text, CDbl(TextBox1.Text))
                                PopUp("Modificado", 90)
                                NuevoClasificacion2()
                            End If
                        Else
                            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    End If
                    Else
                        MsgBox("El precio debe ser un valor numérico.", MsgBoxStyle.Critical, GlobalNombreApp)
                        TextBox1.BackColor = Color.FromArgb(250, 150, 150)
                    End If
            Else
                MsgBox("Debe indicar un nombre a la sub clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
                ComboBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClasEliminar, PermisosN.Secciones.Servicios) = True) Then
                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim SC As New dbServiciosClasificaciones2(MySqlcon)
                    SC.Eliminar(IdClasificacion2)
                    PopUp("Eliminado", 90)
                    NuevoClasificacion2()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        NuevoClasificacion2()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            If ComboBox1.SelectedIndex >= 0 Then
                IdClasificacion2 = IdsClas2.Valor(ComboBox2.SelectedIndex)
                Dim C2 As New dbServiciosClasificaciones2(IdClasificacion2, MySqlcon)
                Button6.Enabled = True
                Button5.Text = "Modificar"
                TextBox1.Text = C2.Precio.ToString
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If Char.IsLower(e.KeyChar) Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If Char.IsLower(e.KeyChar) Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And TextBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub
End Class