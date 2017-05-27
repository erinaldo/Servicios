Public Class frmClientesEquipos

    Public IdEquipo As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdCliente As Integer
    Dim TipoAlta As Integer
    Dim tipoEquipo As Integer '1 para clientes - 2 para sucursales
    Dim idSucursal As Integer
    'dateadd
    Public Sub New(ByVal pIdCliente As Integer, ByVal pTipoAlta As Integer, ByVal pTipoEquipo As Integer)


        InitializeComponent()
        IdCliente = pIdCliente
        TipoAlta = pTipoAlta
        tipoEquipo = pTipoEquipo
        'Me.idSucursal = idSucursal


    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox6.Text = "0"
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox7.Text = ""
            TextBox8.Text = ""
            chck_enviar.Checked = False
            fechaEnvio.Text = Today
            btnHistorial.Enabled = False
            btnDetalles.Enabled = False
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox6.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ConsultaOn = True
            btnReporte.Enabled = False
            Consulta()
            btnHistorial.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If tipoEquipo = 1 Then
                    'Equipos de Clientes
                    If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                    Dim P As New dbClientesEquipos(MySqlcon)
                    If tipoEquipo = 2 Then
                        DataGridView1.DataSource = P.ConsultaSuc(IdCliente, TextBox1.Text + TextBox7.Text)
                    Else
                        DataGridView1.DataSource = P.Consulta(IdCliente, TextBox1.Text + TextBox7.Text)
                    End If
                    DataGridView1.Columns(0).Visible = False
                    DataGridView1.Columns(1).HeaderText = "Nombre"
                    DataGridView1.Columns(2).HeaderText = "Matrícula"
                    DataGridView1.Columns(3).HeaderText = "No. Serie"
                    DataGridView1.Columns(1).Width = 200
                    DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                Else
                    'Equipos de Sucursales
                    If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                    Dim P As New dbClientesEquipos(MySqlcon)
                    DataGridView1.DataSource = P.ConsultaSuc(IdCliente, TextBox1.Text + TextBox7.Text)
                    DataGridView1.Columns(0).Visible = False
                    DataGridView1.Columns(1).HeaderText = "Nombre"
                    DataGridView1.Columns(2).HeaderText = "Matrícula"
                    DataGridView1.Columns(3).HeaderText = "No. Serie"
                    DataGridView1.Columns(1).Width = 200
                    DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Consulta()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbClientesEquipos(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            Dim estado As Integer = 0
            
            If chck_enviar.Checked = True Then
                estado = 1
            Else
                estado = 0
            End If
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar una descripción al equipo."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox6.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " El kilometraje debe ser un valor numérico."
                TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClientesEquiposGuardar, PermisosN.Secciones.Servicios) = False) And Button1.Text = "Guardar" Then
                NoErrores = False
                MensajeError += vbCrLf + "No tiene permiso para realizar esta operación."
            End If
            If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClientesEquiposModificar, PermisosN.Secciones.Servicios) = False) And Button1.Text <> "Guardar" Then
                NoErrores = False
                MensajeError += vbCrLf + "No tiene permiso para realizar esta operación."
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    fechaEnvio.Value = fechaEnvio.Value.AddMonths(6)
                    'MsgBox("Fecha a enviar: " + fechaEnvio.Value.ToString("yyyy/MM/dd"))
                    If tipoEquipo = 1 Then
                        'cliente equiios
                        IdEquipo = P.Guardar(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox8.Text, TextBox7.Text, TextBox5.Text, TextBox6.Text, IdCliente, fechaEnvio.Value.ToString("yyyy/MM/dd"), estado)
                    Else
                        'sucursal equipos
                        IdEquipo = P.GuardarSucursal(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox8.Text, TextBox7.Text, TextBox5.Text, TextBox6.Text, IdCliente)

                    End If

                    btnDetalles.Enabled = True
                    PopUp("Guardado", 90)

                    If TipoAlta = 1 Then
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                    End If
                    Nuevo()
                    btnHistorial.Enabled = True
                    btnReporte.Enabled = True
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        If tipoEquipo = 1 Then

                            P.Modificar(IdEquipo, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox8.Text, TextBox7.Text, TextBox5.Text, TextBox6.Text, fechaEnvio.Value.ToString("yyyy/MM/dd"), estado)
                        Else
                            P.ModificarSucursal(IdEquipo, TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox8.Text, TextBox7.Text, TextBox5.Text, TextBox6.Text)
                        End If

                        PopUp("Modificado", 90)
                        Nuevo()
                        btnReporte.Enabled = True
                        btnHistorial.Enabled = True
                    End If
                End If
                TextBox1.Focus()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosClasEliminar, PermisosN.Secciones.Servicios) = True) Then
            Try
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbClientesEquipos(MySqlcon)
                    If tipoEquipo = 1 Then
                        'Equipos Clientes
                        P.Eliminar(IdEquipo)
                    Else
                        'Equipos sucursal
                        P.EliminarSucursal(IdEquipo)
                    End If

                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox1.Focus()
                End If
            Catch exm As MySql.Data.MySqlClient.MySqlException
                If exm.ErrorCode = -2147467259 Then
                    MsgBox("No se puede eliminar este cliente debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
                Else
                    MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatos()
        btnHistorial.Enabled = True
        btnReporte.Enabled = True
    End Sub
    Private Sub LlenaDatos()
        Try
            IdEquipo = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            If tipoEquipo = 1 Then
                'Equipos clientes
                Dim P As New dbClientesEquipos(IdEquipo, MySqlcon)
                Button1.Text = "Modificar"
                btnDetalles.Enabled = True
                Button2.Enabled = True
                ConsultaOn = False
                TextBox1.Text = P.Nombre
                TextBox2.Text = P.Marca
                TextBox3.Text = P.Modelo
                TextBox4.Text = P.NoSerie
                TextBox8.Text = P.NoMotor
                TextBox7.Text = P.Matricula
                TextBox5.Text = P.Color
                TextBox6.Text = P.Kilometraje
                fechaEnvio.Text = P.fechaEnvio
                If P.estado = 1 Then
                    chck_enviar.Checked = True
                ElseIf P.estado = 0 Then
                    chck_enviar.Checked = False
                End If
                ConsultaOn = True
            Else
                'Equipos sucursales
                Dim P As New dbClientesEquipos(IdEquipo, MySqlcon, 0)
                Button1.Text = "Modificar"
                btnDetalles.Enabled = True
                Button2.Enabled = True
                ConsultaOn = False
                TextBox1.Text = P.Nombre
                TextBox2.Text = P.Marca
                TextBox3.Text = P.Modelo
                TextBox4.Text = P.NoSerie
                TextBox8.Text = P.NoMotor
                TextBox7.Text = P.Matricula
                TextBox5.Text = P.Color
                TextBox6.Text = P.Kilometraje
                ConsultaOn = True
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            btnDetalles.Enabled = True
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            btnDetalles.Enabled = True
            LlenaDatos()
        End If
    End Sub
    Private Sub frmClientesEquipos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception
        End Try
        Nuevo()
    End Sub
    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Consulta()
    End Sub
    Private Sub btnDetalles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetalles.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosVerDetalles, PermisosN.Secciones.Servicios) = True) Then
            Dim B As New frmDetallesEquipos(IdEquipo, tipoEquipo)
            B.ShowDialog()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub
    Private Sub btnHistorial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHistorial.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosVerHistorial, PermisosN.Secciones.Servicios) = True) Then
            Dim P As New dbServicios(MySqlcon)

            Dim B As New frmEquiposHistorial(IdEquipo, tipoEquipo)
            B.ShowDialog()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub
    Private Sub btnReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporte.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosVerReportes, PermisosN.Secciones.Servicios) = True) Then
            Dim P As New dbDetallesEquipo(MySqlcon)
            ' Dim q As New dbPagosProveedores(MySqlcon)
            P.equipoDatos(IdEquipo)

            Dim nombre As String = P.nombre
            Dim marca As String = P.marca
            Dim modelo As String = P.modelo
            Dim matricula As String = P.matricula
            Dim noserie As String = P.noserie
            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument

            If tipoEquipo = 2 Then
                P.clienteDatossuc(IdCliente)
            Else
                P.clienteDatos(IdCliente)
            End If
            Dim nombreCliente As String = P.nombreCliente
            Dim direccion As String = P.direccionCliente
            Dim rfc As String = P.rfc
            Dim direccion2 As String = P.direccion2

            Rep = New repConsumibles
            'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
            ' Dim ta As DataTable
            'ta = P.Reporte1().ToTable()
            Rep.SetDataSource(consumibles())
            Rep.SetParameterValue("nombre", nombre)
            Rep.SetParameterValue("marca", marca)
            Rep.SetParameterValue("modelo", modelo)
            Rep.SetParameterValue("NO. SERIE", noserie)
            Rep.SetParameterValue("matricula", matricula)

            Rep.SetParameterValue("encabezado", nombreCliente)
            Rep.SetParameterValue("direccion", direccion)
            Rep.SetParameterValue("rfc", rfc)
            Rep.SetParameterValue("direccion2", direccion2)
            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
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
    Private Sub TextBox6_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And TextBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub
End Class