Imports System.Text.RegularExpressions
Public Class frmExportacionPaises
    Private codigos() As String = {"AFG", "ALA", "ALB", "DEU", "AND", "AGO", "AIA", "ATA", "ATG", "SAU", "DZA", "ARG", "ARM", "ABW", "AUS", "AUT", "AZE", "BHS", "BGD", "BRB", "BHR", "BEL", "BLZ", "BEN", "BMU", "BLR", "MMR", "BOL", "BIH", "BWA", "BRA", "BRN", "BGR", "BFA", "BDI", "BTN",
        "CPV", "KHM", "CMR", "CAN", "QAT", "BES", "TCD", "CHL", "CHN", "CYP", "COL", "COM", "PRK", "KOR", "CIV", "CRI", "HRV", "CUB", "CUW", "DNK", "DMA", "ECU", "EGY", "SLV", "ARE", "ERI", "SVK", "SVN", "ESP", "USA", "EST", "ETH", "PHL", "FIN", "FJI", "FRA", "GAB", "GMB", "GEO", "GHA", "GIB", "GRD", "GRC", "GRL", "GLP", "GUM", "GTM", "GUF", "GGY", "GIN", "GNB", "GNQ", "GUY", "HTI", "HND", "HKG", "HUN", "IND", "IDN",
        "IRQ", "IRN", "IRL", "BVT", "IMN", "CXR", "NFK", "ISL", "CYM", "CCK", "COK", "FRO", "SGS", "HMD", "FLK", "MNP", "MHL", "PCN", "SLB", "TCA", "UMI", "VGB", "VIR", "ISR", "ITA", "JAM", "JPN", "JEY", "JOR", "KAZ", "KEN", "KGZ", "KIR", "KWT", "LAO", "LSO", "LVA", "LBN", "LBR", "LBY", "LIE", "LTU", "LUX", "MAC", "MDG", "MYS", "MWI", "MDV", "MLI", "MLT", "MAR", "MTQ", "MUS", "MRT", "MYT", "MEX", "FSM", "MDA", "MCO",
"MNG", "MNE", "MSR", "MOZ", "NAM", "NRU", "NPL", "NIC", "NER", "NGA", "NIU", "NOR", "NCL", "NZL", "OMN", "NLD", "PAK", "PLW", "PSE", "PAN", "PNG", "PRY", "PER", "PYF", "POL", "PRT", "PRI", "GBR", "CAF", "CZE", "MKD", "COG", "COD", "DOM", "REU", "RWA", "ROU", "RUS", "WSM", "ASM", "BLM", "KNA", "SMR", "MAF", "SPM", "VCT", "SHN", "LCA", "STP", "SEN", "SRB", "SYC", "SLE", "SGP", "SXM", "SYR", "SOM", "LKA",
"SWZ", "ZAF", "SDN", "SSD", "SWE", "CHE", "SUR", "SJM", "THA", "TWN", "TZA", "TJK", "IOT", "ATF", "TLS", "TGO", "TKL", "TON", "TTO", "TUN", "TKM", "TUR", "TUV", "UKR", "UGA", "URY", "UZB", "VUT", "VAT", "VEN", "VNM", "WLF", "YEM", "DJI", "ZMB", "ZWE", "ESH"}

    Private paises() As String = {"Afganistán", "Åland", "Albania", "Alemania", "Andorra", "Angola", "Anguila", "Antártida", "Antigua y Barbuda", "Arabia Saudita", "Argelia", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Bangladés", "Barbados", "Baréin", "Bélgica", "Belice", "Benín", "Bermudas", "Bielorrusia", "Myanmar", "Bolivia", "Bosnia y Herzegovina", "Botsuana", "Brasil", "Brunéi", "Bulgaria", "Burkina Faso", "Burundi", "Bután",
    "Cabo Verde", "Camboya", "Camerún", "Canadá", "Qatar", "Bonaire, San Eustaquio y Saba", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Corea del Norte", "Corea del Sur", "Costa de Marfil", "Costa Rica", "Croacia", "Cuba", "Curazao", "Dinamarca", "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eritrea", "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Etiopía", "Filipinas", "Finlandia", "Fiyi", "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Gibraltar",
    "Granada", "Grecia", "Groenlandia", "Guadalupe", "Guam", "Guatemala", "Guayana Francesa", "Guernsey", "Guinea", "Guinea-Bisáu", "Guinea Ecuatorial", "Guyana", "Haití", "Honduras", "Hong Kong", "Hungría", "India", "Indonesia", "Irak", "Irán", "Irlanda", "Isla Bouvet", "Isla de Man", "Isla de Navidad", "Norfolk", "Islandia", "Islas Caimán", "Islas Cocos", "Islas Cook", "Islas Feroe", "Islas Georgias del Sur y Sandwich del Sur", "Islas Heard y McDonald", "Islas Malvinas", "Islas Marianas del Norte", "Islas Marshall",
    "Islas Pitcairn", "Islas Salomón", "Islas Turcas y Caicos", "Islas ultramarinas de Estados Unidos", "Islas Vírgenes Británicas", "Islas Vírgenes de los Estados Unidos", "Israel", "Italia", "Jamaica", "Japón", "Jersey", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kuwait", "Laos", "Lesoto", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein", "Lituania", "Luxemburgo", "Macao", "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Martinica", "Mauricio", "Mauritania", "Mayotte", "México",
    "Micronesia", "Moldavia", "Mónaco", "Mongolia", "Montenegro", "Montserrat", "Mozambique", "Namibia", "Nauru", "Nepal", "Nicaragua", "Níger", "Nigeria", "Niue", "Noruega", "Nueva Caledonia", "Nueva Zelanda", "Omán", "Países Bajos", "Pakistán", "Palaos", "Palestina", "Panamá", "Papúa Nueva Guinea", "Paraguay", "Perú", "Polinesia Francesa", "Polonia", "Portugal", "Puerto Rico", "Reino Unido", "República Centroafricana", "República Checa", "Macedonia", "República del Congo", "República Democrática del Congo", "República Dominicana", "Reunión", "Ruanda", "Rumania",
     "Rusia", "Samoa", "Samoa Americana", "San Bartolomé", "San Cristóbal y Nieves", "San Marino", "San Martín", "San Pedro y Miquelón", "San Vicente y las Granadinas", "Santa Elena, Ascensión y Tristán de Acuña", "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Sint Maarten", "Siria", "Somalia", "Sri Lanka", "Suazilandia", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Surinam", "Svalbard y Jan Mayen", "Tailandia", "Taiwán (República de China)", "Tanzania", "Tayikistán", "Territorio Británico del Océano Índico",
    "Tierras Australes y Antárticas Francesas", "Timor Oriental", "Togo", "Tokela", "Tonga", "Trinidad y Tobago", "Túnez", "Turkmenistán", "Turquía", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu", "Vaticano, Ciudad del", "Venezuela", "Vietnam", "Wallis y Futuna", "Yemen", "Yibuti", "Zambia", "Zimbabue", "República Árabe Saharaui Democrática"}

    Private tabla As New DataTable
    Private aux As New DataTable
    Private paisesAux As New List(Of String)
    Private codigosAux As New List(Of String)

    Public codigo As String = ""
    Private Sub frmExportacionPaises_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tabla.Columns.Add("pais")
        tabla.Columns.Add("codigo")
        aux.Columns.Add("pais")
        aux.Columns.Add("codigo")
        Dim r As DataRow
        For i As Integer = 0 To codigos.Length - 1
            r = tabla.NewRow()
            r("pais") = paises(i)
            r("codigo") = codigos(i)
            tabla.Rows.Add(r)
        Next
        llenaGrid()
    End Sub

    Private Sub llenaGrid()
        dgvPaises.DataSource = tabla
        dgvPaises.Columns(1).Visible = False
        dgvPaises.Columns(0).HeaderText = "Países"
        dgvPaises.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub filtrar(ByVal cadena As String)
        If cadena = "" Then
            llenaGrid()
            Exit Sub
        End If
        aux.Rows.Clear()
        paisesAux.Clear()
        codigosAux.Clear()
        Dim p As String
        For i As Integer = 0 To paises.Length - 1
            p = quitarAcentos(i)
            If paises(i).ToUpper.StartsWith(cadena) Then
                paisesAux.Add(paises(i))
                codigosAux.Add(codigos(i))
            End If
        Next
        Dim r As DataRow
        For i As Integer = 0 To codigosAux.ToArray.Length - 1
            r = aux.NewRow()
            r("pais") = paisesAux(i)
            r("codigo") = codigosAux(i)
            aux.Rows.Add(r)
        Next
        dgvPaises.DataSource = aux
    End Sub

    Private Sub dgvPaises_DoubleClick(sender As Object, e As EventArgs) Handles dgvPaises.DoubleClick
        codigo = dgvPaises.CurrentRow.Cells(1).Value.ToString()
        Dispose()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        codigo = dgvPaises.CurrentRow.Cells(1).Value.ToString()
        Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dispose()
    End Sub

    Private Sub txtFiltro_TextChanged(sender As Object, e As EventArgs) Handles txtFiltro.TextChanged
        filtrar(txtFiltro.Text)
    End Sub

    Private Sub txtFiltro_Leave(sender As Object, e As EventArgs) Handles txtFiltro.Leave
        filtrar(txtFiltro.Text)
    End Sub

    Private Function quitarAcentos(inputString As String) As String
        Dim replace_a_Accents As New Regex("[á|à|ä|â]", RegexOptions.Compiled)
        Dim replace_e_Accents As New Regex("[é|è|ë|ê]", RegexOptions.Compiled)
        Dim replace_i_Accents As New Regex("[í|ì|ï|î]", RegexOptions.Compiled)
        Dim replace_o_Accents As New Regex("[ó|ò|ö|ô]", RegexOptions.Compiled)
        Dim replace_u_Accents = New Regex("[ú|ù|ü|û]", RegexOptions.Compiled)
        inputString = replace_a_Accents.Replace(inputString, "a")
        inputString = replace_e_Accents.Replace(inputString, "e")
        inputString = replace_i_Accents.Replace(inputString, "i")
        inputString = replace_o_Accents.Replace(inputString, "o")
        inputString = replace_u_Accents.Replace(inputString, "u")
        Return inputString
    End Function

End Class