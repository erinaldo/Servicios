Public Class frmVentasAsignaSeries

    Dim ConsultaOn As Boolean = True
    Dim IdInventario As Integer
    Dim IdSerie As Integer
    Dim IdSerie2 As Integer
    Dim IdVenta As Integer
    Dim IdRemision As Integer
    Dim IdCotizacion As Integer
    Dim CantidadSeries As Integer
    Dim ConAviso As Boolean
    Public Sub New(ByVal pIdInventario As Integer, Optional ByVal pIdVenta As Integer = 0, Optional ByVal PIdRemision As Integer = 0, Optional ByVal pCantidadSeries As Integer = 1, Optional ByVal pConAviso As Boolean = True, Optional ByVal pIdCotizacion As Integer = 0)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        IdInventario = pIdInventario
        IdVenta = pIdVenta
        IdRemision = PIdRemision
        CantidadSeries = pCantidadSeries
        ConAviso = pConAviso
        IdCotizacion = pIdCotizacion
    End Sub
    
    Private Sub frmVentasAsignaSeries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Dim S As New dbInventarioSeries(MySqlcon)
        If IdVenta <> 0 Then Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaVenta(IdVenta, IdInventario))
        If IdRemision <> 0 Then Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaRemision(IdRemision, IdInventario))
        If IdCotizacion <> 0 Then Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaCotizacion(IdCotizacion, IdInventario))
        Consulta()
        ConsultaAgregadas()
    End Sub

    Private Sub Alta()
        Try
            If DGConsulta.RowCount = 1 Then
                IdSerie = DGConsulta.Item(0, 0).Value
            Else
                IdSerie = DGConsulta.Item(0, DGConsulta.CurrentRow.Index).Value
            End If
            Dim S As New dbInventarioSeries(MySqlcon)
            If IdVenta <> 0 Then
                If S.CantidadDeSeriesAgregadasaVenta(IdVenta, IdInventario) < CantidadSeries * 2 Then
                    S.AsignaSerieAVenta(IdSerie, IdVenta)
                    Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaVenta(IdVenta, IdInventario))
                    Consulta()
                    ConsultaAgregadas()
                Else
                    MsgBox("No se pueden agregar mas series a esta venta.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            End If
            If IdRemision <> 0 Then
                If S.CantidadDeSeriesAgregadasaRemision(IdRemision, IdInventario) < CantidadSeries Then
                    S.AsignaSerieARemision(IdSerie, IdRemision)
                    Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaRemision(IdRemision, IdInventario))
                    Consulta()
                    ConsultaAgregadas()
                Else
                    MsgBox("No se pueden agregar mas series a esta venta.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            End If
            If IdCotizacion <> 0 Then
                If S.CantidadDeSeriesAgregadasaCotizacion(IdCotizacion, IdInventario) < CantidadSeries Then
                    S.AsignaSerieACotizacion(IdSerie, IdCotizacion)
                    Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaCotizacion(IdCotizacion, IdInventario))
                    Consulta()
                    ConsultaAgregadas()
                Else
                    MsgBox("No se pueden agregar mas series a esta venta.", MsgBoxStyle.Information, GlobalNombreApp)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Remover()
        Try
            Dim S As New dbInventarioSeries(MySqlcon)
            If IdVenta <> 0 Then
                S.QuitaSerieAVenta(IdSerie2)
                Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaVenta(IdVenta, IdInventario))
                Consulta()
                ConsultaAgregadas()
            End If
            If IdRemision <> 0 Then
                S.QuitaSerieARemision(IdSerie2)
                Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaRemision(IdRemision, IdInventario))
                Consulta()
                ConsultaAgregadas()
            End If
            If IdCotizacion <> 0 Then
                S.QuitaSerieACotizacion(IdSerie2)
                Label2.Text = "Series por agregar: " + CStr(CantidadSeries - S.CantidadDeSeriesAgregadasaCotizacion(IdCotizacion, IdInventario))
                Consulta()
                ConsultaAgregadas()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Alta()
    End Sub

    Private Sub Consulta()
        Try
            If ConsultaOn Then
                Dim PrimerCeldaRow As Integer = -1
                Dim S As New dbInventarioSeries(MySqlcon)
                If DGConsulta.RowCount > 0 Then PrimerCeldaRow = DGConsulta.FirstDisplayedCell.RowIndex
                DGConsulta.DataSource = S.Consulta(IdInventario, TextBox3.Text, 0, 0, 0, True)
                DGConsulta.Columns(0).Visible = False
                DGConsulta.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGConsulta.Columns(1).HeaderText = "Series Disponibles"
                DGConsulta.Columns(2).HeaderText = "Garantia"
                DGConsulta.Columns(3).HeaderText = "Caducidad"
                DGConsulta.Columns(4).Visible = False
                DGConsulta.Columns(5).Visible = False
                DGConsulta.Columns(6).Visible = False
                DGConsulta.Columns(7).Visible = False
                DGConsulta.Columns(8).Visible = False
                DGConsulta.Columns(9).Visible = False
                'DGConsulta.ClearSelection()
                If DGConsulta.RowCount > 0 Then DGConsulta.CurrentCell = DGConsulta.Item(1, 0)
                If DGConsulta.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGConsulta.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaAgregadas()
        Try
            If ConsultaOn Then
                Dim PrimerCeldaRow As Integer = -1
                Dim S As New dbInventarioSeries(MySqlcon)
                If DGConsulta.RowCount > 0 Then PrimerCeldaRow = DGConsulta.FirstDisplayedCell.RowIndex
                DGConsulta2.DataSource = S.Consulta(IdInventario, TextBox1.Text, 0, IdVenta, IdRemision, , , , IdCotizacion)
                DGConsulta2.Columns(0).Visible = False
                DGConsulta2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                DGConsulta2.Columns(1).HeaderText = "Series Agregadas"
                DGConsulta2.Columns(2).HeaderText = "Garantía"
                DGConsulta2.Columns(3).HeaderText = "Caducidad"
                DGConsulta2.Columns(4).Visible = False
                DGConsulta2.Columns(5).Visible = False
                DGConsulta2.Columns(6).Visible = False
                DGConsulta2.Columns(7).Visible = False
                DGConsulta2.Columns(8).Visible = False
                DGConsulta2.Columns(9).Visible = False
                DGConsulta2.ClearSelection()
                If DGConsulta.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGConsulta.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        ConsultaAgregadas()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim S As New dbInventarioSeries(MySqlcon)
        If IdVenta <> 0 Then
            If S.CantidadDeSeriesAgregadasaVenta(IdVenta, IdInventario) = CantidadSeries Then
                Me.Close()
            Else
                If ConAviso Then
                    If MsgBox("Debe agregar " + CantidadSeries.ToString + " series. ¿Desea cerrar esta ventana de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If
            End If
        End If
        If IdRemision <> 0 Then
            If S.CantidadDeSeriesAgregadasaRemision(IdRemision, IdInventario) = CantidadSeries Then
                Me.Close()
            Else
                If ConAviso Then
                    If MsgBox("Debe agregar " + CantidadSeries.ToString + " series. ¿Desea cerrar esta ventana de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If
            End If
        End If
        If IdCotizacion <> 0 Then
            If S.CantidadDeSeriesAgregadasaCotizacion(IdCotizacion, IdInventario) = CantidadSeries Then
                Me.Close()
            Else
                If ConAviso Then
                    If MsgBox("Debe agregar " + CantidadSeries.ToString + " series. ¿Desea cerrar esta ventana de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Me.Close()
                    End If
                Else
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Remover()
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
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

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Consulta()
    End Sub

    Private Sub DGConsulta_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGConsulta.CellClick
        IdSerie = DGConsulta.Item(0, e.RowIndex).Value
    End Sub
    Private Sub DGConsulta2_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGConsulta2.CellClick
        IdSerie2 = DGConsulta2.Item(0, e.RowIndex).Value
    End Sub
   
    
    Private Sub DGConsulta_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGConsulta.CellContentClick

    End Sub
End Class