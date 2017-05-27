Public Class frmBuscadorGrande
    Dim TipoB As Integer
    Public ID As Integer
    Public Proveedor As dbproveedores
    Public Inventario As dbInventario
    Public Cliente As dbClientes
    Public Servicio As dbServicios
    Private idAlmacen As Integer
    Public Tipo As String
    Private Sub frmBuscador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
        Else
            Consulta()
        End If
    End Sub
    Public Enum TipoDeBusqueda As Integer
        Proveedor = 0
        Articulo = 1
        Cliente = 2
        Producto = 3
        Servicio = 4
        ArticuloProducto = 5
        ArticuloProductoInv = 6
        ArticuloInv = 7
        ArticuloNoInv = 8
    End Enum
    Private Sub Consulta()
        If TipoB = TipoDeBusqueda.Articulo Then
            ConsultaInventario(0)
        End If
        If TipoB = TipoDeBusqueda.Proveedor Then
            ConsultaProveedores()
        End If
        If TipoB = TipoDeBusqueda.Cliente Then
            ConsultaClientes()
        End If
        If TipoB = TipoDeBusqueda.ArticuloNoInv Then
            ConsultaInventario(2)
        End If
        If TipoB = TipoDeBusqueda.Servicio Then
            ConsultaServicios()
        End If
        'If TipoB = TipoDeBusqueda.ArticuloProducto Then
        '    ConsultaInventarioProductos(0)
        'End If
        'If TipoB = TipoDeBusqueda.ArticuloProductoInv Then
        '    ConsultaInventarioProductos(1)
        'End If
        If TipoB = TipoDeBusqueda.ArticuloInv Then
            ConsultaInventario(1)
        End If
        If DGConsulta.RowCount > 1 Then
            DGConsulta.CurrentCell = DGConsulta.Item(1, 0)
        End If
    End Sub
    Public Sub New(ByVal Tipo As TipoDeBusqueda, ByVal pidAlmacen As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TipoB = Tipo
        idAlmacen = pidAlmacen
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub ConsultaProveedores()
        Try
            Dim P As New dbproveedores(MySqlcon)
            DGConsulta.DataSource = P.Consulta(TextBox1.Text)
            DGConsulta.Columns(0).Visible = False
            DGConsulta.Columns(1).HeaderText = "Código"
            DGConsulta.Columns(2).HeaderText = "Nombre"
            DGConsulta.Columns(3).HeaderText = "RFC"
            DGConsulta.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGConsulta.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaClientes()
        Try
            Dim P As New dbClientes(MySqlcon)
            DGConsulta.DataSource = P.Consulta(TextBox1.Text)
            DGConsulta.Columns(0).Visible = False
            DGConsulta.Columns(1).HeaderText = "Código"
            DGConsulta.Columns(2).HeaderText = "Nombre"
            DGConsulta.Columns(3).HeaderText = "RFC"
            DGConsulta.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGConsulta.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub ConsultaInventario(ByVal pInv As Byte)
        Try

            Dim I As New dbInventario(MySqlcon)
            DGConsulta.DataSource = I.Consulta(idAlmacen, TextBox1.Text, , , True, , , , pInv, GlobalpFabricante, GlobalModoBusqueda, 0, 1, 0, True, False, False)
            DGConsulta.Columns(0).Visible = False
            DGConsulta.Columns(1).HeaderText = "Código"
            DGConsulta.Columns(2).HeaderText = "Descripción"
            DGConsulta.Columns(3).HeaderText = "Cantidad"
            DGConsulta.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGConsulta.Columns(1).Width = 180
            'DGConsulta.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    'Private Sub ConsultaInventarioProductos(ByVal pInv As Byte)
    '    Try

    '        Dim I As New dbInventario(MySqlcon)
    '        DGConsulta.DataSource = I.ConsultaAmbos(TextBox1.Text, idAlmacen, pInv, GlobalpFabricante, GlobalModoBusqueda)
    '        DGConsulta.Columns(0).Visible = False
    '        DGConsulta.Columns(3).Visible = False
    '        DGConsulta.Columns(1).HeaderText = "Código"
    '        DGConsulta.Columns(2).HeaderText = "Descripción"
    '        DGConsulta.Columns(4).HeaderText = "Existencia"
    '        DGConsulta.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    '        DGConsulta.Columns(1).Width = 140
    '        'DGConsulta.ClearSelection()
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub

    Private Sub ConsultaServicios()
        Try

            Dim S As New dbServicios(MySqlcon)

            DGConsulta.DataSource = S.Consulta(Format(dtpFecha1.Value, "yyyy/MM/dd"), Format(dtpFecha2.Value, "yyyy/MM/dd"), TextBox1.Text)
            DGConsulta.Columns(0).Visible = False
            DGConsulta.Columns(1).HeaderText = "Folio"
            DGConsulta.Columns(2).HeaderText = "Fecha"
            DGConsulta.Columns(3).HeaderText = "Cliente"
            DGConsulta.Columns(4).Visible = False
            DGConsulta.Columns(3).Width = 280
            DGConsulta.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'DGConsulta.ClearSelection()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

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
            If e.KeyCode = Keys.F2 Then
                Relaciones()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Relaciones()
        'If e.KeyCode = Keys.F2 Then
        If TipoB = TipoDeBusqueda.Articulo Or TipoB = TipoDeBusqueda.ArticuloInv Or TipoB = TipoDeBusqueda.ArticuloProducto Or TipoB = TipoDeBusqueda.ArticuloProductoInv Then
            If DGConsulta.RowCount = 1 Then
                ID = DGConsulta.Item(0, 0).Value
            Else
                ID = DGConsulta.Item(0, DGConsulta.CurrentCell.RowIndex).Value
            End If
            Dim IR As New frmInventarioConsultaRelaciones(ID)
            If IR.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox1.Text = IR.Clave
            End If
            IR.Dispose()
        End If
        'End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Consulta()
    End Sub
    Private Sub RegresaValor()
        Try
            If DGConsulta.RowCount > 0 Then
                If DGConsulta.RowCount = 1 Then
                    ID = DGConsulta.Item(0, 0).Value
                Else
                    ID = DGConsulta.Item(0, DGConsulta.CurrentCell.RowIndex).Value
                End If

                If TipoB = TipoDeBusqueda.Articulo Or TipoB = TipoDeBusqueda.ArticuloInv Or TipoB = TipoDeBusqueda.ArticuloNoInv Then
                    Inventario = New dbInventario(ID, MySqlcon)
                    Tipo = "I"
                End If
               
                If TipoB = TipoDeBusqueda.Proveedor Then
                    Proveedor = New dbproveedores(ID, MySqlcon)
                    Tipo = "PRO"
                End If
                If TipoB = TipoDeBusqueda.Cliente Then
                    Cliente = New dbClientes(ID, MySqlcon)
                    Tipo = "CL"
                End If
                If TipoB = TipoDeBusqueda.Servicio Then
                    Servicio = New dbServicios(ID, MySqlcon)
                    Tipo = "S"
                End If
                If TipoB = TipoDeBusqueda.ArticuloProducto Or TipoB = TipoDeBusqueda.ArticuloProductoInv Then
                    Tipo = DGConsulta.Item(3, DGConsulta.CurrentCell.RowIndex).Value
                    If Tipo = "I" Then
                        Inventario = New dbInventario(ID, MySqlcon)
                    End If

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
        ID = 0
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
        If e.KeyCode = Keys.F2 Then
            Relaciones()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub DGConsulta_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGConsulta.CellFormatting
        If TipoB = 1 Or TipoB = 5 Or TipoB = 6 Or TipoB = 7 Then
            If e.ColumnIndex = 3 Then
                e.Value = Format(e.Value, "###,###,##0.####")
            End If
        End If
    End Sub
End Class