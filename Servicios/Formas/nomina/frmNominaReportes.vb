Public Class frmNominaReportes
    Dim IdsTipos As New elemento
    Dim IdsSucursales As New elemento
    Dim IdTrabajador As Integer
    Dim ConsultaOn As Boolean = True
    Private Sub cmbVariante_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVariante.SelectedIndexChanged
        If cmbVariante.SelectedIndex = 0 Then
            LlenaCombos("tblpercepciones", ComboBox8, "concat(clave,' ',descripcion)", "con", "idpercepcion", IdsTipos, "idpercepcion=1000", "Todos", "clave")
        End If
        If cmbVariante.SelectedIndex = 1 Then
            LlenaCombos("tblpercepciones", ComboBox8, "concat(clave,' ',descripcion)", "con", "idpercepcion", IdsTipos, "visible=1", "Todos", "clave")
        End If
        If cmbVariante.SelectedIndex = 2 Then
            LlenaCombos("tbldeducciones", ComboBox8, "concat(clave,' ',descripcion)", "con", "iddeduccion", IdsTipos, , "Todos", "clave")
        End If

    End Sub

    Private Sub frmNominaReportes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales, , "Todas")
        cmbVariante.Items.Add("Todos")
        cmbVariante.Items.Add("Percepción")
        cmbVariante.Items.Add("Deducción")
        cmbVariante.SelectedIndex = 0
        ListBox1.Items.Add("Nóminas.")
        ListBox1.Items.Add("Nóminas por concepto.")
        ListBox1.Items.Add("Nóminas por concepto concentrado.")
        ListBox1.Items.Add("Trabajadores.")
        ListBox1.Items.Add("Por fecha de pago")
        ListBox1.Items.Add("Por rango de fechas")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim Filtros As String
        Try
            'ListBox1.Items.Add("Nóminas.")
            'ListBox1.Items.Add("Nóminas por concepto.")
            'ListBox1.Items.Add("Trabajadores.")
            'Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)

            Select Case ListBox1.Text
                Case "Nóminas."
                    Dim N As New dbNominas(MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'V.Reporte(TipoCosteo, IdsSucursales.Valor(ComboBox1.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdCliente)
                    Rep = New repNominas
                    Rep.SetDataSource(N.Reporte(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdTrabajador, CheckBox10.Checked, radioFecha.Checked, radioPeriodo.Checked, radioRango.Checked))
                    Rep.SetParameterValue("Empresa", GlobalNombreEmpresa)
                    Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
                    If IdTrabajador <> 0 Then
                        Filtros += " Trabajador: " + TextBox2.Text
                    Else
                        Filtros += " Trabajador: TODOS"
                    End If
                    If CheckBox10.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Nóminas por concepto."
                    Dim N As New dbNominas(MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'N.ReportexConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdTrabajador, CheckBox10.Checked, cmbVariante.SelectedIndex, IdsTipos.Valor(ComboBox8.SelectedIndex))
                    Rep = New repNominasxConcepto
                    Rep.SetDataSource(N.ReportexConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdTrabajador, CheckBox10.Checked, cmbVariante.SelectedIndex, IdsTipos.Valor(ComboBox8.SelectedIndex), radioFecha.Checked, radioPeriodo.Checked, radioRango.Checked))
                        Rep.SetParameterValue("Empresa", GlobalNombreEmpresa)
                        Filtros = "Fechas: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " al " + Format(DateTimePicker2.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
                
                    If IdTrabajador <> 0 Then
                        Filtros += " Trabajador: " + TextBox2.Text
                    Else
                        Filtros += " Trabajador: TODOS"
                    End If
                    If IdsTipos.Valor(ComboBox8.SelectedIndex) > 0 Then
                        Filtros += " Concepto: " + ComboBox8.Text
                    End If
                    If CheckBox10.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()
                Case "Nóminas por concepto concentrado."
                    Dim N As New dbNominas(MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'N.ReportexConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdTrabajador, CheckBox10.Checked, cmbVariante.SelectedIndex, IdsTipos.Valor(ComboBox8.SelectedIndex))
                    If CheckBox1.Checked = False Then
                        Rep = New repNominasxConceptoCon
                    Else
                        Rep = New repNominasxConceptoCon2
                    End If
                    Rep.SetDataSource(N.ReportexConcepto(Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox1.SelectedIndex), IdTrabajador, CheckBox10.Checked, cmbVariante.SelectedIndex, IdsTipos.Valor(ComboBox8.SelectedIndex), radioFecha.Checked, radioPeriodo.Checked, radioRango.Checked))
                    Rep.SetParameterValue("Empresa", GlobalNombreEmpresa)
                    Filtros = "Fecha: " + Format(DateTimePicker1.Value, "yyyy/MM/dd") + " Sucursal: " + ComboBox1.Text
                    If IdTrabajador <> 0 Then
                        Filtros += " Trabajador: " + TextBox2.Text
                    Else
                        Filtros += " Trabajador: TODOS"
                    End If
                    If IdsTipos.Valor(ComboBox8.SelectedIndex) > 0 Then
                        Filtros += " Concepto: " + ComboBox8.Text
                    End If
                    If CheckBox10.Checked Then
                        Filtros += " SOLO CANCELADAS"
                    End If
                    Rep.SetParameterValue("Filtros", Filtros)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()

                Case "Trabajadores."
                    Dim N As New dbTrabajadores(MySqlcon)
                    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
                    'N.Reporte()
                    Rep = New repTrabajadores
                    Rep.SetDataSource(N.Reporte())
                    Rep.SetParameterValue("Empresa", GlobalNombreEmpresa)
                    Dim RV As New frmReportes(Rep, False)
                    RV.Show()

            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Trabajador, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'If B.Cliente.DireccionFiscal = 0 Then
            TextBox2.Text = B.Trabajador.Nombre
            IdTrabajador = B.Trabajador.ID
            ConsultaOn = False
            TextBox1.Text = B.Trabajador.NumeroEmpleado
            ConsultaOn = True
        End If
    End Sub
    Private Sub BuscaTrabajador()
        Try
            If ConsultaOn Then
                Dim c As New dbTrabajadores(MySqlcon)
                If c.BuscaTrabajador(TextBox1.Text) Then
                    'If c.DireccionFiscal = 0 Then
                    TextBox2.Text = c.Nombre
                    IdTrabajador = c.ID
                Else
                    TextBox2.Text = ""
                    IdTrabajador = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaTrabajador()
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8.SelectedIndexChanged

    End Sub
End Class