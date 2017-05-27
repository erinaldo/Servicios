Public Class frmDetallesEquipos
    Dim ConsultaOn As Boolean = True
    Dim IdDetalle As Integer
    Dim idInventario As Integer
    Dim TipoEquipo As Integer
    '  Dim IdInventarioP As Integer
    ' Dim Tipo As Byte
    '  Dim IdDetalleV As Integer
    '  Dim IdDocumento As Integer
    Dim idEquipo As Integer
    Dim i As New dbInventario(MySqlcon)
    Private Sub frmDetallesEquipos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        filtro()
        NuevoConcepto()
        txtCantidad.Focus()
    End Sub

    Public Sub New(ByVal pIdinventario As Integer, ByVal pTipoEquipo As Integer)
        InitializeComponent()
        idEquipo = pIdinventario
        TipoEquipo = pTipoEquipo
    End Sub
    Private Sub filtro()
        Try
            Dim PrimerCeldaRow As Integer = -1
            Dim P As New dbDetallesEquipo(MySqlcon)
            If DGDetalles.RowCount > 0 Then PrimerCeldaRow = DGDetalles.FirstDisplayedCell.RowIndex
            Dim Dt As New DataTable
            Dim TablaFull As New DataTable
            TablaFull.Columns.Add("idDetalle")
            TablaFull.Columns.Add("idEquipo")
            TablaFull.Columns.Add("Cantidad")
            TablaFull.Columns.Add("Código")
            TablaFull.Columns.Add("Descripcion")
            TablaFull.Columns.Add("Tiempo vida")
            If TipoEquipo = 1 Then
                'Equipos clientes
                Dt = P.detallesEquipo(idEquipo)
            Else
                'equipos sucursales
                Dt = P.detallesEquipoSucursale(idEquipo)
            End If


            For i As Integer = 0 To Dt.Rows.Count() - 1
                Dim dr1 As DataRow
                dr1 = TablaFull.NewRow()
                dr1("idDetalle") = Dt.Rows(i)(0).ToString
                dr1("idEquipo") = Dt.Rows(i)(1).ToString
                dr1("Cantidad") = Dt.Rows(i)(2).ToString
                P.buscarArticulo(Dt.Rows(i)(3).ToString)
                dr1("Código") = P.codigo
                dr1("Descripcion") = P.descripcion
                dr1("Tiempo vida") = Dt.Rows(i)(4).ToString

                TablaFull.Rows.Add(dr1)
            Next

            DGDetalles.DataSource = TablaFull
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).HeaderText = "idEquipo"
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).HeaderText = "Cantidad"
            DGDetalles.Columns(3).HeaderText = "Codigo"
            DGDetalles.Columns(4).HeaderText = "Descripcion"
            DGDetalles.Columns(5).HeaderText = "Tiempo vida"
            DGDetalles.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            If DGDetalles.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DGDetalles.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub txtTiempo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTiempo.KeyPress
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        If Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        BuscaArticuloBoton()
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
            txtTiempo.Focus()
        End If
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        Dim Cant As Double
        txtDescripcion.Text = Articulo.Nombre
        If IsNumeric(txtCantidad.Text) Then
            Cant = CDbl(txtCantidad.Text)
        Else
            txtCantidad.Text = "1"
            Cant = 1
        End If
        idInventario = Articulo.ID
        txtArticulo.Text = Articulo.Clave
    End Sub

    Private Sub btnagregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarArticulo.Click

        BotonAgregar()
       
    End Sub
    Private Sub BotonAgregar()
        Try
            Dim CD As New dbDetallesEquipo(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            Dim ano As String
            Dim mes As String
            Dim dia As String
            Dim fecha As String
            ano = Date.Now.Year.ToString()
            mes = Date.Now.Month.ToString()
            dia = Date.Now.Day.ToString()
            fecha = ano + "-" + Format(Integer.Parse(mes), "00") + "-" + Format(Integer.Parse(dia), "00")
            If idInventario = 0 Then
                MsgError += "Debe indicar un artículo del inventario."
                HayError = True
            End If

            If IsNumeric(txtCantidad.Text) = False Then
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.SericiosDetallesEquiposAgregar, PermisosN.Secciones.Servicios) = False) And btnAgregarArticulo.Text = "Agregar Artículo" Then
                MsgError += "No tiene permiso para realizar esta operación."
                HayError = True
            End If
            If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.SericiosDetallesEquiposModificar, PermisosN.Secciones.Servicios) = False) And btnAgregarArticulo.Text <> "Agregar Artículo" Then
                MsgError += "No tiene permiso para realizar esta operación."
                HayError = True
            End If
            If HayError = False Then
                If btnAgregarArticulo.Text = "Agregar Artículo" Then
                    If TipoEquipo = 1 Then
                        'Equipos Clientes
                        CD.Guardar(idEquipo, Double.Parse(txtCantidad.Text), idInventario, txtTiempo.Text, fecha)
                    Else
                        'Equipos sucursales
                        CD.GuardarSucursal(idEquipo, Double.Parse(txtCantidad.Text), idInventario, txtTiempo.Text, fecha)
                    End If

                    filtro()
                    NuevoConcepto()
                    PopUp("Artículo agregado", 90)
                Else
                    If TipoEquipo = 1 Then
                        'Equipos clientes
                        CD.Modificar(IdDetalle, txtCantidad.Text, idInventario, txtTiempo.Text)
                    Else
                        'equipos sucursales
                        CD.ModificarSucursal(IdDetalle, txtCantidad.Text, idInventario, txtTiempo.Text)
                    End If

                    NuevoConcepto()
                    filtro()
                    PopUp("Artículo modificado", 90)
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub NuevoConcepto()
        txtCantidad.Text = "1"
        txtArticulo.Text = ""
        txtDescripcion.Text = ""
        txtTiempo.Text = ""
        idInventario = 0
        IdDetalle = 0
        btnAgregarArticulo.Text = "Agregar Artículo"
        txtArticulo.Focus()
    End Sub

    Private Sub DGDetalles_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellDoubleClick
        LlenaDatos()
    End Sub

    Private Sub LlenaDatos()
        Try
            IdDetalle = Integer.Parse(DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value)
            txtCantidad.Text = DGDetalles.Item(2, DGDetalles.CurrentCell.RowIndex).Value
            txtArticulo.Text = DGDetalles.Item(3, DGDetalles.CurrentCell.RowIndex).Value
            txtDescripcion.Text = DGDetalles.Item(4, DGDetalles.CurrentCell.RowIndex).Value
            txtTiempo.Text = DGDetalles.Item(5, DGDetalles.CurrentCell.RowIndex).Value
            Dim P As New dbDetallesEquipo(MySqlcon)
            If TipoEquipo = 1 Then
                idInventario = P.buscarIDInventario(idEquipo, IdDetalle)
            Else
                idInventario = P.buscarIDInventarioSucursal(idEquipo, IdDetalle)
            End If

            btnAgregarArticulo.Text = "Editar artículo"
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub btnRemover_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemover.Click
        If (GlobalPermisos.ChecaPermiso(PermisosN.Servicios.SericiosDetallesEquiposEliminar, PermisosN.Secciones.Servicios) = True) Then
            If IdDetalle <> 0 Then
                If MsgBox("¿Remover este artículo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim P As New dbDetallesEquipo(MySqlcon)
                    If TipoEquipo = 1 Then
                        P.Eliminar(IdDetalle)

                    Else
                        P.EliminarSucursal(IdDetalle)

                    End If

                    NuevoConcepto()
                    filtro()
                    PopUp("Artítulo removido.", 90)
                End If
            Else
                MsgBox("Debe seleccionar un renglon", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        NuevoConcepto()
        filtro()
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub txtArticulo_TextChanged(sender As Object, e As EventArgs) Handles txtArticulo.TextChanged
        If i.BuscaArticulo(txtArticulo.Text, 0) Then
            LlenaDatosArticulo(i)
        End If
    End Sub

    Private Sub txtArticulo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtArticulo.KeyDown
        If e.KeyCode = Keys.F1 Then
            BuscaArticuloBoton()
        End If
        If e.KeyCode = Keys.Enter Then
            txtTiempo.Focus()
        End If
    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtArticulo.Focus()
        End If
    End Sub

    Private Sub txtTiempo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTiempo.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub
End Class