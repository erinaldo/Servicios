Public Class frmClientesImpuestos
    Public IdImpuesto As Integer
    Dim ConsultaOn As Boolean
    Dim IdCliente As Integer
    Public Sub New(ByVal pIdCliente As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdCliente = pIdCliente
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            TextBox1.Text = ""
            TextBox2.Text = "0"
            ComboBox1.SelectedIndex = 0
            TextBox1.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
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
                Dim P As New dbClientesImpuestos(MySqlcon)
                DataGridView1.DataSource = P.Consulta(IdCliente)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Nombre"
                DataGridView1.Columns(2).HeaderText = "Tasa"
                DataGridView1.Columns(3).HeaderText = "Tipo"
                DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
            Dim P As New dbClientesImpuestos(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If TextBox1.Text = "" Then
                NoErrores = False
                MensajeError += vbCrLf + " Debe indicar una descripción al impuesto."
                TextBox1.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox2.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " La tasa debe ser un valor numérico."
                TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If NoErrores Then
                If Button1.Text = "Guardar" Then
                    P.Guardar(TextBox1.Text.Trim, ComboBox1.SelectedIndex, CDbl(TextBox2.Text), IdCliente)
                    PopUp("Guardado", 90)
                    Nuevo()
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                        P.Modificar(IdImpuesto, TextBox1.Text.Trim, ComboBox1.SelectedIndex, CDbl(TextBox2.Text))
                        PopUp("Modificado", 90)
                        Nuevo()
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
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) Then
                Dim P As New dbClientesImpuestos(MySqlcon)
                P.Eliminar(IdImpuesto)
                PopUp("Eliminado", 90)
                Nuevo()
                TextBox1.Focus()
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este impuesto debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            IdImpuesto = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbClientesImpuestos(IdImpuesto, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            TextBox1.Text = P.Nombre
            TextBox2.Text = P.Tasa
            ComboBox1.SelectedIndex = P.Tipo
            ConsultaOn = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            LlenaDatos()
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
            LlenaDatos()
        End If
    End Sub
    Private Sub frmClientesEquipos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ComboBox1.Items.Add("Traslado")
        ComboBox1.Items.Add("Retención")
        Nuevo()
    End Sub

End Class