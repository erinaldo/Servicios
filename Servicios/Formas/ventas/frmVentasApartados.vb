Public Class frmVentasApartados
    'Dim IdsVariantes As New elemento
    Dim idApartado As Integer
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
    'Dim IdVariante As Integer
    'Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim Tabla As New DataTable
    Dim PrecioNeto As Byte
    Dim IdsSucursales As New elemento
    'Dim idsFormasDePago As New elemento
    Dim SerieAnt As String
    'Dim Cadena As String
    'Dim Sello As String
    'Dim Isr As Double
    'Dim IvaRetenido As Double
    Dim PrecioBase As Double
    'Dim iTipoFacturacion As Byte
    Dim CantidadMostrar As Double
    Dim TipoCantidadMostrar As Integer
    Dim TipoCantidad As Integer
    Dim LlenandoDatos As Boolean = False
    Dim ImpND As New Collection
    Dim ImpNDD As New Collection
    Dim ImpNDDi As New Collection
    Dim ImpNDi As New Collection
    Dim ImpNDi2 As New Collection
    Dim Sobre As Byte
    Dim SIVA As Double
    'Dim ImpNDS As New Collection
    'Dim ImpNDSD As New Collection
    'Dim ImpNDDiS As New Collection
    'Dim ImpNDiS As New Collection
    Dim Posicion As Integer
    Dim CuantosRenglones As Integer
    Dim CadenaCFDI As String
    Dim CodigoBidimensional As Bitmap
    Dim MasPaginas As Boolean
    Dim NumeroPagina As Integer
    Dim CuantaY As Integer
    Dim LimitedeFolios As Boolean = False
    Dim idRemisiones() As Integer
    Dim idRemisionesCero() As Integer = {0}
    Dim SinConcersion As Boolean
    Dim IdLista As Integer
    Dim IdsCuentas As New elemento
    Dim Op As dbOpciones
    Dim SinCredito As Boolean
    Dim Saldo As Double
    Dim CreditoCliente As Double
    Dim Eselectronica As Byte
    Dim CertificadoCaduco As Boolean = False
    Dim SinTimbres As Boolean = False
    Dim DocAImprimir As Byte = 0
    Dim TipoImpresora As Byte = 0
    'Dim UsaAdenda As Integer = 0
    'Dim IdVentaOrigen As Integer
    'Dim Parcialidad As Integer = 1
    'Dim Parcialidades As Integer = 1
    Dim Funcion As Byte
    'Dim ParcialidadImporte As Double
    'Dim IdClienteOrigen As Integer
    Dim UsaFormula As Byte
    Dim Articulos As New Collection
    Dim CostoArticulo As Double
    Dim ArtNombre As String
    Dim IdVendedorU As Integer
    'Dim SaldoaFavor As Double
    'Dim Esamortizacion As Byte
    Public Sub New(ByVal pidVenta As Integer, ByVal pFuncion As Byte, ByVal pImporte As Double, ByVal pidCliente As Integer)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idApartado = pidVenta
        'If pFuncion = 1 Then
        'IdVentaOrigen = pidVenta
        'idVenta = 0
        'End If
        'Funcion = pFuncion
        'ParcialidadImporte = pImporte
        'IdClienteOrigen = pidCliente
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este apartado no ha sido guardado. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idVenta)
                Dim C As New dbVentasApartados(idApartado, MySqlcon)
                'C.RegresaInventario(idVenta)
                C.Eliminar(idApartado)
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub frmVentasN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F10 Then
            If Button14.Enabled = True Then
                Modificar(Estados.Guardada)
            End If
        End If
        If e.KeyCode = Keys.F12 Then
            If idCliente <> 0 Then
                Dim Cl As New frmClientesConsultaArticulos(idCliente, IdInventario)
                Cl.ShowDialog()
                Cl.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F9 Then
            If Button1.Enabled = True Then
                'If idRemisiones.Length = 1 And idRemisiones(0) = 0 Then
                Modificar(Estados.Pendiente)
                'Else
                '   MsgBox("No se puede dejar pendiente esta factura.", MsgBoxStyle.Information, GlobalNombreApp)
                'End If
            End If
        End If
        If e.KeyCode = Keys.F5 Then
            BotonNuevo()
        End If
        If e.KeyCode = Keys.F4 Then
            BotonBuscar()
        End If
        'If e.KeyCode = Keys.F3 Then
        'ImprimirRetenciones()
        'End If
        If e.KeyCode = Keys.F6 And IdInventario <> 0 Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.Consultas, PermisosN.Secciones.Ventas) = True Then
                Dim f As New frmInventarioConsulta(IdInventario, 1, IdsSucursales.Valor(ComboBox3.SelectedIndex))
                If f.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    ComboBox8.SelectedIndex = IdsAlmacenes.Busca(f.IdAlmacen)
                End If
                f.Dispose()
            End If
        End If
        If e.KeyCode = Keys.F7 Then
            Dim f As New frmInventario
            f.ShowDialog()
            f.Dispose()
        End If
        'If e.KeyCode = Keys.F8 Then
        '    If GlobalTipoVersion = 0 Then
        '        Dim f As New frmCompras
        '        f.ShowDialog()
        '    End If
        'End If
    End Sub

    Private Sub frmVentasN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If GlobalChecarConexion() = False Then
            MsgBox("No se pudo establecer una conexion al servidor. Reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
            For Each c As Control In Me.Controls
                c.Enabled = False
            Next
            Button3.Enabled = True
        Else
            Try
                Me.Icon = GlobalIcono
            Catch ex As Exception
            End Try
            Op = New dbOpciones(MySqlcon)
            Dim U As New dbUsuarios(GlobalIdUsuario, MySqlcon)
            IdVendedorU = U.IdVendedor
            CheckScroll.Checked = My.Settings.ventasscroll
            'If GlobalTipoFacturacion > 1 Then
            'SinTimbres = ChecaTimbres()
            'End If
            ConsultaOn = False
            Dim I As Integer = 0
            Dim S As String = ""
            Dim D As Double = 0
            Tabla.Columns.Add("Id", I.GetType)
            Tabla.Columns.Add("TipoR", S.GetType)
            Tabla.Columns.Add("Extra", S.GetType)
            Tabla.Columns.Add("Cantidad", D.GetType)
            Tabla.Columns.Add("Uni.", S.GetType)
            Tabla.Columns.Add("Código", S.GetType)
            Tabla.Columns.Add("Descripción", S.GetType)
            Tabla.Columns.Add("Precio U.", S.GetType)
            Tabla.Columns.Add("Importe", S.GetType)
            Tabla.Columns.Add("Moneda", S.GetType)

            LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
            ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
            ComboBox4.Items.Add("Alta")
            ComboBox4.Items.Add("Media")
            ComboBox4.Items.Add("Baja")
            'LlenaCombos("tblformasdepago", ComboBox4, "concat(convert(if(tipo=0,'Crédito',if(tipo=1,'Contado','Parcialidad')) using utf8),'-',nombre)", "nombret", "idforma", idsFormasDePago, , , "idforma")
            LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
            LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
            LlenaCombos("tblvendedores", ComboBox5, "nombre", "nombret", "idvendedor", IdsVendedores)
            ConsultaOn = True
            If idApartado = 0 And Funcion = 0 Then
                Nuevo()
            Else
                'If Funcion = 0 Then
                LlenaDatosVenta()
                NuevoConcepto()
                'End If
            End If
            

        End If

    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim V As New dbVentasApartados(MySqlcon)
                V.DaTotal(idApartado, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                Label12.Text = Format(V.Subtotal, "#,##0.00")
                Label13.Text = Format(V.TotalIva, "#,##0.00")
                Label14.Text = Format(V.TotalVenta, "#,##0.00")
                Label32.Text = Format(V.TotalPeso, "#,##0.00") + "Kg."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        idRemisiones = idremisionescero
        Button11.Enabled = True
        DateTimePicker1.Value = Date.Now
        DateTimePicker2.Value = Date.Now
        DateTimePicker3.Value = Date.Now
        TextBox1.Text = ""
        FolioAnt = 0
        Button18.Enabled = False
        idApartado = 0
        'SaldoaFavor = 0
        Label29.Visible = False
        TextBox13.Text = ""
        Label30.Text = "Abonado: $0.00"
        Button12.Enabled = False
        Label32.Text = "0.00Kg."
        Panel1.Enabled = True
        Panel2.Enabled = True
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button21.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        ComboBox3.Enabled = True
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        'iTipoFacturacion = GlobalTipoFacturacion
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        If Op._TipoSelAlmacen = "0" Then
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        Else
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. almacen")
            ComboBox8.SelectedIndex = 0
        End If
        'TextBox11.Text = S.Serie

        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Apartados, 0)

        TextBox11.Text = Sf.Serie
        Eselectronica = Sf.EsElectronica
        Dim V As New dbVentasApartados(MySqlcon)
        TextBox2.Text = V.DaNuevoFolio(TextBox11.Text).ToString
        If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
        If GlobaltpBanxico <> "Error" Then
            TextBox10.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox10.Text = CM.Cantidad.ToString
        End If
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        SerieAnt = ""
        Label24.Visible = False
        TextBox14.Text = ""
        ComboBox4.SelectedIndex = 0
        NuevoConcepto()
        If CInt(TextBox2.Text) > Sf.FolioFinal Then
            LimitedeFolios = True
            MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
        Else
            LimitedeFolios = False
        End If
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.CambioSucursal, PermisosN.Secciones.Ventas) = False Then
            ComboBox3.Enabled = False
        End If
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox5.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            BuscaClienteBoton()
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
                        Saldo = c.DaSaldoAFecha(c.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
                    Else
                        Saldo = 0
                    End If
                    CreditoCliente = c.Credito
                    'TextBox13.Text = "Días/Lím: " + c.CreditoDias.ToString + "/" + Format(c.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(Saldo, "#,##0.00") + vbCrLf + "A Favor: " + Format(c.DaSaldoAFavor(c.ID), "#,##0.00")
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                        'If c.Credito > 0 Or c.CreditoDias > 0 Then
                        '    ComboBox4.SelectedIndex = 1
                        'Else
                        '    ComboBox4.SelectedIndex = 0
                        'End If
                        If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                        Else
                            ComboBox5.SelectedIndex = IdsVendedores.Busca(c.IdVendedor)
                        End If

                    End If
                    If Saldo >= c.Credito Then
                        SinCredito = True
                    End If
                    'UsaAdenda = c.UsaAdenda

                    idCliente = c.ID
                    IdLista = c.IdLista
                    'Isr = c.ISR
                    SIVA = c.IVA
                    Sobre = c.SobreescribeIVA
                    'IvaRetenido = c.IvaRetenido
                    'LlenaCombos("tblclientescuentas", ComboBox6, "cuenta", "nombret", "idcuenta", IdsCuentas, "idcliente=" + idCliente.ToString, "NO APLICA")
                    'ComboBox6.SelectedIndex = 0
                Else
                    TextBox7.Text = ""
                    TextBox13.Text = ""
                    idCliente = 0
                    'Isr = 0
                    'IvaRetenido = 0
                    SIVA = 0
                    Sobre = 0
                    'UsaAdenda = 0
                    SinCredito = False
                    Saldo = 0
                    CreditoCliente = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'If idRemisiones.Length = 1 And idRemisiones(0) = 0 Then
        Modificar(Estados.Pendiente)
        'Else
        'MsgBox("No se puede dejar pendiente esta factura.", MsgBoxStyle.Information, GlobalNombreApp)
        'End If
    End Sub
    Private Sub Modificar(ByVal pEstado As Byte)
        Try
            Dim XMLAdenda As String = ""
            Dim MensajeError As String = ""
            Dim C As New dbVentasApartados(MySqlcon)
            If GlobalChecarConexion() = False Then
                MsgBox("No se pudo establecer una conexion al servidor. reinicie el sistema y si el problema persiste verifique su conexión a la red.", MsgBoxStyle.Critical, GlobalNombreApp)
                Exit Sub
            End If
            If C.RevisaConceptos(idApartado) = False Then
                'If MsgBox("Hay conceptos en pesos y en dolares si es correcto seleccione Aceptar si no de click en Cancelar.", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                If MsgBox("Hay conceptos en pesos y en dolares solo se pueden hacer apartados con conceptos en un tipo de moneda.", MsgBoxStyle.Information) = MsgBoxResult.Cancel Then
                    Exit Sub
                End If
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosAlta, PermisosN.Secciones.Ventas) = False And pEstado <> Estados.Cancelada Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosCancelar, PermisosN.Secciones.Ventas) = False And pEstado = Estados.Cancelada Then
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            Button1.Enabled = False
            Button14.Enabled = False
            C.DaTotal(idApartado, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
            If IsNumeric(TextBox2.Text) = False Then MensajeError = "El folio debe ser un valor numérico."
            'If ComboBox6.Text <> "" Then
            '    If ComboBox6.Text.Length < 4 Then MensajeError = "El número de cuenta debe ser por lo menos de 4 dígitos."
            'End If
            'If IsNumeric(TextBox15.Text) Then
            '    If idsFormasDePago.Valor(ComboBox4.SelectedIndex) = 98 And CInt(TextBox15.Text) <= 1 And Op.NParcialidades = 0 And pEstado <> Estados.Cancelada Then
            '        MensajeError += "La cantidad de parcialidades debe ser mayor a uno."
            '    End If
            'Else

            'MensajeError += " Las parcialidades deben llevar un valor numérico."
            'End If
            'Dim FP As New dbFormasdePago(idsFormasDePago.Valor(ComboBox4.SelectedIndex), MySqlcon)
            'If IdVentaOrigen = 0 And FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
            '    MensajeError += " No se puede crear una parcialidad sin indicar su factura origen."
            'End If
            'If FolioAnt <> TextBox2.Text Then
            If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
                If pEstado = Estados.Guardada Then
                    MensajeError = "Folio repetido."
                End If
                'TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex), iTipoFacturacion).ToString
            End If
            'End If

            'If (Saldo + C.TotalVenta > CreditoCliente Or SinCredito) And FP.Tipo = dbFormasdePago.Tipos.Credito And Op._LimitarCredito = 1 Then
            '    If pEstado = Estados.Guardada Then
            '        MensajeError += " El cliente exede de su límite de crédito."
            '    End If
            'End If
            'If SaldoaFavor - (-1 * C.TotalAmortizacion(idVenta)) < 0 And pEstado = Estados.Guardada Then
            '    MensajeError += " Saldo a favor insuficiente, debe indicar una cantidad menor."
            'End If
            'Checa existecia pendiente
            'If pEstado = Estados.Guardada And GlobalSoloExistencia = True Then MensajeError += C.VerificaExistencias(idApartado)
            If MensajeError = "" Then
                
                'Dim O As New dbOpciones(MySqlcon)

                Dim CM As New dbMonedasConversiones(MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                'Dim Credito As Byte
                'Credito = FP.Tipo
                C.DaTotal(idApartado, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim Sf As New dbSucursalesFolios(MySqlcon)
                'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Apartados, 0)
                'Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                'Dim iIdFormaPago As Integer = idsFormasDePago.Valor(ComboBox4.SelectedIndex)

                C.Modificar(idApartado, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), TextBox11.Text, ComboBox4.SelectedIndex, idCliente, TextBox14.Text, TextBox13.Text, Format(DateTimePicker2.Value, "yyyy/MM/dd"), Format(DateTimePicker2.Value, "HH:mm"), IDsMonedas2.Valor(ComboBox2.SelectedIndex), CDbl(TextBox10.Text), IdsVendedores.Valor(ComboBox5.SelectedIndex), pEstado, C.TotalVenta, 0)
                Estado = pEstado
                If pEstado = Estados.Cancelada Then
                    If MsgBox("¿Imprimir Apartado Cancelado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Imprimir()
                    End If
                End If
                If pEstado = Estados.Guardada Then
                    If Op._ApartadosInventariable = "1" Then
                        C.ModificaInventario(idApartado)
                    End If
                    If MsgBox("¿Agregar un anticipo a al apartado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim AP As New frmVentasPagosRemisiones(TextBox2.Text, 1, idCliente, "")
                        AP.ShowDialog()
                        AP.Dispose()
                    End If
                    Imprimir()
                End If
                If pEstado <> Estados.SinGuardar Then
                    Nuevo()
                Else
                    Button1.Enabled = True
                    Button14.Enabled = True
                End If
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
            If LimitedeFolios = True Then
                MsgBox("Se ha alcanzado el limite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
                Exit Sub
            End If
            
            'If Button1.Text = "Guardar" Then
            'If (PermisosVentas And CULng((Math.Pow(2, perVentas.Ventas)))) <> 0 Then
            If idCliente <> 0 Then
                Dim C As New dbVentasApartados(MySqlcon)
                'Dim Desglozar As Byte
                
                If IsNumeric(TextBox2.Text) = False Then
                    MsgBox("El folio debe ser un valor numérico", MsgBoxStyle.Critical, GlobalNombreApp)
                    Exit Sub
                End If
                If C.ChecaFolioRepetido(CInt(TextBox2.Text), TextBox11.Text) Then
                    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                    Label17.Visible = True
                    FolioAnt = 0
                Else
                    FolioAnt = TextBox2.Text
                End If
                'Dim O As New dbOpciones(MySqlcon)
                Dim CM As New dbMonedasConversiones(MySqlcon)
                'Dim Sf As New dbSucursalesFolios(MySqlcon)
                'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Factura, iTipoFacturacion)

                'Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(ComboBox1.SelectedIndex))
                C.DaTotal(idApartado, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Guardar(Format(DateTimePicker1.Value, "yyyy/MM/dd"), IdsSucursales.Valor(ComboBox3.SelectedIndex), CInt(TextBox2.Text), TextBox11.Text, ComboBox4.SelectedIndex, idCliente, TextBox14.Text, TextBox13.Text, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Format(Date.Now, "HH:mm"), Format(Date.Now, "HH:mm"), IDsMonedas2.Valor(ComboBox2.SelectedIndex), CDbl(TextBox10.Text), IdsVendedores.Valor(ComboBox5.SelectedIndex), Op._ApartadosInventariable)
                idApartado = C.ID
                Estado = 1
                'Button1.Text = "Modificar"
                Button2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button15.Enabled = True
                Button21.Enabled = True
                ComboBox3.Enabled = False
                'LlenaDatosDetalles()
            Else
                MsgBox("Debe indicar un cliente", MsgBoxStyle.Critical, GlobalNombreApp)
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
            If UsaFormula = 1 And IdInventario <> 0 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.000")
                    TextBox4.Text = Fo.FormulaString
                End If
                Fo.Dispose()
            End If
            TextBox12.Focus()
        End If
        If e.KeyCode = Keys.F1 Then
            buscaArticuloBoton()
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.000")
                    TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
                End If
                TextBox12.Focus()
                Fo.Dispose()
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
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

    End Sub
    Private Sub LlenaDatosVenta()
        LlenandoDatos = True
        Button11.Enabled = False
        idRemisiones = idRemisionesCero
        Dim C As New dbVentasApartados(idApartado, MySqlcon)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio
        TextBox1.Text = C.Cliente.Clave
        Estado = C.Estado
        Label30.Text = "Abonado: $" + Format(C.Credito, "#,##0.00")
        Button18.Enabled = True
        Button21.Enabled = True
        'TextBox8.Text = C.Iva.ToString
        TextBox11.Text = C.Serie
        TextBox10.Text = C.TipodeCambio.ToString
        TextBox14.Text = C.Comentario
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        DateTimePicker2.Value = C.FechaSalida
        DateTimePicker3.Value = "2016/01/01 " + C.HoraSalida
        ComboBox4.SelectedIndex = C.Prioridad
        TextBox13.Text = C.PrioridadSTR
        'ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdMoneda)
        ComboBox5.SelectedIndex = IdsVendedores.Busca(C.IdVendedor)

        ComboBox3.Enabled = False
        If C.Surtido = 1 Then
            Label29.Visible = True
            Button12.Enabled = False
        Else
            Label29.Visible = False
            Button12.Enabled = True
        End If
        'ConsultaOn = True
        LlenaDatosDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                Button15.Enabled = True
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Button2.Enabled = False
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
                Button15.Enabled = True
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                Button15.Enabled = True
        End Select
        LlenandoDatos = False
    End Sub
    Private Sub LlenaDatosDetalles()
        Panel1.Visible = True
        ConsultaDetalles()
    End Sub
    Private Sub ConsultaDetalles()
        Try
            'If UsaAdenda > 0 Then Articulos.Clear()
            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbVentasApartadosDetalles(MySqlcon)
            T = CD.ConsultaReader(idApartado, False)
            While T.Read
                If T("cantidad") <> 0 Then
                    'If T("idinventario") > 1 Then
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("tipocantidad"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    'Else
                    '   Tabla.Rows.Add(T("idventasinventario"), "P", "", T("cantidadm"), T("tipom"), T("pclave"), T("descripcion"), Format(T("precio") / T("cantidadm"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    'End If
                Else
                    'If T("idinventario") > 1 Then
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("tipocantidad"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    'Else
                    '   Tabla.Rows.Add(T("idventasinventario"), "P", "", T("cantidadm"), T("tipom"), T("pclave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                    'End If
                End If
                'If UsaAdenda > 0 Then
                '    Dim Ar As New Articulo()
                '    Ar.cantidad = T("cantidadm")
                '    Ar.porcIva = T("iva")
                '    Ar.ImporteNeto = T("precio") * (1 + T("iva") / 100)
                '    Ar.montoIva = T("precio") * (T("iva") / 100)
                '    Ar.descripcion = T("descripcion")
                '    Ar.unidadMedida = T("tipom")
                '    Articulos.Add(Ar)
                'End If
            End While
            T.Close()

            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).Width = 80
            DGDetalles.Columns(9).Width = 80
            DGDetalles.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(4).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(5).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(6).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(7).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(8).SortMode = DataGridViewColumnSortMode.NotSortable
            DGDetalles.Columns(9).SortMode = DataGridViewColumnSortMode.NotSortable
            If DGDetalles.RowCount > DGDetalles.DisplayedRowCount(False) Then DGDetalles.FirstDisplayedScrollingRowIndex = DGDetalles.RowCount - DGDetalles.DisplayedRowCount(False)
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        'IdVariante = 0
        'IdServicio = 0
        PrecioNeto = 0
        CostoArticulo = 0
        CantidadMostrar = 0
        UsaFormula = 0
        TipoCantidadMostrar = 0
        TipoCantidad = 0
        ArtNombre = ""
        'Esamortizacion = 0
        'cmbVariante.Visible = False
        TextBox12.Text = "0"
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox9.Text = "0"
        TextBox6.Text = "0"
        Label20.Text = "0"
        PrecioBase = 0
        'cmbVariante.Visible = False
        Button9.Enabled = False
        SinConcersion = False
        TextBox3.Enabled = True
        Button6.Enabled = True
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        TextBox5.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbVentasApartadosDetalles(MySqlcon)
            Dim HayError As Boolean = False
            Dim MsgError As String = ""

            If IdInventario = 0 Then MsgError += "Debe indicar un artículo."

            If IsNumeric(TextBox5.Text) Then
                If CDbl(TextBox5.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += "La cantidad debe ser un valor mayor a 0."
                    HayError = True
                End If
            Else
                MsgError += "La cantidad debe ser un valor numérico."
                HayError = True
            End If
            If IsNumeric(TextBox12.Text) = False Then
                MsgError += vbCrLf + "El precio debe ser un valor numérico."
                HayError = True
            Else
                'If Op._AvisoCosto = "1" Then
                '    If CDbl(TextBox12.Text) < CostoArticulo Then
                '        If MsgBox("El precio del artículo está debajo del costo. ¿Agregar el concepto de todas maneras?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.No Then
                '            MsgError += "Precio debajo del costo."
                '            HayError = True
                '        End If
                '    End If
                'End If
                'If Esamortizacion = 1 Then
                '    If SaldoaFavor - (-1 * CDbl(TextBox6.Text)) < 0 Then
                '        MsgError += " Saldo a favor insuficiente, debe indicar una cantidad menor."
                '        HayError = True
                '    End If
                'End If
                If CDbl(TextBox6.Text) <= 0 And My.Settings.conceptocero = False Then
                    MsgError += " El precio debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If

            Dim I As New dbInventario(IdInventario, MySqlcon)
            If IdInventario <> 0 And GlobalSoloExistencia = True And I.Inventariable = 1 Then
                Dim Cant As Double
                Cant = I.DaInventario(IdsAlmacenes.Valor(ComboBox8.SelectedIndex), IdInventario)
                If I.Contenido <> 1 Then
                    Cant = Cant * I.Contenido
                End If
                'If Cant < CDbl(TextBox5.Text) And I.Inventariable = 1 Then
                '    MsgError += " Artículo sin existencia suficiente." + vbCrLf + "Cantidad disponible: " + Cant.ToString + vbCrLf + "Cantidad solicitada:" + TextBox5.Text + vbCrLf + "Diferencia:" + CStr(Cant - CDbl(TextBox5.Text))
                '    HayError = True
                'End If
            End If
            If IsNumeric(TextBox9.Text) = False Then
                MsgError += vbCrLf + "El descuento debe ser un valor numérico."
                HayError = True
            Else
                If CDbl(TextBox9.Text) <> 0 Then
                    TextBox12.Text = CStr(CDbl(TextBox12.Text) - (CDbl(TextBox12.Text) * CDbl(TextBox9.Text) / 100))
                End If
            End If


            If HayError = False Then

                If PrecioNeto = 1 Then
                    Dim Temp As Double
                    Temp = CStr(CDbl(TextBox6.Text) / (1 + (CDbl(TextBox8.Text)) / 100) / CDbl(TextBox5.Text))
                    TextBox12.Text = Temp.ToString
                End If
                'If CantidadMostrar = 0 Then TipoCantidadMostrar = TipoCantidad
                'If CantidadMostrar = 0 Then CantidadMostrar = CDbl(TextBox5.Text)
                'If SinConcersion Then CantidadMostrar = CDbl(TextBox5.Text)
                If Button4.Text = "Agregar Concepto" Then

                    CD.Guardar(idApartado, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), Trim(TextBox4.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text))
                    'If IdInventario <> 0 Then
                    '    If ManejaSeries <> 0 Then
                    '        If CD.NuevoConcepto Then
                    '            Dim F As New frmVentasAsignaSeries(IdInventario, idApartado, 0, CInt(TextBox5.Text))
                    '            F.ShowDialog()
                    '        Else
                    '            Dim F As New frmVentasAsignaSeries(IdInventario, idApartado, 0, CD.Cantidad)
                    '            F.ShowDialog()
                    '        End If
                    '    End If
                    '    'Dim I As New dbInventario(MySqlcon)
                    '    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Baja, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If
                    'If IdVariante <> 0 Then
                    'Dim PV As New dbProductosVariantes(MySqlcon)
                    'PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If

                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text))
                    'If IdInventario <> 0 Then
                    '    If ManejaSeries <> 0 Then
                    '        Dim F As New frmVentasAsignaSeries(IdInventario, idApartado, 0, CDbl(TextBox5.Text))
                    '        F.ShowDialog()
                    '    End If
                    '    'Dim I As New dbInventario(MySqlcon)
                    '    'I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    'End If
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
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosAlta, PermisosN.Secciones.Ventas) = True Then
            If Op._TipoSelAlmacen = "1" Then
                If ComboBox8.SelectedIndex <= 0 Then
                    MsgBox("Debe seleccionar un almacen.", MsgBoxStyle.Information, GlobalNombreApp)
                    Exit Sub
                End If
            End If
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                If IdInventario <> 0 Then AgregaArticulo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox3.Focus()
        End If
    End Sub
    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try
            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbVentasApartadosDetalles(IdDetalle, MySqlcon)
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            IdInventario = CD.Idinventario
            'IdVariante = CD.idVariante
            TextBox3.Enabled = False
            Button6.Enabled = False
            If IdInventario > 1 Then
                ConsultaOn = False
                TextBox3.Text = CD.Inventario.Clave
                PrecioNeto = CD.Inventario.PrecioNeto
                UsaFormula = CD.Inventario.UsaFormula
                CostoArticulo = CD.Inventario.CostoBase
                'Esamortizacion = CD.Inventario.EsAmortizacion
                ConsultaOn = True
                'IdVariante = 0
                'cmbVariante.Visible = False
                'If CD.Inventario.ManejaSeries = 1 Then
                '    Button12.Visible = True
                'Else
                '    Button12.Visible = True
                'End If
                ArtNombre = CD.Inventario.Nombre
            Else
                PrecioNeto = 0
            End If
            
            'CantidadMostrar = CD.CantidadM
            'TipoCantidadMostrar = CD.TipoCantidadM
            TipoCantidad = TipoCantidadMostrar
            Label20.Text = CantidadMostrar.ToString
            TextBox5.Text = CD.Cantidad.ToString
            TextBox8.Text = CD.Iva.ToString
            TextBox9.Text = CD.Descuento.ToString
            'If CD.Cantidad = CD.CantidadM Then
            '    SinConcersion = True
            'Else
            '    SinConcersion = False
            'End If
            If CD.Cantidad = 0 Then
                PrecioU = 0
            Else
                If CD.Descuento = 0 Then
                    If PrecioNeto = 0 Then
                        PrecioU = CD.Precio / CD.Cantidad
                    Else
                        PrecioU = Math.Round(CD.Precio / CD.Cantidad * (1 + (CD.Iva) / 100), 2)
                    End If
                Else
                    Dim Val As Double = (CD.Precio / (1 - CD.Descuento / 100))
                    If PrecioNeto = 0 Then
                        PrecioU = Val / CD.Cantidad
                    Else
                        PrecioU = Math.Round(Val / CD.Cantidad * (1 + (CD.Iva) / 100), 2)
                    End If
                End If
            End If

            PrecioBase = PrecioU
            TextBox12.Text = PrecioU.ToString
            'cmbVariante.Visible = False
            CantAnt = CD.Cantidad
            IdAlmacen = CD.IdAlmacen
            If PrecioNeto = 0 Then
                If CD.Descuento = 0 Then
                    TextBox6.Text = CD.Precio.ToString
                Else
                    TextBox6.Text = Format(PrecioU * CD.Cantidad, "0.00")
                End If
            End If


            TextBox4.Text = CD.Descripcion
            Button4.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            'cmbtipoarticulo.Text = "A"

            If CheckScroll.Checked Then TextBox5.Focus()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub
    Private Sub BotonNuevo()
        If Estado = Estados.SinGuardar Then
            If MsgBox("Este apartado no ha sido guardado. ¿Desea iniciar una nueva factura? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idApartado)
                Dim C As New dbVentasApartados(idApartado, MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idVenta)
                C.Eliminar(idApartado)
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosAlta, PermisosN.Secciones.Ventas) = True Then
                If MsgBox("¿Desea eliminar este concepto del apartado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbVentasApartadosDetalles(MySqlcon)
                    CD.Eliminar(IdDetalle)
                    'Dim S As New dbInventarioSeries(MySqlcon)
                    'S.QuitarSeriesAventaxArticulo(IdInventario, idApartado)
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

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        BuscaClienteBoton()
    End Sub
    Private Sub BuscaClienteBoton()
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Cliente, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            If B.Cliente.NoChecarCr = 0 Then
                Saldo = B.Cliente.DaSaldoAFecha(B.Cliente.ID, Format(DateAdd(DateInterval.Day, 1, Date.Now), "yyyy/MM/dd"))
            Else
                Saldo = 0
            End If
            CreditoCliente = B.Cliente.Credito
            'SaldoaFavor = CDbl(Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "0.00"))
            If B.Cliente.DireccionFiscal = 0 Then
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            Else
                TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            End If
            'UsaAdenda = B.Cliente.UsaAdenda
            'If B.Cliente.UsaAdenda <> 0 Then
            '    Button23.Visible = True
            'Else
            '    Button23.Visible = False
            'End If

            'TextBox13.Text = "Días/Lím: " + B.Cliente.CreditoDias.ToString + "/" + Format(B.Cliente.Credito, "#,##0.00") + vbCrLf + "Saldo: " + Format(Saldo, "#,##0.00") + vbCrLf + "A Favor: " + Format(B.Cliente.DaSaldoAFavor(B.Cliente.ID), "#,##0.00")
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Or Estado <> Estados.Pendiente Then
                'If B.Cliente.Credito > 0 Or B.Cliente.CreditoDias > 0 Then
                '    ComboBox4.SelectedIndex = 1
                'Else
                '    ComboBox4.SelectedIndex = 0
                'End If
                If IdVendedorU > 0 And Op.VendedorUsuario = 1 Then
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(IdVendedorU)
                Else
                    ComboBox5.SelectedIndex = IdsVendedores.Busca(B.Cliente.IdVendedor)
                End If
            End If
            'If Saldo >= B.Cliente.Credito Then
            '    SinCredito = True
            'End If
            idCliente = B.Cliente.ID
            'LlenaCombos("tblclientescuentas", ComboBox6, "cuenta", "nombret", "idcuenta", IdsCuentas, "idcliente=" + idCliente.ToString, "NO APLICA")
            'ComboBox6.SelectedIndex = 0
            'Isr = B.Cliente.ISR
            IdLista = B.Cliente.IdLista
            'IvaRetenido = B.Cliente.IvaRetenido
            SIVA = B.Cliente.IVA
            Sobre = B.Cliente.SobreescribeIVA
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            B.Dispose()
            TextBox5.Focus()
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        buscaArticuloBoton()
    End Sub
    Private Sub buscaArticuloBoton()
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.Articulo
        Dim B As New frmBuscador(TipodeBusqueda, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), True, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Select Case B.Tipo
                Case "I"
                    LlenaDatosArticulo(B.Inventario)
                
            End Select
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
                End If
                Fo.Dispose()
            End If
            TextBox12.Focus()
        End If
        B.Dispose()
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

        ArtNombre = Articulo.Nombre
        'cmbVariante.Visible = False
        PrecioU = a.Precio
        PrecioBase = a.Precio
        CostoArticulo = Articulo.CostoBase
        'Esamortizacion = Articulo.EsAmortizacion
        If Op._CodigoPostalLocal = "0" Then
            TipoCantidad = Articulo.TipoContenido.ID
        Else
            TipoCantidad = Articulo.TipoCantidad.ID
        End If
        TextBox12.Text = a.Precio.ToString
        ManejaSeries = Articulo.ManejaSeries
        TextBox6.Text = Cant * PrecioU
        UsaFormula = Articulo.UsaFormula
        If Sobre = 0 Then
            TextBox8.Text = Articulo.Iva.ToString
        Else
            TextBox8.Text = SIVA.ToString
        End If
        'cmbVariante.Visible = False
        'cmbtipoarticulo.Text = "A"
        IdInventario = Articulo.ID
        PrecioNeto = Articulo.PrecioNeto
        ConsultaOn = False
        TextBox3.Text = Articulo.Clave
        ComboBox1.SelectedIndex = IDsMonedas.Busca(a.IdMoneda)
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
        BotonBuscar()
    End Sub
    Private Sub BotonBuscar()
        Dim f As New frmVentasApartadosConsulta(ModosDeBusqueda.Secundario, 0)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idApartado = f.IdApartado
            LlenaDatosVenta()
            NuevoConcepto()
        End If
        f.Dispose()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar este apartado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosAlta, PermisosN.Secciones.Ventas) = True Then
                Dim C As New dbVentasApartados(idApartado, MySqlcon)
                C.Eliminar(idApartado)
                PopUp("Apartado Eliminado", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub
    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If GlobalPermisos.ChecaPermiso(PermisosN.Ventas.VentasApartadosCancelar, PermisosN.Secciones.Ventas) = False Then
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Estado = Estados.Pendiente Then
            MsgBox("No se puede cancelar un apartado pendiente.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If MsgBox("¿Cancelar apartado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim VP As New dbVentasPagos(MySqlcon)
            If VP.HayPagosVentas(idApartado) = 0 Then
                Modificar(Estados.Cancelada)
            Else
                MsgBox("Para poder cancelar este apartado primero debe cancelar todos los pagos.", MsgBoxStyle.Information, GlobalNombreApp)
            End If
        End If
    End Sub

    
    Private Sub ComboBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ConsultaOn Then
            If Op._TipoSelAlmacen = "0" Then
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            Else
                LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString, "Sel. Almacen")
            End If
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            If ComboBox8.Items.Count > 0 Then
                If Op._TipoSelAlmacen = "0" Then
                    ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
                Else
                    ComboBox8.SelectedIndex = 0
                End If
            Else
                MsgBox("Esta sucursal no cuenta con almacenes.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
            If LlenandoDatos = False Then
                Dim Sf As New dbSucursalesFolios(MySqlcon)
                Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Apartados, 0)
                TextBox11.Text = Sf.Serie
                Eselectronica = Sf.EsElectronica
                'If Sf.EsElectronica >= 1 Then
                'CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
                'End If
                Dim V As New dbVentasApartados(MySqlcon)
                TextBox2.Text = V.DaNuevoFolio(TextBox11.Text).ToString
                If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
                If CInt(TextBox2.Text) > Sf.FolioFinal Then
                    LimitedeFolios = True
                    MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
                Else
                    LimitedeFolios = False
                End If
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
                    TextBox4.Text = TextBox4.Text + " " + Fo.FormulaString
                End If
                Fo.Dispose()
            End If
        End If
        'If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
        '    Dim Precio As Double
        '    If CantidadMostrar <> 0 Then
        '        Precio = CDbl(TextBox6.Text) / CantidadMostrar
        '    Else
        '        Precio = 0
        '    End If
        '    Dim FE As New frmVentasDaEquivalencia(IdInventario, CDbl(TextBox5.Text), CantidadMostrar, TipoCantidadMostrar, Precio)
        '    If FE.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '        TextBox5.Text = FE.Cantidad.ToString
        '        CantidadMostrar = FE.CantidadM
        '        TipoCantidadMostrar = FE.TipoCantidadM
        '        If FE.Cantidad = FE.CantidadM Then
        '            SinConcersion = True
        '        Else
        '            SinConcersion = False
        '        End If
        '        If FE.Cantidad <> 0 Then
        '            TextBox12.Text = Format((FE.CantidadM * FE.PrecioM) / FE.Cantidad, "0.00")
        '        Else
        '            TextBox12.Text = "0"
        '        End If
        '        Label20.Text = CantidadMostrar.ToString
        '    End If
        '    FE.Dispose()
        'End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IdInventario <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        'Try
        '    Dim Forma As New frmBuscaDocumentoVenta(0, False, 2, IdsSucursales.Valor(ComboBox3.SelectedIndex), 0, True, True, True, 0, False, "", False)
        '    If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '        Dim V As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
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
        '                    idRemisiones = Forma.id

        '                Case 3
        '                    Dim Cv As New dbVentas(Forma.id(0), MySqlcon, Op._Sinnegativos)
        '                    TextBox1.Text = Cv.Cliente.Clave
        '            End Select
        '            Guardar()
        '            If Estado <> 0 Then
        '                V.AgregarDetallesReferencia(idApartado, Forma.id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0)
        '                ConsultaDetalles()
        '            End If
        '        Else
        '            V.AgregarDetallesReferencia(idApartado, Forma.id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), 0)
        '            ConsultaDetalles()
        '        End If
        '        NuevoConcepto()
        '        Button11.Enabled = False
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        'End Try
    End Sub
    
    '---------------------------Nueva Version -----------------------------------------------------
    '-------------------------------------------------------------------------------------
    Private Sub LlenaNodosImpresion()
        Try
            Dim O As New dbOpciones(MySqlcon)
            'Dim AgregaSeries As Boolean
            Dim QuitaIvaCero As Boolean
            Dim TotalDescuento As Double = 0
            If O._IVaCero = 1 Then
                QuitaIvaCero = True
            Else
                QuitaIvaCero = False
            End If
            'If O._AgregaSeriesAVenta = 0 Then
            '    AgregaSeries = False
            'Else
            '    AgregaSeries = True
            'End If
            Dim V As New dbVentasApartados(idApartado, MySqlcon)
            Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
            V.DaTotal(idApartado, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
            'V.DaDatosTimbrado(idApartado)
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
            ImpND.Add(New NodoImpresionN("", "nombrecliente", V.Cliente.Nombre, 0), "nombrecliente")
            ImpND.Add(New NodoImpresionN("", "clavecliente", V.Cliente.Clave, 0), "clavecliente")
            ImpND.Add(New NodoImpresionN("", "telcliente", V.Cliente.Telefono, 0), "telcliente")
            ImpND.Add(New NodoImpresionN("", "contactocliente", V.Cliente.Contacto, 0), "contactocliente")
            'ImpND.Add(New NodoImpresionN("", "regimenfiscal", Sucursal.RegimenFiscal, 0), "regimenfiscal")
            'ImpND.Add(New NodoImpresionN("", "nocuenta", V.NoCuenta, 0), "nocuenta")
            ImpND.Add(New NodoImpresionN("", "vendedor", Vendedor.Nombre, 0), "vendedor")
            If V.Cliente.DireccionFiscal = 0 Then
                ImpND.Add(New NodoImpresionN("", "direccioncliente", V.Cliente.Direccion, 0), "direccioncliente")
                ImpND.Add(New NodoImpresionN("", "noexteriorcliente", V.Cliente.NoExterior, 0), "noexteriorcliente")
                ImpND.Add(New NodoImpresionN("", "nointeriorcliente", V.Cliente.NoInterior, 0), "nointeriorcliente")
                ImpND.Add(New NodoImpresionN("", "coloniacliente", V.Cliente.Colonia, 0), "coloniacliente")
                ImpND.Add(New NodoImpresionN("", "cpcliente", V.Cliente.CP, 0), "cpcliente")
                ImpND.Add(New NodoImpresionN("", "ciudadcliente", V.Cliente.Ciudad, 0), "ciudadcliente")
                ImpND.Add(New NodoImpresionN("", "estadocliente", V.Cliente.Estado, 0), "estadocliente")
                ImpND.Add(New NodoImpresionN("", "municipiocliente", V.Cliente.Municipio, 0), "municipiocliente")
                ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais, 0), "paiscliente")
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
                ImpND.Add(New NodoImpresionN("", "paiscliente", V.Cliente.Pais2, 0), "paiscliente")
                ImpND.Add(New NodoImpresionN("", "refcliente", V.Cliente.ReferenciaDomicilio2, 0), "refcliente")
            End If



            ImpND.Add(New NodoImpresionN("", "rfccliente", V.Cliente.RFC, 0), "rfccliente")
            ImpND.Add(New NodoImpresionN("", "curpcliente", V.Cliente.CURP, 0), "curpcliente")


            ImpND.Add(New NodoImpresionN("", "serie", V.Serie, 0), "serie")
            ImpND.Add(New NodoImpresionN("", "folio", Format(V.Folio, "00000"), 0), "folio")
            'ImpND.Add(New NodoImpresionN("", "noaprobacion", V.NoAprobacion, 0), "noaprobacion")
            'ImpND.Add(New NodoImpresionN("", "yearaprobacion", V.YearAprobacion, 0), "yearaprobacion")
            'ImpND.Add(New NodoImpresionN("", "certificado", V.NoCertificado, 0), "certificado")

            ImpND.Add(New NodoImpresionN("", "fecha", Replace(V.Fecha, "/", "-"), 0), "fecha")
            ImpND.Add(New NodoImpresionN("", "hora", V.Hora, 0), "hora")
            ImpND.Add(New NodoImpresionN("", "fechasalida", Replace(V.FechaSalida, "/", "-"), 0), "fechasalida")
            ImpND.Add(New NodoImpresionN("", "horasalida", V.HoraSalida, 0), "horasalida")

            'If Sucursal.Ciudad2 <> "" And Sucursal.Estado2 <> "" Then
            '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad2 + " " + Sucursal.Estado2 + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
            'Else
            '    ImpND.Add(New NodoImpresionN("", "lugar", Sucursal.Ciudad + " " + Sucursal.Estado + " " + Replace(V.Fecha, "/", "-") + " " + V.Hora, 0), "lugar")
            'End If

            'ImpND.Add(New NodoImpresionN("", "uuid", V.uuid, 0), "uuid")
            'ImpND.Add(New NodoImpresionN("", "cersat", V.NoCertificadoSAT, 0), "cersat")
            'ImpND.Add(New NodoImpresionN("", "fechatimbrado", V.FechaTimbrado, 0), "fechatimbrado")

            'ImpND.Add(New NodoImpresionN("", "sellocfdi", V.SelloCFD, 0), "sellocfdi")
            'ImpND.Add(New NodoImpresionN("", "sellosat", V.SelloSAT, 0), "sellosat")
            'CadenaCFDI = "||1.0|" + V.uuid + "|" + V.FechaTimbrado + "|" + V.SelloCFD + "|" + V.NoCertificadoSAT + "||"
            'ImpND.Add(New NodoImpresionN("", "cadenasat", CadenaCFDI, 0), "cadenasat")


            Dim DR As MySql.Data.MySqlClient.MySqlDataReader
            Dim VI As New dbVentasApartadosDetalles(MySqlcon)
            DR = VI.ConsultaReader(idApartado, False)
            ImpNDD.Clear()
            CuantosRenglones = 0
            Dim Cont As Integer = 0
            While DR.Read
                If DR("cantidad") <> 0 Then
                    ImpNDD.Add(New NodoImpresionN("", "clave", DR("clave"), 0), "clave" + Format(Cont, "000"))
                Else
                    ImpNDD.Add(New NodoImpresionN("", "clave", "", 0), "clave" + Format(Cont, "000"))
                End If
                ImpNDD.Add(New NodoImpresionN("", "descripcion", DR("descripcion"), 0), "descripcion" + Format(Cont, "000"))
                ImpNDD.Add(New NodoImpresionN("", "extra", DR("extra"), 0), "extra" + Format(Cont, "000"))

                If DR("cantidad") <> 0 Then
                    ImpNDD.Add(New NodoImpresionN("", "cantidad", Format(DR("cantidad"), O._formatocantidad).PadLeft(O.EspacioCantidad), 0), "cantidad" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "tipocantidad", DR("tipocantidad"), 0), "tipocantidad" + Format(Cont, "000"))
                Else
                    ImpNDD.Add(New NodoImpresionN("", "cantidad", "", 0), "cantidad" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "tipocantidad", "", 0), "tipocantidad" + Format(Cont, "000"))
                End If
                If DR("cantidad") <> 0 Then
                    ImpNDD.Add(New NodoImpresionN("", "preciou", Format(DR("precio") / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciou" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "preciouiva", Format((DR("precio") / DR("cantidad")) * (1 + DR("iva") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciouiva" + Format(Cont, "000"))
                Else
                    ImpNDD.Add(New NodoImpresionN("", "preciou", "", 0), "preciou" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "preciouiva", "", 0), "preciouiva" + Format(Cont, "000"))
                End If
                If DR("cantidad") <> 0 Then
                    ImpNDD.Add(New NodoImpresionN("", "importe", Format(DR("precio"), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importe" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "importeiva", Format(DR("precio") * (1 + DR("iva") / 100), O._formatoImporte).PadLeft(O.EspacioImporte), 0), "importeiva" + Format(Cont, "000"))
                Else
                    ImpNDD.Add(New NodoImpresionN("", "importe", "", 0), "importe" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "importeiva", "", 0), "importeiva" + Format(Cont, "000"))
                End If

                If DR("cantidad") <> 0 Then
                    Dim Desc As Double
                    Desc = (DR("precio") / (1 - DR("descuento") / 100))
                    TotalDescuento += Desc - DR("precio")
                    ImpNDD.Add(New NodoImpresionN("", "descuentopor", Format(DR("descuento"), "0.00").PadLeft(O.EspacioPrecioUnitacio) + "%", 0), "descuentopor" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "descuentocant", Format(Desc - DR("precio"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocant" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "preciosindesc", Format(Desc / DR("cantidad"), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "preciosindesc" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", Format((Desc / DR("cantidad")) * (DR("descuento") / 100), O._formatoPrecioU).PadLeft(O.EspacioPrecioUnitacio), 0), "descuentocantuni" + Format(Cont, "000"))
                    'Vo = Vd / ( 1 - (Por/100))
                Else
                    ImpNDD.Add(New NodoImpresionN("", "descuentopor", "", 0), "descuentopor" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "descuentocant", "", 0), "descuentocant" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "preciosindesc", "", 0), "preciosindesc" + Format(Cont, "000"))
                    ImpNDD.Add(New NodoImpresionN("", "descuentocantuni", "", 0), "descuentocantuni" + Format(Cont, "000"))
                End If

                CuantosRenglones += 1
                Cont += 1
            End While
            DR.Close()

            ImpND.Add(New NodoImpresionN("", "peso", Format(V.TotalPeso, "#,##0.00") + "Kg.", 0), "peso")
            ImpND.Add(New NodoImpresionN("", "prioridadstr", V.PrioridadSTR, 0), "prioridadstr")
            If V.Prioridad = 0 Then
                ImpND.Add(New NodoImpresionN("", "prioridad", "ALTA", 0), "prioridad")
            End If
            If V.Prioridad = 1 Then
                ImpND.Add(New NodoImpresionN("", "prioridad", "MEDIA", 0), "prioridad")
            End If
            If V.Prioridad = 2 Then
                ImpND.Add(New NodoImpresionN("", "prioridad", "BAJA", 0), "prioridad")
            End If
            ImpND.Add(New NodoImpresionN("", "comentario", V.Comentario, 0), "comentario")
            ImpND.Add(New NodoImpresionN("", "subtotal", Format(V.Subtotal, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotal")
            ImpND.Add(New NodoImpresionN("", "subtotalsindesc", Format(V.Subtotal + TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsindesc")
            ImpND.Add(New NodoImpresionN("", "totaldesc", Format(TotalDescuento, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "totaldesc")
            ImpND.Add(New NodoImpresionN("", "subtotalsinret", Format(V.Subtotal + V.TotalIva, O._formatoSubtotal).PadLeft(O.EspacioSubtotal), 0), "subtotalsinret")
            ImpND.Add(New NodoImpresionN("", "totalcantidad", V.DaTotalCantidad(idApartado).ToString, 0), "totalcantidad")
            If V.Surtido = 0 Then
                ImpND.Add(New NodoImpresionN("", "surtido", "NO", 0), "surtido")
            Else
                ImpND.Add(New NodoImpresionN("", "surtido", "SI", 0), "surtido")
            End If
            Dim Ivas As New Collection
            Dim IvasImporte As New Collection
            DR = V.DaIvas(idApartado)
            ImpNDDi.Clear()
            ImpNDi2.Clear()
            Dim IAnt As Double
            'If Ivas.Count < 2 Then QuitaIvaCero = False
            While DR.Read
                If Ivas.Contains(DR("iva").ToString) = False Then
                    Ivas.Add(DR("iva"), DR("iva").ToString)
                End If
                If IvasImporte.Contains(DR("iva").ToString) = False Then
                    If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                        IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    Else
                        IvasImporte.Add((DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                    End If
                Else
                    IAnt = IvasImporte(DR("iva").ToString)
                    IvasImporte.Remove(DR("iva").ToString)
                    'IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
                    If IDsMonedas2.Valor(ComboBox2.SelectedIndex) = DR("idmoneda") Then
                        IvasImporte.Add(IAnt + DR("precio") * (DR("iva") / 100), DR("iva").ToString)
                    Else
                        IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)) * DR("tipodecambio"), DR("iva").ToString)
                    End If
                End If

            End While
            DR.Close()

            'Dim TotalesXIVa As String = ""

            'For Each I As Double In Ivas
            '    If TotalesXIVa = "" Then
            '        TotalesXIVa = "Subtotal Iva " + Format(I, "#0.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
            '    Else
            '        TotalesXIVa += vbCrLf + "Subtotal Iva " + Format(I, "#0.00") + "%: " + Format(V.DaTotalxIVa(V.ID, I), O._formatoTotal)
            '    End If
            'Next
            'ImpND.Add(New NodoImpresionN("", "totalxiva", TotalesXIVa, 0), "totalxiva")
            If Ivas.Count > 1 And QuitaIvaCero Then
                IvasImporte.Remove("0")
                Ivas.Remove("0")
            End If
            Cont = 0

            For Each I As Double In Ivas
                'If I = 0 And QuitaIvaCero Then

                'Else
                ImpNDDi.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
                ImpNDi2.Add(New NodoImpresionN("", "Iva " + Format(I, "#0.00") + "%:", Format(IvasImporte(I.ToString), O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
                'End If

                Cont += 1
            Next

            'If V.ISR <> 0 Then
            '    ImpNDDi.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            '    ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "isr")
            '    Cont += 1
            'Else
            '    ImpND.Add(New NodoImpresionN("ISR " + Format(V.ISR, "#0.00") + "%:", "ISR " + Format(V.ISR, "#0.00") + "%:", Format(V.TotalISR, O._formatoIva).PadLeft(O.EspacioIva), 0), "isr")
            'End If
            'If V.IvaRetenido <> 0 Then
            '    ImpNDDi.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "iva" + Format(Cont, "00"))
            '    ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "ivaret")
            '    Cont += 1
            'Else
            '    ImpND.Add(New NodoImpresionN("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", "IVAret. " + Format(V.Iva, "#0.00") + "%:", Format(V.TotalIvaRetenido, O._formatoIva).PadLeft(O.EspacioIva), 0), "ivaret")
            'End If

            ImpND.Add(New NodoImpresionN("", "Total:", Format(V.TotalVenta, O._formatoTotal).PadLeft(O.Espaciototal), 0), "total")
            ImpND.Add(New NodoImpresionN("", "anticipo", Format(V.Credito, O._formatoTotal).PadLeft(O.Espaciototal), 0), "anticipo")
            ImpND.Add(New NodoImpresionN("", "restante", Format(V.TotalVenta - V.Credito, O._formatoTotal).PadLeft(O.Espaciototal), 0), "restante")

            Dim f As New StringFunctions
            Dim CL As New CLetras
            'ImpND.Add(New NodoImpresionN("", "totalletra", f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), V.IdConversion), 0), "totalletra")
            If V.Totalapagar >= 0 Then
                ImpND.Add(New NodoImpresionN("", "totalletra", CL.LetrasM(Format(V.TotalVenta, "0.00"), V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
            Else
                ImpND.Add(New NodoImpresionN("", "totalletra", "MENOS " + CL.LetrasM(Format(V.TotalVenta * -1, "0.00"), V.IdMoneda, GlobalIdiomaLetras), 0), "totalletra")
            End If
            'ImpND.Add(New NodoImpresionN("", "cadenaoriginal", Cadena, 0), "cadenaoriginal")
            'ImpND.Add(New NodoImpresionN("", "sello", Sello, 0), "sello")
            'Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
            'If FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
            'ImpND.Add(New NodoImpresionN("", "titulo", O.TituloParcialidad, 0), "titulo")
            'Else
            'ImpND.Add(New NodoImpresionN("", "titulo", O.TituloFactura, 0), "titulo")
            'End If
            'If FP.Tipo = dbFormasdePago.Tipos.Contado Then
            'If V.IdFormadePago <> 98 Then
            '    ImpND.Add(New NodoImpresionN("", "metodopago", FP.Nombre, 0), "metodopago")
            '    If FP.Tipo <> dbFormasdePago.Tipos.Parcialidad Then
            '        ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN UNA SOLA EXHIBICIÓN", 0), "formadepago")
            '        ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
            '        ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
            '        ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
            '        ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
            '        ImpND.Add(New NodoImpresionN("", "uuidorigen", "", 0), "uuidorigen")
            '    Else
            '        V.ObtenerFacturaOriginal(V.IdVentaOrigen)
            '        If V.Parcialidades <> 1 Then
            '            ImpND.Add(New NodoImpresionN("", "formadepago", "PARCIALIDAD " + V.Parcialidad.ToString + " DE " + V.Parcialidades.ToString, 0), "formadepago")
            '        Else
            '            ImpND.Add(New NodoImpresionN("", "formadepago", "PARCIALIDAD " + V.Parcialidad.ToString, 0), "formadepago")
            '        End If
            '        ImpND.Add(New NodoImpresionN("", "folioorigen", Format(V.FolioOrigen, "00000"), 0), "folioorigen")
            '        ImpND.Add(New NodoImpresionN("", "serieorigen", V.SerieOrigen, 0), "serieorigen")
            '        ImpND.Add(New NodoImpresionN("", "montoorigen", Format(V.MontoOrigen, O._formatoTotal), 0), "montoorigen")
            '        ImpND.Add(New NodoImpresionN("", "fechaorigen", V.FechaOrigen, 0), "fechaorigen")
            '        ImpND.Add(New NodoImpresionN("", "uuidorigen", V.FolioUUIDOrigen, 0), "uuidorigen")
            '    End If
            'Else
            '    ImpND.Add(New NodoImpresionN("", "metodopago", "NO IDENTIFICADO", 0), "metodopago")
            '    If V.Parcialidades <> 1 Then
            '        ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN " + V.Parcialidades.ToString + " PARCIALIDADES", 0), "formadepago")
            '    Else
            '        ImpND.Add(New NodoImpresionN("", "formadepago", "PAGO EN PARCIALIDADES", 0), "formadepago")
            '    End If
            '    ImpND.Add(New NodoImpresionN("", "folioorigen", "", 0), "folioorigen")
            '    ImpND.Add(New NodoImpresionN("", "serieorigen", "", 0), "serieorigen")
            '    ImpND.Add(New NodoImpresionN("", "montoorigen", "", 0), "montoorigen")
            '    ImpND.Add(New NodoImpresionN("", "fechaorigen", "", 0), "fechaorigen")
            '    ImpND.Add(New NodoImpresionN("", "uuidorigen", "", 0), "uuidorigen")
            'End If

            ''Else
            ''ImpND.Add(New NodoImpresionN("", "metodopago", "No Identificado", 0), "metodopago")
            ''End If
            'If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
            '    ImpND.Add(New NodoImpresionN("", "condicionpago", "CONTADO", 0), "condicionpago")
            '    ImpND.Add(New NodoImpresionN("", "diascredito", "", 0), "diascredito")
            '    ImpND.Add(New NodoImpresionN("", "limitecredito", "", 0), "limitecredito")
            'Else

            '    ImpND.Add(New NodoImpresionN("", "condicionpago", "CRÉDITO", 0), "condicionpago")
            '    ImpND.Add(New NodoImpresionN("", "diascredito", V.Cliente.CreditoDias.ToString + " Días.", 0), "diascredito")
            '    ImpND.Add(New NodoImpresionN("", "limitecredito", Format(DateAdd(DateInterval.Day, V.Cliente.CreditoDias, CDate(V.Fecha)), "yyyy-MM-dd"), 0), "limitecredito")
            'End If

            'If V.IdConversion = 2 Then
            '    ImpND.Add(New NodoImpresionN("", "leyendadolar", "", 0), "leyendadolar")
            'Else
            '    ImpND.Add(New NodoImpresionN("", "leyendadolar", "El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar a la venta frente al peso vigente al día de su pago.", 0), "leyendadolar")
            'End If
            If V.Estado = Estados.Cancelada Then
                ImpND.Add(New NodoImpresionN("", "cancelado", "CANCELADA", 0), "cancelado")
            Else
                ImpND.Add(New NodoImpresionN("", "cancelado", "", 0), "cancelado")
            End If
            Posicion = 0
            'Dim CB As New ThoughtWorks.QRCode.Codec.QRCodeEncoder
            'CodigoBidimensional = CB.Encode("?&re=" + Sucursal.RFC + "&rr=" + V.Cliente.RFC + "&tt=" + Format(V.TotalVenta, O._formatoTotal) + "&id=" + V.uuid, System.Text.Encoding.Default)
            NumeroPagina = 1
        Catch ex As Exception
            MsgBox("P1 " + ex.Message + " E=" + ImpND.Item(ImpND.Count - 1).DataPropertyName, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try

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
            ImpDb.DaZonaDetalles(TiposDocumentos.VentasApartados, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        Else
            ImpDb.DaZonaDetalles(TiposDocumentos.VentasApartados + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex))
        End If
        If TipoImpresora = 0 Then
            DibujaPaginaEstatico(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt)
        Else
            DibujaPaginaFlujo(e, ImpDb.Y, ImpDb.YL, ImpDb.RG, ImpDb.Alt, ImpDb.Modo)
        End If

    End Sub
    Private Sub DibujaPaginaEstatico(ByRef e As System.Drawing.Graphics, ByVal pZonaY As Integer, ByVal pZonaYL As Integer, ByVal pZonaRG As Integer, ByVal pZonaAlt As Integer)
        Try

        Catch ex As Exception

        End Try
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
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentasApartados, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentasApartados + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '   e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'E'nd If
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
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" Then
                    If YCoord = 0 Then YCoord = pZonaY / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then
                                        'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        'YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If

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
            Posicion += 1
            C += 1
        End While


        'Nodos fijos
        Try
            For Each n As NodoImpresionN In ImpNDi
                If n.DataPropertyName = "iva" Then niva = n
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "codigobi" Then ncb = n
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
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 *, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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

            'e.DrawString(ImpND("cancelado").Valor, ImpNDi("cancelado").Fuente, Brushes.Red, ImpNDi("cancelado").X / 40 * 10, ImpNDi("cancelado").Y / 40 * 10)
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

                        If niva.ConEtiqueta >= 1 Then
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
                C = 0
                If niva2.Visible = 1 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

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
        Dim ncb As New NodoImpresionN("cb", "0", "0", 0)
        strF.Alignment = StringAlignment.Near
        strF.LineAlignment = StringAlignment.Near
        e.PageUnit = GraphicsUnit.Millimeter
        Try
            'If DocAImprimir = 0 Then
            If TipoImpresora = 0 Then
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentasApartados, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            Else
                e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.VentasApartados + 1000, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            End If
            'Else
            'If TipoImpresora = 0 Then
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencion, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'Else
            '    e.DrawImage(Image.FromFile(SA.DaRuta(TiposDocumentos.FormatoRetencionTicket, IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True)), 1, 1, CInt(864 / 40 * 10), CInt(1116 / 40 * 10))
            'End If
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
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "codigobi" Then ncb = n
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
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, n.Y / 40 * 10, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, n.Y / 40 * 10)
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

                        If niva.ConEtiqueta >= 1 Then
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
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 0 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If YCoord = 0 Then YCoord = niva2.Y / 40 * 10

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, YCoord)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, YCoord)
                        End If
                        'End If
                        'Next
                        YCoord += 4
                        C += 1
                    End While
                End If

            End If
            If ncb.Visible = 1 And ncb.Tipo = 0 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(ncb.Y / 40 * 10), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
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
            If YCoord >= LimY And Posicion > 0 And (pModo = 0 Or pModo = 2) Then
                MasPaginas = True
                Exit While
            End If
            For Each n As NodoImpresionN In ImpNDi
                If n.Visible = 1 And n.Tipo = 1 And n.DataPropertyName <> "cancelado" Then
                    If YCoord = 0 Then YCoord = (pZonaY + 3) / 40 * 10
                    Select Case n.TipoNodo
                        Case 0
                            'normal
                            Nimp = ImpNDD(n.DataPropertyName + Format(C, "000"))
                            If n.ConEtiqueta >= 1 Then
                                If n.TipoDato = 0 Then
                                    Rec = New RectangleF(n.X / 40 * 10, YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                    If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                    Else
                                        e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        YExtra2 = InsertaEnters(n.Texto + " " + Nimp.Valor, pZonaRG, YCoord, pZonaAlt)
                                    End If
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
                If n.DataPropertyName = "iva2" Then niva2 = n
                If n.DataPropertyName = "codigobi" Then ncb = n
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
                                        If n.ConEtiqueta >= 1 Then
                                            If n.ConEtiqueta = 2 And Nimp.Valor = "" Then

                                            Else
                                                Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                                e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                            End If
                                            'e.DrawString(n.Texto + " " + Nimp.Valor, n.Fuente, Brushes.Black, n.X / 40 * 10, n.Y / 40 * 10)
                                        Else
                                            Rec = New RectangleF(n.X / 40 * 10, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord, n.XL / 40 * 10, n.YL / 40 * 10)
                                            e.DrawString(Nimp.Valor, n.Fuente, Brushes.Black, Rec, strF)
                                        End If
                                    Else
                                        If n.ConEtiqueta >= 1 Then e.DrawString(n.Texto, n.Fuente, Brushes.Black, (n.X / 40 * 10) - 25, Math.Abs((n.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord)
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

                        If niva.ConEtiqueta >= 1 Then
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
                C = 0
                If niva2.Visible = 1 And niva2.Tipo = 2 Then
                    YCoord = 0
                    While C < ImpNDi2.Count
                        'For Each n As NodoImpresionN In ImpNDi
                        Nimp = ImpNDi2("iva" + Format(C, "00"))

                        'If Nimp.Visible = 1 And Nimp.Tipo = 1 Then
                        If Ycoord2 = 0 Then Ycoord2 = Math.Abs((niva2.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord

                        If niva2.ConEtiqueta >= 1 Then
                            e.DrawString(Nimp.DataPropertyName, niva2.Fuente, Brushes.Black, (niva2.X / 40 * 10) - 25, Ycoord2)
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        Else
                            e.DrawString(Nimp.Valor, niva2.Fuente, Brushes.Black, niva2.X / 40 * 10, Ycoord2)
                        End If
                        'End If
                        'Next
                        Ycoord2 += 4
                        C += 1
                    End While
                End If

            End If

            If ncb.Visible = 1 And ncb.Tipo = 2 Then e.DrawImage(CodigoBidimensional, CInt(ncb.X / 40 * 10), CInt(Math.Abs((ncb.Y / 40 * 10) - ((pZonaY + pZonaYL) / 40 * 10)) + YCoord), CInt(ncb.XL / 40 * 10), CInt(ncb.YL / 40 * 10))
            NumeroPagina += 1
        Catch ex As Exception
            MsgBox("Ha ocurrido un error al generar la impresión " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'If iTipoFacturacion = 2 Then
        '    'CFDi
        '    DibujaPaginai(e.Graphics)
        'Else
        'CFD
        If DocAImprimir = 0 Then
            DibujaPaginaN(e.Graphics)
            If MasPaginas = True Or NumeroPagina > 2 Then
                'e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00") + "/" + Format(CuantaY, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
                e.Graphics.DrawString("Página: " + Format(NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 185, 272)
            End If
            If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
            End If
            'If Estado = Estados.Cancelada Then
            '    e.Graphics.DrawString("CANCELADA", New Font("Arial", 18, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point), Brushes.Red, 80, 130)
            'End If
            e.HasMorePages = MasPaginas
            'DibujaPagina(e.Graphics)
            'e.HasMorePages = True
            'End If
        End If
        If DocAImprimir = 1 Then
            DibujaPaginaN(e.Graphics)
            If Estado = Estados.Inicio Or Estado = Estados.SinGuardar Or Estado = Estados.Pendiente Then
                e.Graphics.DrawString("IMPRESIÓN DE PRUEBA DOCUMENTO NO GUARDADO.", New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Red, 2, 2)
            End If
        End If
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        'Pendiente Llenar Addenda
        'Dim XMLAddenda As String = ""
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        If Estado = Estados.SinGuardar Then
            'Select Case UsaAdenda
            '    Case 1
            '        Dim Ad As New dbAdendasFemsa(MySqlcon)
            '        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            '        Ad.BuscaAddenda(idApartado, 0)
            '        If Ad.ID <> 0 Then
            '            XMLAddenda = Ad.CreaXMLCFDI(Eselectronica, S.Email)
            '        End If
            '    Case 2
            '        Dim frmA As New frmAddendaOxxo(idApartado, True)
            '        frmA.elementos = Articulos
            '        frmA.Serie = TextBox11.Text
            '        frmA.Folio = TextBox2.Text
            '        frmA.Doc = 0
            '        frmA.EsElectronica = Eselectronica
            '        frmA.Total = CDbl(Label14.Text)
            '        frmA.TipodeCambio = CDbl(TextBox10.Text)
            '        frmA.Moneda = If(IDsMonedas.Valor(ComboBox2.SelectedIndex) = 2, "MXN", "USD")
            '        frmA.ShowDialog()
            '        XMLAddenda = frmA.XMLResultado
            '        frmA.Dispose()
            '    Case 3
            '        Dim C As New dbVentas(idApartado, MySqlcon)
            '        C.DaTotal(idApartado, 2)
            '        Dim Suc As New dbSucursales(C.IdSucursal, MySqlcon)
            '        Dim FrmA As New frmAddendaLey(idApartado, C.TotalDescuento, Eselectronica, Format(DateTimePicker1.Value, "yyyy/MM/dd"), Suc.Email)
            '        FrmA.ShowDialog()
            '        XMLAddenda = FrmA.strXML
            '        FrmA.Dispose()
            'End Select
            'Else
            Modificar(Estados.SinGuardar)
        End If
        'Select Case Eselectronica
        'Case 0
        Imprimir()
        'Case 1
        'CadenaOriginal(Estado, XMLAddenda)
        'Case 2
        'CadenaOriginali(Estado, XMLAddenda)
        'End Select
    End Sub
    Private Function InsertaEnters(ByVal Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        C = 0
        Dim CC As Integer = 0
        Dim car As String
        Dim Yx As Integer = 0
        While C < Cadena.Length
            car = Cadena.Substring(C, 1)
            If car = Chr(13) Or CC = CadaCuantos Or car = Chr(10) Then
                If car = Chr(13) Then C += 1
                Yx += AumentoY
                CC = 0
            Else
                CC += 1
            End If
            C += 1
        End While
        Return Yx
    End Function

    Private Sub TextBox12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox12.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Op._TipoSelAlmacen = "0" Then
                BotonAgregar()
            Else
                If ComboBox8.SelectedIndex <= 0 Then
                    ComboBox8.Focus()
                Else
                    BotonAgregar()
                End If
            End If
        End If
        If e.KeyCode = Keys.F11 And IdInventario <> 0 Then
            If UsaFormula = 1 Then
                Dim Fo As New frmInventarioFormula01(ArtNombre)
                If Fo.ShowDialog = Windows.Forms.DialogResult.OK Then
                    TextBox5.Text = Format(Fo.Resultado, "0.00")
                    TextBox4.Text = Fo.FormulaString
                End If
                Fo.Dispose()
            End If
        End If
        'If e.KeyCode = Keys.F2 And IdInventario <> 0 Then
        '    Dim Precio As Double
        '    If CantidadMostrar <> 0 Then
        '        Precio = CDbl(TextBox6.Text) / CantidadMostrar
        '    Else
        '        Precio = 0
        '    End If
        '    Dim FE As New frmVentasDaEquivalencia(IdInventario, CDbl(TextBox5.Text), CantidadMostrar, TipoCantidadMostrar, Precio)
        '    If FE.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '        TextBox5.Text = FE.Cantidad.ToString
        '        CantidadMostrar = FE.CantidadM
        '        TipoCantidadMostrar = FE.TipoCantidadM
        '        If FE.Cantidad = FE.CantidadM Then
        '            SinConcersion = True
        '        Else
        '            SinConcersion = False
        '        End If
        '        If FE.Cantidad <> 0 Then
        '            TextBox12.Text = Format((FE.CantidadM * FE.PrecioM) / FE.Cantidad, "0.00")
        '        Else
        '            TextBox12.Text = "0"
        '        End If
        '        Label20.Text = CantidadMostrar.ToString

        '    End If
        '    FE.Dispose()
        'End If
        If e.KeyCode = Keys.F1 And IdInventario <> 0 Then
            Dim SP As New frmSelectorPrecios(IdInventario)
            If SP.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TextBox12.Text = SP.Precio.ToString
            End If
            SP.Dispose()
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
            TextBox8.Focus()
        End If
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim et As New frmVentasTextoExtra(TextBox4.Text, 2000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox4.Text = et.Texto
        End If

    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 500, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub
    'Private Sub ImprimirSeries()
    '    Dim V As New dbVentas(idApartado, MySqlcon)
    '    Dim Rep As CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    Rep = New repVentasSeries
    '    Rep.SetDataSource(V.ReporteVentasSeries(idApartado))
    '    'Rep.SetParameterValue("Encabezado", O._NombreEmpresa)
    '    Dim RV As New frmReportes(Rep, False)
    '    RV.Show()
    '    RV.Focus()
    'End Sub
    'Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
    '    ImprimirSeries()
    'End Sub
    Private Sub Imprimir()
        Dim RutaPDF As String
        Dim Archivos As New dbSucursalesArchivos
        RutaPDF = Archivos.DaRutaArchivos(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
        IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
        RutaPDF = RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")

        Dim SA As New dbSucursalesArchivos
        Dim Impresora As String
        Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.VentasApartados)
        TipoImpresora = SA.TipoImpresora
        PrintDocument1.DocumentName = "APARTADO" + TextBox11.Text + TextBox2.Text
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
            LlenaNodos(IdsSucursales.Valor(ComboBox3.SelectedIndex), TiposDocumentos.VentasApartados)
        Else
            LlenaNodos(IdsSucursales.Valor(ComboBox3.SelectedIndex), TiposDocumentos.VentasApartados + 1000)
        End If
        DocAImprimir = 0
        PrintDocument1.Print()



        'Dim V As New dbVentasApartados(idApartado, MySqlcon)
        'If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
        '    If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '        'LlenaNodosImpresionRet()
        '        If TipoImpresora = 0 Then
        '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
        '        Else
        '            LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
        '        End If
        '        PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
        '        If Impresora = "Bullzip PDF Printer" Then
        '            Dim obj As New Bullzip.PdfWriter.PdfSettings
        '            obj.Init()
        '            obj.PrinterName = Impresora
        '            obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
        '            obj.SetValue("ShowSettings", "never")
        '            obj.SetValue("ShowPDF", "yes")
        '            obj.SetValue("ShowSaveAS", "nofile")
        '            obj.SetValue("ConfirmOverwrite", "no")
        '            obj.SetValue("Target", "printer")
        '            obj.WriteSettings()
        '        End If
        '        DocAImprimir = 1
        '        PrintDocument1.Print()
        '    End If
        'End If

        'If V.Cliente.Email <> "" Then
        '    Try
        '        If MsgBox("¿Enviar factura por correo electrónico?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
        '            If V.Cliente.Email <> "" Then
        '                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto, My.Settings.encriptacionssl)
        '                'Dim O As New dbOpciones(MySqlcon)
        '                Dim S As New dbSucursales(V.IdSucursal, MySqlcon)
        '                Dim C As String
        '                C = "Eviado por: " + S.NombreFiscal + vbNewLine + "RFC: " + S.RFC + vbNewLine + "FACTURA" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje."
        '                'If pEstado = Estados.Pendiente Then
        '                M.send("Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\Fac" + TextBox11.Text + TextBox2.Text + ".pdf", "")
        '                'Else
        '                '   M.send("Comprobante Fiscal Digital Factura: " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, RutaPDF + "\FAC-" + V.Serie + V.Folio.ToString + ".pdf", RutaXml + "\FAC-" + V.Serie + V.Folio.ToString + ".xml")
        '                'End If

        '                PopUp("Correo enviado", 90)
        '            End If
        '        End If
        '    Catch ex As Exception
        '        MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        '    End Try
        'End If

    End Sub
    'Private Sub ImprimirRetenciones()
    '    Dim RutaPDF As String
    '    Dim Archivos As New dbSucursalesArchivos
    '    RutaPDF = Archivos.DaRutaArchivos(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.FacturaPDF, True)
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
    '    IO.Directory.CreateDirectory(RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
    '    RutaPDF = RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
    '    Dim SA As New dbSucursalesArchivos
    '    Dim Impresora As String
    '    Dim Tipo As Byte
    '    Impresora = SA.DaImpresoraActiva(IdsSucursales.Valor(ComboBox3.SelectedIndex), GlobalIdEmpresa, True, 0, TiposDocumentos.Venta)
    '    Tipo = SA.TipoImpresora
    '    Dim V As New dbVentas(idApartado, MySqlcon)
    '    If V.ISR <> 0 Or V.IvaRetenido <> 0 Then
    '        If MsgBox("Imprimir formato de retención", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
    '            LlenaNodosImpresionRet()
    '            If Tipo = 0 Then
    '                LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencion)
    '            Else
    '                LlenaNodos(V.IdSucursal, TiposDocumentos.FormatoRetencionTicket)
    '            End If
    '            PrintDocument1.DocumentName = "RetFac-" + V.Serie + V.Folio.ToString
    '            PrintDocument1.PrinterSettings.PrinterName = Impresora
    '            If Impresora = "Bullzip PDF Printer" Then
    '                Dim obj As New Bullzip.PdfWriter.PdfSettings
    '                obj.Init()
    '                obj.PrinterName = Impresora
    '                obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
    '                obj.SetValue("ShowSettings", "never")
    '                obj.SetValue("ShowPDF", "yes")
    '                obj.SetValue("ShowSaveAS", "nofile")
    '                obj.SetValue("ConfirmOverwrite", "no")
    '                obj.SetValue("Target", "printer")
    '                obj.WriteSettings()
    '            End If
    '            DocAImprimir = 1
    '            PrintDocument1.Print()
    '        End If
    '    End If
    'End Sub
    Private Sub TextBox11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox11.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub DateTimePicker1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox3.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub TextBox10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox10.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim FC As New frmClientes(1, idCliente)
        If FC.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = FC.CodigoCliente
        End If
        FC.Dispose()
    End Sub

    'Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
    '    Dim V As New dbVentas(MySqlcon)
    '    V.DaDatosTimbrado(idApartado)
    '    Dim FDT As New frmVentasDatosTimbrado(V.uuid, V.FechaTimbrado, V.NoCertificadoSAT, V.SelloCFD, V.SelloSAT)
    '    FDT.ShowDialog()
    '    FDT.Dispose()
    'End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbVentasApartados(MySqlcon)
                V.ActualizaComentario(idApartado, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        If Estado = Estados.Pendiente Or Estado = Estados.Inicio Or Estado = Estados.SinGuardar Then
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Apartados, 0)
            TextBox11.Text = Sf.Serie
            'Eselectronica = Sf.EsElectronica
            'If Sf.EsElectronica > 1 Then
            'CertificadoCaduco = ChecaCertificado(Sf.IdCertificado)
            'End If
            Dim V As New dbVentasApartados(MySqlcon)
            TextBox2.Text = V.DaNuevoFolio(TextBox11.Text).ToString
            If CInt(TextBox2.Text) < Sf.FolioInicial Then TextBox2.Text = Sf.FolioInicial.ToString
            If CInt(TextBox2.Text) > Sf.FolioFinal Then
                LimitedeFolios = True
                MsgBox("Se ha alcanzado el límite de folios.", MsgBoxStyle.Information, GlobalNombreApp)
            Else
                LimitedeFolios = False
            End If
        End If
    End Sub

    Private Sub CheckScroll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckScroll.CheckedChanged
        If CheckScroll.Checked Then
            My.Settings.ventasscroll = True
        Else
            My.Settings.ventasscroll = False
        End If
    End Sub

   

    Private Sub ComboBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
        If e.KeyCode = Keys.Enter Then
            BotonAgregar()
        End If
    End Sub

    Private Sub Label32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label32.Click

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If MsgBox("¿Surtir este apartado?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Dim Ap As New dbVentasApartados(MySqlcon)
            Ap.SurtirApartado(idApartado, 1)
            Label29.Visible = True
            Button12.Enabled = False
        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim AP As New frmVentasPagosRemisiones(TextBox2.Text, 1, idCliente, "")
        AP.ShowDialog()
        AP.Dispose()
        Dim VAP As New dbVentasApartadosPagos(MySqlcon)
        Label30.Text = "Abonado: $" + Format(VAP.DatotalAbonado(idApartado), "#,##0.00")
    End Sub
End Class