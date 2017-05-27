Public Class frmEmpeniosReportes
    Dim idCliente As Integer = 0
    Dim fechaInicial As String = ""
    Dim fechaFinal As String = ""
    Dim idClienteAUX As Integer
    Dim IdsSucursales As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsClasificacion As New elemento
    Dim sa As New dbSucursalesArchivos
    Dim IdsCajas As New elemento
    Dim idCaja As Integer = -1
    Dim idSucursal As Integer = -1
    Dim idClasificacion As Integer = -1
    Dim idVendedor As Integer = -1
    Dim Co As New dbEmpeniosReportes(MySqlcon)
    Dim tablaDetalles As New DataTable
    Dim DS As DataSet = New DataSet
    Dim status() As String = {"Todos", "Empeñado", "Entregado", "Adjudicado"}



    Private Sub frmEmpeniosReportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblsucursales", cmbSucursal, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        LlenaCombos("tblvendedores", cmbVendedor, "nombre", "nombret", "idvendedor", IdsVendedores, , "Todos")
        LlenaCombos("tblempeniosclasificacion", cmbClasificacion, "nombre", "nombret", "idClasificacion", IdsClasificacion, , "Todas")
        '  LlenaCombos("tblformasdepagoremisiones", ComboBox4, "nombre", "nombrec", "idforma", IdsForma, "tipo=1 or tipo=2", , "idforma")
        cmbClasificacion.Enabled = True


        dtpFechaHasta.MinDate = dtpFecha.Value
        tablaDetalles.Columns.Add("Cliente")
        tablaDetalles.Columns.Add("Tipo")
        tablaDetalles.Columns.Add("Fecha")
        tablaDetalles.Columns.Add("Serie")
        tablaDetalles.Columns.Add("Clasificacion")
        tablaDetalles.Columns.Add("Descripcion")
        tablaDetalles.Columns.Add("Evaluo")
        tablaDetalles.Columns.Add("Prestamo")

        For Each s As String In status
            comboStatus.Items.Add(s)
        Next

    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        BuscaClienteBoton()
    End Sub
    Private Sub BuscaClienteBoton()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtNombeCliente.Text = B.Cliente.Nombre
            txtcliente.Text = B.Cliente.Clave
            idCliente = B.Cliente.ID
            chkCliente.Checked = True
        End If
    End Sub

    Private Sub txtcliente_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcliente.TextChanged
        BuscaClienteBoton2()
    End Sub
    Private Sub BuscaClienteBoton2()
        Try
            Dim c As New dbClientes(MySqlcon)

            If c.BuscaCliente(txtcliente.Text) Then
       
                txtNombeCliente.Text = c.Nombre
                txtcliente.Text = c.Clave
                idCliente = c.ID
                chkCliente.Checked = True
            Else
                idCliente = 0
                txtNombeCliente.Text = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub lstTipos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTipos.SelectedIndexChanged
        If lstTipos.SelectedIndex = 1 Then
            'empeños detalles
            ' cmbEmpenios.Enabled = True
            btnImprimir.Enabled = True
            cmbVendedor.Enabled = True
            cmbClasificacion.Enabled = True
            cmbSucursal.Enabled = True
            cmbCaja.Enabled = True
            ' dtpReporteMes.Enabled = False
            txtcliente.Enabled = True
            btnBuscarCliente.Enabled = True
        End If
        If lstTipos.SelectedIndex = 0 Then
            'empeños
            ' cmbEmpenios.Enabled = False
            btnImprimir.Enabled = True
            cmbVendedor.Enabled = True
            cmbSucursal.Enabled = True
            cmbCaja.Enabled = True
            cmbClasificacion.Enabled = False
            '  dtpReporteMes.Enabled = False
            txtcliente.Enabled = True
            btnBuscarCliente.Enabled = True
        End If
        'If lstTipos.SelectedIndex = 2 Then
        '    'empeños detalles por clasif
        '    ' cmbEmpenios.Enabled = True
        '    btnImprimir.Enabled = True
        '    cmbVendedor.Enabled = True
        '    cmbSucursal.Enabled = True
        '    cmbCaja.Enabled = True
        '    cmbClasificacion.Enabled = True
        '    ' dtpReporteMes.Enabled = False
        '    txtcliente.Enabled = True
        '    btnBuscarCliente.Enabled = True
        'End If
        If lstTipos.SelectedIndex = 2 Then
            'PAGOS
            btnImprimir.Enabled = True
            cmbVendedor.Enabled = True
            cmbSucursal.Enabled = True
            cmbCaja.Enabled = True
            cmbClasificacion.Enabled = False
            ' dtpReporteMes.Enabled = False
            txtcliente.Enabled = True
            btnBuscarCliente.Enabled = True
        End If
        If lstTipos.SelectedIndex = 3 Then
            'PAGOS
            btnImprimir.Enabled = True
            cmbVendedor.Enabled = True
            cmbSucursal.Enabled = True
            cmbCaja.Enabled = True
            cmbClasificacion.Enabled = False
            ' dtpReporteMes.Enabled = False
            txtcliente.Enabled = True
            btnBuscarCliente.Enabled = True
        End If
        If lstTipos.SelectedIndex = 4 Then
            'reporte mensual
            ' dtpReporteMes.Enabled = True
            btnImprimir.Enabled = True
            cmbVendedor.Enabled = False
            cmbSucursal.Enabled = True
            cmbCaja.Enabled = False
            cmbClasificacion.Enabled = False
            txtcliente.Enabled = False
            btnBuscarCliente.Enabled = False

        End If
        If lstTipos.SelectedIndex = 6 Then
            'Compras detalles
            ' cmbEmpenios.Enabled = True
            btnImprimir.Enabled = True
            cmbVendedor.Enabled = True
            cmbClasificacion.Enabled = True
            cmbSucursal.Enabled = True
            cmbCaja.Enabled = True
            ' dtpReporteMes.Enabled = False
            txtcliente.Enabled = True
            btnBuscarCliente.Enabled = True
        End If
        If lstTipos.SelectedIndex = 5 Then
            'Compras
            ' cmbEmpenios.Enabled = False
            btnImprimir.Enabled = True
            cmbVendedor.Enabled = True
            cmbSucursal.Enabled = True
            cmbCaja.Enabled = True
            cmbClasificacion.Enabled = False
            '  dtpReporteMes.Enabled = False
            txtcliente.Enabled = True
            btnBuscarCliente.Enabled = True
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        Dim EC As New dbEmpeniosConfiguracion(MySqlcon)
        EC.LlenaDatos()
        Dim todo As String
        Dim sRenglon As String = ""
        If chkFecha.Checked = True Then
            fechaInicial = dtpFecha.Value.ToString("yyyy/MM/dd")
            fechaFinal = dtpFechaHasta.Value.ToString("yyyy/MM/dd")
        Else
            fechaFinal = ""
            fechaInicial = ""
        End If
        'If chkCliente.Checked = True Then

        idClienteAUX = idCliente
        'Else
        'idClienteAUX = 0
        'End If


        If lstTipos.SelectedIndex = 0 Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repEmpenios
            Rep.SetDataSource(Co.filtroTodos(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, True))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Empeños")
            'If fechaFinal <> "" Then
            ' Rep.SetParameterValue("fechas", "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
            todo = "PRESTAMOS PENDIENTES"
            'Else
            ' Rep.SetParameterValue("fechas", " ")
            '   todo = ""
            'End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text

            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", "")

            If idCliente <> 0 Then
                Rep.SetParameterValue("cliente", " ")
                todo += " CLIENTE: " + txtNombeCliente.Text
            Else
                Rep.SetParameterValue("cliente", " ")
            End If
            Rep.SetParameterValue("fechas", todo)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If

        If lstTipos.SelectedIndex = 1 Then
            'Empeños detalles



            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            '  If chkCliente.Checked = True Then

            Rep = New repEmpeniosDetalles
            'Else
            '   Rep = New repEmpeniosDetallesSinCliente
            '
            '  End If


            Rep.SetDataSource(EmpeniosDetalles(True))
            ' Rep.Subreports("repVehiculos").SetDataSource(Co.filtroEmpeniosDetallesV(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion))
            ' Rep.Subreports("repTerrenos").SetDataSource(Co.filtroEmpeniosDetallesT(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion))

            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Empeños (Detalles)")
            'If fechaFinal <> "" Then
            'Rep.SetParameterValue("fechas", "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
            todo = "PRESTAMOS PENDIENTES"

            'Else
            'Rep.SetParameterValue("fechas", " ")
            '   todo = ""
            'End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", sRenglon)

            If idCliente <> 0 Then
                'Rep.SetParameterValue("cliente", "CLIENTE: " + txtNombeCliente.Text)
                todo += " CLIENTE: " + txtNombeCliente.Text
            Else
                'Rep.SetParameterValue("cliente", " ")
            End If
            If cmbClasificacion.SelectedIndex <> 0 Then
                todo += " CLASIFICACIÓN: " + cmbClasificacion.Text
            End If
            Rep.SetParameterValue("fechas", todo)

            Dim RV As New frmReportes(Rep, False)
            RV.Show()

        End If


        If lstTipos.SelectedIndex = 2 Then
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repEmpenios
            Rep.SetDataSource(Co.filtroTodos(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, False))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Empeños")
            If fechaFinal <> "" Then
                ' Rep.SetParameterValue("fechas", "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
                todo = "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")
            Else
                ' Rep.SetParameterValue("fechas", " ")
                todo = ""
            End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text

            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", "")

            If idCliente <> 0 Then
                Rep.SetParameterValue("cliente", " ")
                todo += " CLIENTE: " + txtNombeCliente.Text
            Else
                Rep.SetParameterValue("cliente", " ")
            End If
            Rep.SetParameterValue("fechas", todo)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If

        If lstTipos.SelectedIndex = 3 Then
            'Empeños detalles



            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            '  If chkCliente.Checked = True Then

            Rep = New repEmpeniosDetalles
            'Else
            '   Rep = New repEmpeniosDetallesSinCliente
            '
            '  End If


            Rep.SetDataSource(EmpeniosDetalles(False))
            ' Rep.Subreports("repVehiculos").SetDataSource(Co.filtroEmpeniosDetallesV(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion))
            ' Rep.Subreports("repTerrenos").SetDataSource(Co.filtroEmpeniosDetallesT(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion))

            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Empeños (Detalles)")
            If fechaFinal <> "" Then
                'Rep.SetParameterValue("fechas", "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
                todo = "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")

            Else
                'Rep.SetParameterValue("fechas", " ")
                todo = ""
            End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", sRenglon)

            If idCliente <> 0 Then
                'Rep.SetParameterValue("cliente", "CLIENTE: " + txtNombeCliente.Text)
                todo += " CLIENTE: " + txtNombeCliente.Text
            Else
                'Rep.SetParameterValue("cliente", " ")
            End If
            If cmbClasificacion.SelectedIndex <> 0 Then
                todo += " CLASIFICACIÓN: " + cmbClasificacion.Text
            End If
            Rep.SetParameterValue("fechas", todo)

            Dim RV As New frmReportes(Rep, False)
            RV.Show()

        End If

        If lstTipos.SelectedIndex = 4 Then
            'PAGOS

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Rep = New repEmpeniosPagos
            Rep.SetDataSource(Co.filtroPagos(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, EC.Vis))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Pagos")
            If fechaFinal <> "" Then
                '  Rep.SetParameterValue("fechas", "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
                todo = "PAGOS DEL " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")

            Else
                'Rep.SetParameterValue("fechas", " ")
                todo = ""
            End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", "")

            If chkCliente.Checked = True Then
                Rep.SetParameterValue("cliente", " ")
                todo += "CLIENTE: " + txtNombeCliente.Text
            Else
                Rep.SetParameterValue("cliente", " ")
            End If
            Rep.SetParameterValue("fechas", todo)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()

        End If

        If lstTipos.SelectedIndex = 9 Then
            'PAGOS
            'Co.ReporteEmpeniosIntereses(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, EC.Vis)
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Rep = New repEmpeniosIntereses
            Rep.SetDataSource(Co.ReporteEmpeniosIntereses(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, EC.Vis))
            Rep.SetParameterValue("empresa", S.Nombre)
            'Rep.SetParameterValue("telefono", "Reporte de Pagos")
            'If fechaFinal <> "" Then
            '  Rep.SetParameterValue("fechas", "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
            todo = "PERIODO DEL " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")
            'Else
            'Rep.SetParameterValue("fechas", " ")
            '   todo = ""
            'End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            'Rep.SetParameterValue("sucursal", "")

            If chkCliente.Checked = True Then
                'Rep.SetParameterValue("cliente", " ")
                todo += "CLIENTE: " + txtNombeCliente.Text
            Else
                'Rep.SetParameterValue("cliente", " ")
            End If
            Rep.SetParameterValue("filtros", todo)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If

        If lstTipos.SelectedIndex = 5 Then
            'ADJUDICACIONES


            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Rep = New repEmpeniosAdjudicaciones
            Rep.SetDataSource(Co.filtroAdjudicaciones(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Adjudicaciones")
            If fechaFinal <> "" Then
                ' Rep.SetParameterValue("fechas", "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
                todo = "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")

            Else
                ' Rep.SetParameterValue("fechas", " ")
                todo = ""
            End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", "")

            If chkCliente.Checked = True Then
                Rep.SetParameterValue("cliente", "")
                todo += " CLIENTE: " + txtNombeCliente.Text
            Else
                Rep.SetParameterValue("cliente", " ")
            End If
            Rep.SetParameterValue("fechas", todo)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        If lstTipos.SelectedIndex = 6 Then

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repEmpeniosRepMens
            Rep.SetDataSource(Co.filtroRMEmpenios(fechaInicial, fechaFinal, idSucursal, idCaja, EC.Vis))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte Mensual")
            If fechaFinal <> "" Then
                'Rep.SetParameterValue("fechas", "COMPRAS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
                todo = "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")

            Else
                'Rep.SetParameterValue("fechas", " ")
                todo = ""
            End If
            If idSucursal <> -1 Then
                Rep.SetParameterValue("sucursal", "")
                todo += " SUCURSAL: " + cmbSucursal.Text
            Else
                Rep.SetParameterValue("sucursal", "  ")
            End If
            Rep.SetParameterValue("fechas", todo)

            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        If lstTipos.SelectedIndex = 7 Then
            'Compras

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            Rep = New repEmpeniosCompras2
            Rep.SetDataSource(Co.filtroTodosCompras(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Compras")
            If fechaFinal <> "" Then
                ' Rep.SetParameterValue("fechas", "COMPRAS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
                todo = "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")

            Else
                ' Rep.SetParameterValue("fechas", " ")
                todo = ""
            End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", "")

            If chkCliente.Checked = True Then
                Rep.SetParameterValue("cliente", " ")
                todo += " CLIENTE: " + txtNombeCliente.Text
            Else
                Rep.SetParameterValue("cliente", " ")
            End If
            Rep.SetParameterValue("fechas", todo)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()



        End If

        If lstTipos.SelectedIndex = 8 Then
            'compras detalle



            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            If chkCliente.Checked = True Then

                Rep = New repempeniosComprasDetalles
            Else
                ' Rep = New repEmpeniosDetallesSinCliente
                Rep = New repempeniosComprasDetalles

            End If


            Rep.SetDataSource(comprasDetalles)
            ' Rep.Subreports("repVehiculos").SetDataSource(Co.filtroComprasDetallesV(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion))
            ' Rep.Subreports("repTerrenos").SetDataSource(Co.filtroComprasDetallesT(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Compras (Detalles)")
            If fechaFinal <> "" Then
                'Rep.SetParameterValue("fechas", "COMPRAS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy"))
                todo = "PRESTAMOS DEL DÍA " + dtpFecha.Value.ToString("dd/MM/yyyy") + " AL " + dtpFechaHasta.Value.ToString("dd/MM/yyyy")

            Else
                '  Rep.SetParameterValue("fechas", " ")
                todo = ""
            End If
            If idSucursal <> -1 Then
                todo += " SUCURSAL: " + cmbSucursal.Text
            End If
            If idCaja <> -1 Then
                todo += " CAJA: " + cmbCaja.Text
            End If
            If idVendedor <> -1 Then
                todo += " VENDEDOR: " + cmbVendedor.Text
            End If
            Rep.SetParameterValue("sucursal", "")

            If chkCliente.Checked = True Then
                Rep.SetParameterValue("cliente", "")
                todo += " CLIENTE: " + txtNombeCliente.Text
            Else
                Rep.SetParameterValue("cliente", " ")
            End If
            If cmbClasificacion.SelectedIndex <> 0 Then
                todo += " CLASIFICACIÓN: " + cmbClasificacion.Text
            End If
            Rep.SetParameterValue("fechas", todo)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()



        End If
        If lstTipos.SelectedIndex = 10 Then

            Dim em As New dbEmpenios(MySqlcon)
            Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rep = New repEmpeniosEstatus
            Dim filtros As String = ""
            filtros += "Estado de empeños: " + comboStatus.Text + "  del: " + dtpFecha.Value.ToString("yyyy/MM/dd") + " al: " + dtpFechaHasta.Value.ToString("yyyy/MM/dd")
            'em.reporteEstados(dtpFecha.Value.ToString("yyyy/MM/dd"), dtpFechaHasta.Value.ToString("yyyy/MM/dd"), comboStatus.Text)
            rep.SetDataSource(em.reporteEstados(dtpFecha.Value.ToString("yyyy/MM/dd"), dtpFechaHasta.Value.ToString("yyyy/MM/dd"), comboStatus.Text, idCliente))
            rep.SetParameterValue("encabezado", GlobalNombreEmpresa)
            rep.SetParameterValue("titulo", "Reporte de estados de empeños")
            rep.SetParameterValue("filtros", filtros)
            Dim RV As New frmReportes(rep, False)
            RV.Show()


        End If
    End Sub

    Private Sub cmbCaja_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCaja.SelectedIndexChanged
        If cmbCaja.SelectedIndex = 0 Then
            idCaja = -1
        Else
            idCaja = IdsCajas.Valor(cmbCaja.SelectedIndex)
        End If
    End Sub

    Private Sub cmbSucursal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        If cmbSucursal.SelectedIndex <> 0 Then
            cmbCaja.Enabled = True
            sa.DaOpciones(GlobalIdEmpresa, False)
            idCaja = sa.idCaja
            LlenaCombos("tblcajas", cmbCaja, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(cmbSucursal.SelectedIndex).ToString, "Todas")
            cmbCaja.SelectedIndex = IdsCajas.Busca(idCaja)

            idSucursal = IdsSucursales.Valor(cmbSucursal.SelectedIndex)
        Else
            idSucursal = -1
            cmbCaja.Enabled = False
            cmbCaja.Items.Clear()
            idCaja = -1
        End If
        
    End Sub

    Private Sub cmbClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClasificacion.SelectedIndexChanged

        If cmbClasificacion.SelectedIndex <> 0 Then
            idClasificacion = IdsClasificacion.Valor(cmbClasificacion.SelectedIndex)
        Else
            idClasificacion = -1
        End If
    End Sub

    Private Sub cmbVendedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendedor.SelectedIndexChanged
        If cmbVendedor.SelectedIndex = 0 Then
            idVendedor = -1
        Else
            idVendedor = IdsVendedores.Valor(cmbVendedor.SelectedIndex)
        End If
    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        dtpFechaHasta.MinDate = dtpFecha.Value
    End Sub

    Private Sub dtpFechaHasta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaHasta.ValueChanged

    End Sub

    Private Function EmpeniosDetalles(PPendientes As Boolean)
        Dim tablaJ As DataTable
        Dim tablaV As DataTable
        Dim tablaT As DataTable
        If PPendientes Then
            fechaInicial = ""
            fechaFinal = ""
        End If
        tablaJ = Co.filtroEmpeniosDetalles(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion, PPendientes).ToTable
        tablaV = Co.filtroEmpeniosDetallesV(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion, PPendientes).ToTable
        tablaT = Co.filtroEmpeniosDetallesT(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion, PPendientes).ToTable

        tablaDetalles.Clear()
        For i As Integer = 0 To tablaJ.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = tablaDetalles.NewRow()
            dr1("Cliente") = tablaJ.Rows(i)(0).ToString
            dr1("Tipo") = "Joyería"
            dr1("Fecha") = tablaJ.Rows(i)(1).ToString
            dr1("Serie") = tablaJ.Rows(i)(2).ToString
            dr1("Clasificacion") = tablaJ.Rows(i)(3).ToString
            dr1("Descripcion") = tablaJ.Rows(i)(4).ToString + ", " + tablaJ.Rows(i)(5).ToString + "K, " + tablaJ.Rows(i)(6).ToString + "gr."
            dr1("Evaluo") = tablaJ.Rows(i)(7).ToString
            dr1("Prestamo") = tablaJ.Rows(i)(8).ToString

            tablaDetalles.Rows.Add(dr1)
        Next

        For i As Integer = 0 To tablaV.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = tablaDetalles.NewRow()
            dr1("Cliente") = tablaV.Rows(i)(0).ToString
            dr1("Tipo") = "Vehículos"
            dr1("Fecha") = tablaV.Rows(i)(1).ToString
            dr1("Serie") = tablaV.Rows(i)(2).ToString
            dr1("Clasificacion") = tablaV.Rows(i)(3).ToString
            dr1("Descripcion") = tablaV.Rows(i)(4).ToString + ", " + tablaV.Rows(i)(5).ToString + ", " + tablaV.Rows(i)(6).ToString + ", No. Serie:" + tablaV.Rows(i)(7).ToString + ", Placas:" + tablaV.Rows(i)(8).ToString
            dr1("Evaluo") = tablaV.Rows(i)(9).ToString
            dr1("Prestamo") = tablaV.Rows(i)(10).ToString

            tablaDetalles.Rows.Add(dr1)
        Next
        For i As Integer = 0 To tablaT.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = tablaDetalles.NewRow()
            dr1("Cliente") = tablaT.Rows(i)(0).ToString
            dr1("Tipo") = "Terreno"
            dr1("Fecha") = tablaT.Rows(i)(1).ToString
            dr1("Serie") = tablaT.Rows(i)(2).ToString
            dr1("Clasificacion") = tablaT.Rows(i)(3).ToString
            dr1("Descripcion") = tablaT.Rows(i)(4).ToString
            dr1("Evaluo") = tablaT.Rows(i)(5).ToString
            dr1("Prestamo") = tablaT.Rows(i)(6).ToString

            tablaDetalles.Rows.Add(dr1)
        Next

        If DS.Tables.Count <> 0 Then
            DS.Tables.RemoveAt(0)
        End If
        DS.Tables.Add(tablaDetalles)
        'DS.WriteXmlSchema("tblEmpeniosDetallesV2.xml")
        Return DS
    End Function

    Private Function comprasDetalles()

        Dim tablaJ As DataTable
        Dim tablaV As DataTable
        Dim tablaT As DataTable

        tablaJ = Co.filtroComprasDetalles(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion).ToTable
        tablaV = Co.filtroComprasDetallesV(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion).ToTable
        tablaT = Co.filtroComprasDetallesT(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor, idClasificacion).ToTable

        tablaDetalles.Clear()
        For i As Integer = 0 To tablaJ.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = tablaDetalles.NewRow()
            dr1("Cliente") = tablaJ.Rows(i)(0).ToString
            dr1("Tipo") = "Joyería"
            dr1("Fecha") = tablaJ.Rows(i)(1).ToString
            dr1("Serie") = tablaJ.Rows(i)(2).ToString
            dr1("Clasificacion") = tablaJ.Rows(i)(3).ToString
            dr1("Descripcion") = tablaJ.Rows(i)(4).ToString + ", " + tablaJ.Rows(i)(5).ToString + "K, " + tablaJ.Rows(i)(6).ToString + "gr."
            dr1("Evaluo") = tablaJ.Rows(i)(7).ToString
            dr1("Prestamo") = tablaJ.Rows(i)(8).ToString

            tablaDetalles.Rows.Add(dr1)
        Next

        For i As Integer = 0 To tablaV.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = tablaDetalles.NewRow()
            dr1("Cliente") = tablaV.Rows(i)(0).ToString
            dr1("Tipo") = "Vehículos"
            dr1("Fecha") = tablaV.Rows(i)(1).ToString
            dr1("Serie") = tablaV.Rows(i)(2).ToString
            dr1("Clasificacion") = tablaV.Rows(i)(3).ToString
            dr1("Descripcion") = tablaV.Rows(i)(4).ToString + ", " + tablaV.Rows(i)(5).ToString + ", " + tablaV.Rows(i)(6).ToString + ", No. Serie:" + tablaV.Rows(i)(7).ToString + ", Placas:" + tablaV.Rows(i)(8).ToString
            dr1("Evaluo") = tablaV.Rows(i)(9).ToString
            dr1("Prestamo") = tablaV.Rows(i)(10).ToString

            tablaDetalles.Rows.Add(dr1)
        Next
        For i As Integer = 0 To tablaT.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = tablaDetalles.NewRow()
            dr1("Cliente") = tablaT.Rows(i)(0).ToString
            dr1("Tipo") = "Terreno"
            dr1("Fecha") = tablaT.Rows(i)(1).ToString
            dr1("Serie") = tablaT.Rows(i)(2).ToString
            dr1("Clasificacion") = tablaT.Rows(i)(3).ToString
            dr1("Descripcion") = tablaT.Rows(i)(4).ToString
            dr1("Evaluo") = tablaT.Rows(i)(5).ToString
            dr1("Prestamo") = tablaT.Rows(i)(6).ToString

            tablaDetalles.Rows.Add(dr1)
        Next

        If DS.Tables.Count <> 0 Then
            DS.Tables.RemoveAt(0)
        End If
        DS.Tables.Add(tablaDetalles)
        'DS.WriteXmlSchema("tblEmpeniosDetallesV2.xml")
        Return DS
       
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class