Public Class frmComprasPedidos

    Dim EmbarcarA As String
    Dim Laboratorio As String
    Dim Condiciones As String
    Dim EmbarcarPor As String
    Dim ConsignarA As String
    Dim Observaciones As String
    Dim IdsSucursales As New elemento

    'Dim IdsVariantes As New elemento
    Dim idPedido As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idProveedor As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    'Dim IdsAlmacenes As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    'Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    'Dim IdVariante As Integer
    'Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim PrecioBase As Double
    Dim LlenandoDAtos As Boolean = False
    'Dim IdsSucursales As New elemento
    Dim Tabla As New DataTable
    Dim ImpDoc As ImprimirDocumento
    Dim Op As dbOpciones

    Public Sub New(Optional ByVal pidpedido As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idPedido = pidpedido
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este pedido no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbComprasPedidos(MySqlcon)
                c.Eliminar(idPedido)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmComprasPedidos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled Then
                Modificar(Estados.Guardada, True)
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled Then
                Modificar(Estados.Pendiente, True)
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
        If e.KeyCode = Keys.F7 Then
            Dim f As New frmInventario
            f.ShowDialog()
        End If
        
    End Sub

    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ConsultaOn = False
        'Label5.Text = CStr(CSng("1,000"))
        Try
            Me.Text = Me.Text + " " + GlobalUsuario
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ImpDoc = New ImprimirDocumento()
        Op = New dbOpciones(MySqlcon)
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Tabla.Columns.Add("Id", I.GetType)
        Tabla.Columns.Add("TipoR", S.GetType)
        Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        Tabla.Columns.Add("Surtido", D.GetType)
        Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Precio U.", S.GetType)
        Tabla.Columns.Add("Importe", S.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)

        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(IdAlmacenG)
        ConsultaOn = True
        Me.Show()
        If idPedido = 0 Then
            Nuevo()
        Else
            LlenaDatosVenta()
        End If
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim T As Double
                Dim Iva As Double
                Dim V As New dbComprasPedidos(MySqlcon)
                T = V.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                Iva = V.TotalIva
                Label12.Text = Format(Math.Round(V.Subtotal, 2), "#,##0.00")
                Label13.Text = Format(Math.Round(V.TotalIva - V.TotalISR - V.TotalIvaRetenido, 2), "#,##0.00")
                Label14.Text = Format(Math.Round(V.TotalVenta, 2), "#,##0.00")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        Panel1.Enabled = True
        Panel2.Enabled = True
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        DateTimePicker1.Value = Date.Now
        TextBox2.Text = ""
        TextBox1.Text = ""
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox2.Enabled = True
        TextBox11.Enabled = True
        TextBox14.Text = ""
        FolioAnt = 0
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasPedidos, 0)
        TextBox11.Text = Sf.Serie
        Dim V As New dbComprasPedidos(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        Label24.Visible = False
        idPedido = 0
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        'Dim o As New dbOpciones(MySqlcon)
        'TextBox8.Text = o.Imp.ToString
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        CheckBox1.Checked = False
        'Panel1.Enabled = True
        NuevoConcepto()
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            ComboBox3.Enabled = False
        End If
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox5.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonCliente()
        End If
    End Sub

    'Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        TextBox5.Focus()
    '    End If
    'End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaProveedor()
    End Sub
    Private Sub BuscaProveedor()
        Try
            If ConsultaOn Then
                Dim c As New dbproveedores(MySqlcon)
                If c.BuscaProveedor(TextBox1.Text) Then
                    'If c.DireccionFiscal = 0 Then
                    TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    TextBox13.Text = "Límite: " + Format(c.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + c.DiasCredito.ToString + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    'Else
                    '   TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    'End If
                    'TextBox13.Text = "Límite: " + Format(c.Credito, "#,##0.00") + vbCrLf + "Días: " + c.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
                    idProveedor = c.ID
                Else
                    TextBox7.Text = ""
                    TextBox13.Text = ""
                    idProveedor = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente, True)
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte, pNuevo As Boolean)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbComprasPedidos(MySqlcon)
            Dim Desglozar As Byte
            If TextBox2.Text = "" Then MensajeError = "Debe indicar un folio."
            If IsNumeric(TextBox2.Text) = False Then
                MensajeError = "El folio debe ser un valor numérico."
            Else
                If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                    If pEstado = Estados.Guardada Then
                        MensajeError = "Folio repetido."
                    End If
                End If
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosAlta, PermisosN.Secciones.Compras) = False And pEstado = Estados.Guardada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosCancelar, PermisosN.Secciones.Compras) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If MensajeError = "" Then

                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                C.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Modificar(idPedido, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, IDsMonedas2.Valor(ComboBox2.SelectedIndex), 0, pEstado, C.Subtotal, C.TotalVenta, idProveedor, TextBox11.Text, TextBox14.Text)
                Estado = pEstado
                If pEstado = Estados.Guardada Then Imprimir(idPedido)
                If pEstado = Estados.Cancelada Then
                    If MsgBox("¿Desea imprimir la orden de compra cancelada?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Imprimir(idPedido)
                    End If
                End If
                If pNuevo Then Nuevo()
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
            'If (PermisosVentas And CULng((Math.Pow(2, perVentas.Ventas + 1)))) <> 0 Then
            If idProveedor <> 0 Then
                Dim C As New dbComprasPedidos(MySqlcon)

                C.Guardar(idProveedor, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, IDsMonedas2.Valor(ComboBox2.SelectedIndex), 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text)
                idPedido = C.ID
                Estado = 1
                'Button1.Text = "Modificar"
                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                'LlenaDatosDetalles()
            Else
                MsgBox("Debe indicar un proveedor", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            'Else
            'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox12.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BotonArticulo()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub

    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 1, False, False, True, False) Then
                    LlenaDatosArticulo(p)
                Else
                    TextBox4.Text = ""
                    TextBox6.Text = "0"
                    TextBox8.Text = "0"
                    TextBox9.Text = "0"
                    PrecioU = 0
                    'IdVariante = 0
                    'End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub LlenaDatosVenta()
        Dim C As New dbComprasPedidos(idPedido, MySqlcon)
        LlenandoDAtos = True
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        LlenandoDAtos = False
        TextBox2.Text = C.Folio
        TextBox11.Text = C.Serie
        FolioAnt = C.Folio
        TextBox1.Text = C.Proveedor.Clave
        Estado = C.Estado
        TextBox8.Text = C.Iva.ToString
        TextBox14.Text = C.Comentario
        'If C.Desglosar = 1 Then
        '    CheckBox1.Checked = True
        'Else
        '    CheckBox1.Checked = False
        'End If
        Button2.Enabled = True
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.idMoneda)
        DateTimePicker1.Value = C.Fecha

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
                Panel1.Enabled = True
                Panel2.Enabled = False
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
        End Select


    End Sub

    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbComprasPedidosDetalles(MySqlcon)
            T = CD.ConsultaReader(idPedido)
            While T.Read
                If T("cantidad") <> 0 Then
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("surtido"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                Else
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("surtido"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                End If
            End While
            T.Close()
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
        'IdVariante = 0
        PrecioBase = 0
        TextBox3.Text = ""
        TextBox4.Text = ""
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        TextBox9.Text = "0"
        TextBox12.Text = "0"
        Button6.Enabled = True
        TextBox3.Enabled = True
        'Button12.Visible = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        TextBox5.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbComprasPedidosDetalles(MySqlcon)
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
                'Else
                '    If CDbl(TextBox6.Text) <= 0 Then
                '        MsgError += " El costo debe ser un valor mayor a 0."
                '        HayError = True
                '    End If
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
                If Button4.Text = "Agregar Concepto" Then
                    
                    CD.Guardar(idPedido, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text)) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
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
                    SacaTotal()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text))
                    'If ManejaSeries <> 0 Then
                    '    Dim F As New frmInventarioSeries(IdInventario, 0, idCotizacion, CDbl(TextBox5.Text))
                    '    F.ShowDialog()
                    'End If
                    'Dim I As New dbInventario(MySqlcon)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    ConsultaDetalles()
                    NuevoConcepto()
                    SacaTotal()
                    'PopUp("Artículo modificado", 90)

                End If
                TextBox5.Focus()
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub PresionaEnter()
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosAlta, PermisosN.Secciones.Compras) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                If IdInventario <> 0 Then
                    AgregaArticulo()
                    Dim C As New dbComprasPedidos(idPedido, MySqlcon)
                    C.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                    C.Modificar(idPedido, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, IDsMonedas2.Valor(ComboBox2.SelectedIndex), 0, Estado, C.Subtotal, C.TotalVenta, idProveedor, TextBox11.Text, TextBox14.Text)
                End If
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        PresionaEnter()
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
            Dim CD As New dbComprasPedidosDetalles(IdDetalle, MySqlcon)
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            TextBox5.Text = CD.Cantidad.ToString
            txtIEPS.Text = CD.IEPS
            txtIVARetenido.Text = CD.ivaRetenido
            IdInventario = CD.Idinventario
            'IdVariante = CD.IdVariante
            'If IdInventario > 1 Then
            TextBox3.Text = CD.Inventario.Clave
            '   IdVariante = 0
            'End If
            'If IdVariante > 1 Then
            'Dim P As New dbProductos(CD.Producto.IdProducto, MySqlcon)
            'TextBox3.Text = P.Clave
            'IdInventario = 0
            'End If
            TextBox8.Text = CD.Iva
            If CD.Descuento = 0 Then
                'If PrecioNeto = 0 Then
                PrecioU = Math.Round(CD.Precio / CD.Cantidad, 2)
                'Else
                '   PrecioU = CD.Precio / CD.Cantidad * (1 + CD.Iva / 100)
                'End If
            Else
                Dim Val As Double = (CD.Precio / (1 - CD.Descuento / 100))
                'If PrecioNeto = 0 Then
                PrecioU = Math.Round(Val / CD.Cantidad, 2)
                'Else
                '   PrecioU = Val / CD.Cantidad * (1 + CD.Iva / 100)
                'End If
            End If
            TextBox12.Text = PrecioU.ToString("0.00")
            PrecioBase = PrecioU
            TextBox9.Text = CD.Descuento
            CantAnt = CD.Cantidad

            'IdAlmacen = CD.IdAlmacen
            If CD.Descuento = 0 Then
                TextBox6.Text = CD.Precio.ToString("0.00")
            Else
                TextBox6.Text = Format(PrecioU * CD.Cantidad, "0.00")
            End If
            Button4.Text = "Modificar Concepto"
            TextBox4.Text = CD.Descripcion

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta cotización no ha sido guardada. ¿Desea iniciar una nueva cotización? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbComprasPedidos(MySqlcon)
                c.Eliminar(idPedido)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosAlta, PermisosN.Secciones.Compras) = True Then
                If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbComprasPedidosDetalles(MySqlcon)
                    CD.Eliminar(IdDetalle)
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
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'If B.Cliente.DireccionFiscal = 0 Then
            'TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            'Else
            TextBox7.Text = B.Proveedor.Nombre + vbCrLf + B.Proveedor.RFC + vbCrLf + B.Proveedor.Direccion + " " + B.Proveedor.NoExterior + " " + B.Proveedor.Ciudad + " " + B.Proveedor.CP
            TextBox13.Text = "Límite: " + Format(B.Proveedor.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + B.Proveedor.DiasCredito.ToString + vbCrLf + "Saldo: " + Format(B.Proveedor.DaSaldoAFecha(B.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
            'End If
            'TextBox13.Text = "Límite: " + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Días: " + B.Cliente.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(B.Cliente.DaSaldoAFecha(B.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
            idProveedor = B.Proveedor.ID
            ConsultaOn = False
            TextBox1.Text = B.Proveedor.Clave
            ConsultaOn = True
            TextBox5.Focus()
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BotonCliente()
    End Sub
    Private Sub BotonArticulo()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloInv
        Dim op As New dbOpciones(MySqlcon)
        If op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                LlenaDatosArticulo(B.Inventario)
                TextBox12.Focus()
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, False, True, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                LlenaDatosArticulo(B.Inventario)
                TextBox12.Focus()
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        BotonArticulo()
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
        Articulo.DaUltimaidMoneda(Articulo.ID)
        Articulo.DaUltimoCosto(Articulo.ID)
        PrecioU = Math.Round(Articulo.UltimoPrecioCompra, 2)
        PrecioBase = PrecioU
        TextBox8.Text = Articulo.Iva.ToString
        ManejaSeries = Articulo.ManejaSeries
        TextBox12.Text = PrecioU.ToString("0.00")
        TextBox6.Text = Format(Cant * PrecioU, "0.00")
        txtIEPS.Text = Articulo.ieps.ToString
        txtIVARetenido.Text = Articulo.ivaRetenido.ToString
        ComboBox1.SelectedIndex = IDsMonedas.Busca(Articulo.idMonedaCompra)
        IdInventario = Articulo.ID
        'IdVariante = 0
        'If ManejaSeries = 0 Then
        '    Button12.Visible = False
        'Else
        '    Button12.Visible = True
        'End If
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
        Dim f As New frmComprasPedidosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idPedido = f.IdPedidos
            LlenaDatosVenta()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.PedidosAlta, PermisosN.Secciones.Compras) = True Then
                Dim C As New dbComprasPedidos(MySqlcon)
                C.Eliminar(idPedido)
                PopUp("Pedido Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub



    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada, True)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MsgBox("¿Cancelar la orden de compra?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada, True)
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Enabled Then
                TextBox3.Focus()
            Else
                TextBox12.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IdInventario <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Try
            Dim Forma As New frmBuscaDocumentoVenta(0, True, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, False, True, True, 0, False, "", False)
            If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim V As New dbComprasPedidos(MySqlcon)
                If Estado = 0 Then
                    Select Case Forma.Tipo
                        Case 0
                            Dim Co As New dbComprasCotizacionesb(Forma.id(0), MySqlcon)
                            TextBox1.Text = Co.Proveedor.Clave
                        Case 1
                            Dim Cp As New dbComprasPedidos(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cp.proveedor.Clave
                        Case 2
                            Dim Cr As New dbComprasRemisiones(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cr.Proveedor.Clave
                        Case 3
                            Dim Cv As New dbCompras(Forma.id(0), MySqlcon)
                            TextBox1.Text = Cv.Proveedor.Clave
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
        If e.KeyCode = Keys.Enter Then
            PresionaEnter()
        End If
    End Sub

    'Private Sub TextBox12_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyUp
    '    If e.KeyCode = Keys.Enter Then
    '        TextBox9.Focus()
    '    End If
    'End Sub


    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If IdInventario <> 0 Then
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
            TextBox8.Focus()
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

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label21.Click

    End Sub

    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

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
    

    
    Private Sub Imprimir(pIdPedido As Integer)
        Try
            Dim Compra As New dbComprasPedidos(pIdPedido, MySqlcon)
            ImpDoc.IdSucursal = Compra.idSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.CompraPedido
            ImpDoc.TipoDocumentoT = TiposDocumentos.CompraPedido + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.CompraPedido
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion(pIdPedido)
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "ORDEN DE COMPRA " + Compra.Serie + Compra.Folio.ToString("0000")
            PrintDocument1.Print()
            EnviarCorreo(idPedido, ImpDoc.RutaPDF)
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Estado = Estados.SinGuardar Or Estado = Estados.Inicio Then Modificar(Estados.SinGuardar, False)
        Imprimir(idPedido)
    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            PresionaEnter()
        End If
    End Sub

    Public Sub LlenaNodosImpresion(pIdpedido As Integer)
        Dim Cot As New dbComprasPedidos(pIdpedido, MySqlcon)
        Dim Suc As New dbSucursales(Cot.IdSucursal, MySqlcon)
        Dim Prov As New dbproveedores(Cot.Idproveedor, MySqlcon)
        'Dim firmaChequeRecibido As String = "Firma cheque recibido"
        Cot.DaTotal(pIdpedido, Cot.idMoneda)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFolio", Cot.Folio, 0), "docFolio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFecha", Cot.Fecha, 0), "docFecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docSubTotal", Format(Cot.Subtotal, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "docSubTotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docTotal", Format(Cot.TotalVenta, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "docTotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalieps", Format(Cot.TotalIEPS, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "doctotalieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalivaret", Format(Cot.TotalIvaRetenido, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "doctotalivaret")
        Dim TotalconLetra As String
        Dim CL As New CLetras
        If Cot.TotalVenta >= 0 Then
            TotalconLetra = CL.LetrasM(Cot.TotalVenta, Cot.idMoneda, GlobalIdiomaLetras)
        Else
            TotalconLetra = "MENOS " + CL.LetrasM(Cot.TotalVenta * -1, Cot.idMoneda, GlobalIdiomaLetras)
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docCantLetra", TotalconLetra, 0), "docCantLetra")
        If Cot.Estado = 4 Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADO", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaNombre", Suc.Nombre, 0), "empresaNombre")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaCalle", Suc.Direccion, 0), "empresaCalle")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaColonia", Suc.Colonia, 0), "empresaColonia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaCP", Suc.CP, 0), "empresaCP")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaRFC", Suc.RFC, 0), "empresaRFC")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaTel", Suc.Telefono, 0), "empresaTel")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaTel2", Suc.Telefono, 0), "empresaTel2")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "empresaEMail", Suc.Email, 0), "empresaEMail")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", Cot.Comentario, 0), "comentario")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provNombre", Prov.Nombre, 0), "provNombre")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provDom", Prov.Direccion, 0), "provDom")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCol", Prov.Colonia, 0), "provCol")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCP", Prov.CP, 0), "provCP")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provCiudad", Prov.Ciudad, 0), "provCiudad") 'no
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provRFC", Prov.RFC, 0), "provRFC")
        ImpDoc.CuantosRenglones = 0
        Dim T As MySql.Data.MySqlClient.MySqlDataReader
        Dim CD As New dbComprasPedidosDetalles(MySqlcon)
        T = CD.ConsultaReader(pIdpedido)
        While T.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artCant", Format(T("cantidad"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "artCant" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", T("descripcion"), 0), "descripcion" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artPrecioU", Format(T("precio") / T("cantidad"), Op._formatoPrecioU).PadLeft(Op.EspacioPrecioUnitacio), 0), "artPrecioU" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "provImporte", Format(T("precio"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "provImporte" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artCod", T("clave"), 0), "artCod" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "medida", T("tipocantidad"), 0), "medida" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.CuantosRenglones += 1
        End While
        T.Close()
        ImpDoc.ImpND.Add(New NodoImpresionN("", "privnoInterior", Prov.NoInterior, 0), "privnoInterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provnoExterior", Prov.NoExterior, 0), "provnoExterior")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provEstado", Prov.Estado, 0), "provEstado")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "provPais", Prov.Pais, 0), "provPais")

        Dim Ivas As New Collection
        Dim Cont As Integer = 0

        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim IvasImporte As New Collection
        DR = Cot.DaIvas(pIdpedido)
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

        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And Op._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If

        For Each I As Double In Ivas
            ImpDoc.ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), Op._formatoIva).PadLeft(Op.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
    End Sub
  
    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If LlenandoDAtos = False Then
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasPedidos, 0)
            TextBox11.Text = Sf.Serie
            Dim V As New dbComprasPedidos(MySqlcon)
            TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        End If
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmProveedores(1, idProveedor, "")
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.CodigoProveedor
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbComprasPedidos(MySqlcon)
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
            TextBox9.Focus()
        End If
    End Sub

    Private Sub txtIVARetenido_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVARetenido.Leave
        If txtIVARetenido.Text = "" Then
            txtIVARetenido.Text = "0"
        End If
    End Sub
    Private Sub EnviarCorreo(pIdPedido As Integer, pRutaPDF As String)
        Dim v As New dbComprasPedidos(pIdPedido, MySqlcon)
        If v.Proveedor.Email <> "" Then
            Try
                If MsgBox("¿Enviar orden de compra por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                    Dim C As String
                    Dim S As New dbSucursales(v.IdSucursal, MySqlcon)
                    Dim RutaPDFcompleto As String = pRutaPDF + "\ORDEN DE COMPRA " + v.Serie + v.Folio.ToString("0000") + ".pdf"
                    If IO.File.Exists(RutaPDFcompleto) Then
                        C = "Eviado por: " + S.Nombre + vbNewLine + "RFC: " + S.RFC + vbNewLine + "ORDEN DE COMPRA" + vbNewLine + "Folio: " + v.Serie + v.Folio.ToString("0000")
                        M.send("ORDEN DE COMPRA: " + v.Serie + v.Folio.ToString("0000"), C, v.Proveedor.Email, v.Proveedor.Nombre, RutaPDFcompleto, "")
                        PopUp("Correo Enviado", 60)
                    Else
                        MsgBox("No existe el archivo PDF.", MsgBoxStyle.YesNo, GlobalNombreApp)
                    End If
                End If
            Catch ex As Exception
                MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
    End Sub

    Private Sub btnFacturas_Click(sender As Object, e As EventArgs) Handles btnFacturas.Click
        If idPedido <> 0 Then
            Dim f As New frmComprasConsulta(ModosDeBusqueda.Principal, 0, idPedido)
            f.ShowDialog()
            f.Dispose()
        Else
            MsgBox("No hay orden de compra activa.", MsgBoxStyle.Information, GlobalNombreApp)
        End If
    End Sub
End Class