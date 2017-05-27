

Public Class Actualizador
    Public MensajeError As String
    Public Archivo As String = "prueba.zip"
    Public Sub New()

    End Sub
    Public Sub New(ByVal NombredelArchivo As String)
        Archivo = NombredelArchivo
    End Sub
    Public Function Descarga() As Integer
        Try
            Dim imageAddress As String = String.Empty
            Dim filename As String = String.Empty
            Dim fileReader As New System.Net.WebClient
            imageAddress = "http://www.pullsystemsoft.com/descargas/" + Archivo
            filename = Application.StartupPath + "\" + Archivo
            If System.IO.File.Exists(filename) = False Then
                fileReader.DownloadFile(imageAddress, filename)
            End If
            Return 1
        Catch ex As Exception
            MensajeError = ex.Message
            Return 0
        End Try
    End Function
    Public Function CreaBackUp() As Integer
        Try
            If System.IO.Directory.Exists(Application.StartupPath + "\backup") = False Then
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\backup")
            End If
            System.IO.File.Copy(Application.StartupPath + "\" + Archivo, Application.StartupPath + "\backup\" + Archivo, True)
            Return 1
        Catch ex As Exception
            MensajeError = ex.Message
            Return 0
        End Try
    End Function
    Public Function Actualiza() As Integer

    End Function
End Class
