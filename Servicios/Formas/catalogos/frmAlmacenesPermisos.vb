Public Class frmAlmacenesPermisos
    Dim AlmacenStr As String
    Dim IdAlmacen As Integer
    Dim Sucursal As String
    Dim Almacen As dbAlmacenes
    Dim IdsUsuarios As New elemento
    Private Sub frmAlmacenesPermisos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        LlenaCombos("tblusuarios", ComboBox1, "nombreusuario", "nus", "idusuario", IdsUsuarios, "idusuario<>1000")
        Consulta()
    End Sub
    Public Sub New(pidAlmacen As Integer, pAlmacen As String, pSucursal As String)

        ' This call is required by the designer.
        InitializeComponent()
        IdAlmacen = pidAlmacen
        AlmacenStr = pAlmacen
        Sucursal = pSucursal
        Label1.Text = "Sucursal: " + Sucursal
        Label2.Text = "Almacén: " + AlmacenStr
        IdAlmacen = pidAlmacen
        Almacen = New dbAlmacenes(MySqlcon)
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub Consulta()
        Try
            Dim PrimerCeldaRow As Integer = -1

            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            DataGridView1.DataSource = Almacen.UsuariosAgregados(IdAlmacen)
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).HeaderText = "Usuario"
            DataGridView1.Columns(2).HeaderText = "Nombre"
            DataGridView1.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Almacen.ChecaUsuario(IdsUsuarios.Valor(ComboBox1.SelectedIndex), IdAlmacen) = 0 Then
                Almacen.AgregarUsuario(IdsUsuarios.Valor(ComboBox1.SelectedIndex), IdAlmacen)
                Consulta()
            Else
                MsgBox("Ya se agregó este usuario anteriormente.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each id As Integer In IdsUsuarios.Valor
            If Almacen.ChecaUsuario(id, IdAlmacen) = 0 Then
                Almacen.AgregarUsuario(id, IdAlmacen)
            End If
        Next
        Consulta()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            If MsgBox("¿Remover este usuario de la lista?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Almacen.QuitarUsuario(DataGridView1.Item(0, e.RowIndex).Value, IdAlmacen)
                Consulta()
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class