Public Class frmVentasReportesN
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim IdsSucursales As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsVendedoresZona As New elemento
    Dim IdsVendedoresZonaC As New elemento
    Dim IdsMonedas As New elemento
    Dim IdInventario As Integer
    Dim IdVariante As Integer
    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean
    Dim EncontroClas As Boolean = True
    Dim IdsConceptos As New elemento
    Dim IdsFormas As New elemento
    Dim llenando As Boolean
    Dim IdsAlmacenes As New elemento
    Dim IdsCajas As New elemento
    Dim IdsTipos As New elemento
    Dim IdsTiposSuc As New elemento
    Dim idTipo As Integer = -1
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmVentasReportesN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor, reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button1.Enabled = True
            Exit Sub
        End If
        ComboBox11.Items.Add("Facturas")
        ComboBox11.Items.Add("Remisiones")
        ComboBox11.Items.Add("Otros")
        ComboBox8.Items.Add("20")
        ComboBox8.Items.Add("30")
        ComboBox8.Items.Add("50")
        ComboBox8.Items.Add("100")
        ComboBox8.SelectedIndex = 0
        ComboBox10.Items.Add("Todos")
        ComboBox10.Items.Add("Por surtir")
        ComboBox10.SelectedIndex = 1
        ComboBox9.Items.Add("Cantidad")
        ComboBox9.Items.Add("Importe")
        ComboBox9.SelectedIndex = 0

        ComboBox11.SelectedIndex = 0
        llenando = True
        ConsultaOn = False
        ComboBox5.Items.Add("Pesos")
        ComboBox5.Items.Add("Moneda Original")
        ComboBox5.SelectedIndex = 0
        ComboBox16.Items.Add("Solo sin surtir")
        ComboBox16.Items.Add("Solo surtidos")
        ComboBox16.Items.Add("Con o sin surtir")
        ComboBox16.SelectedIndex = 0
        ComboBox17.Items.Add("Todos")
        ComboBox17.Items.Add("Crédito")
        ComboBox17.Items.Add("Contado")
        ComboBox17.SelectedIndex = 0
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        LlenaCombos("tblsucursalestipos", ComboBox15, "nombre", "nombrec", "idtipo", IdsTiposSuc, , "Todos")
        LlenaCombos("tblmonedas", ComboBox4, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1", "Todas")
        ComboBox1.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas", "nombre")
        LlenaCombos("tblvendedores", ComboBox2, "nombre", "nombret", "idvendedor", IdsVendedores, , "Todos")

        LlenaCombos("tblzona", cmbZona, "zona", "zonat", "idZona", IdsVendedoresZona, , "Todos")
        LlenaCombos("tblzona", cmbZonacliente, "zona", "zonat", "idZona", IdsVendedoresZonaC, , "Todos")
        LlenaCombos("tblclientestipos", comboTipo, "nombre", "nombret", "idtipo", IdsTipos, , "Todos")
        ConsultaOn = True
        llenando = False
        cmbIpoGrafica.SelectedIndex = 0
    End Sub
    Private Sub LlenaLista()
        ListBox1.Items.Clear()
        If ComboBox11.Text = "Facturas" Then
            LlenaCombos("tblformasdepago", ComboBox12, "nombre", "nombret", "idforma", IdsFormas, , "Todas")
            ListBox1.Items.Add("Ventas.")
            ListBox1.Items.Add("Ventas tipo B.")
            ListBox1.Items.Add("Ventas Resumido.")
            ListBox1.Items.Add("Ventas con detalle de IVA.")
            ListBox1.Items.Add("Ventas por tasa de IEPS.")
            ListBox1.Items.Add("Ventas por método de pago A.")
            ListBox1.Items.Add("Ventas por método de pago B.")
            ListBox1.Items.Add("Ventas por artículo.")
            ListBox1.Items.Add("Ventas por artículo concentrado.")
            ListBox1.Items.Add("Ventas por artículo concentrado B.")
            ListBox1.Items.Add("Ventas por artículo concentrado por vendedor.")
            ListBox1.Items.Add("Ventas por clasificación.")
            ListBox1.Items.Add("Ventas por clasificación concentrado.")
            ListBox1.Items.Add("Ventas por vendedor.")
            ListBox1.Items.Add("Ventas los más vendidos.")
            ListBox1.Items.Add("Ventas con detalles por cliente.")
            ListBox1.Items.Add("Ventas Canceladas.")
            ListBox1.Items.Add("Ventas clientes concentrado.")
            ListBox1.Items.Add("Ventas por clasificación y método de pago.")
            ListBox1.Items.Add("Utilidad de venta.")
            ListBox1.Items.Add("Utilidad de venta por artículo.")
            ListBox1.Items.Add("Utilidad de venta por artículo concentrado.")
            ListBox1.Items.Add("Utilidad de venta por clasificación.")
            ListBox1.Items.Add("Antigüedad de saldos.")
            ListBox1.Items.Add("Antigüedad de saldos extendido.")
            'ListBox1.Items.Add("Antigüedad de saldos a fecha.")
            ListBox1.Items.Add("Cobranza.")
            ListBox1.Items.Add("Analítica de clientes.")

            ListBox1.Items.Add("Saldos.")
            ListBox1.Items.Add("Ventas por surtir.")
            ListBox1.Items.Add("Clientes - Saldo a Favor.")
            ListBox1.Items.Add("Ventas articulos concentrado por peso.")
            ListBox1.Items.Add("Ventas de artículo por cliente.")

        End If
        If ComboBox11.Text = "Remisiones" Then
            LlenaCombos("tblformasdepagoremisiones", ComboBox12, "nombre", "nombret", "idforma", IdsFormas, , "Todas")
            ListBox1.Items.Add("Remisiones.")
            ListBox1.Items.Add("Remisiones tipo B.")
            ListBox1.Items.Add("Remisiones Resumido.")
            ListBox1.Items.Add("Remisiones por método de pago.")
            ListBox1.Items.Add("Remisiones por tasa de IEPS.")
            ListBox1.Items.Add("Remisiones por artículo.")
            ListBox1.Items.Add("Remisiones por artículo concentrado.")
            ListBox1.Items.Add("Remisiones por artículo concentrado B.")
            ListBox1.Items.Add("Remisiones por artículo concentrado por vendedor.")
            ListBox1.Items.Add("Remisiones por clasificación.")
            ListBox1.Items.Add("Remisiones por clasificación concentrado.")
            ListBox1.Items.Add("Remisiones por vendedor.")
            ListBox1.Items.Add("Remisiones los más vendidos.")
            ListBox1.Items.Add("Remisiones con detalles por cliente.")
            ListBox1.Items.Add("Remisiones Canceladas.")
            ListBox1.Items.Add("Remisiones por clasificación y método de pago.")
            ListBox1.Items.Add("Utilidad de venta por artículo.")
            ListBox1.Items.Add("Utilidad de venta por artículo concentrado.")
            ListBox1.Items.Add("Utilidad de venta por clasificación.")
            ListBox1.Items.Add("Cobranza Remisiones.")
            ListBox1.Items.Add("Antigüedad de saldos remisiones.")
            ListBox1.Items.Add("Analítica de clientes remisiones.")
            ListBox1.Items.Add("Saldos.")
            ListBox1.Items.Add("Ventas por surtir.")
            ListBox1.Items.Add("Clientes - Saldo a Favor.")
            ListBox1.Items.Add("Remisiones articulos concentrado por peso.")
            ListBox1.Items.Add("Remisiones por artículo por cliente.")
            ListBox1.Items.Add("Remisiones clientes concentrado.")
        End If
        If ComboBox11.Text = "Otros" Then
            ListBox1.Items.Add("Lista de Precios.")
            ListBox1.Items.Add("Comparativo mensual facturas.")
            ListBox1.Items.Add("Comparativo por ejercicio facturas.")
            ListBox1.Items.Add("Comparativo mensual remisiones.")
            ListBox1.Items.Add("Comparativo por ejercicio remisiones.")
            ListBox1.Items.Add("Apartados.")
            ListBox1.Items.Add("Apartados por artículo.")
            ListBox1.Items.Add("Apartados por artículo concentrado.")
            ListBox1.Items.Add("Apartados Saldos.")
            ListBox1.Items.Add("Documentos de clientes.")
            ListBox1.Items.Add("Ventas facturas lotes.")
            ListBox1.Items.Add("Ventas facturas importaciones.")
            ListBox1.Items.Add("Ventas remisiones lotes.")
            ListBox1.Items.Add("Ventas remisiones importaciones.")
            ListBox1.Items.Add("Notas de crédito.")
            ListBox1.Items.Add("Notas de cargo.")
            ListBox1.Items.Add("Devoluciones.")
            ListBox1.Items.Add("Cotizaciones.")
            ListBox1.Items.Add("Cotizaciones por artículo.")
            ListBox1.Items.Add("Pedidos.")
            ListBox1.Items.Add("Pedidos concentrado por cliente.")
            ListBox1.Items.Add("Pedidos concentrado por artículo.")
            ListBox1.Items.Add("Catálogo de clientes.")
            ListBox1.Items.Add("Directorio de clientes.")

        End If

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            Dim V As New dbVentas(MySqlcon)
            'Dim O As New dbOpciones(MySqlcon)
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClas3 As Integer
            Dim idClas4 As Integer
            Dim idClas5 As Integer


            Dim TipoCosteo As Byte
            If cmbZonacliente.SelectedIndex <= 0 Then
                idClas5 = 0
            Else
                idClas5 = IdsVendedoresZonaC.Valor(cmbZonacliente.SelectedIndex)
            End If

            If cmbZona.SelectedIndex <= 0 Then
                idClas4 = 0
            Else
                idClas4 = IdsVendedoresZona.Valor(cmbZona.SelectedIndex)
            End If
            If ComboBox3.SelectedIndex <= 0 Then
                idClas = 0
            Else
                idClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            End If
            If ComboBox6.SelectedIndex <= 0 Then
                idClas2 = 0
            Else
                idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
            End If
            If ComboBox7.SelectedIndex <= 0 Then
                idClas3 = 0
            Else
                idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
            End If
            If RadioButton1.Checked Then TipoCosteo = 1
            If RadioButton2.Checked Then TipoCosteo = 2
            'If RadioButton3.Checked Then TipoCosteo = 3
            'ListBox1.Items.Add("Ventas.")
            'ListBox1.Items.Add("Ventas por tipo de pago.")
            'ListBox1.Items.Add("Ventas por artículo.")
            'ListBox1.Items.Add("Utilidad de venta.")
            'ListBox1.Items.Add("Utilidada de venta por artículo.")
            'ListBox1.Items.Add("Cobranza.")
            'ListBox1.Items.Add("Saldos.")
            'ListBox1.Items.Add("Antigüedad de saldos.")
            'ListBox1.Items.Add("Antigüedad de saldos a fecha.")
            'ListBox1.Items.Add("Documentos de clientes.")
            'ListBox1.Items.Add("Notas de crédito.")
            'ListBox1.Items.Add("Notas de cargo.")
            'ListBox1.Items.Add("Devoluciones.")
            'ListBox1.Items.Add("Remisiones.")
            'ListBox1.Items.Add("Remisiones por artículo.")
            'ListBox1.Items.Add("Cotizaciones.")
            'ListBox1.Items.Add("Pedidos.")
            'ListBox1.Items.Add("Catálogo de clientes.")
            'ListBox1.Items.Add("Direcctorio de clientes.")
            Select Case ListBox1.Text
                Case "Ventas."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    Rep = New repVentasNormal
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5, False, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Dim s1 As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    If idTipo > 0 Then
                        Dim t As New dbTiposCP(idTipo, MySqlcon, 0)
                        Rep.SetParameterValue("tipoClientes", t.Nombre)
                    Else
                        Rep.SetParameterValue("tipoClientes", "Todos")
                    End If
                    Dim suc As Integer = IdsSucursales.Valor(ComboBox1.SelectedIndex)
                    If suc > 0 Then
                        Dim sucursal As New dbSucursales(suc, MySqlcon)
                        Rep.SetParameterValue("sucursal", GlobalNombreEmpresa)
                    Else
                        Rep.SetParameterValue("sucursal", "Todas")
                    End If
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Comparativo mensual facturas."
                    Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rep = New repVentasPorMes
                    Dim ve As New dbVentas(MySqlcon)
                    Dim desde As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
                    desde = desde.Split("/")(0) + "/01/01"
                    Dim hasta As String = desde.Split("/")(0) + "/12/31"
                    rep.SetDataSource(ve.reporteComparativo(desde, hasta, ComboBox5.SelectedIndex, CheckBox14.Checked, True, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsVendedores.Valor(ComboBox2.SelectedIndex), ComboBox17.SelectedIndex))
                    rep.SetParameterValue("sucursal", "Reporte de ventas comparativo mensual.")
                    rep.SetParameterValue("tipo", "Facturas " + DateTimePicker1.Value.Year.ToString + ".")
                    rep.SetParameterValue("encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    rep.SetParameterValue("filtro", Filtros)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Comparativo mensual remisiones."
                    Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rep = New repVentasPorMes
                    'Dim ve As New dbVentas(MySqlcon)
                    Dim r As New dbVentasRemisiones(MySqlcon)
                    Dim desde As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
                    desde = desde.Split("/")(0) + "/01/01"
                    Dim hasta As String = desde.Split("/")(0) + "/12/31"
                    rep.SetDataSource(r.reporteComparativo(desde, hasta, ComboBox5.SelectedIndex, CheckBox14.Checked, True, CheckBox6.Checked, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), IdsVendedores.Valor(ComboBox2.SelectedIndex), ComboBox17.SelectedIndex))
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr

                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    rep.SetParameterValue("filtro", filtros)
                    rep.SetParameterValue("sucursal", "Reporte de ventas comparativo mensual.")
                    rep.SetParameterValue("tipo", "Remisiones " + DateTimePicker1.Value.Year.ToString + ".")
                    rep.SetParameterValue("encabezado", S.NombreFiscal)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Comparativo por ejercicio facturas."
                    Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rep = New repVentasComparativo
                    Dim actual As Integer = DateTimePicker1.Value.Year
                    Dim anterior As Integer = actual - 1
                    Dim ve As New dbVentas(MySqlcon)
                    'Dim r As New dbVentasRemisiones(MySqlcon)
                    've.comparativoMes(actual.ToString(), anterior.ToString())
                    rep.SetDataSource(ve.comparativoMes(actual.ToString(), anterior.ToString(), ComboBox5.SelectedIndex, CheckBox14.Checked, True, ComboBox17.SelectedIndex))
                    Dim filtros As String = ""
                    filtros = "Sucursal: " + ComboBox1.Text
                    If ComboBox5.SelectedIndex > 0 Then
                        filtros += " Mostrado en: " + ComboBox5.Text
                    End If
                    If CheckBox14.Checked Then
                        filtros += " Solo Canceladas "
                    End If
                    filtros += " Forma de pago: " + ComboBox17.Text
                    rep.SetParameterValue("filtro", filtros)
                    rep.SetParameterValue("titulo", "Reporte de ventas comparativo por ejercicio.")
                    rep.SetParameterValue("tipo", "Facturas.")
                    rep.SetParameterValue("anho", actual)
                    rep.SetParameterValue("encabezado", S.NombreFiscal)
                    'Console.WriteLine(DateTimePicker1.Value.Month.ToString())
                    rep.SetParameterValue("mes", DateTimePicker1.Value.Month.ToString())
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Comparativo por ejercicio remisiones."
                    Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rep = New repVentasComparativo
                    Dim actual As Integer = DateTimePicker1.Value.Year
                    Dim anterior As Integer = actual - 1
                    'Dim ve As New dbVentas(MySqlcon)
                    Dim r As New dbVentasRemisiones(MySqlcon)
                    've.comparativoMes(actual.ToString(), anterior.ToString())
                    rep.SetDataSource(r.comparativoMes(actual.ToString(), anterior.ToString(), ComboBox5.SelectedIndex, CheckBox14.Checked, True, CheckBox6.Checked, ComboBox17.SelectedIndex))
                    Dim filtros As String = ""
                    filtros = "Sucursal: " + ComboBox1.Text
                    If ComboBox5.SelectedIndex > 0 Then
                        filtros += " Mostrado en: " + ComboBox5.Text
                    End If
                    If CheckBox6.Checked Then
                        filtros += " Sin Remisiones Facturadas "
                    End If
                    If CheckBox14.Checked Then
                        filtros += " *Solo Canceladas*"
                    End If
                    filtros += " Forma de pago: " + ComboBox17.Text
                    rep.SetParameterValue("filtro", filtros)
                    rep.SetParameterValue("titulo", "Reporte de ventas comparativo por ejercicio.")
                    rep.SetParameterValue("tipo", "Remisiones.")
                    rep.SetParameterValue("anho", actual)
                    rep.SetParameterValue("encabezado", S.NombreFiscal)
                    rep.SetParameterValue("mes", DateTimePicker1.Value.Month.ToString())
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Ventas clientes concentrado."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    Rep = New repVentasClientesConcentrado
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5, False, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, True, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Dim s1 As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al  " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + "  Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += "  Vendedor: " + ComboBox2.Text
                    End If
                    If ComboBox4.SelectedIndex > 0 Then
                        Filtros += "  Moneda: " + ComboBox4.Text
                    End If
                    If ComboBox5.SelectedIndex > 0 Then
                        Filtros += "  Mostrado en: " + ComboBox5.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += "  Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += "  *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    If idTipo > 0 Then
                        Dim t As New dbTiposCP(idTipo, MySqlcon, 0)
                        Rep.SetParameterValue("tipoClientes", t.Nombre)
                    Else
                        Rep.SetParameterValue("tipoClientes", "Todos")
                    End If
                    Dim suc As Integer = IdsSucursales.Valor(ComboBox1.SelectedIndex)
                    If suc > 0 Then
                        Dim sucursal As New dbSucursales(suc, MySqlcon)
                        Rep.SetParameterValue("sucursal", GlobalNombreEmpresa)
                    Else
                        Rep.SetParameterValue("sucursal", "Todas")
                    End If
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas Canceladas."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    Rep = New repVentasNormalCanceladas
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteCanceladas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5, False, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, CheckBox11.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox11.Checked = True Then
                        Filtros += "  Con canceladas en el periodo"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    If idTipo > 0 Then
                        Dim t As New dbTiposCP(idTipo, MySqlcon, 0)
                        Rep.SetParameterValue("tipoClientes", GlobalNombreEmpresa)
                    Else
                        Rep.SetParameterValue("tipoClientes", "Todos")
                    End If
                    Dim suc As Integer = IdsSucursales.Valor(ComboBox1.SelectedIndex)
                    If suc > 0 Then
                        Dim sucursal As New dbSucursales(suc, MySqlcon)
                        Rep.SetParameterValue("sucursal", GlobalNombreEmpresa)
                    Else
                        Rep.SetParameterValue("sucursal", "Todas")
                    End If
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()

                Case "Ventas tipo B."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasIvasB
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, False, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr + " Vendedor: " + ComboBox2.Text
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If idTipo > 0 Then
                        Dim t As New dbTiposCP(idTipo, MySqlcon, 0)
                        Filtros += " Tipo Clientes: " + t.Nombre
                    Else
                        Filtros += " Tipo Clientes: Todos"
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    If ComboBox11.Text = "Facturas" Then
                        Rep.SetParameterValue("tipo", "Facturación")
                    Else
                        Rep.SetParameterValue("tipo", "Remisiones")
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Dim suc = IdsSucursales.Valor(ComboBox1.SelectedIndex)
                    If suc > 0 Then
                        Dim sucursal As New dbSucursales(suc, MySqlcon)
                        Rep.SetParameterValue("sucursal", GlobalNombreEmpresa)
                    Else
                        Rep.SetParameterValue("sucursal", "Todas")
                    End If
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones tipo B."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Dim R As New dbVentasRemisiones(MySqlcon)
                    Rep = New repVentasIvasB
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(R.ReporteVentastipoB(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, False, idClas4, CheckBox14.Checked, Oc.OcultarOc, Oc.SeriesOc, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Tipo Clientes: " + comboTipo.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    If ComboBox11.Text = "Facturas" Then
                        Rep.SetParameterValue("tipo", "Facturación")
                    Else
                        Rep.SetParameterValue("tipo", "Remisiones")
                    End If
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("tipoCliente", comboTipo.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas con detalle de IVA."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    Rep = New repVentasIvas
                    Rep.SetDataSource(V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5, False, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), True, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por tasa de IEPS."

                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    'If CheckBox10.Checked = False Then
                    Rep = New repVentasArticulosIEPS
                    'Else
                    'Rep = New repVentasArticulosEscritura
                    'End If
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), True, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox10.Checked Then
                        Filtros += " Con escritura por descripción"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()

                    'Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    ''V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    'Rep = New repVentasIEPS
                    'Rep.SetDataSource(V.ReporteIEPS(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5, False, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), True))
                    'Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    'Dim Filtros As String
                    'Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    'If IdCliente = 0 Then
                    '    Filtros += " Cliente: Todos"
                    'Else
                    '    Filtros += " Cliente: " + TextBox2.Text
                    'End If
                    'If TextBox4.Text = "" Then
                    '    Filtros += " Artículo: Todos"
                    'Else
                    '    Filtros += " Artículo: " + TextBox4.Text
                    'End If
                    'If CheckBox14.Checked Then
                    '    Filtros += " *SOLO CANCELADAS*"
                    'End If
                    'Rep.SetParameterValue("Filtros", Filtros)
                    'Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    'Rep.SetParameterValue("subtitulo", "Reporte de facturas por tasa de IEPS")
                    'Dim RV As New frmReportes(Rep, False)
                    'RV.Show()
                Case "Remisiones por tasa de IEPS."

                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisionesIEPS
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, False, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, False, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), True, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()

                    
                Case "Ventas Resumido."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    Rep = New repVentasResumen
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Console.Write(ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por método de pago A."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasTipoPago
                    Rep.SetDataSource(V.ReportePorTipodePagoN(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas4, idClas5, CheckBox14.Checked, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, 0, 0, 0, 0, False, False, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    ' Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por clasificación y método de pago."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasTipoPagoClas
                    Rep.SetDataSource(V.ReportePorTipodePagoN(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas4, idClas5, CheckBox14.Checked, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, IdInventario, idClas, idClas2, idClas3, True, False, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + "  Método de pago: " + ComboBox12.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    ' Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por método de pago B."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasTipoPagoB
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReportePorTipodePagoN(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas4, idClas5, CheckBox14.Checked, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, 0, 0, 0, 0, False, True, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.Subreports.Item(0).SetDataSource(V.ReportePorTipodePagoN(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas4, idClas5, CheckBox14.Checked, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, 0, 0, 0, 0, False, True, True, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + "  Método de pago: " + ComboBox12.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por artículo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    If CheckBox10.Checked = False Then
                        Rep = New repVentasArticulos
                    Else
                        Rep = New repVentasArticulosEscritura
                    End If
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If CheckBox10.Checked Then
                        Filtros += " Con escritura por descripción"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas con detalles por cliente."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

                    Rep = New repVentasDetallesCliente
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulosDetalle(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("Titulo", "Facturas con detalles por cliente.")
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones con detalles por cliente."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim Remi As New dbVentasRemisiones(MySqlcon)
                    Rep = New repVentasDetallesCliente
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(Remi.ReporteVentasArticulosDetalle(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Titulo", "Remisiones con detalles por cliente.")
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por artículo concentrado."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosC
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, True, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " *Solo no facturadas*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por surtir."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasporSurtir
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasPorSurtir(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, True, TextBox5.Text, ComboBox10.SelectedIndex, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr

                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " *Solo no facturadas*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por surtir detallado."
                    V.ReporteVentasPorSurtirDetallado(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, True, TextBox5.Text, ComboBox10.SelectedIndex, IdsAlmacenes.Valor(ComboBox13.SelectedIndex))
                    'Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    ''V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    'Rep = New repVentasporSurtir
                    'Rep.SetDataSource(V.ReporteVentasPorSurtir(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, True, TextBox5.Text, ComboBox10.SelectedIndex))
                    'Rep.SetParameterValue("Encabezado", S.Nombre)
                    'Dim Filtros As String = ""
                    'If ComboBox10.SelectedIndex = 0 Then
                    '    Filtros += "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
                    'End If
                    'If IdCliente = 0 Then
                    '    Filtros += " Cliente: Todos"
                    'Else
                    '    Filtros += " Cliente: " + TextBox2.Text
                    'End If
                    'If CheckBox14.Checked Then
                    '    Filtros += " SOLO CANCELADAS"
                    'End If
                    'Filtros += " Vendedor: " + ComboBox2.Text
                    'Rep.SetParameterValue("Filtros", Filtros)
                    'Dim RV As New frmReportes(Rep, False)
                    'RV.Show()
                Case "Ventas por artículo concentrado B."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosCB
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, True, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If CheckBox6.Checked Then
                        Filtros += " *Solo no facturadas*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por artículo concentrado por vendedor."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosCBxVendedor
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, True, TextBox5.Text, True, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("conDetalle", checkLinea.Checked)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " *Solo no facturadas*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por clasificación."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.ReporteVentasArticulosClas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text)
                    Rep = New repVentasArticulosClases
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulosClas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " *Solo no facturadas*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por clasificación concentrado."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.ReporteVentasArticulosClas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text)
                    Rep = New repVentasArticulosClasesCon
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulosClas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas por vendedor."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    Rep = New repVentasVendedor
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReportexVendedor(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("condetalles", checkLinea.Checked)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Ventas los más vendidos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repVentaslosmas
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulosMasVendidos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, False, ComboBox8.Text, ComboBox9.SelectedIndex, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr + " Vendedor: " + ComboBox2.Text
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Tipo Clientes: " + comboTipo.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("Tipo", "Ventas los más vendidos")
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Utilidad de venta."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim coniva As Byte
                    If CheckBox7.Checked Then
                        coniva = 1
                    Else
                        coniva = 0
                    End If
                    Rep = New repVentasUtilidadN
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    If coniva = 1 Then
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " CON IVA")
                    Else
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " SIN IVA")
                    End If

                    Rep.SetParameterValue("coniva", coniva)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Tipo", 1)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Utilidad de venta por artículo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim coniva As Byte
                    If CheckBox7.Checked Then
                        coniva = 1
                    Else
                        coniva = 0
                    End If
                    Rep = New repVentasUtilidadN
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    If ComboBox11.Text = "Facturas" Then
                        Rep.SetDataSource(V.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                        'Rep.SetParameterValue("Encabezado", "Reporte de utilidad de venta (Facturas)")
                        Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    End If
                    If ComboBox11.Text = "Remisiones" Then
                        Dim VR As New dbVentasRemisiones(MySqlcon)
                        Dim Oc As New dbOpcionesOc(MySqlcon)
                        Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                        Rep.SetDataSource(VR.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, Oc.OcultarOc, Oc.SeriesOc, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                        'Rep.SetParameterValue("Encabezado", "Reporte de utilidad de venta (Remisiones)")
                        Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    End If
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    If coniva = 1 Then
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " CON IVA")
                    Else
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " SIN IVA")
                    End If
                    Rep.SetParameterValue("coniva", coniva)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Tipo", 0)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Utilidad de venta por clasificación."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim coniva As Byte
                    If CheckBox7.Checked Then
                        coniva = 1
                    Else
                        coniva = 0
                    End If
                    Rep = New repVentasUtilidadN
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    If ComboBox11.Text = "Facturas" Then
                        Rep.SetDataSource(V.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, True, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                        'Rep.SetParameterValue("Encabezado", "Reporte de utilidad de venta (Facturas)")
                        Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    End If
                    If ComboBox11.Text = "Remisiones" Then
                        Dim VR As New dbVentasRemisiones(MySqlcon)
                        Dim Oc As New dbOpcionesOc(MySqlcon)
                        Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                        Rep.SetDataSource(VR.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, Oc.OcultarOc, Oc.SeriesOc, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, True, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                        'Rep.SetParameterValue("Encabezado", "Reporte de utilidad de venta (Remisiones)")
                        Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    End If
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    If coniva = 1 Then
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " CON IVA")
                    Else
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " SIN IVA")
                    End If
                    Rep.SetParameterValue("coniva", coniva)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Tipo", 3)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Utilidad de venta por artículo concentrado."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim coniva As Byte
                    If CheckBox7.Checked Then
                        coniva = 1
                    Else
                        coniva = 0
                    End If
                    Rep = New repVentasUtilidadNConcentrado
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    If ComboBox11.Text = "Facturas" Then
                        Rep.SetDataSource(V.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))

                        Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                        If coniva = 1 Then
                            Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " CON IVA")
                        Else
                            Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " SIN IVA")
                        End If
                        Dim Filtros As String
                        Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                        If IdCliente = 0 Then
                            Filtros += " Cliente: Todos"
                        Else
                            Filtros += " Cliente: " + TextBox2.Text
                        End If
                        If TextBox4.Text = "" Then
                            Filtros += " Artículo: Todos"
                        Else
                            Filtros += " Artículo: " + TextBox4.Text
                        End If
                        If CheckBox14.Checked Then
                            Filtros += " *SOLO CANCELADAS*"
                        End If
                        'Filtros += " Sucursal: " + ComboBox1.Text
                        Rep.SetParameterValue("coniva", coniva)
                        Rep.SetParameterValue("Tipo", 0)
                        Rep.SetParameterValue("Filtros", Filtros)
                        Rep.SetParameterValue("titulo", "Reporte de utilidad de venta (Facturas)")
                    Else
                        Dim VR As New dbVentasRemisiones(MySqlcon)
                        Dim Oc As New dbOpcionesOc(MySqlcon)
                        Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                        Rep.SetDataSource(VR.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, Oc.OcultarOc, Oc.SeriesOc, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                        Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                        If coniva = 1 Then
                            Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " CON IVA")
                        Else
                            Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " SIN IVA")
                        End If
                        Rep.SetParameterValue("coniva", coniva)
                        Rep.SetParameterValue("Tipo", 0)
                        Dim Filtros As String
                        Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                        If IdCliente = 0 Then
                            Filtros += " Cliente: Todos"
                        Else
                            Filtros += " Cliente: " + TextBox2.Text
                        End If
                        If TextBox4.Text = "" Then
                            Filtros += " Artículo: Todos"
                        Else
                            Filtros += " Artículo: " + TextBox4.Text
                        End If
                        If CheckBox14.Checked Then
                            Filtros += " *SOLO CANCELADAS*"
                        End If
                        Rep.SetParameterValue("titulo", "Reporte de utilidad de venta (Remisiones)")
                        Rep.SetParameterValue("Filtros", Filtros)
                    End If
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Cobranza."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Dim VP As New dbVentasPagos(MySqlcon)
                    Rep = New repVentasPagos
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VP.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsConceptos.Valor(cmbConcepto.SelectedIndex), IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, idClas5, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("idsucursal", IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("titulo", "Reporte de Cobranza")
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Cobranza Remisiones."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Dim VP As New dbVentasPagosRemisiones(MySqlcon)
                    Rep = New repVentasPagos
                    Rep.SetDataSource(VP.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsConceptos.Valor(cmbConcepto.SelectedIndex), IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, idClas5, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("idsucursal", IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox8.Checked = True Then
                        Filtros += "Contado agregado"
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Rep.SetParameterValue("titulo", "Reporte de Cobranza Remisiones")
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Saldos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    '                V.ReporteClientesDeudas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex))
                    Rep = New repVentasClientesDeudas
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteClientesDeudas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, ComboBox11.SelectedIndex, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas5, idTipo))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    If ComboBox11.SelectedIndex = 0 Then
                        Rep.SetParameterValue("titulo", "Reporte de Saldos")
                    Else
                        Rep.SetParameterValue("titulo", "Reporte de Saldos de Remisiones")
                    End If
                    Dim suc As Integer = IdsSucursales.Valor(ComboBox1.SelectedIndex)
                    If suc < 0 Then
                        Rep.SetParameterValue("sucursal", "Todas")
                    Else
                        Dim su As New dbSucursales(suc, MySqlcon)
                        Rep.SetParameterValue("sucursal", su.Nombre)
                    End If
                    Dim Filtros As String
                    Filtros = "Fechas:  " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr + " Vendedor: " + ComboBox2.Text
                    Filtros += " Tipo Clientes: " + comboTipo.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Apartados Saldos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    '                V.ReporteClientesDeudas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex))
                    Rep = New repVentasClientesDeudas
                    'Dim VA As New dbVentasApartados(MySqlcon)
                    Rep.SetDataSource(V.ReporteClientesDeudas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, 2, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas5, idTipo))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    'If ComboBox11.SelectedIndex = 0 Then
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    'Else
                    Rep.SetParameterValue("titulo", "Reporte de Saldos de Apartados")
                    'End If
                    Dim Filtros As String
                    Filtros = "Fechas:  " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr + " Vendedor: " + ComboBox2.Text
                    Filtros += " Tipo Clientes: " + comboTipo.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Antigüedad de saldos."
                    'Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Dim TP As Double
                    Dim CM As New dbMonedasConversiones(1, MySqlcon)
                    TP = CM.Cantidad.ToString

                    'Rep = New repVentasViejosSaldos
                    'Rep.SetDataSource()
                    'Rep.SetParameterValue("Encabezado", S.Nombre)
                    'Dim RV As New frmReportes(Rep, False)
                    'RV.Show()
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    '    'V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repVentasViejosSaldosH
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    '    'Rep.SetDataSource(V.ReporteViejosSaldosHN(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd")))
                    If Format(DateTimePicker1.Value, "yyyyMMdd") = Format(Date.Now, "yyyyMMdd") Then
                        Rep.SetDataSource(V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TP, idClas4, idTipo, idClas5))
                    Else
                        Rep.SetDataSource(V.ReporteViejosSaldosH(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TP, idTipo, idClas5))
                    End If
                    Dim Filtros As String
                    Filtros = "Sucursal: " + ComboBox1.Text + " Vendedor: " + ComboBox2.Text + vbCr + " Mostrado en: " + ComboBox5.Text
                    Filtros += " Tipo Clientes: " + comboTipo.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("fecha", Format(DateTimePicker1.Value, "yyyy/MM/dd"))
                    Rep.SetParameterValue("titulo", "Reporte de Antigüedad de Saldos")
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                    'Case "Antigüedad de saldos a fecha."
                    '    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    '    'V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    '    Rep = New repVentasViejosSaldosH
                    '    'Rep.SetDataSource(V.ReporteViejosSaldosHN(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd")))
                    '    Rep.SetParameterValue("Encabezado", S.Nombre)
                    '    Rep.SetParameterValue("fecha", Format(DateTimePicker1.Value, "yyyy/MM/dd"))
                    '    Dim RV As New frmReportes(Rep, False)
                    '    RV.Show()
                Case "Antigüedad de saldos extendido."
                    Dim TP As Double
                    Dim CM As New dbMonedasConversiones(1, MySqlcon)
                    TP = CM.Cantidad.ToString
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repVentasViejosSaldosHEx
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    If Format(DateTimePicker2.Value, "yyyyMMdd") = Format(Date.Now, "yyyyMMdd") Then
                        Rep.SetDataSource(V.ReporteViejosSaldosEx(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TP, idClas4, idTipo, idClas5))
                    Else
                        Rep.SetDataSource(V.ReporteViejosSaldosHEx(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TP, idTipo, idClas5))
                    End If
                    Dim Filtros As String
                    Filtros = "Sucursal: " + ComboBox1.Text + " Vendedor: " + ComboBox2.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Filtros += " Moneda: " + ComboBox4.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("fecha", Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Rep.SetParameterValue("titulo", "Reporte de Antigüedad de Saldos Extendido")
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                    'V.ReporteViejosSaldosEx(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TP, idClas4)
                Case "Antigüedad de saldos remisiones."
                    'Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Dim TP As Double
                    Dim CM As New dbMonedasConversiones(1, MySqlcon)
                    TP = CM.Cantidad.ToString
                    'Rep = New repVentasViejosSaldos
                    'Rep.SetDataSource()
                    'Rep.SetParameterValue("Encabezado", S.Nombre)
                    'Dim RV As New frmReportes(Rep, False)
                    'RV.Show()
                    Dim Remi As New dbVentasRemisiones(MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    '    'V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
                    Rep = New repVentasViejosSaldosH
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    '    'Rep.SetDataSource(V.ReporteViejosSaldosHN(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd")))
                    If Format(DateTimePicker2.Value, "yyyyMMdd") = Format(Date.Now, "yyyyMMdd") Then
                        Rep.SetDataSource(Remi.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TP, idClas4, idTipo, idClas5))
                    Else
                        Rep.SetDataSource(Remi.ReporteViejosSaldosH(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TP, idTipo, idClas5))
                    End If
                    Dim Filtros As String
                    Filtros = "Sucursal: " + ComboBox1.Text + " Vendedor: " + ComboBox2.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Filtros += " Moneda: " + ComboBox4.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("fecha", Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Rep.SetParameterValue("titulo", "Reporte de Antigüedad de Saldos Remisiones")
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Documentos de clientes."
                    Dim Rep As New repDocumentosClientes
                    Dim db As New dbCapturaDocumentosClientes(MySqlcon)
                    Rep.SetDataSource(db.Reporte(IdCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), TextBox5.Text))
                    Dim Filtros As String
                    Filtros = "Sucursal: " + ComboBox1.Text + " Serie: " + TextBox5.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Notas de crédito."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repNotasCreditoConcepto
                    Rep.SetDataSource(V.ReporteNotasDeCreditoPorConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox5.Text, CheckBox14.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Notas de cargo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repNotasCargoConcepto
                    Rep.SetDataSource(V.ReporteNotasDeCargoPorConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsConceptos.Valor(cmbConcepto.SelectedIndex), TextBox5.Text, CheckBox14.Checked, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Devoluciones."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Dim D As New dbDevoluciones(MySqlcon)
                    Rep = New repDevoluciones
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(D.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, CheckBox14.Checked, ComboBox5.SelectedIndex, IdInventario, TextBox5.Text, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    Filtros += " Tipo Clientes: " + comboTipo.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New RepVentasRemisiones
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, CheckBox6.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, idClas4, idClas5, False, IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Remisiones")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Filtros += " Vendedor: " + ComboBox2.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones clientes concentrado."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New RepVentasRemisionesClientesCon
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, CheckBox6.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, idClas4, idClas5, False, IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, True, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Remisiones")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos" + " Vendedor: " + ComboBox2.Text
                    Else
                        Filtros += " Cliente: " + TextBox2.Text + " Vendedor: " + ComboBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones Canceladas."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New RepVentasRemisionesCancelado
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReporteCanceladas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, CheckBox6.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, idClas4, idClas5, False, IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Remisiones")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text + "  Zona Vendedor: " + cmbZona.Text
                    'If CheckBox14.Checked Then
                    '    Filtros += " SOLO CANCELADAS"
                    'End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones Resumido."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New RepVentasRemisionesResumen
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, CheckBox6.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, idClas4, idClas5, True, IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Remisiones Resumido")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text + "  Zona Vendedor: " + cmbZona.Text
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por método de pago."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New repVentasRemTipoPagoN
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReportePorTipodePagoN(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, Oc.OcultarOc, Oc.SeriesOc, CheckBox6.Checked, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas4, idClas5, CheckBox14.Checked, IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    'Rep.SetParameterValue("TipoRep", "Reporte de Remisiones")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Método: " + ComboBox12.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por clasificación y método de pago."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New repVentasRemTipoPagoClas
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReportePorTipodePago(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, Oc.OcultarOc, Oc.SeriesOc, CheckBox6.Checked, IdsFormas.Valor(ComboBox12.SelectedIndex), idClas4, idClas5, CheckBox14.Checked, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdInventario, idClas, idClas2, idClas3, True, True, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    'Rep.SetParameterValue("TipoRep", "Reporte de Remisiones")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Método: " + ComboBox12.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por artículo."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisiones
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, False, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, False, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por artículo concentrado."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisionesC
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, True, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, False, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por artículo concentrado B."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisionesCB
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, True, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, False, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por artículo concentrado por vendedor."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisionesCBxvendedor
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, True, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, True, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("conDetalle", checkLinea.Checked)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por clasificación."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisionesClases
                    Rep.SetDataSource(VR.ReporteVentasArticulosClases(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, False, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por clasificación concentrado."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisionesCBClases
                    Rep.SetDataSource(VR.ReporteVentasArticulosClases(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, True, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
                    
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text + "  Zona Vendedor: " + cmbZona.Text + "      Zona Cliente: " + cmbZonacliente.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por vendedor."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New repVentasRemisionesVendedor
                    Rep.SetDataSource(VR.ReportexVendedor(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, CheckBox6.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, idClas4, idClas5, IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Remisiones por Vendedor")
                    Rep.SetParameterValue("conDetalle", checkLinea.Checked)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones los más vendidos."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Rep = New repVentaslosmas
                    Rep.SetDataSource(VR.ReporteVentasArticulosMasVendidos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, TextBox5.Text, False, ComboBox8.Text, ComboBox9.SelectedIndex, Oc.OcultarOc, Oc.SeriesOc, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas"
                    End If
                    If ComboBox14.SelectedIndex > 0 Then
                        Filtros += " CAJA: " + ComboBox14.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Tipo", "Remisiones los más vendidos")
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Cotizaciones."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasCotizaciones(MySqlcon)
                    Rep = New RepVentasRPC
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), TextBox5.Text, IdsCajas.Valor(ComboBox14.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Cotizaciones")
                    'Rep.SetParameterValue("columnanombre", "Cliente")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text
                    'If ComboBox14.SelectedIndex > 0 Then
                    'Filtros += " CAJA: " + ComboBox14.Text
                    'End If
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Cotizaciones por artículo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasCotizaciones(MySqlcon)
                    Rep = New repCotizacionesArticulos
                    Rep.SetDataSource(VR.ReportePorArticulo(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), TextBox5.Text, IdInventario, idClas, idClas2, idClas3, IdsCajas.Valor(ComboBox14.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    'Rep.SetParameterValue("TipoRep", "Reporte de Cotizaciones")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    'If ComboBox14.SelectedIndex > 0 Then
                    'Filtros += " CAJA: " + ComboBox14.Text
                    'End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Pedidos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasPedidos(MySqlcon)
                    Rep = New RepVentasRPC
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), TextBox5.Text, IdsCajas.Valor(ComboBox14.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Pedidos")
                    'Rep.SetParameterValue("columnanombre", "Cliente")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    'If ComboBox14.SelectedIndex > 0 Then
                    'Filtros += " CAJA: " + ComboBox14.Text
                    'End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Pedidos concentrado por artículo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasPedidos(MySqlcon)
                    Rep = New repPedidosConArticulos
                    'VR.ReportePorArticulo(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), TextBox5.Text, IdsCajas.Valor(ComboBox14.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex))
                    Rep.SetDataSource(VR.ReportePorArticulo(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), TextBox5.Text, IdsCajas.Valor(ComboBox14.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    'Rep.SetParameterValue("TipoRep", "Reporte de Pedidos")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    'If ComboBox14.SelectedIndex > 0 Then
                    'Filtros += " CAJA: " + ComboBox14.Text
                    'End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Pedidos concentrado por cliente."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasPedidos(MySqlcon)
                    Rep = New repVentasPedidosConcentrado
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, ComboBox5.SelectedIndex, CheckBox14.Checked, IdsVendedores.Valor(ComboBox2.SelectedIndex), TextBox5.Text, IdsCajas.Valor(ComboBox14.SelectedIndex), IdsTiposSuc.Valor(ComboBox15.SelectedIndex), True))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    'Rep.SetParameterValue("TipoRep", "Reporte de Pedidos")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    'If ComboBox14.SelectedIndex > 0 Then
                    'Filtros += " CAJA: " + ComboBox14.Text
                    'End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Catálogo de clientes."
                    Dim rep As New repCatalogoClientes
                    Dim db As New dbClientes(MySqlcon)
                    rep.SetDataSource(db.Reporte2(0, "", CheckBox12.Checked))
                    rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    rep.SetParameterValue("concredito", CheckBox12.Checked)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Clientes - Saldo a Favor."
                    Dim rep As New repClientesSaldoaFavordetalle
                    Dim db As New dbClientes(MySqlcon)
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    rep.SetDataSource(db.ReporteSaldoaFavor(IdCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), idTipo))
                    rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Directorio de clientes."
                    Dim rep As New repDirectorioClientes
                    Dim db As New dbClientes(MySqlcon)
                    rep.SetDataSource(db.Reporte2(0, "", CheckBox12.Checked))
                    rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Lista de Precios."
                    Dim IP As New dbInventarioPrecios(MySqlcon)
                    'IP.Reporte(IdInventario, idClas, idClas2, idClas3)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repListasPrecios
                    Rep.SetDataSource(IP.Reporte(IdInventario, idClas, idClas2, idClas3))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("lista1", CheckBox1.Checked)
                    Rep.SetParameterValue("lista2", CheckBox2.Checked)
                    Rep.SetParameterValue("lista3", CheckBox3.Checked)
                    Rep.SetParameterValue("lista4", CheckBox4.Checked)
                    Rep.SetParameterValue("lista5", CheckBox5.Checked)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Apartados."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim VR As New dbVentasApartados(MySqlcon)
                    'Dim Oc As New dbOpcionesOc(MySqlcon)
                    'Oc.LlenaDatos(0)
                    Rep = New RepVentasApartadosNormal
                    Rep.SetDataSource(VR.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Rep.SetParameterValue("TipoRep", "Reporte de Apartados")
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    'If CheckBox6.Checked Then
                    '    Filtros += " Solo no facturadas"
                    'End If
                    Filtros += " Vendedor: " + ComboBox2.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Apartados por artículo."
                    Dim VR As New dbVentasApartados(MySqlcon)
                    'Dim Oc As New dbOpcionesOc(MySqlcon)
                    'Oc.LlenaDatos(0)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasApartadosArticulos
                    Rep.SetDataSource(VR.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, False, "", 0, TextBox5.Text, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    Filtros += " Vendedor: " + ComboBox2.Text
                    If CheckBox6.Checked Then
                        Filtros += " *Sin remisiones facturadas*"
                    End If
                    'If CheckBox6.Checked Then
                    '    Filtros += " Solo no facturadas."
                    'End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Apartados por artículo concentrado."
                    Dim VR As New dbVentasApartados(MySqlcon)
                    'Dim Oc As New dbOpcionesOc(MySqlcon)
                    'Oc.LlenaDatos(0)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasApartadosArticulosCon
                    Rep.SetDataSource(VR.ReporteVentasArticulosCon(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, False, "", 0, TextBox5.Text, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox16.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    Filtros += " Estado: " + ComboBox16.Text
                    'If  Then
                    'End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Analítica de clientes."
                    Dim rep As New repClientesAna
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Dim db As New dbClientes(MySqlcon)
                    rep.SetDataSource(db.ReporteAnalitico(IdCliente, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), idTipo))
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    rep.SetParameterValue("titulo", "Analítico de Clientes Facturas")
                    rep.SetParameterValue("filtros", Filtros)
                    rep.SetParameterValue("noceros", CheckBox9.Checked)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Analítica de clientes remisiones."
                    Dim rep As New repClientesAna
                    Dim db As New dbClientes(MySqlcon)
                    rep.SetDataSource(db.ReporteAnaliticoRem(IdCliente, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), idTipo))
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    rep.SetParameterValue("titulo", "Analítico de Clientes Remisiones")
                    rep.SetParameterValue("filtros", Filtros)
                    rep.SetParameterValue("noceros", CheckBox9.Checked)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Ventas facturas lotes.", "Ventas remisiones lotes."
                    Dim VL As New dblotes(MySqlcon)
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    If ListBox1.Text = "Ventas facturas lotes." Then
                        VL.reporteVentasLotesFactura(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsVendedores.Valor(ComboBox3.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex))
                    Else
                        VL.reporteVentasLotesRemisiones(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsVendedores.Valor(ComboBox3.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex))
                    End If
                Case "Ventas facturas importaciones.", "Ventas remisiones importaciones."
                    Dim VL As New dbInventarioAduana(MySqlcon)
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    If ListBox1.Text = "Ventas facturas importaciones." Then
                        VL.reporteVentasAduanas(IdsSucursales.Valor(ComboBox1.SelectedIndex), DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox3.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex))
                    Else
                        VL.reporteVentasRemisionesAduanas(IdsSucursales.Valor(ComboBox1.SelectedIndex), DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox3.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex))
                    End If
                Case "Ventas articulos concentrado por peso."
                    Dim rep As New repVentasArticulosCP
                    rep.SetDataSource(V.ReporteVentasArticulosPeso(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, True, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Dim filtros As String = ""
                    filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        filtros += " Cliente: Todos"
                    Else
                        filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        filtros += " Artículo: Todos"
                    Else
                        filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox2.SelectedIndex > 0 Then
                        filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        filtros += " Vendedor: Todos."
                    End If
                    rep.SetParameterValue("encabezado", S.NombreFiscal)
                    rep.SetParameterValue("sucursal", ComboBox1.Text)
                    rep.SetParameterValue("filtros", filtros)
                    rep.SetParameterValue("titulo", "Reporte de ventas de artículos concentrado por peso.")
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Remisiones articulos concentrado por peso."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim rep As New repVentasRemisionesArticulosCP
                    rep.SetDataSource(VR.ReporteVentasArticulosPeso(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, True, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, False, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Dim filtros As String = ""
                    filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        filtros += " Cliente: Todos"
                    Else
                        filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        filtros += " Artículo: Todos"
                    Else
                        filtros += " Artículo: " + TextBox4.Text
                    End If
                    If ComboBox2.SelectedIndex > 0 Then
                        filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        filtros += " Vendedor: Todos."
                    End If
                    rep.SetParameterValue("encabezado", S.NombreFiscal)
                    rep.SetParameterValue("titulo", "Reporte de remisiones de artículos concentrado por peso.")
                    rep.SetParameterValue("filtros", filtros)
                    rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Ventas de artículo por cliente."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    If CheckBox10.Checked = False Then
                        Rep = New repVentasArticulosCliente
                    Else
                        Rep = New repVentasArticulosEscritura
                    End If
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(V.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, False, TextBox5.Text, False, idClas4, idClas5, CheckBox14.Checked, True, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), CheckBox10.Checked, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox13.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If comboTipo.SelectedIndex > 0 Then
                        Filtros += " Tipo Clientes: " + comboTipo.Text
                    End If
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    'Filtros += " Sucursal: " + ComboBox1.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Remisiones por artículo por cliente."
                    Dim VR As New dbVentasRemisiones(MySqlcon)
                    Dim Oc As New dbOpcionesOc(MySqlcon)
                    Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repVentasArticulosRemisionesCliente
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VR.ReporteVentasArticulos(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdInventario, idClas, idClas2, idClas3, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, CheckBox6.Checked, False, Oc.SeriesOc, Oc.OcultarOc, TextBox5.Text, False, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), False, ComboBox17.SelectedIndex))
                    Rep.SetParameterValue("Encabezado", S.NombreFiscal)
                    Dim Filtros As String
                    Filtros = "Período del: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + vbCr
                    If IdCliente = 0 Then
                        Filtros += " Cliente: Todos"
                    Else
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    If TextBox4.Text = "" Then
                        Filtros += " Artículo: Todos"
                    Else
                        Filtros += " Artículo: " + TextBox4.Text
                    End If
                    If CheckBox6.Checked Then
                        Filtros += " Solo no facturadas."
                    End If
                    If CheckBox14.Checked Then
                        Filtros += " *SOLO CANCELADAS*"
                    End If
                    If ComboBox2.SelectedIndex > 0 Then
                        Filtros += " Vendedor: " + ComboBox2.Text
                    ElseIf ComboBox2.SelectedIndex = 0 Then
                        Filtros += " Vendedor: Todos."
                    End If
                    Filtros += " Forma de pago: " + ComboBox17.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
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
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        Dim op As New dbOpciones(MySqlcon)
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        If op.BusquedaporClases = 0 Then
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
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, True, False, False)
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
            B.Dispose()
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
                    IdInventario = 0
                    TextBox4.Text = ""
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

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

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            If IC.BuscaClasificacion(TextBox10.Text) Then
                EncontroClas = True
                ComboBox3.SelectedIndex = IdsClas.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            If IC.BuscaClasificacion(TextBox12.Text, IdsClas.Valor(ComboBox3.SelectedIndex)) Then
                EncontroClas = True
                ComboBox6.SelectedIndex = IdsClas2.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            If IC.BuscaClasificacion(TextBox13.Text, IdsClas2.Valor(ComboBox6.SelectedIndex)) Then
                EncontroClas = True
                ComboBox7.SelectedIndex = IdsClas3.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            ConsultaOn = False
            TextBox10.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IdClas.ToString, "Todas", "nombre")
            HabilitaClase2(True)
        Else
            HabilitaClase2(False)
            HabilitaClase3(False)
            ComboBox6.SelectedIndex = -1
            ConsultaOn = False
            TextBox10.Text = ""
            ConsultaOn = True
        End If

    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas2.Valor(ComboBox6.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            ConsultaOn = False
            TextBox12.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IdClas.ToString, "Todas", "nombre")
            HabilitaClase3(True)
        Else
            HabilitaClase3(False)
            ComboBox7.SelectedIndex = -1
            ConsultaOn = False
            TextBox12.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas3.Valor(ComboBox7.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            ConsultaOn = False
            TextBox13.Text = IC2.Codigo
            ConsultaOn = True
        Else
            ConsultaOn = False
            TextBox13.Text = ""
            ConsultaOn = True
        End If
    End Sub
    Private Sub HabilitaClase2(ByVal Habilita As Boolean)
        TextBox12.Enabled = Habilita
        ComboBox6.Enabled = Habilita
    End Sub
    Private Sub HabilitaClase3(ByVal Habilita As Boolean)
        TextBox13.Enabled = Habilita
        ComboBox7.Enabled = Habilita
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.Text = "Notas de crédito." Then
            LlenaCombos("tblconceptosnotasventas", cmbConcepto, "nombre", "nombret", "idconceptonotaventa", IdsConceptos, " idconceptonotaventa>0 and tipo=1", "Todos")
            Label8.Enabled = True
            cmbConcepto.Enabled = True
        End If
        If ListBox1.Text = "Notas de cargo." Then
            LlenaCombos("tblconceptosnotasventas", cmbConcepto, "nombre", "nombret", "idconceptonotaventa", IdsConceptos, " idconceptonotaventa>0 and tipo=0", "Todos")
            Label8.Enabled = True
            cmbConcepto.Enabled = True
        End If

        'If ListBox1.Text = "Ventas los más vendidos." Or ListBox1.Text = "Remisiones los más vendidos." Then
        '    Label11.Enabled = True
        '    ComboBox8.Enabled = True
        '    Label12.Enabled = True
        '    ComboBox9.Enabled = True
        'Else
        '    Label12.Enabled = False
        '    ComboBox9.Enabled = False
        '    Label11.Enabled = False
        '    ComboBox8.Enabled = False
        'End If
        ''lblzona.Enabled = False
        ''cmbZona.Enabled = False
        ''lblZonaCliente.Enabled = False
        ''cmbZonacliente.Enabled = False

        If ListBox1.Text = "Cobranza." Or ListBox1.Text = "Cobranza Remisiones." Then
            LlenaCombos("tblconceptosnotasventas", cmbConcepto, "nombre", "nombret", "idconceptonotaventa", IdsConceptos, " idconceptonotaventa>0 and tipo=2", "Todos")
            '    Label8.Enabled = True
            '    cmbConcepto.Enabled = True
            '    lblzona.Enabled = True
            '    cmbZona.Enabled = True
            '    lblZonaCliente.Enabled = True
            '    cmbZonacliente.Enabled = True
            '    btnGraficas.Enabled = True
            '    cmbIpoGrafica.Enabled = True
            'Else
            '    btnGraficas.Enabled = False
            '    cmbIpoGrafica.Enabled = False
        End If
        'If ListBox1.Text = "Ventas." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        '    btnGraficas.Enabled = True
        '    cmbIpoGrafica.Enabled = True

        'End If
        'If ListBox1.Text = "Ventas tipo B." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas Resumido." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas con detalle de IVA." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por tipo de pago A." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por tipo de pago B." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por artículo." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por artículo concentrado." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por artículo concentrado B." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por artículo concentrado por vendedor." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por clasificación." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por clasificación concentrado." Or ListBox1.Text = "Ventas con detalles por cliente." Or ListBox1.Text = "Remisiones con detalles por cliente." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas por vendedor." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Ventas los más vendidos." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Utilidad de venta." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        '    btnGraficas.Enabled = True
        '    cmbIpoGrafica.Enabled = False
        'End If
        'If ListBox1.Text = "Utilidad de venta por artículo." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        '    btnGraficas.Enabled = True
        '    cmbIpoGrafica.Enabled = True
        'End If
        'If ListBox1.Text = "Utilidad de venta por artículo concentrado." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        ''Remisiones
        'If ListBox1.Text = "Remisiones." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones tipo B." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones Resumido." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por método de pago." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por artículo." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por artículo concentrado." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por artículo concentrado B." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por artículo concentrado por vendedor." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por clasificación." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por clasificación concentrado." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones por vendedor." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Remisiones los más vendidos." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        ''nuevo
        'If ListBox1.Text = "Antigüedad de saldos." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        'If ListBox1.Text = "Saldos." Then
        '    '    lblzona.Enabled = True
        '    '    cmbZona.Enabled = True
        '    lblZonaCliente.Enabled = True
        '    cmbZonacliente.Enabled = True
        'End If
        ''If ListBox1.Text = "Cobranza." Then
        ''    lblzona.Enabled = True
        ''    cmbZona.Enabled = True
        ''    lblZonaCliente.Enabled = True
        ''    cmbZonacliente.Enabled = True
        ''End If
        ''If ListBox1.Text = "Cobranza Remisiones." Then
        ''    lblzona.Enabled = True
        ''    cmbZona.Enabled = True
        ''    lblZonaCliente.Enabled = True
        ''    cmbZonacliente.Enabled = True
        ''End If
        'If ListBox1.Text = "Antigüedad de saldos remisiones." Then
        '    lblzona.Enabled = True
        '    cmbZona.Enabled = True
        'End If


    End Sub

    Private Sub ComboBox11_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox11.SelectedIndexChanged
        LlenaLista()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If llenando = False Then
            If ComboBox2.SelectedIndex <> 0 Then
                cmbZona.Enabled = False
                cmbZona.SelectedIndex = 0
            Else
                cmbZona.Enabled = True
                cmbZona.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub btnGraficas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGraficas.Click
        Try
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            Dim V As New dbVentas(MySqlcon)
            'Dim O As New dbOpciones(MySqlcon)
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClas3 As Integer
            Dim idClas4 As Integer
            Dim idClas5 As Integer


            Dim TipoCosteo As Byte
            If cmbZonacliente.SelectedIndex <= 0 Then
                idClas5 = 0
            Else
                idClas5 = IdsVendedoresZonaC.Valor(cmbZonacliente.SelectedIndex)
            End If

            If cmbZona.SelectedIndex <= 0 Then
                idClas4 = 0
            Else
                idClas4 = IdsVendedoresZona.Valor(cmbZona.SelectedIndex)
            End If
            If ComboBox3.SelectedIndex <= 0 Then
                idClas = 0
            Else
                idClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            End If
            If ComboBox6.SelectedIndex <= 0 Then
                idClas2 = 0
            Else
                idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
            End If
            If ComboBox7.SelectedIndex <= 0 Then
                idClas3 = 0
            Else
                idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
            End If
            If RadioButton1.Checked Then TipoCosteo = 1
            If RadioButton2.Checked Then TipoCosteo = 2
            Select Case ListBox1.Text
                Case "Ventas."

                    'PRUEBA
                    Dim Rep2 As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(Tipo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente)
                    If cmbIpoGrafica.SelectedIndex = 0 Then
                        Rep2 = New repGraficasVentas
                    Else
                        Rep2 = New repGraficasVentasBarra
                    End If
                    Rep2.SetDataSource(V.ReporteGroupByFolio(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, CheckBox14.Checked, TextBox5.Text, idClas4, idClas5))
                    Rep2.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String
                    Filtros = " Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + "  Sucursal: " + ComboBox1.Text + "  Vendedor: " + ComboBox2.Text + "   Zona Vendedor: " + cmbZona.Text + "   Zona Cliente: " + cmbZonacliente.Text
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    If CheckBox14.Checked Then
                        Filtros += "  SOLO CANCELADAS"
                    End If
                    Rep2.SetParameterValue("Filtros", Filtros)
                    Dim RV2 As New frmReportes(Rep2, False)
                    RV2.Show()

                Case "Utilidad de venta."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim coniva As Byte
                    If CheckBox7.Checked Then
                        coniva = 1
                    Else
                        coniva = 0
                    End If
                    Rep = New repGraficasVentasUtilidad
                    Rep.SetDataSource(V.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    If coniva = 1 Then
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " CON IVA")
                    Else
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " SIN IVA")
                    End If

                    Rep.SetParameterValue("coniva", coniva)
                    Dim Filtros As String
                    Filtros = "  Zona Vendedor: " + cmbZona.Text + "      Zona Cliente: " + cmbZonacliente.Text

                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Tipo", 1)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()

                Case "Utilidad de venta por artículo."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim coniva As Byte
                    If CheckBox7.Checked Then
                        coniva = 1
                    Else
                        coniva = 0
                    End If
                    Rep = New repVentasUtilidadN
                    If ComboBox11.Text = "Facturas" Then
                        Rep.SetDataSource(V.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                        Rep.SetParameterValue("Encabezado", "Reporte de utilidad de venta (Facturas)")
                    End If
                    If ComboBox11.Text = "Remisiones" Then
                        Dim VR As New dbVentasRemisiones(MySqlcon)
                        Dim Oc As New dbOpcionesOc(MySqlcon)
                        Oc.LlenaDatos(0, GlobalIdSucursalDefault)
                        Rep.SetDataSource(VR.ReporteUtilidad(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsVendedores.Valor(ComboBox2.SelectedIndex), IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdInventario, idClas, idClas2, idClas3, TipoCosteo, TextBox5.Text, idClas4, idClas5, Oc.OcultarOc, Oc.SeriesOc, IdsAlmacenes.Valor(ComboBox13.SelectedIndex), IdsCajas.Valor(ComboBox14.SelectedIndex), idTipo, False, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                        Rep.SetParameterValue("Encabezado", "Reporte de utilidad de venta (Remisiones)")
                    End If
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    If coniva = 1 Then
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " CON IVA")
                    Else
                        Rep.SetParameterValue("Fechas", "Del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " SIN IVA")
                    End If
                    Rep.SetParameterValue("coniva", coniva)
                    Dim Filtros As String
                    Filtros = "  Zona Vendedor: " + cmbZona.Text + "      Zona Cliente: " + cmbZonacliente.Text
                    If ComboBox13.SelectedIndex > 0 Then
                        Filtros += " Almacen: " + ComboBox13.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("Tipo", 0)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Cobranza."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Dim VP As New dbVentasPagos(MySqlcon)
                    If cmbIpoGrafica.SelectedIndex = 0 Then
                        Rep = New repGraficasVentaPagos
                    Else
                        Rep = New repGraficasVentaPagosBarra
                    End If
                    idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
                    Rep.SetDataSource(VP.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex, IdsConceptos.Valor(cmbConcepto.SelectedIndex), IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, idClas5, idTipo, IdsTiposSuc.Valor(ComboBox15.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    ' Rep.SetParameterValue("idsucursal", IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    Rep.SetParameterValue("filtros", "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Zona: " + cmbZonacliente.Text)
                    Rep.SetParameterValue("titulo", "Reporte de Cobranza")
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox13, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
        LlenaCombos("tblcajas", ComboBox14, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todas")

    End Sub

    Private Sub comboTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboTipo.SelectedIndexChanged
        idTipo = IdsTipos.Valor(comboTipo.SelectedIndex)
    End Sub

    Private Sub CheckBox11_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox11.CheckedChanged

    End Sub
End Class