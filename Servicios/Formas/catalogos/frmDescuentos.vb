Public Class frmDescuentos
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim IdsClas4 As New elemento
    Dim idClasificacion As Integer
    Dim idClasificacion2 As Integer
    Dim idClasificacion3 As Integer
    Dim idClasificacion4 As Integer
    Dim idAlmacen As Integer = 0
    Dim filtro As Integer = 0
    Dim ConsultaOn As Boolean = False
    Dim checked As Boolean = False
    Dim checked2 As Boolean = True
    Dim TableAnadido As New DataTable
    Dim estado As Boolean
    Dim productos As String = "algo"
    Dim idDescuento As Integer
    Dim tipoDescuento As String
    Dim Descuento As String = ""
    Dim Folio As Integer
    Dim idSucursal As Integer
    Dim IdsSucursales2 As New elemento

    Private Sub frmDescuentos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = True
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas")
        LlenaCombos("tblsucursales", cmbSucursal, "nombre", "nombret", "idsucursal", IdsClas4, "", "Todas")
        LlenaCombos("tblsucursales", ComboBox1, "nombre", "nombret", "idsucursal", IdsSucursales2, "", "Todas")
        ConsultaOn = False
        filtroTodos()
        filtroDescuentos()
        Dim toolTip As New ToolTip
        toolTip.SetToolTip(btnAgregarTodos, "Agregar todos")
        toolTip.SetToolTip(Button2, "Quitar todos")
        toolTip.SetToolTip(btnAgregar, "Agregar seleccionados")
        toolTip.SetToolTip(btnQuitar, "Quitar seleccionados")

        toolTip.SetToolTip(btnGuardar, "Guardar descuento")
        toolTip.SetToolTip(btnEliminar, "Eliminar descuento")
        toolTip.SetToolTip(btnNuevo, "Nuevo descuento")
        Dim P As New dbDescuentos(MySqlcon)
        Folio = P.Folio()
        toolTip.SetToolTip(Button1, "Salir de la pantalla")
        tipoDescuentoPaneles()
        'TableAnadido.Columns.Add("selec")
        'TableAnadido.Columns.Add("id")
        'TableAnadido.Columns.Add("clave")
        'TableAnadido.Columns.Add("nombre")
        'TableAnadido.Columns.Add("cantidad")
        txtDescuento.Focus()


    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            idClasificacion = IdClas
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            '  ConsultaOn = False
            TextBox10.Text = IC2.Codigo
            ConsultaOn = True

            LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IdClas.ToString, "Todas", "codigo")
            'HabilitaClase2(True)
            ConsultaOn = False
            
        End If
        If ConsultaOn = False Then
            If ComboBox3.Text = "Todas" Then
                filtro = 0
                TextBox10.Text = ""
                If ComboBox6.Items.Count() > 0 Then
                    ComboBox6.Items.Clear()
                    ComboBox6.DataSource = Nothing
                End If
                If ComboBox7.Items.Count() > 0 Then
                    ComboBox7.Items.Clear()
                    ComboBox7.DataSource = Nothing
                End If
            Else
                filtro = 1

            End If
        End If

        filtros()
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas2.Valor(ComboBox6.SelectedIndex)
            idClasificacion2 = IdClas
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            '  ConsultaOn = False
            TextBox12.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IdClas.ToString, "Todas", "codigo")
            ConsultaOn = False
           
        End If
        If ConsultaOn = False Then


            If ComboBox6.Text = "Todas" Then
                filtro = 1
                TextBox12.Text = ""
           
                If ComboBox7.Items.Count() > 0 Then
                    ComboBox7.Items.Clear()
                    ComboBox7.DataSource = Nothing
                End If
            Else
                filtro = 2
            End If
        End If
        filtros()

    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        Dim IdClas As Integer
        IdClas = IdsClas.Valor(ComboBox7.SelectedIndex)
        idClasificacion3 = IdClas
        If ConsultaOn = False Then
            If ComboBox7.Text = "Todas" Then
                filtro = 2
                TextBox12.Text = ""
            Else
                filtro = 3
            End If
        End If
        filtros()
    End Sub
    Private Sub filtros()

        If filtro = 0 Then
            filtroTodos()
        End If
        If filtro = 1 Then
            filtro1()
        End If
        If filtro = 2 Then
            filtro2()
        End If
        If filtro = 3 Then
            filtro3()
        End If

    End Sub

    Private Sub filtroTodos()
        Try

            Dim I As New dbInventario(MySqlcon)
            DataGridView2.DataSource = I.Consulta2(idAlmacen, TextBox5.Text, , , True, , , , 0, GlobalpFabricante, GlobalModoBusqueda, 1)
            DataGridView2.Columns(0).HeaderText = "X"
            checked = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub filtroTodos2()
        Try

            Dim I As New dbInventario(MySqlcon)
            DataGridView2.DataSource = I.Consulta22(idAlmacen, TextBox5.Text, , , True, , , , 0, GlobalpFabricante, GlobalModoBusqueda, 1)
            DataGridView2.Columns(0).HeaderText = "X"
            checked = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub filtro1()
        Try

            Dim I As New dbInventario(MySqlcon)
            DataGridView2.DataSource = I.Consulta2(idAlmacen, TextBox5.Text, , idClasificacion, True, , , , 0, GlobalpFabricante, GlobalModoBusqueda, 1)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub filtro2()
        Try

            Dim I As New dbInventario(MySqlcon)
            DataGridView2.DataSource = I.Consulta2(idAlmacen, TextBox5.Text, , idClasificacion, True, idClasificacion2, , , 0, GlobalpFabricante, GlobalModoBusqueda, 1)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub filtro3()
        Try

            Dim I As New dbInventario(MySqlcon)
            DataGridView2.DataSource = I.Consulta2(idAlmacen, TextBox5.Text, , idClasificacion, True, idClasificacion2, idClasificacion3, , 0, GlobalpFabricante, GlobalModoBusqueda, 1)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        filtros()
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex = -1 Then
            btnAgregar.Enabled = False
            btnAgregarTodos.Enabled = False
            btnQuitar.Enabled = False
            Button2.Enabled = False
            If e.ColumnIndex = 0 Then
                Dim nfilas As Integer
                nfilas = DataGridView2.RowCount()
               

                If checked = True Then
                    Try
                        Dim I As New dbInventario(MySqlcon)
                        DataGridView2.DataSource = I.Consulta2(idAlmacen, TextBox5.Text, , , True, , , , 0, GlobalpFabricante, GlobalModoBusqueda, 1)
                        DataGridView2.Columns(0).HeaderText = "X"
                        checked = False
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                    End Try



                Else

                    Try

                        Dim I As New dbInventario(MySqlcon)
                        DataGridView2.DataSource = I.Consulta22(idAlmacen, TextBox5.Text, , , True, , , , 0, GlobalpFabricante, GlobalModoBusqueda, 1)
                        DataGridView2.Columns(0).HeaderText = " "
                        checked = True

                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                    End Try

                End If
                
            End If

            btnAgregar.Enabled = True
            btnAgregarTodos.Enabled = True
            btnQuitar.Enabled = True
            Button2.Enabled = True
        End If
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            If DataGridView2.Rows(e.RowIndex).Cells(0).Value = 1 Then

                DataGridView2.Rows(e.RowIndex).Cells(0).Value = 0

            ElseIf DataGridView2.Rows(e.RowIndex).Cells(0).Value = 0 Then

                DataGridView2.Rows(e.RowIndex).Cells(0).Value = 1

            End If
        End If

    End Sub

 
    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        'AGREGAR SELECCIONADOS
        Dim nfilas As Integer
        nfilas = DataGridView2.RowCount() - 1
        Dim idaux As String
        For i As Integer = 0 To nfilas
            If DataGridView2.Rows(i).Cells(0).Value = 1 Then

                idaux = DataGridView2.Rows(i).Cells(1).Value.ToString()

                If buscarCoincidencias(idaux) = 0 Then
                    Dim row As String() = New String() {"0", DataGridView2.Rows(i).Cells(1).Value.ToString(), DataGridView2.Rows(i).Cells(2).Value.ToString(), DataGridView2.Rows(i).Cells(3).Value.ToString(), DataGridView2.Rows(i).Cells(4).Value.ToString()}
                    DataGridView3.Rows.Add(row)
                End If
                

            End If
        Next
        
    End Sub

    Private Sub btnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitar.Click
        'Eliminar seleccionados

        Dim nfilas As Integer
        nfilas = DataGridView3.RowCount() - 1

        For i As Integer = nfilas To 0 Step -1
            If DataGridView3.Rows(i).Cells(0).Value = 1 Then
                DataGridView3.Rows.RemoveAt(i)
                DataGridView3.Refresh()
                nfilas = DataGridView3.RowCount() - 1
            End If
        Next
    End Sub

    Private Sub DataGridView3_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex = -1 Then
            btnAgregar.Enabled = False
            btnAgregarTodos.Enabled = False
            btnQuitar.Enabled = False
            Button2.Enabled = False
            If e.ColumnIndex = 0 Then
                Dim nfilas As Integer
                nfilas = DataGridView3.RowCount()
                If checked2 = True Then
                    'For i As Integer = 0 To nfilas - 1
                    'DataGridView3.Rows(i).Cells(0).Value = 1

                    DataGridView3.CurrentRow.Cells(0).Value = 1
                    'Next
                    DataGridView3.Columns(0).HeaderText = "X"
                    checked2 = False
                Else
                    'For i As Integer = 0 To nfilas - 1
                    '    DataGridView3.Rows(i).Cells(0).Value = 0
                    'Next
                    DataGridView3.CurrentRow.Cells(0).Value = 0
                    DataGridView3.Columns(0).HeaderText = " "
                    checked2 = True
                End If

            End If

            btnAgregar.Enabled = True
            btnAgregarTodos.Enabled = True
            btnQuitar.Enabled = True
            Button2.Enabled = True
        End If
        If e.ColumnIndex = 0 And e.RowIndex > -1 Then
            If DataGridView3.Rows(e.RowIndex).Cells(0).Value = 1 Then

                DataGridView3.Rows(e.RowIndex).Cells(0).Value = 0

            ElseIf DataGridView3.Rows(e.RowIndex).Cells(0).Value = 0 Then

                DataGridView3.Rows(e.RowIndex).Cells(0).Value = 1

            End If
        End If
    End Sub
    Private Function buscarCoincidencias(ByVal pid As String) As Integer
        Dim coincidencias As Integer = 0
        Dim nfilas As Integer
        nfilas = DataGridView3.RowCount() - 1

        For i As Integer = 0 To nfilas
            If DataGridView3.Rows(i).Cells(1).Value = pid Then
                coincidencias = coincidencias + 1
            End If
        Next
        Return coincidencias

    End Function


  

    Public Function fechaFormato(ByVal pfecha As DateTimePicker) As String
        Dim fechita As String
        fechita = pfecha.Value.ToString("yyyy/MM/dd")
        Return fechita
    End Function

    Public Function horaFormato(ByVal pfecha As DateTimePicker) As String
        Dim fechita As String
        Dim Aux As String = ""
        fechita = pfecha.Value.ToString("HH:mm:ss tt")

        For j As Integer = 0 To 7
            Aux = Aux + fechita.Chars(j)
        Next
        'Aux = Aux + fechita.Chars(11)
        fechita = Aux

        Return fechita
    End Function


    Public Sub Nuevo()
        Dim P As New dbDescuentos(MySqlcon)
        Folio = P.Folio()

        txtDescuento.Text = ""
        txtDescripcion.Text = ""
        RadioButton2.Checked = True
        RadioButton1.Checked = False
        btnGuardar.Text = "Guardar"
        btnEliminar.Enabled = False
        filtroDescuentos()
        If DataGridView3.RowCount() > 0 Then
            DataGridView3.Rows.Clear()                          'para limpiarlo
            DataGridView3.DataSource = Nothing
            DataGridView3.Refresh()
        End If
        ' ComboBox3.Items(

        If ComboBox6.Items.Count() > 0 Then
            ComboBox6.Items.Clear()
            ComboBox6.DataSource = Nothing
        End If
        If ComboBox7.Items.Count() > 0 Then
            ComboBox7.Items.Clear()
            ComboBox7.DataSource = Nothing
        End If
        ConsultaOn = True
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas")
        ConsultaOn = False
        filtroTodos()
        TextBox13.Text = ""
        TextBox12.Text = ""
        TextBox10.Text = ""
        nmrFinal.Value = 1
        nmrInicio.Value = 1
        txtEfectivo.Text = ""
        rdbPorcentaje.Checked = True
        txtDescuento.Focus()
    End Sub

    

    Private Function cargarProductos() As String

        productos = ""
        For i As Integer = 0 To DataGridView3.RowCount() - 1

            productos = productos + DataGridView3.Rows(i).Cells(1).Value + ","

        Next
        Return productos
    End Function

    Private Sub filtroDescuentos()
        Try

            Dim P As New dbDescuentos(MySqlcon)
            DataGridView1.DataSource = P.filtroDescuentos(txtBusqueda.Text, IdsSucursales2.Valor(ComboBox1.SelectedIndex))
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            ' DataGridView2.Columns(1).HeaderText = "Código"
            DataGridView1.Columns(2).HeaderText = "Descuento"
            DataGridView1.Columns(3).HeaderText = "Descripción"
            DataGridView1.Columns(4).HeaderText = "Fecha I"
            DataGridView1.Columns(5).HeaderText = "Hora I"
            DataGridView1.Columns(6).HeaderText = "Fecha F"
            DataGridView1.Columns(7).HeaderText = "Hora F"
            DataGridView1.Columns(8).HeaderText = "Productos"
            DataGridView1.Columns(9).HeaderText = "Tipo"
            DataGridView1.Columns(8).Visible = False
            DataGridView1.Columns(10).Visible = False
            DataGridView1.Columns(11).Visible = False
            DataGridView1.Columns(12).Visible = False
            DataGridView1.Columns(13).Visible = False
            DataGridView1.Columns(2).Width = 70
            DataGridView1.Columns(3).Width = 150
            DataGridView1.Columns(4).Width = 70
            DataGridView1.Columns(5).Width = 80
            DataGridView1.Columns(6).Width = 70
            DataGridView1.Columns(7).Width = 80
            DataGridView1.Columns(8).Width = 70
            DataGridView1.Columns(9).Width = 80
            ' DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DataGridView2.Columns(1).Width = 180
            'DGConsulta.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub txtBusqueda_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBusqueda.TextChanged
        filtroDescuentos()
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            filtroTodos()

            idDescuento = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            'txtDescuento.Text = 
            estado = Boolean.Parse(DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value)
            If estado = True Then
                RadioButton2.Checked = False
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
                RadioButton1.Checked = False
            End If
            txtDescripcion.Text = DataGridView1.Item(3, DataGridView1.CurrentCell.RowIndex).Value
            dtpFechai.Value = (Date.ParseExact(DataGridView1.Item(4, DataGridView1.CurrentCell.RowIndex).Value, "yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo))
            dtpFechaf.Value = (Date.ParseExact(DataGridView1.Item(6, DataGridView1.CurrentCell.RowIndex).Value, "yyyy/MM/dd", System.Globalization.DateTimeFormatInfo.InvariantInfo))
            dtpHorai.Value = Convert.ToDateTime(DataGridView1.Item(5, DataGridView1.CurrentCell.RowIndex).Value.ToString)
            dtpHoraf.Value = Convert.ToDateTime(DataGridView1.Item(7, DataGridView1.CurrentCell.RowIndex).Value.ToString)

            Folio = Integer.Parse(DataGridView1.Item(10, DataGridView1.CurrentCell.RowIndex).Value.ToString)
            desplegarProductos()
            If DataGridView1.Item(9, DataGridView1.CurrentCell.RowIndex).Value.ToString() = "Porcentaje" Then
                rdbPorcentaje.Checked = True
                tipoDescuento = "Porcentaje"
                txtDescuento.Text = DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex).Value
                txtEfectivo.Text = ""
                nmrFinal.Value = 1
                nmrInicio.Value = 1
            End If
            If DataGridView1.Item(9, DataGridView1.CurrentCell.RowIndex).Value.ToString() = "Efectivo" Then
                rdbEfectivo.Checked = True
                tipoDescuento = "Efectivo"
                txtEfectivo.Text = DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex).Value
                nmrFinal.Value = 1
                nmrInicio.Value = 1
                txtDescuento.Text = ""
            End If
            If DataGridView1.Item(9, DataGridView1.CurrentCell.RowIndex).Value.ToString() = "Promocion" Then
                rdbPromocion.Checked = True
                tipoDescuento = "Promocion"
                asignarPromocion(DataGridView1.Item(2, DataGridView1.CurrentCell.RowIndex).Value)
                txtDescuento.Text = ""
                txtEfectivo.Text = ""
            End If

            cmbSucursal.SelectedIndex = IdsClas4.Busca(DataGridView1.Item(13, DataGridView1.CurrentCell.RowIndex).Value)
            'botones
            btnGuardar.Text = "Modificar"
            btnEliminar.Enabled = True
            txtDescuento.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub desplegarProductos()
        Dim id As Integer
        Dim fila As Integer
        ' Dim letras As Integer
        Dim aux As String = ""
        Dim P As New dbDescuentos(MySqlcon)
        Dim Tabla As DataTable
        If DataGridView3.RowCount() > 0 Then
            DataGridView3.Rows.Clear()                          'para limpiarlo
            DataGridView3.DataSource = Nothing
            DataGridView3.Refresh()
        End If
        Tabla = P.Productos(Folio).ToTable()




        For i As Integer = 0 To Tabla.Rows.Count - 1

            id = Integer.Parse(Tabla.Rows(i)(0).ToString())
            fila = buscarFila(id)
            Dim row As String() = New String() {"0", DataGridView2.Rows(fila).Cells(1).Value.ToString(), DataGridView2.Rows(fila).Cells(2).Value.ToString(), DataGridView2.Rows(fila).Cells(3).Value.ToString(), DataGridView2.Rows(fila).Cells(4).Value.ToString()}
            DataGridView3.Rows.Add(row)
            'aqui me quede hay que probarlo con el ultimo registro
        Next

    End Sub

    Private Function buscarFila(ByVal id As Integer) As Integer
        Dim fila As Integer = 0

        For i As Integer = 0 To DataGridView2.RowCount() - 1
            If DataGridView2.Rows(i).Cells(1).Value = id Then
                fila = i
                i = DataGridView2.RowCount() - 1
            End If
        Next
        Return fila
    End Function




    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarTodos.Click
        'AGREGAR Todos
        Dim nfilas As Integer
        nfilas = DataGridView2.RowCount() - 1
        ' Dim idaux As String
        For i As Integer = 0 To nfilas
            ' If DataGridView2.Rows(i).Cells(0).Value = 1 Then

            ' idaux = DataGridView2.Rows(i).Cells(1).Value.ToString()

            ' If buscarCoincidencias(idaux) = 0 Then
            Dim row As String() = New String() {"0", DataGridView2.Rows(i).Cells(1).Value.ToString(), DataGridView2.Rows(i).Cells(2).Value.ToString(), DataGridView2.Rows(i).Cells(3).Value.ToString(), DataGridView2.Rows(i).Cells(4).Value.ToString()}
            DataGridView3.Rows.Add(row)
            '  End If


            ' End If
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Eliminar seleccionados
        DataGridView3.Rows.Clear()
        DataGridView3.DataSource = Nothing
        DataGridView3.Refresh()
        DataGridView3.Columns(0).HeaderText = " "
        checked2 = True

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ComboBox3.SelectedIndex = 0
        TextBox5.Text = ""
    End Sub

    Private Sub tipoDescuentoPaneles()

        If rdbPorcentaje.Checked = True Then
            pnlPorcentaje.Visible = True
            pnlEfectivo.Visible = False
            pnlPromocion.Visible = False

            tipoDescuento = "Porcentaje"
        End If
        If rdbEfectivo.Checked = True Then
            pnlPorcentaje.Visible = False
            pnlEfectivo.Visible = True
            pnlPromocion.Visible = False

            tipoDescuento = "Efectivo"
        End If
        If rdbPromocion.Checked = True Then
            pnlPorcentaje.Visible = False
            pnlEfectivo.Visible = False
            pnlPromocion.Visible = True

            tipoDescuento = "Promocion"
        End If
    End Sub

    Private Sub rdbPorcentaje_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPorcentaje.CheckedChanged
        tipoDescuentoPaneles()
    End Sub

    Private Sub rdbEfectivo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbEfectivo.CheckedChanged
        tipoDescuentoPaneles()
    End Sub

    Private Sub rdbPromocion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPromocion.CheckedChanged
        tipoDescuentoPaneles()
    End Sub

    Private Sub obtDescuento()
        Descuento = ""
        If rdbEfectivo.Checked = True Then
            Descuento = txtEfectivo.Text
        End If
        If rdbPorcentaje.Checked = True Then
            Descuento = txtDescuento.Text
        End If
        If rdbPromocion.Checked = True Then
            Descuento = nmrInicio.Value.ToString() + "x" + nmrFinal.Value.ToString()
        End If
    End Sub

    Private Sub asignarPromocion(ByVal prom As String)
        Dim aux As String = ""
        ' letras = prod.Length() - 1

        For i As Integer = 0 To prom.Length() - 1
            If prom.Chars(i) = "x" Then
                nmrInicio.Value = Integer.Parse(aux)
                aux = ""
            Else
                aux = aux + prom.Chars(i)
            End If
        Next
        nmrFinal.Value = Integer.Parse(aux)

    End Sub

   
    Private Sub txtDescuento_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescuento.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub dtpFechai_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechai.ValueChanged
        'dtpFechai.Value = Date.Now
        dtpFechaf.Value = dtpFechai.Value
    End Sub

    Private Sub dtpFechaf_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFechaf.ValueChanged
        ' dtpFechai.Value = Date.Now

        If dtpFechaf.Value < dtpFechai.Value Then
            dtpFechaf.Value = dtpFechai.Value
        End If
    End Sub

    Private Sub txtEfectivo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEfectivo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or (e.KeyChar = "." And textBox.Text.IndexOf(".") < 0)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

   
    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Dim NoErrores As Boolean = True
        Dim MensajeError As String = ""
        'Dim res As Integer
        txtDescuento.BackColor = Color.White
        txtDescripcion.BackColor = Color.White
        Dim P As New dbDescuentos(MySqlcon)
        obtDescuento()
        If Descuento = "" Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar un descuento."
            ' txtDescuento.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If txtDescripcion.Text = "" Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe indicar una descripción."
            txtDescripcion.BackColor = Color.FromArgb(250, 150, 150)
        End If
        If DataGridView3.RowCount() = 0 Then
            NoErrores = False
            MensajeError += vbCrLf + "Debe asignar productos al descuento."
            ' txtDescuento.BackColor = Color.FromArgb(250, 150, 150)
        End If
        'For i As Integer = 0 To DataGridView3.RowCount() - 1
        '    res = P.HayDescuento(Integer.Parse(DataGridView3.Rows(i).Cells(1).Value), fechaFormato(dtpFechai) + " " + horaFormato(dtpHorai), fechaFormato(dtpFechaf) + " " + horaFormato(dtpHoraf), idSucursal)
        '    If res <> 0 Then
        '        NoErrores = False
        '        MensajeError += vbCrLf + "El producto " + DataGridView3.Rows(i).Cells(3).Value + " existe en otra promoción."
        '    End If
        'Next
        'If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos.AlmacenesAlta, PermisosN.Secciones.Catalagos) = False Then
        '    NoErrores = False
        '    MensajeError += " No tiene permiso para realizar esta operación."
        'End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.OfertasAlta, PermisosN.Secciones.Catalagos2) = False And btnGuardar.Text = "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.OfertasCambio, PermisosN.Secciones.Catalagos2) = False And btnGuardar.Text <> "Guardar" Then
            NoErrores = False
            MensajeError += " No tiene permiso para realizar esta operación."
        End If

        If NoErrores = True Then
            If btnGuardar.Text = "Guardar" Then
                If RadioButton1.Checked = True Then
                    estado = True

                Else
                    estado = False
                End If
                ' cargarProductos()
                '   For i As Integer = 0 To DataGridView3.RowCount() - 1
                'P.Guardar(estado, Descuento, txtDescripcion.Text, fechaFormato(dtpFechai), horaFormato(dtpHorai), fechaFormato(dtpFechaf), horaFormato(dtpHoraf), DataGridView3.Rows(i).Cells(1).Value, tipoDescuento, Folio)
                P.Guardar2(estado, Descuento, txtDescripcion.Text, fechaFormato(dtpFechai), horaFormato(dtpHorai), fechaFormato(dtpFechaf), horaFormato(dtpHoraf), DataGridView3, tipoDescuento, Folio, idSucursal)
                '  Next

                PopUp("Guardado", 90)
                filtroDescuentos()
                Nuevo()
                'btnEliminar.Enabled = True
                'btnGuardar.Text = "Modificar"
                '            btnDiseno.Enabled = True
                '            txtNombre.Focus()

            Else
                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    '            Dim ClaveRepetida As Boolean = False
                    'cargarProductos()
                    P.Eliminar(Folio)
                    If RadioButton1.Checked = True Then
                        estado = True

                    Else
                        estado = False
                    End If
                    ' For i As Integer = 0 To DataGridView3.RowCount() - 1
                    '  P.Guardar(estado, Descuento, txtDescripcion.Text, fechaFormato(dtpFechai), horaFormato(dtpHorai), fechaFormato(dtpFechaf), horaFormato(dtpHoraf), DataGridView3.Rows(i).Cells(1).Value, tipoDescuento, Folio)
                    'Next
                    P.Guardar2(estado, Descuento, txtDescripcion.Text, fechaFormato(dtpFechai), horaFormato(dtpHorai), fechaFormato(dtpFechaf), horaFormato(dtpHoraf), DataGridView3, tipoDescuento, Folio, idSucursal)
                    PopUp("Modificado", 90)
                    filtroDescuentos()
                    Nuevo()
                    '            btnDiseno.Enabled = True
                    '            txtNombre.Focus()
                End If
            End If
        End If


        If NoErrores = False Then
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)

        End If
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.OfertasBaja, PermisosN.Secciones.Catalagos2) = True Then
                If MsgBox("¿Desea eliminar este descuento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbDescuentos(MySqlcon)
                    P.Eliminar(Folio)
                    PopUp("Eliminado", 90)
                    Nuevo()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Nuevo()
        rdbPorcentaje.Checked = True
    End Sub

    Private Sub DataGridView2_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
    End Sub

    Private Sub DataGridView1_ClientSizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.ClientSizeChanged

    End Sub

    Private Sub cmbSucursal_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        If cmbSucursal.SelectedIndex <> 0 Then
            idSucursal = IdsClas4.Valor(cmbSucursal.SelectedIndex)
        Else
            idSucursal = 0
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        filtroDescuentos()
    End Sub
End Class