Public Class frmOpcionesCorreo
    Dim O As New dbOpciones(MySqlcon)
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim Htmltxt As String = "Si recibe este correo es por que la prueba de configuración de correo ha sido exitosa." + vbCrLf + ""
            'Htmltxt = "<html><body><h1><span style=""color: #ff0000;"">Calando HTML</span></h1><p>&nbsp;</p><table style=""height: 107px;"" width=""520""><tbody><tr><td><h3>Marca:</h3></td><td><h3>Nissan</h3></td></tr><tr><td><h3>Modelo:</h3></td><td><h3>Sentra</h3></td></tr><tr><td><h3>Color:</h3></td><td><h3>Chocolat&oacute;n</h3></td></tr><tr><td><h3>Kilometraje:</h3></td><td><h3>1059000</h3></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table><p>&nbsp;</p><p><strong>Blablablabla y que sean muy felices.</strong></p><p>&nbsp;</p></body></html>"

            Dim email As New Net.Mail.MailMessage(TextBox1.Text, TextBox7.Text)
            Dim EmailServer As New Net.Mail.SmtpClient(TextBox5.Text, CInt(TextBox8.Text))
            EmailServer.EnableSsl = CheckBox1.Checked
            Dim UserLogIn As New Net.NetworkCredential
            UserLogIn.UserName = TextBox2.Text
            UserLogIn.Password = TextBox4.Text
            EmailServer.Credentials = UserLogIn
            'email.IsBodyHtml = True
            email.Body = Htmltxt
            email.Subject = "Correo de Prueba"
            EmailServer.Send(email)
            email.Dispose()

            'Dim m As New MailManager(TextBox5.Text, TextBox1.Text, TextBox2.Text, TextBox4.Text, CInt(TextBox8.Text), CheckBox1.Checked)
            'm.send("Correo de Prueba", Htmltxt, TextBox7.Text, "Prueba", "", "")
            PopUp("Correo enviado", 90)
        Catch ex As Exception
            MsgBox("No se pudo enviar el correo. " + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub
    'mail.pullsystemsoft.com
    '28
    'mail@pullsystemsoft.com
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        O.EmailHost = TextBox5.Text
        O.Email = TextBox1.Text
        O.EmailUsuario = TextBox2.Text
        O.EmailPass = TextBox4.Text
        O.EmailPuerto = TextBox8.Text
        O.CorreoContenido = TextBox3.Text
        If CheckBox1.Checked Then
            O.EmailSSL = 1
        Else
            O.EmailSSL = 0
        End If
        O.GuardaCorreo()
        My.Settings.emailfrom = TextBox1.Text
        My.Settings.emailhost = TextBox5.Text
        My.Settings.emailusuario = TextBox2.Text
        My.Settings.emailpassword = TextBox4.Text
        My.Settings.emailpuerto = CInt(TextBox8.Text)
        My.Settings.encriptacionssl = CheckBox1.Checked

        'O._EncabezadoEmail = TextBox3.Text
        'O._PiedepaginaEmail = TextBox6.Text

        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmOpcionesCorreo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        TextBox5.Text = O.EmailHost
        TextBox1.Text = O.Email
        TextBox2.Text = O.EmailUsuario
        TextBox4.Text = O.EmailPass
        TextBox8.Text = O.EmailPuerto
        TextBox3.Text = O.CorreoContenido
        If O.EmailSSL = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
    End Sub
End Class