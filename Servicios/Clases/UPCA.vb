Public Class UPCA
    Private _sName As String = "UPC-A"

    Private _fMinimumAllowableScale As Double = 0.8 'aqui
    Private _fMaximumAllowableScale As Double = 2.0 'aqui


    Private _fWidth As Double = 1.469 'aqui
    Private _fHeight As Double = 1.02 'aqui
    Private _fFontSize As Double = 8.0 'aqui
    Private _fScale As Double = 1.0 'aqui

    '// Left Hand Digits.
    Private _aLeft As String() = {"0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011"}

    '	// Right Hand Digits.
    Private _aRight As String() = {"1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100"}

    Private _sQuiteZone As String = "0000000000"

    Private _sLeadTail As String = "101"

    Private _sSeparator As String = "01010"

    Private _sProductType As String = "0"
    Private _sManufacturerCode As String
    Private _sProductCode As String
    Private _sChecksumDigit As String

    Public Sub UpcA()

    End Sub



    Public Sub UpcA(ByVal mfgNumber As String, ByVal productId As String)
        Me.ProductType = "0"
        Me.ManufacturerCode = mfgNumber
        Me.ProductCode = productId
        Me.CalculateChecksumDigit()
    End Sub

    Public Sub UpcA(ByVal productType As String, ByVal mfgNumber As String, ByVal productId As String)
        Me.ProductType = productType
        Me.ManufacturerCode = mfgNumber
        Me.ProductCode = productId
        Me.CalculateChecksumDigit()
    End Sub

    Public Sub UpcA(ByVal productType As String, ByVal mfgNumber As String, ByVal productId As String, ByVal checkDigit As String)
        Me.ProductType = productType
        Me.ManufacturerCode = mfgNumber
        Me.ProductCode = productId
        Me.ChecksumDigit = checkDigit
    End Sub

    Public Function DrawUpcaBarcode(ByVal g As System.Drawing.Graphics, ByVal pt As System.Drawing.Point, ByVal objPicBox As PictureBox) As Image
        Dim width As Double = Me.Width * Me.Scale '3decimanes
        Dim height As Double = Me.Height * Me.Scale '3decimales
        'ANADIR EXPERIMIENTO
        ' Dim g As System.Drawing.Graphics
        Dim img As New Bitmap(302, 219, Drawing.Imaging.PixelFormat.Format24bppRgb)
        g = Graphics.FromImage(img)

        'TERMINAR

        Dim lineWidth As Double = width / 113.0F '3decimales

        Dim gs As System.Drawing.Drawing2D.GraphicsState = g.Save()



        g.PageUnit = System.Drawing.GraphicsUnit.Inch


        g.PageScale = 1

        Dim brush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(System.Drawing.Color.Black) 'color de la barra

        Dim xPosition As Double = 0

        Dim strbUPC As System.Text.StringBuilder = New System.Text.StringBuilder()

        Dim xStart As Double = pt.X
        Dim yStart As Double = pt.Y
        Dim xEnd As Double = 0

        Dim font As System.Drawing.Font = New System.Drawing.Font("Arial", Me._fFontSize * Me.Scale)

        g.FillRectangle(Brushes.White, 0, 0, 302, 219)
        Me.CalculateChecksumDigit()


        strbUPC.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{1}{0}", Me._sQuiteZone, Me._sLeadTail, ConvertToDigitPatterns(Me.ProductType, Me._aLeft), ConvertToDigitPatterns(Me.ManufacturerCode, Me._aLeft), Me._sSeparator, ConvertToDigitPatterns(Me.ProductCode, Me._aRight), ConvertToDigitPatterns(Me.ChecksumDigit, Me._aRight))

        Dim sTempUPC As String = strbUPC.ToString() 'aqui no da


        Dim fTextHeight As Double = g.MeasureString(sTempUPC, font).Height


        For i As Integer = 0 To strbUPC.Length - 1
            If (sTempUPC.Substring(i, 1) = "1") Then
                If (xStart = pt.X) Then
                    xStart = xPosition
                End If



                If ((i > 19 And i < 56) Or (i > 59 And i < 95)) Then

                    g.FillRectangle(brush, CSng(xPosition), CSng(yStart), CSng(lineWidth), CSng(height - fTextHeight))
                Else

                    g.FillRectangle(brush, CSng(xPosition), CSng(yStart), CSng(lineWidth), CSng(height))
                End If

            End If

            xPosition += lineWidth 'redondear a 3
            xEnd = xPosition 'igual
        Next





        xPosition = xStart - g.MeasureString(Me.ProductType, font).Width
        Dim yPosition As Double = yStart + (height - fTextHeight)

        g.DrawString(Me.ProductType, font, brush, New System.Drawing.PointF(xPosition, yPosition))



        xPosition += g.MeasureString(Me.ProductType, font).Width + 45 * lineWidth - g.MeasureString(Me.ManufacturerCode, font).Width


        g.DrawString(Me.ManufacturerCode, font, brush, New System.Drawing.PointF(xPosition, yPosition))


        xPosition += g.MeasureString(Me.ManufacturerCode, font).Width + 5 * lineWidth

        g.DrawString(Me.ProductCode, font, brush, New System.Drawing.PointF(xPosition, yPosition))


        xPosition += 46 * lineWidth


        g.DrawString(Me.ChecksumDigit, font, brush, New System.Drawing.PointF(xPosition, yPosition))


        g.Restore(gs)

        'EXPERIMENTO
        objPicBox.Image = img
        Dim mimag As Graphics = Graphics.FromImage(objPicBox.Image)
        Return img
        'END 
    End Function


    Public Function CreateBitmap(ByVal objPicBox As PictureBox) As System.Drawing.Bitmap
        Dim tempWidth As Double = (Me.Width * Me.Scale) * 100
        Dim tempHeight As Double = (Me.Height * Me.Scale) * 100

        ' Dim bmp As System.Drawing.Bitmap = New System.Drawing.Bitmap((Int)tempWidth, (Int)tempHeight)
        Dim bmp As New Bitmap(Integer.Parse(Int(tempWidth)), Integer.Parse(Int(tempHeight)))

        Dim g As System.Drawing.Graphics

        g = System.Drawing.Graphics.FromImage(bmp)
        Me.DrawUpcaBarcode(g, New System.Drawing.Point(0, 0), objPicBox)
        g.Dispose()
        Return bmp
    End Function

    Private Function ConvertToDigitPatterns(ByVal inputNumber As String, ByVal patterns As String()) As String
        Dim sbTemp As New System.Text.StringBuilder
        Dim iIndex As Integer = 0
        For i As Integer = 0 To inputNumber.Length - 1
            iIndex = Convert.ToInt32(inputNumber.Substring(i, 1))
            sbTemp.Append(patterns(iIndex))
        Next
			
        Return sbTemp.ToString()
    End Function

    Public Sub CalculateChecksumDigit()
        Dim sTemp As String = Me.ProductType + Me.ManufacturerCode + Me.ProductCode
        Dim iSum As Integer = 0
        Dim iDigit As Integer = 0

        '// Calculate the checksum digit here.
        For i As Integer = 1 To sTemp.Length
            iDigit = Convert.ToInt32(sTemp.Substring(i - 1, 1))
            If (i Mod 2 = 0) Then
                'even
                iSum += iDigit * 1
            Else
                'odd
                iSum += iDigit * 3
            End If

        Next

        Dim iCheckSum As Integer = (10 - (iSum Mod 10)) Mod 10
        Me.ChecksumDigit = iCheckSum.ToString()

    End Sub


    '	#region -- Attributes/Properties --


    Public Property Name() As String
        Get
            Return _sName
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property MinimumAllowableScale() As Double
        Get
            Return _fMinimumAllowableScale
        End Get
        Set(ByVal value As Double)

        End Set
    End Property

    Public Property MaximumAllowableScale() As Double
        Get
            Return _fMaximumAllowableScale
        End Get
        Set(ByVal value As Double)

        End Set
    End Property
    Public Property Width() As Double
        Get
            Return _fWidth
        End Get
        Set(ByVal value As Double)

        End Set
    End Property

    Public Property Height() As Double
        Get
            Return _fHeight
        End Get
        Set(ByVal value As Double)

        End Set
    End Property

    Public Property FontSize() As Double
        Get
            Return _fFontSize
        End Get
        Set(ByVal value As Double)

        End Set
    End Property

    Public Property Scale() As Double
        Get
            Return _fScale
        End Get
        Set(ByVal value As Double)
            If (value < Me._fMinimumAllowableScale Or value > Me._fMaximumAllowableScale) Then
                Throw New Exception("Scale value out of allowable range.  Value must be between " + Me._fMinimumAllowableScale.ToString() + " and " + Me._fMaximumAllowableScale.ToString())
            End If
            _fScale = value
        End Set
    End Property



    '/// <summary>
    '/// System Description 
    '/// 0 - Regular UPC codes 
    '/// 1 - Reserved 
    '/// 2 - Weightitems marked at the store 
    '/// 3 - National Drug/Health-related code 
    '/// 4 - No format restrictions, in-store use on non-food items 
    '/// 5 - Coupons 
    '/// 6 - Reserved 
    '/// 7 - Regular UPC codes 
    '/// 8 - Reserved 
    '/// 9 - Reserved 
    '/// </summary>
    '/// <value></value>
    Public Property ProductType() As String
        Get
            Return _sProductType
        End Get
        Set(ByVal value As String)
            Dim iTemp As Integer = Convert.ToInt32(value)
            If (iTemp = 1 Or iTemp = 6 Or iTemp > 7) Then
                Throw New Exception(value + " is a reserved Product Type. ")
            End If
            _sProductType = value

        End Set
    End Property


    Public Property ManufacturerCode() As String
        Get
            Return _sManufacturerCode
        End Get
        Set(ByVal value As String)
            If (value.Length <> 5) Then
                Throw New Exception("The manufacturer number must be 5 digits.")
            End If
            _sManufacturerCode = value

        End Set
    End Property

    Public Property ProductCode() As String
        Get
            Return _sProductCode
        End Get
        Set(ByVal value As String)
            If (value.Length <> 5) Then

                Throw New Exception("The product identification number must be 5 digits.")
            End If
            _sProductCode = value

        End Set
    End Property

    Public Property ChecksumDigit() As String
        Get
            Return _sChecksumDigit
        End Get
        Set(ByVal value As String)
            Dim iValue As Integer = Convert.ToInt32(value)
            If (iValue < 0 Or iValue > 9) Then
                Throw New Exception("The Check Digit must be between 0 and 9.")
            End If
            _sChecksumDigit = value
        End Set
    End Property


    '#endregion -- Attributes/Properties --




End Class
