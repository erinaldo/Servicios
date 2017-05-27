Public Class frmEmpeniosConsultaMovimientos

    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean = True
    Dim cMov As New dbEmpeniosConsultaMovimientos(MySqlcon)
    Dim tablaFinal As New DataTable()
    Dim tablaInterese As New DataTable()
    Dim tablaSaldoInicial As New DataTable()
    Dim totalIntereses As Double = 0
    Dim saldoInicial As Double
    Dim totalCargo As Double = 0
    Dim totalAbono As Double = 0
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim ColorAzul As Color = Color.FromArgb(200, 200, 255)
    Dim ColorMorado As Color = Color.FromArgb(240, 180, 240)
    Dim ColorNaranja As Color = Color.FromArgb(232, 151, 98)
    Dim Seleccionado As Integer = 0
    Dim contador As Integer = 0


    Private Sub frmEmpeniosConsultaMovimientos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        dtpFechaInicio.Value = "01/" + Date.Now.Month.ToString("00") + "/" + Date.Now.Year.ToString
        tablaFinal.Columns.Add("ID")
        tablaFinal.Columns.Add("Fecha")
        tablaFinal.Columns.Add("Movimiento")
        tablaFinal.Columns.Add("Serie")
        tablaFinal.Columns.Add("Folio")
        tablaFinal.Columns.Add("Cargo")
        tablaFinal.Columns.Add("Abono")
        '  tablaFinal.Columns.Add("Interes")
        tablaFinal.Columns.Add("Saldo")
        tablaFinal.Columns.Add("Pago")

        tablaInterese.Columns.Add("SerieFolio")
        tablaInterese.Columns.Add("Total")

        tablaSaldoInicial.Columns.Add("SerieFolio")
        tablaSaldoInicial.Columns.Add("Total")

    End Sub
    Private Sub txtClienteCodigo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClienteCodigo.TextChanged
        BuscaCliente()
        If chkTiempoReal.Checked = True And IdCliente <> 0 Then
            Buscar()
        End If
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(txtClienteCodigo.Text) Then
                    txtClienteDatos.Text = c.Nombre
                    IdCliente = c.ID
                    'Consulta()
                Else
                    IdCliente = 0
                    txtClienteDatos.Text = ""
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtClienteDatos.Text = B.Cliente.Nombre
            IdCliente = B.Cliente.ID
            ConsultaOn = False
            txtClienteCodigo.Text = B.Cliente.Clave
            ConsultaOn = True
            'Consulta()
        End If
    End Sub

    Private Sub dtpFechaInicio_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaInicio.ValueChanged
        dtpFechaFinal.MinDate = dtpFechaInicio.Value
        If chkTiempoReal.Checked = True And IdCliente <> 0 Then
            Buscar()
        End If
    End Sub
    Private Sub calculaSaldoInicial()
        Dim tablaEmeniosSI As DataTable
        Dim tablaPagosSI As DataTable
        Dim fechaInicial As Date
        Dim FechaAux As Date
        Dim dias As Long
        Dim NextDay As Date
        Dim suma As Double
        tablaEmeniosSI = cMov.ConsultaEmpeniosSI(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), dtpFechaFinal.Value.ToString("yyyy/MM/dd"), IdCliente)
        tablaPagosSI = cMov.ConsultaPagosSI(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), dtpFechaFinal.Value.ToString("yyyy/MM/dd"), IdCliente)
        'Saca la pruimer fecha
        If tablaEmeniosSI.Rows.Count > 0 Or tablaPagosSI.Rows.Count > 0 Then
            If tablaEmeniosSI.Rows.Count > 0 Then
                fechaInicial = Date.Parse(tablaEmeniosSI.Rows(0)(1).ToString())
            Else
                fechaInicial = Date.Parse(tablaPagosSI.Rows(0)(0).ToString("yyyy/MM/dd"))
            End If
        Else
            saldoInicial = 0
        End If

        'calcula cuantos dias son
        dias = DateDiff(DateInterval.Day, fechaInicial, dtpFechaInicio.Value)

        'hacer el recorrido
        FechaAux = fechaInicial
        For i As Integer = 0 To dias


            NextDay = DateAdd(DateInterval.Day, i, FechaAux)

            'Emenios
            For j As Integer = 0 To tablaEmeniosSI.Rows.Count - 1

                If tablaEmeniosSI.Rows(j)(1).ToString <= NextDay.ToString("yyyy/MM/dd") Then
                    If tablaEmeniosSI.Rows(j)(1).ToString = NextDay.ToString("yyyy/MM/dd") Then
                        
                        calculaInteres(tablaEmeniosSI.Rows(j)(3).ToString + "-" + tablaEmeniosSI.Rows(j)(2).ToString, Double.Parse(tablaEmeniosSI.Rows(j)(4).ToString))
                        
                    End If
                Else
                    j = tablaEmeniosSI.Rows.Count - 1
                End If
            Next
            'Pagos


            For j As Integer = 0 To tablaPagosSI.Rows.Count - 1
                If tablaPagosSI.Rows(j)(0).ToString <= NextDay.ToString("yyyy/MM/dd") Then
                    If tablaPagosSI.Rows(j)(0).ToString = NextDay.ToString("yyyy/MM/dd") Then
                        suma = (Double.Parse(tablaPagosSI.Rows(j)(4).ToString) + Double.Parse(tablaPagosSI.Rows(j)(5).ToString)) - Double.Parse(tablaPagosSI.Rows(j)(3).ToString)
                        calculaInteres(tablaPagosSI.Rows(j)(2).ToString + "-" + tablaPagosSI.Rows(j)(1).ToString, suma)
                    End If
                Else
                    j = tablaPagosSI.Rows.Count - 1
                End If
            Next


        Next
        saldoInicial = totalIntereses


    End Sub

    Private Sub btnVerMovimientos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVerMovimientos.Click
        If IdCliente <> 0 Then
            Buscar()
        End If

    End Sub
    Private Sub Buscar()
        'Dim dias As Long = DateDiff(DateInterval.Day, dtpFechaInicio.Value, dtpFechaFinal.Value)
        'Dim NextDay As Date
        'Dim FechaAux As Date = dtpFechaInicio.Value.ToString("yyyy/MM/dd")
        'Dim tablaEmenios As DataTable
        'Dim tablaPagos As DataTable
        'Dim tablaAdjudicados As DataTable
        'Dim tablaCompras As DataTable
        'Seleccionado = 0
        'Dim Primer As Boolean = True
        'Dim totalPagos As Double = 0
        'Dim totalEmpenios As Double = 0
        'Dim suma As Double = 0
        'totalCargo = 0
        'totalAbono = 0
        'totalIntereses = 0

        'tablaFinal.Clear()

        ''saldoInicial = cMov.calcularSaldoInicial(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), IdCliente)
        'tablaEmenios = cMov.ConsultaEmpenios(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), dtpFechaFinal.Value.ToString("yyyy/MM/dd"), IdCliente)
        'tablaPagos = cMov.ConsultaPagos(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), dtpFechaFinal.Value.ToString("yyyy/MM/dd"), IdCliente)
        'tablaAdjudicados = cMov.ConsultaAdjudicaciones(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), dtpFechaFinal.Value.ToString("yyyy/MM/dd"), IdCliente)
        'tablaCompras = cMov.ConsultaEmpeniosCompras(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), dtpFechaFinal.Value.ToString("yyyy/MM/dd"), IdCliente)
        'calculaSaldoInicial()
        'For i As Integer = 0 To dias


        '    NextDay = DateAdd(DateInterval.Day, i, FechaAux)

        '    'Emenios
        '    For j As Integer = 0 To tablaEmenios.Rows.Count - 1

        '        If tablaEmenios.Rows(j)(1).ToString <= NextDay.ToString("yyyy/MM/dd") Then
        '            If tablaEmenios.Rows(j)(1).ToString = NextDay.ToString("yyyy/MM/dd") Then
        '                Dim dr As DataRow
        '                dr = tablaFinal.NewRow()
        '                dr("ID") = Integer.Parse(tablaEmenios.Rows(j)(0).ToString)
        '                dr("Fecha") = tablaEmenios.Rows(j)(1).ToString
        '                dr("Movimiento") = "Empeño"
        '                dr("Serie") = tablaEmenios.Rows(j)(3).ToString
        '                dr("Folio") = tablaEmenios.Rows(j)(2).ToString
        '                dr("Cargo") = Double.Parse(tablaEmenios.Rows(j)(4).ToString).ToString("C2")
        '                totalCargo += Double.Parse(tablaEmenios.Rows(j)(4).ToString)
        '                dr("Abono") = " "
        '                calculaInteres(tablaEmenios.Rows(j)(3).ToString + "-" + tablaEmenios.Rows(j)(2).ToString, Double.Parse(tablaEmenios.Rows(j)(4).ToString))
        '                ' dr("Interes") = (0.0).ToString("C2")
        '                ' totalEmpenios += Double.Parse(tablaEmenios.Rows(j)(4).ToString)
        '                dr("Saldo") = totalIntereses.ToString("C2")
        '                dr("Pago") = ""
        '                tablaFinal.Rows.Add(dr)
        '            End If
        '        Else
        '            j = tablaEmenios.Rows.Count - 1
        '        End If
        '    Next
        '    'Pagos


        '    For j As Integer = 0 To tablaPagos.Rows.Count - 1
        '        If tablaPagos.Rows(j)(0).ToString <= NextDay.ToString("yyyy/MM/dd") Then
        '            If tablaPagos.Rows(j)(0).ToString = NextDay.ToString("yyyy/MM/dd") Then
        '                Dim dr As DataRow
        '                dr = tablaFinal.NewRow()
        '                dr("ID") = 0
        '                dr("Fecha") = tablaPagos.Rows(j)(0).ToString
        '                dr("Movimiento") = "Abono"
        '                dr("Serie") = tablaPagos.Rows(j)(2).ToString
        '                dr("Folio") = tablaPagos.Rows(j)(1).ToString
        '                dr("Cargo") = " "
        '                dr("Abono") = Double.Parse(tablaPagos.Rows(j)(3).ToString).ToString("C2")
        '                totalAbono += Double.Parse(tablaPagos.Rows(j)(3).ToString)
        '                ' dr("Interes") = Double.Parse(tablaPagos.Rows(j)(5).ToString).ToString("C2")
        '                '  totalPagos = Double.Parse(tablaPagos.Rows(j)(3).ToString)
        '                suma = (Double.Parse(tablaPagos.Rows(j)(4).ToString) + Double.Parse(tablaPagos.Rows(j)(5).ToString)) - Double.Parse(tablaPagos.Rows(j)(3).ToString)
        '                calculaInteres(tablaPagos.Rows(j)(2).ToString + "-" + tablaPagos.Rows(j)(1).ToString, suma)
        '                dr("Saldo") = totalIntereses.ToString("C2")
        '                If tablaPagos.Rows(j)(6).ToString = "False" Then
        '                    dr("Pago") = "Interés/Empeño"
        '                Else
        '                    If tablaPagos.Rows(j)(6).ToString = "-1" Then
        '                        dr("Pago") = "Descuento"
        '                    Else
        '                        dr("Pago") = "Interés"
        '                    End If


        '                End If

        '                tablaFinal.Rows.Add(dr)
        '            End If
        '        Else
        '            j = tablaPagos.Rows.Count - 1
        '        End If
        '    Next
        '    'Adjudicaciones
        '    For j As Integer = 0 To tablaAdjudicados.Rows.Count - 1
        '        If tablaAdjudicados.Rows(j)(1).ToString <= NextDay.ToString("yyyy/MM/dd") Then
        '            If tablaAdjudicados.Rows(j)(1).ToString = NextDay.ToString("yyyy/MM/dd") Then
        '                Dim dr As DataRow
        '                dr = tablaFinal.NewRow()
        '                dr("ID") = 0
        '                dr("Fecha") = tablaAdjudicados.Rows(j)(1).ToString
        '                dr("Movimiento") = "Adjudicación"
        '                dr("Serie") = tablaAdjudicados.Rows(j)(3).ToString
        '                dr("Folio") = tablaAdjudicados.Rows(j)(2).ToString
        '                dr("Cargo") = " "
        '                dr("Abono") = Double.Parse(tablaAdjudicados.Rows(j)(4).ToString).ToString("C2")
        '                calculaInteres(tablaAdjudicados.Rows(j)(3).ToString + "-" + tablaAdjudicados.Rows(j)(2).ToString, 0)
        '                dr("Saldo") = totalIntereses.ToString("C2")
        '                dr("Pago") = " "
        '                totalAbono += Double.Parse(tablaAdjudicados.Rows(j)(4).ToString)

        '                tablaFinal.Rows.Add(dr)
        '            End If
        '        Else
        '            j = tablaAdjudicados.Rows.Count - 1
        '        End If
        '    Next
        '    'compras
        '    'For j As Integer = 0 To tablaCompras.Rows.Count - 1

        '    '    If tablaCompras.Rows(j)(1).ToString <= NextDay.ToString("yyyy/MM/dd") Then
        '    '        If tablaCompras.Rows(j)(1).ToString = NextDay.ToString("yyyy/MM/dd") Then
        '    '            Dim dr As DataRow
        '    '            dr = tablaFinal.NewRow()
        '    '            dr("ID") = Integer.Parse(tablaCompras.Rows(j)(0).ToString)
        '    '            dr("Fecha") = tablaCompras.Rows(j)(1).ToString
        '    '            dr("Movimiento") = "Compra"
        '    '            dr("Serie") = tablaCompras.Rows(j)(3).ToString
        '    '            dr("Folio") = tablaCompras.Rows(j)(2).ToString
        '    '            dr("Cargo") = Double.Parse(tablaCompras.Rows(j)(4).ToString).ToString("C2")
        '    '            ' totalCargo += Double.Parse(tablaEmenios.Rows(j)(4).ToString)
        '    '            dr("Abono") = Double.Parse(tablaCompras.Rows(j)(4).ToString).ToString("C2")
        '    '            'calculaInteres(tablaEmenios.Rows(j)(3).ToString + "-" + tablaEmenios.Rows(j)(2).ToString, Double.Parse(tablaEmenios.Rows(j)(4).ToString))
        '    '            ' dr("Interes") = (0.0).ToString("C2")
        '    '            ' totalEmpenios += Double.Parse(tablaEmenios.Rows(j)(4).ToString)
        '    '            dr("Saldo") = totalIntereses.ToString("C2")
        '    '            dr("Pago") = ""
        '    '            tablaFinal.Rows.Add(dr)
        '    '        End If
        '    '    Else
        '    '        j = tablaEmenios.Rows.Count - 1
        '    '    End If
        '    'Next
        'Next

        'If contador = 0 Then
        '    contador += 1
        '    Dim dataSet As DataSet = New DataSet
        '    dataSet.Tables.Clear()
        '    dataSet.Tables.Add(tablaFinal)
        '    'dataSet.WriteXmlSchema("tblEmpeniosMovimientos.xml")
        'End If
        Dim EC As New dbEmpeniosConfiguracion(MySqlcon)
        EC.LlenaDatos()
        DGServicios.DataSource = Nothing
        DGServicios.DataSource = cMov.ConsultaMovimientos(dtpFechaInicio.Value.ToString("yyyy/MM/dd"), dtpFechaFinal.Value.ToString("yyyy/MM/dd"), IdCliente, EC.Vis)
        'id,fecha,movimiento,serie,folio,importe,cargo,abono,saldo,pago,estado,hora,idcliente
        DGServicios.Columns(1).HeaderText = "Fecha"
        DGServicios.Columns(2).HeaderText = "Movimiento"
        DGServicios.Columns(3).HeaderText = "Folio"
        DGServicios.Columns(5).HeaderText = "Importe"
        ' DGServicios.Columns(4).HeaderText = "Folio"
        'DGServicios.Columns(5).HeaderText = "Cargo"
        DGServicios.Columns(6).HeaderText = "Abono"
        DGServicios.Columns(9).HeaderText = "Tipo"
        DGServicios.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DGServicios.Columns(0).Visible = False
        DGServicios.Columns(4).Visible = False
        'DGServicios.Columns(5).Visible = False
        DGServicios.Columns(6).Visible = False
        DGServicios.Columns(7).Visible = False
        DGServicios.Columns(8).Visible = False
        DGServicios.Columns(10).Visible = False
        DGServicios.Columns(11).Visible = False
        DGServicios.Columns(12).Visible = False
    End Sub

    Private Sub calculaInteres(ByVal serieFolio As String, ByVal interes As Double)
        totalIntereses = 0
        For i As Integer = 0 To tablaInterese.Rows.Count - 1
            If tablaInterese.Rows(i)(0).ToString = serieFolio Then
                tablaInterese.Rows(i).Delete()
                i = tablaInterese.Rows.Count

            End If
        Next

        Dim dr As DataRow
        dr = tablaInterese.NewRow()
        dr("SerieFolio") = serieFolio
        dr("Total") = interes
        tablaInterese.Rows.Add(dr)

        For i As Integer = 0 To tablaInterese.Rows.Count - 1

            totalIntereses += Double.Parse(tablaInterese.Rows(i)(1).ToString)
        Next
        totalIntereses = totalIntereses
    End Sub

    Private Sub DGServicios_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        Select Case DGServicios.Item(2, e.RowIndex).Value
            Case "Abono"
                e.CellStyle.BackColor = ColorVerde
            Case "Empeño"
                e.CellStyle.BackColor = ColorAzul
            Case "Adjudicación"
                e.CellStyle.BackColor = ColorMorado
            Case "Compra"
                e.CellStyle.BackColor = ColorNaranja
        End Select
        If e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "$#,###,##0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Seleccionado = 1 Then
            AbreDocumento(DGServicios.CurrentRow.Index)
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub AbreDocumento(ByVal Row As Integer)
        If DGServicios.Item(0, Row).Value <> "0" Then
            Dim sa As New dbSucursalesArchivos
            sa.DaOpciones(GlobalIdEmpresa, False)
            Dim f As New frmEmpenios(DGServicios.Item(0, Row).Value, sa.idCaja, 0, GlobalIdSucursalDefault, "", -1, False)
            f.ShowDialog()
            f.Dispose()
        End If
        
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        If IdCliente <> 0 Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim C As New dbClientes(MySqlcon)
            Dim O As New dbOpciones(MySqlcon)
            Rep = New repEmpeniosMovimientosN
            Rep.SetDataSource(cMov.UltimaConsulta(IdCliente))
            Rep.SetParameterValue("Encabezado", S.Nombre)
            'Rep.SetParameterValue("saldoAnterior", saldoInicial.ToString("c2"))
            Rep.SetParameterValue("dcliente", txtClienteCodigo.Text + " " + txtClienteDatos.Text)
            Rep.SetParameterValue("fechas", Format(dtpFechaInicio.Value, "dd/MM/yyyy") + " al " + Format(dtpFechaFinal.Value, "dd/MM/yyyy"))
            'Rep.SetParameterValue("totalSaldo", totalIntereses.ToString("C2"))
            'Rep.SetParameterValue("totalCargo", totalCargo.ToString("C2"))
            'Rep.SetParameterValue("totalAbono", totalAbono.ToString("C2"))
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If

    End Sub

    Private Sub txtClienteCodigo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtClienteCodigo.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If IdCliente <> 0 Then
                Buscar()
            End If
        End If
       
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        Seleccionado = 1
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub dtpFechaFinal_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaFinal.ValueChanged
        If chkTiempoReal.Checked = True And IdCliente <> 0 Then
            Buscar()
        End If
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        If Seleccionado = 1 Then
            AbreDocumento(DGServicios.CurrentRow.Index)
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub DGServicios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub
End Class
