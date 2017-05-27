Public Class frmBuscadorClases
    Dim TipoB As Integer
    Public ID As Integer
    Public Proveedor As dbproveedores
    Public Inventario As dbInventario
    Public Cliente As dbClientes
    Public Servicio As dbServicios
    Public Trabajador As dbTrabajadores
    Private idAlmacen As Integer
    Public Tipo As String
    Dim IdsClas As New elemento
    Dim IdsClas2 As New elemento
    Dim IdsClas3 As New elemento
    Dim ConsultaOn As Boolean = True
    Dim SV As Boolean = False
    Dim SC As Boolean = False
    Dim SI As Boolean = False
    Private Sub frmBuscador_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
        Else
            ConsultaOn = False
            LlenaCombos("tblinventarioclasificaciones", ComboBox3, "nombre", "nombret", "idclasificacion", IdsClas, "idclasificacion>1", "Todas", "nombre")
            'ComboBox3.SelectedIndex = 1
            ConsultaOn = True
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
        Trabajador = 8
        ArticuloNoInv = 9
    End Enum
    Private Sub Consulta()
        If ConsultaOn = False Then Exit Sub
        If TipoB = TipoDeBusqueda.Articulo Then
            ConsultaInventario(0, 1)
        End If
        If TipoB = TipoDeBusqueda.Proveedor Then
            ConsultaProveedores()
        End If
        If TipoB = TipoDeBusqueda.Cliente Then
            ConsultaClientes()
        End If

        If TipoB = TipoDeBusqueda.ArticuloProducto Then
            'ConsultaInventarioProductos(0)
            ConsultaInventario(0, 0)
        End If
        If TipoB = TipoDeBusqueda.ArticuloProductoInv Then
            'ConsultaInventarioProductos(1)
            ConsultaInventario(1, 0)
        End If
        If TipoB = TipoDeBusqueda.ArticuloInv Then
            ConsultaInventario(1, 1)
        End If
        If TipoB = TipoDeBusqueda.ArticuloNoInv Then
            ConsultaInventario(2, 1)
        End If
        If TipoB = TipoDeBusqueda.Trabajador Then
            ConsultaTrabajadores()
        End If
        If DGConsulta.RowCount > 1 Then
            DGConsulta.CurrentCell = DGConsulta.Item(1, 0)
        End If
    End Sub
    Public Sub New(ByVal Tipo As TipoDeBusqueda, ByVal pidAlmacen As Integer, pSV As Boolean, pSC As Boolean, pSI As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        TipoB = Tipo
        idAlmacen = pidAlmacen
        ' Add any initialization after the InitializeComponent() call.
        SV = pSV
        SC = pSC
        SI = pSI
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
    Private Sub ConsultaInventario(ByVal pInv As Byte, ByVal peskit As Byte)
        Try
            Dim idClas As Integer
            Dim idClas2 As Integer
            Dim idClas3 As Integer
            If ComboBox3.SelectedIndex <= 0 Then
                idClas = 0
            Else
                idClas = IdsClas.Valor(ComboBox3.SelectedIndex)
            End If
            If ComboBox6.SelectedIndex <= 0 Then
                idClas2 = 0
            Else
                idClas2 = IdsClas2.Valor(ComboBox6.SelectedIndex)
            End If
            If ComboBox7.SelectedIndex <= 0 Then
                idClas3 = 0
            Else
                idClas3 = IdsClas3.Valor(ComboBox7.SelectedIndex)
            End If
            Dim I As New dbInventario(MySqlcon)
            DGConsulta.DataSource = I.Consulta(idAlmacen, TextBox1.Text, , idClas, True, idClas2, idClas3, , pInv, GlobalpFabricante, GlobalModoBusqueda, peskit, 1, 0, SV, SC, SI)
            DGConsulta.Columns(0).Visible = False
            DGConsulta.Columns(1).HeaderText = "Código"
            DGConsulta.Columns(2).HeaderText = "Descripción"
            DGConsulta.Columns(3).HeaderText = "Existencia"
            DGConsulta.Columns(3).DefaultCellStyle.Format = "0.####"
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
    '        DGConsulta.Columns(4).DefaultCellStyle.Format = "0.####"
    '        DGConsulta.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
    '        DGConsulta.Columns(1).Width = 180
    '        'DGConsulta.ClearSelection()
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub

    
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

                If TipoB = TipoDeBusqueda.Articulo Or TipoB = TipoDeBusqueda.ArticuloInv Then
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
                If TipoB = TipoDeBusqueda.ArticuloProducto Or TipoB = TipoDeBusqueda.ArticuloProductoInv Or TipoB = TipoDeBusqueda.ArticuloNoInv Then
                    'Tipo = DGConsulta.Item(3, DGConsulta.CurrentCell.RowIndex).Value
                    Tipo = "I"
                    Inventario = New dbInventario(ID, MySqlcon)
                    'Else
                    '   Producto = New dbProductos(ID, MySqlcon)
                    'End If

                End If
                If TipoB = TipoDeBusqueda.Trabajador Then
                    Trabajador = New dbTrabajadores(ID, MySqlcon)
                    Tipo = "TRA"
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
    Private Sub ConsultaTrabajadores()
        Try
            Dim PrimerCeldaRow As Integer = -1
            'If ConsultaOn Then
            'If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
            Dim P As New dbTrabajadores(MySqlcon)
            DGConsulta.DataSource = P.Consulta(TextBox1.Text)
            DGConsulta.Columns(0).Visible = False
            DGConsulta.Columns(1).HeaderText = "Nombre"
            DGConsulta.Columns(2).HeaderText = "No. Empleado"
            DGConsulta.Columns(3).HeaderText = "R. Patronal"
            DGConsulta.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            'If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim ctemp As Boolean
        ctemp = ConsultaOn
        ConsultaOn = False
        LlenaCombos("tblinventarioclasificaciones2", ComboBox6, "nombre", "nombret", "idclasificacion", IdsClas2, "idclasificacion>1 and idnivelsuperior=" + IdsClas.Valor(ComboBox3.SelectedIndex).ToString, "Todas", "nombre")
        ConsultaOn = ctemp
        Consulta()
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        Dim Ctemp As Boolean
        Ctemp = ConsultaOn
        ConsultaOn = False
        LlenaCombos("tblinventarioclasificaciones3", ComboBox7, "nombre", "nombret", "idclasificacion", IdsClas3, "idclasificacion>1 and idnivelsuperior=" + IdsClas2.Valor(ComboBox6.SelectedIndex).ToString, "Todas", "nombre")
        ConsultaOn = Ctemp
        Consulta()
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox7.SelectedIndexChanged
        Consulta()
    End Sub

    Private Sub DGConsulta_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGConsulta.CellFormatting
        If TipoB = 1 Or TipoB = 5 Or TipoB = 6 Or TipoB = 7 Then
            If e.ColumnIndex = 3 Then
                e.Value = Format(e.Value, "###,###,##0.####")
            End If
        End If
    End Sub
End Class