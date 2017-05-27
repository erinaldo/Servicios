Public Class frmBuscadorCatalogosSat

    Public Cat As dbCatalogosSat
    Public Clave As String
    Public Tipo As String
    Public CP As String
    Public Nombre As String
    Public CerrarCon As Boolean = True
    Private Sub frmBuscadorCatalogosSat_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If CerrarCon Then Cat.Con.Close()
    End Sub
    Private Sub frmBuscador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If Cat Is Nothing Then
            Cat = New dbCatalogosSat()
            If Cat.IniciarMySQL(My.Settings.Servidor, My.Settings.DBUsuario, My.Settings.DBPassword, My.Settings.puertodb) = 0 Then
                MsgBox("No se pudo establecer una conexion al servidor. reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            Else
                Consulta()
            End If
        Else
            Consulta()
        End If
        
    End Sub
    
    Private Sub Consulta()
        Select Case Tipo
            Case 0
                DGConsulta.DataSource = Cat.ConsultaFracciones(TextBox1.Text)
            Case 1
                DGConsulta.DataSource = Cat.ConsultaColonias(TextBox1.Text, CP)
            Case 2
                DGConsulta.DataSource = Cat.ConsultaMunicipio(TextBox1.Text)
            Case 3
                DGConsulta.DataSource = Cat.ConsultaLocalidad(TextBox1.Text)
            Case 4
                DGConsulta.DataSource = Cat.ConsultaEstados(TextBox1.Text)
            Case 5
                DGConsulta.DataSource = Cat.ConsultaPais(TextBox1.Text)
            Case 6
                DGConsulta.DataSource = Cat.ConsultaProductoServ(TextBox1.Text)
            Case 7
                DGConsulta.DataSource = Cat.ConsultaUnidadMedida(TextBox1.Text)
        End Select
        Try
            DGConsulta.Columns(0).HeaderText = "Clave"
            DGConsulta.Columns(1).HeaderText = "Descripcion"
            DGConsulta.Columns(0).Width = 100
            DGConsulta.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGConsulta.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Public Sub New(ByVal pTipo As Integer, Optional pCP As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        Tipo = pTipo
        CP = pCP
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Try
            If e.KeyCode = Keys.Down Then
                If DGConsulta.RowCount > 1 Then
                    If DGConsulta.CurrentRow.Index < DGConsulta.RowCount - 1 Then DGConsulta.CurrentCell = DGConsulta.Item(1, DGConsulta.CurrentCell.RowIndex + 1)
                End If
            End If
            If e.KeyCode = Keys.Up Then
                If DGConsulta.RowCount > 1 Then
                    If DGConsulta.CurrentRow.Index > 0 Then DGConsulta.CurrentCell = DGConsulta.Item(1, DGConsulta.CurrentCell.RowIndex - 1)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Consulta()
    End Sub
    Private Sub RegresaValor()
        Try
            If DGConsulta.RowCount > 0 Then
                If DGConsulta.RowCount = 1 Then
                    Clave = DGConsulta.Item(0, 0).Value
                    Nombre = DGConsulta.Item(1, 0).Value
                Else
                    Clave = DGConsulta.Item(0, DGConsulta.CurrentCell.RowIndex).Value
                    Nombre = DGConsulta.Item(1, DGConsulta.CurrentCell.RowIndex).Value
                End If
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RegresaValor()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Clave = ""
    End Sub

    Private Sub DGConsulta_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGConsulta.CellDoubleClick
        RegresaValor()
    End Sub

    Private Sub DGConsulta_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGConsulta.CellContentClick

    End Sub

    Private Sub DGConsulta_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGConsulta.KeyDown
        If e.KeyCode = Keys.Enter Then
            RegresaValor()
        End If
        
    End Sub
    
End Class