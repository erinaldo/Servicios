Public Class frmClientesMovimientos
    Public IdCompra As Integer
    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean = True
    Dim Modo As Integer
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorMorado As Color = Color.FromArgb(240, 180, 240)
    Dim ColorAzul As Color = Color.FromArgb(200, 200, 255)
    Dim ColorAmarillo As Color = Color.FromArgb(255, 255, 192)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim ColorNaranja As Color = Color.FromArgb(250, 250, 145)
    Dim ColorDocumento As Color = Color.FromArgb(250, 250, 145)
    Dim Colorremision As Color = Color.FromArgb(190, 210, 190)
    Dim Seleccionado As Byte = 0
    Public Sub New(ByVal pIdCliente As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdCliente = pIdCliente

    End Sub
    Private Sub frmComprasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        DateTimePicker2.Value = Date.Now 'CDate(Format(Date.Now, "yyyy") + "/01/01")
        DateTimePicker3.Value = Date.Now
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = True
        If IdCliente <> 0 Then

        End If
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                'If IdCliente <> 0 Then
                Dim S As New dbClientes(MySqlcon)
                DGServicios.DataSource = S.ConsultaMovimientos(IdCliente, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), True)
                DGServicios.Columns(1).Visible = False
                DGServicios.Columns(0).HeaderText = "Fecha"
                DGServicios.Columns(2).HeaderText = "Movimiento"
                DGServicios.Columns(3).HeaderText = "Est"
                DGServicios.Columns(4).HeaderText = "Serie"
                DGServicios.Columns(5).HeaderText = "Folio"
                DGServicios.Columns(6).HeaderText = "Cargo"
                DGServicios.Columns(7).HeaderText = "Abono"
                DGServicios.Columns(8).HeaderText = "Saldo"
                DGServicios.Columns(9).Visible = False
                DGServicios.Columns(10).Visible = False
                DGServicios.Columns(11).Visible = False
                If IdCliente = 0 Then
                    DGServicios.Columns(12).Visible = False
                    DGServicios.Columns(13).Visible = False
                    DGServicios.Columns(14).Visible = False
                    DGServicios.Columns(15).Visible = False
                End If
                DGServicios.Columns(3).Width = 30
                DGServicios.Columns(4).Width = 50
                DGServicios.Columns(8).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                CalculaSaldos(0)
                DGServicios.ClearSelection()
                Seleccionado = 0
                'End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub CalculaSaldos(ByVal pSaldoInicial As Double)
        Dim C As Integer = 0
        Dim Cc As New dbClientes(MySqlcon)
        pSaldoInicial = Cc.DaSaldoAFecha(IdCliente, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
        While C < DGServicios.RowCount
            pSaldoInicial = pSaldoInicial + DGServicios.Item(6, C).Value - DGServicios.Item(7, C).Value
            DGServicios.Item(8, C).Value = pSaldoInicial
            C += 1
        End While

    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        Seleccionado = 1
        AbreDocumento(e.RowIndex)
    End Sub
    Private Sub AbreDocumento(ByVal Row As Integer)
        Select Case DGServicios.Item(10, Row).Value
            Case 1
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = True Then
                    Dim Fac As New frmVentasN(DGServicios.Item(11, Row).Value, 0, 0, 0)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 2
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas) = True Then
                    Dim NC As New frmNotasdeCredito(DGServicios.Item(11, Row).Value)
                    NC.ShowDialog()
                    NC.Dispose()
                End If
            Case 3
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas) = True Then
                    Dim Dev As New frmDevoluciones(DGServicios.Item(11, Row).Value, 0, 0, "", 0)
                    Dev.ShowDialog()
                    Dev.Dispose()
                End If
            Case 5
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.NotasdeCargoVer, PermisosN.Secciones.Ventas) = False Then
                    Dim NCA As New frmNotasdeCargo(DGServicios.Item(11, Row).Value)
                    NCA.ShowDialog()
                    NCA.Dispose()
                End If
            Case 8
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas) = True Then
                    Dim Fac As New frmVentasRemisiones(DGServicios.Item(11, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
        End Select
    End Sub
    Private Sub DGServicios_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        On Error Resume Next
        If e.ColumnIndex = 6 Or e.ColumnIndex = 7 Or e.ColumnIndex = 8 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If e.ColumnIndex = 5 Then
            e.Value = Format(e.Value, "00000")
        End If
        If e.ColumnIndex = 4 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If DGServicios.Item(9, e.RowIndex).Value = 4 Then
            e.CellStyle.BackColor = ColorRojo
        Else
            Select Case DGServicios.Item(10, e.RowIndex).Value
                Case 1
                    e.CellStyle.BackColor = ColorVerde
                Case 2
                    e.CellStyle.BackColor = ColorAmarillo
                Case 3
                    e.CellStyle.BackColor = ColorMorado
                Case 4
                    e.CellStyle.BackColor = ColorAzul
                Case 5
                    e.CellStyle.BackColor = ColorNaranja
                Case 6
                    e.CellStyle.BackColor = ColorDocumento
                Case 7
                    e.CellStyle.BackColor = ColorDocumento
                Case 8
                    e.CellStyle.BackColor = Colorremision
            End Select
        End If


    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = B.Cliente.Nombre
            IdCliente = B.Cliente.ID
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            'Consulta()
        End If
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(TextBox1.Text) Then
                    TextBox2.Text = c.Nombre
                    IdCliente = c.ID
                    'Consulta()
                Else
                    IdCliente = 0
                    TextBox2.Text = ""
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        'If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        'If chkTiempoReal.Checked Then Consulta()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub



    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick
        Seleccionado = 1
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Seleccionado = 1 Then
            AbreDocumento(DGServicios.CurrentRow.Index)
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        If IdCliente <> 0 Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim C As New dbClientes(MySqlcon)
            'Dim O As New dbOpciones(MySqlcon)
            Rep = New repClientesMovimientos
            Rep.SetDataSource(C.ConsultaMovimientos(IdCliente, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), False))
            Rep.SetParameterValue("Encabezado", S.Nombre)
            Rep.SetParameterValue("saldoinicial", C.DaSaldoAFecha(IdCliente, Format(DateTimePicker2.Value, "yyyy/MM/dd")))
            Rep.SetParameterValue("dcliente", TextBox1.Text + " " + TextBox2.Text)
            Rep.SetParameterValue("fechas", "Del " + Format(DateTimePicker2.Value, "dd/MM/yyyy") + " al " + Format(DateTimePicker3.Value, "dd/MM/yyyy"))
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        Else
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim C As New dbClientes(MySqlcon)
            'Dim O As New dbOpciones(MySqlcon)
            Rep = New repClietesMovimientosTodos
            Rep.SetDataSource(C.ConsultaMovimientos(IdCliente, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), False))
            Rep.SetParameterValue("Encabezado", S.Nombre)
            'Rep.SetParameterValue("saldoinicial", C.DaSaldoAFecha(IdCliente, Format(DateTimePicker2.Value, "yyyy/MM/dd")))
            'Rep.SetParameterValue("dcliente", TextBox1.Text + " " + TextBox2.Text)
            Rep.SetParameterValue("fechas", "Del " + Format(DateTimePicker2.Value, "dd/MM/yyyy") + " al " + Format(DateTimePicker3.Value, "dd/MM/yyyy"))
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub

End Class