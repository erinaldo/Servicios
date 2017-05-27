Public Class frmFormasDePago
    Dim IdConcepto As Integer
    Dim IdsConceptos As New elemento
    Dim IdsClaves As New elemento
    Dim ConsultaOn As Boolean
    Private Sub frmInvnetarioConceptos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblformasdepagosat", ComboBox3, "concat(if(clave<1000,lpad(convert(clave using utf8),2,'0'),''),' ',nombre)", "nombrec", "clave", IdsClaves)
        ConsultaOn = False
        ComboBox1.Items.Add("Crédito")
        ComboBox1.Items.Add("Contado")
        ComboBox1.Items.Add("Parcialidad")
        ComboBox1.SelectedIndex = 0
        NuevoConcepto()
        ConsultaOn = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub NuevoConcepto()
        Try
            Button5.Text = "Guardar"
            Button6.Enabled = False
            Button5.Enabled = True
            Button7.Enabled = True
            ComboBox3.SelectedIndex = 0
            CheckBox1.Checked = False
            LlenaCombos("tblformasdepago", ComboBox2, "nombre", "nombrec", "idforma", IdsConceptos, "tipo=" + ComboBox1.SelectedIndex.ToString)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            ComboBox2.Text = ComboBox2.Text.ToUpper()
            Dim pb As Byte = 0
            If CheckBox1.Checked Then pb = 1
            If ComboBox2.Text <> "" Then
                Dim SC As New dbFormasdePago(MySqlcon)
                If Button5.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoAlta, PermisosN.Secciones.Catalagos) = True Then
                        SC.Guardar(ComboBox2.Text, ComboBox1.SelectedIndex, IdsClaves.Valor(ComboBox3.SelectedIndex), pb)
                        PopUp("Guardado", 90)
                        NuevoConcepto()
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoCambio, PermisosN.Secciones.Catalagos) = True Then
                        If IdConcepto <> 1 And IdConcepto <> 2 And IdConcepto <> 98 And IdConcepto <> 99 Then
                            If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                SC.Modificar(IdConcepto, ComboBox2.Text, ComboBox1.SelectedIndex, IdsClaves.Valor(ComboBox3.SelectedIndex), pb)
                                PopUp("Modificado", 90)
                                NuevoConcepto()
                            End If
                        Else
                            MsgBox("No se puede modificar ese método de pago.", MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
            Else
                MsgBox("Debe indicar un nombre al método de pago.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea Eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If IdConcepto <> 1 And IdConcepto <> 2 And IdConcepto <> 98 And IdConcepto <> 99 Then
                        Dim SC As New dbFormasdePago(MySqlcon)
                        SC.Eliminar(IdConcepto)
                        PopUp("Eliminado", 90)
                        NuevoConcepto()
                    Else
                        MsgBox("No se puede eliminar esa forma de pago.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        NuevoConcepto()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            If ComboBox1.SelectedIndex >= 0 Then
                IdConcepto = IdsConceptos.Valor(ComboBox2.SelectedIndex)
                Dim C2 As New dbFormasdePago(IdConcepto, MySqlcon)
                Button6.Enabled = True
                Button5.Text = "Modificar"
                'ComboBox1.SelectedIndex = C2.Tipo
                ComboBox3.SelectedIndex = IdsClaves.Busca(C2.clavesat)
                If C2.Parabancos = 1 Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ConsultaOn Then NuevoConcepto()
        If ComboBox1.SelectedIndex = 1 Then
            CheckBox1.Visible = True
        Else
            CheckBox1.Visible = False
        End If
    End Sub
End Class