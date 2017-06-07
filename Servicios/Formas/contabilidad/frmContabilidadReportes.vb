Public Class frmContabilidadReportes
    Dim prov As New dbproveedores(MySqlcon)
    Dim p As New dbContabilidadPolizas(MySqlcon)
    'Dim q As New dbPagosProveedores(MySqlcon)
    Dim cconta As New dbCContables(MySqlcon)
    Dim c As New dbContabilidadClasificacion(MySqlcon)
    'Dim nombreEmpresa As String = q.nombre()
    'Dim rfc As String = q.RFC()
    Dim N1 As Integer
    Dim N2 As Integer
    Dim N3 As Integer
    Dim N4 As Integer
    Dim N5 As Integer
    Dim nivel As Integer
    Dim idCContable As Integer
    Dim periodo As String
    Dim IDProv As Integer = 0
    Dim IdsClasificacionPoliza As New elemento



    Private Sub frmContabilidadReportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
            
        End Try
        nivel = -1
        idCContable = -1
        Try
            LlenaCombos("tblcontabilidadclas", cmbClasificacionPoliza, "nombre", "nombret", "id", IdsClasificacionPoliza, , "TODOS", "nombre")
            periodo = p.buscarPeriodo()

            Try
                If p.ActivarFechaTrabajo = 0 Then
                    dtpdesde.Value = "01/" + Date.Now.Month.ToString + "/" + periodo
                Else
                    dtpdesde.Value = Format(CDate(p.FechaTRabajo), "yyyy/MM/01")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            SelectorCuentas1.P = p
            SelectorCuentas1.C = New dbContabilidadClasificacion(MySqlcon)
            SelectorCuentas1.Inicializar()
            SelectorCuentas1.SoloUltimoNivel = False
            SelectorCuentas1.Label4.Text = "Rango cuenta:"
            SelectorCuentas2.P = p
            SelectorCuentas2.C = New dbContabilidadClasificacion(MySqlcon)
            SelectorCuentas2.Inicializar()
            SelectorCuentas2.SoloUltimoNivel = False
            SelectorCuentas2.Label4.Text = "a"
            'dtpdesde.MinDate = "01/01/" + periodo
            'dtpHasta.MinDate = "01/01/" + periodo
            'dtpdesde.MaxDate = "31/12/" + periodo
            'dtpHasta.MaxDate = "31/12/" + periodo
            'dtpdesde.Value = "01/" + Date.Now.Month.ToString + "/" + periodo
            cmbTipo.SelectedIndex = 0
            cmbTipoPoliza.SelectedIndex = 0
            N1 = -1
            N2 = -1
            N3 = -1
            N4 = -1
            N5 = -1
           
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try

        ' dtpHasta.Value = Date.Now.Day.ToString + "/" + Date.Now.Month.ToString + "/" + periodo
    End Sub
    Private Sub txtProveedorClave_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProveedorClave.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.ProveedorDiot, GlobalIdAlmacen, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                txtProveedorClave.Text = B.Proveedor.Clave
                ' txtIVA.Focus()
            End If
        End If
        If e.KeyCode = Keys.Enter Then
            If txtProveedorNombre.Text = "" Then
                txtProveedorNombre.Focus()
            Else
                '   txtIVA.Focus()
            End If

        End If
        
    End Sub

    Private Sub txtProveedorClave_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProveedorClave.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtProveedorClave_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProveedorClave.TextChanged
        prov.BuscaProveedorDIOT(txtProveedorClave.Text)
        txtProveedorNombre.Text = prov.Nombre
    End Sub

    Private Sub btnBuscarProveedor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarProveedor.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.ProveedorDiot, GlobalIdAlmacen, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtProveedorClave.Text = B.Proveedor.Clave

            ' txtIVA.Focus()
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Select Case lstTipos.Text
            Case "Verificador DIOT"
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Rep = New repContabilidadDIOT
                Rep.SetDataSource(p.reporteDIOT(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), txtProveedorClave.Text, CheckBox7.Checked))
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("provSinRFC", p.provSINRFC(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), txtProveedorClave.Text))
                Rep.SetParameterValue("fechas", dtpdesde.Value + " AL " + dtpHasta.Value)
                If txtProveedorNombre.Text <> "" Then
                    Rep.SetParameterValue("PROVE", "PROVEEDOR: " + txtProveedorNombre.Text)
                Else
                    Rep.SetParameterValue("PROVE", "PROVEEDOR: TODOS. ")
                End If

                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Balanza"
                'Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                'Rep = New repContabilidadBalanza2
                'Rep.SetDataSource(p.reporteBalanzaXML(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                'Rep.SetParameterValue("empresa", s.Nombre)
                'Rep.SetParameterValue("rfc", s.RFC)
                'Rep.SetParameterValue("sumaCargo", p.SumaCargo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                'Rep.SetParameterValue("FECHABALANZA", dtpdesde.Value + " AL " + dtpHasta.Value)
                'Rep.SetParameterValue("noceros", CheckBox1.Checked)
                'Rep.SetParameterValue("SoloMayor", CheckBox3.Checked)
                'Dim RV As New frmReportes(Rep, False)
                'RV.Show()

                Dim Rep2 As CrystalDecisions.CrystalReports.Engine.ReportDocument
                If CheckBox4.Checked = False Then
                    Rep2 = New repContabilidadBalanzaN
                Else
                    Rep2 = New repContabilidadBalanzaNt
                End If
                Rep2.SetDataSource(p.ReporteBalanza(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), CheckBox3.Checked, CheckBox4.Checked))
                Rep2.SetParameterValue("empresa", s.NombreFiscal)
                Rep2.SetParameterValue("rfc", s.RFC)
                Rep2.SetParameterValue("fecha", dtpdesde.Value + " AL " + dtpHasta.Value)
                Rep2.SetParameterValue("noceros", CheckBox1.Checked)
                Dim RV2 As New frmReportes(Rep2, False)
                RV2.Show()
            Case "Relaciones Analíticas"
                Dim rangodesde As String
                Dim rangohasta As String
                Dim filtros As String = ""
                Dim C1 As String = SelectorCuentas1.DaCuentatxt.Replace(" ", "")
                Dim c2 As String = SelectorCuentas2.DaCuentatxt.Replace(" ", "")
                If C1 <> "" Then
                    rangodesde = C1
                    filtros += "DE POLIZA: " + C1
                Else
                    rangodesde = "-1"
                End If
                If c2 <> "" Then
                    rangohasta = c2
                    If rangodesde = "-1" Then
                        filtros += "HASTA POLIZA: " + c2
                    Else
                        filtros += "A : " + c2
                    End If
                Else
                    rangohasta = "999999999"
                End If
                'Dim nivelAux1 As Integer = 0
                Dim nivelAux As Integer = 0
                Dim aux() As String = rangodesde.Split(" ")
                Dim cAux As String = ""
                If rangodesde = "-1" Then
                    cAux = rangodesde
                Else
                    'nivelAux1 = cAux.Length
                    For i As Integer = 0 To aux.Length - 1
                        cAux += aux(i).PadLeft(p.NNiv1, "0")
                    Next
                End If

                Dim aux2() As String = rangohasta.Split(" ")
                Dim cAux2 As String = ""
                If rangohasta = "999999999" Then
                    cAux2 = rangohasta
                Else
                    nivelAux = aux2.Length
                    For i As Integer = 0 To aux2.Length - 1
                        cAux2 += aux2(i).PadLeft(p.NNiv1, "0")
                    Next
                End If

                If nivelAux = 1 Then
                    For i As Integer = 0 To (p.NNiv2 + p.NNiv3 + p.NNiv4 + p.NNiv5)
                        cAux2 += "9"
                    Next
                End If
                If nivelAux = 2 Then
                    For i As Integer = 0 To (p.NNiv3 + p.NNiv4 + p.NNiv5)
                        cAux2 += "9"
                    Next
                End If
                If nivelAux = 3 Then
                    For i As Integer = 0 To (p.NNiv4 + p.NNiv5)
                        cAux2 += "9"
                    Next
                End If
                If nivelAux = 4 Then
                    For i As Integer = 0 To (p.NNiv5)
                        cAux2 += "9"
                    Next
                End If
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Rep = New repContabilidadRelaAna2
                'p.reporteRelaionesAnaliti(dtpdesde.Value.Month.ToString("00"), cmbTipo.SelectedIndex)
                Rep.SetDataSource(p.reporteRelaionesAnaliti(dtpdesde.Value.Month.ToString("00"), cmbTipo.SelectedIndex, cAux, cAux2, dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), CheckBox1.Checked))
                Rep.SetParameterValue("titulo", "RELACIONES ANALÍTICAS")
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                'Rep.SetParameterValue("sumaCargo", p.SumaCargo(dtpdesde.Value.Month.ToString("00")))
                Rep.SetParameterValue("FECHABALANZA", "RELACIONES ANALÍTICAS DEL " + dtpdesde.Value + " AL " + dtpHasta.Value)
                Rep.SetParameterValue("noceros", CheckBox1.Checked)
                If cAux <> -1 Then
                    Rep.SetParameterValue("congrupos", False)
                Else
                    Rep.SetParameterValue("congrupos", True)
                End If
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Relación de pólizas"
                Dim rangodesde As Integer
                Dim rangohasta As Integer
                Dim filtros As String = ""
                If txtRangoDesde.Text <> "" Then
                    rangodesde = txtRangoDesde.Text
                    filtros += "DE POLIZA: " + txtRangoDesde.Text
                Else
                    rangodesde = -1
                End If
                If txtRangoHasta.Text <> "" Then
                    rangohasta = txtRangoHasta.Text
                    If rangodesde = -1 Then
                        filtros += "HASTA POLIZA: " + txtRangoHasta.Text
                    Else
                        filtros += "A : " + txtRangoHasta.Text
                    End If
                Else
                    rangohasta = 999999999
                End If
                If cmbTipoPoliza.Text.Chars(0) <> "T" Then
                    If cmbTipoPoliza.Text.Chars(0) = "E" Then
                        filtros += " TIPO POLIZA: EGRESOS"
                    Else
                        If cmbTipoPoliza.Text.Chars(0) = "I" Then
                            filtros += " TIPO POLIZA: INGRESOS"
                        Else
                            If cmbTipoPoliza.Text.Chars(0) = "D" Then
                                filtros += " TIPO POLIZA: DIARIOS"
                            Else
                                If cmbTipoPoliza.Text.Chars(0) = "A" Then
                                    filtros += " TIPO POLIZA: APERTURA"
                                End If
                            End If

                        End If
                    End If

                End If
                If cmbClasificacionPoliza.SelectedIndex <> 0 Then
                    filtros += vbCrLf + cmbClasificacionPoliza.Text
                End If
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Rep = New repContabilidadRelacionPolizas
                Rep.SetDataSource(p.verificadorPolizas(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoPoliza.Text.Chars(0), rangodesde, rangohasta, -1, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)))
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("FECHABALANZA", "RELACIÓN DE POLIZAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                Rep.SetParameterValue("filtros", filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Auxiliar de Cuentas"
                p.borraSaldosIniciales()

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Rep = New repContabilidadAuxiliarCuentas
                Dim C1 As String = SelectorCuentas1.DaCuentatxt().Replace(" ", "")
                Dim C2 As String = SelectorCuentas2.DaCuentatxt().Replace(" ", "")
                If C2 = "" Then C2 = C1
                C2 = C2.PadRight(p.NNiv1 + p.NNiv2 + p.NNiv3 + p.NNiv4 + p.NNiv5, "9")
                p.todosLosSaldos(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), C1, C2)
                Rep.SetDataSource(p.imprimirTodascuentas(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), C1, C2))
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("FECHABALANZA", "AUXILIAR DE CUENTAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                Rep.SetParameterValue("filtros", "CUENTA: TODAS")
                Rep.SetParameterValue("pormes", CheckBox2.Checked)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
                'If txtDesdeCuenta.Text = "" And txtHastaCuenta.Text = "" And txtNivel1.Text = "" Then
                '    p.todosLosSaldos(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), SelectorCuentas1.DaCuentatxt, SelectorCuentas2.DaCuentatxt)
                '    Rep.SetDataSource(p.imprimirTodascuentas(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), SelectorCuentas1.DaCuentatxt, SelectorCuentas2.DaCuentatxt))
                '    Rep.SetParameterValue("empresa", s.NombreFiscal)
                '    Rep.SetParameterValue("rfc", s.RFC)
                '    Rep.SetParameterValue("FECHABALANZA", "AUXILIAR DE CUENTAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                '    Rep.SetParameterValue("filtros", "CUENTA: TODAS")
                '    Rep.SetParameterValue("pormes", CheckBox2.Checked)
                '    Dim RV As New frmReportes(Rep, False)
                '    RV.Show()
                'ElseIf txtDesdeCuenta.Text <> "" Then
                '    If txtHastaCuenta.Text <> "" Then


                '        ' p.SaldosInicialesRango(dtpHasta.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.Month.ToString("00"))
                '        Dim cuenta As String = txtDesdeCuenta.Text
                '        Dim cAux As String = ""
                '        Dim fincuenta As String = txtHastaCuenta.Text
                '        Dim cAux2 As String = ""
                '        If cuenta.Length = 4 And fincuenta.Length = 4 Then
                '            p.iniciaSaldosPorMayores(cuenta, fincuenta, dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                '            Rep.SetDataSource(p.AuxiliarRangoMayores(cuenta, fincuenta, dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                '            Rep.SetParameterValue("empresa", s.Nombre)
                '            Rep.SetParameterValue("rfc", s.RFC)
                '            Rep.SetParameterValue("FECHABALANZA", "AUXILIAR DE CUENTAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                '            Rep.SetParameterValue("filtros", "CUENTA: " + cAux + " a " + cAux2)
                '            Rep.SetParameterValue("pormes", CheckBox2.Checked)
                '            Dim RA As New frmReportes(Rep, False)
                '            RA.Show()
                '        Else
                '            Dim Caux3 As String
                '            Dim aux() As String = cuenta.Split(" ")
                '            For i As Integer = 0 To aux.Length - 1
                '                cAux += aux(i).PadLeft(4, "0")
                '            Next
                '            Dim aux2() As String = fincuenta.Split(" ")
                '            For i As Integer = 0 To aux2.Length - 1
                '                cAux2 += aux2(i).PadLeft(4, "0")
                '            Next
                '            Caux3 = cAux2
                '            cAux2 = cAux2.PadRight(p.NNiv1 + p.NNiv2 + p.NNiv3 + p.NNiv4 + p.NNiv5, "9")
                '            iniciaSaldosRango("")
                '            Rep.SetDataSource(p.auxiliarCuentasRango(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), nivel, idCContable, cAux, cAux2))
                '            Rep.SetParameterValue("empresa", s.Nombre)
                '            Rep.SetParameterValue("rfc", s.RFC)
                '            Rep.SetParameterValue("FECHABALANZA", "AUXILIAR DE CUENTAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                '            Rep.SetParameterValue("filtros", "CUENTA: " + cAux + " a " + Caux3)
                '            Rep.SetParameterValue("pormes", CheckBox2.Checked)
                '            Dim RV As New frmReportes(Rep, False)
                '            RV.Show()
                '        End If
                '    ElseIf txtDesdeCuenta.Text = "" Or txtHastaCuenta.Text = "" Then
                '        MsgBox("de indicar un rango de cuentas", MsgBoxStyle.Exclamation, GlobalNombreApp)
                '        Return
                '    End If
                'Else

                '    Dim cuenta As String = txtNivel1.Text + " " + txtNivel2.Text + " " + txtNivel3.Text + " " + txtNivel4.Text + " " + txtNivel5.Text
                '    cuenta = cuenta.Trim
                '    Dim cAux As String = ""
                '    Dim fincuenta As String = cuenta
                '    Dim cAux2 As String = ""
                '    If cuenta.Length = 4 And fincuenta.Length = 4 Then
                '        p.iniciaSaldosPorMayores(cuenta, fincuenta, dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"))
                '        Rep.SetDataSource(p.AuxiliarRangoMayores(cuenta, fincuenta, dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                '        Rep.SetParameterValue("empresa", s.NombreFiscal)
                '        Rep.SetParameterValue("rfc", s.RFC)
                '        Rep.SetParameterValue("FECHABALANZA", "AUXILIAR DE CUENTAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                '        Rep.SetParameterValue("filtros", "CUENTA: " + cAux + " a " + cAux2)
                '        Rep.SetParameterValue("pormes", CheckBox2.Checked)
                '        Dim RA As New frmReportes(Rep, False)
                '        RA.Show()
                '    Else
                '        iniciaSaldosRango(cuenta)
                '        Dim aux() As String = cuenta.Split(" ")
                '        For i As Integer = 0 To aux.Length - 1
                '            cAux += aux(i).PadLeft(4, "0")
                '        Next
                '        Dim aux2() As String = fincuenta.Split(" ")
                '        For i As Integer = 0 To aux2.Length - 1
                '            cAux2 += aux2(i).PadLeft(4, "0")
                '        Next

                '        Rep.SetDataSource(p.auxiliarCuentasRango(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), nivel, idCContable, cAux, cAux2.PadRight(p.NNiv1 + p.NNiv2 + p.NNiv3 + p.NNiv4 + p.NNiv5, "9")))
                '        Rep.SetParameterValue("empresa", s.NombreFiscal)
                '        Rep.SetParameterValue("rfc", s.RFC)
                '        Rep.SetParameterValue("FECHABALANZA", "AUXILIAR DE CUENTAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                '        Rep.SetParameterValue("filtros", "CUENTA: " + cAux + " a " + cAux2)
                '        Rep.SetParameterValue("pormes", CheckBox2.Checked)
                '        Dim RV As New frmReportes(Rep, False)
                '        RV.Show()
                '    End If

                'Aqui es cuando es solo una sola cuenta.
                'p.SaldosIniciales2(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpdesde.Value.Month.ToString("00"), nivel, N1, N2, N3, N4, N5, idCContable)
                'Rep.SetDataSource(p.auxiliarCuentas(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), nivel, idCContable))

                'Rep.SetParameterValue("empresa", s.Nombre)
                'Rep.SetParameterValue("rfc", s.RFC)
                'Rep.SetParameterValue("FECHABALANZA", "AUXILIAR DE CUENTAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                'Rep.SetParameterValue("filtros", "CUENTA: " + txtNivel1.Text + " " + txtNivel2.Text + " " + txtNivel3.Text + " " + txtNivel4.Text + " " + txtNivel5.Text)
                'Dim RV As New frmReportes(Rep, False)
                'RV.Show()
                'End If
                limpiaCampos()
                p.borraSaldosIniciales()
            Case "Estado de posición financiera"
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Rep = New repContabilidadBalance
                p.llenaDatosConfig()
                Rep.Subreports("repActivo").SetDataSource(p.activo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repActivoFijo").SetDataSource(p.activofijo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repActivoDiferido").SetDataSource(p.activodiferido(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repActivoOtros").SetDataSource(p.activoOtros(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repPasivo").SetDataSource(p.pasivo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repPasivoFijo").SetDataSource(p.pasivofIJO(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repPasivoDiferido").SetDataSource(p.pasivoDiferido(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repCapital").SetDataSource(p.CAPITAL(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repOrdenAcre").SetDataSource(p.OrdenAcre(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repOrdenD").SetDataSource(p.OrdenDeu(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.SetParameterValue("cuentaNumero", p.resultados.PadLeft(p.NNiv1), "repCapital")
                Rep.SetParameterValue("cuentaNombre", p.nombreResultado(), "repCapital")
                Rep.SetParameterValue("cuentaTotal", p.sumaEdoResTodo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.SetParameterValue("totalActivo", p.Sumaactivo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.SetParameterValue("totalPasivo", p.Sumaapasivo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.SetParameterValue("totalCapital", p.sumaCapitalYEdo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))

                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("FECHABALANZA", Format(dtpHasta.Value, "dd") + " de " + Format(dtpHasta.Value, "MMMM") + " de " + Format(dtpHasta.Value, "yyyy"))
                'Rep.SetParameterValue("filtros", "CUENTA: " + txtNivel1.Text + " " + txtNivel2.Text + " " + txtNivel3.Text + " " + txtNivel4.Text + " " + txtNivel5.Text)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Estado de resultados"
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Rep = New repContabilidadEstadodeResultados

                ' Rep.SetDataSource(p.reporteRelaionesAnaliti(dtpdesde.Value.Month.ToString("00"), cmbTipo.SelectedIndex))
                Rep.Subreports("repIngresos").SetDataSource(p.ingresos(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.Subreports("repEgresos").SetDataSource(p.egresos(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.SetParameterValue("MES", Format(dtpHasta.Value, "MMMM").ToUpper)
                Rep.SetParameterValue("MES2", "ENERO-" + Format(dtpHasta.Value, "MMMM").ToUpper)
                Rep.SetParameterValue("totalMes", p.sumaEdoResMes(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.SetParameterValue("total", p.sumaEdoResTodo(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd")))
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("FECHABALANZA", "Del 01 de enero al " + Format(dtpHasta.Value, "dd") + " de " + Format(dtpHasta.Value, "MMMM") + " de " + Format(dtpHasta.Value, "yyyy"))
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Libro Diario"
                Dim rangodesde As Integer
                Dim rangohasta As Integer
                Dim filtros As String = ""
                txtRangoDesde.Text = ""
                txtRangoHasta.Text = ""
                If txtRangoDesde.Text <> "" Then
                    rangodesde = txtRangoDesde.Text
                    filtros += "DE POLIZA: " + txtRangoDesde.Text
                Else
                    rangodesde = -1
                End If
                If txtRangoHasta.Text <> "" Then
                    rangohasta = txtRangoHasta.Text
                    If rangodesde = -1 Then
                        filtros += "HASTA POLIZA: " + txtRangoHasta.Text
                    Else
                        filtros += "A : " + txtRangoHasta.Text
                    End If
                Else
                    rangohasta = 999999999
                End If
                If cmbTipoPoliza.Text.Chars(0) <> "T" Then
                    If cmbTipoPoliza.Text.Chars(0) = "E" Then
                        filtros += " TIPO POLIZA: EGRESOS"
                    Else
                        If cmbTipoPoliza.Text.Chars(0) = "I" Then
                            filtros += " TIPO POLIZA: INGRESOS"
                        Else
                            If cmbTipoPoliza.Text.Chars(0) = "D" Then
                                filtros += " TIPO POLIZA: DIARIOS"
                            Else
                                If cmbTipoPoliza.Text.Chars(0) = "A" Then
                                    filtros += " TIPO POLIZA: APERTURA"
                                End If
                            End If

                        End If
                    End If

                End If

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Dim pIDCuenta As Integer
                If SelectorCuentas1.txtCuenta.Text <> "" Then
                    pIDCuenta = SelectorCuentas1.IdCuenta
                    filtros += " CUENTA: " + SelectorCuentas1.DaCuentatxt
                Else
                    pIDCuenta = -1
                End If
                Rep = New repContabilidadRelacionPolizas
                Rep.SetDataSource(p.verificadorPolizas(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoPoliza.Text.Chars(0), rangodesde, rangohasta, pIDCuenta))
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("FECHABALANZA", "LIBRO DIARIO DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " A " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                Rep.SetParameterValue("filtros", filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Libro Mayor"
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                Rep = New repContabilidadRelaAna2
                'p.reporteRelaionesAnaliti(dtpdesde.Value.Month.ToString("00"), cmbTipo.SelectedIndex)
                Rep.SetDataSource(p.reporteRelaionesAnaliti(dtpdesde.Value.Month.ToString("00"), cmbTipo.SelectedIndex, "-1", "-1", dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), CheckBox1.Checked))
                Rep.SetParameterValue("titulo", "LIBRO MAYOR")
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("noceros", CheckBox1.Checked)
                'Rep.SetParameterValue("sumaCargo", p.SumaCargo(dtpdesde.Value.Month.ToString("00")))
                Rep.SetParameterValue("FECHABALANZA", "LIBRO MAYOR DE " + dtpdesde.Value + " A " + dtpHasta.Value)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Impresión pólizas"
                'If cmbTipoPoliza.Text.Chars(0) <> "A" Then
                Dim rangodesde As Integer
                Dim rangohasta As Integer
                Dim filtros As String = ""
                If txtRangoDesde.Text <> "" Then
                    rangodesde = txtRangoDesde.Text
                    filtros += "DE POLIZA: " + txtRangoDesde.Text
                Else
                    rangodesde = -1
                End If
                If txtRangoHasta.Text <> "" Then
                    rangohasta = txtRangoHasta.Text
                    If rangodesde = -1 Then
                        filtros += "HASTA POLIZA: " + txtRangoHasta.Text
                    Else
                        filtros += "A : " + txtRangoHasta.Text
                    End If
                Else
                    rangohasta = 999999999
                End If
                If cmbTipoPoliza.Text.Chars(0) <> "T" Then
                    If cmbTipoPoliza.Text.Chars(0) = "E" Then
                        filtros += " TIPO POLIZA: EGRESOS"
                    Else
                        If cmbTipoPoliza.Text.Chars(0) = "I" Then
                            filtros += " TIPO POLIZA: INGRESOS"
                        Else
                            If cmbTipoPoliza.Text.Chars(0) = "D" Then
                                filtros += " TIPO POLIZA: DIARIOS"
                            Else
                                If cmbTipoPoliza.Text.Chars(0) = "A" Then
                                    filtros += " TIPO POLIZA: APERTURA"
                                End If
                            End If

                        End If
                    End If

                End If
                If cmbClasificacionPoliza.SelectedIndex <> 0 Then
                    filtros += vbCrLf + cmbClasificacionPoliza.Text
                End If
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                'Dim Rep2 As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                'If cmbTipoPoliza.Text.Chars(0) = "T" Then
                'se impriman los dos
                Rep = New repContabilidadImpresionPolizas
                Rep.SetDataSource(p.impresionPolizasID(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoPoliza.Text.Chars(0), rangodesde, rangohasta, -1, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)))
                Rep.SetParameterValue("empresa", s.NombreFiscal)
                Rep.SetParameterValue("rfc", s.RFC)
                Rep.SetParameterValue("direccion", s.Direccion + " " + s.NoExterior + " " + s.NoInterior)
                Rep.SetParameterValue("ciudad", "COL: " + s.Colonia + " " + s.Ciudad + " C.P:" + s.CP)
                Rep.SetParameterValue("telefono", "TEL: " + s.Telefono)
                ' Rep.SetParameterValue("FECHABALANZA", "RELACIÓN DE POLIZAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                'Rep.SetParameterValue("filtros", filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()

                'Rep2 = New repContabilidadimpresionPolizasE
                'Rep2.SetDataSource(p.impresionPolizasE(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoPoliza.Text.Chars(0), rangodesde, rangohasta, -1, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)))
                'Rep2.SetParameterValue("empresa", s.Nombre)
                'Rep2.SetParameterValue("rfc", s.RFC)
                'Rep2.SetParameterValue("direccion", s.Direccion + " " + s.NoExterior + " " + s.NoInterior)
                'Rep2.SetParameterValue("colonia", "COL: " + s.Colonia + " " + s.Ciudad + " C.P:" + s.CP)
                'Rep2.SetParameterValue("ciudad", "COL: " + s.Colonia + " " + s.Ciudad + " C.P:" + s.CP)
                'Rep2.SetParameterValue("telefono", "TEL: " + s.Telefono)
                '' Rep.SetParameterValue("FECHABALANZA", "RELACIÓN DE POLIZAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                ''Rep.SetParameterValue("filtros", filtros)
                'Dim RV2 As New frmReportes(Rep2, False)
                'RV2.Show()
                'Else
                '    If cmbTipoPoliza.Text.Chars(0) = "E" Then
                '        'Egresos
                '        Rep = New repContabilidadImpresionPolizas
                '        Rep.SetDataSource(p.impresionPolizasE(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoPoliza.Text.Chars(0), rangodesde, rangohasta, -1, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)))
                '        Rep.SetParameterValue("empresa", s.Nombre)
                '        Rep.SetParameterValue("rfc", s.RFC)
                '        Rep.SetParameterValue("direccion", s.Direccion + " " + s.NoExterior + " " + s.NoInterior)
                '        'Rep.SetParameterValue("colonia", "COL: " + s.Colonia + " " + s.Ciudad + " C.P:" + s.CP)
                '        Rep.SetParameterValue("ciudad", "COL: " + s.Colonia + " " + s.Ciudad + " C.P:" + s.CP)
                '        Rep.SetParameterValue("telefono", "TEL: " + s.Telefono)
                '        ' Rep.SetParameterValue("FECHABALANZA", "RELACIÓN DE POLIZAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                '        'Rep.SetParameterValue("filtros", filtros)
                '        Dim RV As New frmReportes(Rep, False)
                '        RV.Show()
                '    Else
                '        'las demas
                '        Rep = New repContabilidadImpresionPolizas
                '        Rep.SetDataSource(p.impresionPolizasID(dtpdesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), cmbTipoPoliza.Text.Chars(0), rangodesde, rangohasta, -1, IdsClasificacionPoliza.Valor(cmbClasificacionPoliza.SelectedIndex)))
                '        Rep.SetParameterValue("empresa", s.Nombre)
                '        Rep.SetParameterValue("rfc", s.RFC)
                '        Rep.SetParameterValue("direccion", s.Direccion + " " + s.NoExterior + " " + s.NoInterior)
                '        Rep.SetParameterValue("ciudad", "COL: " + s.Colonia + " " + s.Ciudad + " C.P:" + s.CP)
                '        Rep.SetParameterValue("telefono", "TEL: " + s.Telefono)
                '        ' Rep.SetParameterValue("FECHABALANZA", "RELACIÓN DE POLIZAS DEL " + dtpdesde.Value.ToString("dd/MM/yyyy") + " AL " + dtpHasta.Value.ToString("dd/MM/yyyy"))
                '        'Rep.SetParameterValue("filtros", filtros)
                '        Dim RV As New frmReportes(Rep, False)
                '        RV.Show()


                '    End If
                'End If


                'End If
        End Select


        
        
        limpiaCampos()

    End Sub

    Private Sub lstTipos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTipos.SelectedIndexChanged
        btnImprimir.Enabled = True
        If lstTipos.SelectedIndex = 0 Then
            '"Verificador DIOT"
            pnlProveedor.Enabled = True
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlRango.Enabled = False
            cmbTipoPoliza.Enabled = False
            lblTipoPoliza.Enabled = False
            pnlRangoCuenta.Enabled = False
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = True
        End If
        If lstTipos.SelectedIndex = 2 Then
            '"Relaciones Analíticas"
            pnlProveedor.Enabled = False
            dtpdesde.Value = Date.Parse("01/" + dtpdesde.Value.Month.ToString + "/" + dtpdesde.Value.Year.ToString())
            cmbTipo.Enabled = True
            lblTipoCuenta.Enabled = True
            pnlRango.Enabled = False
            cmbTipoPoliza.Enabled = False
            lblTipoPoliza.Enabled = False
            pnlRangoCuenta.Enabled = True
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = False
            CheckBox1.Enabled = True
        Else
            CheckBox1.Enabled = False
        End If
        If lstTipos.SelectedIndex = 1 Then
            '"Balanza"
            pnlProveedor.Enabled = False
            dtpdesde.Value = Date.Parse("01/" + dtpdesde.Value.Month.ToString + "/" + dtpdesde.Value.Year.ToString())
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlRango.Enabled = False
            cmbTipoPoliza.Enabled = False
            lblTipoPoliza.Enabled = False
            pnlRangoCuenta.Enabled = False
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = False
            CheckBox3.Enabled = True
            CheckBox1.Enabled = True
            CheckBox4.Enabled = True
        Else
            CheckBox4.Enabled = False
            CheckBox3.Enabled = False
        End If
        
        If lstTipos.SelectedIndex = 3 Then
            'relación de polizas
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlProveedor.Enabled = False
            pnlRango.Enabled = True
            cmbTipoPoliza.Enabled = True
            lblTipoPoliza.Enabled = True
            pnlRangoCuenta.Enabled = False
            pnlClasificacion.Enabled = True
            CheckBox7.Enabled = False

        End If
        If lstTipos.SelectedIndex = 4 Then
            'Auxiliar de Cuentas
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlProveedor.Enabled = False
            pnlRango.Enabled = False
            cmbTipoPoliza.Enabled = False
            lblTipoPoliza.Enabled = False
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = False
            pnlRangoCuenta.Enabled = True
            CheckBox2.Enabled = True
        Else
            CheckBox2.Enabled = False
        End If
        If lstTipos.SelectedIndex = 5 Then
            'Estado de posición financiera
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlProveedor.Enabled = False
            pnlRango.Enabled = False
            cmbTipoPoliza.Enabled = False
            lblTipoPoliza.Enabled = False
            pnlRangoCuenta.Enabled = False
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = False
        End If
        If lstTipos.SelectedIndex = 6 Then
            'Estado de resultados
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlProveedor.Enabled = False
            pnlRango.Enabled = False
            cmbTipoPoliza.Enabled = False
            lblTipoPoliza.Enabled = False
            pnlRangoCuenta.Enabled = False
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = False
        End If
        If lstTipos.SelectedIndex = 7 Then
            'libroDiario
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlProveedor.Enabled = False
            pnlRango.Enabled = True
            cmbTipoPoliza.Enabled = True
            lblTipoPoliza.Enabled = False
            pnlRangoCuenta.Enabled = True
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = False
        End If
        If lstTipos.SelectedIndex = 8 Then
            'LibroMayor
            pnlProveedor.Enabled = False
            dtpdesde.Value = Date.Parse("01/" + dtpdesde.Value.Month.ToString + "/" + dtpdesde.Value.Year.ToString())
            cmbTipo.Enabled = True
            lblTipoCuenta.Enabled = True
            pnlRango.Enabled = False
            cmbTipoPoliza.Enabled = False
            lblTipoPoliza.Enabled = False
            pnlRangoCuenta.Enabled = False
            pnlClasificacion.Enabled = False
            CheckBox7.Enabled = False
        End If
        If lstTipos.SelectedIndex = 9 Then
            'impresion polizas I y D
            cmbTipo.Enabled = False
            lblTipoCuenta.Enabled = False
            pnlProveedor.Enabled = False
            pnlRango.Enabled = True
            cmbTipoPoliza.Enabled = True
            lblTipoPoliza.Enabled = True
            pnlRangoCuenta.Enabled = False
            pnlClasificacion.Enabled = True
            CheckBox7.Enabled = False
        End If
    End Sub

    Private Sub dtpdesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpdesde.ValueChanged
        If lstTipos.SelectedItem = "Balanza" Or lstTipos.SelectedItem = "Relaciones Analíticas" Or lstTipos.SelectedIndex = 6 Or lstTipos.SelectedIndex = 5 Or lstTipos.SelectedIndex = 8 Then
            dtpdesde.Value = "01/" + dtpdesde.Value.Month.ToString("00") + "/" + dtpdesde.Value.Year.ToString

        End If
        dtpHasta.Value = Date.Parse("01/" + dtpdesde.Value.Month.ToString + "/" + dtpdesde.Value.Year.ToString()).AddMonths(1).AddDays(-1)
        'If lstTipos.SelectedItem = "Auxiliar de Cuentas" Then
        '    dtpdesde.Value = "01/" + dtpdesde.Value.Month.ToString("00") + "/" + dtpdesde.Value.Year.ToString
        'End If
    End Sub

    Private Sub txtRangoDesde_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRangoDesde.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtRangoHasta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRangoHasta.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub
    Private Function quitarCeros(ByVal pTexto As String)
        Dim texto As String = ""
        If pTexto <> "" Then
            If pTexto.Chars(0) = "0" Then
                For i As Integer = 0 To pTexto.Length.ToString - 1
                    If pTexto.Chars(i) <> "0" Then
                        For j As Integer = i To pTexto.Length - 1
                            texto += pTexto.Chars(j)
                        Next
                        If texto = "" Then
                            texto = "0"
                        End If
                        Return texto
                    End If
                Next
            Else
                texto = pTexto
            End If
        End If
        If texto = "" Then
            texto = "0"
        End If

        Return texto
    End Function
    Private Sub btnXML_Click(sender As Object, e As EventArgs) Handles btnXML.Click
        Dim B As New frmContabilidadXML()
        B.ShowDialog()
    End Sub

    Private Sub limpiaCampos()
        
        CheckBox3.Checked = False
        CheckBox2.Checked = False
        CheckBox4.Checked = False
        txtRangoDesde.Text = ""
        txtRangoHasta.Text = ""
        txtProveedorClave.Text = ""
        txtProveedorNombre.Text = ""
        SelectorCuentas1.Vacia()
        SelectorCuentas2.Vacia()
        CheckBox1.Checked = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            CheckBox4.Checked = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            CheckBox3.Checked = False
        End If
    End Sub


  
End Class