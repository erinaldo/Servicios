Public Class dbEAN13

    Private _sName As String = "EAN13"
    Private _fMinimumAllowableScale As Double = 0.8
    Private _fMaximumAllowableScale As Double = 2.0

    Private _fWidth As Double = 37.29
    Private _fHeight As Double = 25.93
  
    Private _fFontSize As Double = 8.0
    Private _fScale As Double = 1.0

    Private _aOddLeft As String() = {"0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011"}
    Private _aEvenLeft As String() = {"0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111"}
    Private _aRight As String() = {"1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100"}

    Private _sQuiteZone As String = "000000000"
    Private _sLeadTail As String = "101"
    Private _sSeparator As String = "01010"
    Private _sCountryCode As String = "00"
    Private _sManufacturerCode As String
    Private _sProductCode As String
    Private _sChecksumDigit As String

    Public Sub Ean13(ByVal mfgNumber As String, ByVal productId As String)
        Me.CountryCode = "00"
        Me.ManufacturerCode = mfgNumber
        Me.ProductCode = productId
        Me.CalculateChecksumDigit()
    End Sub

    Public Sub Ean13(ByVal countryCode As String, ByVal mfgNumber As String, ByVal productId As String)

        Me.CountryCode = countryCode
        Me.ManufacturerCode = mfgNumber
        Me.ProductCode = productId
        Me.CalculateChecksumDigit()

    End Sub

    Public Sub Ean13(ByVal countryCode As String, ByVal mfgNumber As String, ByVal productId As String, ByVal checkDigit As String)
        Me.CountryCode = countryCode
        Me.ManufacturerCode = mfgNumber
        Me.ProductCode = productId
        Me.ChecksumDigit = checkDigit
    End Sub


    Public Function DrawEan13Barcode(ByVal g As System.Drawing.Graphics, ByVal pt As System.Drawing.Point, ByVal objPicBox As PictureBox) As Image

        Dim width As Double = Me.Width * Me.Scale
        Dim height As Double = Me.Height * Me.Scale
        'ANADIR EXPERIMIENTO
        ' Dim g As System.Drawing.Graphics
        'Dim img As New Bitmap(302, 219, Drawing.Imaging.PixelFormat.Format24bppRgb)
        Dim img As New Bitmap(291, 189, Drawing.Imaging.PixelFormat.Format24bppRgb)
        g = Graphics.FromImage(img)

        'TERMINAR
        Dim lineWidth As Double = width / 113.0F
        Dim gs As System.Drawing.Drawing2D.GraphicsState = g.Save()

        g.PageUnit = System.Drawing.GraphicsUnit.Millimeter
        g.PageScale = 1

        Dim brush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(System.Drawing.Color.Black)
        Dim xPosition As Double = 0

        Dim strbEAN13 As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim sbTemp As System.Text.StringBuilder = New System.Text.StringBuilder()

        Dim xStart As Double = pt.X
        Dim yStart As Double = pt.Y
        Dim xEnd As Double = 0

        Dim font As System.Drawing.Font = New System.Drawing.Font("Arial", Me._fFontSize * Me.Scale)
        g.FillRectangle(Brushes.White, 0, 0, 291, 189)
        Me.CalculateChecksumDigit()

        sbTemp.AppendFormat("{0}{1}{2}{3}", Me.CountryCode, Me.ManufacturerCode, Me.ProductCode, Me.ChecksumDigit)


        Dim sTemp As String = sbTemp.ToString()

        Dim sLeftPattern As String = ""

        sLeftPattern = ConvertLeftPattern(sTemp.Substring(0, 7))

        strbEAN13.AppendFormat("{0}{1}{2}{3}{4}{1}{0}", Me._sQuiteZone, Me._sLeadTail, sLeftPattern, Me._sSeparator, ConvertToDigitPatterns(sTemp.Substring(7), Me._aRight))

        Dim sTempUPC As String = strbEAN13.ToString()

        Dim fTextHeight As Double = g.MeasureString(sTempUPC, font).Height


        For i As Integer = 0 To strbEAN13.Length - 1
            If sTempUPC.Substring(i, 1) = "1" Then
                If xStart = pt.X Then
                    xStart = xPosition
                End If


                If (i > 12 And i < 55) Or (i > 57 And i < 101) Then
                    g.FillRectangle(brush, CSng(xPosition), CSng(yStart), CSng(lineWidth), CSng(height - fTextHeight))
                Else

                    g.FillRectangle(brush, CSng(xPosition), CSng(yStart), CSng(lineWidth), CSng(height))
                End If
            End If
            xPosition += lineWidth
            xEnd = xPosition
        Next


        xPosition = xStart - g.MeasureString(Me.CountryCode.Substring(0, 1), font).Width
        Dim yPosition As Double = yStart + (height - fTextHeight)


        g.DrawString(sTemp.Substring(0, 1), font, brush, New System.Drawing.PointF(xPosition, yPosition))

        xPosition += (g.MeasureString(sTemp.Substring(0, 1), font).Width + 43 * lineWidth) - (g.MeasureString(sTemp.Substring(1, 6), font).Width)


        g.DrawString(sTemp.Substring(1, 6), font, brush, New System.Drawing.PointF(xPosition, yPosition))

        xPosition += g.MeasureString(sTemp.Substring(1, 6), font).Width + (11 * lineWidth)


        g.DrawString(sTemp.Substring(7), font, brush, New System.Drawing.PointF(xPosition, yPosition))


        g.Restore(gs)
        'EXPERIMENTO
        objPicBox.Image = img
        ' Dim mimag As Graphics = Graphics.FromImage(objPicBox.Image)
        'END 
        Return img
    End Function

    ' Public Function CreateBitmap() As System.Drawing.Bitmap
    '     float tempWidth = ( this.Width * this.Scale ) * 100 ;
    'float tempHeight = ( this.Height * this.Scale ) * 100;

    'System.Drawing.Bitmap bmp = new System.Drawing.Bitmap( (int)tempWidth, (int)tempHeight );

    'System.Drawing.Graphics g = System.Drawing.Graphics.FromImage( bmp );
    'this.DrawEan13Barcode( g, new System.Drawing.Point( 0, 0 ) );
    'g.Dispose( );
    'return bmp;
    ' End Function

    Private Function ConvertLeftPattern(ByVal sLeft As String) As String
        ' Switch(sLeft.Substring(0, 1))
        Select Case sLeft.Substring(0, 1)
            Case "0"
                Return CountryCode0(sLeft.Substring(1))

            Case "1"
                Return CountryCode1(sLeft.Substring(1))

            Case "2"
                Return CountryCode2(sLeft.Substring(1))

            Case "3"
                Return CountryCode3(sLeft.Substring(1))

            Case "4"
                Return CountryCode4(sLeft.Substring(1))

            Case "5"
                Return CountryCode5(sLeft.Substring(1))

            Case "6"
                Return CountryCode6(sLeft.Substring(1))

            Case "7"
                Return CountryCode7(sLeft.Substring(1))

            Case "8"
                Return CountryCode8(sLeft.Substring(1))

            Case "9"
                Return CountryCode9(sLeft.Substring(1))

            Case Else
                Return ""
        End Select
    End Function
    Private Function CountryCode0(ByVal sLeft As String) As String

        Return ConvertToDigitPatterns(sLeft, Me._aOddLeft)

    End Function
    Private Function CountryCode1(ByVal sLeft As String) As String

        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()

        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aOddLeft))

        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aEvenLeft))
        Return sReturn.ToString()

    End Function

    Private Function CountryCode2(ByVal sLeft As String) As String
        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()

        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aEvenLeft))
        Return sReturn.ToString()

    End Function

    Private Function CountryCode3(ByVal sLeft As String) As String

        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aOddLeft))
        Return sReturn.ToString()

    End Function

    Private Function CountryCode4(ByVal sLeft As String) As String
        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aEvenLeft))
        Return sReturn.ToString()
    End Function

    Private Function CountryCode5(ByVal sLeft As String) As String
        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aEvenLeft))
        Return sReturn.ToString()
    End Function

    Private Function CountryCode6(ByVal sLeft As String) As String
        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aOddLeft))
        Return sReturn.ToString()
    End Function

    Private Function CountryCode7(ByVal sLeft As String) As String
        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aEvenLeft))
        Return sReturn.ToString()
    End Function
    Private Function CountryCode8(ByVal sLeft As String) As String
        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aOddLeft))
        Return sReturn.ToString()

    End Function
    Private Function CountryCode9(ByVal sLeft As String) As String
        Dim sReturn As System.Text.StringBuilder = New System.Text.StringBuilder()
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), Me._aOddLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), Me._aEvenLeft))
        sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), Me._aOddLeft))
        Return sReturn.ToString()
    End Function
    Private Function ConvertToDigitPatterns(ByVal inputNumber As String, ByVal patterns As String()) As String
        Dim sbTemp As System.Text.StringBuilder = New System.Text.StringBuilder()
        Dim iIndex As Integer = 0
        For i As Integer = 0 To inputNumber.Length - 1
            iIndex = Convert.ToInt32(inputNumber.Substring(i, 1))
            sbTemp.Append(patterns(iIndex))
        Next
        Return sbTemp.ToString()
    End Function
    Public Sub CalculateChecksumDigit()
        Dim sTemp As String = Me.CountryCode + Me.ManufacturerCode + Me.ProductCode
        Dim iSum As Integer = 0
        Dim iDigit As Integer = 0

        For i As Integer = sTemp.Length To 1 Step -1
            iDigit = Convert.ToInt32(sTemp.Substring(i - 1, 1))
            If i Mod 2 = 0 Then
                iSum += iDigit * 3
            Else
                iSum += iDigit * 1
            End If
    			
        Next
    		

        Dim iCheckSum As Integer = (10 - (iSum Mod 10)) Mod 10
        Me.ChecksumDigit = iCheckSum.ToString()
    End Sub

