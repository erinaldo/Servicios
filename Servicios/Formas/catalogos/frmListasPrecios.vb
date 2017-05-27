Public Class frmListasPrecios
    Dim IdLista As Integer
    Dim ConsultaOn As Boolean
    Dim ClaveAnterior As String
    Dim IdsSucursales As New elemento
    Private Sub frmAlmacenes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Try
            Nuevo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            txtDescripcion.Text = ""
            txtNumero.Text = ""
            txtNumero.BackColor = Color.FromKnownColor(KnownColor.Window)
            txtDescripcion.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ConsultaOn = True
            Consulta()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbListasPrecios(MySqlcon)
                DataGridView1.DataSource = P.Consulta()
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Número"
                DataGridView1.Columns(1).Width = 80
                DataGridView1.Columns(2).HeaderText = "Descripción"
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim P As New dbListasPrecios(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""

            If txtDescripcion.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un número al almacen."
                txtDescripcion.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If txtNumero.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar un nombre al almacen."
                txtNumero.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Almacenes + 1)))) <> 0 Then
                    If P.ChecaNumeroRepetido(txtDescripcion.Text) = False Then
                        P.Guardar(txtDescripcion.Text, txtNumero.Text)
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("Ya existe una lista con ese número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                        txtDescripcion.BackColor = Color.FromArgb(250, 150, 150)
                    End If
                    'Else
                    '   MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    'End If
                Else
                    'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Almacenes + 2)))) <> 0 Then
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        Dim ClaveRepetida As Boolean = False
                        If txtDescripcion.Text <> ClaveAnterior Then
                            ClaveRepetida = P.ChecaNumeroRepetido(txtDescripcion.Text)
                        End If
                        If ClaveRepetida = False Then
                            P.Modificar(IdLista, txtDescripcion.Text, txtNumero.Text)
                            PopUp("Modificado", 90)
                            Nuevo()
                        Else
                            MsgBox("Ya existe una lista con ese número, favor de cambiarlo.", MsgBoxStyle.Critical, GlobalNombreApp)
                            txtDescripcion.BackColor = Color.FromArgb(250, 150, 150)
                        End If
                    End If
                    'Else
                    'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
                    'End If
                End If
            txtDescripcion.Focus()
            Else
            MsgBox(MensajeError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            'If (PermisosCatalogos1 And CULng((Math.Pow(2, percatalogos1.Almacenes + 3)))) <> 0 Then
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim P As New dbListasPrecios(MySqlcon)
                P.Eliminar(IdLista)
                PopUp("Eliminado", 90)
                Nuevo()
                txtDescripcion.Focus()
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
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

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            IdLista = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbListasPrecios(IdLista, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            ClaveAnterior = P.Numero
            txtNumero.Text = P.Numero
            txtDescripcion.Text = P.Descripcion
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            txtNumero.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub

End Class