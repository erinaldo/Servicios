
Imports System
Imports System.Threading
Imports System.Net.Sockets
Imports System.IO
Imports System.Text
Public Class escuchaTeclado
    Public Socket As Socket 'Socket utilizado para mantener la conexion con el cliente
    Public listener As TcpListener
    Public Thread As Thread 'Thread utilizado para escuchar al cliente
    Public UltimosDatosRecibidos As String 'Ultimos datos enviados por el cliente
    Public cajaTexto As TextBox 'campo en el que escribiremos lo que escuchemos del cliente
    Dim datos(255) As Byte
    Dim cadena As String
    Dim hilo As Threading.Thread

    Public Sub New()
        'Me.cajaTexto = cajaTexto

    End Sub

    Private Sub iniciaConexion()
        Do
            Try
                If hilo.ThreadState = Threading.ThreadState.Aborted Or hilo.ThreadState = Threading.ThreadState.AbortRequested Then
                    Exit Do
                End If
                If listener.Pending Then
                    Socket = listener.AcceptSocket
                    Socket.Receive(datos)
                    cadena = System.Text.Encoding.BigEndianUnicode.GetString(datos)
                    cajaTexto.Text = cadena
                    Socket.Disconnect(False)
                    Socket.Close()
                End If
            Catch
            End Try
        Loop
    End Sub

    Public Sub terminaConexion()
        listener.Stop()
    End Sub
End Class