#Region "Attributes/Properties"

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
            If value < Me._fMinimumAllowableScale Or value > Me._fMaximumAllowableScale Then
                Throw New Exception("Scale value out of allowable range.  Value must be between " + Me._fMinimumAllowableScale.ToString() + " and " + Me._fMaximumAllowableScale.ToString())
            End If
            _fScale = value
        End Set
    End Property
 
    Public Property CountryCode() As String
        Get
            Return _sCountryCode
        End Get
        Set(ByVal value As String)
            While value.Length < 2
                value = "0" + value
            End While
    		
            _sCountryCode = value
        End Set
    End Property
    	
    Public Property ManufacturerCode() As String
        Get
            Return _sManufacturerCode
        End Get
        Set(ByVal value As String)
           _sManufacturerCode = value
        End Set
    End Property
    Public Property ProductCode() As String
        Get
            Return _sProductCode
        End Get
        Set(ByVal value As String)
            _sProductCode = value
        End Set
    End Property
    Public Property ChecksumDigit() As String
        Get
            Return _sChecksumDigit
        End Get
        Set(ByVal value As String)
            Dim iValue As Integer = Convert.ToInt32(value)
            If iValue < 0 Or iValue > 9 Then
                Throw New Exception("The Check Digit must be between 0 and 9.")
            End If
            _sChecksumDigit = value
        End Set
    End Property
   

#End Region


End Class
