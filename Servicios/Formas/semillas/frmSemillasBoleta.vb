Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmSemillasBoleta
    Dim conexion As MySqlConnection
    Dim comm As MySqlCommand
    Dim boletas As dbSemillasBoleta
    Dim productos As dbInventario
    Dim productores As dbproveedores
    Dim id As Integer
    Dim folio As Integer
    Dim fecha As Date
    Dim productor As dbproveedores
    Dim chofer As String
    Dim producto As dbInventario
    Dim peso As Double
    Dim humedad As Double
    Dim impurezas As Double
    Dim granoDanado As Double
    Dim granoQuebrado As Double
    Dim castigoHumedad As Double
    Dim castigoImpurezas As Double
    Dim castigoGranoDanado As Double
    Dim castigoGranoQuebrado As Double
    Dim castigoTotal As Double
    Dim pesoFinal As Double
    Dim porcentajeHumedad As Double
    Dim porcentajeImpurezas As Double
    Dim porcentajeGranoDanado As Double
    Dim porcentajeGranoQuebrado As Double
    Dim editar As Boolean
    Dim tipoBoleta As Char
    Dim hora As String
    Dim camion As String
    Dim placas As String
    Dim analista As String
    Dim pesador As String
    Dim descargo As String
    Dim portero As String
    Dim bodega As String
    Dim destino As String
    Dim variedad As String
    Dim existe As Boolean
    Dim IdDetalle As Integer
    Dim cliente As dbClientes

    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDi2 As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim TipoImpresora As String
    Property boletaP As dbSemillasBoleta = Nothing

    Dim guardado As Boolean = False
    Dim importe As Double
    Dim precio As Double
    Dim tara As Double
    Dim brutosinanalizar As Double
    Dim horaSalida As String
    Dim tamBoleta As Integer = 0

    Dim serie As String = ""
    Dim descuentoH As Double
    Dim descuentoI As Double
    Dim descuentoQ As Double
    Dim descuentoD As Double
    Dim opciones As dbOpciones
    Dim EsNuevo As Boolean = False
    Public pesoBrutoAnalizado As Double
    Enum Accion
        agregar = 1
        modificar = 2
    End Enum
 
    Dim op As Accion

    Public Sub New(ByVal ptamBoleta As Integer, ByVal ocultarPrecio As Boolean)
        productos = New dbInventario(MySqlcon)
        productores = New dbproveedores(MySqlcon)
        cliente = New dbClientes(MySqlcon)
        boletas = New dbSemillasBoleta(MySqlcon)
        opciones = New dbOpciones(MySqlcon)
        tamBoleta = ptamBoleta
        comm = New MySqlCommand
        comm.Connection = MySqlcon
        InitializeComponent()
        txtClaveProductor.Focus()
        'checarConfiguracion()
        folio = boletas.obtenFolio()
        autoCompletadoCajasTexto()
        txtFolio.Text = folio
        'txtFolio.Enabled = False
        op = Accion.agregar
        tipoBoleta = "E"
        'llenarCombos()
        limpiarCampos()
        If tamBoleta = 1 Then
            Me.Height = 383
            Label36.Visible = False
            Label37.Visible = False
            txtTara.Visible = False
            txtBrutoSinAnalizar.Visible = False
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        If ocultarPrecio = False Then
            Label40.Visible = False
            txtPrecio.Visible = False
            Label34.Visible = False
            txtImporte.Visible = False
        Else
            Label40.Visible = True
            txtPrecio.Visible = True
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        txtClaveProductor.Focus()
    End Sub
    Public Sub New(ByVal tipo As Char, ByVal pId As Integer, ptamboleta As Integer, ByVal ocultarPrecio As Boolean)
        id = pId
        tipoBoleta = tipo
        boletas = New dbSemillasBoleta(MySqlcon)
        productos = New dbInventario(MySqlcon)
        productores = New dbproveedores(MySqlcon)
        cliente = New dbClientes(MySqlcon)
        opciones = New dbOpciones(MySqlcon)
        tamBoleta = ptamboleta
        'conexion = New MySqlConnection(ruta)
        'conexion.Open()
        comm = New MySqlCommand
        comm.Connection = MySqlcon
        InitializeComponent()
        txtClaveProductor.Focus()
        checarConfiguracion()
        folio = boletas.obtenFolio()
        autoCompletadoCajasTexto()
        txtFolio.Text = folio
        'txtFolio.Enabled = False
        limpiarCampos()
        If tamBoleta = 1 Then
            Me.Height = 383
            Label36.Visible = False
            Label37.Visible = False
            txtTara.Visible = False
            txtBrutoSinAnalizar.Visible = False
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        If ocultarPrecio = False Then
            Label40.Visible = False
            txtPrecio.Visible = False
            Label34.Visible = False
            txtImporte.Visible = False
        Else
            Label40.Visible = True
            txtPrecio.Visible = True
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        txtClaveProductor.Focus()
        'llenaDatos()
    End Sub


    Public Sub New(ByVal tipo As Char, ByVal pcodigo As String, ByVal pPeso As Double, pIdDetalle As Integer, ptamBoleta As Integer, bodega As String, ByVal ocultarPrecio As Boolean)
        'id = pId

        InitializeComponent()
        txtClaveProductor.Focus()
        tipoBoleta = tipo
        peso = pPeso
        tamBoleta = ptamBoleta
        boletas = New dbSemillasBoleta(MySqlcon)
        productos = New dbInventario(MySqlcon)
        productores = New dbproveedores(MySqlcon)
        cliente = New dbClientes(MySqlcon)
        opciones = New dbOpciones(MySqlcon)
        comm = New MySqlCommand
        comm.Connection = MySqlcon
        Me.bodega = bodega
        id = boletas.DaIdBoletaDeDetalle(pIdDetalle)
        If id = 0 Then
            IdDetalle = pIdDetalle
            nuevo()
            'folio = boletas.obtenFolio()
            txtFolio.Enabled = False
            'txtFolio.Text = folio
            txtPeso.Text = Format(peso, "#,###,##0")
            txtClaveProducto.Text = pcodigo
            If productos.BuscaArticulo(txtClaveProducto.Text, 1) Then
                comboProductos.Text = productos.Nombre
                producto = productos
                id = producto.ID
                checarConfiguracion()
                btnCambiar.Enabled = True
            Else
                comboProductos.Text = ""
                btnCambiar.Enabled = False
                limpiaCamposNumeros()
            End If
            comboBodega.Text = Me.bodega
            'autoCompletadoCajasTexto()
        Else
            'boletaP = New dbSemillasBoleta(id)
            IdDetalle = pIdDetalle
            boletaP = boletas.buscar(New dbSemillasBoleta(id))
            boletaP.producto = New dbInventario(boletaP.producto.ID, MySqlcon)
            boletaP.productor = New dbproveedores(boletaP.productor.ID, MySqlcon)
            txtPeso.Text = Format(peso, "#,###,##0")
            llenaCampos()
            txtImporte.Text = Format(CDbl(txtImporte.Text), opciones._formatoTotal)
            parseaDatosNumeros()

            txtPeso.Text = peso

            guardado = True
            op = Accion.modificar
        End If

        calculaCastigoTotal()
        txtClaveProducto.Enabled = False
        btnBuscarProducto.Enabled = False
        btnBuscar.Visible = False
        btnNuevo.Visible = False
        If tamBoleta = 1 Then
            Me.Height = 383
            Label36.Visible = False
            Label37.Visible = False
            txtTara.Visible = False
            txtBrutoSinAnalizar.Visible = False
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        If ocultarPrecio = False Then
            Label40.Visible = False
            txtPrecio.Visible = False
            Label34.Visible = False
            txtImporte.Visible = False
        Else
            Label40.Visible = True
            txtPrecio.Visible = True
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        txtPrecio.Text = Format(CDbl(txtPrecio.Text), opciones._formatoTotal)


        'llenaDatos()
    End Sub
    Public Sub New(ByRef pBoleta As dbSemillasBoleta, ptamboleta As Integer, ByVal ocultarPrecio As Boolean)
        InitializeComponent()
        txtClaveProductor.Focus()
        boletas = New dbSemillasBoleta(MySqlcon)
        productos = New dbInventario(MySqlcon)
        productores = New dbproveedores(MySqlcon)
        cliente = New dbClientes(MySqlcon)
        opciones = New dbOpciones(MySqlcon)
        comm = New MySqlCommand
        tamBoleta = ptamboleta
        comm.Connection = MySqlcon
        boletaP = boletas.buscar(pBoleta)
        boletaP.producto = New dbInventario(boletaP.producto.ID, MySqlcon)
        boletaP.productor = New dbproveedores(boletaP.productor.ID, MySqlcon)
        id = boletaP.producto.ID
        tipoBoleta = boletaP.tipoBoleta
        'txtFolio.Enabled = False
        op = Accion.modificar
        'btnGuardar.Enabled = False
        autoCompletadoCajasTexto()
        llenaDatos()
        llenaCampos()
        If tamBoleta = 1 Then
            Me.Height = 383
            Label36.Visible = False
            Label37.Visible = False
            txtTara.Visible = False
            txtBrutoSinAnalizar.Visible = False
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        If ocultarPrecio = False Then
            Label40.Visible = False
            txtPrecio.Visible = False
            Label34.Visible = False
            txtImporte.Visible = False
        Else
            Label40.Visible = True
            txtPrecio.Visible = True
            Label34.Visible = True
            txtImporte.Visible = True
        End If
        'btnCambiar.Enabled = False

    End Sub
    Private Sub frmSemillasBoleta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try

        If tipoBoleta = "E" Then
            Me.Text = "Boleta de entrada"
            'comboDestino.Visible = False
            lblTiempo.Text = "Hora de entrada:"

        ElseIf tipoBoleta = "S" Then
            Me.Text = "Boleta de salida"
            lblTiempo.Text = "Hora de entrada:"
            comboProductores.Enabled = True
            'Button1.Visible = False
            'btnAgregarProductor.Visible = False
            'comboDestino.Visible = False
            lblTipoBoleta.Text = "Cliente:"
            txtClaveProductor.Visible = True
            comboProductores.Visible = True

        End If
        Me.Show()
        txtClaveProductor.Focus()

    End Sub
    Private Function calculaCastigo(ByVal peso As Double, ByVal porcentajeReal As Double, ByVal porcentajePermitido As Double, ByVal tipoCastigo As String) As Double
        Dim res As Double
        Select Case tipoCastigo
            Case "H"
                If descuentoH > 0 Then
                    If porcentajeReal > porcentajePermitido Then
                        res = (porcentajeReal - porcentajePermitido) * descuentoH
                    Else
                        res = 0
                    End If
                Else
                    res = ((porcentajeReal - porcentajePermitido) * peso) / 100
                    If res < 0 Then
                        res = 0
                    End If
                End If
            Case "I"
                If descuentoI > 0 Then
                    If porcentajeReal > porcentajePermitido Then
                        res = (porcentajeReal - porcentajePermitido) * descuentoI
                    Else
                        res = 0
                    End If
                Else
                    res = ((porcentajeReal - porcentajePermitido) * peso) / 100
                    If res < 0 Then
                        res = 0
                    End If
                End If
            Case "Q"
                If descuentoQ > 0 Then
                    If porcentajeReal > porcentajePermitido Then
                        res = (porcentajeReal - porcentajePermitido) * descuentoQ
                    Else
                        res = 0
                    End If
                Else
                    res = ((porcentajeReal - porcentajePermitido) * peso) / 100
                    If res < 0 Then
                        res = 0
                    End If
                End If
            Case "D"
                If descuentoD > 0 Then
                    If porcentajeReal > porcentajePermitido Then
                        res = (porcentajeReal - porcentajePermitido) * descuentoD
                    Else
                        res = 0
                    End If
                Else
                    res = ((porcentajeReal - porcentajePermitido) * peso) / 100
                    If res < 0 Then
                        res = 0
                    End If
                End If
        End Select
        Return res
    End Function

    Private Sub calculaCastigoTotal()
        If EsNuevo = False Then
            If Not producto Is Nothing Then
                If brutosinanalizar > 0 Then
                    peso = brutosinanalizar
                End If
                If txtPeso.Text <> "" Then
                    castigoHumedad = Math.Round(calculaCastigo(peso, humedad, porcentajeHumedad, "H"), 0)
                    castigoImpurezas = Math.Round(calculaCastigo(peso, impurezas, porcentajeImpurezas, "I"), 0)
                    castigoGranoDanado = Math.Round(calculaCastigo(peso, granoDanado, porcentajeGranoDanado, "D"), 0)
                    castigoGranoQuebrado = Math.Round(calculaCastigo(peso, granoQuebrado, porcentajeGranoQuebrado, "Q"), 0)
                    txtCastigoHumedad.Text = Format(castigoHumedad, "#,###,##0")
                    txtCastigoImpurezas.Text = Format(castigoImpurezas, "#,###,##0")
                    txtCastigoGranoQ.Text = Format(castigoGranoQuebrado, "#,###,##0")
                    txtCastigoGranoD.Text = Format(castigoGranoDanado, "#,###,##0")
                    castigoTotal = castigoHumedad + castigoImpurezas + castigoGranoDanado + castigoGranoQuebrado
                    pesoFinal = Math.Round((peso - castigoTotal), 0)
                    txtCastigoTotal.Text = Format(castigoTotal, "#,###,##0")
                    txtPesoFinal.Text = Format(pesoFinal, "#,###,##0")
                    'producto = comboProductos.SelectedItem
                    If txtPrecio.Text <> "" Then
                        Dim aux As String = txtPrecio.Text
                        aux = aux.Replace("$", " ")
                        txtPrecio.Text = aux
                        If txtPeso.Text > "0" Then
                            precio = parsea(txtPrecio)
                        End If
                    End If
                    importe = precio * pesoFinal
                    txtImporte.Text = Format(importe, opciones._formatoTotal)
                Else
                    MsgBox("Debe indicar el peso para continuar", MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox("Debe seleccionar un producto", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        If checaCamposCastigos() Then
            parseaDatosNumeros()
            calculaCastigoTotal()
        Else
            MsgBox("Debe llenar los campos necesarios para realizar los calculos.", MsgBoxStyle.Exclamation)
            Return
        End If

    End Sub

    Private Function parsea(ByVal cajaTexto As TextBox) As Double
        If cajaTexto.Text = "" Then
            Return 0
        Else
            Return Double.Parse(cajaTexto.Text)
        End If
    End Function

    Private Sub parseaDatos()
        folio = Integer.Parse(txtFolio.Text)
        fecha = dtpFecha.Value
        'productor = checaProductor()
        chofer = comboChofer.Text
        'producto = comboProductos.SelectedItem
        If txtPeso.Text = "" Then
            peso = 0
        Else
            peso = parsea(txtPeso)
        End If
        If txtHumedad.Text = "" Then
            humedad = 0
        Else
            humedad = parsea(txtHumedad)
        End If
        If txtImpurezas.Text = "" Then
            impurezas = 0
        Else
            impurezas = parsea(txtImpurezas)
        End If
        If txtGranoD.Text = "" Then
            granoDanado = 0
        Else
            granoDanado = parsea(txtGranoD)
        End If
        If txtGranoQ.Text = "" Then
            granoQuebrado = 0
        Else
            granoQuebrado = parsea(txtGranoQ)
        End If
        If txtPorcentajeHumedad.Text = "" Then
            porcentajeHumedad = 0
        Else
            porcentajeHumedad = parsea(txtPorcentajeHumedad)
        End If
        If txtPorcentajeImpurezas.Text = "" Then
            porcentajeImpurezas = 0
        Else
            porcentajeImpurezas = parsea(txtPorcentajeImpurezas)
        End If
        If txtPorcentajeGranoD.Text = "" Then
            porcentajeGranoDanado = 0
        Else
            porcentajeGranoDanado = parsea(txtPorcentajeGranoD)
        End If
        If txtPorcentajeGranoQ.Text = "" Then
            porcentajeGranoQuebrado = 0
        Else
            porcentajeGranoQuebrado = parsea(txtPorcentajeGranoQ)
        End If
        If txtTara.Text = "" Then
            tara = 0
        Else
            tara = parsea(txtTara)
        End If
        If txtBrutoSinAnalizar.Text = "" Then
            brutosinanalizar = 0
        Else
            brutosinanalizar = parsea(txtBrutoSinAnalizar)
        End If
        hora = dtpHora.Value.ToString("HH:mm:ss")
        horaSalida = dtpHoraSalida.Value.ToString("HH:mm:ss")
        analista = comboAnalista.Text
        camion = comboCamion.Text
        placas = comboPlacas.Text
        pesador = comboPesador.Text
        descargo = comboDescargo.Text
        portero = comboPortero.Text
        If comboBodega.Text = "" Then
            bodega = ""
        Else
            bodega = comboBodega.Text
        End If

        variedad = comboVariedad.Text

        If txtSerie.Text = "" Then
            serie = ""
        Else
            serie = txtSerie.Text
        End If

        If txtPrecio.Text <> "" Then
            Dim aux As String = txtPrecio.Text
            aux = aux.Replace("$", "")
            txtPrecio.Text = aux
            If txtPrecio.Text > "0.00000" Then
                precio = parsea(txtPrecio)
            End If
        End If
        If txtPesoFinal.Text <> "" Then
            Dim aux As String = txtPesoFinal.Text
            aux = aux.Replace(",", String.Empty)
            If aux > "0.00000" Then
                pesoFinal = Math.Round(CDbl(aux), 0)
            End If
        End If
        'If objeto Is Nothing Then
        'Else
        '    Objeto.dobleP = txtPeso.Text
        'End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.BoletasAlta, PermisosN.Secciones.Semillas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If checaCampos() Then
            parseaDatos()

            Dim boleta As New dbSemillasBoleta(folio,
                                     fecha,
                                     productor,
                                     chofer.ToUpper,
                                     producto,
                                     peso,
                                     humedad,
                                     impurezas,
                                     granoDanado,
                                     granoQuebrado,
                                     castigoHumedad,
                                     castigoImpurezas,
                                     castigoGranoDanado,
                                     castigoGranoQuebrado,
                                     castigoTotal,
                                     pesoFinal,
                                     hora,
                                     camion,
                                     placas,
                                     analista.ToUpper,
                                     pesador.ToUpper,
                                     descargo.ToUpper,
                                     portero.ToUpper,
                                     tipoBoleta.ToString(),
                                     bodega,
                                     variedad.ToUpper,
                                     porcentajeHumedad,
                                     porcentajeImpurezas,
                                     porcentajeGranoQuebrado,
                                     porcentajeGranoDanado,
                                     0,
                                     importe, IdDetalle, precio, tara, brutosinanalizar, horaSalida, cliente.ID, serie)
            Try
                If op = Accion.modificar Then
                    boleta.id = boletaP.id
                    boletas.actualiza(boleta)
                    boletaP = boletas.buscar(boleta)
                    pesoBrutoAnalizado = boletaP.pesoFinal
                    txtClaveProductor.Focus()
                    confirmaImpresion()
                Else
                    If boletas.checaFolioRepetido(serie, folio) = False Then
                        boletas.agregar(boleta)
                        boleta.id = boletas.ultimoId
                        boletaP = boletas.buscar(boleta)
                        guardado = True
                        pesoBrutoAnalizado = boletaP.pesoFinal
                        txtClaveProductor.Focus()
                        confirmaImpresion()
                    Else
                        MsgBox("Folio repetido.", MsgBoxStyle.Exclamation)
                    End If
                End If
                'Me.Dispose()
                If IdDetalle = 0 Then
                    nuevo()
                Else
                    Me.Close()
                End If
            Catch ex As Exception
                MsgBox("No se pudo guardar la boleta.", MsgBoxStyle.Exclamation)
            End Try

        Else
            MsgBox("Debe indicar el peso para continuar", MsgBoxStyle.Exclamation)
            Return
        End If

    End Sub

    Private Sub obtenerConfiguracion()
        Dim dr As MySqlDataReader
        If existe Then

            dr = boletas.obtenerConfigiracion(id)
            While dr.Read()
                txtPorcentajeHumedad.Text = dr(1)
                txtPorcentajeImpurezas.Text = dr(2)
                txtPorcentajeGranoD.Text = dr(3)
                txtPorcentajeGranoQ.Text = dr(4)
                editar = dr(5)
                descuentoH = dr("castigoH")
                descuentoI = dr("castigoI")
                descuentoQ = dr("castigoQ")
                descuentoD = dr("castigoD")
            End While
            'dr.Close()
            If editar Then
                dtpFecha.Enabled = True
                txtPorcentajeHumedad.Enabled = True
                txtPorcentajeImpurezas.Enabled = True
                txtPorcentajeGranoD.Enabled = True
                txtPorcentajeGranoQ.Enabled = True
            End If
        Else
            Dim frm As New frmSemillasConfiguracion(MySqlcon, id)
            frm.ShowDialog()
            'comm.CommandText = "select * from tblsemillasconfiguracion where idproducto=" + id.ToString() + ";"
            dr = boletas.obtenerConfigiracion(id)
            While dr.Read()
                txtPorcentajeHumedad.Text = dr(1)
                txtPorcentajeImpurezas.Text = dr(2)
                txtPorcentajeGranoD.Text = dr(3)
                txtPorcentajeGranoQ.Text = dr(4)
                editar = dr(5)
                descuentoH = dr("castigoH")
                descuentoI = dr("castigoI")
                descuentoQ = dr("castigoQ")
                descuentoD = dr("castigoD")
            End While
            'dr.Close()
            If editar Then
                dtpFecha.Enabled = True
                txtPorcentajeHumedad.Enabled = True
                txtPorcentajeImpurezas.Enabled = True
                txtPorcentajeGranoD.Enabled = True
                txtPorcentajeGranoQ.Enabled = True
            End If
        End If
        dr.Close()
    End Sub

    Private Sub llenaCombo(combo As ComboBox, lista As List(Of Object))
        For Each obj As Object In lista
            combo.Items.Add(obj)
        Next
    End Sub


    Private Sub btnCambiar_Click(sender As Object, e As EventArgs) Handles btnCambiar.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Semillas.Configuracion, PermisosN.Secciones.Semillas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If

        If op = Accion.modificar Then
            Dim frm As New frmSemillasConfiguracion(MySqlcon, boletaP)
            frm.ShowDialog()
            Dim b As dbSemillasBoleta = frm.boleta
            llenaCamposCastigos(b)
        Else
            Dim bo As New dbSemillasBoleta()
            bo.porcentajeHumedad = parsea(txtPorcentajeHumedad)
            bo.porcentajeImpurezas = parsea(txtPorcentajeImpurezas)
            bo.porcentajeGranoD = parsea(txtPorcentajeGranoD)
            bo.porcentajeGranoQ = parsea(txtPorcentajeGranoQ)
            Dim frm As New frmSemillasConfiguracion(MySqlcon, bo)
            frm.ShowDialog()
            bo = frm.boleta
            llenaCamposCastigos(bo)
            'existe = True
            'obtenerConfiguracion()
        End If
    End Sub

    Private Sub checarConfiguracion()

        Dim dr As MySqlDataReader
        'comm.CommandText = "select * from tblsemillasconfiguracion where idProducto=" + id.ToString() + ";"
        dr = boletas.obtenerConfigiracion(id)
        If dr.HasRows = False Then
            existe = False
        Else
            existe = True
        End If
        dr.Close()
        obtenerConfiguracion()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAgregarProductor.Click
        'Dim frm As New frmSemillasProductores()
        If tipoBoleta = "E" Then
            Dim frm As New frmProveedores(0, 0, "")
            frm.ShowDialog()
        Else
            Dim frm As New frmClientes(0, 0)
            frm.ShowDialog()
        End If
        'comboProductores.Items.Clear()
        'For Each obj As Proveedor In productores.listaProveedores
        '    comboProductores.Items.Add(obj)
        'Next
    End Sub


    Private Sub btnConsultar_Click(sender As Object, e As EventArgs)
        Dim frm As New frmSemillasConsulta(frmSemillasConsulta.tipoConsulta.boletas)
        frm.Show()

    End Sub

    'Private Function checaProductor() As dbproveedores
    '    If comboProductores.Text = "" Then
    '        Return Nothing
    '    Else
    '        Return comboProductores.SelectedItem
    '    End If
    'End Function

    Private Sub llenaCampos()
        txtPeso.Text = Format(boletaP.peso, "#,###,##0")
        precio = boletaP.precio
        If boletaP.tipoBoleta = "E" Then
            productor = boletaP.productor
        Else
            productor = Nothing
            cliente = New dbClientes(boletaP.idCliente, MySqlcon)

        End If
        tipoBoleta = boletaP.tipoBoleta
        producto = boletaP.producto
        txtFolio.Text = boletaP.folio
        dtpFecha.Value = Date.Parse(boletaP.fecha)
        txtClaveProductor.Text = boletaP.producto.Clave
        If boletaP.tipoBoleta = "E" Then
            comboProductores.Text = boletaP.productor.Nombre
            txtClaveProductor.Text = boletaP.productor.Clave
            productor = boletaP.productor
        Else
            comboProductores.Text = cliente.Nombre
            txtClaveProductor.Text = cliente.Clave

        End If

        comboProductos.Text = boletaP.producto.Nombre
        txtClaveProducto.Text = boletaP.producto.Clave
        comboVariedad.Text = boletaP.variedad
        dtpHora.Value = Date.Parse(boletaP.hora)
        dtpHoraSalida.Value = Date.Parse(boletaP.horaSalida)
        comboCamion.Text = boletaP.camion
        comboChofer.Text = boletaP.chofer
        comboAnalista.Text = boletaP.analista
        comboDescargo.Text = boletaP.descargo
        comboPlacas.Text = boletaP.placas
        comboBodega.Text = boletaP.bodega
        comboPesador.Text = boletaP.pesador
        comboPortero.Text = boletaP.portero

        txtHumedad.Text = boletaP.humedad
        txtImpurezas.Text = boletaP.impurezas
        txtGranoQ.Text = boletaP.granoQuebrado
        txtGranoD.Text = boletaP.granoDanado
        txtPorcentajeHumedad.Text = Format(boletaP.porcentajeHumedad, "#,###,##0")
        txtPorcentajeImpurezas.Text = Format(boletaP.porcentajeImpurezas, "#,###,##0")
        txtPorcentajeGranoQ.Text = Format(boletaP.porcentajeGranoQ, "#,###,##0")
        txtPorcentajeGranoD.Text = Format(boletaP.porcentajeGranoD, "#,###,##0")
        txtCastigoHumedad.Text = Format(boletaP.castigoHumedad, "#,###,##0")
        txtCastigoImpurezas.Text = Format(boletaP.castigoImpurezas, "#,###,##0")
        txtCastigoGranoQ.Text = Format(boletaP.castigoGranoQuebrado, "#,###,##0")
        txtCastigoGranoD.Text = Format(boletaP.castigoGranoDanado, "#,###,##0")
        txtCastigoTotal.Text = Format(boletaP.castigoTotal, "#,###,##0")
        txtPesoFinal.Text = Format(boletaP.pesoFinal, "#,###,##0")
        txtTara.Text = Format(boletaP.tara, "#,###,##0")
        txtBrutoSinAnalizar.Text = Format(boletaP.brutosinanalizar, "#,###,##0")
        txtImporte.Text = Format(boletaP.importe, opciones._formatoTotal)
        txtSerie.Text = boletaP.serie
        txtPrecio.Text = Format(boletaP.precio, "$#,###,##0.00######")
        If boletaP.tipoBoleta = "E" Then
            Me.Text = "Boleta de entrada"
            lblTiempo.Text = "Hora de entrada:"
            btnAgregarProductor.Visible = True
            lblTipoBoleta.Text = "Productor:"
            txtClaveProductor.Visible = True
            comboProductores.Visible = True
        Else
            Me.Text = "Boleta de salida"
            lblTiempo.Text = "Hora de Entrada:"
        End If
    End Sub

    Private Sub parseaDatosNumeros()
        tara = parsea(txtTara)
        brutosinanalizar = parsea(txtBrutoSinAnalizar)
        peso = parsea(txtPeso)
        humedad = parsea(txtHumedad)
        impurezas = parsea(txtImpurezas)
        granoDanado = parsea(txtGranoD)
        granoQuebrado = parsea(txtGranoQ)
        porcentajeHumedad = parsea(txtPorcentajeHumedad)
        porcentajeImpurezas = parsea(txtPorcentajeImpurezas)
        porcentajeGranoDanado = parsea(txtPorcentajeGranoD)
        porcentajeGranoQuebrado = parsea(txtPorcentajeGranoQ)
        tara = parsea(txtTara)
        brutosinanalizar = parsea(txtBrutoSinAnalizar)
    End Sub

    Private Sub comboVariedad_KeyDown(sender As Object, e As KeyEventArgs) Handles comboVariedad.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpHora.Focus()
        End If
    End Sub

    Private Sub comboCamion_KeyDown(sender As Object, e As KeyEventArgs) Handles comboCamion.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboPlacas.Focus()
        End If
    End Sub

    Private Sub comboChofer_KeyDown(sender As Object, e As KeyEventArgs) Handles comboChofer.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboCamion.Focus()
        End If
    End Sub

    Private Sub txtAlmacenista_KeyDown(sender As Object, e As KeyEventArgs) Handles comboAnalista.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboDescargo.Focus()
        End If
    End Sub

    Private Sub comboDescargo_KeyDown(sender As Object, e As KeyEventArgs) Handles comboDescargo.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboBodega.Focus()
        End If
    End Sub

    Private Sub comboPlacas_KeyDown(sender As Object, e As KeyEventArgs) Handles comboPlacas.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboPesador.Focus()
        End If
    End Sub

    Private Sub comboBodega_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            comboPesador.Focus()
        End If
    End Sub

    Private Sub comboBodega_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub comboPesador_KeyDown(sender As Object, e As KeyEventArgs) Handles comboPesador.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboPortero.Focus()
        End If
    End Sub

    Private Sub comboPortero_KeyDown(sender As Object, e As KeyEventArgs) Handles comboPortero.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboAnalista.Focus()
        End If
    End Sub

    Private Sub txtPeso_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPeso.KeyDown
        If e.KeyCode = Keys.Enter Then
            If tamBoleta = 0 Then
                txtTara.Focus()
            Else
                txtHumedad.Focus()
            End If
        End If
    End Sub

    Private Sub txtPeso_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPeso.KeyPress
        'If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or e.KeyChar = "." Then
        '    e.Handled = False
        'Else
        '    e.Handled = True
        'End If
        NumConFrac(txtPeso, e)
    End Sub

    Private Sub txtHumedad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHumedad.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtImpurezas.Focus()
        End If
    End Sub

    Private Sub txtHumedad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHumedad.KeyPress
        NumConFrac(txtHumedad, e)
    End Sub

    Private Sub txtImpurezas_KeyDown(sender As Object, e As KeyEventArgs) Handles txtImpurezas.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtGranoQ.Focus()
        End If
    End Sub

    Private Sub txtImpurezas_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImpurezas.KeyPress
        NumConFrac(txtImpurezas, e)
    End Sub

    Private Sub txtGranoQ_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGranoQ.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtGranoD.Focus()
        End If
    End Sub

    Private Sub txtGranoQ_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGranoQ.KeyPress
        NumConFrac(txtGranoQ, e)
    End Sub

    Private Sub txtGranoD_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGranoD.KeyDown
        If e.KeyCode = Keys.Enter Then
            If tamBoleta = 1 Then
                btnGuardar.Focus()
            Else
                comboVariedad.Focus()
            End If
            If checaCamposCastigos() Then
                parseaDatosNumeros()
                calculaCastigoTotal()
            Else
                MsgBox("Debe llenar todos los campos para continuar", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub txtGranoD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGranoD.KeyPress
        NumConFrac(txtGranoD, e)
    End Sub

    Private Sub txtPesoFinal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPesoFinal.KeyPress
        If Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub frmSemillasBoleta_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        'If Not objeto Is Nothing Then
        '    If guardado Then
        '        objeto.booleanoP = True
        '        boletaP = boletas.buscarFolio(New dbBoleta(folio.ToString()))
        '        objeto.enteroP = boletaP.id
        '    Else
        '        objeto.booleanoP = False
        '    End If
        'End If
    End Sub

    Public Sub autoCompletaCampo(ByRef cajaTexto As ComboBox, ByVal columna As String)
        cajaTexto.Items.Clear()
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        dr = boletas.registros(columna)
        If dr.HasRows Then
            While dr.Read()
                cajaTexto.Items.Add(dr.GetString(0))
            End While
        End If
        dr.Close()
    End Sub
    Private Sub autoCompletadoCajasTexto()
        autoCompletaCampo(comboVariedad, "variedad")
        autoCompletaCampo(comboCamion, "camion")
        autoCompletaCampo(comboChofer, "chofer")
        autoCompletaCampo(comboAnalista, "analista")
        autoCompletaCampo(comboDescargo, "descargo")
        autoCompletaCampo(comboPlacas, "placas")
        autoCompletaCampo(comboPesador, "pesador")
        autoCompletaCampo(comboPortero, "portero")
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)


    End Sub

    Private Function checaCampos() As Boolean
        Dim res As Boolean = True
        If tipoBoleta = "S" Then

        Else
            If comboProductores.Text = "" Then
                comboProductores.BackColor = Color.Red
                res = False
            Else
                comboProductores.BackColor = Color.White
            End If
        End If
        If comboProductos.Text = "" Then
            comboProductos.BackColor = Color.Red
            res = False
        Else
            comboProductos.BackColor = Color.White
        End If
        Return res
    End Function

    Public Function checaCamposCastigos() As Boolean
        Dim res As Boolean = True
        If txtPeso.Text = "0" Or txtPeso.Text = "" Then
            txtPeso.BackColor = Color.Red
            res = False
        Else
            txtPeso.BackColor = Color.White
        End If
        If txtHumedad.Text = "" Then
            txtHumedad.BackColor = Color.Red
            res = False
        Else
            txtHumedad.BackColor = Color.White
        End If
        If txtImpurezas.Text = "" Then
            txtImpurezas.BackColor = Color.Red
            res = False
        Else
            txtImpurezas.BackColor = Color.White
        End If
        If txtGranoD.Text = "" Then
            txtGranoD.BackColor = Color.Red
            res = False
        Else
            txtGranoD.BackColor = Color.White
        End If
        If txtGranoQ.Text = "" Then
            txtGranoQ.BackColor = Color.Red
            res = False
        Else
            txtGranoQ.BackColor = Color.White
        End If
        Return res
    End Function

    Private Sub llenaDatos()
        productor = boletaP.productor
        producto = boletaP.producto
    End Sub


    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        buscar()
    End Sub

    Private Sub llenaCamposCastigos(ByVal boleta As dbSemillasBoleta)
        txtPorcentajeHumedad.Text = boleta.porcentajeHumedad
        txtPorcentajeImpurezas.Text = boleta.porcentajeImpurezas
        txtPorcentajeGranoD.Text = boleta.porcentajeGranoD
        txtPorcentajeGranoQ.Text = boleta.porcentajeGranoQ
    End Sub


    Private Sub btnBuscarProducto_Click(sender As Object, e As EventArgs) Handles btnBuscarProducto.Click
        buscarProducto()
    End Sub

    Private Sub buscarProducto()
        Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.ArticuloInv, 1, False, False, False)
        frm.ShowDialog()
        Dim prod As dbInventario = frm.Inventario
        If Not prod Is Nothing Then
            EsNuevo = False
            producto = New dbInventario(prod.ID, MySqlcon)
            Dim art As New dbInventarioPrecios(producto.ID, MySqlcon)
            precio = art.DaPrecioListaUno(producto.ID)
            txtPrecio.Text = Format(precio, opciones._formatoPrecioU)
            comboProductos.Text = producto.Nombre
            txtClaveProducto.Text = producto.Clave
            checarConfiguracion()
            btnCambiar.Enabled = True
        Else
            btnCambiar.Enabled = False
            txtPrecio.Text = ""
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click

        If guardado Then
            Imprimir()
        Else
            MsgBox("primero debe guardar los cambios")
        End If
    End Sub

    Private Sub txtHumedad_Leave(sender As Object, e As EventArgs) Handles txtHumedad.Leave
        If txtPeso.Text <> "" Then
            parseaDatosNumeros()
            calculaCastigoTotal()
            'txtHumedad.Text = formateaCadena(txtHumedad.Text)
        End If
    End Sub

    Private Sub txtImpurezas_Leave(sender As Object, e As EventArgs) Handles txtImpurezas.Leave
        If txtPeso.Text <> "" Then
            parseaDatosNumeros()
            If EsNuevo = False Then
                calculaCastigoTotal()
            End If
            ' txtImpurezas.Text = formateaCadena(txtImpurezas.Text)
        End If
    End Sub

    Private Sub txtGranoQ_Leave(sender As Object, e As EventArgs) Handles txtGranoQ.Leave
        If txtPeso.Text <> "" Then
            parseaDatosNumeros()
            calculaCastigoTotal()
            ' txtGranoQ.Text = formateaCadena(txtGranoQ.Text)
        End If
    End Sub

    Private Sub txtGranoD_Leave(sender As Object, e As EventArgs) Handles txtGranoD.Leave
        If txtPeso.Text <> "" Then
            parseaDatosNumeros()
            calculaCastigoTotal()
            ' txtGranoD.Text = formateaCadena(txtGranoD.Text)
            'End If
        End If
    End Sub

    Private Sub calculaCastigo2(ByVal peso As Double, ByVal real As Double, ByVal permitido As Double, ByVal res As Double, ByVal cajaTexto As TextBox)
        'res = calculaCastigo(peso, real, permitido)
        'cajaTexto.Text = res
    End Sub

    Public Sub NumConFrac(ByVal CajaTexto As Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." And Not CajaTexto.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtClaveProductor_TextChanged(sender As Object, e As EventArgs) Handles txtClaveProductor.TextChanged
        If tipoBoleta = "E" Then
            If productores.BuscaProveedor(txtClaveProductor.Text) Then
                comboProductores.Text = productores.Nombre
                productor = productores
            Else
                comboProductores.Text = ""
            End If
        Else
            If cliente.BuscaCliente(txtClaveProductor.Text) Then
                comboProductores.Text = cliente.Nombre
            Else
                comboProductores.Text = ""
            End If
        End If
    End Sub

    Private Sub txtClaveProducto_TextChanged(sender As Object, e As EventArgs) Handles txtClaveProducto.TextChanged
        If productos.BuscaArticulo(txtClaveProducto.Text, 1) Then
            comboProductos.Text = productos.Nombre
            producto = productos
            id = producto.ID
            Dim art As New dbInventarioPrecios(producto.ID, MySqlcon)
            precio = art.DaPrecioListaUno(producto.ID)
            txtPrecio.Text = Format(precio, opciones._formatoPrecioU)
            checarConfiguracion()
            btnCambiar.Enabled = True
        Else
            comboProductos.Text = ""
            txtPrecio.Text = ""
            btnCambiar.Enabled = False
            limpiaCamposNumeros()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim frm As New frmSemillasConsulta(frmSemillasConsulta.tipoConsulta.boletas)
        frm.ShowDialog()
        If Not frm.boleta Is Nothing Then
            boletaP = frm.boleta
            EsNuevo = False
            boletaP.producto = New dbInventario(boletaP.producto.ID, MySqlcon)
            If boletaP.tipoBoleta = "E" Then
                boletaP.productor = New dbproveedores(boletaP.productor.ID, MySqlcon)
            Else
                txtClaveProductor.Visible = True
                comboProductores.Visible = True
                cliente = New dbClientes(boletaP.idCliente, MySqlcon)
            End If
            tipoBoleta = boletaP.tipoBoleta
            llenaCampos()
            guardado = True
            op = Accion.modificar
        End If
    End Sub

    Private Sub nuevo()
        EsNuevo = True
        limpiarCampos()
        EsNuevo = False
        op = Accion.agregar
        folio = boletas.obtenFolio
        txtFolio.Text = folio
        btnCambiar.Enabled = False
    End Sub

    Private Sub limpiarCampos()
        producto = Nothing
        productor = Nothing
        txtPrecio.Text = ""
        txtClaveProducto.Text = ""
        txtClaveProductor.Text = ""
        comboProductores.Text = ""
        comboProductos.Text = ""
        comboVariedad.Text = ""
        comboBodega.Text = ""
        comboChofer.Text = ""
        comboCamion.Text = ""
        comboPlacas.Text = ""
        comboPesador.Text = ""
        comboPortero.Text = ""
        comboAnalista.Text = ""
        comboDescargo.Text = ""
        txtPeso.Text = "0"
        txtHumedad.Text = "0"
        txtImpurezas.Text = "0"
        txtGranoD.Text = "0"
        txtGranoQ.Text = "0"
        txtPorcentajeHumedad.Text = "0"
        txtPorcentajeImpurezas.Text = "0"
        txtPorcentajeGranoD.Text = "0"
        txtPorcentajeGranoQ.Text = "0"
        txtCastigoHumedad.Text = "0"
        txtCastigoImpurezas.Text = "0"
        txtCastigoGranoD.Text = "0"
        txtCastigoGranoQ.Text = "0"
        txtPesoFinal.Text = ""
        txtImporte.Text = ""
        txtTara.Text = ""
        txtSerie.Text = ""
        txtBrutoSinAnalizar.Text = ""
        btnCambiar.Enabled = False
        autoCompletadoCajasTexto()
    End Sub

    Private Sub limpiaCamposNumeros()
        txtPorcentajeHumedad.Text = ""
        txtPorcentajeImpurezas.Text = ""
        txtPorcentajeGranoD.Text = ""
        txtPorcentajeGranoQ.Text = ""
        txtCastigoHumedad.Text = ""
        txtCastigoImpurezas.Text = ""
        txtCastigoGranoD.Text = ""
        txtCastigoGranoQ.Text = ""
        txtPesoFinal.Text = ""
        txtImporte.Text = ""
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        nuevo()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub LlenaNodosImpresion()
        Dim O As New dbOpciones(MySqlcon)
        CuantosRenglones = 0
        Posicion = 0
        NumeroPagina = 1
        ImpND.Clear()
        Dim i As New dbSemillasBoleta(MySqlcon)
        ImpND.Add(New NodoImpresionN("", "fecha", convierteFecha(boletaP.fecha.ToString("yyyy/MM/dd")), 0), "fecha")
        'ImpND.Add(New NodoImpresionN("", "boleta", boletaP.folio.ToString("000"), 0), "Boleta")
        ImpND.Add(New NodoImpresionN("", "peso bruto", Format(boletaP.peso, "#,###,##0").PadLeft(10, " "), 0), "peso bruto")
        ImpND.Add(New NodoImpresionN("", "humedad", Format(boletaP.castigoHumedad, "#,###,##0"), 0), "humedad")
        ImpND.Add(New NodoImpresionN("", "quebrado", Format(boletaP.castigoGranoQuebrado, "#,###,##0"), 0), "quebrado")
        ImpND.Add(New NodoImpresionN("", "impurezas", Format(boletaP.castigoImpurezas, "#,###,##0"), 0), "impurezas")
        ImpND.Add(New NodoImpresionN("", "dañado", Format(boletaP.castigoGranoDanado, "#,###,##0"), 0), "dañado")
        ImpND.Add(New NodoImpresionN("", "total deducciones", Format(Math.Round(boletaP.castigoTotal, 2), "#,###,##0"), 0), "total deducciones")
        ImpND.Add(New NodoImpresionN("", "peso liquidar", Format(Math.Round(boletaP.pesoFinal, 2), "#,###,##0").PadLeft(10, " "), 0), "peso liquidar")
        Dim art As New dbInventarioPrecios(boletaP.producto.ID, MySqlcon)
        art.BuscaPrecio(boletaP.producto.ID, 1)
        ImpND.Add(New NodoImpresionN("", "precio/ton", Format(art.Precio, opciones._formatoPrecioU), 0), "precio/ton")
        ImpND.Add(New NodoImpresionN("", "importe", Format(boletaP.importe, opciones._formatoTotal), 0), "importe")
        ImpND.Add(New NodoImpresionN("", "FolioBoleta", boletaP.folio.ToString("000"), 0), "FolioBoleta")
        ImpND.Add(New NodoImpresionN("", "nombreProducto", boletaP.producto.Nombre, 0), "nombreProducto")
        If boletaP.tipoBoleta = "E" Then
            ImpND.Add(New NodoImpresionN("", "nombreProductor", boletaP.productor.Nombre, 0), "nombreProductor")
        Else
            ImpND.Add(New NodoImpresionN("", "nombreProductor", cliente.Nombre, 0), "nombreProductor")
        End If
        ImpND.Add(New NodoImpresionN("", "variedad", boletaP.variedad, 0), "variedad")
        ImpND.Add(New NodoImpresionN("", "chofer", boletaP.chofer, 0), "chofer")
        ImpND.Add(New NodoImpresionN("", "camion", boletaP.camion, 0), "camion")
        ImpND.Add(New NodoImpresionN("", "placas", boletaP.placas, 0), "placas")
        ImpND.Add(New NodoImpresionN("", "bodega", boletaP.bodega.ToString(), 0), "bodega")
        ImpND.Add(New NodoImpresionN("", "pesador", boletaP.pesador, 0), "pesador")
        ImpND.Add(New NodoImpresionN("", "almacenista", boletaP.analista, 0), "almacenista")
        ImpND.Add(New NodoImpresionN("", "portero", boletaP.portero, 0), "portero")
        ImpND.Add(New NodoImpresionN("", "descargo", boletaP.placas, 0), "descargo")
        ImpND.Add(New NodoImpresionN("", "porcentajeHumedad", Format(Math.Round(boletaP.humedad, 2), "#,###,##0").PadLeft(10, " "), 0), "porcentajeHumedad")
        ImpND.Add(New NodoImpresionN("", "porcentajeImpurezas", Format(Math.Round(boletaP.impurezas, 2), "#,###,##0").PadLeft(10, " "), 0), "porcentajeImpurezas")
        ImpND.Add(New NodoImpresionN("", "porcentajeQuebrado", Format(Math.Round(boletaP.granoQuebrado, 2), "#,###,##0").PadLeft(10, " "), 0), "porcentajeQuebrado")
        ImpND.Add(New NodoImpresionN("", "porcentajeDanado", Format(Math.Round(boletaP.granoDanado, 2), "#,###,##0").PadLeft(10, " "), 0), "porcentajeDanado")
        ImpND.Add(New NodoImpresionN("", "HumedadPermitida", Format(Math.Round(boletaP.porcentajeHumedad, 2), "#,###,##0").PadLeft(10, " "), 0), "HumedadPermitida")
        ImpND.Add(New NodoImpresionN("", "ImpurezasPermitidas", Format(Math.Round(boletaP.porcentajeImpurezas, 2), "#,###,##0").PadLeft(10, " "), 0), "ImpurezasPermitidas")
        ImpND.Add(New NodoImpresionN("", "QuebradoPermitido", Format(Math.Round(boletaP.porcentajeGranoQ, 2), "#,###,##0").PadLeft(10, " "), 0), "QuebradoPermitido")
        ImpND.Add(New NodoImpresionN("", "DanadoPermitido", Format(Math.Round(boletaP.porcentajeGranoD, 2), "#,###,##0").PadLeft(10, " "), 0), "DanadoPermitido")
        ImpND.Add(New NodoImpresionN("", "hora", boletaP.hora, 0), "hora")
        ImpND.Add(New NodoImpresionN("", "horasalida", boletaP.horaSalida, 0), "horasalida")
        ImpND.Add(New NodoImpresionN("", "tara", Format(Math.Round(boletaP.tara, 2), "#,###,##0").PadLeft(10, " "), 0), "tara")
        ImpND.Add(New NodoImpresionN("", "brutosinanalizar", Format(Math.Round(boletaP.brutosinanalizar, 2), "#,###,##0").PadLeft(10, " "), 0), "brutosinanalizar")
        ImpND.Add(New NodoImpresionN("", "netoSinAnalizar", Format(Math.Round(boletaP.peso - boletaP.tara, 2), "#,###,##0").PadLeft(10, " "), 0), "netoSinAnalizar")

    End Sub
    Private Sub Imprimir()
        Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        PrintDialog1.PrinterSettings.Copies = 1
        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(boletaP.fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(boletaP.fecha), "yyyy") + "\" + Format(CDate(boletaP.fecha), "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(CDate(boletaP.fecha), "yyyy") + "\" + Format(CDate(boletaP.fecha), "MM")
        'Dim SA As New dbSucursalesArchivos
        Dim TipoImpresora As String
        Impresora = Archivos.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.SemillasLiquidacion)
        TipoImpresora = SA.TipoImpresora
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        PrintDocument1.DocumentName = " Boleta" + boletaP.serie + boletaP.folio.ToString("000")
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.WriteSettings()

            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        LlenaNodosImpresion()
        If TipoImpresora = 0 Then
            LlenaNodos(suc.ID, TiposDocumentos.SemillasBoleta)
        Else
            LlenaNodos(suc.ID, TiposDocumentos.SemillasBoleta + 1000)
        End If

        PrintDocument1.Print()


    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            'e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If


        e.HasMorePages = MasPaginas
    End Sub

    Private Sub LlenaNodos(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
        Dim I As New dbImpresionesN(MySqlcon)
        Dim Fs As FontStyle
        ImpNDi.Clear()
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Try
            dr = I.DaNodos(pDocumento, pidSucursal)
            While dr.Read
                Select Case dr("fuentestilo")
                    Case 1
                        Fs = FontStyle.Bold
                    Case 2
                        Fs = FontStyle.Italic
                    Case 0
                        Fs = FontStyle.Regular
                    Case 8
                        Fs = FontStyle.Strikeout
                    Case 4
                        Fs = FontStyle.Underline
                End Select
                ImpNDi.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs, GraphicsUnit.Point), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")))
            End While
            dr.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Function InsertaEnters(ByVal Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Or car = Chr(10) Then
                If car = Chr(13) Then C += 1
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.SemillasBoleta, GlobalIdSucursalDefault)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.SemillasBoleta + 1000, GlobalIdSucursalDefault)
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub

    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)

        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        Dim codigos As New Collection

        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try

            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.SemillasBoleta, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.SemillasBoleta + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos Detalles            

        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            'For Each n As NodoImpresionN In ImpNDi
            '    Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
            '    If n.DataPropertyName.Contains("descripcion") And n.Renglon = 1 Then

            '    End If
            'Next
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra
            '*************Segundoo Renglon***************
            YExtra = 0
            YExtra2 = 0
            Dim HayRenglon2 As Boolean = False
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    HayRenglon2 = True
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon2 Then YCoord = YCoord + 4 + YExtra
            '**************************************
            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "boleta" Then
                    ncb = n
                    codigos.Add(ncb)
                End If

                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "boleta" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 *, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next

            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
                End If
            Next

            'e.DrawString(ImpND("cancelado").Valor, ImpNDi("cancelado").Fuente, Brushes.Red, ImpNDi("cancelado").X / 40 * 10, ImpNDi("cancelado").Y / 40 * 10)
            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            For Each n As NodoImpresionN In codigos

                If n.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.YL / 40 * 10))
            Next
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        'Dim ImpDb As New dbImpresionesN(MySqlcon)
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try

            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.SemillasBoleta, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.SemillasBoleta + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos fijos principio
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "boleta" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "boleta" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 And niva.Tipo = 0 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 0 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        For Each n As NodoImpresionN In ImpNDi
            If n.DataPropertyName.Contains("cancelado") Then
                e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
            End If
        Next

        'Nodos Detalles            
        XCoord = 0
        YCoord = 0
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And (pModo = 0 Or pModo = 2) Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '***************************segundo
            Dim Haysegundo As Boolean = False
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And (pModo = 0 Or pModo = 2) Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    Haysegundo = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If Haysegundo Then YCoord = YCoord + 4 + YExtra


            '*********************************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
        If MasPaginas = True And (pModo = 2 Or pModo = 3) Then
            NumeroPagina += 1
            Exit Sub
        End If
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "boleta" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "boleta" Then
                    If n.Visible = 1 And n.Tipo = 2 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(n.XL / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS
            Dim Ycoord2 As Integer
            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                Ycoord2 = 0
                If niva.Visible = 1 And niva.Tipo = 2 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

                Ycoord2 = 0
                'Ivas Sin Retenciones
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 2 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva2.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

            End If

            If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    'Private Sub txtHumedad_Enter(sender As Object, e As EventArgs) Handles txtHumedad.Enter
    '    If txtPeso.Text = "" Then
    '        MsgBox("Debe indicar el peso.")
    '        txtPeso.BackColor = Color.Red
    '    End If
    'End Sub

    Private Sub txtTara_Leave(sender As Object, e As EventArgs) Handles txtTara.Leave
        If txtTara.Text <> "" Then
            peso = parsea(txtPeso)
            tara = parsea(txtTara)
            brutosinanalizar = peso - tara
            txtTara.Text = Format(tara, "#,###,##0")
            txtBrutoSinAnalizar.Text = Format(brutosinanalizar, "#,###,##0")
        Else
            brutosinanalizar = 0
        End If
    End Sub

    Private Sub txtTara_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTara.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBrutoSinAnalizar.Focus()
        End If
    End Sub

    Private Sub txtBrutoSinAnalizar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBrutoSinAnalizar.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtHumedad.Focus()
        End If
    End Sub

    Private Sub txtClaveProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtClaveProducto.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPeso.Focus()
        ElseIf e.KeyCode = Keys.F1 Then
            buscarProducto()
            txtPeso.Focus()
        End If
    End Sub

    Private Sub dtpFecha_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpFecha.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtClaveProductor.Focus()
        End If
    End Sub

    Private Sub txtClaveProductor_KeyDown(sender As Object, e As KeyEventArgs) Handles txtClaveProductor.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtClaveProducto.Enabled = True Then
                txtClaveProducto.Focus()
            Else
                txtPeso.Focus()
            End If
        ElseIf e.KeyCode = Keys.F1 Then
            buscar()
            txtClaveProductor.Focus()
        End If
    End Sub

    Private Sub dtpHora_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpHora.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpHoraSalida.Focus()
        End If
    End Sub

    Private Sub dtpHoraSalida_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpHoraSalida.KeyDown
        If e.KeyCode = Keys.Enter Then
            comboChofer.Focus()
        End If
    End Sub

    Private Sub txtPeso_Leave(sender As Object, e As EventArgs) Handles txtPeso.Leave
        If txtPeso.Text = "" Or txtPeso.Text = "0" Then
            MsgBox("Debe indicar el peso.")
            txtPeso.BackColor = Color.Red
            txtPeso.Focus()
        Else
            'NumConFrac(txtPeso, e)
            peso = parsea(txtPeso)
            txtPeso.Text = Format(peso, "#,###,##0")
        End If
    End Sub
    Private Function confirmaImpresion() As Boolean
        Dim result As Integer = MessageBox.Show("¿Desea imprimir la boleta?", GlobalNombreApp, MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Yes Then
            Imprimir()
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub Button1_KeyDown(sender As Object, e As KeyEventArgs) Handles Button1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtClaveProducto.Focus()
        End If
    End Sub

    Private Sub btnBuscarProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles btnBuscarProducto.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPeso.Focus()
        End If
    End Sub

    Private Sub buscar()
        If tipoBoleta = "E" Then
            Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 1, False, False, False)
            frm.ShowDialog()
            Dim prov As dbproveedores = frm.Proveedor
            If Not prov Is Nothing Then
                productor = New dbproveedores(prov.ID, MySqlcon)
                'buscaProductorCombo(productor)
                comboProductores.Text = productor.Nombre
                txtClaveProductor.Text = productor.Clave
            End If
        Else
            Dim frm As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 1, False, False, False)
            frm.ShowDialog()
            Dim cli As dbClientes = frm.Cliente
            If Not cli Is Nothing Then
                cliente = New dbClientes(cli.ID, MySqlcon)
                'buscaProductorCombo(productor)
                comboProductores.Text = cliente.Nombre
                txtClaveProductor.Text = cliente.Clave
            End If
        End If
    End Sub

    Private Function formateaCadena(ByVal cadena As String) As String
        Return Format(cadena, "###,###,##0")
    End Function

    Private Function convierteFecha(ByVal fecha As String) As String
        Dim arr() = fecha.Split("/")
        Select Case arr(1)
            Case "01"
                arr(1) = "ene"
            Case "02"
                arr(1) = "feb"
            Case "03"
                arr(1) = "mar"
            Case "04"
                arr(1) = "abr"
            Case "05"
                arr(1) = "may"
            Case "06"
                arr(1) = "jun"
            Case "07"
                arr(1) = "jul"
            Case "08"
                arr(1) = "ago"
            Case "09"
                arr(1) = "sep"
            Case "10"
                arr(1) = "oct"
            Case "11"
                arr(1) = "nov"
            Case "12"
                arr(1) = "dic"
        End Select
        Dim aux = arr(2) + "/" + arr(1) + "/" + arr(0)
        Return aux
    End Function

    Private Sub txtSerie_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSerie.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFolio.Focus()
        End If
    End Sub

    Private Sub txtFolio_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFolio.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub txtPrecio_Leave(sender As Object, e As EventArgs) Handles txtPrecio.Leave
        If txtPrecio.Text <> "" Then
            txtPrecio.Text = Format(CDbl(txtPrecio.Text), "$#,###,##0.00######")
        End If
    End Sub

    Private Sub txtPrecio_TextChanged(sender As Object, e As EventArgs) Handles txtPrecio.TextChanged
        'txtPrecio.Text = Format(boletaP.precio, opciones._formatoTotal)
        parseaDatosNumeros()
        calculaCastigoTotal()
    End Sub
End Class
