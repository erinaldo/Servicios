Imports MySql.Data.MySqlClient
Public Class frmRestauranteOrden
    Private idMesa As Integer
    Public Property IdVenta As Integer
    Private idMesero As Integer
    Private idCliente As Integer = 16

    Private coloresComensales As Color() = {Color.Aqua, Color.Yellow, Color.Gray, Color.GreenYellow, Color.Lime, Color.Silver, Color.Azure, Color.Beige, Color.Aquamarine, Color.BlueViolet, Color.Chartreuse}
   
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
    
    Public Sub New(ByVal idMesa As Integer, idVenta As Integer, ByVal idMesero As Integer, pIdSucursal As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.idMesa = idMesa
        Me.idMesero = idMesero
        Me.IdVenta = idVenta
        btnAgregarComensal.Enabled = idMesa <> 0

        Dim ventas As New dbRestauranteVentas(IdVenta, MySqlcon)
        If idMesa = 0 Then
            If idVenta = 0 Then
                Me.IdVenta = ventas.Agregar(ventas.obtenFolio, idMesero, idCliente, GlobalIdSucursalDefault, idMesa, 1)
            End If
        Else
            If ventas.buscarventaabierta(idMesa) Then
                Me.IdVenta = ventas.idVenta
            Else
                Me.IdVenta = ventas.Agregar(ventas.obtenFolio, idMesero, idCliente, GlobalIdSucursalDefault, idMesa, 1)
            End If
        End If
            IdSucursal = pIdSucursal
    End Sub
    Private Sub frmRestauranteOrden_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        Dim colores As New dbRestauranteColores(MySqlcon)
        cargarConfiguracion()
        Dim mesacontroller As New dbRestauranteMesas(MySqlcon, IdSucursal)
        Dim mesa As RestauranteMesa = mesacontroller.Buscar(idMesa)
        If mesa IsNot Nothing Then lblMesa.Text = "Mesa: " + mesa.Numero.ToString()

        dgvComensales.AutoGenerateColumns = False
        dgvPlatillos.AutoGenerateColumns = False
        Dim comensales As New dbRestauranteComensales(MySqlcon)
        Dim ventas As New dbRestauranteVentas(idVenta, MySqlcon)
        dgvComensales.DataSource = comensales.vistaComensales(idMesa)
        dgvPlatillos.DataSource = ventas.vistaDetalles(IdVenta, -1, -1)
        btnImprimirComanda.Enabled = dgvPlatillos.RowCount > 0
        dgvPlatillos.ClearSelection()
        dgvComensales.ClearSelection()
        If dgvComensales.RowCount > 0 Then dgvComensales.Rows(dgvComensales.RowCount - 1).Selected = True
        If dgvPlatillos.RowCount > 0 Then dgvPlatillos.Rows(dgvPlatillos.RowCount - 1).Selected = True
        dgvComensales.Show()
        dgvPlatillos.Show()

        Label4.Text = "Folio: " + ventas.folio
        Dim mesero As New dbVendedores(idMesero, MySqlcon)
        Label1.Text = "Mesero: " + mesero.Nombre
        panelObjetos.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnAgregarComensal.Click
        Dim comensales As New dbRestauranteComensales(MySqlcon)
        Dim mesacontroller As New dbRestauranteMesas(MySqlcon, IdSucursal)
        Dim mesa As RestauranteMesa = mesacontroller.Buscar(idMesa)
        comensales.Agregar(mesa.Id)
        dgvComensales.DataSource = comensales.vistaComensales(idMesa)
        dgvComensales.ClearSelection()
        dgvComensales.Rows(dgvComensales.RowCount - 1).Selected = True
    End Sub

    
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancelarVenta.Click
        If MsgBox("¿Cancelar esta venta?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim ventas As New dbRestauranteVentas(idVenta, MySqlcon)
            ventas.Cancelar(IdVenta)
            Me.Close()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnCarta.Click
        If idMesa = 0 Or dgvComensales.SelectedRows.Count > 0 Then
            If dgvComensales.SelectedRows.Count = 0 Then
                Dim frm As New frmRestauranteBuscador(IdVenta, 0)
                frm.ShowDialog()
            Else
                Dim frm As New frmRestauranteBuscador(IdVenta, dgvComensales.SelectedRows(0).Cells(0).Value)
                frm.ShowDialog()
            End If
            Dim ventas As New dbRestauranteVentas(IdVenta, MySqlcon)
            dgvPlatillos.DataSource = ventas.vistaDetalles(IdVenta, -1, -1)
            btnImprimirComanda.Enabled = dgvPlatillos.RowCount > 0
        Else
            MsgBox("Seleccione un comensal.")
        End If
    End Sub


    Private Sub cargarConfiguracion()
        Dim c As New dbRestauranteConfiguracion(MySqlcon)
        If c.llenaDatos Then
            btnDirecto1.Tag = c.claveProducto1
            btnDirecto2.Tag = c.claveProducto2
            btnDirecto3.Tag = c.claveProducto3
            btnDirecto4.Tag = c.claveProducto4
            btnDirecto5.Tag = c.claveProducto5
            Me.BackColor = Color.FromArgb(c.colorVentanas)
            Dim inv As New dbInventario(MySqlcon)
            If inv.BuscaArticulo(c.claveProducto1, 1, "") Then btnDirecto1.Text = inv.Nombre
            If inv.BuscaArticulo(c.claveProducto2, 1, "") Then btnDirecto2.Text = inv.Nombre
            If inv.BuscaArticulo(c.claveProducto3, 1, "") Then btnDirecto3.Text = inv.Nombre
            If inv.BuscaArticulo(c.claveProducto4, 1, "") Then btnDirecto4.Text = inv.Nombre
            If inv.BuscaArticulo(c.claveProducto5, 1, "") Then btnDirecto5.Text = inv.Nombre
        End If
    End Sub

    Private Sub btnDirecto1_Click(sender As Object, e As EventArgs) Handles btnDirecto1.Click, btnDirecto2.Click, btnDirecto3.Click, btnDirecto4.Click, btnDirecto5.Click
        If idMesa = 0 Or dgvComensales.SelectedRows.Count > 0 Then
            Dim inv As New dbInventario(MySqlcon)
            inv.BuscaArticulo(DirectCast(sender, Button).Tag, True, "")
            inv.LlenaDatos()
            Dim precios As New dbInventarioPrecios(MySqlcon)
            Dim ventas As New dbRestauranteVentas(IdVenta, MySqlcon)
            precios.BuscaPrecio(inv.ID, 1)
            Dim detalles As New dbRestauranteVentasDetalles(MySqlcon)
            If idMesa = 0 Then
                detalles.Agregar(inv.ID, 1, inv.Nombre, precios.Precio, inv.Iva, IdVenta, "", 0)
            Else
                detalles.Agregar(inv.ID, 1, inv.Nombre, precios.Precio, inv.Iva, IdVenta, "", dgvComensales.SelectedRows(colComensal1.Index).Cells(colComensal1.Index).Value)
            End If
            dgvPlatillos.DataSource = ventas.vistaDetalles(IdVenta, -1, -1)
            btnImprimirComanda.Enabled = dgvPlatillos.RowCount > 0
            dgvPlatillos.ClearSelection()
            If dgvPlatillos.RowCount > 0 Then dgvPlatillos.Rows(dgvPlatillos.RowCount - 1).Selected = True
        Else
            MsgBox("Seleccione un comensal.")
        End If
    End Sub


    Private Sub frmRestauranteOrden_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim ventas As New dbRestauranteVentas(idVenta, MySqlcon)
        Dim numPlatillos As Integer = ventas.vistaDetalles(idVenta, -1, -1).Count
        If numPlatillos = 0 Then
            Dim mesacontroller As New dbRestauranteMesas(MySqlcon, IdSucursal)
            mesacontroller.Desocupar(IdVenta)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles btnImprimirComanda.Click
        If dgvPlatillos.RowCount > 0 Then
            imprimir()
            Dim ventas As New dbRestauranteVentas(IdVenta, MySqlcon)
            Dim detalles As New dbRestauranteVentasDetalles(MySqlcon)
            detalles.modificarEstadosDetalles(idVenta, estadosPlatillos.enviado)
            dgvPlatillos.DataSource = ventas.vistaDetalles(IdVenta, -1, -1)
            btnImprimirComanda.Enabled = dgvPlatillos.RowCount > 0
        Else
            MsgBox("No hay platillos para esta mesa.")
        End If
    End Sub

#Region "Impresion"
    Private Function imprimir() As Boolean
        Try
            Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim ventas As New dbRestauranteVentas(idVenta, MySqlcon)

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
        Dim vista As DataView
        Dim ventas As New dbRestauranteVentas(idVenta, MySqlcon)
        'Dim lista As List(Of Integer)
        'Dim comen As List(Of Integer) = comensales.listaComensales(idMesa)
        If ventas.checaPendientes(IdVenta, 0) Then
            vista = ventas.vistaDetalles(IdVenta, 6, -1)
        Else
            vista = ventas.vistaDetalles(IdVenta, -1, -1)
        End If
        ImpND.Clear()
        ImpNDD.Clear()
        CuantosRenglones = 0
        Posicion = 0
        NumeroPagina = 1
        Dim cont As Integer = 0
        Dim cont1 As Integer = 0

        For Each r As DataRowView In vista
            ImpNDD.Add(New NodoImpresionN("", "comensal", r("comensal"), 0), "comensal" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "producto", r("descripcion"), 0), "producto" + Format(cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cantidad", r("cantidad"), 0), "cantidad" + Format(cont, "000"))
            cont += 1
            CuantosRenglones += 1
            total += r("comensal")
            iva += r("iva")
            subtotal = r("precio")
        Next
        ImpND.Add(New NodoImpresionN("", "mesero", GlobalUsuario, 0), "mesero")
        Dim m As New dbRestauranteMesas(MySqlcon, IdSucursal)
        Dim m2 As RestauranteMesa = m.Buscar(Me.idMesa)
        If m2 Is Nothing Then
            ImpND.Add(New NodoImpresionN("", "mesa", "", 0), "mesa")
        Else
            ImpND.Add(New NodoImpresionN("", "mesa", m2.Numero.ToString, 0), "mesa")
        End If
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
#End Region

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles btnRepetir.Click
        If dgvPlatillos.SelectedRows.Count > 0 Then
            If idMesa = 0 Or dgvComensales.SelectedRows.Count > 0 Then
                Dim ventas As New dbRestauranteVentas(IdVenta, MySqlcon)
                Dim inv As New dbInventario(MySqlcon)
                Dim detalles As New dbRestauranteVentasDetalles(MySqlcon)

                inv.BuscaArticulo("", True, dgvPlatillos.SelectedRows(0).Cells(colDescripcion.Index).Value)
                inv.LlenaDatos()
                If idMesa = 0 Then
                    detalles.Agregar(inv.ID, 1, inv.Nombre, inv.PrecioNeto, inv.Iva, IdVenta, "", 0)
                Else
                    detalles.Agregar(inv.ID, 1, inv.Nombre, inv.PrecioNeto, inv.Iva, IdVenta, "", dgvComensales.SelectedRows(colComensal1.Index).Cells(colComensal1.Index).Value)
                End If
                dgvPlatillos.DataSource = ventas.vistaDetalles(IdVenta, -1, -1)
                btnImprimirComanda.Enabled = dgvPlatillos.RowCount > 0
                dgvPlatillos.ClearSelection()
                If dgvPlatillos.RowCount > 0 Then dgvPlatillos.Rows(dgvPlatillos.RowCount - 1).Selected = True
            Else
                MsgBox("Seleccione un comensal.")
            End If
            Else
                MsgBox("Seleccione un platillo.")
            End If
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles btnRemover.Click
        If dgvPlatillos.SelectedRows.Count > 0 Then
            Dim rowIndex As Integer = dgvPlatillos.SelectedRows(0).Index
            Dim ventas As New dbRestauranteVentas(idVenta, MySqlcon)
            Dim detalles As New dbRestauranteVentasDetalles(MySqlcon)
            If detalles.eliminar(dgvPlatillos.SelectedRows(0).Cells(colId.Index).Value) Then
                dgvPlatillos.DataSource = ventas.vistaDetalles(IdVenta, -1, -1)
                btnImprimirComanda.Enabled = dgvPlatillos.RowCount > 0
                dgvPlatillos.ClearSelection()
                If dgvPlatillos.RowCount > 0 Then
                    If rowIndex < dgvPlatillos.RowCount Then
                        dgvPlatillos.Rows(rowIndex).Selected = True
                    Else
                        dgvPlatillos.Rows(dgvPlatillos.RowCount - 1).Selected = True
                    End If
                End If
            Else
                MsgBox("No es posible eliminar el platillo.")
            End If
        Else
            MsgBox("Seleccione un platillo.")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub dgvComensales_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvComensales.CellPainting
            If e.RowIndex <> -1 Then
            If e.Value < coloresComensales.Length And e.Value > 0 Then e.CellStyle.BackColor = coloresComensales(e.Value - 1)
            End If
    End Sub

    Private Sub dgvPlatillos_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvPlatillos.CellPainting
        If e.RowIndex <> -1 Then
            If dgvPlatillos.Rows(e.RowIndex).Cells(colComensal.Index).Value < coloresComensales.Length And dgvPlatillos.Rows(e.RowIndex).Cells(colComensal.Index).Value > 0 Then e.CellStyle.BackColor = coloresComensales(dgvPlatillos.Rows(e.RowIndex).Cells(colComensal.Index).Value - 1)
        End If
    End Sub



#Region "DragDrop"
    'Private dragBoxFromMouseDown As Rectangle
    'Private rowIndexFromMouseDown As Integer
    'Private rowIndexOfItemUnderMouseToDrop As Integer
    'Private Sub dataGridView1_MouseMove(sender As Object, e As MouseEventArgs) Handles dgvPlatillos.MouseMove

    '    If (e.Button & MouseButtons.Left) = MouseButtons.Left Then
    '        ' If the mouse moves outside the rectangle, start the drag.
    '        If dragBoxFromMouseDown <> Rectangle.Empty And Not dragBoxFromMouseDown.Contains(e.X, e.Y) Then
    '            ' Proceed with the drag and drop, passing in the list item.                    
    '            Dim dropEffect As DragDropEffects = dgvPlatillos.DoDragDrop(dgvPlatillos.Rows(rowIndexFromMouseDown), DragDropEffects.Move)
    '        End If
    '    End If
    'End Sub

    'Private Sub dataGridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvPlatillos.MouseDown
    '    ' Get the index of the item the mouse is below.
    '    rowIndexFromMouseDown = dgvPlatillos.HitTest(e.X, e.Y).RowIndex
    '    If rowIndexFromMouseDown <> -1 Then
    '        ' Remember the point where the mouse down occurred. 
    '        ' The DragSize indicates the size that the mouse can move before a drag event should be started.                
    '        Dim dragSize As Size = SystemInformation.DragSize
    '        ' Create a rectangle using the DragSize, with the mouse position being at the center of the rectangle.
    '        dragBoxFromMouseDown = New Rectangle(New Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize)
    '    Else
    '        ' Reset the rectangle if the mouse is not over an item in the ListBox.
    '        dragBoxFromMouseDown = Rectangle.Empty
    '    End If
    'End Sub

    'Private Sub dataGridView1_DragOver(sender As Object, e As DragEventArgs) Handles dgvComensales.DragOver, dgvPlatillos.DragOver
    '    e.Effect = DragDropEffects.Move
    'End Sub

    'Private Sub dataGridView1_DragDrop(sender As Object, e As DragEventArgs) Handles dgvComensales.DragDrop

    '    ' The mouse locations are relative to the screen, so they must be 
    '    ' converted to client coordinates.
    '    Dim clientPoint As Point = dgvComensales.PointToClient(New Point(e.X, e.Y))

    '    ' Get the row index of the item the mouse is below. 
    '    rowIndexOfItemUnderMouseToDrop = dgvComensales.HitTest(clientPoint.X, clientPoint.Y).RowIndex

    '    ' If the drag operation was a move then remove and insert the row.
    '    If e.Effect = DragDropEffects.Move Then
    '        Dim rowToMove As DataGridViewRow = e.Data.GetData(GetType(DataGridViewRow))
    '        detalles.modificarComensal(dgvPlatillos.Rows(rowIndexFromMouseDown).Cells(colId.Index).Value, dgvComensales.Rows(rowIndexOfItemUnderMouseToDrop).Cells(colComensal1.Index).Value)
    '        dgvPlatillos.DataSource = ventas.vistaDetalles(idVenta, -1, -1)
    'btnImprimirComanda.Enabled = dgvPlatillos.RowCount > 0
    '        btnCambiarMesa.Enabled = dgvPlatillos.RowCount > 0
    '        dgvPlatillos.ClearSelection()
    '        dgvPlatillos.Rows(rowIndexOfItemUnderMouseToDrop).Selected = True
    '        rowIndexFromMouseDown = -1
    '        dragBoxFromMouseDown = Rectangle.Empty
    '    End If
    'End Sub

    'Private Sub dataGridView2_DragDrop(sender As Object, e As DragEventArgs) Handles dgvPlatillos.DragDrop

    '    rowIndexFromMouseDown = -1
    '    dragBoxFromMouseDown = Rectangle.Empty
    'End Sub

#End Region

End Class