Public Class frmSucursalesFolios
    Dim IdFolio As Integer
    Dim ConsultaOn As Boolean
    Dim IdSucursal As Integer
    Dim IdsCertificados As New elemento
    Public Sub New(ByVal pIdSucursal As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdSucursal = pIdSucursal
    End Sub
    Private Sub Nuevo()
        Try
            ConsultaOn = False
            'TextBox6.Text = "0"
            TextBox1.Text = ""
            TextBox2.Text = "0"
            TextBox3.Text = ""
            TextBox4.Text = ""
            'TextBox5.Text = ""
            TextBox7.Text = "0"
            TextBox8.Text = ""
            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedIndex = 0
            'CheckBox2.Checked = False
            RadioButton3.Checked = True
            CheckBox1.Checked = False
            ComboBox1.Enabled = False
            TextBox7.BackColor = Color.FromKnownColor(KnownColor.Window)
            TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
            Button1.Text = "Guardar"
            Button2.Enabled = False
            RadioButton2.Checked = True
            ComboBox1.Enabled = True
            'RadioButton1.Enabled = True
            'RadioButton2.Enabled = True
            'RadioButton3.Enabled = False
            ConsultaOn = True
            Consulta()
            TextBox1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbSucursalesFolios(MySqlcon)
                DataGridView1.DataSource = P.Consulta(IdSucursal)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).HeaderText = "Documento"
                DataGridView1.Columns(2).HeaderText = "Serie"
                DataGridView1.Columns(3).HeaderText = "F. Inicial"
                DataGridView1.Columns(4).HeaderText = "F. Final"
                DataGridView1.Columns(5).HeaderText = "Activo"
                DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DataGridView1.Columns(5).Width = 40
                DataGridView1.Columns(3).Width = 70
                DataGridView1.Columns(4).Width = 70
                DataGridView1.Columns(2).Width = 80
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
            Dim P As New dbSucursalesFolios(MySqlcon)
            Dim NoErrores As Boolean = True
            Dim MensajeError As String = ""
            If IsNumeric(TextBox7.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El folio debe ser un valor numérico."
                TextBox7.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If IsNumeric(TextBox2.Text) = False Then
                NoErrores = False
                MensajeError += vbCrLf + " El folio debe ser un valor numérico."
                TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            End If
            If NoErrores Then
                Dim idcer As Integer
                Dim eselec As Byte = 0
                Dim acti As Byte = 0
                If ComboBox1.SelectedIndex = 0 Then
                    idcer = 0
                Else
                    idcer = IdsCertificados.Valor(ComboBox1.SelectedIndex)
                End If
                If CheckBox1.Checked Then acti = 1
                If RadioButton3.Checked Then eselec = 0
                If RadioButton1.Checked Then eselec = 1
                If RadioButton2.Checked Then eselec = 2
                If Button1.Text = "Guardar" Then
                    P.Guardar(TextBox7.Text, TextBox2.Text, Trim(TextBox1.Text), eselec, idcer, Trim(TextBox4.Text), Trim(TextBox8.Text), acti, ComboBox2.SelectedIndex + 1, IdSucursal, Trim(TextBox3.Text))
                    PopUp("Guardado", 90)
                    Nuevo()
                Else
                    If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        P.Modificar(IdFolio, TextBox7.Text, TextBox2.Text, Trim(TextBox1.Text), eselec, idcer, Trim(TextBox4.Text), Trim(TextBox8.Text), acti, ComboBox2.SelectedIndex + 1, IdSucursal, Trim(TextBox3.Text))
                        PopUp("Modificado", 90)
                        Nuevo()
                    End If
                End If
                'TextBox1.Focus()
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
                Dim P As New dbSucursalesFolios(MySqlcon)
                P.Eliminar(IdFolio)
                PopUp("Eliminado", 90)
                Nuevo()
                TextBox1.Focus()
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar este folio debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
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
            IdFolio = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value
            Dim P As New dbSucursalesFolios(IdFolio, MySqlcon)
            Button1.Text = "Modificar"
            Button2.Enabled = True
            ConsultaOn = False
            TextBox1.Text = P.Serie
            TextBox2.Text = P.FolioFinal
            TextBox7.Text = P.FolioInicial
            TextBox4.Text = P.NoAprobacion
            TextBox8.Text = P.YearAprobacion
            TextBox3.Text = P.Formato
            If P.Activo = 0 Then
                CheckBox1.Checked = False
            Else
                CheckBox1.Checked = True
            End If
            Select Case P.EsElectronica
                Case 0
                    RadioButton3.Checked = True
                    ComboBox1.SelectedIndex = 0
                    ComboBox1.Enabled = False
                Case 1
                    RadioButton1.Checked = True
                    ComboBox1.SelectedIndex = IdsCertificados.Busca(P.IdCertificado)
                Case 2
                    RadioButton2.Checked = True
                    ComboBox1.SelectedIndex = IdsCertificados.Busca(P.IdCertificado)
            End Select
            ComboBox2.SelectedIndex = P.TipoDocumento - 1
            If ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 2 Or ComboBox2.SelectedIndex = 4 Then
                ComboBox1.Enabled = True
                'RadioButton1.Enabled = True
                'RadioButton2.Enabled = True
                'RadioButton3.Enabled = False
            Else
                'RadioButton3.Enabled = True
                'RadioButton3.Checked = True
                'RadioButton1.Enabled = False
                'RadioButton2.Enabled = False
            End If
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
        ComboBox2.Items.Add("Factura")
        ComboBox2.Items.Add("Nota de Crédito")
        ComboBox2.Items.Add("Devolución")
        ComboBox2.Items.Add("Remisión")
        ComboBox2.Items.Add("Notas de Cargo")
        ComboBox2.Items.Add("Cotizaciones")
        ComboBox2.Items.Add("Pedidos")
        ComboBox2.Items.Add("Compras - Cotizaciones")
        ComboBox2.Items.Add("Compras - Pedidos")
        ComboBox2.Items.Add("Movimientos de Caja")
        ComboBox2.Items.Add("Apartados")
        ComboBox2.Items.Add("Nominas")
        ComboBox2.Items.Add("Compras - Factura")
        ComboBox2.Items.Add("Compras - Remisiones")
        ComboBox2.Items.Add("Compras - Devolución")
        ComboBox2.Items.Add("Compras - Notas de crédito")
        ComboBox2.Items.Add("Compras - Notas de cargo")
        ComboBox2.Items.Add("Fertilizantes - Pedidos")
        ComboBox2.Items.Add("Fertilizantes - Movimientos Salida")
        ComboBox2.Items.Add("Fertilizantes - Movimientos Envío")
        ComboBox2.Items.Add("Fertilizantes - Movimientos Traspaso")
        ComboBox2.Items.Add("Fertilizantes - Movimientos Devolución")
        ComboBox2.Items.Add("Semillas - Liquidación")
        ComboBox2.Items.Add("Semillas - Comprobante")
        ComboBox2.Items.Add("Gastos")
        ComboBox2.Items.Add("Remisiones por Surtir")
        ComboBox2.Items.Add("Inventario Pedidos")
        'ComboBox2.Items.Add("Movimientos Inventario")
        LlenaCombos("tblsucursalescertificados", ComboBox1, "noserie", "nombrem", "idcertificado", IdsCertificados, "idsucursal=" + IdSucursal.ToString, "Seleccionar Certificado")
        Nuevo()
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Consulta()
    End Sub

    

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then
            ComboBox1.SelectedIndex = 0
            ComboBox1.Enabled = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            'ComboBox1.SelectedIndex = 0
            ComboBox1.Enabled = True
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            'ComboBox1.SelectedIndex = 0
            ComboBox1.Enabled = True
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        'If ConsultaOn Then
        '    If ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 2 Or ComboBox2.SelectedIndex = 4 Then
        '        RadioButton2.Checked = True
        '        ComboBox1.Enabled = True
        '        RadioButton1.Enabled = True
        '        RadioButton2.Enabled = True
        '        RadioButton3.Enabled = False
        '    Else
        '        RadioButton3.Enabled = True
        '        RadioButton3.Checked = True
        '        RadioButton1.Enabled = False
        '        RadioButton2.Enabled = False
        '    End If
        'End If
        If ConsultaOn Then
            If ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 2 Or ComboBox2.SelectedIndex = 4 Then
                RadioButton2.Checked = True
            Else
                RadioButton3.Checked = True
            End If
        End If
    End Sub
End Class