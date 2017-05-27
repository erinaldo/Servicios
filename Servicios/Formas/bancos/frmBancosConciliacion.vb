Public Class frmBancosConciliacion
    Dim Mes2, anio As String
    Dim Dt As DataTable
    Dim Cargando As Integer = 0
    Dim idCuenta As Integer
    Dim bandera As Integer = 0
    Dim si As Boolean = False
    Dim ConsultaOn As Boolean
    Dim IdsCuentas As New elemento
    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        Dim tmpc As Boolean = ConsultaOn
        ConsultaOn = False
        dtpFechaHasta.Value = (Date.Parse("01/" + dtpFecha.Value.Month.ToString + "/" + dtpFecha.Value.Year.ToString).AddMonths(1)).AddDays(-1)
        ConsultaOn = tmpc
        ConsultaFecha()
    End Sub

    Private Sub frmConciliacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim year As String
        Dim month As String
        ConsultaOn = False
        year = Date.Now.Year.ToString
        month = Format(Date.Now.Month, "00")
        dtpFecha.Value = "01/" + month + "/" + year.ToString
        LlenaCombos("tblcuentas", cmbCuenta, "concat(numero,' ',(select nombre from tblbancoscatalogo where idbanco=banco))", "cuentaN", "idcuenta", IdsCuentas)
        dgvSistema.AutoGenerateColumns = False
        'dtpFechaHasta.MinDate = dtpFecha.Value
        Mes2 = Date.Now.Month.ToString()
        anio = Date.Now.Year.ToString()
        'llenarCuenta()

        Cargando = 1
        ConsultaOn = True
        ConsultaFecha()
    End Sub


    Public Function fechaFormato(ByVal pfecha As Date) As String
        Dim Dia As String
        Dim Mes As String
        Dim year As String
        Dim fechita As String

        Dia = Format(pfecha.Date.Day, "00")
        Mes = Format(pfecha.Date.Month, "00")
        year = pfecha.Date.Year
        fechita = year + "/" + Mes + "/" + Dia
        Return fechita

    End Function

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSistema.CellClick

        If e.ColumnIndex <> 0 Then Exit Sub
        Try
            If dgvSistema.Rows(e.RowIndex).Cells(0).Value = 0 Then
                dgvSistema.Rows(e.RowIndex).Cells(0).Value = 1
            Else
                dgvSistema.Rows(e.RowIndex).Cells(0).Value = 0
            End If

        Catch ex As Exception
        End Try
        Cuentas()

    End Sub

    Private Sub seleccionar()
        Dim nFilas As Integer

        nFilas = dgvSistema.Rows.Count()
        For i As Integer = 0 To nFilas - 1
            dgvSistema.EndEdit()
            dgvSistema.Rows(i).Cells(0).Value = 1
        Next
        Cuentas()
        dgvSistema.Refresh()
    End Sub
    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        seleccionar()
    End Sub

    Private Sub Cuentas()
        Dim TotPagosSelec As Double = 0
        Dim TotPagosUnSelec As Double = 0
        Dim TotDepSelec As Double = 0
        Dim TotDepUnSelec As Double = 0
        Dim nfilas As Integer
        Dim aux As String = ""
        'Pagos
        nfilas = dgvSistema.Rows.Count()
        For i As Integer = 0 To nfilas - 1
            If dgvSistema.Item(colAbono.Index, i).Value IsNot DBNull.Value Then
                If dgvSistema.Item(0, i).Value = 1 Then 'Esta seleccionado
                    TotDepSelec += Double.Parse(dgvSistema.Item(colAbono.Index, i).Value.ToString)
                Else 'No esta seleccionado
                    TotDepUnSelec += Double.Parse(dgvSistema.Item(colAbono.Index, i).Value.ToString)
                End If
            End If
            If dgvSistema.Item(colCargo.Index, i).Value IsNot DBNull.Value Then
                If dgvSistema.Item(0, i).Value = 1 Then 'Esta seleccionado
                    TotPagosSelec += Double.Parse(dgvSistema.Item(colCargo.Index, i).Value.ToString)
                Else 'No esta seleccionado
                    TotPagosUnSelec += Double.Parse(dgvSistema.Item(colCargo.Index, i).Value.ToString)
                End If
            End If
        Next

        txtTotalPagosSelec.Text = Format(TotPagosSelec, "C2")
        txtTotalpagosUnSelec.Text = Format(TotPagosUnSelec, "C2")
        txtTotalDepSelec.Text = Format(TotDepSelec, "C2")
        txtTotalDepUnSelec.Text = Format(TotDepUnSelec, "C2")
        SaldoActual()
    End Sub
    Private Sub saldoActualTodo()

        Dim SaldoInicial As Double = 0
        Dim aux As Double = 0
        Dim Depositos As Double = 0
        Dim Pagos As Double = 0
        Dim P As New dbCosiliacion(MySqlcon)

        SaldoInicial = 0
        aux = (SaldoInicial + Double.Parse(txtTotalDepSelec.Text)) - Double.Parse(txtTotalPagosSelec.Text)
        txtSaldoInicial.Text = Format(SaldoInicial, "C2")
        txtSaldoActual.Text = Format(aux, "C2")

    End Sub
    Private Sub SaldoActual()

        'Dim Tabla1 As DataTable
        'Dim Tabla2 As DataTable
        Dim SaldoInicial As Double = 0
        Dim aux As Double = 0
        'Dim nFilas As Integer
        Dim Depositos As Double = 0
        Dim Pagos As Double = 0
        Dim P As New dbCosiliacion(MySqlcon)
        si = True
        'Tabla1 = P.filtroFecha2(fechaFormato(dtpFecha)) 'error v
        'Tabla2 = P.filtroFechaPagos2(fechaFormato(dtpFecha))
        Depositos = P.filtroFecha2(dtpFecha.Value.ToString("yyyy/MM/dd"), IdsCuentas.Valor(cmbCuenta.SelectedIndex)) 'error v
        Pagos = P.filtroFechaPagos2(dtpFecha.Value.ToString("yyyy/MM/dd"), IdsCuentas.Valor(cmbCuenta.SelectedIndex))
        si = False
        'nFilas = Tabla1.Rows.Count
        'For i As Integer = 0 To nFilas - 1
        '    Depositos = Depositos + Double.Parse(Tabla1.Rows(i)(0).ToString)
        'Next
        'nFilas = Tabla2.Rows.Count
        'For i As Integer = 0 To nFilas - 1
        '    Pagos = Pagos + Double.Parse(Tabla2.Rows(i)(0).ToString)
        'Next

        SaldoInicial = Depositos - Pagos

        aux = SaldoInicial + CDbl(txtTotalDepSelec.Text) - CDbl(txtTotalPagosSelec.Text)

        txtSaldoInicial.Text = Format(SaldoInicial, "C2")
        txtSaldoActual.Text = Format(aux, "C2")

    End Sub
    Private Sub SaldoActualEspecifico()

        Dim SaldoInicial As Double = 0
        Dim aux As Double = 0
        Dim Depositos As Double = 0
        Dim Pagos As Double = 0
        Dim P As New dbCosiliacion(MySqlcon)

        SaldoInicial = Depositos - Pagos
        aux = (SaldoInicial + Double.Parse(txtTotalDepSelec.Text)) - Double.Parse(txtTotalPagosSelec.Text)
        txtSaldoInicial.Text = Format(SaldoInicial, "#,###,##0.00")
        txtSaldoActual.Text = Format(aux, "#,###,##0.00")

    End Sub
    Private Sub ConsultaFecha()
        If ConsultaOn = False Then Exit Sub
        Dim fechaInicial As String
        Dim fechaFinal As String
        Dim Banco As String
        'If chkFecha.Checked = True Then
        fechaInicial = dtpFecha.Value.ToString("yyyy/MM/dd")
        fechaFinal = dtpFechaHasta.Value.ToString("yyyy/MM/dd")

        Banco = IdsCuentas.Valor(cmbCuenta.SelectedIndex).ToString

        Try
            Dim PrimerCeldaRow As Integer = -1
            If dgvSistema.RowCount > 0 Then PrimerCeldaRow = dgvSistema.FirstDisplayedCell.RowIndex
            Dim P As New dbCosiliacion(MySqlcon)
            Dim tabla As DataTable
            Dim TablaFull As New DataTable
            Dim Q As New dbDeposito(MySqlcon)
            tabla = P.filtroPagoProv(fechaInicial, fechaFinal, IdsCuentas.Valor(cmbCuenta.SelectedIndex).ToString).ToTable


            dgvSistema.DataSource = tabla

            If dgvSistema.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then dgvSistema.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Cuentas()

    End Sub

    Private Sub dtpFechaHasta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaHasta.ValueChanged
        'dtpFecha.MaxDate = dtpFechaHasta.Value
        If ConsultaOn Then
            If dtpFecha.Value > dtpFechaHasta.Value Then
                dtpFecha.Value = dtpFechaHasta.Value
            End If
            ConsultaFecha()
        End If
    End Sub


   

    
    Private Sub llenarCuenta()

        Dim P As New dbPagosProveedores(MySqlcon)
        Dt = P.buscarCuenta()
        If Dt.Rows.Count() > 0 Then
            With cmbCuenta
                .DataSource = Dt
                .DisplayMember = "nombre"
                .ValueMember = "Banco"
            End With
        Else
            MsgBox("No hay cuentas de Banco registradas, favor de registrar una.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If

    End Sub

    Private Sub cmbCuenta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCuenta.SelectedIndexChanged
        If ConsultaOn Then ConsultaFecha()
        
    End Sub

   
    Private Sub btnBuscar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        ConsultaFecha()
    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        Dim frm As New OpenFileDialog
        frm.Filter = "Archivo XML (*.xml)|*.xml"
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim doc As New System.Xml.XmlDocument
            doc.Load(frm.FileName)
            Dim arr As New ArrayList
            For Each n As Xml.XmlElement In doc.ChildNodes(2).ChildNodes(4).ChildNodes(0)
                If n.Name = "Row" Then
                    If DateTime.TryParse(n.ChildNodes(0).InnerText, New DateTime) Then
                        arr.Add(New With {.Fecha = CDate(n.ChildNodes(0).InnerText),
                                          .Concepto = n.ChildNodes(1).InnerText,
                                          .Referencia = n.ChildNodes(2).InnerText,
                                          .ReferenciaAmpliada = n.ChildNodes(3).InnerText,
                                          .Cargo = If(IsNumeric(n.ChildNodes(4).InnerText), CDbl(n.ChildNodes(4).InnerText), DBNull.Value),
                                          .Abono = If(IsNumeric(n.ChildNodes(5).InnerText), CDbl(n.ChildNodes(5).InnerText), DBNull.Value)})
                    End If
                End If
            Next
            dgvXML.DataSource = arr
            Dim continuefor As Boolean
            For Each r1 As DataGridViewRow In dgvSistema.Rows
                continuefor = False
                r1.Cells(0).Value = False
                For Each r2 As DataGridViewRow In dgvXML.Rows
                    If r1.Cells(colCargo.Index).Value Is DBNull.Value And r2.Cells(5).Value Is DBNull.Value Then
                        If Not r1.Cells(0).Value And Not r2.Cells(0).Value And
                            r1.Cells(colFecha.Index).Value = r2.Cells(1).Value And
                            r1.Cells(colAbono.Index).Value = r2.Cells(6).Value Then
                            r1.Cells(0).Value = True
                            r2.Cells(0).Value = True
                            continuefor = True
                            Continue For
                        End If
                    ElseIf r1.Cells(colAbono.Index).Value Is DBNull.Value And r2.Cells(6).Value Is DBNull.Value Then
                        If Not r1.Cells(0).Value And Not r2.Cells(0).Value And
                            r1.Cells(colFecha.Index).Value = r2.Cells(1).Value And
                            r1.Cells(colCargo.Index).Value = r2.Cells(5).Value Then
                            r1.Cells(0).Value = True
                            r2.Cells(0).Value = True
                            continuefor = True
                            Continue For
                        End If
                    End If
                Next
                If continuefor Then Continue For
            Next

            Cuentas()
        End If
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim rpt As New rptConciliacion
        Dim arr As New ArrayList
        For Each r As DataGridViewRow In dgvSistema.Rows
            If r.Cells(0).Value = 0 Then
                arr.Add(New MovConciliacion() With {.Fecha = CDate(r.Cells(colFecha.Index).Value),
                                      .Referencia = r.Cells(colReferencia.Index).Value,
                                      .Cuenta = r.Cells(colNoCuenta.Index).Value,
                                      .Banco = r.Cells(colBanco.Index).Value,
                                      .Cargo = If(IsNumeric(r.Cells(colCargo.Index).Value), CDbl(r.Cells(colCargo.Index).Value), 0),
                                      .Abono = If(IsNumeric(r.Cells(colAbono.Index).Value), CDbl(r.Cells(colAbono.Index).Value), 0)})
            End If
        Next
        'If arr.Count = 0 Then
        '    arr.Add(New MovConciliacion() With {.Fecha = "",
        '                              .Referencia = "",
        '                              .Cuenta = "",
        '                              .Banco = "Nomostrar",
        '                              .Cargo = 0,
        '                              .Abono = 0})
        'End If
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        rpt.SetDataSource(arr)
        rpt.SetParameterValue("saldo", txtSaldoActual.Text)
        rpt.SetParameterValue("encabezado", S.NombreFiscal)
        rpt.SetParameterValue("fechas", "DEL  " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
        rpt.SetParameterValue("cuenta", cmbCuenta.Text)
        Dim frm As New frmReportes(rpt, False)
        frm.Show()
        'Else
        'MsgBox("No hay elementos no seleccionados.", MsgBoxStyle.Information, GlobalNombreApp)
        'End If
    End Sub
End Class

Public Class MovConciliacion
    Public Fecha As DateTime
    Public Referencia As String
    Public Cuenta As String
    Public Banco As String
    Public Cargo As Double
    Public Abono As Double
End Class
