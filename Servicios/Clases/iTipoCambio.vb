Public Class iTipoCambio
    Private Function GetPageHTML(ByVal URL As String, Optional ByVal TimeoutSeconds As Integer = 10) As String
        ' Retrieves the HTML from the specified URL,
        ' using a default timeout of 10 seconds
        Dim objRequest As Net.WebRequest
        Dim objResponse As Net.WebResponse
        Dim objStreamReceive As System.IO.Stream
        Dim objEncoding As System.Text.Encoding
        Dim objStreamRead As System.IO.StreamReader

        Try
            ' Setup our Web request
            objRequest = Net.WebRequest.Create(URL)
            objRequest.Timeout = TimeoutSeconds * 1000
            ' Retrieve data from request
            objResponse = objRequest.GetResponse
            objStreamReceive = objResponse.GetResponseStream
            objEncoding = System.Text.Encoding.GetEncoding("utf-8")
            objStreamRead = New System.IO.StreamReader(objStreamReceive, objEncoding)
            ' Set function return value
            GetPageHTML = objStreamRead.ReadToEnd()
            ' Check if available, then close response
            If Not objResponse Is Nothing Then
                objResponse.Close()
            End If
        Catch
            ' Error occured grabbing data, simply return nothing
            Return ""
        End Try
    End Function

    Public Function obtener() As Double
        Dim html As String = GetPageHTML("http://tipodecambiohoy.com/", 5)
        If html = "" Then Throw New Exception("No se pudo obtener el tipo de cambio.")
        Dim inicio As Integer = html.IndexOf("<IMG SRC=""banxico.jpg"" ALT=""Banco de México"">") + html.Substring(html.IndexOf("<IMG SRC=""banxico.jpg"" ALT=""Banco de México"">")).IndexOf("<div id=""valores"">")

        Return html.Substring(inicio + html.Substring(inicio, html.Substring(inicio).IndexOf("</p>")).LastIndexOf(">") + 1).Substring(0, html.Substring(inicio + html.Substring(inicio, html.Substring(inicio).IndexOf("</p>")).LastIndexOf(">") + 1).IndexOf("</p>"))
    End Function
End Class
