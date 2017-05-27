Public Class frmInventarioModificar
    Public tabla As New DataTable
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim IdsClasv As New elemento
    Dim IdsClas2v As New elemento
    Dim IdsClas3v As New elemento
    Dim idTipo As Integer = -1
    Dim idTipo2 As Integer = -1
    Dim idTipo3 As Integer = -1
    Dim ConsultaOn As Boolean = False
    Dim EncontroClas As Boolean = True
    Dim checked As Boolean
    Dim checked2 As Boolean
    Dim NombreAnt1 As String
    Dim CodigoAnt1 As String
    Dim NombreAnt2 As String
    Dim CodigoAnt2 As String
    Dim NombreAnt3 As String
    Dim CodigoAnt3 As String
    Dim Clas1 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
    Dim Clas2 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
    Dim Clas3 As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
    'Dim idsTipos As New elemento
    'Dim idsTipos2 As New elemento
    'Dim idsTipos3 As New elemento
    


    Private Sub frmInventarioModificar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        'Cargar filtros de Categorias
        DataGridView2.Columns(0).Width = 20
        DataGridView2.Columns(2).Width = 100

        'LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", , "codigo")
        ' ComboBox6.Items.Add("General")
        ' ComboBox7.Items.Add("General")

        'Combo de asignación
        LlenaCombos("tblinventarioclasificaciones", cmbNivel1, "nombre", "nombret", "idclasificacion", IdsClasv, "idclasificacion>1", , "nombre")
        ' cmbNivel2.Items.Add("General")
        ' cmbNivel3.Items.Add("General")
        ConsultaOn = False
        Nuevo1()
        Nuevo2()
        Nuevo3()
        nuevo()
        Consulta()

        Dim s As Size = Me.Size
        s.Width = 979
        s.Height = 266
        Me.Size = s
        ' Me.StartPosition = FormStartPosition.CenterScreen
        'Me.Location = New Point(200, 150)
    End Sub
    

    Private Sub nuevo()
        
        'ComboBox6.SelectedIndex = 0
        'ComboBox7.SelectedIndex = 0

        'cmbNivel1.SelectedIndex = 0
        'cmbNivel2.SelectedIndex = 0
        'cmbNivel3.SelectedIndex = 0

        txtDescripcion.Text = ""
        Consulta()
        DataGridView2.Rows.Clear()                          'para limpiarlo
        DataGridView2.DataSource = Nothing              'para limpiar cualquier registro internamente y desenlazar el control de la BD
        txtDescripcion.Focus()
        ConsultaOn = True
        txtImpuesto.Text = ""
        txtIEPS.Text = ""
        txtIvaRetenido.Text = ""
        txtFabricante.Text = ""
        txtUbicacion.Text = ""
        CheckBox5.Checked = False
        chkClasificacion.Checked = False
        chkFabricante.Checked = False
        chkIEPS.Checked = False
        chkImpuesto.Checked = False
        chkIVARetenido.Checked = False
        chkPrecioN.Checked = False
        chkUbicacion.Checked = False
        cmbNivel1.SelectedIndex = 0
        txtDescripcion.Focus()
    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then

                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim idclas As Integer
                Dim idClas2 As Integer
                Dim idClas3 As Integer
                '******************
                If ComboBox1.SelectedIndex < 0 Then
                    idclas = 0
                Else
                    idclas = IdsClas.Valor(ComboBox1.SelectedIndex)
                End If
                If ComboBox2.SelectedIndex < 0 Then
                    'If ComboBox2.SelectedIndex < 0 Then
                    idClas2 = 0
                    'Else
                    '    idClas2 = 1
                    'End If

                Else
                    idClas2 = IdsClas2.Valor(ComboBox2.SelectedIndex)
                End If
                If ComboBox3.SelectedIndex < 0 Then
                    'If ComboBox3.SelectedIndex < 0 Then
                    idClas3 = 0
                    'Else
                    '   idClas3 = 1
                    'End If
                Else
                    idClas3 = IdsClas3.Valor(ComboBox3.SelectedIndex)
                End If

                Dim I As New dbInventario(MySqlcon)
                DataGridView1.DataSource = I.Consulta(0, txtDescripcion.Text, , idclas, True, idClas2, idClas3, , , GlobalpFabricante, GlobalModoBusqueda, 0, 1)
                'If txtDescripcion.Text = "" Then
                '    If Button4.Text = "Guardar" Then

                '    Else
                '        If Button7.Text = "Guardar" Then
                '            DataGridView1.DataSource = I.Consulta(0, txtDescripcion.Text, , IdsClas.Valor(ComboBox3.SelectedIndex), , , , , , GlobalpFabricante, GlobalModoBusqueda)
                '        Else
                '            If Button10.Text = "Guardar" Then
                '                DataGridView1.DataSource = I.Consulta(0, txtDescripcion.Text, , IdsClas.Valor(ComboBox3.SelectedIndex), , idClas2, , , , GlobalpFabricante, GlobalModoBusqueda)
                '            Else
                '                DataGridView1.DataSource = I.Consulta(0, txtDescripcion.Text, , IdsClas.Valor(ComboBox3.SelectedIndex), , idClas2, idClas3, , , GlobalpFabricante, GlobalModoBusqueda)
                '            End If
                '        End If
                '    End If

                'Else
                '    Nuevo1()
                '    DataGridView1.DataSource = I.Consulta(0, txtDescripcion.Text, , , , , , , , GlobalpFabricante, GlobalModoBusqueda)

                'End If


                DataGridView1.Columns(0).Width = 20
                DataGridView1.Columns(2).Width = 100
                'DataGridView1.Columns(2).HeaderText = "Descripción"
                'DataGridView1.Columns(3).HeaderText = "Cantidad"
                'DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        Dim nfilas As Integer
        nfilas = DataGridView1.RowCount()


        'If checked = True Then

        '    For i As Integer = 0 To nfilas - 1
        '        DataGridView1.Rows(i).Cells(0).Value = 0
        '    Next
        '    DataGridView1.Columns(0).HeaderText = " "
        '    checked = False

        'Else
        For i As Integer = 0 To nfilas - 1
            DataGridView1.Rows(i).Cells(0).Value = 1

        Next
        DataGridView1.Columns(0).HeaderText = "X"
        checked = True
        'End If

    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        nuevo1()
        txtDescripcion.Text = ""
        Consulta()

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub cmbNivel1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNivel1.SelectedIndexChanged
        If cmbNivel1.Text = "General" Then
            cmbNivel2.Enabled = False
            txtNivel2.Enabled = False
            txtNivel1.Text = ""
            txtNivel3.Text = ""
            txtNivel2.Text = ""
            'If cmbNivel2.Items.Count > 0 Then
            '    cmbNivel2.SelectedIndex = 0
            'Else
            '    cmbNivel2.Items.Add("General")
            'End If

            'If cmbNivel3.Items.Count > 0 Then
            '    cmbNivel3.SelectedIndex = 0
            'Else
            '    cmbNivel3.Items.Add("General")
            'End If

        Else
            cmbNivel2.Enabled = True
            txtNivel2.Enabled = True
            '  cmbNivel3.Enabled = True
        End If
        If cmbNivel1.Text <> "General" Then
            Dim IdClas As Integer
            IdClas = IdsClasv.Valor(cmbNivel1.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            ConsultaOn = False
            txtNivel1.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones2", cmbNivel2, "nombre", "nombret", "idclasificacion", IdsClas2v, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")

        End If
    End Sub

    Private Sub cmbNivel2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNivel2.SelectedIndexChanged
        If cmbNivel2.Text = "General" Then
            cmbNivel3.Enabled = False
            txtNivel3.Enabled = False
            ' cmbNivel3.Enabled = False

            txtNivel3.Text = ""
            txtNivel2.Text = ""
            'If cmbNivel3.Items.Count > 0 Then
            '    cmbNivel3.SelectedIndex = 0
            'Else
            '    cmbNivel3.Items.Add("General")
            'End If

        Else
            cmbNivel3.Enabled = True
            txtNivel3.Enabled = True
            '  cmbNivel3.Enabled = True
        End If
        If cmbNivel2.Text <> "General" Then
            Dim IdClas As Integer
            IdClas = IdsClas2v.Valor(cmbNivel2.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            ConsultaOn = False
            txtNivel2.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", cmbNivel3, "nombre", "nombret", "idclasificacion", IdsClas3v, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")
            ' HabilitaClase3(True)
        End If
    End Sub

    Private Sub cmbNivel3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNivel3.SelectedIndexChanged
        If cmbNivel3.Text <> "General" Then
            Dim IdClas As Integer
            IdClas = IdsClas3v.Valor(cmbNivel3.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            ConsultaOn = False
            txtNivel3.Text = IC2.Codigo
            ConsultaOn = True
            chkClasificacion.Checked = True
        Else
            txtNivel3.Text = ""
            '  chkClasificacion.Checked = False
        End If

    End Sub





    Private Sub txtNivel1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNivel1.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            If IC.BuscaClasificacion(txtNivel1.Text) Then
                EncontroClas = True
                cmbNivel1.SelectedIndex = IdsClas.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub txtNivel2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNivel2.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            If IC.BuscaClasificacion(txtNivel2.Text, IdsClas.Valor(cmbNivel1.SelectedIndex)) Then
                EncontroClas = True
                cmbNivel2.SelectedIndex = IdsClas2.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub txtNivel3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNivel3.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            If IC.BuscaClasificacion(txtNivel3.Text, IdsClas2.Valor(cmbNivel2.SelectedIndex)) Then
                EncontroClas = True
                cmbNivel3.SelectedIndex = IdsClas3.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub txtImpuesto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtImpuesto.TextChanged
        If txtImpuesto.Text = "" Then
            chkImpuesto.Checked = False
        Else
            chkImpuesto.Checked = True
        End If
    End Sub

    Private Sub txtIEPS_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIEPS.TextChanged
        If txtIEPS.Text = "" Then
            chkIEPS.Checked = False
        Else
            chkIEPS.Checked = True
        End If
    End Sub

    Private Sub txtIvaRetenido_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIvaRetenido.TextChanged
        If txtIvaRetenido.Text = "" Then
            chkIVARetenido.Checked = False
        Else
            chkIVARetenido.Checked = True
        End If
    End Sub

    Private Sub txtFabricante_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFabricante.TextChanged
        If txtFabricante.Text = "" Then
            chkFabricante.Checked = False
        Else
            chkFabricante.Checked = True
        End If
    End Sub

    Private Sub txtUbicacion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUbicacion.TextChanged
        If txtUbicacion.Text = "" Then
            chkUbicacion.Checked = False
        Else
            chkUbicacion.Checked = True
        End If
    End Sub

    Private Sub txtDescripcion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescripcion.TextChanged
        Consulta()
    End Sub


    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        agregar()
    End Sub
    Private Sub agregar()
        Dim nfilas As Integer
        nfilas = DataGridView1.RowCount() - 1
        Dim idaux As String
        For i As Integer = 0 To nfilas
            If DataGridView1.Rows(i).Cells(0).Value = 1 Then

                idaux = DataGridView1.Rows(i).Cells(1).Value.ToString()

                If buscarCoincidencias(idaux) = 0 Then
                    Dim row As String() = New String() {"0", DataGridView1.Rows(i).Cells(1).Value.ToString(), DataGridView1.Rows(i).Cells(2).Value.ToString(), DataGridView1.Rows(i).Cells(3).Value.ToString(), DataGridView1.Rows(i).Cells(4).Value.ToString()}
                    DataGridView2.Rows.Add(row)
                End If


            End If
        Next
    End Sub
    Private Function buscarCoincidencias(ByVal pid As String) As Integer
        Dim coincidencias As Integer = 0
        Dim nfilas As Integer
        nfilas = DataGridView2.RowCount() - 1

        For i As Integer = 0 To nfilas
            If DataGridView2.Rows(i).Cells(1).Value = pid Then
                coincidencias = coincidencias + 1
            End If
        Next
        Return coincidencias

    End Function

    Private Sub btnAgregarTodos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarTodos.Click
        'AGREGAR Todos
        Dim nfilas As Integer
        nfilas = DataGridView1.RowCount() - 1
        Dim idaux As String
        For i As Integer = 0 To nfilas
            ' If DataGridView1.Rows(i).Cells(0).Value = 1 Then

            idaux = DataGridView1.Rows(i).Cells(1).Value.ToString()

            If buscarCoincidencias(idaux) = 0 Then
                Dim row As String() = New String() {"0", DataGridView1.Rows(i).Cells(1).Value.ToString(), DataGridView1.Rows(i).Cells(2).Value.ToString(), DataGridView1.Rows(i).Cells(3).Value.ToString(), DataGridView1.Rows(i).Cells(4).Value.ToString()}
                DataGridView2.Rows.Add(row)
            End If


            ' End If
        Next
    End Sub

    Private Sub btnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitar.Click

        Dim nfilas As Integer
        nfilas = DataGridView2.RowCount() - 1

        For i As Integer = nfilas To 0 Step -1
            If DataGridView2.Rows(i).Cells(0).Value = 1 Then
                DataGridView2.Rows.RemoveAt(i)
                DataGridView2.Refresh()
                nfilas = DataGridView2.RowCount() - 1
            End If
        Next
        If DataGridView2.RowCount() = 0 Then
            DataGridView2.Columns(0).HeaderText = " "
            checked2 = False
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoverTodos.Click
        'Eliminar seleccionados
        DataGridView2.Rows.Clear()
        DataGridView2.DataSource = Nothing
        DataGridView2.Refresh()
        DataGridView2.Columns(0).HeaderText = " "
        '  checked2 = True
        If DataGridView2.RowCount() = 0 Then
            DataGridView2.Columns(0).HeaderText = " "
            checked2 = False
        End If
    End Sub
 

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex = -1 Then
            'btnAgregar.Enabled = False
            'btnAgregarTodos.Enabled = False
            'btnQuitar.Enabled = False
            'Button2.Enabled = False
            If e.ColumnIndex = 0 Then
                Dim nfilas As Integer
                nfilas = DataGridView2.RowCount()


                If checked2 = True Then

                    For i As Integer = 0 To nfilas - 1
                        DataGridView2.Rows(i).Cells(0).Value = 0
                    Next
                    DataGridView2.Columns(0).HeaderText = " "
                    checked2 = False

                Else
                    For i As Integer = 0 To nfilas - 1
                        DataGridView2.Rows(i).Cells(0).Value = 1

                    Next
                    DataGridView2.Columns(0).HeaderText = "X"
                    checked2 = True
                End If

            End If

            'btnAgregar.Enabled = True
            'btnAgregarTodos.Enabled = True
            'btnQuitar.Enabled = True
            'Button2.Enabled = True
        End If
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            If DataGridView2.Rows(e.RowIndex).Cells(0).Value = 1 Then

                DataGridView2.Rows(e.RowIndex).Cells(0).Value = 0

            ElseIf DataGridView2.Rows(e.RowIndex).Cells(0).Value = 0 Then

                DataGridView2.Rows(e.RowIndex).Cells(0).Value = 1

            End If
        End If
    End Sub

    Private Sub txtImpuesto_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtImpuesto.KeyPress
        NumeroDec(e, Me.txtImpuesto)
    End Sub
    Function NumeroDec(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Text As TextBox) As Integer

        Dim dig As Integer = Len(Text.Text & e.KeyChar)
        Dim a, esDecimal, NumDecimales As Integer
        Dim esDec As Boolean
        ' se verifica si es un digito o un punto 
        If IsNumeric(e.KeyChar) Or e.KeyChar = "." Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
            Return a
        Else
            e.Handled = True
        End If
        ' se verifica que el primer digito ingresado no sea un punto al seleccionar
        If Text.SelectedText <> "" Then
            If e.KeyChar = "." Then
                e.Handled = True
                Return a
            End If
        End If
        If dig = 1 And e.KeyChar = "." Then
            e.Handled = True
            Return a
        End If
        'aqui se hace la verificacion cuando es seleccionado el valor del texto
        'y no sea considerado como la adicion de un digito mas al valor ya contenido en el textbox
        If Text.SelectedText = "" Then
            ' aqui se hace el for para controlar que el numero sea de dos digitos - contadose a partir del punto decimal.
            For a = 0 To dig - 1
                Dim car As String = CStr(Text.Text & e.KeyChar)
                If car.Substring(a, 1) = "." Then
                    esDecimal = esDecimal + 1
                    esDec = True
                End If
                If esDec = True Then
                    NumDecimales = NumDecimales + 1
                End If
                ' aqui se controla los digitos a partir del punto numdecimales = 4 si es de dos decimales 
                If NumDecimales >= 4 Or esDecimal >= 2 Then
                    e.Handled = True
                End If
            Next
        End If
    End Function

    Private Sub txtIEPS_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIEPS.KeyPress
        NumeroDec(e, Me.txtIEPS)
    End Sub

    Private Sub txtIvaRetenido_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIvaRetenido.KeyPress
        NumeroDec(e, Me.txtIvaRetenido)
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        If DataGridView2.RowCount > 0 Then
            If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioCambio, PermisosN.Secciones.Catalagos) Then
                    Dim N As New dbInventario(MySqlcon)
                    Dim PrecioNeto As Integer = 0
                    For i As Integer = 0 To DataGridView2.RowCount - 1
                        'IVA
                        If chkImpuesto.Checked = True Then
                            If txtImpuesto.Text = "" Then
                                txtImpuesto.Text = "0"
                            End If
                            N.ModificarImpuesto(Integer.Parse(DataGridView2.Item(1, i).Value), Double.Parse(txtImpuesto.Text))
                        End If
                        'IEPS
                        If chkIEPS.Checked = True Then
                            If txtIEPS.Text = "" Then
                                txtIEPS.Text = "0"
                            End If
                            N.ModificarIEPS(Integer.Parse(DataGridView2.Item(1, i).Value), Double.Parse(txtIEPS.Text))
                        End If
                        'IVA RETENIDO
                        If chkIVARetenido.Checked = True Then
                            If txtIvaRetenido.Text = "" Then
                                txtIvaRetenido.Text = "0"
                            End If
                            N.ModificarIVARetenido(Integer.Parse(DataGridView2.Item(1, i).Value), Double.Parse(txtIvaRetenido.Text))
                        End If
                        'Fabricante
                        If chkFabricante.Checked = True Then
                            N.ModificarFabricante(Integer.Parse(DataGridView2.Item(1, i).Value), txtFabricante.Text)
                        End If
                        'Ubicacion
                        If chkUbicacion.Checked = True Then
                            N.ModificarUbicacion(Integer.Parse(DataGridView2.Item(1, i).Value), txtUbicacion.Text)
                        End If
                        If CheckBox1.Checked = True Then
                            If CheckBox2.Checked Then
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "soloventas", 0, "1")
                            Else
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "soloventas", 0, "0")
                            End If
                        End If
                        If CheckBox3.Checked = True Then
                            If CheckBox4.Checked Then
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "solocompras", 0, "1")
                            Else
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "solocompras", 0, "0")
                            End If
                        End If
                        If CheckBox6.Checked = True Then
                            If CheckBox7.Checked Then
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "soloinventario", 0, "1")
                            Else
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "soloinventario", 0, "0")
                            End If
                        End If
                        If CheckBox8.Checked = True Then
                            If CheckBox9.Checked Then
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "restaurante", 0, "1")
                            Else
                                N.ModificarCampo(Integer.Parse(DataGridView2.Item(1, i).Value), "restaurante", 0, "0")
                            End If
                        End If
                        'PRECIO NETO
                        If chkPrecioN.Checked = True Then
                            If CheckBox5.Checked = True Then
                                PrecioNeto = 1
                            Else
                                PrecioNeto = 0
                            End If
                            N.ModificarPrecioNeto(Integer.Parse(DataGridView2.Item(1, i).Value), PrecioNeto)
                        End If
                        'CLASIFICACIÓN
                        If chkClasificacion.Checked = True Then
                            Dim idClas As Integer
                            Dim idClas2 As Integer
                            Dim idClas3 As Integer
                            'If cmbNivel1.SelectedIndex = 0 Then
                            '    idClas = 1
                            'Else
                            idClas = IdsClasv.Valor(cmbNivel1.SelectedIndex)
                            'End If
                            If cmbNivel2.SelectedIndex = 0 Then
                                idClas2 = 1
                            Else
                                idClas2 = IdsClas2v.Valor(cmbNivel2.SelectedIndex)

                            End If

                            If cmbNivel3.SelectedIndex = 0 Then
                                idClas3 = 1
                            Else
                                idClas3 = IdsClas3v.Valor(cmbNivel3.SelectedIndex)

                            End If
                            N.ModificarClasificacion(Integer.Parse(DataGridView2.Item(1, i).Value), idClas, idClas2, idClas3)
                        End If
                       
                    Next
                    PopUp("Modificado", 90)
                    nuevo()
                End If

            Else
                'No tiene permisos
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If



    End Sub

    Private Sub cmbNivel1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbNivel1.MouseClick
        chkClasificacion.Checked = True
    End Sub

    Private Sub CheckBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.Click
        chkPrecioN.Checked = True
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If DataGridView2.RowCount > 0 Then
            Dim nombre As String = ""
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.InventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                If MsgBox("¿Desea eliminar estos registros?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Try

                        Dim N As New dbInventario(MySqlcon)
                        For i As Integer = DataGridView2.RowCount - 1 To 0 Step -1
                            nombre = DataGridView2.Item(3, i).Value
                            N.Eliminar(Integer.Parse(DataGridView2.Item(1, i).Value))
                            DataGridView2.Rows.RemoveAt(i)
                            DataGridView2.Refresh()
                        Next
                        If DataGridView2.RowCount = 1 Then
                            PopUp("Registro elimiando", 90)
                        Else
                            PopUp("Registros elimiandos", 90)
                        End If

                        nuevo()
                        Nuevo1()
                    Catch exm As MySql.Data.MySqlClient.MySqlException
                        If exm.ErrorCode = -2147467259 Then
                            MsgBox("No se puede eliminar """ + nombre + """ debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
                        Else
                            MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                    End Try
                End If
            End If
        End If
    End Sub



   

    Private Sub txtDescripcion_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDescripcion.KeyUp
        If e.KeyCode = Keys.Enter Then

            agregar()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        nuevo()

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificarInventario.Click
        If btnModificarInventario.Text = "Modificar inventario >>" Then

            Dim s As Size = Me.Size
            s.Width = 979
            s.Height = 692
            Me.Size = s
            Me.StartPosition = FormStartPosition.CenterScreen
            btnModificarInventario.Text = "<< Contraer ventana"
            Me.Location = New Point(200, 2)
        Else

            Dim s As Size = Me.Size
            s.Width = 979
            s.Height = 266
            Me.Size = s
            Me.StartPosition = FormStartPosition.CenterScreen
            btnModificarInventario.Text = "Modificar inventario >>"
            Me.Location = New Point(200, 150)
        End If

       


    End Sub

    

    


   

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Then
            'btnAgregar.Enabled = False
            'btnAgregarTodos.Enabled = False
            'btnQuitar.Enabled = False
            'Button2.Enabled = False
            If e.ColumnIndex = 0 Then
                Dim nfilas As Integer
                nfilas = DataGridView1.RowCount()


                If checked = True Then

                    For i As Integer = 0 To nfilas - 1
                        DataGridView1.Rows(i).Cells(0).Value = 0
                    Next
                    DataGridView1.Columns(0).HeaderText = " "
                    checked = False

                Else
                    For i As Integer = 0 To nfilas - 1
                        DataGridView1.Rows(i).Cells(0).Value = 1

                    Next
                    DataGridView1.Columns(0).HeaderText = "X"
                    checked = True
                End If

            End If

            'btnAgregar.Enabled = True
            'btnAgregarTodos.Enabled = True
            'btnQuitar.Enabled = True
            'Button2.Enabled = True
        End If
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            If DataGridView1.Rows(e.RowIndex).Cells(0).Value = 1 Then

                DataGridView1.Rows(e.RowIndex).Cells(0).Value = 0

            ElseIf DataGridView1.Rows(e.RowIndex).Cells(0).Value = 0 Then

                DataGridView1.Rows(e.RowIndex).Cells(0).Value = 1

            End If
        End If
    End Sub

    

  

    

    
  
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub


    Private Sub Nuevo1()
        Try
            ComboBox1.Text = ""

            Button4.Text = "Guardar"
            Button2.Enabled = False
            Panel1.Enabled = False
            Panel2.Enabled = False
            ComboBox1.Items.Clear()
            LlenaCombos("tblinventarioclasificaciones", ComboBox1, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", , "nombre")
            TextBox1.Text = Clas1.DaSiguienteCodigo
            'TreeView1.Nodes.Add("Calando")
            'TreeView1.Nodes.Add("Calando B")
            'TreeView1.Nodes(0).Nodes.Add("Calando2")
            idTipo = -1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Nuevo2()
        Try
            ComboBox2.Text = ""
            TextBox2.Text = Clas2.DaSiguienteCodigo(idTipo)
            Button7.Text = "Guardar"
            Button6.Enabled = False
            Panel2.Enabled = False
            ComboBox2.Items.Clear()
            LlenaCombos("tblinventarioclasificaciones2", ComboBox2, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + idTipo.ToString, , "nombre")
            idTipo2 = -1

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo3()
        Try
            ComboBox3.Text = ""
            TextBox3.Text = Clas3.DaSiguienteCodigo(idTipo2)
            Button10.Text = "Guardar"
            Button9.Enabled = False
            ComboBox3.Items.Clear()
            LlenaCombos("tblinventarioclasificaciones3", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + idTipo2.ToString, , "nombre")
            idTipo3 = -1
            'LLenaArbol()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox1.Text <> "" And TextBox1.Text <> "" Then
                If Button4.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos) = True Then
                        If TC.ChecaNombreRepetido(ComboBox1.Text) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox1.Text) Then
                            NoError = False
                            Errores += " Ya existe una clasificación con ese código."
                        End If
                        If NoError Then
                            TC.Guardar(ComboBox1.Text, TextBox1.Text)
                            PopUp("Guardado", 90)
                            Button1.Text = "Modificar"
                            Button2.Enabled = True
                            idTipo = TC.ID
                            NombreAnt1 = TC.Nombre
                            CodigoAnt1 = TC.Codigo
                            Panel1.Enabled = True
                            'LLenaArbol()
                            TextBox2.Focus()
                            Nuevo1()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If TC.ChecaNombreRepetido(ComboBox1.Text) And ComboBox1.Text <> NombreAnt1 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If TC.ChecaCodigoRepetido(TextBox1.Text) And TextBox1.Text <> CodigoAnt1 Then
                                NoError = False
                                Errores += " Ya existe una clasificación con ese código."
                            End If
                            If NoError Then
                                TC.Modificar(idTipo, ComboBox1.Text, TextBox1.Text)
                                PopUp("Modificado", 90)
                                TextBox1.Focus()
                                Nuevo1()
                            Else
                                MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                            End If
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        LlenaCombos("tblinventarioclasificaciones", cmbNivel1, "nombre", "nombret", "idclasificacion", IdsClasv, "idclasificacion>1", , "nombre")
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Eliminar(idTipo)
                    PopUp("Eliminado", 90)
                    Nuevo1()
                    ComboBox1.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta clasificación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo1()
        Nuevo2()
        Nuevo3()
        Consulta()
        TextBox1.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex >= 0 Then
            idTipo = IdsClas.Valor(ComboBox1.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(idTipo, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            TextBox1.Text = IC2.Codigo
            NombreAnt1 = IC2.Nombre
            CodigoAnt1 = IC2.Codigo
            Button4.Text = "Modificar"
            Button2.Enabled = True
            Nuevo2()
            Panel1.Enabled = True

        End If
        Consulta()
        'LlenaCombos("tblinventarioclasificaciones", ComboBox1, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", , "nombre")
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex >= 0 Then
            idTipo2 = IdsClas2.Valor(ComboBox2.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(idTipo2, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            TextBox2.Text = IC2.Codigo
            NombreAnt2 = IC2.Nombre
            CodigoAnt2 = IC2.Codigo
            Button7.Text = "Modificar"
            Button6.Enabled = True
            Nuevo3()
            Panel2.Enabled = True
        End If
        Consulta()
        'LlenaCombos("tblinventarioclasificaciones2", ComboBox2, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + idTipo.ToString, , "nombre")
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex >= 0 Then
            idTipo3 = IdsClas3.Valor(ComboBox3.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(idTipo3, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            TextBox3.Text = IC2.Codigo
            NombreAnt3 = IC2.Nombre
            CodigoAnt3 = IC2.Codigo
            Button10.Text = "Modificar"
            Button9.Enabled = True
        End If
        Consulta()
        'LlenaCombos("tblinventarioclasificaciones3", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + idTipo2.ToString, , "nombre")
    End Sub

    Private Sub Button7_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox2.Text <> "" And TextBox2.Text <> "" Then
                If Button7.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos) = True Then
                        If TC.ChecaNombreRepetido(ComboBox2.Text, idTipo) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox2.Text, idTipo) Then
                            NoError = False
                            Errores += " Ya existe una clasificación con ese código."
                        End If
                        If NoError Then
                            TC.Guardar(ComboBox2.Text, TextBox2.Text, idTipo)
                            PopUp("Guardado", 90)
                            Button7.Text = "Modificar"
                            Button6.Enabled = True
                            idTipo2 = TC.ID
                            NombreAnt2 = TC.Nombre
                            CodigoAnt2 = TC.Codigo
                            Panel2.Enabled = True
                            'LLenaArbol()
                            TextBox3.Focus()
                            'Nuevo2()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If

                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If TC.ChecaNombreRepetido(ComboBox2.Text, idTipo) And ComboBox2.Text <> NombreAnt2 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If TC.ChecaCodigoRepetido(TextBox2.Text, idTipo) And TextBox2.Text <> CodigoAnt2 Then
                                NoError = False
                                Errores += " Ya existe una clasificación con ese código."
                            End If
                            If NoError Then
                                TC.Modificar(idTipo2, ComboBox2.Text, TextBox2.Text)
                                PopUp("Modificado", 90)
                                TextBox2.Focus()
                                'Nuevo2()
                            Else
                                MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                            End If
                        End If

                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If

                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        LlenaCombos("tblinventarioclasificaciones2", ComboBox2, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + idTipo.ToString, , "nombre")
    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Eliminar(idTipo2)
                    PopUp("Eliminado", 90)
                    Nuevo2()
                    ComboBox2.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta clasificación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Nuevo2()
        Nuevo3()
        Consulta()
        TextBox2.Focus()
    End Sub

    Private Sub Button10_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Try
            Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            Dim NoError As Boolean = True
            Dim Errores As String = ""
            '---------------
            If ComboBox3.Text <> "" And TextBox3.Text <> "" Then
                If Button10.Text = "Guardar" Then
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos) = True Then
                        If TC.ChecaNombreRepetido(ComboBox3.Text, idTipo2) Then
                            Errores += "Ya existe una clasificación con ese nombre."
                            NoError = False
                        End If
                        If TC.ChecaCodigoRepetido(TextBox3.Text, idTipo2) Then
                            NoError = False
                            Errores += " Ya existe una clasificación con ese código."
                        End If
                        If NoError Then
                            TC.Guardar(ComboBox3.Text, TextBox3.Text, idTipo2)
                            PopUp("Guardado", 90)
                            Nuevo3()
                        Else
                            MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                Else
                    If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos) = True Then
                        If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            If TC.ChecaNombreRepetido(ComboBox3.Text, idTipo2) And ComboBox3.Text <> NombreAnt3 Then
                                Errores += "Ya existe una clasificación con ese nombre."
                                NoError = False
                            End If
                            If TC.ChecaCodigoRepetido(TextBox3.Text, idTipo2) And TextBox3.Text <> CodigoAnt3 Then
                                NoError = False
                                Errores += " Ya existe una clasificación con ese código."
                            End If
                            If NoError Then
                                TC.Modificar(idTipo3, ComboBox3.Text, TextBox3.Text)
                                PopUp("Modificado", 90)
                                Nuevo3()
                            Else
                                MsgBox(Errores, MsgBoxStyle.Critical, GlobalNombreApp)
                            End If
                        End If
                    Else
                        MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
                TextBox3.Focus()
                '----------------------
            Else
                MsgBox("Debe indicar un nombre y código a la clasificación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        LlenaCombos("tblinventarioclasificaciones3", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + idTipo2.ToString, , "nombre")
    End Sub

    Private Sub Button9_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos) = True Then
                Dim TC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Eliminar(idTipo3)
                    PopUp("Eliminado", 90)
                    Nuevo3()
                    ComboBox3.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta clasificación debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Nuevo3()
        Consulta()
        TextBox3.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If Char.IsLower(e.KeyChar) Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If Char.IsLower(e.KeyChar) Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        Dim textBox As ComboBox = DirectCast(sender, ComboBox)
        If Char.IsLower(e.KeyChar) Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtDescripcion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescripcion.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtFabricante_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFabricante.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    Private Sub txtUbicacion_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUbicacion.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Char.IsLower(e.KeyChar) And textBox.Text.Length < textBox.MaxLength() Then
            textBox.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub

    
End Class