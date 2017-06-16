Imports MySql.Data.MySqlClient
Public Class frmExportacion
    Private idVenta As Integer
    Private totalapagar As Double
    Private motivosTraslado() As String = {"Envío de mercancias facturadas con anterioridad", "Reubicación de mercancías propias", "Envío de mercancías objeto de contrato de consignación", "Envío de mercancías para posterior enajenación", "Envío de mercancías propiedad de terceros", "Otros"}
    Private certificadosOrigen() As String = {"No funge como certificado de origen", "Funge como certificado de origen"}
    Private tieneSubdivision() As String = {"No tiene subdivisión", "Si tiene subdivisión"}
    Private valoresIncoterm() As String = {"CFR", "CIF", "CPT", "CIP", "DAF", "DAP", "DAT", "DES", "DEQ", "DDU", "DDP", "EXW", "FCA", "FAS", "FOB"}
    Private codigos() As String = {"AFG", "ALA", "ALB", "DEU", "AND", "AGO", "AIA", "ATA", "ATG", "SAU", "DZA", "ARG", "ARM", "ABW", "AUS", "AUT", "AZE", "BHS", "BGD", "BRB", "BHR", "BEL", "BLZ", "BEN", "BMU", "BLR", "MMR", "BOL", "BIH", "BWA", "BRA", "BRN", "BGR", "BFA", "BDI", "BTN",
    "CPV", "KHM", "CMR", "CAN", "QAT", "BES", "TCD", "CHL", "CHN", "CYP", "COL", "COM", "PRK", "KOR", "CIV", "CRI", "HRV", "CUB", "CUW", "DNK", "DMA", "ECU", "EGY", "SLV", "ARE", "ERI", "SVK", "SVN", "ESP", "USA", "EST", "ETH", "PHL", "FIN", "FJI", "FRA", "GAB", "GMB", "GEO", "GHA", "GIB", "GRD", "GRC", "GRL", "GLP", "GUM", "GTM", "GUF", "GGY", "GIN", "GNB", "GNQ", "GUY", "HTI", "HND", "HKG", "HUN", "IND", "IDN",
    "IRQ", "IRN", "IRL", "BVT", "IMN", "CXR", "NFK", "ISL", "CYM", "CCK", "COK", "FRO", "SGS", "HMD", "FLK", "MNP", "MHL", "PCN", "SLB", "TCA", "UMI", "VGB", "VIR", "ISR", "ITA", "JAM", "JPN", "JEY", "JOR", "KAZ", "KEN", "KGZ", "KIR", "KWT", "LAO", "LSO", "LVA", "LBN", "LBR", "LBY", "LIE", "LTU", "LUX", "MAC", "MDG", "MYS", "MWI", "MDV", "MLI", "MLT", "MAR", "MTQ", "MUS", "MRT", "MYT", "MEX", "FSM", "MDA", "MCO",
    "MNG", "MNE", "MSR", "MOZ", "NAM", "NRU", "NPL", "NIC", "NER", "NGA", "NIU", "NOR", "NCL", "NZL", "OMN", "NLD", "PAK", "PLW", "PSE", "PAN", "PNG", "PRY", "PER", "PYF", "POL", "PRT", "PRI", "GBR", "CAF", "CZE", "MKD", "COG", "COD", "DOM", "REU", "RWA", "ROU", "RUS", "WSM", "ASM", "BLM", "KNA", "SMR", "MAF", "SPM", "VCT", "SHN", "LCA", "STP", "SEN", "SRB", "SYC", "SLE", "SGP", "SXM", "SYR", "SOM", "LKA",
    "SWZ", "ZAF", "SDN", "SSD", "SWE", "CHE", "SUR", "SJM", "THA", "TWN", "TZA", "TJK", "IOT", "ATF", "TLS", "TGO", "TKL", "TON", "TTO", "TUN", "TKM", "TUR", "TUV", "UKR", "UGA", "URY", "UZB", "VUT", "VAT", "VEN", "VNM", "WLF", "YEM", "DJI", "ZMB", "ZWE", "ESH"}
    Public unidadesMedida() As String = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "99"}
    Private complemento As dbComplementoExportacion
    Private receptor As dbComplementoExportacionReceptor
    Private destinatario As dbComplementoExportacioDestinatario
    Private mercancia As dbComplementoExportacionMercancia
    Private domicilio As dbComplementoExportacionDomicilio
    Private descripcion As dbComplementoExportacionDescripcion
    Private venta As dbVentas
    Private nuevoComplemento As Boolean = True
    Private nuevoDomicilio As Boolean = True
    Private nuevaMercancia As Boolean = True
    Private nuevaDescripcion As Boolean = True
    Private nuevoDestinatario As Boolean = True
    Private guardado As Boolean = False
    Public xml As String = ""
    Public cadenaOriginal = ""

    Public Sub New(ByVal idVenta As Integer, ByVal totalapagar As Double)
        InitializeComponent()
        Me.idVenta = idVenta
        Me.totalapagar = totalapagar
        venta = New dbVentas(idVenta, MySqlcon, "")
    End Sub
    Private Sub frmExportacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        LlenaComboBox()
        complemento = New dbComplementoExportacion(MySqlcon, venta.IdCliente)
        receptor = New dbComplementoExportacionReceptor(MySqlcon)
        destinatario = New dbComplementoExportacioDestinatario(MySqlcon)
        mercancia = New dbComplementoExportacionMercancia(MySqlcon)
        domicilio = New dbComplementoExportacionDomicilio(MySqlcon)
        descripcion = New dbComplementoExportacionDescripcion(MySqlcon)
        If complemento.buscar(idVenta) Then
            llenaDatosComplemento()
            nuevoComplemento = False
        Else
            If complemento.guardar("2", "A1", comboOrigen.SelectedIndex, txtCertificado.Text, txtExportador.Text, txtIncoterm.Text, comboSubdivision.SelectedIndex, txtObservaciones.Text, 0, 0, idVenta, txtCurpEmisor.Text, claveMotivoTraslado) Then
                complemento.buscar(idVenta)
                'venta.DaTotal(venta.ID, venta.DaMoneda(venta.ID), "", "")
                txtTipoCambio.Text = venta.TipodeCambio
                Dim activaTraslado As New dbVentasCartaPorte(idVenta, MySqlcon)
                If activaTraslado.Origen <> "Nohay" Then
                    Label1.Visible = True
                    Combo_MTraslado.Visible = True
                End If
                If venta.IdConversion = 2 Then
                    txtTotalUSD.Text = Format(totalapagar / venta.TipodeCambio, "0.00")
                Else
                    txtTotalUSD.Text = venta.TotalaPagar
                End If
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                txtCurpEmisor.Text = s.CURP
                llenaDatosDestinatario()
                txtUnidadAduana.SelectedIndex = 0
                cargarMercancia()
            Else
                MsgBox("Error al guardar complemento.")
                Dispose()
            End If
        End If
        Panel_Destinatario.Visible = False
    End Sub
    Private Function claveMotivoTraslado()
        Dim clave As String = ""
        Select Case Combo_MTraslado.SelectedIndex
            Case 0
                clave = "01"
            Case 1
                clave = "02"
            Case 2
                clave = "03"
            Case 3
                clave = "04"
            Case 4
                clave = "05"
            Case 5
                clave = "99"
        End Select
        Return clave
    End Function
    Private Sub LlenaComboBox()
        Combo_MTraslado.DataSource = motivosTraslado
        Combo_MTraslado.SelectedIndex = 0
        comboOrigen.DataSource = certificadosOrigen
        comboSubdivision.DataSource = tieneSubdivision
        comboSubdivision.SelectedIndex = 0
        txtIncoterm.DataSource = valoresIncoterm
        txtIncoterm.SelectedIndex = 0
    End Sub
    Private Function trasladoACombo()
        Dim clave As Integer
        Select Case complemento.motivoTraslado
            Case "01"
                clave = 0
            Case "02"
                clave = 1
            Case "03"
                clave = 2
            Case "04"
                clave = 3
            Case "05"
                clave = 4
            Case "99"
                clave = 5
        End Select
        Return clave
    End Function
    Private Sub llenaDatosComplemento()
        receptor.buscaComplemento(complemento.idComplemento)
        destinatario.buscaComplemento(complemento.idComplemento)
        domicilio.buscaDestinatario(destinatario.idDestinatario)
        comboOrigen.SelectedIndex = complemento.certificadoOrigen
        Combo_MTraslado.SelectedIndex = trasladoACombo()
        txtCertificado.Text = complemento.numCertificado
        comboSubdivision.SelectedIndex = complemento.subDivision
        txtObservaciones.Text = complemento.observaciones
        txtTipoCambio.Text = complemento.tipoCambioUSD
        txtTotalUSD.Text = complemento.totalUSD
        txtReceptorCurp.Text = receptor.curp
        txtReceptorID.Text = receptor.numRegIdTrib
        destinatario_id.Text = destinatario.numRegIdTrip
        destinatario_curp.Text = destinatario.curp
        destinatario_nombre.Text = destinatario.nombre
        destinatario_rfc.Text = destinatario.rfc
        destinatario_calle.Text = domicilio.calle
        destinatario_numext.Text = domicilio.numExterior
        destinatario_NumInt.Text = domicilio.numInterior
        destinatario_Col.Text = domicilio.colonia
        destinatario_localidad.Text = domicilio.localidad
        destinatario_Ref.Text = domicilio.referencia
        destinatario_Municipio.Text = domicilio.municipio
        destinatario_Estado.Text = domicilio.estado
        destinatario_Pais.Text = domicilio.pais
        destinatario_CP.Text = domicilio.cp
        txtExportador.Text = complemento.numExportarConfiable
        txtCurpEmisor.Text = complemento.curpEmisor
        txtIncoterm.SelectedIndex = txtIncoterm.Items.IndexOf(complemento.incoterm)
        txtUnidadAduana.SelectedIndex = 0
        receptor_pais.Text = receptor.clave_Pais

        llenaGridMercancia()
    End Sub
    Private Function GuardarComlemento() As Boolean
        If checaMercancia() = False Then
            MsgBox("Debe indicar el número de identificación para cada mercancía.")
            Return False
        End If
        Dim activaTraslado As New dbVentasCartaPorte(idVenta, MySqlcon)
        If activaTraslado.Origen <> "Nohay" Then
            complemento.modificar(complemento.idComplemento, "2", "A1", comboOrigen.SelectedIndex, txtCertificado.Text, txtExportador.Text, txtIncoterm.Text, comboSubdivision.SelectedIndex, txtObservaciones.Text, If(txtTipoCambio.Text <> "", CDbl(txtTipoCambio.Text), 0), If(txtTotalUSD.Text <> "", CDbl(txtTotalUSD.Text), 0), idVenta, txtCurpEmisor.Text, claveMotivoTraslado.ToString)
        Else
            complemento.modificar(complemento.idComplemento, "2", "A1", comboOrigen.SelectedIndex, txtCertificado.Text, txtExportador.Text, txtIncoterm.Text, comboSubdivision.SelectedIndex, txtObservaciones.Text, If(txtTipoCambio.Text <> "", CDbl(txtTipoCambio.Text), 0), If(txtTotalUSD.Text <> "", CDbl(txtTotalUSD.Text), 0), idVenta, txtCurpEmisor.Text, "")
        End If

        If CheckBox1.Checked Then
            If guardarDestinatario() = False Then
                Return False
            End If
        End If
        complemento.buscar(idVenta)
        guardarReceptor()
        xml = complemento.crearXML()
        cadenaOriginal = complemento.creaCadenaOriginal()
        If nuevoComplemento Then
            complemento.guardar(complemento.idComplemento)
        End If
        guardarTodo()
        Return True
    End Function
    Private Sub GuardarMercancia()
        'If nuevaMercancia Then
        '    mercancia.guardar(txtMercanciaID.Text, txtFraccArance.Text, If(txtCantidadAduana.Text <> "", CDbl(txtCantidadAduana.Text), 0), txtUnidadAduana.Text, If(txtValorUnitario.Text <> "", CDbl(txtValorUnitario.Text), 0), If(txtValorDolares.Text <> "", CDbl(txtValorDolares.Text), 0), complemento.idComplemento, "")
        '    limpiaCamposMercancia()
        '    limpiaCamposDescripcion()
        '    dgvMercancia.DataSource = mercancia.lista(complemento.idComplemento)
        'Else
        mercancia.modificar(mercancia.idMercancia, txtMercanciaID.Text, txtFraccArance.Text, If(txtCantidadAduana.Text <> "", CDbl(txtCantidadAduana.Text), 0), unidadesMedida.GetValue(txtUnidadAduana.SelectedIndex), If(txtValorUnitario.Text <> "", CDbl(txtValorUnitario.Text), 0), If(txtValorDolares.Text <> "", CDbl(txtValorDolares.Text), 0), complemento.idComplemento, mercancia.nombre)
        'nuevaMercancia = True
        limpiaCamposMercancia()
        limpiaCamposDescripcion()
        llenaGridMercancia()
        'End If
    End Sub
    Private Sub cargarMercancia()
        Dim d As New dbVentasInventario(MySqlcon)
        ' Dim inv As dbInventario
        Dim dr As MySqlDataReader = d.ConsultaReader(idVenta, False, "", 0, "", False)
        Dim ids As New List(Of Integer)
        While dr.Read()
            ' mercancia.guardar(txtMercanciaID.Text, txtFraccArance.Text, dr("cantidad"), txtUnidadAduana.Text, If(txtValorUnitario.Text <> "", CDbl(txtValorUnitario.Text), 0), If(txtValorDolares.Text <> "", CDbl(txtValorDolares.Text), 0), complemento.idComplemento, dr("descripcion"))
            ids.Add(dr("idventasinventario"))
        End While
        dr.Close()
        For Each i As Integer In ids
            'inv = New dbInventario(d.BuscaridInventario(i), MySqlcon)
            d.ID = i
            d.LlenaDatos()
            'mercancia.guardar(txtMercanciaID.Text, txtFraccArance.Text, d.Cantidad, txtUnidadAduana.Text, If(txtValorUnitario.Text <> "", CDbl(txtValorUnitario.Text), 0), If(txtValorDolares.Text <> "", CDbl(txtValorDolares.Text), 0), complemento.idComplemento, d.Descripcion)
            If d.IdMoneda = 2 Then
                mercancia.guardar(d.Inventario.Clave, d.Inventario.FraccionArancel, d.Cantidad, d.Inventario.UnidadAduana.Substring(0, 2), d.Precio / d.Cantidad / venta.TipodeCambio, d.Precio / venta.TipodeCambio, complemento.idComplemento, d.Descripcion)
            Else
                mercancia.guardar(d.Inventario.Clave, d.Inventario.FraccionArancel, d.Cantidad, d.Inventario.UnidadAduana.Substring(0, 2), d.Precio / d.Cantidad, d.Precio, complemento.idComplemento, d.Descripcion)
            End If

        Next
        llenaGridMercancia()
    End Sub
    Private Sub GuardarEspecificacion()
        If mercancia.idMercancia > 0 Then
            If nuevaDescripcion Then
                descripcion.guardar(txtMarca.Text, txtModelo.Text, txtSubModelo.Text, txtNumSerie.Text, mercancia.idMercancia)
                limpiaCamposDescripcion()
                llenaGridEspecificaciones()
            Else
                descripcion.modificar(descripcion.idDescripcion, txtMarca.Text, txtModelo.Text, txtSubModelo.Text, txtNumSerie.Text, mercancia.idMercancia)
                nuevaDescripcion = True
                limpiaCamposDescripcion()
                llenaGridEspecificaciones()
            End If
        Else
            MsgBox("Debe seleccionar una mercancía para guardar la especificación.")
        End If
    End Sub
    Private Sub llenaGridMercancia()
        dgvMercancia.DataSource = mercancia.lista(complemento.idComplemento)
        dgvMercancia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvMercancia.Columns(0).Visible = False
    End Sub
    Private Sub llenaGridEspecificaciones()
        dgvEspecificaciones.DataSource = descripcion.lista(mercancia.idMercancia)
        dgvEspecificaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvEspecificaciones.Columns(0).Visible = False
        dgvEspecificaciones.Columns(1).HeaderText = "Marca"
        dgvEspecificaciones.Columns(2).HeaderText = "Modelo"
        dgvEspecificaciones.Columns(3).HeaderText = "Submodelo"
        dgvEspecificaciones.Columns(4).HeaderText = "Numero de Serie"
        dgvEspecificaciones.Columns(5).Visible = False
        dgvEspecificaciones.Columns(6).Visible = False
    End Sub
    Private Function guardarDestinatario() As Boolean
        If nuevoComplemento Then
            If destinatario_id.Text <> "" Or destinatario_rfc.Text <> "" Or destinatario_curp.Text <> "" Or destinatario_nombre.Text <> "" Then
                If destinatario.buscaComplemento(complemento.idComplemento) Then
                    destinatario.modificar(destinatario.idDestinatario, destinatario_id.Text, destinatario_rfc.Text, destinatario_curp.Text, destinatario_nombre.Text, complemento.idComplemento)
                Else
                    destinatario.guardar(destinatario_id.Text, destinatario_rfc.Text, destinatario_curp.Text, destinatario_nombre.Text, complemento.idComplemento)
                End If
                destinatario.buscaComplemento(complemento.idComplemento)

                If guardarDomicilio() = False Then
                    Return False
                End If
                Return True
            End If
        Else
            If destinatario.buscaComplemento(complemento.idComplemento) Then
                destinatario.modificar(destinatario.idDestinatario, destinatario_curp.Text, destinatario_rfc.Text, destinatario_curp.Text, destinatario_nombre.Text, complemento.idComplemento)
                If guardarDomicilio() = False Then
                    Return False
                End If
                Return True
            Else
                If destinatario_id.Text <> "" Or destinatario_rfc.Text <> "" Or destinatario_curp.Text <> "" Or destinatario_nombre.Text <> "" Then
                    destinatario.guardar(destinatario_curp.Text, destinatario_rfc.Text, destinatario_curp.Text, destinatario_nombre.Text, complemento.idComplemento)
                    destinatario.buscaComplemento(complemento.idComplemento)
                    If guardarDomicilio() = False Then
                        Return False
                    End If
                    Return True
                End If
            End If
        End If
    End Function
    Private Sub guardarReceptor()
        If txtReceptorID.Text <> "" Then
            If receptor.buscaComplemento(complemento.idComplemento) Then
                receptor.modificar(receptor.idReceptor, txtReceptorID.Text, complemento.idComplemento, txtReceptorCurp.Text, receptor_pais.Text)
            Else
                receptor.guardar(txtReceptorID.Text, complemento.idComplemento, txtReceptorCurp.Text, receptor_pais.Text)
            End If
        End If
    End Sub
    Private Function guardarDomicilio() As Boolean
        If destinatario_calle.Text = "" Or destinatario_Estado.Text = "" Or destinatario_Pais.Text = "" Or destinatario_CP.Text = "" Then
            MsgBox("Faltan datos. Los campos Marcados son de caracter obligatorio, favor de llenarlos.")
            If destinatario_calle.Text = "" Then destinatario_calle.BackColor = Color.Red Else destinatario_calle.BackColor = Color.White
            If destinatario_Estado.Text = "" Then destinatario_Estado.BackColor = Color.Red Else destinatario_Estado.BackColor = Color.White
            If destinatario_Pais.Text = "" Then destinatario_Pais.BackColor = Color.Red Else destinatario_Pais.BackColor = Color.White
            If destinatario_CP.Text = "" Then destinatario_CP.BackColor = Color.Red Else destinatario_CP.BackColor = Color.White
            Return False
        Else
            If domicilio.buscaDestinatario(destinatario.idDestinatario) Then
                domicilio.modificar(domicilio.idDomicilio, destinatario_calle.Text, destinatario_numext.Text, destinatario_NumInt.Text, destinatario_Col.Text, destinatario_localidad.Text, destinatario_Ref.Text, destinatario_Municipio.Text, destinatario_Estado.Text, destinatario_Pais.Text, destinatario_CP.Text, destinatario.idDestinatario)
            Else
                domicilio.guardar(destinatario_calle.Text, destinatario_numext.Text, destinatario_NumInt.Text, destinatario_Col.Text, destinatario_localidad.Text, destinatario_Ref.Text, destinatario_Municipio.Text, destinatario_Estado.Text, destinatario_Pais.Text, destinatario_CP.Text, destinatario.idDestinatario)
            End If
        End If
        Return True
    End Function
    Private Sub guardarTodo()
        Dim idsMerca() As Integer = mercancia.buscaComplemento(complemento.idComplemento)
        For Each i As Integer In idsMerca
            mercancia.guardarTodo(complemento.idComplemento, i)
            Dim des() As Integer = descripcion.buscaMercancia(i)
            For Each x As Integer In des
                descripcion.guardarTodo(i, x)
            Next
        Next
        If destinatario.buscaComplemento(complemento.idComplemento) Then
            destinatario.guardarTodo(complemento.idComplemento, destinatario.idDestinatario)
            If domicilio.buscaDestinatario(destinatario.idDestinatario) Then
                domicilio.guardarTodo(destinatario.idDestinatario, domicilio.idDomicilio)
            End If
        End If
        If receptor.buscaComplemento(complemento.idComplemento) Then
            receptor.guardarTodo(complemento.idComplemento, receptor.idReceptor)
        End If

    End Sub
    Private Sub muestaMercancia()
        Dim i As Integer = CInt(dgvMercancia.CurrentRow.Cells(0).Value)
        mercancia.buscar(i)
        nuevaMercancia = False
        txtMercanciaID.Text = mercancia.noIdentificacion
        txtFraccArance.Text = mercancia.fraccionArancelaria
        txtCantidadAduana.Text = mercancia.cantidadAduana
        txtValorUnitario.Text = mercancia.valorUnitarioAduana
        txtValorDolares.Text = mercancia.valorDolares
        txtUnidadAduana.SelectedIndex = unidadACombo() 'aqui
        llenaGridEspecificaciones()
    End Sub
    Public Function unidadACombo() As Integer
        Dim clave As Integer
        Select Case mercancia.unidadAduana
            Case "01"
                clave = 0
            Case "02"
                clave = 1
            Case "03"
                clave = 2
            Case "04"
                clave = 3
            Case "05"
                clave = 4
            Case "06"
                clave = 5
            Case "07"
                clave = 6
            Case "08"
                clave = 7
            Case "09"
                clave = 8
            Case "10"
                clave = 9
            Case "11"
                clave = 10
            Case "12"
                clave = 11
            Case "13"
                clave = 12
            Case "14"
                clave = 13
            Case "15"
                clave = 14
            Case "16"
                clave = 15
            Case "17"
                clave = 16
            Case "18"
                clave = 17
            Case "19"
                clave = 18
            Case "20"
                clave = 19
            Case "21"
                clave = 20
            Case "99"
                clave = 21
        End Select
        Return clave
    End Function
    Private Sub eliminaSinGuardar()
        Dim idsDes() As Integer
        Dim idsMerca() As Integer = mercancia.buscaComplemento(complemento.idComplemento)
        For Each i As Integer In idsMerca
            mercancia.buscar(i)
            idsDes = descripcion.buscaMercancia(i)
            For Each x As Integer In idsDes
                descripcion.buscar(x)
                If descripcion.guardado <> CInt(Estados.Guardada) Then
                    descripcion.eliminar(x)
                End If
            Next
            If mercancia.guardado <> CInt(Estados.Guardada) Then
                mercancia.eliminar(i)
            End If
        Next

    End Sub
    Private Sub MuestaEspecificacion()
        Dim i As Integer = CInt(dgvEspecificaciones.CurrentRow.Cells(0).Value)
        descripcion.buscar(i)
        nuevaDescripcion = False
        txtMarca.Text = descripcion.marca
        txtModelo.Text = descripcion.modelo
        txtSubModelo.Text = descripcion.submodelo
        txtNumSerie.Text = descripcion.numeroSerie
    End Sub
    Private Sub limpiaCamposDescripcion()
        txtMarca.Text = ""
        txtModelo.Text = ""
        txtSubModelo.Text = ""
        txtNumSerie.Text = ""
    End Sub
    Private Sub limpiaCamposMercancia()
        txtMercanciaID.Text = ""
        txtFraccArance.Text = ""
        txtCantidadAduana.Text = ""
        txtValorUnitario.Text = ""
        txtValorDolares.Text = ""
        txtUnidadAduana.SelectedIndex = 0
    End Sub
    Private Sub txtTipoCambio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipoCambio.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If InStr("0123456789.", Chr(KeyAscii)) = 0 Then
            If KeyAscii <> 8 Then
                KeyAscii = 0
            End If
            e.KeyChar = Chr(KeyAscii)
            If KeyAscii = 0 Then

                e.Handled = True

            End If
        End If
    End Sub
    Private Sub txtTotalUSD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalUSD.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If InStr("0123456789.", Chr(KeyAscii)) = 0 Then
            If KeyAscii <> 8 Then
                KeyAscii = 0
            End If
            e.KeyChar = Chr(KeyAscii)
            If KeyAscii = 0 Then

                e.Handled = True

            End If
        End If
    End Sub
    Private Sub txtValorUnitario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValorUnitario.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If InStr("0123456789.", Chr(KeyAscii)) = 0 Then
            If KeyAscii <> 8 Then
                KeyAscii = 0
            End If
            e.KeyChar = Chr(KeyAscii)
            If KeyAscii = 0 Then

                e.Handled = True

            End If
        End If
    End Sub
    Private Sub txtValorDolares_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValorDolares.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If InStr("0123456789.", Chr(KeyAscii)) = 0 Then
            If KeyAscii <> 8 Then
                KeyAscii = 0
            End If
            e.KeyChar = Chr(KeyAscii)
            If KeyAscii = 0 Then

                e.Handled = True

            End If
        End If
    End Sub
    Private Sub dgvMercancia_Click(sender As Object, e As EventArgs) Handles dgvMercancia.Click
        Try
            muestaMercancia()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub dgvEspecificaciones_Click(sender As Object, e As EventArgs) Handles dgvEspecificaciones.Click
        Try
            MuestaEspecificacion()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnAgregarMerca_Click(sender As Object, e As EventArgs) Handles btnAgregarMerca.Click
        If txtMercanciaID.Text = "" Then
            MsgBox("Debe indicar un número de identificación.")
            txtMercanciaID.BackColor = Color.Red
            Exit Sub
        Else
            txtMercanciaID.BackColor = Color.White
        End If
        GuardarMercancia()
    End Sub
    Private Sub btnAgregarEsp_Click(sender As Object, e As EventArgs) Handles btnAgregarEsp.Click
        GuardarEspecificacion()
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If GuardarComlemento() Then
            guardado = True
            Dispose()
        End If
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dispose()
    End Sub
    Private Sub frmExportacion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If guardado = False Then
            Dim result = MsgBox("Hay cambios sin guardar, ¿desea salir sin guardarlos?", MsgBoxStyle.YesNo)
            If result = DialogResult.No Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnPais.Click
        Dim CSat = New dbCatalogosSat
        CSat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
        Dim fbc As New frmBuscadorCatalogosSat(5)
        fbc.Cat = CSat
        fbc.CerrarCon = True
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            destinatario_Pais.Text = fbc.Clave
        End If
        fbc.Dispose()
    End Sub
    Private Sub dgvMercancia_DoubleClick(sender As Object, e As EventArgs) Handles dgvMercancia.DoubleClick
        Try
            Dim id As Integer = CInt(dgvMercancia.CurrentRow.Cells(0).Value)
            Dim result = MsgBox("¿Desea eliminar esta marcancía?. Se borraran también las especificaciones correspondientes.", MsgBoxStyle.YesNo)
            If result = DialogResult.Yes Then
                Dim idsDes() As Integer = descripcion.buscaMercancia(id)
                For Each i As Integer In idsDes
                    descripcion.eliminar(i)
                Next
                mercancia.eliminar(id)
                llenaGridMercancia()
                limpiaCamposMercancia()
                llenaGridEspecificaciones()
                limpiaCamposDescripcion()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub dgvEspecificaciones_DoubleClick(sender As Object, e As EventArgs) Handles dgvEspecificaciones.DoubleClick
        Dim id As Integer = CInt(dgvEspecificaciones.CurrentRow.Cells(0).Value)
        Dim result = MsgBox("¿Desea eliminar la especificacíon seleccionada?", MsgBoxStyle.YesNo)
        If result = DialogResult.Yes Then
            descripcion.eliminar(id)
        End If
        llenaGridEspecificaciones()
        limpiaCamposDescripcion()
    End Sub
    Private Sub txtPais_Leave(sender As Object, e As EventArgs) Handles destinatario_Pais.Leave
        Dim res As Boolean = False
        If destinatario_Pais.Text <> "" Then
            For Each s As String In codigos
                If destinatario_Pais.Text = s Then
                    res = True
                    Exit For
                End If
            Next
            If res Then
                destinatario_Pais.BackColor = Color.White
            Else
                MsgBox("El código no corresponde a ningun país, verifíquelo.")
                destinatario_Pais.BackColor = Color.Red
                destinatario_Pais.Focus()
            End If
        Else
            If btnPais.Focus() = False Then
                MsgBox("Debe indicar el código de un páis.")
                destinatario_Pais.BackColor = Color.Red
                destinatario_Pais.Focus()
            End If
        End If
    End Sub
    'Private Sub txtNumSerie_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumSerie.KeyPress
    '    Dim KeyAscii As Short = Asc(e.KeyChar)
    '    If InStr("0123456789", Chr(KeyAscii)) = 0 Then
    '        If KeyAscii <> 8 Then
    '            KeyAscii = 0
    '        End If
    '        e.KeyChar = Chr(KeyAscii)
    '        If KeyAscii = 0 Then

    '            e.Handled = True

    '        End If
    '    End If
    'End Sub
    Private Sub txtFraccArance_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFraccArance.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If InStr("0123456789", Chr(KeyAscii)) = 0 Then
            If KeyAscii <> 8 Then
                KeyAscii = 0
            End If
            e.KeyChar = Chr(KeyAscii)
            If KeyAscii = 0 Then

                e.Handled = True

            End If
        End If
    End Sub
    Private Function checaMercancia() As Boolean
        Dim res As Boolean = True
        Dim aux() As Integer = mercancia.buscaComplemento(complemento.idComplemento)
        For Each i As Integer In aux
            mercancia.buscar(i)
            If mercancia.noIdentificacion = "" Then
                res = False
                Exit For
            End If
        Next
        Return res
    End Function
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        nuevaDescripcion = True
        limpiaCamposDescripcion()
    End Sub
    Private Sub llenaDatosDestinatario()
        Dim idCliente As Integer = venta.IdCliente
        Dim cliente As New dbClientes(idCliente, MySqlcon)
        txtReceptorCurp.Text = cliente.CURP
        destinatario_rfc.Text = cliente.RFC
        destinatario_curp.Text = cliente.CURP
        destinatario_nombre.Text = cliente.Nombre
        destinatario_calle.Text = cliente.Direccion
        destinatario_numext.Text = cliente.NoExterior
        destinatario_NumInt.Text = cliente.NoInterior
        destinatario_Col.Text = cliente.Colonia
        destinatario_Ref.Text = cliente.ReferenciaDomicilio
        destinatario_Municipio.Text = cliente.Municipio
        destinatario_Estado.Text = cliente.Estado
        destinatario_CP.Text = cliente.CP
    End Sub

    Private Sub comboOrigen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboOrigen.SelectedIndexChanged

    End Sub

    Private Sub Combo_MTraslado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Combo_MTraslado.SelectedIndexChanged
        If Combo_MTraslado.SelectedIndex = 4 Then
            btnPropietarios.Visible = True
        Else
            btnPropietarios.Visible = False
        End If
    End Sub

    Private Sub btnPropietarios_Click(sender As Object, e As EventArgs) Handles btnPropietarios.Click
        Dim propietarios As New frmExportacionPropietarios(complemento.idComplemento)
        propietarios.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Panel_Destinatario.Visible = True
        ElseIf CheckBox1.Checked = False Then
            Panel_Destinatario.Visible = False
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim CSat = New dbCatalogosSat
        Csat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb)
        Dim fbc As New frmBuscadorCatalogosSat(5)
        fbc.Cat = CSat
        fbc.CerrarCon = True
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            receptor_pais.Text = fbc.Clave
        End If
        fbc.Dispose()
    End Sub

    Private Sub txtIncoterm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtIncoterm.SelectedIndexChanged
        If txtIncoterm.SelectedIndex = 0 Then
            txt_incoterms.Text = "COSTE Y FLETE (PUERTO DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 1 Then
            txt_incoterms.Text = "COSTE, SEGURO Y FLETE (PUERTO DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 2 Then
            txt_incoterms.Text = "TRANSPORTE PAGADO HASTA (EL LUGAR DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 3 Then
            txt_incoterms.Text = "TRANSPORTE Y SEGURO PAGADOS HASTA (LUGAR DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 4 Then
            txt_incoterms.Text = "ENTREGADA EN FRONTERA (LUGAR CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 5 Then
            txt_incoterms.Text = "ENTREGADA EN LUGAR."
        ElseIf txtIncoterm.SelectedIndex = 6 Then
            txt_incoterms.Text = "ENTREGADA EN TERMINAL."
        ElseIf txtIncoterm.SelectedIndex = 7 Then
            txt_incoterms.Text = "ENTREGADA SOBRE BUQUE (PUERTO DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 8 Then
            txt_incoterms.Text = "ENTREGADA EN MUELLE (PUERTO DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 9 Then
            txt_incoterms.Text = "ENTREGADA DERECHOS NO PAGADOS (LUGAR DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 10 Then
            txt_incoterms.Text = "ENTREGADA DERECHOS PAGADOS (LUGAR DE DESTINO CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 11 Then
            txt_incoterms.Text = "EN FABRICA (LUGAR CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 12 Then
            txt_incoterms.Text = "FRANCO TRANSPORTISTA (LUGAR DESIGNADO)."
        ElseIf txtIncoterm.SelectedIndex = 13 Then
            txt_incoterms.Text = "FRANCO AL COSTADO DEL BUQUE (PUERTO DE CARGA CONVENIDO)."
        ElseIf txtIncoterm.SelectedIndex = 14 Then
            txt_incoterms.Text = "FRANCO A BORDO (PUERTO DE CARGA CONVENIDO)."
        End If
    End Sub

    Private Sub dgvMercancia_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvMercancia.CellContentClick

    End Sub
End Class