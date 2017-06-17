Public Class frmInventarioKardex
   
    Public IdCompra As Integer
    Dim IdInventario As Integer
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
    Dim IdsSucursales As New elemento
    Dim IDsAlmacenes As New elemento
    Dim TablaLista As Boolean = False
    Public Sub New(ByVal pIdCliente As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdInventario = pIdCliente

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
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        ConsultaOn = True

    End Sub
    Private Sub CreaTabla()
        Try
            Dim S As New dbInventario(MySqlcon)
            S.ConsultaKardex(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IDsAlmacenes.Valor(ComboBox8.SelectedIndex))
            TablaLista = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                If IdInventario <> 0 Then
                    Dim S As New dbInventario(MySqlcon)
                    DGServicios.DataSource = S.ConsultaKardex2(IdInventario)
                    'DGServicios.Columns(1).Visible = False
                    DGServicios.Columns(0).HeaderText = "Fecha"
                    DGServicios.Columns(1).HeaderText = "Hora"
                    DGServicios.Columns(2).HeaderText = "Movimiento"
                    DGServicios.Columns(3).HeaderText = "TipoMovimiento"
                    DGServicios.Columns(4).HeaderText = "IdDocumento"
                    DGServicios.Columns(5).HeaderText = "E."
                    DGServicios.Columns(6).HeaderText = "Folio"
                    DGServicios.Columns(7).HeaderText = "Ref."
                    DGServicios.Columns(8).HeaderText = "Entrada"
                    DGServicios.Columns(9).HeaderText = "Salida"
                    DGServicios.Columns(10).HeaderText = "Existencia"
                    DGServicios.Columns(11).HeaderText = "Costou"
                    DGServicios.Columns(12).HeaderText = "Costo"
                    DGServicios.Columns(13).HeaderText = "Costo Prom."
                    DGServicios.Columns(14).HeaderText = "Almacen O."
                    DGServicios.Columns(15).HeaderText = "Almacen D."
                    DGServicios.Columns(3).Visible = False
                    DGServicios.Columns(4).Visible = False
                    DGServicios.Columns(1).Visible = False
                    DGServicios.Columns(11).Visible = False
                    'DGServicios.Columns(13).Visible = False
                    DGServicios.Columns(15).Visible = False
                    'DGServicios.Columns(14).Visible = False
                    DGServicios.Columns(16).Visible = False
                    DGServicios.Columns(17).Visible = False
                    DGServicios.Columns(0).Width = 80
                    DGServicios.Columns(5).Width = 20
                    DGServicios.Columns(8).Width = 80
                    DGServicios.Columns(9).Width = 80
                    DGServicios.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
        Dim Cc As New dbInventario(MySqlcon)
        pSaldoInicial = Cc.DaInventarioAFecha(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IDsAlmacenes.Valor(ComboBox8.SelectedIndex))
        'Dim TotalCompras As Double
        'TotalCompras = Cc.DaCostoAFechaC(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
        Dim Costo As Double
        Dim InventarioAnt As Double
        Dim TotalComprasprecio As Double
        TotalComprasprecio = Cc.DaCostoAFecha(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
        Costo = TotalComprasprecio
        While C < DGServicios.RowCount
            InventarioAnt = pSaldoInicial
            pSaldoInicial = pSaldoInicial + DGServicios.Item(8, C).Value - DGServicios.Item(9, C).Value
            DGServicios.Item(10, C).Value = pSaldoInicial
            If DGServicios.Item(3, C).Value = 5 Or DGServicios.Item(3, C).Value = 0 Or DGServicios.Item(3, C).Value = 1 Or DGServicios.Item(3, C).Value = 7 Then

                If DGServicios.Item(5, C).Value = "A" Then

                    'If DGServicios.Item(7, C).Value = "6" Or DGServicios.Item(7, C).Value = "0" Or DGServicios.Item(7, C).Value = "1" Then
                    'TotalCompras = TotalCompras
                    'End If
                    If (InventarioAnt + DGServicios.Item(8, C).Value) > 0 Then
                        Costo = ((Costo * InventarioAnt) + (DGServicios.Item(11, C).Value * DGServicios.Item(8, C).Value)) / (InventarioAnt + DGServicios.Item(8, C).Value)
                    Else
                        Costo = 0
                    End If
                    DGServicios.Item(13, C).Value = Costo
                    'TotalCompras += DGServicios.Item(8, C).Value
                    'TotalComprasprecio += DGServicios.Item(11, C).Value * DGServicios.Item(8, C).Value
                    'If TotalCompras <> 0 Then
                    '    DGServicios.Item(13, C).Value = TotalComprasprecio / TotalCompras
                    'Else
                    '    DGServicios.Item(13, C).Value = 0
                    'End If
                Else
                    If (InventarioAnt - DGServicios.Item(9, C).Value) > 0 Then
                        Costo = ((Costo * InventarioAnt) - (DGServicios.Item(11, C).Value * DGServicios.Item(9, C).Value)) / (InventarioAnt - DGServicios.Item(9, C).Value)
                    Else
                        Costo = 0
                    End If
                    DGServicios.Item(13, C).Value = Costo
                    'TotalCompras -= DGServicios.Item(9, C).Value
                    'TotalComprasprecio -= DGServicios.Item(11, C).Value * DGServicios.Item(9, C).Value
                    'If TotalCompras <> 0 Then
                    '    DGServicios.Item(13, C).Value = TotalComprasprecio / TotalCompras
                    'Else
                    '    DGServicios.Item(13, C).Value = 0
                    'End If
                End If
            Else
                DGServicios.Item(13, C).Value = Costo
            End If
            DGServicios.Item(12, C).Value = pSaldoInicial * DGServicios.Item(13, C).Value
            'If CostoPromedio
            C += 1
        End While
        C = 0
        If CheckBox1.Checked Then
            Dim cm As CurrencyManager
            cm = CType(Me.BindingContext(DGServicios.DataSource, DGServicios.DataMember), CurrencyManager)
            cm.SuspendBinding()
            While C < DGServicios.RowCount
                If DGServicios.Item(10, C).Value >= 0 And DGServicios.Item(2, C).Value <> "Compra" And DGServicios.Item(2, C).Value <> "Entrada" Then
                    DGServicios.Rows(C).Visible = False
                End If
                C += 1
            End While
        End If
    End Sub
    Private Sub ActualizaTabla()

        MySqlcom.Transaction = MySqlcom.Connection.BeginTransaction()
        Try
            Dim r As DataGridViewRow
            For Each r In DGServicios.Rows
                'MySqlcom.CommandText = "select count(idinventario) from tblinventarioconciliaciones where fecha='" + Format(DateTimePicker2.Value, "yyyy/MM/dd") + "' and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString + " and idalmacen=" + IdAlmacenes.Valor(ComboBox8.SelectedIndex).ToString
                'If MySqlcom.ExecuteScalar > 0 Then
                MySqlcom.CommandText = "update tblinventariomovimientosk2 set existencia=" + r.Cells(10).Value.ToString + ",costou=" + r.Cells(13).Value.ToString + ",costoe=" + r.Cells(12).Value.ToString + " where id=" + r.Cells(16).Value.ToString + ";"
                'Else
                'MySqlcom.CommandText = "insert into tblinventarioconciliaciones values ('" + Format(DateTimePicker2.Value, "yyyy/MM/dd") + "'," + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString + "," + IdAlmacenes.Valor(ComboBox8.SelectedIndex).ToString + "," + r.Cells(3).Value.ToString + ",'" + r.Cells(4).Value.ToString + "','" + r.Cells(5).Value.ToString + "'," + r.Cells(6).Value.ToString + "," + r.Cells(7).Value.ToString + "," + r.Cells(8).Value.ToString + "," + r.Cells(9).Value.ToString + "," + r.Cells(10).Value.ToString + ");"
                'End If
                MySqlcom.ExecuteNonQuery()
            Next
            MySqlcom.Transaction.Commit()
            'PopUp("Conciliación Guardada.", 90)
        Catch ex As Exception
            MySqlcom.Transaction.Rollback()
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGServicios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGServicios.CellClick
        Seleccionado = 1
    End Sub
    Private Sub DGServicios_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        Seleccionado = 1
        AbreDocumento(e.RowIndex)
    End Sub
    Private Sub AbreDocumento(ByVal Row As Integer)
        Select Case DGServicios.Item(3, Row).Value
            Case 8
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = True Then
                    Dim Fac As New frmVentasN(DGServicios.Item(4, Row).Value, 0, 0, 0)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 10
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas) = True Then
                    Dim Fac As New frmVentasRemisiones(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 5
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras) = True Then
                    Dim Fac As New frmCompras(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 7
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesVer, PermisosN.Secciones.Compras) = True Then
                    Dim Fac As New frmComprasRemisiones(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 1
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim Fac As New frmInventarioMovimientosN(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 2
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim Fac As New frmInventarioMovimientosN(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 0
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim Fac As New frmInventarioMovimientosN(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 3
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim Fac As New frmInventarioMovimientosN(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 4
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim Fac As New frmInventarioMovimientosN(DGServicios.Item(4, Row).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 13
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim FP As New dbFertilizantesPedido(MySqlcon)
                    Dim Fac As New frmFertilizantesPedido(FP.DaId(DGServicios.Item(7, Row).Value))
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 14
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim FP As New dbFertilizantesPedido(MySqlcon)
                    Dim Fac As New frmFertilizantesPedido(FP.DaId(DGServicios.Item(7, Row).Value))
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case 15
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario) = True Then
                    Dim FP As New dbFertilizantesPedido(MySqlcon)
                    Dim Fac As New frmFertilizantesPedido(FP.DaId(DGServicios.Item(7, Row).Value))
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
                'Case 9

                '    Dim Fac As New frmDevoluciones(DGServicios.Item(4, Row).Value, )
                '    Fac.ShowDialog()
                '    Case 2
                '        Dim NC As New frmNotasdeCredito(DGServicios.Item(11, Row).Value)
                '        NC.ShowDialog()
                '    Case 3
                '        Dim Dev As New frmDevoluciones(DGServicios.Item(11, Row).Value, 0, 0, "", 0)
                '        Dev.ShowDialog()
                '    Case 5
                '        Dim NCA As New frmNotasdeCargo(DGServicios.Item(11, Row).Value)
                '        NCA.ShowDialog()
                '
        End Select
    End Sub
    Private Sub DGServicios_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting

        If e.ColumnIndex = 12 Then
            e.Value = Format(e.Value, "#0.00")
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If e.ColumnIndex = 11 Or e.ColumnIndex = 10 Or e.ColumnIndex = 13 Then
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
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.ArticuloInv, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = B.Inventario.Nombre
            IdInventario = B.Inventario.ID
            ConsultaOn = False
            TextBox1.Text = B.Inventario.Clave
            ConsultaOn = True
            'Consulta()
        End If
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox1.Text, 1, "") Then
                    IdInventario = p.ID
                    TextBox2.Text = p.Nombre
                Else
                    IdInventario = 0
                    DGServicios.DataSource = Nothing
                    TablaLista = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaArticulo()
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
        If IdInventario <> 0 And TablaLista Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            ActualizaTabla()
            'PopUp("OK", 90)
            Dim C As New dbInventario(MySqlcon)
            'Dim TotalCompras As Double
            'TotalCompras = C.DaCostoAFechaC(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"))
            'Dim TotalComprasprecio As Double
            'TotalComprasprecio = C.DaCostoAFechaP(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"))

            'Dim Cc As New dbInventario(MySqlcon)
            'TotalCompras = Cc.DaInventarioAFecha(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IDsAlmacenes.Valor(ComboBox8.SelectedIndex))
            Dim TotalComprasprecio As Double
            TotalComprasprecio = C.DaCostoAFecha(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"))


            Dim s As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Rep = New repInventarioKardexN
            Rep.SetDataSource(C.ConsultaKardex2(IdInventario))
            Rep.SetParameterValue("Encabezado", s.Nombre)
            Rep.SetParameterValue("Articulo", TextBox1.Text + " " + TextBox2.Text)
            Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker2.Value, "dd/MM/yyyy") + " al " + Format(DateTimePicker3.Value, "dd/MM/yyyy"))
            Rep.SetParameterValue("InventarioInicial", C.DaInventarioAFecha(IdInventario, Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IDsAlmacenes.Valor(ComboBox8.SelectedIndex)))
            Rep.SetParameterValue("CostoInicial", TotalComprasprecio)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        Else
            MsgBox("No hay información para imprimir el reporte.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        CreaTabla()
        Consulta()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IDsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
    End Sub

   
    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged

    End Sub
End Class