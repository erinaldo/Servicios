Public Class ConectorMacro
    Dim Lineas() As String
    Dim IConexion As MySql.Data.MySqlClient.MySqlConnection
    Dim Comm As New MySql.Data.MySqlClient.MySqlCommand
    Dim Cliente As dbClientes
    Dim Tipo As String
    Public Folio As Integer
    Public Serie As String
    Dim Fecha As String
    Dim Moneda As String
    Dim TipodeCambio As Double
    Dim IdForma As Integer = 1
    Dim idMoneda As Integer
    Public MensajeError As String
    Public Cadena As String
    Public Sello As String
    Public CadenaCFDI As String
    Public TipoImpresora As Byte
    Public RutaXmlTemp As String
    Public RutaXml As String
    Public RutaXmlTimbrado As String
    Public RutaPDF As String
    Public IdDocumento As Integer
    Dim EMail As String
    Dim idVendedor As Integer = 1
    Dim Descuento As Double
    Dim Cargo As Double
    Dim FormadePAgo As String
    Dim NoCuenta As String
    Public Estado As Byte
    Dim IVAGeneral As Double
    Dim RefFacturas As String
    Dim IdSucursalb As Integer
    Dim SerieB As String
    Dim LugarExp As String
    Dim CalleExp As String
    Dim NumeroExp As String
    Dim CodigoCliente As String
    Dim DiasCredito As Integer
    Public IdSucursal As Integer
    Dim Version As String
    Dim RefDocumento As String
    Dim Adicional As String
    Dim CodigoVendedor As String
    Dim Munrec As String
    Public Sub New(ByVal pLineas() As String, ByVal pMysqlCon As MySql.Data.MySqlClient.MySqlConnection, ByVal pVersion As String, ByVal pmunrec As String)
        Lineas = pLineas
        IConexion = pMysqlCon
        Version = pVersion
        Munrec = pmunrec
        Comm.Connection = IConexion
    End Sub

    Public Function ProcesaArchivo(ByVal Tipo As String, ByVal pIdSucursal As Integer, ByVal pSerieB As String, ByVal pIdSucursalB As Integer) As Boolean
        Dim EncontroCliente As Boolean = False

        Cliente = New dbClientes(IConexion)
        IdDocumento = 0
        NoCuenta = ""
        FormadePAgo = ""
        RefFacturas = ""
        CalleExp = ""
        LugarExp = ""
        NumeroExp = ""
        DiasCredito = 0
        CodigoCliente = ""
        RefDocumento = ""
        CodigoVendedor = ""
        Adicional = ""
        EMail = ""
        IdSucursal = pIdSucursal
        IdSucursalb = pIdSucursalB
        SerieB = pSerieB
        IVAGeneral = 0
        Dim TipoForma As Byte
        Try
            For Each L As String In Lineas
                'If L.StartsWith("NOMDOC") Then Tipo = L.Substring(L.IndexOf("   ") + 3, L.Length - L.IndexOf("   ") - 3)

                If Version = "0" Then
                    'Versión 1
                    If Munrec = "0" Then
                        'If L.StartsWith("NUMEMI") Then FormadePAgo = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("ADICL1") Then FormadePAgo = L.Substring(L.IndexOf("   ")).Trim
                    Else
                        If L.StartsWith("MUNREC") Then FormadePAgo = L.Substring(L.IndexOf("   ")).Trim
                        'If L.StartsWith("ADICL1") Then NoCuenta = L.Substring(L.IndexOf("   ")).Trim
                    End If

                    If L.StartsWith("ADICL2") Then NoCuenta = L.Substring(L.IndexOf("   ")).Trim

                    If L.StartsWith("FORPAG") Then
                        If L.Substring(L.IndexOf("   ")).Trim = "CREDITO" Then
                            IdForma = 2
                        Else
                            IdForma = 1
                        End If

                    End If
                    If L.StartsWith("DIAPAG") Then Cliente.CreditoDias = CInt(L.Substring(L.IndexOf("   ")).Trim)
                    If L.StartsWith("NOMENT") Or L.StartsWith("NOMREC") Then Cliente.Nombre = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("CALENT") Or L.StartsWith("CALREC") Then Cliente.Direccion = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("NEXENT") Or L.StartsWith("NEXREC") Then Cliente.Direccion += " " + L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("COLENT") Or L.StartsWith("COLREC") Then Cliente.Colonia = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("MUNENT") Then Cliente.Municipio = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("MUNREC") And Munrec = "0" Then Cliente.Municipio2 = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("LOCENT") Or L.StartsWith("LOCREC") Then Cliente.Ciudad = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("ESTENT") Or L.StartsWith("ESTREC") Then Cliente.Estado = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("PAIENT") Then Cliente.Pais = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("CODENT") Or L.StartsWith("CODREC") Then Cliente.CP = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("TELENT") Then Cliente.Telefono = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("RFCENT") Or L.StartsWith("RFCREC") Then Cliente.RFC = L.Substring(L.IndexOf("   ")).Trim
                Else
                    'Version 1.1
                    If L.StartsWith("METPAG") Then FormadePAgo = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("CTAPAG") Then NoCuenta = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("LUGEXP") Then LugarExp = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("CALEXP") Then CalleExp = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("NEXEXP") Then NumeroExp = L.Substring(L.IndexOf("   ")).Trim
                    If L.StartsWith("DIAPAG") Then
                        Cliente.CreditoDias = CInt(L.Substring(L.IndexOf("   ")).Trim)
                        If Cliente.CreditoDias <> 0 Then
                            IdForma = 2
                        Else
                            IdForma = 1
                        End If
                    End If
                    If Tipo <> "NOTACREDITO" Then
                        If L.StartsWith("NOMENT") Then Cliente.Nombre = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("CALENT") Then Cliente.Direccion = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("NEXENT") Then Cliente.Direccion += " " + L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("COLENT") Then Cliente.Colonia = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("MUNENT") Then Cliente.Municipio = L.Substring(L.IndexOf("   ")).Trim
                        'If L.StartsWith("MUNREC") Then Cliente.Municipio = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("LOCENT") Then Cliente.Ciudad = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("ESTENT") Then Cliente.Estado = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("PAIENT") Then Cliente.Pais = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("CODENT") Then Cliente.CP = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("TELCLI") Then Cliente.Telefono = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("RFCENT") Then Cliente.RFC = L.Substring(L.IndexOf("   ")).Trim
                    Else
                        If L.StartsWith("NOMENT") Or L.StartsWith("NOMREC") Then Cliente.Nombre = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("CALENT") Or L.StartsWith("CALREC") Then Cliente.Direccion = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("NEXENT") Or L.StartsWith("NEXREC") Then Cliente.Direccion += " " + L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("COLENT") Or L.StartsWith("COLREC") Then Cliente.Colonia = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("MUNENT") Then Cliente.Municipio = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("MUNREC") Then Cliente.Municipio2 = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("LOCENT") Or L.StartsWith("LOCREC") Then Cliente.Ciudad = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("ESTENT") Or L.StartsWith("ESTREC") Then Cliente.Estado = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("PAIENT") Then Cliente.Pais = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("CODENT") Or L.StartsWith("CODREC") Then Cliente.CP = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("TELENT") Then Cliente.Telefono = L.Substring(L.IndexOf("   ")).Trim
                        If L.StartsWith("RFCENT") Or L.StartsWith("RFCREC") Then Cliente.RFC = L.Substring(L.IndexOf("   ")).Trim
                    End If
                End If



                If L.StartsWith("NUMCLI") Then Cliente.Clave = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("REFERE") Then RefDocumento = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("EANENT") Then Adicional = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("NUMFOL") Then Folio = CInt(L.Substring(L.IndexOf("   ")).Trim)
                If L.StartsWith("SERFOL") Then Serie = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("FECEXP") Then Fecha = Replace(L.Substring(L.IndexOf("   ")).Trim, "-", "/").Substring(0, 10)
                'If L.StartsWith("FECHOC") Then Fecha = Replace(L.Substring(L.IndexOf("   ")).Trim, "-", "/")
                If L.StartsWith("TIPMON") Then Moneda = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("TIPCAM") Then TipodeCambio = CDbl(L.Substring(L.IndexOf("   ")).Trim)


                If L.StartsWith("TOTIVA") Then IVAGeneral = CDbl(L.Substring(L.IndexOf("   ")).Trim)
                If L.StartsWith("REFFACT") Then RefFacturas = L.Substring(L.IndexOf("  ")).Trim
                If L.StartsWith("AGENTE") Then CodigoVendedor = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("NOMAGE") Then
                    Dim Vendedor As New dbVendedores(IConexion)
                    If Vendedor.BuscaVendedor(L.Substring(L.IndexOf("   ")).Trim) Then
                        idVendedor = Vendedor.ID
                        If CodigoVendedor = "" Then
                            Vendedor.Modificar(Vendedor.ID, L.Substring(L.IndexOf("   ")).Trim, "", "", "", L.Substring(L.IndexOf("   ")).Trim, "", "", "", "", "0", 0, 0)
                        Else
                            Vendedor.Modificar(Vendedor.ID, L.Substring(L.IndexOf("   ")).Trim, "", "", "", CodigoVendedor, "", "", "", "", "0", 0, 0)
                        End If
                    Else
                        If CodigoVendedor = "" Then
                            Vendedor.Guardar(L.Substring(L.IndexOf("   ")).Trim, "", "", "", L.Substring(L.IndexOf("   ")).Trim, "", "", "", "", "0", 0, 0)
                        Else
                            Vendedor.Guardar(L.Substring(L.IndexOf("   ")).Trim, "", "", "", CodigoVendedor, "", "", "", "", "0", 0, 0)
                        End If
                        idVendedor = Vendedor.ID
                    End If
                End If

                If L.StartsWith("MAIL") Then EMail = L.Substring(L.IndexOf("     ")).Trim

                CodigoCliente = Cliente.Clave
                If EncontroCliente = False Then
                    If Cliente.BuscaCliente(Cliente.Clave) Then
                        EncontroCliente = True
                        Cliente.Municipio = ""
                        Cliente.Municipio2 = ""
                    End If
                End If

                If L.StartsWith("MONDET") Then Descuento = CDbl(L.Substring(L.IndexOf("   ")).Trim)
                'If EncontroCliente = False Then


                'End If
            Next
            If EncontroCliente = False Then
                If Cliente.Pais = "" Then Cliente.Pais = "México"
                Cliente.Guardar(Cliente.Nombre, Cliente.Direccion, Cliente.Telefono, EMail, "", Cliente.Clave, Cliente.RFC, "", Cliente.Ciudad, Cliente.CP, Cliente.Estado, Cliente.Pais, "", "", "", "", "", Cliente.NoExterior, Cliente.NoInterior, Cliente.Colonia, Cliente.Municipio + " " + Cliente.Municipio2, Cliente.ReferenciaDomicilio, _
                                    "", "", "", "", "", 0, 0, Cliente.CreditoDias, 0, 0, 0, 1, 1, "", 0, 0, 0, "", "", "", 0, 0, "0", "0", "", 0, "", 0, 1, 0, 0, 0, 1, 1, 0, "", "", "", "", "", "", "G01")
            Else
                If Tipo <> "NOTACREDITO" Then
                    Cliente.Modificar(Cliente.ID, Cliente.Nombre, Cliente.Direccion, Cliente.Telefono, EMail, "", Cliente.Clave, Cliente.RFC, "", Cliente.Ciudad, Cliente.CP, Cliente.Estado, Cliente.Pais, "", "", "", "", "", Cliente.NoExterior, Cliente.NoInterior, Cliente.Colonia, Cliente.Municipio + " " + Cliente.Municipio2, Cliente.ReferenciaDomicilio, _
                                        "", "", "", "", "", 0, 0, Cliente.CreditoDias, 0, 0, 1, 1, "", 0, 0, 0, "", "", "", 0, 0, "0", "0", "", 0, "", 0, 1, 0, 0, 0, 1, 1, 0, "", "", "", "", "", "", "G01")
                End If
                'modificar
            End If
            If Moneda = "USD" Or Moneda = "DOL" Then
                idMoneda = 3
            Else
                idMoneda = 2
            End If
            If TipodeCambio <> 0 Then
                Dim CM2 As New dbMonedasConversiones(MySqlcon)
                CM2.Modificar(1, TipodeCambio)
            End If
            If TipodeCambio = 0 And idMoneda <> 2 Then
                Dim CM As New dbMonedasConversiones(1, MySqlcon)
                TipodeCambio = CM.Cantidad
            End If

            If Tipo = "FACTURA" Then
                Dim TF As New dbFormasdePago(IConexion)

                If IdForma = 1 Then
                    TipoForma = dbFormasdePago.Tipos.Contado
                Else
                    TipoForma = dbFormasdePago.Tipos.Credito
                End If
                If Trim(FormadePAgo) <> "" Then
                    If TF.BuscaForma(FormadePAgo, TipoForma) = False Then
                        Dim ClaveSat As String
                        ClaveSat = FormadePAgo.Trim.Substring(0, 2)
                        If IsNumeric(ClaveSat) = False Then
                            ClaveSat = "1000"
                        End If
                        TF.Guardar(FormadePAgo, TipoForma, CInt(ClaveSat), 0)
                        IdForma = TF.ID
                    Else
                        IdForma = TF.ID
                    End If
                End If

                Dim V As New dbVentas(IConexion)
                IdDocumento = V.DaId(Folio, Serie)
                If V.ID <> 0 Then IdSucursal = V.IdSucursal
                If IdDocumento = 0 Then
                    Return ProcesaFactura()
                Else
                    Estado = V.Estado
                    MensajeError += "Factura ya procesada."
                    If V.EsElectronica > 1 Then
                        'timbrar
                        CadenaOriginaliVentas(Estados.Guardada, V.ID)
                    Else
                        CadenaOriginalVentas(Estados.Guardada, V.ID)
                        'cfd
                    End If
                    Return False
                End If
                'Else
                'Return False
            End If

            If Tipo = "NOTACREDITO" Then
                Dim NC As New dbNotasDeCredito(IConexion)
                IdDocumento = NC.DaId(Folio, Serie)
                If NC.ID <> 0 Then IdSucursal = NC.IdSucursal
                If IdDocumento = 0 Then
                    Return ProcesaNotadeCredito()
                Else
                    Estado = NC.Estado
                    MensajeError += "Nota ya procesada."
                    If NC.EsElectronica > 1 Then
                        'timbrar
                        CadenaOriginaliNC(Estados.Guardada, NC.ID)
                    Else
                        CadenaOriginalNC(Estados.Guardada, NC.ID)
                        'cfd
                    End If
                    Return False
                End If
                'Else
                'Return False
            End If

            If Tipo = "DEVOLUCION" Then
                'Dim TF As New dbFormasdePago(IConexion)
                'Dim TipoForma As Byte
                'If IdForma = 1 Then
                IdForma = 1
                'Else
                'TipoForma = dbFormasdePago.Tipos.Credito
                'End If
                'If Trim(FormadePAgo) <> "" Then
                '    If TF.BuscaForma(FormadePAgo, TipoForma) = False Then
                '        TF.Guardar(FormadePAgo, TipoForma)
                '        IdForma = TF.ID
                '    Else
                '        IdForma = TF.ID
                '    End If
                'End If

                Dim D As New dbDevoluciones(IConexion)
                IdDocumento = D.DaId(Folio, Serie)
                If D.ID <> 0 Then IdSucursal = D.IdSucursal
                If IdDocumento = 0 Then
                    Return ProcesaDevolucion()
                Else
                    Estado = D.Estado
                    MensajeError += "Devolución ya procesada."
                    If D.EsElectronica > 1 Then
                        'timbrar
                        CadenaOriginalDevoluciones(Estados.Guardada, D.ID)
                        'Else
                        '   CadenaOriginalVentas(Estados.Guardada, V.ID)
                        'cfd
                    End If
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            MensajeError += ex.Message
            Return False
        End Try


    End Function
    Public Function ProcesaCancelacion(ByVal pTipo As String) As Boolean
        Try
            Select Case pTipo
                Case "CANCELARFACTURA"
                    Folio = CInt(Replace(Lineas(0), "C", ""))
                    Serie = Lineas(1)
                    Dim V As New dbVentas(IConexion)
                    IdDocumento = V.DaId(Folio, Serie)
                    V = New dbVentas(IdDocumento, IConexion, "0")
                    Estado = V.Estado
                    IdSucursal = V.IdSucursal
                    If IdDocumento <> 0 Then
                        If V.Estado = Estados.Cancelada Then
                            MensajeError += "Factura ya cancelada."
                            Return False
                        End If
                        If V.EsElectronica = 2 And GlobalConector = 1 And GlobalPacCFDI = 1 Then
                            'Dim V As New dbVentas(idVenta, MySqlcon)
                            V.DaDatosTimbrado(IdDocumento)
                            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                            If V.CancelarTimbrado(S.RFC, V.uuid) = 1 Then
                                'Modificar(Estados.Cancelada)
                                V.ModificaEstado(IdDocumento, Estados.Cancelada)
                                If CadenaOriginaliVentas(Estados.Cancelada, V.ID) = False Then Return False
                            Else
                                MensajeError += V.MensajeError
                                Return False
                            End If
                        Else
                            If V.EsElectronica = 2 And GlobalPacCFDI = 2 Then
                                V.DaDatosTimbrado(IdDocumento)
                                Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                                Dim op As New dbOpciones(MySqlcon)
                                If V.CancelarTimbrado2(S.RFC, V.uuid, op._ApiKey) = 1 Then
                                    'Modificar(Estados.Cancelada)
                                    V.ModificaEstado(IdDocumento, Estados.Cancelada)
                                    If CadenaOriginaliVentas(Estados.Cancelada, V.ID) = False Then Return False
                                Else
                                    MensajeError += V.MensajeError
                                    Return False
                                End If
                            Else
                                If V.EsElectronica > 1 Then
                                    'timbrar
                                    If CadenaOriginaliVentas(Estados.Cancelada, V.ID) = False Then Return False
                                Else
                                    If CadenaOriginalVentas(Estados.Cancelada, V.ID) = False Then Return False
                                    'cfd
                                End If
                                V.ModificaEstado(IdDocumento, Estados.Cancelada)
                            End If

                            'Modificar(Estados.Cancelada)
                        End If
                        Return True
                    Else
                        MensajeError += "No existe la factura que se intentó cancelar."
                        Return False
                    End If
                Case "CANCELARNOTACREDITO"
                    Folio = CInt(Replace(Lineas(0), "C", ""))
                    Serie = Lineas(1)
                    Dim V As New dbNotasDeCredito(IConexion)
                    IdDocumento = V.DaId(Folio, Serie)
                    V = New dbNotasDeCredito(IdDocumento, IConexion)
                    Estado = V.Estado
                    IdSucursal = V.IdSucursal
                    If IdDocumento <> 0 Then
                        If V.Estado = Estados.Cancelada Then
                            MensajeError += "Nota de crédito ya cancelada."
                            Return False
                        End If
                        If V.EsElectronica = 2 And GlobalConector = 1 And GlobalPacCFDI = 1 Then
                            'Dim V As New dbVentas(idVenta, MySqlcon)
                            V.DaDatosTimbrado(IdDocumento)
                            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                            If V.CancelarTimbrado(S.RFC, V.uuid) = 1 Then
                                'Modificar(Estados.Cancelada)
                                V.ModificaEstado(IdDocumento, Estados.Cancelada)
                                If CadenaOriginaliNC(Estados.Cancelada, V.ID) = False Then Return False
                            Else
                                MensajeError += V.MensajeError
                                Return False
                            End If
                        Else
                            If V.EsElectronica = 2 And GlobalPacCFDI = 2 Then
                                V.DaDatosTimbrado(IdDocumento)
                                Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                                Dim op As New dbOpciones(MySqlcon)
                                If V.CancelarTimbrado2(S.RFC, V.uuid, op._ApiKey) = 1 Then
                                    'Modificar(Estados.Cancelada)
                                    V.ModificaEstado(IdDocumento, Estados.Cancelada)
                                    If CadenaOriginaliNC(Estados.Cancelada, V.ID) = False Then Return False
                                Else
                                    MensajeError += V.MensajeError
                                    Return False
                                End If
                            Else
                                If V.EsElectronica > 1 Then
                                    'timbrar
                                    If CadenaOriginaliNC(Estados.Cancelada, V.ID) = False Then Return False
                                Else
                                    If CadenaOriginalNC(Estados.Cancelada, V.ID) = False Then Return False
                                    'cfd
                                End If
                                V.ModificaEstado(IdDocumento, Estados.Cancelada)
                                'Modificar(Estados.Cancelada)
                            End If
                        End If
                        Return True
                    Else
                        MensajeError += "No existe la nota de crédito que se intentó cancelar."
                        Return False
                    End If
                Case "CANCELARDEVOLUCION"
                    Folio = CInt(Replace(Lineas(0), "C", ""))
                    Serie = Lineas(1)
                    Dim V As New dbDevoluciones(IConexion)
                    IdDocumento = V.DaId(Folio, Serie)
                    V = New dbDevoluciones(IdDocumento, IConexion)
                    Estado = V.Estado
                    IdSucursal = V.IdSucursal
                    If IdDocumento <> 0 Then
                        If V.Estado = Estados.Cancelada Then
                            MensajeError += "Devolución ya cancelada."
                            Return False
                        End If
                        If V.EsElectronica = 2 And GlobalConector = 1 And GlobalPacCFDI = 1 Then
                            'Dim V As New dbVentas(idVenta, MySqlcon)
                            V.DaDatosTimbrado(IdDocumento)
                            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                            If V.CancelarTimbrado(S.RFC, V.uuid) = 1 Then
                                'Modificar(Estados.Cancelada)
                                V.ModificaEstado(IdDocumento, Estados.Cancelada)
                                If CadenaOriginalDevoluciones(Estados.Cancelada, V.ID) = False Then Return False
                            Else
                                MensajeError += V.MensajeError
                                Return False
                            End If
                        Else
                            If V.EsElectronica = 2 And GlobalPacCFDI = 2 Then
                                V.DaDatosTimbrado(IdDocumento)
                                Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
                                Dim op As New dbOpciones(MySqlcon)
                                If V.CancelarTimbrado2(S.RFC, V.uuid, op._ApiKey) = 1 Then
                                    'Modificar(Estados.Cancelada)
                                    V.ModificaEstado(IdDocumento, Estados.Cancelada)
                                    If CadenaOriginalDevoluciones(Estados.Cancelada, V.ID) = False Then Return False
                                Else
                                    MensajeError += V.MensajeError
                                    Return False
                                End If
                            Else
                                If V.EsElectronica > 1 Then
                                    'timbrar
                                    If CadenaOriginalDevoluciones(Estados.Cancelada, V.ID) = False Then Return False
                                    'Else
                                    '   If CadenaOriginalVentas(Estados.Cancelada, V.ID) = False Then Return False
                                    'cfd
                                End If
                                V.ModificaEstado(IdDocumento, Estados.Cancelada)
                            End If

                            'Modificar(Estados.Cancelada)
                        End If
                        Return True
                    Else
                        MensajeError += "No existe la devolución que se intentó cancelar."
                        Return False
                    End If
            End Select
            'For Each L As String In Lineas
            '    MsgBox(L)
            'Next
        Catch ex As Exception
            MensajeError += ex.Message
            Return False
        End Try
    End Function
    Private Function ProcesaFactura() As Boolean
        Dim Estado1 As Byte = 0
        Dim Factura As New dbVentas(IConexion)
        Dim FacturaDetalles As New dbVentasInventario(IConexion)
        Dim Medidas As New dbTiposCantidades(IConexion)
        Dim Inventario As New dbInventario(IConexion)
        Dim Aduana As New dbventasaduana(IConexion)
        Dim Unidad As String = ""
        Dim Clave As String = ""
        Dim NuevoConcepto As Boolean = False
        If GlobalTipoFacturacion > 1 Then
            If ChecaTimbres() = True Then
                Return False
            End If
        End If

        If Serie = SerieB And IdSucursalb <> 0 Then
            IdSucursal = IdSucursalb
        End If
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdSucursal, dbSucursalesFolios.TipoDocumentos.Factura, GlobalTipoFacturacion)
        If Folio > Sf.FolioFinal Then
            MensajeError += "Se ha alcanzado el límite de folios."
            Return False
        End If
        Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)

        If ChecaCertificado(Sf.IdCertificado) Then
            Return False
        End If
        Factura.Guardar(Cliente.ID, Fecha, Folio, 0, 0, Serie, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, GlobalTipoFacturacion, IdSucursal, IdForma, TipodeCambio, idMoneda, Cliente.ISR, Cliente.IvaRetenido, idVendedor, Descuento, 0, 0, "G01", "", "")
        If LugarExp <> "" And NumeroExp <> "" And CalleExp <> "" Then
            Comm.CommandText = "select count(id) from tblventasexpedicion"
            Dim HayExp As Integer
            HayExp = Comm.ExecuteScalar
            If HayExp <= 0 Then
                Comm.CommandText = "insert into tblventasexpedicion(lugarexp,numexp,calexp,documento,iddocumento) values('" + Replace(LugarExp, "'", "''") + "','" + Replace(NumeroExp, "'", "''") + "','" + Replace(CalleExp, "'", "''") + "',0," + Factura.ID.ToString + ")"
                Comm.ExecuteNonQuery()
            Else
                Comm.CommandText = "update tblventasexpedicion set lugarexp='" + Replace(LugarExp, "'", "''") + "',numexp='" + Replace(NumeroExp, "'", "''") + "',calexp='" + Replace(CalleExp, "'", "''") + "' where documento=0 and iddocumento=" + Factura.ID.ToString
                Comm.ExecuteNonQuery()
            End If
        End If
        Try
            For Each L As String In Lineas
                If Estado1 = 1 Then
                    If L.StartsWith("UNIDAD") Then
                        Unidad = L.Substring(L.IndexOf("   ")).Trim
                        If Medidas.BuscaUnidad(Unidad) = False Then
                            Medidas.Guardar(Unidad, Unidad, 0, 0)
                            'Inventario.Guardar(Unidad, 0, Medidas.ID, 0, Medidas.ID, 1, "", Unidad, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0)
                        End If

                    End If
                    If L.StartsWith("ADART5") Then
                        Inventario.Ubicacion = L.Substring(L.IndexOf("   ")).Trim
                        'If NuevoConcepto = True Then
                        Inventario.Modificar(Inventario.ID, Clave, 0, Medidas.ID, 0, Medidas.ID, 1, "", Clave, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0, Inventario.Ubicacion, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, 0, 0, "", "06 PIEZA", "", "", 0, 0, 0, False)
                        'End If
                    End If

                    If L.StartsWith("ESTILV") Then
                        Clave = L.Substring(L.IndexOf("   ")).Trim
                        If Inventario.BuscaArticulo(Clave, 0) = False Then
                            If Medidas.ID <= 0 Then Medidas.ID = 1
                            Inventario.Guardar(Clave, 0, Medidas.ID, 0, Medidas.ID, 1, "", Clave, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0, Inventario.Ubicacion, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, 0, "", "06 PIEZA", "", "", 0, 0, 0, False)
                            NuevoConcepto = True
                        Else
                            'Inventario.BuscaArticulo(Unidad, 0)
                            NuevoConcepto = False
                        End If
                    End If
                    If L.StartsWith("TDECON1") Then FacturaDetalles.Descuento = CDbl(L.Substring(L.IndexOf("  ")).Trim)
                    If FacturaDetalles.Descuento <> 0 Then Descuento = 0
                    'FacturaDetalles.Descuento = 0
                    If L.StartsWith("IMPBRU") Then FacturaDetalles.Precio = CDbl(L.Substring(L.IndexOf("   ")).Trim)

                    If L.StartsWith("TASIPE") Then FacturaDetalles.Iva = CDbl(L.Substring(L.IndexOf("   ")).Trim)
                    'IVARET_P
                    If L.StartsWith("IVARET_P") Then FacturaDetalles.ivaRetencion = CDbl(L.Substring(L.IndexOf(" ")).Trim)
                    If L.StartsWith("DESCRI") Then
                        If L.StartsWith("DESCRI2") Or L.StartsWith("DESCRI3") Or L.StartsWith("DESCRI4") Then
                            FacturaDetalles.Descripcion += " " + L.Substring(L.IndexOf("  ")).Trim
                        Else
                            FacturaDetalles.Descripcion = L.Substring(L.IndexOf("   ")).Trim
                        End If

                    End If

                    If L.StartsWith("COMEN") Then
                        If L.Contains("|") Then
                            FacturaDetalles.Extra += Replace(L.Substring(L.IndexOf("  ")).Trim, "|", "")
                        Else
                            If L.Substring(L.IndexOf("  ")).Trim.StartsWith(".") Then
                                FacturaDetalles.Descripcion += vbCrLf
                            Else
                                FacturaDetalles.Descripcion += vbCrLf + L.Substring(L.IndexOf("  ")).Trim
                            End If

                        End If
                    End If

                End If
                If L.StartsWith("NUMADU") Then Aduana.Numero = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("FECADU") Then Aduana.Fecha = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("ADUANA") Then Aduana.Aduana = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("CANTID") Then
                    If Estado1 = 1 Then
                        If Medidas.ID <= 0 Then Medidas.ID = 1
                        If FacturaDetalles.Descuento <> 0 Then
                            FacturaDetalles.Precio = FacturaDetalles.Precio - (FacturaDetalles.Precio * FacturaDetalles.Descuento / 100)
                        End If
                        FacturaDetalles.Guardar(Factura.ID, Inventario.ID, FacturaDetalles.Cantidad, FacturaDetalles.Precio, idMoneda, Replace(FacturaDetalles.Descripcion, "|", ""), 1, FacturaDetalles.Iva, FacturaDetalles.Descuento, 1, 0, 0, FacturaDetalles.Cantidad, Medidas.ID, 0, FacturaDetalles.ivaRetencion, "", 0, "")
                        FacturaDetalles.Extra = ""
                        FacturaDetalles.ivaRetencion = 0
                        If Aduana.Numero <> "" And Aduana.Fecha <> "" And Aduana.Aduana <> "" Then
                            Aduana.Guardar(FacturaDetalles.ID, Aduana.Numero, Aduana.Fecha, Aduana.Aduana)
                            Aduana.Reset()
                        End If
                        Estado1 = 0
                    End If
                    FacturaDetalles.Cantidad = CDbl(L.Substring(L.IndexOf("   ")).Trim)
                    Estado1 = 1
                End If
            Next
            If Estado1 = 1 Then
                If Medidas.ID <= 0 Then Medidas.ID = 1
                If FacturaDetalles.Descuento <> 0 Then
                    FacturaDetalles.Precio = FacturaDetalles.Precio - (FacturaDetalles.Precio * FacturaDetalles.Descuento / 100)
                End If
                FacturaDetalles.Guardar(Factura.ID, Inventario.ID, FacturaDetalles.Cantidad, FacturaDetalles.Precio, idMoneda, Replace(FacturaDetalles.Descripcion, "|", ""), 1, FacturaDetalles.Iva, FacturaDetalles.Descuento, 1, 0, 0, FacturaDetalles.Cantidad, Medidas.ID, 0, FacturaDetalles.ivaRetencion, "", 0, "")
                FacturaDetalles.Extra = ""
                FacturaDetalles.ivaRetencion = 0
                If Aduana.Numero <> "" And Aduana.Fecha <> "" And Aduana.Aduana <> "" Then
                    Aduana.Guardar(FacturaDetalles.ID, Aduana.Numero, Aduana.Fecha, Aduana.Aduana)
                    Aduana.Reset()
                End If
                Estado1 = 0
            End If
            Factura.Descuento = Descuento
            Factura.Alterno = "0"
            Factura.DaTotal(Factura.ID, idMoneda, "0", "0")
            Factura.Modificar(Factura.ID, Factura.Fecha, Factura.Folio, 0, 0, Factura.Serie, Factura.NoAprobacion, Factura.NoCertificado, Factura.YearAprobacion, Factura.EsElectronica, Estados.Pendiente, IdForma, 0, Factura.TipodeCambio, idMoneda, Factura.Subtototal, Factura.TotalVenta, Factura.IdCliente, idVendedor, "", NoCuenta, Descuento, 0, 0, 1, 1, 0, RefDocumento, Adicional, 0, 0, 0, 0, "0", Factura.Fecha, "G01", "", "")
            Dim MetodosDePago As New dbVentasAddMetodos(MySqlcon)
            MetodosDePago.Guardar(0, IdForma, Factura.TotalVenta, Factura.ID)
            If Factura.EsElectronica > 1 Then
                'timbrar
                If CadenaOriginaliVentas(Estados.Guardada, Factura.ID) = False Then Return False
            Else
                If CadenaOriginalVentas(Estados.Guardada, Factura.ID) = False Then Return False
                'cfd
            End If
            Factura.ModificaEstado(Factura.ID, Estados.Guardada)
            IdDocumento = Factura.ID
            Estado = Estados.Guardada
            Return True
        Catch ex As Exception
            If Factura.Estado = Estados.Inicio Or Factura.Estado = Estados.Pendiente Then Factura.Eliminar(Factura.ID)
            MensajeError += ex.Message
            Estado = Estados.Inicio
            Return False
        End Try


    End Function

    Private Function ProcesaDevolucion() As Boolean
        Dim Estado1 As Byte = 0
        Dim Factura As New dbDevoluciones(IConexion)
        Dim FacturaDetalles As New dbDevolucionesDetalles(IConexion)
        Dim Medidas As New dbTiposCantidades(IConexion)
        Dim Inventario As New dbInventario(IConexion)
        'Dim Aduana As New dbventasaduana(IConexion)
        Dim Unidad As String = ""
        Dim Clave As String = ""
        Dim NuevoConcepto As Boolean = False
        If GlobalTipoFacturacion > 1 Then
            If ChecaTimbres() = True Then
                Return False
            End If
        End If

        If Serie = SerieB And IdSucursalb <> 0 Then
            IdSucursal = IdSucursalb
        End If
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdSucursal, dbSucursalesFolios.TipoDocumentos.Devolucionn, GlobalTipoFacturacion)
        If Folio > Sf.FolioFinal Then
            MensajeError += "Se ha alcanzado el límite de folios."
            Return False
        End If
        Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)

        If ChecaCertificado(Sf.IdCertificado) Then
            Return False
        End If
        Factura.Guardar(Cliente.ID, Fecha, Folio, 0, 0, Serie, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, GlobalTipoFacturacion, IdSucursal, IdForma, TipodeCambio, idMoneda, Cliente.ISR, Cliente.IvaRetenido, 0, 0, RefFacturas)
        If LugarExp <> "" And NumeroExp <> "" And CalleExp <> "" Then
            Comm.CommandText = "select count(id) from tblventasexpedicion"
            Dim HayExp As Integer
            HayExp = Comm.ExecuteScalar
            If HayExp <= 0 Then
                Comm.CommandText = "insert into tblventasexpedicion(lugarexp,numexp,calexp,documento,iddocumento) values('" + Replace(LugarExp, "'", "''") + "','" + Replace(NumeroExp, "'", "''") + "','" + Replace(CalleExp, "'", "''") + "',2," + Factura.ID.ToString + ")"
                Comm.ExecuteNonQuery()
            Else
                Comm.CommandText = "update tblventasexpedicion set lugarexp='" + Replace(LugarExp, "'", "''") + "',numexp='" + Replace(NumeroExp, "'", "''") + "',calexp='" + Replace(CalleExp, "'", "''") + "' where documento=2 and iddocumento=" + Factura.ID.ToString
                Comm.ExecuteNonQuery()
            End If
        End If
        Try
            For Each L As String In Lineas
                If Estado1 = 1 Then
                    If L.StartsWith("UNIDAD") Then
                        Unidad = L.Substring(L.IndexOf("   ")).Trim
                        If Medidas.BuscaUnidad(Unidad) = False Then
                            Medidas.Guardar(Unidad, Unidad, 0, 0)
                            'Inventario.Guardar(Unidad, 0, Medidas.ID, 0, Medidas.ID, 1, "", Unidad, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0)
                        End If

                    End If
                    If L.StartsWith("ADART5") Then
                        Inventario.Ubicacion = L.Substring(L.IndexOf("   ")).Trim
                        If NuevoConcepto = True Then
                            Inventario.Modificar(Inventario.ID, Clave, 0, Medidas.ID, 0, Medidas.ID, 1, "", Clave, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0, Inventario.Ubicacion, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, 0, 0, "", "06 PIEZA", "", "", 0, 0, 0, False)
                        End If
                    End If

                    If L.StartsWith("ESTILV") Then
                        Clave = L.Substring(L.IndexOf("   ")).Trim
                        If Inventario.BuscaArticulo(Clave, 0) = False Then
                            If Medidas.ID <= 0 Then Medidas.ID = 1
                            Inventario.Guardar(Clave, 0, Medidas.ID, 0, Medidas.ID, 1, "", Clave, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0, Inventario.Ubicacion, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, 0, "", "06 PIEZA", "", "", 0, 0, 0, False)
                            NuevoConcepto = True
                        Else
                            'Inventario.BuscaArticulo(Unidad, 0)
                            NuevoConcepto = False
                        End If
                    End If
                    If L.StartsWith("TDECON1") Then FacturaDetalles.Descuento = CDbl(L.Substring(L.IndexOf("  ")).Trim)
                    If FacturaDetalles.Descuento <> 0 Then Descuento = 0
                    'FacturaDetalles.Descuento = 0
                    If L.StartsWith("IMPBRU") Then FacturaDetalles.Precio = CDbl(L.Substring(L.IndexOf("   ")).Trim)

                    If L.StartsWith("TASIPE") Then FacturaDetalles.Iva = CDbl(L.Substring(L.IndexOf("   ")).Trim)

                    If L.StartsWith("DESCRI") Then
                        If L.StartsWith("DESCRI2") Or L.StartsWith("DESCRI3") Or L.StartsWith("DESCRI4") Then
                            FacturaDetalles.Descripcion += " " + L.Substring(L.IndexOf("  ")).Trim
                        Else
                            FacturaDetalles.Descripcion = L.Substring(L.IndexOf("   ")).Trim
                        End If
                    End If

                    If L.StartsWith("COMEN") Then
                        If L.Contains("|") Then
                            FacturaDetalles.Extra += Replace(L.Substring(L.IndexOf("  ")).Trim, "|", "")
                        Else
                            If L.Substring(L.IndexOf("  ")).Trim.StartsWith(".") Then
                                FacturaDetalles.Descripcion += vbCrLf
                            Else
                                FacturaDetalles.Descripcion += vbCrLf + L.Substring(L.IndexOf("  ")).Trim
                            End If
                        End If
                    End If

                End If
                'If L.StartsWith("NUMADU") Then Aduana.Numero = L.Substring(L.IndexOf("   ")).Trim
                'If L.StartsWith("FECADU") Then Aduana.Fecha = L.Substring(L.IndexOf("   ")).Trim
                'If L.StartsWith("ADUANA") Then Aduana.Aduana = L.Substring(L.IndexOf("   ")).Trim
                If L.StartsWith("CANTID") Then
                    If Estado1 = 1 Then
                        If Medidas.ID <= 0 Then Medidas.ID = 1
                        'If FacturaDetalles.Descuento <> 0 Then
                        'FacturaDetalles.Precio = FacturaDetalles.Precio - (FacturaDetalles.Precio * FacturaDetalles.Descuento / 100)
                        'End If
                        FacturaDetalles.Guardar(Factura.ID, Inventario.ID, FacturaDetalles.Cantidad, FacturaDetalles.Precio, idMoneda, Replace(FacturaDetalles.Descripcion, "|", ""), 1, FacturaDetalles.Iva, FacturaDetalles.Descuento, 1, 0, FacturaDetalles.Cantidad, Medidas.ID)
                        FacturaDetalles.Extra = ""
                        'If Aduana.Numero <> "" And Aduana.Fecha <> "" And Aduana.Aduana <> "" Then
                        ' Aduana.Guardar(FacturaDetalles.ID, Aduana.Numero, Aduana.Fecha, Aduana.Aduana)
                        ' Aduana.Reset()
                        'End If
                        Estado1 = 0
                    End If
                    FacturaDetalles.Cantidad = CDbl(L.Substring(L.IndexOf("   ")).Trim)
                    Estado1 = 1
                End If
            Next
            If Estado1 = 1 Then
                If Medidas.ID <= 0 Then Medidas.ID = 1
                'If FacturaDetalles.Descuento <> 0 Then
                'FacturaDetalles.Precio = FacturaDetalles.Precio - (FacturaDetalles.Precio * FacturaDetalles.Descuento / 100)
                'End If
                FacturaDetalles.Guardar(Factura.ID, Inventario.ID, FacturaDetalles.Cantidad, FacturaDetalles.Precio, idMoneda, Replace(FacturaDetalles.Descripcion, "|", ""), 1, FacturaDetalles.Iva, FacturaDetalles.Descuento, 1, 0, FacturaDetalles.Cantidad, Medidas.ID)
                FacturaDetalles.Extra = ""
                'If Aduana.Numero <> "" And Aduana.Fecha <> "" And Aduana.Aduana <> "" Then
                ' Aduana.Guardar(FacturaDetalles.ID, Aduana.Numero, Aduana.Fecha, Aduana.Aduana)
                ' Aduana.Reset()
                'End If
                Estado1 = 0
            End If
            'Factura.Descuento = Descuento
            Factura.DaTotal(Factura.ID, idMoneda)
            Factura.Modificar(Factura.ID, Factura.Fecha, Factura.Folio, 0, 0, Factura.Serie, Factura.NoAprobacion, Factura.NoCertificado, Factura.YearAprobacion, Factura.EsElectronica, Estados.Pendiente, IdForma, 0, Factura.TipodeCambio, idMoneda, Factura.Subtototal, Factura.TotalVenta, Factura.IdCliente, "")
            If Factura.EsElectronica > 1 Then
                'timbrar
                If CadenaOriginalDevoluciones(Estados.Guardada, Factura.ID) = False Then Return False
                'Else
                '   If CadenaOriginalVentas(Estados.Guardada, Factura.ID) = False Then Return False
                'cfd
            End If
            Factura.ModificaEstado(Factura.ID, Estados.Guardada)
            IdDocumento = Factura.ID
            Estado = Estados.Guardada
            Return True
        Catch ex As Exception
            If Factura.Estado = Estados.Inicio Or Factura.Estado = Estados.Pendiente Then Factura.Eliminar(Factura.ID)
            MensajeError += ex.Message
            Estado = Estados.Inicio
            Return False
        End Try


    End Function

    Private Function ProcesaNotadeCredito() As Boolean
        Dim Estado1 As Byte = 0
        Dim Factura As New dbNotasDeCredito(IConexion)
        Dim FacturaDetalles As New dbNotasDeCreditoDetalles(IConexion)
        'Dim Medidas As New dbTiposCantidades(IConexion)
        'Dim Inventario As New dbInventario(IConexion)
        Dim Unidad As String = ""
        Dim Clave As String = ""
        Dim NuevoConcepto As Boolean = False
        If GlobalTipoFacturacion > 1 Then
            If ChecaTimbres() = True Then
                Return False
            End If
        End If
        If Serie = SerieB And IdSucursalb <> 0 Then
            IdSucursal = IdSucursalb
        End If
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdSucursal, dbSucursalesFolios.TipoDocumentos.NotadeCredito, GlobalTipoFacturacion)
        If Folio > Sf.FolioFinal Then
            MensajeError += "Se ha alcanzado el límite de folios."
            Return False
        End If
        Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)

        If ChecaCertificado(Sf.IdCertificado) Then
            Return False
        End If
        Factura.Guardar(Cliente.ID, Fecha, Folio, 0, 0, IdSucursal, Serie, TipodeCambio, Sf.NoAprobacion, Sf.YearAprobacion, Sc.NoSerie, GlobalTipoFacturacion, idMoneda, 1, 0, 0)
        If LugarExp <> "" Then
            Comm.CommandText = "select count(id) from tblventasexpedicion where documento=1 and iddocumento=" + Factura.ID.ToString
            Dim HayExp As Integer
            HayExp = Comm.ExecuteScalar
            If HayExp <= 0 Then
                Comm.CommandText = "insert into tblventasexpedicion(lugarexp,numexp,calexp,documento,iddocumento) values('" + Replace(LugarExp, "'", "''") + "','" + Replace(NumeroExp, "'", "''") + "','" + Replace(CalleExp, "'", "''") + "',1," + Factura.ID.ToString + ")"
                Comm.ExecuteNonQuery()
            Else
                Comm.CommandText = "update tblventasexpedicion set lugarexp='" + Replace(LugarExp, "'", "''") + "',numexp='" + Replace(NumeroExp, "'", "''") + "',calexp='" + Replace(CalleExp, "'", "''") + "' where documento=1 and iddocumento=" + Factura.ID.ToString
                Comm.ExecuteNonQuery()
            End If
        End If
        Try
            For Each L As String In Lineas
                If Estado1 = 1 Then
                    'If L.StartsWith("UNIDAD") Then
                    '    Unidad = L.Substring(L.IndexOf("   ")).Trim
                    '    If Medidas.BuscaUnidad(Unidad) = False Then
                    '        Medidas.Guardar(Unidad, Unidad)
                    '        'Inventario.Guardar(Unidad, 0, Medidas.ID, 0, Medidas.ID, 1, "", Unidad, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0)
                    '    End If

                    'End If
                    'If L.StartsWith("ESTILV") Then
                    '    Clave = L.Substring(L.IndexOf("   ")).Trim
                    '    If Inventario.BuscaArticulo(Clave, 0) = False Then
                    '        If Medidas.ID <= 0 Then Medidas.ID = 1
                    '        Inventario.Guardar(Clave, 0, Medidas.ID, 0, Medidas.ID, 1, "", Clave, 0, 0, 2, "", 1, 1, 0, 0, 16, 0, "", "", 0)
                    '        NuevoConcepto = True
                    '    Else
                    '        'Inventario.BuscaArticulo(Unidad, 0)
                    '        NuevoConcepto = False
                    '    End If

                    'End If
                    If L.StartsWith("IMPBRU") Then FacturaDetalles.Precio = CDbl(L.Substring(L.IndexOf("   ")).Trim)
                    'If L.StartsWith("TASIPE") Then FacturaDetalles.Iva = CDbl(L.Substring(L.IndexOf("   ")).Trim)


                    If L.StartsWith("DESCRI") Then
                        If L.StartsWith("DESCRI2") Or L.StartsWith("DESCRI3") Or L.StartsWith("DESCRI4") Then
                            FacturaDetalles.Descripcion += " " + L.Substring(L.IndexOf("  ")).Trim
                        Else
                            FacturaDetalles.Descripcion = L.Substring(L.IndexOf("   ")).Trim
                        End If
                        If RefFacturas <> "" Then
                            FacturaDetalles.Descripcion += vbCrLf + "APLICADO(A) A LA(S) FACTURA(S): " + RefFacturas
                            RefFacturas = ""
                        End If

                    End If

                    If L.StartsWith("COMEN") Then
                        If L.StartsWith("|") Then
                            FacturaDetalles.Extra += Replace(L.Substring(L.IndexOf("  ")).Trim, "|", "")
                        Else
                            If L.Substring(L.IndexOf("  ")).Trim.StartsWith(".") Then
                                FacturaDetalles.Descripcion += vbCrLf
                            Else
                                FacturaDetalles.Descripcion += vbCrLf + L.Substring(L.IndexOf("  ")).Trim
                            End If

                        End If
                    End If

                End If

                If L.StartsWith("CANTID") Then
                    If Estado1 = 1 Then
                        'If Medidas.ID <= 0 Then Medidas.ID = 1
                        If Version <> "0" Then
                            FacturaDetalles.Precio = FacturaDetalles.Precio * (1 + (IVAGeneral / 100))
                        End If
                        FacturaDetalles.Guardar(Factura.ID, 0, FacturaDetalles.Cantidad, FacturaDetalles.Precio, idMoneda, Replace(FacturaDetalles.Descripcion, "|", ""), IVAGeneral, 0, 0)
                        FacturaDetalles.Extra = ""
                        Estado1 = 0
                    End If
                    FacturaDetalles.Cantidad = CDbl(L.Substring(L.IndexOf("   ")).Trim)
                    Estado1 = 1
                End If
            Next
            If Estado1 = 1 Then
                'If Medidas.ID <= 0 Then Medidas.ID = 1
                If Version <> "0" Then
                    FacturaDetalles.Precio = FacturaDetalles.Precio * (1 + (IVAGeneral / 100))
                End If
                FacturaDetalles.Guardar(Factura.ID, 0, FacturaDetalles.Cantidad, FacturaDetalles.Precio, idMoneda, Replace(FacturaDetalles.Descripcion, "|", ""), IVAGeneral, 0, 0)
                FacturaDetalles.Extra = ""
                Estado1 = 0
            End If
            'Factura.Descuento = Descuento
            Factura.DaTotal(Factura.ID, idMoneda)
            Factura.Modificar(Factura.ID, Factura.Fecha, Factura.Folio, 0, 0, Estados.Guardada, Factura.Subtotal, Factura.TotalNota, Factura.IdCliente, Factura.Serie, Factura.TipodeCambio, Factura.NoAprobacion, Factura.YearAprobacion, Factura.NoCertificado, Factura.IdMoneda, 1, Factura.EsElectronica, "", 0, 0)

            If Factura.EsElectronica > 1 Then
                'timbrar
                If CadenaOriginaliNC(Estados.Guardada, Factura.ID) = False Then Return False
            Else
                If CadenaOriginalNC(Estados.Guardada, Factura.ID) = False Then Return False
                'cfd
            End If
            Factura.ModificaEstado(Factura.ID, Estados.Guardada)
            IdDocumento = Factura.ID
            Estado = Estados.Guardada
            Return True
        Catch ex As Exception
            If Factura.Estado = Estados.Inicio Or Factura.Estado = Estados.Pendiente Then Factura.Eliminar(Factura.ID)
            MensajeError += ex.Message
            Estado = Estados.Inicio
            Return False
        End Try


    End Function
    Private Function ChecaTimbres() As Boolean
        Dim TTimbres As Integer
        TTimbres = CuentaTimbres()
        Dim Ops As New dbOpciones(MySqlcon)
        If Ops.FechaVen <= Format(Date.Now, "yyyy/MM/dd") Then
            MensajeError += "Los timbres han caducado."
            Return True
        Else
            If TTimbres >= Ops.Timbres Then
                MensajeError += "Los timbres se han terminado."
                Return True
            Else
                If Format(Date.Now, "yyyy/MM/dd") > DateAdd(DateInterval.Day, Ops.AvisoDias * -1, CDate(Ops.FechaVen)) Then
                    MensajeError += "Los timbres estan por caducar."
                End If
                If Ops.Timbres - TTimbres <= Ops.AvisoTimbres Then
                    MensajeError += "Los timbres estan por terminarse."
                End If
                Return False
            End If
        End If
    End Function
    'Private Function ChecaFolios(ByVal pDocumento As Integer, ByVal pFolio As Integer, ByVal pidSucursal As Integer, ByVal pEselectronica As Byte) As Boolean
    '    Dim Sf As New dbSucursalesFolios(MySqlcon)
    '    Sf.BuscaFolios(pidSucursal, pDocumento, pEselectronica)
    '    If pFolio > Sf.FolioFinal Then
    '        MensajeError += "Se ha alcanzado el límite de folios."
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    Private Function ChecaCertificado(ByVal pIdCertificado As Integer) As Boolean
        Dim SC As New dbSucursalesCertificados(pIdCertificado, MySqlcon)
        If SC.FechaVencimiento <= Format(Date.Now, "yyyy/MM/dd") Then
            MensajeError += "El certificado del sello digital a caducado."
            Return True
        Else
            If Format(Date.Now, "yyyy/MM/dd") > DateAdd(DateInterval.Day, SC.Aviso * -1, CDate(SC.FechaVencimiento)) Then
                MensajeError += "El certificado del sello digital esta por caducar."
            End If
            Return False
        End If
    End Function

    Private Function CadenaOriginaliVentas(ByVal pEstado As Byte, ByVal Idventa As Integer) As Boolean
        Dim en As New Encriptador
        Dim V As New dbVentas(Idventa, MySqlcon, "0")
        Try
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                Cadena = V.CreaCadenaOriginali32(Idventa, GlobalIdMoneda, "", 0)
            Else
                Cadena = V.CreaCadenaOriginali(Idventa, GlobalIdMoneda)
            End If
            'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
            RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSFACTURA-" + V.Serie + V.Folio.ToString + ".xml"

            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
            Dim strXML As String
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                strXML = V.CreaXMLi32(Idventa, GlobalIdMoneda, Sello, GlobalIdEmpresa, "", 0)
            Else
                strXML = V.CreaXMLi(Idventa, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            End If

            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
            V.NoCertificadoSAT = ""
            V.DaDatosTimbrado(Idventa)



            If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
                If GlobalPacCFDI = 0 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    en.GuardaArchivoTexto("co.txt", Cadena, System.Text.Encoding.UTF8)
                    Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    V.MensajeError = ""
                    Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado, False)
                    MensajeError += V.MensajeError
                    V.NoCertificadoSAT = Timbre.noCertificadoSAT
                    If V.NoCertificadoSAT <> "Error" Then
                        V.uuid = Timbre.UUID
                        V.FechaTimbrado = Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss")
                        V.SelloCFD = Timbre.selloCFD
                        V.NoCertificadoSAT = Timbre.noCertificadoSAT
                        V.SelloSAT = Timbre.selloSAT
                        V.GuardaDatosTimbrado(Idventa, Timbre.UUID, Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss"), Timbre.selloCFD, Timbre.noCertificadoSAT, Timbre.selloSAT)
                        Dim strTimbrado As String
                        strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                        strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                        strTimbrado += "</cfdi:Complemento>" + vbCrLf
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    End If
                End If
                If GlobalPacCFDI = 1 Then
                    Dim Os As New dbOpciones(MySqlcon)
                    If GlobalConector = 0 Then
                        en.GuardaArchivoTexto(RutaXmlTemp, strXML, System.Text.Encoding.UTF8)
                        If IO.File.Exists(RutaXml) Then
                            IO.File.Delete(RutaXml)
                        End If
                    Else
                        If IO.File.Exists(RutaXmlTimbrado) Then
                            IO.File.Delete(RutaXmlTimbrado)
                        End If
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    End If
                    V.MensajeError = ""
                    If V.Timbrar2(Os._UsuarioFacCom, Os._passFacCom, S.RFC, RutaXmlTemp, RutaXml, GlobalConector, False) = 0 Then
                        MensajeError = V.MensajeError
                        V.NoCertificadoSAT = "Error"
                    End If
                    Dim xmldoc As New Xml.XmlDocument
                    'Dim xmldoc2 As New Xml.XmlDocument
                    If GlobalConector = 0 Then
                        xmldoc.Load(RutaXml)
                        If xmldoc.DocumentElement.Name = "ERROR" Then
                            V.NoCertificadoSAT = "Error"
                            MensajeError += xmldoc.InnerText
                        End If
                    Else
                        Dim ChecarXML As String
                        ChecarXML = en.LeeArchivoTexto(RutaXmlTimbrado)
                        If ChecarXML.StartsWith("ERROR") Then
                            MensajeError += ChecarXML
                            V.NoCertificadoSAT = "Error"
                        Else
                            If ChecarXML.StartsWith("<?xml version=""1.0"" encoding=""utf-8""?>" + vbCrLf + "<?xml version=""1.0"" encoding=""utf-8""?>") Then
                                ChecarXML = ChecarXML.Substring(40, ChecarXML.Length - 40)
                                en.GuardaArchivoTexto(RutaXmlTimbrado, ChecarXML, System.Text.Encoding.UTF8)
                            End If
                            xmldoc.Load(RutaXmlTimbrado)
                        End If

                        'xmldoc2.Load(RutaXmlTimbrado)
                    End If
                    If V.NoCertificadoSAT <> "Error" Then
                        '    V.NoCertificadoSAT = "Error"
                        '    MsgError = xmldoc.InnerText
                        'Else
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value

                        V.GuardaDatosTimbrado(Idventa, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                        If IO.File.Exists(RutaXmlTemp) Then
                            IO.File.Delete(RutaXmlTemp)
                        End If
                        If IO.File.Exists(RutaXml) And GlobalConector = 1 Then
                            IO.File.Delete(RutaXml)
                        End If
                    End If
                End If
                If GlobalPacCFDI = 2 Then
                    Dim Os As New dbOpciones(MySqlcon)
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    en.GuardaArchivoTexto("co.txt", Cadena, System.Text.Encoding.UTF8)
                    Dim Timbre As String
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = V.Timbrar3(S.RFC, strXML, "", Os._ApiKey, False, V.Folio, V.Serie)
                    If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                        Dim xmldoc As New Xml.XmlDocument
                        en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                        xmldoc.Load(RutaXmlTimbrado)
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                        V.GuardaDatosTimbrado(Idventa, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                    Else
                        MensajeError += Timbre
                        V.NoCertificadoSAT = "Error"
                    End If
                End If
            Else
                'Crear xml timbrado
                Dim ExisteArchivo As Boolean = False
                If GlobalConector = 0 Then
                    If IO.File.Exists(RutaXml) Then ExisteArchivo = True
                Else
                    If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
                End If


                If pEstado = Estados.Guardada And ExisteArchivo = False Then
                    Dim strTimbrado As String
                    strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                    strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                    strTimbrado += "</cfdi:Complemento>" + vbCrLf
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If
            End If

            If V.NoCertificadoSAT <> "Error" Then
                CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
                Return True
            Else
                MensajeError += "Ha ocurrido un error en el timbrado del la factura, intente mas tarde."
                Dim Se As New dbInventarioSeries(MySqlcon)
                Se.QuitaSeriesAVenta(Idventa)
                If V.Estado = Estados.Guardada Then V.RegresaInventario(Idventa)
                V.Eliminar(Idventa)
                Return False
            End If
        Catch ex As Exception
            MensajeError += ex.Message
            Return False
        End Try


    End Function

    Private Function CadenaOriginalDevoluciones(ByVal pEstado As Byte, ByVal Idventa As Integer) As Boolean
        Dim en As New Encriptador
        Dim V As New dbDevoluciones(Idventa, MySqlcon)
        Try
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                Cadena = V.CreaCadenaOriginali32(Idventa, GlobalIdMoneda)
            Else
                Cadena = V.CreaCadenaOriginali(Idventa, GlobalIdMoneda)
            End If
            'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesXML, False)
            RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.DevolucionesPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSDEVOLUCION-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSDEVOLUCION-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSDEVOLUCION-" + V.Serie + V.Folio.ToString + ".xml"

            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
            Dim strXML As String
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                strXML = V.CreaXMLi32(Idventa, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            Else
                strXML = V.CreaXMLi(Idventa, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            End If

            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
            V.NoCertificadoSAT = ""
            V.DaDatosTimbrado(Idventa)



            If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
                If GlobalPacCFDI = 0 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    V.MensajeError = ""
                    Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado, False)
                    MensajeError += V.MensajeError
                    V.NoCertificadoSAT = Timbre.noCertificadoSAT
                    If V.NoCertificadoSAT <> "Error" Then
                        V.uuid = Timbre.UUID
                        V.FechaTimbrado = Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss")
                        V.SelloCFD = Timbre.selloCFD
                        V.NoCertificadoSAT = Timbre.noCertificadoSAT
                        V.SelloSAT = Timbre.selloSAT
                        V.GuardaDatosTimbrado(Idventa, Timbre.UUID, Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss"), Timbre.selloCFD, Timbre.noCertificadoSAT, Timbre.selloSAT)
                        Dim strTimbrado As String
                        strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                        strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                        strTimbrado += "</cfdi:Complemento>" + vbCrLf
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    End If
                End If
                If GlobalPacCFDI = 1 Then
                    Dim Os As New dbOpciones(MySqlcon)
                    If GlobalConector = 0 Then
                        en.GuardaArchivoTexto(RutaXmlTemp, strXML, System.Text.Encoding.UTF8)
                        If IO.File.Exists(RutaXml) Then
                            IO.File.Delete(RutaXml)
                        End If
                    Else
                        If IO.File.Exists(RutaXmlTimbrado) Then
                            IO.File.Delete(RutaXmlTimbrado)
                        End If
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    End If
                    V.MensajeError = ""
                    If V.Timbrar2(Os._UsuarioFacCom, Os._passFacCom, S.RFC, RutaXmlTemp, RutaXml, GlobalConector, False) = 0 Then
                        MensajeError = V.MensajeError
                        V.NoCertificadoSAT = "Error"
                    End If
                    Dim xmldoc As New Xml.XmlDocument
                    'Dim xmldoc2 As New Xml.XmlDocument
                    If GlobalConector = 0 Then
                        xmldoc.Load(RutaXml)
                        If xmldoc.DocumentElement.Name = "ERROR" Then
                            V.NoCertificadoSAT = "Error"
                            MensajeError += xmldoc.InnerText
                        End If
                    Else
                        Dim ChecarXML As String
                        ChecarXML = en.LeeArchivoTexto(RutaXmlTimbrado)
                        If ChecarXML.StartsWith("ERROR") Then
                            MensajeError += ChecarXML
                            V.NoCertificadoSAT = "Error"
                        Else
                            If ChecarXML.StartsWith("<?xml version=""1.0"" encoding=""utf-8""?>" + vbCrLf + "<?xml version=""1.0"" encoding=""utf-8""?>") Then
                                ChecarXML = ChecarXML.Substring(40, ChecarXML.Length - 40)
                                en.GuardaArchivoTexto(RutaXmlTimbrado, ChecarXML, System.Text.Encoding.UTF8)
                            End If
                            xmldoc.Load(RutaXmlTimbrado)
                        End If

                        'xmldoc2.Load(RutaXmlTimbrado)
                    End If
                    If V.NoCertificadoSAT <> "Error" Then
                        '    V.NoCertificadoSAT = "Error"
                        '    MsgError = xmldoc.InnerText
                        'Else
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value

                        V.GuardaDatosTimbrado(Idventa, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                        If IO.File.Exists(RutaXmlTemp) Then
                            IO.File.Delete(RutaXmlTemp)
                        End If
                        If IO.File.Exists(RutaXml) And GlobalConector = 1 Then
                            IO.File.Delete(RutaXml)
                        End If
                    End If
                End If
                If GlobalPacCFDI = 2 Then
                    Dim Os As New dbOpciones(MySqlcon)
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    Dim Timbre As String
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = V.Timbrar3(S.RFC, strXML, "", Os._ApiKey, False, V.Serie, V.Folio)
                    If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                        Dim xmldoc As New Xml.XmlDocument
                        en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                        xmldoc.Load(RutaXmlTimbrado)
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                        V.GuardaDatosTimbrado(Idventa, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                    Else
                        MensajeError += Timbre
                        V.NoCertificadoSAT = "Error"
                    End If
                End If
            Else
                'Crear xml timbrado
                Dim ExisteArchivo As Boolean = False
                If GlobalConector = 0 Then
                    If IO.File.Exists(RutaXml) Then ExisteArchivo = True
                Else
                    If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
                End If


                If pEstado = Estados.Guardada And ExisteArchivo = False Then
                    Dim strTimbrado As String
                    strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                    strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                    strTimbrado += "</cfdi:Complemento>" + vbCrLf
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If
            End If

            If V.NoCertificadoSAT <> "Error" Then
                CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
                Return True
                
            Else
                MensajeError += "Ha ocurrido un error en el timbrado del la devolución, intente mas tarde."
                
                V.Eliminar(Idventa)
                Return False

                'Error en timbrado
            End If
        Catch ex As Exception
            MensajeError += ex.Message
            Return False
        End Try


    End Function

    Private Function CadenaOriginalVentas(ByVal pEstado As Byte, ByVal idVenta As Integer) As Boolean
        Try
            Dim en As New Encriptador
            Dim V As New dbVentas(idVenta, MySqlcon, "0")
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                Cadena = V.CreaCadenaOriginal22(idVenta, GlobalIdMoneda)
            Else
                Cadena = V.CreaCadenaOriginal(idVenta, GlobalIdMoneda)
            End If

            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaXML, False)
            RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
            Dim Enc As New System.Text.UTF8Encoding
            Dim xmldoc As String
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                xmldoc = V.CreaXML22(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            Else
                xmldoc = V.CreaXML(idVenta, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            End If

            Dim Bytes() As Byte = Enc.GetBytes(xmldoc)


            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
           
            en.GuardaArchivoTexto(RutaXml + "\FAC-" + Trim(V.Serie) + V.Folio.ToString + ".xml", xmldoc, System.Text.Encoding.UTF8)
            Return True
        Catch ex As Exception
            MensajeError += ex.Message
            Return False
        End Try
    End Function

    Private Function CadenaOriginaliNC(ByVal pEstado As Byte, ByVal IdNota As Integer) As Boolean
        Dim en As New Encriptador
        Dim V As New dbNotasDeCredito(IdNota, MySqlcon)
        Try
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                Cadena = V.CreaCadenaOriginali32(IdNota, GlobalIdMoneda)
            Else
                Cadena = V.CreaCadenaOriginali(IdNota, GlobalIdMoneda)
            End If
            'en.GuardaArchivoTexto(Application.StartupPath + "\cadena.txt", Cadena, System.Text.Encoding.UTF8)
            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
            RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaXmlTemp = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXmlTimbrado = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PSSNOTADECREDITO-" + V.Serie + V.Folio.ToString + ".xml"

            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
            Dim strXML As String
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                strXML = V.CreaXMLi32(IdNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            Else
                strXML = V.CreaXMLi(IdNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            End If

            Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
            V.NoCertificadoSAT = ""
            V.DaDatosTimbrado(IdNota)



            If (V.uuid = "**No Timbrado**" Or V.uuid = "") And pEstado = Estados.Guardada Then
                If GlobalPacCFDI = 0 Then
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    en.GuardaArchivoTexto("co.txt", Cadena, System.Text.Encoding.UTF8)
                    Dim Timbre As TimbreFiscal.TimbreFiscalDigital
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    V.MensajeError = ""
                    Timbre = V.Timbrar(S.RFC, V.Cliente.RFC, sa.RutaPFX, sa.PassPFX, strXML, GlobalDireccionTimbrado, False)
                    MensajeError += V.MensajeError
                    V.NoCertificadoSAT = Timbre.noCertificadoSAT
                    If V.NoCertificadoSAT <> "Error" Then
                        V.uuid = Timbre.UUID
                        V.FechaTimbrado = Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss")
                        V.SelloCFD = Timbre.selloCFD
                        V.NoCertificadoSAT = Timbre.noCertificadoSAT
                        V.SelloSAT = Timbre.selloSAT
                        V.GuardaDatosTimbrado(IdNota, Timbre.UUID, Format(Timbre.FechaTimbrado, "yyyy-MM-ddTHH:mm:ss"), Timbre.selloCFD, Timbre.noCertificadoSAT, Timbre.selloSAT)
                        Dim strTimbrado As String
                        strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                        strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                        strTimbrado += "</cfdi:Complemento>" + vbCrLf
                        strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    End If
                End If
                If GlobalPacCFDI = 1 Then
                    Dim Os As New dbOpciones(MySqlcon)
                    If GlobalConector = 0 Then
                        en.GuardaArchivoTexto(RutaXmlTemp, strXML, System.Text.Encoding.UTF8)
                        If IO.File.Exists(RutaXml) Then
                            IO.File.Delete(RutaXml)
                        End If
                    Else
                        If IO.File.Exists(RutaXmlTimbrado) Then
                            IO.File.Delete(RutaXmlTimbrado)
                        End If
                        en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                    End If
                    V.MensajeError = ""
                    If V.Timbrar2(Os._UsuarioFacCom, Os._passFacCom, S.RFC, RutaXmlTemp, RutaXml, GlobalConector, False) = 0 Then
                        MensajeError = V.MensajeError
                        V.NoCertificadoSAT = "Error"
                    End If
                    Dim xmldoc As New Xml.XmlDocument
                    'Dim xmldoc2 As New Xml.XmlDocument
                    If GlobalConector = 0 Then
                        xmldoc.Load(RutaXml)
                        If xmldoc.DocumentElement.Name = "ERROR" Then
                            V.NoCertificadoSAT = "Error"
                            MensajeError += xmldoc.InnerText
                        End If
                    Else
                        Dim ChecarXML As String
                        ChecarXML = en.LeeArchivoTexto(RutaXmlTimbrado)
                        If ChecarXML.StartsWith("ERROR") Then
                            MensajeError += ChecarXML
                            V.NoCertificadoSAT = "Error"
                        Else
                            If ChecarXML.StartsWith("<?xml version=""1.0"" encoding=""utf-8""?>" + vbCrLf + "<?xml version=""1.0"" encoding=""utf-8""?>") Then
                                ChecarXML = ChecarXML.Substring(40, ChecarXML.Length - 40)
                                en.GuardaArchivoTexto(RutaXmlTimbrado, ChecarXML, System.Text.Encoding.UTF8)
                            End If
                            xmldoc.Load(RutaXmlTimbrado)
                        End If

                        'xmldoc2.Load(RutaXmlTimbrado)
                    End If
                    If V.NoCertificadoSAT <> "Error" Then
                        '    V.NoCertificadoSAT = "Error"
                        '    MsgError = xmldoc.InnerText
                        'Else
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value

                        V.GuardaDatosTimbrado(IdNota, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                        If IO.File.Exists(RutaXmlTemp) Then
                            IO.File.Delete(RutaXmlTemp)
                        End If
                        If IO.File.Exists(RutaXml) And GlobalConector = 1 Then
                            IO.File.Delete(RutaXml)
                        End If
                    End If
                End If
                If GlobalPacCFDI = 2 Then
                    Dim Os As New dbOpciones(MySqlcon)
                    en.GuardaArchivoTexto("temp.xml", strXML, System.Text.Encoding.UTF8)
                    en.GuardaArchivoTexto("co.txt", Cadena, System.Text.Encoding.UTF8)
                    Dim Timbre As String
                    Dim sa As New dbSucursalesArchivos
                    sa.DaOpciones(GlobalIdEmpresa, True)
                    Timbre = V.Timbrar3(S.RFC, strXML, "", Os._ApiKey, False, V.Serie, V.Folio)
                    If UCase(Timbre.Substring(0, 5)) <> "ERROR" Then
                        Dim xmldoc As New Xml.XmlDocument
                        en.GuardaArchivoTexto(RutaXmlTimbrado, Timbre, System.Text.Encoding.UTF8)
                        xmldoc.Load(RutaXmlTimbrado)
                        V.uuid = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
                        V.SelloCFD = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloCFD").Value
                        V.NoCertificadoSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("noCertificadoSAT").Value
                        V.FechaTimbrado = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("FechaTimbrado").Value
                        V.SelloSAT = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("selloSAT").Value
                        V.GuardaDatosTimbrado(IdNota, V.uuid, V.FechaTimbrado, V.SelloCFD, V.NoCertificadoSAT, V.SelloSAT)
                    Else
                        MensajeError += Timbre
                        V.NoCertificadoSAT = "Error"
                    End If
                End If
            Else
                'Crear xml timbrado
                Dim ExisteArchivo As Boolean = False
                If GlobalConector = 0 Then
                    If IO.File.Exists(RutaXml) Then ExisteArchivo = True
                Else
                    If IO.File.Exists(RutaXmlTimbrado) Then ExisteArchivo = True
                End If


                If pEstado = Estados.Guardada And ExisteArchivo = False Then
                    Dim strTimbrado As String
                    strTimbrado = vbCrLf + "<cfdi:Complemento>" + vbCrLf
                    strTimbrado += "<tfd:TimbreFiscalDigital version=""1.0"" UUID=""" + V.uuid + """ FechaTimbrado=""" + V.FechaTimbrado + """ selloCFD=""" + V.SelloCFD + """ noCertificadoSAT=""" + V.NoCertificadoSAT + """ selloSAT=""" + V.SelloSAT + """ />" + vbCrLf
                    strTimbrado += "</cfdi:Complemento>" + vbCrLf
                    strXML = strXML.Insert(strXML.LastIndexOf("</cfdi:Comprobante>") - 1, strTimbrado)
                    en.GuardaArchivoTexto(RutaXml, strXML, System.Text.Encoding.UTF8)
                End If
            End If

            If V.NoCertificadoSAT <> "Error" Then
                CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
                Return True
                
            Else
                MensajeError += "Ha ocurrido un error en el timbrado del la nota de crédito, intente mas tarde."
                V.Eliminar(IdNota)
                Return False

                'Error en timbrado
            End If
        Catch ex As Exception
            MensajeError += ex.Message
            Return False
        End Try


    End Function

    Private Function CadenaOriginalNC(ByVal pEstado As Byte, ByVal idNota As Integer) As Boolean
        Try
            Dim en As New Encriptador
            Dim V As New dbNotasDeCredito(idNota, MySqlcon)
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                Cadena = V.CreaCadenaOriginal22(idNota, GlobalIdMoneda)
            Else
                Cadena = V.CreaCadenaOriginal(idNota, GlobalIdMoneda)
            End If

            Dim Archivos As New dbSucursalesArchivos
            Archivos.DaRutaCER(V.IdSucursal, GlobalIdEmpresa, False)
            RutaXml = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoXML, False)
            RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.NotasdeCreditoPDF, False)
            Archivos.CierraDB()
            Sello = en.GeneraSello(Cadena, Archivos.RutaCer, Format(CDate(V.Fecha), "yyyy"), False)
            Dim Enc As New System.Text.UTF8Encoding
            Dim xmldoc As String
            If V.Fecha > FechaVerPunto2 And ActivaVerPunto2 Then
                xmldoc = V.CreaXML22(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            Else
                xmldoc = V.CreaXML(idNota, GlobalIdMoneda, Sello, GlobalIdEmpresa)
            End If

            Dim Bytes() As Byte = Enc.GetBytes(xmldoc)


            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
            IO.Directory.CreateDirectory(RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
            RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
            RutaXml = RutaXml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
           
            en.GuardaArchivoTexto(RutaXml + "\NC-" + Trim(V.Serie) + V.Folio.ToString + ".xml", xmldoc, System.Text.Encoding.UTF8)
            Return True
        Catch ex As Exception
            MensajeError += ex.Message
            Return False
        End Try
    End Function

End Class
