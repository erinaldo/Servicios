Public Class frmInventarioGeneradorMTC
    Dim IdsTC1 As New elemento
    Dim IdsTC2 As New elemento
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim IdInventario As Integer
    Dim IdsMonedas As New elemento
    Dim ConsultaOn As Boolean = False
    Dim EncontroClas As Boolean = True
    Dim idModelo As Integer
    Dim IdPrecio As Integer
    Dim Modelo As dbModelos
    Public Sub New(ByVal pIdModelo As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        idModelo = pIdModelo
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub frmInventarioGeneradorMTC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tbltiposcantidades", ComboBox1, "nombre", "nombret", "idtipocantidad", IdsTC1, "idtipocantidad>1")
        LlenaCombos("tbltiposcantidades", ComboBox2, "nombre", "nombret", "idtipocantidad", IdsTC2, "idtipocantidad>1")
        ComboBox6.Items.Add("General")
        ComboBox7.Items.Add("General")
        LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", , "nombre")
        LlenaCombos("tblmonedas", ComboBox4, "nombre", "nombrem", "idmoneda", IdsMonedas, "idmoneda>1")
        TextBox18.Text = "0"
        TextBox19.Text = "0"
        TextBox20.Text = "0"
        TextBox4.Text = "0"
        TextBox3.Text = "1"
        TextBox14.Text = "16"
        Modelo = New dbModelos(idModelo, MySqlcon)
        ConsultaOn = True
        GeneraCombinaciones()
        ConsultaPrecios(True)
        Label14.Text = "Descripción: " + TextBox1.Text + " " + Modelo.Nombre + " Azul XL"
        TextBox6.Text = Modelo.Codigo + "0101"

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim IdClas As Integer
        IdClas = IdsClas.Valor(ComboBox3.SelectedIndex)
        Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel1)
        ConsultaOn = False
        TextBox10.Text = IC2.Codigo
        ConsultaOn = True
        LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "General", "nombre")
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex > 0 Then
            Dim IdClas As Integer
            IdClas = IdsClas2.Valor(ComboBox6.SelectedIndex)
            Dim IC2 As New dbInventarioClasificaciones(IdClas, MySqlcon, dbInventarioClasificaciones.TiposClasificacion.Nivel2)
            ConsultaOn = False
            TextBox12.Text = IC2.Codigo
            ConsultaOn = True
            LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IC2.ID.ToString, "General", "nombre")
            HabilitaClase3(True)
        Else
            HabilitaClase3(False)
            ComboBox7.SelectedIndex = 0
            ConsultaOn = False
            TextBox12.Text = ""
            ConsultaOn = True
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
    Private Sub HabilitaClase2(ByVal Habilita As Boolean)
        TextBox12.Enabled = Habilita
        ComboBox6.Enabled = Habilita
    End Sub
    Private Sub HabilitaClase3(ByVal Habilita As Boolean)
        TextBox13.Enabled = Habilita
        ComboBox7.Enabled = Habilita
    End Sub

    Private Sub GeneraCombinaciones()

        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim I As New dbModelos(MySqlcon)
                DataGridView1.DataSource = I.Generacombinaciones(CheckBox1.Checked, CheckBox4.Checked, CheckBox6.Checked)
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        GeneraCombinaciones()
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        GeneraCombinaciones()
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        GeneraCombinaciones()
    End Sub
    Private Sub LlenaDatosPrecios()
        Try
            IdPrecio = DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value
            Dim IP As New dbInventarioPrecios(MySqlcon)
            IP.ID = IdPrecio
            IP.LlenaDatosTemp()
            txtPrecio.Text = Format(IP.Precio, "0.00")
            TextBox9.Text = IP.Comentario
            txtUtilidad.Text = Format(IP.utilidad, "0.00")
            TextBox7.Text = Format(IP.Descuento, "0.00")
            ComboBox4.SelectedIndex = IdsMonedas.Busca(IP.IdMoneda)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub NuevoPrecio()
        txtPrecio.Text = "0"
        txtPrecio.BackColor = Color.FromKnownColor(KnownColor.Window)
        TextBox9.Text = ""
        TextBox7.Text = "0"
        txtUtilidad.Text = "0"
        ConsultaPrecios(False)
    End Sub
    Private Sub ConsultaPrecios(ByVal ConReset As Boolean)
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex
                Dim I As New dbInventarioPrecios(MySqlcon)
                If ConReset Then I.ReseteaTemp()
                DataGridView2.DataSource = I.ConsultaTemp(IdInventario)
                DataGridView2.Columns(0).Visible = False
                DataGridView2.Columns(1).HeaderText = "Lista precio"
                DataGridView2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DataGridView2.Columns(2).HeaderText = "Precio"
                DataGridView2.Columns(2).DefaultCellStyle.Format = "C2"
                DataGridView2.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView2.Columns(2).Width = 80
                DataGridView2.Columns(3).HeaderText = "Utilidad"
                DataGridView2.Columns(3).Width = 80
                DataGridView2.Columns(4).HeaderText = "Descuento"
                DataGridView2.Columns(4).Width = 80
                DataGridView2.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView2.Columns(5).HeaderText = "Comentario"
                DataGridView2.Columns(5).Width = 80
                If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        LlenaDatosPrecios()
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Dim EsNuevo As Boolean = False
            If GlobalPermisos.ChecaPermiso(PermisosN.Catalogos2.InventarioPrecios, PermisosN.Secciones.Catalagos2) = True Then  
                Dim IP As New dbInventarioPrecios(MySqlcon)
                If IsNumeric(txtPrecio.Text) And IsNumeric(txtUtilidad.Text) Then
                    'If Button5.Text = "Agregar" Then
                    '    IP.Guardar(CDbl(txtPrecio.Text), IdInventario, CDbl(txtUtilidad.Text), TextBox9.Text, IdsMonedas2.Valor(ComboBox4.SelectedIndex))
                    '    PopUp("Precio Agregado", 90)
                    '    NuevoPrecio()
                    'Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        'If CDbl(txtUtilidad.Text) <> 0 Then
                        '    txtPrecio.Text = CStr(CDbl(TextBox7.Text) * (1 + CDbl(txtUtilidad.Text) / 100))
                        'End If
                        If CDbl(TextBox7.Text) <> 0 Then
                            txtPrecio.Text = IP.DaPrecioListaUnotEMP(IdInventario).ToString
                            txtPrecio.Text = CStr(CDbl(txtPrecio.Text) - (CDbl(txtPrecio.Text) * CDbl(TextBox7.Text) / 100))
                        End If

                        IP.ModificarTemp(IdPrecio, CDbl(txtPrecio.Text), CDbl(txtUtilidad.Text), TextBox9.Text, IdsMonedas.Valor(ComboBox4.SelectedIndex), CInt(TextBox7.Text))
                        PopUp("Precio Modificado", 90)
                        NuevoPrecio()
                    End If
                    ' End If
                Else
                    'txtPrecio.BackColor = Color.FromArgb(250, 150, 150)
                    MsgBox("El precio y utilidad deben ser un valor numérico.", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If ConsultaOn Then
            Label14.Text = "Descripción: " + TextBox1.Text
            If CheckBox12.Checked Then Label14.Text += " " + Modelo.Nombre
            If CheckBox11.Checked Then Label14.Text += " Azul"
            If CheckBox10.Checked Then Label14.Text += " XL"
        End If
    End Sub

    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        If ConsultaOn Then
            Label14.Text = "Descripción: " + TextBox1.Text
            If CheckBox12.Checked Then Label14.Text += " " + Modelo.Nombre
            If CheckBox11.Checked Then Label14.Text += " Azul"
            If CheckBox10.Checked Then Label14.Text += " XL"
        End If
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        If ConsultaOn Then
            Label14.Text = "Descripción: " + TextBox1.Text
            If CheckBox12.Checked Then Label14.Text += " " + Modelo.Nombre
            If CheckBox11.Checked Then Label14.Text += " Azul"
            If CheckBox10.Checked Then Label14.Text += " XL"
        End If
    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If ConsultaOn Then
            Label14.Text = "Descripción: " + TextBox1.Text
            If CheckBox12.Checked Then Label14.Text += " " + Modelo.Nombre
            If CheckBox11.Checked Then Label14.Text += " Azul"
            If CheckBox10.Checked Then Label14.Text += " XL"
        End If
    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        Dim r As Integer = 0
        While r <= DataGridView1.RowCount - 1
            If DataGridView1.Item(1, r).Value <> "" And DataGridView1.Item(2, r).Value = "" Then
                If CheckBox9.Checked Then
                    DataGridView1.Item(0, r).Value = 1
                Else
                    DataGridView1.Item(0, r).Value = 0
                End If
            End If
            r += 1
        End While
        DataGridView1.Refresh()
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        Dim r As Integer = 0
        While r <= DataGridView1.RowCount - 1
            If DataGridView1.Item(2, r).Value <> "" And DataGridView1.Item(1, r).Value = "" Then
                If CheckBox8.Checked Then
                    DataGridView1.Item(0, r).Value = 1
                Else
                    DataGridView1.Item(0, r).Value = 0
                End If
            End If
            r += 1
        End While
        DataGridView1.Refresh()
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        Dim r As Integer = 0
        While r <= DataGridView1.RowCount - 1
            If DataGridView1.Item(1, r).Value <> "" And DataGridView1.Item(2, r).Value <> "" Then
                If CheckBox7.Checked Then
                    DataGridView1.Item(0, r).Value = 1
                Else
                    DataGridView1.Item(0, r).Value = 0
                End If
            End If
            r += 1
        End While
        DataGridView1.Refresh()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Button1.Enabled = False
            Dim I As New dbInventario(MySqlcon)
            Dim Desc As String
            Dim IdClas2 As Integer
            Dim IdClas3 As Integer
            Dim PrecioNeto As Byte = 0
            Dim Inventariable As Byte = 0
            Dim ManejaSeries As Byte = 0
            Dim Rest As Byte = 0
            If CheckBox13.Checked Then Rest = 1
            Dim MensajeError As String = ""
            If CheckBox5.Checked Then PrecioNeto = 1
            If CheckBox3.Checked Then Inventariable = 1
            If CheckBox2.Checked Then ManejaSeries = 1
            If ComboBox6.SelectedIndex = 0 Then
                IdClas2 = 1
            Else
                IdClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
            End If
            If ComboBox7.SelectedIndex = 0 Then
                IdClas3 = 1
            Else
                IdClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
            End If

            'If IsNumeric(TextBox2.Text) = False Then
            '    'NoErrores = False
            '    MensajeError = "La Cantidad debe ser un valor numérico."
            '    'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            'End If
            If IsNumeric(TextBox3.Text) = False Then
                'NoErrores = False
                MensajeError += vbCrLf + " El contenido debe ser un valor numérico."
                'TextBox3.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox4.Text) = False Then
                'NoErrores = False
                MensajeError += vbCrLf + " El punto de reorden debe ser un valor numérico."
                'TextBox4.BackColor = Color.FromArgb(250, 150, 150)
            End If
            Desc = TextBox1.Text
            If CheckBox12.Checked Then Desc += " " + Modelo.Nombre
            If Desc = "" Then
                'NoErrores = False
                MensajeError += vbCrLf + " Debe indicar una descripción al artículo."
                'TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            'If TextBox6.Text = "" Then
            '    'NoErrores = False
            '    MensajeError += vbCrLf + " Debe indicar un código al artículo."
            '    'TextBox6.BackColor = Color.FromArgb(250, 150, 150)
            'End If
            Dim Impresora As String = ""
            If checkImprimir.Checked Then
                If PrintDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Impresora = PrintDialog1.PrinterSettings.PrinterName
                Else
                    MensajeError = "Debe indicar una impresora."
                End If
                If IsNumeric(TextBox11.Text) = False Then
                    TextBox11.Text = "1"
                End If
            End If
            If IsNumeric(TextBox18.Text) = False Then
                'NoErrores = False
                MensajeError = "El peso debe ser un valor numérico."
                'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox19.Text) = False Then
                'NoErrores = False
                MensajeError = "El Mínimo debe ser un valor numérico."
                'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox20.Text) = False Then
                'NoErrores = False
                MensajeError = "La Máximo debe ser un valor numérico."
                'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If MensajeError = "" Then
                If MsgBox("¿Generar los artículos seleccionados? Esta operación no se puede deshacer.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim r As Integer = 0
                    Dim B As Integer = 0
                    While r <= DataGridView1.RowCount - 1
                        If DataGridView1.Item(0, r).Value = 1 Then
                            B += 1
                        End If
                        r += 1
                    End While
                    ProgressBar1.Value = 0
                    ProgressBar1.Maximum = B
                    r = 0
                    B = 0
                    While r <= DataGridView1.RowCount - 1
                        If DataGridView1.Item(0, r).Value = 1 Then
                            I.Clave = TextBox5.Text + Modelo.Codigo + DataGridView1.Item(3, r).Value + DataGridView1.Item(4, r).Value + TextBox2.Text
                            If I.ChecaClaveRepetida(I.Clave) = False Then
                                Desc = TextBox1.Text
                                If CheckBox12.Checked Then Desc += " " + Modelo.Nombre
                                If CheckBox11.Checked Then Desc += " " + DataGridView1.Item(1, r).Value
                                If CheckBox10.Checked Then Desc += " " + DataGridView1.Item(2, r).Value
                                I.Guardar(Desc, 0, IdsTC1.Valor(ComboBox1.SelectedIndex), CDbl(TextBox3.Text), IdsTC2.Valor(ComboBox2.SelectedIndex), IdsClas.Valor(ComboBox3.SelectedIndex), Desc.Trim, I.Clave.Trim, CDbl(TextBox4.Text), 0, 2, "", IdClas2, IdClas3, ManejaSeries, Inventariable, CDbl(TextBox14.Text), 0, "", TextBox17.Text, PrecioNeto, TextBox8.Text, 0, 0, CDbl(TextBox18.Text), CDbl(TextBox20.Text), CDbl(TextBox19.Text), 0, 0, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIvaRetenido.Text), "", 0, 0, 0, Rest, "", "06 PIEZA", TextBox23.Text, TextBox24.Text, 0, 0, "", "")
                                I.AgregaPreciosFromTemp(I.ID)
                                ProgressBar1.Value += 1
                                Application.DoEvents()
                                If checkImprimir.Checked Then
                                    'Dim copias As Integer = Integer.Parse(InputBox("indique el numero de copias a imprimir", GlobalNombreApp, "1"))
                                    Dim precio As Double = DataGridView2.Rows(0).Cells(2).Value
                                    Dim frmImp As New frmInventarioImpresionCodigo(I.Clave, I.ID, precio, CInt(TextBox11.Text), Impresora)
                                End If
                            Else
                                B += 1
                            End If
                        End If
                        r += 1
                    End While
                    If B = 0 Then
                        MsgBox("Artículos generados", MsgBoxStyle.OkOnly, GlobalNombreApp)
                    Else
                        MsgBox("Artículos generados. " + B.ToString + " artículos no se pudieron agregar debido a códigos repetidos.", MsgBoxStyle.Information, GlobalNombreApp)
                    End If
                End If
            Else
                Button1.Enabled = True
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        Finally
            Button1.Enabled = True
        End Try
        
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Item(0, e.RowIndex).Value = 1 Then
            DataGridView1.Item(0, e.RowIndex).Value = 0
        Else
            DataGridView1.Item(0, e.RowIndex).Value = 1
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim fbc As New frmBuscadorCatalogosSat(6)
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox23.Text = fbc.Clave
        End If
        fbc.Dispose()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim fbc As New frmBuscadorCatalogosSat(7)
        If fbc.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox24.Text = fbc.Clave
        End If
        fbc.Dispose()
    End Sub

   
End Class