Public Class frmListaPreciosCambio
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim ConsultaOn As Boolean = False
    Dim EncontroClas As Boolean = True
    Dim IdInventario As Integer = 0
    Dim IdVariante As Integer = 0
    Dim IdLista As Integer = 1
    Private Sub frmListaPreciosCambio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas", "nombre")
        Consulta()
    End Sub
    Private Sub HabilitaClase2(ByVal Habilita As Boolean)
        TextBox12.Enabled = Habilita
        ComboBox6.Enabled = Habilita
    End Sub
    Private Sub HabilitaClase3(ByVal Habilita As Boolean)
        TextBox13.Enabled = Habilita
        ComboBox7.Enabled = Habilita
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            ConsultaOn = False
            TextBox10.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")
            HabilitaClase2(True)
        Else
            HabilitaClase2(False)
            HabilitaClase3(False)
            ComboBox6.SelectedIndex = -1
            ConsultaOn = False
            TextBox10.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas2.Valor(ComboBox6.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            ConsultaOn = False
            TextBox12.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "Todas", "nombre")
            HabilitaClase3(True)
        Else
            HabilitaClase3(False)
            ComboBox7.SelectedIndex = -1
            ConsultaOn = False
            TextBox12.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
            If IC.BuscaClasificacion(TextBox10.Text) Then
                EncontroClas = True
                ComboBox3.SelectedIndex = IdsClas.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            If IC.BuscaClasificacion(TextBox12.Text, IdsClas.Valor(ComboBox3.SelectedIndex)) Then
                EncontroClas = True
                ComboBox6.SelectedIndex = IdsClas2.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        If ConsultaOn Then
            Dim IC As New dbInventarioClasificaciones(MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            If IC.BuscaClasificacion(TextBox13.Text, IdsClas2.Valor(ComboBox6.SelectedIndex)) Then
                EncontroClas = True
                ComboBox7.SelectedIndex = IdsClas3.Busca(IC.ID)
            Else
                EncontroClas = False
            End If
        End If
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas3.Valor(ComboBox7.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel3)
            ConsultaOn = False
            TextBox13.Text = IC2.Codigo
            ConsultaOn = True
        Else
            ConsultaOn = False
            TextBox13.Text = ""
            ConsultaOn = True
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
        Dim B As New frmBuscador(TipodeBusqueda, 0, True, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Select Case B.Tipo
                Case "I"
                    IdInventario = B.Inventario.ID
                    IdVariante = 0
                    ConsultaOn = False
                    TextBox3.Text = B.Inventario.Clave
                    ConsultaOn = True
                    TextBox4.Text = B.Inventario.Nombre
            End Select

        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 1) Then
                    IdInventario = p.ID
                    TextBox4.Text = p.Nombre
                Else
                    IdInventario = 0
                        TextBox4.Text = ""
                End If
            End If
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
                DataGridView1.Columns(1).Width = 50
                DataGridView1.Columns(2).HeaderText = "Descripción"
                DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        IdLista = DataGridView1.Item(0, e.RowIndex).Value
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim IP As New dbInventarioPrecios(MySqlcon)
            Dim idClas As Integer
            Dim IdClas2 As Integer
            Dim IdClas3 As Integer
            Dim Tipo As Byte
            If ComboBox3.SelectedIndex <= 0 Then
                idClas = 0
            Else
                idClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            End If
            If ComboBox6.SelectedIndex <= 0 Then
                idClas2 = 0
            Else
                idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
            End If
            If ComboBox7.SelectedIndex <= 0 Then
                idClas3 = 0
            Else
                idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
            End If
            If MsgBox("¿Guardar los cambios? Este proceso no se puede deshacer.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                If CheckBox1.Checked Then IdLista = 0
                If RadioButton1.Checked Then Tipo = 0
                If RadioButton2.Checked Then Tipo = 1
                If RadioButton3.Checked Then Tipo = 2
                If RadioButton4.Checked Then Tipo = 3
                If RadioButton5.Checked Then Tipo = 4
                If RadioButton6.Checked Then Tipo = 5
                If RadioButton1.Checked And TextBox1.Text.Contains("%") Then Tipo = 6
                Dim op As New dbOpciones(MySqlcon)
                IP.CambiaPrecio(IdInventario, idClas, IdClas2, IdClas3, IdLista, CDbl(TextBox1.Text.Replace("%", "")), Tipo, op._MetodoUtilidad)
                PopUp("Guardado.", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If MsgBox("Esta operación no se puede deshacer. ¿Continuar?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                Exit Sub
            End If
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim I As New Importador(OpenFileDialog1.FileName, MySqlcon)
                Dim IdC As Integer = IdsClas.Valor(ComboBox3.SelectedIndex)
                Dim IdC2 As Integer = 0
                Dim IDc3 As Integer = 0
                If IdC > 0 Then
                    IdC2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
                    If IdC2 > 0 Then
                        IDc3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
                    End If
                End If
                I.ImportarPrecios(IdC, IdC2, IDc3)
                I.CierraConexiones()
                MsgBox("Listo", MsgBoxStyle.OkOnly, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class