Public Class frmInventarioAduanaConsulta

    Dim Aduana As dbInventarioAduana
    Dim Idinvetario As Integer
    Public Sub New(pIdInventario As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        Idinvetario = pIdInventario
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub frmInventarioLotesConsulta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        Aduana = New dbInventarioAduana(MySqlcon)
        Aduana.IdInventario = Idinvetario
        ConsultaAduanas()
    End Sub
    Private Sub ConsultaAduanas()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex
            'Dim P As New dblotes(MySqlcon)
            DataGridView2.DataSource = Aduana.ConsultaAduanas(Aduana.IdInventario, "", False, 0)
            DataGridView2.Columns(0).Visible = False
            'DataGridView2.Columns(1).Visible = False
            DataGridView2.Columns(1).HeaderText = "Aduana"
            DataGridView2.Columns(2).HeaderText = "Número"
            DataGridView2.Columns(3).HeaderText = "Fecha"
            DataGridView2.Columns(4).HeaderText = "Cantidad"
            DataGridView2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView2.Columns(4).Width = 60
            If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'Label3.Text = CStr(Cantidad - Lote.CantidadAsignados()) + " de " + Cantidad.ToString + " artículos por asignar."
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaMovimientosAduana()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            'Dim P As New dblotes(MySqlcon)
            DataGridView1.DataSource = Aduana.ConsultaMovimientosAduana(Aduana.IdAduana)
            DataGridView1.Columns(0).Visible = False
            'DataGridView2.Columns(5).Visible = False
            'iddocumento,tipodoc,fecha,folio,cantidad
            DataGridView1.Columns(1).HeaderText = "Doc"
            DataGridView1.Columns(2).HeaderText = "Fecha"
            DataGridView1.Columns(3).HeaderText = "Folio"
            DataGridView1.Columns(4).HeaderText = "Entrada"
            DataGridView1.Columns(5).HeaderText = "Salida"
            DataGridView1.Columns(6).HeaderText = "Almacen"
            DataGridView1.Columns(7).HeaderText = "Destino"
            DataGridView1.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView1.Columns(2).Width = 80
            DataGridView1.Columns(4).Width = 60
            DataGridView1.Columns(5).Width = 60
            If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'Label3.Text = CStr(Cantidad - Lote.CantidadAsignados()) + " de " + Cantidad.ToString + " artículos por asignar."
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex >= 0 Then
            Aduana.IdAduana = DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value
            ConsultaMovimientosAduana()
        End If
    End Sub
    Private Sub AbreDocumento()
        Select Case DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value
            Case "COMPRA"
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.ComprasVer, PermisosN.Secciones.Compras) = True Then
                    Dim Fac As New frmCompras(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If

            Case "REM. COMPRA"
                If GlobalPermisos.ChecaPermiso(PermisosN.Compras.RemisionesVer, PermisosN.Secciones.Compras) = True Then
                    Dim Fac As New frmComprasRemisiones(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case "REMISIÓN"
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.RemisionesVer, PermisosN.Secciones.Ventas) = True Then
                    Dim Fac As New frmVentasRemisiones(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
            Case "FACTURA"
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = True Then
                    Dim Fac As New frmVentasN(DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value, 0, 0, 0)
                    Fac.ShowDialog()
                    Fac.Dispose()
                End If
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView2.CurrentCell.RowIndex >= 0 Then
            AbreDocumento()
        Else
            MsgBox("Debe seleccionar un documento.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        AbreDocumento()
    End Sub
End Class