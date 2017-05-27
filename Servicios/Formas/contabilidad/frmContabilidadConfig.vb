Public Class frmContabilidadConfig
    Dim YearAnterior As String
    Dim p As New dbContabilidadPolizas(MySqlcon)
    Private Sub frmContabilidadConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        p.llenaDatosConfig()
        txtNivel1.Text = p.NNiv1.ToString
        txtNivel2.Text = p.NNiv2.ToString
        txtNivel3.Text = p.NNiv3.ToString
        txtNivel4.Text = p.NNiv4.ToString
        txtNivel5.Text = p.NNiv5.ToString
        cmbAnios.Text = p.anio
        YearAnterior = p.anio
        txtActivoCirculante1.MaxLength = p.NNiv1
        txtActivoCirculante2.MaxLength = p.NNiv1
        txtActivoFijo1.MaxLength = p.NNiv1
        txtActivoFijo2.MaxLength = p.NNiv1
        txtActivoDif1.MaxLength = p.NNiv1
        txtActivoDif2.MaxLength = p.NNiv1
        txtActivoOtros1.MaxLength = p.NNiv1
        txtActivoOtros2.MaxLength = p.NNiv1
        txtPasivoCirculante1.MaxLength = p.NNiv1
        txtPasivoCirculante2.MaxLength = p.NNiv1
        txtPasivoFijo1.MaxLength = p.NNiv1
        txtPasivoFijo2.MaxLength = p.NNiv1
        txtPasivoDiferido1.MaxLength = p.NNiv1
        txtPasivoDiferido2.MaxLength = p.NNiv1
        txtCapital1.MaxLength = p.NNiv1
        txtRecultados.MaxLength = p.NNiv1
        txtOrdenAcre1.MaxLength = p.NNiv1
        txtOrdenAcre2.MaxLength = p.NNiv1
        txtOrdenD1.MaxLength = p.NNiv1
        txtOrdenD2.MaxLength = p.NNiv1
        txtIngresos1.MaxLength = p.NNiv1
        txtEgresos1.MaxLength = p.NNiv1
        txtIngresos2.MaxLength = p.NNiv1
        txtEgresos2.MaxLength = p.NNiv1
        TextBox3.Text = p.RutaXMLIngresos
        txtActivoCirculante1.Text = p.activoC1.PadLeft(p.NNiv1, "0")
        txtActivoCirculante2.Text = p.activoC2.PadLeft(p.NNiv1, "0")
        txtActivoFijo1.Text = p.activoF1.PadLeft(p.NNiv1, "0")
        txtActivoFijo2.Text = p.activoF2.PadLeft(p.NNiv1, "0")
        txtActivoDif1.Text = p.activoD1.PadLeft(p.NNiv1, "0")
        txtActivoDif2.Text = p.activoD2.PadLeft(p.NNiv1, "0")
        txtActivoOtros1.Text = p.activoO1.PadLeft(p.NNiv1, "0")
        txtActivoOtros2.Text = p.activoO2.PadLeft(p.NNiv1, "0")
        txtPasivoCirculante1.Text = p.pasivoC1.PadLeft(p.NNiv1, "0")
        txtPasivoCirculante2.Text = p.pasivoC2.PadLeft(p.NNiv1, "0")
        txtPasivoFijo1.Text = p.pasivoF1.PadLeft(p.NNiv1, "0")
        txtPasivoFijo2.Text = p.pasivoF2.PadLeft(p.NNiv1, "0")
        txtPasivoDiferido1.Text = p.pasivoD1.PadLeft(p.NNiv1, "0")
        txtPasivoDiferido2.Text = p.pasivoD2.PadLeft(p.NNiv1, "0")
        txtCapital1.Text = p.capital1.PadLeft(p.NNiv1, "0")
        txtcapital2.Text = p.capital2.PadLeft(p.NNiv1, "0")
        txtRecultados.Text = p.resultados.PadLeft(p.NNiv1, "0")
        txtOrdenAcre1.Text = p.ordenA1.PadLeft(p.NNiv1, "0")
        txtOrdenAcre2.Text = p.ordenA2.PadLeft(p.NNiv1, "0")
        txtOrdenD1.Text = p.ordenD1.PadLeft(p.NNiv1, "0")
        txtOrdenD2.Text = p.ordenD2.PadLeft(p.NNiv1, "0")
        txtIngresos1.Text = p.ingresos1.PadLeft(p.NNiv1, "0")
        txtEgresos1.Text = p.egresos1.PadLeft(p.NNiv1, "0")
        txtIngresos2.Text = p.ingresos2.PadLeft(p.NNiv1, "0")
        txtEgresos2.Text = p.egresos2.PadLeft(p.NNiv1, "0")
        TextBox2.Text = p.capital3.PadLeft(p.NNiv1, "0")
        TextBox1.Text = p.capital4.PadLeft(p.NNiv1, "0")
        txtRutaUUID.Text = p.rutaUUID
        If p.ordenVisible = 1 Then
            ckOrden.Checked = True
        Else
            ckOrden.Checked = False
        End If
        If p.PreguntarCuadrar = 1 Then
            CheckBox1.Checked = True
        End If
        If p.ActivarFechaTrabajo = 1 Then
            CheckBox2.Checked = True
        End If
        If p.SoloIvaEnDiot = 1 Then
            CheckBox3.Checked = True
        End If
        DateTimePicker1.Value = CDate(p.FechaTRabajo)
        GroupBox1.Focus()
        txtNivel1.Focus()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ConfiguracionModificar, PermisosN.Secciones.Contabilidad) = False Then
                MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If cmbAnios.Text <> DateTimePicker1.Value.Year Then
                MsgBox("El año de la fecha de trabajo es diferente al año de ejercicio de trabajo.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim ck As Integer
                If ckOrden.Checked = True Then
                    ck = 1
                Else
                    ck = 0
                End If
                Dim GC As Byte
                If CheckBox1.Checked Then
                    GC = 1
                Else
                    GC = 0
                End If
                Dim AF As Byte
                If CheckBox2.Checked Then
                    AF = 1
                Else
                    AF = 0
                End If
                Dim soloiva As Byte
                If CheckBox3.Checked Then
                    soloiva = 1
                Else
                    soloiva = 0
                End If
                p.modificarConf(txtNivel1.Text, txtNivel2.Text, txtNivel3.Text, txtNivel4.Text, txtNivel5.Text, quitarCeros(txtActivoCirculante1.Text), quitarCeros(txtActivoCirculante2.Text), quitarCeros(txtActivoFijo1.Text), quitarCeros(txtActivoFijo2.Text), quitarCeros(txtActivoDif1.Text), quitarCeros(txtActivoDif2.Text), quitarCeros(txtActivoOtros1.Text), quitarCeros(txtActivoOtros2.Text), quitarCeros(txtPasivoCirculante1.Text), quitarCeros(txtPasivoCirculante2.Text), quitarCeros(txtPasivoFijo1.Text), quitarCeros(txtPasivoFijo2.Text), quitarCeros(txtPasivoDiferido1.Text), quitarCeros(txtPasivoDiferido2.Text), quitarCeros(txtCapital1.Text), quitarCeros(txtcapital2.Text), quitarCeros(txtRecultados.Text), quitarCeros(txtOrdenAcre1.Text), quitarCeros(txtOrdenAcre2.Text), quitarCeros(txtOrdenD1.Text), quitarCeros(txtOrdenD2.Text), ck, quitarCeros(txtIngresos1.Text), quitarCeros(txtIngresos2.Text), quitarCeros(txtEgresos1.Text), quitarCeros(txtEgresos2.Text), txtRutaUUID.Text, GC, Format(DateTimePicker1.Value, "yyyy/MM/dd"), AF, quitarCeros(TextBox2.Text), quitarCeros(TextBox1.Text), TextBox3.Text, soloiva)
                If YearAnterior <> cmbAnios.Text Then
                    If p.modificarPeriodo(cmbAnios.Text, txtCodigoPeriodo.Text) = False Then
                        MsgBox("Contraseña incorrecta. El ejercicio no pudo ser modificado.", MsgBoxStyle.Critical, GlobalNombreApp)
                    Else
                        YearAnterior = cmbAnios.Text
                    End If
                End If
                PopUp("Modificado", 60)
                p.llenaDatosConfig()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Function quitarCeros(ByVal pTexto As String)
        Dim texto As String = ""
        If pTexto.Chars(0) = "0" Then
            For i As Integer = 0 To pTexto.Length.ToString - 1
                If pTexto.Chars(i) <> "0" Then
                    For j As Integer = i To pTexto.Length - 1
                        texto += pTexto.Chars(j)
                    Next
                    If texto = "" Then
                        texto = "0"
                    End If
                    Return texto
                End If
            Next
        Else
            texto = pTexto
        End If
        If texto = "" Then
            texto = "0"
        End If

        Return texto
    End Function
    Private Sub txtNivel1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNivel1.KeyDown

    End Sub

    Private Sub txtNivel1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNivel1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNivel2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNivel2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNivel3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNivel3.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNivel4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNivel4.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNivel5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNivel5.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtOrdenAcre1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdenAcre1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtOrdenAcre2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdenAcre2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtOrdenD1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdenD1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtOrdenD2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdenD2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPasivoCirculante1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasivoCirculante1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPasivoCirculante2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasivoCirculante2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPasivoDiferido1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasivoDiferido1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPasivoDiferido2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasivoDiferido2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPasivoFijo1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasivoFijo1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtRecultados_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecultados.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtIngresos1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIngresos1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtIngresos2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIngresos2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtEgresos1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEgresos1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtEgresos2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEgresos2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoCirculante1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoCirculante1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoCirculante1_Leave(sender As Object, e As EventArgs) Handles txtActivoCirculante1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtActivoCirculante2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoCirculante2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoCirculante2_Leave(sender As Object, e As EventArgs) Handles txtActivoCirculante2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtActivoDif1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoDif1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoDif1_Leave(sender As Object, e As EventArgs) Handles txtActivoDif1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtActivoDif2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoDif2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoDif2_Leave(sender As Object, e As EventArgs) Handles txtActivoDif2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtActivoFijo1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoFijo1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoFijo1_Leave(sender As Object, e As EventArgs) Handles txtActivoFijo1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtActivoFijo2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoFijo2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoFijo2_Leave(sender As Object, e As EventArgs) Handles txtActivoFijo2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtActivoOtros1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoOtros1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoOtros1_Leave(sender As Object, e As EventArgs) Handles txtActivoOtros1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtActivoOtros2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActivoOtros2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtActivoOtros2_Leave(sender As Object, e As EventArgs) Handles txtActivoOtros2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtCapital1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCapital1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCapital1_Leave(sender As Object, e As EventArgs) Handles txtCapital1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtCapital2_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCapital2_Leave(sender As Object, e As EventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtEgresos1_Leave(sender As Object, e As EventArgs) Handles txtEgresos1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtEgresos2_Leave(sender As Object, e As EventArgs) Handles txtEgresos2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtIngresos1_Leave(sender As Object, e As EventArgs) Handles txtIngresos1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtIngresos2_Leave(sender As Object, e As EventArgs) Handles txtIngresos2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtOrdenAcre1_Leave(sender As Object, e As EventArgs) Handles txtOrdenAcre1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtOrdenAcre2_Leave(sender As Object, e As EventArgs) Handles txtOrdenAcre2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtOrdenD1_Leave(sender As Object, e As EventArgs) Handles txtOrdenD1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtOrdenD2_Leave(sender As Object, e As EventArgs) Handles txtOrdenD2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtPasivoCirculante1_Leave(sender As Object, e As EventArgs) Handles txtPasivoCirculante1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtPasivoCirculante2_Leave(sender As Object, e As EventArgs) Handles txtPasivoCirculante2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtPasivoDiferido1_Leave(sender As Object, e As EventArgs) Handles txtPasivoDiferido1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtPasivoDiferido2_Leave(sender As Object, e As EventArgs) Handles txtPasivoDiferido2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtPasivoFijo1_Leave(sender As Object, e As EventArgs) Handles txtPasivoFijo1.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = (textBox.Text).PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtPasivoFijo2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPasivoFijo2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPasivoFijo2_Leave(sender As Object, e As EventArgs) Handles txtPasivoFijo2.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = textBox.Text.PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub txtRecultados_Leave(sender As Object, e As EventArgs) Handles txtRecultados.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        textBox.Text = textBox.Text.PadLeft(p.NNiv1, "0")
    End Sub

    Private Sub btnCierre_Click(sender As Object, e As EventArgs) Handles btnCierre.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ConfiguracionModificar, PermisosN.Secciones.Contabilidad) = False Then
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Dim respuesta As DialogResult = DialogResult.Cancel
        If p.existePolizaCierre() > 0 Then
            respuesta = MsgBox("La póliza de apertura para el ejercicio " + (p.anio + 1).ToString + " ya exite. ¿Desea reemplazarla?" + vbCrLf + "Si responde Sí, se reemplazará la poliza existente." + vbCrLf + "Si respode No, se cancelará la operación sin realizar cambios.", MsgBoxStyle.YesNo, GlobalNombreApp)
            If respuesta = DialogResult.Yes Then
                'ELIMINAR APERTURA EXISTENTE
                p.eliminarAperturaExistente()
                agregarPolizaApertura(True)
                'Else
                '    If respuesta = DialogResult.No Then
                '        'AGREGAR A YA EXISTENTE
                '        agregarPolizaApertura(False)
                '    End If
            End If

        Else
            'NORMAL
            agregarPolizaApertura(True)
        End If


    End Sub
    Private Sub agregarPolizaApertura(ByVal pNuevo As Boolean, Optional ByVal pSP As Boolean = True)
        btnCierre.Enabled = False
        btnGuardar.Enabled = False
        Try
            Dim progress As Integer = 0
            Dim anio As Integer
            Dim tabla As DataTable = p.polizaCierre()
            Dim Progreso As New frmProgreso((tabla.Rows.Count) + 8, 0)
            If pSP Then
                Progreso.Show()
            End If
            progress += 1
            Progreso.Aumentar(progress)

            'Dim tablaSI As DataTable = p.CAPITALCierre(p.anio.ToString + "/12/31")
            progress += 1
            Progreso.Aumentar(progress)
            p.CAPITALResultados(p.anio.ToString + "/12/31")
            Dim tablaSE As Double = p.sumaEdoResTodo(p.anio + "/01/01", p.anio + "/12/31")
            progress += 1
            Progreso.Aumentar(progress)
            My.Application.DoEvents()
            Dim cargos, abonos As String

            Dim suma As Double = 0

            If pNuevo Then
                p.guardarPoliza("A", 1, (p.anio + 1).ToString + "/01/01", "PÓLIZA APERTURA", "", "$0.00", 1, 3, 0, 0)
                p.DetallesOrden = 1
            End If
            progress += 1
            Progreso.Aumentar(progress)


            For i As Integer = 0 To tabla.Rows.Count - 1
                progress += 1
                If tabla.Rows(i)(3) <> 0 Then 'SI SALDO INICIAL ES DIFERENTE DE 0
                    If tabla.Rows(i)(2) <> "D" Then 'SI SON DEUDORAS
                        cargos = ""
                        abonos = tabla.Rows(i)(3).ToString
                        suma += Double.Parse(tabla.Rows(i)(3).ToString)
                    Else 'SI SON ACREEDORAS
                        cargos = tabla.Rows(i)(3).ToString
                        abonos = ""
                    End If
                    p.guardarDetalles(tabla.Rows(i)(1), "SALDOS INICIALES", cargos, abonos, tabla.Rows(i)(0), "", 0, "0", "", 0, 0, (p.anio + 1).ToString + "/01/01", "", 0, 0, p.DetallesOrden, 0, 0, 0, 0, 0)
                End If
                Progreso.Aumentar(progress)
                My.Application.DoEvents()
            Next
            progress += 1
            Progreso.Aumentar(progress)
            My.Application.DoEvents()

            'For i As Integer = 0 To tablaSI.Rows.Count - 1
            '    cargos = ""
            '    abonos = tablaSI.Rows(i)(3).ToString
            '    p.guardarDetalles(tablaSI.Rows(i)(0), "SALDOS INICIALES", cargos, abonos, tablaSI.Rows(i)(4), "", 0, 0, "", 0)

            'Next
            progress += 1
            Progreso.Aumentar(progress)
            ' For i As Integer = 0 To tablaSE.Rows.Count - 1
            cargos = ""
            abonos = CStr(p.ResultadosSaldo + tablaSE)
            p.guardarDetalles(p.ResultadosCuenta, "SALDOS INICIALES", cargos, abonos, p.ResultadosidCuenta, "", 0, 0, "", 0, 0, (p.anio + 1).ToString + "/01/01", "", 0, 0, p.DetallesOrden, 0, 0, 0, 0, 0)
            progress += 1
            Progreso.Aumentar(progress)
            '  Next

            My.Application.DoEvents()
            p.modificarImportePoliza(p.IDPoliza, suma.ToString("c2"))
            p.ActualizaIdsDetalles(p.IDPoliza)
            progress += 1
            Progreso.Aumentar(progress)
            Progreso.Close()
            PopUp("Periodo Cerrado", 90)
            'ACTUALIZAR SALDOS POSTERIORES

            If pSP Then

                anio = p.anio
                anio += 1
                For i As Integer = 0 To 10
                    anio += 1
                    If p.buscarPolizasPosteriores(anio - 1) > 0 Then
                        'realizar apertura
                        p.eliminarAperturaExistente(anio - 1)
                        agregarPolizaApertura(True, False)
                    Else
                        i = 10
                    End If
                Next
                p.llenaDatosConfig()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
        btnCierre.Enabled = True
        btnGuardar.Enabled = True
    End Sub

    Private Sub btnBuscarUUID_Click(sender As Object, e As EventArgs) Handles btnBuscarUUID.Click
       If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtRutaUUID.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub


    Private Sub btnCambiar_Click(sender As Object, e As EventArgs) Handles btnCambiar.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.ConfiguracionModificar, PermisosN.Secciones.Contabilidad) = False Then
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Dim change As New frmContabilidadContra()
        change.ShowDialog()
        change.Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox3.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub cmbAnios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAnios.SelectedIndexChanged
        DateTimePicker1.Value = CDate(cmbAnios.Text + "/" + DateTimePicker1.Value.Month.ToString("00") + "/" + DateTimePicker1.Value.Day.ToString("00"))
    End Sub
End Class