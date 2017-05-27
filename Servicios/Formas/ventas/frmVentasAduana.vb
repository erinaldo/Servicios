Public Class frmVentasAduana

    Dim ConsultaOn As Boolean = True
    Dim IdAduana As Integer
    Dim IdDetalle As Integer

    Public Sub New(ByVal pidDetalle As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdDetalle = pidDetalle
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub frmInventarioProductos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        NuevoConcepto()
        ConsultaDetalles()
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        'If (PermisosInventario And CULng((Math.Pow(2, perInventario.Ajuste_Inventario + 2)))) <> 0 Then
        Try
            Dim CD As New dbventasaduana(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            'Dim InvAnt As Double
            If TextBox4.Text = "" Or TextBox5.Text = "" Then
                MsgError = "Debe llenar todos los campos."
                HayError = True
            End If
            If HayError = False Then
                If Button4.Text = "Agregar" Then
                    CD.Guardar(IdDetalle, TextBox5.Text, Format(dtpFecha.Value, "yyyy/MM/dd"), TextBox4.Text)
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdAduana, TextBox5.Text, Format(dtpFecha.Value, "yyyy/MM/dd"), TextBox4.Text)
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo modificado", 90)
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
        'Else
        'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        'TextBox3.Focus()
        'End If
    End Sub
    Private Sub NuevoConcepto()
        dtpFecha.Value = Date.Now
        TextBox5.Text = ""
        TextBox4.Text = ""
        Button4.Text = "Agregar"
        'Button9.Enabled = False
        TextBox5.Focus()
    End Sub
    Private Sub ConsultaDetalles()
        Try
            Dim PrimerCeldaRow As Integer = -1
            Dim ID As New dbventasaduana(MySqlcon)
            If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex

            DGDetalles.DataSource = ID.Consulta(IdDetalle)
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).HeaderText = "Número"
            DGDetalles.Columns(2).HeaderText = "Fecha"
            DGDetalles.Columns(3).HeaderText = "Aduana"
            DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


            If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            IdAduana = 0
            ' End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick

        IdAduana = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
        LlenaDAtos(IdAduana)
    End Sub
    Private Sub LlenaDAtos(ByVal pIddetalle As Integer)


        'Al vuelo
        Dim Ide As New dbventasaduana(pIddetalle, MySqlcon)
        'Dim I As New dbInventario(Ide.Idinventario, MySqlcon)
        TextBox5.Text = Ide.Numero
        TextBox4.Text = Ide.Aduana
        dtpFecha.Value = CDate(Ide.Fecha)
        Button9.Enabled = True
        Button4.Text = "Modificar"
        TextBox5.Focus()

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If IdAduana <> 0 Then
            If MsgBox("¿Remover este artículo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim ide As New dbventasaduana(MySqlcon)
                ide.Eliminar(IdAduana)
                ConsultaDetalles()
                PopUp("Removido.", 90)
            End If
        Else
            MsgBox("Debe seleccionar un renglon", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpFecha.Focus()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub dtpFecha_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtpFecha.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

End Class