Public Class frmLicencias
    Dim Lic As Licencia
    Dim IdsDistribuidor As New elemento
    Dim IdsDistribuidor2 As New elemento
    Dim IDLicencia As Integer
    Dim tabla As New DataTable
    Dim IDTimbre As Integer
    Public tablaReporte As New DataTable
    Public tablaLicencias As New DataTable
    Dim idCliente As Integer

    Private Sub frmLicencias_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Lic.CierraConexion()

    End Sub
    Private Sub frmLicencias_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            ComboBox1.Items.Add("Completo.")
            ComboBox1.Items.Add("Facturación y clientes.")
            ComboBox1.Items.Add("Solo facturación.")
            ComboBox1.Items.Add("Conector.")
            ComboBox1.Items.Add("Nada.")
            cmbTipo.Items.Add("Todos.")
            cmbTipo.Items.Add("Especificar.")
            cmbActivada.Items.Add("Todos.")
            cmbActivada.Items.Add("Sí.")
            cmbActivada.Items.Add("No.")
            cmbActivada.SelectedIndex = 0
            cmbTipo.SelectedIndex = 0

            Lic = New Licencia("servidor", True)
            ' LlenaCombos("tbldistribuidor", cmbDistribuidor, "nombre", "nombret", "ID", IdsDistribuidor, , "Ninguno")
            'Label7.Text = Lic.SystemSerialNumber

            tabla.Columns.Add("ID")
            tabla.Columns.Add("nombre")
            tabla = Lic.ConsultaCombo("").ToTable
            Dim ds As DataRow
            ds = tabla.NewRow()
            ds("ID") = 0
            ds("nombre") = "Ninguno"
            tabla.Rows.Add(ds)

            dtpHasta.MinDate = dtpDesde.Value
            ' cmbDistribuidor.Items.Add("Ninguno")

            With cmbDistribuidor
                .DataSource = tabla
                .ValueMember = "ID"
                .DisplayMember = "nombre"
            End With

            With cmbDistribuidor2
                .DataSource = tabla
                .ValueMember = "ID"
                .DisplayMember = "nombre"
            End With

            ' cmbDistribuidor.Items.Add("Ninguno")
            tablaReporte.Columns.Add("idCliente")
            tablaReporte.Columns.Add("NombreCliente")
            tablaReporte.Columns.Add("RFC")
            tablaReporte.Columns.Add("Distribuidor")

            tablaReporte.Columns.Add("fechaLicencia")
            tablaReporte.Columns.Add("tipoLicencia")
            tablaReporte.Columns.Add("Licencia")
            tablaReporte.Columns.Add("Activado")


            tablaLicencias.Columns.Add("ID")
            tablaLicencias.Columns.Add("tipoVersion")
            tablaLicencias.Columns.Add("lic")
            tablaLicencias.Columns.Add("activada")
            tablaLicencias.Columns.Add("fecha")
            tablaLicencias.Columns.Add("dis")

            NuevoCliente()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaClientes()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            'Dim I As New dbInventario(MySqlcon)
            DataGridView1.DataSource = Lic.ConsultaClientes(TextBox16.Text)
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "RFC"
            DataGridView1.Columns(2).HeaderText = "Nombre"
            'DataGridView1.Columns(3).HeaderText = "Cantidad"
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaLicencias()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex
            'Dim I As New dbInventario(MySqlcon)
            DataGridView2.DataSource = Lic.ConsultaLicencias(Lic.IdCliente)
            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(1).HeaderText = "Licencia"
            DataGridView2.Columns(2).HeaderText = "Fecha"
            DataGridView2.Columns(3).HeaderText = "Activada"
            DataGridView2.Columns(4).HeaderText = "Completo"
            DataGridView2.Columns(5).HeaderText = "SoloFact"
            DataGridView2.Columns(6).HeaderText = "FacClientes"
            DataGridView2.Columns(7).HeaderText = "P.V."
            DataGridView2.Columns(8).HeaderText = "Bancos"
            DataGridView2.Columns(9).HeaderText = "30 Días."
            DataGridView2.Columns(10).HeaderText = "Conector"
            DataGridView2.Columns(11).HeaderText = "Lic"
            DataGridView2.Columns(12).HeaderText = "Nom"
            DataGridView2.Columns(13).HeaderText = "Serv"
            DataGridView2.Columns(14).HeaderText = "Gas"
            DataGridView2.Columns(15).HeaderText = "Emp"
            DataGridView2.Columns(16).HeaderText = "Serv Int."
            DataGridView2.Columns(17).HeaderText = "Cont."
            DataGridView2.Columns(18).HeaderText = "Fert."
            DataGridView2.Columns(19).HeaderText = "Val."
            DataGridView2.Columns(20).HeaderText = "Sem."
            DataGridView2.Columns(21).HeaderText = "Ext."
            DataGridView2.Columns(22).HeaderText = "Int."
            DataGridView2.Columns(23).HeaderText = "Us."
            DataGridView2.Columns(24).HeaderText = "Rest."
            DataGridView2.Columns(25).HeaderText = "Comentario"
            DataGridView2.Columns(3).Width = 65
            DataGridView2.Columns(4).Width = 65
            DataGridView2.Columns(5).Width = 65
            DataGridView2.Columns(6).Width = 65
            DataGridView2.Columns(7).Width = 65
            DataGridView2.Columns(8).Width = 65
            DataGridView2.Columns(9).Width = 70
            DataGridView2.Columns(10).Width = 65
            DataGridView2.Columns(11).Width = 65
            DataGridView2.Columns(12).Width = 65
            DataGridView2.Columns(13).Width = 65
            DataGridView2.Columns(14).Width = 65
            DataGridView2.Columns(15).Width = 65
            DataGridView2.Columns(16).Width = 65
            DataGridView2.Columns(17).Width = 65
            DataGridView2.Columns(18).Width = 65
            DataGridView2.Columns(19).Width = 65
            DataGridView2.Columns(20).Width = 65
            DataGridView2.Columns(21).Width = 65
            DataGridView2.Columns(22).Width = 65
            DataGridView2.Columns(23).Width = 65
            DataGridView2.Columns(24).Width = 65
            DataGridView2.Columns(25).Width = 300
            'DataGridView1.Columns(3).HeaderText = "Cantidad"
            'DataGridView2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub NuevoCliente()
        TextBox1.Text = ""
        TextBox6.Text = ""
        TextBox3.Text = ""
        txTelefono.Text = ""
        Lic.IdCliente = 0
        TextBox6.Focus()
        Button5.Text = "Guardar"
        Button1.Enabled = False
        ConsultaClientes()
        ConsultaLicencias()
        cmbDistribuidor.SelectedIndex = tabla.Rows.Count - 1
        grpTimbres.Enabled = False
        IDLicencia = -1
        'cmbDistribuidor.SelectedIndex = 0
        btnImprimir.Enabled = False
    End Sub
    Private Sub NuevaLic()
        TextBox2.Text = Lic.CreaLicencia()
        ComboBox1.SelectedIndex = 0
        Button8.Enabled = False
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False
        CheckBox7.Checked = False
        CheckBox8.Checked = False
        CheckBox10.Checked = False
        CheckBox11.Checked = False
        CheckBox12.Checked = False
        CheckBox13.Checked = False
        CheckBox15.Checked = False
        CheckBox16.Checked = False
        CheckBox17.Checked = False
        CheckBox18.Checked = False
        CheckBox24.Checked = False
        DateTimePicker1.Value = Date.Now
        Label5.Text = "Activada: No."
        TextBox4.Text = ""
        'Button6.Text = "Guardar"
        Button6.Enabled = True
        Button4.Enabled = False
        ConsultaLicencias()
        btnImprimir.Enabled = False
        ' IDLicencia = -1
        ' grpTimbres.Enabled = False
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If TextBox1.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Debe indicar un RFC y nombre al cliente.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Button5.Text = "Guardar" Then
            If Lic.ChecaClienteRepetido(TextBox6.Text) = False Then
                Lic.GuardaCliente(TextBox1.Text, TextBox6.Text, TextBox3.Text, cmbDistribuidor.SelectedValue, txTelefono.Text)
                ConsultaClientes()
                NuevaLic()
                'LlenaDatosCliente()
                PopUp("Cliente Guardado", 90)
            Else
                MsgBox("Ya existe ese cliente.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Else
            Lic.ModificaCliente(Lic.IdCliente, TextBox1.Text, TextBox6.Text, TextBox3.Text, cmbDistribuidor.SelectedValue, txTelefono.Text)
            NuevoCliente()
            PopUp("Cliente Modificado", 90)
        End If
    End Sub
    Private Sub LlenaDatosCliente()
        Lic.LlenaDatosCliente(Lic.IdCliente)
        IDLicencia = Lic.IdCliente
        TextBox6.Text = Lic.RFC
        TextBox1.Text = Lic.NombreCliente
        TextBox3.Text = Lic.ClienteCorreo
        txTelefono.Text = Lic.Tel
        If Lic.Distribuidor = 0 Then
            cmbDistribuidor.SelectedIndex = tabla.Rows.Count - 1
        Else
            cmbDistribuidor.SelectedValue = Lic.Distribuidor
        End If
        'cmbDistribuidor.SelectedIndex = IdsDistribuidor.Busca(Lic.Distribuidor)
        Button5.Text = "Modificar"
        Button1.Enabled = True
        ConsultaLicencias()
        NuevaLic()
        grpTimbres.Enabled = True
    End Sub
    Private Sub LlenaDatosLicencia()
        Lic.LlenaDatosLicencia(Lic.Id)
        TextBox2.Text = Lic.Licencia
        If (Lic.TipoVersion And 1) <> 0 Then
            ComboBox1.SelectedIndex = 0
        End If
        If (Lic.TipoVersion And 2) <> 0 Then
            ComboBox1.SelectedIndex = 2
        End If
        If (Lic.TipoVersion And 4) <> 0 Then
            ComboBox1.SelectedIndex = 1
        End If
       

        If (Lic.TipoVersion And 8) <> 0 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        If Lic.Activada = 0 Then
            Label5.Text = "Activada: No."
        Else
            Label5.Text = "Activada: Si."
        End If
        If (Lic.TipoVersion And 16) <> 0 Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If
        If (Lic.TipoVersion And 32) <> 0 Then
            CheckBox3.Checked = True
        Else
            CheckBox3.Checked = False
        End If
        If (Lic.TipoVersion And 128) <> 0 Then
            CheckBox4.Checked = True
        Else
            CheckBox4.Checked = False
        End If
        If (Lic.TipoVersion And 256) <> 0 Then
            CheckBox5.Checked = True
        Else
            CheckBox5.Checked = False
        End If
        If (Lic.TipoVersion And 512) <> 0 Then
            CheckBox6.Checked = True
        Else
            CheckBox6.Checked = False
        End If
        If (Lic.TipoVersion And 1024) <> 0 Then
            CheckBox7.Checked = True
        Else
            CheckBox7.Checked = False
        End If
        If (Lic.TipoVersion And 2048) <> 0 Then
            CheckBox8.Checked = True
        Else
            CheckBox8.Checked = False
        End If
        If (Lic.TipoVersion And 4096) <> 0 Then
            CheckBox10.Checked = True
        Else
            CheckBox10.Checked = False
        End If
        If (Lic.TipoVersion And 8192) <> 0 Then
            CheckBox11.Checked = True
        Else
            CheckBox11.Checked = False
        End If
        If (Lic.TipoVersion And 16384) <> 0 Then
            CheckBox12.Checked = True
        Else
            CheckBox12.Checked = False
        End If
        If (Lic.TipoVersion And 32768) <> 0 Then
            CheckBox13.Checked = True
        Else
            CheckBox13.Checked = False
        End If
        If (Lic.TipoVersion And 65536) <> 0 Then
            CheckBox15.Checked = True
        Else
            CheckBox15.Checked = False
        End If
        If (Lic.TipoVersion And 131072) <> 0 Then
            CheckBox16.Checked = True
        Else
            CheckBox16.Checked = False
        End If
        If (Lic.TipoVersion And 262144) <> 0 Then
            CheckBox17.Checked = True
        Else
            CheckBox17.Checked = False
        End If
        If (Lic.TipoVersion And 524288) <> 0 Then
            CheckBox18.Checked = True
        Else
            CheckBox18.Checked = False
        End If
        If (Lic.TipoVersion And 1048576) <> 0 Then
            CheckBox24.Checked = True
        Else
            CheckBox24.Checked = False
        End If
        If (Lic.TipoVersion And 64) <> 0 Then
            ComboBox1.SelectedIndex = 3
        End If

        If (Lic.TipoVersion And 1) = 0 And (Lic.TipoVersion And 2) = 0 And (Lic.TipoVersion And 4) = 0 And (Lic.TipoVersion And 64) = 0 Then
            ComboBox1.SelectedIndex = 4
        End If

        TextBox4.Text = Lic.Comentario
        Button6.Enabled = False
        Button4.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If MsgBox("¿Eliminar este cliente? Todas las licencias de este cliente también serán eliminadas.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Lic.EliminarCliente(Lic.IdCliente)
            PopUp("Cliente Eliminado", 90)
            NuevoCliente()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        NuevoCliente()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If Lic.IdCliente = 0 Then
            MsgBox("Debe seleccionar un cliente.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Lic.ChecaLienciaRepetida(TextBox2.Text) Then
            MsgBox("Ya existe usa licencia favor de generar una nueva.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Button6.Text = "Guardar" Then
            Lic.TipoVersion = 0
            If ComboBox1.SelectedIndex = 0 Then
                Lic.TipoVersion = Lic.TipoVersion Or 1
            End If
            If ComboBox1.SelectedIndex = 1 Then
                Lic.TipoVersion = Lic.TipoVersion Or 4
            End If
            If ComboBox1.SelectedIndex = 2 Then
                Lic.TipoVersion = Lic.TipoVersion Or 2
            End If
            If ComboBox1.SelectedIndex = 3 Then
                Lic.TipoVersion = Lic.TipoVersion Or 64
            End If
            If CheckBox1.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 8
            End If
            If CheckBox2.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 16
            End If
            If CheckBox3.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 32
            End If
            If CheckBox4.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 128
            End If
            If CheckBox5.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 256
            End If
            If CheckBox6.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 512
            End If
            If CheckBox7.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 1024
            End If
            If CheckBox8.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 2048
            End If
            If CheckBox10.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 4096
            End If
            If CheckBox11.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 8192
            End If
            If CheckBox12.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 16384
            End If
            If CheckBox13.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 32768
            End If
            If CheckBox15.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 65536
            End If
            If CheckBox16.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 131072
            End If
            If CheckBox17.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 262144
            End If
            If CheckBox18.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 524288
            End If
            If CheckBox24.Checked Then
                Lic.TipoVersion = Lic.TipoVersion Or 1048576
            End If
            Lic.GuardarLicencia(Lic.IdCliente, Lic.TipoVersion, TextBox2.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox4.Text)
            PopUp("Licencia Guardada.", 90)
            NuevaLic()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If MsgBox("¿Eliminar esta licencia?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Lic.EliminarLicencia(Lic.Id)
            NuevaLic()
            PopUp("Licencia eliminada.", 90)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        NuevaLic()
    End Sub

    Private Sub TextBox16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox16.TextChanged
        ConsultaClientes()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            btnReporteLicCliente.Enabled = True
            Lic.IdCliente = DataGridView1.Item(0, e.RowIndex).Value
            LlenaDatosCliente()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Lic.Id = DataGridView2.Item(0, e.RowIndex).Value
        IDLicencia = Lic.Id
        LlenaDatosLicencia()
        grpTimbres.Enabled = True
        btnImprimir.Enabled = True
        Button8.Enabled = True
        Nuevotimbre()
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 3 Then
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
        Else
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If MsgBox("¿Actualizar Comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Lic.ActualizaComentario(Lic.Id, TextBox4.Text)
            PopUp("Actualizado.", 90)
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidadTimbres.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnTimbresGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimbresGuardar.Click
        If IDLicencia <> -1 Then
            If txtCantidadTimbres.Text <> "" Then
                If btnTimbresGuardar.Text = "Guardar" Then
                    Lic.GuardarTimbre(IDLicencia, txtCantidadTimbres.Text, dtpFechaTimbre.Value.ToString("yyyy/MM/dd"))
                    PopUp("Timbre Guardado", 90)

                Else
                    Lic.ModificarTimbre(IDTimbre, IDLicencia, txtCantidadTimbres.Text, dtpFechaTimbre.Value.ToString("yyyy/MM/dd"))
                    PopUp("Timbre Modificado", 90)
                End If
                Nuevotimbre()
            Else
                MsgBox("Debe indicar una cantidad de timbres", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Else
            MsgBox("No se ha seleccionado ninguna licencia.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Nuevotimbre()
        txtCantidadTimbres.Text = ""
        IDTimbre = -1
        dtpFechaTimbre.Value = Date.Now
        btnTimbresEliminar.Enabled = False
        btnTimbresGuardar.Text = "Guardar"
        txtCantidadTimbres.Focus()

    End Sub

    Private Sub btnTimbresNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimbresNuevo.Click
        Nuevotimbre()
    End Sub

    Private Sub btnTimbresEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimbresEliminar.Click
        If MsgBox("¿Desea eliminar este timbre ", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Lic.EliminarTimbre(IDTimbre)
            PopUp("Timbre Eliminado", 90)
            Nuevotimbre()
        End If
    End Sub

    Private Sub btnTimbresHistorial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimbresHistorial.Click
      
        Dim f As New frmTimbres(IDLicencia)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            IDTimbre = f.idTimbre
            LlenaDatosTimbres()
        End If
        f.Dispose()
    End Sub
    Private Sub LlenaDatosTimbres()
        Lic.LlenaDatosTimbre(IDTimbre)
        txtCantidadTimbres.Text = Lic.cantidadTimbre.ToString
        dtpFechaTimbre.Value = Lic.fechaTimbre
        btnTimbresGuardar.Text = "Modificar"
        btnTimbresEliminar.Enabled = True
    End Sub

    Private Sub frmImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        If Lic.IdCliente = 0 Then
            MsgBox("Debe seleccionar un cliente.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        Else


            Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
            Dim tipos As String = ""
            Rep = New repLicenciaImpresion
            'Rep.SetDataSource(Co.filtroTodos(fechaInicial, fechaFinal, idClienteAUX, idSucursal, idCaja, idVendedor))
            Lic.LlenaDatosLicencia(Lic.Id)
            'LlenaDatosCliente()
            If Lic.Distribuidor = 0 Then
                cmbDistribuidor.SelectedIndex = tabla.Rows.Count - 1
            Else
                cmbDistribuidor.SelectedValue = Lic.Distribuidor
            End If

            If (Lic.TipoVersion And 1) <> 0 Then
                ComboBox1.SelectedIndex = 0
            End If
            If (Lic.TipoVersion And 2) <> 0 Then
                ComboBox1.SelectedIndex = 2
            End If
            If (Lic.TipoVersion And 4) <> 0 Then
                ComboBox1.SelectedIndex = 1
            End If
            If (Lic.TipoVersion And 8) <> 0 Then
                tipos += "* CON PUNTO DE VENTA. " + vbCrLf
           
            End If
            If (Lic.TipoVersion And 16) <> 0 Then
                tipos += "* CON MANEJO DE LICENCIAS. " + vbCrLf
            End If
            If (Lic.TipoVersion And 32) <> 0 Then
                tipos += "* SOLO 30 DÍAS. " + vbCrLf
            End If
            If (Lic.TipoVersion And 128) <> 0 Then
                tipos += "* CON BANCOS. " + vbCrLf
            End If
            If (Lic.TipoVersion And 256) <> 0 Then
                tipos += "* CON NÓMINA. " + vbCrLf
            End If
            If (Lic.TipoVersion And 512) <> 0 Then
                tipos += "* CON SERVICIOS. " + vbCrLf
            End If
            If (Lic.TipoVersion And 1024) <> 0 Then
                tipos += "* CON GASTOS. " + vbCrLf
            End If
            If (Lic.TipoVersion And 2048) <> 0 Then
                tipos += "* CON EMPEÑOS. " + vbCrLf
            End If
            
            Rep.SetParameterValue("nombre", Lic.NombreCliente)
            Rep.SetParameterValue("rfc", Lic.RFC)
            Rep.SetParameterValue("email", Lic.ClienteCorreo)
            Rep.SetParameterValue("distribuidor", cmbDistribuidor.Text)

            Rep.SetParameterValue("fecha", Lic.Fecha)
            Rep.SetParameterValue("Licencia", Lic.Licencia)
            Rep.SetParameterValue("tipo", ComboBox1.Text)
            Rep.SetParameterValue("tipos", tipos)
            Rep.SetParameterValue("comentario", Lic.Comentario)
            Rep.SetParameterValue("conmanejo", "")
            Rep.SetParameterValue("activada", Label5.Text)

            Dim RV As New frmReportes(Rep, False)
            RV.Show()
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDesde.ValueChanged
        dtpHasta.MinDate = dtpDesde.Value
    End Sub

    Private Sub btnReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReporte.Click
        Dim activada As Integer
        Dim fechaI As String
        Dim fechaF As String
        Dim tipo As Integer
        Dim distri As Integer
        Dim Filtros As String = ""
        Dim Filtrost As String = ""
        If cmbActivada.SelectedIndex = 0 Then
            'Todas
            activada = -1

        End If
        If cmbActivada.SelectedIndex = 1 Then
            'sí
            activada = 1
            Filtros += "ACTIVADA= SÍ"
        End If
        If cmbActivada.SelectedIndex = 2 Then
            'No
            activada = 0
            Filtros += "ACTIVADA= NO"
        End If
        If chkFecha.Checked = True Then
            fechaI = dtpDesde.Value.ToString("yyyy/MM/dd")
            fechaF = dtpHasta.Value.ToString("yyyy/MM/dd")
            Filtros += " FECHA INICIO=" + fechaI + ", FECHA FINAL=" + fechaF
        Else
            fechaI = "1000/01/01"
            fechaF = "3000/01/01"
        End If
        If cmbTipo.SelectedIndex = 0 Then
            tipo = -1
        Else

            'Lic.TipoVersion = 0
            If cmbTipo.SelectedIndex = 0 Then
                tipo = -1
            Else
                tipo = 1
                Filtrost = "TIPO:"
                If ckcompleto.Checked Then
                    Filtrost += " COMPLETO."
                End If
                If ckFacturacion.Checked Then
                    Filtrost += " CON FACTURACIÓN Y CLIENTES."
                End If
                If ck30dias.Checked Then
                    Filtrost += " SOLO 30 DÍAS."
                End If
                If ckGastos.Checked Then
                    Filtrost += " CON GASTOS."
                End If
                If ckBancos.Checked Then
                    Filtrost += " CON BANCOS."
                End If
                If ckempenios.Checked Then
                    Filtrost += " CON EMPEÑOS."
                End If
                If ckNomina.Checked Then
                    Filtrost += " CON NÓMINA."
                End If
                If ckPuntodeV.Checked Then
                    Filtrost += " CON PUNTO DE VENTA."
                End If
                If ckServicios.Checked Then
                    Filtrost += " CON SERVICIOS."
                End If
                If ckLicencias.Checked Then
                    Filtrost += " CON MANEJO DE LICENCIAS."
                End If
                If ckConector.Checked Then
                    Filtrost += " CONECTOR."
                End If
                If ckSoloFact.Checked Then
                    Filtrost += " SOLO FACTURACIÓN."
                End If
                If CheckBox9.Checked Then
                    Filtrost += " SERVICIOS INTERNOS."
                End If
                If chkContabilidad.Checked Then
                    Filtrost += " CONTABILIDAD."
                End If
                If chkfertilizantes.Checked Then
                    Filtrost += " FERTILIZANTES."
                End If
                If CheckBox14.Checked Then
                    Filtrost += " VALIDADOR."
                End If
                If CheckBox19.Checked Then
                    Filtrost += " SEMILLAS."
                End If
                If CheckBox20.Checked Then
                    Filtrost += " EXTRA."
                End If
                If CheckBox21.Checked Then
                    Filtrost += " INTEGRACION."
                End If
                If CheckBox22.Checked Then
                    Filtrost += " USUARIOS."
                End If
            End If


        End If
        distri = cmbDistribuidor2.SelectedValue
        Dim tabla2 As DataTable

        tabla2 = Lic.ClientesRep(activada, fechaI, fechaF, tipo, ckcompleto.Checked, ckFacturacion.Checked, ck30dias.Checked, ckGastos.Checked, ckBancos.Checked, ckempenios.Checked, ckNomina.Checked, ckPuntodeV.Checked, ckServicios.Checked, ckLicencias.Checked, ckConector.Checked, ckSoloFact.Checked, CheckBox9.Checked, distri, chkContabilidad.Checked, chkfertilizantes.Checked, CheckBox14.Checked, CheckBox19.Checked, CheckBox20.Checked, CheckBox22.Checked, CheckBox21.Checked, CheckBox23.Checked).ToTable
        tablaReporte.Clear()
        For i As Integer = 0 To tabla2.Rows.Count - 1
            Dim ds As DataRow
            ds = tablaReporte.NewRow()
            ds("idCliente") = tabla2.Rows(i)(0).ToString
            Lic.LlenaDatosCliente(tabla2.Rows(i)(0).ToString)
            ds("NombreCliente") = Lic.NombreCliente
            ds("RFC") = Lic.RFC
            ds("Distribuidor") = Lic.distribuidorNombre

            ds("fechaLicencia") = tabla2.Rows(i)(4).ToString
            ds("tipoLicencia") = otipo(tabla2.Rows(i)(1).ToString)
            ds("Licencia") = tabla2.Rows(i)(2).ToString
            If tabla2.Rows(i)(3).ToString = 0 Then
                ds("Activado") = "NO"
            Else
                ds("Activado") = "SÍ"
            End If


            tablaReporte.Rows.Add(ds)
        Next
        'Dim Da As DataSet = New DataSet
        'Da.Tables.Add(tablaReporte)
        ' Da.WriteXmlSchema("tblLicenciasReporte.xml")

        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        ' Dim tipos As String = ""
        Rep = New repLicenciasReporte
        Rep.SetDataSource(tablaReporte)
        Rep.SetParameterValue("FILTROS", Filtros)
        Rep.SetParameterValue("FILTROST", Filtrost)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        If cmbTipo.SelectedIndex = 0 Then
            pnlTipos.Enabled = False
        Else
            pnlTipos.Enabled = True
        End If
    End Sub
    Private Function otipo(ByVal ptipo As Integer) As String
        Dim Tipos As String = ""
        If (ptipo And 1) <> 0 Then
            Tipos += "* COMPLETO. "
        End If
        If (ptipo And 2) <> 0 Then
            Tipos += "* SOLO FACTURACIÓN."
        End If
        If (ptipo And 4) <> 0 Then
            Tipos += "* CONECTOR."
        End If
        If (ptipo And 8) <> 0 Then
            Tipos += "* CON PUNTO DE VENTA. "

        End If
        If (ptipo And 16) <> 0 Then
            Tipos += "* CON MANEJO DE LICENCIAS. "
        End If
        If (ptipo And 32) <> 0 Then
            Tipos += "* SOLO 30 DÍAS. "
        End If
        If (ptipo And 128) <> 0 Then
            Tipos += "* CON BANCOS. "
        End If
        If (ptipo And 256) <> 0 Then
            Tipos += "* CON NÓMINA. "
        End If
        If (ptipo And 512) <> 0 Then
            Tipos += "* CON SERVICIOS. "
        End If
        If (ptipo And 1024) <> 0 Then
            Tipos += "* CON GASTOS. "
        End If
        If (ptipo And 2048) <> 0 Then
            Tipos += "* CON EMPEÑOS. "
        End If
        If (ptipo And 4096) <> 0 Then
            Tipos += "* CON SERVICIOS INTERNO. "
        End If
        If (ptipo And 8192) <> 0 Then
            Tipos += "* CON CONTABILIDAD. "
        End If
        If (ptipo And 16384) <> 0 Then
            Tipos += "* CON FERTILIZANTES. "
        End If
        If (ptipo And 32768) <> 0 Then
            Tipos += "* CON VALIDADOR. "
        End If
        If (ptipo And 65536) <> 0 Then
            Tipos += "* CON SEMILLAS. "
        End If
        If (ptipo And 131072) <> 0 Then
            Tipos += "* CON EXTRA. "
        End If
        If (ptipo And 262144) <> 0 Then
            Tipos += "* CON INTEGRAL. "
        End If
        If (ptipo And 524288) <> 0 Then
            Tipos += "* CON USUARIOS. "
        End If
        '"if(tipoversion & 4096,'X','') as SerInt," + _
        '"if(tipoversion & 8192,'X','') as Cont," + _
        '"if(tipoversion & 16384,'X','') as Fert," + _
        '"if(tipoversion & 32768,'X','') as Val," + _
        '"if(tipoversion & 65536,'X','') as Semi," + _
        '"if(tipoversion & 131072,'X','') as Ext," + _
        '"if(tipoversion & 262144,'X','') as Intgrl," + _
        '"if(tipoversion & 524288,'X','') as Users," + _
        Return Tipos
    End Function
    Private Function otipo2(ByVal ptipo As Integer) As String
        Dim Tipos As String = ""
        If (ptipo And 1) <> 0 Then
            Tipos += "COMPLETO. "
        End If
        If (ptipo And 2) <> 0 Then
            Tipos += "SOLO FACTURACIÓN."
        End If
        If (ptipo And 4) <> 0 Then
            Tipos += "CONECTOR."
        End If
        If (ptipo And 8) <> 0 Then
            Tipos += "PUNTO DE VENTA. "

        End If
        If (ptipo And 16) <> 0 Then
            Tipos += "MANEJO DE LICENCIAS. "
        End If
        If (ptipo And 32) <> 0 Then
            Tipos += "SOLO 30 DÍAS. "
        End If
        If (ptipo And 128) <> 0 Then
            Tipos += "BANCOS. "
        End If
        If (ptipo And 256) <> 0 Then
            Tipos += "NÓMINA. "
        End If
        If (ptipo And 512) <> 0 Then
            Tipos += "SERVICIOS. "
        End If
        If (ptipo And 1024) <> 0 Then
            Tipos += "GASTOS. "
        End If
        If (ptipo And 2048) <> 0 Then
            Tipos += "*EMPEÑOS. "
        End If
        If (ptipo And 4096) <> 0 Then
            Tipos += "* CON SERVICIOS INTERNO. "
        End If
        If (ptipo And 8192) <> 0 Then
            Tipos += "* CON CONTABILIDAD. "
        End If
        If (ptipo And 16384) <> 0 Then
            Tipos += "* CON FERTILIZANTES. "
        End If
        If (ptipo And 32768) <> 0 Then
            Tipos += "* CON VALIDADOR. "
        End If
        If (ptipo And 65536) <> 0 Then
            Tipos += "* CON SEMILLAS. "
        End If
        If (ptipo And 131072) <> 0 Then
            Tipos += "* CON EXTRA. "
        End If
        If (ptipo And 262144) <> 0 Then
            Tipos += "* CON INTEGRAL. "
        End If
        If (ptipo And 524288) <> 0 Then
            Tipos += "* CON USUARIOS. "
        End If
        Return Tipos
    End Function


    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click

       
        Dim tabla2 As DataTable
        tabla2 = Lic.ClientesRepDistri(cmbDistribuidor2.SelectedValue).ToTable


        
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        ' Dim tipos As String = ""
        Rep = New repLicenciasClientesRep
        Rep.SetDataSource(tabla2)

        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub btnReporteLicCliente_Click(sender As Object, e As EventArgs) Handles btnReporteLicCliente.Click
        Dim Filtros As String = ""
        Dim Filtrost As String = ""
        Dim tabla2 As DataTable

        tabla2 = Lic.ClientesLicencias(Lic.IdCliente).ToTable

        tablaLicencias.Clear()
        For i As Integer = 0 To tabla2.Rows.Count - 1
            Dim ds As DataRow
            ds = tablaLicencias.NewRow()
            ds("ID") = tabla2.Rows(i)(0).ToString
            ds("tipoVersion") = otipo2(tabla2.Rows(i)(1).ToString)
            ds("lic") = tabla2.Rows(i)(2).ToString
            If tabla2.Rows(i)(3).ToString = 0 Then
                ds("activada") = "NO"
            Else
                ds("activada") = "SÍ"
            End If
            'ds("activada") = tabla2.Rows(i)(3).ToString
            ds("fecha") = tabla2.Rows(i)(4).ToString
            ds("dis") = tabla2.Rows(i)(5).ToString
           


            tablaLicencias.Rows.Add(ds)
        Next
        'Dim Da As DataSet = New DataSet
        'Da.Tables.Add(tablaReporte)
        ' Da.WriteXmlSchema("tblLicenciasReporte.xml")
        Lic.LlenaDatosCliente(Lic.IdCliente)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        ' Dim tipos As String = ""
        Rep = New repLicenciasporCliente
        Rep.SetDataSource(tablaLicencias)
        Rep.SetParameterValue("CLIENTE", "CLIENTE: " + Lic.NombreCliente)

        'Lic.LlenaDatosCliente(tabla2.Rows(i)(0).ToString)
        'ds("NombreCliente") = Lic.NombreCliente
        'ds("RFC") = Lic.RFC
        'ds("Distribuidor") = Lic.distribuidorNombre
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub CheckBox15_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox15.CheckedChanged

    End Sub
End Class