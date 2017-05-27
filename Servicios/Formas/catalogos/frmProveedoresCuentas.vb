Public Class frmProveedoresCuentas
    Private IdProv, idBanco, idProvCuenta As Integer
    Dim idsTipos As New elemento
    Dim idTipo As Integer = -1
    Dim accion As Boolean = True
    Dim cuenta, clabe As String
    Dim pc As New dbProveedoresCuentas(MySqlcon)
    Public Sub New(ByVal pIdProv As Integer)
        InitializeComponent()
        IdProv = pIdProv
        LlenaCombos("tblbancoscatalogo", ComboBancos, "nombre", "nombret", "idbanco", idsTipos, "", "", "idbanco")
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            Dim TC As New dbProveedoresCuentas(MySqlcon)
            If MsgBox("¿Desea eliminar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                TC.Eliminar(Integer.Parse(DataGridView1.CurrentRow.Cells(0).Value))
                Nuevo()
                PopUp("Eliminado", 90)
            End If
        Catch exm As MySql.Data.MySqlClient.MySqlException
            If exm.ErrorCode = -2147467259 Then
                MsgBox("No se puede eliminar esta cuenta debido a que se encuentra en uso.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                MsgBox(exm.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Nuevo()
        Try
            ComboBancos.SelectedIndex = 0
            btnGuardar.Text = "Guardar"
            txtNumcuenta.Text = ""
            txtClabe.Text = ""
            btnEliminar.Enabled = False
            Consulta()

            accion = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub



    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If btnGuardar.Text = "Guardar" Then
            Guardar()
        ElseIf MsgBox("¿Desea modificar este registro?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar()
        End If

    End Sub

    Private Sub Guardar()
        If txtNumcuenta.Text = "" And txtClabe.Text = "" Then
            MsgBox("Debe indicar una cuenta o una CLABE.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Try
            clabe = txtClabe.Text
            cuenta = txtNumcuenta.Text
            pc.Guardar(IdProv, cuenta, clabe, idsTipos.Valor(ComboBancos.SelectedIndex).ToString)
            PopUp("Guardado", 90)
            Nuevo()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Modificar()
        Try
            If txtNumcuenta.Text = "" And txtClabe.Text = "" Then
                MsgBox("Debe indicar una cuenta o una CLABE.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            clabe = txtClabe.Text
            cuenta = txtNumcuenta.Text
            idBanco = idsTipos.Valor(ComboBancos.SelectedIndex)
            pc.Modificar(idProvCuenta, cuenta, clabe, idsTipos.Valor(ComboBancos.SelectedIndex).ToString)
            Nuevo()
            PopUp("Modificado", 90)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Nuevo()
    End Sub


    Private Sub frmProveedoresCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Dim P As New dbClientes(MySqlcon)
            DataGridView1.DataSource = pc.Consulta(IdProv)
            DataGridView1.ReadOnly = True
            DataGridView1.RowHeadersVisible = False
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "No. Cuenta"
            DataGridView1.Columns(2).HeaderText = "Clabe"
            DataGridView1.Columns(3).HeaderText = "Banco"
            DataGridView1.Columns(4).Visible = False
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        If DataGridView1.Rows.Count > 0 Then
            idProvCuenta = Integer.Parse(DataGridView1.CurrentRow.Cells(0).Value)
            txtNumCuenta.Text = Convert.ToString(DataGridView1.CurrentRow.Cells(1).Value)
            txtClabe.Text = Convert.ToString(DataGridView1.CurrentRow.Cells(2).Value)
            ComboBancos.SelectedIndex = idsTipos.Busca(Integer.Parse(DataGridView1.CurrentRow.Cells(4).Value))
            btnGuardar.Text = "Modificar"
            btnEliminar.Enabled = True
            accion = False
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class