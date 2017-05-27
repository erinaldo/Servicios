Public Class frmNominaMasDatos
    Public Modo As Byte
    Public OrigenRecurso As String
    Public Monto As Double
    Public Tipo As Byte
    Public IdDetalle As Integer
    Dim IdDetalleH As Integer
    Public Estado As Byte
    Public ValorMercado As Double
    Public PrecioOtorgarse As Double
    Public IdNomina As Integer
    Public Sub New(pEstado)

        ' This call is required by the designer.
        InitializeComponent()
        Estado = pEstado
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub frmNominaMasDatos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' {"Ingresos propios", "Ingresos Federales", "Ingresos Mixtos"}
        'Private clavesRecursos() As String = {"IP", "IF", "IM"}
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox5.Items.Add("01 Dobles")
        ComboBox5.Items.Add("02 Triples")
        ComboBox5.Items.Add("03 Simples")
        comboRecursos.Items.Add("No Aplica")
        comboRecursos.Items.Add("IP Ingresos Propios")
        comboRecursos.Items.Add("IF Ingresos Federales")
        comboRecursos.Items.Add("IM Ingresos Mixtos")
        txtMonto.Text = Monto.ToString
        If OrigenRecurso = "NA" Then comboRecursos.SelectedIndex = 0
        If OrigenRecurso = "IP" Then comboRecursos.SelectedIndex = 1
        If OrigenRecurso = "IF" Then comboRecursos.SelectedIndex = 2
        If OrigenRecurso = "IM" Then comboRecursos.SelectedIndex = 3
        If Tipo = 1 Then
            Panel3.Left = 2
            Panel3.Top = 2
            Panel1.Visible = False
            Panel2.Visible = False
            GroupBox4.Visible = False
            GroupBox5.Visible = False
            Me.Width = 220
            Me.Height = 220
            btnGuardar.Left = 11
            btnGuardar.Top = 130
            btnCancelar.Top = 130
            btnCancelar.Left = 105
            txtMercado.Text = ValorMercado.ToString
            txtValor.Text = PrecioOtorgarse.ToString
            Me.Show()
            txtMercado.Focus()
        End If
        If Tipo = 0 Then
            Panel1.Left = 2
            Panel1.Top = 2
            Panel2.Visible = False
            Panel3.Visible = False
            GroupBox4.Visible = False
            GroupBox5.Visible = False
            Me.Width = 350
            Me.Height = 200
            btnGuardar.Left = 11
            btnGuardar.Top = 110
            btnCancelar.Top = 110
            btnCancelar.Left = 105
            Me.Show()
            comboRecursos.Focus()
        End If
        If Tipo = 2 Then
            Panel2.Left = 2
            Panel2.Top = 2
            Panel1.Visible = False
            Panel3.Visible = False
            GroupBox4.Visible = False
            GroupBox5.Visible = False
            Me.Width = 510
            Me.Height = 240
            btnGuardar.Left = 11
            btnGuardar.Top = 150
            btnCancelar.Top = 150
            btnCancelar.Left = 105
            txtMercado.Text = ValorMercado.ToString
            txtValor.Text = PrecioOtorgarse.ToString
            Me.Show()
            ConsultaDetallesHorasExtra()
            ComboBox5.SelectedIndex = 0
            ComboBox5.Focus()
        End If
        If Tipo = 3 Then
            GroupBox5.Left = 2
            GroupBox5.Top = 2
            Panel1.Visible = False
            Panel3.Visible = False
            GroupBox4.Visible = False
            Panel2.Visible = False
            Me.Width = 310
            Me.Height = 266
            btnGuardar.Left = 11
            btnGuardar.Top = 176
            btnCancelar.Top = 176
            btnCancelar.Left = 105
            Me.Show()
            txtPagado.Focus()
        End If
        If Tipo = 4 Then
            GroupBox4.Left = 2
            GroupBox4.Top = 2
            Panel1.Visible = False
            Panel3.Visible = False
            GroupBox5.Visible = False
            Panel2.Visible = False
            Me.Width = 310
            Me.Height = 266
            btnGuardar.Left = 11
            btnGuardar.Top = 176
            btnCancelar.Top = 176
            btnCancelar.Left = 105
            txtParcialidad.Visible = False
            Label11.Visible = False
            Label12.Visible = False
            txtDiario.Visible = False
            Me.Show()
            txtExhibicion.Focus()
        End If
        If Tipo = 5 Then
            GroupBox4.Left = 2
            GroupBox4.Top = 2
            Panel1.Visible = False
            Panel3.Visible = False
            GroupBox5.Visible = False
            Panel2.Visible = False
            Me.Width = 310
            Me.Height = 266
            btnGuardar.Left = 11
            btnGuardar.Top = 176
            btnCancelar.Top = 176
            btnCancelar.Left = 105
            Label10.Visible = False
            txtExhibicion.Visible = False
            Me.Show()
            txtParcialidad.Focus()
        End If
        If Tipo >= 3 Then
            Dim Nt As New dbNominaTRabajador(IdNomina, MySqlcon)
            txtExhibicion.Text = Nt.JtotalUnaExhibicion.ToString
            txtParcialidad.Text = Nt.JtotalParcialidad.ToString
            txtDiario.Text = Nt.JmontoDiario.ToString
            txtAcumulable2.Text = Nt.Jacumulable.ToString
            txtNoAcumulable2.Text = Nt.JnoAcumulable.ToString
            txtPagado.Text = Nt.StotalPagado.ToString
            txtServicio.Text = Nt.SanhosServicio.ToString
            txtSueldo.Text = Nt.SsueldoMensual.ToString
            txtAcumulable1.Text = Nt.Sacumulable.ToString
            txtNoAcumulable1.Text = Nt.SnoAcumulable.ToString
        End If
        If Estado = Estados.Guardada Or Estado = Estados.Cancelada Then
            btnGuardar.Enabled = False
            Button23.Enabled = False
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        BotonGuardar()
    End Sub
    Private Sub BotonGuardar()
        Try
            If Tipo = 0 Then
                Monto = CDbl(txtMonto.Text)
                If txtMonto.Visible = True And Monto = 0 Then
                    MsgBox("Debe indicar el monto del recurso.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
            End If
            If Tipo = 1 Then
                PrecioOtorgarse = CDbl(txtValor.Text)
                ValorMercado = CDbl(txtMercado.Text)
            End If
            If Tipo >= 3 Then
                Dim Nt As New dbNominaTRabajador(IdNomina, MySqlcon)
                If Nt.HayHatos Then
                    Nt.modificar(Nt.id, IdNomina, CDbl(txtPagado.Text), CInt(txtServicio.Text), CDbl(txtSueldo.Text), CDbl(txtAcumulable2.Text), CDbl(txtNoAcumulable2.Text), CDbl(txtExhibicion.Text), CDbl(txtParcialidad.Text), CDbl(txtDiario.Text), CDbl(txtAcumulable1.Text), CDbl(txtAcumulable1.Text))
                Else
                    Nt.agregar(IdNomina, CDbl(txtPagado.Text), CInt(txtServicio.Text), CDbl(txtSueldo.Text), CDbl(txtAcumulable2.Text), CDbl(txtNoAcumulable2.Text), CDbl(txtExhibicion.Text), CDbl(txtParcialidad.Text), CDbl(txtDiario.Text), CDbl(txtAcumulable1.Text), CDbl(txtAcumulable1.Text))
                End If
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
        End Try
    End Sub

    Private Sub comboRecursos_KeyDown(sender As Object, e As KeyEventArgs) Handles comboRecursos.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMonto.Focus()
        End If
    End Sub
    Private Sub comboRecursos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboRecursos.SelectedIndexChanged
        If comboRecursos.SelectedIndex = 0 Then OrigenRecurso = "NA"
        If comboRecursos.SelectedIndex = 1 Then OrigenRecurso = "IP"
        If comboRecursos.SelectedIndex = 2 Then OrigenRecurso = "IF"
        If comboRecursos.SelectedIndex = 3 Then
            OrigenRecurso = "IM"
            txtMonto.Visible = True
        Else
            txtMonto.Visible = False
        End If
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        AgregaHorasExtra()
    End Sub
    Private Sub AgregaHorasExtra()
        Try
            Dim CD As New dbNoominaHorasExtra(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            If IsNumeric(TextBox15.Text) = False Or IsNumeric(TextBox13.Text) = False Or IsNumeric(TextBox17.Text) = False Then
                MsgError += vbCrLf + "Las horas, los dias y el importe deben ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox15.Text) = 0 Or CDbl(TextBox13.Text) = 0 Or CDbl(TextBox17.Text) = 0 Then
                    MsgError += vbCrLf + "Las cantidades deben ser diferente de cero."
                    HayError = True
                End If
            End If
            If HayError = False Then
                If Button23.Text = "Agregar Horas Extra" Then
                    CD.GuardarC(IdDetalle, CInt(TextBox15.Text), ComboBox5.Text, CInt(TextBox13.Text), CDbl(TextBox17.Text))
                    ConsultaDetallesHorasExtra()
                    NuevoHorasExtra()
                Else
                    CD.ModificarC(IdDetalleH, CInt(TextBox15.Text), ComboBox5.Text, CInt(TextBox13.Text), CDbl(TextBox17.Text))
                    ConsultaDetallesHorasExtra()
                    NuevoHorasExtra()
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub NuevoHorasExtra()
        ComboBox5.SelectedIndex = 0
        TextBox15.Text = "0"
        TextBox13.Text = "0"
        TextBox17.Text = "0"
        Button23.Text = "Agregar Horas Extra"
        Button22.Enabled = False
        ComboBox5.Focus()
    End Sub
    Private Sub ConsultaDetallesHorasExtra()
        Try
            Dim CD As New dbNoominaHorasExtra(MySqlcon)
            DGHorasextra.DataSource = CD.ConsultaC(IdDetalle)
            DGHorasextra.Columns(0).Visible = False
            DGHorasextra.Columns(1).HeaderText = "Tipo"
            DGHorasextra.Columns(2).HeaderText = "Dias"
            DGHorasextra.Columns(3).HeaderText = "Horas"
            DGHorasextra.Columns(4).HeaderText = "Importe"
            DGHorasextra.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DGHorasextra.RowCount > DGHorasextra.DisplayedRowCount(False) Then DGHorasextra.FirstDisplayedScrollingRowIndex = DGHorasextra.RowCount - DGHorasextra.DisplayedRowCount(False)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosDetallesHorasExtra()
        Try
            IdDetalleH = DGHorasextra.Item(0, DGHorasextra.CurrentCell.RowIndex).Value
            Dim CD As New dbNoominaHorasExtra(IdDetalleH, 1, MySqlcon)
            'ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            ComboBox5.Text = CD.TipoHoras
            TextBox15.Text = CD.Dias.ToString
            TextBox13.Text = CD.HorasExtra.ToString
            TextBox17.Text = CD.ImportePagado.ToString
            Button23.Text = "Modificar Hora Extra"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
                Button22.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        NuevoHorasExtra()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Nominas.NominaAlta, PermisosN.Secciones.Nomina) = True Then
                If MsgBox("¿Desea eliminar esta hora extra de la nomina?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbNoominaHorasExtra(MySqlcon)
                    CD.EliminarC(IdDetalleH)
                    ConsultaDetallesHorasExtra()
                    NuevoHorasExtra()
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGHorasextra_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGHorasextra.CellClick
        LlenaDatosDetallesHorasExtra()
    End Sub

    Private Sub ComboBox5_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ComboBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox15.Focus()
        End If
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged

    End Sub

    Private Sub TextBox15_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox15.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox13.Focus()
        End If
    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged

    End Sub

    Private Sub TextBox13_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox13.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox17.Focus()
        End If
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged

    End Sub

    Private Sub TextBox17_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox17.KeyDown
        If e.KeyCode = Keys.Enter Then
            AgregaHorasExtra()
        End If
    End Sub

    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged

    End Sub

    Private Sub txtMercado_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMercado.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtValor.Focus()
        End If
    End Sub

    Private Sub txtMercado_TextChanged(sender As Object, e As EventArgs) Handles txtMercado.TextChanged

    End Sub

    Private Sub txtValor_KeyDown(sender As Object, e As KeyEventArgs) Handles txtValor.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

    Private Sub txtValor_TextChanged(sender As Object, e As EventArgs) Handles txtValor.TextChanged

    End Sub

    Private Sub txtMonto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMonto.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

    Private Sub txtMonto_TextChanged(sender As Object, e As EventArgs) Handles txtMonto.TextChanged

    End Sub

    Private Sub txtExhibicion_KeyDown(sender As Object, e As KeyEventArgs) Handles txtExhibicion.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtAcumulable2.Focus()
        End If
    End Sub

    Private Sub txtExhibicion_TextChanged(sender As Object, e As EventArgs) Handles txtExhibicion.TextChanged

    End Sub

    Private Sub txtAcumulable2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAcumulable2.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

    Private Sub txtAcumulable2_TextChanged(sender As Object, e As EventArgs) Handles txtAcumulable2.TextChanged

    End Sub

    Private Sub txtNoAcumulable2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNoAcumulable2.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

    Private Sub txtNoAcumulable2_TextChanged(sender As Object, e As EventArgs) Handles txtNoAcumulable2.TextChanged

    End Sub

    Private Sub txtParcialidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtParcialidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDiario.Focus()
        End If
    End Sub

    Private Sub txtParcialidad_TextChanged(sender As Object, e As EventArgs) Handles txtParcialidad.TextChanged

    End Sub

    Private Sub txtDiario_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDiario.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtAcumulable2.Focus()
        End If
    End Sub

    Private Sub txtDiario_TextChanged(sender As Object, e As EventArgs) Handles txtDiario.TextChanged

    End Sub

    Private Sub txtPagado_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPagado.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtServicio.Focus()
        End If
    End Sub

    Private Sub txtPagado_TextChanged(sender As Object, e As EventArgs) Handles txtPagado.TextChanged

    End Sub

    Private Sub txtServicio_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtServicio.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtSueldo.Focus()
        End If
    End Sub

    Private Sub txtServicio_TextChanged(sender As Object, e As EventArgs) Handles txtServicio.TextChanged

    End Sub

    Private Sub txtSueldo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSueldo.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtAcumulable1.Focus()
        End If
    End Sub

    Private Sub txtSueldo_TextChanged(sender As Object, e As EventArgs) Handles txtSueldo.TextChanged

    End Sub

    Private Sub txtAcumulable1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAcumulable1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNoAcumulable1.Focus()
        End If
    End Sub

    Private Sub txtAcumulable1_TextChanged(sender As Object, e As EventArgs) Handles txtAcumulable1.TextChanged

    End Sub

    Private Sub txtNoAcumulable1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNoAcumulable1.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonGuardar()
        End If
    End Sub

    Private Sub txtNoAcumulable1_TextChanged(sender As Object, e As EventArgs) Handles txtNoAcumulable1.TextChanged

    End Sub
End Class