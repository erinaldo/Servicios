Public Class frmInventarioSeries
    Dim Tabla As New Data.DataTable
    Dim ConsultaOn As Boolean = True
    Dim IdInventario As Integer
    Dim IdSerie As Integer
    Dim IdCompra As Integer
    Dim IdVenta As Integer
    Dim IdRemisionC As Integer
    Dim idMovimiento As Integer
    Dim CantidadSeries As Integer
    Dim CantidadAgregada As Integer
    Dim Fecha As Date
    Public Sub New(ByVal pIdInventario As Integer, ByVal pIdCompra As Integer, ByVal pIdVenta As Integer, ByVal pCantidadSeries As Integer, ByVal pFecha As Date, ByVal pIdMovimiento As Integer, ByVal pIdRemisionC As Integer)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdInventario = pIdInventario
        IdCompra = pIdCompra
        IdVenta = pIdVenta
        idMovimiento = pIdMovimiento
        CantidadSeries = pCantidadSeries
        IdRemisionC = pIdRemisionC
        Fecha = DateAdd(DateInterval.Year, 1, pFecha)
    End Sub
    Private Function FormaCadenaFormato(ByVal LongitudCadena As Integer) As String
        Dim C As Integer = 0
        Dim CadenaFormato As String = ""
        While C < LongitudCadena
            CadenaFormato = CadenaFormato + "0"
            C += 1
        End While
        Return CadenaFormato
    End Function
    Private Sub frmInventarioSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim I As String = ""
        Tabla.Columns.Add("Series", I.GetType)
        DateTimePicker2.Value = Fecha
        Nuevo()

    End Sub
   
    Private Sub TextBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        TextBox2.SelectAll()
    End Sub

    Private Sub TextBox2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.GotFocus
        TextBox2.SelectAll()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If ConsultaOn Then
            CreaTabla()
        End If
    End Sub
    Private Sub Nuevo()
        ConsultaOn = False
        TextBox2.Text = "1"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        RadioButton3.Checked = True
        DGNuevo.DataSource = Nothing
        ConsultaOn = True
        CuantasVan()
        Consulta()
    End Sub
    Private Sub CreaTabla()
        Try
            If IsNumeric(TextBox2.Text) Then
                If CInt(TextBox2.Text) < 10000 Then
                    Dim ParteNumericaOk As Boolean = True
                    If RadioButton1.Checked Then
                        If Not IsNumeric(TextBox3.Text) Then ParteNumericaOk = False
                    End If
                    If RadioButton2.Checked Then
                        If Not IsNumeric(TextBox4.Text) Then ParteNumericaOk = False
                    End If
                    If RadioButton3.Checked Then
                        If Not IsNumeric(TextBox5.Text) Then ParteNumericaOk = False
                    End If
                    If ParteNumericaOk Then
                        Dim Veces As Integer
                        Dim Cont As Integer = 0
                        Dim Serie As Integer
                        If RadioButton1.Checked Then Serie = CInt(TextBox3.Text)
                        If RadioButton2.Checked Then Serie = CInt(TextBox4.Text)
                        If RadioButton3.Checked Then Serie = CInt(TextBox5.Text)
                        Veces = CInt(TextBox2.Text)
                        Tabla.Rows.Clear()
                        If Veces > 1 Then
                            While Cont < Veces
                                If RadioButton1.Checked Then Tabla.Rows.Add(Format((Serie + Cont), FormaCadenaFormato(TextBox3.Text.Length)) + TextBox4.Text + TextBox5.Text)
                                If RadioButton2.Checked Then Tabla.Rows.Add(TextBox3.Text + Format((Serie + Cont), FormaCadenaFormato(TextBox4.Text.Length)) + TextBox5.Text)
                                If RadioButton3.Checked Then Tabla.Rows.Add(TextBox3.Text + TextBox4.Text + Format((Serie + Cont), FormaCadenaFormato(TextBox5.Text.Length)))
                                Cont += 1
                            End While
                        End If
                        DGNuevo.DataSource = Tabla.DefaultView
                        DGNuevo.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    Else
                        PopUp("La parte numérica de la serie debe ser una cantidad numérica.", 90)
                    End If
                Else
                    PopUp("Debe indicar un número menor a diez mil.", 90)
                End If
            Else
                PopUp("El número de veces debe ser una cantidad numérica.", 90)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Alta()
        Try
            Dim MsgError As String = ""
            Dim InS As New dbInventarioSeries(MySqlcon)
            If IsNumeric(TextBox2.Text) = False Then TextBox2.Text = "1"
            If CInt(TextBox2.Text) > 10000 Then MsgError += "Debe indicar un número menor a diez mil."
            Dim Veces As Integer
            Veces = DGNuevo.Rows.Count
            If Veces > CantidadSeries Or CantidadAgregada = CantidadSeries Then MsgError += " No puede agregar mas de " + CantidadSeries.ToString + "."
            'If IsNumeric(TextBox2.Text) Then
            If MsgError = "" Then
                If Veces > 1 Then
                    Dim ParteNumericaOk As Boolean = True
                    If RadioButton1.Checked Then
                        If Not IsNumeric(TextBox3.Text) Then ParteNumericaOk = False
                    End If
                    If RadioButton2.Checked Then
                        If Not IsNumeric(TextBox4.Text) Then ParteNumericaOk = False
                    End If
                    If RadioButton3.Checked Then
                        If Not IsNumeric(TextBox5.Text) Then ParteNumericaOk = False
                    End If
                    If ParteNumericaOk Then

                        Dim Cont As Integer = 0
                        Dim NoGuardadas As String = "Series no guardadas por estar repetidas:"
                        'If Veces > 1 Then
                        'Tabla.Rows.Clear()
                        While Cont < Veces
                            If InS.ChecaNoSerieRepetido(DGNuevo.Item(0, Cont).Value, IdInventario) = False Then
                                InS.Guardar(IdInventario, DGNuevo.Item(0, Cont).Value, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCompra, IdVenta, 0, 0, idMovimiento, IdRemisionC, 0)
                            Else
                                NoGuardadas += vbCrLf + DGNuevo.Item(0, Cont).Value.ToString
                            End If
                            Cont += 1
                        End While
                        If NoGuardadas <> "Series no guardadas por estar repetidas:" Then
                            MsgBox(NoGuardadas, MsgBoxStyle.Information, GlobalNombreApp)
                        Else
                            PopUp("Guardado", 90)
                        End If
                        Nuevo()
                        'Else
                        'End If
                    Else
                        PopUp("La parte numérica de la serie debe ser un valor numérico.", 90)
                    End If
                Else
                    If InS.ChecaNoSerieRepetido(TextBox3.Text + TextBox4.Text + TextBox5.Text, IdInventario) = False Then
                        InS.Guardar(IdInventario, TextBox3.Text + TextBox4.Text + TextBox5.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "yyyy/MM/dd"), IdCompra, IdVenta, 0, 0, idMovimiento, IdRemisionC, 0)
                        PopUp("Guardado", 90)
                        Nuevo()
                    Else
                        MsgBox("Ya esta registrado ese número de serie.", MsgBoxStyle.Critical, GlobalNombreApp)
                    End If
                End If
            Else
                MsgBox("Debe indicar un número menor a diez mil.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            'Else
            'PopUp("La cantidad de veces debe ser un valor numérico.", 90)
            'End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Alta()
        TextBox3.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If MsgBox("¿Desea remover el elemento seleccionado?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
            DGNuevo.Rows.Remove(DGNuevo.Rows(DGNuevo.CurrentCell.RowIndex))
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            If MsgBox("¿Desea eliminar la serie seleccionada?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                IdSerie = DGConsulta.Item(0, DGConsulta.CurrentCell.RowIndex).Value
                S.Eliminar(IdSerie)
                Consulta()
                PopUp("Registro Eliminado", 90)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este número de serie debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim PrimerCeldaRow As Integer = -1
                Dim S As New dbInventarioSeries(MySqlcon)
                If DGConsulta.RowCount > 0 Then PrimerCeldaRow = DGConsulta.FirstDisplayedCell.RowIndex
                DGConsulta.DataSource = S.Consulta(IdInventario, TextBox3.Text + TextBox4.Text + TextBox5.Text, IdCompra, IdVenta, , , idMovimiento, IdRemisionC)
                DGConsulta.Columns(0).Visible = False
                DGConsulta.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGConsulta.Columns(1).HeaderText = "Series Agregadas"
                DGConsulta.Columns(2).HeaderText = "Garantía"
                DGConsulta.Columns(3).HeaderText = "Caducidad"
                DGConsulta.Columns(4).Visible = False
                DGConsulta.Columns(5).Visible = False
                DGConsulta.Columns(6).Visible = False
                DGConsulta.Columns(7).Visible = False
                DGConsulta.Columns(8).Visible = False
                DGConsulta.Columns(9).Visible = False
                If DGConsulta.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGConsulta.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Consulta()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Label1.Text = TextBox3.Text + TextBox4.Text + TextBox5.Text
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Label1.Text = TextBox3.Text + TextBox4.Text + TextBox5.Text
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        Label1.Text = TextBox3.Text + TextBox4.Text + TextBox5.Text
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Nuevo()
        TextBox3.Focus()
    End Sub


    Private Sub DGNuevo_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGNuevo.CellContentClick

    End Sub

    Private Sub DGNuevo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGNuevo.KeyDown
        If e.KeyCode = Keys.Delete Then
            If MsgBox("¿Desea remover el elemento seleccionado?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                DGNuevo.Rows.Remove(DGNuevo.Rows(DGNuevo.CurrentCell.RowIndex))
            End If
        End If
    End Sub
    Private Sub CuantasVan()
        Dim S As New dbInventarioSeries(MySqlcon)
        If IdCompra <> 0 Then
            CantidadAgregada = S.CantidadDeSeriesAgregadasaCompra(IdCompra, IdInventario)
        End If
        If idMovimiento <> 0 Then
            CantidadAgregada = S.CantidadDeSeriesAgregadasaMovimiento(idMovimiento, IdInventario)
        End If
        If IdRemisionC <> 0 Then
            CantidadAgregada = S.CantidadDeSeriesAgregadasaRemisionCompra(IdRemisionC, IdInventario)
        End If
        Label6.Text = CantidadAgregada.ToString + " de " + CantidadSeries.ToString + " series agregadas."
    End Sub
End Class