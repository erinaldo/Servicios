Public Class frmSemillasImpresion


    Dim boleta As dbSemillasBoleta
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
    Dim codigo As String
    Dim idInventario As Integer
    Dim precio As Double
    Dim copias As Integer
    Public Sub New(ByRef boleta As dbSemillasBoleta)

        ' This call is required by the designer.
        InitializeComponent()
        Me.boleta = boleta
        copias = 1
        ' Add any initialization after the InitializeComponent() call.

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
            ImpDb.DaZonaDetalles(TiposDocumentos.codigoBarras, GlobalIdSucursalDefault)
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.codigoBarras, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CodigoBarras + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'If iTipoFacturacion = 2 Then
        '    'CFDi
        '    DibujaPaginai(e.Graphics)
        'Else
        'CFD

        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            'e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If

        'If Estado = Estados.Cancelada Then
        '    e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
        'End If
        e.HasMorePages = MasPaginas
        'DibujaPagina(e.Graphics)
        'e.HasMorePages = True
        'End If


    End Sub

    Private Sub Imprimir()
        'If txtPrecio.Text <> "" Then
        '    precio = Double.Parse(txtPrecio.Text)
        'End If
        'Try

        '    Dim code As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
        '    code.Code = codigo
        '    Dim image As Image = code.CreateDrawingImage(Color.Black, Color.White)
        '    CodigoBidimensional = New Bitmap(image, New Size(300, 200))

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        Dim copiasAux As Short = copias
        If txtCopias.Text <> "" Then
            copiasAux = Short.Parse(txtCopias.Text)
        End If
        Dim suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        PrintDialog1.PrinterSettings.Copies = copiasAux
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            Impresora = PrintDialog1.PrinterSettings.PrinterName

            Dim TipoImpresora As String
            TipoImpresora = 0
            PrintDocument1.DocumentName = "Boleta"

            PrintDocument1.PrinterSettings.PrinterName = Impresora
            LlenaNodosImpresion()
            If TipoImpresora = 0 Then
                LlenaNodos(suc.ID, TiposDocumentos.SemillasBoleta)
            Else
                LlenaNodos(suc.ID, TiposDocumentos.SemillasBoleta + 1000)
            End If

            PrintDocument1.Print()

        End If
        'Dim V As New dbVentas(idVenta, MySqlcon, Op._Sinnegativos)
        'If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
        '    If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '        LlenaNodosImpresionRet()
        '        If TipoImpresora = 0 Then
        '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
        '        Else
        '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
        '        End If
        '        PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
        '        If Impresora = "Bullzip PDF Printer" Then
        '            Dim obj As New Bullzip.PdfWriter.PdfSettings
        '            obj.Init()
        '            obj.PrinterName = Impresora
        '            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
        '            obj.SetValue("ShowSettings", "never")
        '            obj.SetValue("ShowPDF", "yes")
        '            obj.SetValue("ShowSaveAS", "nofile")
        '            obj.SetValue("ConfirmOverwrite", "no")
        '            obj.SetValue("Target", "printer")
        '            obj.WriteSettings()
        '        End If
        '        DocAImprimir = 1
        '        PrintDocument1.Print()

        '    End If
        'End If
        'imprimirNotarial()
        'If V.Cliente.Email <> "" Then
        '    Try
        '        If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '            If V.Cliente.Email <> "" Then
        '                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
        '                'Dim O As New dbOpciones(MySqlcon)
        '                Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        '                Dim C As String
        '                C = "Eviado por: " + S.NombreFiscal + vbNewLine + "RFC: " + S.RFC + vbNewLine + "FACTURA" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje."
        '                'If pEstado = Estados.Pendiente Then
        '                M.send("Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\Fac" + TextBox11.Text + TextBox2.Text + ".pdf", "")
        '                'Else
        '                '   M.send("Comprobante Fiscal Digital Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\FAC-" + V.Serie + V.Folio.ToString + ".pdf", RutaXml + "\FAC-" + V.Serie + V.Folio.ToString + ".xml")
        '                'End If

        '                PopUp("Correo enviado", 90)
        '            End If
        '        End If
        '    Catch ex As Exception
        '        MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    End Try
        'End If

    End Sub

    Private Sub LlenaNodosImpresion()
        Dim O As New dbOpciones(MySqlcon)

        ImpND.Clear()
        Dim i As New dbSemillasBoleta(MySqlcon)
        ImpND.Add(New NodoImpresionN("", "fecha", boleta.fecha.ToString("yyyy/MM/dd"), 0), "fecha")
        ImpND.Add(New NodoImpresionN("", "boleta", boleta.id.ToString(), 0), "Boleta")
        ImpND.Add(New NodoImpresionN("", "peso bruto", boleta.peso.ToString(), 0), "peso bruto")
        ImpND.Add(New NodoImpresionN("", "humedad", boleta.humedad.ToString(), 0), "humedad")
        ImpND.Add(New NodoImpresionN("", "quebrado", boleta.granoQuebrado.ToString(), 0), "quebrado")
        ImpND.Add(New NodoImpresionN("", "impurezas", boleta.impurezas.ToString(), 0), "impurezas")
        ImpND.Add(New NodoImpresionN("", "dañado", boleta.granoDanado.ToString(), 0), "dañado")
        ImpND.Add(New NodoImpresionN("", "total deducciones", boleta.castigoTotal.ToString(), 0), "total deducciones")
        ImpND.Add(New NodoImpresionN("", "peso liquidar", boleta.pesoFinal.ToString(), 0), "peso liquidar")
        Dim art As New dbInventarioPrecios(boleta.producto.ID, MySqlcon)
        art.BuscaPrecio(boleta.producto.ID, 1)
        ImpND.Add(New NodoImpresionN("", "precio/ton", art.Precio.ToString(), 0), "precion/ton")
        ImpND.Add(New NodoImpresionN("", "importe", boleta.importe.ToString(), 0), "importe")
        ImpND.Add(New NodoImpresionN("", "FolioBoleta", boleta.folio.ToString(), 0), "FolioBoleta")
        'ImpND.Add(New NodoImpresionN("", "codigo", precio, 0), "precio")


    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Imprimir()
    End Sub
End Class