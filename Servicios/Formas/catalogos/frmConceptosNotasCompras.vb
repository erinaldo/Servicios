Public Class frmConceptosNotasCompras
    Dim idsTipos As New elemento
    Dim idTipo As Integer = -1

    Private Sub frmCantidades_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox2.Items.Add("Notas de Cargo")
        ComboBox2.Items.Add("Notas de Crédito")
        ComboBox2.Items.Add("Pagos")
        ComboBox2.SelectedIndex = 0
        'Nuevo()
    End Sub
    Private Sub Nuevo()
        Try
            Button1.Text = "Guardar"
            Button2.Enabled = False
            LlenaCombos("tblconceptosnotascompras", ComboBox1, "nombre", "nombret", "idconceptonotacompra", idsTipos, "tipo=" + ComboBox2.SelectedIndex.ToString)
            ComboBox1.Text = ""
            ComboBox1.Focus()
            idTipo = -1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim TC As New dbConceptosNotasCompras(MySqlcon)
            If ComboBox1.Text <> "" Then
                If Button1.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasAlta, PermisosN.Secciones.Catalagos) = True Then
                        TC.Guardar(ComboBox1.Text, ComboBox2.SelectedIndex)
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasCambio, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            TC.Modificar(idTipo, ComboBox1.Text, ComboBox2.SelectedIndex)
                            PopUp("Modificado", 90)
                            Nuevo()
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
                ComboBox1.Focus()
            Else
                MsgBox("Debe de indicar un nombre a al concepto.", MsgBoxStyle.Exclamation, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If idTipo = 4 Or idTipo = 5 Or idTipo = 90 Then
                MsgBox("No se puede eliminar ese concepto.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            Dim TC As New dbConceptosNotasCompras(MySqlcon)
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasBaja, PermisosN.Secciones.Catalagos) = True Then
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
                MsgBox("No se puede eliminar este concepto debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            'Dim TC As New dbConceptosNotasCompras(idTipo, MySqlcon)
            'ComboBox2.SelectedIndex = TC.Tipo
            Button1.Text = "Modificar"
            Button2.Enabled = True
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        LlenaCombos("tblconceptosnotascompras", ComboBox1, "nombre", "nombret", "idconceptonotacompra", idsTipos, "tipo=" + ComboBox2.SelectedIndex.ToString)
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If Char.IsLower(e.KeyChar) Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If

    End Sub
End Class