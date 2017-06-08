Public Class frmInventarioMovimientosN
    Dim IdsVariantes As New elemento
    Dim idMovimiento As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim IdsVendedores As New elemento
    Dim idCliente As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim IdsAlmacenes2 As New elemento
    Dim IdsMovimientos As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As Integer
    Dim IdVariante As Integer
    Dim IdServicio As Integer
    Dim PrecioU As Double
    'Dim Tabla As New DataTable
    Dim IdsSucursales As New elemento
    Dim idsFormasDePago As New elemento
    Dim SerieAnt As String
    Dim Cadena As String
    Dim Sello As String
    Dim Isr As Double
    Dim IvaRetenido As Double
    Dim PrecioBase As Double
    Dim iTipoFacturacion As Byte
    Dim LlenandoDatos As Boolean = False
    Dim ImpDoc As ImprimirDocumento
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim LimitedeFolios As Boolean = False
    Dim CosteoTiempoREal As Byte
    Dim IdVenta As Integer
    Dim IdRemision As Integer
    Dim EsSemillas As Boolean
    Dim Lotes As Byte
    Dim Aduana As Byte
    Dim IdPedido As Integer
    Dim Transito As Byte
    Dim IdAlmacenA As Integer
    Dim IdAlmacenB As Integer
    Dim IdSucursalB As Integer
    Dim IdConceptoP As Integer
    Dim IdPedidoV As Integer
    Dim O As dbOpciones
    Dim Almacen As dbAlmacenes
    'Dim SerieAnt As String
    'Dim FolioAnt As Integer
    Public Sub New(Optional ByVal pidVenta As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idMovimiento = pidVenta
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(pIdPedido As Integer, pIdAlmacenA As Integer, pIdAlmacenB As Integer, pIdSucursalB As Integer, pIdConeptoP As Integer)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idMovimiento = 0
        ' Add any initialization after the InitializeComponent() call.
        IdPedido = pIdPedido
        IdAlmacenA = pIdAlmacenA
        IdAlmacenB = pIdAlmacenB
        IdSucursalB = pIdSucursalB
        IdConceptoP = pIdConeptoP
    End Sub
    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este movimiento no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesAMovimiento(idMovimiento)
                Dim C As New dbMovimientos(idMovimiento, MySqlcon)
                C.QuitarBoletas(idMovimiento)
                'C.RegresaInventario(idVenta)
                C.Eliminar(idMovimiento, GlobalTipoCosteo, CDbl(TextBox1.Text), CosteoTiempoREal)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmInventarioMovimientosN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled Then
                Modificar(Estados.Guardada)
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled Then
                Modificar(Estados.Pendiente)
            End If
        End If
    End Sub

    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Icon = GlobalIcono
        Catch ex As Exception

        End Try
        DGDetalles.AutoGenerateColumns = False
        ConsultaOn = False
        CheckScroll.Checked = My.Settings.movimientosscroll
        Dim I As Integer = 0
        Dim S As String = ""
        Dim D As Double = 0
        o = New dbOpciones(MySqlcon)
        ImpDoc = New ImprimirDocumento()
        Almacen = New dbAlmacenes(MySqlcon)
        Almacen.AlmacenesSinPermiso(GlobalIdUsuario)
        CosteoTiempoREal = o.CostoTiempoReal
       
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        'LlenaCombos("tblformasdepago", ComboBox4, "nombre", "nombret", "idforma", idsFormasDePago, , , "idforma")
        LlenaCombos("tblmonedas", ComboBox4, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        'LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)

        LlenaCombos("tblalmacenes", cmbAlmacenDestino, "nombre", "nombret", "idalmacen", IdsAlmacenes2, "idalmacen<>1")
        LlenaCombos("tblinventarioconceptos", ComboBox6, "nombre", "nombret", "idconcepto", IdsMovimientos, "idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        ConsultaOn = True
        If idMovimiento = 0 Then
            Nuevo(IdPedido)
        Else
            'Nuevo()
            LlenaCombos("tblalmacenes", cmbAlmacenOrigen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            LlenaDatosVenta()
            NuevoConcepto()
        End If
        If IdPedido <> 0 Then
            'Nuevo()
            ComboBox3.SelectedIndex = IdsSucursales.Busca(IdSucursalB)
            ComboBox6.SelectedIndex = IdsMovimientos.Busca(idconceptoP)
            cmbAlmacenOrigen.SelectedIndex = IdsAlmacenes.Busca(IdAlmacenB)
            cmbAlmacenDestino.SelectedIndex = IdsAlmacenes2.Busca(IdAlmacenA)
            CrearDesdePedido()
        End If
        TextBox2.Focus()
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim V As New dbMovimientos(MySqlcon)
                V.DaTotal(idMovimiento, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Label12.Text = Format(Math.Round(V.Subtotal, 2), "#,##0.00")
                'Label13.Text = Format(Math.Round(V.TotalIva - V.TotalISR - V.TotalIvaRetenido, 2), "#,##0.00")
                Label14.Text = Format(Math.Round(V.TotalVenta, 2), "#,##0.00")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo(pidPedido As Integer)
        DateTimePicker1.Value = Date.Now
        IdVenta = 0
        IdRemision = 0
        IdPedido = pidPedido
        Transito = 0
        btnRecibir.Enabled = False
        Label11.Visible = False
        TextBox7.Text = ""
        FolioAnt = 0
        idMovimiento = 0
        IdPedidoV = 0
        idCliente = 0

        Panel1.Enabled = True
        Panel3.Enabled = True
        Panel2.Enabled = True
        btnReferencia.Enabled = True
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        CheckBox1.Checked = False
        ComboBox3.Enabled = True
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        TextBox11.Enabled = True
        TextBox2.Enabled = True
        Dim CM As New dbMonedasConversiones(1, MySqlcon)
        TextBox1.Text = CM.Cantidad.ToString
        Estado = Estados.Inicio
        iTipoFacturacion = GlobalTipoFacturacion
        LlenaCombos("tblalmacenes", cmbAlmacenOrigen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        'LlenaCombos("tblinventarioconceptos", ComboBox6, "nombre", "nombret", "idconcepto", IdsMovimientos, "idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'TextBox11.Text = S.Serie
        cmbAlmacenOrigen.SelectedIndex = IdsAlmacenes.Busca(S.IdAlmacenM)
        'Dim Sf As New dbSucursalesFolios(MySqlcon)
        'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.MovimientosInventario, 0)
        'TextBox11.Text = Sf.Serie
        If ComboBox6.Items.Count > 0 Then
            Dim C As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            TextBox11.Text = C.Serie
            Dim V As New dbMovimientos(MySqlcon)
            TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex)).ToString
            If CInt(TextBox2.Text) < C.Folio Then TextBox2.Text = C.Folio.ToString
        End If

        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        CheckBox1.Checked = False
        SerieAnt = ""
        Button2.Enabled = True
        Label24.Visible = False
        TextBox14.Text = ""
        ComboBox6.Enabled = True
        ConsultaOn = False
        txtcliente.Text = ""
        txtdatoscliente.Text = ""
        ConsultaOn = True
        txtcliente.Enabled = True
        btnBuscarCliente.Enabled = True
        'ComboBox4.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        NuevoConcepto()
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.CambioSucursal, PermisosN.Secciones.Inventario) = False Then
            ComboBox3.Enabled = False
        End If
        'If CInt(TextBox2.Text) > Sf.FolioFinal Then
        '    LimitedeFolios = True
        '    MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, NombreApp)
        'Else
        '    LimitedeFolios = False
        'End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Modificar(Estados.Pendiente)
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Button1.Enabled = False
            Button14.Enabled = False
            Dim MensajeError As String = ""
            Dim Op As New dbOpciones(MySqlcon)
            Dim C As New dbMovimientos(MySqlcon)
            Dim Concepto As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            Dim Desglozar As Byte

            If IsNumeric(TextBox2.Text) = False Then
                MensajeError = "El folio debe ser un valor numérico."
            Else
                If FolioAnt <> CInt(TextBox2.Text) Or SerieAnt <> TextBox11.Text Then
                    If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex)) Then
                        If pEstado = Estados.Guardada Then
                            MensajeError = "Folio repetido."
                        End If
                        'TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
                    End If
                End If
            End If


            If pEstado = Estados.Guardada And GlobalSoloExistencia = True And (Concepto.Tipo = dbInventarioConceptos.Tipos.Salida Or Concepto.Tipo = dbInventarioConceptos.Tipos.Traspaso) Then MensajeError += C.VerificaExistencias(idMovimiento)
            If pEstado = Estados.Cancelada Then
                Dim ConPermiso As Boolean = True
                Dim Mov As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
                If Mov.Tipo = dbInventarioConceptos.Tipos.Entrada Or Mov.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarEntrada, PermisosN.Secciones.Inventario)
                End If
                If Mov.Tipo = dbInventarioConceptos.Tipos.Salida Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarSalida, PermisosN.Secciones.Inventario)
                End If
                If Mov.Tipo = dbInventarioConceptos.Tipos.Ajuste Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarAjuste, PermisosN.Secciones.Inventario)
                End If
                If Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosCancelarTraspaso, PermisosN.Secciones.Inventario)
                End If
                If ConPermiso = False Then MensajeError += " No tiene permiso para realizar esta operación."
                If C.TieneBoletas(idMovimiento) Then
                    MensajeError += " Hay boletas asignadas a este movimiento, tiene que eliminarlas primero."
                End If
            End If
            If pEstado = Estados.Guardada Then
                Dim ConPermiso As Boolean = True
                Dim Mov As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
                If Mov.Tipo = dbInventarioConceptos.Tipos.Entrada Or Mov.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaEntrada, PermisosN.Secciones.Inventario)
                End If
                If Mov.Tipo = dbInventarioConceptos.Tipos.Salida Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaSalidas, PermisosN.Secciones.Inventario)
                End If
                If Mov.Tipo = dbInventarioConceptos.Tipos.Ajuste Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaAjuste, PermisosN.Secciones.Inventario)
                End If
                If Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso Then
                    ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaTraspaso, PermisosN.Secciones.Inventario)
                End If
                If ConPermiso = False Then MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If MensajeError = "" Then
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                'Dim Sf As New dbSucursalesFolios(MySqlcon)
                'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasCotizaciones, iTipoFacturacion)
                'C.Modificar(idVenta, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), Desglozar, 0, TextBox11.Text, Sf.NoAprobacion, Sc.NoSerie, Sf.YearAprobacion, iTipoFacturacion, pEstado, idsFormasDePago.Valor(ComboBox4.SelectedIndex), 0, CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), C.Subtototal, C.TotalVenta, idCliente, IdsVendedores.Valor(ComboBox5.SelectedIndex), TextBox14.Text)
                C.DaTotal(idMovimiento, IDsMonedas.Valor(ComboBox4.SelectedIndex))
                C.Modificar(idMovimiento, CInt(TextBox2.Text), pEstado, TextBox14.Text, TextBox11.Text, C.Subtotal, C.TotalVenta, CDbl(TextBox1.Text), IDsMonedas.Valor(ComboBox4.SelectedIndex), Format(DateTimePicker1.Value, "yyyy/MM/dd"), IdVenta, IdRemision, IdPedido, IdPedidoV, idCliente)
                Estado = pEstado
                If pEstado = Estados.Cancelada Then
                    Dim S As New dbInventarioSeries(MySqlcon)
                    S.QuitaSeriesAMovimiento(idMovimiento)
                    C.RegresaInventario(idMovimiento, GlobalTipoCosteo, CDbl(TextBox1.Text), CosteoTiempoREal)
                    C.ReCalculaCostos(idMovimiento, GlobalTipoCosteo, CosteoTiempoREal, CDbl(TextBox1.Text))
                    'If MsgBox("¿Imprimir movimiento Cancelada?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then

                    '                End If
                End If
                If pEstado = Estados.Guardada Then
                    Estado = Estados.Guardada
                    If Op.RecibidoDefault = 1 Then
                        C.Recibir(idMovimiento)
                    End If
                    C.ModificaInventario(idMovimiento, GlobalTipoCosteo, CDbl(TextBox1.Text))
                    C.ReCalculaCostos(idMovimiento, GlobalTipoCosteo, CosteoTiempoREal, CDbl(TextBox1.Text))
                    If MsgBox("¿Imprimir documento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Imprimir(True)
                    End If
                    Dim Ss As New dbInventarioSeries(MySqlcon)
                    If Ss.CantidadDeSeriesAgregadasaMovimiento(idMovimiento, 0) > 0 Then
                        If MsgBox("¿Imprimir listado de series?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                            ImprimirSeries()
                        End If
                    End If
                End If
                If pEstado = Estados.Guardada Or pEstado = Estados.Cancelada Then GeneraPoliza(idMovimiento)
                Nuevo(0)
            Else
                MsgBox(MensajeError, MsgBoxStyle.Information, GlobalNombreApp)
                Button1.Enabled = True
                Button14.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            Estado = Estados.Pendiente
            Button1.Enabled = True
            Button14.Enabled = True
        End Try
    End Sub
    Private Sub Guardar()
        Try
            Dim ConPermiso As Boolean = True
            Dim Mov As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            If Mov.Tipo = dbInventarioConceptos.Tipos.Entrada Or Mov.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaEntrada, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Salida Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaSalidas, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Ajuste Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaAjuste, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaTraspaso, PermisosN.Secciones.Inventario)
            End If
            If ConPermiso Then
                Dim C As New dbMovimientos(MySqlcon)
                If IsNumeric(TextBox2.Text) = False Then
                    MsgBox("El folio debe ser un valor numérico", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                If IsNumeric(TextBox1.Text) = False Then
                    MsgBox("El tipo de cambio debe ser un valor numérico", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                If C.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex)) Then
                    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                    Label17.Visible = True
                    FolioAnt = 0
                Else
                    FolioAnt = TextBox2.Text
                End If
                C.Guardar(CInt(TextBox2.Text), Format(DateTimePicker1.Value, "yyyy/MM/dd"), IdsMovimientos.Valor(ComboBox6.SelectedIndex), TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), CDbl(TextBox1.Text), IDsMonedas.Valor(ComboBox4.SelectedIndex), IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex), IdPedido, idCliente)
                idMovimiento = C.ID
                Estado = 1

                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button15.Enabled = True
                ComboBox6.Enabled = False
                ComboBox3.Enabled = False
                If GlobalconIntegracion Then
                    Panel3.Enabled = False
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
            If txtCosto.Enabled = True Then
                txtCosto.Focus()
            Else
                BotonAgregar()
            End If
        End If
        If e.KeyCode = Keys.F1 Then
            BuscaArticuloBoton()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 1, False, False, False, True) Then
                    LlenaDatosArticulo(p)
                Else
                    IdInventario = 0
                    TextBox4.Text = ""
                    TextBox6.Text = "0"
                    cmbUbicacionOrigen.Text = ""
                    cmbUbicacionDestino.Text = ""
                    PrecioU = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub LlenaDatosVenta()
        LlenandoDatos = True
        Dim C As New dbMovimientos(idMovimiento, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        ComboBox6.SelectedIndex = IdsMovimientos.Busca(C.IdConcepto)
        ComboBox4.SelectedIndex = IDsMonedas.Busca(C.IdMoneda)
        Dim Concep As New dbInventarioConceptos(C.IdConcepto, MySqlcon)
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio

        Estado = C.Estado
        TextBox1.Text = C.TipodeCambio.ToString
        TextBox11.Text = C.Serie
        SerieAnt = C.Serie
        IdVenta = C.IdVenta
        IdRemision = C.idRemision
        TextBox7.Text = C.FolioRef
        TextBox14.Text = C.Comentario
        'If C.Desglosar = 1 Then
        '    CheckBox1.Checked = True
        'Else
        '    CheckBox1.Checked = False
        'End If
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        IdPedido = C.IdPedido
        Transito = C.Transito
        If Transito = 0 Then
            Label11.Text = "En Tránsito"
        Else
            Label11.Text = "Recibido"
        End If
        txtcliente.Text = C.ClienteClRef
        idCliente = C.IdCliente
        'ConsultaOn = True
        'LlenaDatosDetalles()
        ConsultaDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel3.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                Button15.Enabled = True
                ComboBox6.Enabled = False
                ComboBox3.Enabled = False
                TextBox11.Enabled = False
                TextBox2.Enabled = False
                btnRecibir.Enabled = False
                Label11.Visible = False
                txtcliente.Enabled = False
                btnBuscarCliente.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Button2.Enabled = False
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel3.Enabled = False
                Panel2.Enabled = False
                Button15.Enabled = True
                ComboBox6.Enabled = False
                ComboBox3.Enabled = False
                TextBox11.Enabled = False
                TextBox2.Enabled = False
                If Transito = 0 Then
                    btnRecibir.Enabled = True
                Else
                    btnRecibir.Enabled = False
                End If
                Label11.Visible = True
                If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosEdicion, PermisosN.Secciones.Inventario) Then
                    Panel1.Enabled = True
                    Panel2.Enabled = True
                    Button1.Enabled = False
                    Button14.Enabled = True
                End If
                txtcliente.Enabled = False
                btnBuscarCliente.Enabled = False
            Case Else
                Label11.Visible = False
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                If GlobalconIntegracion Then
                    Panel3.Enabled = False
                Else
                    Panel3.Enabled = True
                End If
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Button15.Enabled = True
                ComboBox6.Enabled = False
                ComboBox3.Enabled = False
                TextBox11.Enabled = True
                TextBox2.Enabled = True
                btnRecibir.Enabled = False
                txtcliente.Enabled = True
                btnBuscarCliente.Enabled = True
        End Select


        LlenandoDatos = False
        If Concep.Tipo <> dbInventarioConceptos.Tipos.Traspaso Then
            'Button19.Visible = False
            Label11.Visible = False
        End If
    End Sub

    Private Sub ConsultaDetalles()
        Try
            Dim CD As New dbMovimientosDetalles(MySqlcon)
            DGDetalles.DataSource = CD.Consulta(idMovimiento, 0, 0, cmbAlmacenDestino.Visible)

            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        IdVariante = 0
        IdServicio = 0
        txtCosto.Text = "0"
        TextBox3.Text = ""
        TextBox4.Text = ""
        txtCantidad.Text = "0"
        cmbUbicacionOrigen.Text = ""
        cmbUbicacionDestino.Text = ""
        cmbUbicacionOrigen.Visible = False
        cmbUbicacionDestino.Visible = False
        lblUbicacionOrigen.Visible = False
        lblUbicacionDestino.Visible = False
        cmbUbicacionOrigen.Enabled = False
        cmbUbicacionDestino.Enabled = False

        TextBox6.Text = "0"
        PrecioBase = 0
        Button12.Visible = False
        Button9.Enabled = False
        TextBox3.Enabled = True
        Button6.Enabled = True
        EsSemillas = False
        Button4.Text = "Agregar Concepto"
        If GlobalPermisos.ChecaPermiso(PermisosN.Inventario.CambiodeAlamcen, PermisosN.Secciones.Inventario) = False Then
            cmbAlmacenOrigen.Enabled = False
            cmbAlmacenDestino.Enabled = False
        Else
            cmbAlmacenOrigen.Enabled = True
            cmbAlmacenDestino.Enabled = True
        End If
        Button16.Visible = False
        Lotes = 0
        Aduana = 0
        Button25.Enabled = False
        Button28.Enabled = False
        txtCantidad.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbMovimientosDetalles(MySqlcon)
            Dim MsgError As String = ""
            Dim InvAnt As Double
            If IdInventario = 0 Then MsgError += "Debe indicar un artículo."
            If Not IsNumeric(txtCantidad.Text) Then MsgError += "La cantidad debe ser un valor numérico."
            If Not IsNumeric(txtCosto.Text) Then MsgError += vbCrLf + "El costo debe ser un valor numérico."

            Dim I As New dbInventario(IdInventario, MySqlcon)
            If I.UsaUbicacion And cmbUbicacionOrigen.SelectedIndex = -1 And cmbUbicacionDestino.SelectedIndex = -1 Then MsgError += vbCrLf + "Debe indicar una ubicación."

            If Almacen.TienePermiso(IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex)) = False Then
                MsgError += vbCrLf + " No tiene permiso para realizar operaciones en el almacén seleccionado."
            End If
            Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)

            'Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)

            If Concep.Tipo = dbInventarioConceptos.Tipos.Traspaso Then
                If Not I.UsaUbicacion And IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex) = IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex) Then
                    MsgError += vbCrLf + "No se puede hacer un traspaso al mismo almacen."
                ElseIf I.UsaUbicacion And IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex) = IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex) And cmbUbicacionDestino.SelectedValue = cmbUbicacionOrigen.SelectedValue Then
                    MsgError += vbCrLf + "No se puede hacer un traspaso al mismo almacen y ubicación."
                End If
            End If

            If IdInventario > 1 And GlobalSoloExistencia And I.Inventariable = 1 And (Concep.Tipo = dbInventarioConceptos.Tipos.Salida Or Concep.Tipo = dbInventarioConceptos.Tipos.Traspaso) Then
                InvAnt = I.DaInventario(IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdInventario)
                If InvAnt < CDbl(txtCantidad.Text) And I.Inventariable = 1 Then
                    MsgError += " Artículo sin existencia suficiente."
                End If
                If IdInventario > 1 And GlobalSoloExistencia And I.Inventariable = 1 And (Concep.Tipo = dbInventarioConceptos.Tipos.Salida Or Concep.Tipo = dbInventarioConceptos.Tipos.Traspaso) And I.UsaUbicacion Then
                    InvAnt = I.DaInventario(IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdInventario, cmbUbicacionOrigen.SelectedValue)
                    If InvAnt < CDbl(txtCantidad.Text) And I.Inventariable = 1 Then
                        MsgError += " Ubicación de origen sin existencia suficiente."
                    End If

                End If
            End If

            If (IdVenta <> 0 Or IdRemision <> 0) And Button4.Text = "Agregar Concepto" Then
                MsgError += " No se pueden agregar artículos a una salida por surtir."
            End If
            If MsgError = "" Then
                If Button4.Text = "Agregar Concepto" Then
                    btnReferencia.Enabled = False
                    CD.Guardar(idMovimiento, IdInventario, CDbl(txtCantidad.Text), CDbl(TextBox6.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), Trim(TextBox4.Text), IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex), IdVariante, 1, InvAnt, If(cmbUbicacionOrigen.Visible, cmbUbicacionOrigen.SelectedValue, ""), If(cmbUbicacionDestino.Visible, cmbUbicacionDestino.SelectedValue, ""))
                    If ManejaSeries <> 0 Then
                        If Concep.Tipo <> dbInventarioConceptos.Tipos.Traspaso Then
                            If CD.NuevoConcepto Then
                                Dim F As New frmInventarioSeries(IdInventario, 0, 0, CDbl(txtCantidad.Text), DateTimePicker1.Value, idMovimiento, 0)
                                F.ShowDialog()
                                F.Dispose()
                            Else
                                Dim F As New frmInventarioSeries(IdInventario, 0, 0, CD.Cantidad, DateTimePicker1.Value, idMovimiento, 0)
                                F.ShowDialog()
                                F.Dispose()
                            End If
                        End If
                    End If
                    If EsSemillas And (Concep.Tipo = dbInventarioConceptos.Tipos.Entrada Or Concep.Tipo = dbInventarioConceptos.Tipos.Salida Or Concep.Tipo = dbInventarioConceptos.Tipos.InventarioInicial) Then
                        Dim Tipoboleta As String = ""
                        If Concep.Tipo = dbInventarioConceptos.Tipos.Entrada Or Concep.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                            Tipoboleta = "E"
                        End If
                        If Concep.Tipo = dbInventarioConceptos.Tipos.Salida Then
                            Tipoboleta = "S"
                        End If
                        Dim Boleta As New frmSemillasBoleta(Tipoboleta, TextBox3.Text, CDbl(txtCantidad.Text), CD.ID, GlobalSemillasResumida, cmbAlmacenOrigen.Text, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
                        Boleta.ShowDialog()
                        txtCantidad.Text = Boleta.pesoBrutoAnalizado.ToString
                        CD.ModificarCantidad(CD.ID, Boleta.pesoBrutoAnalizado, CDbl(TextBox6.Text))
                        Boleta.Dispose()
                    End If
                    If Lotes = 1 Then
                        Dim F As New frmInventarioLotes(0, 0, 0, 0, CDbl(txtCantidad.Text), IdInventario, 0, 0, 0, CD.ID, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), cmbAlmacenOrigen.Text, Concep.Tipo, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    If Aduana = 1 Then
                        Dim F As New frmInventarioAduana(0, 0, 0, 0, CDbl(txtCantidad.Text), IdInventario, 0, 0, 0, CD.ID, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), cmbAlmacenOrigen.Text, Concep.Tipo, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If


                    'If IdVariante <> 0 Then
                    'Dim PV As New dbProductosVariantes(MySqlcon)
                    'PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If

                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(txtCantidad.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex), CDbl(TextBox6.Text), If(cmbUbicacionOrigen.Visible, cmbUbicacionOrigen.SelectedValue, ""), If(cmbUbicacionDestino.Visible, cmbUbicacionDestino.SelectedValue, ""))

                    If ManejaSeries <> 0 Then
                        'Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
                        If Concep.Tipo <> dbInventarioConceptos.Tipos.Traspaso Then
                            Dim F As New frmInventarioSeries(IdInventario, 0, 0, CDbl(txtCantidad.Text), DateTimePicker1.Value, idMovimiento, 0)
                            F.ShowDialog()
                            F.Dispose()
                        End If
                    End If

                    If EsSemillas And (Concep.Tipo = dbInventarioConceptos.Tipos.Entrada Or Concep.Tipo = dbInventarioConceptos.Tipos.Salida Or Concep.Tipo = dbInventarioConceptos.Tipos.InventarioInicial) Then
                        Dim Tipoboleta As String = ""
                        If Concep.Tipo = dbInventarioConceptos.Tipos.Entrada Or Concep.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                            Tipoboleta = "E"
                        End If
                        If Concep.Tipo = dbInventarioConceptos.Tipos.Salida Then
                            Tipoboleta = "S"
                        End If
                        Dim Boleta As New frmSemillasBoleta(Tipoboleta, TextBox3.Text, CDbl(txtCantidad.Text), IdDetalle, GlobalSemillasResumida, cmbAlmacenOrigen.Text, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
                        Boleta.ShowDialog()
                        txtCantidad.Text = Boleta.pesoBrutoAnalizado.ToString
                        CD.ModificarCantidad(CD.ID, Boleta.pesoBrutoAnalizado, CDbl(TextBox6.Text))
                        Boleta.Dispose()
                    End If
                    'Dim I As New dbInventario(MySqlcon)
                    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    If Lotes = 1 Then
                        Dim F As New frmInventarioLotes(0, 0, 0, 0, CDbl(txtCantidad.Text), IdInventario, 0, 0, 0, IdDetalle, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), cmbAlmacenOrigen.Text, Concep.Tipo, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    If Aduana = 1 Then
                        Dim F As New frmInventarioAduana(0, 0, 0, 0, CDbl(txtCantidad.Text), IdInventario, 0, 0, 0, IdDetalle, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), cmbAlmacenOrigen.Text, Concep.Tipo, 0, 0)
                        F.ShowDialog()
                        F.Dispose()
                    End If
                    'If IdVariante <> 0 Then
                    'Dim PV As New dbProductosVariantes(MySqlcon)
                    'PV.ModificaInventario(IdVariante, CantAnt * -1, IdAlmacen)
                    'PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdAlmacen)
                    'End If
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





    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        BotonAgregar()
    End Sub
    Private Sub BotonAgregar()
        'If (PermisosInventario And CULng((Math.Pow(2, perInventario.Ajuste_Inventario + 2)))) <> 0 Then
        If Estado = 0 Then
            Guardar()
        End If
        If Estado <> 0 Then
            If IdInventario <> 0 Or IdVariante <> 0 Then AgregaArticulo()
        End If
        'Else
        'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        'TextBox3.Focus()
        'End If
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        If e.RowIndex >= 0 Then LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try

            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbMovimientosDetalles(IdDetalle, MySqlcon)


            IdInventario = CD.Idinventario
            IdVariante = CD.idVariante
            If IdInventario > 1 Then
                ConsultaOn = False
                TextBox3.Text = CD.Inventario.Clave
                ConsultaOn = True
                IdVariante = 0
                If CD.Inventario.Semillas = 0 Then
                    EsSemillas = False
                    Button16.Visible = False
                Else
                    EsSemillas = True
                    Button16.Visible = True
                End If
                Lotes = CD.Inventario.PorLotes
                Aduana = CD.Inventario.Aduana
                If Aduana = 1 Then
                    Button25.Enabled = True
                Else
                    Button25.Enabled = False
                End If
                If Lotes = 1 Then
                    Button28.Enabled = True
                Else
                    Button28.Enabled = False
                End If
            End If
            txtCantidad.Text = CD.Cantidad.ToString
            If CD.Cantidad = 0 Then
                PrecioU = 0
            Else
                PrecioU = CD.Precio / CD.Cantidad
            End If

            PrecioBase = PrecioU
            txtCosto.Text = PrecioU.ToString
            CantAnt = CD.Cantidad
            IdAlmacen = CD.IdAlmacen

            TextBox6.Text = CD.Precio.ToString
            TextBox4.Text = CD.Descripcion
            cmbUbicacionOrigen.Text = CD.Ubicacion
            cmbUbicacionDestino.Text = CD.UbicacionD
            Button4.Text = "Modificar Concepto"
            If IdVenta <> 0 Or IdRemision <> 0 Then
                Button6.Enabled = False
                TextBox3.Enabled = False
            End If
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            ComboBox2.SelectedIndex = IDsMonedas2.Busca(CD.IdMoneda)

            cmbAlmacenOrigen.SelectedIndex = IdsAlmacenes.Busca(CD.IdAlmacen)
            cmbAlmacenDestino.SelectedIndex = IdsAlmacenes2.Busca(CD.IdAlmacen2)
            cmbAlmacenDestino.Enabled = False
            cmbAlmacenOrigen.Enabled = False

            Dim Mov As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            Dim articulo As New dbInventario(IdInventario, MySqlcon)
            lblUbicacionOrigen.Visible = articulo.UsaUbicacion
            cmbUbicacionOrigen.Visible = articulo.UsaUbicacion
            lblUbicacionDestino.Visible = articulo.UsaUbicacion And Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso
            cmbUbicacionDestino.Visible = articulo.UsaUbicacion And Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso
            cmbUbicacionOrigen.DataSource = articulo.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdInventario)
            cmbUbicacionOrigen.SelectedValue = CD.Ubicacion
            cmbUbicacionDestino.DataSource = articulo.Ubicaciones(IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex), IdInventario)
            cmbUbicacionDestino.SelectedValue = CD.UbicacionD
            cmbUbicacionDestino.Enabled = Estado = Estados.Inicio Or Estado = Estados.Pendiente Or Estado = Estados.SinGuardar Or (Transito = 0 And Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso)
            cmbUbicacionOrigen.Enabled = Estado = Estados.Inicio Or Estado = Estados.Pendiente Or Estado = Estados.SinGuardar

            'cmbtipoarticulo.Text = "A"
            'ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            If CheckScroll.Checked Then
                txtCantidad.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este movimiento no ha sido guardado. ¿Desea iniciar un nuevo movimiento? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesAMovimiento(idMovimiento)
                Dim C As New dbMovimientos(idMovimiento, MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idVenta)
                C.Eliminar(idMovimiento, 1, CDbl(TextBox1.Text), CosteoTiempoREal)
                Nuevo(0)
            End If
        Else
            Nuevo(0)
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            Dim ConPermiso As Boolean = True
            Dim Mov As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            If Mov.Tipo = dbInventarioConceptos.Tipos.Entrada Or Mov.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaEntrada, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Salida Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaSalidas, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Ajuste Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaAjuste, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaTraspaso, PermisosN.Secciones.Inventario)
            End If
            If ConPermiso Then
                If MsgBox("¿Desea eliminar este concepto?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbMovimientosDetalles(MySqlcon)
                    CD.QuitarBoletaxArticulo(IdDetalle)
                    CD.Eliminar(IdDetalle)
                    Dim S As New dbInventarioSeries(MySqlcon)
                    S.QuitarSeriesAmovimientoxArticulo(IdInventario, idMovimiento)
                    'If IdInventario <> 0 Then
                    '    Dim I As New dbInventario(MySqlcon)
                    '    I.MovimientoDeInventario(IdInventario, CantAnt, CantAnt, dbInventario.TipoMovimiento.Alta, IdAlmacen)
                    'End If
                    'If IdVariante <> 0 Then
                    '    Dim PV As New dbProductosVariantes(MySqlcon)
                    '    PV.ModificaInventario(IdVariante, CantAnt * -1, IdAlmacen)
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



    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        BuscaArticuloBoton()
    End Sub
    Private Sub BuscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloInv
        Dim op As New dbOpciones(MySqlcon)
        If op.BusquedaporClases = 0 Then
            Dim B As New frmBuscador(TipodeBusqueda, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), False, False, True)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                        txtCosto.Focus()
                End Select
            End If
            B.Dispose()
        Else
            Dim B As New frmBuscadorClases(TipodeBusqueda, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), False, False, True)
            B.ShowDialog()
            If B.DialogResult = Windows.Forms.DialogResult.OK Then
                Select Case B.Tipo
                    Case "I"
                        LlenaDatosArticulo(B.Inventario)
                        txtCosto.Focus()
                End Select
            End If
            B.Dispose()
        End If
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        'Dim a As dbInventarioPrecios
        Dim Cant As Double
        TextBox4.Text = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        If IsNumeric(txtCantidad.Text) Then
            Cant = CDbl(txtCantidad.Text)
        Else
            txtCantidad.Text = "1"
            Cant = 1
        End If
        Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
        If Concep.Tipo = dbInventarioConceptos.Tipos.Entrada Or Concep.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
            Articulo.DaUltimaidMoneda(Articulo.ID)
            PrecioU = Math.Round(Articulo.DaUltimoCosto(Articulo.ID), 2)

            ComboBox2.SelectedIndex = IDsMonedas.Busca(Articulo.idMonedaCompra)
        Else
            PrecioU = Articulo.CostoBase
            ComboBox2.SelectedIndex = IDsMonedas.Busca(2)
        End If
        PrecioBase = PrecioU
        txtCosto.Text = Format(PrecioU, "0.00")
        ManejaSeries = Articulo.ManejaSeries
        TextBox6.Text = Cant * PrecioU
        Lotes = Articulo.PorLotes
        Aduana = Articulo.Aduana
        If Aduana = 1 Then
            Button25.Enabled = True
        Else
            Button25.Enabled = False
        End If
        If Lotes = 1 Then
            Button28.Enabled = True
        Else
            Button28.Enabled = False
        End If
        'cmbtipoarticulo.Text = "A"
        IdInventario = Articulo.ID
        If Articulo.Semillas = 1 Then
            EsSemillas = True
            Button16.Visible = True
        Else
            EsSemillas = False
            Button16.Visible = False
        End If
        If ManejaSeries = 0 Then
            Button12.Visible = False
        Else
            If Concep.Tipo <> dbInventarioConceptos.Tipos.Traspaso Then
                Button12.Visible = True
            Else
                Button12.Visible = False
            End If

        End If
        ConsultaOn = False
        TextBox3.Text = Articulo.Clave
        TextBox3.Select(TextBox3.Text.Length, 0)
        'ComboBox1.SelectedIndex = IDsMonedas.Busca(a.IdMoneda)

        Dim dbmov As New dbInventario(MySqlcon)
        cmbUbicacionDestino.DataSource = dbmov.Ubicaciones(IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex), IdInventario)
        cmbUbicacionOrigen.DataSource = dbmov.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdInventario)

        lblUbicacionOrigen.Visible = False
        cmbUbicacionOrigen.Visible = False
        lblUbicacionDestino.Visible = False
        cmbUbicacionDestino.Visible = False
        cmbUbicacionOrigen.Enabled = True
        cmbUbicacionDestino.Enabled = True
        Dim inv As New dbInventario(IdInventario, MySqlcon)
        Select Case Concep.Tipo
            Case 0, 4
                lblUbicacionOrigen.Visible = inv.UsaUbicacion
                cmbUbicacionOrigen.Visible = inv.UsaUbicacion
            Case 1
                lblUbicacionOrigen.Visible = inv.UsaUbicacion
                cmbUbicacionOrigen.Visible = inv.UsaUbicacion
            Case 3
                lblUbicacionOrigen.Visible = inv.UsaUbicacion
                cmbUbicacionOrigen.Visible = inv.UsaUbicacion
                lblUbicacionDestino.Visible = inv.UsaUbicacion
                cmbUbicacionDestino.Visible = inv.UsaUbicacion
        End Select
        cmbUbicacionDestino.Text = ""
        cmbUbicacionOrigen.Text = ""

        ConsultaOn = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'SacaTotal()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        'SacaTotal()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim f As New frmMovimientosConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idMovimiento = f.IdMovimiento
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim ConPermiso As Boolean = True
            Dim Mov As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            If Mov.Tipo = dbInventarioConceptos.Tipos.Entrada Or Mov.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaEntrada, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Salida Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaSalidas, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Ajuste Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaAjuste, PermisosN.Secciones.Inventario)
            End If
            If Mov.Tipo = dbInventarioConceptos.Tipos.Traspaso Then
                ConPermiso = GlobalPermisos.ChecaPermiso(PermisosN.Inventario.MovimientosAltaTraspaso, PermisosN.Secciones.Inventario)
            End If
            If ConPermiso Then
                Dim S As New dbInventarioSeries(MySqlcon)
                S.QuitaSeriesAMovimiento(idMovimiento)
                Dim C As New dbMovimientos(idMovimiento, MySqlcon)
                C.QuitarBoletas(idMovimiento)
                C.Eliminar(idMovimiento, GlobalTipoCosteo, CDbl(TextBox1.Text), CosteoTiempoREal)
                PopUp("Movimiento Eliminado", 90)
                Nuevo(0)
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmInventarioSeries(IdInventario, 0, 0, CDbl(txtCantidad.Text), DateTimePicker1.Value, idMovimiento, 0)
        F.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MsgBox("¿Cancelar este movimiento?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada)
        End If
    End Sub



    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ConsultaOn Then
            LlenaCombos("tblalmacenes", cmbAlmacenOrigen, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            LlenaCombos("tblinventarioconceptos", ComboBox6, "nombre", "nombret", "idconcepto", IdsMovimientos, "idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            If cmbAlmacenOrigen.Items.Count > 0 Then
                cmbAlmacenOrigen.SelectedIndex = IdsAlmacenes.Busca(S.IdAlmacenM)
                Button4.Enabled = True
            Else
                MsgBox("No hay almacenes registrados en esta sucursal.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button4.Enabled = False
                Exit Sub
            End If
            If ComboBox6.Items.Count = 0 Then
                MsgBox("No hay conceptos de inventario registrados en esta sucursal.", MsgBoxStyle.Critical, GlobalNombreApp)
                Button4.Enabled = False
                Exit Sub
            Else
                Button4.Enabled = True
            End If
            If LlenandoDatos = False Then
                Dim C As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
                TextBox11.Text = C.Serie
                Dim V As New dbMovimientos(MySqlcon)
                TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex)).ToString
                If CInt(TextBox2.Text) < C.Folio Then TextBox2.Text = C.Folio.ToString
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Enabled Then
                TextBox3.Focus()
            Else
                If txtCosto.Enabled Then
                    txtCosto.Focus()
                Else
                    BotonAgregar()
                End If
            End If

        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress

    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(txtCantidad.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(txtCantidad.Text))
            End If
        End If
    End Sub


    ''---------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------
    '---------------------------Nueva Version -----------------------------------------------------
    '-------------------------------------------------------------------------------------
    Private Sub LlenaNodosImpresion()

        Dim V As New dbMovimientos(idMovimiento, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        V.DaTotal(idMovimiento, IDsMonedas2.Valor(ComboBox4.SelectedIndex))
        'V.DaDatosTimbrado(idMovimiento)
        ImpDoc.ImpND.Clear()

        ImpDoc.ImpND.Add(New NodoImpresionN("", "nombrefiscal", Sucursal.NombreFiscal, 0), "nombrefiscal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "concepto", V.Concepto.Nombre, 0), "concepto")

        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folio", V.Folio, 0), "folio")


        ImpDoc.ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")



        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbMovimientosDetalles(MySqlcon)
        'Dim Op As New dbOpciones(MySqlcon)
        DR = VI.ConsultaReader(idMovimiento, O._AgregaSeriesAVenta, 1)
        ImpDoc.ImpNDD.Clear()
        ImpDoc.CuantosRenglones = 0
        Dim Cont As Integer = 0
        While DR.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), "#,##0.00").PadLeft(8), 0), "cantidad" + Format(Cont, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), 0), "preciou" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), "$#,###,##0.00").PadLeft(13), 0), "importe" + Format(Cont, "000"))

            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "almacen1", DR("almacen1"), 0), "almacen1" + Format(Cont, "000"))
            If V.Concepto.Tipo = dbInventarioConceptos.Tipos.Traspaso Then
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "almacen2", DR("almacen2"), 0), "almacen2" + Format(Cont, "000"))
            Else
                ImpDoc.ImpNDD.Add(New NodoImpresionN("", "almacen2", "", 0), "almacen2" + Format(Cont, "000"))
            End If

            ImpDoc.CuantosRenglones += 1
            Cont += 1
        End While
        DR.Close()


        ImpDoc.ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
        'If V.IdVenta <> 0 Or V.idRemision <> 0 Then
        ImpDoc.ImpND.Add(New NodoImpresionN("", "folioref", V.FolioRef, 0), "folioref")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "clienteref", V.ClienteRef, 0), "clienteref")
        'Else
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "folioref", "", 0), "folioref")
        'ImpDoc.ImpND.Add(New NodoImpresionN("", "clienteref", "", 0), "clienteref")
        'End If
        ImpDoc.ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), 0), "total")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "usuario", GlobalUsuario, 0), "usuario")
        If V.Estado = Estados.Cancelada Then
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
        Else
            ImpDoc.ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
        End If
        ImpDoc.Posicion = 0
        ImpDoc.NumeroPagina = 1
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

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        If O.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Imprimir(False)
    End Sub


    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCosto.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCosto.TextChanged
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(txtCosto.Text) Then
                PrecioU = CDbl(txtCosto.Text)
                If IsNumeric(txtCantidad.Text) Then
                    TextBox6.Text = CDbl(PrecioU * CDbl(txtCantidad.Text))
                End If
            End If
        End If
    End Sub




    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 500, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
        et.Dispose()
    End Sub

    Private Sub ComboBox6_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox6.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmbAlmacenOrigen.Focus()
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
        txtCosto.Enabled = False
        lblAlmacenOrigen.Visible = False
        cmbAlmacenOrigen.Visible = False
        lblAlmacenDestino.Visible = False
        cmbAlmacenDestino.Visible = False
        btnReferencia.Visible = False
        TextBox7.Visible = False
        btnDocumento.Visible = False
        btnRecibir.Visible = False
        Label11.Visible = False
        lblUbicacionOrigen.Visible = False
        cmbUbicacionOrigen.Visible = False
        lblUbicacionDestino.Visible = False
        cmbUbicacionDestino.Visible = False
        btnEntrega.Enabled = Concep.Tipo = dbInventarioConceptos.Tipos.Entrada
        btnCartaSalida.Enabled = Concep.Tipo = dbInventarioConceptos.Tipos.Entrada
        Dim inv As New dbInventario(IdInventario, MySqlcon)
        Select Case Concep.Tipo
            Case dbInventarioConceptos.Tipos.Traspaso
                lblAlmacenOrigen.Visible = True
                cmbAlmacenOrigen.Visible = True
                lblAlmacenDestino.Visible = True
                cmbAlmacenDestino.Visible = True
                btnReferencia.Visible = True
                TextBox7.Visible = True
                btnDocumento.Visible = True
                btnRecibir.Visible = True
                lblUbicacionOrigen.Visible = inv.UsaUbicacion
                cmbUbicacionOrigen.Visible = inv.UsaUbicacion
                lblUbicacionDestino.Visible = inv.UsaUbicacion
                cmbUbicacionDestino.Visible = inv.UsaUbicacion
            Case dbInventarioConceptos.Tipos.Entrada, dbInventarioConceptos.Tipos.InventarioInicial
                txtCosto.Enabled = True
                lblAlmacenOrigen.Visible = True
                cmbAlmacenOrigen.Visible = True
                lblUbicacionOrigen.Visible = inv.UsaUbicacion
                cmbUbicacionOrigen.Visible = inv.UsaUbicacion

            Case dbInventarioConceptos.Tipos.Ajuste
                txtCosto.Enabled = True

            Case dbInventarioConceptos.Tipos.Salida
                btnReferencia.Visible = True
                TextBox7.Visible = True
                lblAlmacenOrigen.Visible = True
                cmbAlmacenOrigen.Visible = True
                btnDocumento.Visible = True
                lblUbicacionOrigen.Visible = inv.UsaUbicacion
                cmbUbicacionOrigen.Visible = inv.UsaUbicacion
        End Select
        TextBox11.Text = Concep.Serie
        Dim V As New dbMovimientos(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdsMovimientos.Valor(ComboBox6.SelectedIndex)).ToString
        If CInt(TextBox2.Text) < Concep.Folio Then TextBox2.Text = Concep.Folio.ToString
    End Sub

    Private Sub ComboBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbAlmacenOrigen.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cmbAlmacenDestino.Visible Then
                cmbAlmacenDestino.Focus()
            Else
                TextBox11.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAlmacenOrigen.SelectedIndexChanged
        If ComboBox6.Items.Count > 0 Then
            Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            If Concep.Tipo <> dbInventarioConceptos.Tipos.Traspaso Then
                cmbAlmacenDestino.SelectedIndex = IdsAlmacenes2.Busca(IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex))
            End If
        End If
        Dim dbmov As New dbInventario(MySqlcon)
        cmbUbicacionOrigen.DataSource = dbmov.Ubicaciones(IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdInventario)
        cmbUbicacionOrigen.Text = ""
    End Sub
    Private Sub ImprimirSeries()
        Dim V As New dbMovimientos(idMovimiento, MySqlcon)
        Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Rep = New repMovimientosSeries
        Rep.SetDataSource(V.ReporteVentasSeries(idMovimiento))
        'Rep.SetParameterValue("Encabezado", O._NombreEmpresa)
        Dim RV As New frmReportes(Rep, False)
        RV.Show()
        RV.Focus()
    End Sub
    Private Sub Imprimir(ByVal pDialog As Boolean)

        Try
            Dim Mov As New dbMovimientos(idMovimiento, MySqlcon)
            ImpDoc.IdSucursal = Mov.IdSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.MovimientosInventario
            ImpDoc.TipoDocumentoT = TiposDocumentos.MovimientosInventario + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.MovimientosInventario
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion()
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If o._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "MOV-" + Mov.Serie + Mov.Folio.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

        ''Dim PRV As New PrintPreviewDialog()
        ''PRV.Document = PrintDocument1
        ''PRV.Height = 800
        ''PRV.Width = 900
        ''If pDialog Then
        ''PRV.ShowDialog()
        ''Else
        ''PRV.Show()
        ''End If

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
            txtCantidad.Focus()
        End If
    End Sub

    Private Sub ComboBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbAlmacenDestino.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub
    Private Sub CrearDesdePedido()
        Dim P As New dbInventarioPedidos(IdPedido, MySqlcon)
        TextBox7.Text = P.Serie + P.Folio.ToString()
        IdVenta = 0
        IdRemision = 0
        If Estado = 0 Then
            Guardar()
            If Estado <> 0 Then
                Dim Mov As New dbMovimientos(MySqlcon)
                Mov.AgregarDetallesReferencia(idMovimiento, IdPedido, 1, IdAlmacenA, IdAlmacenB)
                ConsultaDetalles()
                btnReferencia.Enabled = False
            End If
        End If
    End Sub
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferencia.Click

        Try
            Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
            Select Case Concep.Tipo
                Case dbInventarioConceptos.Tipos.Traspaso
                    Dim PC As New frmInventarioPedidosConsulta(1, False, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                    If PC.ShowDialog = Windows.Forms.DialogResult.OK Then
                        IdAlmacenB = IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex)
                        IdAlmacenA = IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex)
                        IdPedido = PC.IdPedido
                        CrearDesdePedido()
                    End If
                    PC.Dispose()
                Case dbInventarioConceptos.Tipos.Salida
                    Dim Forma As New frmBuscaDocumentoVenta(2, False, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), 2, False, True, True, 0, True, "", True)
                    If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                        Dim V As New dbMovimientos(Forma.id(0), MySqlcon)
                        If Estado = 0 Then
                            '0 cotizacion
                            '1 pedido
                            '2 remision
                            '3 ventas
                            Select Case Forma.Tipo
                                Case 2
                                    Dim Cr As New dbVentasRemisiones(Forma.id(0), MySqlcon)
                                    'TextBox1.Text = Cr.Cliente.Clave
                                    TextBox7.Text = Cr.Serie + Cr.Folio.ToString
                                    IdRemision = Forma.id(0)
                                    idCliente = Cr.IdCliente
                                    Dim CL As New dbClientes(idCliente, MySqlcon)
                                    ConsultaOn = False
                                    txtcliente.Text = CL.Clave
                                    txtdatoscliente.Text = CL.Nombre + " | " + CL.RFC
                                    ConsultaOn = True
                                    IdVenta = 0
                                Case 3
                                    Dim op As New dbOpciones(MySqlcon)
                                    Dim Cv As New dbVentas(Forma.id(0), MySqlcon, op._Sinnegativos)
                                    'TextBox1.Text = Cv.Cliente.Clave
                                    TextBox7.Text = Cv.Serie + Cv.Folio.ToString
                                    IdVenta = Forma.id(0)
                                    idCliente = Cv.IdCliente
                                    Dim CL As New dbClientes(idCliente, MySqlcon)
                                    ConsultaOn = False
                                    txtcliente.Text = CL.Clave
                                    txtdatoscliente.Text = CL.Nombre + " | " + CL.RFC
                                    ConsultaOn = True
                                    IdRemision = 0
                            End Select

                            Guardar()
                            If Estado <> 0 Then
                                V.AgregarDetallesReferencia(idMovimiento, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdsAlmacenes2.Valor(cmbAlmacenOrigen.SelectedIndex))
                                ConsultaDetalles()
                            End If
                        Else
                            V.AgregarDetallesReferencia(idMovimiento, Forma.id(0), Forma.Tipo, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), IdsAlmacenes2.Valor(cmbAlmacenOrigen.SelectedIndex))
                            ConsultaDetalles()
                        End If
                        NuevoConcepto()
                        btnReferencia.Enabled = False
                    End If
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumento.Click
        If IdVenta <> 0 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasVer, PermisosN.Secciones.Ventas) = True Then
                Dim Fac As New frmVentasN(IdVenta, 0, 0, 0)
                Fac.ShowDialog()
                Fac.Dispose()
            End If
        End If
        If IdRemision <> 0 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.ReportesVer, PermisosN.Secciones.Ventas) = True Then
                Dim Fac As New frmVentasRemisiones(IdRemision)
                Fac.ShowDialog()
                Fac.Dispose()
            End If
        End If
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBox7.Text = ""
        IdVenta = 0
        IdRemision = 0
    End Sub

    Private Sub CheckScroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckScroll.CheckedChanged
        My.Settings.movimientosscroll = CheckScroll.Checked
    End Sub

    Private Sub Button16_Click_1(sender As Object, e As EventArgs) Handles Button16.Click
        Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
        If EsSemillas And (Concep.Tipo = dbInventarioConceptos.Tipos.Entrada Or Concep.Tipo = dbInventarioConceptos.Tipos.Salida Or Concep.Tipo = dbInventarioConceptos.Tipos.InventarioInicial) Then
            Dim Tipoboleta As String = ""
            If Concep.Tipo = dbInventarioConceptos.Tipos.Entrada Or Concep.Tipo = dbInventarioConceptos.Tipos.InventarioInicial Then
                Tipoboleta = "E"
            End If
            If Concep.Tipo = dbInventarioConceptos.Tipos.Salida Then
                Tipoboleta = "S"
            End If
            Dim Boleta As New frmSemillasBoleta(Tipoboleta, TextBox3.Text, CDbl(txtCantidad.Text), IdDetalle, GlobalSemillasResumida, cmbAlmacenOrigen.Text, GlobalPermisos.ChecaPermiso(PermisosN.Semillas.PrecioVerBoleta, PermisosN.Secciones.Semillas))
            Boleta.ShowDialog()
            Boleta.Dispose()
        End If
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
        Dim F As New frmInventarioAduana(0, 0, 0, 0, CDbl(txtCantidad.Text), IdInventario, 1, 0, 0, IdDetalle, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), cmbAlmacenOrigen.Text, Concep.Tipo, 0, 0)
        F.ShowDialog()
        F.Dispose()
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Dim Concep As New dbInventarioConceptos(IdsMovimientos.Valor(ComboBox6.SelectedIndex), MySqlcon)
        Dim F As New frmInventarioLotes(0, 0, 0, 0, CDbl(txtCantidad.Text), IdInventario, 1, 0, 0, IdDetalle, IdsAlmacenes.Valor(cmbAlmacenOrigen.SelectedIndex), cmbAlmacenOrigen.Text, Concep.Tipo, 0, 0)
        F.ShowDialog()
        F.Dispose()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles btnRecibir.Click
        If MsgBox("¿Recibir la mercancia?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim mov As New dbMovimientos(MySqlcon)
            mov.Recibir(idMovimiento)
            btnRecibir.Enabled = False
            Label11.Text = "Recibido"
        End If
    End Sub
    Private Sub GeneraPoliza(pIdMovimiento As Integer)
        Try
            'Dim Op As New dbOpciones(MySqlcon)
            If O.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbMovimientos(pIdMovimiento, MySqlcon)
                V.ID = pIdMovimiento
                'V.Buscar(pIdMovimiento)
                Dim Canceladas As Byte = 0
                Dim credito As Byte = 2
                Dim cuantas As Integer
                If V.Estado = Estados.Cancelada Then Canceladas = 1
                cuantas = M.CuantasHay(11, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(11, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 11)
                        f.ShowDialog()
                        If f.DialogResult = Windows.Forms.DialogResult.OK Then
                            M.ID = f.IdMascara
                        Else
                            Exit Sub
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    'If Canceladas = 0 Then
                    GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    'Else
                    '   GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    'End If
                    GP.GeneraPolizaGeneral(V.ID, V.idalmacenO, 5, V.idalmacenD, 6, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    End If
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = False Then PopUp("Póliza Generada", 90)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub DGDetalles_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.ColumnIndex = 4 Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            txtdatoscliente.Text = B.Cliente.Nombre + " | " + B.Cliente.RFC
            idCliente = B.Cliente.ID
            ConsultaOn = False
            txtcliente.Text = B.Cliente.Clave
            ConsultaOn = True
        End If
    End Sub

    Private Sub txtcliente_TextChanged(sender As Object, e As EventArgs) Handles txtcliente.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbClientes(MySqlcon)
                If c.BuscaCliente(txtcliente.Text) Then
                    txtdatoscliente.Text = c.Nombre + " | " + c.RFC
                    IdCliente = c.ID
                Else
                    txtdatoscliente.Text = ""
                    IdCliente = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub cmbAlmacenDestino_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAlmacenDestino.SelectedIndexChanged
        Dim dbmov As New dbInventario(MySqlcon)
        cmbUbicacionDestino.DataSource = dbmov.Ubicaciones(IdsAlmacenes2.Valor(cmbAlmacenDestino.SelectedIndex), IdInventario)
        cmbUbicacionDestino.Text = ""
    End Sub

    Private Sub DGDetalles_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub btnEntrega_Click(sender As Object, e As EventArgs) Handles btnEntrega.Click
        If idMovimiento <> 0 Then
            Dim f As New frmMovimientosEntrega(idMovimiento)
            f.ShowDialog()
        End If
    End Sub

    Private Sub btnCartaSalida_Click(sender As Object, e As EventArgs) Handles btnCartaSalida.Click
        If idMovimiento <> 0 Then
            Dim f As New frmCartaSalida(idMovimiento)
            f.ShowDialog()
        End If
    End Sub
End Class