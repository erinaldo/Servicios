Public Class frmClientesCuentas
    Dim IdCliente As Integer
    Dim accion As Boolean = True
    Dim idsTipos As New elemento
    Dim idTipo As Integer = -1
    Dim idCuenta As Integer
    Dim c As New dbClientesCuentas(MySqlcon)
    Public Sub New(ByVal pIdCliente As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdCliente = pIdCliente
        'DataGridView1.ReadOnly = True
        ''DataGridView1.DataSource = c.Consulta(IdCliente)
        'DataGridView1.RowHeadersVisible = False
        'DataGridView1.Columns(0).Visible = False
        'DataGridView1.Columns(1).HeaderText = "Cuenta"
        'DataGridView1.Columns(2).HeaderText = "num. Cuenta"
        'DataGridView1.Columns(3).HeaderText = "Clabe"
        'DataGridView1.Columns(4).HeaderText = "Banco"
        'DataGridView1.Columns(5).Visible = False
        LlenaCombos("tblbancoscatalogo", ComboBanco, "nombre", "nombret", "idbanco", idsTipos, "", "", "idbanco")
    End Sub

    Private Sub Nuevo()
        Try
            TextBox1.Text = ""
            txtNumCuenta.Text = ""
            txtClabe.Text = ""
            Button1.Text = "Guardar"
            Button2.Enabled = False
            ComboBanco.SelectedIndex = 0
            'DataGridView1.DataSource = c.Consulta(IdCliente)
            Consulta()
            'TextBox1.Text = ""
            ' LlenaCombos("tblclientescuentas", ComboCuentas, "cuenta", "nombret", "idcuenta", idsTipos, "idcliente=" + IdCliente.ToString)
            idTipo = -1
            TextBox1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Then
                MsgBox("Debe indicar una cuenta.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            Dim TC As New dbClientesCuentas(MySqlcon)

            If Button1.Text = "Guardar" Then
                'Dim i As Integer = ComboBanco.SelectedIndex
                TC.Guardar(TextBox1.Text, IdCliente, idsTipos.Valor(ComboBanco.SelectedIndex), txtClabe.Text, txtNumCuenta.Text)
                PopUp("Guardado", 90)
                Nuevo()
            Else
                If MsgBox("¿Desea guardar los cambios?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    TC.Modificar(idCuenta, TextBox1.Text, txtClabe.Text, idsTipos.Valor(ComboBanco.SelectedIndex), txtNumCuenta.Text)
                    PopUp("Modificado", 90)
                    Nuevo()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim TC As New dbClientesCuentas(MySqlcon)
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'idTipo = Integer.Parse(DataGridView1.Rows(ComboCuentas.SelectedIndex).Cells(0).Value)
                TC.Eliminar(Integer.Parse(DataGridView1.CurrentRow.Cells(0).Value))
                PopUp("Eliminado", 90)
                Nuevo()
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta medida debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Nuevo()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

    End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim cc As New dbClientesCuentas(MySqlcon)
    '    Dim dt As DataTable = cc.Consulta(IdCliente).ToTable
    '    If ComboCuentas.SelectedIndex >= 0 Then
    '        Dim x As Integer = ComboCuentas.SelectedIndex
    '        txtClabe.Text = Convert.ToString(dt.Rows(x).Item(3))
    '        txtNumCuenta.Text = Convert.ToString(dt.Rows(x).Item(2))
    '        ComboBanco.SelectedIndex = Convert.ToString(dt.Rows(x).Item(4))
    '        Button1.Text = "Modificar"
    '        Button2.Enabled = True
    '    End If
    'End Sub

    'Private Sub Cargar()
    '    Dim cc As New dbClientesCuentas(MySqlcon)
    '    Dim dt As DataTable = cc.Consulta(IdCliente).ToTable
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        ComboCuentas.Items.Add(Convert.ToString(dt.Rows(i).Item(1)))
    '    Next
    'End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub


    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        If DataGridView1.Rows.Count > 0 Then
            idCuenta = Integer.Parse(DataGridView1.CurrentRow.Cells(0).Value)
            TextBox1.Text = Convert.ToString(DataGridView1.CurrentRow.Cells(1).Value)
            txtNumCuenta.Text = Convert.ToString(DataGridView1.CurrentRow.Cells(2).Value)
            txtClabe.Text = Convert.ToString(DataGridView1.CurrentRow.Cells(3).Value)
            ComboBanco.SelectedIndex = idsTipos.Busca(Integer.Parse(DataGridView1.CurrentRow.Cells(5).Value))
            Button1.Text = "Modificar"
            Button2.Enabled = True
        End If
    End Sub

    Private Sub frmClientesCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Nuevo()
    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            'Dim P As New dbClientesCuentas(MySqlcon)
            DataGridView1.DataSource = c.Consulta(IdCliente)
            DataGridView1.ReadOnly = True
            DataGridView1.RowHeadersVisible = False
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Cuenta"
            DataGridView1.Columns(2).HeaderText = "No. Cuenta"
            DataGridView1.Columns(3).HeaderText = "Clabe"
            DataGridView1.Columns(4).HeaderText = "Banco"
            DataGridView1.Columns(5).Visible = False
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

End Class