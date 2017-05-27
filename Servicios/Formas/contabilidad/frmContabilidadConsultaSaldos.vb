Public Class frmContabilidadConsultaSaldos
    'Dim p As New dbContabilidadPolizas(MySqlcon)
    'Dim idCuentas As Integer

    'Dim Pediodo As String
    Private Sub frmContabilidadConsultaSaldos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        'Pediodo = p.buscarPeriodo
        sc.C = New dbContabilidadClasificacion(MySqlcon)
        sc.P = New dbContabilidadPolizas(MySqlcon)
        sc.Inicializar()
        sc.SoloUltimoNivel = False
        bcc.P = New dbContabilidadClasificacion(MySqlcon)
        'bcc.CambiaAncho(892)
        Label10.Text = "Ejercicio: " + sc.P.anio
    End Sub

    'Private Sub dgvCuentas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCuentas.CellClick
    '    cargarDatos()
    'End Sub

    Private Sub dgvCuentas_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    'Private Sub dgvCuentas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvCuentas.CellFormatting
    '    If e.ColumnIndex = 7 Then
    '        If dgvCuentas.Item(1, e.RowIndex).Value = 0 Then
    '            e.Value = Double.Parse(dgvCuentas.Item(4, e.RowIndex).Value + dgvCuentas.Item(5, e.RowIndex).Value - dgvCuentas.Item(6, e.RowIndex).Value).ToString("###,##0.00")
    '        Else
    '            e.Value = Double.Parse(dgvCuentas.Item(4, e.RowIndex).Value - dgvCuentas.Item(5, e.RowIndex).Value + dgvCuentas.Item(6, e.RowIndex).Value).ToString("###,##0.00")
    '        End If
    '    End If

    '    If e.ColumnIndex >= 4 And e.ColumnIndex <= 7 Then
    '        If e.Value < 0 Then
    '            e.CellStyle.ForeColor = Color.Red
    '        End If
    '    End If
    'End Sub




    Private Sub cargarDatos()
        'btnSeleccionar.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        dgvCuenta.DataSource = sc.P.consultaSaldosMes(sc.IdCuenta, sc.Nivel, GlobalLicenciaSTR)
        'lblDescripcion.Text = "CUENTA: " + sc.txtDesc.Text
        'btnSeleccionar.Enabled = True
        Dim TCargos As Double = 0
        Dim TAbobos As Double = 0
        Dim R As Integer
        While R < dgvCuenta.RowCount
            TCargos += dgvCuenta.Item(3, R).Value
            TAbobos += dgvCuenta.Item(4, R).Value
            R += 1
        End While
        lblCargos.Text = TCargos.ToString("N2")
        lblAbonos.Text = TAbobos.ToString("N2")
        If TCargos >= 0 Then
            lblCargos.ForeColor = Color.Black
        Else
            lblCargos.ForeColor = Color.Red
        End If
        If TAbobos >= 0 Then
            lblAbonos.ForeColor = Color.Black
        Else
            lblAbonos.ForeColor = Color.Red
        End If
        Label2.Text = CDbl(dgvCuenta.Item(2, 0).Value).ToString("#,###,###,##0.00")
        If CDbl(Label2.Text) >= 0 Then
            Label2.ForeColor = Color.Black
        Else
            Label2.ForeColor = Color.Red
        End If
        Label4.Text = CDbl(dgvCuenta.Item(5, 11).Value).ToString("#,###,###,##0.00")
        If CDbl(Label4.Text) >= 0 Then
            Label4.ForeColor = Color.Black
        Else
            Label4.ForeColor = Color.Red
        End If
        Me.Cursor = Cursors.Default
        'btnImprimir.Enabled = True
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim c As New dbContabilidadPolizas(MySqlcon)

        Rep = New repContabilidadSaldos

        Rep.SetDataSource(sc.P.consultaSaldosMes(sc.IdCuenta, sc.Nivel, GlobalLicenciaSTR))
        Rep.SetParameterValue("empresa", s.Nombre)
        Rep.SetParameterValue("rfc", s.RFC)
        Rep.SetParameterValue("CUENTA", Label1.Text)
        Rep.SetParameterValue("ejercicio", c.buscarPeriodo)
        ' Rep.SetParameterValue("FECHABALANZA", Format(dtpdesde.Value, "MMMM").ToUpper + " " + dtpdesde.Value.Year.ToString())
        ' Rep.SetParameterValue("filtros", "CUENTA: " + txtNivel1.Text + " " + txtNivel2.Text + " " + txtNivel3.Text + " " + txtNivel4.Text + " " + txtNivel5.Text)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub dgvCuenta_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCuenta.CellContentClick

    End Sub

    Private Sub dgvCuenta_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCuenta.CellDoubleClick
        If dgvCuenta.CurrentCell.RowIndex >= 0 Then
            Dim Mes As String
            Dim Fecha1 As String = ""
            Dim Fecha2 As String = ""
            Mes = dgvCuenta.Item(1, dgvCuenta.CurrentCell.RowIndex).Value
            Select Case Mes
                Case "ENERO"
                    Fecha1 = sc.P.anio + "/01/01"
                    Fecha2 = sc.P.anio + "/01/31"
                Case "FEBRERO"
                    Fecha1 = sc.P.anio + "/02/01"
                    Fecha2 = sc.P.anio + "/02/29"
                Case "MARZO"
                    Fecha1 = sc.P.anio + "/03/01"
                    Fecha2 = sc.P.anio + "/03/31"
                Case "ABRIL"
                    Fecha1 = sc.P.anio + "/04/01"
                    Fecha2 = sc.P.anio + "/04/31"
                Case "MAYO"
                    Fecha1 = sc.P.anio + "/05/01"
                    Fecha2 = sc.P.anio + "/05/31"
                Case "JUNIO"
                    Fecha1 = sc.P.anio + "/06/01"
                    Fecha2 = sc.P.anio + "/06/31"
                Case "JULIO"
                    Fecha1 = sc.P.anio + "/07/01"
                    Fecha2 = sc.P.anio + "/07/31"
                Case "AGOSTO"
                    Fecha1 = sc.P.anio + "/08/01"
                    Fecha2 = sc.P.anio + "/08/31"
                Case "SEPTIEMBRE"
                    Fecha1 = sc.P.anio + "/09/01"
                    Fecha2 = sc.P.anio + "/09/31"
                Case "OCTUBRE"
                    Fecha1 = sc.P.anio + "/10/01"
                    Fecha2 = sc.P.anio + "/10/31"
                Case "NOVIEMBRE"
                    Fecha1 = sc.P.anio + "/11/01"
                    Fecha2 = sc.P.anio + "/11/31"
                Case "DICIEMBRE"
                    Fecha1 = sc.P.anio + "/12/01"
                    Fecha2 = sc.P.anio + "/12/31"
            End Select
            Dim Faux As New frmContabilidadAuxiliarCuentas(Fecha1, Fecha2, sc.IdCuenta, Label1.Text + " PERIODO: " + Mes + " " + sc.P.anio, Format(dgvCuenta.Item(2, dgvCuenta.CurrentCell.RowIndex).Value, "###,###,##0.00"), Format(dgvCuenta.Item(5, dgvCuenta.CurrentCell.RowIndex).Value, "###,###,##0.00"), Format(dgvCuenta.Item(3, dgvCuenta.CurrentCell.RowIndex).Value, "###,###,##0.00"), Format(dgvCuenta.Item(4, dgvCuenta.CurrentCell.RowIndex).Value, "###,###,##0.00"))
            Faux.ShowDialog()
            Faux.Dispose()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub sc_CambiaID() Handles sc.CambiaID
        If sc.IdCuenta <> 0 Then
            cargarDatos()
            Label1.Text = "CUENTA: " + sc.P.DNiv1
            If sc.P.DNiv2 <> "" Then Label1.Text += " - " + sc.P.DNiv2
            If sc.P.DNiv3 <> "" Then Label1.Text += " - " + sc.P.DNiv3
            If sc.P.DNiv4 <> "" Then Label1.Text += " - " + sc.P.DNiv4
            If sc.P.DNiv5 <> "" Then Label1.Text += " - " + sc.P.DNiv5
        Else
            Label1.Text = "CUENTA:"
            'dgvCuenta.Rows.Clear()
        End If
    End Sub

    Private Sub sc_KeyDown(sender As Object, e As KeyEventArgs) Handles sc.KeyDown
        If e.KeyCode = Keys.Enter And sc.ActivaBuscador Then
            sc.LlenaCuenta(bcc.DaId)
        End If
        If sc.ActivaBuscador Then
            Select Case e.KeyCode
                Case Keys.Up
                    bcc.PresionaArriba()
                Case Keys.Down
                    bcc.PresionaAbajo()
                Case Keys.Left
                Case Keys.Right
                Case Else
                    bcc.Visible = True
                    bcc.filtroCuenta = Trim(sc.txtCuenta.Text.Trim + " " + sc.txtN2.Text.Trim + " " + sc.txtN3.Text.Trim + " " + sc.txtN4.Text.Trim + " " + sc.txtN5.Text.Trim)
                    bcc.Consulta()
            End Select
        Else
            bcc.Visible = False
        End If
    End Sub

   
    Private Sub sc_Load(sender As Object, e As EventArgs) Handles sc.Load

    End Sub

    Private Sub bcc_Click(sender As Object, e As EventArgs) Handles bcc.Click
        If bcc.ID <> 0 Then
            sc.ActivaBuscador = False
            bcc.Visible = False
            My.Application.DoEvents()
            sc.LlenaCuenta(bcc.ID)
        End If
    End Sub
End Class