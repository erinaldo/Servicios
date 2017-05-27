Public Class frmContabilidadGeneraPolizas
    Dim IdsMascaras As New elemento
    Dim Mascara As dbContabilidadMascaras
    'Dim MascaraDetalles As dbContabilidadMascarasDetalles

    'Structure stMascaraDetalles
    '    Dim CodigoV As String
    '    Dim IdCuenta As Integer
    '    Dim Cargo As Double
    '    Dim Abono As Double
    '    Dim Monto As Double
    '    Dim MontoNeg As Double
    '    Dim Modulo As Byte
    '    Dim Cuenta As String
    '    Dim Descripcion As String
    '    Dim Negativo As Byte
    '    Dim Beneficiario As Byte
    'End Structure
    Private Sub frmContabilidadGeneraPolizas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Mascara = New dbContabilidadMascaras(MySqlcon)
        'MascaraDetalles = New dbContabilidadMascarasDetalles(MySqlcon)
        DateTimePicker1.Value = Now.Date
        DateTimePicker2.Value = Now.Date
        ComboBox1.Items.Add("Todos")
        ComboBox1.Items.Add("Mensual")
        ComboBox1.Items.Add("Semanal")
        ComboBox1.Items.Add("Diario")
        ComboBox1.Items.Add("Por Rango")
        ComboBox1.SelectedIndex = 0
        DateTimePicker3.Value = Date.Now
        ConsultaUsadas()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex > 0 Then
            If ComboBox1.SelectedIndex <> 4 Then
                LlenaCombos("tblcontabilidadmascaras", ComboBox3, "titulo", "titu", "idmascara", IdsMascaras, "tipo=" + CStr(ComboBox1.SelectedIndex - 1) + " and activo=1")
            Else
                LlenaCombos("tblcontabilidadmascaras", ComboBox3, "titulo", "titu", "idmascara", IdsMascaras, "tipo=4 and activo=1")
            End If
        Else
            LlenaCombos("tblcontabilidadmascaras", ComboBox3, "titulo", "titu", "idmascara", IdsMascaras, "tipo<>3 and activo=1")
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ChecaMascara()
    End Sub
    Private Sub ChecaMascara()
        Try
            Mascara.ID = IdsMascaras.Valor(ComboBox3.SelectedIndex)
            Mascara.LlenaDatos()
            If Mascara.Tipo = 2 Then
                DateTimePicker2.Visible = False
                Label4.Text = "Día a procesar:"
            Else
                DateTimePicker2.Visible = True
                DateTimePicker2.Enabled = False
                Label4.Text = "Período a procesar:"
            End If
            If Mascara.Tipo = 4 Then
                DateTimePicker2.Visible = True
                DateTimePicker2.Enabled = True
            End If
            'DateTimePicker2.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DateTimePicker1_Leave(sender As Object, e As EventArgs) Handles DateTimePicker1.Leave
        If Mascara.Tipo = 0 Then
            DateTimePicker1.Value = CDate(DateTimePicker1.Value.ToString("yyyy") + "/" + DateTimePicker1.Value.ToString("MM") + "/01")
        End If
        If Mascara.Tipo = 1 Then
            Dim DiaSemana As Integer
            DiaSemana = DateTimePicker1.Value.DayOfWeek
            Select Case DiaSemana
                Case DayOfWeek.Monday
                    DiaSemana = 0
                Case DayOfWeek.Tuesday
                    DiaSemana = -1
                Case DayOfWeek.Wednesday
                    DiaSemana = -2
                Case DayOfWeek.Thursday
                    DiaSemana = -3
                Case DayOfWeek.Friday
                    DiaSemana = -4
                Case DayOfWeek.Saturday
                    DiaSemana = -5
                Case DayOfWeek.Sunday
                    DiaSemana = -6
            End Select
            DateTimePicker1.Value = DateAdd(DateInterval.Day, DiaSemana, DateTimePicker1.Value)
        End If

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If Mascara.Tipo = 0 Then
            Dim Ftemp As Date
            Ftemp = DateAdd(DateInterval.Month, 1, CDate(DateTimePicker1.Value.ToString("yyyy") + "/" + DateTimePicker1.Value.ToString("MM") + "/01"))
            Ftemp = DateAdd(DateInterval.Day, -1, Ftemp)
            DateTimePicker2.Value = Ftemp
        End If
        If Mascara.Tipo = 1 Then
            Dim DiaSemana As Integer
            DiaSemana = DateTimePicker1.Value.DayOfWeek
            Select Case DiaSemana
                Case DayOfWeek.Monday
                    DiaSemana = 6
                Case DayOfWeek.Tuesday
                    DiaSemana = 5
                Case DayOfWeek.Wednesday
                    DiaSemana = 4
                Case DayOfWeek.Thursday
                    DiaSemana = 3
                Case DayOfWeek.Friday
                    DiaSemana = 2
                Case DayOfWeek.Saturday
                    DiaSemana = 1
                Case DayOfWeek.Sunday
                    DiaSemana = 0
            End Select
            DateTimePicker2.Value = DateAdd(DateInterval.Day, DiaSemana, DateTimePicker1.Value)

        End If
    End Sub
    'Private Sub GeneraPolizaGeneral()
    '        Dim Po As New dbContabilidadPolizas(MySqlcon)
    '        Po.llenaDatosConfig()
    '    Dim Variables As New Collection
    '    Dim BeneficiarioStr As String = ""
    '    Dim Bene1 As Boolean = False
    '    Dim Bene2 As Boolean = False
    '    Dim Bene3 As Boolean = False
    '    Dim IdCuentaMov As Integer = 0
    '    Dim CuentaMov As String = ""
    '    Dim Hay5 As Boolean = False
    '        Dim Monto As Double = 0
    '        Dim St As stMascaraDetalles
    '        If Mascara.Tipo = 0 Then
    '            DateTimePicker1.Value = CDate(DateTimePicker1.Value.ToString("yyyy/MM") + "/01")
    '        End If
    '        If Mascara.Tipo = 2 Then
    '            DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    Mascara.PN1 = Po.NNiv1
    '    Mascara.pN2 = Po.NNiv2
    '    Mascara.pN3 = Po.NNiv3
    '    Mascara.pN4 = Po.NNiv4
    '    Mascara.pN5 = Po.NNiv5

    '    Mascara.InicializaVariables()
    '    For Each vmod As Integer In MascaraDetalles.ModulosUsados(Mascara.ID)
    '        Mascara.LlenaVariables(vmod, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), 0, Mascara.Credito, Mascara.IdSucursal)
    '        If vmod = 5 Then
    '            Hay5 = True
    '        End If
    '    Next
    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    dr = MascaraDetalles.ConsultaReader(Mascara.ID, Po.NNiv1, Po.NNiv2, Po.NNiv3, Po.NNiv4, Po.NNiv5)
    '    While dr.Read
    '        St.IdCuenta = dr("idcuenta")
    '        St.Abono = dr("abono")
    '        St.Cargo = dr("cargo")
    '        St.CodigoV = dr("codigo")
    '        St.Modulo = dr("modulo")
    '        St.Cuenta = dr("Cuenta")
    '        St.Descripcion = dr("descripcion")
    '        St.Negativo = dr("negativo")
    '        St.Monto = Mascara.DaValorVariable(St.CodigoV, St.Modulo, False)
    '        St.MontoNeg = Mascara.DaValorVariable(St.CodigoV, St.Modulo, True)
    '        If St.Cargo <> 0 Then Monto += St.Monto - St.MontoNeg
    '        St.Beneficiario = dr("beneficiario")
    '        If St.Modulo = 5 Then
    '            If St.CodigoV = "TDEP" Then
    '                Bene1 = True
    '            End If
    '            If St.CodigoV.Contains("con") Or St.CodigoV.Contains("Con") Then
    '                Bene3 = True
    '            End If
    '            If St.CodigoV.Contains("cr") Or St.CodigoV.Contains("Cr") Then
    '                Bene2 = True
    '            End If
    '        End If
    '        Variables.Add(St)
    '    End While
    '    dr.Close()
    '    If Hay5 Then
    '        BeneficiarioStr = Mascara.InsertayDaProveedoresTemp(Bene1, Bene2, Bene3)
    '    End If
    '    Dim Concepto As String = ""
    '    Dim ConceptoB As String = ""
    '    If Mascara.Tipo = 1 Or Mascara.Tipo = 4 Then
    '        Concepto = "PÓLIZA DEL PERÍODO " + DateTimePicker1.Value.ToString("dd/MM/yyyy") + " AL " + DateTimePicker2.Value.ToString("dd/MM/yyyy")
    '    End If
    '    If Mascara.Tipo = 0 Then
    '        Concepto = "PÓLIZA DEL MES " + DateTimePicker1.Value.ToString("MMMM").ToUpper + " " + DateTimePicker1.Value.ToString("yyyy")
    '    End If
    '    If Mascara.Tipo = 2 Then
    '        Concepto = "PÓLIZA DEL DÍA " + DateTimePicker1.Value.ToString("dd/MM/yyyy")
    '        DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    If Mascara.Modulo = 0 Or Mascara.Modulo = 1 Then
    '        If Mascara.Credito = 0 Then
    '            Concepto += " CONTADO"
    '        Else
    '            If Mascara.Credito = 1 Then
    '                Concepto += " CRÉDITO"
    '            End If
    '        End If
    '    End If
    '    ConceptoB = Concepto + " " + Mascara.StrAfectadas
    '    Po.guardarPoliza(Mascara.TipoPoliza, Po.bucarNumero(DateTimePicker3.Value.Month.ToString("00"), DateTimePicker3.Value.Year.ToString, Mascara.TipoPoliza), DateTimePicker3.Value.ToString("yyyy/MM/dd"), ConceptoB, "", Monto.ToString("0.00"), Mascara.IdClasificacion)
    '    If (Mascara.Canceladas = 0 Or Mascara.Canceladas = 1) Then
    '        For Each stt As stMascaraDetalles In Variables
    '            If stt.CodigoV.Contains("xf") = False And stt.CodigoV <> "TPXC" And stt.CodigoV <> "TDXC" And stt.CodigoV.Contains("TDEP") = False And stt.CodigoV.Contains("TXP") = False Then
    '                If stt.Cargo = 0 Then
    '                    stt.Cargo = -999999999
    '                Else
    '                    stt.Cargo = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Cargo *= -1
    '                End If
    '                If stt.Abono = 0 Then
    '                    stt.Abono = -999999999
    '                Else
    '                    stt.Abono = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Abono *= -1
    '                End If
    '                If stt.Monto <> 0 Then
    '                    If Mascara.Tipo = 3 And stt.IdCuenta < 0 Then
    '                        Po.guardarDetalles(CuentaMov, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), IdCuentaMov, "", 0, "", "", 0, 0, "", "")
    '                    Else
    '                        Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                    End If
    '                End If
    '            Else
    '                Dim MontoMov As Double
    '                Dim CargoTemp As Double
    '                Dim AbonoTemp As Double
    '                If stt.Modulo = 4 Then
    '                    Dim tmpCargo As Double
    '                    Dim tmpAbono As Double
    '                    If stt.CodigoV = "TDEP" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosLista
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        Next
    '                    End If
    '                    If stt.CodigoV = "TDEPcr" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCr
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        Next
    '                    End If
    '                    If stt.CodigoV = "TXP" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosLista
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        Next
    '                    End If

    '                    If stt.CodigoV = "TDEPcon" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCon
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        Next
    '                    End If
    '                    If stt.CodigoV = "TXPcon" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosListaCon
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '                If stt.Modulo = 5 Then
    '                    Dim tmpCargo As Double
    '                    Dim tmpAbono As Double
    '                    If stt.CodigoV = "TDEP" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosLista
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                If stt.Beneficiario = 0 Then
    '                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                    If stt.CodigoV = "TXP" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosLista
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                If stt.Beneficiario = 0 Then
    '                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If



    '                    If stt.CodigoV = "TDEPcr" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCr
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                If stt.Beneficiario = 0 Then
    '                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If



    '                    If stt.CodigoV = "TDEPcon" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCon
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                If stt.Beneficiario = 0 Then
    '                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                    If stt.CodigoV = "TXPcon" Then
    '                        For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosListaCon
    '                            If stt.Cargo = 0 Then
    '                                tmpCargo = -999999999
    '                            Else
    '                                tmpCargo = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpCargo *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                tmpAbono = -999999999
    '                            Else
    '                                tmpAbono = cn.Cantidad
    '                                If stt.Negativo = 1 Then tmpAbono *= -1
    '                            End If
    '                            If stt.IdCuenta > 0 Then
    '                                cn.Idcuenta = stt.IdCuenta
    '                                cn.Cuenta = stt.Cuenta
    '                            End If
    '                            If cn.Cantidad <> 0 Then
    '                                If stt.Beneficiario = 0 Then
    '                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                End If
    '                If stt.CodigoV = "TPXC" Then
    '                        For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TPxC
    '                            If stt.Cargo = 0 Then
    '                                CargoTemp = -999999999
    '                            Else
    '                                CargoTemp = cn.importe
    '                                If stt.Negativo = 1 Then CargoTemp *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                AbonoTemp = -999999999
    '                            Else
    '                                AbonoTemp = cn.importe
    '                                If stt.Negativo = 1 Then AbonoTemp *= -1
    '                            End If
    '                            If stt.Monto <> 0 Then
    '                                If stt.IdCuenta < 0 Then
    '                                    Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                Po.guardarDetalles(stt.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next


    '                End If
    '                If stt.CodigoV = "TDXC" Then
    '                    For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TDxC
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            CargoTemp = cn.importe
    '                            If stt.Negativo = 1 Then CargoTemp *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            AbonoTemp = cn.importe
    '                            If stt.Negativo = 1 Then AbonoTemp *= -1
    '                        End If
    '                        If stt.Monto <> 0 Then
    '                            If stt.IdCuenta < 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(stt.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If


    '                    If stt.CodigoV.Contains("netoxf") Then
    '                        For Each Factura As dbContabilidadMascaras.stDepositos In Mascara.FacturasLista
    '                            Select Case stt.CodigoV
    '                                Case "TNnetoxf"
    '                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                                Case "TSInetoxf"
    '                                    MontoMov = Factura.Cantidad
    '                                Case "TIVAnetoxf"
    '                                    MontoMov = Factura.Iva
    '                                Case "TIEPSnetoxf"
    '                                    MontoMov = Factura.Ieps
    '                                Case "TIVARnetoxf"
    '                                    MontoMov = Factura.Retendido
    '                                Case "TISRnetoxf"
    '                                    MontoMov = Factura.ISR
    '                                Case "TIVANnetoxf"
    '                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                            End Select
    '                            If stt.Cargo = 0 Then
    '                                CargoTemp = -999999999
    '                            Else
    '                                CargoTemp = MontoMov
    '                                If stt.Negativo = 1 Then CargoTemp *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                AbonoTemp = -999999999
    '                            Else
    '                                AbonoTemp = MontoMov
    '                                If stt.Negativo = 1 Then AbonoTemp *= -1
    '                            End If
    '                            If MontoMov <> 0 Then
    '                                If stt.IdCuenta > 0 Then
    '                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                    If stt.CodigoV.Contains("Conxf") Then
    '                        For Each Factura As dbContabilidadMascaras.stDepositos In Mascara.FacturasListaCon
    '                            Select Case stt.CodigoV
    '                                Case "TNConxf"
    '                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                                Case "TSIConxf"
    '                                    MontoMov = Factura.Cantidad
    '                                Case "TIVAConxf"
    '                                    MontoMov = Factura.Iva
    '                                Case "TIEPSConxf"
    '                                    MontoMov = Factura.Ieps
    '                                Case "TIVARConxf"
    '                                    MontoMov = Factura.Retendido
    '                                Case "TISRConxf"
    '                                    MontoMov = Factura.ISR
    '                                Case "TIVANConxf"
    '                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                            End Select
    '                            If stt.Cargo = 0 Then
    '                                CargoTemp = -999999999
    '                            Else
    '                                CargoTemp = MontoMov
    '                                If stt.Negativo = 1 Then CargoTemp *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                AbonoTemp = -999999999
    '                            Else
    '                                AbonoTemp = MontoMov
    '                                If stt.Negativo = 1 Then AbonoTemp *= -1
    '                            End If
    '                            If MontoMov <> 0 Then
    '                                If stt.IdCuenta > 0 Then
    '                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '                    If stt.CodigoV.Contains("Crxf") Then
    '                        For Each Factura As dbContabilidadMascaras.stDepositos In Mascara.FacturasListaCr
    '                            Select Case stt.CodigoV
    '                                Case "TNCrxf"
    '                                    MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                                Case "TSICrxf"
    '                                    MontoMov = Factura.Cantidad
    '                                Case "TIVACrxf"
    '                                    MontoMov = Factura.Iva
    '                                Case "TIEPSCrxf"
    '                                    MontoMov = Factura.Ieps
    '                                Case "TIVARCrxf"
    '                                    MontoMov = Factura.Retendido
    '                                Case "TISRCrxf"
    '                                    MontoMov = Factura.ISR
    '                                Case "TIVANCrxf"
    '                                    MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                            End Select
    '                            If stt.Cargo = 0 Then
    '                                CargoTemp = -999999999
    '                            Else
    '                                CargoTemp = MontoMov
    '                                If stt.Negativo = 1 Then CargoTemp *= -1
    '                            End If
    '                            If stt.Abono = 0 Then
    '                                AbonoTemp = -999999999
    '                            Else
    '                                AbonoTemp = MontoMov
    '                                If stt.Negativo = 1 Then AbonoTemp *= -1
    '                            End If
    '                            If MontoMov <> 0 Then
    '                                If stt.IdCuenta > 0 Then
    '                                    Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                                Else
    '                                    Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                                End If
    '                            End If
    '                        Next
    '                    End If
    '            End If
    '        Next
    '    End If
    '    If Mascara.TNNeg <> 0 And (Mascara.Canceladas = 0 Or Mascara.Canceladas = 2) Then
    '        ConceptoB = Concepto + " " + Mascara.strAfectadasNeg
    '        For Each stt As stMascaraDetalles In Variables
    '            If stt.CodigoV.Contains("xf") = False Then
    '                If stt.Cargo = 0 Then
    '                    stt.Cargo = -999999999
    '                Else
    '                    If stt.Negativo = 0 Then
    '                        stt.Cargo = stt.MontoNeg * -1
    '                    Else
    '                        stt.Cargo = stt.MontoNeg
    '                    End If
    '                End If
    '                If stt.Abono = 0 Then
    '                    stt.Abono = -999999999
    '                Else
    '                    If stt.Negativo = 0 Then
    '                        stt.Abono = stt.MontoNeg * -1
    '                    Else
    '                        stt.Abono = stt.MontoNeg
    '                    End If
    '                End If
    '                If stt.MontoNeg <> 0 Then
    '                    Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                End If
    '            Else

    '                Dim MontoMov As Double
    '                Dim CargoTemp As Double
    '                Dim AbonoTemp As Double
    '                If stt.CodigoV.Contains("netoxf") Then
    '                    For Each Factura As dbContabilidadMascaras.stDepositos In Mascara.FacturasListaNeg
    '                        Select Case stt.CodigoV
    '                            Case "TNnetoxf"
    '                                MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                            Case "TSInetoxf"
    '                                MontoMov = Factura.Cantidad
    '                            Case "TIVAnetoxf"
    '                                MontoMov = Factura.Iva
    '                            Case "TIEPSnetoxf"
    '                                MontoMov = Factura.Ieps
    '                            Case "TIVARnetoxf"
    '                                MontoMov = Factura.Retendido
    '                            Case "TISRnetoxf"
    '                                MontoMov = Factura.ISR
    '                            Case "TIVANnetoxf"
    '                                MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                        End Select
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                CargoTemp = MontoMov * -1
    '                            Else
    '                                CargoTemp = MontoMov
    '                            End If
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                AbonoTemp = MontoMov * -1
    '                            Else
    '                                AbonoTemp = MontoMov
    '                            End If
    '                        End If
    '                        If MontoMov <> 0 Then
    '                            If stt.IdCuenta > 0 Then
    '                                Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '                If stt.CodigoV.Contains("Conxf") Then
    '                    For Each Factura As dbContabilidadMascaras.stDepositos In Mascara.FacturasListaNegCon
    '                        Select Case stt.CodigoV
    '                            Case "TNConxf"
    '                                MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                            Case "TSIConxf"
    '                                MontoMov = Factura.Cantidad
    '                            Case "TIVAConxf"
    '                                MontoMov = Factura.Iva
    '                            Case "TIEPSConxf"
    '                                MontoMov = Factura.Ieps
    '                            Case "TIVARConxf"
    '                                MontoMov = Factura.Retendido
    '                            Case "TISRConxf"
    '                                MontoMov = Factura.ISR
    '                            Case "TIVANConxf"
    '                                MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                        End Select
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                CargoTemp = MontoMov * -1
    '                            Else
    '                                CargoTemp = MontoMov
    '                            End If
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                AbonoTemp = MontoMov * -1
    '                            Else
    '                                AbonoTemp = MontoMov
    '                            End If
    '                        End If
    '                        If MontoMov <> 0 Then
    '                            If stt.IdCuenta > 0 Then
    '                                Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '                If stt.CodigoV.Contains("Crxf") Then
    '                    For Each Factura As dbContabilidadMascaras.stDepositos In Mascara.FacturasListaNegCr
    '                        Select Case stt.CodigoV
    '                            Case "TNCrxf"
    '                                MontoMov = Factura.Cantidad + Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                            Case "TSICrxf"
    '                                MontoMov = Factura.Cantidad
    '                            Case "TIVACrxf"
    '                                MontoMov = Factura.Iva
    '                            Case "TIEPSCrxf"
    '                                MontoMov = Factura.Ieps
    '                            Case "TIVARCrxf"
    '                                MontoMov = Factura.Retendido
    '                            Case "TISRCrxf"
    '                                MontoMov = Factura.ISR
    '                            Case "TIVANCrxf"
    '                                MontoMov = Factura.Iva + Factura.Ieps - Factura.ISR - Factura.Retendido
    '                        End Select
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                CargoTemp = MontoMov * -1
    '                            Else
    '                                CargoTemp = MontoMov
    '                            End If
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                AbonoTemp = MontoMov * -1
    '                            Else
    '                                AbonoTemp = MontoMov
    '                            End If
    '                        End If
    '                        If MontoMov <> 0 Then
    '                            If stt.IdCuenta > 0 Then
    '                                Po.guardarDetalles(stt.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(Factura.Cuenta, Factura.Concepto, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), Factura.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If

    '            End If

    '        Next
    '    End If
    '    Po.ActualizaMontoPoliza(Po.IDPoliza)
    '    Mascara.GuardaUsada(Po.IDPoliza, Mascara.ID, DateTimePicker3.Value.ToString("yyyy/MM/dd"))
    '    'End Select
    '    ConsultaUsadas()
    '    PopUp("Póliza generada", 90)
    'End Sub
    'Private Sub GeneraPolizasnominas()
    '    Dim Po As New dbContabilidadPolizas(MySqlcon)
    '    Po.llenaDatosConfig()
    '    Dim Variables As New Collection
    '    Dim Monto As Double = 0
    '    Dim St As stMascaraDetalles
    '    If Mascara.Tipo = 0 Then
    '        DateTimePicker1.Value = CDate(DateTimePicker1.Value.ToString("yyyy/MM") + "/01")
    '    End If
    '    If Mascara.Tipo = 2 Then
    '        DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    Mascara.PN1 = Po.NNiv1
    '    Mascara.pN2 = Po.NNiv2
    '    Mascara.pN3 = Po.NNiv3
    '    Mascara.pN4 = Po.NNiv4
    '    Mascara.pN5 = Po.NNiv5
    '    Mascara.LlenaVariables(Mascara.Modulo, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), 0, Mascara.Credito, Mascara.IdSucursal)
    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    dr = MascaraDetalles.ConsultaReader(Mascara.ID, Po.NNiv1, Po.NNiv2, Po.NNiv3, Po.NNiv4, Po.NNiv5)
    '    Mascara.PN1 = Po.NNiv1
    '    Mascara.pN2 = Po.NNiv2
    '    Mascara.pN3 = Po.NNiv3
    '    Mascara.pN4 = Po.NNiv4
    '    Mascara.pN5 = Po.NNiv5
    '    While dr.Read
    '        St.IdCuenta = dr("idcuenta")
    '        St.Abono = dr("abono")
    '        St.Cargo = dr("cargo")
    '        St.CodigoV = dr("codigo")
    '        St.Modulo = dr("modulo")
    '        St.Cuenta = dr("Cuenta")
    '        St.Descripcion = dr("descripcion")
    '        St.Negativo = dr("negativo")
    '        St.Beneficiario = 0
    '        St.Monto = Mascara.DaValorVariable(St.CodigoV, St.Modulo, False)
    '        St.MontoNeg = Mascara.DaValorVariable(St.CodigoV, St.Modulo, True)
    '        If St.Cargo <> 0 Then Monto += St.Monto - St.MontoNeg
    '        Variables.Add(St)
    '    End While
    '    dr.Close()
    '    Dim Concepto As String = ""
    '    Dim ConceptoB As String = ""
    '    If Mascara.Tipo = 1 Or Mascara.Tipo = 4 Then
    '        Concepto = "PÓLIZA DEL PERÍODO " + DateTimePicker1.Value.ToString("dd/MM/yyyy") + " AL " + DateTimePicker2.Value.ToString("dd/MM/yyyy")
    '    End If
    '    If Mascara.Tipo = 0 Then
    '        Concepto = "PÓLIZA DEL MES " + DateTimePicker1.Value.ToString("MMMM").ToUpper + " " + DateTimePicker1.Value.ToString("yyyy")
    '    End If
    '    If Mascara.Tipo = 2 Then
    '        Concepto = "PÓLIZA DEL DÍA " + DateTimePicker1.Value.ToString("dd/MM/yyyy")
    '        DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    'If Mascara.Credito = 0 Then
    '    '    Concepto += " CONTADO"
    '    'Else
    '    '    Concepto += " CRÉDITO"
    '    'End If
    '    ConceptoB = Concepto + " " + Mascara.StrAfectadas
    '    Po.guardarPoliza(Mascara.TipoPoliza, Po.bucarNumero(DateTimePicker3.Value.Month.ToString("00"), DateTimePicker3.Value.Year.ToString, Mascara.TipoPoliza), DateTimePicker3.Value.ToString("yyyy/MM/dd"), ConceptoB, "", Monto.ToString("0.00"), Mascara.IdClasificacion)
    '    Dim CargoTemp As Double = 0
    '    Dim AbonoTemp As Double = 0
    '    If (Mascara.Canceladas = 0 Or Mascara.Canceladas = 1) Then
    '        For Each stt As stMascaraDetalles In Variables
    '            If stt.IdCuenta > 0 Then
    '                If stt.Cargo = 0 Then
    '                    stt.Cargo = -999999999
    '                Else
    '                    stt.Cargo = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Cargo *= -1
    '                End If
    '                If stt.Abono = 0 Then
    '                    stt.Abono = -999999999
    '                Else
    '                    stt.Abono = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Abono *= -1
    '                End If
    '                If stt.Monto <> 0 Then
    '                    Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                End If
    '            Else

    '                If stt.IdCuenta = -1 Then
    '                    For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TPxC
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            CargoTemp = cn.importe
    '                            If stt.Negativo = 1 Then CargoTemp *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            AbonoTemp = cn.importe
    '                            If stt.Negativo = 1 Then AbonoTemp *= -1
    '                        End If
    '                        If stt.Monto <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If
    '                If stt.IdCuenta = -2 Then
    '                    For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TDxC
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            CargoTemp = cn.importe
    '                            If stt.Negativo = 1 Then CargoTemp *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            AbonoTemp = cn.importe
    '                            If stt.Negativo = 1 Then AbonoTemp *= -1
    '                        End If
    '                        If stt.Monto <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Next
    '    End If
    '    If Mascara.TNNeg <> 0 And (Mascara.Canceladas = 0 Or Mascara.Canceladas = 2) Then
    '        ConceptoB = Concepto + " " + Mascara.strAfectadasNeg
    '        For Each stt As stMascaraDetalles In Variables
    '            If stt.IdCuenta > 0 Then
    '                If stt.Cargo = 0 Then
    '                    CargoTemp = -999999999
    '                Else
    '                    If stt.Negativo = 0 Then
    '                        CargoTemp = stt.MontoNeg * -1
    '                    Else
    '                        CargoTemp = stt.MontoNeg
    '                    End If
    '                End If
    '                If stt.Abono = 0 Then
    '                    AbonoTemp = -999999999
    '                Else
    '                    If stt.Negativo = 0 Then
    '                        AbonoTemp = stt.MontoNeg * -1
    '                    Else
    '                        AbonoTemp = stt.MontoNeg
    '                    End If
    '                End If
    '                If stt.MontoNeg <> 0 Then
    '                    Po.guardarDetalles(stt.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                End If
    '            Else
    '                If stt.IdCuenta = -1 Then
    '                    For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TPxCNeg
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                CargoTemp = cn.importe * -1
    '                            Else
    '                                CargoTemp = cn.importe
    '                            End If
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                AbonoTemp = cn.importe * -1
    '                            Else
    '                                AbonoTemp = cn.importe
    '                            End If
    '                        End If
    '                        If stt.Monto <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If
    '                If stt.IdCuenta = -2 Then
    '                    For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TDxCNeg
    '                        If stt.Cargo = 0 Then
    '                            CargoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                CargoTemp = cn.importe * -1
    '                            Else
    '                                CargoTemp = cn.importe
    '                            End If
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            AbonoTemp = -999999999
    '                        Else
    '                            If stt.Negativo = 0 Then
    '                                AbonoTemp = cn.importe * -1
    '                            Else
    '                                AbonoTemp = cn.importe
    '                            End If
    '                        End If
    '                        If stt.Monto <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, CargoTemp.ToString("0.00"), AbonoTemp.ToString("0.00"), cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If

    '            End If
    '        Next
    '    End If
    '    Po.ActualizaMontoPoliza(Po.IDPoliza)
    '    Mascara.GuardaUsada(Po.IDPoliza, Mascara.ID, DateTimePicker3.Value.ToString("yyyy/MM/dd"))
    '    'End Select
    '    ConsultaUsadas()
    '    PopUp("Póliza generada", 90)
    'End Sub
    'Private Sub GeneraPolizasDepositos()
    '    Dim Po As New dbContabilidadPolizas(MySqlcon)
    '    Po.llenaDatosConfig()
    '    Dim Variables As New Collection
    '    Dim Monto As Double = 0
    '    Dim St As stMascaraDetalles
    '    If Mascara.Tipo = 0 Then
    '        DateTimePicker1.Value = CDate(DateTimePicker1.Value.ToString("yyyy/MM") + "/01")
    '    End If
    '    If Mascara.Tipo = 2 Then
    '        DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    Mascara.PN1 = Po.NNiv1
    '    Mascara.pN2 = Po.NNiv2
    '    Mascara.pN3 = Po.NNiv3
    '    Mascara.pN4 = Po.NNiv4
    '    Mascara.pN5 = Po.NNiv5
    '    Mascara.LlenaVariables(Mascara.Modulo, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), 0, Mascara.Credito, Mascara.IdSucursal)
    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    dr = MascaraDetalles.ConsultaReader(Mascara.ID, Po.NNiv1, Po.NNiv2, Po.NNiv3, Po.NNiv4, Po.NNiv5)
    '    While dr.Read
    '        St.IdCuenta = dr("idcuenta")
    '        St.Abono = dr("abono")
    '        St.Cargo = dr("cargo")
    '        St.CodigoV = dr("codigo")
    '        St.Modulo = dr("modulo")
    '        St.Cuenta = dr("Cuenta")
    '        St.Descripcion = dr("descripcion")
    '        St.Negativo = dr("negativo")
    '        St.Beneficiario = dr("beneficiario")
    '        St.Monto = Mascara.DaValorVariable(St.CodigoV, St.Modulo, False)
    '        'St.MontoNeg = Mascara.DaValorVariabla(St.CodigoV, St.Modulo, True)
    '        If St.Cargo <> 0 Then Monto += St.Monto '- St.MontoNeg
    '        Variables.Add(St)
    '    End While
    '    dr.Close()
    '    Dim Concepto As String = ""
    '    Dim ConceptoB As String = ""
    '    If Mascara.Tipo = 1 Or Mascara.Tipo = 4 Then
    '        Concepto = "PÓLIZA DEL PERÍODO " + DateTimePicker1.Value.ToString("dd/MM/yyyy") + " AL " + DateTimePicker2.Value.ToString("dd/MM/yyyy")
    '    End If
    '    If Mascara.Tipo = 0 Then
    '        Concepto = "PÓLIZA DEL MES " + DateTimePicker1.Value.ToString("MMMM").ToUpper + " " + DateTimePicker1.Value.ToString("yyyy")
    '    End If
    '    If Mascara.Tipo = 2 Then
    '        Concepto = "PÓLIZA DEL DÍA " + DateTimePicker1.Value.ToString("dd/MM/yyyy")
    '        DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    'If Mascara.Credito = 0 Then
    '    '    Concepto += " CONTADO"
    '    'Else
    '    '    Concepto += " CRÉDITO"
    '    'End If
    '    ConceptoB = Concepto + " " + Mascara.StrAfectadas
    '    Po.guardarPoliza(Mascara.TipoPoliza, Po.bucarNumero(DateTimePicker3.Value.Month.ToString("00"), DateTimePicker3.Value.Year.ToString, Mascara.TipoPoliza), DateTimePicker3.Value.ToString("yyyy/MM/dd"), ConceptoB, "", Monto.ToString("0.00"), Mascara.IdClasificacion)
    '    If (Mascara.Canceladas = 0 Or Mascara.Canceladas = 1) Then
    '        For Each stt As stMascaraDetalles In Variables
    '            If stt.CodigoV.Contains("TDEP") = False And stt.CodigoV.Contains("TXP") = False Then
    '                If stt.Cargo = 0 Then
    '                    stt.Cargo = -999999999
    '                Else
    '                    stt.Cargo = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Cargo *= -1
    '                End If
    '                If stt.Abono = 0 Then
    '                    stt.Abono = -999999999
    '                Else
    '                    stt.Abono = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Abono *= -1
    '                End If
    '                If stt.Monto <> 0 Then
    '                    Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                End If
    '            Else
    '                Dim tmpCargo As Double
    '                Dim tmpAbono As Double
    '                If stt.CodigoV = "TDEP" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosLista
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If
    '                If stt.CodigoV = "TDEPcr" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCr
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If
    '                If stt.CodigoV = "TXP" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosLista
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If

    '                If stt.CodigoV = "TDEPcon" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCon
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If
    '                If stt.CodigoV = "TXPcon" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosListaCon
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                        End If
    '                    Next
    '                End If

    '            End If
    '        Next
    '    End If
    '    'If Mascara.TNNeg <> 0 And (Mascara.Canceladas = 0 Or Mascara.Canceladas = 2) Then
    '    '    ConceptoB = Concepto + " " + Mascara.strAfectadasNeg
    '    '    For Each stt As stMascaraDetalles In Variables
    '    '        If stt.IdCuenta > 0 Then
    '    '            If stt.Cargo = 0 Then
    '    '                stt.Cargo = -999999999
    '    '            Else
    '    '                stt.Cargo = stt.MontoNeg * -1
    '    '            End If
    '    '            If stt.Abono = 0 Then
    '    '                stt.Abono = -999999999
    '    '            Else
    '    '                stt.Abono = stt.MontoNeg * -1
    '    '            End If
    '    '            If stt.MontoNeg <> 0 Then
    '    '                Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo, stt.Abono, stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '    '            End If
    '    '        Else
    '    '            If stt.IdCuenta = -1 Then
    '    '                For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TPxCNeg
    '    '                    If stt.Cargo = 0 Then
    '    '                        stt.Cargo = -999999999
    '    '                    Else
    '    '                        stt.Cargo = cn.importe
    '    '                    End If
    '    '                    If stt.Abono = 0 Then
    '    '                        stt.Abono = -999999999
    '    '                    Else
    '    '                        stt.Abono = cn.importe
    '    '                    End If
    '    '                    If stt.Monto <> 0 Then
    '    '                        Po.guardarDetalles(cn.Cuenta, ConceptoB, stt.Cargo, stt.Abono, cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '    '                    End If
    '    '                Next
    '    '            End If
    '    '            If stt.IdCuenta = -2 Then
    '    '                For Each cn As dbContabilidadMascaras.conceptosNomina In Mascara.TDxCNeg
    '    '                    If stt.Cargo = 0 Then
    '    '                        stt.Cargo = -999999999
    '    '                    Else
    '    '                        stt.Cargo = cn.importe
    '    '                    End If
    '    '                    If stt.Abono = 0 Then
    '    '                        stt.Abono = -999999999
    '    '                    Else
    '    '                        stt.Abono = cn.importe
    '    '                    End If
    '    '                    If stt.Monto <> 0 Then
    '    '                        Po.guardarDetalles(cn.Cuenta, ConceptoB, stt.Cargo, stt.Abono, cn.idcuenta, "", 0, "", "", 0, 0, "", "")
    '    '                    End If
    '    '                Next
    '    '            End If

    '    '        End If
    '    '    Next
    '    'End If
    '    Po.ActualizaMontoPoliza(Po.IDPoliza)
    '    Mascara.GuardaUsada(Po.IDPoliza, Mascara.ID, DateTimePicker3.Value.ToString("yyyy/MM/dd"))
    '    'End Select
    '    ConsultaUsadas()
    '    PopUp("Póliza generada", 90)
    'End Sub
    'Private Sub GeneraPolizasRetiros()
    '    Dim Po As New dbContabilidadPolizas(MySqlcon)
    '    Po.llenaDatosConfig()
    '    Dim Variables As New Collection
    '    'Dim Beneficiario As String
    '    Dim Monto As Double = 0
    '    Dim BeneficiarioStr As String = ""
    '    Dim Bene1 As Boolean = False
    '    Dim Bene2 As Boolean = False
    '    Dim Bene3 As Boolean = False
    '    Dim St As stMascaraDetalles
    '    If Mascara.Tipo = 0 Then
    '        DateTimePicker1.Value = CDate(DateTimePicker1.Value.ToString("yyyy/MM") + "/01")
    '    End If
    '    If Mascara.Tipo = 2 Then
    '        DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    Mascara.PN1 = Po.NNiv1
    '    Mascara.pN2 = Po.NNiv2
    '    Mascara.pN3 = Po.NNiv3
    '    Mascara.pN4 = Po.NNiv4
    '    Mascara.pN5 = Po.NNiv5
    '    Mascara.LlenaVariables(Mascara.Modulo, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), 0, Mascara.Credito, Mascara.IdSucursal)
    '    Dim dr As MySql.Data.MySqlClient.MySqlDataReader
    '    dr = MascaraDetalles.ConsultaReader(Mascara.ID, Po.NNiv1, Po.NNiv2, Po.NNiv3, Po.NNiv4, Po.NNiv5)
    '    While dr.Read
    '        St.IdCuenta = dr("idcuenta")
    '        St.Abono = dr("abono")
    '        St.Cargo = dr("cargo")
    '        St.CodigoV = dr("codigo")
    '        St.Modulo = dr("modulo")
    '        St.Cuenta = dr("Cuenta")
    '        St.Descripcion = dr("descripcion")
    '        St.Negativo = dr("negativo")
    '        St.Beneficiario = dr("beneficiario")
    '        If St.CodigoV = "TDEP" Then
    '            Bene1 = True
    '        End If
    '        If St.CodigoV.Contains("con") Or St.CodigoV.Contains("Con") Then
    '            Bene3 = True
    '        End If
    '        If St.CodigoV.Contains("cr") Or St.CodigoV.Contains("Cr") Then
    '            Bene2 = True
    '        End If
    '        St.Monto = Mascara.DaValorVariable(St.CodigoV, St.Modulo, False)
    '        'St.MontoNeg = Mascara.DaValorVariabla(St.CodigoV, St.Modulo, True)
    '        If St.Cargo <> 0 Then Monto += St.Monto '- St.MontoNeg
    '        Variables.Add(St)
    '    End While
    '    dr.Close()
    '    Dim Concepto As String = ""
    '    Dim ConceptoB As String = ""
    '    BeneficiarioStr = Mascara.InsertayDaProveedoresTemp(Bene1, Bene2, Bene3)
    '    If Mascara.Tipo = 1 Or Mascara.Tipo = 4 Then
    '        Concepto = "PÓLIZA DEL PERÍODO " + DateTimePicker1.Value.ToString("dd/MM/yyyy") + " AL " + DateTimePicker2.Value.ToString("dd/MM/yyyy")
    '    End If
    '    If Mascara.Tipo = 0 Then
    '        Concepto = "PÓLIZA DEL MES " + DateTimePicker1.Value.ToString("MMMM").ToUpper + " " + DateTimePicker1.Value.ToString("yyyy")
    '    End If
    '    If Mascara.Tipo = 2 Then
    '        Concepto = "PÓLIZA DEL DÍA " + DateTimePicker1.Value.ToString("dd/MM/yyyy")
    '        DateTimePicker2.Value = DateTimePicker1.Value
    '    End If
    '    'If Mascara.Credito = 0 Then
    '    '    Concepto += " CONTADO"
    '    'Else
    '    '    Concepto += " CRÉDITO"
    '    'End If
    '    ConceptoB = Concepto + " " + Mascara.StrAfectadas
    '    Po.guardarPoliza(Mascara.TipoPoliza, Po.bucarNumero(DateTimePicker3.Value.Month.ToString("00"), DateTimePicker3.Value.Year.ToString, Mascara.TipoPoliza), DateTimePicker3.Value.ToString("yyyy/MM/dd"), ConceptoB, BeneficiarioStr, Monto.ToString("0.00"), Mascara.IdClasificacion)
    '    If (Mascara.Canceladas = 0 Or Mascara.Canceladas = 1) Then
    '        For Each stt As stMascaraDetalles In Variables
    '            If stt.CodigoV.Contains("TDEP") = False And stt.CodigoV.Contains("TXP") = False Then
    '                If stt.Cargo = 0 Then
    '                    stt.Cargo = -999999999
    '                Else
    '                    stt.Cargo = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Cargo *= -1
    '                End If
    '                If stt.Abono = 0 Then
    '                    stt.Abono = -999999999
    '                Else
    '                    stt.Abono = stt.Monto
    '                    If stt.Negativo = 1 Then stt.Abono *= -1
    '                End If
    '                If stt.Monto <> 0 Then
    '                    If stt.Beneficiario = 0 Then
    '                        Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                    Else
    '                        Po.guardarDetalles(stt.Cuenta, ConceptoB, stt.Cargo.ToString("0.00"), stt.Abono.ToString("0.00"), stt.IdCuenta, "", 0, "", "", 0, 0, "", "")
    '                    End If
    '                End If
    '            Else
    '                Dim tmpCargo As Double
    '                Dim tmpAbono As Double
    '                If stt.CodigoV = "TDEP" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosLista
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            If stt.Beneficiario = 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '                If stt.CodigoV = "TXP" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosLista
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            If stt.Beneficiario = 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If



    '                If stt.CodigoV = "TDEPcr" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCr
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            If stt.Beneficiario = 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If



    '                If stt.CodigoV = "TDEPcon" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.DepositosListaCon
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            If stt.Beneficiario = 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '                If stt.CodigoV = "TXPcon" Then
    '                    For Each cn As dbContabilidadMascaras.stDepositos In Mascara.PagosListaCon
    '                        If stt.Cargo = 0 Then
    '                            tmpCargo = -999999999
    '                        Else
    '                            tmpCargo = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpCargo *= -1
    '                        End If
    '                        If stt.Abono = 0 Then
    '                            tmpAbono = -999999999
    '                        Else
    '                            tmpAbono = cn.Cantidad
    '                            If stt.Negativo = 1 Then tmpAbono *= -1
    '                        End If
    '                        If stt.IdCuenta > 0 Then
    '                            cn.Idcuenta = stt.IdCuenta
    '                            cn.Cuenta = stt.Cuenta
    '                        End If
    '                        If cn.Cantidad <> 0 Then
    '                            If stt.Beneficiario = 0 Then
    '                                Po.guardarDetalles(cn.Cuenta, ConceptoB, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            Else
    '                                Po.guardarDetalles(cn.Cuenta, BeneficiarioStr, tmpCargo.ToString("0.00"), tmpAbono.ToString("0.00"), cn.Idcuenta, "", 0, "", "", 0, 0, "", "")
    '                            End If
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Next
    '    End If

    '    Po.ActualizaMontoPoliza(Po.IDPoliza)
    '    Mascara.GuardaUsada(Po.IDPoliza, Mascara.ID, DateTimePicker3.Value.ToString("yyyy/MM/dd"))
    '    'End Select
    '    ConsultaUsadas()
    '    PopUp("Póliza generada", 90)
    'End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If MsgBox("¿Generar la poliza seleccionada?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If Mascara.Tipo = 0 Then
                    DateTimePicker1.Value = CDate(DateTimePicker1.Value.ToString("yyyy/MM") + "/01")
                End If
                
                If Mascara.Tipo = 2 Then
                    DateTimePicker2.Value = DateTimePicker1.Value
                End If
                If CheckBox1.Checked = False Then
                    Dim Generador As New dbContabilidadGeneraPolizas(Mascara, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), DateTimePicker3.Value.ToString("yyyy/MM/dd"))
                    Generador.GeneraPolizaGeneral(0, 0, 0, 0, 0, 0, 0)
                    ConsultaUsadas()
                Else
                    '0 mensual
                    '1 semanal
                    '2 diario
                    '4 rango
                    If Mascara.Tipo = 4 Then
                        MsgBox("No se pueden generar de forma masiva máscaras de tipo: POR RANGO.", MsgBoxStyle.Information, GlobalNombreApp)
                        Exit Sub
                    End If
                    Dim Fecha1 As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
                    Dim Fecha2 As String = DateTimePicker2.Value.ToString("yyyy/MM/dd")
                    Dim Fecha4 As String = DateTimePicker5.Value.ToString("yyyy/MM/dd")
                    Dim Ftemp As Date
                    If Mascara.Tipo = 2 Then
                        Fecha4 = DateAdd(DateInterval.Day, 1, DateTimePicker5.Value).ToString("yyyy/MM/dd")
                    End If
                    Dim PP As Integer = 0
                    While Fecha2 < Fecha4
                        Dim Generador As New dbContabilidadGeneraPolizas(Mascara, Fecha1, Fecha2, Fecha2)
                        Generador.GeneraPolizaGeneral(0, 0, 0, 0, 0, 0, 0)
                        Select Case Mascara.Tipo
                            Case 0
                                Fecha1 = DateAdd(DateInterval.Month, 1, CDate(Fecha1)).ToString("yyyy/MM/dd")
                                Ftemp = DateAdd(DateInterval.Month, 1, CDate(Fecha1))
                                Ftemp = DateAdd(DateInterval.Day, -1, Ftemp)
                                Fecha2 = Ftemp.ToString("yyyy/MM/dd")
                            Case 1
                                Fecha1 = DateAdd(DateInterval.Day, 8, CDate(Fecha1))
                                Fecha2 = DateAdd(DateInterval.Day, 7, CDate(Fecha1)).ToString("yyyy/MM/dd")
                            Case 2
                                Fecha1 = DateAdd(DateInterval.Day, 1, CDate(Fecha1)).ToString("yyyy/MM/dd")
                                Fecha2 = Fecha1
                        End Select
                        PP += 1
                        Label6.Text = "Periodos procesados: " + PP.ToString
                        Application.DoEvents()
                    End While
                    ConsultaUsadas()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub ConsultaUsadas()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
            'Dim P As New dbFertilizantesMovimientos(MySqlcon)
            DGDetalles.DataSource = Mascara.ConsultaUsadas
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).HeaderText = "Máscara"
            DGDetalles.Columns(2).HeaderText = "Tipo"
            DGDetalles.Columns(3).HeaderText = "Folio"
            DGDetalles.Columns(4).HeaderText = "Fecha"
            DGDetalles.Columns(5).HeaderText = "Concepto"
            DGDetalles.Columns(1).Width = 200
            DGDetalles.Columns(2).Width = 40
            DGDetalles.Columns(3).Width = 60
            DGDetalles.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            DGDetalles.ClearSelection()
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub DGDetalles_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellDoubleClick
        If DGDetalles.CurrentCell.RowIndex >= 0 Then
            Dim Poliza As New frmContabilidadPolizasN(DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value)
            Poliza.ShowDialog()
            Poliza.Dispose()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Label5.Visible = CheckBox1.Checked
        DateTimePicker5.Visible = CheckBox1.Checked
    End Sub
End Class