Public Class frmAcercade

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmAcercade_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label2.Text = Version + "  " + My.Settings.serial
        Label4.Text = "CFDI3.2: " + FechaVerPunto2 + " C:" + GlobalConector.ToString
        Select Case GlobalTipoVersion
            Case 0
                Label3.Text = "Módulos: Completo" + vbCrLf
            Case 1
                Label3.Text = "Módulos: Facturación" + vbCrLf
            Case 2
                Label3.Text = "Módulos: Facturación y Clientes" + vbCrLf
            Case 3
                Label3.Text = "Módulos: Conector Macropro" + vbCrLf
            Case 4
                Label3.Text = "Módulos: ----" + vbCrLf
        End Select
        If ConPuntodeVenta Then Label3.Text += " + Punto de Venta"
        If GlobalConNomina Then Label3.Text += " + Nómina"
        If GlobalConBancos Then Label3.Text += " + Bancos"
        Label3.Text += vbCrLf
        If GlobalConGastos Then Label3.Text += " + Gastos"
        If GlobalconEmpenios Then Label3.Text += " + Empeños"
        If GlobalConServicios Then Label3.Text += " + Servicios"
        Label3.Text += vbCrLf
        If GlobalConContabilidad Then Label3.Text += " + Contabilidad"
        If GlobalConFertilizantes Then Label3.Text += " + Fertilizantes"
        If GlobalConValidador Then Label3.Text += " + Validador"
        Label3.Text += vbCrLf
        If GlobalconSemillas Then Label3.Text += " + Semillas"
        If GlobalConExtra Then Label3.Text += " + Extra"
        If Global30Dias Then Label3.Text += " + 30 días"
        If GlobalconIntegracion Then Label3.Text += " + Integración"
        If GlobalConUsuarios Then Label3.Text += " + Usuarios"
        If GlobalConRestaurant Then Label3.Text += " + Restaurante"
        Label3.Text += "."
        Label5.Text = "Licencia otorgada a: " + GlobalLicenciaA
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.BackgroundImage = ImagenFondo
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://www.pullsystemsoft.com")
    End Sub

   
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Licencia
        If MsgBox("Está por indicar una nueva licencia, la licencia anterior quedará inválida. Este proceso no se puede deshacer. ¿Desea continuar?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim Lic As New Licencia("localhost", False)
            'If My.Settings.serial = "" Then
            Dim frmI As New frmInput("Licencia:", frmInput.TipoDatos.Texto, False, False, 10)
            If frmI.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Lic.Licencia = frmI.Valor
            Else
                Lic.Licencia = ""
            End If
            frmI.Dispose()
            'Else
            '   Lic.Licencia = My.Settings.serial
            'End If
            If Lic.Licencia <> "" Then
                If Lic.Validar(Lic.Licencia, True) Then
                    MsgBox("Licencia validada con éxito. Para que la nueva licencia tenga efecto debe reiniciar el sistema.", MsgBoxStyle.Information, GlobalNombreApp)
                Else
                    MsgBox(Lic.MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            End If
        End If
    End Sub

   
End Class