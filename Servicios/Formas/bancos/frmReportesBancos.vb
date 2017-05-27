Public Class frmReportesBancos
    Dim tipo As String
    Dim Dt As DataTable
    Dim cargos As String
    Dim abonos As String
    Dim cargos1 As String
    Dim abonos1 As String
    Dim cargo As Double
    Dim abono As Double
    Dim SaldoInicial As Double
    Dim saldoActual As Double
    Dim idBanco As Integer
    Dim IdsCuentas As New elemento


    Private Sub frmReportesBancos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        tipo = ""
        Dim year As String
        Dim month As String
        year = Date.Now.Year.ToString
        month = Format(Date.Now.Month, "00")
        dtpFecha.Value = "01/" + month + "/" + year.ToString
        'dtpFechaHasta.MinDate = dtpFecha.Value
        LlenaCombos("tblcuentas", cmbCuenta, "concat(numero,' ',(select nombre from tblbancoscatalogo where idbanco=banco))", "cuentaN", "idcuenta", IdsCuentas)

    End Sub
    

    Private Sub ListBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTipos.Click
        Dim num As Integer = lstTipos.SelectedIndex
        If num >= 0 Then
            tipo = lstTipos.Items(num).ToString()
            btnImprimir.Enabled = True
            If num <= "5" Then
                Panel1.Enabled = True
            Else
                Panel1.Enabled = False
            End If
        End If
        
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        idBanco = IdsCuentas.Valor(cmbCuenta.SelectedIndex)
        Dim fecha As Date = dtpFecha.Value
        Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim fechaHasta As Date = dtpFechaHasta.Value
        Dim P As New dbBanco(MySqlcon)
        Dim q As New dbPagosProveedores(MySqlcon)
        If tipo = "Bancos" Then


            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repBancos
            'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
            Rep.SetDataSource(P.Reporte1())
            Rep.SetParameterValue("encabezado", Suc.Nombre)
            Rep.SetParameterValue("direccion", "")
            Rep.SetParameterValue("rfc", "")
            Rep.SetParameterValue("direccion2", "")
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        If tipo = "Cuentas" Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Rep = New repCuentas
            'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
            'Dim ta As DataTable
            'ta = P.Reporte1().ToTable()
            Dim Cuenta As New dbCuentas(MySqlcon)
            Rep.SetDataSource(Cuenta.Reporte1())
            Rep.SetParameterValue("encabezado", Suc.Nombre)
            Rep.SetParameterValue("direccion", "")
            Rep.SetParameterValue("rfc", "")
            Rep.SetParameterValue("direccion2", "")
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        'If tipo = "Cuentas Contables" Then
        '    ' Dim P As New dbCuentas(MySqlcon)
        '    Dim q As New dbPagosProveedores(MySqlcon)
        '    Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
        '    Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
        '    Dim direccion As String = q.Calles(nombreEmpresa)
        '    Dim direccion2 As String = q.direccion2(nombreEmpresa)
        '    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

        '    Rep = New repCuentasContables
        '    'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        '    Rep.SetDataSource(reporte())
        '    Rep.SetParameterValue("encabezado", nombreEmpresa)
        '    Rep.SetParameterValue("direccion", direccion)
        '    Rep.SetParameterValue("rfc", rfc)
        '    Rep.SetParameterValue("direccion2", direccion2)
        '    Dim RV As New frmReportes(Rep, False)
        '    RV.Show()
        'End If
        If tipo = "Estado de cuenta" Then
            ' Dim P As New dbCuentas(MySqlcon)
            'Dim q As New dbPagosProveedores(MySqlcon)
            'Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
            'Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
            'Dim direccion As String = q.Calles(nombreEmpresa)
            'Dim direccion2 As String = q.direccion2(nombreEmpresa)

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repEdoCuenta
            'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
            Rep.SetDataSource(estadoCuenta())
            Rep.SetParameterValue("encabezado", Suc.Nombre)
            Rep.SetParameterValue("direccion", "")
            Rep.SetParameterValue("rfc", "")
            Rep.SetParameterValue("direccion2", "")
            Rep.SetParameterValue("Cuenta", cmbCuenta.Text)
            Rep.SetParameterValue("Banco", cmbCuenta.Text)
            Rep.SetParameterValue("fechaInicio", fechaFormato(dtpFecha))
            Rep.SetParameterValue("fechaFinal", fechaFormato(dtpFechaHasta))
            Rep.SetParameterValue("saldoInicial", calcularSaldoInicial())
            Rep.SetParameterValue("totalCargo", cargos)
            Rep.SetParameterValue("totalAbono", abonos)
            saldoActual = (SaldoInicial + Double.Parse(abonos1)) - Double.Parse(cargos1)
            Rep.SetParameterValue("saldoActual", Format(saldoActual, "0.00"))
            Dim RV As New frmReportes(Rep, False)
            RV.Show()

        End If
        'If tipo = "Pólizas - Pagos proveedores" Then

        '    Dim q As New dbPagosProveedores(MySqlcon)
        '    Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
        '    Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
        '    Dim direccion As String = q.Calles(nombreEmpresa)
        '    Dim direccion2 As String = q.direccion2(nombreEmpresa)

        '    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

        '    Rep = New repPolizaPagoProv
        '    'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        '    Rep.SetDataSource(PolizaPagoProv())
        '    Rep.SetParameterValue("encabezado", nombreEmpresa)
        '    Rep.SetParameterValue("direccion", direccion)
        '    Rep.SetParameterValue("rfc", rfc)
        '    Rep.SetParameterValue("direccion2", direccion2)
        '    Rep.SetParameterValue("fechaInicio", fechaFormato(dtpFecha))
        '    Rep.SetParameterValue("fechaFinal", fechaFormato(dtpFechaHasta))
        '    Rep.SetParameterValue("Cuenta", txtCodigo.Text)
        '    Rep.SetParameterValue("Banco", cmbCuenta.Text)
        '    Rep.SetParameterValue("totalCargo", "$" + Format(cargo, "0.00"))
        '    Rep.SetParameterValue("totalAbono", "$" + Format(abono, "0.00"))
        '    Dim RV As New frmReportes(Rep, False)
        '    RV.Show()
        'End If
        'If tipo = "Pólizas - Depósitos" Then

        '    Dim q As New dbPagosProveedores(MySqlcon)
        '    Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
        '    Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
        '    Dim direccion As String = q.Calles(nombreEmpresa)
        '    Dim direccion2 As String = q.direccion2(nombreEmpresa)

        '    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

        '    Rep = New repPolizaDeposito
        '    'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        '    Rep.SetDataSource(PolizaDeposito())
        '    Rep.SetParameterValue("encabezado", nombreEmpresa)
        '    Rep.SetParameterValue("direccion", direccion)
        '    Rep.SetParameterValue("rfc", rfc)
        '    Rep.SetParameterValue("direccion2", direccion2)
        '    Rep.SetParameterValue("fechaInicio", fechaFormato(dtpFecha))
        '    Rep.SetParameterValue("fechaFinal", fechaFormato(dtpFechaHasta))
        '    Rep.SetParameterValue("Cuenta", txtCodigo.Text)
        '    Rep.SetParameterValue("Banco", cmbCuenta.Text)
        '    Rep.SetParameterValue("totalCargo", "$" + Format(cargo, "0.00"))
        '    Rep.SetParameterValue("totalAbono", "$" + Format(abono, "0.00"))
        '    Dim RV As New frmReportes(Rep, False)
        '    RV.Show()
        'End If
        'If tipo = "Reporte DIOT" Then


        '    'Dim q As New dbPagosProveedores(MySqlcon)
        '    'Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
        '    'Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
        '    'Dim direccion As String = q.Calles(nombreEmpresa)
        '    'Dim direccion2 As String = q.direccion2(nombreEmpresa)

        '    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

        '    Rep = New repDIOT
        '    'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        '    Rep.SetDataSource(reporteDIOT())
        '    Rep.SetParameterValue("encabezado", Suc.Nombre)
        '    Rep.SetParameterValue("direccion", "")
        '    Rep.SetParameterValue("rfc", "")
        '    Rep.SetParameterValue("direccion2", "")
        '    Rep.SetParameterValue("fechaInicio", fechaFormato(dtpFecha))
        '    Rep.SetParameterValue("fechaFinal", fechaFormato(dtpFechaHasta))
        '    Rep.SetParameterValue("Cuenta", "")
        '    Rep.SetParameterValue("Banco", cmbCuenta.Text)
        '    'Rep.SetParameterValue("totalCargo", "$" + Format(cargo, "0.00"))
        '    ' Rep.SetParameterValue("totalAbono", "$" + Format(abono, "0.00"))
        '    Dim RV As New frmReportes(Rep, False)
        '    RV.Show()
        'End If
        If tipo = "Depósitos" Then

            'Dim q As New dbPagosProveedores(MySqlcon)
            'Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
            'Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
            'Dim direccion As String = q.Calles(nombreEmpresa)
            'Dim direccion2 As String = q.direccion2(nombreEmpresa)

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            'Dim P As New dbDeposito(MySqlcon)
            Rep = New repDeposito
            'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
            Rep.SetDataSource(Deposito())
            Rep.SetParameterValue("encabezado", Suc.Nombre)
            Rep.SetParameterValue("direccion", "")
            Rep.SetParameterValue("rfc", "")
            Rep.SetParameterValue("direccion2", "")
            Rep.SetParameterValue("fechaInicio", fechaFormato(dtpFecha))
            Rep.SetParameterValue("fechaFinal", fechaFormato(dtpFechaHasta))
            Rep.SetParameterValue("Cuenta", cmbCuenta.Text)
            Rep.SetParameterValue("Banco", cmbCuenta.Text)

            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        If tipo = "Pagos" Then
            'Dim P As New dbPagosProveedores(MySqlcon)

            'Dim q As New dbPagosProveedores(MySqlcon)
            'Dim nombreEmpresa As String = q.nombre(GlobalIdSucursalDefault)
            'Dim rfc As String = q.RFC(GlobalIdSucursalDefault)
            'Dim direccion As String = q.Calles(nombreEmpresa)
            'Dim direccion2 As String = q.direccion2(nombreEmpresa)

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            idBanco = IdsCuentas.Valor(cmbCuenta.SelectedIndex)
            Rep = New repPagoProv
            'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
            Rep.SetDataSource(q.pagoProvedoorRep(dtpFecha.Value.ToString("yyyy/MM/dd"), dtpFechaHasta.Value.ToString("yyyy/MM/dd"), idBanco))
            Rep.SetParameterValue("encabezado", Suc.Nombre)
            Rep.SetParameterValue("direccion", "")
            Rep.SetParameterValue("rfc", "")
            Rep.SetParameterValue("direccion2", "")
            Rep.SetParameterValue("fechaInicio", fechaFormato(dtpFecha))
            Rep.SetParameterValue("fechaFinal", fechaFormato(dtpFechaHasta))
            Rep.SetParameterValue("Cuenta", cmbCuenta.Text)
            Rep.SetParameterValue("Banco", cmbCuenta.Text)

            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        'dtpFecha.Value = fecha
        'dtpFechaHasta.Value = fechaHasta
    End Sub
    Public Function reporte() As DataSet
        Dim P As New dbCContables(MySqlcon)
        ' DataGridView1.DataSource = P.reporte()

        'aqui
        Dim Tabla As New DataTable
        Dim Tabla2 As New DataTable
        Dim Tabla3 As New DataTable
        Dim Tabla4 As New DataTable
        Dim Tabla5 As New DataTable
        Dim TablaFull As New DataTable
        Tabla = P.reporte().ToTable()
        Dim contador As Integer = 0
        Dim sc2 As Integer = 0
        Dim sc3 As Integer = 0
        Dim sc4 As Integer = 0
        Dim sc5 As Integer = 0
        TablaFull.Columns.Add("ID")
        TablaFull.Columns.Add("Cuenta")
        TablaFull.Columns.Add("Descripcion")
        TablaFull.Columns.Add("Nivel")
        TablaFull.Columns.Add("Tipo")
        TablaFull.Columns.Add("Naturaleza") '******

        For i As Integer = 0 To Tabla.Rows.Count() - 1
            Dim dr As DataRow

            dr = TablaFull.NewRow()
            dr("ID") = Tabla.Rows(i)(0).ToString
            dr("Cuenta") = Tabla.Rows(i)(1).ToString
            dr("Descripcion") = Tabla.Rows(i)(2).ToString
            dr("Nivel") = Tabla.Rows(i)(3).ToString
            dr("Tipo") = Tabla.Rows(i)(4).ToString
            dr("Naturaleza") = Tabla.Rows(i)(5).ToString
            TablaFull.Rows.Add(dr)

            contador = contador + 1

            'TablaFull.ImportRow(Tabla.Rows(i))
            Tabla2 = P.subCuentas(Tabla.Rows(i)(1).ToString).Table
            sc2 = Tabla2.Rows.Count

            For j As Integer = 0 To sc2 - 1 'Nivel 2
                Dim dr2 As DataRow
                dr2 = TablaFull.NewRow()
                dr2("ID") = (Tabla2.Rows(j)(0).ToString())
                dr2("Cuenta") = (Tabla2.Rows(j)(1).ToString())
                dr2("Descripcion") = Tabla2.Rows(j)(2).ToString()
                dr2("Nivel") = Tabla2.Rows(j)(3).ToString
                dr2("Tipo") = Tabla2.Rows(j)(5).ToString
                dr2("Naturaleza") = Tabla2.Rows(j)(6).ToString
                TablaFull.Rows.Add(dr2)
                contador = contador + 1

                Tabla3 = P.subCuentas3(Tabla.Rows(i)(1).ToString, Tabla2.Rows(j)(4).ToString()).Table
                sc3 = Tabla3.Rows.Count

                For k As Integer = 0 To sc3 - 1 'nivel3
                    Dim dr3 As DataRow
                    dr3 = TablaFull.NewRow()
                    dr3("ID") = (Tabla3.Rows(k)(0).ToString())
                    dr3("Cuenta") = (Tabla3.Rows(k)(1).ToString())
                    dr3("Descripcion") = Tabla3.Rows(k)(2).ToString()
                    dr3("Nivel") = Tabla3.Rows(k)(3).ToString
                    dr3("Tipo") = Tabla3.Rows(k)(5).ToString
                    dr3("Naturaleza") = Tabla3.Rows(k)(6).ToString
                    TablaFull.Rows.Add(dr3)
                    contador = contador + 1

                    Tabla4 = P.subCuentas4(Tabla.Rows(i)(1).ToString, Tabla2.Rows(j)(4).ToString, Tabla3.Rows(k)(4).ToString()).Table
                    sc4 = Tabla4.Rows.Count

                    For l As Integer = 0 To sc4 - 1 'nivel4
                        Dim dr4 As DataRow
                        dr4 = TablaFull.NewRow()
                        dr4("ID") = (Tabla4.Rows(l)(0).ToString())
                        dr4("Cuenta") = (Tabla4.Rows(l)(1).ToString())
                        dr4("Descripcion") = Tabla4.Rows(l)(2).ToString()
                        dr4("Nivel") = Tabla4.Rows(l)(3).ToString
                        dr4("Tipo") = Tabla4.Rows(l)(4).ToString
                        dr4("Naturaleza") = Tabla4.Rows(l)(5).ToString
                        TablaFull.Rows.Add(dr4)
                        contador = contador + 1

                        Tabla5 = P.subCuentas5(Tabla.Rows(i)(1).ToString, Tabla2.Rows(j)(4).ToString, Tabla3.Rows(k)(4).ToString(), Tabla4.Rows(l)(6).ToString()).Table
                        sc5 = Tabla5.Rows.Count

                        For m As Integer = 0 To sc5 - 1 'nivel5
                            Dim dr5 As DataRow
                            dr5 = TablaFull.NewRow()
                            dr5("ID") = (Tabla5.Rows(m)(0).ToString())
                            dr5("Cuenta") = (Tabla5.Rows(m)(1).ToString())
                            dr5("Descripcion") = Tabla5.Rows(m)(2).ToString()
                            dr5("Nivel") = Tabla5.Rows(m)(3).ToString
                            dr5("Tipo") = Tabla5.Rows(m)(4).ToString
                            dr5("Naturaleza") = Tabla5.Rows(m)(5).ToString
                            TablaFull.Rows.Add(dr5)
                            contador = contador + 1

                        Next
                    Next

                Next

            Next

        Next

        'aqui
        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblccontables.xml")

        Return dataSet
    End Function
    Private Function estadoCuenta() As DataSet

        Dim Tabla1 As DataTable
        Dim Tabla2 As DataTable
        Dim TablaFull As New DataTable
        TablaFull.Columns.Add("Tipo")
        TablaFull.Columns.Add("Folio")
        TablaFull.Columns.Add("Fecha")
        TablaFull.Columns.Add("Fecha cobro")
        TablaFull.Columns.Add("Referencia")
        TablaFull.Columns.Add("Cargo")
        TablaFull.Columns.Add("Abono")
        Dim P As New dbCosiliacion(MySqlcon)
        'Tbla de pagos
        Tabla1 = P.filtroAmbosPagos1(dtpFecha.Value.ToString("yyyy/MM/dd"), dtpFechaHasta.Value.ToString("yyyy/MM/dd"), idBanco)
        'Tabla depósitos
        Tabla2 = P.filtroAmbos1(dtpFecha.Value.ToString("yyyy/MM/dd"), dtpFechaHasta.Value.ToString("yyyy/MM/dd"), idBanco)

        'llenar TablaFull
        'esta solo copiado
        'Dim dr1 As DataRow
        'dr1 = TablaFull.NewRow()
        'dr1("Tipo") = "Pagos proveedores"
        'dr1("Folio") = " "
        'dr1("Fecha") = " "
        'dr1("Fecha cobro") = " "
        'dr1("Referencia") = " "
        'dr1("Cargo") = "0"
        'dr1("Abono") = "0"
        'TablaFull.Rows.Add(dr1)

        For i As Integer = 0 To Tabla1.Rows.Count() - 1
            Dim dr As DataRow

            dr = TablaFull.NewRow()
            dr("Tipo") = " "
            dr("Folio") = Tabla1.Rows(i)(2).ToString
            dr("Fecha") = Tabla1.Rows(i)(4).ToString
            dr("Fecha cobro") = Tabla1.Rows(i)(5).ToString
            dr("Referencia") = Tabla1.Rows(i)(6).ToString
            dr("Cargo") = Tabla1.Rows(i)(9)
            dr("Abono") = 0
            TablaFull.Rows.Add(dr)
        Next

        'Dim dr2 As DataRow
        'dr2 = TablaFull.NewRow()
        'dr2("Tipo") = "Depósitos"
        'dr2("Folio") = " "
        'dr2("Fecha") = " "
        'dr2("Fecha cobro") = " "
        'dr2("Referencia") = " "
        'dr2("Cargo") = "0"
        'dr2("Abono") = "0"
        'TablaFull.Rows.Add(dr2)

        For i As Integer = 0 To Tabla2.Rows.Count() - 1
            Dim dr As DataRow

            dr = TablaFull.NewRow()
            dr("Tipo") = " "
            dr("Folio") = Format(Integer.Parse(Tabla2.Rows(i)(1)), "0000")
            dr("Fecha") = Tabla2.Rows(i)(2).ToString
            dr("Fecha cobro") = Tabla2.Rows(i)(2).ToString
            dr("Referencia") = Tabla2.Rows(i)(3).ToString
            dr("Cargo") = 0
            dr("Abono") = Tabla2.Rows(i)(7)
            TablaFull.Rows.Add(dr)
        Next
        Dim car As Double = 0
        Dim abon As Double = 0

        For i As Integer = 0 To Tabla1.Rows.Count() - 1
            car = car + Double.Parse(Tabla1.Rows(i)(9).ToString)
        Next
        For i As Integer = 0 To Tabla2.Rows.Count() - 1
            abon = abon + Double.Parse(Tabla2.Rows(i)(7).ToString)
        Next

        cargos = Format(car, "0.00")
        abonos = Format(abon, "0.00")
        cargos1 = Format(car, "0.00")
        abonos1 = Format(abon, "0.00")

        Dim DS As DataSet = New DataSet
        DS.Tables.Add(TablaFull)
        'DS.WriteXmlSchema("tblEdoCuenta.xml")
        Return DS
    End Function
    Public Function fechaFormato(ByVal pfecha As DateTimePicker) As String
        Dim Dia As String
        Dim Mes As String
        Dim year As String
        Dim fechita As String


        Dia = Format(pfecha.Value.Day, "00")
        Mes = Format(pfecha.Value.Month, "00")
        year = pfecha.Value.Year

        fechita = year + "-" + Mes + "-" + Dia
        Return fechita
    End Function
    Public Function fechaFormato2(ByVal pfecha As DateTimePicker) As String
        Dim Dia As String
        Dim Mes As String
        Dim year As String
        Dim fechita As String

        pfecha.Value = pfecha.Value.AddDays(-1)

        Dia = Format(pfecha.Value.Day, "00")
        Mes = Format(pfecha.Value.Month, "00")
        year = pfecha.Value.Year

        fechita = year + "-" + Mes + "-" + Dia
        Return fechita
    End Function

    
    Private Function calcularSaldoInicial() As Double
        'Dim Tabla1 As DataTable
        'Dim Tabla2 As DataTable
        SaldoInicial = 0
        Dim aux As Double = 0
        'Dim nFilas As Integer
        Dim Depositos As Double = 0
        Dim Pagos As Double = 0
        Dim saldo As String
        Dim P As New dbCosiliacion(MySqlcon)

        Depositos = P.saldoInicialAmbos(dtpFecha.Value.ToString("yyyy/MM/dd"), idBanco) 'error v
        Pagos = P.saldoInicialAmbosPagos(dtpFecha.Value.ToString("yyyy/MM/dd"), idBanco)

        'nFilas = Tabla1.Rows.Count
        'For i As Integer = 0 To nFilas - 1
        '    Depositos = Depositos + Double.Parse(Tabla1.Rows(i)(0).ToString)
        'Next
        'nFilas = Tabla2.Rows.Count
        'For i As Integer = 0 To nFilas - 1
        '    Pagos = Pagos + Double.Parse(Tabla2.Rows(i)(0).ToString)
        'Next

        SaldoInicial = Depositos - Pagos
        '''''''''''''''''
        'Saldo actual
        '''''''''''''

        saldo = Format(SaldoInicial, "0.00")
        Return SaldoInicial
    End Function
    Private Function PolizaPagoProv() As DataSet

        Dim Tabla1 As DataTable
        ' Dim Tabla2 As DataTable
        Dim TablaFull As New DataTable

        TablaFull.Columns.Add("Fecha")
        TablaFull.Columns.Add("NumPoliza")
        TablaFull.Columns.Add("Cuenta")
        TablaFull.Columns.Add("Descripcion")
        TablaFull.Columns.Add("Cargo")
        TablaFull.Columns.Add("Abono")
        cargos = 0
        abonos = 0
        Dim P As New dbPagosProveedores(MySqlcon)

        Tabla1 = P.Reporteid(fechaFormato(dtpFecha), fechaFormato(dtpFechaHasta), idBanco)
        For i As Integer = 0 To Tabla1.Rows.Count() - 1
            Dim Tabla2 As DataTable
            Tabla2 = P.ReportePoliza(Tabla1.Rows(i)(0).ToString)
            For j As Integer = 0 To Tabla2.Rows.Count - 1
                Dim dr1 As DataRow
                dr1 = TablaFull.NewRow()
                dr1("Fecha") = Tabla1.Rows(i)(1).ToString
                dr1("NumPoliza") = Tabla2.Rows(j)(0).ToString
                dr1("Cuenta") = Tabla2.Rows(j)(1).ToString
                dr1("Descripcion") = Tabla2.Rows(j)(2).ToString
                If Double.Parse(Tabla2.Rows(j)(3).ToString) > 0.0 Then
                    dr1("Cargo") = "$" + Tabla2.Rows(j)(3).ToString
                    cargo = cargo + Double.Parse(Tabla2.Rows(j)(3).ToString)
                Else
                    dr1("Cargo") = " "
                End If

                If Double.Parse(Tabla2.Rows(j)(4).ToString) > 0.0 Then
                    dr1("Abono") = "$" + Tabla2.Rows(j)(4).ToString
                    abono = abono + Double.Parse(Tabla2.Rows(j)(4).ToString)
                Else
                    dr1("Abono") = " "
                End If
                TablaFull.Rows.Add(dr1)
            Next
            'If Tabla2.Rows.Count > 0 Then
            '    Dim dr2 As DataRow
            '    dr2 = TablaFull.NewRow()
            '    dr2("Fecha") = " "
            '    dr2("NumPoliza") = " "
            '    dr2("Cuenta") = " "
            '    dr2("Descripcion") = " "
            '    dr2("Cargo") = " "
            '    dr2("Abono") = " "
            '    TablaFull.Rows.Add(dr2)
            'End If
            
        Next


        Dim DS As DataSet = New DataSet
        DS.Tables.Add(TablaFull)
        DS.WriteXmlSchema("tblPagoProv.xml")
        Return DS
    End Function

    Private Function PolizaDeposito() As DataSet

        Dim Tabla1 As DataTable
        ' Dim Tabla2 As DataTable
        Dim TablaFull As New DataTable

        TablaFull.Columns.Add("Fecha")
        TablaFull.Columns.Add("NumPoliza")
        TablaFull.Columns.Add("Cuenta")
        TablaFull.Columns.Add("Descripcion")
        TablaFull.Columns.Add("Cargo")
        TablaFull.Columns.Add("Abono")
        cargos = 0
        abonos = 0

        Dim P As New dbDeposito(MySqlcon)

        Tabla1 = P.Reporteid(fechaFormato(dtpFecha), fechaFormato(dtpFechaHasta), idBanco)
        For i As Integer = 0 To Tabla1.Rows.Count() - 1
            Dim Tabla2 As DataTable
            Tabla2 = P.ReportePoliza(Tabla1.Rows(i)(0).ToString)
            For j As Integer = 0 To Tabla2.Rows.Count - 1
                Dim dr1 As DataRow
                dr1 = TablaFull.NewRow()
                dr1("Fecha") = Tabla1.Rows(i)(1).ToString
                dr1("NumPoliza") = Tabla2.Rows(j)(0).ToString
                dr1("Cuenta") = Tabla2.Rows(j)(1).ToString
                dr1("Descripcion") = Tabla2.Rows(j)(2).ToString
                If Double.Parse(Tabla2.Rows(j)(3).ToString) > 0.0 Then
                    dr1("Cargo") = "$" + Tabla2.Rows(j)(3).ToString
                    cargo = cargo + Double.Parse(Tabla2.Rows(j)(3).ToString)
                Else
                    dr1("Cargo") = " "
                End If

                If Double.Parse(Tabla2.Rows(j)(4).ToString) > 0.0 Then
                    dr1("Abono") = "$" + Tabla2.Rows(j)(4).ToString
                    abono = abono + Double.Parse(Tabla2.Rows(j)(4).ToString)
                Else
                    dr1("Abono") = " "
                End If

                TablaFull.Rows.Add(dr1)
            Next
            'If Tabla2.Rows.Count > 0 Then
            '    Dim dr2 As DataRow
            '    dr2 = TablaFull.NewRow()
            '    dr2("Fecha") = " "
            '    dr2("NumPoliza") = " "
            '    dr2("Cuenta") = " "
            '    dr2("Descripcion") = " "
            '    dr2("Cargo") = " "
            '    dr2("Abono") = " "
            '    TablaFull.Rows.Add(dr2)
            'End If

        Next


        Dim DS As DataSet = New DataSet
        DS.Tables.Add(TablaFull)
        DS.WriteXmlSchema("tblDeposito.xml")
        Return DS
    End Function

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        '  dtpFechaHasta.MinDate = dtpFecha.Value

    End Sub
    Private Function reporteDIOT() As DataSet

        Dim Tabla1 As DataTable
        ' Dim Tabla2 As DataTable
        Dim TablaFull As New DataTable

        TablaFull.Columns.Add("Proveedor")
        TablaFull.Columns.Add("RFC")
        TablaFull.Columns.Add("IVA")
        TablaFull.Columns.Add("Importe")
        TablaFull.Columns.Add("IVAPagado")
        TablaFull.Columns.Add("Total")
        TablaFull.Columns.Add("Poliza")
        TablaFull.Columns.Add("Fecha")
        TablaFull.Columns.Add("Tipo")
        TablaFull.Columns.Add("Folio")
        TablaFull.Columns.Add("Referencia")

        Dim P As New dbPagosProveedores(MySqlcon)

        Tabla1 = P.ReporteDIOT(fechaFormato(dtpFecha), fechaFormato(dtpFechaHasta), idBanco)
        Dim importe1 As Double = 0.0
        Dim IVA1 As Double = 0.0
        Dim total1 As Double = 0.0

        Dim nombre As String = " "
        Dim temoFilas As SByte = 0
        Do Until Tabla1.Rows.Count = temoFilas

            ' If primerRecorrido = 0 Then
            For j As Integer = 0 To Tabla1.Rows.Count() - 1
                If Tabla1.Rows(j)(1).ToString <> " " Then
                    nombre = Tabla1.Rows(j)(1).ToString
                    j = Tabla1.Rows.Count - 1
                End If
            Next

            importe1 = 0
            IVA1 = 0
            total1 = 0

            For i As Integer = 0 To Tabla1.Rows.Count - 1
                If Tabla1.Rows(i)(1).ToString = nombre Then
                    Dim dr2 As DataRow
                    Dim importe As Double
                    Dim ivaTotal As Double
                    Dim aux As Double
                    aux = Double.Parse(Tabla1.Rows(i)(2).ToString) / 100
                    ivaTotal = Double.Parse(Tabla1.Rows(i)(3).ToString) * aux
                    importe = Double.Parse(Tabla1.Rows(i)(3).ToString) - ivaTotal

                    dr2 = TablaFull.NewRow()

                    dr2("Proveedor") = Tabla1.Rows(i)(1).ToString
                    dr2("RFC") = P.buscarRFCPorveedor(Tabla1.Rows(i)(1).ToString)
                    dr2("IVA") = Tabla1.Rows(i)(2).ToString + "%"
                    dr2("Importe") = "$" + importe.ToString()
                    dr2("IVAPagado") = "$" + ivaTotal.ToString()
                    dr2("Total") = "$" + Tabla1.Rows(i)(3).ToString
                    If P.buscarNumPoliza(Tabla1.Rows(i)(0).ToString) <> "" Then
                        dr2("Poliza") = P.buscarNumPoliza(Tabla1.Rows(i)(0).ToString)
                    Else
                        dr2("Poliza") = " "
                    End If

                    dr2("Fecha") = Tabla1.Rows(i)(4).ToString
                    dr2("Tipo") = Tabla1.Rows(i)(5).ToString
                    dr2("Folio") = Tabla1.Rows(i)(6).ToString
                    dr2("Referencia") = Tabla1.Rows(i)(7).ToString
                    TablaFull.Rows.Add(dr2)


                    Tabla1.Rows(i)(1) = " "
                    temoFilas = temoFilas + 1

                    importe1 = importe1 + importe
                    IVA1 = IVA1 + ivaTotal
                    total1 = total1 + Double.Parse(Tabla1.Rows(i)(3).ToString)

                End If
            Next
            Dim dr3 As DataRow
            dr3 = TablaFull.NewRow()

            dr3("Proveedor") = " "
            dr3("RFC") = P.buscarRFCPorveedor(nombre)
            dr3("IVA") = " "
            dr3("Importe") = "$" + Format(importe1, "0.00")
            dr3("IVAPagado") = "$" + Format(IVA1, "0.00")
            dr3("Total") = "$" + Format(total1, "0.00")
            dr3("Poliza") = " "
            dr3("Fecha") = " "
            dr3("Tipo") = " "
            dr3("Folio") = " "
            dr3("Referencia") = " "
            TablaFull.Rows.Add(dr3)

           
        Loop


        

        Dim DS As DataSet = New DataSet
        DS.Tables.Add(TablaFull)
        DS.WriteXmlSchema("tblReporteDIOT.xml")
        Return DS
    End Function
    Private Function Deposito() As DataSet

        Dim Tabla1 As DataTable
        ' Dim Tabla2 As DataTable
        Dim TablaFull As New DataTable

        TablaFull.Columns.Add("Fecha")
        TablaFull.Columns.Add("Referencia")
        TablaFull.Columns.Add("Cuenta 1")
        TablaFull.Columns.Add("Cantidad")
        TablaFull.Columns.Add("Cuenta 2")


        idBanco = IdsCuentas.Valor(cmbCuenta.SelectedIndex)
        Dim P As New dbDeposito(MySqlcon)

        Tabla1 = P.DepositoRep(dtpFecha.Value.ToString("yyyy/MM/dd"), dtpFechaHasta.Value.ToString("yyyy/MM/dd"), idBanco)
        For i As Integer = 0 To Tabla1.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = TablaFull.NewRow()
            dr1("Fecha") = Tabla1.Rows(i)(1).ToString
            dr1("Referencia") = Tabla1.Rows(i)(2).ToString
            dr1("Cuenta 1") = P.BuscarCuenta(Integer.Parse(Tabla1.Rows(i)(3).ToString))
            dr1("Cantidad") = Tabla1.Rows(i)(4).ToString

            dr1("Cuenta 2") = P.BuscarCuenta(Integer.Parse(Tabla1.Rows(i)(6).ToString))

            TablaFull.Rows.Add(dr1)


        Next


        Dim DS As DataSet = New DataSet
        DS.Tables.Add(TablaFull)
        DS.WriteXmlSchema("tblDepositoReP.xml")
        Return DS
    End Function

    Private Sub cmbCuenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCuenta.SelectedIndexChanged

    End Sub

    Private Sub dtpFechaHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpFechaHasta.ValueChanged

    End Sub
End Class