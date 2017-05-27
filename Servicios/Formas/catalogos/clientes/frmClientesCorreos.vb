Imports System.Net.Mail
Public Class frmClientesCorreos
    Dim hoy As Date = Today
    Public IdEquipo As Integer

    Private Sub btn_cerrar_Click(sender As Object, e As EventArgs) Handles btn_cerrar.Click
        Me.Close()
    End Sub
    Private Sub frmlClientesCorreos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        cargaCartas()
    End Sub
    Private Sub btn_enviar_Click(sender As Object, e As EventArgs) Handles btn_enviar.Click
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential(My.Settings.emailusuario, My.Settings.emailpassword)
            Smtp_Server.Port = My.Settings.emailpuerto
            Smtp_Server.EnableSsl = My.Settings.encriptacionssl
            Smtp_Server.Host = My.Settings.emailhost


            'Comienza el for
            Dim contador As Integer = 0
            Dim cartasmenos As Integer = 0
            For Each row As DataGridViewRow In DataGridView1.Rows
                IdEquipo = DataGridView1.Item(3, contador).Value
                Dim P As New dbClientesEquipos(IdEquipo, MySqlcon)
                Dim C As New dbClientes(P.IdCliente, MySqlcon)
                Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Dim direccion As String = ""
                direccion = S.Direccion.ToString + " " + S.NoExterior.ToString + " " + S.Colonia.ToString
                If Not row.IsNewRow Then
                    Try
                        e_mail = New MailMessage()
                        e_mail.From = New MailAddress(My.Settings.emailusuario)

                        If C.Email = "" Then
                            MsgBox("El usuario: " + C.Nombre + " no tiene correo electronico.")
                            cartasmenos = cartasmenos + 1
                            Continue For
                        End If
                        e_mail.To.Add(C.Email)
                        e_mail.Subject = "TireMax - Recordatorio de servicio."
                        e_mail.IsBodyHtml = True
                        e_mail.Body = "<!DOCTYPE html> <html> <head> <style> table, th, td { border: 1px solid black; border-collapse: collapse; } th, td { padding: 5px; text-align: left; } table tr:nth-child(even) { background-color:#fff ; } table tr:nth-child(odd) { background-color:#eee; } table th { background-color: black; color: white; } </style> <meta charset='UTF-8'>  <title>Correo</title> <meta name='viewport' content='width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0'> <center><img src='http://i1250.photobucket.com/albums/hh536/Duzho01/TiremaxHeader_zpszxvrsk02.png?t=1484939188' alt='TireMax' style='width:800px;height:130px;'></center> </head> <body> <div style='text-align: center;font-size:115%; font-family:arial;'> <div style='display: inline-block; text-align: left; font-size:100%; font-family:arial;'> <p style='text-align: right;font-size:100%; font-family:arial; margin-top:0;margin-bottom:0;'>CD. OBREGÓN, SON. A " + Today.Day.ToString + " DE " + MonthName(Today.Month.ToString).ToUpper + " DEL " + Today.Year.ToString + "</p> <br> <b>" + C.Nombre + "</b> <br> <br> <br> LA PRESENTE ES PARA INFORMARLE EL ULTIMO DÍA QUE SE LE DIO SERVICIO A SU <br> VEHICULO: <br> <table style='width:100%'> <caption>Datos del vehiculo:</caption> <tr> <th>Dato</th> <th>Información</th> </tr> <tr> <td>Fecha de compra: </td> <td>" + P.fechaEnvio + "</td> </tr> <tr> <td>Marca:</td> <td>" + P.Marca + "</td> </tr> <tr> <td>Tipo:</td> <td>" + P.Modelo + "</td> </tr>  <tr> <td>Placas:</td> <td>" + P.Matricula.ToString + "</td> </tr> <td>Color:</td> <td>" + P.Color + "</td> </tr> <tr> <td>Modelo:</td> <td>" + P.NoSerie.ToString + "</td> </tr> <tr> <td>Kilometraje:</td> <td>" + P.Kilometraje.ToString + "</td> </tr> </table> <br> SE RECOMIENDA ROTACIÓN, ALINEACIÓN Y BALANCEO CADA 10,000 KMS O CADA 6 <br> MESES, ASI LAS LLANTAS DE SU VEHICULO SE DESGASTAN EN FORMA PAREJA. <br> <br> ASÍMISMO REVISAR SUSPENSIÓN, FRENOS Y AMORTIGUADORES. <br> <br> LA <b>ROTACIÓN ES GRATIS</b> SOLO SE COBRARÁ ALINEACIÓN Y BALANCEO. <center><img src='http://i1250.photobucket.com/albums/hh536/Duzho01/Michelin_zpsmfzn12vu.png?t=1484939185' align='middle' alt='Michelin' style='width:240px;height:80px;'></center> </div> <br> <b>TIREMAX<br> LLANTAS Y SERVICIOS ABASTOS S.A. DE C.V.</b><br>" + direccion + "<br>" + S.ReferenciaDomicilio + "<br>" + S.Ciudad + ", " + S.Estado + "<br> Tels: " + S.Telefono + "</div> </body> <footer> <center><img src='http://i1250.photobucket.com/albums/hh536/Duzho01/TiremaxFooter_zpsccmyetyu.png?t=1484939188' alt='TiremaxFooter' style='width:800px;height:130px;'></center> </footer> <div style='background-color: blue; text-align: center;font-size:115%; font-family:arial;'> <div style='width: 780px; display: inline-block; text-align: justify; font-size:100%; font-family:arial; color:white;'><br><br> <center style='font-size:120%;'>" +
                            "<b>SERVICIOS</b></center> <br> <br><b>ALINEACIÓN</b><br> La alineación es el seguro de vida de los neumáticos. Si este servicio se hace periódicamente, además de la rotación y balanceo, éstos duran más, se desgastan en forman pareja, teniendo una vida útil mucho mayor. Realizamos alineación delantera y de cuatro ruedas para todo tipo de autos y camionetas, incluyendo vehículos sedan y deportivos de lujo.<br> Recomendamos alinear su vehículo cada 6 meses o 10.000 kms. <br><br><b>BALANCEO</b><br> Al balancear ponemos en equilibrio el conjunto llanta-rin a través de contrapesos. El balanceo del conjunto llanta-rin evita el desgaste irregular de los neumáticos y de la suspensión eliminando las molestas vibraciones en el volante. <br><br><b>FRENOS</b><br> Cada vez que sienta que sus neumáticos no le responden o sus pastillas suenan, es tiempo de que se acerque  a hacer una revisión. Le ofrecemos un servicio especialista en frenos para todo tipo de vehículos  Realizamos los siguientes servicios: cambio de pastillas, balatas y rectificado o cambio de discos, además de una revisión de seguridad.<br><br><b>SUSPENSIÓN</b><br> Tiene como objetivo principal el permitir que las ruedas se muevan adecuadamente e independientemente del vehículo mantienendolo estable. Un desajuste en el sistema de suspensión trae como resultado un deterioro en el manejo del vehículo así como también un desgaste prematuro de sus neumáticos. <br><br><b>AMORTIGUADORES</b><br> Su función es mantener siempre la rueda pegada al piso. Cuando los amortiguadores fallan, el vehículo pierde estabilidad, se produce un menor efecto de frenado y los neumáticos se desgastan en forma dispareja.<br><br>Amortiguadores para todo tipo de autos, camionetas,  etc. El cambio de amortiguadores se realiza cada  60.000 kms. O cada 4 años. <br> <br> <br> </div> </html>"
                        Smtp_Server.Send(e_mail)
                        P.aumentaFecha(P.ID, P.fechaEnvio, MySqlcon)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
                        cartasmenos = cartasmenos + 1
                    End Try
                End If
                contador = contador + 1
            Next
            'Termina el for de envios
            PopUp("Cartas Enviadas: #" + (DataGridView1.Rows.Count - cartasmenos).ToString, 70)

        Catch error_t As Exception
            MsgBox(error_t.ToString, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
        cargaCartas()
    End Sub
    Public Sub cargaCartas()
        Dim PrimerCeldaRow As Integer = -1
        Dim P As New dbClientesEquipos(MySqlcon)
        DataGridView1.DataSource = P.ConsultaCartas()
        DataGridView1.Columns(1).Width = 120
        DataGridView1.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns(2).Width = 300
        DataGridView1.Columns(3).Visible = False
    End Sub
    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = 1 Then
            e.Value = Format(CDate(e.Value), "dd/MM/yyyy")
        End If
    End Sub
End Class