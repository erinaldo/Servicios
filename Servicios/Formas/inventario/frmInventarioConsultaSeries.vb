Public Class frmInventarioConsultaSeries

    
    Dim ConsultaOn As Boolean = True
    Dim IdInventario As Integer
    Dim IdSerie As Integer
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorMorado As Color = Color.FromArgb(240, 180, 240)
    Dim ColorAzul As Color = Color.FromArgb(200, 200, 255)
    Dim ColorAmarillo As Color = Color.FromArgb(255, 255, 192)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    Public Sub New(ByVal pIdInventario As Integer)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdInventario = pIdInventario
    End Sub

    Private Sub frmInventarioConsultaSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        chkTiempoReal.Checked = GlobalConsultaTiempoReal
        Consulta()
    End Sub

    

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim S As New dbInventarioSeries(IdSerie, MySqlcon)
        If S.idCompra <> 0 Then
            Dim F As New frmCompras(S.idCompra)
            F.ShowDialog()
        End If
    End Sub

    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim PrimerCeldaRow As Integer = -1
                Dim S As New dbInventarioSeries(MySqlcon)
                If DGConsulta.RowCount > 0 Then PrimerCeldaRow = DGConsulta.FirstDisplayedCell.RowIndex
                DGConsulta.DataSource = S.Consulta(IdInventario, TextBox3.Text, 0, 0)
                DGConsulta.Columns(0).Visible = False
                DGConsulta.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGConsulta.Columns(1).HeaderText = "Series Disponibles"
                DGConsulta.Columns(2).HeaderText = "Garantía"
                DGConsulta.Columns(3).HeaderText = "Caducidad"
                DGConsulta.Columns(4).Visible = False
                DGConsulta.Columns(5).Visible = False
                DGConsulta.Columns(6).Visible = False
                DGConsulta.Columns(7).Visible = False
                DGConsulta.Columns(8).Visible = False
                DGConsulta.Columns(9).Visible = False
                If DGConsulta.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGConsulta.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                DGConsulta.ClearSelection()
                Button1.Enabled = False
                Button2.Enabled = False
                Button5.Enabled = False
                Button7.Enabled = False
                Button6.Enabled = False
                Button8.Enabled = False
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim S As New dbInventarioSeries(IdSerie, MySqlcon)
        If S.idVenta <> 0 Then
            Dim F As New frmVentasN(S.idVenta, 0, 0, 0)
            F.ShowDialog()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If chkTiempoReal.Checked Then Consulta()
    End Sub

    Private Sub DGConsulta_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGConsulta.CellClick
        IdSerie = DGConsulta.Item(0, e.RowIndex).Value
        If DGConsulta.Item(4, e.RowIndex).Value <> 0 Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
        If DGConsulta.Item(5, e.RowIndex).Value <> 0 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
        If DGConsulta.Item(6, e.RowIndex).Value <> 0 Then
            Button5.Enabled = True
        Else
            Button5.Enabled = False
        End If
        If DGConsulta.Item(7, e.RowIndex).Value <> 0 Then
            Button6.Enabled = True
        Else
            Button6.Enabled = False
        End If
        If DGConsulta.Item(8, e.RowIndex).Value <> 0 Then
            Button7.Enabled = True
        Else
            Button7.Enabled = False
        End If
        If DGConsulta.Item(9, e.RowIndex).Value <> 0 Then
            Button8.Enabled = True
        Else
            Button8.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Consulta()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim S As New dbInventarioSeries(IdSerie, MySqlcon)
        If S.idMovimiento <> 0 Then
            Dim F As New frmInventarioMovimientosN(S.idMovimiento)
            F.ShowDialog()
        End If
    End Sub

    Private Sub DGConsulta_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGConsulta.CellContentClick

    End Sub

    Private Sub DGConsulta_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGConsulta.CellFormatting
        If DGConsulta.Item(4, e.RowIndex).Value <> 0 Or DGConsulta.Item(7, e.RowIndex).Value <> 0 Then
            e.CellStyle.BackColor = ColorRojo
        Else
            e.CellStyle.BackColor = ColorVerde
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim S As New dbInventarioSeries(IdSerie, MySqlcon)
        If S.IdRemisionC <> 0 Then
            Dim F As New frmComprasRemisiones(S.IdRemisionC)
            F.ShowDialog()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim S As New dbInventarioSeries(IdSerie, MySqlcon)
        If S.idRemision <> 0 Then
            Dim F As New frmVentasRemisiones(S.idRemision)
            F.ShowDialog()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim S As New dbInventarioSeries(IdSerie, MySqlcon)
        If S.IdCotizacion <> 0 Then
            Dim F As New frmVentasCotizacion(S.IdCotizacion)
            F.ShowDialog()
        End If
    End Sub
End Class