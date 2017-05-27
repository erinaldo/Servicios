Public Class frmFertilizantesReportes
    Dim IdsSucursales As New elemento
    Dim IdsAlmacenes As New elemento
    Dim IdInventario As Integer
    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean
    Private Sub frmFertilizantesReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button1.Enabled = True
            Exit Sub
        End If
        ListBox1.Items.Add("Pedidos.")
        ListBox1.Items.Add("Pedidos con peso.")
        ListBox1.Items.Add("Estado almacenes.")
        ListBox1.Items.Add("Movimientos.")
        ListBox1.Items.Add("Movimientos Sencillo.")
        ListBox1.Items.Add("Movimientos con Devolución.")
        ListBox1.Items.Add("Devoluciones.")
        ListBox1.Items.Add("Estado de equipos.")
        ListBox1.Items.Add("Inventario a favor.")
        ComboBox3.Items.Add("Todos")
        ComboBox3.Items.Add("En tránsito")
        ComboBox3.Items.Add("Surtido")
        ComboBox2.Items.Add("Todos")
        ComboBox2.Items.Add("Salida")
        ComboBox2.Items.Add("Envío")
        ComboBox2.Items.Add("Traspaso")
        ComboBox4.Items.Add("Todos")
        ComboBox4.Items.Add("Abierto")
        ComboBox4.Items.Add("Cerrado")
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        ComboBox1.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        ComboBox3.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        ConsultaOn = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = B.Cliente.Nombre
            IdCliente = B.Cliente.ID
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
        End If
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(TextBox1.Text) Then
                    TextBox2.Text = c.Nombre
                    idCliente = c.ID
                Else
                    IdCliente = 0
                    TextBox2.Text = ""
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        Dim B As New frmBuscador(TipodeBusqueda, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Select Case B.Tipo
                Case "I"
                    IdInventario = B.Inventario.ID
                    ConsultaOn = False
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
                If p.BuscaArticulo(TextBox3.Text, 0) Then
                    IdInventario = p.ID
                    TextBox4.Text = p.Nombre
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim FP As New dbFertilizantesPedido(MySqlcon)
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

        Select Case ListBox1.Text
            Case "Pedidos."

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repFertilizantesPedidos
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(FP.Reporte(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, CheckBox6.Checked, 0, ComboBox4.SelectedIndex))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros += " Sucursal: " + ComboBox1.Text
                'End If
                Filtros += " Estado Pedido: " + ComboBox4.Text
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Pedidos con peso."

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repFertilizantesPedidosConPeso
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(FP.Reporte(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, CheckBox6.Checked, 0, ComboBox4.SelectedIndex))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros += " Sucursal: " + ComboBox1.Text
                'End If
                Filtros += " Estado Pedido: " + ComboBox4.Text
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Estado almacenes."
                Dim Al As New dbAlmacenes(MySqlcon)

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repAlmacenesEstado
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(Al.ReporteEstadoAlmacenes(IdsSucursales.Valor(ComboBox1.SelectedIndex), ComboBox3.SelectedIndex))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                'Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros = " Sucursal: " + ComboBox1.Text
                'End If
                'Filtros += " Estado Pedido: " + ComboBox4.Text
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Movimientos."
                Dim FM As New dbFertilizantesMovimientos(MySqlcon)
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repFertilizantesMovimientos
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(FM.ReporteMovimientos(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), ComboBox2.SelectedIndex, IdCliente, ComboBox3.SelectedIndex, IdInventario, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), False, False))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros += " Sucursal: " + ComboBox1.Text
                If ComboBox8.SelectedIndex > 0 Then
                    Filtros += " Almacen: " + ComboBox8.Text
                End If
                Filtros += " Concepto: " + ComboBox2.Text
                Filtros += " Estado: " + ComboBox3.Text

                'End If
                Filtros += " Estado Pedido: " + ComboBox4.Text
                If IdCliente <> 0 Then
                    Filtros += " Cliente: " + TextBox2.Text
                End If
                If IdInventario <> 0 Then
                    Filtros += " Artículo: " + TextBox4.Text
                End If
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Movimientos Sencillo."
                Dim FM As New dbFertilizantesMovimientos(MySqlcon)
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repFertilizantesMovimientosB2
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(FM.ReporteMovimientos(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), ComboBox2.SelectedIndex, IdCliente, ComboBox3.SelectedIndex, IdInventario, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), False, False))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros += " Sucursal: " + ComboBox1.Text
                If ComboBox8.SelectedIndex > 0 Then
                    Filtros += " Almacen: " + ComboBox8.Text
                End If
                Filtros += " Concepto: " + ComboBox2.Text
                Filtros += " Estado: " + ComboBox3.Text

                'End If
                Filtros += " Estado Pedido: " + ComboBox4.Text
                If IdCliente <> 0 Then
                    Filtros += " Cliente: " + TextBox2.Text
                End If
                If IdInventario <> 0 Then
                    Filtros += " Artículo: " + TextBox4.Text
                End If
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Movimientos con Devolución."
                Dim FM As New dbFertilizantesMovimientos(MySqlcon)
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repFertilizantesMovimientosResB
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(FM.ReporteMovimientos(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), ComboBox2.SelectedIndex, IdCliente, ComboBox3.SelectedIndex, IdInventario, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), True, False))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros += " Sucursal: " + ComboBox1.Text
                If ComboBox8.SelectedIndex > 0 Then
                    Filtros += " Almacen: " + ComboBox8.Text
                End If
                Filtros += " Concepto: " + ComboBox2.Text
                Filtros += " Estado: " + ComboBox3.Text

                'End If
                Filtros += " Estado Pedido: " + ComboBox4.Text
                If IdCliente <> 0 Then
                    Filtros += " Cliente: " + TextBox2.Text
                End If
                If IdInventario <> 0 Then
                    Filtros += " Artículo: " + TextBox4.Text
                End If
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Devoluciones."
                Dim FM As New dbFertilizantesMovimientos(MySqlcon)
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repFertilizantesMovimientosDev
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(FM.ReporteMovimientos(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), 0, IdCliente, ComboBox3.SelectedIndex, IdInventario, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), False, True))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros += " Sucursal: " + ComboBox1.Text
                If ComboBox8.SelectedIndex > 0 Then
                    Filtros += " Almacen: " + ComboBox8.Text
                End If
                'Filtros += " Concepto: " + ComboBox2.Text
                Filtros += " Estado: " + ComboBox3.Text

                'End If
                Filtros += " Estado Pedido: " + ComboBox4.Text
                If IdCliente <> 0 Then
                    Filtros += " Cliente: " + TextBox2.Text
                End If
                If IdInventario <> 0 Then
                    Filtros += " Artículo: " + TextBox4.Text
                End If
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Estado de equipos."
                Dim Al As New dbClientesEquipos(MySqlcon)

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repFertilizantesEstadoEquipos
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(Al.ConsultaSucReporte(IdsSucursales.Valor(ComboBox1.SelectedIndex), ""))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                Dim Filtros As String
                'Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                Filtros = " Sucursal: " + ComboBox1.Text
                'End If
                'Filtros += " Estado Pedido: " + ComboBox4.Text
                Rep.SetParameterValue("Filtros", Filtros)
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
            Case "Inventario a favor."
                'Dim Al As New dbClientesEquipos(MySqlcon)
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repClientesInventario
                'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                Rep.SetDataSource(FP.ReporteClientesInventario(IdCliente))
                Rep.SetParameterValue("Encabezado", S.Nombre)
                'Dim Filtros As String
                'Filtros = "Del " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                'If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then
                'Filtros = " Sucursal: " + ComboBox1.Text
                'End If
                'Filtros += " Estado Pedido: " + ComboBox4.Text
                Rep.SetParameterValue("Filtros", "")
                Dim RV As New frmReportes(Rep, False)
                RV.Show()
        End Select
    End Sub
End Class