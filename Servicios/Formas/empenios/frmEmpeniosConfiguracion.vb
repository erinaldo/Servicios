Public Class frmEmpeniosConfiguracion
    Dim C As New dbEmpeniosConfiguracion(1, MySqlcon)
    Dim Folio As Integer

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub frmEmpeniosConfiguracion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox1.Items.Add("No")
        ComboBox1.Items.Add("Si")
        llenadatos()
    End Sub

    Private Sub llenadatos()
        C.LlenaDatos()
        TextBox3.Text = My.Settings.impresoraempenios
        txtFactor.Text = C.factor.ToString("0.000#")
        txtInteres.Text = C.interes.ToString("0.000#")
        txtAlmacenaje.Text = C.almacenaje.ToString("0.000#")
        ' txtGasto.Text = C.gasto.ToString("0.000")
        TextBox1.Text = C.diasProrroga.ToString
        txt31a60.Text = C.A31a60.ToString("0.000#")
        txt61a90.Text = C.A61a90.ToString("0.000#")
        txt90mas.Text = C.A90mas.ToString("0.000#")
        txtAlmacenaje31a60.Text = C.B31a60.ToString("0.000#")
        txtAlmacenaje61a90.Text = C.B61a90.ToString("0.000#")
        txtAlmacenaje90mas.Text = C.B90mas.ToString("0.000#")
        TextBox2.Text = C.aumentoPrenda.ToString("0.000#")
        txtPROFECO.Text = C.numRegistro
        txtPlata.Text = C.valorPlata.ToString("0.000#")
        txtAumentoPlata.Text = C.aumentoPlata.ToString("0.000#")


        txtDiasProrrogaT.Text = C.diasProrrogaT.ToString("0.000#")
        txtDiasProrrogaV.Text = C.diasProrrogaV.ToString("0.000#")
        txtInteresV16a30.Text = C.interes16a30V.ToString("0.000#")
        txtInteresV31a60.Text = C.interes31a60V.ToString("0.000#")
        txtInteresV1a15.Text = C.interes1a15V.ToString("0.000#")
        txtAlmacenajeV1a15.Text = C.almacenaje1a15V.ToString("0.000#")
        txtAlmacenajeV16a30.Text = C.almacenaje16a30V.ToString("0.000#")
        txtAlmacenajeV31a60.Text = C.almacenaje31a60V.ToString("0.000#")
        txtInteres1a15T.Text = C.interes1a15T.ToString("0.000#")
        txtInteres16a30T.Text = C.interes16a30T.ToString("0.000#")
        txtInteres31a60T.Text = C.interes31a60T.ToString("0.000#")
        txtAlmacenajeT1a15.Text = C.almacenaje1a15T.ToString("0.000#")
        txtAlmacenajeT16a30.Text = C.almacenaje16a30T.ToString("0.000#")
        txtAlmacenajeT31a60.Text = C.almacenaje31a60T.ToString("0.000#")
        txtAumentoPrendaVehiculo.Text = C.aumentoValorPrendaV.ToString("0.000#")
        txtAumentoValorTerreno.Text = C.aumentoValorPrendaT.ToString("0.000#")
        ComboBox1.SelectedIndex = C.Vis
        TextBox15.Text = C.ClaveAutorizacion
        Folio = C.folio
        If Folio = 0 Then
            rdbFecha.Checked = True
            rdbNormal.Checked = False
        Else
            rdbFecha.Checked = False
            rdbNormal.Checked = True
        End If
        'cmbImpresionDoble.SelectedIndex = C.impresion
        If My.Settings.empenioscantimpresion = 2 Then
            cmbImpresionDoble.SelectedIndex = 0
        Else
            cmbImpresionDoble.SelectedIndex = 1
        End If
    End Sub

    Private Sub AgregaArticulo()
        Try

            If rdbFecha.Checked = True Then
                Folio = 0
            Else
                Folio = 1
            End If
            C.Modificar(1, txtFactor.Text, Folio, txtInteres.Text, TextBox1.Text, txtPROFECO.Text, txtAlmacenaje.Text, txtInteres.Text, txt31a60.Text, txt61a90.Text, txt90mas.Text, txtAlmacenaje.Text, txtAlmacenaje31a60.Text, txtAlmacenaje61a90.Text, txtAlmacenaje90mas.Text, TextBox2.Text, txtPlata.Text, txtAumentoPlata.Text, txtDiasProrrogaV.Text, txtDiasProrrogaT.Text, txtInteresV1a15.Text, txtInteresV16a30.Text, txtInteresV31a60.Text, txtInteres1a15T.Text, txtInteres16a30T.Text, txtInteres31a60T.Text, txtAlmacenajeV1a15.Text, txtAlmacenajeV16a30.Text, txtAlmacenajeV31a60.Text, txtAlmacenajeT1a15.Text, txtAlmacenajeT16a30.Text, txtAlmacenajeT31a60.Text, txtAumentoPrendaVehiculo.Text, txtAumentoValorTerreno.Text, TextBox15.Text, cmbImpresionDoble.SelectedIndex, ComboBox1.SelectedIndex)
            My.Settings.impresoraempenios = TextBox3.Text
            If cmbImpresionDoble.SelectedIndex = 0 Then
                My.Settings.empenioscantimpresion = 2
            Else
                My.Settings.empenioscantimpresion = 1
            End If
            My.Settings.Save()
            PopUp("Modificado", 90)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnRestaurar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestaurar.Click
        llenadatos()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosConfiguracionAlta, PermisosN.Secciones.Empenios) Then
            ' " No tiene permiso para realizar esta operación."
            AgregaArticulo()
            Me.Close()
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub
#Region "Validación de TextBox"

    Private Sub txtFactor_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFactor.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInteres.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFactor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFactor.Leave
        Dim x As Double
        If txtFactor.Text = "." Then
            txtFactor.Text = "0.000"
        End If
        If txtFactor.Text = "" Then
            txtFactor.Text = "0.000"
        Else
            x = Double.Parse(txtFactor.Text)
            txtFactor.Text = Format(x, "0.000#")
        End If
    End Sub
    Private Sub txtInteres_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInteres.Leave
        Dim x As Double
        If txtInteres.Text = "." Then
            txtInteres.Text = "0.000"
        End If
        If txtInteres.Text = "" Then
            txtInteres.Text = "0.000"
        Else
            x = Double.Parse(txtInteres.Text)
            txtInteres.Text = Format(x, "0.000#")
        End If
    End Sub


    Private Sub TextBox1_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If TextBox1.Text = "" Then
            TextBox1.Text = "0"
        End If
    End Sub

    Private Sub txtPROFECO_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPROFECO.Leave
        If txtPROFECO.Text = "" Then
            txtPROFECO.Text = "xxxx-xxxx"
        End If
    End Sub

    Private Sub txtAlmacenaje_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenaje.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenaje_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenaje.Leave
        Dim x As Double
        If txtAlmacenaje.Text = "." Then
            txtAlmacenaje.Text = "0.000"
        End If
        If txtAlmacenaje.Text = "" Then
            txtAlmacenaje.Text = "0.000"
        Else
            x = Double.Parse(txtAlmacenaje.Text)
            txtAlmacenaje.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txt1a30_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt31a60_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt31a60.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt61a90_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt61a90.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txt90mas_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt90mas.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txt31a60_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt31a60.Leave
        Dim x As Double
        If txt31a60.Text = "." Then
            txt31a60.Text = "0.000"
        End If
        If txt31a60.Text = "" Then
            txt31a60.Text = "0.000"
        Else
            x = Double.Parse(txt31a60.Text)
            txt31a60.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txt61a90_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt61a90.Leave
        Dim x As Double
        If txt61a90.Text = "." Then
            txt61a90.Text = "0.000"
        End If
        If txt61a90.Text = "" Then
            txt61a90.Text = "0.000"
        Else
            x = Double.Parse(txt61a90.Text)
            txt61a90.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txt90mas_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt90mas.Leave
        Dim x As Double
        If txt90mas.Text = "." Then
            txt90mas.Text = "0.000"
        End If
        If txt90mas.Text = "" Then
            txt90mas.Text = "0.000"
        Else
            x = Double.Parse(txt90mas.Text)
            txt90mas.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub TextBox2_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        Dim x As Double
        If TextBox2.Text = "." Then
            TextBox2.Text = "0.000"
        End If
        If TextBox2.Text = "" Then
            TextBox2.Text = "0.000"
        Else
            x = Double.Parse(TextBox2.Text)
            TextBox2.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenaje31a60_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenaje31a60.Leave
        Dim x As Double
        If txtAlmacenaje31a60.Text = "." Then
            txtAlmacenaje31a60.Text = "0.000"
        End If
        If txtAlmacenaje31a60.Text = "" Then
            txtAlmacenaje31a60.Text = "0.000"
        Else
            x = Double.Parse(txtAlmacenaje31a60.Text)
            txtAlmacenaje31a60.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenaje61a90_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenaje61a90.Leave
        Dim x As Double
        If txtAlmacenaje61a90.Text = "." Then
            txtAlmacenaje61a90.Text = "0.000"
        End If
        If txtAlmacenaje61a90.Text = "" Then
            txtAlmacenaje61a90.Text = "0.000"
        Else
            x = Double.Parse(txtAlmacenaje61a90.Text)
            txtAlmacenaje61a90.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenaje90mas_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenaje90mas.Leave
        Dim x As Double
        If txtAlmacenaje90mas.Text = "." Then
            txtAlmacenaje90mas.Text = "0.000"
        End If
        If txtAlmacenaje90mas.Text = "" Then
            txtAlmacenaje90mas.Text = "0.000"
        Else
            x = Double.Parse(txtAlmacenaje90mas.Text)
            txtAlmacenaje90mas.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenaje31a60_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenaje31a60.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenaje61a90_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenaje61a90.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenaje90mas_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenaje90mas.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox3.Text = PrintDialog1.PrinterSettings.PrinterName
        End If
    End Sub

    Private Sub txtPlata_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPlata.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPlata_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPlata.Leave
        Dim x As Double
        If txtPlata.Text = "." Then
            txtPlata.Text = "0.000"
        End If
        If txtPlata.Text = "" Then
            txtPlata.Text = "0.000"
        Else
            x = Double.Parse(txtPlata.Text)
            txtPlata.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAumentoPlata_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAumentoPlata.TextChanged

    End Sub

    Private Sub txtAumentoPlata_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAumentoPlata.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAumentoPlata_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAumentoPlata.Leave
        Dim x As Double
        If txtAumentoPlata.Text = "." Then
            txtAumentoPlata.Text = "0.000"
        End If
        If txtAumentoPlata.Text = "" Then
            txtAumentoPlata.Text = "0.000"
        Else
            x = Double.Parse(txtAumentoPlata.Text)
            txtAumentoPlata.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtDiasProrrogaV_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiasProrrogaV.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDiasProrrogaT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiasProrrogaT.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDiasProrrogaV_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiasProrrogaV.Leave
        If txtDiasProrrogaV.Text = "" Then
            txtDiasProrrogaV.Text = "0"
        End If
    End Sub

    Private Sub TabControl1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.Leave
        If TextBox1.Text = "" Then
            TextBox1.Text = "0"
        End If
    End Sub

    Private Sub txtInteresV1a15_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInteresV1a15.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInteresV16a30_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInteresV16a30.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInteresV31a60_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInteresV31a60.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenajeV1a15_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenajeV1a15.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenajeV16a30_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenajeV16a30.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenajeV31a60_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenajeV31a60.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAumentoPrendaVehiculo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAumentoPrendaVehiculo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInteres1a15T_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInteres1a15T.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInteres16a30T_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInteres16a30T.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInteres31a60T_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInteres31a60T.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenajeT1a15_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenajeT1a15.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenajeT16a30_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenajeT16a30.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAlmacenajeT31a60_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAlmacenajeT31a60.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtAumentoValorTerreno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAumentoValorTerreno.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtInteresV1a15_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInteresV1a15.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtInteresV16a30_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInteresV16a30.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtInteresV31a60_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInteresV31a60.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenajeV1a15_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenajeV1a15.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenajeV16a30_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenajeV16a30.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenajeV31a60_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenajeV31a60.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAumentoPrendaVehiculo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAumentoPrendaVehiculo.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtInteres1a15T_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInteres1a15T.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtInteres16a30T_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInteres16a30T.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtInteres31a60T_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInteres31a60T.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenajeT1a15_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenajeT1a15.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenajeT16a30_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenajeT16a30.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAlmacenajeT31a60_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlmacenajeT31a60.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtAumentoValorTerreno_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAumentoValorTerreno.Leave
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim x As Double
        If textBox.Text = "" Then
            textBox.Text = "0.000"
        Else
            x = Double.Parse(textBox.Text)
            textBox.Text = Format(x, "0.000#")
        End If
    End Sub

    Private Sub txtDiasProrrogaT_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiasProrrogaT.Leave
        If txtDiasProrrogaT.Text = "" Then
            txtDiasProrrogaT.Text = "0"
        End If
    End Sub
#End Region


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'If BuscaVentanas("frmEmpeniosNover") = False Then
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.PermitirExtraCambio, PermisosN.Secciones.Empenios) Then
            Dim f As New frmEmpeniosNover()
            f.ShowDialog()
            f.Dispose()
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If
        'End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.PermitirExtraVer, PermisosN.Secciones.Empenios) Then
            Dim EC As New dbEmpeniosConfiguracion(MySqlcon)
            EC.ModificarExtraVer(ComboBox1.SelectedIndex)
            PopUp("Modificado", 50)
        Else
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub
End Class