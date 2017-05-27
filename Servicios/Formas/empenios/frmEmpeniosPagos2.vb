Public Class frmEmpeniosPagos2
    Dim idCliente As Integer
    Dim Saldo As Double
    Dim CreditoCliente As Double
    Dim SaldoaFavor As Double
    Dim ConsultaOn As Boolean
    Dim IdsSucursales As New elemento
    Dim IdsSucursalesT As New elemento
    Dim IdsVendedores As New elemento
    Dim EP As New dbEmpeniosPagos(MySqlcon)
    Dim E As New dbEmpenios(MySqlcon)
    Dim fechaI As String
    Dim fechaF As String
    Dim canceladas As Integer
    Dim pagadas As Integer
    Dim tabla As New DataTable
    Dim interes As Double = 0
    Dim Almacenaje As Double = 0
    Dim desempenio As Double
    Dim refrendo As Double = 0
    Dim pago As Double
    Dim idEmpenio As Integer = 0
    Dim tablaPagos As New DataTable
    Dim idAbono As Integer = 0
    Dim RefrendoAux As Double = 0
    Dim TotalAux As Double = 0
    Dim restante As Double = 0
    Dim FechaEmpenio As String = ""
    Dim fechaAbono As String = ""
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorMorado As Color = Color.FromArgb(240, 180, 240)
    Dim ColorAzul As Color = Color.FromArgb(200, 200, 255)
    Dim ColorAmarillo As Color = Color.FromArgb(255, 255, 192)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim fechaAux As String
    Dim cantidadAux As Double
    Dim totalEmpenio As Double
    Dim totalRefrendo As Double
    Dim totalTotales As Double
    Dim refrendoG As Double
    Dim totalG As Double
    Dim primerCelda As Integer = -1
    Dim activo As Boolean
    Dim folio As String
    Dim serie As String
    Dim sa As New dbSucursalesArchivos
    Dim idCaja As Integer
    Dim IdsCajas As New elemento
    Dim IDCajaMov As Integer
    Dim AuxCantidad As Double
    Dim soloInteres As Boolean = True
    Dim buscando As Boolean = False
    Dim Intereses As dbEmpeniosConfiguracion
    Private Sub frmEmpeniosPagos2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        dtpFecha1.Value = Date.Now.Year.ToString + "/" + Date.Now.Month.ToString("00") + "/01"
        dtpFecha2.MinDate = dtpFecha1.Value
        dtpFecha1.MaxDate = dtpFecha2.Value
        ConsultaOn = True
        'LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursalesT)
        LlenaCombos("tblvendedores", cmbVendedor, "nombre", "nombret", "idvendedor", IdsVendedores)
        LlenaCombos("tblsucursales", ComboBox2, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        ConsultaOn = False
        'ComboBox3.SelectedIndex = IdsSucursalesT.Busca(GlobalIdSucursalDefault)
        idCliente = 0
        tabla.Columns.Add("idMovimiento")
        tabla.Columns.Add("F. Inicio")
        tabla.Columns.Add("Fecha")
        tabla.Columns.Add("Fecha V.")
        tabla.Columns.Add("Folio")
        tabla.Columns.Add("Descripción")
        tabla.Columns.Add("Total")
        tabla.Columns.Add("Refrendo")
        tabla.Columns.Add("Restante")
        tabla.Columns.Add("Estado")
        tabla.Columns.Add("Días")

        tablaPagos.Columns.Add("idAbono")
        tablaPagos.Columns.Add("idEmpenio")
        tablaPagos.Columns.Add("Fecha")
        tablaPagos.Columns.Add("Cantidad")
        tablaPagos.Columns.Add("Estado")
        tablaPagos.Columns.Add("Descuento")
        dgvTodosPagos.DataSource = tabla
        dgvTodosPagos.Columns(0).Visible = False
        dgvTodosPagos.Columns(10).Width = 65
        dgvTodosPagos.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        dgvPagosRealizados.DataSource = tablaPagos
        dgvPagosRealizados.Columns(0).Visible = False
        dgvPagosRealizados.Columns(1).Visible = False
        dgvPagosRealizados.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        lblDescripcion.Text = ""
        sa.DaOpciones(GlobalIdEmpresa, False)
        idCaja = sa.idCaja
        LlenaCombos("tblcajas", cmbCaja, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + GlobalIdSucursalDefault.ToString)
        cmbCaja.SelectedIndex = IdsCajas.Busca(idCaja)
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.PermitirCambioFecha, PermisosN.Secciones.Empenios) = False Then
            dtpFechaPago.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.PermitirDescuento, PermisosN.Secciones.Empenios) = False Then
            ckDescuento.Enabled = False
        End If
        txtcliente.Focus()

    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        BuscaClienteBoton()
        primerCelda = -1
        idEmpenio = 0
        lblPagado.Visible = False
        GroupBox2.Enabled = True
        
        'nuevoConcepto()

        'buscar()
    End Sub
    Private Sub BuscaClienteBoton()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Saldo = B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
            CreditoCliente = B.Cliente.Credito
            SaldoaFavor = CDbl(Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "0.00"))
            If B.Cliente.DireccionFiscal = 0 Then
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            Else
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            End If


            'TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(Saldo, "#,##0.00") + vbCrLf + "A Favor: " + Format(SaldoaFavor, "#,##0.00")

            idCliente = B.Cliente.ID
            tablaPagos.Clear()
            dgvPagosRealizados.DataSource = Nothing
            dgvTodosPagos.DataSource = Nothing
            ConsultaOn = False
            txtcliente.Text = B.Cliente.Clave
            ConsultaOn = True
            B.Dispose()
            Intereses = New dbEmpeniosConfiguracion(MySqlcon)
            Intereses.LlenaDatos()
            buscar()
            nuevoConcepto(False)
            buscando = True
            idEmpenio = 0
            'ConsultaDetalles()
            buscando = False
        End If
    End Sub

    Private Sub txtcliente_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcliente.TextChanged
        BuscaClienteBoton2()
    End Sub
    Private Sub BuscaClienteBoton2()
        Try
            Dim c As New dbClientes(MySqlcon)

            If c.BuscaCliente(txtcliente.Text) Then
                If c.DireccionFiscal = 0 Then
                    TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                Else
                    TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                End If
                Saldo = c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                CreditoCliente = c.Credito

                SaldoaFavor = CDbl(Format(c.DaSaldoAFavor(c.ID), "0.00"))
                'TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(Saldo, "#,##0.00") + vbCrLf + "A Favor: " + Format(SaldoaFavor, "#,##0.00")


                idCliente = c.ID
                Intereses = New dbEmpeniosConfiguracion(MySqlcon)
                Intereses.LlenaDatos()
                buscar()
                nuevoConcepto(False)
                'dgvPagosRealizados.DataSource = Nothing
            Else
                Label8.Text = ""
                idCliente = 0
                tablaPagos.Clear()
                nuevoConcepto(False)
                dgvPagosRealizados.DataSource = Nothing
                dgvTodosPagos.DataSource = Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub dtpFecha1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha1.ValueChanged
        dtpFecha2.MinDate = dtpFecha1.Value
        If idCliente <> 0 And chkFecha.Checked = True And chkTiempoReal.Checked = True And chkTiempoReal.Checked = True Then
            buscar()
        End If
    End Sub

    Private Sub dtpFecha2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha2.ValueChanged
        dtpFecha1.MaxDate = dtpFecha2.Value
        If idCliente <> 0 And chkFecha.Checked = True And chkTiempoReal.Checked = True And chkTiempoReal.Checked = True Then
            buscar()
        End If
    End Sub
    Private Sub buscar()
        Dim tablaAux As DataTable
        Dim Saldo As Double = 0
        lblDescripcion.Text = ""
        If chkFecha.Checked = True Then
            fechaI = dtpFecha1.Value.ToString("yyyy/MM/dd")
            fechaF = dtpFecha2.Value.ToString("yyyy/MM/dd")
        Else
            fechaI = ""
            fechaF = ""
        End If
        If chkCanceladas.Checked = True Then
            canceladas = 1
        Else
            canceladas = 0

        End If
        If chkPagadas.Checked = True Then
            pagadas = 1
        Else
            pagadas = 0
        End If
        tablaAux = EP.filtroBusqueda(idCliente, fechaI, fechaF, txtSerie.Text, txtFolio.Text, IdsSucursales.Valor(ComboBox2.SelectedIndex), canceladas, pagadas)
        tabla.Clear()
        For i As Integer = 0 To tablaAux.Rows.Count - 1
            Dim dr As DataRow

            dr = tabla.NewRow()
            dr("idMovimiento") = tablaAux.Rows(i)(0).ToString
            dr("F. Inicio") = Date.Parse(tablaAux.Rows(i)(1)).ToString("dd/MM/yyyy")
            dr("Fecha") = Date.Parse(tablaAux.Rows(i)(4)).ToString("dd/MM/yyyy")
            dr("Fecha V.") = (Date.Parse(tablaAux.Rows(i)(4)).AddDays(120)).ToString("dd/MM/yyyy")
            dr("Folio") = tablaAux.Rows(i)(2).ToString
            If tablaAux.Rows(i)(6).ToString = "0" Then
                dr("Descripción") = EP.buscarDescripcion(Double.Parse(tablaAux.Rows(i)(0).ToString))
            End If
            If tablaAux.Rows(i)(6).ToString = "1" Then
                dr("Descripción") = EP.buscarDescripcionV(Double.Parse(tablaAux.Rows(i)(0).ToString))
            End If
            If tablaAux.Rows(i)(6).ToString = "2" Then
                dr("Descripción") = EP.buscarDescripcionT(Double.Parse(tablaAux.Rows(i)(0).ToString))
            End If


            dr("Total") = Double.Parse(tablaAux.Rows(i)(3).ToString).ToString("C2")
            If tablaAux.Rows(i)(5).ToString = "0" Then
                pago = EP.buscarPagos(tablaAux.Rows(i)(0), tablaAux.Rows(i)(1).ToString)
              
                CalculaRefrendo(Integer.Parse(tablaAux.Rows(i)(0).ToString), tablaAux.Rows(i)(4).ToString, "", Integer.Parse(tablaAux.Rows(i)(6).ToString))
                dr("Refrendo") = (refrendo - pago).ToString("C2")
                If idEmpenio = Integer.Parse(tablaAux.Rows(i)(0)) Then
                    totalG = Double.Parse(tablaAux.Rows(i)(3))
                    refrendoG = Double.Parse((refrendo - pago).ToString("0.00"))
                End If
                dr("Restante") = ((Double.Parse(tablaAux.Rows(i)(3)) + refrendo) - pago).ToString("C2")
                Saldo += tablaAux.Rows(i)(3) + refrendo - pago
            Else
                'CalculaRefrendo(Integer.Parse(tablaAux.Rows(i)(0).ToString), tablaAux.Rows(i)(1).ToString, EP.buscarUltimoPago(tablaAux.Rows(i)(0).ToString))
                'dr("Refrendo") = refrendo.ToString("C2")
                dr("Refrendo") = "0.00"
                dr("Restante") = "0.00"
                'pago = EP.buscarPagos(tablaAux.Rows(i)(0), tablaAux.Rows(i)(1).ToString)
                'dr("Restante") = ((Double.Parse(tablaAux.Rows(i)(3).ToString) + refrendo) - pago).ToString("C2")
                'Saldo += tablaAux.Rows(i)(3) + refrendo - pago
                'If idEmpenio = Integer.Parse(tablaAux.Rows(i)(0).ToString) Then
                'totalG = Double.Parse(tablaAux.Rows(i)(3).ToString)
                'refrendoG = refrendo
                'End If
            End If
            dr("Estado") = tablaAux.Rows(i)(5).ToString

            dr("Días") = tablaAux.Rows(i)(7).ToString
            tabla.Rows.Add(dr)
        Next
        Label8.Text = "Saldo: $" + Format(Saldo, "#,###,##0.00")
        dgvTodosPagos.DataSource = tabla
        dgvTodosPagos.Columns(0).Visible = False
        dgvTodosPagos.Columns(9).Visible = False
        dgvTodosPagos.Columns(10).Width = 65
        dgvTodosPagos.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvTodosPagos.ClearSelection()
        tablaPagos.Clear()
        'dgvPagosRealizados.DataSource = Nothing
        dgvPagosRealizados.DataSource = tablaPagos
        dgvPagosRealizados.Columns(0).Visible = False
        dgvPagosRealizados.Columns(1).Visible = False
        dgvPagosRealizados.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    End Sub

    Private Sub chkFecha_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFecha.CheckedChanged
        If idCliente <> 0 Then
            buscar()
        End If
    End Sub

    Private Sub txtSerie_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSerie.TextChanged
        If idCliente <> 0 And chkTiempoReal.Checked = True Then
            buscar()
        End If
    End Sub

    Private Sub txtFolio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged
        If idCliente <> 0 And chkTiempoReal.Checked = True Then
            buscar()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

        If ConsultaOn = False And idCliente <> 0 And chkTiempoReal.Checked = True Then

            buscar()

        End If


    End Sub
    Private Sub CalculaRefrendo(ByVal idMov As Integer, ByVal fecha As String, ByVal fechaAux As String, ByVal pTipoemenio As Integer)
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
            If pTipoemenio = 1 Then
                'vehiculos
                If E.renovado = 0 Then
                    interes = E.TotalAux * Intereses.interes1a15V * 15
                    interes = interes / 100
                    Almacenaje = E.TotalAux * Intereses.almacenaje1a15V * 15
                    Almacenaje = Almacenaje / 100
                    refrendo = interes + Almacenaje
                Else
                    'que sea por dias si ya se renovó
                    interes = E.TotalAux * Intereses.interes1a15V * dias
                    interes = interes / 100
                    Almacenaje = E.TotalAux * Intereses.almacenaje1a15V * dias
                    Almacenaje = Almacenaje / 100
                    refrendo = interes + Almacenaje
                End If
            End If
            If pTipoemenio = 2 Then
                'terrenos
                If E.renovado = 0 Then
                    interes = E.TotalAux * Intereses.interes1a15T * 15
                    interes = interes / 100
                    Almacenaje = E.TotalAux * Intereses.almacenaje1a15T * 15
                    Almacenaje = Almacenaje / 100
                    refrendo = interes + Almacenaje
                Else
                    'que sea por dias si ya se renovó
                    interes = E.TotalAux * Intereses.interes1a15T * dias
                    interes = interes / 100
                    Almacenaje = E.TotalAux * Intereses.almacenaje1a15T * dias
                    Almacenaje = Almacenaje / 100
                    refrendo = interes + Almacenaje
                End If
            End If
        End If
        If dias >= 1 Then
            Dim Diasmenos30 As Integer
            If dias < 30 Then
                Diasmenos30 = dias
            Else
                Diasmenos30 = 30
            End If
            If pTipoemenio = 0 Then
                interes = E.TotalAux * Intereses.A1a30 * Diasmenos30
                interes = interes / 100
                Almacenaje = E.TotalAux * Intereses.B1a30 * Diasmenos30
                Almacenaje = Almacenaje / 100
                refrendo = interes + Almacenaje
            End If
            If pTipoemenio = 1 Then
                'VEHICULOS
                If Diasmenos30 <= 15 Then
                    If E.renovado = 0 Then
                        interes = E.TotalAux * Intereses.interes1a15V * 15
                        interes = interes / 100
                        Almacenaje = E.TotalAux * Intereses.almacenaje1a15V * 15
                        Almacenaje = Almacenaje / 100
                    Else
                        interes = E.TotalAux * Intereses.interes1a15V * dias
                        interes = interes / 100
                        Almacenaje = E.TotalAux * Intereses.almacenaje1a15V * dias
                        Almacenaje = Almacenaje / 100
                    End If

                Else

                    interes = E.TotalAux * Intereses.interes16a30V * Diasmenos30
                    interes = interes / 100
                    Almacenaje = E.TotalAux * Intereses.almacenaje16a30V * Diasmenos30
                    Almacenaje = Almacenaje / 100
                End If

                refrendo = interes + Almacenaje
            End If
            If pTipoemenio = 2 Then
                'TERRENOS
                If Diasmenos30 <= 15 Then
                    If E.renovado = 0 Then
                        interes = E.TotalAux * Intereses.interes1a15T * 15
                        interes = interes / 100
                        Almacenaje = E.TotalAux * Intereses.almacenaje1a15T * 15
                        Almacenaje = Almacenaje / 100
                    Else
                        interes = E.TotalAux * Intereses.interes1a15T * dias
                        interes = interes / 100
                        Almacenaje = E.TotalAux * Intereses.almacenaje1a15T * dias
                        Almacenaje = Almacenaje / 100
                    End If

                Else

                    interes = E.TotalAux * Intereses.interes16a30T * Diasmenos30
                    interes = interes / 100
                    Almacenaje = E.TotalAux * Intereses.almacenaje16a30T * Diasmenos30
                    Almacenaje = Almacenaje / 100
                End If

                refrendo = interes + Almacenaje
            End If

        End If
        If dias >= 31 Then
            ' a los 31-60
            Dim Diasmenos60 As Integer
            If dias < 60 Then
                Diasmenos60 = dias - 30
            Else
                Diasmenos60 = 30
            End If
            If pTipoemenio = 0 Then
                'JOYAS
                interes = interes + ((E.TotalAux * Intereses.A31a60 * Diasmenos60) / 100)
                Almacenaje = Almacenaje + ((E.TotalAux * Intereses.B31a60 * Diasmenos60 / 100))
                refrendo = interes + Almacenaje
            Else
                'VEHICULOS Y TERRENOS
                If pTipoemenio = 1 Then
                    interes = interes + ((E.TotalAux * Intereses.interes31a60V * Diasmenos60) / 100)
                    Almacenaje = Almacenaje + ((E.TotalAux * Intereses.almacenaje31a60V * Diasmenos60 / 100))
                    refrendo = interes + Almacenaje
                Else
                    interes = interes + ((E.TotalAux * Intereses.interes31a60T * Diasmenos60) / 100)
                    Almacenaje = Almacenaje + ((E.TotalAux * Intereses.almacenaje31a60T * Diasmenos60 / 100))
                    refrendo = interes + Almacenaje
                End If
            End If

            If dias >= 61 Then
                ' a los 61-90
                Dim Diasmenos90 As Integer
                If dias < 90 Then
                    Diasmenos90 = dias - 60
                Else
                    Diasmenos90 = 30
                End If
                If pTipoemenio = 0 Then
                    interes = interes + ((E.TotalAux * Intereses.A61a90 * Diasmenos90) / 100)
                    Almacenaje = Almacenaje + ((E.TotalAux * Intereses.B61a90 * Diasmenos90 / 100))
                    refrendo = interes + Almacenaje

                Else
                    'VEHICULOS Y TERRENOS
                    If pTipoemenio = 1 Then
                        interes = interes + ((E.TotalAux * Intereses.interes31a60V * Diasmenos90) / 100)
                        Almacenaje = Almacenaje + ((E.TotalAux * Intereses.interes31a60V * Diasmenos90 / 100))
                        refrendo = interes + Almacenaje
                    Else
                        interes = interes + ((E.TotalAux * Intereses.interes31a60T * Diasmenos90) / 100)
                        Almacenaje = Almacenaje + ((E.TotalAux * Intereses.almacenaje31a60T * Diasmenos90 / 100))
                        refrendo = interes + Almacenaje
                    End If
                    

                End If

                If dias >= 91 Then
                    'mas de 90
                    Dim Diasmas90 As Integer
                    Diasmas90 = dias - 90
                    If pTipoemenio = 0 Then
                        interes = interes + ((E.TotalAux * Intereses.A90mas * Diasmas90) / 100)
                        Almacenaje = Almacenaje + ((E.TotalAux * Intereses.B90mas * Diasmas90 / 100))
                        refrendo = interes + Almacenaje
                    Else
                        'VEHICULOS Y TERRENOS
                        Dim diasint As Integer = DateDiff(DateInterval.Day, Date.Parse(fecha), Date.Now)
                        If pTipoemenio = 1 Then
                            interes = E.TotalAux * Intereses.interes31a60V * diasint
                            interes = interes / 100
                            Almacenaje = E.TotalAux * Intereses.almacenaje31a60V * diasint
                            Almacenaje = Almacenaje / 100
                        Else
                            interes = E.TotalAux * Intereses.interes31a60T * diasint
                            interes = interes / 100
                            Almacenaje = E.TotalAux * Intereses.almacenaje31a60T * diasint
                            Almacenaje = Almacenaje / 100
                        End If
                        'interes = interes + ((E.TotalAux * E.A61a90 * Diasmenos60) / 100)
                        'Almacenaje = Almacenaje + ((E.TotalAux * E.B61a90 * Diasmenos60 / 100))
                        refrendo = interes + Almacenaje

                    End If

                End If

            End If

        End If
        If refrendo <> 0 Then
            'Dim dec As Decimal = Decimal.Parse(refrendo)
            refrendo = Math.Round(refrendo, 0)
            'refrendo = Double.Parse(Decimal.Round(dec, 0))
        End If

    End Sub

    Private Sub chkPagadas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPagadas.CheckedChanged
        If idCliente <> 0 And chkTiempoReal.Checked = True Then
            buscar()
        End If
    End Sub

    Private Sub chkCanceladas_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCanceladas.CheckedChanged
        If idCliente <> 0 And chkTiempoReal.Checked = True Then
            buscar()
        End If
    End Sub
    Private Sub pagos(ByVal pidEmpenio As Integer, ByVal pFecha As String)
        Intereses = New dbEmpeniosConfiguracion(MySqlcon)
        Intereses.LlenaDatos()
        dgvPagosRealizados.DataSource = EP.filtroBusqueda(pidEmpenio, pFecha, Intereses.Vis)
        dgvPagosRealizados.Columns(0).Visible = False
        dgvPagosRealizados.Columns(1).Visible = False
        dgvPagosRealizados.Columns(2).HeaderText = "Fecha"
        dgvPagosRealizados.Columns(3).HeaderText = "Cantidad"
        dgvPagosRealizados.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgvPagosRealizados.ClearSelection()
    End Sub

    Private Sub dgvTodosPagos_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTodosPagos.CellClick
        llenarPagos()
        nuevoConcepto(True)
        primerCelda = dgvTodosPagos.FirstDisplayedCell.RowIndex
    End Sub
    Private Sub llenarPagos()
        Dim pagosAux As Double 'MODIFIQUE
        pagosAux = EP.buscarPagos(dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value, dgvTodosPagos.Item(2, dgvTodosPagos.CurrentCell.RowIndex).Value) 'MODIFIQUE
        idEmpenio = dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value
        'estos serviran para guardarlos proximamente
        lblDescripcion.Text = " DESCRIPCIÓN: " + EP.buscarDescripcioncom(dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value)
        totalG = dgvTodosPagos.Item(6, dgvTodosPagos.CurrentCell.RowIndex).Value
        refrendoG = dgvTodosPagos.Item(7, dgvTodosPagos.CurrentCell.RowIndex).Value
        'en el pago
        RefrendoAux = dgvTodosPagos.Item(7, dgvTodosPagos.CurrentCell.RowIndex).Value + pagosAux
        'RefrendoAux = dgvTodosPagos.Item(6, dgvTodosPagos.CurrentCell.RowIndex).Value
        TotalAux = dgvTodosPagos.Item(6, dgvTodosPagos.CurrentCell.RowIndex).Value
        FechaEmpenio = Date.Parse(dgvTodosPagos.Item(2, dgvTodosPagos.CurrentCell.RowIndex).Value).ToString("yyyy/MM/dd")
        pagos(idEmpenio, Date.Parse(dgvTodosPagos.Item(2, dgvTodosPagos.CurrentCell.RowIndex).Value).ToString("yyyy/MM/dd"))
        lblPagado.Visible = False
        GroupBox2.Enabled = True
        ConsultaOn = True
        ConsultaDetalles(False)
        ConsultaOn = False
    End Sub

    Private Sub btnGuardarConcepto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardarConcepto.Click
        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        If idCliente <> 0 Then
            If idEmpenio = 0 Then
                MsgBox("No se ha seleccionado ningún empeño.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                Try
                    Dim HayError As Boolean = False
                    Dim MsgError As String = ""
                    Dim PagoTipo As Integer
                    Dim descuento As Integer = 0
                    If ckDescuento.Checked = True Then
                        descuento = 1
                    End If
                    If activo = False Then
                        MsgError += vbCrLf + "El pago está inactivo."
                        HayError = True
                    End If
                    If Double.Parse(txtCantidadPago.Text) = 0 Then
                        MsgError += vbCrLf + " La cantidad debe ser mayor a cero."
                        HayError = True
                    End If
                    If Double.Parse(txtCantidadPago.Text) + Double.Parse(txtDescuento.Text) > Double.Parse(refrendoG + TotalAux) And btnGuardarConcepto.Text = "Guardar" Then
                        MsgError += vbCrLf + " La cantidad no puede ser mayor al total del adeudo."
                        HayError = True
                    End If
                    'If ckDescuento.Checked = True And ((refrendoG + TotalAux) <> (Double.Parse(txtCantidadPago.Text) + Double.Parse(txtDescuento.Text))) Then
                    'MsgError += vbCrLf + " El total del pago más el descuento deben ser igual al total general."
                    '    HayError = True
                    'End If
                    If ckDescuento.Checked = True And refrendoG < Double.Parse(txtDescuento.Text) Then
                        MsgError += vbCrLf + " El total del descuento no puede ser mayor al refrendo."
                        HayError = True
                    End If
                    If Double.Parse(txtCantidadPago.Text) + Double.Parse(txtDescuento.Text) > Double.Parse(Double.Parse(RefrendoAux + TotalAux + EP.Cantidad) - Double.Parse(EP.buscarPagos(idEmpenio, FechaEmpenio))) And btnGuardarConcepto.Text <> "Guardar" Then
                        MsgError += vbCrLf + " La cantidad no puede ser mayor al total del adeudo."
                        HayError = True
                    End If
                    Dim c As New dbEmpenios(MySqlcon)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.PermitirCambioFecha, PermisosN.Secciones.Empenios) = False And dtpFechaPago.Value.ToString("yyyy/MM/dd") < c.DaUltimaFecha() Then
                        MsgError += " Fecha incorrecta. Revise que la fecha de su computadora este correcta."
                        HayError = True
                    End If

                    If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPAgosAlta, PermisosN.Secciones.Empenios) = False And btnGuardarConcepto.Text = "Guardar" Then
                        HayError = True
                        MsgError += " No tiene permiso para realizar esta operación."
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPagosCambios, PermisosN.Secciones.Empenios) = False And btnGuardarConcepto.Text <> "Guardar" Then
                        HayError = True
                        MsgError += " No tiene permiso para realizar esta operación."
                    End If
                    If HayError = False Then

                        If btnGuardarConcepto.Text = "Guardar" Then


                            If Double.Parse(txtCantidadPago.Text) + Double.Parse(txtDescuento.Text) > refrendoG Then
                                PagoTipo = 0
                                'esto indica que el pago involucra refrendo y empeño
                                soloInteres = False
                            Else
                                PagoTipo = 1
                                'esto indica que solo pago refrendo(interes)
                                If Double.Parse(txtCantidadPago.Text) + Double.Parse(txtDescuento.Text) = refrendoG Then
                                    soloInteres = False
                                Else
                                    soloInteres = True
                                End If

                            End If

                            MovCajaAdd()
                            If PagoTipo = False And soloInteres = False And refrendoG > 0 Then
                                'Uno para interes y otro para pago
                                Dim RefrendoAux As Double = refrendoG
                                If refrendoG - CDbl(txtDescuento.Text) <> 0 Then
                                    EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), refrendoG - CDbl(txtDescuento.Text), totalG, refrendoG, 1, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 0)
                                    RefrendoAux = refrendoG - CDbl(txtDescuento.Text)
                                End If
                                If refrendoG - CDbl(txtDescuento.Text) <> 0 Then                                    
                                        EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), Double.Parse(txtCantidadPago.Text) - RefrendoAux, totalG, 0, 0, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 0)
                                Else
                                    EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), Double.Parse(txtCantidadPago.Text), totalG, 0, 0, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 0)
                                End If
                                'End If
                                If descuento = 1 Then
                                    EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), Double.Parse(txtDescuento.Text), totalG - (Double.Parse(txtCantidadPago.Text) - refrendo), 0, 0, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 1)
                                End If
                            Else
                                EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), txtCantidadPago.Text, totalG, refrendoG, PagoTipo, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 0)
                                If descuento = 1 Then
                                    EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), txtDescuento.Text, totalG - txtCantidadPago.Text, refrendoG, PagoTipo, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 1)
                                End If
                            End If


                            fechaAux = dtpFechaPago.Value.ToString("yyyy/MM/dd")
                            cantidadAux = Double.Parse(txtCantidadPago.Text)
                            '  If ckDescuento.Checked = False Then
                            ConsultaDetalles(False)
                            'End If

                            'nuevoContrato()
                            nuevoConcepto(True)
                            buscar()
                            If dgvTodosPagos.RowCount > primerCelda And primerCelda > -1 Then dgvTodosPagos.FirstDisplayedScrollingRowIndex = primerCelda
                            'buscar()
                            ' If soloInteres = False Then
                            'If ckDescuento.Checked = False Then
                            ImprimirRecibo()
                            'End If
                            ckDescuento.Checked = False
                            'End If

                        Else
                            MovCajaMod()
                            EP.ModificarPago(idAbono, idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), txtCantidadPago.Text, descuento)
                            fechaAux = dtpFechaPago.Value.ToString("yyyy/MM/dd")
                            cantidadAux = Double.Parse(txtCantidadPago.Text)
                            ' If ckDescuento.Checked = False Then
                            ConsultaDetalles(True)
                            'End If
                            'nuevoContrato()
                            nuevoConcepto(True)
                            buscar()
                            If dgvTodosPagos.RowCount > primerCelda And primerCelda > -1 Then dgvTodosPagos.FirstDisplayedScrollingRowIndex = primerCelda
                            'buscar()
                            'If soloInteres = False Then
                            ' If ckDescuento.Checked = False Then
                            ImprimirRecibo()
                            'End If
                            ckDescuento.Checked = False
                            'End If
                        End If
                        idEmpenio = 0
                    Else
                        MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
            End If
        End If
    End Sub
    Private Sub ConsultaDetalles(pSinNuevo As Boolean)
        'tengo que sacar el total de los abonos
        'el total de lo que debe
        'agregarselos a los label
        'verificar si ya se saldo la deuda
        pagos(idEmpenio, FechaEmpenio)
        lblAbono.Text = EP.buscarPagos(idEmpenio, FechaEmpenio).ToString("C2")
        ' lblRefrendo.Text = (RefrendoAux + TotalAux).ToString("C2")
        lblRefrendo.Text = RefrendoAux.ToString("C2")
        ' restante = Double.Parse(RefrendoAux + TotalAux) - Double.Parse(EP.buscarPagos(idEmpenio, FechaEmpenio))
        restante = Double.Parse(RefrendoAux + TotalAux) - Double.Parse(EP.buscarPagos(idEmpenio, FechaEmpenio))
        lblRestante.Text = (restante).ToString("C2")
        If buscando <> True Then
            If restante = 0 Then
                'ya esta pagado
                EP.ModificarPagado(idEmpenio)
                Dim em As New dbEmpenios(MySqlcon)
                em.entregar(idEmpenio)
                ' EP.ModificarHabilitado(idEmpenio)
                lblPagado.Visible = True
                GroupBox2.Enabled = False

                totalEmpenio = 0
                totalRefrendo = 0
                totalTotales = 0

            Else
                'no esta pagado
                If pSinNuevo = False Then nuevoContrato()

            End If
        End If
    End Sub

    Private Sub dgvPagosRealizados_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPagosRealizados.CellClick
        idAbono = dgvPagosRealizados.Item(0, dgvPagosRealizados.CurrentCell.RowIndex).Value
        EP.llenaDatos(idAbono)
        dtpFechaPago.Value = EP.Fecha
        fechaAbono = EP.Fecha
        cmbCaja.SelectedIndex = IdsCajas.Busca(EP.caja)
        txtCantidadPago.Text = EP.Cantidad
        AuxCantidad = EP.Cantidad
        btnEliminarConcepto.Enabled = True
        IDCajaMov = EP.idMovimiento
        btnGuardarConcepto.Text = "Modificar"
        If EP.Estado = True Then
            activo = False
        Else
            activo = True
        End If
        If lblRestante.Text = "$0.00" Then
            GroupBox2.Enabled = False


        Else
            If EP.Estado = 1 Then
                GroupBox2.Enabled = False
            Else

                GroupBox2.Enabled = True
            End If

        End If
        If dgvPagosRealizados.Item(5, dgvPagosRealizados.CurrentCell.RowIndex).Value <> " " Then
            ckDescuento.Checked = True
        Else
            ckDescuento.Checked = False
        End If
    End Sub

    Private Sub txtCantidadPago_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidadPago.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    BotonGuardar()
        'End If
    End Sub

    Private Sub txtCantidadPago_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidadPago.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCantidadPago_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidadPago.Leave
        Dim x As Double
        If txtCantidadPago.Text = "." Then
            txtCantidadPago.Text = "0.00"
        End If
        If txtCantidadPago.Text = "" Then
            txtCantidadPago.Text = "0.00"
        Else
            x = Double.Parse(txtCantidadPago.Text)
            txtCantidadPago.Text = Format(x, "0.00")
        End If
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        nuevoConcepto(True)
        If dgvTodosPagos.RowCount > primerCelda And primerCelda > -1 Then dgvTodosPagos.FirstDisplayedScrollingRowIndex = primerCelda
    End Sub
    Private Sub nuevoConcepto(pFocus As Boolean)
        btnEliminarConcepto.Enabled = False
        btnGuardarConcepto.Text = "Guardar"
        txtCantidadPago.Text = "0.00"
        dtpFechaPago.Value = Date.Now
        idAbono = -1
        cmbCaja.SelectedIndex = IdsCajas.Busca(idCaja)
        ckDescuento.Checked = False
        activo = True
        If pFocus Then txtCantidadPago.Focus()
    End Sub

    Private Sub btnEliminarConcepto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarConcepto.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Empenios.EmpeniosPAgosBaja, PermisosN.Secciones.Empenios) = False Then
                MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If activo = False Then
                MsgBox("El Pago está inactivo.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                If MsgBox("¿Desea eliminar este pago?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    MovCajaElim()
                    EP.EliminarPago(idAbono)
                    'ConsultaDetalles()
                    pagos(idEmpenio, FechaEmpenio)
                    buscar()
                    nuevoConcepto(True)
                    If dgvTodosPagos.RowCount > primerCelda And primerCelda > -1 Then dgvTodosPagos.FirstDisplayedScrollingRowIndex = primerCelda
                    PopUp("Concepto Eliminado", 90)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub nuevoFolio(ByVal pIdSucursal As Integer)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Dim Co As New dbEmpeniosConfiguracion(MySqlcon)

        If Co.folio = 0 Then
            Dim anio As String = Date.Now.Year.ToString.Chars(2) + Date.Now.Year.ToString.Chars(3)
            serie = Date.Now.Day.ToString("00") + Date.Now.Month.ToString("00") + anio
            Dim V As New dbEmpenios(MySqlcon)
            folio = V.DaNuevoFolio(serie, pIdSucursal).ToString
            If CInt(folio) < Sf.FolioInicial Then folio = Sf.FolioInicial.ToString
        Else
            If Sf.BuscaFolios(pIdSucursal, dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0) Then
                serie = Sf.Serie
            Else
                serie = ""
            End If

            Dim V As New dbEmpenios(MySqlcon)
            folio = V.DaNuevoFolio(txtSerie.Text, pIdSucursal).ToString
            If CInt(folio) < Sf.FolioInicial Then folio = Sf.FolioInicial.ToString
        End If

    End Sub

    Private Sub nuevoContrato()
        If ConsultaOn = False Then
            Dim diferencia As Double = 0
            'SE ESTA NMOVIENDO EL IDMOVIMIENTO
            Dim em As New dbEmpenios(MySqlcon)
            'em.LlenaDatos(idEmpenio)
            If lblRestante.Text <> "0.00" Then
                '  soloInteres = True
                If EP.buscarPagos(idEmpenio, FechaEmpenio).ToString("C2") > RefrendoAux Then
                    'reseteo
                    restante = RefrendoAux + TotalAux - EP.buscarPagos(idEmpenio, FechaEmpenio)
                    EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), restante, totalG, 0, 3, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 0)
                    EP.ModificarPagado(idEmpenio)
                    idEmpenio = em.CreaRenovado(idEmpenio, Date.Now.ToString("yyyy/MM/dd"), restante, IdsCajas.Valor(cmbCaja.SelectedIndex))
                    em.LlenaDatos(idEmpenio)
                    'ActualizarConfig(em.tipoEmpenio, idEmpenio)
                    'diferencia = EP.buscarPagos(idEmpenio, FechaEmpenio).ToString("C2") - RefrendoAux
                    'EP.ModificarTotalAuxEmpenio(idEmpenio, TotalAux - diferencia)
                    'EP.ModificarTotalAuxEmpenio(idEmpenio, TotalAux + RefrendoAux - CDbl(txtCantidadPago.Text))
                    'EP.ModificarFechaEmpenio(idEmpenio, Date.Now.ToString("yyyy/MM/dd"))
                    nuevoFolio(em.IdSucursal)
                    EP.ModificarFolio(idEmpenio, serie, folio)
                    'EP.ModificarHabilitado(idEmpenio)
                    'CalculaRefrendo(idEmpenio, Date.Now.ToString("yyyy/MM/dd"), "", em.tipoEmpenio)
                    'RefrendoAux = refrendo
                    'TotalAux = TotalAux - diferencia
                    'FechaEmpenio = Date.Now.ToString("yyyy/MM/dd")
                    'pagos(idEmpenio, FechaEmpenio)
                    'lblAbono.Text = EP.buscarPagos(idEmpenio, FechaEmpenio).ToString("C2")
                    'lblRefrendo.Text = (RefrendoAux + TotalAux).ToString("C2")
                    'restante = RefrendoAux + TotalAux - EP.buscarPagos(idEmpenio, FechaEmpenio)
                    'lblRestante.Text = restante.ToString("C2")
                    'EP.renovado(idEmpenio)
                    'soloInteres = False
                    'ConsultaDetalles()
                    ' Dim F As New frmEmpenios(idEmpenio, 0, 0, 0, Date.Now.ToString("yyyy/MM/dd"), TotalAux, True)
                    ' F.ShowDialog()
                    ' F.Dispose()
                    ' Dim Fe As New frmEmpenios(idEmpenio, 0, 0, 0, Date.Now.ToString("yyyy/MM/dd"), TotalAux, True)
                    ' Fe.ShowDialog()
                    ' Fe.Dispose()
                Else
                    If EP.buscarPagos(idEmpenio, FechaEmpenio).ToString("C2") = RefrendoAux Then
                        'reseteo
                        em.LlenaDatos(idEmpenio)
                        'restante = RefrendoAux + TotalAux - EP.buscarPagos(idEmpenio, FechaEmpenio)
                        EP.GuardarPago(idEmpenio, dtpFechaPago.Value.ToString("yyyy/MM/dd"), em.TotalAux, totalG, 0, 3, IdsCajas.Valor(cmbCaja.SelectedIndex), IDCajaMov, 0)
                        EP.ModificarPagado(idEmpenio)
                        idEmpenio = em.CreaRenovado(idEmpenio, Date.Now.ToString("yyyy/MM/dd"), em.TotalAux, IdsCajas.Valor(cmbCaja.SelectedIndex))
                        em.LlenaDatos(idEmpenio)
                        'ActualizarConfig(em.tipoEmpenio, idEmpenio)
                        'diferencia = EP.buscarPagos(idEmpenio, FechaEmpenio).ToString("C2") - RefrendoAux
                        'EP.ModificarTotalAuxEmpenio(idEmpenio, TotalAux - diferencia)
                        'EP.ModificarTotalAuxEmpenio(idEmpenio, TotalAux + RefrendoAux - CDbl(txtCantidadPago.Text))
                        'EP.ModificarFechaEmpenio(idEmpenio, Date.Now.ToString("yyyy/MM/dd"))
                        nuevoFolio(em.IdSucursal)
                        EP.ModificarFolio(idEmpenio, serie, folio)
                        'idEmpenio = em.CreaRenovado(idEmpenio, Date.Now.ToString("yyyy/MM/dd"), TotalAux + RefrendoAux - cantidadAux)
                        'em.LlenaDatos(idEmpenio)
                        'ActualizarConfig(em.tipoEmpenio, idEmpenio)
                        'EP.ModificarFechaEmpenio(idEmpenio, Date.Now.ToString("yyyy/MM/dd"))
                        'nuevoFolio(em.IdSucursal)
                        'EP.ModificarFolio(idEmpenio, serie, folio)
                        'FechaEmpenio = Date.Now.ToString("yyyy/MM/dd")
                        'EP.ModificarHabilitado(idEmpenio)
                        'CalculaRefrendo(idEmpenio, Date.Now.ToString("yyyy/MM/dd"), "", em.tipoEmpenio)
                        'RefrendoAux = refrendo.ToString("C2")
                        'pagos(idEmpenio, FechaEmpenio)
                        'lblAbono.Text = EP.buscarPagos(idEmpenio, FechaEmpenio).ToString("C2")
                        'lblRefrendo.Text = (RefrendoAux + TotalAux).ToString("C2")
                        'restante = RefrendoAux + TotalAux - EP.buscarPagos(idEmpenio, FechaEmpenio)
                        'lblRestante.Text = restante.ToString("C2")
                        'EP.renovado(idEmpenio)
                        ' ConsultaDetalles()
                        ' soloInteres = False

                    End If
                End If
                If soloInteres = False Then
                    Dim F As New frmEmpenios(idEmpenio, 0, 0, 0, em.Fecha, em.Total, True)
                    'Dim F As New frmEmpenios(dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value, 0, 0, 0, dgvTodosPagos.Item(2, dgvTodosPagos.CurrentCell.RowIndex).Value.ToString, Double.Parse(EP.buscaTotalAux(dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value)), True)
                    F.ShowDialog()
                    F.Dispose()
                End If


            End If

        End If

    End Sub
    Private Sub ActualizarConfig(ByVal TipoEmpeño As Integer, ByVal idMovimiento As Integer)
        Dim C As New dbEmpenios(MySqlcon)
        Dim Co As New dbEmpeniosConfiguracion(MySqlcon)
        Co.LlenaDatos()
        If TipoEmpeño = 0 Then
            C.ModificarConfiguracion(idMovimiento, Co.A1a30, Co.A31a60, Co.A61a90, Co.A90mas, Co.B1a30, Co.B31a60, Co.B61a90, Co.B90mas)
        End If
        If TipoEmpeño = 1 Then
            C.ModificarConfiguracion(idMovimiento, Co.interes1a15V, Co.interes16a30V, Co.interes31a60V, 0, Co.almacenaje1a15V, Co.almacenaje16a30V, Co.almacenaje31a60V, 0)
        End If
        If TipoEmpeño = 2 Then
            C.ModificarConfiguracion(idMovimiento, Co.interes1a15T, Co.interes16a30T, Co.interes31a60T, 0, Co.almacenaje1a15T, Co.almacenaje16a30T, Co.almacenaje31a60T, 0)
        End If


    End Sub
    Private Sub chkTiempoReal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTiempoReal.CheckedChanged
        If chkTiempoReal.Checked = True And idCliente <> 0 Then
            buscar()
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        buscar()
    End Sub

    Private Sub dgvTodosPagos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTodosPagos.CellContentClick

    End Sub

    Private Sub dgvTodosPagos_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTodosPagos.CellDoubleClick
        If MsgBox("¿Imprimir recibo del empeño?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim F As New frmEmpenios(dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value, 0, 0, 0, dgvTodosPagos.Item(2, dgvTodosPagos.CurrentCell.RowIndex).Value.ToString, Double.Parse(EP.buscaTotalAux(dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value)), True)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub dgvPagosRealizados_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPagosRealizados.CellContentClick

    End Sub

    Private Sub dgvPagosRealizados_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvPagosRealizados.CellFormatting
        'Activo', 'Inactivo'
        Select Case dgvPagosRealizados.Item(4, e.RowIndex).Value
            Case "Activo"
                e.CellStyle.BackColor = ColorVerde
            Case "Inactivo"
                e.CellStyle.BackColor = ColorAzul
        End Select
    End Sub

    Private Sub dgvTodosPagos_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvTodosPagos.CellFormatting
        If dgvTodosPagos.Item(9, e.RowIndex).Value <> 0 Then
            e.CellStyle.BackColor = ColorVerde
        End If

    End Sub
    Private Sub ImprimirRecibo()
        Dim em As New dbEmpenios(MySqlcon)
        em.LlenaDatos(idEmpenio)
        Dim cantidad1 As Double
        Dim centavos As Integer
        Dim folio As String
        cantidad1 = Int(Double.Parse(cantidadAux))
        centavos = Integer.Parse((Double.Parse(cantidadAux) - Double.Parse(cantidad1)) * 100)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        folio = em.Serie.ToString + "-" + em.Folio.ToString
        Rep = New repEmpeniosAbono()
        ' Rep.SetDataSource(Servicios())
        Rep.SetParameterValue("fechaPago", fechaAux)
        Rep.SetParameterValue("clienteNombre", em.Cliente.Nombre)
        Rep.SetParameterValue("folio", folio)
        Rep.SetParameterValue("empenioDescripcion", EP.buscarDescripcion(idEmpenio))
        Rep.SetParameterValue("tipoPago", em.formaPago)
        Rep.SetParameterValue("cantidadNumero", cantidadAux.ToString("C2"))
        Rep.SetParameterValue("Encabezado", GlobalNombreEmpresa)
        Dim CL As New CLetras
        Rep.SetParameterValue("cantidadLetra", CL.LetrasM(cantidad1, 2, GlobalIdiomaLetras))

        If em.Pagado = False Then
            Rep.SetParameterValue("totalEmpenio", em.TotalAux.ToString("C2"))
            CalculaRefrendo(idEmpenio, em.fechaContrato, "", em.tipoEmpenio)
            pago = EP.buscarPagos(idEmpenio, em.fechaContrato)
            Rep.SetParameterValue("totalRefrendo", (refrendo - pago).ToString("C2"))


            Rep.SetParameterValue("totalTotales", ((Double.Parse(em.TotalAux) + refrendo) - pago).ToString("C2"))
        Else
            'If soloInteres = False And refrendoG > 0 Then
            'Rep.SetParameterValue("totalRefrendo", Double.Parse(refrendoG.ToString("c2")))
            ' Rep.SetParameterValue("totalTotales", ((Double.Parse(em.TotalAux) + refrendo) - pago).ToString("C2"))
            'Else
            Rep.SetParameterValue("totalEmpenio", "$0.00")
            Rep.SetParameterValue("totalRefrendo", "$0.00")
            Rep.SetParameterValue("totalTotales", "$0.00")
            ' End If

        End If
        Rep.PrintOptions.PrinterName = My.Settings.impresoraempenios
        Rep.PrintToPrinter(1, False, 0, 0)

        ' Dim RV As New frmReportes(Rep, False)
        ' RV.Show()

    End Sub
    Public Function Num2Text(ByVal value As Double) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (dgvTodosPagos.CurrentCell Is Nothing) = False Then
            Dim F As New frmEmpenios(dgvTodosPagos.Item(0, dgvTodosPagos.CurrentCell.RowIndex).Value, 0, 0, 0, "", -1, False)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub txtCantidadPago_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidadPago.TextChanged
        If ckDescuento.Checked Then
            If IsNumeric(txtCantidadPago.Text) Then
                txtDescuento.Text = ((Double.Parse(refrendoG + TotalAux)) - txtCantidadPago.Text).ToString("0.00")
            End If
        End If
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub dgvPagosRealizados_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPagosRealizados.CellDoubleClick
        idAbono = dgvPagosRealizados.Item(0, dgvPagosRealizados.CurrentCell.RowIndex).Value
        EP.llenaDatos(idAbono)
        dtpFechaPago.Value = EP.Fecha
        fechaAbono = EP.Fecha
        txtCantidadPago.Text = EP.Cantidad
        ' AuxCantidad = EP.Cantidad
        btnEliminarConcepto.Enabled = True
        btnGuardarConcepto.Text = "Modificar"
        If EP.Estado = True Then
            activo = False
        Else
            activo = True
        End If
        If lblRestante.Text = "$0.00" Then
            GroupBox2.Enabled = False


        Else
            If EP.Estado = 1 Then
                GroupBox2.Enabled = False
            Else

                GroupBox2.Enabled = True
            End If

        End If
        fechaAux = dtpFechaPago.Value.ToString("yyyy/MM/dd")
        cantidadAux = Double.Parse(txtCantidadPago.Text)
        ImprimirRecibo()
    End Sub

   
    Private Sub MovCajaAdd()
       
        Dim Ca As New dbCajas(MySqlcon)
      
        Ca.MovimientodeCaja(IdsCajas.Valor(cmbCaja.SelectedIndex), Double.Parse(txtCantidadPago.Text))
    End Sub
    Private Sub MovCajaMod()
        Dim diferencia As Double = 0
        If ckDescuento.Checked = True Then
            diferencia = AuxCantidad * (-1)
        Else
            diferencia = Double.Parse(txtCantidadPago.Text) - AuxCantidad
        End If

      
        Dim Ca As New dbCajas(MySqlcon)
        Ca.MovimientodeCaja(IdsCajas.Valor(cmbCaja.SelectedIndex), diferencia)
    End Sub
    Private Sub MovCajaElim()
     
        Dim Ca As New dbCajas(MySqlcon)
        Ca.MovimientodeCaja(IdsCajas.Valor(cmbCaja.SelectedIndex), AuxCantidad * -1)
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub ckDescuento_CheckedChanged(sender As Object, e As EventArgs) Handles ckDescuento.CheckedChanged
        If ckDescuento.Checked = True Then
            txtDescuento.Enabled = True
            lblDescuento.Enabled = True
            txtDescuento.Text = ((Double.Parse(refrendoG + TotalAux)) - txtCantidadPago.Text).ToString("0.00")
        Else
            txtDescuento.Enabled = False
            lblDescuento.Enabled = False
            txtDescuento.Text = "0.00"
        End If
    End Sub
End Class