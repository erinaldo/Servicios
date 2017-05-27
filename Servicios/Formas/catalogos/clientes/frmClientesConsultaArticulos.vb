Public Class frmClientesConsultaArticulos
    'Public IdCompra As Integer
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
    Dim Seleccionado As Byte = 0
    Dim IdInventario As Integer
    Dim IdVariante As Integer
    Public Sub New(ByVal pIdCliente As Integer, ByVal pIdInventario As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdCliente = pIdCliente
        IdInventario = pIdInventario
    End Sub
    Private Sub frmComprasConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        DateTimePicker2.Value = CDate(Format(Date.Now, "yyyy") + "/01/01")
        DateTimePicker3.Value = Date.Now
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        ConsultaOn = True
        If IdCliente <> 0 Then
            Dim Cl As New dbClientes(IdCliente, MySqlcon)
            TextBox1.Text = Cl.Clave
        End If
        If IdInventario <> 0 Then
            Dim Inv As New dbInventario(IdInventario, MySqlcon)
            TextBox3.Text = Inv.Clave
        End If

        If IdCliente <> 0 Then Consulta()
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim S As New dbClientes(MySqlcon)
                DGServicios.DataSource = S.ConsultaMovimientosXArticulo(IdCliente, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), IdInventario)
                DGServicios.Columns(0).Visible = False
                DGServicios.Columns(1).HeaderText = "Doc."
                DGServicios.Columns(2).HeaderText = "Fecha"
                DGServicios.Columns(3).HeaderText = "Folio"
                DGServicios.Columns(4).HeaderText = "Código"
                DGServicios.Columns(5).HeaderText = "Descripción"
                DGServicios.Columns(6).HeaderText = "Cant."
                DGServicios.Columns(7).HeaderText = "P/U"
                DGServicios.Columns(8).HeaderText = "Importe"
                DGServicios.Columns(1).Width = 25
                DGServicios.Columns(6).Width = 60
                DGServicios.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGServicios.ClearSelection()
                Seleccionado = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    
    Private Sub AbreDocumento(ByVal Row As Integer)
        If DGServicios.Item(1, Row).Value = "F" Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = True Then
                Dim Fac As New frmVentasN(DGServicios.Item(0, Row).Value, 0, 0, 0)
                Fac.ShowDialog()
                Fac.Dispose()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Else
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas) = True Then
                Dim Fac As New frmVentasRemisiones(DGServicios.Item(0, Row).Value)
                Fac.ShowDialog()
                Fac.Dispose()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        End If
        'Select Case DGServicios.Item(10, Row).Value
        '   Case 1
        'Dim Fac As New frmVentasN(DGServicios.Item(11, Row).Value, 0, 0, 0)
        ' Fac.ShowDialog()
        'Case 2
        '    Dim NC As New frmNotasdeCredito(DGServicios.Item(11, Row).Value)
        '    NC.ShowDialog()
        'Case 3
        '    Dim Dev As New frmDevoluciones(DGServicios.Item(11, Row).Value, 0, 0, "", 0)
        '    Dev.ShowDialog()
        'Case 5
        '    Dim NCA As New frmNotasdeCargo(DGServicios.Item(11, Row).Value)
        '    NCA.ShowDialog()
        'End Select
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        Seleccionado = 1
    End Sub
    Private Sub DGServicios_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        'On Error Resume Next
        If e.ColumnIndex = 6 Or e.ColumnIndex = 7 Or e.ColumnIndex = 8 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        'If e.ColumnIndex = 5 Then
        '    e.Value = Format(e.Value, "00000")
        'End If
        'If e.ColumnIndex = 4 Then
        '    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'End If
        'If DGServicios.Item(9, e.RowIndex).Value = 4 Then
        '    e.CellStyle.BackColor = ColorRojo
        'Else
        '    Select Case DGServicios.Item(10, e.RowIndex).Value
        '        Case 1
        '            e.CellStyle.BackColor = ColorVerde
        '        Case 2
        '            e.CellStyle.BackColor = ColorAmarillo
        '        Case 3
        '            e.CellStyle.BackColor = ColorMorado
        '        Case 4
        '            e.CellStyle.BackColor = ColorAzul
        '        Case 5
        '            e.CellStyle.BackColor = ColorNaranja
        '        Case 6
        '            e.CellStyle.BackColor = ColorDocumento
        '        Case 7
        '            e.CellStyle.BackColor = ColorDocumento
        '    End Select
        'End If


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


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
        Dim B As New frmBuscador(TipodeBusqueda, 0, True, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Select Case B.Tipo
                Case "I"
                    IdInventario = B.Inventario.ID
                    IdVariante = 0
                    TextBox4.Text = B.Inventario.Nombre
                    TextBox3.Text = B.Inventario.Clave
                    ConsultaOn = True
                    TextBox4.Text = B.Inventario.Nombre
            End Select

        End If

    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0, False, True, False, False) Then
                    IdInventario = p.ID
                    TextBox4.Text = p.Nombre

                Else
                    TextBox4.Text = ""
                    IdInventario = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        AbreDocumento(e.RowIndex)
    End Sub
End Class