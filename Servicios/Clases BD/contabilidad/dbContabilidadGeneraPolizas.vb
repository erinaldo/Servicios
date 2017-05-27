Public Class dbContabilidadGeneraPolizas
    Dim Mascara As dbContabilidadMascaras
    Dim MascaraDetalles As dbContabilidadMascarasDetalles
    Dim Fecha1 As String
    Dim Fecha2 As String
    Dim Fechapoliza As String
    Public IdPoliza As Integer
    Public Exito As Boolean
    
    Structure stMascaraDetalles
        Dim CodigoV As String
        Dim IdCuenta As Integer
        Dim Cargo As Double
        Dim Abono As Double
        Dim Monto As Double
        Dim MontoNeg As Double
        Dim Modulo As Byte
        Dim Cuenta As String
        Dim Descripcion As String
        Dim Negativo As Byte
        Dim Beneficiario As Byte
        Dim PagoIngo As Byte
        Dim Uuids As Byte
    End Structure
    Public Sub New(ByRef pMascara As dbContabilidadMascaras, pFecha1 As String, pFecha2 As String, pFechaPoliza As String)
        Mascara = pMascara
        MascaraDetalles = New dbContabilidadMascarasDetalles(MySqlcon)
        Fecha1 = pFecha1
        Fecha2 = pFecha2
        Fechapoliza = pFechaPoliza
    End Sub
    ''' <summary>
    '''   Generador de polizas.
    ''' </summary>
    ''' <param name="pMovimiento">IdMovimiento= cuando es por movimiento aqui es diferente de cero.</param>
    ''' <param name="pidGenerico">IdGenerico=id para el comodín.</param>
    ''' <param name="pdonde">pdonde: De donde viene el idGenerico:  0 Clientes, 1 Proveedores, 2 Trabajadores, 3 Cuenta Banco, 4 Cuenta banco destino</param>
    ''' <remarks></remarks>
    Public Sub GeneraPolizaGeneral(pidMovimiento As Integer, pidGenerico As Integer, pdonde As Byte, pidGenerico2 As Integer, pdonde2 As Byte, pIdGenerico3 As Integer, pdonde3 As Byte, Optional pCantCheque As Double = 0, Optional pLeyenda As Byte = 0, Optional pConcepto As String = "")
        Dim Po As New dbContabilidadPolizas(MySqlcon)
        Dim Variables As New Collection
        Dim BeneficiarioStr As String = ""
        Dim Bene1 As Boolean = False
        Dim Bene2 As Boolean = False
        Dim Bene3 As Boolean = False
        Dim Bene4 As Boolean = False
        Dim Hay5 As Boolean = False
        Dim Monto As Double = 0

        Dim St As stMascaraDetalles
        Dim FacturasTemp As New Collection
        Mascara.PN1 = Po.NNiv1
        Mascara.pN2 = Po.NNiv2
        Mascara.pN3 = Po.NNiv3
        Mascara.pN4 = Po.NNiv4
        Mascara.pN5 = Po.NNiv5

        If pidMovimiento > 0 Then
            Mascara.DaCuentaComodin(pidGenerico, pdonde)
            If pidGenerico2 > 0 Then Mascara.DaCuentaComodin(pidGenerico2, pdonde2)
            If pIdGenerico3 > 0 Then Mascara.DaCuentaComodin(pIdGenerico3, pdonde3)
        End If

        Mascara.InicializaVariables()
        For Each vmod As Integer In MascaraDetalles.ModulosUsados(Mascara.ID)
            Mascara.ComodinesUsados = MascaraDetalles.ComodinesUsados(Mascara.ID)
            Mascara.LlenaVariables(vmod, Fecha1, Fecha2, pidMovimiento, Mascara.Credito, Mascara.IdSucursal, Mascara.IdTipoS)
            If vmod = 5 Then
                Hay5 = True
            End If
        Next
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        dr = MascaraDetalles.ConsultaReader(Mascara.ID, Po.NNiv1, Po.NNiv2, Po.NNiv3, Po.NNiv4, Po.NNiv5)
        While dr.Read
            St.IdCuenta = dr("idcuenta")
            St.Abono = dr("abono")
            St.Cargo = dr("cargo")
            St.CodigoV = dr("codigo")
            St.Modulo = dr("modulo")
            St.Cuenta = dr("Cuenta")
            St.Descripcion = dr("descripcion")
            St.Negativo = dr("negativo")
            St.PagoIngo = dr("inpagoinfo")
            St.Uuids = dr("inuuids")
            St.Monto = Mascara.DaValorVariable(St.CodigoV, St.Modulo, False)
            St.MontoNeg = Mascara.DaValorVariable(St.CodigoV, St.Modulo, True)
            If St.Cargo <> 0 Then Monto += St.Monto - St.MontoNeg
            St.Beneficiario = dr("beneficiario")
            If St.Modulo = 5 Then
                If St.Monto = Mascara.DaValorVariable("TNSL", St.Modulo, False) <> 0 Then
                    Bene1 = True
                End If
                If St.CodigoV.Contains("con") Or St.CodigoV.Contains("Con") Then
                    Bene3 = True
                End If
                If St.CodigoV.Contains("cr") Or St.CodigoV.Contains("Cr") Then
                    Bene2 = True
                End If
                If St.CodigoV.Contains("TRAS") = True Then
                    Bene4 = True
                End If
            End If
            Variables.Add(St)
        End While
        dr.Close()
        If Hay5 Then
            BeneficiarioStr = Mascara.InsertayDaProveedoresTemp(Bene1, Bene2, Bene3, Bene4)
        End If
        Dim Concepto As String = ""
        Dim ConceptoB As String = ""
        If Mascara.Tipo = 1 Or Mascara.Tipo = 4 Then
            Concepto = Mascara.Titulo + " " + CDate(Fecha1).ToString("dd/MM/yyyy") + " AL " + CDate(Fecha2).ToString("dd/MM/yyyy")
        End If
        If Mascara.Tipo = 0 Then
            Concepto = Mascara.Titulo + " " + CDate(Fecha1).ToString("MMMM").ToUpper + " " + CDate(Fecha1).ToString("yyyy")
        End If
        If Mascara.Tipo = 2 Then
            Concepto = Mascara.Titulo + " " + CDate(Fecha1).ToString("dd/MM/yyyy")
        End If
        If Mascara.Tipo = 3 Then
            Concepto = Mascara.Titulo + " " + CDate(Fecha1).ToString("dd/MM/yyyy")
        End If
        If pConcepto <> "" Then Concepto = pConcepto
        'ConceptoB = Trim(Concepto + " " + Mascara.StrAfectadas)
        If Hay5 = False Then
            ConceptoB = Concepto
        Else
            If pidMovimiento <> 0 Then
                ConceptoB = Trim(Mascara.StrAfectadas)
            Else
                ConceptoB = Trim(Concepto)
            End If
        End If
        Po.DetallesOrden = 1
        Po.guardarPoliza(Mascara.TipoPoliza, Po.bucarNumero(CDate(Fechapoliza).Month.ToString("00"), CDate(Fechapoliza).Year.ToString, Mascara.TipoPoliza, Mascara.IdClasificacion), Fechapoliza, ConceptoB, BeneficiarioStr, Monto.ToString("0.00"), Mascara.IdClasificacion, 3, pCantCheque, pLeyenda)
        Po.fecha = Fechapoliza
        IdPoliza = Po.IDPoliza
        If (Mascara.Canceladas = 0 Or Mascara.Canceladas = 1) Then
            For Each stt As stMascaraDetalles In Variables
                If stt.CodigoV.Contains("xf") = False And stt.CodigoV <> "TPXC" And stt.CodigoV <> "TDXC" And stt.CodigoV.Contains("TDEP") = False And stt.CodigoV.Contains("TXP") = False And stt.CodigoV.Contains("TIVAXP") = False And stt.CodigoV.Contains("TIEPSXP") = False And stt.CodigoV.Contains("TIVARETXP") = False And stt.CodigoV.Contains("TXTRAS") = False And stt.CodigoV.Contains("TISRXRetSL") = False Then
                    If stt.Cargo = 0 Then
                        stt.Cargo = -999999999
                    Else
                        stt.Cargo = stt.Monto
                        If stt.Negativo = 1 Then stt.Cargo *= -1
                    End If
                    If stt.Abono = 0 Then
                        stt.Abono = -999999999
                    Else
                        stt.Abono = stt.Monto
                        If stt.Negativo = 1 Then stt.Abono *= -1
                    End If
                    If stt.Monto <> 0 Then
                        If stt.Beneficiario = 0 Then
                            If Mascara.Tipo = 3 And stt.IdCuenta < 0 Then
                                Mascara.EligeComodin(stt.IdCuenta, stt.CodigoV)
                                Po.guardarDetalles(Mascara.CuentaPorMov, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), Mascara.IdCuentaPorMov, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                                If stt.PagoIngo = 1 Then GuardaPagoInfo(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            Else
                                Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                                If stt.PagoIngo = 1 Then GuardaPagoInfo(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            End If
                        Else
                            If Mascara.Tipo = 3 And stt.IdCuenta < 0 Then
                                Mascara.EligeComodin(stt.IdCuenta, stt.CodigoV)
                                Po.guardarDetalles(Mascara.CuentaPorMov, BeneficiarioStr, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), Mascara.IdCuentaPorMov, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                                If stt.PagoIngo = 1 Then GuardaPagoInfo(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            Else
                                Po.guardarDetalles(stt.Cuenta, BeneficiarioStr, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                                If stt.PagoIngo = 1 Then GuardaPagoInfo(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            End If
                        End If
                    End If
                Else
                    Dim MontoMov As Double
                    Dim CargoTemp As Double
                    Dim AbonoTemp As Double
                    If stt.Modulo = 4 Then
                        Dim tmpCargo As Double
                        Dim tmpAbono As Double
                        If stt.CodigoV = "TDEP" Then
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosLista
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -9
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -10
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -11
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If cn.Cantidad <> 0 Then
                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If
                        If stt.CodigoV = "TDEPcr" Then
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCr
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -9
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -10
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -11
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If cn.Cantidad <> 0 Then
                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If
                        If stt.CodigoV = "TXP" Or stt.CodigoV = "TXPSIGCr" Or stt.CodigoV = "TXPSINGCr" Or stt.CodigoV = "TXPGC" Or stt.CodigoV = "TXPPC" Or stt.CodigoV = "TXPO" Then
                            Dim TmpCant As Double
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosLista
                                Select Case stt.CodigoV
                                    Case "TXP"
                                        TmpCant = cn.Cantidad
                                    Case "TXPSIGCr"
                                        TmpCant = cn.SinIvaGravable
                                    Case "TXPSINGCr"
                                        TmpCant = cn.SinIvaNoGravable
                                    Case "TXPGC"
                                        TmpCant = cn.GananciaCambiaria
                                    Case "TXPPC"
                                        TmpCant = cn.PerdidaCambiaria
                                    Case "TXPO"
                                        TmpCant = cn.PagoEnMonedaOriginal
                                End Select
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = TmpCant
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = TmpCant
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -12
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -13
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -14
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If TmpCant <> 0 Then
                                    Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TDEPcon" Then
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCon
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -9
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -10
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -11
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If cn.Cantidad <> 0 Then
                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If
                        If stt.CodigoV = "TXPcon" Or stt.CodigoV = "TXPSIGCon" Or stt.CodigoV = "TXPSINGCon" Then
                            Dim TmpCant As Double
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosListaCon
                                Select Case stt.CodigoV
                                    Case "TXPcon"
                                        TmpCant = cn.Cantidad
                                    Case "TXPSIGCon"
                                        TmpCant = cn.SinIvaGravable
                                    Case "TXPSINGCon"
                                        TmpCant = cn.SinIvaNoGravable
                                End Select
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = TmpCant
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = TmpCant
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -12
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -13
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -14
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If TmpCant <> 0 Then
                                    Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    If stt.Uuids = 1 Then
                                        Po.guardarComprobante(IdPoliza, Po.IDRenglon, cn.Uuid.Uuid, cn.Uuid.Monto, cn.Uuid.RFC, cn.Uuid.IdMoneda, cn.Uuid.TipodeCambio)
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If
                    End If
                    If stt.Modulo = 5 Then
                        Dim tmpCargo As Double
                        Dim tmpAbono As Double
                        If stt.CodigoV = "TDEP" Or stt.CodigoV = "TXPSIGSL" Or stt.CodigoV = "TXPSINGSL" Then
                            Dim TmpCant As Double
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaRet
                                Select Case stt.CodigoV
                                    Case "TDEP"
                                        TmpCant = cn.Cantidad
                                    Case "TXPSIGSL"
                                        TmpCant = cn.SinIvaGravable
                                    Case "TXPSINGSL"
                                        TmpCant = cn.SinIvaNoGravable
                                End Select
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = TmpCant
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = TmpCant
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -9
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -10
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -11
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If TmpCant <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "0", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.ChequeTrans.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaDestino, cn.ChequeTrans.IdBancoDestino, cn.ChequeTrans.BancoDestinoEx, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NumeroCheque, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            End If
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.ChequeTrans.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaDestino, cn.ChequeTrans.IdBancoDestino, cn.ChequeTrans.BancoDestinoEx, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NumeroCheque, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            End If
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If
                        If stt.CodigoV = "TXP" Or stt.CodigoV = "TXPSIGCr" Or stt.CodigoV = "TXPSINGCr" Or stt.CodigoV = "TXPGC" Or stt.CodigoV = "TXPPC" Or stt.CodigoV = "TXPO" Then
                            Dim TmpCant As Double
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosListaRet
                                Select Case stt.CodigoV
                                    Case "TXP"
                                        TmpCant = cn.Cantidad
                                    Case "TXPSIGCr"
                                        TmpCant = cn.SinIvaGravable
                                    Case "TXPSINGCr"
                                        TmpCant = cn.SinIvaNoGravable
                                    Case "TXPGC"
                                        TmpCant = cn.GananciaCambiaria
                                    Case "TXPPC"
                                        TmpCant = cn.PerdidaCambiaria
                                    Case "TXPO"
                                        TmpCant = cn.PagoEnMonedaOriginal
                                End Select
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = TmpCant
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = TmpCant
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -15
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -16
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -17
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If TmpCant <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "0", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "0", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If



                        If stt.CodigoV = "TDEPcr" Then
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaRetCr
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -9
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -10
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -11
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If cn.Cantidad <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.ChequeTrans.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaDestino, cn.ChequeTrans.IdBancoDestino, cn.ChequeTrans.BancoDestinoEx, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NumeroCheque, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            End If
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.ChequeTrans.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaDestino, cn.ChequeTrans.IdBancoDestino, cn.ChequeTrans.BancoDestinoEx, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NumeroCheque, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            End If
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If



                        If stt.CodigoV = "TDEPcon" Then
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaRetCon
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -9
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -10
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -11
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If cn.Cantidad <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.ChequeTrans.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaDestino, cn.ChequeTrans.IdBancoDestino, cn.ChequeTrans.BancoDestinoEx, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NumeroCheque, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            End If
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.ChequeTrans.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaDestino, cn.ChequeTrans.IdBancoDestino, cn.ChequeTrans.BancoDestinoEx, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.ChequeTrans.NumeroCheque, cn.ChequeTrans.IdBancoOrigen, "", cn.ChequeTrans.NoCuentaOrigen, cn.ChequeTrans.Fecha, cn.ChequeTrans.Beneficiario, cn.ChequeTrans.RFC, cn.ChequeTrans.Monto, cn.ChequeTrans.IdMoneda, cn.ChequeTrans.TipodeCambio)
                                            End If
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If
                        If stt.CodigoV = "TXPcon" Or stt.CodigoV = "TXPSIGCon" Or stt.CodigoV = "TXPSINGCon" Then
                            Dim TmpCant As Double
                            For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosListaRetCon
                                Select Case stt.CodigoV
                                    Case "TXPcon"
                                        TmpCant = cn.Cantidad
                                    Case "TXPSIGCon"
                                        TmpCant = cn.SinIvaGravable
                                    Case "TXPSINGCon"
                                        TmpCant = cn.SinIvaNoGravable
                                End Select
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = TmpCant
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = TmpCant
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.Idcuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                Else
                                    Select Case stt.IdCuenta
                                        Case -15
                                            cn.Idcuenta = cn.IdCuenta2
                                            cn.Cuenta = cn.Cuenta2
                                        Case -16
                                            cn.Idcuenta = cn.IdCuenta3
                                            cn.Cuenta = cn.Cuenta3
                                        Case -17
                                            cn.Idcuenta = cn.IdCuenta4
                                            cn.Cuenta = cn.Cuenta4
                                    End Select
                                End If
                                If TmpCant <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "0", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.Uuids = 1 Then
                                            Po.guardarComprobante(IdPoliza, Po.IDRenglon, cn.Uuid.Uuid, cn.Uuid.Monto, cn.Uuid.RFC, cn.Uuid.IdMoneda, cn.Uuid.TipodeCambio)
                                        End If
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "0", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.Uuids = 1 Then
                                            Po.guardarComprobante(IdPoliza, Po.IDRenglon, cn.Uuid.Uuid, cn.Uuid.Monto, cn.Uuid.RFC, cn.Uuid.IdMoneda, cn.Uuid.TipodeCambio)
                                        End If
                                    End If
                                End If
                            Next
                        End If
                        If stt.CodigoV = "TIVAXPRet" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRet
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIva <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                Else
                                    If cn.ValorActos <> 0 Then
                                        If stt.Beneficiario = 0 Then
                                            Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        Else
                                            Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIVAXPRetCr" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetCr
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIva <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                Else
                                    If cn.ValorActos <> 0 Then
                                        If stt.Beneficiario = 0 Then
                                            Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        Else
                                            Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIVAXPRetCon" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetCon
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIva <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                Else
                                    If cn.ValorActos <> 0 Then
                                        If stt.Beneficiario = 0 Then
                                            Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        Else
                                            Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIVAXPRetSL" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetSL
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIva
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIva <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                Else
                                    If cn.ValorActos <> 0 Then
                                        If stt.Beneficiario = 0 Then
                                            Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        Else
                                            Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, cn.Ivapor.ToString, cn.NombreProveedor, 1, cn.ValorActos, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        End If
                                        If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                    End If
                                End If
                            Next
                        End If
                        If stt.CodigoV = "TISRXRetSL" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetSL
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalISR
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalISR
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If

                                If stt.Beneficiario = 0 Then
                                    Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", 0, 0, "", 0, 0, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", 0, 0, "", 0, 0, Po.fecha, "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                                If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)

                            Next
                        End If

                        If stt.CodigoV = "TIVARETXPRet" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRet
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIVARETXPRetSL" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetSL
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIVARETXPRetCr" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetCr
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIVARETXPRetCon" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetCon
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIvaRet
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", cn.Ivaretpor, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIEPSXPRet" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRet
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIEPSXPRetSL" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetSL
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIEPSXPRetCr" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetCr
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TIEPSXPRetCon" Then
                            For Each cn As dbContabilidadMascaras.stIvaInfo In Mascara.TIVAxPagoRetCon
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.TotalIeps
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuenta = stt.IdCuenta
                                    cn.Cuenta = stt.Cuenta
                                End If
                                If cn.TotalIvaRet <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.Cuenta, cn.Concepto, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    Else
                                        Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuenta, "", cn.IdProveedor, "0", cn.NombreProveedor, 1, 0, Po.fecha, "", 0, cn.IepsPor, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                    End If
                                    If stt.Uuids = 1 Then GuardaUuidsPorFicha(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV, cn.IdDeposito)
                                End If
                            Next
                        End If

                        If stt.CodigoV = "TXTRAS" Or stt.CodigoV = "TXTRASD" Then
                            For Each cn As dbContabilidadMascaras.stTraspasos In Mascara.TxTraspaso
                                If stt.Cargo = 0 Then
                                    tmpCargo = -999999999
                                Else
                                    tmpCargo = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpCargo *= -1
                                End If
                                If stt.Abono = 0 Then
                                    tmpAbono = -999999999
                                Else
                                    tmpAbono = cn.Cantidad
                                    If stt.Negativo = 1 Then tmpAbono *= -1
                                End If
                                If stt.IdCuenta > 0 Then
                                    cn.IdCuentaO = stt.IdCuenta
                                    cn.CuentaO = stt.Cuenta
                                    cn.IdCuentaD = stt.IdCuenta
                                    cn.CuentaD = stt.Cuenta
                                End If
                                If stt.CodigoV = "TXTRASD" Then
                                    cn.IdCuentaO = cn.IdCuentaD
                                    cn.CuentaO = cn.CuentaD
                                End If
                                If cn.Cantidad <> 0 Then
                                    If stt.Beneficiario = 0 Then
                                        Po.guardarDetalles(cn.CuentaO, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuentaO, "", 0, "0", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.PagoInf.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.PagoInf.NoCuentaOrigen, cn.PagoInf.IdBancoOrigen, "", cn.PagoInf.NoCuentaDestino, cn.PagoInf.IdBancoDestino, cn.PagoInf.BancoDestinoEx, cn.PagoInf.Fecha, cn.PagoInf.Beneficiario, cn.PagoInf.RFC, cn.PagoInf.Monto, cn.PagoInf.IdMoneda, cn.PagoInf.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.PagoInf.NumeroCheque, cn.PagoInf.IdBancoOrigen, "", cn.PagoInf.NoCuentaOrigen, cn.PagoInf.Fecha, cn.PagoInf.Beneficiario, cn.PagoInf.RFC, cn.PagoInf.Monto, cn.PagoInf.IdMoneda, cn.PagoInf.TipodeCambio)
                                            End If
                                        End If
                                    Else
                                        Po.guardarDetalles(cn.CuentaO, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.IdCuentaO, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                        If stt.PagoIngo = 1 Then
                                            If cn.PagoInf.NumeroCheque = "" Then
                                                Po.guardarTransferencia(IdPoliza, Po.IDRenglon, cn.PagoInf.NoCuentaOrigen, cn.PagoInf.IdBancoOrigen, "", cn.PagoInf.NoCuentaDestino, cn.PagoInf.IdBancoDestino, cn.PagoInf.BancoDestinoEx, cn.PagoInf.Fecha, cn.PagoInf.Beneficiario, cn.PagoInf.RFC, cn.PagoInf.Monto, cn.PagoInf.IdMoneda, cn.PagoInf.TipodeCambio)
                                            Else
                                                Po.guardarCheque(IdPoliza, Po.IDRenglon, cn.PagoInf.NumeroCheque, cn.PagoInf.IdBancoOrigen, "", cn.PagoInf.NoCuentaOrigen, cn.PagoInf.Fecha, cn.PagoInf.Beneficiario, cn.PagoInf.RFC, cn.PagoInf.Monto, cn.PagoInf.IdMoneda, cn.PagoInf.TipodeCambio)
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        End If

                    End If
                    'Percepciones Nómina
                    If stt.CodigoV = "TPXC" Then
                        For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TPxC
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                CargoTemp = cn.importe
                                If stt.Negativo = 1 Then CargoTemp *= -1
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                AbonoTemp = cn.importe
                                If stt.Negativo = 1 Then AbonoTemp *= -1
                            End If
                            If cn.importe <> 0 Then
                                If stt.IdCuenta < 0 Then
                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Po.guardarDetalles(stt.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                            End If
                        Next


                    End If
                    'Deducciones nómina
                    If stt.CodigoV = "TDXC" Then
                        For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TDxC
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                CargoTemp = cn.importe
                                If stt.Negativo = 1 Then CargoTemp *= -1
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                AbonoTemp = cn.importe
                                If stt.Negativo = 1 Then AbonoTemp *= -1
                            End If
                            If cn.importe <> 0 Then
                                If stt.IdCuenta < 0 Then
                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Po.guardarDetalles(stt.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                            End If
                        Next
                    End If
                    If stt.CodigoV.Contains("netoxf") Then
                        Select Case stt.Modulo
                            Case 0
                                FacturasTemp = Mascara.FacturasLista
                            Case 1
                                FacturasTemp = Mascara.FacturasListaC
                            Case 2
                                FacturasTemp = Mascara.FacturasListaD
                            Case 3
                                FacturasTemp = Mascara.FacturasListaDC
                            Case 6
                                FacturasTemp = Mascara.FacturasListaNC
                            Case 7
                                FacturasTemp = Mascara.FacturasListaNCC
                            Case 8
                                FacturasTemp = Mascara.FacturasListaNCA
                            Case 9
                                FacturasTemp = Mascara.FacturasListaNCAC
                            Case 11
                                Select Case stt.CodigoV
                                    Case "TNEnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMIE
                                    Case "TNSnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMIS
                                    Case "TNTnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMIT
                                    Case "TNTRnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMITR
                                End Select
                            Case 12
                                FacturasTemp = Mascara.FacturasListaDOC
                        End Select
                        For Each Factura As dbContabilidadMascaras.stDepositos In FacturasTemp
                            Select Case stt.CodigoV
                                Case "TNnetoxf"
                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido + Factura.TotalImpLocT - Factura.TotalImpLocR
                                Case "TSInetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TIVAnetoxf"
                                    MontoMov = Factura.Iva
                                Case "TIEPSnetoxf"
                                    MontoMov = Factura.Ieps
                                Case "TIVARnetoxf"
                                    MontoMov = Factura.Retendido
                                Case "TISRnetoxf"
                                    MontoMov = Factura.ISR
                                Case "TIVANnetoxf"
                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
                                Case "TILTnetoxf"
                                    MontoMov = Factura.TotalImpLocT
                                Case "TILRnetoxf"
                                    MontoMov = Factura.TotalImpLocR
                                Case "TSIGnetoxf"
                                    MontoMov = Factura.SinIvaGravable
                                Case "TSINGnetoxf"
                                    MontoMov = Factura.SinIvaNoGravable
                                Case "TNEnetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TNSnetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TNTnetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TNTRnetoxf"
                                    MontoMov = Factura.Cantidad
                            End Select
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                CargoTemp = MontoMov
                                If stt.Negativo = 1 Then CargoTemp *= -1
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                AbonoTemp = MontoMov
                                If stt.Negativo = 1 Then AbonoTemp *= -1
                            End If
                            If MontoMov <> 0 Then
                                If stt.IdCuenta > 0 Then
                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Select Case stt.IdCuenta
                                        Case -18
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -19
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -20
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -21
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -22
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -23
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -27
                                            Factura.Idcuenta = Factura.Idcuenta
                                            Factura.Cuenta = Factura.Cuenta
                                        Case -28
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -29
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -30
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4

                                    End Select
                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                                If stt.Uuids = 1 Then
                                    Po.guardarComprobante(IdPoliza, Po.IDRenglon, Factura.Uuid.Uuid, Factura.Uuid.Monto, Factura.Uuid.RFC, Factura.Uuid.IdMoneda, Factura.Uuid.TipodeCambio)
                                End If
                            End If
                        Next
                    End If
                    If stt.CodigoV.Contains("Conxf") Then
                        Select Case stt.Modulo
                            Case 0
                                FacturasTemp = Mascara.FacturasListaCon
                            Case 1
                                FacturasTemp = Mascara.FacturasListaCCon
                            Case 2
                                FacturasTemp = Mascara.FacturasListaDCon
                            Case 3
                                FacturasTemp = Mascara.FacturasListaDCCon

                        End Select
                        For Each Factura As dbContabilidadMascaras.stDepositos In FacturasTemp
                            Select Case stt.CodigoV
                                Case "TNConxf"
                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido + Factura.TotalImpLocT - Factura.TotalImpLocR
                                Case "TSIConxf"
                                    MontoMov = Factura.Cantidad
                                Case "TIVAConxf"
                                    MontoMov = Factura.Iva
                                Case "TIEPSConxf"
                                    MontoMov = Factura.Ieps
                                Case "TIVARConxf"
                                    MontoMov = Factura.Retendido
                                Case "TISRConxf"
                                    MontoMov = Factura.ISR
                                Case "TIVANConxf"
                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
                                Case "TILTConxf"
                                    MontoMov = Factura.TotalImpLocT
                                Case "TILRConxf"
                                    MontoMov = Factura.TotalImpLocR
                                Case "TSIGConxf"
                                    MontoMov = Factura.SinIvaGravable
                                Case "TSINGConxf"
                                    MontoMov = Factura.SinIvaNoGravable
                            End Select
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                CargoTemp = MontoMov
                                If stt.Negativo = 1 Then CargoTemp *= -1
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                AbonoTemp = MontoMov
                                If stt.Negativo = 1 Then AbonoTemp *= -1
                            End If
                            If MontoMov <> 0 Then
                                If stt.IdCuenta > 0 Then
                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Select Case stt.IdCuenta
                                        Case -18
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -19
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -20
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -21
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -22
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -23
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                    End Select
                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                                If stt.Uuids = 1 Then
                                    Po.guardarComprobante(IdPoliza, Po.IDRenglon, Factura.Uuid.Uuid, Factura.Uuid.Monto, Factura.Uuid.RFC, Factura.Uuid.IdMoneda, Factura.Uuid.TipodeCambio)
                                End If
                            End If
                        Next
                    End If
                    If stt.CodigoV.Contains("Crxf") Then
                        Select Case stt.Modulo
                            Case 0
                                FacturasTemp = Mascara.FacturasListaCr
                            Case 1
                                FacturasTemp = Mascara.FacturasListaCCr
                            Case 2
                                FacturasTemp = Mascara.FacturasListaDCr
                            Case 3
                                FacturasTemp = Mascara.FacturasListaDCCr
                        End Select
                        For Each Factura As dbContabilidadMascaras.stDepositos In FacturasTemp
                            Select Case stt.CodigoV
                                Case "TNCrxf"
                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido + Factura.TotalImpLocT - Factura.TotalImpLocR
                                Case "TSICrxf"
                                    MontoMov = Factura.Cantidad
                                Case "TIVACrxf"
                                    MontoMov = Factura.Iva
                                Case "TIEPSCrxf"
                                    MontoMov = Factura.Ieps
                                Case "TIVARCrxf"
                                    MontoMov = Factura.Retendido
                                Case "TISRCrxf"
                                    MontoMov = Factura.ISR
                                Case "TIVANCrxf"
                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
                                Case "TILTCrxf"
                                    MontoMov = Factura.TotalImpLocT
                                Case "TILRCrxf"
                                    MontoMov = Factura.TotalImpLocR
                                Case "TSIGCrxf"
                                    MontoMov = Factura.SinIvaGravable
                                Case "TSINGCrxf"
                                    MontoMov = Factura.SinIvaNoGravable
                            End Select
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                CargoTemp = MontoMov
                                If stt.Negativo = 1 Then CargoTemp *= -1
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                AbonoTemp = MontoMov
                                If stt.Negativo = 1 Then AbonoTemp *= -1
                            End If
                            If MontoMov <> 0 Then
                                If stt.IdCuenta > 0 Then
                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Select Case stt.IdCuenta
                                        Case -18
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -19
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -20
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -21
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -22
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -23
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                    End Select
                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                                If stt.Uuids = 1 Then
                                    Po.guardarComprobante(IdPoliza, Po.IDRenglon, Factura.Uuid.Uuid, Factura.Uuid.Monto, Factura.Uuid.RFC, Factura.Uuid.IdMoneda, Factura.Uuid.TipodeCambio)
                                End If
                            End If
                        Next
                    End If
                End If
            Next
        End If
        If Mascara.TNNeg <> 0 And (Mascara.Canceladas = 0 Or Mascara.Canceladas = 2) Then
            ConceptoB = Concepto + " " + Mascara.strAfectadasNeg
            For Each stt As stMascaraDetalles In Variables
                If stt.CodigoV.Contains("xf") = False And stt.CodigoV <> "TPXC" And stt.CodigoV <> "TDXC" And stt.CodigoV.Contains("TDEP") = False And stt.CodigoV.Contains("TXP") = False Then
                    If stt.Cargo = 0 Then
                        stt.Cargo = -999999999
                    Else
                        If stt.Negativo = 0 Then
                            stt.Cargo = stt.MontoNeg * -1
                        Else
                            stt.Cargo = stt.MontoNeg
                        End If
                    End If
                    If stt.Abono = 0 Then
                        stt.Abono = -999999999
                    Else
                        If stt.Negativo = 0 Then
                            stt.Abono = stt.MontoNeg * -1
                        Else
                            stt.Abono = stt.MontoNeg
                        End If
                    End If
                    If stt.MontoNeg <> 0 Then
                        If stt.Beneficiario = 0 Then
                            If Mascara.Tipo = 3 And stt.IdCuenta < 0 Then
                                Mascara.EligeComodin(stt.IdCuenta, stt.CodigoV)
                                Po.guardarDetalles(Mascara.CuentaPorMov, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), Mascara.IdCuentaPorMov, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            Else
                                Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            End If
                        Else
                            If Mascara.Tipo = 3 And stt.IdCuenta < 0 Then
                                Mascara.EligeComodin(stt.IdCuenta, stt.CodigoV)
                                Po.guardarDetalles(Mascara.CuentaPorMov, BeneficiarioStr, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), Mascara.IdCuentaPorMov, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            Else
                                Po.guardarDetalles(stt.Cuenta, BeneficiarioStr, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                If stt.Uuids = 1 Then GuardaUuids(stt.Modulo, Po.IDPoliza, Po.IDRenglon, stt.CodigoV)
                            End If
                        End If
                    End If
                Else

                    Dim MontoMov As Double
                    Dim CargoTemp As Double
                    Dim AbonoTemp As Double
                    If stt.CodigoV.Contains("netoxf") Then
                        Select Case stt.Modulo
                            Case 0
                                FacturasTemp = Mascara.FacturasListaNeg
                            Case 1
                                FacturasTemp = Mascara.FacturasListaCNeg
                            Case 2
                                FacturasTemp = Mascara.FacturasListaDNeg
                            Case 3
                                FacturasTemp = Mascara.FacturasListaDCNeg
                            Case 6
                                FacturasTemp = Mascara.FacturasListaNCNeg
                            Case 7
                                FacturasTemp = Mascara.FacturasListaNCCNeg
                            Case 8
                                FacturasTemp = Mascara.FacturasListaNCANeg
                            Case 9
                                FacturasTemp = Mascara.FacturasListaNCACNeg
                            Case 11
                                Select Case stt.CodigoV
                                    Case "TNEnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMIENeg
                                    Case "TNSnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMISNeg
                                    Case "TNTnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMITNeg
                                    Case "TNTRnetoxf"
                                        FacturasTemp = Mascara.FacturasListaMITRNeg
                                End Select
                            Case 12
                                FacturasTemp = Mascara.FacturasListaDOCNeg
                        End Select
                        For Each Factura As dbContabilidadMascaras.stDepositos In FacturasTemp
                            Select Case stt.CodigoV
                                Case "TNnetoxf"
                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido + Factura.TotalImpLocT - Factura.TotalImpLocR
                                Case "TSInetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TIVAnetoxf"
                                    MontoMov = Factura.Iva
                                Case "TIEPSnetoxf"
                                    MontoMov = Factura.Ieps
                                Case "TIVARnetoxf"
                                    MontoMov = Factura.Retendido
                                Case "TISRnetoxf"
                                    MontoMov = Factura.ISR
                                Case "TIVANnetoxf"
                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
                                Case "TILTnetoxf"
                                    MontoMov = Factura.TotalImpLocT
                                Case "TILRnetoxf"
                                    MontoMov = Factura.TotalImpLocR
                                Case "TSIGnetoxf"
                                    MontoMov = Factura.SinIvaGravable
                                Case "TSINGnetoxf"
                                    MontoMov = Factura.SinIvaNoGravable
                                Case "TNEnetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TNSnetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TNTnetoxf"
                                    MontoMov = Factura.Cantidad
                                Case "TNTRnetoxf"
                                    MontoMov = Factura.Cantidad
                            End Select
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    CargoTemp = MontoMov * -1
                                Else
                                    CargoTemp = MontoMov
                                End If
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    AbonoTemp = MontoMov * -1
                                Else
                                    AbonoTemp = MontoMov
                                End If
                            End If
                            If MontoMov <> 0 Then
                                If stt.IdCuenta > 0 Then
                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Select Case stt.IdCuenta
                                        Case -18
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -19
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -20
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -21
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -22
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -23
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -27
                                            Factura.Idcuenta = Factura.Idcuenta
                                            Factura.Cuenta = Factura.Cuenta
                                        Case -28
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -29
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -30
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                    End Select
                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                                If stt.Uuids = 1 Then
                                    Po.guardarComprobante(IdPoliza, Po.IDRenglon, Factura.Uuid.Uuid, Factura.Uuid.Monto, Factura.Uuid.RFC, Factura.Uuid.IdMoneda, Factura.Uuid.TipodeCambio)
                                End If
                            End If
                        Next
                    End If
                    If stt.CodigoV.Contains("Conxf") Then
                        Select Case stt.Modulo
                            Case 0
                                FacturasTemp = Mascara.FacturasListaNegCon
                            Case 1
                                FacturasTemp = Mascara.FacturasListaCNegCon
                            Case 2
                                FacturasTemp = Mascara.FacturasListaDNegCon
                            Case 3
                                FacturasTemp = Mascara.FacturasListaDCNegCon
                        End Select
                        For Each Factura As dbContabilidadMascaras.stDepositos In FacturasTemp
                            Select Case stt.CodigoV
                                Case "TNConxf"
                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido + Factura.TotalImpLocT - Factura.TotalImpLocR
                                Case "TSIConxf"
                                    MontoMov = Factura.Cantidad
                                Case "TIVAConxf"
                                    MontoMov = Factura.Iva
                                Case "TIEPSConxf"
                                    MontoMov = Factura.Ieps
                                Case "TIVARConxf"
                                    MontoMov = Factura.Retendido
                                Case "TISRConxf"
                                    MontoMov = Factura.ISR
                                Case "TIVANConxf"
                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
                                Case "TILTConxf"
                                    MontoMov = Factura.TotalImpLocT
                                Case "TILRConxf"
                                    MontoMov = Factura.TotalImpLocR
                                Case "TSIGConxf"
                                    MontoMov = Factura.SinIvaGravable
                                Case "TSINGConxf"
                                    MontoMov = Factura.SinIvaNoGravable
                            End Select
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    CargoTemp = MontoMov * -1
                                Else
                                    CargoTemp = MontoMov
                                End If
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    AbonoTemp = MontoMov * -1
                                Else
                                    AbonoTemp = MontoMov
                                End If
                            End If
                            If MontoMov <> 0 Then
                                If stt.IdCuenta > 0 Then
                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Select Case stt.IdCuenta
                                        Case -18
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -19
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -20
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -21
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -22
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -23
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                    End Select
                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                                If stt.Uuids = 1 Then
                                    Po.guardarComprobante(IdPoliza, Po.IDRenglon, Factura.Uuid.Uuid, Factura.Uuid.Monto, Factura.Uuid.RFC, Factura.Uuid.IdMoneda, Factura.Uuid.TipodeCambio)
                                End If
                            End If
                        Next
                    End If
                    If stt.CodigoV.Contains("Crxf") Then
                        Select Case stt.Modulo
                            Case 0
                                FacturasTemp = Mascara.FacturasListaNegCr
                            Case 1
                                FacturasTemp = Mascara.FacturasListaCNegCr
                            Case 2
                                FacturasTemp = Mascara.FacturasListaDNegCr
                            Case 3
                                FacturasTemp = Mascara.FacturasListaDCNegCr
                        End Select
                        For Each Factura As dbContabilidadMascaras.stDepositos In FacturasTemp
                            Select Case stt.CodigoV
                                Case "TNCrxf"
                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido + Factura.TotalImpLocT - Factura.TotalImpLocR
                                Case "TSICrxf"
                                    MontoMov = Factura.Cantidad
                                Case "TIVACrxf"
                                    MontoMov = Factura.Iva
                                Case "TIEPSCrxf"
                                    MontoMov = Factura.Ieps
                                Case "TIVARCrxf"
                                    MontoMov = Factura.Retendido
                                Case "TISRCrxf"
                                    MontoMov = Factura.ISR
                                Case "TIVANCrxf"
                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
                                Case "TILTCrxf"
                                    MontoMov = Factura.TotalImpLocT
                                Case "TILRCrxf"
                                    MontoMov = Factura.TotalImpLocR
                                Case "TSIGCrxf"
                                    MontoMov = Factura.SinIvaGravable
                                Case "TSINGCrxf"
                                    MontoMov = Factura.SinIvaNoGravable
                            End Select
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    CargoTemp = MontoMov * -1
                                Else
                                    CargoTemp = MontoMov
                                End If
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    AbonoTemp = MontoMov * -1
                                Else
                                    AbonoTemp = MontoMov
                                End If
                            End If
                            If MontoMov <> 0 Then
                                If stt.IdCuenta > 0 Then
                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Select Case stt.IdCuenta
                                        Case -18
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -19
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -20
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                        Case -21
                                            Factura.Idcuenta = Factura.IdCuenta2
                                            Factura.Cuenta = Factura.Cuenta2
                                        Case -22
                                            Factura.Idcuenta = Factura.IdCuenta3
                                            Factura.Cuenta = Factura.Cuenta3
                                        Case -23
                                            Factura.Idcuenta = Factura.IdCuenta4
                                            Factura.Cuenta = Factura.Cuenta4
                                    End Select
                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                                If stt.Uuids = 1 Then
                                    Po.guardarComprobante(IdPoliza, Po.IDRenglon, Factura.Uuid.Uuid, Factura.Uuid.Monto, Factura.Uuid.RFC, Factura.Uuid.IdMoneda, Factura.Uuid.TipodeCambio)
                                End If
                            End If
                        Next
                    End If
                    'nóminas percepciones canceladas
                    If stt.CodigoV = "TPXC" Then
                        For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TPxCNeg
                            'If stt.Negativo = 0 Then
                            '    stt.Cargo = stt.MontoNeg * -1
                            'Else
                            '    stt.Cargo = stt.MontoNeg
                            'End If
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    CargoTemp = cn.importe * -1
                                Else
                                    CargoTemp = cn.importe
                                End If
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                If stt.Negativo = 0 Then
                                    AbonoTemp = cn.importe * -1
                                Else
                                    AbonoTemp = cn.importe
                                End If
                            End If
                            If cn.importe <> 0 Then
                                If stt.IdCuenta < 0 Then
                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Po.guardarDetalles(stt.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                            End If
                        Next
                    End If
                    'nóminas deducciones canceladas
                    If stt.CodigoV = "TDXC" Then
                        For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TDxCNeg
                            If stt.Cargo = 0 Then
                                CargoTemp = -999999999
                            Else
                                CargoTemp = cn.importe
                                If stt.Negativo = 1 Then CargoTemp *= -1
                            End If
                            If stt.Abono = 0 Then
                                AbonoTemp = -999999999
                            Else
                                AbonoTemp = cn.importe
                                If stt.Negativo = 1 Then AbonoTemp *= -1
                            End If
                            If cn.importe <> 0 Then
                                If stt.IdCuenta < 0 Then
                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                Else
                                    Po.guardarDetalles(stt.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "", 0, 0, Po.DetallesOrden, 0, 0, 0, 0, 0)
                                End If
                            End If
                        Next
                    End If
                End If

            Next
        End If
        Exito = Po.ActualizaMontoPoliza(Po.IDPoliza)
        If pidMovimiento = 0 And Po.IDPoliza > 0 Then Mascara.GuardaUsada(Po.IDPoliza, Mascara.ID, Fechapoliza)
        'End Select
        'ConsultaUsadas()
    End Sub
    Private Sub GuardaUuids(pModulo As Byte, pIdPoliza As Integer, pidRenglon As Integer, pVariable As String)
        'ComboBox10.Items.Add("Ventas") '0
        'ComboBox10.Items.Add("Compras") '1
        'ComboBox10.Items.Add("Ventas Devoluciones") '2
        'ComboBox10.Items.Add("Compras Devoluciones") '3
        'ComboBox10.Items.Add("Depósitos") '4
        'ComboBox10.Items.Add("Pagos") '5
        'ComboBox10.Items.Add("Notas de crédito ventas") '6
        'ComboBox10.Items.Add("Notas de crédito compras") '7
        'ComboBox10.Items.Add("Notas de cargo ventas") '8
        'ComboBox10.Items.Add("Notas de cargo compras") '9
        'ComboBox10.Items.Add("Nómina") '10
        Dim Po As New dbContabilidadPolizas(MySqlcon)
        Select Case pModulo
            Case 0
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDs
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 1
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsC
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 2
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDev
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 3
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDevC
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 4
                If pVariable.Contains("SL") Then
                    For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDepSL
                        Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                    Next
                Else
                    If pVariable.Contains("Cr") Then
                        For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDepCr
                            Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                        Next
                    Else
                        If pVariable.Contains("Con") Then
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDepCon
                                Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        Else
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDep
                                Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        End If
                    End If
                End If
            Case 5
                If pVariable.Contains("SL") Then
                    For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRetSL
                        Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                    Next
                Else
                    If pVariable.Contains("Cr") Then
                        For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRetCr
                            Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                        Next
                    Else
                        If pVariable.Contains("Con") Then
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRetCon
                                Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        Else
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRet
                                Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        End If
                    End If
                End If
            Case 6
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCr
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 7
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCrC
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 8
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCA
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 9
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCAC
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 10
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNOM
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
        End Select
    End Sub

    Private Sub GuardaPagoInfo(pModulo As Byte, pIdPoliza As Integer, pidRenglon As Integer, pVariable As String)

        'ComboBox10.Items.Add("Depósitos") '4
        'ComboBox10.Items.Add("Pagos") '5

        Dim Po As New dbContabilidadPolizas(MySqlcon)
        Select Case pModulo
            Case 5
                If pVariable.Contains("SL") Then
                    For Each pago As dbContabilidadMascaras.stChequeTrans In Mascara.PagoInfoRetSL
                        If pago.NumeroCheque = "" Then
                            Po.guardarTransferencia(IdPoliza, pidRenglon, pago.NoCuentaOrigen, pago.IdBancoOrigen, "", pago.NoCuentaDestino, pago.IdBancoDestino, pago.BancoDestinoEx, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                        Else
                            Po.guardarCheque(IdPoliza, pidRenglon, pago.NumeroCheque, pago.IdBancoOrigen, "", pago.NoCuentaOrigen, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                        End If
                    Next
                Else
                    If pVariable.Contains("Cr") Then
                        For Each pago As dbContabilidadMascaras.stChequeTrans In Mascara.PagoInfoRetCr
                            If pago.NumeroCheque = "" Then
                                Po.guardarTransferencia(IdPoliza, pidRenglon, pago.NoCuentaOrigen, pago.IdBancoOrigen, "", pago.NoCuentaDestino, pago.IdBancoDestino, pago.BancoDestinoEx, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                            Else
                                Po.guardarCheque(IdPoliza, pidRenglon, pago.NumeroCheque, pago.IdBancoOrigen, "", pago.NoCuentaOrigen, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                            End If
                        Next
                    Else
                        If pVariable.Contains("Con") Then
                            For Each pago As dbContabilidadMascaras.stChequeTrans In Mascara.PagoInfoRetCon
                                If pago.NumeroCheque = "" Then
                                    Po.guardarTransferencia(IdPoliza, pidRenglon, pago.NoCuentaOrigen, pago.IdBancoOrigen, "", pago.NoCuentaDestino, pago.IdBancoDestino, pago.BancoDestinoEx, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                                Else
                                    Po.guardarCheque(IdPoliza, pidRenglon, pago.NumeroCheque, pago.IdBancoOrigen, "", pago.NoCuentaOrigen, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                                End If
                            Next
                        Else
                            For Each pago As dbContabilidadMascaras.stChequeTrans In Mascara.PagoInfoRet
                                If pago.NumeroCheque = "" Then
                                    Po.guardarTransferencia(IdPoliza, pidRenglon, pago.NoCuentaOrigen, pago.IdBancoOrigen, "", pago.NoCuentaDestino, pago.IdBancoDestino, pago.BancoDestinoEx, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                                Else
                                    Po.guardarCheque(IdPoliza, pidRenglon, pago.NumeroCheque, pago.IdBancoOrigen, "", pago.NoCuentaOrigen, pago.Fecha, pago.Beneficiario, pago.RFC, pago.Monto, pago.IdMoneda, pago.TipodeCambio)
                                End If
                            Next
                        End If
                    End If
                End If

        End Select
    End Sub

    Private Sub GuardaUuidsNeg(pModulo As Byte, pIdPoliza As Integer, pidRenglon As Integer)
        'ComboBox10.Items.Add("Ventas") '0
        'ComboBox10.Items.Add("Compras") '1
        'ComboBox10.Items.Add("Ventas Devoluciones") '2
        'ComboBox10.Items.Add("Compras Devoluciones") '3
        'ComboBox10.Items.Add("Depósitos") '4
        'ComboBox10.Items.Add("Pagos") '5
        'ComboBox10.Items.Add("Notas de crédito ventas") '6
        'ComboBox10.Items.Add("Notas de crédito compras") '7
        'ComboBox10.Items.Add("Notas de cargo ventas") '8
        'ComboBox10.Items.Add("Notas de cargo compras") '9
        'ComboBox10.Items.Add("Nómina") '10
        Dim Po As New dbContabilidadPolizas(MySqlcon)
        Select Case pModulo
            Case 0
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 1
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsCNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 2
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDevNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 3
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDevCNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 4

            Case 5
            Case 6
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCrNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 7
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCrCNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 8
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCANeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 9
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNCACNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
            Case 10
                For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsNOMNeg
                    Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                Next
        End Select
    End Sub

    Private Sub GuardaUuidsPorFicha(pModulo As Byte, pIdPoliza As Integer, pidRenglon As Integer, pVariable As String, pIdDocumento As Integer)
        'ComboBox10.Items.Add("Ventas") '0
        'ComboBox10.Items.Add("Compras") '1
        'ComboBox10.Items.Add("Ventas Devoluciones") '2
        'ComboBox10.Items.Add("Compras Devoluciones") '3
        'ComboBox10.Items.Add("Depósitos") '4
        'ComboBox10.Items.Add("Pagos") '5
        'ComboBox10.Items.Add("Notas de crédito ventas") '6
        'ComboBox10.Items.Add("Notas de crédito compras") '7
        'ComboBox10.Items.Add("Notas de cargo ventas") '8
        'ComboBox10.Items.Add("Notas de cargo compras") '9
        'ComboBox10.Items.Add("Nómina") '10
        Dim Po As New dbContabilidadPolizas(MySqlcon)
        Select Case pModulo
            Case 4
                If pVariable.Contains("SL") Then
                    For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDepSL
                        If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                    Next
                Else
                    If pVariable.Contains("Cr") Or pVariable.Contains("cr") Then
                        For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDepCr
                            If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                        Next
                    Else
                        If pVariable.Contains("Con") Or pVariable.Contains("con") Then
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDepCon
                                If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        Else
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsDep
                                If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        End If
                    End If
                End If
            Case 5
                If pVariable.Contains("SL") Then
                    For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRetSL
                        If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                    Next
                Else
                    If pVariable.Contains("Cr") Or pVariable.Contains("cr") Then
                        For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRetCr
                            If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                        Next
                    Else
                        If pVariable.Contains("Con") Or pVariable.Contains("cr") Then
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRetCon
                                If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        Else
                            For Each Uuid As dbContabilidadMascaras.stUuids In Mascara.UUIDsRet
                                If Uuid.IdDocumento = pIdDocumento Then Po.guardarComprobante(pIdPoliza, pidRenglon, Uuid.Uuid, Uuid.Monto, Uuid.RFC, Uuid.IdMoneda, Uuid.TipodeCambio)
                            Next
                        End If
                    End If
                End If
        End Select
    End Sub
    
End Class
