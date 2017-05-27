Public Class CLetrasIngles
    Public Function ConvertMoneyToText(ByVal value As String) As String

        'Takes a string value that translates as a decimal value of up to one pentillion (?) dollars.  
        'Note: Removing the comma for European currency will remove the decimal pointer.  

        value = value.Replace(",", "").Replace("$", "")
        value = value.TrimStart("0")

        Dim decimalCount As Int32 = 0
        For x As Int32 = 0 To value.Length - 1
            If value(x).ToString = "." Then
                decimalCount += 1
                If decimalCount > 1 Then Throw New ArgumentException("Only monetary values are accepted")
            End If

            If Not (Char.IsDigit(value(x)) Or value(x).ToString = ".") And Not (x = 0 And value(x).ToString = "-") Then
                Throw New ArgumentException("Only monetary values are accepted")
            End If
        Next

        Dim returnValue As String = ""
        Dim parts() As String = value.Split(".")

        If parts.Length > 1 Then
            parts(1) = parts(1).Substring(0, 2).ToCharArray 'Truncates -- doesn't round.  
        End If

        Dim IsNegative As Boolean = parts(0).Contains("-")
        If parts(0).Replace("-", "").Length > 18 Then
            Throw New ArgumentException("Maximum value is $999,999,999,999,999,999.99")
        End If

        If IsNegative Then
            parts(0) = parts(0).Replace("-", "")
            returnValue &= "Minus "
        End If

        'If you know the names of what is beyond quadrillion and feel the urge to do the  
        ' rediculous just follow the pattern below.  
        If parts(0).Length > 15 Then
            returnValue &= HundredsText(parts(0).PadLeft(18, "0").Substring(0, 3)) & "QUADRILLION "
            returnValue &= HundredsText(parts(0).PadLeft(18, "0").Substring(3, 3)) & "TRILLION "
            returnValue &= HundredsText(parts(0).PadLeft(18, "0").Substring(6, 3)) & "BILLION "
            returnValue &= HundredsText(parts(0).PadLeft(18, "0").Substring(9, 3)) & "MILLION "
            returnValue &= HundredsText(parts(0).PadLeft(18, "0").Substring(12, 3)) & "THOUSAND "
        ElseIf parts(0).Length > 12 Then
            returnValue &= HundredsText(parts(0).PadLeft(15, "0").Substring(0, 3)) & "TRILLION "
            returnValue &= HundredsText(parts(0).PadLeft(15, "0").Substring(3, 3)) & "BILLION "
            returnValue &= HundredsText(parts(0).PadLeft(15, "0").Substring(6, 3)) & "MILLION "
            returnValue &= HundredsText(parts(0).PadLeft(15, "0").Substring(9, 3)) & "THOUSAND "
        ElseIf parts(0).Length > 9 Then
            returnValue &= HundredsText(parts(0).PadLeft(12, "0").Substring(0, 3)) & "BILLION "
            returnValue &= HundredsText(parts(0).PadLeft(12, "0").Substring(3, 3)) & "MILLION "
            returnValue &= HundredsText(parts(0).PadLeft(12, "0").Substring(6, 3)) & "THOUSAND "
        ElseIf parts(0).Length > 6 Then
            returnValue &= HundredsText(parts(0).PadLeft(9, "0").Substring(0, 3)) & "MILLION "
            returnValue &= HundredsText(parts(0).PadLeft(9, "0").Substring(3, 3)) & "THOUSAND "
        ElseIf parts(0).Length > 3 Then
            returnValue &= HundredsText(parts(0).PadLeft(6, "0").Substring(0, 3)) & "THOUSAND "
        End If

        Dim hundreds As String = parts(0).PadLeft(3, "0")
        hundreds = hundreds.Substring(hundreds.Length - 3, 3)
        If CInt(hundreds) <> 0 Then
            If CInt(hundreds) < 100 And parts.Length > 1 And returnValue <> "" Then returnValue &= "AND "
            returnValue &= HundredsText(hundreds)
            'If CInt(hundreds) <> 1 Then returnValue &= "s"
            'If parts.Length > 1 AndAlso CInt(parts(1)) <> 0 Then returnValue &= " and "
        Else
            'returnValue &= " No Dollars"
            'If parts.Length > 1 AndAlso CInt(parts(1)) <> 0 Then returnValue &= " and "
        End If

        'If parts.Length = 2 Then
        '    If CInt(parts(1)) <> 0 Then
        '        returnValue &= HundredsText(parts(1).PadLeft(3, "0"))
        '        returnValue &= "Cent"
        '        If CInt(parts(1)) <> 1 Then returnValue &= "s"
        '    End If
        'End If

        Return returnValue

    End Function

    Private Function HundredsText(ByVal value As String) As String

        Dim Tens As String() = {"TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTTY", "NINETY"}
        Dim Ones As String() = {"ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"}

        Dim returnValue As String = ""
        Dim IsSingleDigit As Boolean = True

        If CInt(value(0).ToString) <> 0 Then
            returnValue &= Ones(CInt(value(0).ToString) - 1) & " HUNDRED "
            IsSingleDigit = False
        End If

        If CInt(value(1).ToString) > 1 Then
            returnValue &= "AND " + Tens(CInt(value(1).ToString) - 1) & " "
            If CInt(value(2).ToString) <> 0 Then
                returnValue &= Ones(CInt(value(2).ToString) - 1) & " "
            End If
        ElseIf CInt(value(1).ToString) = 1 Then
            returnValue &= "AND " + Ones(CInt(value(1).ToString & value(2).ToString) - 1) & " "
        Else
            If CInt(value(2).ToString) <> 0 Then
                If Not IsSingleDigit Then
                    returnValue &= "AND "
                End If
                returnValue &= Ones(CInt(value(2).ToString) - 1) & " "
            End If
        End If

        Return returnValue

    End Function

End Class
