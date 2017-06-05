Public Class frmInventarioReportes
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim IdsSucursales As New elemento
    Dim IdsSucursales2 As New elemento
    Dim IdsSucursales3 As New elemento
    Dim IdsAlmacenes As New elemento
    Dim IdsAlmacenes2 As New elemento
    Dim IdsConceptos As New elemento
    Dim IdInventario As Integer
    Dim IdVariante As Integer
    Dim IdCliente As Integer
    Dim ConsultaOn As Boolean
    Dim EncontroClas As Boolean = True
    Dim TipoDeCambio As Double
    Dim IdsTiposSuc As New elemento
    Private Sub frmVentasReportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        CheckBox1.Checked = My.Settings.reportecostoultimo
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button1.Enabled = True
            Exit Sub
        End If
        ListBox1.Items.Add("Inventario.")
        ListBox1.Items.Add("Inventario a fecha.")
        ListBox1.Items.Add("Inventario por clasificación.")
        ListBox1.Items.Add("Inventario a fecha por clasificación.")
        ListBox1.Items.Add("Inventario por almacen.")
        ListBox1.Items.Add("Inventario Equivalencia.")
        ListBox1.Items.Add("Costos.")
        ListBox1.Items.Add("Movimientos.")
        ListBox1.Items.Add("Salidas por cliente.")
        ListBox1.Items.Add("Conciliación.")
        ListBox1.Items.Add("Conciliación Resumido.")
        ListBox1.Items.Add("Máximos y mínimos.")
        ListBox1.Items.Add("Inventario Apartado.")
        ListBox1.Items.Add("Lotes.")
        ListBox1.Items.Add("Aduana.")
        ListBox1.Items.Add("Pedidos.")
        ListBox1.Items.Add("Control de entrega de mercancia.")
        ListBox1.Items.Add("Control de inventario.")
        ListBox1.Items.Add("Entregas.")
        'ListBox1.Items.Add("Facturas Entregas de mercancías.")
        ConsultaOn = False
        ComboBox4.Items.Add("Todos")
        ComboBox4.Items.Add("Entrada")
        ComboBox4.Items.Add("Salida")
        ComboBox4.Items.Add("Inventario Físico")
        ComboBox4.Items.Add("Traspaso")
        ComboBox4.Items.Add("Inventario Inicial")
        ComboBox10.Items.Add("Pesos")
        ComboBox10.Items.Add("Moneda Original")
        ComboBox10.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        LlenaCombos("tblsucursalestipos", ComboBox15, "nombre", "nombrec", "idtipo", IdsTiposSuc, , "Todos")
        LlenaCombos("tblalmacenes", ComboBox5, "nombre", "nombret", "idalmacen", IdsAlmacenes2, "idalmacen<>1", "Todos")
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        LlenaCombos("tblsucursales", ComboBox9, "nombre", "nombret", "idsucursal", IdsSucursales2, , "Todas")
        ComboBox1.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas", "nombre")
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        TipoDeCambio = CM.Cantidad
        ConsultaOn = True
        If My.Settings.costoenreporteinventario = 1 Then
            CheckBox6.Checked = True
        Else
            CheckBox6.Checked = False
        End If
        If My.Settings.repultimocosto Then
            CheckBox8.Checked = True
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
            LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")
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
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")
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

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim V As New dbInventario(MySqlcon)
            'Dim O As New dbOpciones(MySqlcon)
            Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClas3 As Integer
            Dim TipoCosteo As Byte
            Dim Descon As Byte = 0
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
            If CheckBox7.Checked Then
                Descon = 1
            End If
            If RadioButton1.Checked Then TipoCosteo = 1
            If RadioButton2.Checked Then TipoCosteo = 2
            If RadioButton3.Checked Then TipoCosteo = 3
            'ListBox1.Items.Add("Inventario.")
            'ListBox1.Items.Add("Inventario Equivalencia.")
            'ListBox1.Items.Add("Costos.")
            'ListBox1.Items.Add("Movimientos.")
            'ListBox1.Items.Add("Conciliación.")
            'ListBox1.Items.Add("Conciliación Resumido.")
            Dim Descontinuados As String = ""
            If CheckBox2.Checked Then
                Descontinuados = " Solo descontinuados"
            End If
            Select Case ListBox1.Text
                Case "Inventario."
                    If CheckBox4.Checked Then
                        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        Rep = New repInventarioE
                        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                        Rep.SetDataSource(V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, CheckBox12.Checked, Descon, checkNombre.Checked, CheckBox2.Checked))
                        Rep.SetParameterValue("Encabezado", S.Nombre)
                        Rep.SetParameterValue("Almacen", ComboBox8.Text)
                        Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                        Rep.SetParameterValue("titulo", "Reporte de Inventario" + Descontinuados)
                        Rep.SetParameterValue("quitaceros", False)
                        Dim RV As New frmReportes(Rep, False)
                        RV.Show()
                    Else
                        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        Rep = New repInventarioEB
                        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                        Rep.SetDataSource(V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, CheckBox12.Checked, Descon, checkNombre.Checked, CheckBox2.Checked))
                        Rep.SetParameterValue("Encabezado", S.Nombre)
                        Rep.SetParameterValue("Almacen", ComboBox8.Text)
                        Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                        If CheckBox6.Checked Then
                            Rep.SetParameterValue("concosto", 1)
                        Else
                            Rep.SetParameterValue("concosto", 0)
                        End If
                        Rep.SetParameterValue("titulo", "Reporte de Inventario" + Descontinuados)
                        Rep.SetParameterValue("quitarceros", False)
                        Dim RV As New frmReportes(Rep, False)
                        RV.Show()
                    End If

                Case "Inventario a fecha."
                    If CheckBox4.Checked Then
                        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        Rep = New repInventarioE
                        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                        Rep.SetDataSource(V.ReporteInventarioAFecha(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox9.Checked, CheckBox10.Checked, DateTimePicker1.Value.ToString("yyyy/MM/dd"), Descon, checkNombre.Checked))
                        Rep.SetParameterValue("Encabezado", S.Nombre)
                        Rep.SetParameterValue("Almacen", ComboBox8.Text)
                        Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                        Rep.SetParameterValue("titulo", "Reporte de Inventario a la fecha: " + DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                        Rep.SetParameterValue("quitaceros", CheckBox9.Checked)
                        Dim RV As New frmReportes(Rep, False)
                        RV.Show()
                    Else
                        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        Rep = New repInventarioEB
                        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                        Rep.SetDataSource(V.ReporteInventarioAFecha(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox9.Checked, CheckBox10.Checked, DateTimePicker1.Value.ToString("yyyy/MM/dd"), Descon, checkNombre.Checked))
                        Rep.SetParameterValue("Encabezado", S.Nombre)
                        Rep.SetParameterValue("Almacen", ComboBox8.Text)
                        Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                        If CheckBox6.Checked Then
                            Rep.SetParameterValue("concosto", 1)
                        Else
                            Rep.SetParameterValue("concosto", 0)
                        End If
                        'Rep.SetParameterValue("titulo", "Reporte de Iventario")
                        Rep.SetParameterValue("titulo", "Reporte de Inventario a la fecha: " + DateTimePicker1.Value.ToString("yyyy/MM/dd"))
                        Rep.SetParameterValue("quitarceros", CheckBox9.Checked)
                        Dim RV As New frmReportes(Rep, False)
                        RV.Show()
                    End If
                Case "Inventario por clasificación."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repInventarioClasificacion
                    'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    Rep.SetDataSource(V.ReporteInventarioPorClasificacion(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, "", False, Descon, checkNombre.Checked))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    If CheckBox6.Checked Then
                        Rep.SetParameterValue("concosto", 0)
                    Else
                        Rep.SetParameterValue("concosto", 1)
                    End If
                    Rep.SetParameterValue("titulo", "Reporte de Inventario por Clasificación")
                    Rep.SetParameterValue("quitaceros", False)
                    Dim Filtros As String
                    Filtros = "Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox8.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Inventario a fecha por clasificación."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repInventarioClasificacion
                    Rep.SetDataSource(V.ReporteInventarioPorClasificacion(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, DateTimePicker1.Value.ToString("yyyy/MM/dd"), True, Descon, checkNombre.Checked))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    If CheckBox6.Checked Then
                        Rep.SetParameterValue("concosto", 0)
                    Else
                        Rep.SetParameterValue("concosto", 1)
                    End If
                    Rep.SetParameterValue("titulo", "Reporte de Inventario por Clasificación a Fecha")
                    Rep.SetParameterValue("quitaceros", False)
                    Dim Filtros As String
                    Filtros = "Inventario al:" + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox8.Text
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Inventario por almacen."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repInventarioAlmacenes
                    'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    Rep.SetDataSource(V.ReporteInventarioporAlmacen(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, Descon, checkNombre.Checked))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Dim Filtros As String = ""
                    If IdsSucursales.Valor(ComboBox1.SelectedIndex) > 0 Then Filtros += "Sucursal: " + ComboBox1.Text
                    If IdsAlmacenes.Valor(ComboBox8.SelectedIndex) > 0 Then Filtros += " Alamcen: " + ComboBox8.Text

                    If idClas > 0 Or idClas2 > 0 Or idClas3 > 0 Then
                        Filtros += " Clasificación: Nivel1: " + ComboBox3.Text + " Nivel2: " + ComboBox6.Text + " Nivel3: " + ComboBox7.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Rep.SetParameterValue("sincosto", CheckBox6.Checked)
                    Rep.SetParameterValue("soloexistencia", CheckBox9.Checked)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()

                Case "Inventario Equivalencia."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repInventarioEE
                    'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    Rep.SetDataSource(V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, CheckBox12.Checked, Descon, checkNombre.Checked, CheckBox2.Checked))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("Almacen", ComboBox8.Text + Descontinuados)
                    Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                    If CheckBox6.Checked Then
                        Rep.SetParameterValue("concosto", 1)
                    Else
                        Rep.SetParameterValue("concosto", 0)
                    End If

                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Costos."
                    My.Settings.reportecostoultimo = CheckBox1.Checked
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repInventarioCostos
                    Rep.SetDataSource(V.ReporteCostos(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, TipoCosteo, TipoDeCambio, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, Descon, checkNombre.Checked))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("tipocosto", CheckBox1.Checked)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Movimientos."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim Ms As New dbMovimientos(MySqlcon)
                    'Ms.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsAlmacenes2.Valor(ComboBox5.SelectedIndex), IdsConceptos.Valor(ComboBox2.SelectedIndex), ComboBox4.SelectedIndex - 1)

                    Rep = New repMovimientos
                    Rep.SetDataSource(Ms.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsAlmacenes2.Valor(ComboBox5.SelectedIndex), IdsConceptos.Valor(ComboBox2.SelectedIndex), ComboBox4.SelectedIndex - 1, IdInventario, idClas, idClas2, idClas3, IdsTiposSuc.Valor(ComboBox15.SelectedIndex), ComboBox10.SelectedIndex, IdCliente))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("fechas", "Período del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd"))

                    Dim Filtros As String
                    Filtros = "Sucursal: " + ComboBox1.Text + " Almacen: " + ComboBox8.Text + " Tipo: " + ComboBox4.Text + " Concepto: " + ComboBox2.Text
                    If ComboBox4.SelectedIndex = 4 Then
                        Filtros += " Almacen Destino: " + ComboBox5.Text
                    End If
                    If ComboBox10.SelectedIndex = 0 Then
                        Filtros += " En Pesos"
                    Else
                        Filtros += " En Moneda Original"
                    End If
                    If IdCliente > 0 Then
                        Filtros += " Cliente: " + TextBox2.Text
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Salidas por cliente."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim Ms As New dbMovimientos(MySqlcon)
                    'Ms.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsAlmacenes2.Valor(ComboBox5.SelectedIndex), IdsConceptos.Valor(ComboBox2.SelectedIndex), ComboBox4.SelectedIndex - 1)
                    Rep = New repSalidasporCliente
                    Rep.SetDataSource(Ms.ReporteMovimientosxCliente(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdsAlmacenes2.Valor(ComboBox5.SelectedIndex), IdsConceptos.Valor(ComboBox2.SelectedIndex), ComboBox4.SelectedIndex - 1, IdInventario, idClas, idClas2, idClas3, IdCliente))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("fechas", "Período del " + Format(DateTimePicker1.Value, "yyyy-MMMM-dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Conciliación."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim Ms As New dbInventario(MySqlcon)
                    'Ms.ReporteAnalisisb(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex))

                    Rep = New repInventarioAnalisisB
                    Rep.SetDataSource(Ms.ReporteAnalisisb(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), idClas, idClas2, idClas3, Descon))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("Fechas", "Período del " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Dim Filtros As String
                    Filtros = "Sucursal: " + ComboBox1.Text
                    If idClas > 0 Or idClas2 > 0 Or idClas3 > 0 Then
                        Filtros += " Clasificación: Nivel1: " + ComboBox3.Text + " Nivel2: " + ComboBox6.Text + " Nivel3: " + ComboBox7.Text
                    End If
                    Rep.SetParameterValue("filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Conciliación Resumido."
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Dim Ms As New dbInventario(MySqlcon)
                    'Ms.ReporteAnalisisC(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdInventario, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    Rep = New repInventarioAnalisisC
                    Rep.SetDataSource(Ms.ReporteAnalisisC(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdInventario, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), Descon))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("Fechas", Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd"))
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Máximos y mínimos."
                    'If CheckBox4.Checked Then
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repInventarioMaxMin
                    Rep.SetDataSource(V.ReporteMaxMin(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, CheckBox9.Checked, CheckBox10.Checked, Descon, checkNombre.Checked))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("Menoscero", CheckBox11.Checked)
                    'Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Pedidos."

                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    Rep = New repInventarioPedidos
                    Rep.SetDataSource(V.ReportePedidos(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, CheckBox13.Checked, IdsSucursales2.Valor(ComboBox9.SelectedIndex)))
                    Rep.SetParameterValue("Encabezado", S.Nombre)
                    Rep.SetParameterValue("Filtros", "")
                    Rep.SetParameterValue("idsucursal", IdsSucursales.Valor(ComboBox1.SelectedIndex))
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Inventario Apartado."
                    If CheckBox4.Checked Then
                        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        Rep = New repInventarioE
                        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                        Rep.SetDataSource(V.ReporteInventarioApartado(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, Descon, checkNombre.Checked))
                        Rep.SetParameterValue("Encabezado", S.Nombre)
                        Rep.SetParameterValue("Almacen", ComboBox8.Text)
                        Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                        Rep.SetParameterValue("titulo", "Reporte de Iventario Apartado")
                        Rep.SetParameterValue("quitarceros", True)
                        Dim RV As New frmReportes(Rep, False)
                        RV.Show()
                    Else
                        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        Rep = New repInventarioEB
                        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
                        Rep.SetDataSource(V.ReporteInventarioApartado(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex), CheckBox8.Checked, Descon, checkNombre.Checked))
                        Rep.SetParameterValue("Encabezado", S.Nombre)
                        Rep.SetParameterValue("Almacen", ComboBox8.Text)
                        Rep.SetParameterValue("Sucursal", ComboBox1.Text)
                        Rep.SetParameterValue("quitarceros", True)
                        If CheckBox6.Checked Then
                            Rep.SetParameterValue("concosto", 1)
                        Else
                            Rep.SetParameterValue("concosto", 0)
                        End If
                        Rep.SetParameterValue("titulo", "Reporte de Iventario Apartado")
                        Dim RV As New frmReportes(Rep, False)
                        RV.Show()
                    End If
                Case "Lotes."
                    Dim Lote As New dblotes(MySqlcon)
                    Lote.reporteInventarioLotes(IdInventario, idClas, idClas2, idClas3, CheckBox9.Checked, Descon)
                Case "Aduana."
                    Dim Ad As New dbInventarioAduana(MySqlcon)
                    Ad.reporteInventarioAduanas(IdInventario, idClas, idClas2, idClas3, CheckBox9.Checked, Descon)
                Case "Control de inventario."
                    Dim m As New dbMovimientos(MySqlcon)
                    Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rep = New repInventarioMercancia
                    rep.SetDataSource(m.reporteTraspasosMercancia(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, IdsSucursales2.Valor(ComboBox9.SelectedIndex)))
                    Dim filtros As String = ""
                    filtros += "Fecha: " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " al: " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                    filtros += " Sucursal: " + ComboBox1.Text
                    If ComboBox9.SelectedIndex > 0 Then
                        filtros += " + " + ComboBox9.Text
                    End If
                    filtros += "  Almacen: " + ComboBox8.Text
                    rep.SetParameterValue("filtros", filtros)
                    rep.SetParameterValue("titulo", "Control de entrega de mercancia detallado.")
                    rep.SetParameterValue("encabezado", GlobalNombreEmpresa)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                Case "Control de entrega de mercancia."
                    Dim m As New dbMovimientos(MySqlcon)
                    Dim rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    rep = New repInventarioEntregas
                    rep.SetDataSource(m.controlEntregas(DateTimePicker1.Value.ToString("yyyy/MM/dd"), DateTimePicker2.Value.ToString("yyyy/MM/dd"), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario))
                    rep.SetParameterValue("encabezado", GlobalNombreEmpresa)
                    rep.SetParameterValue("titulo", "Control de entrega a agentes de ventas.")
                    Dim filtros As String = ""
                    filtros += "Fechas: " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                    filtros += "  Sucursal: " + ComboBox1.Text
                    filtros += "  Vendedor: " + ComboBox8.Text
                    rep.SetParameterValue("filtros", filtros)
                    'rep.SetParameterValue("conFacturas", False)
                    Dim RV As New frmReportes(rep, False)
                    RV.Show()
                   
                Case "Entregas."
                    Dim db As New dbMovimientos(MySqlcon)
                    Dim r As New repEntregasPorCliente
                    r.SetDataSource(db.ReporteEntregas(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCliente, IdsSucursales.Valor(ComboBox1.SelectedIndex)))
                    Dim filtros As String = ""
                    filtros += "Fechas: " + DateTimePicker1.Value.ToString("yyyy/MM/dd") + " al " + DateTimePicker2.Value.ToString("yyyy/MM/dd")
                    filtros += "  Sucursal: " + ComboBox1.Text
                    If IdCliente <= 0 Then
                        filtros += "  Cliente: Todos"
                    Else
                        filtros += "  Cliente: " + TextBox2.Text
                    End If
                    r.SetParameterValue("filtros", filtros)
                    r.SetParameterValue("Encabezado", S.Nombre)
                    Dim f As New frmReportes(r, False)
                    f.Show()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0, False, False, False, False) Then
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


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
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
    
  
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
        LlenaCombos("tblinventarioconceptos", ComboBox2, "nombre", "nombret", "idconcepto", IdsConceptos, "idsucursal=" + IdsSucursales.Valor(ComboBox1.SelectedIndex).ToString, "Todos")
    End Sub

    'Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
    '    If CheckBox5.Checked Then
    '        CheckBox1.Checked = False
    '        CheckBox2.Checked = False
    '        CheckBox3.Checked = False
    '        Label1.Visible = False
    '        ComboBox2.Visible = False
    '        Label2.Visible = False
    '        ComboBox4.Visible = False
    '        Label3.Visible = False
    '        ComboBox5.Visible = False
    '        CheckBox7.Checked = False
    '    End If
    'End Sub

   

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            My.Settings.costoenreporteinventario = 1
        Else
            My.Settings.costoenreporteinventario = 0
        End If
    End Sub

    'Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If CheckBox7.Checked Then
    '        CheckBox1.Checked = False
    '        CheckBox2.Checked = False
    '        CheckBox3.Checked = False
    '        CheckBox5.Checked = False
    '        Label1.Visible = False
    '        ComboBox2.Visible = False
    '        Label2.Visible = False
    '        ComboBox4.Visible = False
    '        Label3.Visible = False
    '        ComboBox5.Visible = False
    '    End If
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.Text = "Movimientos." Then
            Label1.Visible = True
            ComboBox2.Visible = True
            Label2.Visible = True
            ComboBox4.Visible = True
            Label3.Visible = True
            ComboBox5.Visible = True
            Label9.Visible = True
            ComboBox10.Visible = True
        Else
            Label1.Visible = False
            ComboBox2.Visible = False
            Label2.Visible = False
            ComboBox4.Visible = False
            Label3.Visible = False
            ComboBox5.Visible = False
            Label9.Visible = False
            ComboBox10.Visible = False
        End If
        If ListBox1.Text = "Control de inventario." Then
            Label8.Visible = True
            ComboBox9.Visible = True
            Label8.Text = "Sucursal 2:"
        Else
            If ListBox1.Text = "Pedidos." Then
                Label8.Visible = True
                ComboBox9.Visible = True
                Label8.Text = "Solicitante:"
            Else
                Label8.Visible = False
                ComboBox9.Visible = False
            End If
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked Then
            My.Settings.repultimocosto = True
        Else
            My.Settings.repultimocosto = False
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

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub

   
End Class