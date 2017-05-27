Public Class DibujaReportes
    Public ImpND As New Collection
    Public ImpNDD As New Collection
    Public ImpNDDi As New Collection
    Public ImpNDi As New Collection
    Public ImpNDi2 As New Collection
    Public Posicion As Integer
    Public CuantosRenglones As Integer
    Public MasPaginas As Boolean
    Public NumeroPagina As Integer
    Public CuantaY As Integer
    Public CodigoBidimensional As Bitmap
    Public DocAImprimir As Byte = 0
    Public TipoImpresora As Byte = 0
    Public TipoImpresion As String
    '  Dim ImpNDi2 As New Collection
    Public Sub New()

    End Sub
    Public Sub CorteX3P(ByRef e As System.Drawing.Graphics, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdVendedor As Integer, ByVal pNombreVendedor As String, ByVal pHora1 As String, ByVal pHora2 As String, ByVal pConHoras As Boolean, ByVal pTipo As Byte, ByVal pIdSucursal As Integer, ByVal pidCaja As Integer)
        Dim strF As New StringFormat
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        'Dim Rec As RectangleF
        Dim F As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
        'Dim XCoord As Single = 1
        Dim Ooc As New dbOpcionesOc(MySqlcon)
        Dim Op As New dbOpciones(MySqlcon)
        Ooc.LlenaDatos(0, GlobalIdSucursalDefault)
        Dim YCoord As Single = 5
        'Dim C As Integer
        Dim PV As New dbPuntodeVenta(Conexion)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        DR = Nothing
        Try
            Select Case pTipo
                Case 0
                    DR = PV.CorteX(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, Op.FormatoFechaPv, pIdSucursal, pidCaja)
                Case 1
                    DR = PV.CorteXVendedor(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, Op.FormatoFechaPv, pIdSucursal, pidCaja)
                Case 2
                    DR = PV.CorteXTipodePago(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, Op.FormatoFechaPv, pIdSucursal, pidCaja)
                Case 3
                    DR = PV.CorteXTipodePagoConcentrado(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, Op.FormatoFechaPv, pIdSucursal, pidCaja)
            End Select

            While DR.Read
                If DR("cantidad") <> 0 Then
                    e.DrawString(DR("concepto"), F, Brushes.Black, 2, YCoord)
                    If DR("mostrarcantidad") = 1 Then
                        e.DrawString(Format(DR("cantidad"), DR("formato")).PadLeft(10), F, Brushes.Black, 45, YCoord)
                    End If
                    YCoord += 3
                End If
            End While
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'If DR.IsClosed = False Then DR.Close()
        End Try

    End Sub
    Public Sub CorteX2P(ByRef e As System.Drawing.Graphics, ByVal Conexion As MySql.Data.MySqlClient.MySqlConnection, ByVal pFecha1 As String, ByVal pFecha2 As String, ByVal pIdVendedor As Integer, ByVal pNombreVendedor As String, ByVal pHora1 As String, ByVal pHora2 As String, ByVal pConHoras As Boolean, ByVal pTipo As Byte, ByVal pIdSucursal As Integer, ByVal pIdCaja As Integer)
        Dim strF As New StringFormat
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        'Dim Rec As RectangleF
        Dim F As New Font("Lucida Console", 7, FontStyle.Regular, GraphicsUnit.Point)
        Dim Ooc As New dbOpcionesOc(MySqlcon)
        Dim op As New dbOpciones(MySqlcon)
        Ooc.LlenaDatos(0, GlobalIdSucursalDefault)
        'Dim XCoord As Single = 1
        Dim YCoord As Single = 5
        'Dim C As Integer
        Dim PV As New dbPuntodeVenta(Conexion)
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        DR = Nothing
        Try
            Select Case pTipo
                Case 0
                    DR = PV.CorteX(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, op.FormatoFechaPv, pIdSucursal, pIdCaja)
                Case 1
                    DR = PV.CorteXVendedor(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, op.FormatoFechaPv, pIdSucursal, pIdCaja)
                Case 2
                    DR = PV.CorteXTipodePago(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, op.FormatoFechaPv, pIdSucursal, pIdCaja)
                Case 3
                    DR = PV.CorteXTipodePagoConcentrado(pFecha1, pFecha2, pIdVendedor, pHora1, pHora2, pConHoras, pNombreVendedor, Ooc.SeriesOc, Ooc.OcultarOc, op.FormatoFechaPv, pIdSucursal, pIdCaja)
            End Select
            While DR.Read
                If DR("cantidad") <> 0 Then
                    e.DrawString(DR("concepto"), F, Brushes.Black, 2, YCoord)
                    If DR("mostrarcantidad") = 1 Then
                        'YCoord += 3
                        e.DrawString(Format(DR("cantidad"), DR("formato")).PadLeft(10), F, Brushes.Black, 28, YCoord)
                    End If
                    YCoord += 3
                End If
            End While
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            'If DR.IsClosed = False Then DR.Close()
        End Try

    End Sub

    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pidSucursal As Integer, ByVal pConCBB As Boolean, ByVal pIdEmpresa As Integer)

        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(DocAImprimir, pidSucursal, pIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(DocAImprimir + 16, pidSucursal, pIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If


            'If DocAImprimir = 0 Then
            '    If TipoImpresora = 0 Then
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Venta, pidSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    Else
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentaTicket, pidSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    End If
            'Else
            '    If TipoImpresora = 0 Then
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, pidSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    Else
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, pidSucursal, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    End If
            'End If
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
                If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
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

            If ncb.Visible = 1 And pConCBB Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
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
    Private Function DaImagenFondo(ByVal pDocumento As Integer, ByVal pIdsucursal As Integer, ByVal pIdEmpresa As Integer, ByVal pTipoImpresora As Byte) As Image
        Dim SA As New dbSucursalesArchivos
        If pTipoImpresora = 0 Then
            Return Image.FromFile(SA.DaRuta(pDocumento, pIdsucursal, pIdEmpresa, True))
        Else
            Return Image.FromFile(SA.DaRuta(pDocumento + 16, pIdsucursal, pIdEmpresa, True))
        End If
    End Function


    '-------
    Public Function Imprimir(ByVal tipoDoc As String, Optional ByVal nombreArchivo As String = "", Optional ByVal fechaImpresion As String = "") As String
        TipoImpresion = tipoDoc
        Dim en As New Encriptador
        '  Dim P As New dbDeposito(MySqlcon) 'Modificado
        ' P.Buscar(idDeposito) 'Modificado
        Dim RutaPDF As String

        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(GlobalIdSucursalDefault, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(fechaImpresion, "yyyy") + "\") 'Modificado
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(fechaImpresion, "yyyy") + "\" + Format(fechaImpresion, "MM") + "\") 'Modificado
        RutaPDF = RutaPDF + "\" + Format(fechaImpresion, "yyyy") + "\" + Format(fechaImpresion, "MM") 'Modificado

        'PrintDocument1.DocumentName = nombreArchivo 'Modificado ESTO SE NECESITA PONER AFUERA
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String = ""
        If TipoImpresion = "Compras" Then '****************************************
            Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.Compra) 'Modificado

        End If
        If TipoImpresion = "PreOrden" Then

            Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CompraCotizacion) 'Modificado
        End If
        If TipoImpresion = "Pedido" Then

            Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CompraPedido) 'Modificado
        End If
        If TipoImpresion = "Remision" Then

            Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CompraRemision) 'Modificado
        End If
        If TipoImpresion = "Devoluciones" Then
            Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CompraDevolucion) 'Modificado
        End If

        If TipoImpresion = "NotasCredito" Then
            Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CompraNotadeCredito) 'Modificado
        End If
        If TipoImpresion = "NotasCargo" Then
            Impresora = SA.DaImpresoraActiva(GlobalIdSucursalDefault, GlobalIdEmpresa, True, 0, TiposDocumentos.CompraNotadeCargo) 'Modificado
        End If
        TipoImpresora = SA.TipoImpresora




        'obj.WriteSettings()
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        ' PrintDocument1.PrinterSettings.PrinterName = Impresora '***********
        'If tipoDoc = "Compras-Factura" Then
        '    LlenaNodosImpresionComprasFactura()
        'End If
        'LlenaNodosImpresion()
        'If TipoImpresora = 0 Then
        '    LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosPolizaDepósitos) 'Modificado
        'Else
        '    LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.BancosPolizaDepósitos) 'Modificado
        'End If
        'PrintDocument1.Print() '******************************
        Return Impresora
    End Function
    Public Sub LlenaNodosImpresionNotasCargo(ByVal Folio As String, ByVal Fecha As String, ByVal SubTotal As String, ByVal IVA As String, ByVal Total As String, ByVal IVAcant As String, ByVal CantLetra As String, ByVal cancelado As String, ByVal empresaNombre As String, ByVal empresaCalle As String, ByVal empresaColonia As String, ByVal empresaCP As String, ByVal empresaRFC As String, ByVal empresaTel As String, ByVal empresaTel2 As String, ByVal empresaEMail As String, ByVal provNombre As String, ByVal provDom As String, ByVal provCol As String, ByVal provCP As String, ByVal provCiudad As String, ByVal provRFC As String, ByVal datos As DataTable, ByVal pidNota As Integer, ByVal pNumeroInt As String, ByVal pNumeroExt As String, ByVal pEstado As String, ByVal pPais As String, ByVal pComentario As String)
        ImpND.Clear()
        ImpNDD.Clear()
        ImpNDDi.Clear()
        ImpNDi2.Clear()
        Dim O As New dbOpciones(MySqlcon)
        'Dim firmaChequeRecibido As String = "Firma cheque recibido"
        ImpND.Add(New NodoImpresionN("", "privnoInterior", pNumeroInt, 0), "privnoInterior")
        ImpND.Add(New NodoImpresionN("", "provnoExterior", pNumeroExt, 0), "provnoExterior")
        ImpND.Add(New NodoImpresionN("", "provEstado", pEstado, 0), "provEstado")
        ImpND.Add(New NodoImpresionN("", "provPais", pPais, 0), "provPais")

        ImpND.Add(New NodoImpresionN("", "docFecha", Fecha, 0), "docFecha")
        ImpND.Add(New NodoImpresionN("", "docSubTotal", Format(CDbl(SubTotal), O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "docSubTotal")
        'ImpND.Add(New NodoImpresionN("", "doctotalieps", pTotalIeps, 0), "doctotalieps")
        'ImpND.Add(New NodoImpresionN("", "doctotalivaret", pTotalIvaRet, 0), "doctotalivaret")
        ImpND.Add(New NodoImpresionN("", "docTotal", Format(CDbl(Total), O._formatoTotal).PadLeft(O.Espaciototal), 0), "docTotal")

        ImpND.Add(New NodoImpresionN("", "docCantLetra", CantLetra, 0), "docCantLetra")

        ImpND.Add(New NodoImpresionN("", "cancelado", cancelado, 0), "cancelado")
        'ImpND.Add(New NodoImpresionN("", "docAutorizo", autorizo, 0), "docAutorizo")

        ImpND.Add(New NodoImpresionN("", "empresaNombre", empresaNombre, 0), "empresaNombre")
        ImpND.Add(New NodoImpresionN("", "empresaCalle", empresaCalle, 0), "empresaCalle")
        ImpND.Add(New NodoImpresionN("", "empresaColonia", empresaColonia, 0), "empresaColonia")
        ImpND.Add(New NodoImpresionN("", "empresaCP", empresaCP, 0), "empresaCP")
        ImpND.Add(New NodoImpresionN("", "empresaRFC", empresaRFC, 0), "empresaRFC")
        ImpND.Add(New NodoImpresionN("", "empresaTel", empresaTel, 0), "empresaTel")
        ImpND.Add(New NodoImpresionN("", "empresaTel2", empresaTel2, 0), "empresaTel2")
        ImpND.Add(New NodoImpresionN("", "empresaEMail", empresaEMail, 0), "empresaEMail")
        ImpND.Add(New NodoImpresionN("", "comentario", pComentario, 0), "comentario")
        ImpND.Add(New NodoImpresionN("", "provNombre", provNombre, 0), "provNombre")
        ImpND.Add(New NodoImpresionN("", "provDom", provDom, 0), "provDom")
        ImpND.Add(New NodoImpresionN("", "provCol", provCol, 0), "provCol")
        ImpND.Add(New NodoImpresionN("", "provCP", provCP, 0), "provCP")
        ImpND.Add(New NodoImpresionN("", "provCiudad", provCiudad, 0), "provCiudad") 'no
        ImpND.Add(New NodoImpresionN("", "provRFC", provRFC, 0), "provRFC")
        CuantosRenglones = 0
        For i As Integer = 0 To datos.Rows.Count() - 1
            ImpNDD.Add(New NodoImpresionN("", "artCant", datos.Rows(i)(1).ToString(), 0), "artCant" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "descripcion", datos.Rows(i)(0).ToString(), 0), "descripcion" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "artPrecioU", datos.Rows(i)(2).ToString(), 0), "artPrecioU" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "provImporte", datos.Rows(i)(3).ToString(), 0), "provImporte" + Format(i, "000"))
            ' ImpNDD.Add(New NodoImpresionN("", "artCod", datos.Rows(i)(4).ToString(), 0), "artCod" + Format(i, "000"))
            CuantosRenglones += 1
        Next

        Dim V As New dbNotasdeCargoCompras(pidNota, MySqlcon)

        ImpND.Add(New NodoImpresionN("", "docFolio", Format(V.Folioi, "0000"), 0), "docFolio")
        ImpND.Add(New NodoImpresionN("", "referencia", V.Folio, 0), "referencia")
        ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpND.Add(New NodoImpresionN("", "foliocfdi", V.FolioCFDI, 0), "foliocfdi")

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasdeCargoComprasDetalles(MySqlcon)
        Dim Ivas As New Collection
        Dim ivaCantidad As Integer = 0
        Dim aux As Integer = 0
        Dim IvasImporte As New Collection
        DR = VI.ConsultaReader(pidNota)
        Dim IAnt As Double


        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
                ivaCantidad += 1
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                ivaCantidad += 1
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                ivaCantidad += 1
            End If
        End While
        DR.Close()
        ImpND.Add(New NodoImpresionN("", "docIVAcant", Format(55, "#0.00") + "%:", 0), "docIVAcant")
        ' Y += 4

        'If IVA = 0 Then
        '    ImpND.Add(New NodoImpresionN("", "docIVA", Format(0.0, "$#,###,##0.00").PadLeft(13), 0), "docIVA")
        'End If
        ' ImpND.Add(New NodoImpresionN("", "docIVAcant", Format(I, "#0.00") + "%:", 0), "docIVAcant")

        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If

        If ivaCantidad = 0 Then
            ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(0, "#0.00") + "%:", Format(0, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(aux, "00"))
        Else
            For Each I As Double In Ivas
                ' ImpNDD.Add(New NodoImpresionN("", "docIVA", Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), 0), "docIVA")

                ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(aux, "00"))

                aux = aux + 1

            Next
        End If
        'ImpNDD.Add(New NodoImpresionN("", "docIVA", 55, 0), "docIVA")
        ' ImpNDD.Add(New NodoImpresionN("", "docIVAcant", Format(16, "#0.00"), 0), "docIVAcant")
        ' ImpND.Add(New NodoImpresionN("", "docIVA", Format(55555, "$#,###,##0.00"), 0), "docIVA")
        Posicion = 0
        NumeroPagina = 1
        If TipoImpresora = 0 Then
            LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.CompraNotadeCargo) 'Modificado
        Else
            LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.CompraNotadeCargo + 1000) 'Modificado
        End If

    End Sub
    Public Sub LlenaNodosImpresionNotasCredito(ByVal Folio As String, ByVal Fecha As String, ByVal SubTotal As String, ByVal IVA As String, ByVal Total As String, ByVal IVAcant As String, ByVal CantLetra As String, ByVal cancelado As String, ByVal empresaNombre As String, ByVal empresaCalle As String, ByVal empresaColonia As String, ByVal empresaCP As String, ByVal empresaRFC As String, ByVal empresaTel As String, ByVal empresaTel2 As String, ByVal empresaEMail As String, ByVal provNombre As String, ByVal provDom As String, ByVal provCol As String, ByVal provCP As String, ByVal provCiudad As String, ByVal provRFC As String, ByVal datos As DataTable, ByVal pidNota As Integer, ByVal pNumeroInt As String, ByVal pNumeroExt As String, ByVal pEstado As String, ByVal pPais As String, ByVal pComentario As String)
        ImpND.Clear()
        ImpNDD.Clear()
        ImpNDDi.Clear()
        ImpNDi2.Clear()
        Dim O As New dbOpciones(MySqlcon)
        'Dim firmaChequeRecibido As String = "Firma cheque recibido"

        ImpND.Add(New NodoImpresionN("", "docFecha", Fecha, 0), "docFecha")
        ImpND.Add(New NodoImpresionN("", "docSubTotal", Format(CDbl(SubTotal), O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "docSubTotal")

        ImpND.Add(New NodoImpresionN("", "docTotal", Format(CDbl(Total), O._formatoTotal).PadLeft(O.Espaciototal), 0), "docTotal")

        ImpND.Add(New NodoImpresionN("", "docCantLetra", CantLetra, 0), "docCantLetra")

        ImpND.Add(New NodoImpresionN("", "cancelado", cancelado, 0), "cancelado")
        'ImpND.Add(New NodoImpresionN("", "docAutorizo", autorizo, 0), "docAutorizo")

        ImpND.Add(New NodoImpresionN("", "empresaNombre", empresaNombre, 0), "empresaNombre")
        ImpND.Add(New NodoImpresionN("", "empresaCalle", empresaCalle, 0), "empresaCalle")
        ImpND.Add(New NodoImpresionN("", "empresaColonia", empresaColonia, 0), "empresaColonia")
        ImpND.Add(New NodoImpresionN("", "empresaCP", empresaCP, 0), "empresaCP")
        ImpND.Add(New NodoImpresionN("", "empresaRFC", empresaRFC, 0), "empresaRFC")
        ImpND.Add(New NodoImpresionN("", "empresaTel", empresaTel, 0), "empresaTel")
        ImpND.Add(New NodoImpresionN("", "empresaTel2", empresaTel2, 0), "empresaTel2")
        ImpND.Add(New NodoImpresionN("", "empresaEMail", empresaEMail, 0), "empresaEMail")
        ImpND.Add(New NodoImpresionN("", "comentario", pComentario, 0), "comentario")
        ImpND.Add(New NodoImpresionN("", "provNombre", provNombre, 0), "provNombre")
        ImpND.Add(New NodoImpresionN("", "provDom", provDom, 0), "provDom")
        ImpND.Add(New NodoImpresionN("", "provCol", provCol, 0), "provCol")
        ImpND.Add(New NodoImpresionN("", "provCP", provCP, 0), "provCP")
        ImpND.Add(New NodoImpresionN("", "provCiudad", provCiudad, 0), "provCiudad") 'no
        ImpND.Add(New NodoImpresionN("", "provRFC", provRFC, 0), "provRFC")
        CuantosRenglones = 0
        For i As Integer = 0 To datos.Rows.Count() - 1
            ImpNDD.Add(New NodoImpresionN("", "artCant", datos.Rows(i)(1).ToString(), 0), "artCant" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "descripcion", datos.Rows(i)(0).ToString(), 0), "descripcion" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "artPrecioU", datos.Rows(i)(2).ToString(), 0), "artPrecioU" + Format(i, "000"))
            ImpNDD.Add(New NodoImpresionN("", "provImporte", datos.Rows(i)(3).ToString(), 0), "provImporte" + Format(i, "000"))
            ' ImpNDD.Add(New NodoImpresionN("", "artCod", datos.Rows(i)(4).ToString(), 0), "artCod" + Format(i, "000"))
            CuantosRenglones += 1
        Next







        ImpND.Add(New NodoImpresionN("", "privnoInterior", pNumeroInt, 0), "privnoInterior")
        ImpND.Add(New NodoImpresionN("", "provnoExterior", pNumeroExt, 0), "provnoExterior")
        ImpND.Add(New NodoImpresionN("", "provEstado", pEstado, 0), "provEstado")
        ImpND.Add(New NodoImpresionN("", "provPais", pPais, 0), "provPais")


        Dim V As New dbNotasdeCreditoCompras(pidNota, MySqlcon)

        ImpND.Add(New NodoImpresionN("", "docFolio", Format(V.Folioi, "0000"), 0), "docFolio")
        ImpND.Add(New NodoImpresionN("", "referencia", V.Folio, 0), "referencia")
        ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpND.Add(New NodoImpresionN("", "foliocfdi", V.FolioCFDI, 0), "foliocfdi")

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasdeCreditoComprasDetalles(MySqlcon)
        Dim Ivas As New Collection
        Dim ivaCantidad As Integer = 0
        Dim aux As Integer = 0
        Dim IvasImporte As New Collection
        Dim IAnt As Double
        DR = VI.ConsultaReader(pidNota)

        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
                ivaCantidad += 1
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") - (DR("precio") / (1 + ((DR("iva") / 100)))), DR("iva").ToString)
                ivaCantidad += 1
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") - ((DR("precio") / (1 + ((DR("iva") / 100)))))), DR("iva").ToString)
                ivaCantidad += 1
            End If

        End While
        DR.Close()
        'ImpND.Add(New NodoImpresionN("", "docIVAcant", Format(55, "#0.00") + "%:", 0), "docIVAcant")

        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If

        If ivaCantidad = 0 Then
            ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(0, "#0.00") + "%:", Format(0, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(aux, "00"))
        Else
            For Each I As Double In Ivas
                ' ImpNDD.Add(New NodoImpresionN("", "docIVA", Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), 0), "docIVA")

                ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(aux, "00"))

                aux = aux + 1

            Next
        End If

        'ImpNDD.Add(New NodoImpresionN("", "docIVA", 55, 0), "docIVA")
        ' ImpNDD.Add(New NodoImpresionN("", "docIVAcant", Format(16, "#0.00"), 0), "docIVAcant")
        ' ImpND.Add(New NodoImpresionN("", "docIVA", Format(55555, "$#,###,##0.00"), 0), "docIVA")
        Posicion = 0
        NumeroPagina = 1
        If TipoImpresora = 0 Then
            LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.CompraNotadeCredito) 'Modificado
        Else
            LlenaNodos(GlobalIdSucursalDefault, TiposDocumentos.CompraNotadeCredito + 1000) 'Modificado
        End If

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

    Public Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            If TipoImpresion = "Compras" Then '************************
                ImpDb.DaZonaDetalles(TiposDocumentos.Compra, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "PreOrden" Then

                ImpDb.DaZonaDetalles(TiposDocumentos.CompraCotizacion, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "Pedido" Then

                ImpDb.DaZonaDetalles(TiposDocumentos.CompraPedido, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "Remision" Then

                ImpDb.DaZonaDetalles(TiposDocumentos.CompraRemision, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "Devoluciones" Then
                ImpDb.DaZonaDetalles(TiposDocumentos.CompraDevolucion, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "NotasCredito" Then
                ImpDb.DaZonaDetalles(TiposDocumentos.CompraNotadeCredito, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "NotasCargo" Then
                ImpDb.DaZonaDetalles(TiposDocumentos.CompraNotadeCargo, GlobalIdSucursalDefault) '****
            End If

        Else
            If TipoImpresion = "Compras" Then '*********************
                ImpDb.DaZonaDetalles(TiposDocumentos.Compra + 1000, GlobalIdSucursalDefault) '*****
          
            End If
            If TipoImpresion = "PreOrden" Then

                ImpDb.DaZonaDetalles(TiposDocumentos.CompraCotizacion + 1000, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "Pedido" Then

                ImpDb.DaZonaDetalles(TiposDocumentos.CompraPedido + 1000, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "Remision" Then '*********************
                ImpDb.DaZonaDetalles(TiposDocumentos.CompraRemision + 1000, GlobalIdSucursalDefault) '*****

            End If
            If TipoImpresion = "Devoluciones" Then
                ImpDb.DaZonaDetalles(TiposDocumentos.CompraDevolucion + 1000, GlobalIdSucursalDefault) '****
            End If

            If TipoImpresion = "NotasCredito" Then
                ImpDb.DaZonaDetalles(TiposDocumentos.CompraNotadeCredito + 1000, GlobalIdSucursalDefault) '****
            End If
            If TipoImpresion = "NotasCargo" Then
                ImpDb.DaZonaDetalles(TiposDocumentos.CompraNotadeCargo + 1000, GlobalIdSucursalDefault) '****
            End If
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
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            If TipoImpresora = 0 Then
                If TipoImpresion = "Compras" Then '**********************
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Compra, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
              
                End If
                If TipoImpresion = "PreOrden" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraCotizacion, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Pedido" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraPedido, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Remision" Then '*********************
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraRemision, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))

                End If
                If TipoImpresion = "Devoluciones" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraDevolucion, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "NotasCredito" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCredito, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))

                End If
                If TipoImpresion = "NotasCargo" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCargo, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresion = "Compras" Then '**************************
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Compra + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))

                End If
                If TipoImpresion = "PreOrden" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraCotizacion + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Pedido" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraPedido + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Remision" Then '*********************
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraRemision + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))

                End If
                If TipoImpresion = "Devoluciones" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraDevolucion + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "NotasCredito" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCredito + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))

                End If
                If TipoImpresion = "NotasCargo" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCargo + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            End If

        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer


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
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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

            '***********************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '***********************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n

                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName) 'aqui
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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

                        If niva.ConEtiqueta = 1 Then
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

            End If
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                If TipoImpresion = "Compras" Then '******************************
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Compra, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
              
                End If
                If TipoImpresion = "PreOrden" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraCotizacion, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Pedido" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraPedido, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If

                If TipoImpresion = "Remision" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraRemision, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Devoluciones" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraDevolucion, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "NotasCredito" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCredito, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "NotasCargo" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCargo, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            Else
                If TipoImpresion = "Compras" Then '************************************
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.Compra + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
               
                End If
                If TipoImpresion = "PreOrden" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraCotizacion + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Pedido" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraPedido + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Remision" Then

                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraRemision + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "Devoluciones" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraDevolucion + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "NotasCredito" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCredito + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
                If TipoImpresion = "NotasCargo" Then
                    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CompraNotadeCargo + 1000, GlobalIdSucursalDefault, GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
                End If
            End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then

                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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

                        If niva.ConEtiqueta = 1 Then
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

            End If
            'If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
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
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
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
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            '*****************************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0

            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
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
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '********************************

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
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
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
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
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

                        If niva.ConEtiqueta = 1 Then
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

            End If

            'If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    'Private Sub ImprimirCompra()
    '    DibujaPaginaN(e.Graphics)
    '    If MasPaginas = True Or NumeroPagina > 2 Then
    '        e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
    '    End If

    '    e.HasMorePages = MasPaginas
    'End Sub
End Class
