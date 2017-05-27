Public Class frmContabilidadMascaras
    Dim Mascara As dbContabilidadMascaras
    Dim Detalles As dbContabilidadMascarasDetalles
    Dim ConsultaOn As Boolean = False
    Dim IdsVariables As New elemento
    Dim IdsVariables2 As New elemento
    Dim IdsSucursales As New elemento
    Dim IdsClasificaciones As New elemento
    Dim IdsTipoSuc As New elemento
    'Dim IdCuenta As Integer
    Dim p As New dbContabilidadPolizas(MySqlcon)
    'Dim C As dbContabilidadClasificacion

    Dim IdsNivel1 As New elemento
    Dim IdsNivel2 As New elemento
    Dim IdsNivel3 As New elemento
    Dim IdsNivel4 As New elemento
    Dim IdsNivel5 As New elemento
    Private Sub frmContabilidadMascaras_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Mascara.Estado = Estados.SinGuardar Then
            If MsgBox("Esta máscara no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Mascara.Eliminar(Mascara.ID)
                e.Cancel = False
            Else
                'GlobalEstadoVentanas = GlobalEstadoVentanas And Not 1
                e.Cancel = True
            End If
            'Else
            'GlobalEstadoVentanas = GlobalEstadoVentanas And Not 1
        End If
    End Sub
    Private Sub frmContabilidadMascaras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Mascara = New dbContabilidadMascaras(MySqlcon)
        Detalles = New dbContabilidadMascarasDetalles(MySqlcon)

        p = New dbContabilidadPolizas(MySqlcon)
        SelectorCuentas1.P = p
        SelectorCuentas1.C = New dbContabilidadClasificacion(MySqlcon)
        SelectorCuentas1.Inicializar()
        'P.llenaDatosConfig()

        LlenaCombos("tblsucursales", ComboBox4, "nombre", "nombret", "idsucursal", IdsSucursales, "", "Todas")
        LlenaCombos("tblContabilidadClas", ComboBox5, "nombre", "nombret", "id", IdsClasificaciones)
        'LlenaCombos("tblccontables", ComboNivel1, "concat(LPAD(Cuenta," + P.NNiv1.ToString + ",'0'),' ',descripcion)", "descr", "idccontable", IdsNivel1, "n2='' and n3='' and n4='' and n5=''", , "descr")
        LlenaCombos("tblsucursalestipos", ComboBox11, "nombre", "nombrec", "idtipo", IdsTipoSuc, , "Todos")
        ComboBox2.Items.Add("Ventas") '0
        ComboBox2.Items.Add("Compras") '1
        ComboBox2.Items.Add("Ventas Devoluciones") '2
        ComboBox2.Items.Add("Compras Devoluciones") '3
        ComboBox2.Items.Add("Depósitos") '4
        ComboBox2.Items.Add("Pagos") '5
        ComboBox2.Items.Add("Notas de crédito ventas") '6
        ComboBox2.Items.Add("Notas de crédito compras") '7
        ComboBox2.Items.Add("Notas de cargo ventas") '8
        ComboBox2.Items.Add("Notas de cargo compras") '9
        ComboBox2.Items.Add("Nómina") '10
        ComboBox2.Items.Add("Movimientos inventario") '11
        ComboBox2.Items.Add("Documentos Proveedores") '12
        ComboBox10.Items.Add("Ventas") '0
        ComboBox10.Items.Add("Compras") '1
        ComboBox10.Items.Add("Ventas Devoluciones") '2
        ComboBox10.Items.Add("Compras Devoluciones") '3
        ComboBox10.Items.Add("Depósitos") '4
        ComboBox10.Items.Add("Pagos") '5
        ComboBox10.Items.Add("Notas de crédito ventas") '6
        ComboBox10.Items.Add("Notas de crédito compras") '7
        ComboBox10.Items.Add("Notas de cargo ventas") '8
        ComboBox10.Items.Add("Notas de cargo compras") '9
        ComboBox10.Items.Add("Nómina") '10
        ComboBox10.Items.Add("Movimientos inventario") '11
        ComboBox10.Items.Add("Documentos Proveedores") '12

        ComboBox1.Items.Add("Mensual")
        ComboBox1.Items.Add("Semanal")
        ComboBox1.Items.Add("Diario")
        ComboBox1.Items.Add("Por Movimiento")
        ComboBox1.Items.Add("Por Rango")

        ComboBox6.Items.Add("Todos")
        ComboBox6.Items.Add("Solo Vigentes")
        ComboBox6.Items.Add("Solo Canceladas")
        ComboBox7.Items.Add("Egreso") 'E
        ComboBox7.Items.Add("Ingreso") 'I
        ComboBox7.Items.Add("Diario") 'D
        'ComboBox7.Items.Add("A") 'E
        'I Ingreso
        'D Diario
        'A Apertura
        ComboBox8.Items.Add("Contado")
        ComboBox8.Items.Add("Crédito")
        ComboBox8.Items.Add("Todos")

        Nuevo()
    End Sub
    Private Sub Nuevo()
        ComboBox2.SelectedIndex = 0
        ComboBox1.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        ComboBox6.SelectedIndex = 0
        ComboBox5.SelectedIndex = 0
        ComboBox7.SelectedIndex = 0
        ComboBox8.SelectedIndex = 0
        ComboBox10.SelectedIndex = 0
        ComboBox11.SelectedIndex = 0
        'ComboBox2.Enabled = True
        TextBox1.Text = ""
        CheckBox1.Checked = True
        Mascara.ID = 0
        NuevoConcepto()
        Button7.Enabled = True
        Button9.Enabled = False
        TextBox1.Focus()
        Mascara.Estado = Estados.Inicio
    End Sub
    Private Sub NuevoConcepto()
        ConsultaOn = False
        ConsultaOn = True
        'Resetear cuenta
        SelectorCuentas1.Vacia()
        Detalles.ID = 0
        RadioButton1.Checked = True
        Button1.Text = "Agregar"
        Button3.Enabled = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        ComboBox3.Focus()
        ComboBox10.Enabled = True
        ConsultaDetalles()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        NuevoConcepto()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasAlta, PermisosN.Secciones.Contabilidad) = False Then
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Mascara.Estado = Estados.Inicio Then
            Guardar()
        End If
        If Mascara.Estado = Estados.SinGuardar Or Mascara.Estado = Estados.Guardada Then
            AgregaConcepto()
        End If
    End Sub
    Private Sub Guardar()
        Try
            Dim HayError As String = ""
            If TextBox1.Text = "" Then
                HayError = "Debe indicar un título."
            End If
            If HayError = "" Then
                Dim Act As Byte = 0
                Dim IdTipos As Integer
                IdTipos = IdsTipoSuc.Valor(ComboBox11.SelectedIndex)
                If IdTipos < 0 Then IdTipos = 0
                'Dim Can As Byte = 0
                Dim TipoP As String = ""
                If ComboBox7.SelectedIndex = 0 Then TipoP = "E"
                If ComboBox7.SelectedIndex = 1 Then TipoP = "I"
                If ComboBox7.SelectedIndex = 2 Then TipoP = "D"
                If CheckBox1.Checked Then Act = 1
                'If CheckBox2.Checked Then Can = 0
                Dim IdSuc As Integer = IdsSucursales.Valor(ComboBox4.SelectedIndex)
                If IdSuc < 0 Then IdSuc = 0
                Mascara.Guardar(ComboBox2.SelectedIndex, ComboBox1.SelectedIndex, TextBox1.Text.Trim, Act, ComboBox8.SelectedIndex, ComboBox6.SelectedIndex, IdSuc, IdsClasificaciones.Valor(ComboBox5.SelectedIndex), TipoP, IdTipos)
                Button7.Enabled = False
                Mascara.Estado = Estados.SinGuardar
            Else
                MsgBox(HayError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            Mascara.Estado = Estados.Inicio
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
    End Sub
    Private Sub AgregaConcepto()
        Try
            Dim cargo As Byte = 0
            Dim abono As Byte = 0
            Dim HayError As String = ""
            If RadioButton1.Checked Then
                cargo = 1
            Else
                abono = 1
            End If
            'percepciones
            If (SelectorCuentas1.txtCuenta.Text.Contains("*P") Or SelectorCuentas1.txtCuenta.Text.Contains("*p")) And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.IdCuenta = -1
            End If
            'deducciones
            If (SelectorCuentas1.txtCuenta.Text.Contains("*D") Or SelectorCuentas1.txtCuenta.Text.Contains("*d")) And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.IdCuenta = -2
            End If
            'bancos pagos y depositos
            If (SelectorCuentas1.txtCuenta.Text.Contains("*B") Or SelectorCuentas1.txtCuenta.Text.Contains("*b")) And (ComboBox10.SelectedIndex = 4 Or ComboBox10.SelectedIndex = 5) Then
                If (SelectorCuentas1.txtCuenta.Text.Contains("*B2") Or SelectorCuentas1.txtCuenta.Text.Contains("*b2")) Then
                    SelectorCuentas1.IdCuenta = -9
                Else
                    If (SelectorCuentas1.txtCuenta.Text.Contains("*B3") Or SelectorCuentas1.txtCuenta.Text.Contains("*b3")) Then
                        SelectorCuentas1.IdCuenta = -10
                    Else
                        If (SelectorCuentas1.txtCuenta.Text.Contains("*B4") Or SelectorCuentas1.txtCuenta.Text.Contains("*b4")) Then
                            SelectorCuentas1.IdCuenta = -11
                        Else
                            SelectorCuentas1.IdCuenta = -3
                        End If
                    End If
                End If

            End If
            'clientes depositos
            If (SelectorCuentas1.txtCuenta.Text.Contains("*C") Or SelectorCuentas1.txtCuenta.Text.Contains("*c")) And ComboBox10.SelectedIndex = 4 Then
                If (SelectorCuentas1.txtCuenta.Text.Contains("*C2") Or SelectorCuentas1.txtCuenta.Text.Contains("*c2")) Then
                    SelectorCuentas1.IdCuenta = -12
                Else
                    If (SelectorCuentas1.txtCuenta.Text.Contains("*C3") Or SelectorCuentas1.txtCuenta.Text.Contains("*c3")) Then
                        SelectorCuentas1.IdCuenta = -13
                    Else
                        If (SelectorCuentas1.txtCuenta.Text.Contains("*C4") Or SelectorCuentas1.txtCuenta.Text.Contains("*c4")) Then
                            SelectorCuentas1.IdCuenta = -14
                        Else
                            SelectorCuentas1.IdCuenta = -4
                        End If
                    End If
                End If

            End If
            'proveedores pagos bancos
            If (SelectorCuentas1.txtCuenta.Text.Contains("*P") Or SelectorCuentas1.txtCuenta.Text.Contains("*p")) And ComboBox10.SelectedIndex = 5 Then
                If (SelectorCuentas1.txtCuenta.Text.Contains("*P2") Or SelectorCuentas1.txtCuenta.Text.Contains("*p2")) Then
                    SelectorCuentas1.IdCuenta = -15
                Else
                    If (SelectorCuentas1.txtCuenta.Text.Contains("*P3") Or SelectorCuentas1.txtCuenta.Text.Contains("*p3")) Then
                        SelectorCuentas1.IdCuenta = -16
                    Else
                        If (SelectorCuentas1.txtCuenta.Text.Contains("*P4") Or SelectorCuentas1.txtCuenta.Text.Contains("*p4")) Then
                            SelectorCuentas1.IdCuenta = -17
                        Else
                            SelectorCuentas1.IdCuenta = -5
                        End If
                    End If
                End If

            End If
            'clientes ventas
            If (SelectorCuentas1.txtCuenta.Text.Contains("*C") Or SelectorCuentas1.txtCuenta.Text.Contains("*c")) And (ComboBox10.SelectedIndex = 0 Or ComboBox10.SelectedIndex = 2 Or ComboBox10.SelectedIndex = 6 Or ComboBox10.SelectedIndex = 8) Then
                If (SelectorCuentas1.txtCuenta.Text.Contains("*C2") Or SelectorCuentas1.txtCuenta.Text.Contains("*c2")) Then
                    SelectorCuentas1.IdCuenta = -18
                Else
                    If (SelectorCuentas1.txtCuenta.Text.Contains("*C3") Or SelectorCuentas1.txtCuenta.Text.Contains("*c3")) Then
                        SelectorCuentas1.IdCuenta = -19
                    Else
                        If (SelectorCuentas1.txtCuenta.Text.Contains("*C4") Or SelectorCuentas1.txtCuenta.Text.Contains("*c4")) Then
                            SelectorCuentas1.IdCuenta = -20
                        Else
                            SelectorCuentas1.IdCuenta = -6
                        End If
                    End If
                End If

            End If
            'proveedores compras
            If (SelectorCuentas1.txtCuenta.Text.Contains("*P") Or SelectorCuentas1.txtCuenta.Text.Contains("*p")) And (ComboBox10.SelectedIndex = 1 Or ComboBox10.SelectedIndex = 3 Or ComboBox10.SelectedIndex = 7 Or ComboBox10.SelectedIndex = 9 Or ComboBox10.SelectedIndex = 12) Then
                If (SelectorCuentas1.txtCuenta.Text.Contains("*P2") Or SelectorCuentas1.txtCuenta.Text.Contains("*p2")) Then
                    SelectorCuentas1.IdCuenta = -21
                Else
                    If (SelectorCuentas1.txtCuenta.Text.Contains("*P3") Or SelectorCuentas1.txtCuenta.Text.Contains("*p3")) Then
                        SelectorCuentas1.IdCuenta = -22
                    Else
                        If (SelectorCuentas1.txtCuenta.Text.Contains("*P4") Or SelectorCuentas1.txtCuenta.Text.Contains("*p4")) Then
                            SelectorCuentas1.IdCuenta = -23
                        Else
                            SelectorCuentas1.IdCuenta = -7
                        End If
                    End If
                End If

            End If
            'trabajadores nomina
            If (SelectorCuentas1.txtCuenta.Text.Contains("*T") Or SelectorCuentas1.txtCuenta.Text.Contains("*t")) And ComboBox10.SelectedIndex = 10 Then
                If (SelectorCuentas1.txtCuenta.Text.Contains("*T2") Or SelectorCuentas1.txtCuenta.Text.Contains("*t2")) Then
                    SelectorCuentas1.IdCuenta = -24
                Else
                    If (SelectorCuentas1.txtCuenta.Text.Contains("*T3") Or SelectorCuentas1.txtCuenta.Text.Contains("*t3")) Then
                        SelectorCuentas1.IdCuenta = -25
                    Else
                        If (SelectorCuentas1.txtCuenta.Text.Contains("*T4") Or SelectorCuentas1.txtCuenta.Text.Contains("*t4")) Then
                            SelectorCuentas1.IdCuenta = -26
                        Else
                            SelectorCuentas1.IdCuenta = -8
                        End If
                    End If
                End If

            End If

            'Almacenes
            If (SelectorCuentas1.txtCuenta.Text.Contains("*A") Or SelectorCuentas1.txtCuenta.Text.Contains("*a")) And ComboBox10.SelectedIndex = 11 Then
                If (SelectorCuentas1.txtCuenta.Text.Contains("*A2") Or SelectorCuentas1.txtCuenta.Text.Contains("*a2")) Then
                    SelectorCuentas1.IdCuenta = -28
                Else
                    If (SelectorCuentas1.txtCuenta.Text.Contains("*A3") Or SelectorCuentas1.txtCuenta.Text.Contains("*a3")) Then
                        SelectorCuentas1.IdCuenta = -29
                    Else
                        If (SelectorCuentas1.txtCuenta.Text.Contains("*A4") Or SelectorCuentas1.txtCuenta.Text.Contains("*a4")) Then
                            SelectorCuentas1.IdCuenta = -30
                        Else
                            SelectorCuentas1.IdCuenta = -27
                        End If
                    End If
                End If

            End If

            'checar si es ultimo nivel
            If SelectorCuentas1.IdCuenta >= 0 Then
                Dim Cc As New dbCContables(MySqlcon)
                If Cc.UtlimoNivel(SelectorCuentas1.IdCuenta, SelectorCuentas1.Nivel) <> 0 Then
                    HayError = "Debe seleccionar una cuenta de último nivel."
                End If
            End If
            If SelectorCuentas1.IdCuenta = 0 Then
                HayError = " Debe seleccionar una cuenta"
            End If
            Dim Neg As Byte
            If CheckBox2.Checked Then
                Neg = 1
            Else
                Neg = 0
            End If
            Dim Bene As Byte
            If CheckBox3.Checked Then
                Bene = 1
            Else
                Bene = 0
            End If
            Dim Uuids As Byte
            If CheckBox4.Checked Then
                Uuids = 1
            Else
                Uuids = 0
            End If
            Dim pagoinfo As Byte
            If CheckBox5.Checked Then
                pagoinfo = 1
            Else
                pagoinfo = 0
            End If
            If HayError = "" Then
                If Button1.Text = "Agregar" Then
                    Detalles.Guardar(IdsVariables.Valor(ComboBox3.SelectedIndex), SelectorCuentas1.IdCuenta, cargo, abono, Mascara.ID, Neg, Bene, ComboBox10.SelectedIndex, Uuids, pagoinfo)
                Else
                    Detalles.Modificar(Detalles.ID, IdsVariables.Valor(ComboBox3.SelectedIndex), SelectorCuentas1.IdCuenta, cargo, abono, Neg, Bene, Uuids, pagoinfo)
                End If
                NuevoConcepto()
            Else
                MsgBox(HayError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
        
    End Sub
    Private Sub ConsultaDetalles()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
                'Dim P As New dbFertilizantesMovimientos(MySqlcon)
                DGDetalles.DataSource = Detalles.Consulta(Mascara.ID, P.NNiv1, P.NNiv2, P.NNiv3, P.NNiv4, P.NNiv5)
                DGDetalles.Columns(0).Visible = False
                'DGDetalles.Columns(1).Visible = False
                'DGDetalles.Columns(8).Visible = False
                DGDetalles.Columns(1).HeaderText = "Variable"
                DGDetalles.Columns(2).HeaderText = "Módulo"
                DGDetalles.Columns(3).HeaderText = "Cuenta"
                DGDetalles.Columns(4).HeaderText = "Descripción"
                DGDetalles.Columns(5).HeaderText = "Cargo"
                DGDetalles.Columns(6).HeaderText = "Abono"
                DGDetalles.Columns(7).HeaderText = "B"
                DGDetalles.Columns(8).HeaderText = "P.I."
                DGDetalles.Columns(9).HeaderText = "Uuid"
                DGDetalles.Columns(1).Width = 300
                DGDetalles.Columns(3).Width = 190
                DGDetalles.Columns(5).Width = 50
                DGDetalles.Columns(6).Width = 50
                DGDetalles.Columns(7).Width = 30
                DGDetalles.Columns(8).Width = 30
                DGDetalles.Columns(9).Width = 30
                DGDetalles.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                DGDetalles.ClearSelection()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub LlenaDatos()
        Try
            Mascara.LlenaDatos()
            ComboBox2.SelectedIndex = Mascara.Modulo
            ComboBox1.SelectedIndex = Mascara.Tipo
            ComboBox4.SelectedIndex = IdsSucursales.Busca(Mascara.IdSucursal)
            ComboBox5.SelectedIndex = IdsClasificaciones.Busca(Mascara.IdClasificacion)
            ComboBox6.SelectedIndex = Mascara.Canceladas
            If Mascara.TipoPoliza = "E" Then ComboBox7.SelectedIndex = 0
            If Mascara.TipoPoliza = "I" Then ComboBox7.SelectedIndex = 1
            If Mascara.TipoPoliza = "D" Then ComboBox7.SelectedIndex = 2
            TextBox1.Text = Mascara.Titulo
            If Mascara.Activo = 1 Then
                CheckBox1.Checked = True
            Else
                CheckBox1.Checked = False
            End If
            'ComboBox2.Enabled = False
            ComboBox8.SelectedIndex = Mascara.Credito
            ComboBox11.SelectedIndex = IdsTipoSuc.Busca(Mascara.IdTipoS)
            Button7.Enabled = False
            NuevoConcepto()
            Button9.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
        
    End Sub
    Private Sub Modificar()
        Try
            Dim Hayerror As String = ""
            If TextBox1.Text = "" Then
                Hayerror = "Debe indicar un título."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasAlta, PermisosN.Secciones.Contabilidad) = False And Button9.Enabled = False Then
                Hayerror += "No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad) = False And Button9.Enabled = True Then
                Hayerror += "No tiene permiso para realizar esta operación"
            End If
            If Hayerror = "" Then
                Dim Act As Byte = 0
                'Dim Can As Byte = 0
                If CheckBox1.Checked Then Act = 1
                Dim IdTipos As Integer
                IdTipos = IdsTipoSuc.Valor(ComboBox11.SelectedIndex)
                If IdTipos < 0 Then IdTipos = 0
                'If CheckBox2.Checked Then Can = 0
                Dim TipoP As String = ""
                If ComboBox7.SelectedIndex = 0 Then TipoP = "E"
                If ComboBox7.SelectedIndex = 1 Then TipoP = "I"
                If ComboBox7.SelectedIndex = 2 Then TipoP = "D"
                Dim IdSuc As Integer = IdsSucursales.Valor(ComboBox4.SelectedIndex)
                If IdSuc < 0 Then IdSuc = 0
                Mascara.Modificar(Mascara.ID, TextBox1.Text.Trim, Act, Estados.Guardada, ComboBox8.SelectedIndex, ComboBox6.SelectedIndex, IdSuc, IdsClasificaciones.Valor(ComboBox5.SelectedIndex), TipoP, ComboBox1.SelectedIndex, ComboBox2.SelectedIndex, IdTipos)
                Nuevo()
                PopUp("Máscara Guardada", 90)
            Else
                MsgBox(Hayerror, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosDetalle()
        Try
            Detalles.LlenaDatos()
            ComboBox10.SelectedIndex = Detalles.Modulo
            ComboBox10.Enabled = False
            ComboBox3.SelectedIndex = IdsVariables.Busca(Detalles.idVariable)
            'Llena cuenta
            If Detalles.idCuenta = -1 And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.txtCuenta.Text = "*P"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PERCEPCIONES"
            End If
            If Detalles.idCuenta = -2 And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.txtCuenta.Text = "*D"
                SelectorCuentas1.txtDesc.Text = "COMODÍN DEDUCCIONES"
            End If
            If Detalles.idCuenta = -8 And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.txtCuenta.Text = "*T"
                SelectorCuentas1.txtDesc.Text = "COMODÍN TRABAJADORES"
            End If
            If Detalles.idCuenta = -24 And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.txtCuenta.Text = "*T2"
                SelectorCuentas1.txtDesc.Text = "COMODÍN TRABAJADORES"
            End If
            If Detalles.idCuenta = -25 And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.txtCuenta.Text = "*T3"
                SelectorCuentas1.txtDesc.Text = "COMODÍN TRABAJADORES"
            End If
            If Detalles.idCuenta = -26 And ComboBox10.SelectedIndex = 10 Then
                SelectorCuentas1.txtCuenta.Text = "*T4"
                SelectorCuentas1.txtDesc.Text = "COMODÍN TRABAJADORES"
            End If
            If Detalles.idCuenta = -3 And (ComboBox10.SelectedIndex = 4 Or ComboBox10.SelectedIndex = 5) Then
                SelectorCuentas1.txtCuenta.Text = "*B"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CUENTAS BANCOS"
            End If
            If Detalles.idCuenta = -9 And (ComboBox10.SelectedIndex = 4 Or ComboBox10.SelectedIndex = 5) Then
                SelectorCuentas1.txtCuenta.Text = "*B2"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CUENTAS BANCOS"
            End If
            If Detalles.idCuenta = -10 And (ComboBox10.SelectedIndex = 4 Or ComboBox10.SelectedIndex = 5) Then
                SelectorCuentas1.txtCuenta.Text = "*B3"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CUENTAS BANCOS"
            End If
            If Detalles.idCuenta = -11 And (ComboBox10.SelectedIndex = 4 Or ComboBox10.SelectedIndex = 5) Then
                SelectorCuentas1.txtCuenta.Text = "*B4"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CUENTAS BANCOS"
            End If
            If Detalles.idCuenta = -4 And ComboBox10.SelectedIndex = 4 Then
                SelectorCuentas1.txtCuenta.Text = "*C"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -12 And ComboBox10.SelectedIndex = 4 Then
                SelectorCuentas1.txtCuenta.Text = "*C2"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -13 And ComboBox10.SelectedIndex = 4 Then
                SelectorCuentas1.txtCuenta.Text = "*C3"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -14 And ComboBox10.SelectedIndex = 4 Then
                SelectorCuentas1.txtCuenta.Text = "*C4"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -5 And ComboBox10.SelectedIndex = 5 Then
                SelectorCuentas1.txtCuenta.Text = "*P"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If
            If Detalles.idCuenta = -15 And ComboBox10.SelectedIndex = 5 Then
                SelectorCuentas1.txtCuenta.Text = "*P2"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If
            If Detalles.idCuenta = -16 And ComboBox10.SelectedIndex = 5 Then
                SelectorCuentas1.txtCuenta.Text = "*P3"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If
            If Detalles.idCuenta = -17 And ComboBox10.SelectedIndex = 5 Then
                SelectorCuentas1.txtCuenta.Text = "*P4"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If
            If Detalles.idCuenta = -6 And (ComboBox10.SelectedIndex = 0 Or ComboBox10.SelectedIndex = 2 Or ComboBox10.SelectedIndex = 6 Or ComboBox10.SelectedIndex = 8) Then
                SelectorCuentas1.txtCuenta.Text = "*C"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -18 And (ComboBox10.SelectedIndex = 0 Or ComboBox10.SelectedIndex = 2 Or ComboBox10.SelectedIndex = 6 Or ComboBox10.SelectedIndex = 8) Then
                SelectorCuentas1.txtCuenta.Text = "*C2"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -19 And (ComboBox10.SelectedIndex = 0 Or ComboBox10.SelectedIndex = 2 Or ComboBox10.SelectedIndex = 6 Or ComboBox10.SelectedIndex = 8) Then
                SelectorCuentas1.txtCuenta.Text = "*C3"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -20 And (ComboBox10.SelectedIndex = 0 Or ComboBox10.SelectedIndex = 2 Or ComboBox10.SelectedIndex = 6 Or ComboBox10.SelectedIndex = 8) Then
                SelectorCuentas1.txtCuenta.Text = "*C4"
                SelectorCuentas1.txtDesc.Text = "COMODÍN CLIENTES"
            End If
            If Detalles.idCuenta = -7 And (ComboBox10.SelectedIndex = 1 Or ComboBox10.SelectedIndex = 3 Or ComboBox10.SelectedIndex = 7 Or ComboBox10.SelectedIndex = 9 Or ComboBox10.SelectedIndex = 12) Then
                SelectorCuentas1.txtCuenta.Text = "*P"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If
            If Detalles.idCuenta = -21 And (ComboBox10.SelectedIndex = 1 Or ComboBox10.SelectedIndex = 3 Or ComboBox10.SelectedIndex = 7 Or ComboBox10.SelectedIndex = 9 Or ComboBox10.SelectedIndex = 12) Then
                SelectorCuentas1.txtCuenta.Text = "*P2"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If
            If Detalles.idCuenta = -22 And (ComboBox10.SelectedIndex = 1 Or ComboBox10.SelectedIndex = 3 Or ComboBox10.SelectedIndex = 7 Or ComboBox10.SelectedIndex = 9 Or ComboBox10.SelectedIndex = 12) Then
                SelectorCuentas1.txtCuenta.Text = "*P3"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If
            If Detalles.idCuenta = -23 And (ComboBox10.SelectedIndex = 1 Or ComboBox10.SelectedIndex = 3 Or ComboBox10.SelectedIndex = 7 Or ComboBox10.SelectedIndex = 9 Or ComboBox10.SelectedIndex = 12) Then
                SelectorCuentas1.txtCuenta.Text = "*P4"
                SelectorCuentas1.txtDesc.Text = "COMODÍN PROVEEDORES"
            End If

            If Detalles.idCuenta = -27 And ComboBox10.SelectedIndex = 11 Then
                SelectorCuentas1.txtCuenta.Text = "*A"
                SelectorCuentas1.txtDesc.Text = "COMODÍN ALMACENES"
            End If
            If Detalles.idCuenta = -28 And ComboBox10.SelectedIndex = 11 Then
                SelectorCuentas1.txtCuenta.Text = "*A2"
                SelectorCuentas1.txtDesc.Text = "COMODÍN ALMACENES"
            End If
            If Detalles.idCuenta = -29 And ComboBox10.SelectedIndex = 11 Then
                SelectorCuentas1.txtCuenta.Text = "*A3"
                SelectorCuentas1.txtDesc.Text = "COMODÍN ALMACENES"
            End If
            If Detalles.idCuenta = -30 And ComboBox10.SelectedIndex = 11 Then
                SelectorCuentas1.txtCuenta.Text = "*A4"
                SelectorCuentas1.txtDesc.Text = "COMODÍN ALMACENES"
            End If
            If Detalles.idCuenta > 0 Then SelectorCuentas1.LlenaCuenta(Detalles.idCuenta)

            If Detalles.Cargo = 1 Then
                RadioButton1.Checked = True
            Else
                RadioButton2.Checked = True
            End If
            If Detalles.Negativo = 0 Then
                CheckBox2.Checked = False
            Else
                CheckBox2.Checked = True
            End If
            If Detalles.Beneficiario = 0 Then
                CheckBox3.Checked = False
            Else
                CheckBox3.Checked = True
            End If
            If Detalles.InUUIDs = 0 Then
                CheckBox4.Checked = False
            Else
                CheckBox4.Checked = True
            End If
            If Detalles.InPagoInfo = 0 Then
                CheckBox5.Checked = False
            Else
                CheckBox5.Checked = True
            End If
            Button1.Text = "Modificar"
            Button3.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        

    End Sub

    Private Sub DGDetalles_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If e.RowIndex >= 0 Then
            Detalles.ID = DGDetalles.Item(0, e.RowIndex).Value
            LlenaDatosDetalle()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Mascara.Estado = Estados.SinGuardar Then
            If MsgBox("Esta máscara no esta guardada. ¿Desea continuar?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Mascara.Eliminar(Mascara.ID)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasBaja, PermisosN.Secciones.Contabilidad) = False Then
            MsgBox("No tiene permiso para realizar esta operación", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If MsgBox("¿Eliminar esta máscara?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Mascara.Eliminar(Mascara.ID)
            Nuevo()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Modificar()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim hayError As String = ""
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasAlta, PermisosN.Secciones.Contabilidad) = False And Button9.Enabled = False Then
            hayError = "No tiene permiso para realizar esta operación."
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad) = False And Button9.Enabled = True Then
            hayError = "No tiene permiso para realizar esta operación"
        End If
        If hayError = "" Then
            If MsgBox("¿Eliminar este registro de la máscara?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Detalles.Eliminar(Detalles.ID)
                NuevoConcepto()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        BotonBuscar()
    End Sub
    Private Sub BotonBuscar()
        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, True, 0)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            Mascara.ID = f.IdMascara
            LlenaDatos()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Private Sub RadioButton1_KeyDown(sender As Object, e As KeyEventArgs) Handles RadioButton1.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged

    End Sub

    Private Sub RadioButton2_KeyDown(sender As Object, e As KeyEventArgs) Handles RadioButton2.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

  
   
    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        Label4.Text = "Qué hace la variable: " + ComboBox9.Text
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox9.Items.Count > 0 Then
            ComboBox9.SelectedIndex = ComboBox3.SelectedIndex
        End If

    End Sub

    Private Sub ComboBox10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox10.SelectedIndexChanged
        LlenaCombos("tblcontabilidadvariables", ComboBox3, "descripcion", "descr", "idvariable", IdsVariables, "modulo=" + ComboBox10.SelectedIndex.ToString, , "descripcion")
        LlenaCombos("tblcontabilidadvariables", ComboBox9, "quehace", "descr", "idvariable", IdsVariables2, "modulo=" + ComboBox10.SelectedIndex.ToString, , "descripcion")
        Label4.Text = ComboBox9.Text
        If ComboBox10.SelectedIndex <> 5 Then
            CheckBox3.Visible = False
            CheckBox5.Visible = False
        Else
            CheckBox5.Visible = True
            CheckBox3.Visible = True
        End If
        Select Case ComboBox10.SelectedIndex
            Case 10
                Label10.Text = "Comodines:  *P Se puede aplicar en la variable de percepciones para que cada percepción tome la cuenta que se asignó." + vbCrLf +
                " *D Se puede aplicar en la variable de deducciones para que cada percepción tome la cuenta que se asignó."
            Case 4
                Label10.Text = "Comodines: *B Se puede aplicar en la variable total por ficha, para que se tome la cuenta contable asignada a la cuenta bancaria." + vbCrLf +
            "*C Se puede usar para la variable de Total por pago, para que tome la cuenta contable asignada al cliente."
            Case 5
                Label10.Text = "Comodines: *B Se puede aplicar en la variable total por ficha, para que se tome la cuenta contable asignada a la cuenta bancaria." + vbCrLf +
                "*P Se puede usar para la variable de Total por pago, para que tome la cuenta contable asignada al proveedor."
            Case 0
                Label10.Text = "Comodines: *C Se puede usar en las variables que son por factura o cuando la máscara es por movimiento." + vbCrLf + " Para que se asigne la cuenta contable del cliente."
            Case 1
                Label10.Text = "Comodines: *P Se puede usar en las variables que son por factura o cuando la máscara es por movimiento." + vbCrLf + " Para que se asigne la cuenta contable del proveedor."
            Case 11
                Label10.Text = "Comodines: *A Se puede usar en variables que por movimiento o si la máscara es por movimiento." + vbCrLf + "Para que se asigne la cuenta contable del almacen."
            Case 12
                Label10.Text = "Comodines: *P Se puede usar en las variables que son por documento o cuando la máscara es por movimiento." + vbCrLf + " Para que se asigne la cuenta contable del proveedor."
            Case Else
                Label10.Text = "Comodines: "
        End Select
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, True, 0)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            Mascara.ID = Mascara.Creardesde(f.IdMascara)
            LlenaDatos()
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim S As New dbSucursales(GlobalIdSucursalDefault, MySqlcon)
        'V.ReporteViejosSaldos(IdsSucursales.Valor(ComboBox1.SelectedIndex), IdCliente, IdsMonedas.Valor(ComboBox4.SelectedIndex), ComboBox5.SelectedIndex)
        Rep = New repContabilidadMascaras
        Rep.SetDataSource(Detalles.Consulta(Mascara.ID, p.NNiv1, p.NNiv2, p.NNiv3, p.NNiv4, p.NNiv5))
        Rep.SetParameterValue("Encabezado", S.Nombre)
        Rep.SetParameterValue("Titulo", TextBox1.Text)
        Rep.SetParameterValue("Modulo", ComboBox2.Text)
        Rep.SetParameterValue("Clasificacion", ComboBox5.Text)
        Rep.SetParameterValue("Tipo", ComboBox1.Text)
        Rep.SetParameterValue("Sucursal", ComboBox4.Text)
        Rep.SetParameterValue("Tipopoliza", ComboBox7.Text)
        Rep.SetParameterValue("tipomovimiento", ComboBox6.Text)
        Rep.SetParameterValue("FormadePago", ComboBox8.Text)
        If CheckBox1.Checked Then
            Rep.SetParameterValue("activo", "SI")
        Else
            Rep.SetParameterValue("activo", "NO")
        End If
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
    End Sub

    Private Sub SelectorCuentas1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles SelectorCuentas1.KeyDown
        If e.KeyCode = Keys.Enter Then
            'If SelectorCuentas1.IdCuenta <> 0 Then
            Dim Cc As New dbCContables(MySqlcon)
            If Cc.UtlimoNivel(SelectorCuentas1.IdCuenta, SelectorCuentas1.Nivel) = 0 Then
                Select Case SelectorCuentas1.Nivel
                    Case 1
                        RadioButton1.Focus()
                    Case 2
                        If SelectorCuentas1.txtN3.Focused Then RadioButton1.Focus()
                    Case 3
                        If SelectorCuentas1.txtN4.Focused Then RadioButton1.Focus()
                    Case 4
                        If SelectorCuentas1.txtN5.Focused Then RadioButton1.Focus()
                    Case 5
                        RadioButton1.Focus()
                End Select
            End If
            'Else
            '    MsgBox("No existe esa cuenta", MsgBoxStyle.Information, GlobalNombreApp)
            'End If
        End If
    End Sub

    Private Sub SelectorCuentas1_Load(sender As Object, e As EventArgs) Handles SelectorCuentas1.Load

    End Sub

    Private Sub DGDetalles_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub
End Class