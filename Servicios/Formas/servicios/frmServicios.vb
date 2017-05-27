Public Class frmServicios
    Dim ConsultaOn As Boolean = True
    Dim idCliente As Integer
    Dim IdEquipo As Integer
    Dim clienteCodigo As String
    Dim clienteNombre As String
    Dim clienteTelefono As String
    Dim clienteDireccion As String
    Dim clienteDireccion2 As String
    Dim EquipoNombre As String
    Dim EquipoMatricula As String
    Dim EquipoNoSerie As String
    Dim bandera As Boolean = False
    Dim IdsTecnicos As New elemento

    Private Sub frmServicios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tbltecnicos", ComboBox3, "nombre", "nombret", "idtecnico", IdsTecnicos)
        Nuevo()
        If My.Settings.pulgadasServicios2 = 0 Then
            RadioButton1.Checked = True
        Else
            RadioButton1.Checked = False
        End If
        txtImpresora.Text = My.Settings.impresoraServicios2
    End Sub
    Private Sub ConsultaClientes()
        Try
            If ConsultaOn Then
                Dim C As New dbClientes(MySqlcon)
                DGC.DataSource = C.Consulta(TextBox1.Text)
                DGC.Columns(0).Visible = False
                DGC.Columns(1).HeaderText = "Código"
                DGC.Columns(2).HeaderText = "Nombre"
                DGC.Columns(3).HeaderText = "RFC"
                DGC.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub filtroClientesTodos()
        Try
            If ConsultaOn Then
                Dim C As New dbClientes(MySqlcon)
                DGC.DataSource = C.Consulta2()
                DGC.Columns(0).Visible = False
                DGC.Columns(1).HeaderText = "Código"
                DGC.Columns(2).HeaderText = "Nombre"
                DGC.Columns(3).HeaderText = "RFC"
                DGC.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        ConsultaClientes()
    End Sub

    Private Sub DGC_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGC.CellClick
        Button1.Enabled = False 'guardar
        pnlServicio.Enabled = True
        lbllisto.Visible = False
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        TextBox2.Text = ""
        LlenaDatosClientes()
    End Sub
    Private Sub LlenaDatosClientes()
        Try
            idCliente = DGC.Item(0, DGC.CurrentCell.RowIndex).Value
            Dim C As New dbClientes(idCliente, MySqlcon)
            Label4.Text = "Código: " + C.Clave
            Label4.Text += vbCrLf + "Nombre: " + C.Nombre
            Label4.Text += vbCrLf + "Teléfono: " + C.Telefono
            Label4.Text += vbCrLf + "Dirección: " + C.Direccion + " " + C.NoExterior + " " + C.Colonia + " " + C.Municipio
            Label4.Text += vbCrLf + C.CP + " " + C.Ciudad + ", " + C.Estado + ", " + C.Pais

            clienteCodigo = "Código: " + C.Clave
            clienteNombre = "Nombre: " + C.Nombre
            clienteTelefono = "Teléfono: " + C.Telefono
            clienteDireccion = "Dirección: " + C.Direccion + " " + C.NoExterior + " " + C.Colonia + " " + C.Municipio
            clienteDireccion2 = C.CP + " " + C.Ciudad + ", " + C.Estado + ", " + C.Pais

            TextBox2.Enabled = True
            Button5.Enabled = True
            DataGridView1.Enabled = True
            ConsultaOn = False
            TextBox2.Text = ""
            Label5.Text = ""
            ConsultaOn = True
            ConsultaEquipos()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosClientesEquipo(ByVal pidEquipo As Integer)
        Try
            IdEquipo = pidEquipo
            Dim C As New dbClientesEquipos(IdEquipo, MySqlcon)
            Label5.Text = "Nombre: " + C.Nombre
            Label5.Text += vbCrLf + "Matrícula: " + C.Matricula
            Label5.Text += vbCrLf + "No. Serie: " + C.NoSerie
            EquipoMatricula = "Matrícula: " + C.Matricula
            EquipoNombre = "Nombre: " + C.Nombre
            EquipoNoSerie = "No. Serie: " + C.NoSerie
            pnlServicio.Enabled = True
            pnlServicio.Visible = True
            lbllisto.Visible = False
            Button1.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        DateTimePicker1.Value = Date.Now
        TextBox3.Text = ""
        TextBox1.Text = ""
        Button5.Enabled = False
        TextBox2.Text = ""
        TextBox2.Enabled = False
        Label4.Text = ""
        Label5.Text = ""
        IdEquipo = 0
        Button1.Enabled = False
        pnlServicio.Enabled = True
        lbllisto.Visible = False
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()
        btnTicket.Enabled = False
        ' Button1.Enabled = True
        'DataGridView1.Enabled = False
        filtroClientesTodos()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim f As New frmClientes(1, 0)
        f.ShowDialog()
        If f.CodigoCliente <> "" Then
            TextBox1.Text = f.CodigoCliente
        End If
    End Sub

    Private Sub DGC_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGC.CellContentClick

    End Sub
    Private Sub ConsultaEquipos()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbClientesEquipos(MySqlcon)
                If TextBox2.Text = "" Then
                    DataGridView1.DataSource = P.Consulta2(idCliente)
                Else
                    DataGridView1.DataSource = P.Consulta(idCliente, TextBox2.Text)
                End If

                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Nombre"
                DataGridView1.Columns(2).HeaderText = "Matrícula"
                DataGridView1.Columns(3).HeaderText = "No. Serie"
                DataGridView1.Columns(1).Width = 150
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        ConsultaEquipos()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New frmClientesEquipos(idCliente, 1, 1)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            LlenaDatosClientesEquipo(f.IdEquipo)

        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatosClientesEquipo(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)

        'Dim P As New dbServiciosEquipos(MySqlcon)
        ' If P.verificarServicio(idCliente, IdEquipo) = 0 Then
        
        '    Else
        '    pnlServicio.Enabled = False
        '    lbllisto.Visible = True
        '    Button1.Enabled = False
        '   End If

    End Sub


    Private Sub Button1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServicioGuardar, PermisosN.Secciones.Servicios) = True Then
                Dim S As New dbServicios(MySqlcon)
                S.Guardar(TextBox3.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), 0, idCliente, 0, S.DaNuevoFolio, IdEquipo, IdsTecnicos.Valor(ComboBox3.SelectedIndex))
                If IdEquipo > 0 Then
                    Dim SE As New dbServiciosEquipos(MySqlcon)
                    SE.Guardar(S.ID, IdEquipo, idCliente)
                    btnTicket.Enabled = True
                    Button1.Enabled = False
                    bandera = False
                    If MessageBox.Show("¿Desea imprimir ticket?", "Pull System Soft", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If txtImpresora.Text <> "" Then
                            IMPRIMIR()

                        Else
                            MsgBox("Debe seleccionar una impresora.", MsgBoxStyle.Critical, GlobalNombreApp)
                            bandera = True
                        End If
                    End If
                    If bandera = False Then
                        Nuevo()
                    End If
                End If

                PopUp("Guardado", 90)
                ' Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Nuevo()
    End Sub

    Private Sub Button3_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
    Private Function reporte() As DataSet
        ' Dim Tabla1 As DataTable
        Dim TablaFull As New DataTable

        TablaFull.Columns.Add("ClienteCodigo")
        TablaFull.Columns.Add("ClienteNombre")
        TablaFull.Columns.Add("ClienteTelefono")
        TablaFull.Columns.Add("ClienteDireccion")
        TablaFull.Columns.Add("ClienteDireccion2")
        TablaFull.Columns.Add("EquipoNombre")
        TablaFull.Columns.Add("EquipoMatricula")
        TablaFull.Columns.Add("EquipoNoSerie")
        TablaFull.Columns.Add("Descripcion")

        Dim dr1 As DataRow
        dr1 = TablaFull.NewRow()
        dr1("ClienteCodigo") = clienteCodigo
        dr1("ClienteNombre") = clienteNombre
        dr1("ClienteTelefono") = clienteTelefono
        dr1("ClienteDireccion") = clienteDireccion
        dr1("ClienteDireccion2") = clienteDireccion2
        dr1("EquipoNombre") = EquipoNombre
        dr1("EquipoMatricula") = EquipoMatricula
        dr1("EquipoNoSerie") = EquipoNoSerie
        dr1("Descripcion") = TextBox3.Text

        TablaFull.Rows.Add(dr1)

        Dim DS As DataSet = New DataSet
        DS.Tables.Add(TablaFull)
        DS.WriteXmlSchema("tblServicioNuevo.xml")
        Return DS
    End Function

    Public Sub IMPRIMIR()
        '  Dim P As New dbBanco(MySqlcon)
        'Dim q As New dbPagosProveedores(MySqlcon)
        Dim Suc As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        'Dim nombreEmpresa As String = q.nombre()
        'Dim rfc As String = q.RFC()
        'Dim direccion As String = q.Calles(nombreEmpresa)
        'Dim direccion2 As String = q.direccion2(nombreEmpresa)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        If RadioButton1.Checked = True Then
            Rep = New repServicioNuevo
        Else
            Rep = New repServicioNuevo2
        End If

        'V.ReporteInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario, idClas, idClas2, idClas3, IdsSucursales.Valor(ComboBox1.SelectedIndex))
        Rep.SetDataSource(reporte())
        'Rep.SetParameterValue("encabezado", nombreEmpresa)
        'Rep.SetParameterValue("direccion", direccion)
        'Rep.SetParameterValue("rfc", rfc)

        Rep.SetParameterValue("tipo", "DATOS DEL CLIENTE")
        

        Dim PrintLayout As New CrystalDecisions.Shared.PrintLayoutSettings
        PrintLayout.Scaling = CrystalDecisions.Shared.PrintLayoutSettings.PrintScaling.Scale
        Dim PS As New System.Drawing.Printing.PrinterSettings
        PS.PrinterName = txtImpresora.Text
        Dim pageSettings As New System.Drawing.Printing.PageSettings(PS)
        Rep.PrintOptions.PrinterName = txtImpresora.Text
        Rep.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
        'Rep.PrintToPrinter(1, False, 0, 0)
        Rep.PrintToPrinter(PS, pageSettings, False, PrintLayout)
        'Dim RV As New frmReportes(Rep, False)
        'RV.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtImpresora.Text = PrintDialog1.PrinterSettings.PrinterName
            My.Settings.impresoraServicios2 = txtImpresora.Text
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.pulgadasServicios2 = 0

        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            My.Settings.pulgadasServicios2 = 1

        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTicket.Click
        If txtImpresora.Text <> "" Then

            IMPRIMIR()
            If bandera = True Then
                bandera = False
                Nuevo()
            End If

        Else
            MsgBox("Debe seleccionar una impresora.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.ServiciosConsultaVer, PermisosN.Secciones.Servicios) = True) Then
            'If BuscaVentanas("frmServiciosConsulta") = False Then
            Dim f As New frmServiciosConsulta
            'f.MdiParent = Me
            f.ShowDialog()
            f.Dispose()
            'End If
        Else
        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub
End Class