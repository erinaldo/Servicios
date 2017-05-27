Public Class frmInventarioRelaciones
    Dim IR As New dbInventarioRelaciones(MySqlcon)
    Dim Inv As New dbInventario(MySqlcon)
    Dim Tipo As Byte

    Private Sub frmInventarioRelaciones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaInventario()
        Nuevo()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Guardar(False)
    End Sub
    Private Sub Guardar(ByVal SinNuevo As Boolean)
        If TextBox6.Text <> "" Then
            If Button1.Text = "Guardar" Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesAlta, PermisosN.Secciones.Catalagos2) = False Then
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                IR.Guardar(TextBox6.Text, "")
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesCambio, PermisosN.Secciones.Catalagos2) = False Then
                    MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
                IR.Modificar(IR.ID, TextBox6.Text, "")
            End If
            If SinNuevo = False Then Nuevo()
        Else
            MsgBox("Debe indicar un nombre a la relación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        AgregarArticulo()
    End Sub
    Private Sub AgregarArticulo()
        If Inv.ID <> 0 Then
            If Button1.Text = "Guardar" Then
                Guardar(True)
                Button2.Enabled = True
                Button1.Text = "Modificar"
            End If
            If IR.ChecaSiyaEsta(Inv.ID) = False Then
                IR.GuardarDetalle(IR.ID, Inv.ID)
                ConsultaDetalles()
            Else
                MsgBox("Este artículo ya se encuentra en la relación o está en otra relación ya asignado.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        RemoverArticulo()
    End Sub
    Private Sub RemoverArticulo()
        If IR.IdDetalle <> 0 Then
            IR.EliminarDetalle(IR.IdDetalle)
            ConsultaDetalles()
            IR.IdDetalle = 0
        End If
    End Sub
    Private Sub ConsultaDetalles()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then

            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex

            'Dim I As New dbInventario(MySqlcon)
            DataGridView1.DataSource = IR.ConsultaDetalles(IR.ID, "")
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Código"
            DataGridView1.Columns(2).HeaderText = "Descripción"
            'DataGridView1.Columns(3).HeaderText = "Cantidad"
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            IR.IdDetalle = 0
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then

            If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex

            'Dim I As New dbInventario(MySqlcon)
            DataGridView2.DataSource = IR.Consulta(TextBox1.Text)
            DataGridView2.Columns(0).Visible = False
            DataGridView2.Columns(1).HeaderText = "Nombre"
            'DataGridView1.Columns(2).HeaderText = "Descripción"
            'DataGridView1.Columns(3).HeaderText = "Cantidad"
            DataGridView2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaInventario()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then

            If DataGridView3.RowCount > 0 Then PrimerCeldaRow = DataGridView3.FirstDisplayedCell.RowIndex

            'Dim I As New dbInventario(MySqlcon)
            DataGridView3.DataSource = Inv.Consulta(0, TextBox2.Text, "", , True, , , , , , , , 0)
            DataGridView3.Columns(0).Visible = False
            DataGridView3.Columns(3).Visible = False
            DataGridView3.Columns(1).HeaderText = "Código"
            DataGridView3.Columns(2).HeaderText = "Descripción"
            'DataGridView3.Columns(3).HeaderText = "Cantidad"
            DataGridView3.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView3.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView3.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub Nuevo()
        IR.Nuevo()
        TextBox6.Text = ""
        Button1.Text = "Guardar"
        Button2.Enabled = False
        ConsultaDetalles()
        Consulta()
        TextBox6.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Consulta()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        IR.IdDetalle = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        IR.IdDetalle = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
        RemoverArticulo()
    End Sub

    Private Sub DataGridView3_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Inv.ID = DataGridView3.Item(0, DataGridView3.CurrentCell.RowIndex).Value
    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub DataGridView3_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        Inv.ID = DataGridView3.Item(0, DataGridView3.CurrentCell.RowIndex).Value
        AgregarArticulo()
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        LlenaDatos()
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick


    End Sub
    Private Sub LlenaDatos()
        IR.LlenaDAtos(DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value)
        TextBox6.Text = IR.Nombre
        ConsultaDetalles()
        Button1.Text = "Modificar"
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioRelacionesBaja, PermisosN.Secciones.Catalagos2) = True Then
            If MsgBox("¿Eliminar esta relación?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                IR.Eliminar(IR.ID)
                Nuevo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
        
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        ConsultaInventario()
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub
End Class