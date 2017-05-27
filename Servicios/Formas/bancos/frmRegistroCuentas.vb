Public Class frmRegistroCuentas
    Dim idCuentas As New elemento
    Dim IdCuenta As Integer
    Dim IdCuenta1 As Integer
    Dim IdCuenta2 As Integer
    Dim IdCuenta3 As Integer
    Dim IdCuenta4 As Integer
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Dim idBanco As Integer
    Dim Dt As DataTable
    Dim IdsBancos As New elemento

    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            
            'llenarCuenta()
            LlenaCombos("tblbancoscatalogo", cmbBanco, "nombre", "nombreb", "idbanco", IdsBancos)
            If cmbBanco.Items.Count = 0 Then
                MsgBox("Se deben registrar primero los bancos para poder dar de alta cuentas.", MsgBoxStyle.Critical, GlobalNombreApp)
                btnEliminar.Enabled = False
                btnGuardar.Enabled = False
                btnLimpiar.Enabled = False
            End If
            Nuevo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        FiltroTodos()
        txtNumero.Focus()

    End Sub
    'Private Sub llenarCuenta()

    '    Dim P As New dbCuentas(MySqlcon)
    '    Dt = P.buscarBancos()
    '    If Dt.Rows.Count() > 0 Then
    '        With cmbBanco
    '            .DataSource = Dt
    '            .DisplayMember = "nombre"
    '            .ValueMember = "nombre"
    '        End With
    '    Else
    '        MsgBox("No hay cuentas de Banco registradas, favor de registrar una.", MsgBoxStyle.Critical, GlobalNombreApp)
    '    End If

    'End Sub
    Private Sub Nuevo()

        Try
            Dim P As New dbCuentas(MySqlcon)
            txtFolio.Text = Format(P.buscarFolio(), "0000")
            btnGuardar.Text = "Guardar"
            txtNumero.Text = ""
            cmbBanco.SelectedIndex = 0
            cmbTipo.SelectedIndex = 0
            txtConsulta.Text = ""
            txtNumero.BackColor = Color.White
            'cmbBanco.BackColor = Color.White
            FiltroTodos()
            btnEliminar.Enabled = False
            IdCuenta1 = 0
            IdCuenta2 = 0
            IdCuenta3 = 0
            IdCuenta4 = 0
            Button9.BackColor = ColorRojo
            Button10.BackColor = ColorRojo
            Button11.BackColor = ColorRojo
            Button12.BackColor = ColorRojo
            txtNumero.Focus()
            CheckBox1.Checked = False
            TextBox1.Text = ""
            TextBox1.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim P As New dbCuentas(MySqlcon)
        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        Dim EsEx As Byte = 0
        txtNumero.BackColor = Color.White
        cmbBanco.BackColor = Color.White
        If CheckBox1.Checked Then EsEx = 1
        If txtNumero.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe indicar el número de cuenta."
            txtNumero.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If cmbBanco.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + " Debe elegir un banco."
            cmbBanco.BackColor = Color.FromArgb(250, 150, 150)
        End If

        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.CuentasAlta, PermisosN.Secciones.Bancos) = False And btnGuardar.Text = "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.CuentasModificar, PermisosN.Secciones.Bancos) = False And btnGuardar.Text <> "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        
        If NoErrores = True Then
            If btnGuardar.Text = "Guardar" Then
                If P.ChecaCuentaRepetida(txtNumero.Text) = False Then ' no es cuenta repetida
                    P.Guardar(txtNumero.Text, IdsBancos.Valor(cmbBanco.SelectedIndex), cmbTipo.Text, IdCuenta1, EsEx, TextBox1.Text, IdCuenta2, IdCuenta3, IdCuenta4)
                    PopUp("Guardado", 90)
                    txtFolio.Enabled = False
                    txtFolio.BackColor = Color.White
                    Nuevo()
                Else
                    MsgBox("Ya existe una cuenta con el mismo número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                    txtNumero.BackColor = Color.FromArgb(250, 150, 150)
                End If
            Else 'Modificar

                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim ClaveRepetida As Boolean = False
                    P.Modificar(IdCuenta, txtNumero.Text, IdsBancos.Valor(cmbBanco.SelectedIndex), cmbTipo.Text, IdCuenta1, EsEx, TextBox1.Text, IdCuenta2, IdCuenta3, IdCuenta4)
                    PopUp("Modificado", 90)
                    Nuevo()
                   
                End If
            End If
        End If

        If NoErrores = False Then

            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If

    End Sub

    Private Sub FiltroTodos()

        Try
            Dim PrimerCeldaRow As Integer = -1
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            Dim P As New dbCuentas(MySqlcon)
            'Dim Tabla As DataTable
            'Dim TablaFull As New DataTable
            'TablaFull.Columns.Add("ID")
            'TablaFull.Columns.Add("Numero")
            'TablaFull.Columns.Add("Banco")
            'TablaFull.Columns.Add("Tipo")

            'Tabla = P.reporte().

            'For i As Integer = 0 To Tabla.Rows.Count() - 1
            '    Dim dr As DataRow

            '    dr = TablaFull.NewRow()
            '    dr("ID") = Tabla.Rows(i)(0).ToString
            '    dr("Numero") = Tabla.Rows(i)(1).ToString
            '    dr("Banco") = P.nombreBanco(Integer.Parse(Tabla.Rows(i)(2).ToString))
            '    dr("Tipo") = Tabla.Rows(i)(3).ToString

            '    TablaFull.Rows.Add(dr)
            'Next
            DataGridView1.DataSource = P.reporte(txtConsulta.Text)
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Número"
            DataGridView1.Columns(2).HeaderText = "Banco"
            DataGridView1.Columns(3).HeaderText = "Tipo"
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub txtConsulta_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtConsulta.TextChanged
        'Filtro de consulta

        FiltroTodos()
    End Sub

    'Private Sub Consulta()

    '    Try
    '        Dim PrimerCeldaRow As Integer = -1
    '        If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
    '        Dim P As New dbCuentas(MySqlcon)
    '        Dim Tabla As DataTable
    '        Dim TablaFull As New DataTable
    '        TablaFull.Columns.Add("ID")
    '        TablaFull.Columns.Add("Numero")
    '        TablaFull.Columns.Add("Banco")
    '        TablaFull.Columns.Add("Tipo")
    '        Tabla = P.Consultar(txtConsulta.Text).ToTable()
    '        For i As Integer = 0 To Tabla.Rows.Count() - 1
    '            Dim dr As DataRow

    '            dr = TablaFull.NewRow()
    '            dr("ID") = Tabla.Rows(i)(0).ToString
    '            dr("Numero") = Tabla.Rows(i)(1).ToString
    '            dr("Banco") = P.nombreBanco(Integer.Parse(Tabla.Rows(i)(2).ToString))
    '            dr("Tipo") = Tabla.Rows(i)(3).ToString

    '            TablaFull.Rows.Add(dr)
    '        Next
    '        DataGridView1.DataSource = TablaFull
    '        DataGridView1.Columns(0).Visible = False
    '        DataGridView1.Columns(1).HeaderText = "Número"
    '        DataGridView1.Columns(2).HeaderText = "Banco"
    '        DataGridView1.Columns(3).HeaderText = "Tipo"
    '        DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    '        If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try

    'End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        llenaDatos()
        btnEliminar.Enabled = True

    End Sub
    Private Sub llenaDatos()

        Try
            IdCuenta = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbCuentas(IdCuenta, MySqlcon)
            btnGuardar.Text = "Modificar"
            txtFolio.Text = Format(Integer.Parse(P.Folio), "0000")
            txtNumero.Text = P.Numero
            cmbBanco.SelectedIndex = IdsBancos.Busca(P.Banco)
            cmbTipo.Text = P.Tipo
            If P.EsExtranjero = 0 Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If

            IdCuenta1 = P.Idcuenta
            IdCuenta2 = P.IdCuenta2
            IdCuenta3 = P.IdCuenta3
            IdCuenta4 = P.IdCuenta4

            If IdCuenta1 <> 0 Then
                Button9.BackColor = ColorVerde
            Else
                Button9.BackColor = ColorRojo
            End If
            If IdCuenta2 <> 0 Then
                Button10.BackColor = ColorVerde
            Else
                Button10.BackColor = ColorRojo
            End If
            If IdCuenta3 <> 0 Then
                Button11.BackColor = ColorVerde
            Else
                Button11.BackColor = ColorRojo
            End If
            If IdCuenta4 <> 0 Then
                Button12.BackColor = ColorVerde
            Else
                Button12.BackColor = ColorRojo
            End If

            TextBox1.Text = P.NombreEx

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Bancos.CuentasEliminar, PermisosN.Secciones.Bancos) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbCuentas(MySqlcon)
                    P.Eliminar(IdCuenta)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    txtNumero.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este almacen debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click

        Nuevo()

    End Sub
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click

        Me.Close()

    End Sub

    'Private Sub cmbBanco_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBanco.SelectedIndexChanged

    '    If Dt.Rows.Count > 0 Then
    '        Dim i As Integer = 0
    '        i = cmbBanco.SelectedIndex()
    '        idBanco = Integer.Parse(Dt.Rows(i)(0).ToString)
    '    End If

    'End Sub

    Private Sub txtNumero_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNumero.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtConsulta_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConsulta.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtFolio_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolio.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox1.Enabled = True
        Else
            TextBox1.Enabled = False
        End If
    End Sub

   
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta1)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta1 = fsc.IdCuenta
            If IdCuenta1 <> 0 Then
                Button9.BackColor = ColorVerde
            Else
                Button9.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta2)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta2 = fsc.IdCuenta
            If IdCuenta2 <> 0 Then
                Button10.BackColor = ColorVerde
            Else
                Button10.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta3)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta3 = fsc.IdCuenta
            If IdCuenta3 <> 0 Then
                Button11.BackColor = ColorVerde
            Else
                Button11.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim fsc As New frmSelecionarCuenta(IdCuenta4)
        If fsc.ShowDialog = Windows.Forms.DialogResult.OK Then
            IdCuenta4 = fsc.IdCuenta
            If IdCuenta4 <> 0 Then
                Button12.BackColor = ColorVerde
            Else
                Button12.BackColor = ColorRojo
            End If
        End If
        fsc.Dispose()
    End Sub
End Class
