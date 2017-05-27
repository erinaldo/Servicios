Public Class frmEmpeniosAdjudicaciones
    Dim idsSucursales As New elemento
    Dim A As New dbEmpeniosAdjudicaciones(MySqlcon)
    Dim EP As New dbEmpeniosPagos(MySqlcon)
    Dim E As New dbEmpenios(MySqlcon)
    Dim fechaI As String
    Dim fechaF As String
    Dim tablaAux As DataTable
    'Dim tabla As New DataTable
    Dim pago As Double
    Dim interes As Double = 0
    Dim Almacenaje As Double = 0
    Dim refrendo As Double = 0
    Dim cargando As Boolean = True
    Dim seleccionado As Integer
    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean
    Dim checked As Boolean

    Private Sub frmEmpeniosQuedar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            chkTiempoReal.Checked = GlobalConsultaTiempoReal
            cargando = True
            ConsultaOn = False
            LlenaCombos("tblsucursales", cmbSucursal, "nombre", "nombret", "idsucursal", idsSucursales, , "Todas")
            dtpFecha1.Value = Date.Now
            dtpFecha2.Value = Date.Now
            'dtpFecha2.MinDate = dtpFecha1.Value
            'tabla.Columns.Add("Selección")
            'tabla.Columns.Add("idMovimiento")
            'tabla.Columns.Add("Folio")
            'tabla.Columns.Add("F. Inicio")
            'tabla.Columns.Add("Cliente")
            'tabla.Columns.Add("Descripción")
            'tabla.Columns.Add("Total")
            'tabla.Columns.Add("Refrendo") '7
            'tabla.Columns.Add("Restante") '8
            'tabla.Columns.Add("Último Pago")
            'tabla.Columns.Add("Cant. último Pago") '10


            'DGServicios.DataSource = tabla
            '  DGServicios.Columns(0).Visible = False
            'DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            ' tabla.Columns.Add("Estado")
            cargando = False
            ConsultaOn = True
            If chkTiempoReal.Checked Then Buscar()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)

        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub


    Private Sub Buscar()
        If cargando = False Then
            btnSeleccionar.Enabled = False
            checked = True
            'If chkFecha.Checked = True Then
            fechaI = dtpFecha1.Value.ToString("yyyy/MM/dd")
            fechaF = dtpFecha2.Value.ToString("yyyy/MM/dd")
            'Else
            '   fechaF = ""
            '   fechaI = ""
            'End If
            'tablaAux = A.filtroBusqueda(fechaI, fechaF, txtSerie.Text, txtFolio.Text, idsSucursales.Valor(cmbSucursal.SelectedIndex), IdCliente)
            'tabla.Clear()
            'For i As Integer = 0 To tablaAux.Rows.Count - 1
            '    Dim dr As DataRow

            '    dr = tabla.NewRow()
            '    dr("Selección") = 0
            '    dr("Folio") = tablaAux.Rows(i)(3).ToString
            '    dr("idMovimiento") = tablaAux.Rows(i)(0).ToString
            '    dr("F. Inicio") = Date.Parse(tablaAux.Rows(i)(1).ToString).ToString("dd/MM/yyyy")
            '    dr("Cliente") = tablaAux.Rows(i)(5).ToString
            '    If tablaAux.Rows(i)(6).ToString = "0" Then
            '        dr("Descripción") = A.buscarDescripcion(Double.Parse(tablaAux.Rows(i)(0).ToString))

            '    End If
            '    If tablaAux.Rows(i)(6).ToString = "1" Then
            '        dr("Descripción") = A.buscarDescripcionv(Double.Parse(tablaAux.Rows(i)(0).ToString))
            '    End If
            '    If tablaAux.Rows(i)(6).ToString = "2" Then
            '        dr("Descripción") = A.buscarDescripciont(Double.Parse(tablaAux.Rows(i)(0).ToString))
            '    End If
            '    dr("Total") = Double.Parse(tablaAux.Rows(i)(4).ToString).ToString("C2")
            '    If tablaAux.Rows(i)(4).ToString = "0" Then
            '        pago = EP.buscarPagos(tablaAux.Rows(i)(0), tablaAux.Rows(i)(1).ToString)
            '        CalculaRefrendo(Integer.Parse(tablaAux.Rows(i)(0).ToString), tablaAux.Rows(i)(2).ToString, "")
            '        dr("Refrendo") = (refrendo - pago).ToString("C2")
            '        '    If idEmpenio = Integer.Parse(tablaAux.Rows(i)(0).ToString) Then
            '        '        totalG = Double.Parse(tablaAux.Rows(i)(3).ToString)
            '        '        refrendoG = Double.Parse((refrendo - pago).ToString("0.00"))
            '        '    End If
            '        dr("Restante") = ((Double.Parse(tablaAux.Rows(i)(4).ToString) + refrendo) - pago).ToString("C2")
            '    Else
            '        CalculaRefrendo(Integer.Parse(tablaAux.Rows(i)(0).ToString), tablaAux.Rows(i)(1).ToString, EP.buscarUltimoPago(tablaAux.Rows(i)(0).ToString))
            '        dr("Refrendo") = refrendo.ToString("C2")
            '        pago = EP.buscarPagos(tablaAux.Rows(i)(0), tablaAux.Rows(i)(1).ToString)
            '        dr("Restante") = ((Double.Parse(tablaAux.Rows(i)(4).ToString) + refrendo) - pago).ToString("C2")
            '        ' If idEmpenio = Integer.Parse(tablaAux.Rows(i)(0).ToString) Then
            '        '        totalG = Double.Parse(tablaAux.Rows(i)(3).ToString)
            '        '        refrendoG = refrendo
            '        '  End If
            '    End If

            '    If (A.buscarFechaUltimoPago(Integer.Parse(tablaAux.Rows(i)(0).ToString)) <> "0") Then
            '        dr("Fecha último Pago") = A.buscarFechaUltimoPago(Integer.Parse(tablaAux.Rows(i)(0).ToString))
            '        dr("Cant. último Pago") = A.cantFechaUltomo.ToString("C2")
            '    Else
            '        dr("Fecha último Pago") = tablaAux.Rows(i)(2).ToString
            '        dr("Cant. último Pago") = "$0.00"
            '    End If








            '    tabla.Rows.Add(dr)
            'Next

            DGServicios.DataSource = A.filtroBusqueda(fechaI, fechaF, txtSerie.Text, txtFolio.Text, idsSucursales.Valor(cmbSucursal.SelectedIndex), IdCliente)
            Label7.Text = "Peso total: " + A.TotalPeso.ToString("0.00") + "gr."
            'DGServicios.Columns(7).Visible = False
            'DGServicios.Columns(8).Visible = False
            'DGServicios.Columns(10).Visible = False
            'DGServicios.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGServicios.ClearSelection()
        End If
    End Sub
    Private Sub CalculaRefrendo(ByVal idMov As Integer, ByVal fecha As String, ByVal fechaAux As String)
        E.LlenaDatos(idMov)
        Dim FechaInicio As Date = Date.Parse(fecha)
        Dim dias As Long
        If fechaAux = "" Then
            dias = DateDiff(DateInterval.Day, FechaInicio, Date.Now())
        Else
            dias = DateDiff(DateInterval.Day, FechaInicio, Date.Parse(fechaAux))
        End If

        interes = 0
        Almacenaje = 0
        refrendo = 0
        If dias = 0 Then
            interes = 0
            Almacenaje = 0
            refrendo = 0
        End If
        If dias >= 1 Then
            Dim Diasmenos30 As Integer
            If dias < 30 Then
                Diasmenos30 = dias
            Else
                Diasmenos30 = 30
            End If

            interes = E.TotalAux * E.A1a30 * Diasmenos30
            interes = interes / 100
            Almacenaje = E.TotalAux * E.B1a30 * Diasmenos30
            Almacenaje = Almacenaje / 100
            refrendo = interes + Almacenaje
        End If
        If dias >= 31 Then
            ' a los 31-60
            Dim Diasmenos60 As Integer
            If dias < 60 Then
                Diasmenos60 = dias - 30
            Else
                Diasmenos60 = 30
            End If
            interes = interes + ((E.TotalAux * E.A31a60 * Diasmenos60) / 100)
            Almacenaje = Almacenaje + ((E.TotalAux * E.B31a60 * Diasmenos60 / 100))
            refrendo = interes + Almacenaje
            If dias >= 61 Then
                ' a los 61-90
                Dim Diasmenos90 As Integer
                If dias < 90 Then
                    Diasmenos90 = dias - 60
                Else
                    Diasmenos90 = 30
                End If
                interes = interes + ((E.TotalAux * E.A61a90 * Diasmenos90) / 100)
                Almacenaje = Almacenaje + ((E.TotalAux * E.B61a90 * Diasmenos90 / 100))
                refrendo = interes + Almacenaje
                If dias >= 91 Then
                    'mas de 90
                    Dim Diasmas90 As Integer
                    Diasmas90 = dias - 90
                    interes = interes + ((E.TotalAux * E.A90mas * Diasmas90) / 100)
                    Almacenaje = Almacenaje + ((E.TotalAux * E.B90mas * Diasmas90 / 100))
                    refrendo = interes + Almacenaje
                End If

            End If

        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Buscar()
    End Sub

    Private Sub dtpFecha1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        If chkTiempoReal.Checked = True Then
            Buscar()
        End If
    End Sub

    Private Sub dtpFecha2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
       
        If chkTiempoReal.Checked = True Then
            Buscar()
        End If
    End Sub

    Private Sub txtSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerie.TextChanged
        If chkTiempoReal.Checked = True Then
            Buscar()
        End If
    End Sub

    Private Sub txtFolio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged
        If chkTiempoReal.Checked = True Then
            Buscar()
        End If
    End Sub

    Private Sub cmbSucursal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        If chkTiempoReal.Checked = True Then
            Buscar()
        End If
    End Sub

    Private Sub DGServicios_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellDoubleClick
        AbreDocumento(DGServicios.CurrentRow.Index)
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

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosAdjudicacionesAlta, PermisosN.Secciones.Empenios) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Try
            If MsgBox("¿Desea adjudicar este artículo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim nfilas As Integer = DGServicios.RowCount()
                Dim contador As Integer = 0
                For i As Integer = 0 To nfilas - 1
                    If DGServicios.Rows(i).Cells(0).Value = 1 Then
                        A.Adjudicar(DGServicios.Item(1, i).Value, DGServicios.Item(8, i).Value, Date.Now.ToString("yyyy/MM/dd"))
                        Dim em As New dbEmpenios(MySqlcon)
                        em.adjudicar(DGServicios.Item(1, i).Value)
                        contador += 1
                    End If
                Next
                If contador > 0 Then
                    PopUp("Adjudicado", 90)
                    Buscar()
                End If
                

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGServicios_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellClick
        btnSeleccionar.Enabled = True
        If e.RowIndex = -1 Then
            btnSeleccionar.Enabled = False

            If e.ColumnIndex = 0 Then
                Dim nfilas As Integer
                nfilas = DGServicios.RowCount()


                If checked = True Then
                    For i As Integer = 0 To nfilas - 1
                        DGServicios.Rows(i).Cells(0).Value = 1

                    Next
                    DGServicios.Columns(0).HeaderText = "X"
                    checked = False

                Else
                    For i As Integer = 0 To nfilas - 1
                        DGServicios.Rows(i).Cells(0).Value = 0
                    Next
                    DGServicios.Columns(0).HeaderText = " "
                    checked = True
                End If

            End If

            btnSeleccionar.Enabled = True
        End If
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            If DGServicios.Rows(e.RowIndex).Cells(0).Value = 1 Then

                DGServicios.Rows(e.RowIndex).Cells(0).Value = 0

            ElseIf DGServicios.Rows(e.RowIndex).Cells(0).Value = 0 Then

                DGServicios.Rows(e.RowIndex).Cells(0).Value = 1

            End If
        End If
    End Sub

    Private Sub txtClienteCodigo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClienteCodigo.TextChanged
        BuscaCliente()
        If chkTiempoReal.Checked = True Then
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
            If chkTiempoReal.Checked = True Then
                Buscar()
            End If
        End If

    End Sub

    Private Sub btnimprimir_Click(sender As Object, e As EventArgs) Handles btnimprimir.Click
        Try
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim filtros As String = ""
            Rep = New repEmpeniosAdjud
            Rep.SetDataSource(A.filtroBusqueda(fechaI, fechaF, txtSerie.Text, txtFolio.Text, idsSucursales.Valor(cmbSucursal.SelectedIndex), IdCliente))
            Rep.SetParameterValue("empresa", S.Nombre)
            Rep.SetParameterValue("telefono", "Reporte de Empeños")
            filtros = "DEL " + dtpFecha1.Value.ToString("dd/MM/yyyy") + " A " + dtpFecha2.Value.ToString("dd/MM/yyyy")
            If txtClienteCodigo.Text <> "" Then
                filtros += vbCrLf + "CLIENTE: " + txtClienteDatos.Text
            End If
            Rep.SetParameterValue("filtros", filtros)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, GlobalNombreApp)
        End Try
       
    End Sub
End Class