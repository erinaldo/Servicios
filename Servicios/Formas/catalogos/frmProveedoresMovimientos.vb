Public Class frmProveedoresMovimientos
    Public IdCompra As Integer
    Dim IdProveedor As Integer
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
    Public Sub New(ByVal pIdCliente As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdProveedor = pIdCliente

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
        If IdProveedor <> 0 Then

        End If
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                If IdProveedor <> 0 Then
                    Dim S As New dbproveedores(MySqlcon)
                    DGServicios.DataSource = S.ConsultaMovimientos(IdProveedor, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), True)
                    DGServicios.Columns(1).Visible = False
                    DGServicios.Columns(0).HeaderText = "Fecha"
                    DGServicios.Columns(2).HeaderText = "Movimiento"
                    DGServicios.Columns(3).HeaderText = "Est"
                    DGServicios.Columns(4).HeaderText = "Folio"
                    DGServicios.Columns(5).HeaderText = "Cargo"
                    DGServicios.Columns(6).HeaderText = "Abono"
                    DGServicios.Columns(7).HeaderText = "Saldo"
                    DGServicios.Columns(8).Visible = False
                    DGServicios.Columns(9).Visible = False
                    DGServicios.Columns(10).Visible = False
                    DGServicios.Columns(3).Width = 30
                    DGServicios.Columns(7).DefaultCellStyle.Format = "N2"
                    DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    CalculaSaldos(0)
                    DGServicios.ClearSelection()
                    Seleccionado = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub CalculaSaldos(ByVal pSaldoInicial As Double)
        Dim C As Integer = 0
        Dim Cc As New dbproveedores(MySqlcon)
        pSaldoInicial = Cc.DaSaldoAFecha(IdProveedor, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
        While C < DGServicios.RowCount
            pSaldoInicial = pSaldoInicial + DGServicios.Item(5, C).Value - DGServicios.Item(6, C).Value
            DGServicios.Item(7, C).Value = pSaldoInicial
            C += 1
        End While

    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        Seleccionado = 1
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        Seleccionado = 1
        AbreDocumento(e.RowIndex)
    End Sub
    Private Sub AbreDocumento(ByVal Row As Integer)
        Select Case DGServicios.Item(9, Row).Value
            Case 1
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras) = True Then
                    Dim Fac As New frmCompras(DGServicios.Item(10, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 2
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoVer, PermisosN.Secciones.Compras) = True Then
                    Dim NC As New frmNotasdeCreditoCompras(DGServicios.Item(10, Row).Value)
                    NC.ShowDialog()
                    NC.Dispose()
                End If
            Case 3
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras) = True Then
                    Dim Dev As New frmDevolucionesCompras(DGServicios.Item(10, Row).Value, 0, 0, "", 0)
                    Dev.ShowDialog()
                    Dev.Dispose()
                End If
            Case 5
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCargoVer, PermisosN.Secciones.Compras) = True Then
                    Dim NCA As New frmNotasdeCargoC(DGServicios.Item(10, Row).Value)
                    NCA.ShowDialog()
                    NCA.Dispose()
                End If
        End Select
    End Sub
    Private Sub DGServicios_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting

        If e.ColumnIndex = 5 Or e.ColumnIndex = 6 Or e.ColumnIndex = 7 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If DGServicios.Item(8, e.RowIndex).Value = 4 Then
            e.CellStyle.BackColor = ColorRojo
        Else
            Select Case DGServicios.Item(9, e.RowIndex).Value
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
                    e.CellStyle.BackColor = ColorNaranja
                Case 7
                    e.CellStyle.BackColor = ColorNaranja
            End Select
        End If


    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = B.Proveedor.Nombre
            IdProveedor = B.Proveedor.ID
            ConsultaOn = False
            TextBox1.Text = B.Proveedor.Clave
            ConsultaOn = True
            Consulta()
        End If
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbproveedores(MySqlcon)
                If c.BuscaProveedor(TextBox1.Text) Then
                    TextBox2.Text = c.Nombre
                    IdProveedor = c.ID
                    Consulta()
                Else
                    IdProveedor = 0
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
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub



    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Seleccionado = 1 Then
            AbreDocumento(DGServicios.CurrentRow.Index)
        Else
            MsgBox("Debe seleccionar un registro.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If IdProveedor <> 0 Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim C As New dbproveedores(MySqlcon)
            Dim O As New dbOpciones(MySqlcon)
            Rep = New repProveedoresMovimientos
            Rep.SetDataSource(C.ConsultaMovimientos(IdProveedor, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), False))
            Rep.SetParameterValue("Encabezado", O._NombreEmpresa)
            Rep.SetParameterValue("saldoinicial", C.DaSaldoAFecha(IdProveedor, Format(DateTimePicker2.Value, "yyyy/MM/dd")))
            Rep.SetParameterValue("proveedor", TextBox1.Text + " " + TextBox2.Text)
            Rep.SetParameterValue("fechas", "Del " + Format(DateTimePicker2.Value, "dd/MM/yyyy") + " al " + Format(DateTimePicker3.Value, "dd/MM/yyyy"))
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Consulta()
    End Sub
End Class