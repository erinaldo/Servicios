Public Class frmFormasdePagoRemisiones

    Dim IdConcepto As Integer
    Dim IdsConceptos As New elemento
    Dim CodigoAnt As String
    Dim Consultaon As Boolean
    Private Sub frmInvnetarioConceptos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        'ComboBox1.Items.Add("Seleccionar tipo")
        Consultaon = False
        ComboBox1.Items.Add("Contado - Efectivo")
        ComboBox1.Items.Add("Contado - No Efectivo")
        ComboBox1.Items.Add("Crédito")
        ComboBox1.SelectedIndex = 0
        Consultaon = True
        NuevoConcepto()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub NuevoConcepto()
        Try
            Button5.Text = "Guardar"
            Button6.Enabled = False
            LlenaCombos("tblformasdepagoremisiones", ComboBox2, "nombre", "nombrec", "idforma", IdsConceptos, "tipo=" + CStr(ComboBox1.SelectedIndex + 1))
            Button5.Enabled = True
            Button7.Enabled = True
            'ComboBox1.SelectedIndex = 0
            TextBox1.Text = ""
            CodigoAnt = ""
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim SC As New dbFormasdePagoRemisiones(MySqlcon)
            Dim MsgError As String = ""
            Dim CodigoRepetido As Boolean
            ComboBox2.Text = ComboBox2.Text.ToUpper()
            If TextBox1.Text = "" Then MsgError = "Debe indicar un código."
            If TextBox1.Text.Contains("*") Then MsgError += "El código contiene un caracter inválido."
            If TextBox1.Text.ToUpper <> CodigoAnt Then
                CodigoRepetido = SC.ChecaCodigoRepetida(TextBox1.Text)
                If CodigoRepetido Then MsgError += " Código repetido."
            End If
            If ComboBox2.Text = "" Then MsgError += "Debe indicar un nombre al método de pago."
            If MsgError = "" Then
                TextBox1.Text = TextBox1.Text.ToUpper
                If Button5.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemAlta, PermisosN.Secciones.Catalagos) = True Then
                        SC.Guardar(ComboBox2.Text, ComboBox1.SelectedIndex + 1, TextBox1.Text)
                        PopUp("Guardado", 90)
                        NuevoConcepto()
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemCambio, PermisosN.Secciones.Catalagos) = True Then
                        If IdConcepto <> 1 And IdConcepto <> 3 Then
                            If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                                SC.Modificar(IdConcepto, ComboBox2.Text, ComboBox1.SelectedIndex + 1, TextBox1.Text)
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
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea Eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If IdConcepto <> 1 And IdConcepto <> 3 Then
                        Dim SC As New dbFormasdePagoRemisiones(MySqlcon)
                        SC.Eliminar(IdConcepto)
                        PopUp("Eliminado", 90)
                        NuevoConcepto()
                    Else
                        MsgBox("No se puede eliminar es método de pago.", MsgBoxStyle.Critical, GlobalNombreApp)
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
                Dim C2 As New dbFormasdePagoRemisiones(IdConcepto, MySqlcon)
                Button6.Enabled = True
                Button5.Text = "Modificar"
                'ComboBox1.SelectedIndex = C2.Tipo
                TextBox1.Text = C2.Codigo
                CodigoAnt = C2.Codigo
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Consultaon Then NuevoConcepto()
    End Sub
End Class