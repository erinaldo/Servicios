
Public Class StringFunctions


    Public Function PASELETRASx(ByVal x As Double, ByVal moneda As Integer) As String
        'If pidioma = 0 Then
        Return ALETRAS(Math.Round(x, 2)) + If(moneda = 2, " PESOS ", " DOLARES ") + Right(Format(x, "#.00"), 2) + "/100" + If(moneda = 2, " M.N.", " U.S.D.")
        ' Else
        '    Return ConvertMoneyToText(CStr(x))
        'End If
    End Function

    Private Function ALETRAS(ByVal x As Double) As String
        'DIVIDE DE A GRUPOS DE TRES DIGITOS Y CORRIGE LAS EXCEPCIONES
        Dim PARTE(4) As String
        Dim i As Integer
        'Ver para anular los decimales para que no se produzcan errores
        x = Fix(x)
        For i = 1 To 1 + Len(x) / 3
            PARTE(i) = Right(x, 3)
            x = Int(x / 1000)
        Next
        ALETRAS = ""
        ALETRAS = Nombre(PARTE(1))
        If PARTE(2) = 1 Then ALETRAS = "MIL " + ALETRAS
        If PARTE(2) > 1 Then
            If Right(Nombre(PARTE(2)), 3) = "UNO" Then
                ALETRAS = Left(Nombre(PARTE(2)), Len(Nombre(PARTE(2))) - 1) + " MIL" + ALETRAS
            Else
                ALETRAS = Nombre(PARTE(2)) + " MIL " + ALETRAS
            End If
        End If
        If PARTE(3) = 1 Then ALETRAS = "UN MILLON " + ALETRAS
        If PARTE(3) > 1 Then
            If Right(Nombre(PARTE(3)), 3) = "UNO" Then
                ALETRAS = Left(Nombre(PARTE(3)), Len(Nombre(PARTE(3)))) + "MILLONES" + ALETRAS
            Else
                ALETRAS = Nombre(PARTE(3)) + " MILLONES " + ALETRAS
            End If
        End If
        If PARTE(4) = 1 Then
            If PARTE(3) = 0 Then ALETRAS = "MILLONES" + ALETRAS
            ALETRAS = "MIL " + ALETRAS
        End If
        If PARTE(4) > 1 Then
            If PARTE(3) = 0 Then ALETRAS = "MILLONES" + ALETRAS
            If Right(Nombre(PARTE(4)), 3) = "UNO" Then
                ALETRAS = Left(Nombre(PARTE(4)), Len(Nombre(PARTE(4)))) + "MIL" + ALETRAS
            Else
                ALETRAS = Nombre(PARTE(4)) + " MIL " + ALETRAS
            End If
        End If
    End Function

    Private Function Nombre(ByVal x As Double) As String
        Dim xuni, xdec, xcent As String
        Dim uni, dec, cent As Double
        'aRMA CADA GRUPO DE TRES DIGITOS
        xuni = "UNO         DOS         TRES        CUATRO      CINCO       " + _
               "SEIS        SIETE       OCHO        NUEVE       DIEZ        " + _
               "ONCE        DOCE        TRECE       CATORCE     QUINCE      " + _
               "DIECISEIS   DIECISIETE  DIECIOCHO   DIECINUEVE  VEINTE      " + _
               "VEINTIUNO   VEINTIDOS   VEINTITRES  VEINTICUATROVEINTICINCO " + _
               "VEINTISEIS  VEINTISIETE VEINTIOCHO  VEINTINUEVE "
        xdec = "TREINTA  CUARENTA CINCUENTASESENTA  SETENTA  OCHENTA  NOVENTA"
        xcent = "DOSC   TRESC  CUATROCQUIN   SEISC  SETEC  OCHOC  NOVEC  "
        Nombre = ""
        'Ver para anular los decimales para que no se produzcan erroresx = Fix(x)
        uni = Right(x, 2)
        If uni < 30 And uni > 0 Then
            Nombre = Trim(Mid(xuni, 12 * (uni - 1) + 1, 12))
        End If
        If uni > 29 Then
            dec = Left(uni, 1)
            uni = Right(uni, 1)
            Nombre = Trim(Mid(xdec, 9 * (dec - 3) + 1, 9))
            If uni <> 0 Then
                Nombre = Nombre + " Y " + Trim(Mid(xuni, 12 * (uni - 1) + 1, 12))
            End If
        End If
        cent = Int(x / 100)
        If cent = 1 Then
            Nombre = "CIENTO " + Nombre
        End If
        If x = 100 Then
            Nombre = "CIEN"
        End If
        If cent > 1 Then
            Nombre = Trim(Mid(xcent, 7 * (cent - 2) + 1, 7)) + "IENTOS " + Nombre
        End If
    End Function

    Public Function FormatPrintString(ByVal inputstr As String, ByVal tipo As Integer, ByVal length As Integer, ByVal ajuste As Boolean, ByVal formato As String, Optional ByVal descstr As String = "", Optional ByVal desclength As Integer = 0, Optional ByVal ajustedesc As Boolean = False) As String
        'inputstr=cadena a la que se le van a agregar los saltos de linea
        'tipo=el formato que va a usar (cadena, decimal, moneda)
        'length=longitud a la cual se va a cortar la cadena
        'descstr=(descripcion) se usa si a una cadena se le van a agregar saltos dependiendo de otra cadena, por ejemplo las cantidades de los detalles de una factura
        'desclength=longitud de la descripcion
        Dim start = 0, position As Integer = 0
        Dim outputstr As String = ""
        inputstr = Format(inputstr)
        If formato = "" And length <> 0 Then 'si es cadena y tiene ancho definido
            If ajuste Then 'ajuste de cadena
                Do
                    position = If(inputstr.Substring(start, inputstr.Length - start).Length <= length, inputstr.Length - start, If(IndexUltimoSalto(inputstr.Substring(start, length)) = -1, length, IndexUltimoSalto(inputstr.Substring(start, length))))
                    outputstr += inputstr.Substring(start, position) + vbNewLine
                    start += position
                Loop While start < Format(inputstr).Length
                Return outputstr.Remove(outputstr.Length - 1, 1)
            Else 'corte de cadena
                Return If(inputstr.Length <= length, inputstr, inputstr.Substring(0, length))
            End If
        Else
            If ajustedesc Then
                Do 'obtiene los saltos de linea producidos por el ajuste de linea de la descripcion de articulo
                    position = If(descstr.Substring(start, descstr.Length - start).Length <= desclength, descstr.Length - start, If(IndexUltimoSalto(descstr.Substring(start, desclength)) = -1, descstr.Length, IndexUltimoSalto(descstr.Substring(start, desclength))))
                    outputstr += vbNewLine
                    start += position + 1
                Loop While start < descstr.Length And desclength < descstr.Length
                outputstr = outputstr.Remove(outputstr.Length - 1, 1)
            End If
            'If tipo = 0 Then Return inputstr + outputstr 'cadena sin ajuste de linea
            'If tipo = 1 Then Return If(inputstr = "", "", Format(CDec(inputstr), "C2").PadLeft(length)) + outputstr 'monedas
            'If tipo = 2 Then Return If(inputstr = "", "", Format(CDec(inputstr), "N2").PadLeft(length)) + outputstr 'decimales
            If inputstr = "" Then Return "" + outputstr
            If formato = "" Then Return inputstr + outputstr 'cadena sin ajuste de linea
            If formato.StartsWith("C") Or formato.StartsWith("N") Or formato.StartsWith("F") Then Return Format(CDec(inputstr), formato).PadLeft(length) + outputstr
            Return Format(CDate(inputstr), formato).PadLeft(length) + outputstr
        End If
        Return inputstr
    End Function

    Private Function IndexUltimoSalto(ByVal str As String) As Integer
        Dim index As Integer = -1
        If str.LastIndexOf(" ") > index Then index = str.LastIndexOf(" ") + 1
        If str.LastIndexOf("|") > index And str.LastIndexOf("|") > 0 Then index = str.LastIndexOf("|")
        If str.IndexOf(Chr(13)) <> -1 Then index = str.IndexOf(Chr(13)) + 1
        If str.IndexOf(vbNewLine) <> -1 Then index = str.IndexOf(vbNewLine) + 1
        Return index
    End Function

    Public Function longToGeneral(ByVal value As Long, ByVal length As Integer)
        Dim str As String = ""
        Dim nmod As String = ""
        Do
            nmod = value Mod 10
            value = Math.Truncate(value / 10)
            str = nmod + str
        Loop While value > 0
        Return str.PadLeft(length, "0")
    End Function

    Public Function StrToByteArray(ByVal str As String) As Byte()
        Dim encoding As New System.Text.UnicodeEncoding()
        Return encoding.GetBytes(str)
    End Function

    Public Function ByteArrayToStr(ByVal bytes As Byte()) As String
        Dim enc As New System.Text.UnicodeEncoding()
        Return enc.GetString(bytes)
    End Function


    
End Class
