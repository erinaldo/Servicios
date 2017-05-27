Imports System.Net.Mail
Public Class MailManager
    Dim Host As String
    Dim Usuario As String
    Dim Password As String
    Dim FromMail As String
    Dim PuertoMail As Integer
    Dim ESSL As Boolean
    Public Sub New(ByVal pHost As String, ByVal pFromMail As String, ByVal pUsuario As String, ByVal pPassword As String, ByVal pPuerto As Integer, ByVal pEncriptacionSSL As Boolean)
        Host = pHost
        Usuario = pUsuario
        Password = pPassword
        FromMail = pFromMail
        PuertoMail = pPuerto
        ESSL = pEncriptacionSSL
    End Sub
    Public Sub send(ByVal asunto As String, ByVal mensaje As String, ByVal addressesto As String, ByVal nombre As String, ByVal pRutaArchivo1 As String, ByVal pRutaArchivo2 As String)
        Dim client As New SmtpClient()

        Dim message As New MailMessage
        Dim Attach1 As Attachment
        Dim Attach2 As Attachment
        Try


            Message.From = New MailAddress(FromMail)

            'Dim Nombre As String = ""
            Dim Pos As Integer = 0
            Dim Listo As Boolean = False
            Dim AddDir As String = ""

            While addressesto.Length > Pos
                If addressesto.Substring(Pos, 1) = "," Then
                    
                    message.To.Add(New MailAddress(AddDir, nombre))
                    AddDir = ""
                    Listo = True
                Else
                    
                    Dim Xs As Integer
                    Dim X() As Char
                    X = addressesto.Substring(Pos, 1).ToCharArray
                    Xs = Asc(X(0))
                    If Xs > 31 Then
                        AddDir += addressesto.Substring(Pos, 1)
                    End If
                    Listo = False
                    End If
                    Pos += 1
            End While

            If Listo = False Then Message.To.Add(New MailAddress(AddDir, nombre))
            Message.Subject = asunto
            Message.Body = mensaje
            Dim basicAuthenticationInfo As New System.Net.NetworkCredential(Usuario, Password)
            If pRutaArchivo1 <> "" Then
                Attach1 = New Attachment(pRutaArchivo1)
                Message.Attachments.Add(Attach1)
            End If
            If pRutaArchivo2 <> "" Then
                Attach2 = New Attachment(pRutaArchivo2)
                Message.Attachments.Add(Attach2)
            End If
            client.Host = Host
            client.EnableSsl = ESSL
            client.Port = PuertoMail
            client.UseDefaultCredentials = False
            client.Timeout = 120000
            client.Credentials = basicAuthenticationInfo
            client.Send(Message)
            Message.Attachments.Dispose()
            Message.Dispose()
        Catch ex As System.Net.Mail.SmtpException
            Throw New Exception("No se pudo enviar el correo: " + ex.Message)
            message.Dispose()
        End Try
    End Sub

    'Public Sub send(ByVal asunto As String, ByVal mensaje As String, ByVal clientes As ArrayList)
    '    Try
    '        Dim client As New SmtpClient()

    '        Dim message As New MailMessage
    '        message.From = New MailAddress("mail@jcballesteros.com")
    '        'For Each c In clientes
    '        '    If c.email <> "" Then
    '        '        Dim addressto As New MailAddress(c.email, c.nombre)
    '        '        message.To.Add(addressto)
    '        '    End If
    '        'Next
    '        message.Subject = asunto
    '        message.Body = mensaje + vbNewLine + vbNewLine + "Este es un mensaje enviado automaticamente, no responda a este mensaje a la dirección de la que se envio."

    '        Dim basicAuthenticationInfo As New System.Net.NetworkCredential("mail@jcballesteros.com", "d4m28xr")
    '        'Put your own, or your ISPs, mail server name onthis next line
    '        client.Host = "mail.jcballesteros.com"
    '        client.Port = 28
    '        client.UseDefaultCredentials = False
    '        client.Credentials = basicAuthenticationInfo

    '        client.Send(message)
    '    Catch ex As System.Net.Mail.SmtpException
    '        Throw New Exception("No se pudo enviar el correo: " + ex.Message)
    '    End Try
    'End Sub
End Class
