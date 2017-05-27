﻿Public Class frmVentasPedidos
    Dim IdsVariantes As New elemento
    Dim idPedido As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim IdsVendedores As New elemento
    Dim idCliente As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    Dim IdVariante As Integer
    Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim PrecioBase As Double
    Dim Tabla As New DataTable
    Dim IdsSucursales As New elemento
    Dim IdLista As Integer
    Dim Sobre As Byte
    Dim SIVA As Double
    Dim UsaFormula As Byte
    Dim PrecioNeto As Byte
    Dim EsKit As Byte
    Dim SeparaKit As Byte
    Dim Op As dbOpciones
    Dim P As New dbDescuentos(MySqlcon)
    Dim CD As New dbVentasPedidosInventario(MySqlcon)
    Dim promocion1 As Integer
    Dim promocion2 As Integer
    Dim nombreProducto As String
    Dim cantAntModificar As Double
    Dim tipoElimianr As String
    Dim ArtNombre As String
    Dim ImpDoc As ImprimirDocumento
    Dim IdVendedorU As Integer
    Public Sub New(Optional ByVal pidVenta As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idPedido = pidVenta
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este pedido no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbVentasPedidos(MySqlcon)
                c.Eliminar(idPedido)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                e.Cancel = False
            Else
                GlobalEstadoVentanas = GlobalEstadoVentanas And Not 4
                e.Cancel = True
            End If
        Else
            GlobalEstadoVentanas = GlobalEstadoVentanas And Not 4
        End If
    End Sub

    Private Sub frmVentasPedidos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F12 Then
            If idCliente <> 0 Then
                Dim Cl As New frmClientesConsultaArticulos(idCliente, IdInventario)
                Cl.ShowDialog()
                Cl.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled = True Then
                Modificar(Estados.Guardada)
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled = True Then
                Modificar(Estados.Pendiente)
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
        If e.KeyCode = Keys.F6 And IdInventario <> 0 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas) = True Then
                Dim f As New frmInventarioConsulta(IdInventario)
                f.ShowDialog()
                f.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F7 And IdInventario <> 0 And EsKit <> 0 And IdDetalle <> 0 Then
            Dim IDe As New frmInventarioDetalles(IdInventario, 4, IdDetalle, idPedido)
            IDe.ShowDialog()
            IDe.Dispose()
        End If
        If e.KeyCode = Keys.F8 Then
            If GlobalTipoVersion = 0 Then
                Dim f As New frmCompras
                f.ShowDialog()
            End If
        End If
    End Sub
    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Op = New dbOpciones(MySqlcon)
        Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        IdVendedorU = U.IdVendedor
        ImpDoc = New ImprimirDocumento
        Tabla.Columns.Add("Id", I.GetType)
        Tabla.Columns.Add("TipoR", S.GetType)
        Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Precio U.", S.GetType)
        Tabla.Columns.Add("Importe", S.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1")
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(IdAlmacenG)
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodePrecio, PermisosN.Secciones.Ventas) = False Then
            TextBox12.ReadOnly = True
        End If
        ConsultaOn = True
        If idPedido = 0 Then
            Nuevo()
            NuevoConcepto()
        Else
            LlenaDatosVenta()
        End If
        Panel3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                'Dim T As Double
                'Dim Iva As Double
                Dim V As New dbVentasPedidos(MySqlcon)
                V.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)

                'If CheckBox1.Checked Then
                'Iva = T - (T / (1 + (O.Imp / 100)))
                Label12.Text = Format(V.Subtotal, "#,###,##0.00")
                Label13.Text = Format(V.TotalIva, "#,###,##0.00")
                Label14.Text = Format(V.TotalVenta, "#,###,##0.00")
                Label32.Text = Format(V.TotalPeso, "#,##0.00") + "Kg."
                'Else
                'Iva = T * (O.Imp / 100)
                '   Label12.Text = System.Math.Round(T, 2).ToString
                '   Label13.Text = System.Math.Round(Iva, 2).ToString
                '   Label14.Text = Format(System.Math.Round(T + Iva, 2), "#,###0.00")
                'End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)

        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox2.Enabled = True
        TextBox11.Enabled = True
        Label32.Text = "0.00Kg."
        DateTimePicker1.Value = Date.Now
        TextBox2.Text = ""
        TextBox1.Text = ""
        TextBox14.Text = ""
        FolioAnt = 0
        ComboBox5.SelectedIndex = 0
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.VentasPedidos, 0) Then
            TextBox11.Text = Sf.Serie
        Else
            TextBox11.Text = ""
        End If
        Dim V As New dbVentasPedidos(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        idPedido = 0
        Label24.Visible = False
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        CheckBox1.Checked = False
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        Panel1.Enabled = True
        Panel2.Enabled = True
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        CheckBox1.Checked = False
        'Dim o As New dbOpciones(MySqlcon)
        'TextBox8.Text = o.Imp.ToString
        NuevoConcepto()

        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
            TextBox11.Enabled = False
            TextBox2.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirDescuento, PermisosN.Secciones.Ventas) = False Then
            TextBox9.Enabled = False
        End If
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._CursorVentas = "0" Then
                TextBox5.Focus()
            Else
                TextBox3.Focus()
            End If
        End If
        If e.KeyCode = Keys.F1 Then
            BotonCliente()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(TextBox1.Text) Then
                    If c.DireccionFiscal = 0 Then
                        TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    Else
                        TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    End If
                    If c.NoChecarCr = 0 Then
                        TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00") + vbCrLf + "A Favor: " + Format(c.DaSaldoAFavor(c.ID), "#,##0.00")
                    Else
                        TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00")
                    End If
                    idCliente = c.ID
                    IdLista = c.IdLista
                    SIVA = c.IVA
                    Sobre = c.SobreescribeIVA
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                        Else
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(c.IdVendedor)
                        End If

                    End If
                Else
                    TextBox7.Text = ""
                    TextBox13.Text = ""
                    idCliente = 0
                    IdLista = 0
                    SIVA = 0
                    Sobre = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbVentasPedidos(MySqlcon)
            Dim Desglozar As Byte
            'If TextBox2.Text = "" Then MensajeError = "Debe indicar un folio."
            'If FolioAnt <> TextBox2.Text Then
            '    If C.ChecaFolioRepetido(TextBox2.Text) Then
            '        MensajeError += " Ya existe una compra con este folio."
            '        TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            '        Label17.Visible = True
            '    End If
            'End If
            If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosCancelar, PermisosN.Secciones.Ventas) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If C.RevisaConceptos(idPedido) = False Then
                MensajeError += " Hay conceptos agregados en monedas diferentes favor de verificarlo."
            Else
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.DaMoneda(idPedido))
            End If
            If MensajeError = "" Then
                If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                    'Dim Sf As New dbSucursalesFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
                    'TextBox11.Text = Sf.Serie
                    If pEstado = Estados.Guardada Then TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                End If
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                'Dim O As New dbOpciones(MySqlcon)
                C.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Modificar(idPedido, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, Desglozar, CDbl(TextBox8.Text), pEstado, C.Subtotal, C.TotalVenta, idCliente, TextBox11.Text, IdsVendedores.Valor(ComboBox5.SelectedIndex), TextBox14.Text)
                If pEstado = Estados.Guardada Then Imprimir(idPedido)
                'If pEstado = Estados.Cancelada Then
                ' C.RegresaInventario(idCotizacion)
                'End If
                If pEstado <> Estados.SinGuardar Then Nuevo()
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Guardar()
        Try

            'If Button1.Text = "Guardar" Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas) = True Then
                If idCliente <> 0 Then
                    Dim C As New dbVentasPedidos(MySqlcon)
                    Dim Desglozar As Byte
                    If CheckBox1.Checked Then
                        Desglozar = 1
                    Else
                        Desglozar = 0
                    End If

                    If C.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox11.Text) Then
                        TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                        Label17.Visible = True
                        FolioAnt = 0
                    Else
                        FolioAnt = CInt(TextBox2.Text)
                    End If
                    Dim O As New dbOpciones(MySqlcon)
                    C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, Desglozar, CDbl(TextBox8.Text), IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text, 1, IdsVendedores.Valor(ComboBox5.SelectedIndex))
                    idPedido = C.ID
                    Estado = 1
                    'Button1.Text = "Modificar"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    Button14.Enabled = True
                    'LlenaDatosDetalles()
                Else
                    MsgBox("Debe indicar un cliente", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IdInventario <> 0 Then
                If UsaFormula = 1 Then
                    Dim Fo As New frmInventarioFormula01(ArtNombre)
                    If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TextBox5.Text = Format(Fo.Resultado, "0.00")
                        TextBox4.Text = Fo.FormulaString
                    End If
                    'TextBox12.Focus()
                End If
            End If
            TextBox12.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            botonarticulo()
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                TextBox12.Focus()
            End If
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub
    
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 0, False, True, False, False) Then
                    LlenaDatosArticulo(p)
                Else
                    IdInventario = 0

                    TextBox4.Text = ""
                    TextBox6.Text = "0"
                    TextBox8.Text = "0"
                    TextBox9.Text = "0"
                    PrecioU = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub LlenaDatosVenta()
        Dim C As New dbVentasPedidos(idPedido, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio.ToString
        FolioAnt = C.Folio
        TextBox11.Text = C.Serie
        TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        'TextBox8.Text = C.Iva.ToString
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        TextBox14.Text = C.Comentario

        ConsultaDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas) = False Then
                    TextBox11.Enabled = False
                    TextBox2.Enabled = False
                End If
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
                    ComboBox3.Enabled = False
                End If
        End Select

    End Sub

    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbVentasPedidosInventario(MySqlcon)
            T = CD.ConsultaReader(idPedido)
            While T.Read
                If T("cantidad") <> 0 Then
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    Else
                        Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("pclave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    End If
                Else
                    If T("idinventario") > 1 Then
                        Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    Else
                        Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("pclave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    End If
                End If
            End While
            T.Close()
            'Dim VP As New dbVentasPedidosProductos(MySqlcon)
            'T = VP.ConsultaReader(idPedido)
            'While T.Read
            '    Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("clave"), T("descripcion"), T("precio"), T("abreviatura"))
            'End While
            'T.Close()
            'Dim VS As New dbVentasServicios(MySqlcon)
            'T = VS.ConsultaReader(idCotizacion)
            'While T.Read
            '    Tabla.Rows.Add(T("idventasservicio"), "S", "", T("cantidad"), T("folio"), T("descripcion"), T("precio"), T("abreviatura"))
            'End While
            'T.Close()
            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(4).Width = 80
            DGDetalles.Columns(8).Width = 80
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        IdVariante = 0
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        UsaFormula = 0
        PrecioU = 0
        PrecioBase = 0
        PrecioNeto = 0
        Button12.Visible = False
        Button6.Enabled = True
        ArtNombre = ""
        TextBox3.Enabled = True
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        TextBox8.Text = "0"
        SeparaKit = 0
        TextBox9.Text = "0"
        TextBox12.Text = "0"
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas) = False Then
            TextBox4.Enabled = False
            Button16.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PermitirDescuento, PermisosN.Secciones.Ventas) = False Then
            TextBox9.Enabled = False
        End If
        If Op._CursorVentas = "0" Then
            TextBox5.Focus()
        Else
            TextBox5.Text = "1"
            TextBox3.Focus()
        End If
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbVentasPedidosInventario(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox6.Text) = False Then
                MsgError += vbCrLf + "El costo debe ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox6.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El costo debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If IsNumeric(TextBox9.Text) = False Then
                MsgError += vbCrLf + "El descuento debe ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox9.Text) <> 0 Then
                    TextBox12.Text = CStr(CDbl(TextBox12.Text) - (CDbl(TextBox12.Text) * CDbl(TextBox9.Text) / 100))
                End If
            End If
            If IsNumeric(txtIEPS.Text) = False Then
                MsgError += vbCrLf + "El IEPS debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(txtIVARetenido.Text) = False Then
                MsgError += vbCrLf + "El IVA Retenido debe ser un valor numérico."
                HayError = True
            End If
            If HayError = False Then
                If PrecioNeto = 1 Then
                    Dim Temp As Double
                    'Temp = CStr(CDbl(TextBox6.Text) / (1 + CDbl(TextBox8.Text) / 100) / CDbl(TextBox5.Text))
                    Temp = CStr(CDbl(TextBox6.Text) / (1 + (CDbl(TextBox8.Text) + CDbl(txtIEPS.Text) - CDbl(txtIVARetenido.Text)) / 100) / CDbl(TextBox5.Text))
                    TextBox12.Text = Temp.ToString
                End If
                If Button4.Text = "Agregar Concepto" Then
                    If SeparaKit = 0 Then
                        CD.Guardar(idPedido, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text)) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                        hayDescuento()
                    Else
                        If EsKit = 1 And SeparaKit = 1 Then
                            CD.SeparaKit(idPedido, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, CDbl(txtIEPS.Text), CDbl(txtIVARetenido.Text))
                        End If
                    End If
                    If EsKit = 1 And SeparaKit = 0 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.InsertarArticulosPedidos(IdInventario, idPedido, CD.ID, CDbl(TextBox5.Text), 1)
                    End If
                    'If ManejaSeries <> 0 Then
                    '    If CD.NuevoConcepto Then
                    '        Dim F As New frmInventarioSeries(IdInventario, 0, idCotizacion, CInt(TextBox5.Text))
                    '        F.ShowDialog()
                    '    Else
                    '        Dim F As New frmInventarioSeries(IdInventario, 0, idCotizacion, CD.Cantidad)
                    '        F.ShowDialog()
                    '    End If
                    'End If
                    'Dim I As New dbInventario(MySqlcon)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Baja, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
                    If tipoElimianr = "Promocion" Then
                        modificarDescuento(P.descModificar(IdDetalle, "Pedidos"))
                    Else
                        If P.descModificar(IdDetalle, "Pedidos") <> 0 Then
                            modificarDescuento(P.descModificar(IdDetalle, "Pedidos"))
                        End If
                    End If
                    If EsKit = 1 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.ModificaArtículosPedido(IdDetalle, CDbl(TextBox5.Text), IdInventario)
                    End If
                    'If ManejaSeries <> 0 Then
                    '    Dim F As New frmInventarioSeries(IdInventario, 0, idCotizacion, CDbl(TextBox5.Text))
                    '    F.ShowDialog()
                    'End If
                    'Dim I As New dbInventario(MySqlcon)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
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
    End Sub

    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas) = True Then

            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                If IdInventario <> 0 Or IdVariante <> 0 Then AgregaArticulo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try
            Button6.Enabled = False
            TextBox3.Enabled = False
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            'Tipo = DGDetalles.Item(1, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbVentasPedidosInventario(IdDetalle, MySqlcon)
            TextBox5.Text = CD.Cantidad.ToString
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            IdInventario = CD.Idinventario
            IdVariante = CD.IdVariante
            ArtNombre = CD.Inventario.Nombre
            If IdInventario > 1 Then
                TextBox3.Text = CD.Inventario.Clave
                PrecioNeto = CD.Inventario.PrecioNeto
                EsKit = CD.Inventario.EsKit
                IdVariante = 0
            Else
                PrecioNeto = 0
            End If
            TextBox8.Text = CD.Iva
            If IdInventario <> 1 Then
                UsaFormula = CD.Inventario.UsaFormula
            End If

            If CD.Descuento = 0 Then
                If PrecioNeto = 0 Then
                    PrecioU = Math.Round(CD.Precio / CD.Cantidad, 2)
                Else
                    PrecioU = Math.Round(CD.Precio / CD.Cantidad * (1 + (CD.Iva + CD.IEPS - CD.IVARetenido) / 100), 2)
                End If
            Else
                Dim Val As Double = (CD.Precio / (1 - CD.Descuento / 100))
                If PrecioNeto = 0 Then
                    PrecioU = Math.Round(Val / CD.Cantidad, 2)
                Else
                    PrecioU = Math.Round(Val / CD.Cantidad * (1 + (CD.Iva + CD.IEPS - CD.IVARetenido) / 100), 2)
                End If
            End If
            TextBox12.Text = PrecioU.ToString("0.00")
            PrecioBase = PrecioU
            TextBox9.Text = CD.Descuento.ToString
            txtIEPS.Text = CD.IEPS.ToString
            txtIVARetenido.Text = CD.IVARetenido.ToString
            CantAnt = CD.Cantidad

            'IdAlmacen = CD.IdAlmacen
            If PrecioNeto = 0 Then
                If CD.Descuento = 0 Then
                    TextBox6.Text = CD.Precio.ToString("0.00")
                Else
                    TextBox6.Text = Format(PrecioU * CD.Cantidad, "0.00")
                End If
            End If
            Button4.Text = "Modificar Concepto"
            TextBox4.Text = CD.Descripcion

            nombreProducto = TextBox4.Text

            cantAntModificar = Double.Parse(TextBox5.Text)

            If CD.Inventario.Inventariable = 1 Then
                If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas) = False Then
                    TextBox4.Enabled = False
                    Button16.Enabled = False
                Else
                    TextBox4.Enabled = True
                    Button16.Enabled = True
                End If
            Else
                TextBox4.Enabled = True
                Button16.Enabled = True
            End If

            'Select Case Tipo
            '    Case "A"

            '    Case "P"
            '        Dim CD As New dbVentasPedidosProductos(IdDetalle, MySqlcon)

            '        PrecioU = CD.Precio / CD.Cantidad
            '        TextBox5.Text = CD.Cantidad.ToString
            '        TextBox4.Text = CD.Descripcion
            '        TextBox8.Text = CD.iva
            '        TextBox9.Text = CD.Descuento
            '        IdVariante = CD.IdVariante
            '        CantAnt = CD.Cantidad
            '        'IdAlmacen = CD.IdAlmacen
            '        TextBox6.Text = CD.Precio.ToString
            '        Button4.Text = "Modificar Concepto"
            '        ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            '        cmbVariante.Enabled = False
            '        'Case "S"
            '        '    Dim CD As New dbVentasServicios(IdDetalle, MySqlcon)
            '        '    Dim S As New dbServicios(CD.IdServicio, MySqlcon)
            '        '    TextBox3.Text = S.Folio
            '        '    TextBox5.Text = CD.Cantidad.ToString
            '        '    TextBox4.Text = CD.Descripcion
            '        '    IdServicio = CD.IdServicio
            '        '    CantAnt = CD.Cantidad
            '        '    TextBox6.Text = CD.Precio.ToString
            '        '    Button4.Text = "Modificar Concepto"
            '        '    cmbtipoarticulo.Text = "S"
            '        '    ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            'End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este pedido no ha sido guardado. ¿Desea iniciar un nuevo pedido? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbVentasPedidos(MySqlcon)
                c.Eliminar(idPedido)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        BotonNuevo()

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas) = True Then
                If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbVentasPedidosInventario(MySqlcon)
                    If EsKit = 1 Then
                        Dim IKits As New dbVentasKits(MySqlcon)
                        IKits.EliminarArticulosPedidos(IdDetalle)
                    End If
                    tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    CD.Eliminar(IdDetalle)
                    If tipoElimianr = "Promocion" Then

                        eliminarDescuescuento(P.descModificar(IdDetalle, "Pedidos"), tipoElimianr)
                    Else

                        If P.descModificar(IdDetalle, "Pedidos") <> 0 Then
                            eliminarDescuescuento(P.descModificar(IdDetalle, "Pedidos"), tipoElimianr)
                        End If
                    End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    PopUp("Concepto Eliminado", 90)
                End If

            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub BotonCliente()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            If B.Cliente.DireccionFiscal = 0 Then
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            Else
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            End If
            If B.Cliente.NoChecarCr = 0 Then
                TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00") + vbCrLf + "A Favor: " + Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "#,##0.00")
            Else
                TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00")
            End If
            idCliente = B.Cliente.ID
            IdLista = B.Cliente.IdLista
            Sobre = B.Cliente.SobreescribeIVA
            SIVA = B.Cliente.IVA
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                Else
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(B.Cliente.IdVendedor)
                End If
            End If
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            If Op._CursorVentas = "0" Then
                TextBox5.Focus()
            Else
                TextBox3.Focus()
            End If
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BotonCliente()
    End Sub
    Private Sub BotonArticulo()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
        If Op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                        'TextBox12.Focus()
                    
                End Select
                'If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
                If UsaFormula = 1 Then
                    Dim Fo As New frmInventarioFormula01(ArtNombre)
                    If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TextBox5.Text = Format(Fo.Resultado, "0.00")
                        TextBox4.Text = Fo.FormulaString
                    End If
                    'TextBox12.Focus()
                    'End If
                End If
                TextBox12.Focus()
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, True, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                        'TextBox12.Focus()
                    
                End Select
                'If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
                If UsaFormula = 1 Then
                    Dim Fo As New frmInventarioFormula01(ArtNombre)
                    If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TextBox5.Text = Format(Fo.Resultado, "0.00")
                        TextBox4.Text = Fo.FormulaString
                    End If
                    'TextBox12.Focus()
                    'End If
                End If
                TextBox12.Focus()
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        BotonArticulo()
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        Dim a As New dbInventarioPrecios(MySqlcon)
        a.BuscaPrecio(Articulo.ID, IdLista)
        Dim Cant As Double
        TextBox4.Text = Articulo.Nombre
        ArtNombre = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If

        Dim Desc As Double = 0
        Dim cdesc As New dbClientesDescuentos(MySqlcon)
        Desc = cdesc.buscaDescuento(idCliente, Articulo.Clasificacion.ID, Articulo.Clasificacion2.ID, Articulo.Clasificacion3.ID)
        If Desc = -1000 Then
            Desc = cdesc.buscaDescuento(idCliente, Articulo.Clasificacion.ID, Articulo.Clasificacion2.ID, 0)
            If Desc = -1000 Then
                Desc = cdesc.buscaDescuento(idCliente, Articulo.Clasificacion.ID, 0, 0)
                If Desc = -1000 Then
                    Desc = 0
                End If
            End If
        End If
        TextBox9.Text = Desc.ToString()

        PrecioU = Math.Round(a.Precio, 2)
        TextBox12.Text = Format(PrecioU, "0.00")
        PrecioBase = PrecioU
        UsaFormula = Articulo.UsaFormula
        ManejaSeries = Articulo.ManejaSeries
        TextBox6.Text = Format(Cant * PrecioU, "0.00")
        PrecioNeto = Articulo.PrecioNeto
        EsKit = Articulo.EsKit
        SeparaKit = Articulo.SepararKit
        txtIEPS.Text = Articulo.ieps.ToString
        txtIVARetenido.Text = Articulo.ivaRetenido.ToString
        ComboBox1.SelectedIndex = IDsMonedas.Busca(a.IdMoneda)
        If Sobre = 0 Then
            TextBox8.Text = Articulo.Iva.ToString
        Else
            TextBox8.Text = SIVA.ToString
        End If
        IdInventario = Articulo.ID
        If ManejaSeries = 0 Then
            Button12.Visible = False
        Else
            Button12.Visible = True
        End If
        If Articulo.Inventariable = 1 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioDescripcion, PermisosN.Secciones.Ventas) = False Then
                TextBox4.Enabled = False
            Else
                TextBox4.Enabled = True
            End If
        Else
            TextBox4.Enabled = True
        End If
        ConsultaOn = False
        TextBox3.Text = Articulo.Clave
        TextBox3.Select(TextBox3.Text.Length, 0)
        ConsultaOn = True
    End Sub
    
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SacaTotal()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        SacaTotal()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmVentasPedidosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idPedido = f.IdPedidos
            LlenaDatosVenta()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas) = True Then
                Dim C As New dbVentasPedidos(MySqlcon)
                C.Eliminar(idPedido)
                P.limpiarDescPromociones()
                P.limpiarVentasdesc()
                PopUp("Pedido Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        'Dim F As New frmInventarioSeries(IdInventario, 0, idPedido, CDbl(TextBox5.Text), DateTimePicker1.Value)
        'F.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MsgBox("¿Cancelar este pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada)
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Enabled = True Then
                TextBox3.Focus()
            Else
                TextBox12.Focus()
            End If
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                ' TextBox12.Focus()
            End If
        End If
    End Sub


    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim Forma As New frmBuscaDocumentoVenta(0, False, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, False, True, True, 0, False, "", False)
            If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim V As New dbVentasPedidos(MySqlcon)
                If Estado = 0 Then
                    '0 cotizacion
                    '1 pedido
                    '2 remision
                    '3 ventas
                    Select Case Forma.Tipo
                        Case 0
                            Dim Co As New dbVentasCotizaciones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Co.Cliente.Clave
                        Case 1
                            Dim Cp As New dbVentasPedidos(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cp.Cliente.Clave
                        Case 2
                            Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cr.Cliente.Clave
                        Case 3

                            Dim Cv As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
                            TextBox1.Text = Cv.Cliente.Clave
                    End Select
                    Guardar()
                    If Estado <> 0 Then
                        V.AgregarDetallesReferencia(idPedido, Forma.id(0), Forma.Tipo)
                        ConsultaDetalles()
                    End If
                Else
                    V.AgregarDetallesReferencia(idPedido, Forma.id(0), Forma.Tipo)
                    ConsultaDetalles()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.F1 And IdInventario <> 0 Then
            Dim SP As New frmSelectorPrecios(IdInventario)
            If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox12.Text = SP.Precio.ToString("0.00")
            End If
        End If
        If e.KeyCode = Keys.Enter Then
            botonAgregar()
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                'TextBox12.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(TextBox12.Text) Then
                PrecioU = CDbl(TextBox12.Text)
                If IsNumeric(TextBox5.Text) Then
                    TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
                End If
            End If
        End If
    End Sub

    Private Sub TextBox9_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        'If ConsultaOn Then
        '    If IsNumeric(TextBox12.Text) And IsNumeric(TextBox9.Text) Then
        '        If PrecioBase <> 0 Then
        '            TextBox12.Text = CStr(PrecioBase - (PrecioBase * CDbl(TextBox9.Text) / 100))
        '        Else
        '            TextBox12.Text = CStr(PrecioU - (PrecioU * CDbl(TextBox9.Text) / 100))
        '        End If
        '        'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
        '    End If
        'End If
    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.VentasPedidos, 0)
        TextBox11.Text = Sf.Serie
        Dim V As New dbVentasPedidos(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
    End Sub

     Private Sub Imprimir(pIdPedido As Integer)
        Try
            Dim Cot As New dbVentasPedidos(pIdPedido, MySqlcon)
            Dim S As New dbSucursales(Cot.IdSucursal, MySqlcon)
            ImpDoc.IdSucursal = Cot.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.VentaPedido
            ImpDoc.TipoDocumentoT = TiposDocumentos.VentaPedido + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.VentaPedido
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.PedidosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "PSSPEDIDO " + Cot.Serie + Cot.Folio.ToString
            PrintDocument1.Print()
            If Cot.Cliente.Email <> "" Then
                Try
                    If MsgBox("¿Enviar pedido por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        If Cot.Cliente.Email <> "" Then
                            Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                            Dim C As String
                            C = "Eviado por: " + S.NombreFiscal + vbNewLine + "RFC: " + S.RFC + vbNewLine + "PEDIDO" + vbNewLine + "Folio: " + Cot.Serie + Cot.Folio.ToString + vbNewLine + Op.CorreoContenido
                            M.send("Pedido: " + Cot.Serie + Cot.Folio.ToString, C, Cot.Cliente.Email, Cot.Cliente.Nombre, ImpDoc.RutaPDF + "\PSSPEDIDO " + Cot.Serie + Cot.Folio.ToString + ".pdf", "")
                            PopUp("Correo Enviado", 90)
                        End If
                    End If
                Catch ex As Exception
                    MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                End Try
            End If
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ImpDoc.DibujaPaginaN(e.Graphics)
        If ImpDoc.MasPaginas = True Or ImpDoc.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(ImpDoc.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
        End If
        If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
            e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
        End If
        e.HasMorePages = ImpDoc.MasPaginas
    End Sub
    Private Sub LlenaNodosImpresion()

        Dim V As New dbVentasPedidos(idPedido, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
        Dim TotalDescuento As Double = 0
        Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "usuario", V.Usuario, 0), "usuario")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
            ImpDoc.ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "foliobarras", "*$" + V.Serie + V.Folio.ToString + "$*", 0), "foliobarras")
        Dim CodigoBarras As iTextSharp.text.pdf.Barcode128 = New iTextSharp.text.pdf.Barcode128
        Dim Nimagen As NodoImpresionN
        Nimagen = New NodoImpresionN("", "codigobarras1", "", 0)
        CodigoBarras.Code = "$" + V.Serie + V.Folio.ToString + "$"
        Try
            Nimagen.Imagen = CodigoBarras.CreateDrawingImage(Color.Black, Color.White)
        Catch ex As Exception

        End Try
        ImpDoc.ImpND.Add(Nimagen, "codigobarras1")
        'ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        'ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        'ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        'ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        'ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        'ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        'ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbVentasPedidosInventario(MySqlcon)
        DR = VI.ConsultaReader(idPedido)
        Dim TotalIEPS As Double = 0
        Dim TotalIVARetenido As Double = 0
        Dim Cont As Integer = 0
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            
            If DR("cantidad") <> 0 Then

                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidad")) * (1 + DR("iva") / 100), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + DR("iva") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))

                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(Op.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", Format(DR("precio") * (DR("ieps") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(DR("precio") * (DR("ivaretenido") / 100), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
                TotalIEPS += Double.Parse(DR("IEPS").ToString())
                TotalIVARetenido += Double.Parse(DR("IVARetenido").ToString())
            Else

                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciouiva", "", 0), "preciouiva" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importeiva", "", 0), "importeiva" + Format(Cont, "000"))

                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ieps", "", 0), "ieps" + Format(Cont, "000"))
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", "", 0), "ivaRetenido" + Format(Cont, "000"))
            End If

            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Totalieps", Format(V.TotalIeps, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "Totalieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(V.TotalIvaRetenidoConceptos, Op._formatoIva).PadLeft(Op.EspacioIva), 0), "TotalivaRetenido")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtotal + TotalDescuento, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "subtotalsindesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "totaldesc")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(idPedido).ToString, 0), "totalcantidad")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idPedido)
        Dim IAnt As Double
        While DR.Read
            If Ivas.Contains(DR("iva").ToString) = False Then
                Ivas.Add(DR("iva"), DR("iva").ToString)
            End If
            If IvasImporte.Contains(DR("iva").ToString) = False Then
                IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
            Else
                IAnt = IvasImporte(DR("iva").ToString)
                IvasImporte.Remove(DR("iva").ToString)
                IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
            End If
        End While
        DR.Close()
        Cont = 0
        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And Op._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "total")
        Dim CL As New CLetras
        If V.TotalVenta >= 0 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), 2, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(Format(V.TotalVenta * -1, "0.00"), 2, GlobalIdiomaLetras), 0), "totalletra")
        End If
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmClientes(1, idCliente)
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.CodigoCliente
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtIEPS.Focus()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Estado = Estados.SinGuardar Then
            Modificar(Estados.SinGuardar)
        End If
        Imprimir(idPedido)
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim et As New frmVentasTextoExtra(TextBox4.Text, 2000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox4.Text = et.Texto
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbVentasPedidos(MySqlcon)
                V.ActualizaComentario(idPedido, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub

    '*************Descuentos***********
    Private Sub hayDescuento()

        'Dim CD As New dbVentasInventario(MySqlcon)
        Dim idDescuento As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim TablaDesc As DataTable
        Dim des As Double = 0
        Dim descripcion As String = ""

        If idDescuento = 0 Then
            'No hay descuento
        Else
            TablaDesc = P.tablaDesc(idDescuento)
            If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

                If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
                    des = TotalPorcentaje(CDbl(TextBox6.Text), Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    'des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U"
                End If

                'CD.Guardar(idVenta, 1, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, Double.Parse(TextBox5.Text), 1)
                CD.Guardar(idPedido, 1, CDbl(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

                ConsultaDetalles()
                NuevoConcepto()

            Else
                nombreProducto = TextBox4.Text
                Promociones(Integer.Parse(TablaDesc.Rows(0)(10).ToString()), TablaDesc.Rows(0)(2).ToString(), CDbl(TextBox12.Text), "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, Integer.Parse(TablaDesc.Rows(0)(8).ToString()))
                'hay promocion
                'primero checar si se cumple la promocion
                'si no añadir

            End If

        End If


        'Si haye descuento, agregar el renglon a la venta
        'agregarlo a la tabla de descuentos

    End Sub
    Public Sub Promociones(ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer)
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        Dim cant As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim regAnadir As Integer = 0
        Dim regDesc As Integer = 0
        valorProm(valor) 'esto establece los valores 2 x 1  y esos
        'primero que agregue el renglon a la db
        cant = Int(Double.Parse(TextBox5.Text)) 'cantidad de productos que se estan registrando
        For i As Integer = 1 To cant
            P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
        Next


        If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

            regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
            regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

            cDesc = promocion1 - promocion2
            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
            des = des * regDesc

            'CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, regDesc, 1)
            CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

            ConsultaDetalles()
            NuevoConcepto()
            P.EliminarDesc(idPedido, idDescuento, idProducto)
            'anadir registros faltantes
            For i As Integer = 1 To regAnadir
                P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
            Next

        End If

    End Sub
    Public Sub modificarPromociones(ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer)
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        Dim regAnadir As Integer = 0
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim regDesc As Integer = 0
        Dim mayor As Integer
        valorProm(valor)
        If cantAntModificar <= Int(Double.Parse(TextBox5.Text)) Then
            mayor = Int(Double.Parse(TextBox5.Text)) - cantAntModificar 'los que estan de mas

            For i As Integer = 1 To mayor 'agregar los que se almacenaron
                P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
            Next

            If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

                regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
                regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

                cDesc = promocion1 - promocion2
                des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
                des = des * regDesc

                'CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, regDesc, 1)
                CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

                ConsultaDetalles()
                NuevoConcepto()
                P.EliminarDesc(idPedido, idDescuento, idProducto)
                'anadir registros faltantes
                For i As Integer = 1 To regAnadir
                    P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
                Next



            End If

        Else
            'hay que eliminar todos los registros de promociones y hacer los calculos otra vez
            P.EliminarDesc(idPedido, idDescuento, idProducto)
            P.EliminarDescAnadidosPed(idPedido, descripcion)
            Dim dt As DataTable
            Dim tot As Double = 0
            Dim tot2 As Integer
            dt = P.buscarDesAnadidosPed(idPedido, IdInventario)

            For i As Integer = 0 To dt.Rows.Count - 1
                tot = tot + Double.Parse(dt.Rows(i)(4).ToString())
            Next

            tot2 = Int(tot)

            For i As Integer = 1 To tot2 'agregar los que se almacenaron
                P.guardarPromocion(idPedido, idDescuento, idProducto, 0)
            Next

            If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

                regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
                regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

                cDesc = promocion1 - promocion2
                des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
                des = des * regDesc

                CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text)) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

                ConsultaDetalles()
                NuevoConcepto()
                P.EliminarDesc(idPedido, idDescuento, idProducto)
                'anadir registros faltantes
                For i As Integer = 1 To regAnadir
                    P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
                Next
            End If

        End If

    End Sub

    Public Sub eliminarPromocion()
        Dim cDesc As Integer = 0
        Dim des As Double = 0
        'Dim cant As Integer
        Dim regAnadir As Integer = 0
        Dim precio As Double
        Dim descripcion As String
        Dim idDescuento As Integer
        Dim idProducto As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim regDesc As Integer = 0
        'Dim mayor As Integer
        Dim TablaDesc As DataTable
        Dim valor As String
        TablaDesc = P.tablaDesc(idDescuento)
        valor = TablaDesc.Rows(0)(2).ToString()
        valorProm(valor)
        descripcion = "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto
        idProducto = Integer.Parse(TablaDesc.Rows(0)(8).ToString())
        precio = Double.Parse(TextBox12.Text)
        P.EliminarDesc(idPedido, idDescuento, idProducto)
        P.EliminarDescAnadidosPed(idPedido, descripcion)
        Dim dt As DataTable
        Dim tot As Double = 0
        Dim tot2 As Integer
        dt = P.buscarDesAnadidosPed(idPedido, IdInventario)

        For i As Integer = 0 To dt.Rows.Count - 1
            tot = tot + Double.Parse(dt.Rows(i)(4).ToString())
        Next

        tot2 = Int(tot)

        For i As Integer = 1 To tot2 'agregar los que se almacenaron
            P.guardarPromocion(idPedido, idDescuento, idProducto, 0)
        Next

        If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

            regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
            regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

            cDesc = promocion1 - promocion2
            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta


            CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "VentasN")

            ConsultaDetalles()
            NuevoConcepto()
            P.EliminarDesc(idPedido, idDescuento, idProducto)
            'anadir registros faltantes
            For i As Integer = 1 To regAnadir
                P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
            Next
        End If

    End Sub
    Public Sub modificarDescuento(ByVal idMod As Integer)
        Dim idDescuento As Integer
        idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim TablaDesc As DataTable
        Dim des As Double = 0
        Dim descripcion As String = ""

        If idDescuento = 0 Then
            'No hay descuento
        Else
            TablaDesc = P.tablaDesc(idDescuento)
            If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

                If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
                    des = TotalPorcentaje(CDbl(TextBox6.Text), Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
                    'des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: " + TablaDesc.Rows(0)(2).ToString() + " %"
                Else
                    des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
                    des = des * Double.Parse(TextBox5.Text)
                    des = des - (2 * des)
                    descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U"
                End If
                CD.Modificar(idMod, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
                P.ModificarDescuento(idMod, idDescuento, idPedido, "Pedidos")
                ConsultaDetalles()
                NuevoConcepto()
            Else
                'promociones

                modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), Double.Parse(TextBox12.Text), "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, IdInventario)

            End If
        End If


        'Si haye descuento, agregar el renglon a la venta
        'agregarlo a la tabla de descuentos
    End Sub

    Public Sub eliminarDescuescuento(ByVal idElim As Integer, ByVal Tipo As String)
        If Tipo <> "Promocion" Then
            CD.Eliminar(idElim)
            P.EliminarDesc(idElim, "VentasN")
        Else
            eliminarPromocion()
        End If

    End Sub
    Public Sub valorProm(ByVal valor As String)
        Dim aux As String = ""
        Dim bandera As Boolean = False

        For j As Integer = 0 To valor.Length() - 1
            If bandera = False Then
                'agarrar el primero
                If valor.Chars(j) <> "x" Then
                    aux = aux + valor.Chars(j)

                Else
                    ' es X
                    promocion1 = Integer.Parse(aux)
                    bandera = True
                    aux = ""
                End If


            Else
                'agarrar el segundo numero
                aux = aux + valor.Chars(j)
            End If


        Next
        promocion2 = Integer.Parse(aux)
    End Sub
    Public Function horaFormato() As String
        Dim fechita As String
        Dim Aux As String = ""
        fechita = Now.ToString("HH:mm:ss")

        For j As Integer = 0 To 7
            Aux = Aux + fechita.Chars(j)
        Next
        fechita = Aux

        Return fechita
    End Function

    Public Function fechaFormato() As String
        Dim fechita2 As String
        fechita2 = Date.Now.Year.ToString() + "/" + Integer.Parse(Date.Now.Month.ToString).ToString("00") + "/" + Integer.Parse(Date.Now.Day.ToString).ToString("00")
        Return fechita2
    End Function
    'Metodo de sacar el Total del descuento
    Public Function TotalPorcentaje(ByVal total As Double, ByVal porcentaje As Integer) As Double
        Dim desc As Double = 0
        desc = (total * porcentaje) / 100
        Return desc 'devuelve el descuento solamente
    End Function

    Private Sub txtIEPS_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIEPS.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtIVARetenido.Focus()
        End If
    End Sub

    Private Sub txtIEPS_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIEPS.Leave
        If txtIEPS.Text = "" Then
            txtIEPS.Text = "0"
        End If
    End Sub

    Private Sub txtIVARetenido_KeyDown(sender As Object, e As KeyEventArgs) Handles txtIVARetenido.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub txtIVARetenido_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVARetenido.Leave
        If txtIVARetenido.Text = "" Then
            txtIVARetenido.Text = "0"
        End If
    End Sub

    Private Sub frmVentasPedidos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Panel3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub txtIEPS_TextChanged(sender As Object, e As EventArgs) Handles txtIEPS.TextChanged

    End Sub

    Private Sub txtIVARetenido_TextChanged(sender As Object, e As EventArgs) Handles txtIVARetenido.TextChanged

    End Sub
End Class