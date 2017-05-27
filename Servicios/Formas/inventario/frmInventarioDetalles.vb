Public Class frmInventarioDetalles
    Dim ConsultaOn As Boolean = True
    Dim IdDetalle As Integer
    Dim idInventario As Integer
    Dim IdInventarioP As Integer
    Dim Tipo As Byte
    Dim IdDetalleV As Integer
    Dim IdDocumento As Integer
    Public Sub New(ByVal pIdinventario As Integer, ByVal pTipo As Byte, ByVal pidDetalle As Integer, ByVal pIdDocumento As Integer)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        IdInventarioP = pIdinventario
        Tipo = pTipo
        IdDetalleV = pidDetalle
        IdDocumento = pIdDocumento
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmInventarioDetalles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            BuscaArticuloBoton()
        End If
    End Sub
    Private Sub frmInventarioProductos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        If Tipo >= 1 Then
            Panel1.Visible = False
            Button9.Visible = False
        End If
        NuevoConcepto()
        ConsultaDetalles()
    End Sub
    Private Sub BuscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
        Dim B As New frmBuscador(TipodeBusqueda, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'Select Case B.Tipo
            '   Case "I"
            LlenaDatosArticulo(B.Inventario)
            'TextBox4.Focus
            'End Select
        End If
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        'Dim a As dbInventarioPrecios
        Dim Cant As Double
        TextBox4.Text = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If
        idInventario = Articulo.ID
        ConsultaOn = False
        TextBox3.Text = Articulo.Clave
        TextBox3.Select(TextBox3.Text.Length, 0)
        ConsultaOn = True
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
        If e.KeyCode = Keys.F1 Then
            BuscaArticuloBoton()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        BuscaArticuloBoton()
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0) Then
                    LlenaDatosArticulo(p)
                Else
                    idInventario = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        'If (PermisosInventario And CULng((Math.Pow(2, perInventario.Ajuste_Inventario + 2)))) <> 0 Then
        Try
            Dim CD As New dbInventarioDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            'Dim InvAnt As Double
            If idInventario = 0 Then
                MsgError += "Debe indicar un artículo."
                HayError = True
            End If
            If IsNumeric(TextBox5.Text) = False Then
                'If CDbl(TextBox5.Text) <= 0 And My.Settings.conceptocero = False Then
                '    MsgError += "La cantidad debe ser un valor mayor a 0."
                '    HayError = True
                'End If
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If HayError = False Then
                If Button4.Text = "Agregar Artículo" Then
                    CD.Guardar(idInventario, CDbl(TextBox5.Text), IdInventarioP)
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text))
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
        TextBox3.Text = ""
        TextBox5.Text = "1"
        TextBox4.Text = ""
        'Button4.Text = "Agregar Artículo"
        'Button9.Enabled = False
        TextBox5.Focus()
    End Sub
    Private Sub ConsultaDetalles()
        Try
            Dim PrimerCeldaRow As Integer = -1
            Dim ID As New dbInventarioDetalles(MySqlcon)
            If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
            If Tipo = 0 Then
                DGDetalles.DataSource = ID.Consulta(IdInventarioP)
                DGDetalles.Columns(0).Visible = False
                DGDetalles.Columns(1).HeaderText = "Cantidad"
                DGDetalles.Columns(2).HeaderText = "Código"
                DGDetalles.Columns(3).HeaderText = "Descripción"
                DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            If Tipo = 1 Then
                DGDetalles.DataSource = ID.ConsultaAgregadosVentas(IdDetalleV)
                DGDetalles.Columns(0).Visible = False
                DGDetalles.Columns(1).HeaderText = "Cantidad"
                DGDetalles.Columns(2).HeaderText = "Código"
                DGDetalles.Columns(3).HeaderText = "Descripción"
                DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            If Tipo = 2 Then
                DGDetalles.DataSource = ID.ConsultaAgregadosRemisiones(IdDetalleV)
                DGDetalles.Columns(0).Visible = False
                DGDetalles.Columns(1).HeaderText = "Cantidad"
                DGDetalles.Columns(2).HeaderText = "Código"
                DGDetalles.Columns(3).HeaderText = "Descripción"
                DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            If Tipo = 4 Then
                DGDetalles.DataSource = ID.ConsultaAgregadosPedidos(IdDetalleV)
                DGDetalles.Columns(0).Visible = False
                DGDetalles.Columns(1).HeaderText = "Cantidad"
                DGDetalles.Columns(2).HeaderText = "Código"
                DGDetalles.Columns(3).HeaderText = "Descripción"
                DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            If Tipo = 3 Then
                DGDetalles.DataSource = ID.ConsultaAgregadosCotizaciones(IdDetalleV)
                DGDetalles.Columns(0).Visible = False
                DGDetalles.Columns(1).HeaderText = "Cantidad"
                DGDetalles.Columns(2).HeaderText = "Código"
                DGDetalles.Columns(3).HeaderText = "Descripción"
                DGDetalles.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            IdDetalle = 0
            ' End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick

        IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
        LlenaDAtos(IdDetalle)
    End Sub
    Private Sub LlenaDAtos(ByVal pIddetalle As Integer)
        Select Case Tipo
            Case 0
                'Al vuelo
                Dim Ide As New dbInventarioDetalles(pIddetalle, MySqlcon)
                Dim I As New dbInventario(Ide.Idinventario, MySqlcon)
                TextBox5.Text = Ide.Cantidad.ToString
                TextBox3.Text = I.Clave
                idInventario = Ide.Idinventario
                Button9.Enabled = True
                Button4.Text = "Modificar Ar"
            Case 1
                'fctura
                Dim vI As New dbVentasKits(MySqlcon)
                vI.ID = pIddetalle
                vI.LlenaDatos()
                TextBox5.Text = vI.Cantidad.ToString
                idInventario = vI.Idinventario
                Dim I As New dbInventario(idInventario, MySqlcon)
                If I.ManejaSeries = 1 Then
                    Button2.Visible = True
                Else
                    Button2.Visible = False
                End If
            Case 2
                'remision
                Dim vI As New dbVentasKits(MySqlcon)
                vI.ID = pIddetalle
                vI.LlenaDatosRemision()
                TextBox5.Text = vI.Cantidad.ToString
                idInventario = vI.Idinventario
                Dim I As New dbInventario(idInventario, MySqlcon)
                If I.ManejaSeries = 1 Then
                    Button2.Visible = True
                Else
                    Button2.Visible = False
                End If
            Case 3
                'cotizacion
                Dim vI As New dbVentasKits(MySqlcon)
                vI.ID = pIddetalle
                vI.LlenaDatosCotizacion()
                TextBox5.Text = vI.Cantidad.ToString
                idInventario = vI.Idinventario
                Dim I As New dbInventario(idInventario, MySqlcon)
                If I.ManejaSeries = 1 Then
                    Button2.Visible = True
                Else
                    Button2.Visible = False
                End If
        End Select
        
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If IdDetalle <> 0 Then
            If MsgBox("¿Remover este artículo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim ide As New dbInventarioDetalles(MySqlcon)
                ide.Eliminar(IdDetalle)
                ConsultaDetalles()
                NuevoConcepto()
                PopUp("Artítulo removido.", 90)
            End If
        Else
            MsgBox("Debe seleccionar un renglon", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Tipo = 1 Then
            Dim F As New frmVentasAsignaSeries(idInventario, IdDocumento, 0, CDbl(TextBox5.Text))
            F.ShowDialog()
            F.Dispose()
        End If
        If Tipo = 2 Then
            Dim F As New frmVentasAsignaSeries(idInventario, 0, IdDocumento, CDbl(TextBox5.Text))
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        NuevoConcepto()
    End Sub
End Class