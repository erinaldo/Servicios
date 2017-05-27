Public Class frmUsuarios

    Dim IdUsuario As Integer
    Dim ConsultaOn As Boolean
    Dim NUsuarioAnterior As String
    Dim PasswordAnterior As String
    Dim IdsVendedores As New elemento
    Dim IdsPerfiles As New elemento
    Dim Per As New PermisosN
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            ReseteaPermisos(False, -1)
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox7.Text = ""
            TextBox8.Text = ""
            ComboBox1.SelectedIndex = 1
            PasswordAnterior = ""
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox7.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox8.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            TextBox7.Enabled = True
            TextBox8.Enabled = True
            TextBox1.Enabled = True
            ComboBox5.SelectedIndex = 0
            ConsultaOn = True
            'ReseteaPermisos()
            'ConsultaPermisos(0, 0, 0, 0, 0, 0)
            Consulta()
            'DGPermisosCatalogos1.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbUsuarios(MySqlcon)
                DataGridView1.DataSource = P.Consulta(TextBox3.Text)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Nombre Usuario"
                DataGridView1.Columns(2).HeaderText = "Nombre"
                DataGridView1.Columns(1).Width = 150
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
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
            Dim U As New dbUsuarios(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre de usuario."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox7.Text <> TextBox8.Text And TextBox7.Text <> PasswordAnterior Then
                NoErrores = False
                MensajeError += vbCrLf + " Error en la confirmación de password."
                TextBox8.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If TextBox7.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un password."
                TextBox7.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.UsuariosAlta, PermisosN.Secciones.Catalagos2) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.UsuariosCambio, PermisosN.Secciones.Catalagos2) = False Then
                    NoErrores = False
                    MensajeError += " No tiene permiso para realizar esta operación."
                End If
            End If
            'If U.CuentaUsuarios = 30 And Button1.Text = "Guardar" Then
            '    NoErrores = False
            '    MensajeError += vbCrLf + " A alcanzado el máximo de usuarios."
            'End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    If U.ChecaNombreUsuarioRepetido(TextBox1.Text) = False Then
                        AsignaPermisos()
                        U.Guardar(TextBox1.Text, ComboBox1.SelectedIndex, TextBox2.Text, TextBox7.Text, Per.PermisosCatalogos, Per.PermisosCatalogos2, Per.PermisosVentas, Per.PermisosCompras, Per.PermisosInventario, Per.PermisosHerramientas, IdsVendedores.Valor(ComboBox5.SelectedIndex), Per.PermisosPuntodeVenta, Per.PermisosBancos, Per.PermisosServicios, Per.PermisosNomina, Per.PermisosGastos, Per.PermisosEmpenios, Per.PermisosFertilizantes, Per.PermisosContabilidad, Per.PermisosSemillas)
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("Ya existe ese nombre de usuario, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        TextBox1.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                        Dim NUsuarioRepetido As Boolean = False

                        If TextBox1.Text <> NUsuarioAnterior Then
                            NUsuarioRepetido = U.ChecaNombreUsuarioRepetido(TextBox1.Text)
                        End If

                        If NUsuarioRepetido = False Then
                            AsignaPermisos()
                            U.Modificar(IdUsuario, TextBox1.Text, ComboBox1.SelectedIndex, TextBox2.Text, TextBox7.Text, Per.PermisosCatalogos, Per.PermisosCatalogos2, Per.PermisosVentas, Per.PermisosCompras, Per.PermisosInventario, Per.PermisosHerramientas, IdsVendedores.Valor(ComboBox5.SelectedIndex), Per.PermisosPuntodeVenta, Per.PermisosBancos, Per.PermisosServicios, Per.PermisosNomina, Per.PermisosGastos, Per.PermisosEmpenios, Per.PermisosFertilizantes, Per.PermisosContabilidad, Per.PermisosSemillas)
                            PopUp("Modificado", 90)
                            Nuevo()
                        Else
                            MsgBox("Ya existe ese nombre de usuario, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            TextBox1.BackColor = Color.FromArgb(250, 150, 150)
                        End If

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
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.UsuariosBaja, PermisosN.Secciones.Catalagos2) = True Then
                If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim u As New dbUsuarios(MySqlcon)
                    u.Eliminar(IdUsuario)
                    PopUp("Eliminado", 90)
                    Nuevo()
                    TextBox1.Focus()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este usuario debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            IdUsuario = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            If IdUsuario = 1000 And GlobalIdUsuario <> 1000 Then
                Exit Sub
            End If
            Dim U As New dbUsuarios(IdUsuario, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            TextBox1.Text = U.NombreUsuario
            TextBox2.Text = U.Nombre
            TextBox7.Text = U.PasswordS
            PasswordAnterior = U.PasswordS
            NUsuarioAnterior = U.NombreUsuario
            ComboBox1.SelectedIndex = U.Tipo
            ComboBox5.SelectedIndex = IdsVendedores.Busca(U.IdVendedor)
            Per.PermisosCatalogos = U.PermisosCatalogos
            Per.PermisosCatalogos2 = U.PermisosCatalogos2
            Per.PermisosCompras = U.PermisosCompras
            Per.PermisosVentas = U.PermisosVentas
            Per.PermisosInventario = U.PermisosInventario
            Per.PermisosServicios = U.PermisosServicios
            Per.PermisosHerramientas = U.PermisosHerramientas
            Per.PermisosPuntodeVenta = U.PermisosPuntodeVenta
            Per.PermisosBancos = U.PermisosBancos
            Per.PermisosNomina = U.PermisosNomina
            Per.PermisosGastos = U.PermisosGastos
            Per.PermisosEmpenios = U.PermisosEmpenios
            Per.PermisosFertilizantes = U.PermisosFertilizantes
            Per.PermisosContabilidad = U.PermisosContabilidad
            Per.PermisosSemillas = U.PermisosSemillas
            'iPermisosCatalogos1 = U.permisoscatalogos1
            'iPermisosVentas = U.PermisosVentas
            'iPermisosCompras = U.PermisosCompras
            'iPermisosInventario = U.PermisosInventario
            'iPermisosOtros = U.PermisosOtros
            If GlobalTipoUsuario <> 0 And GlobalIdUsuario <> IdUsuario Then
                TextBox7.Enabled = False
                TextBox8.Enabled = False
                TextBox1.Enabled = False
            Else
                TextBox7.Enabled = True
                TextBox8.Enabled = True
                TextBox1.Enabled = True
            End If
            ReseteaPermisos(False, -1)
            ConsultaPermisos()
            'ConsultaPermisos(U.permisoscatalogos1, U.PermisosCatalogos12, U.PermisosVentas, U.PermisosCompras, U.PermisosInventario, U.PermisosOtros)
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Restaurante")
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            TextBox1.Focus()
        End If
    End Sub

    Private Sub frmUsuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox1.Items.Add("Administrador")
        ComboBox1.Items.Add("Usuario")
        LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores, , "No Aplica.")
        LlenaCombos("tblperfilespermisos", ComboBox2, "nombre", "nombrep", "idperfil", IdsPerfiles)
        'LlenaGrid()
        LlenaTablas()
        Nuevo()
        If GlobalTipoUsuario = 1 Then
            ComboBox1.Enabled = False
            Button7.Enabled = False
            Button5.Enabled = False
            Panel1.Enabled = False
            grdPerVentas.Enabled = False
            'inhabilitar permisos
        End If
    End Sub

    Private Sub ReseteaPermisos(ByVal Valor As Boolean, ByVal pSeccion As Integer)
        Dim i As Integer
        If pSeccion = 0 Or pSeccion = -1 Then
            For i = 0 To DGCatalogosN.Rows.Count - 1
                If DGCatalogosN.Item(1, i).ReadOnly = False Then DGCatalogosN.Item(1, i).Value = Valor
                If DGCatalogosN.Item(2, i).ReadOnly = False Then DGCatalogosN.Item(2, i).Value = Valor
                If DGCatalogosN.Item(3, i).ReadOnly = False Then DGCatalogosN.Item(3, i).Value = Valor
                If DGCatalogosN.Item(4, i).ReadOnly = False Then DGCatalogosN.Item(4, i).Value = Valor
                If DGCatalogosN.Item(5, i).ReadOnly = False Then DGCatalogosN.Item(5, i).Value = Valor
                If DGCatalogosN.Item(6, i).ReadOnly = False Then DGCatalogosN.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 1 Or pSeccion = -1 Then
            For i = 0 To DGVentasN.Rows.Count - 1
                If DGVentasN.Item(1, i).ReadOnly = False Then DGVentasN.Item(1, i).Value = Valor
                If DGVentasN.Item(2, i).ReadOnly = False Then DGVentasN.Item(2, i).Value = Valor
                If DGVentasN.Item(3, i).ReadOnly = False Then DGVentasN.Item(3, i).Value = Valor
                If DGVentasN.Item(4, i).ReadOnly = False Then DGVentasN.Item(4, i).Value = Valor
                If DGVentasN.Item(5, i).ReadOnly = False Then DGVentasN.Item(5, i).Value = Valor
                If DGVentasN.Item(6, i).ReadOnly = False Then DGVentasN.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 2 Or pSeccion = -1 Then
            For i = 0 To DGComprasN.Rows.Count - 1
                If DGComprasN.Item(1, i).ReadOnly = False Then DGComprasN.Item(1, i).Value = Valor
                If DGComprasN.Item(2, i).ReadOnly = False Then DGComprasN.Item(2, i).Value = Valor
                If DGComprasN.Item(3, i).ReadOnly = False Then DGComprasN.Item(3, i).Value = Valor
                If DGComprasN.Item(4, i).ReadOnly = False Then DGComprasN.Item(4, i).Value = Valor
                If DGComprasN.Item(5, i).ReadOnly = False Then DGComprasN.Item(5, i).Value = Valor
                If DGComprasN.Item(6, i).ReadOnly = False Then DGComprasN.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 3 Or pSeccion = -1 Then
            For i = 0 To DGInventarioN.Rows.Count - 1
                If DGInventarioN.Item(1, i).ReadOnly = False Then DGInventarioN.Item(1, i).Value = Valor
                If DGInventarioN.Item(2, i).ReadOnly = False Then DGInventarioN.Item(2, i).Value = Valor
                If DGInventarioN.Item(3, i).ReadOnly = False Then DGInventarioN.Item(3, i).Value = Valor
                If DGInventarioN.Item(4, i).ReadOnly = False Then DGInventarioN.Item(4, i).Value = Valor
                If DGInventarioN.Item(5, i).ReadOnly = False Then DGInventarioN.Item(5, i).Value = Valor
                If DGInventarioN.Item(6, i).ReadOnly = False Then DGInventarioN.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 4 Or pSeccion = -1 Then
            For i = 0 To DGPVentaN.Rows.Count - 1
                If DGPVentaN.Item(1, i).ReadOnly = False Then DGPVentaN.Item(1, i).Value = Valor
                If DGPVentaN.Item(2, i).ReadOnly = False Then DGPVentaN.Item(2, i).Value = Valor
                If DGPVentaN.Item(3, i).ReadOnly = False Then DGPVentaN.Item(3, i).Value = Valor
                If DGPVentaN.Item(4, i).ReadOnly = False Then DGPVentaN.Item(4, i).Value = Valor
                If DGPVentaN.Item(5, i).ReadOnly = False Then DGPVentaN.Item(5, i).Value = Valor
                If DGPVentaN.Item(6, i).ReadOnly = False Then DGPVentaN.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 5 Or pSeccion = -1 Then
            For i = 0 To DGHerramientasN.Rows.Count - 1
                If DGHerramientasN.Item(1, i).ReadOnly = False Then DGHerramientasN.Item(1, i).Value = Valor
                If DGHerramientasN.Item(2, i).ReadOnly = False Then DGHerramientasN.Item(2, i).Value = Valor
                If DGHerramientasN.Item(3, i).ReadOnly = False Then DGHerramientasN.Item(3, i).Value = Valor
                If DGHerramientasN.Item(4, i).ReadOnly = False Then DGHerramientasN.Item(4, i).Value = Valor
                If DGHerramientasN.Item(5, i).ReadOnly = False Then DGHerramientasN.Item(5, i).Value = Valor
                If DGHerramientasN.Item(6, i).ReadOnly = False Then DGHerramientasN.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 6 Or pSeccion = -1 Then
            For i = 0 To DGBancosN.Rows.Count - 1
                If DGBancosN.Item(1, i).ReadOnly = False Then DGBancosN.Item(1, i).Value = Valor
                If DGBancosN.Item(2, i).ReadOnly = False Then DGBancosN.Item(2, i).Value = Valor
                If DGBancosN.Item(3, i).ReadOnly = False Then DGBancosN.Item(3, i).Value = Valor
                If DGBancosN.Item(4, i).ReadOnly = False Then DGBancosN.Item(4, i).Value = Valor
                If DGBancosN.Item(5, i).ReadOnly = False Then DGBancosN.Item(5, i).Value = Valor
                If DGBancosN.Item(6, i).ReadOnly = False Then DGBancosN.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 7 Or pSeccion = -1 Then
            For i = 0 To DGServicios.Rows.Count - 1
                If DGServicios.Item(1, i).ReadOnly = False Then DGServicios.Item(1, i).Value = Valor
                If DGServicios.Item(2, i).ReadOnly = False Then DGServicios.Item(2, i).Value = Valor
                If DGServicios.Item(3, i).ReadOnly = False Then DGServicios.Item(3, i).Value = Valor
                If DGServicios.Item(4, i).ReadOnly = False Then DGServicios.Item(4, i).Value = Valor
                If DGServicios.Item(5, i).ReadOnly = False Then DGServicios.Item(5, i).Value = Valor
                If DGServicios.Item(6, i).ReadOnly = False Then DGServicios.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 8 Or pSeccion = -1 Then
            For i = 0 To DGNomina.Rows.Count - 1
                If DGNomina.Item(1, i).ReadOnly = False Then DGNomina.Item(1, i).Value = Valor
                If DGNomina.Item(2, i).ReadOnly = False Then DGNomina.Item(2, i).Value = Valor
                If DGNomina.Item(3, i).ReadOnly = False Then DGNomina.Item(3, i).Value = Valor
                If DGNomina.Item(4, i).ReadOnly = False Then DGNomina.Item(4, i).Value = Valor
                If DGNomina.Item(5, i).ReadOnly = False Then DGNomina.Item(5, i).Value = Valor
                If DGNomina.Item(6, i).ReadOnly = False Then DGNomina.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 9 Or pSeccion = -1 Then
            For i = 0 To DGGastos.Rows.Count - 1
                If DGGastos.Item(1, i).ReadOnly = False Then DGGastos.Item(1, i).Value = Valor
                If DGGastos.Item(2, i).ReadOnly = False Then DGGastos.Item(2, i).Value = Valor
                If DGGastos.Item(3, i).ReadOnly = False Then DGGastos.Item(3, i).Value = Valor
                If DGGastos.Item(4, i).ReadOnly = False Then DGGastos.Item(4, i).Value = Valor
                If DGGastos.Item(5, i).ReadOnly = False Then DGGastos.Item(5, i).Value = Valor
                If DGGastos.Item(6, i).ReadOnly = False Then DGGastos.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 10 Or pSeccion = -1 Then
            For i = 0 To DGEmpenios.Rows.Count - 1
                If DGEmpenios.Item(1, i).ReadOnly = False Then DGEmpenios.Item(1, i).Value = Valor
                If DGEmpenios.Item(2, i).ReadOnly = False Then DGEmpenios.Item(2, i).Value = Valor
                If DGEmpenios.Item(3, i).ReadOnly = False Then DGEmpenios.Item(3, i).Value = Valor
                If DGEmpenios.Item(4, i).ReadOnly = False Then DGEmpenios.Item(4, i).Value = Valor
                If DGEmpenios.Item(5, i).ReadOnly = False Then DGEmpenios.Item(5, i).Value = Valor
                If DGEmpenios.Item(6, i).ReadOnly = False Then DGEmpenios.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 11 Or pSeccion = -1 Then
            For i = 0 To DGFertilizantes.Rows.Count - 1
                If DGFertilizantes.Item(1, i).ReadOnly = False Then DGFertilizantes.Item(1, i).Value = Valor
                If DGFertilizantes.Item(2, i).ReadOnly = False Then DGFertilizantes.Item(2, i).Value = Valor
                If DGFertilizantes.Item(3, i).ReadOnly = False Then DGFertilizantes.Item(3, i).Value = Valor
                If DGFertilizantes.Item(4, i).ReadOnly = False Then DGFertilizantes.Item(4, i).Value = Valor
                If DGFertilizantes.Item(5, i).ReadOnly = False Then DGFertilizantes.Item(5, i).Value = Valor
                If DGFertilizantes.Item(6, i).ReadOnly = False Then DGFertilizantes.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 12 Or pSeccion = -1 Then
            For i = 0 To DGcontabilidad.Rows.Count - 1
                If DGcontabilidad.Item(1, i).ReadOnly = False Then DGcontabilidad.Item(1, i).Value = Valor
                If DGcontabilidad.Item(2, i).ReadOnly = False Then DGcontabilidad.Item(2, i).Value = Valor
                If DGcontabilidad.Item(3, i).ReadOnly = False Then DGcontabilidad.Item(3, i).Value = Valor
                If DGcontabilidad.Item(4, i).ReadOnly = False Then DGcontabilidad.Item(4, i).Value = Valor
                If DGcontabilidad.Item(5, i).ReadOnly = False Then DGcontabilidad.Item(5, i).Value = Valor
                If DGcontabilidad.Item(6, i).ReadOnly = False Then DGcontabilidad.Item(6, i).Value = Valor
            Next
        End If
        If pSeccion = 13 Or pSeccion = -1 Then
            For i = 0 To DGSemillas.Rows.Count - 1
                If DGSemillas.Item(1, i).ReadOnly = False Then DGSemillas.Item(1, i).Value = Valor
                If DGSemillas.Item(2, i).ReadOnly = False Then DGSemillas.Item(2, i).Value = Valor
                If DGSemillas.Item(3, i).ReadOnly = False Then DGSemillas.Item(3, i).Value = Valor
                If DGSemillas.Item(4, i).ReadOnly = False Then DGSemillas.Item(4, i).Value = Valor
                If DGSemillas.Item(5, i).ReadOnly = False Then DGSemillas.Item(5, i).Value = Valor
                If DGSemillas.Item(6, i).ReadOnly = False Then DGSemillas.Item(6, i).Value = Valor
            Next
        End If
    End Sub

    'Private Sub DGPermisos_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
    '    If e.ColumnIndex > 0 Then
    '        If DGPermisosCatalogos1.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
    '            If e.Value = 0 Then
    '                e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
    '            Else
    '                e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
    '            End If
    '        Else
    '            e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
    '        End If
    '    End If
    'End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ReseteaPermisos(True, -1)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub


    Private Sub LlenaTablas()
        Dim R As Integer = 0
        DGCatalogosN.Rows.Add("Vendedores", 0, 0, 0, 0, 0, 0)
        DGCatalogosN.Item(5, r).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True

        DGCatalogosN.Rows.Add("Proveedores", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True

        DGCatalogosN.Rows.Add("Clientes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Sucursales", 0, 0, 0, 0, 0, 0)

        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Sucursales folios", 0, 0, 0, 0, 0, 0)

        R += 1
        DGCatalogosN.Item(1, R).ReadOnly = True
        DGCatalogosN.Item(2, R).ReadOnly = True
        DGCatalogosN.Item(3, R).ReadOnly = True
        DGCatalogosN.Item(4, R).ReadOnly = True
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Sucursales certificados", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).ReadOnly = True
        DGCatalogosN.Item(2, R).ReadOnly = True
        DGCatalogosN.Item(3, R).ReadOnly = True
        DGCatalogosN.Item(4, R).ReadOnly = True
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Almacenes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Clasificaciones artículos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Artículos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Artículos Precios", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).ReadOnly = True
        DGCatalogosN.Item(2, R).ReadOnly = True
        DGCatalogosN.Item(3, R).ReadOnly = True
        DGCatalogosN.Item(4, R).ReadOnly = True
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Conceptos de inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Formas de pago", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Formas de pago remisiones.", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Cajas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Medidas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Monedas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Conceptos ventas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Conceptos compras", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(2, R).ReadOnly = True
        DGCatalogosN.Item(3, R).ReadOnly = True
        DGCatalogosN.Item(4, R).ReadOnly = True
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Usuarios", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Empresas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True

        DGCatalogosN.Rows.Add("Técnicos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True

        DGCatalogosN.Rows.Add("Zonas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True

        DGCatalogosN.Rows.Add("Ofertas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Artículos Relaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True

        DGCatalogosN.Rows.Add("Clientes días de crédito", 0, 0, 0, 0, 0, 0)

        R += 1
        DGCatalogosN.Item(1, R).ReadOnly = True
        DGCatalogosN.Item(2, R).ReadOnly = True
        DGCatalogosN.Item(3, R).ReadOnly = True
        DGCatalogosN.Item(4, R).ReadOnly = True
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Clientes monto de crédito", 0, 0, 0, 0, 0, 0)

        R += 1
        DGCatalogosN.Item(1, R).ReadOnly = True
        DGCatalogosN.Item(2, R).ReadOnly = True
        DGCatalogosN.Item(3, R).ReadOnly = True
        DGCatalogosN.Item(4, R).ReadOnly = True
        DGCatalogosN.Item(5, R).ReadOnly = True

        DGCatalogosN.Rows.Add("Clientes Tipos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Proveedores Tipos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True
        DGCatalogosN.Rows.Add("Sucursales Tipos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(5, R).ReadOnly = True
        DGCatalogosN.Item(6, R).ReadOnly = True

        '----------------------------------------
        '-------------------------------------------
        '--------------------------------------------
        R = 0
        DGVentasN.Rows.Add("Cotizaciones", 0, 0, 0, 0, 0, 0)
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Pedidos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Facturación", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Devoluciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Notas de crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Notas de cargo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Pagaré", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Pagos remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Consultas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Cambio sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Apartados", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(6, R).ReadOnly = True
        DGVentasN.Rows.Add("Cambio de alamacen", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Cambio de descripción", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Permitir descuentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True

        DGVentasN.Rows.Add("Cambio de precio", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Cambio de folio", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Vender bajo costo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Cambio de fecha pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Cambio de fecha pagos remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True

        DGVentasN.Rows.Add("Cambio de fecha facturación", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True

        DGVentasN.Rows.Add("Cambio de fecha remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True

        DGVentasN.Rows.Add("Permitir pendientes facturación", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True

        DGVentasN.Rows.Add("Permitir pendientes remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True

        DGVentasN.Rows.Add("Permitir facturas a crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True
        DGVentasN.Rows.Add("Permitir remisiones a crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).ReadOnly = True
        DGVentasN.Item(2, R).ReadOnly = True
        DGVentasN.Item(3, R).ReadOnly = True
        DGVentasN.Item(4, R).ReadOnly = True
        DGVentasN.Item(5, R).ReadOnly = True

        R = 0
        DGComprasN.Rows.Add("Pre-orden", 0, 0, 0, 0, 0, 0)
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Orden de compra", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Compras", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Devoluciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Notas de crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Notas de cargo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(2, R).ReadOnly = True
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(5, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Consultas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(2, R).ReadOnly = True
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(5, R).ReadOnly = True
        DGComprasN.Item(6, R).ReadOnly = True
        DGComprasN.Rows.Add("Cambio sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).ReadOnly = True
        DGComprasN.Item(2, R).ReadOnly = True
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(5, R).ReadOnly = True

        DGComprasN.Rows.Add("Cambio de almacen", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).ReadOnly = True
        DGComprasN.Item(2, R).ReadOnly = True
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(5, R).ReadOnly = True
        DGComprasN.Rows.Add("Cambio de fecha pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).ReadOnly = True
        DGComprasN.Item(2, R).ReadOnly = True
        DGComprasN.Item(3, R).ReadOnly = True
        DGComprasN.Item(4, R).ReadOnly = True
        DGComprasN.Item(5, R).ReadOnly = True



        DGPVentaN.Rows.Add("Ventas", 0, 0, 0, 0, 0, 0)
        R = 0
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(6, R).ReadOnly = True
        DGPVentaN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True
        DGPVentaN.Item(6, R).ReadOnly = True
        DGPVentaN.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True
        DGPVentaN.Rows.Add("Cambiar Precios", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True
        DGPVentaN.Rows.Add("Hacer Descuentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True
        DGPVentaN.Rows.Add("Movimientos de caja", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(6, R).ReadOnly = True
        DGPVentaN.Rows.Add("Cambio de fecha en mov cajas.", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True

        DGPVentaN.Rows.Add("Eliminar artículos.", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True

        DGPVentaN.Rows.Add("Cambio de vendedor.", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True

        DGPVentaN.Rows.Add("Cambio de caja.", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True

        DGPVentaN.Rows.Add("Asignar cantidad.", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).ReadOnly = True
        DGPVentaN.Item(2, R).ReadOnly = True
        DGPVentaN.Item(3, R).ReadOnly = True
        DGPVentaN.Item(4, R).ReadOnly = True
        DGPVentaN.Item(5, R).ReadOnly = True
        'DGPVentaN.Rows.Add("Cambiar de almacen", 0, 0, 0, 0, 0, 0)
        'R += 1
        'DGPVentaN.Item(1, R).ReadOnly = True
        'DGPVentaN.Item(2, R).ReadOnly = True
        'DGPVentaN.Item(3, R).ReadOnly = True
        'DGPVentaN.Item(4, R).ReadOnly = True
        'DGPVentaN.Item(5, R).ReadOnly = True

        R = 0
        DGHerramientasN.Rows.Add("Opciones", 0, 0, 0, 0, 0, 0)
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True
        DGHerramientasN.Item(6, R).ReadOnly = True
        DGHerramientasN.Rows.Add("Reporte nensual CFD", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(3, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True
        DGHerramientasN.Item(6, R).ReadOnly = True
        DGHerramientasN.Rows.Add("Diseño de documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True
        DGHerramientasN.Item(6, R).ReadOnly = True
        DGHerramientasN.Rows.Add("Configuración correo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True
        DGHerramientasN.Item(6, R).ReadOnly = True
        DGHerramientasN.Rows.Add("Respaldo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(1, R).ReadOnly = True
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(3, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True
        DGHerramientasN.Rows.Add("Restauración", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(1, R).ReadOnly = True
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(3, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True
        DGHerramientasN.Rows.Add("Cambio de precios", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(1, R).ReadOnly = True
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(3, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True
        DGHerramientasN.Rows.Add("Importador", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(1, R).ReadOnly = True
        DGHerramientasN.Item(2, R).ReadOnly = True
        DGHerramientasN.Item(3, R).ReadOnly = True
        DGHerramientasN.Item(4, R).ReadOnly = True
        DGHerramientasN.Item(5, R).ReadOnly = True

        R = 0
        DGInventarioN.Rows.Add("Movimientos de inventario", 0, 0, 0, 0, 0, 0)
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Movimientos - Entradas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Movimientos - Salidas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Movimientos - Traspasos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Movimientos - Ajustes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Kardex", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Revisión de inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True
        DGInventarioN.Rows.Add("Recalcular costos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Rows.Add("Recalcular inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Rows.Add("Buscar Negativos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Rows.Add("Cambio de almacen", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        DGInventarioN.Rows.Add("Editar Movimientos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True

        DGInventarioN.Rows.Add("Pedidos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(6, R).ReadOnly = True

        DGInventarioN.Rows.Add("Pedidos Autorizar", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).ReadOnly = True
        DGInventarioN.Item(2, R).ReadOnly = True
        DGInventarioN.Item(3, R).ReadOnly = True
        DGInventarioN.Item(4, R).ReadOnly = True
        DGInventarioN.Item(5, R).ReadOnly = True
        'DGInventarioN.Rows.Add("Permitir salidas y traspasos sin inventario", 0, 0, 0, 0, 0, 0)
        'R += 1
        'DGInventarioN.Item(1, R).ReadOnly = True
        'DGInventarioN.Item(2, R).ReadOnly = True
        'DGInventarioN.Item(3, R).ReadOnly = True
        'DGInventarioN.Item(4, R).ReadOnly = True
        'DGInventarioN.Item(5, R).ReadOnly = True

        R = 0
        DGBancosN.Rows.Add("Bancos", 0, 0, 0, 0, 0, 0)
        DGBancosN.Item(5, R).ReadOnly = True
        DGBancosN.Item(6, R).ReadOnly = True
        DGBancosN.Rows.Add("Cuentas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(5, R).ReadOnly = True
        DGBancosN.Item(6, R).ReadOnly = True
        DGBancosN.Rows.Add("Cuentas contables", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(5, R).ReadOnly = True
        DGBancosN.Item(6, R).ReadOnly = True
        DGBancosN.Rows.Add("Depósitos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(3, R).ReadOnly = True
        DGBancosN.Item(4, R).ReadOnly = True
        DGBancosN.Item(6, R).ReadOnly = True
        DGBancosN.Rows.Add("Pagos Proveedores", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(3, R).ReadOnly = True
        DGBancosN.Item(4, R).ReadOnly = True
        DGBancosN.Item(6, R).ReadOnly = True
        DGBancosN.Rows.Add("Conciliación", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(1, R).ReadOnly = True
        DGBancosN.Item(2, R).ReadOnly = True
        DGBancosN.Item(3, R).ReadOnly = True
        DGBancosN.Item(4, R).ReadOnly = True
        DGBancosN.Item(5, R).ReadOnly = True
        DGBancosN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(1, R).ReadOnly = True
        DGBancosN.Item(2, R).ReadOnly = True
        DGBancosN.Item(3, R).ReadOnly = True
        DGBancosN.Item(4, R).ReadOnly = True
        DGBancosN.Item(5, R).ReadOnly = True

        'Servicios
        R = 0
        DGServicios.Rows.Add("Servicios", 0, 0, 0, 0, 0, 0)
        DGServicios.Item(3, R).ReadOnly = True
        DGServicios.Item(4, R).ReadOnly = True
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Servicios Clasificaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Servicios Consulta", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(2, R).ReadOnly = True
        DGServicios.Item(3, R).ReadOnly = True
        DGServicios.Item(4, R).ReadOnly = True
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Servicios Detalles", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Agregar artículo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Clientes equipos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(1, R).ReadOnly = True
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Detalles equipos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Consultar Historial", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(2, R).ReadOnly = True
        DGServicios.Item(3, R).ReadOnly = True
        DGServicios.Item(4, R).ReadOnly = True
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Consultar componentes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(2, R).ReadOnly = True
        DGServicios.Item(3, R).ReadOnly = True
        DGServicios.Item(4, R).ReadOnly = True
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Consultar detalles", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(2, R).ReadOnly = True
        DGServicios.Item(3, R).ReadOnly = True
        DGServicios.Item(4, R).ReadOnly = True
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True

        DGServicios.Rows.Add("Generar reporte", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(2, R).ReadOnly = True
        DGServicios.Item(3, R).ReadOnly = True
        DGServicios.Item(4, R).ReadOnly = True
        DGServicios.Item(5, R).ReadOnly = True
        DGServicios.Item(6, R).ReadOnly = True


        DGNomina.Rows.Add("Nomina", 0, 0, 0, 0, 0, 0)
        R = 0
        DGNomina.Item(3, R).ReadOnly = True
        DGNomina.Item(4, R).ReadOnly = True
        DGNomina.Item(6, R).ReadOnly = True

        DGNomina.Rows.Add("Trabajadores", 0, 0, 0, 0, 0, 0)
        R += 1
        DGNomina.Item(5, R).ReadOnly = True
        DGNomina.Item(6, R).ReadOnly = True

        DGNomina.Rows.Add("Importar", 0, 0, 0, 0, 0, 0)
        R += 1
        DGNomina.Item(1, R).ReadOnly = True
        DGNomina.Item(2, R).ReadOnly = True
        DGNomina.Item(3, R).ReadOnly = True
        DGNomina.Item(4, R).ReadOnly = True
        DGNomina.Item(5, R).ReadOnly = True
        DGNomina.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGNomina.Item(1, R).ReadOnly = True
        DGNomina.Item(2, R).ReadOnly = True
        DGNomina.Item(3, R).ReadOnly = True
        DGNomina.Item(4, R).ReadOnly = True
        DGNomina.Item(5, R).ReadOnly = True

        DGGastos.Rows.Add("Clasificaciones", 0, 0, 0, 0, 0, 0)
        R = 0
        DGGastos.Item(5, R).ReadOnly = True
        DGGastos.Item(6, R).ReadOnly = True
        DGGastos.Rows.Add("Empleados", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(5, R).ReadOnly = True
        DGGastos.Item(6, R).ReadOnly = True
        DGGastos.Rows.Add("Gastos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(3, R).ReadOnly = True
        DGGastos.Item(4, R).ReadOnly = True
        DGGastos.Item(6, R).ReadOnly = True
        DGGastos.Rows.Add("Programar Gastos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(5, R).ReadOnly = True
        DGGastos.Item(6, R).ReadOnly = True
        DGGastos.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(6, R).ReadOnly = True
        DGGastos.Item(2, R).ReadOnly = True
        DGGastos.Item(3, R).ReadOnly = True
        DGGastos.Item(4, R).ReadOnly = True
        DGGastos.Item(5, R).ReadOnly = True
        DGGastos.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(1, R).ReadOnly = True
        DGGastos.Item(2, R).ReadOnly = True
        DGGastos.Item(3, R).ReadOnly = True
        DGGastos.Item(4, R).ReadOnly = True
        DGGastos.Item(5, R).ReadOnly = True
        DGGastos.Rows.Add("Permitir cambio de fecha", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(1, R).ReadOnly = True
        DGGastos.Item(2, R).ReadOnly = True
        DGGastos.Item(3, R).ReadOnly = True
        DGGastos.Item(4, R).ReadOnly = True
        DGGastos.Item(5, R).ReadOnly = True
        DGGastos.Rows.Add("Ver notificaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(6, R).ReadOnly = True
        DGGastos.Item(2, R).ReadOnly = True
        DGGastos.Item(3, R).ReadOnly = True
        DGGastos.Item(4, R).ReadOnly = True
        DGGastos.Item(5, R).ReadOnly = True

        DGEmpenios.Rows.Add("Configuración", 0, 0, 0, 0, 0, 0)
        R = 0
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Clasificaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Identificaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Empeños", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Permitir empeño arriba del evaluo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Adjudicaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Consulta de movimientos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Rows.Add("Empeños Compras", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Cambiar vendedor", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Rows.Add("Ver Corte", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True
        DGEmpenios.Item(6, R).ReadOnly = True
        DGEmpenios.Rows.Add("Sin límite de evaluo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True

        DGEmpenios.Rows.Add("Permitir descuento en pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True

        DGEmpenios.Rows.Add("Permitir cambio de fechas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True

        DGEmpenios.Rows.Add("Exta ver", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True

        DGEmpenios.Rows.Add("Extra Cambio", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).ReadOnly = True
        DGEmpenios.Item(2, R).ReadOnly = True
        DGEmpenios.Item(3, R).ReadOnly = True
        DGEmpenios.Item(4, R).ReadOnly = True
        DGEmpenios.Item(5, R).ReadOnly = True


        R = 0
        DGFertilizantes.Rows.Add("Pedidos", 0, 0, 0, 0, 0, 0)
        DGFertilizantes.Item(3, R).ReadOnly = True
        DGFertilizantes.Item(4, R).ReadOnly = True
        DGFertilizantes.Item(6, R).ReadOnly = True

        R += 1
        DGFertilizantes.Rows.Add("Movimientos de almacen", 0, 0, 0, 0, 0, 0)
        DGFertilizantes.Item(1, R).ReadOnly = True
        DGFertilizantes.Item(4, R).ReadOnly = True
        

        R += 1
        DGFertilizantes.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        DGFertilizantes.Item(2, R).ReadOnly = True
        DGFertilizantes.Item(3, R).ReadOnly = True
        DGFertilizantes.Item(4, R).ReadOnly = True
        DGFertilizantes.Item(5, R).ReadOnly = True
        DGFertilizantes.Item(6, R).ReadOnly = True

        R = 0
        DGcontabilidad.Rows.Add("Configuración", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(2, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True
        
        R += 1
        DGcontabilidad.Rows.Add("Clasificaciones Pólizas", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        R += 1
        DGcontabilidad.Rows.Add("Catálogo de cuentas", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        R += 1
        DGcontabilidad.Rows.Add("Pólizas", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        R += 1
        DGcontabilidad.Rows.Add("Consultar Saldos", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(2, R).ReadOnly = True
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        R += 1
        DGcontabilidad.Rows.Add("Conciliación DIOT", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(2, R).ReadOnly = True
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        R += 1
        DGcontabilidad.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(2, R).ReadOnly = True
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        R += 1
        DGcontabilidad.Rows.Add("Máscaras", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        R += 1
        DGcontabilidad.Rows.Add("Generador de Pólizas", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(1, R).ReadOnly = True
        DGcontabilidad.Item(2, R).ReadOnly = True
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        R += 1
        DGcontabilidad.Rows.Add("Conceptos Nómina", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True
        R += 1
        DGcontabilidad.Rows.Add("Ver Pólizas Generadas", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(2, R).ReadOnly = True
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True
        R += 1
        DGcontabilidad.Rows.Add("Ver Fecha Contabilidad Facturas.", 0, 0, 0, 0, 0, 0)
        DGcontabilidad.Item(2, R).ReadOnly = True
        DGcontabilidad.Item(3, R).ReadOnly = True
        DGcontabilidad.Item(4, R).ReadOnly = True
        DGcontabilidad.Item(5, R).ReadOnly = True
        DGcontabilidad.Item(6, R).ReadOnly = True

        'SEMILLAS
        '------------------------------


        R = 0
        DGSemillas.Rows.Add("Boletas", 0, 0, 0, 0, 0, 0)
        'DGcontabilidad.Item(1, R).ReadOnly = True
        'DGcontabilidad.Item(2, R).ReadOnly = True
        DGSemillas.Item(3, R).ReadOnly = True
        'DGSemillas.Item(4, R).ReadOnly = True
        DGSemillas.Item(5, R).ReadOnly = True
        DGSemillas.Item(6, R).ReadOnly = True

        R += 1
        DGSemillas.Rows.Add("Comprobantes", 0, 0, 0, 0, 0, 0)
        'DGcontabilidad.Item(1, R).ReadOnly = True
        'DGcontabilidad.Item(2, R).ReadOnly = True
        DGSemillas.Item(3, R).ReadOnly = True
        'DGSemillas.Item(4, R).ReadOnly = True
        DGSemillas.Item(5, R).ReadOnly = True
        DGSemillas.Item(6, R).ReadOnly = True

        R += 1
        DGSemillas.Rows.Add("Liquidaciones", 0, 0, 0, 0, 0, 0)
        'DGcontabilidad.Item(1, R).ReadOnly = True
        'DGcontabilidad.Item(2, R).ReadOnly = True
        DGSemillas.Item(3, R).ReadOnly = True
        'DGSemillas.Item(4, R).ReadOnly = True
        DGSemillas.Item(5, R).ReadOnly = True
        DGSemillas.Item(6, R).ReadOnly = True

        R += 1
        DGSemillas.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        'DGcontabilidad.Item(1, R).ReadOnly = True
        DGSemillas.Item(2, R).ReadOnly = True
        DGSemillas.Item(3, R).ReadOnly = True
        DGSemillas.Item(4, R).ReadOnly = True
        DGSemillas.Item(5, R).ReadOnly = True
        DGSemillas.Item(6, R).ReadOnly = True

        R += 1
        DGSemillas.Rows.Add("Configuración", 0, 0, 0, 0, 0, 0)
        'DGcontabilidad.Item(1, R).ReadOnly = True
        DGSemillas.Item(1, R).ReadOnly = True
        DGSemillas.Item(2, R).ReadOnly = True
        DGSemillas.Item(3, R).ReadOnly = True
        DGSemillas.Item(4, R).ReadOnly = True
        DGSemillas.Item(5, R).ReadOnly = True

        R += 1
        DGSemillas.Rows.Add("Ver precios en boletas.", 0, 0, 0, 0, 0, 0)
        'DGcontabilidad.Item(1, R).ReadOnly = True
        DGSemillas.Item(2, R).ReadOnly = True
        DGSemillas.Item(3, R).ReadOnly = True
        DGSemillas.Item(4, R).ReadOnly = True
        DGSemillas.Item(5, R).ReadOnly = True
        DGSemillas.Item(6, R).ReadOnly = True

    End Sub

    Private Sub ConsultaPermisos()
        Dim R As Integer = 0
        'DGCatalogosN.Rows.Add("Vendedores", 0, 0, 0, 0, 0, 0)
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.VendedoresVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.VendedoresAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.VendedoresCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.VendedoresBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Proveedores", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ProveedoresVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ProveedoresAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ProveedoresCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ProveedoresBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Proveedores", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClientesVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClientesAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClientesCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClientesBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Sucursales", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.SucursalesVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.SucursalesAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.SucursalesCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.SucursalesBaja, PermisosN.Secciones.Catalagos)
        'sucursales folios
        R += 1
        DGCatalogosN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.SucursalesFolio, PermisosN.Secciones.Catalagos)
        'sucursales certificados
        R += 1
        DGCatalogosN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.SucursalesCertificados, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Almacenes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.AlmacenesVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.AlmacenesAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.AlmacenesCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Clasificaciones artículos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Artículos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.InventarioVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.InventarioAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.InventarioCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.InventarioBaja, PermisosN.Secciones.Catalagos)
        'artiulos precios
        R += 1
        DGCatalogosN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.InventarioPrecios, PermisosN.Secciones.Catalagos2)

        'DGCatalogosN.Rows.Add("Conceptos de inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosdeInventarioBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Formas de pago", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoBaja, PermisosN.Secciones.Catalagos)


        'DGCatalogosN.Rows.Add("Formas de pago remisiones.", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.FormasdePagoRemBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Cajas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.CajasVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.CajasAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.CajasCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.CajasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Medidas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MedidasVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MedidasAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MedidasCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MedidasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Monedas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MonedasVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MonedasAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MonedasCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.MonedasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Conceptos ventas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasVentasVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasVentasAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasVentasCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasVentasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Conceptos compras", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasVer, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasAlta, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasCambio, PermisosN.Secciones.Catalagos)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos.ConceptosNotasComprasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ReportesVer, PermisosN.Secciones.Catalagos2)

        'DGCatalogosN.Rows.Add("Usuarios", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.UsuariosVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.UsuariosAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.UsuariosCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.UsuariosBaja, PermisosN.Secciones.Catalagos2)

        'DGCatalogosN.Rows.Add("Empresas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.EmpresasVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.EmpresasAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.EmpresasCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.EmpresasBaja, PermisosN.Secciones.Catalagos2)

        R += 1 'Tecnicos
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.TecnicosVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.TecnicosAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.TecnicosCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.TecnicosBaja, PermisosN.Secciones.Catalagos2)

        R += 1 'Zonas
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ZonasVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ZonasAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ZonasCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ZonasBaja, PermisosN.Secciones.Catalagos2)

        R += 1 'Ofertas
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.OfertasVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.OfertasAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.OfertasCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.OfertasBaja, PermisosN.Secciones.Catalagos2)

        R += 1 'Relaciones
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesBaja, PermisosN.Secciones.Catalagos2)

        'dias credito
        R += 1
        DGCatalogosN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ClientesDiasCredito, PermisosN.Secciones.Catalagos2)
        'monto credito
        R += 1
        DGCatalogosN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ClientesMontoCredito, PermisosN.Secciones.Catalagos2)
        'Tipos
        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ClientesTiposBaja, PermisosN.Secciones.Catalagos2)

        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ProvTiposVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ProvTiposAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ProvTiposCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.ProvTiposBaja, PermisosN.Secciones.Catalagos2)

        R += 1
        DGCatalogosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.SucTiposVer, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.SucTiposAlta, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.SucTiposCambio, PermisosN.Secciones.Catalagos2)
        DGCatalogosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Catalogos2.SucTiposBaja, PermisosN.Secciones.Catalagos2)

        '-----------------------------------------------
        '-----------------------------------------------------------------
        '-------------------------------------------
        R = 0

        'DGVentasN.Rows.Add("Cotizaciones", 0, 0, 0, 0, 0, 0)
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CotizacionesVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CotizacionesAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CotizacionesCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pedidos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PedidosVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PedidosCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.RemisionesAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.RemisionesCancelar, PermisosN.Secciones.Ventas)


        'DGVentasN.Rows.Add("Facturación", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.VentasAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.VentasCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Devoluciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.DevolucionesAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.DevolucionesCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Notas de crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.NotasdeCreditoCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Notas de cargo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.NotasdeCargoVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.NotasdeCargoAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.NotasdeCargoCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pagaré", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagareVer, PermisosN.Secciones.Ventas)
        'DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas)
        'DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PedidosCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.DocumentosClientesVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.DocumentosClientesAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.DocumentosClientesCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosCambios, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pagos remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosRemVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosRemAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosRemCambios, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PagosRemCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas)
        

        'DGVentasN.Rows.Add("Consultas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Cambio Sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Apartados", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Ventas.VentasApartadosVer, PermisosN.Secciones.Ventas)
        DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.VentasApartadosAlta, PermisosN.Secciones.Ventas)
        DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.VentasApartadosCancelar, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Cambio almacen", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CambiodeAlmacen, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Cambio descripcion", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Permitir descuento", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirDescuento, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("cambio de precio", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CambiodePrecio, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("cambio de folio", 0, 0, 0, 0, 0, 0)
        R += 1
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas)
        R += 1 'bajo costo
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirVentaBajoCosto, PermisosN.Secciones.Ventas)
        R += 1 'fecha pagos
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirCambioFechaPagos, PermisosN.Secciones.Ventas)
        R += 1 'fecha pagos 2
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirCambioFechaPagosRemisiones, PermisosN.Secciones.Ventas)
        R += 1 'fecha ventas
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirCambiarFechaVentas, PermisosN.Secciones.Ventas)
        R += 1 'fecha remisiones
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirCambiarFechaRemisiones, PermisosN.Secciones.Ventas)
        R += 1 'pendientes 
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirPendienteVentas, PermisosN.Secciones.Ventas)
        R += 1 'pendientes remisiones
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirPendientesRemisiones, PermisosN.Secciones.Ventas)
        R += 1 'pendientes remisiones
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirVentasCredito, PermisosN.Secciones.Ventas)
        R += 1 'pendientes remisiones
        DGVentasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PermitirRemisionesCredito, PermisosN.Secciones.Ventas)

        '-------------------------------
        '--------------------------------------------------
        '-----------------------------------------------------------

        R = 0
        'DGComprasN.Rows.Add("Pre-orden", 0, 0, 0, 0, 0, 0)
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.CotizacionesVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.CotizacionesAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.CotizacionesCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Orden de compra", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.PedidosVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.PedidosAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.PedidosCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.RemisionesVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.RemisionesAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.RemisionesCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Compras", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.ComprasAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Compras.ComprasModificar, PermisosN.Secciones.Compras)
        DGComprasN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Compras.ComprasBaja, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.ComprasCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Devoluciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.DevolucionesAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.DevolucionesCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Notas de crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.NotasdeCreditoVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.NotasdeCreditoCancelacion, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Notas de cargo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.NotasdeCargoVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.NotasdeCargoAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.NotasdeCargoCancelacion, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.DocumentosProveedoresCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.PagosVer, PermisosN.Secciones.Compras)
        DGComprasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Compras.PagosAlta, PermisosN.Secciones.Compras)
        DGComprasN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Compras.PagosCambios, PermisosN.Secciones.Compras)
        DGComprasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Compras.PagosCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.Reportes, PermisosN.Secciones.Compras)
        
        'DGComprasN.Rows.Add("Consultas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Compras.Consultas, PermisosN.Secciones.Compras)
        'DGComprasN.Rows.Add("Cambio Sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras)
        'DGComprasN.Rows.Add("Cambio almacen", 0, 0, 0, 0, 0, 0)
        R += 1
        DGComprasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Compras.CambiodeAlmacen, PermisosN.Secciones.Compras)
        R += 1 'Cambio fecha
        DGComprasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Compras.CambiodeFechaPagos, PermisosN.Secciones.Compras)
        '-----------------------------------------------
        '-------------------------------------------------------
        '----------------------------------------------

        'DGPVentaN.Rows.Add("Ventas", 0, 0, 0, 0, 0, 0)
        R = 0
        DGPVentaN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.VentasVer, PermisosN.Secciones.PuntodeVenta)
        DGPVentaN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.VentasAlta, PermisosN.Secciones.PuntodeVenta)
        DGPVentaN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.VentasCancelar, PermisosN.Secciones.PuntodeVenta)

        'DGPVentaN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.ReportesVer, PermisosN.Secciones.PuntodeVenta)
        'Cambio sucursal
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CambiarSucursal, PermisosN.Secciones.PuntodeVenta)
        'Cambio precio
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CambiodePrecio, PermisosN.Secciones.PuntodeVenta)
        'Descuentos
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.HacerDescuento, PermisosN.Secciones.PuntodeVenta)
        'DGPVentaN.Rows.Add("Cajas movimientos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGPVentaN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosVer, PermisosN.Secciones.PuntodeVenta)
        DGPVentaN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosAlta, PermisosN.Secciones.PuntodeVenta)
        DGPVentaN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosCancelar, PermisosN.Secciones.PuntodeVenta)
        'Cambio fecha cajas
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CajasPermitirCambioFecha, PermisosN.Secciones.PuntodeVenta)
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.VentasBaja, PermisosN.Secciones.PuntodeVenta)
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CambioVendedor, PermisosN.Secciones.PuntodeVenta)
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CambioCaja, PermisosN.Secciones.PuntodeVenta)
        R += 1
        DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.AsignarCantidad, PermisosN.Secciones.PuntodeVenta)
        'Cambio almacen
        'R += 1
        'DGPVentaN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.PuntodeVentas.CambiodeAlamcen, PermisosN.Secciones.PuntodeVenta)
        '-------------------------------
        '--------------------------------------------
        '----------------------------------

        R = 0
        'DGHerramientasN.Rows.Add("Opciones", 0, 0, 0, 0, 0, 0)
        DGHerramientasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.OpcionesVer, PermisosN.Secciones.Herramientas)
        DGHerramientasN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.OpcionesModificar, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Reporte nensual CFD", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.Reportemensual, PermisosN.Secciones.Herramientas)
        'DGHerramientasN.Rows.Add("Diseño de documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingVer, PermisosN.Secciones.Herramientas)
        DGHerramientasN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas)
        'DGHerramientasN.Rows.Add("Configuración correo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.OpcionesCorreoVer, PermisosN.Secciones.Herramientas)
        DGHerramientasN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.OpcionesCorreoMoficar, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Respaldo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.RespaldoVer, PermisosN.Secciones.Herramientas)


        'DGHerramientasN.Rows.Add("Restauración", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.RestaurarVer, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Cambio de precios", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.CambiodePrecios, PermisosN.Secciones.Herramientas)
        'DGHerramientasN.Rows.Add("Importador", 0, 0, 0, 0, 0, 0)
        R += 1
        DGHerramientasN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.Importador, PermisosN.Secciones.Herramientas)
        '-----------------------------------
        '----------------------------------------------------
        '--------------------------------------------

        R = 0
        'Movimientos ver
        DGInventarioN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario)
        'DGInventarioN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.OpcionesModificar, PermisosN.Secciones.Herramientas)
        'DGInventarioN.Rows.Add("Movimientos - Entradas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosAltaEntrada, PermisosN.Secciones.Inventario)
        DGInventarioN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarEntrada, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Movimientos - Salidas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosAltaSalidas, PermisosN.Secciones.Inventario)
        DGInventarioN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarSalida, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Movimientos - Traspasos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosAltaTraspaso, PermisosN.Secciones.Inventario)
        DGInventarioN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarTraspaso, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Movimientos - Inv. Físico", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosAltaAjuste, PermisosN.Secciones.Inventario)
        DGInventarioN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarAjuste, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Kardex", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Inventario.KardexVer, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Inventario.Reportes, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Revisión de inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Inventario.RevisionVer, PermisosN.Secciones.Inventario)
        'DGInventarioN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Inventario.RevisionAlta, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Recalcular costos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.RecalcularCostos, PermisosN.Secciones.Inventario)
        R += 1
        DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.CambioSucursal, PermisosN.Secciones.Inventario)
        'DGInventarioN.Rows.Add("Recalcular inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.RecalcularInventarios, PermisosN.Secciones.Inventario)
        R += 1
        DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.BuscarNegativos, PermisosN.Secciones.Inventario)
        R += 1 'cambio almacen
        DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.CambiodeAlamcen, PermisosN.Secciones.Inventario)
        R += 1 'editar movimientos
        DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosEdicion, PermisosN.Secciones.Inventario)

        R += 1
        DGInventarioN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Inventario.PedidosVer, PermisosN.Secciones.Inventario)
        DGInventarioN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Inventario.PedidosAlta, PermisosN.Secciones.Inventario)
        DGInventarioN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Inventario.PedidosCancelar, PermisosN.Secciones.Inventario)

        R += 1
        DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.PedidosAutorizar, PermisosN.Secciones.Inventario)
        'R += 1
        'DGInventarioN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Inventario.MovimientosSinInventario, PermisosN.Secciones.Inventario)

        '---------------------------------
        '-------------------------------------------
        '-------------------------------------------
        R = 0
        'DGBancosN.Rows.Add("Bancos", 0, 0, 0, 0, 0, 0)
        DGBancosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Bancos.BancosVer, PermisosN.Secciones.Bancos)
        DGBancosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Bancos.BancosAlta, PermisosN.Secciones.Bancos)
        DGBancosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Bancos.BancosModificar, PermisosN.Secciones.Bancos)
        DGBancosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Bancos.BancosEliminar, PermisosN.Secciones.Bancos)
        'DGBancosN.Item(5, R).ReadOnly = True
        'DGBancosN.Item(6, R).ReadOnly = True
        'DGBancosN.Rows.Add("Cuentas", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasVer, PermisosN.Secciones.Bancos)
        DGBancosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasAlta, PermisosN.Secciones.Bancos)
        DGBancosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasModificar, PermisosN.Secciones.Bancos)
        DGBancosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasEliminar, PermisosN.Secciones.Bancos)
        'DGBancosN.Item(5, R).ReadOnly = True
        'DGBancosN.Item(6, R).ReadOnly = True
        'DGBancosN.Rows.Add("Cuentas contables", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasContablesVer, PermisosN.Secciones.Bancos)
        DGBancosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasContablesAlta, PermisosN.Secciones.Bancos)
        DGBancosN.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasContablesModificar, PermisosN.Secciones.Bancos)
        DGBancosN.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Bancos.CuentasContablesEliminar, PermisosN.Secciones.Bancos)
        'DGBancosN.Item(5, R).ReadOnly = True
        'DGBancosN.Item(6, R).ReadOnly = True
        'DGBancosN.Rows.Add("Depósitos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Bancos.DepositosVer, PermisosN.Secciones.Bancos)
        DGBancosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Bancos.DepositosAlta, PermisosN.Secciones.Bancos)
        DGBancosN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Bancos.DepositosCancelar, PermisosN.Secciones.Bancos)
        'DGBancosN.Item(3, R).ReadOnly = True
        'DGBancosN.Item(4, R).ReadOnly = True
        'DGBancosN.Item(6, R).ReadOnly = True
        'DGBancosN.Rows.Add("Pagos Proveedores", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Bancos.PagoProveedoresVer, PermisosN.Secciones.Bancos)
        DGBancosN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Bancos.PagoProveedoresAlta, PermisosN.Secciones.Bancos)
        DGBancosN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Bancos.PagoProveedoresCancelar, PermisosN.Secciones.Bancos)
        'DGBancosN.Item(3, R).ReadOnly = True
        'DGBancosN.Item(4, R).ReadOnly = True
        'DGBancosN.Item(6, R).ReadOnly = True
        'DGBancosN.Rows.Add("Conciliación", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Bancos.Consiliacion, PermisosN.Secciones.Bancos)
        'DGBancosN.Item(1, R).ReadOnly = True
        'DGBancosN.Item(2, R).ReadOnly = True
        'DGBancosN.Item(3, R).ReadOnly = True
        'DGBancosN.Item(4, R).ReadOnly = True
        'DGBancosN.Item(5, R).ReadOnly = True
        'DGBancosN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGBancosN.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Bancos.Reportes, PermisosN.Secciones.Bancos)
        'DGBancosN.Item(1, R).ReadOnly = True
        'DGBancosN.Item(2, R).ReadOnly = True
        'DGBancosN.Item(3, R).ReadOnly = True
        'DGBancosN.Item(4, R).ReadOnly = True
        'DGBancosN.Item(5, R).ReadOnly = True

        'Servicios
        'frmServicios
        R = 0
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServicioVer, PermisosN.Secciones.Servicios)
        DGServicios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServicioGuardar, PermisosN.Secciones.Servicios)
        'Servicios clas icaciones
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosClasVer, PermisosN.Secciones.Servicios)
        DGServicios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosClasGuardar, PermisosN.Secciones.Servicios)
        DGServicios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosClasModificar, PermisosN.Secciones.Servicios)
        DGServicios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosClasEliminar, PermisosN.Secciones.Servicios)

        'Servicios Consulta
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosConsultaVer, PermisosN.Secciones.Servicios)

        'Servicios Detalles
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesVer, PermisosN.Secciones.Servicios)
        DGServicios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesAgregarEstatus, PermisosN.Secciones.Servicios)
        DGServicios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesModificarEstatus, PermisosN.Secciones.Servicios)
        DGServicios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosDetallesEliminarEstatus, PermisosN.Secciones.Servicios)

        'Agregar Artículo
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloVer, PermisosN.Secciones.Servicios)
        DGServicios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloAgregar, PermisosN.Secciones.Servicios)
        DGServicios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloModificar, PermisosN.Secciones.Servicios)
        DGServicios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloEliminar, PermisosN.Secciones.Servicios)

        'Clientes equipos
        R += 1
        DGServicios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosClientesEquiposGuardar, PermisosN.Secciones.Servicios)
        DGServicios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosClientesEquiposModificar, PermisosN.Secciones.Servicios)
        DGServicios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosClientesEquiposEliminar, PermisosN.Secciones.Servicios)

        'Detalles equipos
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.SericiosDetallesEquiposVer, PermisosN.Secciones.Servicios)
        DGServicios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Servicios.SericiosDetallesEquiposAgregar, PermisosN.Secciones.Servicios)
        DGServicios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Servicios.SericiosDetallesEquiposModificar, PermisosN.Secciones.Servicios)
        DGServicios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Servicios.SericiosDetallesEquiposEliminar, PermisosN.Secciones.Servicios)

        'DGServicios.Rows.Add("Consultar Historial", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosVerHistorial, PermisosN.Secciones.Servicios)
        'DGServicios.Rows.Add("Consultar componentes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosVerConponentes, PermisosN.Secciones.Servicios)
        'DGServicios.Rows.Add("Consultar detalles", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosVerDetalles, PermisosN.Secciones.Servicios)
        'DGServicios.Rows.Add("Generar reporte", 0, 0, 0, 0, 0, 0)
        R += 1
        DGServicios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Servicios.ServiciosVerReportes, PermisosN.Secciones.Servicios)

        R = 0
        DGNomina.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Nominas.NominaVer, PermisosN.Secciones.Nomina)
        DGNomina.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina)
        DGNomina.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Nominas.NomminaCancelar, PermisosN.Secciones.Nomina)
        R += 1
        DGNomina.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Nominas.TrabajadoresVer, PermisosN.Secciones.Nomina)
        DGNomina.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Nominas.TrabajadoresAlta, PermisosN.Secciones.Nomina)
        DGNomina.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Nominas.TrabajadoresCambios, PermisosN.Secciones.Nomina)
        DGNomina.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Nominas.TrabajadoresBaja, PermisosN.Secciones.Nomina)
        R += 1
        DGNomina.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Nominas.NominaImportar, PermisosN.Secciones.Nomina)
        R += 1
        DGNomina.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Nominas.CambiarSucursal, PermisosN.Secciones.Nomina)

        '---------------------------------------
        '--------------------------
        '-------------------------------------------------

        'DGGastos.Rows.Add("Clasificaciones", 0, 0, 0, 0, 0, 0)
        R = 0
        DGGastos.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesVer, PermisosN.Secciones.Gastos)
        DGGastos.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesAltas, PermisosN.Secciones.Gastos)
        DGGastos.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesCambios, PermisosN.Secciones.Gastos)
        DGGastos.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosClasificacionesBajas, PermisosN.Secciones.Gastos)

        'DGGastos.Rows.Add("Empleados", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosVer, PermisosN.Secciones.Gastos)
        DGGastos.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosAlta, PermisosN.Secciones.Gastos)
        DGGastos.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosCambios, PermisosN.Secciones.Gastos)
        DGGastos.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosEmpleadosBaja, PermisosN.Secciones.Gastos)

        'DGGastos.Rows.Add("Gastos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosVer, PermisosN.Secciones.Gastos)
        DGGastos.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosAlta, PermisosN.Secciones.Gastos)
        DGGastos.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosCancelar, PermisosN.Secciones.Gastos)

        'DGGastos.Rows.Add("Programar Gastos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosProgramarVer, PermisosN.Secciones.Gastos)
        DGGastos.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosProgramarAlta, PermisosN.Secciones.Gastos)
        DGGastos.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosProgramarCambios, PermisosN.Secciones.Gastos)
        DGGastos.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosProgramarBajas, PermisosN.Secciones.Gastos)

        'DGGastos.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosReportesVer, PermisosN.Secciones.Gastos)
        'DGGastos.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGGastos.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosCambiarSucursal, PermisosN.Secciones.Gastos)
        R += 1 'cambio fecha
        DGGastos.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosPermitirCambioFecha, PermisosN.Secciones.Gastos)
        R += 1
        DGGastos.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Gastos.GastosVerNotificaciones, PermisosN.Secciones.Gastos)

        'DGEmpenios.Rows.Add("Configuración", 0, 0, 0, 0, 0, 0)
        R = 0
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosConfiguracionVer, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosConfiguracionAlta, PermisosN.Secciones.Empenios)

        'DGEmpenios.Rows.Add("Identificaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosIdentificacionVer, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosIdentificacionAlta, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosIdentificacionCambios, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosIdentificacionBaja, PermisosN.Secciones.Empenios)
        'DGEmpenios.Item(5, R).ReadOnly = True
        'DGEmpenios.Item(6, R).ReadOnly = True

        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesVer, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesAlta, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesCambios, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosClasificacionesBaja, PermisosN.Secciones.Empenios)

        'DGEmpenios.Rows.Add("Empeños", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosVer, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosAlta, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosCancelar, PermisosN.Secciones.Empenios)

        'DGEmpenios.Rows.Add("Permitir empeño arriba del evaluo", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosSobreValouPermitir, PermisosN.Secciones.Empenios)

        'DGEmpenios.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosPagosVer, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosPAgosAlta, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosPagosCambios, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosPAgosBaja, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Adjudicaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosAdjudicacionesVer, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosAdjudicacionesAlta, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Consulta de movimientos", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosConsultaMovVer, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosReportesVer, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosCambiarSucursal, PermisosN.Secciones.Empenios)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasVer, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasAlta, PermisosN.Secciones.Empenios)
        DGEmpenios.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosComprasCancelar, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Cambiar vendedor", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.EmpeniosPermitirCambiarVendedor, PermisosN.Secciones.Empenios)
        R += 1
        DGEmpenios.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Empenios.VerCorte, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("no limite", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.NoLimitarEvaluo, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Descuento", 0, 0, 0, 0, 0, 0)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.PermitirDescuento, PermisosN.Secciones.Empenios)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.PermitirCambioFecha, PermisosN.Secciones.Empenios)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.PermitirExtraVer, PermisosN.Secciones.Empenios)
        R += 1
        DGEmpenios.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Empenios.PermitirExtraCambio, PermisosN.Secciones.Empenios)

        ''''''''''''''''''''--------------------------------FERTILIZANTEs
        R = 0
        'Pedidos
        DGFertilizantes.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.PedidosVer, PermisosN.Secciones.Fertilizantes)
        DGFertilizantes.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.PedidosAlta, PermisosN.Secciones.Fertilizantes)
        DGFertilizantes.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.PedidosCancelar, PermisosN.Secciones.Fertilizantes)

        R += 1
        'movimientos
        DGFertilizantes.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.MovimientosAlta, PermisosN.Secciones.Fertilizantes)
        DGFertilizantes.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.MovimientosCambios, PermisosN.Secciones.Fertilizantes)
        DGFertilizantes.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.MovimientosCancelar, PermisosN.Secciones.Fertilizantes)
        DGFertilizantes.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.MovimientosVer, PermisosN.Secciones.Fertilizantes)

        R += 1
        'reportes
        DGFertilizantes.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Fertilizantes.ReportesVer, PermisosN.Secciones.Fertilizantes)

        '''''''''''''---------------------------Conta
        R = 0
        'Configuración
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ConfiguracionVer, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ConfiguracionModificar, PermisosN.Secciones.Contabilidad)

        R += 1
        'Clasificacion Pólizas
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasVer, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasAlta, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasCambios, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        'Cuentas
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.CuentasVer, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.CuentasAlta, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.CuentasModificar, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.CuentasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        ' Pólizas
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.PolizasVer, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.PolizasAlta, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.PolizasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        'Consulta saldos
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ConsultaSaldosVer, PermisosN.Secciones.Contabilidad)

        R += 1
        'Diot
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ConsiliaciondiotVer, PermisosN.Secciones.Contabilidad)

        R += 1
        'Reportes
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.ReportesVer, PermisosN.Secciones.Contabilidad)
        
        R += 1
        'mascaras
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.MascarasVer, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.MascarasAlta, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.MascarasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        'Generar
        DGcontabilidad.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.GenerarPolizasPermitir, PermisosN.Secciones.Contabilidad)
        R += 1
        'mascaras
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.NominaConceptosVer, PermisosN.Secciones.Contabilidad)
        DGcontabilidad.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.NominaConceptosAlta, PermisosN.Secciones.Contabilidad)
        R += 1
        'ver generadas
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad)
        R += 1
        'ver fecha
        DGcontabilidad.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.VerFechaConta, PermisosN.Secciones.Contabilidad)


        R = 0
        'Boletas
        DGSemillas.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Semillas.BoletasVer, PermisosN.Secciones.Semillas)
        DGSemillas.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Semillas.BoletasAlta, PermisosN.Secciones.Semillas)
        'DGcontabilidad.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        DGSemillas.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Semillas.BoletasBaja, PermisosN.Secciones.Semillas)

        R += 1
        'comprobante
        DGSemillas.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Semillas.ComprobanteVer, PermisosN.Secciones.Semillas)
        DGSemillas.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Semillas.ComprobanteAlta, PermisosN.Secciones.Semillas)
        'DGcontabilidad.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        DGSemillas.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Semillas.ComprobanteBaja, PermisosN.Secciones.Semillas)

        R += 1
        'liquidacion
        DGSemillas.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Semillas.LiquidacionVer, PermisosN.Secciones.Semillas)
        DGSemillas.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Semillas.LiquidacionAlta, PermisosN.Secciones.Semillas)
        'DGcontabilidad.Item(3, R).Value = Per.ChecaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        DGSemillas.Item(4, R).Value = Per.ChecaPermiso(PermisosN.Semillas.LiquidacionBaja, PermisosN.Secciones.Semillas)

        R += 1
        'reportes
        DGSemillas.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Semillas.ReportesVer, PermisosN.Secciones.Semillas)
        R += 1
        'config
        DGSemillas.Item(6, R).Value = Per.ChecaPermiso(PermisosN.Semillas.Configuracion, PermisosN.Secciones.Semillas)
        R += 1
        'precio
        DGSemillas.Item(1, R).Value = Per.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas)

    End Sub


    Private Sub AsignaPermisos()
        Dim R As Integer = 0
        Per.PermisosCatalogos = 0
        Per.PermisosCatalogos2 = 0
        Per.PermisosCompras = 0
        Per.PermisosHerramientas = 0
        Per.PermisosInventario = 0
        Per.PermisosPuntodeVenta = 0
        Per.PermisosVentas = 0
        Per.PermisosServicios = 0
        Per.PermisosBancos = 0
        Per.PermisosNomina = 0
        Per.PermisosGastos = 0
        Per.PermisosEmpenios = 0
        Per.PermisosContabilidad = 0
        Per.PermisosFertilizantes = 0
        Per.PermisosSemillas = 0
        'DGCatalogosN.Rows.Add("Vendedores", 0, 0, 0, 0, 0, 0)

        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.VendedoresVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.VendedoresAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.VendedoresCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.VendedoresBaja, PermisosN.Secciones.Catalagos)

        R += 1 'Proveedores
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ProveedoresVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ProveedoresAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ProveedoresCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ProveedoresBaja, PermisosN.Secciones.Catalagos)

        R += 1 'Clientes
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClientesVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClientesAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClientesCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClientesBaja, PermisosN.Secciones.Catalagos)
        'DGCatalogosN.Rows.Add("Sucursales", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.SucursalesVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.SucursalesAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.SucursalesCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.SucursalesBaja, PermisosN.Secciones.Catalagos)
        'sucursales folios
        R += 1
        If DGCatalogosN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.SucursalesFolio, PermisosN.Secciones.Catalagos)
        'sucursales certificados
        R += 1
        If DGCatalogosN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.SucursalesCertificados, PermisosN.Secciones.Catalagos)
        'DGCatalogosN.Rows.Add("Almacenes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.AlmacenesVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.AlmacenesAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.AlmacenesCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.AlmacenesBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Clasificaciones artículos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioCambios, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ClasificacionesdeInventarioBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Artículos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.InventarioVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.InventarioAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.InventarioCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.InventarioBaja, PermisosN.Secciones.Catalagos)

        'artiulos precios
        R += 1
        If DGCatalogosN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.InventarioPrecios, PermisosN.Secciones.Catalagos2)

        'DGCatalogosN.Rows.Add("Conceptos de inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosdeInventarioVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosdeInventarioAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosdeInventarioCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosdeInventarioBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Formas de pago", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Formas de pago remisiones.", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoRemVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoRemAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoRemCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.FormasdePagoRemBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Cajas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.CajasVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.CajasAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.CajasCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.CajasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Medidas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MedidasVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MedidasAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MedidasCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MedidasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Monedas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MonedasVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MonedasAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MonedasCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.MonedasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Conceptos ventas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasVentasVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasVentasAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasVentasCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasVentasBaja, PermisosN.Secciones.Catalagos)

        'DGCatalogosN.Rows.Add("Conceptos compras", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasComprasVer, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasComprasAlta, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasComprasCambio, PermisosN.Secciones.Catalagos)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos.ConceptosNotasComprasBaja, PermisosN.Secciones.Catalagos)
        'DGCatalogosN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ReportesVer, PermisosN.Secciones.Catalagos2)

        'DGCatalogosN.Rows.Add("Usuarios", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.UsuariosVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.UsuariosAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.UsuariosCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.UsuariosBaja, PermisosN.Secciones.Catalagos2)

        'DGCatalogosN.Rows.Add("Empresas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.EmpresasVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.EmpresasAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.EmpresasCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.EmpresasBaja, PermisosN.Secciones.Catalagos2)
        R += 1 'Tecnicos
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.TecnicosVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.TecnicosAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.TecnicosCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.TecnicosBaja, PermisosN.Secciones.Catalagos2)
        R += 1 'Zonas
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ZonasVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ZonasAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ZonasCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ZonasBaja, PermisosN.Secciones.Catalagos2)
        R += 1 'Ofertas
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.OfertasVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.OfertasAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.OfertasCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.OfertasBaja, PermisosN.Secciones.Catalagos2)
        R += 1 'Ofertas
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.InventarioRelacionesVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.InventarioRelacionesAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.InventarioRelacionesCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.InventarioRelacionesBaja, PermisosN.Secciones.Catalagos2)
        R += 1 'Clientes dias credito
        If DGCatalogosN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ClientesDiasCredito, PermisosN.Secciones.Catalagos2)
        R += 1 'Clientes monto credito
        If DGCatalogosN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ClientesMontoCredito, PermisosN.Secciones.Catalagos2)
        R += 1 'tipo
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ClientesTiposVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ClientesTiposAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ClientesTiposCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ClientesTiposBaja, PermisosN.Secciones.Catalagos2)
        R += 1 'tipo
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ProvTiposVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ProvTiposAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ProvTiposCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.ProvTiposBaja, PermisosN.Secciones.Catalagos2)
        R += 1 'tipo
        If DGCatalogosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.SucTiposVer, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.SucTiposAlta, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.SucTiposCambio, PermisosN.Secciones.Catalagos2)
        If DGCatalogosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Catalogos2.SucTiposBaja, PermisosN.Secciones.Catalagos2)

        '-----------------------------------------------
        '-----------------------------------------------------------------
        '-------------------------------------------
        R = 0
        'DGVentasN.Rows.Add("Cotizaciones", 0, 0, 0, 0, 0, 0)
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CotizacionesVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CotizacionesAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CotizacionesCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pedidos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PedidosVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PedidosCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.RemisionesAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.RemisionesCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Facturación", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.VentasAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.VentasCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Devoluciones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.DevolucionesVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.DevolucionesAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.DevolucionesCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Notas de crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.NotasdeCreditoVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.NotasdeCreditoAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.NotasdeCreditoCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Notas de cargo", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.NotasdeCargoVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.NotasdeCargoAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.NotasdeCargoCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pagaré", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagareVer, PermisosN.Secciones.Ventas)
        'DGVentasN.Item(2, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas)
        'DGVentasN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Ventas.PedidosCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.DocumentosClientesVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.DocumentosClientesAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.DocumentosClientesCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosCambios, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Pagos remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosRemVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosRemAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosRemCambios, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PagosRemCancelar, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Consultas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Cambio Sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Apartados", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.VentasApartadosVer, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.VentasApartadosAlta, PermisosN.Secciones.Ventas)
        If DGVentasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.VentasApartadosCancelar, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Cambio almacen", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CambiodeAlmacen, PermisosN.Secciones.Ventas)


        'DGVentasN.Rows.Add("Permitir descripcion", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Permitir descuentos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirDescuento, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Cambio de precio", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CambiodePrecio, PermisosN.Secciones.Ventas)

        'DGVentasN.Rows.Add("Cambio de folio", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Bajo Costo", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirVentaBajoCosto, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Fecha pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirCambioFechaPagos, PermisosN.Secciones.Ventas)
        'DGVentasN.Rows.Add("Fecha Pagos rem", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirCambioFechaPagosRemisiones, PermisosN.Secciones.Ventas)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirCambiarFechaVentas, PermisosN.Secciones.Ventas)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirCambiarFechaRemisiones, PermisosN.Secciones.Ventas)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirPendienteVentas, PermisosN.Secciones.Ventas)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirPendientesRemisiones, PermisosN.Secciones.Ventas)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirVentasCredito, PermisosN.Secciones.Ventas)
        R += 1
        If DGVentasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Ventas.PermitirRemisionesCredito, PermisosN.Secciones.Ventas)

        '-------------------------------
        '--------------------------------------------------
        '-----------------------------------------------------------
        
        R = 0
        'DGComprasN.Rows.Add("Pre-orden", 0, 0, 0, 0, 0, 0)
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.CotizacionesVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.CotizacionesAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.CotizacionesCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Orden de compra", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.PedidosVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.PedidosAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.PedidosCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("remisiones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.RemisionesVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.RemisionesAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.RemisionesCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Compras", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.ComprasAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.ComprasModificar, PermisosN.Secciones.Compras)
        If DGComprasN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.ComprasBaja, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.ComprasCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Devoluciones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.DevolucionesVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.DevolucionesAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.DevolucionesCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Notas de crédito", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.NotasdeCreditoVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.NotasdeCreditoCancelacion, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Notas de cargo", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.NotasdeCargoVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.NotasdeCargoAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.NotasdeCargoCancelacion, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.DocumentosProveedoresVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.DocumentosProveedoresAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.DocumentosProveedoresCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.PagosVer, PermisosN.Secciones.Compras)
        If DGComprasN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.PagosAlta, PermisosN.Secciones.Compras)
        If DGComprasN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.PagosCambios, PermisosN.Secciones.Compras)
        If DGComprasN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.PagosCancelar, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.Reportes, PermisosN.Secciones.Compras)

        'DGComprasN.Rows.Add("Consultas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.Consultas, PermisosN.Secciones.Compras)
        'DGComprasN.Rows.Add("Cambio Sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras)
        'DGComprasN.Rows.Add("Cambio Almacen", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.CambiodeAlmacen, PermisosN.Secciones.Compras)
        'DGComprasN.Rows.Add("Cambio fecha pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGComprasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Compras.CambiodeFechaPagos, PermisosN.Secciones.Compras)

        '-----------------------------------------------
        '-------------------------------------------------------
        '----------------------------------------------

        'DGPVentaN.Rows.Add("Ventas", 0, 0, 0, 0, 0, 0)
        R = 0
        If DGPVentaN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.VentasVer, PermisosN.Secciones.PuntodeVenta)
        If DGPVentaN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.VentasAlta, PermisosN.Secciones.PuntodeVenta)
        If DGPVentaN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.VentasCancelar, PermisosN.Secciones.PuntodeVenta)

        'DGPVentaN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGPVentaN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.ReportesVer, PermisosN.Secciones.PuntodeVenta)
        'Cambio sucursal
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CambiarSucursal, PermisosN.Secciones.PuntodeVenta)
        'Cambio precio
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CambiodePrecio, PermisosN.Secciones.PuntodeVenta)
        'Descuentos
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.HacerDescuento, PermisosN.Secciones.PuntodeVenta)

        'DGPVentaN.Rows.Add("Movs Cajas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGPVentaN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CajasmovimientosVer, PermisosN.Secciones.PuntodeVenta)
        If DGPVentaN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CajasmovimientosAlta, PermisosN.Secciones.PuntodeVenta)
        If DGPVentaN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CajasmovimientosCancelar, PermisosN.Secciones.PuntodeVenta)
        'Cambio fecha mov cajas cajas
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CajasPermitirCambioFecha, PermisosN.Secciones.PuntodeVenta)
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.VentasBaja, PermisosN.Secciones.PuntodeVenta)
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CambioVendedor, PermisosN.Secciones.PuntodeVenta)
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CambioCaja, PermisosN.Secciones.PuntodeVenta)
        R += 1
        If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.AsignarCantidad, PermisosN.Secciones.PuntodeVenta)
        'Cambio almacen
        'R += 1
        'If DGPVentaN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.PuntodeVentas.CambiodeAlamcen, PermisosN.Secciones.PuntodeVenta)
        '-------------------------------
        '--------------------------------------------
        '----------------------------------

        R = 0
        'DGHerramientasN.Rows.Add("Opciones", 0, 0, 0, 0, 0, 0)
        If DGHerramientasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.OpcionesVer, PermisosN.Secciones.Herramientas)
        If DGHerramientasN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.OpcionesModificar, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Reporte nensual CFD", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGHerramientasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.Reportemensual, PermisosN.Secciones.Herramientas)
        'DGHerramientasN.Rows.Add("Diseño de documentos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGHerramientasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.DocumentosDesingVer, PermisosN.Secciones.Herramientas)
        If DGHerramientasN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.DocumentosDesingModificar, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Configuración correo", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGHerramientasN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.OpcionesCorreoVer, PermisosN.Secciones.Herramientas)
        If DGHerramientasN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.OpcionesCorreoMoficar, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Respaldo", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGHerramientasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.RespaldoVer, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Restauración", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGHerramientasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.RestaurarVer, PermisosN.Secciones.Herramientas)

        'DGHerramientasN.Rows.Add("Cambio de precios", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGHerramientasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.CambiodePrecios, PermisosN.Secciones.Herramientas)
        'DGHerramientasN.Rows.Add("Importador", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGHerramientasN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Herramientas.Importador, PermisosN.Secciones.Herramientas)

        '-----------------------------------
        '----------------------------------------------------
        '--------------------------------------------

        R = 0
        'Movimientos ver
        If DGInventarioN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosVer, PermisosN.Secciones.Inventario)
        'DGInventarioN.Item(5, R).Value = Per.ChecaPermiso(PermisosN.Herramientas.OpcionesModificar, PermisosN.Secciones.Herramientas)
        'DGInventarioN.Rows.Add("Movimientos - Entradas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosAltaEntrada, PermisosN.Secciones.Inventario)
        If DGInventarioN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosCancelarEntrada, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Movimientos - Salidas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosAltaSalidas, PermisosN.Secciones.Inventario)
        If DGInventarioN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosCancelarSalida, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Movimientos - Traspasos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosAltaTraspaso, PermisosN.Secciones.Inventario)
        If DGInventarioN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosCancelarTraspaso, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Movimientos - Inv. Físico", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosAltaAjuste, PermisosN.Secciones.Inventario)
        If DGInventarioN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosCancelarAjuste, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Kardex", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.KardexVer, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.Reportes, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Revisión de inventario", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.RevisionVer, PermisosN.Secciones.Inventario)

        'DGInventarioN.Rows.Add("Recalcular costos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.RecalcularCostos, PermisosN.Secciones.Inventario)
        'cambio sucursal
        R += 1
        If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.CambioSucursal, PermisosN.Secciones.Inventario)
        'Recalcular Inventario
        R += 1
        If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.RecalcularInventarios, PermisosN.Secciones.Inventario)
        R += 1 'negativos
        If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.BuscarNegativos, PermisosN.Secciones.Inventario)
        'cambio almacen
        R += 1
        If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.CambiodeAlamcen, PermisosN.Secciones.Inventario)
        'cambio almacen
        R += 1
        If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosEdicion, PermisosN.Secciones.Inventario)

        R += 1
        If DGInventarioN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.PedidosVer, PermisosN.Secciones.Inventario)
        If DGInventarioN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.PedidosAlta, PermisosN.Secciones.Inventario)
        If DGInventarioN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.PedidosCancelar, PermisosN.Secciones.Inventario)

        R += 1
        If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.PedidosAutorizar, PermisosN.Secciones.Inventario)
        'Movs sin inventario
        'R += 1
        'If DGInventarioN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Inventario.MovimientosSinInventario, PermisosN.Secciones.Inventario)
        
        '-----------------
        '-----------------
        '----------------------
        R = 0
        'DGBancosN.Rows.Add("Bancos", 0, 0, 0, 0, 0, 0)
        If DGBancosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.BancosVer, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.BancosAlta, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.BancosModificar, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.BancosEliminar, PermisosN.Secciones.Bancos)
        
        'DGBancosN.Rows.Add("Cuentas", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGBancosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasVer, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasAlta, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasModificar, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasEliminar, PermisosN.Secciones.Bancos)
        
        'DGBancosN.Rows.Add("Cuentas contables", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGBancosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasContablesVer, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasContablesAlta, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasContablesModificar, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.CuentasContablesEliminar, PermisosN.Secciones.Bancos)
        'DGBancosN.Rows.Add("Depósitos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGBancosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.DepositosVer, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.DepositosAlta, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.DepositosCancelar, PermisosN.Secciones.Bancos)

        'DGBancosN.Rows.Add("Pagos Proveedores", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGBancosN.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.PagoProveedoresVer, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.PagoProveedoresAlta, PermisosN.Secciones.Bancos)
        If DGBancosN.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.PagoProveedoresCancelar, PermisosN.Secciones.Bancos)

        'DGBancosN.Rows.Add("Conciliación", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGBancosN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.Consiliacion, PermisosN.Secciones.Bancos)
        
        'DGBancosN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGBancosN.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Bancos.Reportes, PermisosN.Secciones.Bancos)
        '--------------------------------------------
        'Servicios
        'frmServicios
        R = 0
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServicioVer, PermisosN.Secciones.Servicios)
        If DGServicios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServicioGuardar, PermisosN.Secciones.Servicios)
        'Servicios clas icaciones
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClasVer, PermisosN.Secciones.Servicios)
        If DGServicios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClasGuardar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClasModificar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClasEliminar, PermisosN.Secciones.Servicios)

        'Servicios Consulta
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosConsultaVer, PermisosN.Secciones.Servicios)

        'Servicios Detalles
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosDetallesVer, PermisosN.Secciones.Servicios)
        If DGServicios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosDetallesAgregarEstatus, PermisosN.Secciones.Servicios)
        If DGServicios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosDetallesModificarEstatus, PermisosN.Secciones.Servicios)
        If DGServicios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosDetallesEliminarEstatus, PermisosN.Secciones.Servicios)

        'Agregar Artículo
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloVer, PermisosN.Secciones.Servicios)
        If DGServicios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloAgregar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloModificar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosAgregarArticuloEliminar, PermisosN.Secciones.Servicios)

        'Clientes equipos
        R += 1
        ' If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClientesEquipo, PermisosN.Secciones.Servicios)
        If DGServicios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClientesEquiposGuardar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClientesEquiposModificar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosClientesEquiposEliminar, PermisosN.Secciones.Servicios)

        'Detalles equipos
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.SericiosDetallesEquiposVer, PermisosN.Secciones.Servicios)
        If DGServicios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.SericiosDetallesEquiposAgregar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.SericiosDetallesEquiposModificar, PermisosN.Secciones.Servicios)
        If DGServicios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.SericiosDetallesEquiposEliminar, PermisosN.Secciones.Servicios)

        'if DGServicios.Rows.Add("Consultar Historial", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosVerHistorial, PermisosN.Secciones.Servicios)
        'if DGServicios.Rows.Add("Consultar componentes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosVerConponentes, PermisosN.Secciones.Servicios)
        'if DGServicios.Rows.Add("Consultar detalles", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosVerDetalles, PermisosN.Secciones.Servicios)
        'if DGServicios.Rows.Add("Generar reporte", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGServicios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Servicios.ServiciosVerReportes, PermisosN.Secciones.Servicios)



        'DGNomina.Rows.Add("Nomina", 0, 0, 0, 0, 0, 0)
        R = 0
        If DGNomina.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.NominaVer, PermisosN.Secciones.Nomina)
        If DGNomina.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina)
        If DGNomina.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.NomminaCancelar, PermisosN.Secciones.Nomina)

        'DGNomina.Rows.Add("Trabajadores", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGNomina.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.TrabajadoresVer, PermisosN.Secciones.Nomina)
        If DGNomina.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.TrabajadoresAlta, PermisosN.Secciones.Nomina)
        If DGNomina.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.TrabajadoresCambios, PermisosN.Secciones.Nomina)
        If DGNomina.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.TrabajadoresBaja, PermisosN.Secciones.Nomina)

        'DGNomina.Rows.Add("Importar", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGNomina.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.NominaImportar, PermisosN.Secciones.Nomina)
        'DGNomina.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGNomina.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Nominas.CambiarSucursal, PermisosN.Secciones.Nomina)


        '----------------------------------------


        'DGGastos.Rows.Add("Clasificaciones", 0, 0, 0, 0, 0, 0)
        R = 0
        If DGGastos.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosClasificacionesVer, PermisosN.Secciones.Gastos)
        If DGGastos.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosClasificacionesAltas, PermisosN.Secciones.Gastos)
        If DGGastos.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosClasificacionesCambios, PermisosN.Secciones.Gastos)
        If DGGastos.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosClasificacionesBajas, PermisosN.Secciones.Gastos)
        
        'DGGastos.Rows.Add("Empleados", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGGastos.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosEmpleadosVer, PermisosN.Secciones.Gastos)
        If DGGastos.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosEmpleadosAlta, PermisosN.Secciones.Gastos)
        If DGGastos.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosEmpleadosCambios, PermisosN.Secciones.Gastos)
        If DGGastos.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosEmpleadosBaja, PermisosN.Secciones.Gastos)

        'DGGastos.Rows.Add("Gastos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGGastos.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosVer, PermisosN.Secciones.Gastos)
        If DGGastos.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosAlta, PermisosN.Secciones.Gastos)
        If DGGastos.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosCancelar, PermisosN.Secciones.Gastos)
        
        'DGGastos.Rows.Add("Programar Gastos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGGastos.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosProgramarVer, PermisosN.Secciones.Gastos)
        If DGGastos.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosProgramarAlta, PermisosN.Secciones.Gastos)
        If DGGastos.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosProgramarCambios, PermisosN.Secciones.Gastos)
        If DGGastos.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosProgramarBajas, PermisosN.Secciones.Gastos)

        'DGGastos.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGGastos.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosReportesVer, PermisosN.Secciones.Gastos)
        'DGGastos.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGGastos.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosCambiarSucursal, PermisosN.Secciones.Gastos)
        R += 1 'fecha
        If DGGastos.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosPermitirCambioFecha, PermisosN.Secciones.Gastos)
        R += 1
        If DGGastos.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Gastos.GastosVerNotificaciones, PermisosN.Secciones.Gastos)

        'DGEmpenios.Rows.Add("Configuración", 0, 0, 0, 0, 0, 0)
        R = 0
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosConfiguracionVer, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosConfiguracionAlta, PermisosN.Secciones.Empenios)
        
        'DGEmpenios.Rows.Add("Identificaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosIdentificacionVer, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosIdentificacionAlta, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosIdentificacionCambios, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosIdentificacionBaja, PermisosN.Secciones.Empenios)
        'DGEmpenios.Item(5, R).ReadOnly = True
        'DGEmpenios.Item(6, R).ReadOnly = True
        'DGGastos.Rows.Add("Clasificaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosClasificacionesVer, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosClasificacionesAlta, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosClasificacionesCambios, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosClasificacionesBaja, PermisosN.Secciones.Empenios)

        'DGEmpenios.Rows.Add("Empeños", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosVer, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosAlta, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosCancelar, PermisosN.Secciones.Empenios)
        
        'DGEmpenios.Rows.Add("Permitir empeño arriba del evaluo", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosSobreValouPermitir, PermisosN.Secciones.Empenios)
        
        'DGEmpenios.Rows.Add("Pagos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosPagosVer, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosPAgosAlta, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosPagosCambios, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosPAgosBaja, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Adjudicaciones", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosAdjudicacionesVer, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosAdjudicacionesAlta, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Consulta de movimientos", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosConsultaMovVer, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosReportesVer, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Cambiar sucursal", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosCambiarSucursal, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Empeños compras", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosComprasVer, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosComprasAlta, PermisosN.Secciones.Empenios)
        If DGEmpenios.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosComprasCancelar, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Cambiar vendedor", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.EmpeniosPermitirCambiarVendedor, PermisosN.Secciones.Empenios)
        'DGCatalogosN.Rows.Add("Reportes", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.VerCorte, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Sin limite", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.NoLimitarEvaluo, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Descuento", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.PermitirDescuento, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("cambio Fecha", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.PermitirCambioFecha, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Extra ver", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.PermitirExtraVer, PermisosN.Secciones.Empenios)
        'DGEmpenios.Rows.Add("Extra Cambio", 0, 0, 0, 0, 0, 0)
        R += 1
        If DGEmpenios.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Empenios.PermitirExtraCambio, PermisosN.Secciones.Empenios)

        '''''-------------------------------
        '-------------------------Fertilizantes
        R = 0
        'Pedidos
        If DGFertilizantes.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.PedidosVer, PermisosN.Secciones.Fertilizantes)
        If DGFertilizantes.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.PedidosAlta, PermisosN.Secciones.Fertilizantes)
        If DGFertilizantes.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.PedidosCancelar, PermisosN.Secciones.Fertilizantes)

        R += 1
        'Movimientos
        If DGFertilizantes.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.MovimientosAlta, PermisosN.Secciones.Fertilizantes)
        If DGFertilizantes.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.MovimientosCambios, PermisosN.Secciones.Fertilizantes)
        If DGFertilizantes.Item(5, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.MovimientosCancelar, PermisosN.Secciones.Fertilizantes)
        If DGFertilizantes.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.MovimientosVer, PermisosN.Secciones.Fertilizantes)

        R += 1
        'Reportes
        If DGFertilizantes.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Fertilizantes.ReportesVer, PermisosN.Secciones.Fertilizantes)

        '---------------CONTA---------------
        ''------------------------------------------
        R = 0
        'Config
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ConfiguracionVer, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ConfiguracionModificar, PermisosN.Secciones.Contabilidad)

        R += 1
        'Clasificacion
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasVer, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasAlta, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasCambios, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ClasificacionesPolizasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        'Cuentas
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.CuentasVer, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.CuentasAlta, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.CuentasModificar, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.CuentasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        'Pólizas
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.PolizasVer, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.PolizasAlta, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.PolizasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        'saldos
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ConsultaSaldosVer, PermisosN.Secciones.Contabilidad)
        R += 1
        'diot
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ConsiliaciondiotVer, PermisosN.Secciones.Contabilidad)

        R += 1
        'Reportes
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.ReportesVer, PermisosN.Secciones.Contabilidad)

        R += 1
        'máscaras
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.MascarasVer, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.MascarasAlta, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.MascarasBaja, PermisosN.Secciones.Contabilidad)

        R += 1
        'Generar
        If DGcontabilidad.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.GenerarPolizasPermitir, PermisosN.Secciones.Contabilidad)
        R += 1
        'Nomina
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.NominaConceptosVer, PermisosN.Secciones.Contabilidad)
        If DGcontabilidad.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.NominaConceptosAlta, PermisosN.Secciones.Contabilidad)

        R += 1
        'ver generadas
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad)
        R += 1
        'ver fecha
        If DGcontabilidad.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.VerFechaConta, PermisosN.Secciones.Contabilidad)

        'SEMILLAS------------------------
        '---------------------------------------------
        R = 0
        'Boletas
        If DGSemillas.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.BoletasVer, PermisosN.Secciones.Semillas)
        If DGSemillas.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.BoletasAlta, PermisosN.Secciones.Semillas)
        'If DGSemillas.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        If DGSemillas.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.BoletasBaja, PermisosN.Secciones.Semillas)
        R += 1
        'Comprobante
        If DGSemillas.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.ComprobanteVer, PermisosN.Secciones.Semillas)
        If DGSemillas.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.ComprobanteAlta, PermisosN.Secciones.Semillas)
        'If DGSemillas.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        If DGSemillas.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.ComprobanteBaja, PermisosN.Secciones.Semillas)
        R += 1
        'Liquidacion
        If DGSemillas.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.LiquidacionVer, PermisosN.Secciones.Semillas)
        If DGSemillas.Item(2, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.LiquidacionAlta, PermisosN.Secciones.Semillas)
        'If DGSemillas.Item(3, R).Value = True Then Per.AsignaPermiso(PermisosN.Contabilidad.MascarasCambios, PermisosN.Secciones.Contabilidad)
        If DGSemillas.Item(4, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.LiquidacionBaja, PermisosN.Secciones.Semillas)
        R += 1
        'reportes
        If DGSemillas.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.ReportesVer, PermisosN.Secciones.Semillas)
        R += 1
        'config
        If DGSemillas.Item(6, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.Configuracion, PermisosN.Secciones.Semillas)
        R += 1
        'reportes
        If DGSemillas.Item(1, R).Value = True Then Per.AsignaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas)
    End Sub

    Private Sub DGCatalogosN_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGCatalogosN.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGCatalogosN.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGCatalogosN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGCatalogosN.CellContentClick

    End Sub

    Private Sub DGVentasN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVentasN.CellContentClick

    End Sub

    Private Sub DGVentasN_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGVentasN.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGVentasN.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGComprasN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGComprasN.CellContentClick

    End Sub

    Private Sub DGComprasN_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGComprasN.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGComprasN.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGInventarioN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGInventarioN.CellContentClick

    End Sub

    Private Sub DGInventarioN_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGInventarioN.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGInventarioN.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGPVentaN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGPVentaN.CellContentClick

    End Sub

    Private Sub DGPVentaN_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGPVentaN.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGPVentaN.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGHerramientasN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGHerramientasN.CellContentClick

    End Sub

    Private Sub DGHerramientasN_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGHerramientasN.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGHerramientasN.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        ReseteaPermisos(False, -1)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        ReseteaPermisos(False, 0)
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        ReseteaPermisos(False, 1)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        ReseteaPermisos(False, 2)
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        ReseteaPermisos(False, 3)
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        ReseteaPermisos(False, 4)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ReseteaPermisos(False, 5)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        ReseteaPermisos(True, 5)
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        ReseteaPermisos(True, 4)
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        ReseteaPermisos(True, 3)
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        ReseteaPermisos(True, 2)
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        ReseteaPermisos(True, 1)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        ReseteaPermisos(True, 0)
    End Sub

    Private Sub DGBancosN_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGBancosN.CellContentClick

    End Sub

    Private Sub DGBancosN_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGBancosN.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGBancosN.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        ReseteaPermisos(False, 6)
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        ReseteaPermisos(True, 6)
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        ReseteaPermisos(True, 7)
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        ReseteaPermisos(False, 7)
    End Sub

    Private Sub DGServicios_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGServicios.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGServicios.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGServicios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGServicios.CellContentClick

    End Sub

    Private Sub DGNomina_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGNomina.CellContentClick

    End Sub

    Private Sub DGNomina_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGNomina.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGNomina.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        ReseteaPermisos(False, 8)
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        ReseteaPermisos(True, 8)
    End Sub

    Private Sub DGGastos_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGGastos.CellContentClick
        
    End Sub

    Private Sub DGGastos_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGGastos.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGGastos.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGEmpenios_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGEmpenios.CellContentClick

    End Sub

    Private Sub DGEmpenios_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGEmpenios.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGEmpenios.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        ReseteaPermisos(False, 9)
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        ReseteaPermisos(False, 10)
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        ReseteaPermisos(True, 10)
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        ReseteaPermisos(True, 9)
    End Sub

    Private Sub DGFertilizantes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGFertilizantes.CellContentClick

    End Sub

    Private Sub DGFertilizantes_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGFertilizantes.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGFertilizantes.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub DGcontabilidad_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGcontabilidad.CellContentClick

    End Sub

    Private Sub DGcontabilidad_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGcontabilidad.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGcontabilidad.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        ReseteaPermisos(False, 11)
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        ReseteaPermisos(True, 11)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        ReseteaPermisos(False, 12)
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        ReseteaPermisos(True, 12)
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim U As New dbUsuarios(MySqlcon)
        Dim MsgError As String = ""
        If TextBox4.Text = "" Then
            MsgError = "Debe indicar un nombre al perfil."
        End If
        If U.ChecaPerfilRepetido(TextBox4.Text) And TextBox4.Text <> "" Then
            MsgError = "Ya existe un perfil con ese nombre."
        End If
        If MsgError = "" Then
            AsignaPermisos()
            U.GuardaPerfil(TextBox4.Text, Per.PermisosCatalogos, Per.PermisosCatalogos2, Per.PermisosVentas, Per.PermisosCompras, Per.PermisosInventario, Per.PermisosHerramientas, Per.PermisosPuntodeVenta, Per.PermisosBancos, Per.PermisosServicios, Per.PermisosNomina, Per.PermisosGastos, Per.PermisosEmpenios, Per.PermisosFertilizantes, Per.PermisosContabilidad, Per.PermisosSemillas)
            PopUp("Perfil guardado", 90)
            TextBox4.Text = ""
            LlenaCombos("tblperfilespermisos", ComboBox2, "nombre", "nombrep", "idperfil", IdsPerfiles)
        Else
            MsgBox(MsgError, MsgBoxStyle.Information, GlobalNombreApp)
        End If

    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If ComboBox2.Items.Count > 0 Then
            Dim U As New dbUsuarios(MySqlcon)
            U.LlenaPerfil(IdsPerfiles.Valor(ComboBox2.SelectedIndex))
            Per.PermisosCatalogos = U.PermisosCatalogos
            Per.PermisosCatalogos2 = U.PermisosCatalogos2
            Per.PermisosCompras = U.PermisosCompras
            Per.PermisosVentas = U.PermisosVentas
            Per.PermisosInventario = U.PermisosInventario
            Per.PermisosServicios = U.PermisosServicios
            Per.PermisosHerramientas = U.PermisosHerramientas
            Per.PermisosPuntodeVenta = U.PermisosPuntodeVenta
            Per.PermisosBancos = U.PermisosBancos
            Per.PermisosNomina = U.PermisosNomina
            Per.PermisosGastos = U.PermisosGastos
            Per.PermisosEmpenios = U.PermisosEmpenios
            Per.PermisosFertilizantes = U.PermisosFertilizantes
            Per.PermisosContabilidad = U.PermisosContabilidad
            ReseteaPermisos(False, -1)
            ConsultaPermisos()
            PopUp("Ok", 50)
        End If
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        If MsgBox("¿Eliminar este perfil?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim u As New dbUsuarios(MySqlcon)
            u.EliminaPerfil(IdsPerfiles.Valor(ComboBox2.SelectedIndex))
            LlenaCombos("tblperfilespermisos", ComboBox2, "nombre", "nombrep", "idperfil", IdsPerfiles)
            PopUp("Perfil Eliminado", 90)
        End If
    End Sub

    Private Sub DGSemillas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGSemillas.CellContentClick
        
    End Sub

    Private Sub DGSemillas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGSemillas.CellFormatting
        If e.ColumnIndex > 0 Then
            If DGSemillas.Item(e.ColumnIndex, e.RowIndex).ReadOnly = False Then
                If e.Value = 0 Then
                    e.CellStyle.BackColor = Color.FromArgb(250, 150, 150)
                Else
                    e.CellStyle.BackColor = Color.FromArgb(150, 250, 150)
                End If
            Else
                e.CellStyle.BackColor = Color.FromArgb(200, 200, 200)
            End If
        End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        ReseteaPermisos(False, 13)
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        ReseteaPermisos(True, 13)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class