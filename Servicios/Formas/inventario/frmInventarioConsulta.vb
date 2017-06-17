Public Class frmInventarioConsulta
    Dim IdInventario As Integer
    Dim ConsultaOn As Boolean = True
    Dim Modo As Byte
    Public IdAlmacen As Integer
    Public IdSucursal As Integer
    Public Sub New(ByVal pIdInventario As Integer, pModo As Byte, pIdSucursal As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdInventario = pIdInventario
        IdSucursal = pIdSucursal
        ' Add any initialization after the InitializeComponent() call.
        Modo = pModo
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0, "") Then
                    LlenaDatosArticulo(p)
                Else
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox4.Text = "0.00"
                    TextBox5.Text = "0.00"
                    TextBox6.Text = "0.00"
                    TextBox7.Text = "0.0"
                    TextBox8.Text = ""
                    TextBox9.Text = ""
                    TextBox10.Text = ""
                    TextBox11.Text = "0.00"
                    DataGridView1.DataSource = Nothing
                    DataGridView2.DataSource = Nothing
                    DataGridView3.DataSource = Nothing
                    DataGridView4.DataSource = Nothing
                    IdInventario = 0
                    btnInventarioUbicacion.Visible = False
                    'Dim ps As New dbProductos(MySqlcon)
                    'If ps.BuscaProducto(TextBox3.Text) Then
                    '    LlenaDatosProducto(ps)
                    'Else
                    '    TextBox4.Text = ""
                    '    TextBox6.Text = "0"
                    '    TextBox8.Text = "0"
                    '    TextBox9.Text = "0"
                    '    PrecioU = 0
                    '    IdVariante = 0
                    'End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub ConsultaLotes(pIdInventario As Integer)
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView3.RowCount > 0 Then PrimerCeldaRow = DataGridView3.FirstDisplayedCell.RowIndex
            Dim lote As New dblotes(MySqlcon)
            DataGridView3.DataSource = lote.ConsultaLotesxAlmacen(pIdInventario, "", True, 0)
            DataGridView3.Columns(0).Visible = False
            'DataGridView3.Columns(1).Visible = False
            DataGridView3.Columns(1).HeaderText = "Lote"
            DataGridView3.Columns(2).HeaderText = "Caducidad"
            DataGridView3.Columns(3).HeaderText = "Almacén"
            DataGridView3.Columns(4).HeaderText = "Cant."
            DataGridView3.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView3.Columns(1).Width = 90
            DataGridView3.Columns(2).Width = 80
            DataGridView3.Columns(4).Width = 60
            If DataGridView3.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView3.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'Label3.Text = CStr(Cantidad - Lote.CantidadAsignados()) + " de " + Cantidad.ToString + " artículos por asignar."
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub buscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        Dim op As New dbOpciones(MySqlcon)
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        If op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                'Select Case B.Tipo
                '   Case "I"
                LlenaDatosArticulo(B.Inventario)
                '    TextBox12.Focus()
                'Case "P"
                '    LlenaDatosProducto(B.Producto)
                '    TextBox12.Focus()
                'Case "S"
                '    LlenaDatosServicio(B.Servicio)
                '    TextBox12.Focus()
                'End Select

            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                'Select Case B.Tipo
                '   Case "I"
                LlenaDatosArticulo(B.Inventario)
                '    TextBox12.Focus()
                'Case "P"
                '    LlenaDatosProducto(B.Producto)
                '    TextBox12.Focus()
                'Case "S"
                '    LlenaDatosServicio(B.Servicio)
                '    TextBox12.Focus()
                'End Select

            End If
            B.Dispose()
        End If
        
    End Sub

    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        ConsultaOn = False
        TextBox3.Text = Articulo.Clave
        TextBox3.Select(TextBox3.Text.Length, 0)
        ConsultaOn = True
        TextBox1.Text = Articulo.Nombre
        TextBox2.Text = Articulo.Descripcion
        'TextBox8.Text = Articulo.Iva.ToString
        IdInventario = Articulo.ID
        TextBox4.Text = Format(Articulo.DaInventarioTodos(IdInventario), "0.00")
        TextBox5.Text = Format(Articulo.CostoBase, "0.00")
        'Articulo.DaUltimaidMoneda(Articulo.ID)
        'Articulo.DaUltimoCosto(Articulo.ID)
        TextBox6.Text = Format(Articulo.DaUltimoCosto(IdInventario), "0.00")
        TextBox7.Text = Articulo.Iva.ToString
        TextBox8.Text = Articulo.Ubicacion
        TextBox9.Text = Format(Articulo.Contenido * CDbl(TextBox4.Text), "0.00")
        TextBox10.Text = Format(Articulo.DaInventarioApartado(Articulo.ID), "0.00")
        TextBox11.Text = Format(Articulo.ieps, "0.00")
        TextBox12.Text = Format(Articulo.DaInventarioEnTransito(Articulo.ID, 0, 0), "0.00")
        If Articulo.EsKit = 1 Then
            Label12.Visible = True
            Label12.Text = "Cantidad de kits: " + Format(Articulo.CuantosKits(Articulo.ID, 0), "0.00")
        Else
            Label12.Visible = False
        End If
        btnInventarioUbicacion.Visible = True
        ConsultaPrecios()
        ConsultaAlmacenes()
        ConsultaLotes(Articulo.ID)
        ConsultaAduanas(Articulo.ID)
    End Sub
    Private Sub ConsultaPrecios()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then

                If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex
                Dim I As New dbInventarioPrecios(MySqlcon)
                DataGridView2.DataSource = I.Consulta(IdInventario)
                DataGridView2.Columns(0).Visible = False
                DataGridView2.Columns(1).HeaderText = "Lista"
                DataGridView2.Columns(2).HeaderText = "Precio"
                DataGridView2.Columns(2).DefaultCellStyle.Format = "C2"
                DataGridView2.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DataGridView2.Columns(3).HeaderText = "U."
                DataGridView2.Columns(4).HeaderText = "Desc."
                DataGridView2.Columns(5).HeaderText = "Coment."
                DataGridView2.Columns(3).Width = 40
                DataGridView2.Columns(4).Width = 50
                DataGridView2.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaAlmacenes()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then

                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim I As New dbInventario(MySqlcon)
                DataGridView1.DataSource = I.ConsultaInventarioPorAlmacen(IdSucursal, 0, IdInventario)
                'DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(0).HeaderText = "Sucursal"
                DataGridView1.Columns(1).HeaderText = "Almacen"
                DataGridView1.Columns(2).HeaderText = "Existencia"
                DataGridView1.Columns(3).HeaderText = "Tránsito"
                DataGridView1.Columns(4).Visible = False
                DataGridView1.Columns(2).DefaultCellStyle.Format = "0.####"
                'DataGridView2.Columns(2).Width = 20
                DataGridView1.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                Dim R As Integer = DataGridView1.RowCount
                Dim C As Integer = 0
                While C < R
                    DataGridView1.Item(2, C).Value = DataGridView1.Item(2, C).Value - DataGridView1.Item(3, C).Value
                    C += 1
                End While
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub frmInventarioConsulta_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            buscaArticuloBoton()
        End If
    End Sub

    Private Sub frmInventarioConsulta_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If IdInventario = 0 Then
            buscaArticuloBoton()
        Else
            Dim I As New dbInventario(IdInventario, MySqlcon)
            LlenaDatosArticulo(I)
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        buscaArticuloBoton()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim IR As New frmInventarioConsultaRelaciones(IdInventario)
        IR.ShowDialog()
        IR.Dispose()
    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
    Private Sub ConsultaAduanas(pIdinventario As Integer)
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            If DataGridView4.RowCount > 0 Then PrimerCeldaRow = DataGridView4.FirstDisplayedCell.RowIndex
            Dim Aduana As New dbInventarioAduana(MySqlcon)
            DataGridView4.DataSource = Aduana.ConsultaAduanasxAlmacen(pIdinventario, "", True, 0)
            DataGridView4.Columns(0).Visible = False
            'DataGridView2.Columns(1).Visible = False
            DataGridView4.Columns(1).HeaderText = "Aduana"
            DataGridView4.Columns(2).HeaderText = "Número"
            DataGridView4.Columns(3).HeaderText = "Fecha"
            DataGridView4.Columns(4).HeaderText = "Almacen"
            DataGridView4.Columns(5).HeaderText = "Cant."
            DataGridView4.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DataGridView4.Columns(1).Width = 90
            DataGridView4.Columns(2).Width = 70
            DataGridView4.Columns(3).Width = 80
            DataGridView4.Columns(5).Width = 60
            If DataGridView4.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView4.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'Label3.Text = CStr(Cantidad - Lote.CantidadAsignados()) + " de " + Cantidad.ToString + " artículos por asignar."
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnInventarioUbicacion_Click(sender As Object, e As EventArgs) Handles btnInventarioUbicacion.Click
        If IdInventario <> 0 Then
            Dim f As New frmConsultaUbicaciones(GlobalIdSucursalDefault, IdInventario)
            f.ShowDialog()
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If Modo = 1 Then
            If e.RowIndex >= 0 Then
                IdAlmacen = DataGridView1.Item(4, e.RowIndex).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
            End If
        End If
    End Sub

    Public ReadOnly Property Almacen As String
        Get
            If DataGridView1.ColumnCount > 0 And DataGridView1.SelectedRows.Count > 0 Then Return DataGridView1.SelectedRows(0).Cells(1).Value
            Return ""
        End Get
    End Property

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class