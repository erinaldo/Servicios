Public Class frmReportesServicios
    Public tipo As String
    Dim idCliente As Integer
    Dim idEquipo As Integer
    Dim Dt As DataTable
    Dim tablaTecnicos As DataTable
    Dim tablaClas As DataTable
    Dim tablaSubClas As DataTable
    Dim num As Integer
    Dim idTecnico As Integer
    Dim idClasificacion As Integer
    Dim idSubClasificacion As Integer
    Dim bandera As Boolean

    Private Sub frmReportesServicios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Year As String
        Dim Month As String
        Year = Date.Now.Year.ToString
        Month = Format(Date.Now.Month, "00")
        dtpFecha.Value = "01/" + Month + "/" + Year.ToString
        dtpFecha2.MinDate = dtpFecha.Value

    End Sub

    Private Sub lstTipos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTipos.Click
        num = lstTipos.SelectedIndex
        If num >= 0 Then
            tipo = lstTipos.Items(num).ToString()

            If num = "1" Then
                txtCliente.BackColor = SystemColors.Window
                cmbEquipo.Enabled = False
                txtCliente.Enabled = True
                btnBuscarCliente.Enabled = True
                btnImprimir.Enabled = True
                dtpFecha.Enabled = False
                dtpFecha2.Enabled = False
                txtCliente.Text = ""
                cmbTecnico.Enabled = False
                cmbClasificacion.Enabled = False
                cmbSubClasificacion.Enabled = False
                bandera = False
                cmbEquipo.DataSource = Nothing
                cmbEquipo.Items.Clear()
                cmbEquipo.Items.Remove(cmbEquipo.DisplayMember)

                cmbTecnico.DataSource = Nothing
                cmbTecnico.Items.Clear()
                cmbTecnico.Items.Remove(cmbTecnico.DisplayMember)

                cmbClasificacion.DataSource = Nothing
                cmbClasificacion.Items.Clear()
                cmbClasificacion.Items.Remove(cmbClasificacion.DisplayMember)

                cmbSubClasificacion.DataSource = Nothing
                cmbSubClasificacion.Items.Clear()
                cmbSubClasificacion.Items.Remove(cmbSubClasificacion.DisplayMember)


                bandera = True


            Else
                If num = "0" Then
                    txtCliente.BackColor = SystemColors.Window
                    btnImprimir.Enabled = True
                    btnBuscarCliente.Enabled = False
                    cmbEquipo.Enabled = False
                    txtCliente.Enabled = False
                    dtpFecha.Enabled = True
                    dtpFecha2.Enabled = True
                    txtCliente.Text = ""
                    cmbTecnico.Enabled = False
                    cmbClasificacion.Enabled = False
                    cmbSubClasificacion.Enabled = False
                    bandera = False
                    cmbEquipo.DataSource = Nothing
                    cmbEquipo.Items.Clear()
                    cmbEquipo.Items.Remove(cmbEquipo.DisplayMember)

                    cmbTecnico.DataSource = Nothing
                    cmbTecnico.Items.Clear()
                    cmbTecnico.Items.Remove(cmbTecnico.DisplayMember)

                    cmbClasificacion.DataSource = Nothing
                    cmbClasificacion.Items.Clear()
                    cmbClasificacion.Items.Remove(cmbClasificacion.DisplayMember)

                    cmbSubClasificacion.DataSource = Nothing
                    cmbSubClasificacion.Items.Clear()
                    cmbSubClasificacion.Items.Remove(cmbSubClasificacion.DisplayMember)
                    bandera = True
                Else
                    If num = "2" Then
                        txtCliente.BackColor = SystemColors.Window
                        cmbEquipo.Enabled = True
                        txtCliente.Enabled = True
                        btnBuscarCliente.Enabled = True
                        btnImprimir.Enabled = True
                        dtpFecha.Enabled = False
                        dtpFecha2.Enabled = False
                        txtCliente.Text = ""
                        cmbTecnico.Enabled = False
                        cmbClasificacion.Enabled = False
                        cmbSubClasificacion.Enabled = False
                        bandera = False
                        cmbEquipo.DataSource = Nothing
                        cmbEquipo.Items.Clear()
                        cmbEquipo.Items.Remove(cmbEquipo.DisplayMember)

                        cmbTecnico.DataSource = Nothing
                        cmbTecnico.Items.Clear()
                        cmbTecnico.Items.Remove(cmbTecnico.DisplayMember)

                        cmbClasificacion.DataSource = Nothing
                        cmbClasificacion.Items.Clear()
                        cmbClasificacion.Items.Remove(cmbClasificacion.DisplayMember)

                        cmbSubClasificacion.DataSource = Nothing
                        cmbSubClasificacion.Items.Clear()
                        cmbSubClasificacion.Items.Remove(cmbSubClasificacion.DisplayMember)
                        bandera = True

                    else
                    If num = "3" Then
                        txtCliente.BackColor = SystemColors.Window
                        cmbEquipo.Enabled = True
                        txtCliente.Enabled = True
                        btnBuscarCliente.Enabled = True
                        btnImprimir.Enabled = True
                        dtpFecha.Enabled = False
                        dtpFecha2.Enabled = False
                        txtCliente.Text = ""
                            cmbTecnico.Enabled = False
                            cmbClasificacion.Enabled = False
                            cmbSubClasificacion.Enabled = False
                        bandera = False
                        cmbEquipo.DataSource = Nothing
                        cmbEquipo.Items.Clear()
                            cmbEquipo.Items.Remove(cmbEquipo.DisplayMember)

                            cmbTecnico.DataSource = Nothing
                            cmbTecnico.Items.Clear()
                            cmbTecnico.Items.Remove(cmbTecnico.DisplayMember)

                            cmbClasificacion.DataSource = Nothing
                            cmbClasificacion.Items.Clear()
                            cmbClasificacion.Items.Remove(cmbClasificacion.DisplayMember)

                            cmbSubClasificacion.DataSource = Nothing
                            cmbSubClasificacion.Items.Clear()
                            cmbSubClasificacion.Items.Remove(cmbSubClasificacion.DisplayMember)
                        bandera = True
                        End If
                    End If
                End If

            End If
            If num = "4" Then
                txtCliente.BackColor = SystemColors.Window
                cmbEquipo.Enabled = False
                txtCliente.Enabled = False
                btnBuscarCliente.Enabled = False
                btnImprimir.Enabled = False
                dtpFecha.Enabled = False
                dtpFecha2.Enabled = False
                txtCliente.Text = ""
                cmbTecnico.Enabled = True
                cmbClasificacion.Enabled = True
                cmbSubClasificacion.Enabled = True
                bandera = False
                cmbEquipo.DataSource = Nothing
                cmbEquipo.Items.Clear()
                cmbEquipo.Items.Remove(cmbEquipo.DisplayMember)

                cmbTecnico.DataSource = Nothing
                cmbTecnico.Items.Clear()
                cmbTecnico.Items.Remove(cmbTecnico.DisplayMember)

                cmbClasificacion.DataSource = Nothing
                cmbClasificacion.Items.Clear()
                cmbClasificacion.Items.Remove(cmbClasificacion.DisplayMember)

                cmbSubClasificacion.DataSource = Nothing
                cmbSubClasificacion.Items.Clear()
                cmbSubClasificacion.Items.Remove(cmbSubClasificacion.DisplayMember)
                bandera = True
                CargarTecnicos()
                CargarSubClasificacion()
                CargarClasificacion()
            End If

            cargarEquipos()
        End If

    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            
            txtCliente.Text = B.Cliente.Nombre.ToString()
            idCliente = B.Cliente.ID
            If num <> 1 And num <> 5 Then
                cargarEquipos()
            End If
            ' txtReferencia.Focus()
        End If
    End Sub
    Private Sub cargarEquipos()
        If txtCliente.Text <> "" Then

            Dim P As New dbServicios(MySqlcon)

            Dt = P.buscarEquipos(idCliente)
            If Dt.Rows.Count > 0 Then

                With cmbEquipo
                    .DataSource = Dt
                    .DisplayMember = "nombre"
                    .ValueMember = "nombre"
                End With
                btnImprimir.Enabled = True
            Else
                'If num = 2 Then
                MsgBox("El cliente no tiene equipos registrados.", MsgBoxStyle.Critical, GlobalNombreApp)
                btnImprimir.Enabled = False
                'End If
            End If
        End If
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If tipo = "Servicios" Then

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim fecha1 As String = conversionFecha(dtpFecha.Value)
            Dim fecha2 As String = conversionFecha(dtpFecha2.Value)

            Rep = New repServicios
            Rep.SetDataSource(servicios())
            Rep.SetParameterValue("fecha1", fecha1)
            Rep.SetParameterValue("fecha2", fecha2)
            Rep.SetParameterValue("tipo", "CLIENTE")
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
        If tipo = "Cliente - Equipos" Then

            If txtCliente.Text = "" Then
                'mostrar todos

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repEquiposClientesTodos
                Rep.SetDataSource(clienteEquiposTodos())
                Rep.SetParameterValue("tipo", "CLIENTE")
                Dim RV As New frmReportes(Rep, False)
                RV.Show()

            Else
                'mostrar por cliente
                Dim NoErrores As Boolean = True
                Dim MensajeError As String = ""
                txtCliente.BackColor = SystemColors.Window
                Dim p As New dbServicios(MySqlcon)
                If p.Existe(txtCliente.Text) = 0 Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe seleccionar un cliente registrado"
                    txtCliente.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    idCliente = p.obtID(txtCliente.Text)
                End If


                If NoErrores = True Then


                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim q As New dbServicios(MySqlcon)
                    Dim nombreEmpresa As String = q.nombreCliente(idCliente)
                    Dim rfc As String = q.ConsultaRFC(idCliente)
                    Dim direccion As String = q.Consultadireccion(idCliente)
                    Dim direccion2 As String = q.direccion2(idCliente)

                    Rep = New repEquiposClientes
                    Rep.SetDataSource(clienteEquipos())
                    Rep.SetParameterValue("encabezado", nombreEmpresa)
                    Rep.SetParameterValue("direccion", direccion)
                    Rep.SetParameterValue("rfc", rfc)
                    Rep.SetParameterValue("direccion2", direccion2)

                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                End If
                If NoErrores = False Then
                    MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

                End If
            End If


        End If
        If tipo = "Componentes de equipo" Then
            If txtCliente.Text = "" Then
                'Mostrar todos los componenetes de todos los equipos

                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repComponentesTodos
                Rep.SetDataSource(consumiblesTodos())

                Dim RV As New frmReportes(Rep, False)
                RV.Show()


            Else

                Dim NoErrores As Boolean = True
                Dim MensajeError As String = ""
                txtCliente.BackColor = SystemColors.Window
                Dim P As New dbDetallesEquipo(MySqlcon)
                Dim q As New dbServicios(MySqlcon)
                If q.Existe(txtCliente.Text) = 0 Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe seleccionar un cliente registrado"
                    txtCliente.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    idCliente = q.obtID(txtCliente.Text)
                End If


                If NoErrores = True Then
                    P.equipoDatos(idEquipo)

                    Dim nombre As String = P.nombre
                    Dim marca As String = P.marca
                    Dim modelo As String = P.modelo
                    Dim matricula As String = P.matricula
                    Dim noserie As String = P.noserie
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    P.clienteDatos(idCliente)
                    Dim nombreCliente As String = P.nombreCliente
                    Dim direccion As String = P.direccionCliente
                    Dim rfc As String = P.rfc
                    Dim direccion2 As String = P.direccion2

                    Rep = New repConsumibles
                    Rep.SetDataSource(consumibles())
                    Rep.SetParameterValue("nombre", nombre)
                    Rep.SetParameterValue("marca", marca)
                    Rep.SetParameterValue("modelo", modelo)
                    Rep.SetParameterValue("NO. SERIE", noserie)
                    Rep.SetParameterValue("matricula", matricula)
                    Rep.SetParameterValue("DATOS", "CLIENTE")
                    Rep.SetParameterValue("encabezado", nombreCliente)
                    Rep.SetParameterValue("direccion", direccion)
                    Rep.SetParameterValue("rfc", rfc)
                    Rep.SetParameterValue("direccion2", direccion2)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                End If
                If NoErrores = False Then
                    MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

                End If
            End If
        End If
        If tipo = "Historial de equipos" Then
            If txtCliente.Text = "" Then
                Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                Rep = New repEquiposHistorialTODOS
                Rep.SetDataSource(HistorialTodos())
                Dim RV As New frmReportes(Rep, False)
                RV.Show()


            Else

                Dim NoErrores As Boolean = True
                Dim MensajeError As String = ""
                txtCliente.BackColor = SystemColors.Window
                Dim P As New dbDetallesEquipo(MySqlcon)
                Dim q As New dbServicios(MySqlcon)
                If q.Existe(txtCliente.Text) = 0 Then
                    NoErrores = False
                    MensajeError += vbCrLf + "Debe seleccionar un cliente registrado"
                    txtCliente.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    idCliente = q.obtID(txtCliente.Text)
                End If


                If NoErrores = True Then
                    P.equipoDatos(idEquipo)

                    Dim nombre As String = P.nombre
                    Dim marca As String = P.marca
                    Dim modelo As String = P.modelo
                    Dim matricula As String = P.matricula
                    Dim noserie As String = P.noserie
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    P.clienteDatos(idCliente)
                    Dim nombreCliente As String = P.nombreCliente
                    Dim direccion As String = P.direccionCliente
                    Dim rfc As String = P.rfc
                    Dim direccion2 As String = P.direccion2

                    Rep = New repEquiposHistorial
                    Rep.SetDataSource(HistorialEspecifico())
                    Rep.SetParameterValue("nombre", nombre)
                    Rep.SetParameterValue("marca", marca)
                    Rep.SetParameterValue("modelo", modelo)
                    Rep.SetParameterValue("NO. SERIE", noserie)
                    Rep.SetParameterValue("matricula", matricula)
                    Rep.SetParameterValue("DATOS", "CLIENTE")
                    Rep.SetParameterValue("encabezado", nombreCliente)
                    Rep.SetParameterValue("direccion", direccion)
                    Rep.SetParameterValue("rfc", rfc)
                    Rep.SetParameterValue("direccion2", direccion2)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                End If
                If NoErrores = False Then
                    MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

                End If
            End If
           
        End If
        If tipo = "Técnicos" Then

            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            
            Rep = New repTecnicos
            Rep.SetDataSource(HistoriaTecnico())
            Rep.SetParameterValue("tecnico", cmbTecnico.Text)
            Rep.SetParameterValue("clas", cmbClasificacion.Text)
            Rep.SetParameterValue("clas2", cmbSubClasificacion.Text)

            Dim RV As New frmReportes(Rep, False)
            RV.Show()

        End If
    End Sub

    Private Function consumibles() As DataSet
        'Try
        Dim PrimerCeldaRow As Integer = -1
        Dim P As New dbDetallesEquipo(MySqlcon)
        '  If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
        Dim Dt As New DataTable
        Dim TablaFull As New DataTable
        Dim tablaConsumibles As DataTable
        Dim diasRestantes As Integer
        Dim diasRestantes1 As String = ""
        ' Dim tablaFinal As DataTable
        Dim hoy As Date
        Dim diaFinal As Date
        TablaFull.Columns.Add("fecha")
        TablaFull.Columns.Add("Cantidad")
        TablaFull.Columns.Add("Código")
        TablaFull.Columns.Add("Descripcion")
        TablaFull.Columns.Add("TiepoVida")
        TablaFull.Columns.Add("Fechaexpira")
        TablaFull.Columns.Add("DiasRestantes")
        ' TablaFull.Columns.Add("Tiempo vida")
        Dt = P.detallesEquipo(IdEquipo)
        'Dim P As New dbDetallesEquipo(MySqlcon)
        For i As Integer = 0 To Dt.Rows.Count() - 1

            If P.cuantosConsumibles(Dt.Rows(i)(1), Dt.Rows(i)(3)) > 0 Then
                tablaConsumibles = P.consumibles(Dt.Rows(i)(1), Dt.Rows(i)(3)) 'asegurarse que se rdenen de mayor a menor la fehca
                Dim dr1 As DataRow
                hoy = DateTime.ParseExact(tablaConsumibles.Rows(0)(0).ToString, "yyyy-MM-dd", Nothing)
                diaFinal = hoy.AddDays(Integer.Parse(Dt.Rows(i)(4).ToString))

                dr1 = TablaFull.NewRow()
                dr1("fecha") = tablaConsumibles.Rows(0)(0).ToString
                dr1("Cantidad") = tablaConsumibles.Rows(0)(1).ToString
                P.buscarArticulo(tablaConsumibles.Rows(0)(2).ToString)
                dr1("Código") = P.codigo
                dr1("Descripcion") = P.descripcion
                dr1("TiepoVida") = Dt.Rows(i)(4).ToString
                dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha
                diasRestantes = DateDiff("d", conversionFecha(Date.Now()), conversionFecha(diaFinal))
                If diasRestantes <= 0 Then
                    diasRestantes1 = "rojo"
                Else
                    If diasRestantes < 4 Then
                        diasRestantes1 = "amarillo"
                    Else
                        diasRestantes1 = "blanco"
                    End If
                End If
                dr1("DiasRestantes") = diasRestantes1
                TablaFull.Rows.Add(dr1)
            Else

                Dim dr1 As DataRow
                hoy = DateTime.ParseExact(Dt.Rows(i)(5).ToString, "yyyy-MM-dd", Nothing)
                diaFinal = hoy.AddDays(Integer.Parse(Dt.Rows(i)(4).ToString))

                dr1 = TablaFull.NewRow()
                dr1("fecha") = Dt.Rows(i)(5).ToString
                dr1("Cantidad") = Dt.Rows(i)(2).ToString
                P.buscarArticulo(Dt.Rows(i)(3).ToString)
                dr1("Código") = P.codigo
                dr1("Descripcion") = P.descripcion
                ' dr1("idInventario") = P.descripcion
                'P.buscarArticulo(Dt.Rows(i)(3).ToString)
                dr1("TiepoVida") = Dt.Rows(i)(4).ToString
                dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha
                diasRestantes = DateDiff("d", conversionFecha(Date.Now()), conversionFecha(diaFinal))
                If diasRestantes <= 0 Then
                    diasRestantes1 = "rojo"
                Else
                    If diasRestantes < 4 Then
                        diasRestantes1 = "amarillo"
                    Else
                        diasRestantes1 = "blanco"
                    End If
                End If
                dr1("DiasRestantes") = diasRestantes1
                TablaFull.Rows.Add(dr1)


            End If

        Next


        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblConsumibles.xml")
        Return dataSet
    End Function

    Public Function conversionFecha(ByVal fecha As Date) As String
        Dim dia As String
        Dim mes As String
        Dim anio As String
        Dim fechaFinal As String

        dia = fecha.Date.Day.ToString()
        mes = fecha.Date.Month.ToString()
        anio = fecha.Date.Year.ToString()
        fechaFinal = anio + "-" + Format(Integer.Parse(mes), "00") + "-" + Format(Integer.Parse(dia), "00")
        Return fechaFinal
    End Function

    Private Sub cmbEquipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbEquipo.SelectedIndexChanged
        If bandera = True Then

            If Dt.Rows.Count > 0 Then
                Dim i As Integer = 0

                i = cmbEquipo.SelectedIndex()
                idEquipo = Integer.Parse(Dt.Rows(i)(0).ToString())

            End If

        End If
    End Sub

    Private Function servicios() As DataSet
        'Try
        Dim PrimerCeldaRow As Integer = -1
        Dim P As New dbServicios(MySqlcon)
        Dim estado As String
        '  If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
        Dim Dt2 As New DataTable
        Dim TablaFull As New DataTable

       
        TablaFull.Columns.Add("idServicio")
        TablaFull.Columns.Add("Equipo")
        TablaFull.Columns.Add("Detalles")
        TablaFull.Columns.Add("fechae")
        TablaFull.Columns.Add("horae")
        TablaFull.Columns.Add("fechas")
        TablaFull.Columns.Add("horas")
        TablaFull.Columns.Add("Estado")
        TablaFull.Columns.Add("Cliente")

        ' TablaFull.Columns.Add("Tiempo vida")
        Dt2 = P.buscarServicios(conversionFecha(dtpFecha.Value), conversionFecha(dtpFecha2.Value))

        For i As Integer = 0 To Dt2.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = TablaFull.NewRow()
            dr1("idServicio") = Dt2.Rows(i)(0).ToString
            dr1("Equipo") = P.ConsultanombreEquipo(Integer.Parse(Dt2.Rows(i)(11).ToString))
            dr1("Detalles") = Dt2.Rows(i)(1).ToString
            dr1("fechae") = Dt2.Rows(i)(2).ToString
            dr1("horae") = Dt2.Rows(i)(3).ToString
            dr1("fechas") = Dt2.Rows(i)(4).ToString
            dr1("horas") = Dt2.Rows(i)(5).ToString
            If Dt2.Rows(i)(6).ToString = "1" Then
                estado = "Listo"
            Else
                estado = "En taller"
            End If

            dr1("Estado") = estado
            dr1("Cliente") = P.ConsultaCliente(Integer.Parse(Dt2.Rows(i)(7).ToString))
            TablaFull.Rows.Add(dr1)
            'P.buscarArticulo(tablaConsumibles.Rows(0)(2).ToString)
            ' diasRestantes = DateDiff("d", conversionFecha(Date.Now()), conversionFecha(diaFinal))
        Next


        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblReporteServicios.xml")
        Return dataSet
    End Function


    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        dtpFecha2.MinDate = dtpFecha.Value
    End Sub

    Private Function clienteEquipos() As DataSet
        'Try
        Dim PrimerCeldaRow As Integer = -1
        Dim P As New dbServicios(MySqlcon)

        Dim q As New dbDetallesEquipo(MySqlcon)
        ' Dim estado As String
        Dim Dt2 As New DataTable
        Dim Dt3 As New DataTable
        Dim hoy As Date
        Dim diaFinal As Date
        Dim tablaConsumibles As New DataTable
        Dim TablaFull As New DataTable

        'Equipo
        TablaFull.Columns.Add("idEquipo")
        TablaFull.Columns.Add("Nombre")
        TablaFull.Columns.Add("Marca")
        TablaFull.Columns.Add("Modelo")
        TablaFull.Columns.Add("No. Serie")
        TablaFull.Columns.Add("No. Motor")
        TablaFull.Columns.Add("Matricula")
        TablaFull.Columns.Add("Color")
        TablaFull.Columns.Add("Kilometraje")
        'Componentes

        TablaFull.Columns.Add("fecha")
        TablaFull.Columns.Add("Cantidad")
        TablaFull.Columns.Add("Código")
        TablaFull.Columns.Add("Descripcion")
        TablaFull.Columns.Add("TiepoVida")
        TablaFull.Columns.Add("Fechaexpira")
        'TablaFull.Columns.Add("DiasRestantes")

        Dt2 = P.buscarEquioposCliente(idCliente)

        For i As Integer = 0 To Dt2.Rows.Count() - 1
            
            Dt3 = q.detallesEquipo(Integer.Parse(Dt2.Rows(i)(0)))


            If Dt3.Rows.Count = 0 Then
                Dim dr1 As DataRow
                dr1 = TablaFull.NewRow()

                dr1("idEquipo") = Dt2.Rows(i)(0).ToString
                dr1("Nombre") = Dt2.Rows(i)(1).ToString
                dr1("Marca") = Dt2.Rows(i)(2).ToString
                dr1("Modelo") = Dt2.Rows(i)(3).ToString
                dr1("No. Serie") = Dt2.Rows(i)(4).ToString
                dr1("No. Motor") = Dt2.Rows(i)(5).ToString
                dr1("Matricula") = Dt2.Rows(i)(6).ToString
                dr1("Color") = Dt2.Rows(i)(7).ToString
                dr1("Kilometraje") = Dt2.Rows(i)(8).ToString
                dr1("fecha") = ""
                dr1("Cantidad") = ""
                dr1("Código") = ""
                dr1("Descripcion") = ""
                dr1("TiepoVida") = ""
                dr1("Fechaexpira") = ""

                TablaFull.Rows.Add(dr1)

            Else


                For j As Integer = 0 To Dt3.Rows.Count() - 1
                    Dim dr1 As DataRow
                    dr1 = TablaFull.NewRow()

                    dr1("idEquipo") = Dt2.Rows(i)(0).ToString
                    dr1("Nombre") = Dt2.Rows(i)(1).ToString
                    dr1("Marca") = Dt2.Rows(i)(2).ToString
                    dr1("Modelo") = Dt2.Rows(i)(3).ToString
                    dr1("No. Serie") = Dt2.Rows(i)(4).ToString
                    dr1("No. Motor") = Dt2.Rows(i)(5).ToString
                    dr1("Matricula") = Dt2.Rows(i)(6).ToString
                    dr1("Color") = Dt2.Rows(i)(7).ToString
                    dr1("Kilometraje") = Dt2.Rows(i)(8).ToString

                    'Componentes


                    If q.cuantosConsumibles(Dt3.Rows(j)(1), Dt3.Rows(j)(3)) > 0 Then
                        tablaConsumibles = q.consumibles(Dt3.Rows(j)(1), Dt3.Rows(j)(3)) 'asegurarse que se rdenen de mayor a menor la fehca
                        hoy = DateTime.ParseExact(tablaConsumibles.Rows(0)(0).ToString, "yyyy-MM-dd", Nothing)
                        diaFinal = hoy.AddDays(Integer.Parse(Dt3.Rows(j)(4).ToString))

                        dr1("fecha") = tablaConsumibles.Rows(0)(0).ToString
                        dr1("Cantidad") = tablaConsumibles.Rows(0)(1).ToString
                        q.buscarArticulo(tablaConsumibles.Rows(0)(2).ToString)
                        dr1("Código") = q.codigo
                        dr1("Descripcion") = q.descripcion
                        dr1("TiepoVida") = Dt3.Rows(j)(4).ToString
                        dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha

                    Else

                        hoy = DateTime.ParseExact(Dt3.Rows(j)(5).ToString, "yyyy-MM-dd", Nothing)
                        diaFinal = hoy.AddDays(Integer.Parse(Dt3.Rows(j)(4).ToString))

                        dr1("fecha") = Dt3.Rows(j)(5).ToString
                        dr1("Cantidad") = Dt3.Rows(j)(2).ToString
                        q.buscarArticulo(Dt3.Rows(j)(3).ToString)
                        dr1("Código") = q.codigo
                        dr1("Descripcion") = q.descripcion
                        dr1("TiepoVida") = Dt3.Rows(j)(4).ToString
                        dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha

                    End If

                    TablaFull.Rows.Add(dr1)
                Next
            End If
        Next


        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblReporteClientesEquipos.xml")
        Return dataSet
    End Function
    Private Function clienteEquiposTodos() As DataSet
        'Try
        Dim PrimerCeldaRow As Integer = -1
        Dim P As New dbServicios(MySqlcon)

        Dim q As New dbDetallesEquipo(MySqlcon)
        ' Dim estado As String
        Dim Dt2 As New DataTable
        Dim Dt3 As New DataTable
        Dim hoy As Date
        Dim diaFinal As Date
        Dim tablaConsumibles As New DataTable
        Dim TablaFull As New DataTable

        'Cliente
        TablaFull.Columns.Add("idCliente")
        TablaFull.Columns.Add("Cliente")
        TablaFull.Columns.Add("RFC")
        TablaFull.Columns.Add("Direccion")
        TablaFull.Columns.Add("Direccion2")
        'equipo
        TablaFull.Columns.Add("idEquipo")
        TablaFull.Columns.Add("Nombre")
        TablaFull.Columns.Add("Marca")
        TablaFull.Columns.Add("Modelo")
        TablaFull.Columns.Add("No. Serie")
        TablaFull.Columns.Add("No. Motor")
        TablaFull.Columns.Add("Matricula")
        TablaFull.Columns.Add("Color")
        TablaFull.Columns.Add("Kilometraje")
        'Componentes

        TablaFull.Columns.Add("fecha")
        TablaFull.Columns.Add("Cantidad")
        TablaFull.Columns.Add("Código")
        TablaFull.Columns.Add("Descripcion")
        TablaFull.Columns.Add("TiepoVida")
        TablaFull.Columns.Add("Fechaexpira")
        'TablaFull.Columns.Add("DiasRestantes")

        Dt2 = P.buscarEquioposClienteTodos()

        For i As Integer = 0 To Dt2.Rows.Count() - 1

            Dt3 = q.detallesEquipo(Integer.Parse(Dt2.Rows(i)(0)))

            If Dt3.Rows.Count = 0 Then
                Dim dr1 As DataRow
                dr1 = TablaFull.NewRow()
                idCliente = Integer.Parse(Dt2.Rows(i)(9).ToString)

                dr1("idCliente") = Dt2.Rows(i)(9).ToString
                dr1("Cliente") = P.nombreCliente(idCliente)
                dr1("RFC") = P.ConsultaRFC(idCliente)
                dr1("Direccion") = P.Consultadireccion(idCliente)
                dr1("Direccion2") = P.direccion2(idCliente)
                dr1("idEquipo") = Dt2.Rows(i)(0).ToString
                dr1("Nombre") = Dt2.Rows(i)(1).ToString
                dr1("Marca") = Dt2.Rows(i)(2).ToString
                dr1("Modelo") = Dt2.Rows(i)(3).ToString
                dr1("No. Serie") = Dt2.Rows(i)(4).ToString
                dr1("No. Motor") = Dt2.Rows(i)(5).ToString
                dr1("Matricula") = Dt2.Rows(i)(6).ToString
                dr1("Color") = Dt2.Rows(i)(7).ToString
                dr1("Kilometraje") = Dt2.Rows(i)(8).ToString
                dr1("fecha") = ""
                dr1("Cantidad") = ""
                dr1("Código") = ""
                dr1("Descripcion") = ""
                dr1("TiepoVida") = ""
                dr1("Fechaexpira") = ""

                TablaFull.Rows.Add(dr1)

            Else


                For j As Integer = 0 To Dt3.Rows.Count() - 1
                    Dim dr1 As DataRow
                    dr1 = TablaFull.NewRow()
                    idCliente = Integer.Parse(Dt2.Rows(i)(9).ToString)

                    dr1("idCliente") = Dt2.Rows(i)(9).ToString
                    dr1("Cliente") = P.nombreCliente(idCliente)
                    dr1("RFC") = P.ConsultaRFC(idCliente)
                    dr1("Direccion") = P.Consultadireccion(idCliente)
                    dr1("Direccion2") = P.direccion2(idCliente)



                    dr1("idEquipo") = Dt2.Rows(i)(0).ToString
                    dr1("Nombre") = Dt2.Rows(i)(1).ToString
                    dr1("Marca") = Dt2.Rows(i)(2).ToString
                    dr1("Modelo") = Dt2.Rows(i)(3).ToString
                    dr1("No. Serie") = Dt2.Rows(i)(4).ToString
                    dr1("No. Motor") = Dt2.Rows(i)(5).ToString
                    dr1("Matricula") = Dt2.Rows(i)(6).ToString
                    dr1("Color") = Dt2.Rows(i)(7).ToString
                    dr1("Kilometraje") = Dt2.Rows(i)(8).ToString

                    'Componentes


                    If q.cuantosConsumibles(Dt3.Rows(j)(1), Dt3.Rows(j)(3)) > 0 Then
                        tablaConsumibles = q.consumibles(Dt3.Rows(j)(1), Dt3.Rows(j)(3)) 'asegurarse que se rdenen de mayor a menor la fehca
                        hoy = DateTime.ParseExact(tablaConsumibles.Rows(0)(0).ToString, "yyyy-MM-dd", Nothing)
                        diaFinal = hoy.AddDays(Integer.Parse(Dt3.Rows(j)(4).ToString))

                        dr1("fecha") = tablaConsumibles.Rows(0)(0).ToString
                        dr1("Cantidad") = tablaConsumibles.Rows(0)(1).ToString
                        q.buscarArticulo(tablaConsumibles.Rows(0)(2).ToString)
                        dr1("Código") = q.codigo
                        dr1("Descripcion") = q.descripcion
                        dr1("TiepoVida") = Dt3.Rows(j)(4).ToString
                        dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha

                    Else

                        hoy = DateTime.ParseExact(Dt3.Rows(j)(5).ToString, "yyyy-MM-dd", Nothing)
                        diaFinal = hoy.AddDays(Integer.Parse(Dt3.Rows(j)(4).ToString))

                        dr1("fecha") = Dt3.Rows(j)(5).ToString
                        dr1("Cantidad") = Dt3.Rows(j)(2).ToString
                        q.buscarArticulo(Dt3.Rows(j)(3).ToString)
                        dr1("Código") = q.codigo
                        dr1("Descripcion") = q.descripcion
                        dr1("TiepoVida") = Dt3.Rows(j)(4).ToString
                        dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha

                    End If

                    TablaFull.Rows.Add(dr1)
                Next
            End If
        Next


        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblReporteClientesEquiposTodos.xml")
        Return dataSet
    End Function

    Private Function consumiblesTodos() As DataSet
        '  Try
        Dim PrimerCeldaRow As Integer = -1
        Dim P As New dbServicios(MySqlcon)
        Dim q As New dbDetallesEquipo(MySqlcon)
        Dim Dt2 As New DataTable
        Dim Dt3 As New DataTable
        Dim hoy As Date
        Dim diasRestantes As Integer
        Dim diasRestantes1 As String = ""
        Dim diaFinal As Date
        Dim tablaConsumibles As New DataTable
        Dim TablaFull As New DataTable


        TablaFull.Columns.Add("idEquipo")
        TablaFull.Columns.Add("Nombre")
        TablaFull.Columns.Add("Marca")
        TablaFull.Columns.Add("Modelo")
        TablaFull.Columns.Add("No. Serie")
        TablaFull.Columns.Add("No. Motor")
        TablaFull.Columns.Add("Matricula")
        TablaFull.Columns.Add("Color")
        TablaFull.Columns.Add("Kilometraje")
        'Componentes

        TablaFull.Columns.Add("fecha")
        TablaFull.Columns.Add("Cantidad")
        TablaFull.Columns.Add("Código")
        TablaFull.Columns.Add("Descripcion")
        TablaFull.Columns.Add("TiepoVida")
        TablaFull.Columns.Add("Fechaexpira")
        TablaFull.Columns.Add("DiasRestantes")

        Dt2 = P.buscarEquioposClienteTodos()

        For i As Integer = 0 To Dt2.Rows.Count() - 1

            Dt3 = q.detallesEquipo(Integer.Parse(Dt2.Rows(i)(0)))

            If Dt3.Rows.Count = 0 Then
                Dim dr1 As DataRow
                dr1 = TablaFull.NewRow()
                idCliente = Integer.Parse(Dt2.Rows(i)(9).ToString)


                dr1("idEquipo") = Dt2.Rows(i)(0).ToString
                dr1("Nombre") = Dt2.Rows(i)(1).ToString
                dr1("Marca") = Dt2.Rows(i)(2).ToString
                dr1("Modelo") = Dt2.Rows(i)(3).ToString
                dr1("No. Serie") = Dt2.Rows(i)(4).ToString
                dr1("No. Motor") = Dt2.Rows(i)(5).ToString
                dr1("Matricula") = Dt2.Rows(i)(6).ToString
                dr1("Color") = Dt2.Rows(i)(7).ToString
                dr1("Kilometraje") = Dt2.Rows(i)(8).ToString

                dr1("fecha") = ""
                dr1("Cantidad") = ""
                dr1("Código") = ""
                dr1("Descripcion") = ""
                dr1("TiepoVida") = ""
                dr1("Fechaexpira") = ""
                dr1("DiasRestantes") = ""

                TablaFull.Rows.Add(dr1)
            Else




                For j As Integer = 0 To Dt3.Rows.Count() - 1
                    Dim dr1 As DataRow
                    dr1 = TablaFull.NewRow()
                    idCliente = Integer.Parse(Dt2.Rows(i)(9).ToString)


                    dr1("idEquipo") = Dt2.Rows(i)(0).ToString
                    dr1("Nombre") = Dt2.Rows(i)(1).ToString
                    dr1("Marca") = Dt2.Rows(i)(2).ToString
                    dr1("Modelo") = Dt2.Rows(i)(3).ToString
                    dr1("No. Serie") = Dt2.Rows(i)(4).ToString
                    dr1("No. Motor") = Dt2.Rows(i)(5).ToString
                    dr1("Matricula") = Dt2.Rows(i)(6).ToString
                    dr1("Color") = Dt2.Rows(i)(7).ToString
                    dr1("Kilometraje") = Dt2.Rows(i)(8).ToString

                    'Componentes


                    If q.cuantosConsumibles(Dt3.Rows(j)(1), Dt3.Rows(j)(3)) > 0 Then
                        tablaConsumibles = q.consumibles(Dt3.Rows(j)(1), Dt3.Rows(j)(3)) 'asegurarse que se rdenen de mayor a menor la fehca
                        hoy = DateTime.ParseExact(tablaConsumibles.Rows(0)(0).ToString, "yyyy-MM-dd", Nothing)
                        diaFinal = hoy.AddDays(Integer.Parse(Dt3.Rows(j)(4).ToString))

                        dr1("fecha") = tablaConsumibles.Rows(0)(0).ToString
                        dr1("Cantidad") = tablaConsumibles.Rows(0)(1).ToString
                        q.buscarArticulo(tablaConsumibles.Rows(0)(2).ToString)
                        dr1("Código") = q.codigo
                        dr1("Descripcion") = q.descripcion
                        dr1("TiepoVida") = Dt3.Rows(j)(4).ToString
                        dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha
                        diasRestantes = DateDiff("d", conversionFecha(Date.Now()), conversionFecha(diaFinal))
                        If diasRestantes <= 0 Then
                            diasRestantes1 = "rojo"
                        Else
                            If diasRestantes < 4 Then
                                diasRestantes1 = "amarillo"
                            Else
                                diasRestantes1 = "blanco"
                            End If
                        End If
                        dr1("DiasRestantes") = diasRestantes1
                    Else

                        hoy = DateTime.ParseExact(Dt3.Rows(j)(5).ToString, "yyyy-MM-dd", Nothing)
                        diaFinal = hoy.AddDays(Integer.Parse(Dt3.Rows(j)(4).ToString))
                        dr1("fecha") = Dt3.Rows(j)(5).ToString
                        dr1("Cantidad") = Dt3.Rows(j)(2).ToString
                        q.buscarArticulo(Dt3.Rows(j)(3).ToString)
                        dr1("Código") = q.codigo
                        dr1("Descripcion") = q.descripcion
                        dr1("TiepoVida") = Dt3.Rows(j)(4).ToString
                        dr1("Fechaexpira") = conversionFecha(diaFinal) 'aqui hacer calculos de fecha
                        diasRestantes = DateDiff("d", conversionFecha(Date.Now()), conversionFecha(diaFinal))
                        If diasRestantes <= 0 Then
                            diasRestantes1 = "rojo"
                        Else
                            If diasRestantes < 4 Then
                                diasRestantes1 = "amarillo"
                            Else
                                diasRestantes1 = "blanco"
                            End If
                        End If
                        dr1("DiasRestantes") = diasRestantes1
                    End If

                    TablaFull.Rows.Add(dr1)
                Next
            End If
        Next


        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblconsumiblesTodos.xml")
        Return dataSet
    End Function

    Private Function HistorialEspecifico() As DataSet
        Dim dataSet As DataSet = New DataSet
        Try
            Dim P As New dbServiciosEventos(MySqlcon)
            Dim SE As New dbServiciosEventos(MySqlcon)
            Dim TablaFull As New DataTable
            Dim TablanumServicio As New DataTable
            Dim TablaServicios As New DataTable
            Dim pidServicio As Integer
            Dim estado As String
            TablanumServicio = P.ConsultaServiciosId(idEquipo).ToTable

            TablaFull.Columns.Add("id")
            TablaFull.Columns.Add("Servicio")
            TablaFull.Columns.Add("Clasificación")
            TablaFull.Columns.Add("Subclasificación")
            TablaFull.Columns.Add("Comentario")
            TablaFull.Columns.Add("Precio")
            TablaFull.Columns.Add("Minutos")
            'Detalles del servicio
            TablaFull.Columns.Add("detalleServicio")
            TablaFull.Columns.Add("fechae")
            TablaFull.Columns.Add("horae")
            TablaFull.Columns.Add("fechas")
            TablaFull.Columns.Add("horas")
            TablaFull.Columns.Add("estado")


            For i As Integer = 0 To TablanumServicio.Rows.Count() - 1
                pidServicio = Integer.Parse(TablanumServicio.Rows(i)(0).ToString)
                TablaServicios = SE.ConsultaTodosServicios(idEquipo, pidServicio).ToTable

                For j As Integer = 0 To TablaServicios.Rows.Count() - 1
                    Dim dr1 As DataRow
                    dr1 = TablaFull.NewRow()


                    dr1("id") = TablaServicios.Rows(j)(0).ToString
                    dr1("Servicio") = TablaServicios.Rows(j)(1).ToString
                    dr1("Clasificación") = TablaServicios.Rows(j)(2).ToString
                    dr1("Subclasificación") = TablaServicios.Rows(j)(3).ToString
                    dr1("Comentario") = TablaServicios.Rows(j)(4).ToString
                    dr1("Precio") = "$" + Format(Double.Parse(TablaServicios.Rows(j)(5).ToString), "0.00")
                    dr1("Minutos") = TablaServicios.Rows(j)(6).ToString
                    P.detallesServicio(pidServicio)
                    'detalles servicio
                    dr1("detalleServicio") = P.detalles
                    dr1("fechae") = P.fechae
                    dr1("horae") = P.horae
                    dr1("fechas") = P.fechas
                    dr1("horas") = P.horas
                    If P.estado.ToString = "1" Then
                        estado = "Listo"
                    Else
                        estado = "En taller"
                    End If

                    dr1("estado") = estado

                    TablaFull.Rows.Add(dr1)

                Next
            Next



            DataSet.Tables.Add(TablaFull)
            DataSet.WriteXmlSchema("tblHistorialEquipos.xml")


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        Return dataSet
    End Function

    Private Function HistorialTodos() As DataSet
        'Try
        Dim P As New dbServiciosEventos(MySqlcon)
        Dim D As New dbDetallesEquipo(MySqlcon)
        Dim SE As New dbServiciosEventos(MySqlcon)
        Dim TablaFull As New DataTable
        Dim TablanumServicio As New DataTable
        Dim TablaServicios As New DataTable
        Dim tablaEquipos As New DataTable
        Dim pidServicio As Integer
        Dim estado As String
        tablaEquipos = P.idEquipoConServicio().ToTable()
        'datos del equipo 
        TablaFull.Columns.Add("idEquipo")
        TablaFull.Columns.Add("nombre")
        TablaFull.Columns.Add("marca")
        TablaFull.Columns.Add("modelo")
        TablaFull.Columns.Add("matrícula")
        TablaFull.Columns.Add("noSerie")


        TablaFull.Columns.Add("id")
        TablaFull.Columns.Add("Servicio")
        TablaFull.Columns.Add("Clasificación")
        TablaFull.Columns.Add("Subclasificación")
        TablaFull.Columns.Add("Comentario")
        TablaFull.Columns.Add("Precio")
        TablaFull.Columns.Add("Minutos")
        'Detalles del servicio
        TablaFull.Columns.Add("detalleServicio")
        TablaFull.Columns.Add("fechae")
        TablaFull.Columns.Add("horae")
        TablaFull.Columns.Add("fechas")
        TablaFull.Columns.Add("horas")
        TablaFull.Columns.Add("estado")

        For k As Integer = 0 To tablaEquipos.Rows.Count() - 1
            idEquipo = tablaEquipos.Rows(k)(0)


            TablanumServicio = P.ConsultaServiciosId(idEquipo).ToTable
            For i As Integer = 0 To TablanumServicio.Rows.Count() - 1
                pidServicio = Integer.Parse(TablanumServicio.Rows(i)(0).ToString)
                TablaServicios = SE.ConsultaTodosServicios(idEquipo, pidServicio).ToTable

                For j As Integer = 0 To TablaServicios.Rows.Count() - 1
                    Dim dr1 As DataRow
                    dr1 = TablaFull.NewRow()

                    D.equipoDatos(idEquipo)

                    dr1("idEquipo") = idEquipo
                    dr1("nombre") = D.nombre
                    dr1("marca") = D.marca
                    dr1("modelo") = D.modelo
                    dr1("matrícula") = D.matricula
                    dr1("noSerie") = D.noserie

                    dr1("id") = TablaServicios.Rows(j)(0).ToString
                    dr1("Servicio") = TablaServicios.Rows(j)(1).ToString
                    dr1("Clasificación") = TablaServicios.Rows(j)(2).ToString
                    dr1("Subclasificación") = TablaServicios.Rows(j)(3).ToString
                    dr1("Comentario") = TablaServicios.Rows(j)(4).ToString
                    dr1("Precio") = "$" + Format(Double.Parse(TablaServicios.Rows(j)(5).ToString), "0.00")
                    dr1("Minutos") = TablaServicios.Rows(j)(6).ToString
                    P.detallesServicio(pidServicio)
                    'detalles servicio
                    dr1("detalleServicio") = P.detalles
                    dr1("fechae") = P.fechae
                    dr1("horae") = P.horae
                    dr1("fechas") = P.fechas
                    dr1("horas") = P.horas
                    If P.estado.ToString = "1" Then
                        estado = "Listo"
                    Else
                        estado = "En taller"
                    End If

                    dr1("estado") = estado

                    TablaFull.Rows.Add(dr1)

                Next
            Next
        Next

        Dim dataSet As DataSet = New DataSet
        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblHistorialEquiposTODOS.xml")


        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        'End Try
        Return dataSet
    End Function

    Private Sub CargarTecnicos()


        Dim P As New dbServicios(MySqlcon)

        tablaTecnicos = P.buscarTecnicos
        If tablaTecnicos.Rows.Count > 0 Then

            Dim dr1 As DataRow
            dr1 = tablaTecnicos.NewRow()
            dr1("idtecnico") = 0
            dr1("nombre") = "Todos"
            tablaTecnicos.Rows.Add(dr1)

            With cmbTecnico
                .DataSource = tablaTecnicos
                .DisplayMember = "nombre"
                .ValueMember = "nombre"
            End With
            cmbTecnico.Text = "Todos"
            ' idTecnico = 0
            btnImprimir.Enabled = True
        Else
            'If num = 2 Then
            MsgBox("El cliente no tiene técnicos registrados.", MsgBoxStyle.Critical, GlobalNombreApp)
            btnImprimir.Enabled = False
            'End If
        End If

    End Sub

    Private Sub CargarClasificacion()


        Dim P As New dbServicios(MySqlcon)

        tablaClas = P.buscarClasificaciones
        If tablaClas.Rows.Count > 0 Then
            Dim dr1 As DataRow
            dr1 = tablaClas.NewRow()
            dr1("idclasificacion") = 0
            dr1("nombre") = "Todos"
            tablaClas.Rows.Add(dr1)

            With cmbClasificacion
                .DataSource = tablaClas
                .DisplayMember = "nombre"
                .ValueMember = "nombre"
            End With
            cmbClasificacion.Text = "Todos"
            btnImprimir.Enabled = True
        Else
            'If num = 2 Then
            MsgBox("El cliente no tiene clasificaciones registrados.", MsgBoxStyle.Critical, GlobalNombreApp)
            btnImprimir.Enabled = False
            'End If
        End If

    End Sub

    Private Sub CargarSubClasificacion()


        Dim P As New dbServicios(MySqlcon)

        tablaSubClas = P.buscarSubClasificaciones
        If tablaSubClas.Rows.Count > 0 Then
            Dim dr1 As DataRow
            dr1 = tablaSubClas.NewRow()
            dr1("idclasificacion2") = 0
            dr1("nombre") = "Todos"
            tablaSubClas.Rows.Add(dr1)
            With cmbSubClasificacion
                .DataSource = tablaSubClas
                .DisplayMember = "nombre"
                .ValueMember = "nombre"
            End With
            cmbSubClasificacion.Text = "Todos"
            btnImprimir.Enabled = True
        Else
            'If num = 2 Then
            MsgBox("El cliente no tiene Subclasificaciones registrados.", MsgBoxStyle.Critical, GlobalNombreApp)
            btnImprimir.Enabled = False
            'End If
        End If

    End Sub

    Private Sub cmbTecnico_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTecnico.SelectedIndexChanged
        If bandera = True Then

            If tablaTecnicos.Rows.Count > 0 Then
                Dim i As Integer = 0

                i = cmbTecnico.SelectedIndex()
                If cmbTecnico.Text = "Todos" Then
                    idTecnico = 0
                Else
                    idTecnico = Integer.Parse(tablaTecnicos.Rows(i)(0).ToString())
                End If


            End If

        End If
    End Sub

    Private Sub cmbClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbClasificacion.SelectedIndexChanged
        If bandera = True Then

            If tablaClas.Rows.Count > 0 Then
                Dim i As Integer = 0

                i = cmbClasificacion.SelectedIndex()
                If cmbClasificacion.Text = "Todos" Then
                    idClasificacion = 0
                Else
                    idClasificacion = Integer.Parse(tablaClas.Rows(i)(0).ToString())
                End If


            End If

        End If
    End Sub

    Private Sub cmbSubClasificacion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSubClasificacion.SelectedIndexChanged
        If bandera = True Then

            If tablaSubClas.Rows.Count > 0 Then
                Dim i As Integer = 0

                i = cmbSubClasificacion.SelectedIndex()
                If cmbSubClasificacion.Text = "Todos" Then
                    idSubClasificacion = 0
                Else
                    idSubClasificacion = Integer.Parse(tablaSubClas.Rows(i)(0).ToString())
                End If


            End If

        End If
    End Sub


    Private Function HistoriaTecnico() As DataSet
        Dim dataSet As DataSet = New DataSet
        '  Try
        Dim P As New dbServiciosEventos(MySqlcon)
        ' Dim SE As New dbServiciosEventos(MySqlcon)
        Dim TablaFull As New DataTable
        Dim Tabla As New DataTable
        'Dim TablanumServicio As New DataTable
        'Dim TablaServicios As New DataTable
        'Dim pidServicio As Integer
        'Dim estado As String
        Tabla = P.buscarServiciosTecnico(idTecnico, idClasificacion, idSubClasificacion).ToTable()
        ' TablanumServicio = P.ConsultaServiciosId(idEquipo).ToTable

        TablaFull.Columns.Add("idEvento")
        TablaFull.Columns.Add("idServicio")
        TablaFull.Columns.Add("Clasificación")
        TablaFull.Columns.Add("Subclasificación")
        TablaFull.Columns.Add("Comentario")
        TablaFull.Columns.Add("Tiempo")
        TablaFull.Columns.Add("Precio")
        TablaFull.Columns.Add("Equipo")
        TablaFull.Columns.Add("Tecnico")
     


        For i As Integer = 0 To Tabla.Rows.Count() - 1
            Dim dr1 As DataRow
            dr1 = TablaFull.NewRow()


            dr1("idEvento") = Tabla.Rows(i)(0).ToString
            dr1("idServicio") = Tabla.Rows(i)(1).ToString
            dr1("Clasificación") = P.buscarClasificacionNombre(Tabla.Rows(i)(2).ToString)
            dr1("Subclasificación") = P.buscarSubClasificacionNombre(Tabla.Rows(i)(3).ToString)
            dr1("Comentario") = Tabla.Rows(i)(4).ToString
            dr1("Tiempo") = Tabla.Rows(i)(5).ToString
            dr1("Precio") = Format(Double.Parse(Tabla.Rows(i)(6).ToString), "0.00")
            dr1("Equipo") = P.buscarEquipoNombre(Tabla.Rows(i)(7).ToString)
            dr1("Tecnico") = P.buscarTecnicoNombre(Tabla.Rows(i)(8).ToString)

            TablaFull.Rows.Add(dr1)

        Next


        dataSet.Tables.Add(TablaFull)
        dataSet.WriteXmlSchema("tblHistorialTecnicos.xml")
        Return dataSet


    End Function

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class