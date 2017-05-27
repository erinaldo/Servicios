Public Class frmCajasMovimientos
    Dim IdsVariantes As New elemento
    Dim idMovimiento As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim IdsVendedores As New elemento
    Dim IdsCajas As New elemento
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
    Dim TipoImpresora As Byte
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim IdLista As Integer
    Dim Sobre As Byte
    Dim SIVA As Double
    Dim CuantaY As Integer
    Dim UsaFormula As Byte
    Dim PrecioNeto As Byte
    Dim IdCajaDefault As Integer
    Dim IdVendedorDefault As Integer
    Dim idSucursalEntrada As Integer
    Public Sub New(ByVal pidVenta As Integer, ByVal pIdCaja As Integer, ByVal pIdVendedor As Integer, ByVal pidSucursal As Integer)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idMovimiento = pidVenta
        IdCajaDefault = pIdCaja
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este movimiento no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbCajasMovimientos(MySqlcon)
                c.Eliminar(idMovimiento)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmVentasPedidos_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'If e.KeyCode = Keys.F12 Then
        '    If idCliente <> 0 Then
        '        Dim Cl As New frmClientesConsultaArticulos(idCliente, IdInventario)
        '        Cl.ShowDialog()
        '        Cl.Dispose()
        '    End If
        'End If
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
        'If e.KeyCode = Keys.F6 And IdInventario <> 0 Then
        '    Dim f As New frmInventarioConsulta(IdInventario)
        '    f.ShowDialog()
        'End If
        'If e.KeyCode = Keys.F7 Then
        '    Dim f As New frmInventario
        '    f.ShowDialog()
        'End If
        'If e.KeyCode = Keys.F8 Then
        '    If GlobalTipoVersion = 0 Then
        '        Dim f As New frmCompras
        '        f.ShowDialog()
        '    End If
        'End If
    End Sub
    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        IdVendedorDefault = U.IdVendedor
        ComboBox4.Items.Add("Ingreso")
        ComboBox4.Items.Add("Retiro")
        Tabla.Columns.Add("Id", I.GetType)
        'Tabla.Columns.Add("TipoR", S.GetType)
        'Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        'Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        'Tabla.Columns.Add("Precio U.", S.GetType)
        Tabla.Columns.Add("Importe", S.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1")
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(IdAlmacenG)

        ConsultaOn = True
        If idMovimiento = 0 Then
            Nuevo()
            NuevoConcepto()
        Else
            LlenaDatosVenta()
        End If
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                'Dim T As Double
                'Dim Iva As Double
                Dim V As New dbCajasMovimientos(MySqlcon)
                V.DaTotal(idMovimiento, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)

                'If CheckBox1.Checked Then
                'Iva = T - (T / (1 + (O.Imp / 100)))
                Label12.Text = Format(V.Subtotal, "#,###,##0.00")
                Label13.Text = Format(V.TotalIva, "#,###,##0.00")
                Label14.Text = Format(V.TotalVenta, "#,###,##0.00")
                'Label32.Text = Format(V.TotalPeso, "#,##0.00") + "Kg."
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
        ComboBox6.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        ComboBox6.Enabled = True
        TextBox2.Enabled = True
        TextBox11.Enabled = True
        Label32.Text = "0.00Kg."
        DateTimePicker1.Value = Date.Now
        TextBox2.Text = ""
        ComboBox4.SelectedIndex = 0
        TextBox14.Text = ""
        FolioAnt = 0
        ComboBox4.Enabled = True
        ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorDefault)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0) Then
            TextBox11.Text = Sf.Serie
        Else
            TextBox11.Text = ""
        End If
        Dim V As New dbCajasMovimientos(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        idMovimiento = 0
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
        Button2.Enabled = False
        CheckBox1.Checked = False
        'Dim o As New dbOpciones(MySqlcon)
        'TextBox8.Text = o.Imp.ToString
        NuevoConcepto()

        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CambiarSucursal, PermisosN.Secciones.PuntodeVenta) = False Then
            ComboBox3.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasPermitirCambioFecha, PermisosN.Secciones.PuntodeVenta) = False Then
            DateTimePicker1.Enabled = False
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim MensajeError As String = ""
            Dim C As New dbCajasMovimientos(MySqlcon)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosAlta, PermisosN.Secciones.PuntodeVenta) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosCancelar, PermisosN.Secciones.PuntodeVenta) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalconEmpenios Then
                Dim Em As New dbEmpenios(MySqlcon)
                If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasPermitirCambioFecha, PermisosN.Secciones.PuntodeVenta) = False And DateTimePicker1.Value.ToString("yyyy/MM/dd") < Em.DaUltimaFecha() Then
                    MensajeError += " Fecha incorrecta. Revise que la fecha de su computadora este correcta."
                End If
            Else
                If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasPermitirCambioFecha, PermisosN.Secciones.PuntodeVenta) = False And DateTimePicker1.Value.ToString("yyyy/MM/dd") < C.DaUltimaFecha() Then
                    MensajeError += " Fecha incorrecta. Revise que la fecha de su computadora este correcta."
                End If
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
                C.DaTotal(idMovimiento, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Modificar(idMovimiento, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, Desglozar, CDbl(TextBox8.Text), pEstado, C.Subtotal, C.TotalVenta, idCliente, TextBox11.Text, IdsVendedores.Valor(ComboBox5.SelectedIndex), TextBox14.Text, ComboBox4.SelectedIndex, IdsCajas.Valor(ComboBox6.SelectedIndex))
                If pEstado = Estados.Guardada Then
                    Dim Ca As New dbCajas(MySqlcon)
                    If ComboBox4.SelectedIndex = 0 Then
                        Ca.MovimientodeCaja(C.IdCaja, C.TotalVenta)
                    Else
                        Ca.MovimientodeCaja(C.IdCaja, C.TotalVenta * -1)
                    End If
                    Imprimir()
                End If

                If pEstado = Estados.Cancelada Then
                    Dim Ca As New dbCajas(MySqlcon)
                    If ComboBox4.SelectedIndex = 0 Then
                        Ca.MovimientodeCaja(C.IdCaja, C.TotalVenta * -1)
                    Else
                        Ca.MovimientodeCaja(C.IdCaja, C.TotalVenta)
                    End If
                End If
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
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosAlta, PermisosN.Secciones.PuntodeVenta) = True Then
                'If idCliente <> 0 Then
                Dim C As New dbCajasMovimientos(MySqlcon)
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
                C.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, Desglozar, CDbl(TextBox8.Text), IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text, IdsCajas.Valor(ComboBox6.SelectedIndex), IdsVendedores.Valor(ComboBox5.SelectedIndex), ComboBox4.SelectedIndex)
                idMovimiento = C.ID
                Estado = 1
                ComboBox4.Enabled = False
                'Button1.Text = "Modificar"
                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                'LlenaDatosDetalles()
                'Else
                '   MsgBox("Debe indicar un cliente", MsgBoxStyle.Critical, GlobalNombreApp)
                'End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            'If IdInventario <> 0 Then
            '    If UsaFormula = 1 Then
            '        Dim Fo As New frmInventarioFormula01
            '        If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
            '            TextBox5.Text = Format(Fo.Resultado, "0.00")
            '            TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
            '        End If
            '        'TextBox12.Focus()
            '    End If
            'End If
            TextBox12.Focus()
        End If
        'If e.KeyCode = Keys.F1 Then
        '    BotonArticulo()
        'End If
        'If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
        '    If UsaFormula = 1 Then
        '        Dim Fo As New frmInventarioFormula01
        '        If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            TextBox5.Text = Format(Fo.Resultado, "0.00")
        '            TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
        '        End If
        '        TextBox12.Focus()
        '    End If
        'End If
    End Sub
   
    
    
    Private Sub LlenaDatosVenta()
        Dim C As New dbCajasMovimientos(idMovimiento, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio.ToString
        FolioAnt = C.Folio
        TextBox11.Text = C.Serie
        'TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        'TextBox8.Text = C.Iva.ToString
        If C.Desglosar = 1 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If

        ComboBox4.SelectedIndex = C.Tipo
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        TextBox14.Text = C.Comentario

        ComboBox6.SelectedIndex = IdsCajas.Busca(C.IdCaja)
        ComboBox4.Enabled = False
        ConsultaDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                ComboBox6.Enabled = False
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
                ComboBox6.Enabled = False
                Button2.Enabled = False
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                ComboBox6.Enabled = True
                Button2.Enabled = True
        End Select

    End Sub

    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbCajasMovimientosDetalles(MySqlcon)
            T = CD.ConsultaReader(idMovimiento)
            While T.Read
                'If T("cantidad") <> 0 Then
                'If T("idinventario") > 1 Then
                Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), Format(T("precio"), "0.00"), T("abreviatura"))
                'Else
                '   Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("pclave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                'End If
                'Else
                'If T("idinventario") > 1 Then
                'Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                'Else
                '   Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("pclave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                'End If
                'End If
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
            'DGDetalles.Columns(1).Visible = False
            'DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(4).Width = 80
            'DGDetalles.Columns(8).Width = 80
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
            'DGDetalles.Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        IdVariante = 0
        TextBox4.Text = ""
        TextBox6.Text = "0"
        TextBox5.Text = "1"
        UsaFormula = 0
        PrecioU = 0
        PrecioBase = 0
        PrecioNeto = 0
        Button12.Visible = False
        'Button6.Enabled = True
        'TextBox3.Enabled = True
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        TextBox8.Text = "0"
        TextBox9.Text = "0"
        TextBox12.Text = "0"
        TextBox12.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbCajasMovimientosDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""
            'If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox12.Text) = False Then
                MsgError += vbCrLf + "El importe debe ser un valor numérico."
                HayError = True
            Else
                If CInt(TextBox12.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El importe debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            'If IsNumeric(TextBox9.Text) = False Then
            '    MsgError += vbCrLf + "El descuento debe ser un valor numérico."
            '    HayError = True
            'Else
            '    If CDbl(TextBox9.Text) <> 0 Then
            '        TextBox12.Text = CStr(CDbl(TextBox12.Text) - (CDbl(TextBox12.Text) * CDbl(TextBox9.Text) / 100))
            '    End If
            'End If
            If HayError = False Then
                'If PrecioNeto = 1 Then
                '    Dim Temp As Double
                '    Temp = CStr(CDbl(TextBox6.Text) / (1 + CDbl(TextBox8.Text) / 100) / CDbl(TextBox5.Text))
                '    TextBox12.Text = Temp.ToString
                'End If
                If Button4.Text = "Agregar Concepto" Then

                    CD.Guardar(idMovimiento, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox12.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
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
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox12.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text))
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
        If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosAlta, PermisosN.Secciones.PuntodeVenta) = True Then

            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaArticulo()
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
            'Button6.Enabled = False
            'TextBox3.Enabled = False
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            'Tipo = DGDetalles.Item(1, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbCajasMovimientosDetalles(IdDetalle, MySqlcon)
            TextBox5.Text = CD.Cantidad.ToString
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            IdInventario = CD.Idinventario
            IdVariante = CD.IdVariante
            'If IdInventario > 1 Then
            '    TextBox3.Text = CD.Inventario.Clave
            '    PrecioNeto = CD.Inventario.PrecioNeto
            '    IdVariante = 0
            'Else
            '    PrecioNeto = 0
            'End If
            'If IdVariante > 1 Then
            '    Dim P As New dbProductos(CD.Producto.IdProducto, MySqlcon)
            '    TextBox3.Text = P.Clave
            '    IdInventario = 0
            'End If
            TextBox8.Text = CD.Iva
            'UsaFormula = CD.Inventario.UsaFormula
            If CD.Descuento = 0 Then
                If PrecioNeto = 0 Then
                    PrecioU = CD.Precio / CD.Cantidad
                Else
                    PrecioU = CD.Precio / CD.Cantidad * (1 + CD.Iva / 100)
                End If
            Else
                Dim Val As Double = (CD.Precio / (1 - CD.Descuento / 100))
                If PrecioNeto = 0 Then
                    PrecioU = Val / CD.Cantidad
                Else
                    PrecioU = Val / CD.Cantidad * (1 + CD.Iva / 100)
                End If
            End If
            TextBox12.Text = PrecioU.ToString
            PrecioBase = PrecioU
            TextBox9.Text = CD.Descuento
            CantAnt = CD.Cantidad

            'IdAlmacen = CD.IdAlmacen
            If CD.Descuento = 0 Then
                TextBox6.Text = CD.Precio.ToString
            Else
                TextBox6.Text = Format(PrecioU * CD.Cantidad, "0.00")
            End If
            Button4.Text = "Modificar Concepto"
            TextBox4.Text = CD.Descripcion



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
            If MsgBox("Este movimiento no ha sido guardado. ¿Desea iniciar un nuevo movimiento? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbCajasMovimientos(MySqlcon)
                c.Eliminar(idMovimiento)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosAlta, PermisosN.Secciones.PuntodeVenta) = True Then
                If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbCajasMovimientosDetalles(MySqlcon)
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
    'Private Sub BotonArticulo()
    '    Dim TipodeBusqueda As Integer
    '    TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloProducto
    '    Dim B As New frmBuscador(TipodeBusqueda, 0)
    '    B.ShowDialog()
    '    If B.DialogResult = Windows.Forms.DialogResult.OK Then
    '        Select Case B.Tipo
    '            Case "I"
    '                LlenaDatosArticulo(B.Inventario)
    '                'TextBox12.Focus()
    '            Case "P"
    '                'LlenaDatosProducto(B.Producto)
    '                'cmbVariante.Focus()
    '            Case "S"
    '                'LlenaDatosServicio(B.Servicio)
    '                'TextBox12.Focus()
    '        End Select
    '        'If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
    '        If UsaFormula = 1 Then
    '            Dim Fo As New frmInventarioFormula01
    '            If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
    '                TextBox5.Text = Format(Fo.Resultado, "0.00")
    '                TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
    '            End If
    '            'TextBox12.Focus()
    '            'End If
    '        End If
    '        TextBox12.Focus()
    '    End If
    'End Sub
    
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        Dim a As New dbInventarioPrecios(MySqlcon)
        a.BuscaPrecio(Articulo.ID, IdLista)
        Dim Cant As Double
        TextBox4.Text = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If
        PrecioU = a.Precio
        TextBox12.Text = PrecioU
        PrecioBase = a.Precio
        UsaFormula = Articulo.UsaFormula
        ManejaSeries = Articulo.ManejaSeries
        TextBox6.Text = Cant * PrecioU
        PrecioNeto = Articulo.PrecioNeto
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
        ConsultaOn = False
        'TextBox3.Text = Articulo.Clave
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
        Dim f As New frmCajasMovimientosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idMovimiento = f.IdCompra
            LlenaDatosVenta()
        End If
        f.Dispose()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.PuntodeVentas.CajasmovimientosAlta, PermisosN.Secciones.PuntodeVenta) = True Then
                Dim C As New dbCajasMovimientos(MySqlcon)
                C.Eliminar(idMovimiento)
                PopUp("Movimiento Eliminada", 90)
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
        If MsgBox("¿Cancelar este movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada)
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If TextBox3.Enabled = True Then
        '        TextBox3.Focus()
        '    Else
        '        TextBox12.Focus()
        '    End If
        'End If
        'If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
        '    If UsaFormula = 1 Then
        '        Dim Fo As New frmInventarioFormula01
        '        If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            TextBox5.Text = Format(Fo.Resultado, "0.00")
        '            TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
        '        End If
        '        ' TextBox12.Focus()
        '    End If
        'End If
    End Sub


    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        'Try
        '    Dim Forma As New frmBuscaDocumentoVenta(0, False, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, False)
        '    If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '        Dim V As New dbVentasPedidos(MySqlcon)
        '        If Estado = 0 Then
        '            '0 cotizacion
        '            '1 pedido
        '            '2 remision
        '            '3 ventas
        '            Select Case Forma.Tipo
        '                Case 0
        '                    Dim Co As New dbVentasCotizaciones(Forma.id(0), MySqlcon)
        '                    TextBox1.Text = Co.Cliente.Clave
        '                Case 1
        '                    Dim Cp As New dbVentasPedidos(Forma.id(0), MySqlcon)
        '                    TextBox1.Text = Cp.Cliente.Clave
        '                Case 2
        '                    Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
        '                    TextBox1.Text = Cr.Cliente.Clave
        '                Case 3
        '                    Dim Cv As New dbVentas(Forma.id(0), MySqlcon)
        '                    TextBox1.Text = Cv.Cliente.Clave
        '            End Select
        '            Guardar()
        '            If Estado <> 0 Then
        '                V.AgregarDetallesReferencia(idMovimiento, Forma.id(0), Forma.Tipo)
        '                ConsultaDetalles()
        '            End If
        '        Else
        '            V.AgregarDetallesReferencia(idMovimiento, Forma.id(0), Forma.Tipo)
        '            ConsultaDetalles()
        '        End If
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        'End Try
    End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        'If e.KeyCode = Keys.F1 And IdInventario <> 0 Then
        '    Dim SP As New frmSelectorPrecios(IdInventario)
        '    If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '        TextBox12.Text = SP.Precio.ToString
        '    End If
        'End If
        If e.KeyCode = Keys.Enter Then
            TextBox4.Focus()
        End If
        'If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
        '    If UsaFormula = 1 Then
        '        Dim Fo As New frmInventarioFormula01
        '        If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
        '            TextBox5.Text = Format(Fo.Resultado, "0.00")
        '            TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
        '        End If
        '        'TextBox12.Focus()
        '    End If
        'End If
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
            ComboBox6.Focus()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        LlenaCombos("tblcajas", ComboBox6, "nombre", "nombret", "idcaja", IdsCajas, "idcaja>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        ComboBox6.SelectedIndex = IdsCajas.Busca(IdCajaDefault)
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosCaja, 0)
        TextBox11.Text = Sf.Serie
        Dim V As New dbCajasMovimientos(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
    End Sub

    Private Function InsertaEnters(ByRef Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Then
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function
    Private Sub Imprimir()
        Dim en As New Encriptador
        Dim V As New dbCajasMovimientos(idMovimiento, MySqlcon)
        Dim RutaPDF As String
        'TextBox9.Text = 
        'TextBox10.Text = 
        'en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
        'Cadena = V.CreaCadenaOriginal(idVenta, IdMonedaG)
        'Sello = en.GeneraSello(Cadena, My.Settings.rutacer, Format(CDate(V.Fecha), "yyyy"))
        'Dim Enc As New System.Text.UTF8Encoding
        'Dim Bytes() As Byte = Enc.GetBytes(V.CreaXML(idVenta, IdMonedaG, Sello))
        'Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
        Dim Archivos As New dbSucursalesArchivos

        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        'IO.Directory.CreateDirectory(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        'IO.Directory.CreateDirectory(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        'en.GuardaArchivo(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFac-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        PrintDocument1.DocumentName = "MOV " + V.Serie + V.Folio.ToString
        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.CajasMovimientos)
        TipoImpresora = SA.TipoImpresora


        
        'obj.WriteSettings()
        If Impresora = "Bullzip PDF Printer" Then
            Dim obj As New Bullzip.PdfWriter.PdfSettings
            obj.Init()
            obj.PrinterName = Impresora
            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
            obj.SetValue("ShowSettings", "never")
            obj.SetValue("ShowPDF", "yes")
            obj.SetValue("ShowSaveAS", "nofile")
            obj.SetValue("ConfirmOverwrite", "no")
            obj.SetValue("Target", "printer")
            obj.WriteSettings()
        End If
        PrintDocument1.PrinterSettings.PrinterName = Impresora
        LlenaNodosImpresion()
        If TipoImpresora = 0 Then
            LlenaNodos(V.IdSucursal, TiposDocumentos.CajasMovimientos)
        Else
            LlenaNodos(V.IdSucursal, TiposDocumentos.CajasMovimientos + 1000)
        End If
        PrintDocument1.Print()
        'Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\Pedido " + V.Serie + V.Folio.ToString + ".pdf", 1000)
        'If V.Cliente.Email <> "" Then
        '    Try
        '        If MsgBox("¿Enviar pedido por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '            If V.Cliente.Email <> "" Then
        '                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
        '                Dim O As New dbOpciones(MySqlcon)
        '                Dim C As String
        '                C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "PEDIDO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Pedido enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
        '                M.send("Pedido: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\Pedido " + V.Serie + V.Folio.ToString + ".pdf", "")
        '            End If
        '        End If
        '    Catch ex As Exception
        '        MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    End Try
        'End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        DibujaPaginaN(e.Graphics)
        If MasPaginas = True Or NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If
        'If Estado = Estados.Cancelada Then
        '    e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
        'End If
        e.HasMorePages = MasPaginas
    End Sub
    Private Sub LlenaNodosImpresion()

        Dim V As New dbCajasMovimientos(idMovimiento, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idMovimiento, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
        Dim O As New dbOpciones(MySqlcon)
        Dim TotalDescuento As Double = 0
        Dim Vendedor As New dbVendedores(V.IdVendedor, MySqlcon)
        ImpND.Clear()

        ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpND.Add(New NodoImpresionN("", "direccion", Sucursal.Direccion, 0), "direccion")
        ImpND.Add(New NodoImpresionN("", "noexterior", Sucursal.NoExterior, 0), "noexterior")
        ImpND.Add(New NodoImpresionN("", "nointerior", Sucursal.NoInterior, 0), "nointerior")
        ImpND.Add(New NodoImpresionN("", "colonia", Sucursal.Colonia, 0), "colonia")
        ImpND.Add(New NodoImpresionN("", "ciudad", Sucursal.Ciudad, 0), "ciudad")
        ImpND.Add(New NodoImpresionN("", "estado", Sucursal.Estado, 0), "estado")
        ImpND.Add(New NodoImpresionN("", "cp", Sucursal.CP, 0), "cp")
        ImpND.Add(New NodoImpresionN("", "rfc", Sucursal.RFC, 0), "rfc")
        ImpND.Add(New NodoImpresionN("", "tel", Sucursal.Telefono, 0), "tel")
        ImpND.Add(New NodoImpresionN("", "email", Sucursal.Email, 0), "email")
        ImpND.Add(New NodoImpresionN("", "referencia", Sucursal.ReferenciaDomicilio, 0), "referencia")
        ImpND.Add(New NodoImpresionN("", "municipio", Sucursal.Municipio, 0), "municipio")
        ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
        If V.Tipo = 0 Then
            ImpND.Add(New NodoImpresionN("", "tipo", "INGRESO", 0), "tipo")
        Else
            ImpND.Add(New NodoImpresionN("", "tipo", "RETIRO", 0), "tipo")
        End If
        'ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        'ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        'ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        'If V.Cliente.DireccionFiscal = 0 Then
        '    ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
        '    ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
        '    ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
        '    ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
        '    ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
        '    ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
        '    ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        'Else
        '    ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
        '    ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
        '    ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
        '    ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
        '    ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
        '    ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
        '    ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
        '    ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        'End If
        'ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")

        'ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")

        ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")
        'ImpND.Add(New NodoImpresionN("", "foliobarras", "*$" + V.Serie + V.Folio.ToString + "$*", 0), "foliobarras")
        'ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        'ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")

        'ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        'ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        'ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        'ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        'ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbCajasMovimientosDetalles(MySqlcon)
        DR = VI.ConsultaReader(idMovimiento)
        ImpNDD.Clear()
        CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            'ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidad")) * (1 + DR("iva") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
            'ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + DR("iva") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
            'If DR("cantidad") <> 0 Then
            '    Dim Desc As Double
            '    Desc = (DR("precio") / (1 - DR("descuento") / 100))
            '    TotalDescuento += Desc - DR("precio")
            '    ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
            '    ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
            '    ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
            '    'Vo = Vd / ( 1 - (Por/100))
            'Else
            '    ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
            '    ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
            '    ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
            'End If

            CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()
        'ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        'ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
        'ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtotal + TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
        'ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idMovimiento)
        ImpNDDi.Clear()
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
        'For Each I As Double In Ivas
        '    ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'Next
        'If V.ISR <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "isr", Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        'If V.IvaRetenido <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "ivaretenido", Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")

        'Dim f As New StringFunctions
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), 2), 0), "totalletra")
        Dim CL As New CLetras
        If V.TotalVenta >= 0 Then
            ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalVenta, 2, GlobalIdiomaLetras), 0), "totalletra")
        Else
            ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalVenta * -1, 2, GlobalIdiomaLetras), 0), "totalletra")
        End If
        If V.Estado = Estados.Cancelada Then
            ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        'ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
        'ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
        Posicion = 0
        'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
        'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, "$#,###,##0.00") + "&id=" + V.uuid, System.Text.Encoding.Default)
        NumeroPagina = 1
    End Sub

    Private Sub LlenaNodos(ByVal pidSucursal As Integer, ByVal pDocumento As Integer)
        Dim I As New dbImpresionesN(MySqlcon)
        Dim Fs As FontStyle
        ImpNDi.Clear()
        Dim dr As MySql.Data.MySqlClient.MySqlDataReader
        Try
            dr = I.DaNodos(pDocumento, pidSucursal)
            While dr.Read
                Select Case dr("fuentestilo")
                    Case 1
                        Fs = FontStyle.Bold
                    Case 2
                        Fs = FontStyle.Italic
                    Case 0
                        Fs = FontStyle.Regular
                    Case 8
                        Fs = FontStyle.Strikeout
                    Case 4
                        Fs = FontStyle.Underline
                End Select
                ImpNDi.Add(New NodoImpresionN(dr("id"), dr("x"), dr("y"), dr("xl"), dr("yl"), dr("texto"), dr("datapropertyname"), New Font(dr("fuente").ToString, CSng(dr("fuentesize")), Fs, GraphicsUnit.Point), dr("alineacion"), dr("tipo"), dr("tipodato"), dr("visible"), dr("documento"), dr("tiponodo"), dr("idsucursal"), dr("conetiqueta"), dr("nombre"), dr("renglon"), dr("clasificacion")))
            End While
            dr.Close()
        Catch ex As Exception

        End Try

    End Sub
    

    Private Sub DibujaPaginaN(ByRef e As System.Drawing.Graphics)
        Dim ImpDb As New dbImpresionesN(MySqlcon)
        If TipoImpresora = 0 Then
            ImpDb.DaZonaDetalles(TiposDocumentos.CajasMovimientos, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.CajasMovimientos + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos

        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            'e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            'e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
            'End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF
        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos Detalles            

        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra

            '***********************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '***********************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next

            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName.Contains("cancelado") Then
                    e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
                End If
            Next
            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                'C = 0
                'If niva2.Visible = 1 Then
                '    YCoord = 0
                '    While C < ImpNDi2.Count
                '        'For Each n As NodoImpresionN In ImpNDi
                '        Nimp = ImpNDi2("iva" + Format(C, "00"))

                '        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                '        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                '        If niva.ConEtiqueta = 1 Then
                '            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        Else
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        End If
                '        'End If
                '        'Next
                '        YCoord += 4
                '        C += 1
                '    End While
                'End If

            End If

            If ncb.Visible = 1 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub DibujaPaginaFlujo(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer, ByVal pModo As Byte)
        Dim Nimp As NodoImpresionN
        Dim strF As New StringFormat
        Dim SA As New dbSucursalesArchivos
        'Dim ImpDb As New dbImpresionesN(MySqlcon)
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        MasPaginas = False
        Dim niva As New NodoImpresionN("iva", "0", "0", 0)
        Dim niva2 As New NodoImpresionN("iva2", "0", "0", 0)
        'Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.CajasMovimientos + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            '    If TipoImpresora = 0 Then
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    Else
            '        e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            '    End If
            'End If
        Catch ex As Exception

        End Try
        Dim Rec As RectangleF


        Dim XCoord As Integer = 0
        Dim YCoord As Integer = 0
        Dim C As Integer

        'Nodos fijos principio
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 0 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                    End If
                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(n.Y / 40 * 10), CInt(n.XL / 40 * 10), CInt(n.Y / 40 * 10))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS

            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                YCoord = 0
                If niva.Visible = 1 And niva.Tipo = 0 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva.Y / 40 * 10

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

                YCoord = 0
                'Ivas Sin Retenciones
                'C = 0
                'If niva2.Visible = 1 And niva2.Tipo = 0 Then
                '    YCoord = 0
                '    While C < ImpNDi2.Count
                '        'For Each n As NodoImpresionN In ImpNDi
                '        Nimp = ImpNDi2("iva" + Format(C, "00"))

                '        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                '        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                '        If niva.ConEtiqueta = 1 Then
                '            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        Else
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                '        End If
                '        'End If
                '        'Next
                '        YCoord += 4
                '        C += 1
                '    End While
                'End If

            End If
            'If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        For Each n As NodoImpresionN In ImpNDi
            If n.DataPropertyName.Contains("cancelado") Then
                e.DrawString(ImpND("cancelado").Valor, New Font(n.Fuente.Name, n.Fuente.Size, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, n.X / 40 * 10, n.Y / 40 * 10)
            End If
        Next
        'Nodos Detalles            
        XCoord = 0
        YCoord = 0
        'ImpDb.DaZonaDetalles(TiposDocumentos.Venta, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Dim LimY As Integer
        LimY = (pZonaY + pZonaYL) / 40 * 10
        C = Posicion
        YCoord = 0
        'Calcula páginas
        CuantaY = 0
        For Each n As NodoImpresionN In ImpNDD
            If n.DataPropertyName.Contains("descripcion") Then
                CuantaY = CuantaY + 4 + InsertaEnters(n.Valor, pZonaRG, YCoord, pZonaAlt)
            End If
        Next
        CuantaY = Math.Round(CuantaY / LimY) + 1
        Dim YExtra As Integer = 0
        Dim YExtra2 As Integer = 0
        While C < CuantosRenglones
            YExtra = 0
            YExtra2 = 0
            If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 0 Then
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            YCoord = YCoord + 4 + YExtra
            '*****************************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    HayRenglon = True
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta = 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            Else
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                    YExtra2 = InsertaEnters(Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    If YExtra < YExtra2 Then YExtra = YExtra2
                                Else
                                    e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, YCoord)
                                End If
                            End If
                        Case 1
                            Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                            e.DrawLine(Pluma, CInt(n.X / 40 * 10), YCoord, CInt(n.X * 10 / 40), YCoord)
                            'YCoord += n.y/40L + 1
                            'lineas
                        Case 2
                            'etiquetas
                            e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                    End Select
                End If
            Next
            If HayRenglon Then YCoord = YCoord + 4 + YExtra
            '********************************

            Posicion += 1
            C += 1
        End While


        'Nodos fijos final
        If MasPaginas = True And (pModo = 2 Or pModo = 3) Then
            NumeroPagina += 1
            Exit Sub
        End If
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                'If n.DataPropertyName = "iva2" Then niva2 = n
                'If n.DataPropertyName = "codigobi" Then ncb = n
                If n.DataPropertyName <> "iva" And n.DataPropertyName <> "iva2" And n.DataPropertyName <> "cancelado" And n.DataPropertyName <> "codigobi" Then
                    If n.Visible = 1 And n.Tipo = 2 Then
                        If MasPaginas And (n.DataPropertyName = "total" Or n.DataPropertyName = "subtotal" Or n.DataPropertyName = "totalletra") Then
                            'Cuando hay varias paginas---------------------------------------------------
                            'If n.DataPropertyName = "totalletra" Then
                            '    e.DrawString("Página " + NumeroPagina.ToString + " de " + CuantaY.ToString, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                            'End If
                        Else
                            'Normal
                            Select Case n.TipoNodo
                                Case 0
                                    'normal
                                    Nimp = ImpND(n.DataPropertyName)
                                    If n.TipoDato = 0 Then
                                        If n.ConEtiqueta = 1 Then
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta = 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                        e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                                    End If

                                Case 1
                                    Dim Pluma As New Pen(Color.Black, n.YL / 40 * 10)
                                    e.DrawLine(Pluma, CInt(n.X / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(n.XL / 40 * 10), CInt(Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord))
                                    'lineas
                                Case 2
                                    'etiquetas
                                    e.DrawString(n.Texto, n.Fuente, Brushes.Black, n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
                            End Select
                        End If
                    End If
                End If
            Next


            'IVAS
            Dim Ycoord2 As Integer
            If MasPaginas = False Then
                'Ivas con Retenciones
                C = 0
                Ycoord2 = 0
                If niva.Visible = 1 And niva.Tipo = 2 Then
                    While C < ImpNDDi.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDDi("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva.ConEtiqueta = 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva.Fuente, Brushes.Black, (niva.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva.Fuente, Brushes.Black, niva.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

                Ycoord2 = 0
                'Ivas Sin Retenciones
                'C = 0
                'If niva2.Visible = 1 And niva2.Tipo = 2 Then
                '    YCoord = 0
                '    While C < ImpNDi2.Count
                '        'For Each n As NodoImpresionN In ImpNDi
                '        Nimp = ImpNDi2("iva" + Format(C, "00"))

                '        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                '        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva2.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                '        If niva.ConEtiqueta = 1 Then
                '            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, Ycoord2)
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                '        Else
                '            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                '        End If
                '        'End If
                '        'Next
                '        Ycoord2 += 4
                '        C += 1
                '    End While
                'End If

            End If

            'If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
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
            ComboBox4.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged

    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If Estado = Estados.SinGuardar Then
            Modificar(Estados.SinGuardar)
        End If
        Imprimir()
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
                Dim V As New dbCajasMovimientos(MySqlcon)
                V.ActualizaComentario(idMovimiento, TextBox14.Text)
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

    Private Sub ComboBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox12.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub ComboBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim F As New frmPuntodeVentaCajaEstado(IdsCajas.Valor(ComboBox6.SelectedIndex))
        F.ShowDialog()
        F.Dispose()
    End Sub

    Private Sub TextBox14_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox14.TextChanged
        Dim textBox As TextBox = DirectCast(sender, TextBox)
        Dim I As Integer
        textBox.Text = UCase(textBox.Text)
        I = Len(textBox.Text)
        textBox.SelectionStart = I
    End Sub

    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub
End Class