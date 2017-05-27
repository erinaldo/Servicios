Public Class frmContabilidadDIOT
    Dim p As New dbContabilidadPolizas(MySqlcon)
    Dim periodo As String
    Dim llenando As Boolean = False
    Dim ContTEst As Integer = 0
    Const dataFmt As String = "{0,26}{1,8}{2,26}"
    Private Sub frmContabilidadDIOT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try

        llenando = True
        periodo = p.buscarPeriodo()
        'dtpDesde.MinDate = "01/01/" + periodo
        'dtpHasta.MinDate = "01/01/" + periodo
        'dtpDesde.MaxDate = "31/12/" + periodo
        'dtpHasta.MaxDate = "31/12/" + periodo
        dtpDesde.Value = "01/" + Date.Now.Month.ToString("00") + "/" + periodo
        'dtpHasta.Value = (Date.Parse("01/" + Date.Now.Month.ToString("00") + "/" + periodo).AddMonths(1)).AddDays(-1)


        llenando = False
        dgvCompro.DataSource = p.conciliacionDIOT(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T")
        'dgvCompro.Columns(11).Visible = False
        calculos()
    End Sub
    Private Sub calculos()
        'ContTEst += 1
        'If dgvCompro.RowCount <= 0 Then Exit Sub
        'No tránsito
        'Dim total As Double
        'total = p.TotalIVA(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 0)
        vaLabel11.Text = Format(p.TotalValor(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 1), "###,###,##0.00")
        vaLabel12.Text = Format(p.TotalValor(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 2), "###,###,##0.00")

        ivaLabel14.Text = Format(p.TotalIVA(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 1), "###,###,##0.00")
        ivaLabel17.Text = Format(p.TotalIVA(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 2), "###,###,##0.00")

        ivaretLabel15.Text = Format(p.TotalIVARet(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 1), "###,###,##0.00")
        ivaretLabel19.Text = Format(p.TotalIVARet(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 2), "###,###,##0.00")

        'iepsLabel16.Text = Format(p.TotalIeps(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 1), "###,###,##0.00")
        'iepsLabel21.Text = Format(p.TotalIeps(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T", 2), "###,###,##0.00")

        ivaLabel18.Text = (Double.Parse(ivaLabel14.Text) + Double.Parse(ivaLabel17.Text)).ToString("###,###,##0.00")
        vaLabel13.Text = (Double.Parse(vaLabel11.Text) + Double.Parse(vaLabel12.Text)).ToString("###,###,##0.00")
        ivaretLabel20.Text = (Double.Parse(ivaretLabel15.Text) + Double.Parse(ivaretLabel19.Text)).ToString("###,###,##0.00")
        'iepsLabel22.Text = (Double.Parse(iepsLabel16.Text) + Double.Parse(iepsLabel21.Text)).ToString("###,###,##0.00")
        'iepsLabel22.Text = ContTEst.ToString
    End Sub

    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged
        'Dim LlenandoTemp As Boolean = llenando
        'llenando = True
        dtpHasta.Value = (Date.Parse("01/" + dtpDesde.Value.Month.ToString("00") + "/" + periodo).AddMonths(1)).AddDays(-1)
        'llenando = LlenandoTemp
        If llenando = False Then
            dgvCompro.DataSource = p.conciliacionDIOT(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T")
            'dgvCompro.Columns(11).Visible = False
            calculos()
        End If

    End Sub

    Private Sub dtpHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpHasta.ValueChanged
        ''Dim LlenandoTemp As Boolean = llenando
        ''llenando = False
        ''dtpDesde.Value = "01/" + dtpHasta.Value.Month.ToString("00") + "/" + periodo
        ''llenando = LlenandoTemp
        'If llenando = False Then
        '    dgvCompro.DataSource = p.conciliacionDIOT(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T")
        '    'dgvCompro.Columns(11).Visible = False
        '    calculos()
        'End If

    End Sub

    Private Sub dgvCompro_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompro.CellClick
        'If e.ColumnIndex <> 0 Then Exit Sub
        Try
            If e.ColumnIndex = 0 Then
                If dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0 Then
                    'en transito
                    dgvCompro.Rows(e.RowIndex).Cells(0).Value = 1
                    p.modificarDIOTTran(dgvCompro.Rows(e.RowIndex).Cells(1).Value, 1, dtpDesde.Value.ToString("yyyy/MM/dd"))
                Else
                    'no en transito
                    dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0
                    p.modificarDIOTTran(dgvCompro.Rows(e.RowIndex).Cells(1).Value, 0, dtpHasta.Value.ToString("yyyy/MM/dd"))
                End If
            End If
            calculos()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Me.Close()
    End Sub
   

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        If dtpDesde.Value.Month <> dtpHasta.Value.Month Then
            MsgBox("El rango de fechas debe ser del mismo mes.", MsgBoxStyle.OkOnly, GlobalNombreApp)
        Else
            Try

                'Dim tabla As DataTable
                Dim cadena As String = ""
                Dim CantidadIva As String = ""
                Dim CantidadIvaCero As String = ""
                Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                'tabla = p.generarDIOT(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T")
                DR = p.generarDIOT(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T")
                While DR.Read
                    If DR("ivaret") = 0 And DR("ieps") = 0 Then
                        If cadena <> "" Then cadena += vbCrLf
                        If DR("cantidadiva") <> 0 Then
                            CantidadIva = Math.Round(DR("cantidadiva"), 0).ToString
                        Else
                            CantidadIva = ""
                        End If
                        If DR("cantidadivacero") <> 0 Then
                            CantidadIvaCero = Math.Round(DR("cantidadivacero"), 0).ToString
                        Else
                            CantidadIvaCero = ""
                        End If
                        cadena += "04|85|" + DR("RFC") + "|||||" + CantidadIva + "|||||||||||" + CantidadIvaCero + "||||"
                    End If
                    'If DR("ivap") = "0" Or DR("ivap") = "E" Then
                    '    cadena += "04|85|" + DR("RFC") + "||||||||||||||||" + Math.Round(DR("cantidadiva"), 0).ToString + "||||"
                    'Else
                    '    'IVA 16/15/etc
                    '    'Dim x As Double = tabla.Rows(i)(5).ToString()
                    '    cadena += "04|85|" + DR("RFC") + "|||||" + Math.Round(DR("cantidadiva"), 0).ToString + "|||||||||||||||"
                    'End If
                End While
                DR.Close()
                'For i As Integer = 0 To tabla.Rows.Count - 1
                '    If tabla.Rows(i)(6).ToString = "0" Or tabla.Rows(i)(6).ToString = "E" Then
                '        cadena += "04|85|" + tabla.Rows(i)(3).ToString + "||||||||||||||||" + ShowDecimalRound(Decimal.Parse(tabla.Rows(i)(5).ToString()), 0) + "||||" + vbCrLf
                '    Else
                '        'IVA 16/15/etc
                '        Dim x As Double = tabla.Rows(i)(5).ToString()
                '        cadena += "04|85|" + tabla.Rows(i)(3).ToString + "|||||" + ShowDecimalRound(Decimal.Parse(tabla.Rows(i)(5).ToString()), 0) + "|||||||||||||||" + vbCrLf
                '    End If
                'Next
                If My.Computer.FileSystem.DirectoryExists("C:\SIS_FE\DEM" + dtpDesde.Value.Year.ToString) = False Then
                    My.Computer.FileSystem.CreateDirectory("C:\SIS_FE\DEM" + dtpDesde.Value.Year.ToString)
                End If
                Dim mes As String = MonthName(dtpDesde.Value.Month, True).ToUpper
                Dim fic As String = "C:\SIS_FE\DEM" + dtpDesde.Value.Year.ToString + "\DEC_A29_" + mes + dtpDesde.Value.Year.ToString + ".txt"
                Dim sw As New System.IO.StreamWriter(fic)
                sw.WriteLine(cadena)
                sw.Close()
                PopUp("Guardado", 60)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
            End Try
        End If

       
    End Sub
    Function ShowDecimalRound(Argument As Decimal, Digits As Integer)

        Dim rounded As Decimal = Decimal.Round(Argument, Digits)

        Console.WriteLine(dataFmt, Argument, Digits, rounded)
        Return Double.Parse(rounded).ToString
    End Function

    Private Sub btnCambiar_Click(sender As Object, e As EventArgs) Handles btnCambiar.Click
        For i As Integer = 0 To dgvCompro.Rows.Count - 1
            If dgvCompro.Rows(i).Cells(0).Value = 1 Then
                'p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 0, Date.Parse(dtpDesde.Value.Year.ToString("00") + "/" + (dtpDesde.Value.Month + 1).ToString("00") + "/" + dtpDesde.Value.Day.ToString).ToString("yyyy/MM/dd"))
                p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 0, DateAdd(DateInterval.Month, 1, CDate(dgvCompro.Rows(i).Cells("fechaDiot").Value)).ToString("yyyy/MM/dd"))
            End If
        Next
        dgvCompro.DataSource = p.conciliacionDIOT(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T")
        'dgvCompro.Columns(11).Visible = False
    End Sub

    Private Sub frmContabilidadDIOT_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        For i As Integer = 0 To dgvCompro.Rows.Count - 1
            If dgvCompro.Rows(i).Cells(0).Value = 1 Then
                dgvCompro.Rows(i).Cells(0).Value = 0
                p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 0, dtpDesde.Value.ToString("yyyy/MM/dd"))
            End If
        Next
    End Sub

    Private Sub frmContabilidadDIOT_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        For i As Integer = 0 To dgvCompro.Rows.Count - 1
            If dgvCompro.Rows(i).Cells(0).Value = 1 Then
                dgvCompro.Rows(i).Cells(0).Value = 0
                p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 0, dtpDesde.Value.ToString("yyyy/MM/dd"))
            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        For i As Integer = 0 To dgvCompro.Rows.Count - 1
            
            If dgvCompro.Rows(i).Cells(0).Value = 1 Then
                Dim aux As String() = dgvCompro.Rows(i).Cells("fechaDiot").Value.ToString.Split("/")
                Dim fechaAux As Date = Date.Parse(aux(0) + "/" + aux(1) + "/" + aux(2))
                If DateAdd(DateInterval.Month, -1, dtpDesde.Value).ToString("yyyy/MM/99") < dgvCompro.Rows(i).Cells("fecha").Value Then
                    MsgBox("No se puede pasar este movimiento a un mes anterior al que se realizó. Póliza " + dgvCompro.Rows(i).Cells("TipoPoliza").Value + dgvCompro.Rows(i).Cells("No").Value.ToString, MsgBoxStyle.Information, GlobalNombreApp)
                Else
                    p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 0, DateAdd(DateInterval.Month, -1, CDate(dgvCompro.Rows(i).Cells("fechaDiot").Value)).ToString("yyyy/MM/dd"))
                End If
            End If
        Next
        dgvCompro.DataSource = p.conciliacionDIOT(dtpDesde.Value.ToString("yyyy/MM/dd"), dtpHasta.Value.ToString("yyyy/MM/dd"), "T")
        'dgvCompro.Columns(11).Visible = False
    End Sub

    Private Sub dgvCompro_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompro.CellDoubleClick
        If e.ColumnIndex = 13 Then

            If dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0 Then
                'en  transito
                dgvCompro.Rows(e.RowIndex).Cells(0).Value = 1
                p.modificarDIOTTran(dgvCompro.Rows(e.RowIndex).Cells(1).Value, 1, dtpDesde.Value.ToString("yyyy/MM/dd"))
                For i As Integer = 0 To dgvCompro.Rows.Count - 1
                    If dgvCompro.Rows(i).Cells(0).Value = 0 Then
                        If dgvCompro.Item(13, i).Value = dgvCompro.Item(13, e.RowIndex).Value Then
                            dgvCompro.Rows(i).Cells(0).Value = 1
                            p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 1, dtpDesde.Value.ToString("yyyy/MM/dd"))
                        End If
                    End If
                Next
            Else
                'en no transito
                dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0
                p.modificarDIOTTran(dgvCompro.Rows(e.RowIndex).Cells(1).Value, 0, dtpHasta.Value.ToString("yyyy/MM/dd"))
                For i As Integer = 0 To dgvCompro.Rows.Count - 1
                    If dgvCompro.Rows(i).Cells(0).Value = 1 Then
                        If dgvCompro.Item(13, i).Value = dgvCompro.Item(13, e.RowIndex).Value Then
                            dgvCompro.Rows(i).Cells(0).Value = 0
                            p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 0, dtpHasta.Value.ToString("yyyy/MM/dd"))
                        End If
                    End If
                Next
            End If
        End If

        If e.ColumnIndex = 15 Then

            If dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0 Then
                'en  transito
                dgvCompro.Rows(e.RowIndex).Cells(0).Value = 1
                p.modificarDIOTTran(dgvCompro.Rows(e.RowIndex).Cells(1).Value, 1, dtpDesde.Value.ToString("yyyy/MM/dd"))
                For i As Integer = 0 To dgvCompro.Rows.Count - 1
                    If dgvCompro.Rows(i).Cells(0).Value = 0 Then
                        If dgvCompro.Item(15, i).Value = dgvCompro.Item(15, e.RowIndex).Value Then
                            dgvCompro.Rows(i).Cells(0).Value = 1
                            p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 1, dtpDesde.Value.ToString("yyyy/MM/dd"))
                        End If
                    End If
                Next
            Else
                'en transito
                dgvCompro.Rows(e.RowIndex).Cells(0).Value = 0
                p.modificarDIOTTran(dgvCompro.Rows(e.RowIndex).Cells(1).Value, 0, dtpHasta.Value.ToString("yyyy/MM/dd"))
                For i As Integer = 0 To dgvCompro.Rows.Count - 1
                    If dgvCompro.Rows(i).Cells(0).Value = 1 Then
                        If dgvCompro.Item(15, i).Value = dgvCompro.Item(15, e.RowIndex).Value Then
                            dgvCompro.Rows(i).Cells(0).Value = 0
                            p.modificarDIOTTran(dgvCompro.Rows(i).Cells(1).Value, 0, dtpHasta.Value.ToString("yyyy/MM/dd"))
                        End If
                    End If
                Next
            End If
        End If

        calculos()
    End Sub

    Private Sub dgvCompro_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCompro.CellContentClick

    End Sub
End Class