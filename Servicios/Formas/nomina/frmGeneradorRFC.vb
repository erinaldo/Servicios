Public Class frmGeneradorRFC
    
    Public RFC As String
    Public Function pMoral(ByVal Nombre As String, ByVal fCreacion As Date)
        Dim strArr() As String 'Arreglo con las palabras del nombre
        Dim listaNombres As List(Of String) = New List(Of String) 'Lista palabras del nombre
        Dim count As Integer 'Contador para ver palabras del nombre
        Dim count2 As Integer ' contador para recorrer la lista de conjunciones
        Dim size As Integer 'tamaño del arreglo
        Dim numerica As String
        Dim alfabetica As String = ""
        Dim RFC As String = ""
        Dim listaConjunciones As List(Of String) = New List(Of String) 'Lista de conjunciones
        listaConjunciones.Add("s.a.")
        listaConjunciones.Add("s.")
        listaConjunciones.Add("n.s.")
        listaConjunciones.Add("n.c.")
        listaConjunciones.Add("c.v.")
        listaConjunciones.Add("r.l.")
        listaConjunciones.Add("s.c.l.")
        listaConjunciones.Add("s.c.s.")
        listaConjunciones.Add("s.r.l.")
        listaConjunciones.Add("s.n.c.")
        listaConjunciones.Add("c.")
        listaConjunciones.Add("m.i.")
        listaConjunciones.Add("a.")
        listaConjunciones.Add("p.")
        listaConjunciones.Add("m.c.")
        listaConjunciones.Add("a.c.")
        listaConjunciones.Add("Compañía")
        listaConjunciones.Add("Cía.")
        listaConjunciones.Add("Compañia")
        listaConjunciones.Add("Cia.")
        listaConjunciones.Add("Company")
        listaConjunciones.Add("Sociedad")
        listaConjunciones.Add("cooperativa")
        listaConjunciones.Add("Soc.")
        listaConjunciones.Add("y")
        listaConjunciones.Add("en")
        listaConjunciones.Add("o")
        listaConjunciones.Add("e")
        listaConjunciones.Add("u")
        listaConjunciones.Add("of")
        listaConjunciones.Add("si")
        listaConjunciones.Add("pues")
        listaConjunciones.Add("mas")
        listaConjunciones.Add("de")
        listaConjunciones.Add("von")
        listaConjunciones.Add("como")
        listaConjunciones.Add("del")
        listaConjunciones.Add("las")
        listaConjunciones.Add("que")
        listaConjunciones.Add("a")
        listaConjunciones.Add("mi")
        listaConjunciones.Add("unos")
        listaConjunciones.Add("un")
        listaConjunciones.Add("al")
        listaConjunciones.Add("lo")
        listaConjunciones.Add("una")
        listaConjunciones.Add("el")
        listaConjunciones.Add("en")
        listaConjunciones.Add("un")
        listaConjunciones.Add("la")
        listaConjunciones.Add("los")
        listaConjunciones.Add("para")
        listaConjunciones.Add("por")
        listaConjunciones.Add("sin")
        listaConjunciones.Add("porque")
        'Alfabetica STARTS HERE
        strArr = Nombre.Split(New [Char]() {CChar(vbTab), CChar(" "), CChar("-")}) 'Splitter del nombre
        listaNombres.AddRange(strArr) 'Se agregan las palabras del nombre a una lista para mejor manejo
        'For count = 0 To strArr.Length - 1 '------->>>impresion de nombres
        '    MsgBox(strArr(count))
        'Next
        size = strArr.Length 'Se le asigna el valor al Size
        Console.WriteLine(size) 'Checar el size
        For count = 0 To strArr.Length - 1 'Aqui se hace la lista de palabras que no contiene conjunciones
            For count2 = 0 To listaConjunciones.Count - 1
                If String.Equals(strArr(count), listaConjunciones(count2), StringComparison.InvariantCultureIgnoreCase) Then
                    MsgBox(strArr(count) & count2)
                    listaNombres.Remove(strArr(count))
                End If
            Next
            'AQUI YA ESTA LA LISTA CON PALABRAS SIN CONJUNCIONES LISTA ^
        Next
        For contador As Integer = 0 To listaNombres.Count - 1
            'Aqui se le quitan los puntos a todas las palabras del nombre.
            listaNombres(contador) = listaNombres(contador).Replace(".", "")
        Next
        If listaNombres.Count >= 3 Then 'Si el nombre contiene mas de 3 palabras
            alfabetica = listaNombres(0).Substring(0, 1) + listaNombres(1).Substring(0, 1) + listaNombres(2).Substring(0, 1)
            MsgBox(alfabetica)
        ElseIf listaNombres.Count = 2 Then 'Si el nombre cuenta con 2 palabras solamente
            alfabetica = listaNombres(0).Substring(0, 1) + listaNombres(1).Substring(0, 2)
            MsgBox(alfabetica + "aqui")
        ElseIf listaNombres.Count = 1 Then 'Si el nombre cuenta con una palabra solamente
            If listaNombres(0).Length >= 3 Then 'En estos if se checa la longitud de la palabra en caso de ser solo una
                alfabetica = listaNombres(0).Substring(0, 3) 'Esto para agregar las respectivas "X" para completar
                MsgBox(alfabetica + "Entro")
            ElseIf listaNombres(0).Length = 2 Then
                MsgBox("Longitud de palabra = 2")
                alfabetica = listaNombres(0).Substring(0, 2) + "X"
                MsgBox(alfabetica + "EntroX")
            ElseIf listaNombres(0).Length = 1 Then
                MsgBox("Longitud de palabra = 1")
                alfabetica = listaNombres(0).Substring(0, 1) + "XX"
                MsgBox(alfabetica + "EntroXX")
            End If
        End If

        alfabetica = alfabetica.ToUpper
        'Alfabetica ENDS HERE

        'Númerica STARTS HERE
        Dim dia As String = fCreacion.Day.ToString() 'Se agrega un 0 si es un solo caracter
        If dia.Length = 1 Then
            dia = "0" + dia
        End If
        Dim mes As String = fCreacion.Month.ToString() 'Se agrega un 0 si es un solo caracter
        If mes.Length = 1 Then
            mes = "0" + mes
        End If
        Dim año As String = fCreacion.Year.ToString()
        año = año.Substring(Math.Max(0, año.Length - 2))
        numerica = año + mes + dia
        MsgBox(numerica)
        'Númerica ENDS HERE
        Console.WriteLine(listaNombres.Count)
        For Each Nombre In listaNombres
            Console.WriteLine(Nombre)
        Next
        'RFC se generá aqui
        RFC = alfabetica + numerica
        MsgBox(RFC)
        Return RFC
    End Function
    Public Function pFisica(ByVal Nombre As String, ByVal aPaterno As String, ByVal AMaterno As String, ByVal fNacimiento As Date)
        Dim strArr() As String 'Arreglo con las palabras del nombre
        Dim paternoArr() As String 'Arreglo con las palabras del apellido paterno
        Dim maternoArr() As String 'Arreglo con las palabras del apellido materno
        Dim listaNombres As List(Of String) = New List(Of String) 'Lista palabras del nombre
        Dim listaPaterno As List(Of String) = New List(Of String) 'Lista palabras del paterno
        Dim listaMaterno As List(Of String) = New List(Of String) 'Lista palabras del materno
        Dim count As Integer 'Contador para ver palabras del nombre
        Dim count2 As Integer ' contador para recorrer la lista de conjunciones
        Dim size As Integer 'tamaño del arreglo
        Dim pVocal As Char
        Dim numerica As String
        Nombre = Nombre.ToLower
        aPaterno = aPaterno.ToLower
        AMaterno = AMaterno.ToLower
        Dim alfabetica As String = ""
        Dim RFC As String = ""
        Dim listaConjunciones As List(Of String) = New List(Of String) 'Lista de conjunciones
        listaConjunciones.Add("la")
        listaConjunciones.Add("s.a.")
        listaConjunciones.Add("de")
        listaConjunciones.Add("c.v.")
        listaConjunciones.Add("los")
        listaConjunciones.Add("y")
        listaConjunciones.Add("c.i.a.")
        listaConjunciones.Add("soc")
        listaConjunciones.Add("coop")
        listaConjunciones.Add("a.")
        listaConjunciones.Add("en")
        listaConjunciones.Add("p.")
        listaConjunciones.Add("s.")
        listaConjunciones.Add("c.")
        listaConjunciones.Add("con")
        listaConjunciones.Add("sus")
        listaConjunciones.Add("s.c.")
        listaConjunciones.Add("s.c.s.")
        listaConjunciones.Add("the")
        listaConjunciones.Add("and")
        listaConjunciones.Add("co")
        listaConjunciones.Add("mac")
        listaConjunciones.Add("van")
        listaConjunciones.Add("a")
        listaConjunciones.Add("m.i.")
        listaConjunciones.Add("compañia")
        listaConjunciones.Add("compañía")
        listaConjunciones.Add("s.r.l.")
        listaConjunciones.Add("las")
        listaConjunciones.Add("m.c.")
        listaConjunciones.Add("von")
        listaConjunciones.Add("del")
        listaConjunciones.Add("mi")

        strArr = Nombre.Split(New [Char]() {CChar(vbTab), CChar(" "), CChar("-"), CChar("'")}) 'Splitter del nombre
        listaNombres.AddRange(strArr) 'Se agregan las palabras del nombre a una lista para mejor manejo
        paternoArr = aPaterno.Split(New [Char]() {CChar(vbTab), CChar(" "), CChar("-"), CChar("'")}) 'Splitter del apellido paterno
        listaPaterno.AddRange(paternoArr) 'Se agregan las palabras del nombre a una lista para mejor manejo
        maternoArr = AMaterno.Split(New [Char]() {CChar(vbTab), CChar(" "), CChar("-"), CChar("'")}) 'Splitter del apellido materno
        listaMaterno.AddRange(maternoArr) 'Se agregan las palabras del nombre a una lista para mejor manejo
        'For count = 0 To strArr.Length - 1 '------->>>impresion de nombres
        '    MsgBox(strArr(count))
        'Next
        size = strArr.Length 'Se le asigna el valor al Size
        'Console.WriteLine(size) 'Checar el size
        '-------------------------------------------------Listas de nombre, apellidos
        For count = 0 To strArr.Length - 1 'Aqui se hace la lista de palabras que no contiene conjunciones
            For count2 = 0 To listaConjunciones.Count - 1
                If String.Equals(strArr(count), listaConjunciones(count2), StringComparison.InvariantCultureIgnoreCase) Then
                    'MsgBox(strArr(count) & count2)
                    listaNombres.Remove(strArr(count))
                End If
            Next
        Next
        For count = 0 To paternoArr.Length - 1 'Se limpia el apellido paterno de conjunciones
            For count2 = 0 To listaConjunciones.Count - 1
                If String.Equals(paternoArr(count), listaConjunciones(count2), StringComparison.InvariantCultureIgnoreCase) Then
                    'MsgBox(paternoArr(count) & count2)
                    listaPaterno.Remove(paternoArr(count))
                End If
            Next
        Next
        For count = 0 To maternoArr.Length - 1 'Se limpia el apellido materno de conjunciones
            For count2 = 0 To listaConjunciones.Count - 1
                If String.Equals(maternoArr(count), listaConjunciones(count2), StringComparison.InvariantCultureIgnoreCase) Then
                    'MsgBox(maternoArr(count) & count2)
                    listaMaterno.Remove(maternoArr(count))
                End If
            Next
        Next
        '-------------------------------------AQUI YA ESTAn LAs LISTAs CON PALABRAS SIN CONJUNCIONES LISTA ^
        '---------------------Checa paterno    v
        If aPaterno <> Nothing Then
            pVocal = primerVocal(aPaterno)
            'MsgBox("Primer vocal de apellido paterno: " & pVocal)
        Else
            'MsgBox("No hay apellido paterno")
        End If '   ^---tERMINA CHEQUEO DEL APELLIDO PATERNO
        'ALFABETICA STARTS HERE
        Dim caracteresPaterno = listaPaterno(0).Length
        Dim numNombres = listaNombres.Count
        'MsgBox("Caracteres del paterno: " & caracteresPaterno)
        If listaNombres(0).Contains("JOSE") Then  'QUITAMOS EL NOMBRE JOSE SI SE ENCUENTRA EN LA PRIMER POSICION
            'MsgBox("CONTIENE JOSE")
            If numNombres > 1 Then
                listaNombres.Remove("JOSE")
                numNombres = listaNombres.Count
            End If
        End If
        If listaNombres(0).Contains("MARÍA") Then 'QUITAMOS EL NOMBRE MARIA SI SE ENCUENTRA EN LA PRIMER POSICION
            'MsgBox("CONTIENE MARÍA")
            If numNombres > 1 Then
                listaNombres.Remove("MARÍA")
                numNombres = listaNombres.Count
            End If
        End If
        If listaNombres(0).Contains("MARIA") Then 'QUITAMOS EL NOMBRE MARIA SI SE ENCUENTRA EN LA PRIMER POSICION
            'MsgBox("CONTIENE MARIA")
            If numNombres > 1 Then
                listaNombres.Remove("MARIA")
                numNombres = listaNombres.Count
            End If
        End If
        'AQUI SE GENERA LA CONCATENACION DE LOS CARACTERES PARA LA CLAVE ALFABETICA DEL RFC
        If AMaterno <> Nothing AndAlso aPaterno <> Nothing AndAlso caracteresPaterno > 2 Then 'SI SE TIENE AMBOS APELLIDOS Y EL PATERNO PASA DE LOS 2 CARACTERES
            alfabetica = listaPaterno(0).Substring(0, 1) + pVocal + listaMaterno(0).Substring(0, 1) + listaNombres(0).Substring(0, 1)
            'MsgBox(alfabetica)
        ElseIf AMaterno <> Nothing AndAlso aPaterno <> Nothing AndAlso caracteresPaterno < 2 Then 'SI SE TIENE AMBOS APELLIDOS PERO EL PATERNO NO TIENE SUFICIENTES CARACTERES
            alfabetica = listaPaterno(0).Substring(0, 1) + listaMaterno(0).Substring(0, 1) + listaNombres(0).Substring(0, 1) + listaNombres(0).Substring(1, 1)
        ElseIf aPaterno = "" Then 'NO SE CUENTA CON APELLIDO PATERNO
            alfabetica = listaMaterno(0).Substring(0, 1) + listaMaterno(0).Substring(1, 1) + listaNombres(0).Substring(0, 1) + listaNombres(0).Substring(1, 1)
        ElseIf AMaterno = "" Then 'NO SE CUENTA CON APELLIDO MATERNO
            alfabetica = listaPaterno(0).Substring(0, 1) + listaPaterno(0).Substring(1, 1) + listaNombres(0).Substring(0, 1) + listaNombres(0).Substring(1, 1)
        End If
        'SE TERMINA DE GENERAR LA CLAVE ALFABETICA
        alfabetica = alfabetica.ToUpper() 'SE PASA LA CLAVE A MAYUSCULAS
        'ALFABETICA ENDS HERE

        'Aqui se verifica que la clave alfabetica no contenga una palabra inconveniente
        'Esto para que a la gente no le hagan carrilla cuando vean que su rfj es PNDJ911360WED
        Select Case alfabetica
            Case "BUEI"
                alfabetica = "BUEX"
            Case "BUEY"
                alfabetica = "BUEX"
            Case "CACA"
                alfabetica = "CACX"
            Case "CACO"
                alfabetica = "CACX"
            Case "CAGO"
                alfabetica = "CAGX"
            Case "CAGA"
                alfabetica = "CAGX"
            Case "COGE"
                alfabetica = "COGX"
            Case "COJA"
                alfabetica = "COJX"
            Case "COJE"
                alfabetica = "COJX"
            Case "COJI"
                alfabetica = "COJX"
            Case "COJO"
                alfabetica = "COJX"
            Case "CULO"
                alfabetica = "CULX"
            Case "FETO"
                alfabetica = "FETX"
            Case "GUEY"
                alfabetica = "GUEX"
            Case "JOTO"
                alfabetica = "JOTX"
            Case "KACA"
                alfabetica = "KACX"
            Case "KACO"
                alfabetica = "KACX"
            Case "KAGA"
                alfabetica = "KAGX"
            Case "KAGO"
                alfabetica = "KAGX"
            Case "KOGE"
                alfabetica = "KOGX"
            Case "KOJO"
                alfabetica = "KOJX"
            Case "KAKA"
                alfabetica = "KAKX"
            Case "KULO"
                alfabetica = "KULX"
            Case "MAME"
                alfabetica = "MAMX"
            Case "MAMO"
                alfabetica = "MAMX"
            Case "MEAR"
                alfabetica = "MEAX"
            Case "MEON"
                alfabetica = "MEOX"
            Case "MION"
                alfabetica = "MIOX"
            Case "MOCO"
                alfabetica = "MOCX"
            Case "MULA"
                alfabetica = "MULX"
            Case "PEDA"
                alfabetica = "PEDX"
            Case "PENE"
                alfabetica = "PENX"
            Case "PUTA"
                alfabetica = "PUTX"
            Case "PUTO"
                alfabetica = "PUTX"
            Case "QULO"
                alfabetica = "QULX"
            Case "RATA"
                alfabetica = "RATX"
            Case "RUIN"
                alfabetica = "RUIX"
        End Select
        'MsgBox("ALFABETICA LIMPIA: " & alfabetica) 'CLAVE ALFABETICA SIN PALABRAS INCONVENIENTES
        'Fin de comprobación

        'Númerica STARTS HERE
        Dim dia As String = fNacimiento.Day.ToString() 'Se agrega un 0 si es un solo caracter
        If dia.Length = 1 Then
            dia = "0" + dia
        End If
        Dim mes As String = fNacimiento.Month.ToString() 'Se agrega un 0 si es un solo caracter
        If mes.Length = 1 Then
            mes = "0" + mes
        End If
        Dim año As String = fNacimiento.Year.ToString()
        año = año.Substring(Math.Max(0, año.Length - 2))
        numerica = año + mes + dia
        'MsgBox(numerica)
        'Númerica ENDS HERE
        RFC = alfabetica + numerica 'SE JUNTA LA CLAVE ALFABETICA CON LA NUMERICA PARA GENERAR EL RFC
        'MsgBox(RFC)
        Return RFC
    End Function
    Public Function claveHomonimia(ByVal nombre As String, ByVal aPaterno As String, ByVal aMaterno As String, ByVal fecha As Date)
        Dim homonimia As String = "0" 'Cadena que concatenara los valores de la clave diferenciadora de homonimio
        nombre = nombre + " " + aPaterno + " " + aMaterno 'Cadena con el nombre completo.
        Dim charArray() As Char = nombre.ToCharArray 'Array con todos los chars del nombre (A,r,m,a,n,d,o)
        Dim claveNum() As Char 'Arreglo con los valores numericos de la homonimia
        Dim numMultiplicaciones As Integer = charArray.Length * 2 'Numero de multiplicaciones a realizar
        Dim apuntadorUno As Integer = 0 'Los apuntadores sirven para hacer las multiplicaciones en part
        Dim apuntadorDos As Integer = 1 '-----^   Aquí se inicializan
        Dim pareja As Integer 'Pareja de multiplicacion, solo sirve para imprimir las multiplicaciones en pantalla.
        Dim resultadoMulti As Integer = 0 'Resultado de la multiplicación
        Dim divisor As Integer = 34 'Divisor(Siempre será 34)
        Dim dividendo As Single = 0 'Dividendo
        Dim cociente As Integer = 0 'Cociente de la division
        Dim residuo As Integer 'Residuo de la division
        Dim cocienteCadena As String = "" 'Cadena de los valores para el metodo del sacaClave
        Dim residuoCadena As String = "" '-------^
        Dim cveHomonimia As String = "" 'Resultado de la función

        'START OF VALORES
        'MsgBox("# de multiplicaciones: " & numMultiplicaciones)
        For i As Integer = 0 To charArray.Length - 1
            Select Case charArray(i)
                Case " "
                    homonimia = homonimia + "00"
                Case "0"
                    homonimia = homonimia + "00"
                Case "1"
                    homonimia = homonimia + "01"
                Case "2"
                    homonimia = homonimia + "02"
                Case "3"
                    homonimia = homonimia + "03"
                Case "4"
                    homonimia = homonimia + "04"
                Case "5"
                    homonimia = homonimia + "05"
                Case "6"
                    homonimia = homonimia + "06"
                Case "7"
                    homonimia = homonimia + "07"
                Case "8"
                    homonimia = homonimia + "08"
                Case "9"
                    homonimia = homonimia + "09"
                Case "&"
                    homonimia = homonimia + "10"
                Case "A", "a"
                    homonimia = homonimia + "11"
                Case "B", "b"
                    homonimia = homonimia + "12"
                Case "C", "c"
                    homonimia = homonimia + "13"
                Case "D", "d"
                    homonimia = homonimia + "14"
                Case "E", "e"
                    homonimia = homonimia + "15"
                Case "F", "f"
                    homonimia = homonimia + "16"
                Case "G", "g"
                    homonimia = homonimia + "17"
                Case "H", "h"
                    homonimia = homonimia + "18"
                Case "I", "i"
                    homonimia = homonimia + "19"
                Case "J", "j"
                    homonimia = homonimia + "21"
                Case "K", "k"
                    homonimia = homonimia + "22"
                Case "L", "l"
                    homonimia = homonimia + "23"
                Case "M", "m"
                    homonimia = homonimia + "24"
                Case "N", "n"
                    homonimia = homonimia + "25"
                Case "O", "o"
                    homonimia = homonimia + "26"
                Case "P", "p"
                    homonimia = homonimia + "27"
                Case "Q", "q"
                    homonimia = homonimia + "28"
                Case "R", "r"
                    homonimia = homonimia + "29"
                Case "S", "s"
                    homonimia = homonimia + "32"
                Case "T", "t"
                    homonimia = homonimia + "33"
                Case "U", "u"
                    homonimia = homonimia + "34"
                Case "V", "v"
                    homonimia = homonimia + "35"
                Case "W", "w"
                    homonimia = homonimia + "36"
                Case "X", "x"
                    homonimia = homonimia + "37"
                Case "Y", "y"
                    homonimia = homonimia + "38"
                Case "Z", "z"
                    homonimia = homonimia + "39"
                Case "Ñ", "ñ"
                    homonimia = homonimia + "40"
            End Select
        Next
        'MsgBox("Clave Homonimia: " & homonimia)
        'End of VALORES
        claveNum = homonimia.ToCharArray 'Se asignan los valores numericos de la homonimia porque ya esta lista
        For i As Integer = 0 To numMultiplicaciones - 1
            'MsgBox("Multiplicacion de:" & CInt(AscW(claveNum(apuntadorUno))) - 48 & CInt(AscW(claveNum(apuntadorDos))) - 48 & "Y" & CInt(AscW(claveNum(apuntadorDos))) - 48)
            pareja = CInt(claveNum(apuntadorUno) + claveNum(apuntadorDos))
            'MsgBox("Multiplicación: " & pareja & "X" & CInt(AscW(claveNum(apuntadorDos))) - 48 & " :" & pareja * (CInt(AscW(claveNum(apuntadorDos))) - 48))

            resultadoMulti = resultadoMulti + pareja * (CInt(AscW(claveNum(apuntadorDos))) - 48)
            apuntadorUno = apuntadorUno + 1 'Aumentan los apuntadores
            apuntadorDos = apuntadorDos + 1 '---^
        Next
        'MsgBox(resultadoMulti)
        Dim multiString As String = resultadoMulti.ToString 'Resultado de la multiplicacion en string para el sig. paso
        dividendo = multiString.Substring(multiString.Length - 3) 'Se sacan los ultimos 3 valores del resultado de la multiplicación
        residuo = dividendo Mod divisor  'Se saca el residuo
        'MsgBox("Residuo: " & residuo)  'Imprime residuo
        cociente = Math.Floor(dividendo / divisor) 'Se saca el cociente
        'MsgBox("Cociente: " & cociente) 'Imprime el cociente

        residuoCadena = sacaClave(residuo.ToString) 'Se obtiene la clave correspondiente al residuo
        cocienteCadena = sacaClave(cociente.ToString) 'Se obtiene la clave correspondiente al cociente
        'MsgBox("Cociente: " & cocienteCadena & "Residuo: " & residuoCadena)
        cveHomonimia = cocienteCadena + residuoCadena 'Se junta la clave homonimia(2 digitos)
        'MsgBox("Clave Homonimia:" & cveHomonimia)

        Return cveHomonimia 'Returna la clave Homonimia
    End Function
    Public Function digitoVerificador(ByVal rfc As String, ByVal fecha As Date)
        Dim charArray() As Char = rfc.ToCharArray 'Arreglo de char que contiene todos los caracteres del rfc para hacer uso del select case
        Dim listaNumeros As List(Of String) = New List(Of String) 'Lista pares de digitos para la ecuación
        Dim strArr() As String  'Arreglo utilizado para separar la cadena de digitos producida a partir del nombre en el select case
        Dim resultadoMulti As Integer = 0 'resultado de la multiplicacion
        Dim residuo As Integer = 0 'Residuo de la multiplicacion
        Dim divisor As Integer = 11 'Divisor de la multiplicacion, siempre será 11
        Dim rfcDigitos As String = "" 'cadena que concatena los digitos que se asignan a cada letra del RFC
        Dim pi As Integer = 13 'variable utilizada para el calculo del digito verificador, siempre será 13
        Dim digVerificador As String = "" 'Resultado del metodo
        Dim añoNacimiento As Integer = fecha.Year
        'PASO #!
        For i As Integer = 0 To charArray.Length - 1 'Se recorre el RFC asignando los valores que corresponden a cada caracter del mismo.
            Select Case charArray(i)
                Case " "
                    rfcDigitos = rfcDigitos + "37 " 'Siempre se asigna un ""(Espacio) entre digitos para poder hacer uso de los datos más adelante mediante un split
                Case "0"
                    rfcDigitos = rfcDigitos + "00 "
                Case "1"
                    rfcDigitos = rfcDigitos + "01 "
                Case "2"
                    rfcDigitos = rfcDigitos + "02 "
                Case "3"
                    rfcDigitos = rfcDigitos + "03 "
                Case "4"
                    rfcDigitos = rfcDigitos + "04 "
                Case "5"
                    rfcDigitos = rfcDigitos + "05 "
                Case "6"
                    rfcDigitos = rfcDigitos + "06 "
                Case "7"
                    rfcDigitos = rfcDigitos + "07 "
                Case "8"
                    rfcDigitos = rfcDigitos + "08 "
                Case "9"
                    rfcDigitos = rfcDigitos + "09 "
                Case "A"
                    rfcDigitos = rfcDigitos + "10 "
                Case "B"
                    rfcDigitos = rfcDigitos + "11 "
                Case "C"
                    rfcDigitos = rfcDigitos + "12 "
                Case "D"
                    rfcDigitos = rfcDigitos + "13 "
                Case "E"
                    rfcDigitos = rfcDigitos + "14 "
                Case "F"
                    rfcDigitos = rfcDigitos + "15 "
                Case "G"
                    rfcDigitos = rfcDigitos + "16 "
                Case "H"
                    rfcDigitos = rfcDigitos + "17 "
                Case "I"
                    rfcDigitos = rfcDigitos + "18 "
                Case "J"
                    rfcDigitos = rfcDigitos + "19 "
                Case "K"
                    rfcDigitos = rfcDigitos + "20 "
                Case "L"
                    rfcDigitos = rfcDigitos + "21 "
                Case "M"
                    rfcDigitos = rfcDigitos + "22 "
                Case "N"
                    rfcDigitos = rfcDigitos + "23 "
                Case "&"
                    rfcDigitos = rfcDigitos + "24 "
                Case "O"
                    rfcDigitos = rfcDigitos + "25 "
                Case "P"
                    rfcDigitos = rfcDigitos + "26 "
                Case "Q"
                    rfcDigitos = rfcDigitos + "27 "
                Case "R"
                    rfcDigitos = rfcDigitos + "28 "
                Case "S"
                    rfcDigitos = rfcDigitos + "29 "
                Case "T"
                    rfcDigitos = rfcDigitos + "30 "
                Case "U"
                    rfcDigitos = rfcDigitos + "31 "
                Case "V"
                    rfcDigitos = rfcDigitos + "32 "
                Case "W"
                    rfcDigitos = rfcDigitos + "33 "
                Case "X"
                    rfcDigitos = rfcDigitos + "34 "
                Case "Y"
                    rfcDigitos = rfcDigitos + "35 "
                Case "Z"
                    rfcDigitos = rfcDigitos + "36 "
                Case "Ñ"
                    rfcDigitos = rfcDigitos + "37 "
            End Select
        Next
        'MsgBox("Cadena de digitos: " & rfcDigitos)

        'PASO #2
        strArr = rfcDigitos.Split(New [Char]() {CChar(vbTab), CChar(" ")}) 'Splitter de la cadena que contiene todos los digitos producto del case
        listaNumeros.AddRange(strArr) 'Lista llena con los digitos para realizar la ecuación Paso # 2
        For i As Integer = 0 To listaNumeros.Count - 2
            'MsgBox("Multiplicación: " & listaNumeros(i) + " X " & pi) 'Imprime las multiplicaciones.
            resultadoMulti = resultadoMulti + CInt(listaNumeros(i)) * pi  '(Vi * (Pi - 1)) + (Vi * (Pi - 1)) + ..............+ (Vi * (Pi - 1))
            pi = pi - 1
        Next
        'MsgBox("Resultado de multiplicación: " & resultadoMulti)

        'Paso #3 - Final del metodo.
        residuo = resultadoMulti Mod divisor  'Se saca el residuo
        'MsgBox("Residuo: " & residuo)
        Select Case residuo
            Case 0
                digVerificador = 0
            Case 1, 2, 3, 4, 5, 6, 7, 8, 9
                digVerificador = CStr(divisor - residuo)
            Case 10
                If añoNacimiento < 2000 Then
                    digVerificador = CStr(divisor - residuo)
                ElseIf añoNacimiento >= 2000 Then
                    digVerificador = "A"
                End If
        End Select
        If digVerificador = "10" Then
            digVerificador = "A"
        End If
        'MsgBox("Digito Verificador: " & digVerificador)
        Return digVerificador
    End Function
    Public Function sacaClave(ByVal valor As String) As String
        Dim resultado As String = ""
        Select Case valor
            Case 0
                resultado = "1"
            Case 1
                resultado = "2"
            Case 2
                resultado = "3"
            Case 3
                resultado = "4"
            Case 4
                resultado = "5"
            Case 5
                resultado = "6"
            Case 6
                resultado = "7"
            Case 7
                resultado = "8"
            Case 8
                resultado = "9"
            Case 9
                resultado = "A"
            Case 10
                resultado = "B"
            Case 11
                resultado = "C"
            Case 12
                resultado = "D"
            Case 13
                resultado = "E"
            Case 14
                resultado = "F"
            Case 15
                resultado = "G"
            Case 16
                resultado = "H"
            Case 17
                resultado = "I"
            Case 18
                resultado = "J"
            Case 19
                resultado = "K"
            Case 20
                resultado = "L"
            Case 21
                resultado = "M"
            Case 22
                resultado = "N"
            Case 23
                resultado = "P"
            Case 24
                resultado = "Q"
            Case 25
                resultado = "R"
            Case 26
                resultado = "S"
            Case 27
                resultado = "T"
            Case 28
                resultado = "U"
            Case 29
                resultado = "V"
            Case 30
                resultado = "W"
            Case 31
                resultado = "X"
            Case 32
                resultado = "Y"
            Case 33
                resultado = "Z"
        End Select
        Return resultado
    End Function
    Public Function primerVocal(ByVal palabra As String) As Char
        Dim letra As Char
        Dim listaVocales As List(Of Char) = New List(Of Char)()
        Dim primera = 0
        listaVocales.Add("u")
        For i As Integer = 0 To palabra.Length - 1
            'MsgBox(palabra(i))
            If primera = 0 Then
                Select Case palabra(i)
                    Case "a"
                        letra = "a"
                        primera = 1
                    Case "e"
                        letra = "e"
                        primera = 1
                    Case "i"
                        letra = "i"
                        primera = 1
                    Case "o"
                        letra = "o"
                        primera = 1
                    Case "u"
                        letra = "u"
                        primera = 1
                End Select
            End If
        Next
        If primera <> 0 Then
            Return letra
        End If
        Return ""
    End Function
    'Falta regla 10 y 12 en persona moral La 10 es la que incluye números arabigos y romanos en el nombre
    'La 12 es la que contiene simbolos especiales como @,%,/,#,',!,",-,(,),+ y .
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            PressEnter()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker1.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub DateTimePicker1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            PressEnter()
        End If
    End Sub

    Private Sub PressEnter()
        Dim digito As String
        RFC = pFisica(LimpiarTexto(TextBox1.Text), LimpiarTexto(TextBox3.Text), LimpiarTexto(TextBox2.Text), DateTimePicker1.Value) + claveHomonimia(LimpiarTexto(TextBox1.Text), LimpiarTexto(TextBox3.Text), LimpiarTexto(TextBox2.Text), DateTimePicker1.Value)
        digito = digitoVerificador(RFC, DateTimePicker1.Value)
        RFC = RFC + digito
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Function LimpiarTexto(pstr As String) As String
        pstr = Replace(pstr, "Á", "A")
        pstr = Replace(pstr, "É", "E")
        pstr = Replace(pstr, "Í", "I")
        pstr = Replace(pstr, "Ó", "O")
        pstr = Replace(pstr, "Ú", "U")

        pstr = Replace(pstr, "Ä", "A")
        pstr = Replace(pstr, "Ë", "E")
        pstr = Replace(pstr, "Ï", "I")
        pstr = Replace(pstr, "Ö", "O")
        pstr = Replace(pstr, "Ü", "U")

        pstr = Replace(pstr, "'", "")
        Return pstr
    End Function
End Class