Imports MySql.Data.MySqlClient
Public Class frmRestauranteOrden
    Private idMesa As Integer
    Private mesaController As dbRestauranteMesas
    Private mesa As RestauranteMesa
    Public ventas As New dbRestauranteVentas(MySqlcon)
    Private comensales As New dbRestauranteComensales(MySqlcon)
    Private mesaVenta As New dbRestauranteVentasMesas(MySqlcon)
    Public estado As Integer = EstadosMesas.Libre
    Dim cantidadComensales As Integer
    Private inv As New dbInventario(MySqlcon)
    Private detalles As New dbRestauranteVentasDetalles(MySqlcon)
    Private precios As New dbInventarioPrecios(MySqlcon)
    Private platillosComensal As New dbRestauranteComensalesPlatillos(MySqlcon)
    Public idVenta As Integer = -1
    Private ClaveInventario1 As String
    Private ClaveInventario2 As String
    Private ClaveInventario3 As String
    Private ClaveInventario4 As String
    Private ClaveInventario5 As String
    Private idMesero As Integer
    Private idCliente As Integer = 16
    Public listaPagar As New List(Of Integer)
    Public pagar As Boolean = False
    Private multiSelect As Boolean = False
    Public cuentaCompleta As Boolean = True
    Public idnuevaVenta As Integer
    'Public mesero As String
    Private colores As dbRestauranteColores
    Private idComensal As Integer = -1
    Private impreso As Boolean = False
    Private numComensal As Integer = 1
    Private coloresComensales As Color() = {Color.Aqua, Color.Yellow, Color.Gray, Color.GreenYellow, Color.Lime, Color.Silver, Color.Azure, Color.Beige, Color.Aquamarine, Color.BlueViolet, Color.Chartreuse}
    Private comandaEnviada = False
    Private articuloAgregado = False
    Private totalPlatillos As Integer = 0
    Dim listaNuevos As List(Of Integer)
    Private nuevosPlatillos As Boolean = False

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
    Dim IdSucursal As Integer
    Public mesero As New dbVendedores(MySqlcon)
    Dim EstadoRenglon As Integer
    Public Sub New(ByVal idMesa As Integer, ByVal idMesero As Integer, pIdSucursal As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.idMesa = idMesa
        Me.idMesero = idMesero
        mesero = New dbVendedores(idMesero, MySqlcon)
        checaVenta()
        IdSucursal = pIdSucursal
        mesaController = New dbRestauranteMesas(MySqlcon, IdSucursal)
    End Sub
    Private Sub frmRestauranteOrden_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        colores = New dbRestauranteColores(MySqlcon)
        cargarConfiguracion()
        mesa = mesaController.buscar(idMesa)
        lblMesa.Text = "Mesa: " + mesa.numero.ToString()
        actualizaGridComensales()
        llenaGridPlatillos()
        If dgvComensales.RowCount > 0 Then
            idComensal = ventas.NoPersonas
        End If
        'dgvComensales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'dgvPlatillos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Label4.Text = "Folio: " + ventas.folio
        Label1.Text = "Mesero: " + mesero.Nombre
        panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub llenaGridPlatillos()
        dgvPlatillos.DataSource = detalles.vistaDetalles(idVenta, -1, CInt(estadosPlatillos.inicio).ToString)
        If dgvPlatillos.Rows.Count > 0 Then
            'For i As Integer = 0 To dgvPlatillos.Rows.Count - 1
            '    dgvPlatillos.Rows(i).Selected = False
            'Next
            dgvPlatillos.Columns(0).Visible = False
            dgvPlatillos.Columns(5).Visible = False
            dgvPlatillos.Columns(1).HeaderText = "Cant."
            dgvPlatillos.Columns(2).HeaderText = "Descripción"
            dgvPlatillos.Columns(3).HeaderText = "Com."
            dgvPlatillos.Columns(4).HeaderText = "Precio"
            dgvPlatillos.Columns(1).Width = 40
            dgvPlatillos.Columns(3).Width = 40
            dgvPlatillos.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            dgvPlatillos.Columns(4).Width = 110
            dgvPlatillos.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvPlatillos.Columns(4).DefaultCellStyle.Format = "#,##0.00"
            dgvPlatillos.ClearSelection()
            For Each i As DataGridViewRow In dgvComensales.Rows
                Dim c As Integer = CInt(i.Cells(0).Value.ToString)
                For Each x As DataGridViewRow In dgvPlatillos.Rows
                    Dim pc As Integer = CInt(x.Cells("comensal").Value.ToString())
                    If c = pc Then
                        x.DefaultCellStyle.BackColor = i.DefaultCellStyle.BackColor
                    End If
                Next
            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'idComensal = comensales.guardar(cantidadComensales + 1, idMesa)
        'cantidadComensales += 1
        ventas.AumentaComensal(idVenta)
        actualizaGridComensales()
        idComensal = ventas.NoPersonas
    End Sub

    Private Sub actualizaGridComensales()
        dgvComensales.Rows.Clear()
        Dim SelColor As Byte = 0
        For i As Integer = 0 To ventas.NoPersonas - 1
            dgvComensales.Rows.Add((i + 1).ToString)
            dgvComensales.Rows(i).DefaultCellStyle.BackColor = coloresComensales(SelColor)
            SelColor += 1
            If SelColor = 11 Then SelColor = 0
        Next
        dgvComensales.ClearSelection()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'estado = EstadosMesas.Ocupada
        If MsgBox("¿Cancelar esta venta?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            ventas.modificar(ventas.idVenta, ventas.idCliente, ventas.descuento, ventas.total, ventas.totalapagar, Estados.Cancelada, ventas.fecha, ventas.idSucursal, ventas.IdCajero, ventas.IdCaja, "")
            Me.Close()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnPagar.Click
        Dim contPlatillos As Integer = 0
        If comandaEnviada Then
            If nuevosPlatillos Then
                EnviarComanda()
            Else
                'Dim result = MsgBox("¿El ticket es correcto?", MsgBoxStyle.YesNo)
                'If result = DialogResult.Yes Then
                If dgvPlatillos.Rows.Count > 0 Then
                    'For Each row As DataGridViewRow In dgvPlatillos.Rows
                    '    If row.Cells("sel").Value = True Then
                    '        contPlatillos += 1
                    '    End If
                    'Next
                    'If contPlatillos > 0 And contPlatillos < dgvPlatillos.Rows.Count Then
                    '    cuentaCompleta = False
                    'Else
                    cuentaCompleta = True
                    'End If
                    If cuentaCompleta = False Then
                        'Dim arr As Integer() = {}
                        'Dim r As DataGridViewRow
                        'For Each r In DGServicios.SelectedRows
                        '    ReDim Preserve arr(arr.Length)
                        '    arr(arr.Length - 1) = r.Cells(0).Value
                        'Next
                        'Return arr
                        idnuevaVenta = ventas.Guardar(ventas.obtenFolio, idMesero, idCliente, GlobalIdSucursalDefault, idMesa, My.Settings.cajadefault)
                        For Each i As DataGridViewRow In dgvPlatillos.Rows
                            If i.Cells("sel").Value = True Then
                                detalles.buscar(CInt(i.Cells("iddetalle").Value))
                                detalles.modificar(detalles.idDetalle, detalles.idInventario, detalles.cantidad, detalles.descripcion, detalles.precio, detalles.iva, idnuevaVenta, "")
                                detalles.pagarDetalle(CInt(i.Cells("iddetalle").Value), CInt(estadosPlatillos.pendiente))
                                detalles.cambiarEstado(detalles.idDetalle, CInt(estadosPlatillos.pendiente))
                                'platillosComensal.eliminarDetalle(detalles.idDetalle)
                                listaPagar.Add(detalles.idDetalle)
                            End If
                        Next
                        idVenta = idnuevaVenta
                        'comensales.eliminar(idComensal)
                        estado = EstadosMesas.Ocupada
                    Else
                        'For Each i As DataGridViewRow In dgvPlatillos.Rows
                        '    detalles.buscar(CInt(i.Cells("iddetalle").Value))
                        '    detalles.pagarDetalle(CInt(i.Cells("iddetalle").Value), CInt(estadosPlatillos.pendiente))
                        '    detalles.cambiarEstado(detalles.idDetalle, CInt(estadosPlatillos.pendiente))
                        '    'platillosComensal.eliminarDetalle(CInt(i.Cells("iddetalle").Value))
                        '    listaPagar.Add(detalles.idDetalle)
                        'Next
                        'For Each i As DataGridViewRow In dgvComensales.Rows
                        '    'comensales.eliminar(CInt(i.Cells("idcomensal").Value.ToString))
                        'Next
                        Dim f As New frmRestaurantePuntoVenta(ventas.IdMesa, ventas.idVenta, , True)
                        f.ShowDialog()
                        estado = EstadosMesas.Libre
                        'mesaVenta.eliminar(idMesa)
                    End If
                    'If ventas.cuantosPlatillos(ventas.idVenta, CInt(estadosPlatillos.inicio).ToString) <= 0 Then
                    '    estado = EstadosMesas.Libre
                    'Else
                    '    estado = EstadosMesas.Ocupada
                    'End If
                    'cuentaCompleta = False
                    pagar = True
                End If
                'Else
                '   impreso = False
                'End If
            End If
        Else
            totalPlatillos = dgvPlatillos.Rows.Count
            If EnviarComanda() Then
                btnPagar.Text = "Pagar"
                If ventas.cuantosPlatillos(ventas.idVenta, estadosPlatillos.inicio) <= 0 Then
                    estado = EstadosMesas.Libre
                Else
                    estado = EstadosMesas.Ocupada
                End If
            End If
            End If
        Me.Close()
    End Sub

    Private Function EnviarComanda() As Boolean

        If imprimir() Then
            comandaEnviada = True
            Dim listaAux As List(Of Integer) = ventas.listaDetalles(ventas.idVenta, estadosPlatillos.sinEnviar)
            'For Each i As Integer In listaAux
            detalles.cambiarEstado(idVenta, estadosPlatillos.enviado)
            'Next
            'listaNuevos = New List(Of Integer)
            nuevosPlatillos = False
            Return True
        End If
        Return False
    End Function

    'Private Sub dgvComensales_DoubleClick(sender As Object, e As EventArgs) Handles dgvComensales.DoubleClick

    '    Dim id As Integer = dgvComensales.CurrentRow.Cells(0).Value
    '    comensales.eliminar(id)
    '    actualizaGridComensales()
    'End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim frm As New frmRestauranteBuscador()
        frm.ShowDialog()
        If frm.clave.Count > 0 Then
            For Each c As String In frm.clave
                inv.BuscaArticulo(c, 0)
                If agregarPlatillo() = False Then
                    MsgBox("Debe seleccionar un comensal.")
                    Exit Sub
                End If
            Next
            comandaEnviada = False
            btnPagar.Text = "Enviar comanda."
            estado = EstadosMesas.Ocupada
        End If
    End Sub

    Private Function agregarPlatillo() As Boolean
        If idComensal < 0 Then
            Return False
        End If
        Dim precio As Double
        precios.BuscaPrecio(inv.ID, 1)
        precio = precios.Precio
        detalles.agregar(inv.ID, 1, inv.Nombre, precio, inv.Iva, idVenta, "", idComensal)
        'detalles.buscar(detalles.ultimoId)
        'platillosComensal.agregar(idComensal, detalles.ultimoId)
        'If comandaEnviada Then
        '    listaNuevos.Add(detalles.idDetalle)
        '    nuevosPlatillos = True
        '    btnPagar.Text = "Enviar comanda"
        'End If
        nuevosPlatillos = ventas.checaPendientes(ventas.idVenta)
        llenaGridPlatillos()
        Return True
    End Function

    Private Sub cargarConfiguracion()
        Dim c As New dbRestauranteConfiguracion(MySqlcon)
        If c.llenaDatos Then
            ClaveInventario1 = c.claveProducto1
            ClaveInventario2 = c.claveProducto2
            ClaveInventario3 = c.claveProducto3
            ClaveInventario4 = c.claveProducto4
            ClaveInventario5 = c.claveProducto5
            Me.BackColor = Color.FromArgb(c.colorVentanas)
            If inv.BuscaArticulo(ClaveInventario1, 1) Then
                btnDirecto1.Text = inv.Nombre
            Else
                btnDirecto1.Text = "No Asig."
            End If
            If inv.BuscaArticulo(ClaveInventario2, 1) Then
                btnDirecto2.Text = inv.Nombre
            Else
                btnDirecto2.Text = "No Asig."
            End If
            If inv.BuscaArticulo(ClaveInventario3, 1) Then
                btnDirecto3.Text = inv.Nombre
            Else
                btnDirecto3.Text = "No Asig."
            End If
            If inv.BuscaArticulo(ClaveInventario4, 1) Then
                btnDirecto4.Text = inv.Nombre
            Else
                btnDirecto4.Text = "No Asig."
            End If
            If inv.BuscaArticulo(ClaveInventario5, 1) Then
                btnDirecto5.Text = inv.Nombre
            Else
                btnDirecto5.Text = "No Asig."
            End If
        End If
    End Sub

    Private Sub checaVenta()
        mesero = New dbVendedores(idMesero, MySqlcon)
        If ventas.buscarventaabierta(idMesa) Then
            idVenta = ventas.idVenta
            checaPendientes()
        Else
            idVenta = ventas.Guardar(ventas.obtenFolio, idMesero, idCliente, GlobalIdSucursalDefault, idMesa, My.Settings.cajadefault)
        End If
    End Sub

    Private Sub btnDirecto1_Click(sender As Object, e As EventArgs) Handles btnDirecto1.Click
        If inv.BuscaArticulo(ClaveInventario1, 0) Then
            If agregarPlatillo() = False Then
                MsgBox("Debe seleccionar un comensal.")
            End If
        End If
    End Sub

    Private Sub btnDirecto2_Click(sender As Object, e As EventArgs) Handles btnDirecto2.Click
        If inv.BuscaArticulo(ClaveInventario2, 0) Then
            If agregarPlatillo() = False Then
                MsgBox("Debe seleccionar un comensal.")
            End If
        End If
    End Sub

    Private Sub btnDirecto3_Click(sender As Object, e As EventArgs) Handles btnDirecto3.Click
        If inv.BuscaArticulo(ClaveInventario3, 0) Then
            If agregarPlatillo() = False Then
                MsgBox("Debe seleccionar un comensal.")
            End If
        End If
    End Sub

    Private Sub btnDirecto4_Click(sender As Object, e As EventArgs) Handles btnDirecto4.Click
        If inv.BuscaArticulo(ClaveInventario4, 0) Then
            If agregarPlatillo() = False Then
                MsgBox("Debe seleccionar un comensal.")
            End If
        End If
    End Sub

    Private Sub btnDirecto5_Click(sender As Object, e As EventArgs) Handles btnDirecto5.Click
        If inv.BuscaArticulo(ClaveInventario5, 0) Then
            If agregarPlatillo() = False Then
                MsgBox("Debe seleccionar un comensal.")
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        For i As Integer = 0 To dgvPlatillos.RowCount - 1
            listaPagar.Add(CInt(dgvPlatillos.Rows(i).Cells(0).Value.ToString()))
        Next
        estado = EstadosMesas.Libre
        cuentaCompleta = True
        pagar = True
        Me.Dispose()
    End Sub

    Private Sub btnCambiar_Click(sender As Object, e As EventArgs)
        Dim f As New frmRestauranteCambioMesa(Me.idMesa, Nothing, idSucursal)
        f.ShowDialog()
        If Me.idMesa <> f.idMesa Then
            mesa = mesaController.buscar(ventas.IdMesa)
            lblMesa.Text = "Mesa: " + mesa.numero.ToString()
        End If
    End Sub

    Private Sub frmRestauranteOrden_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim t As Integer = ventas.listaDetalles(ventas.idVenta, -1).Count
        If t <= 0 Then
            ventas.eliminar(ventas.idVenta)
            mesaVenta.eliminar(Me.idMesa)
            comensales.eliminarPorMesa(Me.idMesa)
            estado = EstadosMesas.Libre
        End If
    End Sub

    Private Sub seleccionaPlatillosComensal(ByVal idComensal As Integer)
        Dim lista As List(Of Integer) = platillosComensal.listaDetalles(idComensal, -1)
        For x As Integer = 0 To dgvPlatillos.Rows.Count - 1
            dgvPlatillos.Rows(x).Cells("seleccionado").Value = False
        Next
        For Each i As Integer In lista
            For x As Integer = 0 To dgvPlatillos.Rows.Count - 1
                If CInt(dgvPlatillos.Rows(x).Cells("iddetalle").Value) = i Then
                    dgvPlatillos.Rows(x).Cells("seleccionado").Value = True
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub dgvComensales_Click(sender As Object, e As EventArgs)
        Try
            idComensal = CInt(dgvComensales.CurrentRow.Cells("idcomensal").Value)
            seleccionaPlatillosComensal(idComensal)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim contPlatillos As Integer = 0
        If dgvPlatillos.Rows.Count > 0 Then
            For Each row As DataGridViewRow In dgvPlatillos.Rows
                If row.Cells("seleccionado").Value = True Then
                    contPlatillos += 1
                End If
            Next
            If contPlatillos > 0 And contPlatillos < dgvPlatillos.Rows.Count Then
                cuentaCompleta = False
            Else
                cuentaCompleta = True
            End If
            If cuentaCompleta = False Then
                idnuevaVenta = ventas.Guardar(ventas.obtenFolio, idMesero, idCliente, GlobalIdSucursalDefault, idMesa, My.Settings.cajadefault)
                For Each i As DataGridViewRow In dgvPlatillos.Rows
                    If i.Cells("seleccionado").Value = True Then
                        detalles.buscar(CInt(i.Cells("iddetalle").Value))
                        detalles.modificar(detalles.idDetalle, detalles.idInventario, detalles.cantidad, detalles.descripcion, detalles.precio, detalles.iva, idnuevaVenta, "")
                        detalles.pagarDetalle(CInt(i.Cells("iddetalle").Value), CInt(estadosPlatillos.pendiente).ToString)
                    End If
                Next
                ventas.buscar(idnuevaVenta)
            Else
                For Each i As DataGridViewRow In dgvPlatillos.Rows
                    detalles.pagarDetalle(CInt(i.Cells("iddetalle").Value), CInt(estadosPlatillos.pendiente).ToString)
                    'platillosComensal.eliminarDetalle(CInt(i.Cells("iddetalle").Value))
                Next
                'For i As Integer = 0 To dgvComensales.Rows.Count - 1
                'comensales.eliminar(CInt(dgvComensales.CurrentRow.Cells(0).Value))
                'Next
            End If
            If imprimir() Then
                impreso = True
                btnPagar.Enabled = True
            End If
        Else
            MsgBox("No hay platillos para esta mesa.")
        End If
    End Sub

    Private Function imprimir() As Boolean
        Try
            Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

            Dim SA As New dbSucursalesArchivos
            Dim Impresora As String
            PrintDialog1.PrinterSettings.Copies = 1
            Dim RutaPDF As String
            Dim Archivos As New dbSucursalesArchivos
            RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, False)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(ventas.fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(ventas.fecha), "yyyy") + "\" + Format(CDate(ventas.fecha), "MM") + "\")
            RutaPDF = RutaPDF + "\" + Format(CDate(ventas.fecha), "yyyy") + "\" + Format(CDate(ventas.fecha), "MM")
            'Dim SA As New dbSucursalesArchivos
            Dim TipoImpresora As String
            Impresora = Archivos.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.Venta)
            TipoImpresora = SA.TipoImpresora
            PrintDocument1.PrinterSettings.PrinterName = Impresora
            PrintDocument1.DocumentName = " Venta" + ventas.folio
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
                LlenaNodos(suc.ID, TiposDocumentos.RestauranteComanda)
            Else
                LlenaNodos(suc.ID, TiposDocumentos.RestauranteComanda + 1000)
            End If

            PrintDocument1.Print()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

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

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.RestauranteComanda, GlobalIdSucursalDefault)
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.RestauranteComanda + 1000, GlobalIdSucursalDefault)
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.RestauranteComanda + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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

    Private Sub LlenaNodosImpresion()
        Dim O As New dbOpciones(MySqlcon)
        Dim iva As Double = 0
        Dim total As Double = 0
        Dim subtotal As Double = 0
        Dim lista As List(Of Integer)
        Dim comen As List(Of Integer) = comensales.listaComensales(idMesa)
        If nuevosPlatillos Then
            lista = listaNuevos
        Else
            If comandaEnviada Then
                lista = ventas.listaDetalles(ventas.idVenta, 2)
            Else
                lista = ventas.listaDetalles(ventas.idVenta, 0)
            End If
        End If
        ImpND.Clear()
        ImpNDD.Clear()
        CuantosRenglones = 0
        Posicion = 0
        NumeroPagina = 1
        Dim cont As Integer = 0
        Dim cont1 As Integer = 0

        For Each i As Integer In comen
            comensales.buscar(i)
            lista = platillosComensal.listaDetalles(i, CInt(estadosPlatillos.sinEnviar))
            For Each x As Integer In lista
                detalles.buscar(x)
                ImpNDD.Add(New NodoImpresionN("", "comensal", comensales.numero.ToString, 0), "comensal" + Format(cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "producto", detalles.descripcion, 0), "producto" + Format(cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "cantidad", detalles.cantidad.ToString, 0), "cantidad" + Format(cont, "000"))
                cont += 1
                CuantosRenglones += 1
                total += detalles.precio
                iva += detalles.iva
                subtotal = detalles.precio
            Next
        Next
        ImpND.Add(New NodoImpresionN("", "mesero", GlobalUsuario, 0), "mesero")
        Dim m As New dbRestauranteMesas(MySqlcon, IdSucursal)
        Dim m2 As RestauranteMesa = m.buscar(Me.idMesa)
        ImpND.Add(New NodoImpresionN("", "mesa", m2.numero.ToString, 0), "mesa")
        ImpND.Add(New NodoImpresionN("", "hora", TimeOfDay.ToString("HH:mm:ss"), 0), "hora")


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



    Private Sub checaPendientes()
        If ventas.checaPendientes(ventas.idVenta) Then
            comandaEnviada = False
        Else
            comandaEnviada = True
            btnPagar.Text = "Pagar"
        End If
    End Sub

    Private Sub frmRestauranteOrden_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If detalles.idUltimoInventario <> 0 Then
            inv.ID = detalles.idUltimoInventario
            inv.LlenaDatos()
            agregarPlatillo()
        End If
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        If detalles.idUltimoDetalle <> 0 And detalles.EsBorrable(detalles.idUltimoDetalle) Then
            detalles.eliminar(detalles.idUltimoDetalle)
            llenaGridPlatillos()
            detalles.DaUltimoDetalle(idVenta)
        End If
    End Sub

    Private Sub dgvPlatillos_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            detalles.idUltimoDetalle = dgvPlatillos.Item(6, e.RowIndex).Value
        End If
    End Sub

    Private Sub dgvPlatillos_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
        If e.ColumnIndex = 5 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            e.Value = Format(CDbl(e.Value), "###,##0.00")
        End If
    End Sub

    Private Sub dgvComensales_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComensales.CellClick
        If e.RowIndex >= 0 Then
            idComensal = e.RowIndex + 1
        End If
    End Sub

    
   
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If detalles.vistaDetalles(idVenta, -1, CInt(estadosPlatillos.inicio).ToString).Count > 0 Then
            estado = EstadosMesas.Ocupada
        End If
        Me.Close()
    End Sub

    Private Sub dgvComensales_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvComensales.CellContentClick

    End Sub
End Class