Public Class frmFertilizantesPedido
    Dim idPedido As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim IdsVendedores As New elemento
    Dim idCliente As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim ColorRojo As Color = Color.FromArgb(255, 200, 200)
    Dim ColorVerde As Color = Color.FromArgb(200, 255, 200)
    'Dim IdsAlmacenes As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    'Dim IdVariante As Integer
    'Dim IdServicio As Integer
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
    Dim EsKit As Byte
    Dim SeparaKit As Byte
    Dim Op As dbOpciones
    'Dim P As New dbDescuentos(MySqlcon)
    'Dim CD As New dbFertilizantesPedidoDetalles(MySqlcon)
    Dim promocion1 As Integer
    Dim promocion2 As Integer
    Dim nombreProducto As String
    Dim cantAntModificar As Double
    Dim tipoElimianr As String
    Dim FP As dbFertilizantesPedido
    Dim FPD As dbFertilizantesPedidoDetalles
    Dim idsFormadePago As New elemento
    Dim ArtNombre As String
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
                'Dim c As New dbFertilizantesPedido(MySqlcon)
                Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                DR = FP.DaIdsIventario(idPedido, True)
                Dim Ids As New Collection
                While DR.Read
                    Ids.Add(New InventarioAFavorBase(DR("idinventario"), DR("cantidad")))
                End While
                DR.Close()
                For Each id As InventarioAFavorBase In Ids
                    'Restante = FP.ChecaTotalSurtido(idPedido, id)
                    'If Restante <> 0 Then
                    FP.DaInventarioaCliente(idCliente, id.IdInventario, id.Cantidad)
                    'End If
                Next
                FP.Eliminar(idPedido)
                'P.limpiarDescPromociones()
                'P.limpiarVentasdesc()
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
            Dim f As New frmInventarioConsulta(IdInventario, 0, 0)
            f.ShowDialog()
            f.Dispose()
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
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        ConsultaOn = False
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        Op = New dbOpciones(MySqlcon)
        FP = New dbFertilizantesPedido(MySqlcon)
        FPD = New dbFertilizantesPedidoDetalles(MySqlcon)
        Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
        IdVendedorU = U.IdVendedor
        Tabla.Columns.Add("Id", I.GetType)
        'Tabla.Columns.Add("TipoR", S.GetType)
        'Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Hras.", D.GetType)
        Tabla.Columns.Add("Cantxhra", D.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Precio U.", S.GetType)
        Tabla.Columns.Add("Importe", S.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        ComboBox6.Items.Add("Abierto")
        ComboBox6.Items.Add("Cerrado")
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1")
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(IdAlmacenG)
        LlenaCombos("tblformasdepago", ComboBox4, "concat(convert(if(tipo=0,'CRÉDITO',if(tipo=1,'CONTADO','PARCIALIDAD')) using utf8),'-',nombre)", "nombret", "idforma", idsFormadePago, "tipo<>2", , "idforma")

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
        'Panel3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                'Dim T As Double
                'Dim Iva As Double
                ' Dim V As New dbFertilizantesPedido(MySqlcon)
                FP.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)

                'If CheckBox1.Checked Then
                'Iva = T - (T / (1 + (O.Imp / 100)))
                Label12.Text = Format(FP.Subtotal, "#,###,##0.00")
                Label13.Text = Format(FP.TotalIva, "#,###,##0.00")
                Label14.Text = Format(FP.TotalVenta, "#,###,##0.00")
                Label32.Text = FP.TotalPeso.ToString + "Kg."
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
        Dim IdsGenerico As New elemento
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        ComboBox6.SelectedIndex = 0
        txtIEPS.Text = "0"
        txtIVARetenido.Text = "0"
        TextBox2.Enabled = True
        TextBox11.Enabled = True
        Label32.Text = "0.00Kg."
        Label43.Text = "0.00Kg."
        DateTimePicker1.Value = Date.Now
        ComboBox6.Enabled = False
        TextBox2.Text = ""
        TextBox1.Text = ""
        TextBox14.Text = ""
        Button22.Visible = False
        LlenaCombos("tblfertilizantespedidos", ComboBox7, " distinct cultivo", "nombret", "idpedido", IdsGenerico, "cultivo<>''", , "nombret", True)
        LlenaCombos("tblfertilizantespedidos", ComboBox9, " distinct tipoaplicacion", "nombret", "idpedido", IdsGenerico, "tipoaplicacion<>''", , "nombret", True)
        ComboBox7.Text = ""
        ComboBox9.Text = ""
        'TextBox15.Text = ""
        Button20.Enabled = True
        DataGridView2.Enabled = True
        DataGridView2.DataSource = Nothing
        DateTimePicker2.Value = Date.Now
        DateTimePicker3.Value = Date.Now
        DateTimePicker4.Value = Date.Now
        FolioAnt = 0
        ComboBox5.SelectedIndex = 0
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        If Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.FertilizantesPedidos, 0) Then
            TextBox11.Text = Sf.Serie
        Else
            TextBox11.Text = ""
        End If
        'Dim V As New dbVentasPedidos(MySqlcon)
        TextBox2.Text = FP.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        idPedido = 0
        Label42.Text = ""
        Label24.Visible = False
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        Panel1.Enabled = True
        Panel2.Enabled = True
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        If GlobaltpBanxico <> "Error" Then
            TextBox19.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox19.Text = CM.Cantidad.ToString
        End If
        'Dim o As New dbOpciones(MySqlcon)
        'TextBox8.Text = o.Imp.ToString
        NuevoConcepto()
        Button18.Enabled = False
        DataGridView1.DataSource = Nothing
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambiodeFolio, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
            TextBox11.Enabled = False
            TextBox2.Enabled = False
        End If
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox7.Focus()
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
                    'TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00") + vbCrLf + "A Favor: " + Format(c.DaSaldoAFavor(c.ID), "#,##0.00")
                    idCliente = c.ID
                    IdLista = c.IdLista
                    SIVA = c.IVA
                    Sobre = c.SobreescribeIVA
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        If IdVendedorU > 0 Then
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                        Else
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(c.IdVendedor)
                        End If
                    End If
                Else
                    TextBox7.Text = ""
                    'TextBox13.Text = ""
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
            ComboBox7.Text = ComboBox7.Text.ToUpper
            ComboBox9.Text = ComboBox9.Text.ToUpper
            'Dim C As New dbFertilizantesPedido(MySqlcon)
            'If TextBox2.Text = "" Then MensajeError = "Debe indicar un folio."
            'If FolioAnt <> TextBox2.Text Then
            '    If C.ChecaFolioRepetido(TextBox2.Text) Then
            '        MensajeError += " Ya existe una compra con este folio."
            '        TextBox2.BackColor = Color.FromArgb(250, 150, 150)
            '        Label17.Visible = True
            '    End If
            'End If
            If IsNumeric(TextBox16.Text) = False Then
                MensajeError += " Los dias de aplicación deben ser un valor numérico."
            End If
            If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosAlta, PermisosN.Secciones.Fertilizantes) = False And pEstado <> Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosCancelar, PermisosN.Secciones.Fertilizantes) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If FP.RevisaConceptos(idPedido) = False Then
                MensajeError += " Hay conceptos agregados en monedas diferentes favor de verificarlo."
            Else
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(FP.DaMoneda(idPedido))
            End If
            If MensajeError = "" Then
                If FP.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                    'Dim Sf As New dbSucursalesFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
                    'TextBox11.Text = Sf.Serie
                    TextBox2.Text = FP.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                End If
                'Dim O As New dbOpciones(MySqlcon)
                FP.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                FP.Modificar(idPedido, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, 0, pEstado, FP.Subtotal, FP.TotalVenta, idCliente, TextBox11.Text, IdsVendedores.Valor(ComboBox5.SelectedIndex), TextBox14.Text.Trim, ComboBox7.Text.Trim, ComboBox9.Text.Trim, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), CDbl(TextBox16.Text), Format(DateTimePicker4.Value, "yyyy/MM/dd"), "", CDbl(TextBox19.Text), idsFormadePago.Valor(ComboBox4.SelectedIndex), ComboBox6.SelectedIndex)
                If pEstado = Estados.Guardada Then Imprimir()
                If pEstado = Estados.Cancelada Then
                    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                    DR = FP.DaIdsIventario(idPedido, True)
                    Dim Ids As New Collection
                    While DR.Read
                        Ids.Add(New InventarioAFavorBase(DR("idinventario"), DR("cantidad")))
                    End While
                    DR.Close()
                    For Each id As InventarioAFavorBase In Ids
                        'Restante = FP.ChecaTotalSurtido(idPedido, id)
                        'If Restante <> 0 Then
                        FP.DaInventarioaCliente(idCliente, id.IdInventario, id.Cantidad)
                        'End If
                    Next

                    'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                    DR = FP.DaIdsIventario(idPedido, False)
                    'Dim Ids As New Collection
                    Ids.Clear()
                    While DR.Read
                        Ids.Add(DR("idinventario"))
                    End While
                    DR.Close()
                    'Dim Restante As Double
                    For Each id As Integer In Ids
                        'Restante = FP.ChecaTotalSurtido(idPedido, id)
                        'If Restante <> 0 Then
                        'FP.DaInventarioaCliente(idCliente, id, Restante)
                        FP.AjustaIventarioAFavorCliente(idCliente, id)
                        'End If
                    Next
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
            ComboBox7.Text = ComboBox7.Text.ToUpper
            ComboBox9.Text = ComboBox9.Text.ToUpper
            'If Button1.Text = "Guardar" Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.PedidosAlta, PermisosN.Secciones.Ventas) = True Then
                If idCliente <> 0 Then
                    'Dim C As New dbFertilizantesPedido(MySqlcon)
                    

                    If FP.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox11.Text) Then
                        TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                        Label17.Visible = True
                        FolioAnt = 0
                    Else
                        FolioAnt = CInt(TextBox2.Text)
                    End If
                    Dim O As New dbOpciones(MySqlcon)
                    FP.Guardar(idCliente, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), TextBox11.Text, IdsVendedores.Valor(ComboBox5.SelectedIndex), ComboBox7.Text.Trim, ComboBox9.Text.Trim, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker3.Value, "yyyy/MM/dd"), CDbl(TextBox16.Text), Format(DateTimePicker4.Value, "yyyy/MM/dd"), "", CDbl(TextBox19.Text), idsFormadePago.Valor(ComboBox4.SelectedIndex))
                    idPedido = FP.ID
                    Estado = 1
                    'Button1.Text = "Modificar"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    Button14.Enabled = True
                    Button18.Enabled = True
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
            BotonArticulo()
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
                If p.BuscaArticulo(TextBox3.Text, 0) Then
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
        'Dim C As New dbFertilizantesPedido(idPedido, MySqlcon)

        FP.ID = idPedido
        FP.LlenaDatos()
        ComboBox3.SelectedIndex = IdsSucursales.Busca(FP.IdSucursal)
        TextBox2.Text = FP.Folio.ToString
        FolioAnt = FP.Folio
        TextBox11.Text = FP.Serie
        TextBox1.Text = FP.Cliente.Clave
        Estado = FP.Estado
        'TextBox8.Text = fp.Iva.ToString

        ComboBox7.Text = FP.Cultivo
        ComboBox9.Text = FP.TipodeAplicacion
        'TextBox15.Text = FP.EquipoAplicacion
        DateTimePicker2.Value = FP.FechaFin
        DateTimePicker3.Value = FP.FechaFin
        DateTimePicker4.Value = FP.DiaEntrega
        TextBox16.Text = FP.DiasAplicacion.ToString
        If FP.IdVenta <> 0 Then
            Label42.Text = FP.FacturadoEn
            Button22.Visible = True
        Else
            Label42.Text = ""
            Button22.Visible = False
        End If
        Button2.Enabled = True
        DateTimePicker1.Value = FP.Fecha
        TextBox14.Text = FP.Comentario

        ComboBox4.SelectedIndex = idsFormadePago.Busca(FP.IdForma)
        ConsultaOn = False
        ComboBox6.SelectedIndex = FP.EstadoPedido
        ConsultaOn = True
        NuevoConcepto()
        If FP.EstadoPedido = 1 Then
            DataGridView2.Enabled = False
            Button20.Enabled = False
            ComboBox6.Enabled = False
        Else
            DataGridView2.Enabled = True
            Button20.Enabled = True
            ComboBox6.Enabled = True
        End If
        ConsultaDetalles()
        ConsultaMovimientos()
        ConsultaEquipos()
        Label43.Text = FP.ChecaTotalSurtidoGlobal(idPedido).ToString + "Kg."
        Button18.Enabled = False
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                DataGridView2.Enabled = False
                Button20.Enabled = False
                ComboBox6.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                If FP.EstadoPedido = 0 Then
                    Panel1.Enabled = True
                Else
                    Panel1.Enabled = False
                End If
                Panel2.Enabled = False
                Button2.Enabled = False
            Case Else
                ComboBox6.Enabled = False
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button2.Enabled = True
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
            Dim CD As New dbFertilizantesPedidoDetalles(MySqlcon)
            T = CD.ConsultaReader(idPedido)
            While T.Read
                If T("cantidad") <> 0 Then
                    'If T("idinventario") > 1 Then
                    Tabla.Rows.Add(T("iddetalle"), T("hectareas"), T("cantxhec"), T("cantidad"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    'Else
                    '   Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("pclave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    'End If
                Else
                    ' If T("idinventario") > 1 Then
                    Tabla.Rows.Add(T("iddetalle"), T("hectareas"), T("cantxhec"), T("cantidad"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    'Else
                    '   Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("pclave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    'End If
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
            'DGDetalles.Columns(1).Visible = False
            'DGDetalles.Columns(2).Visible = False
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
            DGDetalles.ClearSelection()
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox6.Text = "0"
        TextBox17.Text = "0"
        TextBox18.Text = "0"
        ArtNombre = ""
        UsaFormula = 0
        PrecioU = 0
        Label40.Text = ""
        PrecioBase = 0
        PrecioNeto = 0
        Button18.Enabled = False
        Button12.Visible = False
        Button6.Enabled = True
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
        'If Op._CursorVentas = "0" Then
        TextBox17.Focus()
        'Else
        'TextBox5.Text = "1"
        'TextBox3.Focus()
        'End If
    End Sub
    Private Sub AgregaArticulo()
        Try
            'Dim CD As New dbFertilizantesPedidoDetalles(MySqlcon)
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
            If IsNumeric(TextBox17.Text) = False Then
                MsgError += vbCrLf + "Las hectareas debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox18.Text) = False Then
                MsgError += vbCrLf + "La cantidad por hectarea debe ser un valor numérico."
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
                    'If SeparaKit = 0 Then
                    FPD.Guardar(idPedido, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), CDbl(txtIEPS.Text), CDbl(txtIVARetenido.Text), CDbl(TextBox17.Text), CDbl(TextBox18.Text), 0)
                    If FP.ChecaInventarioAFavor(idCliente, IdInventario) > 0 Then
                        If MsgBox("El cliente cuenta con inventario a favor. ¿Usar saldo?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            Dim Ncant As Double
                            Ncant = FP.ClienteInvAFavor - CDbl(TextBox5.Text)
                            If Ncant < 0 Then
                                Ncant = FP.ClienteInvAFavor
                            Else
                                Ncant = CDbl(TextBox5.Text)
                            End If
                            FPD.Guardar(idPedido, IdInventario, Ncant, CDbl(TextBox12.Text) * Ncant * -1, IDsMonedas.Valor(ComboBox1.SelectedIndex), "DESCUENTO " + TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), CDbl(txtIEPS.Text), CDbl(txtIVARetenido.Text), 1, Ncant, 1)

                            FP.QuitaInventarioaFavor(idCliente, IdInventario, Ncant)
                        End If
                    End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                    If Estado = Estados.Guardada Then
                        FP.ModificaTotales(idPedido, IDsMonedas.Valor(ComboBox1.SelectedIndex))
                    End If
                Else
                    'tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    FPD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), CDbl(txtIEPS.Text), CDbl(txtIVARetenido.Text), CDbl(TextBox17.Text), CDbl(TextBox18.Text))
                    If FPD.AFavor = 1 Then
                        If CDbl(TextBox5.Text) <> FPD.AfavorAnterior Then
                            FP.DaInventarioaCliente(idCliente, IdInventario, FPD.AfavorAnterior)
                            FP.QuitaInventarioaFavor(idCliente, IdInventario, CDbl(TextBox5.Text))
                        End If
                    End If
                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo modificado", 90)
                End If
                If Estado = Estados.Guardada Then
                    FP.ModificaTotales(idPedido, IDsMonedas.Valor(ComboBox1.SelectedIndex))
                End If
            Else
                MsgBox(MsgError, MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


   
    Private Sub BotonAgregar()
        If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosAlta, PermisosN.Secciones.Fertilizantes) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                If IdInventario <> 0 Then AgregaArticulo()
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
            'Dim CD As New dbFertilizantesPedidoDetalles(IdDetalle, MySqlcon)
            FPD.ID = IdDetalle
            FPD.LlenaDatos()
            TextBox5.Text = FPD.Cantidad.ToString
            ComboBox1.SelectedIndex = IDsMonedas.Busca(FPD.IdMoneda)
            IdInventario = FPD.Idinventario
            ArtNombre = FPD.Inventario.Nombre
            'IdVariante = FPD.IdVariante
            If IdInventario > 1 Then
                TextBox3.Text = FPD.Inventario.Clave
                PrecioNeto = FPD.Inventario.PrecioNeto
                EsKit = FPD.Inventario.EsKit
                '   IdVariante = 0
            Else
                PrecioNeto = 0
            End If
            'If IdVariante > 1 Then
            '    Dim P As New dbProductos(FPD.Producto.IdProducto, MySqlcon)
            '    TextBox3.Text = P.Clave
            '    IdInventario = 0
            'End If
            TextBox8.Text = FPD.Iva
            If IdInventario <> 1 Then
                UsaFormula = FPD.Inventario.UsaFormula
            End If
            TextBox17.Text = FPD.Hectareas.ToString("0.00")
            TextBox18.Text = FPD.Cantxhect.ToString("0.00")
            If FPD.Descuento = 0 Then
                If PrecioNeto = 0 Then
                    PrecioU = Math.Round(FPD.Precio / FPD.Cantidad, 2)
                Else
                    PrecioU = Math.Round(FPD.Precio / FPD.Cantidad * (1 + (FPD.Iva + FPD.IEPS - FPD.IVARetenido) / 100), 2)
                End If
            Else
                Dim Val As Double = (FPD.Precio / (1 - FPD.Descuento / 100))
                If PrecioNeto = 0 Then
                    PrecioU = Math.Round(Val / FPD.Cantidad, 2)
                Else
                    PrecioU = Math.Round(Val / FPD.Cantidad * (1 + (FPD.Iva + FPD.IEPS - FPD.IVARetenido) / 100), 2)
                End If
            End If
            TextBox12.Text = PrecioU.ToString("0.00")
            PrecioBase = PrecioU
            TextBox9.Text = FPD.Descuento.ToString
            txtIEPS.Text = FPD.IEPS.ToString
            txtIVARetenido.Text = FPD.IVARetenido.ToString
            CantAnt = FPD.Cantidad
            If Estado = Estados.Guardada Then
                Button18.Enabled = True
            Else
                Button18.Enabled = False
            End If
            'IdAlmacen = FPD.IdAlmacen
            If PrecioNeto = 0 Then
                If FPD.Descuento = 0 Then
                    TextBox6.Text = FPD.Precio.ToString("0.00")
                Else
                    TextBox6.Text = Format(PrecioU * FPD.Cantidad, "0.00")
                End If
            End If
            Button4.Text = "Modificar Concepto"
            TextBox4.Text = FPD.Descripcion

            nombreProducto = TextBox4.Text

            cantAntModificar = Double.Parse(TextBox5.Text)

            If FPD.Inventario.Inventariable = 1 Then
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
            Label40.Text = "Restante: " + FP.ChecaTotalSurtido(idPedido, FPD.Idinventario).ToString + " Kg."
            Label43.Text = FP.TotalSurtido.ToString + "Kg."
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
                'Dim c As New dbFertilizantesPedido(MySqlcon)
                Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                DR = FP.DaIdsIventario(idPedido, True)
                Dim Ids As New Collection
                While DR.Read
                    Ids.Add(New InventarioAFavorBase(DR("idinventario"), DR("cantidad")))
                End While
                DR.Close()
                For Each id As InventarioAFavorBase In Ids
                    'Restante = FP.ChecaTotalSurtido(idPedido, id)
                    'If Restante <> 0 Then
                    FP.DaInventarioaCliente(idCliente, id.IdInventario, id.Cantidad)
                    'End If
                Next
                FP.Eliminar(idPedido)
                'P.limpiarDescPromociones()
                'P.limpiarVentasdesc()
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
                    'Dim CD As New dbFertilizantesPedidoDetalles(MySqlcon)
                    'If EsKit = 1 Then
                    '    Dim IKits As New dbVentasKits(MySqlcon)
                    '    IKits.EliminarArticulosPedidos(IdDetalle)
                    'End If
                    'tipoElimianr = P.BuscarTipo(P.HayDescuento(CD.BuscaridInventario(IdDetalle), fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex)))
                    FP.DaInventarioaCliente(idCliente, IdInventario, FPD.AfavorAnterior)
                    FPD.Eliminar(IdDetalle)
                    'If tipoElimianr = "Promocion" Then

                    '    eliminarDescuescuento(P.descModificar(IdDetalle, "Pedidos"), tipoElimianr)
                    'Else

                    '    If P.descModificar(IdDetalle, "Pedidos") <> 0 Then
                    '        eliminarDescuescuento(P.descModificar(IdDetalle, "Pedidos"), tipoElimianr)
                    '    End If
                    'End If
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
            'TextBox13.Text = "Límite: " + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Días: " + B.Cliente.CreditoDias.ToString + vbCrLf + "Saldo: " + Format(B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00")
            'TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd")), "#,##0.00") + vbCrLf + "A Favor: " + Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "#,##0.00")
            idCliente = B.Cliente.ID
            IdLista = B.Cliente.IdLista
            Sobre = B.Cliente.SobreescribeIVA
            SIVA = B.Cliente.IVA
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                If IdVendedorU > 0 Then
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                Else
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(B.Cliente.IdVendedor)
                End If
            End If
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            'If Op._CursorVentas = "0" Then
            ComboBox7.Focus()
            'Else
            '   TextBox3.Focus()
            'End If
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BotonCliente()
    End Sub
    Private Sub BotonArticulo()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
        If Op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, 0, False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                        'TextBox12.Focus()
                        'Case "P"
                        '    LlenaDatosProducto(B.Producto)
                        '    'cmbVariante.Focus()
                        'Case "S"
                        '    LlenaDatosServicio(B.Servicio)
                        '    'TextBox12.Focus()
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
            Dim B As New frmBuscadorClases(TipodeBusqueda, 0, False, False, False)
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
        'a = Articulo.DaPrecioDefault
        If IsNumeric(TextBox5.Text) Then
            Cant = CDbl(TextBox5.Text)
        Else
            TextBox5.Text = "1"
            Cant = 1
        End If
        ArtNombre = Articulo.Nombre
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

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SacaTotal()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmFertilizantesPedidosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idPedido = f.IdVenta
            LlenaDatosVenta()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.PedidosAlta, PermisosN.Secciones.Fertilizantes) = True Then
                'Dim C As New dbFertilizantesPedido(MySqlcon)
                Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                DR = FP.DaIdsIventario(idPedido, True)
                Dim Ids As New Collection
                While DR.Read
                    Ids.Add(New InventarioAFavorBase(DR("idinventario"), DR("cantidad")))
                End While
                DR.Close()
                For Each id As InventarioAFavorBase In Ids
                    'Restante = FP.ChecaTotalSurtido(idPedido, id)
                    'If Restante <> 0 Then
                    FP.DaInventarioaCliente(idCliente, id.IdInventario, id.Cantidad)
                    'End If
                Next
                FP.Eliminar(idPedido)
                'P.limpiarDescPromociones()
                'P.limpiarVentasdesc()
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
            If FP.RevisaMovimientos(idPedido) = False Then
                Modificar(Estados.Cancelada)
            Else
                MsgBox("Debe de cancelar todos los movimientos para poder cancelar un pedido.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
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
        If IdInventario <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    'Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
    '    Try
    '        Dim Forma As New frmBuscaDocumentoVenta(0, False, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, False)
    '        If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
    '            Dim V As New dbVentasPedidos(MySqlcon)
    '            If Estado = 0 Then
    '                '0 cotizacion
    '                '1 pedido
    '                '2 remision
    '                '3 ventas
    '                Select Case Forma.Tipo
    '                    Case 0
    '                        Dim Co As New dbVentasCotizaciones(Forma.id(0), MySqlcon)
    '                        TextBox1.Text = Co.Cliente.Clave
    '                    Case 1
    '                        Dim Cp As New dbVentasPedidos(Forma.id(0), MySqlcon)
    '                        TextBox1.Text = Cp.Cliente.Clave
    '                    Case 2
    '                        Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
    '                        TextBox1.Text = Cr.Cliente.Clave
    '                    Case 3

    '                        Dim Cv As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
    '                        TextBox1.Text = Cv.Cliente.Clave
    '                End Select
    '                Guardar()
    '                If Estado <> 0 Then
    '                    V.AgregarDetallesReferencia(idPedido, Forma.id(0), Forma.Tipo)
    '                    ConsultaDetalles()
    '                End If
    '            Else
    '                V.AgregarDetallesReferencia(idPedido, Forma.id(0), Forma.Tipo)
    '                ConsultaDetalles()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
    '    End Try
    'End Sub

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.F1 And IdInventario <> 0 Then
            Dim SP As New frmSelectorPrecios(IdInventario)
            If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox12.Text = SP.Precio.ToString("0.00")
            End If
        End If
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
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
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.FertilizantesPedidos, 0)
        TextBox11.Text = Sf.Serie
        Dim V As New dbFertilizantesPedido(MySqlcon)
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
        Dim V As New dbFertilizantesPedido(idPedido, MySqlcon)
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

        RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FertilizantesPDF, False)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")
        'IO.Directory.CreateDirectory(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        'IO.Directory.CreateDirectory(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        'en.GuardaArchivo(My.Settings.rutaxml + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLFac-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        PrintDocument1.DocumentName = "PEDIDO " + V.Serie + V.Folio.ToString
        'Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = Archivos.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.FertilizantesPedido)
        TipoImpresora = Archivos.TipoImpresora

        Dim obj As New Bullzip.PdfWriter.PdfSettings
        obj.Init()
        obj.PrinterName = Impresora
        'obj.WriteSettings()
        If Impresora = "Bullzip PDF Printer" Then
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
            LlenaNodos(V.IdSucursal, TiposDocumentos.FertilizantesPedido)
        Else
            LlenaNodos(V.IdSucursal, TiposDocumentos.FertilizantesPedido + 1000)
        End If
        PrintDocument1.Print()
        'Bullzip.PdfWriter.PdfUtil.WaitForFile(RutaPDF + "\Pedido " + V.Serie + V.Folio.ToString + ".pdf", 1000)
        If V.Cliente.Email <> "" Then
            Try
                If MsgBox("¿Enviar pedido por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    If V.Cliente.Email <> "" Then
                        Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
                        Dim O As New dbOpciones(MySqlcon)
                        Dim C As String
                        C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "PEDIDO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Pedido enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
                        M.send("Pedido: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\Pedido " + V.Serie + V.Folio.ToString + ".pdf", "")
                    End If
                End If
            Catch ex As Exception
                MsgBox("No se puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
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

        Dim V As New dbFertilizantesPedido(idPedido, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idPedido, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
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
        ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
        ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
        ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
        If V.Cliente.DireccionFiscal = 0 Then
            ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
            ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
            ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
            ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
            ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
            ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
            ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
            ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
            ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio, 0), "refcliente")
        Else
            ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion2, 0), "direccioncliente")
            ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior2, 0), "noexteriorcliente")
            ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior2, 0), "nointeriorcliente")
            ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia2, 0), "coloniacliente")
            ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP2, 0), "cpcliente")
            ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad2, 0), "ciudadcliente")
            ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado2, 0), "estadocliente")
            ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio2, 0), "municipiocliente")
            ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
        End If
        ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")

        ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")

        ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")

        'agregar esto 
        ImpND.Add(New NodoImpresionN("", "cultivo", V.Cultivo, 0), "cultivo")
        ImpND.Add(New NodoImpresionN("", "tipoaplicacion", V.TipodeAplicacion, 0), "tipoaplicacion")
        ImpND.Add(New NodoImpresionN("", "fechainicio", V.FechaInicio, 0), "fechainicio")
        ImpND.Add(New NodoImpresionN("", "fechafin", V.FechaFin, 0), "fechafin")
        ImpND.Add(New NodoImpresionN("", "diasaplicacion", V.DiasAplicacion.ToString, 0), "diasaplicacion")
        ImpND.Add(New NodoImpresionN("", "diaentrega", V.DiaEntrega, 0), "diaentrega")
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        DR = V.ConsultaEquiposReader(V.ID)
        Dim Eq As String = ""
        While DR.Read
            If Eq = "" Then
                Eq = DR("nombre")
            Else
                Eq += "," + DR("nombre")
            End If
        End While
        DR.Close()
        ImpND.Add(New NodoImpresionN("", "equipoaplicacion", Eq, 0), "equipoaplicacion")
        ImpND.Add(New NodoImpresionN("", "tipodecambio", V.TipodeCambio, 0), "tipodecambio")
        Dim FormaP As New dbFormasdePago(V.IdForma, MySqlcon)
        ImpND.Add(New NodoImpresionN("", "formadepago", FormaP.Nombre, 0), "formadepago")

        If FormaP.Tipo = dbFormasdePago.Tipos.Contado Then
            ImpND.Add(New NodoImpresionN("", "condicionpago", "CONTADO", 0), "condicionpago")
        End If
        If FormaP.Tipo = dbFormasdePago.Tipos.Credito Then
            ImpND.Add(New NodoImpresionN("", "condicionpago", "CRÉDITO", 0), "condicionpago")
        End If
        'Hasta aqui
        'ImpND.Add(New NodoImpresionN("", "foliobarras", "*$" + V.Serie + V.Folio.ToString + "$*", 0), "foliobarras")
        'ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
        'ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
        'ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

        ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpND.Add(New NodoImpresionN("", "fecha2", Replace(V.Fecha, "/", "-"), 0), "fecha2")
        ImpND.Add(New NodoImpresionN("", "fechavencimiento", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "fechavencimiento")
        ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")




        'ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
        'ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
        'ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")
        'ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
        'ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
        'ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
        'ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")



        Dim VI As New dbFertilizantesPedidoDetalles(MySqlcon)
        DR = VI.ConsultaReader(idPedido)
        ImpNDD.Clear()
        CuantosRenglones = 0
        Dim TotalIEPS As Double = 0
        Dim TotalIVARetenido As Double = 0
        Dim Cont As Integer = 0
        While DR.Read
            ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))

            ImpNDD.Add(New NodoImpresionN("", "hectareas", Format(DR("hectareas"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "hectareas" + Format(Cont, "000"))
            ImpNDD.Add(New NodoImpresionN("", "cantxhec", Format(DR("cantxhec"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantxhec" + Format(Cont, "000"))

            If DR("cantidad") <> 0 Then

                ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidad")) * (1 + DR("iva") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + DR("iva") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))

                Dim Desc As Double
                Desc = (DR("precio") / (1 - DR("descuento") / 100))
                TotalDescuento += Desc - DR("precio")
                ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                'Vo = Vd / ( 1 - (Por/100))
                ImpNDD.Add(New NodoImpresionN("", "ieps", Format(DR("precio") * (DR("ieps") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ieps" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", Format(DR("precio") * (DR("ivaretenido") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "ivaRetenido" + Format(Cont, "000"))
                TotalIEPS += Double.Parse(DR("IEPS").ToString())
                TotalIVARetenido += Double.Parse(DR("IVARetenido").ToString())
            Else

                ImpNDD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciouiva", "", 0), "preciouiva" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "importeiva", "", 0), "importeiva" + Format(Cont, "000"))

                ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ieps", "", 0), "ieps" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "ivaRetenido", "", 0), "ivaRetenido" + Format(Cont, "000"))
            End If

            CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()
        ImpND.Add(New NodoImpresionN("", "Totalieps", Format(V.TotalIeps, O._formatoIva).PadLeft(O.EspacioIva), 0), "Totalieps")
        ImpND.Add(New NodoImpresionN("", "TotalivaRetenido", Format(V.TotalIvaRetenidoConceptos, O._formatoIva).PadLeft(O.EspacioIva), 0), "TotalivaRetenido")
        'ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
        ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
        ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtotal + TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
        ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
        ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(idPedido).ToString, 0), "totalcantidad")
        Dim Ivas As New Collection
        Dim IvasImporte As New Collection
        DR = V.DaIvas(idPedido)
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
        For Each I As Double In Ivas
            ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            Cont += 1
        Next
        If IvasImporte.Contains("0") Then
            If Ivas.Count > 1 And O._IVaCero = 1 Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
        End If
        'If V.ISR <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "isr", Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        'If V.IvaRetenido <> 0 Then
        '    ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "ivaretenido", Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), 0), "iva" + Format(Cont, "00"))
        '    Cont += 1
        'End If
        ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")
        ImpND.Add(New NodoImpresionN("", "Total2:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total2")

        'Dim f As New StringFunctions
        'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), 2), 0), "totalletra")
        Dim CL As New CLetras
        If V.TotalVenta >= 0 Then
            ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(V.TotalVenta, 2, GlobalIdiomaLetras), 0), "totalletra")
            ImpND.Add(New NodoImpresionN("", "totalletra2", CL.LetrasM(V.TotalVenta, 2, GlobalIdiomaLetras), 0), "totalletra2")
        Else
            ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(V.TotalVenta * -1, 2, GlobalIdiomaLetras), 0), "totalletra")
            ImpND.Add(New NodoImpresionN("", "totalletra2", "MENOS " + CL.LetrasM(V.TotalVenta * -1, 2, GlobalIdiomaLetras), 0), "totalletra2")
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
            ImpDb.DaZonaDetalles(TiposDocumentos.FertilizantesPedido, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.FertilizantesPedido + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex))
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesPedido, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesPedido + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
            '**********************************
            Dim HayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            'If YCoord >= LimY And Posicion > 0 Then
            '    MasPaginas = True
            '    Exit While
            'End If
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
            '********************************
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesPedido, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FertilizantesPedido + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
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
            '****************************************
            Dim hayRenglon As Boolean = False
            YExtra = 0
            YExtra2 = 0
            'If YCoord >= LimY And Posicion > 0 And pModo = 0 Then
            '    MasPaginas = True
            '    Exit While
            'End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" And n.Renglon = 1 Then
                    hayRenglon = True
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
            If hayRenglon Then YCoord = YCoord + 4 + YExtra

            '***************************************
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
                'Dim V As New dbFertilizantesPedido(MySqlcon)
                FP.ActualizaComentario(idPedido, TextBox14.Text)
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
    'Private Sub hayDescuento()

    '    'Dim CD As New dbVentasInventario(MySqlcon)
    '    Dim idDescuento As Integer
    '    idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
    '    Dim TablaDesc As DataTable
    '    Dim des As Double = 0
    '    Dim descripcion As String = ""

    '    If idDescuento = 0 Then
    '        'No hay descuento
    '    Else
    '        TablaDesc = P.tablaDesc(idDescuento)
    '        If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

    '            If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
    '                des = TotalPorcentaje(CDbl(TextBox6.Text), Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
    '                'des = des * Double.Parse(TextBox5.Text)
    '                des = des - (2 * des)
    '                descripcion = "DESCUENTO: " + TablaDesc.Rows(0)(2).ToString() + " %"
    '            Else
    '                des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
    '                des = des * Double.Parse(TextBox5.Text)
    '                des = des - (2 * des)
    '                descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U"
    '            End If

    '            'CD.Guardar(idVenta, 1, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, Double.Parse(TextBox5.Text), 1)
    '            CD.Guardar(idPedido, 1, CDbl(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), 0)
    '            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

    '            ConsultaDetalles()
    '            NuevoConcepto()

    '        Else
    '            nombreProducto = TextBox4.Text
    '            Promociones(Integer.Parse(TablaDesc.Rows(0)(10).ToString()), TablaDesc.Rows(0)(2).ToString(), CDbl(TextBox12.Text), "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, Integer.Parse(TablaDesc.Rows(0)(8).ToString()))
    '            'hay promocion
    '            'primero checar si se cumple la promocion
    '            'si no añadir

    '        End If

    '    End If


    '    'Si haye descuento, agregar el renglon a la venta
    '    'agregarlo a la tabla de descuentos

    'End Sub
    'Public Sub Promociones(ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer)
    '    Dim cDesc As Integer = 0
    '    Dim des As Double = 0
    '    Dim cant As Integer
    '    idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
    '    Dim regAnadir As Integer = 0
    '    Dim regDesc As Integer = 0
    '    valorProm(valor) 'esto establece los valores 2 x 1  y esos
    '    'primero que agregue el renglon a la db
    '    cant = Int(Double.Parse(TextBox5.Text)) 'cantidad de productos que se estan registrando
    '    For i As Integer = 1 To cant
    '        P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
    '    Next


    '    If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

    '        regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
    '        regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

    '        cDesc = promocion1 - promocion2
    '        des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
    '        des = des * regDesc

    '        'CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, regDesc, 1)
    '        CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), 0)
    '        P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

    '        ConsultaDetalles()
    '        NuevoConcepto()
    '        P.EliminarDesc(idPedido, idDescuento, idProducto)
    '        'anadir registros faltantes
    '        For i As Integer = 1 To regAnadir
    '            P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
    '        Next

    '    End If

    'End Sub
    'Public Sub modificarPromociones(ByVal idDescuento As Integer, ByVal valor As String, ByVal precio As Double, ByVal descripcion As String, ByVal idProducto As Integer)
    '    Dim cDesc As Integer = 0
    '    Dim des As Double = 0
    '    Dim regAnadir As Integer = 0
    '    idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
    '    Dim regDesc As Integer = 0
    '    Dim mayor As Integer
    '    valorProm(valor)
    '    If cantAntModificar <= Int(Double.Parse(TextBox5.Text)) Then
    '        mayor = Int(Double.Parse(TextBox5.Text)) - cantAntModificar 'los que estan de mas

    '        For i As Integer = 1 To mayor 'agregar los que se almacenaron
    '            P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
    '        Next

    '        If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

    '            regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
    '            regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

    '            cDesc = promocion1 - promocion2
    '            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
    '            des = des * regDesc

    '            'CD.Guardar(idVenta, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0, 0, 1, 0, 0, regDesc, 1)
    '            CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), 0)
    '            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

    '            ConsultaDetalles()
    '            NuevoConcepto()
    '            P.EliminarDesc(idPedido, idDescuento, idProducto)
    '            'anadir registros faltantes
    '            For i As Integer = 1 To regAnadir
    '                P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
    '            Next



    '        End If

    '    Else
    '        'hay que eliminar todos los registros de promociones y hacer los calculos otra vez
    '        P.EliminarDesc(idPedido, idDescuento, idProducto)
    '        P.EliminarDescAnadidosPed(idPedido, descripcion)
    '        Dim dt As DataTable
    '        Dim tot As Double = 0
    '        Dim tot2 As Integer
    '        dt = P.buscarDesAnadidosPed(idPedido, IdInventario)

    '        For i As Integer = 0 To dt.Rows.Count - 1
    '            tot = tot + Double.Parse(dt.Rows(i)(4).ToString())
    '        Next

    '        tot2 = Int(tot)

    '        For i As Integer = 1 To tot2 'agregar los que se almacenaron
    '            P.guardarPromocion(idPedido, idDescuento, idProducto, 0)
    '        Next

    '        If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

    '            regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
    '            regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

    '            cDesc = promocion1 - promocion2
    '            des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta
    '            des = des * regDesc

    '            CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), CDbl(TextBox9.Text), 1, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), 0) ',  IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
    '            P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "Pedidos")

    '            ConsultaDetalles()
    '            NuevoConcepto()
    '            P.EliminarDesc(idPedido, idDescuento, idProducto)
    '            'anadir registros faltantes
    '            For i As Integer = 1 To regAnadir
    '                P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
    '            Next
    '        End If

    '    End If

    'End Sub

    'Public Sub eliminarPromocion()
    '    Dim cDesc As Integer = 0
    '    Dim des As Double = 0
    '    'Dim cant As Integer
    '    Dim regAnadir As Integer = 0
    '    Dim precio As Double
    '    Dim descripcion As String
    '    Dim idDescuento As Integer
    '    Dim idProducto As Integer
    '    idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
    '    Dim regDesc As Integer = 0
    '    'Dim mayor As Integer
    '    Dim TablaDesc As DataTable
    '    Dim valor As String
    '    TablaDesc = P.tablaDesc(idDescuento)
    '    valor = TablaDesc.Rows(0)(2).ToString()
    '    valorProm(valor)
    '    descripcion = "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto
    '    idProducto = Integer.Parse(TablaDesc.Rows(0)(8).ToString())
    '    precio = Double.Parse(TextBox12.Text)
    '    P.EliminarDesc(idPedido, idDescuento, idProducto)
    '    P.EliminarDescAnadidosPed(idPedido, descripcion)
    '    Dim dt As DataTable
    '    Dim tot As Double = 0
    '    Dim tot2 As Integer
    '    dt = P.buscarDesAnadidosPed(idPedido, IdInventario)

    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        tot = tot + Double.Parse(dt.Rows(i)(4).ToString())
    '    Next

    '    tot2 = Int(tot)

    '    For i As Integer = 1 To tot2 'agregar los que se almacenaron
    '        P.guardarPromocion(idPedido, idDescuento, idProducto, 0)
    '    Next

    '    If P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() >= promocion1 Then

    '        regAnadir = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count() Mod promocion1)
    '        regDesc = Int(P.buscarPromociones(idPedido, idDescuento, idProducto).Rows.Count / promocion1) 'la cantidad de promociones a nadir

    '        cDesc = promocion1 - promocion2
    '        des = (precio * cDesc) * (-1) 'lo que se le va a descontar a la oferta


    '        CD.Guardar(idPedido, 1, regDesc, des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, 0, 0, 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), 0)
    '        P.GuardarDesc(CD.UltomoRegistro(), idDescuento, idPedido, "VentasN")

    '        ConsultaDetalles()
    '        NuevoConcepto()
    '        P.EliminarDesc(idPedido, idDescuento, idProducto)
    '        'anadir registros faltantes
    '        For i As Integer = 1 To regAnadir
    '            P.guardarPromocion(idPedido, idDescuento, idProducto, CD.UltomoRegistro())
    '        Next
    '    End If

    'End Sub
    'Public Sub modificarDescuento(ByVal idMod As Integer)
    '    Dim idDescuento As Integer
    '    idDescuento = P.HayDescuento(IdInventario, fechaFormato() + " " + horaFormato(), IdsSucursales.Valor(ComboBox3.SelectedIndex))
    '    Dim TablaDesc As DataTable
    '    Dim des As Double = 0
    '    Dim descripcion As String = ""

    '    If idDescuento = 0 Then
    '        'No hay descuento
    '    Else
    '        TablaDesc = P.tablaDesc(idDescuento)
    '        If TablaDesc.Rows(0)(9).ToString() <> "Promocion" Then

    '            If TablaDesc.Rows(0)(9).ToString() = "Porcentaje" Then
    '                des = TotalPorcentaje(CDbl(TextBox6.Text), Integer.Parse(TablaDesc.Rows(0)(2).ToString()))
    '                'des = des * Double.Parse(TextBox5.Text)
    '                des = des - (2 * des)
    '                descripcion = "DESCUENTO: " + TablaDesc.Rows(0)(2).ToString() + " %"
    '            Else
    '                des = Double.Parse(TablaDesc.Rows(0)(2).ToString())
    '                des = des * Double.Parse(TextBox5.Text)
    '                des = des - (2 * des)
    '                descripcion = "DESCUENTO: $" + TablaDesc.Rows(0)(2).ToString() + " P/U"
    '            End If
    '            CD.Modificar(idMod, Double.Parse(TextBox5.Text), des, IDsMonedas.Valor(ComboBox1.SelectedIndex), descripcion, CDbl(TextBox8.Text), 0, Double.Parse(txtIEPS.Text), Double.Parse(txtIVARetenido.Text), 0, 0)
    '            P.ModificarDescuento(idMod, idDescuento, idPedido, "Pedidos")
    '            ConsultaDetalles()
    '            NuevoConcepto()
    '        Else
    '            'promociones

    '            modificarPromociones(idDescuento, TablaDesc.Rows(0)(2).ToString(), Double.Parse(TextBox12.Text), "DESCUENTO: promoción " + TablaDesc.Rows(0)(2).ToString() + " " + nombreProducto, IdInventario)

    '        End If
    '    End If


    '    'Si haye descuento, agregar el renglon a la venta
    '    'agregarlo a la tabla de descuentos
    'End Sub

    'Public Sub eliminarDescuescuento(ByVal idElim As Integer, ByVal Tipo As String)
    '    If Tipo <> "Promocion" Then
    '        CD.Eliminar(idElim)
    '        P.EliminarDesc(idElim, "VentasN")
    '    Else
    '        eliminarPromocion()
    '    End If

    'End Sub
    'Public Sub valorProm(ByVal valor As String)
    '    Dim aux As String = ""
    '    Dim bandera As Boolean = False

    '    For j As Integer = 0 To valor.Length() - 1
    '        If bandera = False Then
    '            'agarrar el primero
    '            If valor.Chars(j) <> "x" Then
    '                aux = aux + valor.Chars(j)

    '            Else
    '                ' es X
    '                promocion1 = Integer.Parse(aux)
    '                bandera = True
    '                aux = ""
    '            End If


    '        Else
    '            'agarrar el segundo numero
    '            aux = aux + valor.Chars(j)
    '        End If


    '    Next
    '    promocion2 = Integer.Parse(aux)
    'End Sub
    'Public Function horaFormato() As String
    '    Dim fechita As String
    '    Dim Aux As String = ""
    '    fechita = Now.ToString("HH:mm:ss")

    '    For j As Integer = 0 To 7
    '        Aux = Aux + fechita.Chars(j)
    '    Next
    '    fechita = Aux

    '    Return fechita
    'End Function

    'Public Function fechaFormato() As String
    '    Dim fechita2 As String
    '    fechita2 = Date.Now.Year.ToString() + "/" + Integer.Parse(Date.Now.Month.ToString).ToString("00") + "/" + Integer.Parse(Date.Now.Day.ToString).ToString("00")
    '    Return fechita2
    'End Function
    ''Metodo de sacar el Total del descuento
    'Public Function TotalPorcentaje(ByVal total As Double, ByVal porcentaje As Integer) As Double
    '    Dim desc As Double = 0
    '    desc = (total * porcentaje) / 100
    '    Return desc 'devuelve el descuento solamente
    'End Function

    Private Sub txtIEPS_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIEPS.Leave
        If txtIEPS.Text = "" Then
            txtIEPS.Text = "0"
        End If
    End Sub

    Private Sub txtIVARetenido_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIVARetenido.Leave
        If txtIVARetenido.Text = "" Then
            txtIVARetenido.Text = "0"
        End If
    End Sub

    Private Sub frmVentasPedidos_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Panel3.Left = (Me.Size.Width / 2) - (Me.MinimumSize.Width / 2 - 1)
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Fertilizantes.MovimientosVer, PermisosN.Secciones.Fertilizantes) = False Then
            MsgBox("No  tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            Exit Sub
        End If
        If ComboBox6.SelectedIndex = 0 Then
            Dim F As New frmFertilizantesMovimientos(idPedido, IdInventario, 0, TextBox4.Text, PrecioU, IdsSucursales.Valor(ComboBox3.SelectedIndex))
            F.ShowDialog()
            F.Dispose()
            ConsultaMovimientos()
            ConsultaDetalles()
            'If IdInventario <> 0 Then
            Label40.Text = "Restante: " + FP.ChecaTotalSurtido(IdInventario, idPedido).ToString + " Kg."
            Label43.Text = FP.TotalSurtido.ToString + " Kg."
            'End If
        Else
        MsgBox("No se puede agregar movimientos a un pedido cerrado.", MsgBoxStyle.Critical, GlobalNombreApp)
        End If
    End Sub
    Private Sub ConsultaMovimientos()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView1.RowCount > 0 Then PrimerCeldaRow = DataGridView1.FirstDisplayedCell.RowIndex
                Dim P As New dbFertilizantesMovimientos(MySqlcon)
                DataGridView1.DataSource = P.Consulta(idPedido)
                DataGridView1.Columns(0).Visible = False
                DataGridView1.Columns(1).Visible = False
                DataGridView1.Columns(8).Visible = False
                DataGridView1.Columns(2).HeaderText = "Código"
                DataGridView1.Columns(3).HeaderText = "Descripción"
                DataGridView1.Columns(4).HeaderText = "Movimiento"
                DataGridView1.Columns(5).HeaderText = "Peso Neto"
                DataGridView1.Columns(6).HeaderText = "Nodriza"
                DataGridView1.Columns(7).HeaderText = "Estado"
                DataGridView1.Columns(8).HeaderText = "Dev."
                DataGridView1.Columns(9).HeaderText = "G/C"
                DataGridView1.Columns(4).Width = 80
                DataGridView1.Columns(5).Width = 80
                DataGridView1.Columns(7).Width = 80
                DataGridView1.Columns(8).Width = 80
                DataGridView1.Columns(9).Width = 30
                DataGridView1.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView1.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView1.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
                DataGridView1.ClearSelection()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub ConsultaEquipos()
        Try
            Dim PrimerCeldaRow As Integer = -1
            If ConsultaOn Then
                If DataGridView2.RowCount > 0 Then PrimerCeldaRow = DataGridView2.FirstDisplayedCell.RowIndex

                DataGridView2.DataSource = FP.ConsultaEquipos(idPedido)
                DataGridView2.Columns(0).Visible = False
                DataGridView2.Columns(1).HeaderText = "Descripción"
                'DataGridView1.Columns(3).HeaderText = "Descripción"
                'DataGridView1.Columns(4).HeaderText = "Movimiento"
                'DataGridView1.Columns(5).HeaderText = "Peso Neto"
                'DataGridView1.Columns(6).HeaderText = "Nodriza"
                'DataGridView1.Columns(7).HeaderText = "Estado"
                'DataGridView1.Columns(1).Width = 150
                DataGridView2.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                If DataGridView2.RowCount > PrimerCeldaRow And PrimerCeldaRow > -1 Then DataGridView2.FirstDisplayedScrollingRowIndex = PrimerCeldaRow
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
   

    


    Private Sub DateTimePicker2_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker2.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker3.Focus()
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        TextBox16.Text = CStr(DateDiff(DateInterval.Day, DateTimePicker2.Value, DateTimePicker3.Value) + 1)
    End Sub

    Private Sub DateTimePicker3_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker3.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker4.Focus()
        End If
    End Sub

    Private Sub DateTimePicker3_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker3.ValueChanged
        TextBox16.Text = CStr(DateDiff(DateInterval.Day, DateTimePicker2.Value, DateTimePicker3.Value) + 1)
    End Sub

    Private Sub DateTimePicker4_KeyDown(sender As Object, e As KeyEventArgs) Handles DateTimePicker4.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox17.Focus()
        End If
    End Sub

    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker4.ValueChanged

    End Sub

    Private Sub TextBox17_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox17.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox18.Focus()
        End If
    End Sub

    Private Sub TextBox17_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox17.KeyPress

    End Sub

    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.TextChanged
        If IsNumeric(TextBox17.Text) And IsNumeric(TextBox18.Text) Then
            TextBox5.Text = CStr(CDbl(TextBox17.Text) * CDbl(TextBox18.Text))
        Else
            TextBox5.Text = "0"
        End If
    End Sub

    Private Sub TextBox18_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox18.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox5.Focus()
        End If
    End Sub

    Private Sub TextBox18_TextChanged(sender As Object, e As EventArgs) Handles TextBox18.TextChanged
        If IsNumeric(TextBox17.Text) And IsNumeric(TextBox18.Text) Then
            TextBox5.Text = CStr(CDbl(TextBox17.Text) * CDbl(TextBox18.Text))
        Else
            TextBox5.Text = ""
        End If
    End Sub

    Private Sub TextBox16_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox16.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker4.Focus()
        End If
    End Sub

    Private Sub TextBox16_TextChanged(sender As Object, e As EventArgs) Handles TextBox16.TextChanged

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

       
    End Sub

  

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim F As New frmFertilizantesMovimientos(idPedido, DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value, DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex).Value, DataGridView1.Item(3, DataGridView1.CurrentCell.RowIndex).Value, PrecioU, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        F.ShowDialog()
        F.Dispose()
        Label40.Visible = True
        Label40.Text = "Restante: " + FP.ChecaTotalSurtido(idPedido, DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex).Value).ToString + " Kg."
        Label43.Text = FP.TotalSurtido.ToString + "Kg."
        ConsultaMovimientos()
        ConsultaDetalles()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If Estado = 0 Then
            Guardar()
        End If
        If Estado <> 0 Then
            Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.EquiposSucursales, IdsSucursales.Valor(ComboBox3.SelectedIndex), False, False, False)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                FP.AgregaEquipo(B.EquipoSucursal.ID, idPedido)
                ConsultaEquipos()
            End If
        End If
        
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        If MsgBox("¿Quitar este equipo del pedido?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            FP.EliminaEquipo(DataGridView2.Item(0, DataGridView2.CurrentCell.RowIndex).Value)
            ConsultaEquipos()
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ConsultaOn Then
            If ComboBox6.SelectedIndex = 1 And Estado = Estados.Guardada Then
                If FP.RevisaMovimientosEntransito(idPedido) Then
                    MsgBox("No puede cerrar un pedido con movimientos en tránsito.", MsgBoxStyle.Information, GlobalNombreApp)
                    ComboBox6.SelectedIndex = 0
                    Exit Sub
                End If
                If MsgBox("¿Cerrar este pedido? Esta operación no se puede deshacer.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Try
                        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
                        DR = FP.DaIdsIventario(idPedido, False)
                        Dim Ids As New Collection
                        While DR.Read
                            Ids.Add(DR("idinventario"))
                        End While
                        DR.Close()
                        Dim Restante As Double
                        For Each id As Integer In Ids
                            Restante = FP.ChecaTotalSurtido(idPedido, id)
                            If Restante <> 0 Then
                                FP.DaInventarioaCliente(idCliente, id, Restante)
                            End If
                        Next
                        Panel1.Enabled = False
                        FP.CierraPedido(idPedido)
                        ComboBox6.Enabled = False
                    Catch ex As Exception
                        MsgBox("No se pudo cerrar el pedido." + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox7_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox9.Focus()
        End If
    End Sub

    Private Sub ComboBox9_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox9.KeyDown
        If e.KeyCode = Keys.Enter Then
            DateTimePicker2.Focus()
        End If
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Dim Fac As New frmVentasN(FP.IdVenta, 0, 0, 0)
        Fac.ShowDialog()
        Fac.Dispose()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If DataGridView1.Item(9, e.RowIndex).Value = "C" Then
            e.CellStyle.BackColor = ColorRojo
        Else
            If DataGridView1.Item(7, e.RowIndex).Value = "Surtido" Then
                e.CellStyle.BackColor = ColorVerde
            End If
        End If
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class