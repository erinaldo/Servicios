Public Class frmNotasdeCreditoCompras
    'Dim IdsVariantes As New elemento
    Dim idNota As Integer
    Dim IVaDefault As Double
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idProveedor As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim IdsTipos As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As String
    Dim IdVariante As Integer
    Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim Tabla As New DataTable
    Dim IdsSucursales As New elemento
    Dim idsFormasDePago As New elemento
    Dim SerieAnt As String
    Dim Cadena As String
    Dim Sello As String
    Dim Isr As Double
    Dim IvaRetenido As Double
    Dim PrecioBase As Double
    Dim iTipoFacturacion As Byte
    Dim Rep As New DibujaReportes()
    Public Sub New(Optional ByVal pidNota As Integer = 0)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idNota = pidNota
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nota de crédito no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim C As New dbNotasdeCreditoCompras(idNota, MySqlcon)
                'C.RegresaInventario(idNota)
                C.Eliminar(idNota)
                e.Cancel = False
            Else
                e.Cancel = True
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
        Tabla.Columns.Add("Id", I.GetType)
        'Tabla.Columns.Add("TipoR", S.GetType)
        'Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        'Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        'Tabla.Columns.Add("Serie", S.GetType)
        Tabla.Columns.Add("Folio", S.GetType)
        Tabla.Columns.Add("Precio", D.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        'LlenaCombos("tblformasdepago", ComboBox4, "nombre", "nombret", "idforma", idsFormasDePago, , , "idforma")
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        LlenaCombos("tblconceptosnotascompras", cmbConcepto, "nombre", "nombret", "idconceptonotacompra", IdsTipos, " tipo=1 and idconceptonotacompra>0")
        Dim op As New dbOpciones(MySqlcon)
        op.RutaXMLEgresos = op.DaRutaXMLCompras
        If op.RutaXMLEgresos <> "" Then OpenFileDialog1.InitialDirectory = op.RutaXMLEgresos
        ConsultaOn = True
        If idNota = 0 Then
            Nuevo()
        Else
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim T As Double
                Dim Iva As Double
                Dim V As New dbNotasdeCreditoCompras(MySqlcon)
                T = V.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)

                Iva = V.TotalIva
                Label12.Text = Format(V.Subtotal, "#,##0.00")
                Label13.Text = Format(V.TotalIva, "#,##0.00")
                Label14.Text = Format(V.TotalNota, "#,##0.00")

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        DateTimePicker1.Value = Date.Now
        iTipoFacturacion = GlobalTipoFacturacion
        TextBox1.Text = ""
        FolioAnt = 0
        idNota = 0
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button14.Enabled = False
        TextBox11.Enabled = True
        TextBox15.Enabled = True
        TextBox14.Text = ""
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'TextBox11.Text = S.Serie
        IVaDefault = S.Impuesto
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        'Dim Sf As New dbSucursalesFolios(MySqlcon)
        'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, 1)
        'TextBox11.Text = Sf.Serie
        'Dim V As New dbNotasDeCredito(MySqlcon)
        'TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasNotasCredito, 0)
        TextBox11.Text = Sf.Serie
        Dim V As New dbNotasdeCreditoCompras(MySqlcon)
        TextBox15.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox15.Text) < Sf.FolioInicial Then TextBox15.Text = Sf.FolioInicial.ToString
        TextBox2.Text = ""
        TextBox16.Text = ""
        If GlobaltpBanxico <> "Error" Then
            TextBox10.Text = GlobaltpBanxico
        Else
            Dim CM As New dbMonedasConversiones(1, MySqlcon)
            TextBox10.Text = CM.Cantidad.ToString
        End If
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        Panel1.Enabled = True
        Panel2.Enabled = True
        SerieAnt = ""
        Button2.Enabled = True
        Label24.Visible = False
        'ComboBox4.SelectedIndex = 0
        NuevoConcepto()
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.CambioSucursal, PermisosN.Secciones.Compras) = False Then
            ComboBox3.Enabled = False
        End If
        TextBox1.Focus()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        BuscaCliente()
    End Sub
    Private Sub BuscaCliente()
        Try
            If ConsultaOn Then
                Dim c As New dbproveedores(MySqlcon)
                If c.BuscaProveedor(TextBox1.Text) Then
                    'If c.DireccionFiscal = 0 Then
                    TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion + " " + c.NoExterior + " " + c.Ciudad + " " + c.CP
                    'Else
                    '   TextBox7.Text = c.Nombre + vbCrLf + c.RFC + vbCrLf + c.Direccion2 + " " + c.NoExterior2 + " " + c.Ciudad2 + " " + c.CP2
                    'End If
                    TextBox13.Text = "Límite: " + Format(c.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + c.DiasCredito.ToString '+ vbCrLf + "Saldo: " + Format(c.Saldo, "#,##0.00")
                    If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Then
                        'If c.Credito > 0 Then
                        '    ComboBox4.SelectedIndex = 1
                        'Else
                        '    ComboBox4.SelectedIndex = 0
                        'End If
                    End If
                    idProveedor = c.ID
                    Isr = 0
                    IvaRetenido = 0
                Else
                    TextBox7.Text = ""
                    TextBox13.Text = ""
                    idProveedor = 0
                    Isr = 0
                    IvaRetenido = 0
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
            Dim C As New dbNotasdeCreditoCompras(MySqlcon)
            If IsNumeric(TextBox15.Text) Then
                If pEstado = Estados.Guardada Then
                    If C.ChecaFolioRepetido(CInt(TextBox15.Text), TextBox11.Text) Then
                        MensajeError += " Folio Repetido"
                    End If
                End If
            Else
                MensajeError += "Debe indicar un folio."
            End If

            'If FolioAnt <> TextBox2.Text Then
            'If C.ChecaFolioRepetido(TextBox2.Text, TextBox11.Text) Then
            'If pEstado = Estados.Guardada Then TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            'End If
            'End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras) = False And pEstado <> Estados.Cancelada Then
                MensajeError = "No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoCancelacion, PermisosN.Secciones.Compras) = False And pEstado = Estados.Cancelada Then
                MensajeError = " No tiene permiso para realizar esta operación."
            End If
            If MensajeError = "" Then
                
                Dim O As New dbOpciones(MySqlcon)
                'Dim FP As New dbFormasdePago(idsFormasDePago.Valor(ComboBox4.SelectedIndex), MySqlcon)
                Dim CM As New dbMonedasConversiones(MySqlcon)
                CM.Modificar(1, CDbl(TextBox10.Text))
                'Dim Credito As Byte
                'Credito = FP.Tipo
                'Dim Sf As New dbSucursalesFolios(MySqlcon)
                'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, 1)
                'Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                C.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                C.Modificar(idNota, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, 0, pEstado, C.Subtotal, C.TotalNota, idProveedor, CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), TextBox14.Text, TextBox11.Text, CInt(TextBox15.Text), TextBox16.Text)
                If pEstado = Estados.Cancelada Then
                    Dim VP As New dbComprasPagos(MySqlcon)
                    VP.CancelarPagosxDocumento(idNota, 1, idProveedor, Estados.Cancelada)
                    '    Dim S As New dbInventarioSeries(MySqlcon)
                    '    S.QuitaSeriesAVenta(idNota)
                    '    If FP.Tipo = 0 And Estado = Estados.Guardada Then
                    '        Dim Cliente As New dbClientes(MySqlcon)
                    '        Cliente.ModificaSaldo(idCliente, C.TotalVenta, 1)
                    '    End If
                    '    C.RegresaInventario(idNota)
                End If
                If pEstado = Estados.Guardada Then
                    'If FP.Tipo = 0 Then
                    '    Dim Cliente As New dbClientes(MySqlcon)
                    '    Cliente.ModificaSaldo(idCliente, C.TotalVenta, 0)
                    'End If
                    'If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    CadenaOriginal()
                    'End If
                End If
                'CadenaOriginal()
                If Estado = Estados.Guardada Or Estados.Cancelada Then
                    GeneraPoliza()
                End If
                Nuevo()
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras) = True Then
                If idProveedor <> 0 Then
                    Dim C As New dbNotasdeCreditoCompras(MySqlcon)
                    If IsNumeric(CInt(TextBox15.Text)) = False Then
                        MsgBox("Debe indicar un folio", MsgBoxStyle.Critical, GlobalNombreApp)
                        Exit Sub
                    End If
                    If C.ChecaFolioRepetido(CInt(TextBox15.Text), TextBox11.Text) Then
                        'TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                        'Label17.Visible = True
                        FolioAnt = ""
                    Else
                        FolioAnt = TextBox2.Text
                    End If
                    'Dim O As New dbOpciones(MySqlcon)
                    Dim CM As New dbMonedasConversiones(MySqlcon)
                    CM.Modificar(1, CDbl(TextBox10.Text))
                    ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(ComboBox1.SelectedIndex))
                    C.DaTotal(idNota, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                    'Dim Sf As New dbSucursalesFolios(MySqlcon)
                    'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.NotadeCredito, 1)
                    'Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                    C.Guardar(idProveedor, Format(DateTimePicker1.Value, "yyyy/MM/dd"), CInt(TextBox2.Text), 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), CDbl(TextBox10.Text), IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdsTipos.Valor(cmbConcepto.SelectedIndex), TextBox11.Text, CInt(TextBox15.Text), TextBox16.Text)
                    idNota = C.ID
                    Estado = 1
                    'Button1.Text = "Modificar"
                    Button2.Enabled = True
                    Button1.Enabled = True
                    Button14.Enabled = True
                    'LlenaDatosDetalles()
                Else
                    MsgBox("Debe indicar un proveedor", MsgBoxStyle.Critical, GlobalNombreApp)
                End If
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    
    
    Private Sub LlenaDatosVenta()
        Dim C As New dbNotasdeCreditoCompras(idNota, MySqlcon)
        ConsultaOn = False
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        ConsultaOn = True
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio
        'iTipoFacturacion = C.EsElectronica
        TextBox1.Text = C.Proveedor.Clave
        Estado = C.Estado
        TextBox8.Text = C.Iva.ToString
        'TextBox11.Text = C.Serie
        TextBox10.Text = C.TipodeCambio.ToString
        TextBox14.Text = C.Comentario
        'If C.Desglosar = 1 Then
        ' CheckBox1.Checked = True
        ' Else
        ' CheckBox1.Checked = False
        'End If
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        'ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdMoneda)
        cmbConcepto.SelectedIndex = IdsTipos.Busca(C.IdConcepto)
        
        TextBox15.Text = C.Folioi.ToString
        TextBox11.Text = C.Serie
        TextBox16.Text = C.FolioCFDI
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
                TextBox11.Enabled = False
                TextBox15.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Button2.Enabled = False
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                Panel1.Enabled = False
                Panel2.Enabled = False
                TextBox11.Enabled = False
                TextBox15.Enabled = False
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
                TextBox11.Enabled = True
                TextBox15.Enabled = True
        End Select
    End Sub
    Private Sub LlenaDatosDetalles()
        Panel1.Visible = True
        ConsultaDetalles()
    End Sub
    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbNotasdeCreditoComprasDetalles(MySqlcon)
            T = CD.ConsultaReader(idNota)
            While T.Read
                'If T("idinventario") > 1 Then
                Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("folioventa"), T("precio"), T("abreviatura"))
                'Else
                'Tabla.Rows.Add(T("iddetalle"), T("cantidad"), T("descripcion"), T("precio"), T("abreviatura"))
                'End If
            End While
            T.Close()
            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            'DGDetalles.Columns(1).Visible = False
            'DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
        TextBox12.Text = "0"
        TextBox4.Text = ""
        TextBox5.Text = "1"
        TextBox9.Text = "0"
        TextBox6.Text = "0"
        TextBox8.Text = IVaDefault.ToString
        PrecioBase = 0
        Button12.Visible = False
        Button9.Enabled = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Agregar Concepto"
        TextBox4.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbNotasdeCreditoComprasDetalles(MySqlcon)
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
            If IsNumeric(TextBox6.Text) = False Then
                MsgError += vbCrLf + "El precio debe ser un valor numérico."
                HayError = True
            Else
                If CInt(TextBox6.Text) <= 0 Then
                    MsgError += " El precio debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If HayError = False Then
                If Button4.Text = "Agregar Concepto" Then
                    CD.Guardar(idNota, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante)
                    'If IdInventario <> 0 Then
                    '    If ManejaSeries <> 0 Then
                    '        If CD.NuevoConcepto Then
                    '            Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CInt(TextBox5.Text))
                    '            F.ShowDialog()
                    '        Else
                    '            Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CD.Cantidad)
                    '            F.ShowDialog()
                    '        End If
                    '    End If
                    '    Dim I As New dbInventario(MySqlcon)
                    '    I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), 0, dbInventario.TipoMovimiento.Baja, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If
                    'If IdVariante <> 0 Then
                    '    Dim PV As New dbProductosVariantes(MySqlcon)
                    '    PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
                    'End If

                    ConsultaDetalles()
                    NuevoConcepto()
                    'PopUp("Artículo agregado", 90)
                Else
                    CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text))
                    'If IdInventario <> 0 Then
                    '    If ManejaSeries <> 0 Then
                    '        Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CDbl(TextBox5.Text))
                    '        F.ShowDialog()
                    '    End If
                    '    Dim I As New dbInventario(MySqlcon)
                    '    I.MovimientoDeInventario(IdInventario, CDbl(TextBox5.Text), CantAnt, dbInventario.TipoMovimiento.CambioBaja, IdAlmacen)
                    'End If
                    'If IdVariante <> 0 Then
                    '    Dim PV As New dbProductosVariantes(MySqlcon)
                    '    PV.ModificaInventario(IdVariante, CantAnt * -1, IdAlmacen)
                    '    PV.ModificaInventario(IdVariante, CDbl(TextBox5.Text), IdAlmacen)
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
        If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras) = True Then
            If Estado = 0 Then
                Guardar()
            End If
            If Estado <> 0 Then
                AgregaArticulo()
            End If
        Else
            MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            TextBox5.Focus()
        End If
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try

            IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbNotasdeCreditoComprasDetalles(IdDetalle, MySqlcon)
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)
            TextBox5.Text = CD.Cantidad.ToString
            TextBox8.Text = CD.Iva.ToString
            TextBox9.Text = CD.Descuento.ToString
            IdInventario = CD.Idinventario
            IdVariante = CD.idVariante
            'If IdInventario > 1 Then
            '    TextBox3.Text = CD.Inventario.Clave
            '    IdVariante = 0
            '    cmbVariante.Visible = False
            'End If
            'If IdVariante > 1 Then
            '    Dim P As New dbProductos(CD.Producto.IdProducto, MySqlcon)
            '    TextBox3.Text = P.Clave
            '    IdInventario = 0
            '    cmbVariante.Visible = True
            'End If

            PrecioU = CD.Precio / CD.Cantidad
            PrecioBase = PrecioU
            TextBox12.Text = PrecioU.ToString
            CantAnt = CD.Cantidad
            'IdAlmacen = CD.IdAlmacen
            TextBox6.Text = CD.Precio.ToString
            TextBox4.Text = CD.Descripcion
            Button4.Text = "Modificar Concepto"
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            'cmbtipoarticulo.Text = "A"



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta nota de crédito no ha sido guardada. ¿Desea iniciar una nueva nota? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim c As New dbNotasdeCreditoCompras(MySqlcon)
                c.Eliminar(idNota)
                Nuevo()
            End If
        Else
            Nuevo()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras) = True Then
                If MsgBox("¿Desea eliminar este concepto de la nota de crédito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbNotasdeCreditoComprasDetalles(MySqlcon)
                    CD.Eliminar(IdDetalle)
                    'Dim S As New dbInventarioSeries(MySqlcon)
                    'S.QuitarSeriesAventaxArticulo(IdInventario, idNota)
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
        Dim B As New frmBuscador(frmBuscador.TipoDeBusqueda.Proveedor, 0, False, False, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            'If B.Cliente.DireccionFiscal = 0 Then
            TextBox7.Text = B.Proveedor.Nombre + vbCrLf + B.Proveedor.RFC + vbCrLf + B.Proveedor.Direccion + " " + B.Proveedor.NoExterior + " " + B.Proveedor.Ciudad + " " + B.Proveedor.CP
            'Else
            '    TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            'End If
            TextBox13.Text = "Límite: " + Format(B.Proveedor.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + B.Proveedor.DiasCredito.ToString '+ vbCrLf + "Saldo: " + Format(B.Cliente.Saldo, "#,##0.00")
            If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Then
                'If B.Cliente.Credito > 0 Then
                '    ComboBox4.SelectedIndex = 1
                'Else
                '    ComboBox4.SelectedIndex = 0
                'End If
            End If
            idProveedor = B.Proveedor.ID
            'Isr = B.Cliente.ISR
            'IvaRetenido = B.Cliente.IvaRetenido
            ConsultaOn = False
            TextBox1.Text = B.Proveedor.Clave
            ConsultaOn = True
            TextBox5.Focus()
        End If
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
        Dim f As New frmNotasdeCreditoComprasConsulta(ModosDeBusqueda.Secundario, "")
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idNota = f.IdVenta
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta nota de crédito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.NotasdeCreditoAlta, PermisosN.Secciones.Compras) = True Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idNota)
                Dim C As New dbNotasdeCreditoCompras(idNota, MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                'C.RegresaInventario(idNota)
                C.Eliminar(idNota)
                PopUp("Factura Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmVentasAsignaSeries(IdInventario, idNota, 0, CDbl(TextBox5.Text))
        F.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MsgBox("¿Cancelar esta nota de crédito?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada)
        End If
    End Sub

    Private Sub CadenaOriginal()

        Dim en As New Encriptador
        Dim V As New dbNotasdeCreditoCompras(idNota, MySqlcon)
        Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)
        Dim CadenaB As String
        Dim CL As New CLetras
        Dim Tabla As New DataTable
        Tabla.Columns.Add("Descripcion")
        Tabla.Columns.Add("Cantidad")
        Tabla.Columns.Add("PU")
        Tabla.Columns.Add("Total")
        ' Tabla.Columns.Add("Codigo")
        Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        Dim VI As New dbNotasdeCreditoComprasDetalles(MySqlcon)
        DR = VI.ConsultaReader(idNota)
        While DR.Read
            Dim dr2 As DataRow

            dr2 = Tabla.NewRow()
            dr2("Descripcion") = DR("descripcion")
            dr2("Cantidad") = Format(DR("cantidad"), "#,##0.00").PadLeft(8)
            dr2("PU") = Format((DR("precio") / DR("cantidad")), "$#,###,##0.00").PadLeft(13)
            dr2("Total") = Format(DR("precio"), "$#,###,##0.00").PadLeft(13)
            ' dr2("Codigo") = Format(DR("clave"))
            Tabla.Rows.Add(dr2)


        End While
        DR.Close()

        PrintDocument1.DocumentName = "NotadeCredito - " + V.Serie + V.Folio.ToString() '+ " - " + V.Proveedor.Nombre.ToString()  'Modificado ESTO SE NECESITA PONER AFUERA
        PrintDocument1.PrinterSettings.PrinterName = Rep.Imprimir("NotasCredito", "NotasCredito - " + V.Folio.ToString() + " - " + V.Proveedor.Nombre.ToString(), Date.Today.ToString("dd-MM-yyyy"))

        V.DaTotal(idNota, V.IdMoneda)
        If V.TotalaPagar >= 0 Then
            CadenaB = CL.LetrasM(V.TotalaPagar, V.IdMoneda, GlobalIdiomaLetras)
        Else
            CadenaB = "MENOS " + CL.LetrasM(V.TotalaPagar * -1, V.IdMoneda, GlobalIdiomaLetras)
        End If
        Rep.Posicion = 0
        Rep.LlenaNodosImpresionNotasCredito(Format(V.Folio, "0000"), Replace(V.Fecha, "/", "-") + " " + V.Hora, Format(V.Subtotal, "$#,###,##0.00"), Format(V.TotalIva, "$#,###,##0.00"), Format(V.TotalaPagar, "$#,###,##0.00"), "00", CadenaB, " ", Sucursal.NombreFiscal, Sucursal.Direccion + " " + Sucursal.NoExterior, Sucursal.Colonia + ", " + Sucursal.Ciudad, Sucursal.CP, Sucursal.RFC, Sucursal.Telefono, Sucursal.Telefono, Sucursal.Email, V.Proveedor.Nombre, V.Proveedor.Direccion, V.Proveedor.Colonia, V.Proveedor.CP, V.Proveedor.Ciudad, V.Proveedor.RFC, Tabla, idNota, V.Proveedor.NoInterior, V.Proveedor.NoExterior, V.Proveedor.Estado, V.Proveedor.Pais, V.Comentario)
        PrintDocument1.Print()




        ''Dim en As New Encriptador
        'Dim V As New dbNotasdeCreditoCompras(idNota, MySqlcon)
        ''TextBox9.Text = 
        ''TextBox10.Text = 
        ''en.GuardaArchivoTexto("XMLFac-" + V.Folio.ToString + ".xml", V.CreaXML(idVenta, IdMonedaG, TextBox10.Text), System.Text.Encoding.UTF8)
        ''Cadena = V.CreaCadenaOriginal(idNota, IdMonedaG)
        ''Sello = en.GeneraSello(Cadena, My.Settings.rutacer, Format(CDate(V.Fecha), "yyyy"))
        ''Dim Enc As New System.Text.UTF8Encoding
        ''Dim Bytes() As Byte = Enc.GetBytes(V.CreaXML(idNota, IdMonedaG, Sello))
        ''Dim Bytes() As Byte = Encoder.GetBytes(pCadenaOriginal)
        ''IO.Directory.CreateDirectory(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        ''IO.Directory.CreateDirectory(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        ''IO.Directory.CreateDirectory(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        ''IO.Directory.CreateDirectory(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        ''en.GuardaArchivo(My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNC-" + V.Serie + V.Folio.ToString + ".xml", Bytes)
        'Dim RutaPDF As String
        'Dim Archivos As New dbSucursalesArchivos
        'RutaPDF = Archivos.DaRutaArchivos(V.IdSucursal, GlobalIdEmpresa, dbSucursalesArchivos.TipoRutas.OtrosPDF, True)
        'IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\")
        'IO.Directory.CreateDirectory(RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\")
        'RutaPDF = RutaPDF + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM")

        'PrintDocument1.DocumentName = "PDFNCC-" + V.Folio
        'Dim obj As New Bullzip.PdfWriter.PdfSettings
        'obj.Init()
        'obj.PrinterName = My.Settings.impresoraPDF
        'obj.WriteSettings()

        'obj.SetValue("Output", RutaPDF + "\<docname>.pdf")
        'obj.SetValue("ShowSettings", "never")
        'obj.SetValue("ShowPDF", "yes")
        'obj.SetValue("ShowSaveAS", "nofile")
        'obj.SetValue("ConfirmOverwrite", "no")
        'obj.SetValue("Target", "printer")
        'obj.WriteSettings()
        'PrintDocument1.PrinterSettings.PrinterName = My.Settings.impresoraPDF
        'PrintDocument1.Print()
        ''Bullzip.PdfWriter.PdfUtil.WaitForFile(My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PDFNCC-"  V.Folio + ".pdf", 1000)
        ''If V.Cliente.Email <> "" Then
        ''    Try
        ''        If MsgBox("¿Enviar nota de crédito por correo electrónico?", MsgBoxStyle.YesNo, NombreApp) = MsgBoxResult.Yes Then
        ''            If V.Cliente.Email <> "" Then
        ''                Dim M As New MailManager(My.Settings.emailhost, My.Settings.emailfrom, My.Settings.emailusuario, My.Settings.emailpassword, My.Settings.emailpuerto)
        ''                Dim O As New dbOpciones(MySqlcon)
        ''                Dim C As String
        ''                C = "Eviado por: " + O._NombreEmpresa + vbNewLine + "RFC: " + O._RFC + vbNewLine + "NOTA DE CRÉDITO" + vbNewLine + "Folio: " + V.Serie + V.Folio.ToString + vbNewLine + "Comprobante fiscal digital enviado por medio del sistema de facturación de Pull System Soft, S.A. de C.V." + vbNewLine + "http://pullsystemsoft.com" + vbNewLine + "Este es un mensaje enviado automáticamente, no responda a este mensaje a la dirección de la que se envió."
        ''                M.send("Comprobante fiscal digital Nota de crédito " + V.Serie + V.Folio.ToString, C, V.Cliente.Email, V.Cliente.Nombre, My.Settings.rutapdfnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\PDFNC" + V.Serie + V.Folio.ToString + ".pdf", My.Settings.rutaxmlnc + "\" + Format(CDate(V.Fecha), "yyyy") + "\" + Format(CDate(V.Fecha), "MM") + "\XMLNC-" + V.Serie + V.Folio.ToString + ".xml")
        ''            End If
        ''        End If
        ''    Catch ex As Exception
        ''        MsgBox("No puedo enviar el correo, verifique la configuración de correo o el correo del cliente." + vbCrLf + ex.Message, MsgBoxStyle.Critical, NombreApp)
        ''    End Try
        ''End If
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        'LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        'Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        'IVaDefault = S.Impuesto
        If ConsultaOn Then
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasNotasCredito, 0)
            TextBox11.Text = Sf.Serie
            Dim V As New dbNotasdeCreditoCompras(MySqlcon)
            TextBox15.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(TextBox15.Text) < Sf.FolioInicial Then TextBox15.Text = Sf.FolioInicial.ToString
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        'If IdInventario <> 0 Or IdVariante <> 0 Then
        If IsNumeric(TextBox5.Text) Then
            TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
        End If
        'End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        'Try
        '    Dim Forma As New frmBuscaDocumentoVenta(1, True)
        '    If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '        Dim V As New dbVentas(Forma.Id, MySqlcon)
        '        If Estado = 0 Then
        '            '0 cotizacion
        '            '1 pedido
        '            '2 remision
        '            '3 ventas
        '            Select Case Forma.Tipo
        '                Case 0
        '                    Dim Co As New dbVentasCotizaciones(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Co.Cliente.Clave
        '                Case 1
        '                    Dim Cp As New dbVentasPedidos(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Cp.Cliente.Clave
        '                Case 2
        '                    Dim Cr As New dbVentasRemisiones(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Cr.Cliente.Clave
        '                Case 3
        '                    Dim Cv As New dbVentas(Forma.Id, MySqlcon)
        '                    TextBox1.Text = Cv.Cliente.Clave
        '            End Select

        '            Guardar()
        '            If Estado <> 0 Then
        '                V.AgregarDetallesReferencia(idNota, Forma.Id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
        '                ConsultaDetalles()
        '            End If
        '        Else
        '            V.AgregarDetallesReferencia(idNota, Forma.Id, Forma.Tipo, IdsAlmacenes.Valor(ComboBox8.SelectedIndex))
        '            ConsultaDetalles()
        '        End If
        '        NuevoConcepto()
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        'End Try
    End Sub
    'Version Normal
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        Rep.DibujaPaginaN(e.Graphics)
        If Rep.MasPaginas = True Or Rep.NumeroPagina > 2 Then
            e.Graphics.DrawString("Página: " + Format(Rep.NumeroPagina - 1, "00"), New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point), Brushes.Black, 190, 272)
        End If

        e.HasMorePages = Rep.MasPaginas
        ''Dim O As New dbOpciones(MySqlcon)
        ''Dim en As New Encriptador
        ''Dim XMLDoc As String
        'Dim V As New dbNotasdeCreditoCompras(idNota, MySqlcon)
        'Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
        'Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
        'Dim Pluma As New Pen(Color.Black, 0.2)
        'Dim Sucursal As New dbSucursales(V.IdSucursal, MySqlcon)

        ''en.Leex509(My.Settings.rutacer)

        'V.DaTotal(idNota, V.IdMoneda)
        ''CI = TI * (V.Iva / 100) 'Pendiente

        ''Try
        ''    e.Graphics.DrawImage(Image.FromFile(My.Settings.rutancfondo), 20, 1, 810, 1100)
        ''Catch ex As Exception

        ''End Try

        'e.Graphics.PageUnit = GraphicsUnit.Millimeter

        ''e.Graphics.DrawLine(Pluma, 10, 10, 10, 30)
        ''e.Graphics.DrawLine(Pluma, 10, 30, 100, 30)
        'Dim XEncabezado As Integer = 5
        'e.Graphics.DrawString(Sucursal.NombreFiscal, FuenteArialC, Brushes.Black, XEncabezado, 4)
        'e.Graphics.DrawString(Sucursal.Direccion + " " + Sucursal.NoExterior + " " + Sucursal.NoInterior, FuenteArialC, Brushes.Black, XEncabezado, 8)
        'e.Graphics.DrawString("Col: " + Sucursal.Colonia + " " + Sucursal.Ciudad + " " + Sucursal.Estado, FuenteArialC, Brushes.Black, XEncabezado, 12)
        'e.Graphics.DrawString("CP: " + Sucursal.CP + " RFC:" + Sucursal.RFC, FuenteArialC, Brushes.Black, XEncabezado, 16)
        'e.Graphics.DrawString(If(Sucursal.Telefono <> "", "Tel: " + Sucursal.Telefono, "") + If(Sucursal.Email <> "", " E-mail:" + Sucursal.Email, ""), FuenteArialC, Brushes.Black, XEncabezado, 20)
        'e.Graphics.DrawString(Sucursal.ReferenciaDomicilio, FuenteArialC, Brushes.Black, XEncabezado, 24)


        ''e.Graphics.DrawLine(Pluma, 10, 55, 100, 55)
        ''e.Graphics.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)

        'Dim strF As New StringFormat
        'strF.Alignment = StringAlignment.Near
        'strF.LineAlignment = StringAlignment.Near
        'Dim Rec As RectangleF
        'Rec = New RectangleF(5, 40, 100, 100)
        ''e.Graphics.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)
        'e.Graphics.DrawString("Datos del Proveedor:", FuenteArialC, Brushes.Black, 5, 30)
        ''e.Graphics.DrawString("Nombre: " + V.Cliente.Nombre, FuenteArialC, Brushes.Black, 5, 34)
        ''If V.Cliente.DireccionFiscal = 0 Then
        'e.Graphics.DrawString("Dom: " + V.Proveedor.Direccion + " " + V.Proveedor.NoExterior + " " + V.Proveedor.NoInterior, FuenteArialC, Brushes.Black, 5, 38)
        'e.Graphics.DrawString("Col: " + V.Proveedor.Colonia + " C.P:" + V.Proveedor.CP, FuenteArialC, Brushes.Black, 5, 42)
        'e.Graphics.DrawString("Ciudad: " + V.Proveedor.Ciudad + " " + V.Proveedor.Estado, FuenteArialC, Brushes.Black, 5, 46)
        ''Else
        ''e.Graphics.DrawString("Dom: " + V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteArialC, Brushes.Black, 5, 38)
        ''e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteArialC, Brushes.Black, 5, 42)
        ''e.Graphics.DrawString("Ciudad: " + V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteArialC, Brushes.Black, 5, 46)
        ''End If
        'e.Graphics.DrawString("RFC: " + V.Proveedor.RFC, FuenteArialC, Brushes.Black, 5, 50)
        ''Dim SF As New dbSucursalesFolios(MySqlcon)
        ''SF.BuscaFolios(V.IdSucursal, dbSucursalesFolios.TipoDocumentos.NotadeCredito, 1)
        'e.Graphics.DrawString("NOTA DE CRÉDITO COMPRAS", Fuente, Brushes.Black, 155, 4)
        'e.Graphics.DrawString("Folio:", Fuente, Brushes.Black, 155, 8)
        'e.Graphics.DrawString(V.Folio, Fuente, Brushes.Black, 155, 12)
        ''e.Graphics.DrawString("No. y año de aprobación:", Fuente, Brushes.Black, 155, 16)
        ''e.Graphics.DrawString(V.NoAprobacion + " " + V.YearAprobacion, Fuente, Brushes.Black, 155, 20)
        ''e.Graphics.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
        ''e.Graphics.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
        ''e.Graphics.DrawString("No. Certificado: ", Fuente, Brushes.Black, 155, 24)
        ''e.Graphics.DrawString(V.NoCertificado, Fuente, Brushes.Black, 155, 28)
        'e.Graphics.DrawString("Fecha:", Fuente, Brushes.Black, 155, 32)
        'e.Graphics.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 155, 36)
        ''e.Graphics.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)
        'e.Graphics.DrawString("CONCEPTOS", FuenteB, Brushes.Black, 10, 56)
        'e.Graphics.DrawLine(Pluma, 5, 61, 210, 61)

        ''e.Graphics.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
        ''e.Graphics.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
        ''e.Graphics.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
        ''e.Graphics.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
        ''e.Graphics.DrawString("Importe", Fuente, Brushes.Black, 170, 60)


        'Dim DR As MySql.Data.MySqlClient.MySqlDataReader
        'Dim VI As New dbNotasdeCreditoComprasDetalles(MySqlcon)
        'DR = VI.ConsultaReader(idNota)
        'Dim CadenaB As String
        'Dim Y As Integer = 64
        'Dim YB As Integer
        'While DR.Read
        '    'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        '    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

        '    YB = Y
        '    'Dim Venta As New dbVentas(DR("idventa"), MySqlcon)
        '    CadenaB = ""
        '    Select Case DR("idinventario")
        '        Case 0
        '            CadenaB = CStr(DR("descripcion")) + " | Compra: " + DR("folioventa").ToString
        '        Case 1
        '            CadenaB = CStr(DR("descripcion")) + " | Nota de Cargo: " + DR("folioventa").ToString
        '        Case 2
        '            CadenaB = CStr(DR("descripcion")) + " | Saldo Inicial: " + DR("folioventa").ToString
        '        Case 3
        '            CadenaB = CStr(DR("descripcion")) + " | Documento: " + DR("folioventa").ToString
        '    End Select
        '    Y = InsertaEnters(CadenaB, 60, Y, 4)
        '    Rec = New RectangleF(5, YB, 160, 200)
        '    Select Case DR("idinventario")
        '        Case 0
        '            e.Graphics.DrawString(DR("descripcion").ToString + " | Compra: " + DR("folioventa").ToString, Fuente, Brushes.Black, Rec, strF)
        '        Case 1
        '            e.Graphics.DrawString(DR("descripcion").ToString + " | Nota de Cargo: " + DR("folioventa").ToString, Fuente, Brushes.Black, Rec, strF)
        '        Case 2
        '            e.Graphics.DrawString(DR("descripcion").ToString + " | Saldo Inicial: " + DR("folioventa").ToString, Fuente, Brushes.Black, Rec, strF)
        '        Case 3
        '            e.Graphics.DrawString(DR("descripcion").ToString + " | Documento: " + DR("folioventa").ToString, Fuente, Brushes.Black, Rec, strF)
        '    End Select
        '    'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        '    'e.Graphics.DrawString(, Fuente, Brushes.Black, 130, YB)
        '    'e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 119, YB)
        '    'e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        '    e.Graphics.DrawString(Format(DR("precio") / (1 + (DR("iva") / 100)), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        '    Y += 6
        'End While
        'DR.Close()

        ''Dim VP As New dbVentasProductos(MySqlcon)
        ''DR = VP.ConsultaReader(idVenta)
        ''While DR.Read
        ''    e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        ''    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        ''    YB = Y
        ''    CadenaB = DR("descripcion")
        ''    Y = InsertaEnters(CadenaB, 34, Y, 4)
        ''    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        ''    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        ''    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        ''    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        ''    Y += 6
        ''End While
        ''DR.Close()


        ''Dim VS As New dbVentasServicios(MySqlcon)
        ''DR = VS.ConsultaReader(idVenta)
        ''While DR.Read
        ''    'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
        ''    'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)
        ''    YB = Y
        ''    CadenaB = DR("descripcion")
        ''    Y = InsertaEnters(CadenaB, 34, Y, 4)
        ''    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 43, YB)
        ''    e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 123, YB)
        ''    e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 148, YB)
        ''    e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
        ''    Y += 6
        ''End While
        ''DR.Close()
        'e.Graphics.DrawLine(Pluma, 5, Y, 210, Y)
        'Y += 4
        'e.Graphics.DrawString("Sub total:", FuenteC, Brushes.Black, 153, Y)
        'e.Graphics.DrawString(Format(V.Subtotal - V.TotalIva, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        ''If V.IdMoneda <> 2 Then
        ''e.Graphics.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
        ''End If
        ''“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
        'Dim Ivas As New Collection
        'Dim IvasImporte As New Collection
        'DR = V.DaIvas(idNota)
        'Dim IAnt As Double
        'While DR.Read
        '    If Ivas.Contains(DR("iva").ToString) = False Then
        '        Ivas.Add(DR("iva"), DR("iva").ToString)
        '    End If
        '    If IvasImporte.Contains(DR("iva").ToString) = False Then
        '        IvasImporte.Add(DR("precio") - (DR("precio") / (1 + DR("iva") / 100)), DR("iva").ToString)
        '    Else
        '        IAnt = IvasImporte(DR("iva").ToString)
        '        IvasImporte.Remove(DR("iva").ToString)
        '        IvasImporte.Add(IAnt + (DR("precio") - (DR("precio") / (1 + DR("iva") / 100))), DR("iva").ToString)
        '    End If
        'End While
        'DR.Close()
        'Y += 4
        'For Each I As Double In Ivas
        '    'If I <> 0 Then
        '    e.Graphics.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.Black, 153, Y)
        '    e.Graphics.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        '    Y += 4
        '    'End If
        'Next
        ''If V.ISR <> 0 Then
        ''    e.Graphics.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
        ''    e.Graphics.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        ''    Y += 4
        ''End If
        ''If V.IvaRetenido <> 0 Then
        ''    e.Graphics.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
        ''    e.Graphics.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
        ''    Y += 4
        ''End If
        'e.Graphics.DrawString("Total: ", Fuente, Brushes.Black, 153, Y)
        'e.Graphics.DrawString(Format(V.TotalNota, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

        'Dim f As New StringFunctions

        ''        Y = 175

        ''If V.IdMoneda = 2 Then
        ''    CadenaB = f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda)
        ''Else
        ''    CadenaB = f.PASELETRAS(System.Math.Round(V.TotalNota, 2), V.IdMoneda)
        ''End If
        'Dim CL As New CLetras
        'CadenaB = CL.LetrasM(V.TotalNota, V.IdMoneda, GlobalIdiomaLetras)
        'YB = Y
        'Y = InsertaEnters(CadenaB, 63, Y, 4)
        'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 10, YB)



        ''Y += 10
        ''e.Graphics.DrawString("Cadena Original:", FuenteC, Brushes.Black, 10, Y)
        ''Y += 4
        ''YB = Y
        ''CadenaB = Cadena
        ''Y = InsertaEnters(CadenaB, 105, Y, 4)
        ''e.Graphics.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

        ' ''e.Graphics.DrawString("Sello:", Fuente, Brushes.Black, 10, Y + 6)

        ''Dim SelloB As String
        ''Y += 10
        ''e.Graphics.DrawString("Sello Digital:", FuenteC, Brushes.Black, 10, Y)
        ''Y += 4
        ''SelloB = Sello
        ''YB = Y
        ''Y = InsertaEnters(SelloB, 105, Y, 4)

        ''e.Graphics.DrawString(SelloB, FuenteC, Brushes.Black, 10, YB)
        ''e.Graphics.DrawString("Pago en una sola exhibición", Fuente, Brushes.Black, 10, Y + 10)
        ''e.Graphics.DrawString("Este documento es una representación impresa de un CFD", Fuente, Brushes.Black, 10, Y + 16)

    End Sub
    '---------------Termina Version Normal
    '--------------------------------------------------------------------
    'Version Daniel
    'Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage



    '    Dim O As New dbOpciones(MySqlcon)
    '    Dim en As New Encriptador
    '    'Dim XMLDoc As String
    '    Dim V As New dbVentas(idVenta, MySqlcon)
    '    Dim Fuente As New Font("Lucida Console", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteB As New Font("Lucida Console", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteC As New Font("Lucida Console", 8, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArial As New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialB As New Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim FuenteArialC As New Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point)
    '    Dim Pluma As New Pen(Color.Black, 0.5)
    '    'Dim Fondo As New Image
    '    'XMLDoc = "<?xml version=""1.0"" encoding=""UTF-8""?>" + vbCrLf
    '    ' XMLDoc += "<Comprobante " + vbCrLf
    '    en.Leex509(My.Settings.rutacer)
    '    'Dim TI As Double
    '    'Dim CI As Double
    '    V.DaTotal(idVenta, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
    '    'CI = TI * (V.Iva / 100) 'Pendiente

    '    Try
    '        e.Graphics.DrawImage(Image.FromFile(My.Settings.rutafacturafondo), 20, 1, 810, 1100)
    '    Catch ex As Exception

    '    End Try

    '    e.Graphics.PageUnit = GraphicsUnit.Millimeter

    '    'e.Graphics.DrawLine(Pluma, 10, 10, 10, 30)
    '    'e.Graphics.DrawLine(Pluma, 10, 30, 100, 30)
    '    Dim XEncabezado As Integer = 65
    '    e.Graphics.DrawString(O._NombreEmpresa, FuenteArialB, Brushes.Black, XEncabezado, 4)
    '    e.Graphics.DrawString(O._Calle + " " + O._noExterior + " " + O._noInterior, FuenteArial, Brushes.Black, XEncabezado, 10)
    '    e.Graphics.DrawString("Col: " + O._Colonia + " " + O._Localidad + " " + O._Estado, FuenteArial, Brushes.Black, XEncabezado, 15)
    '    e.Graphics.DrawString("CP: " + O._CodigoPostal + " RFC:" + O._RFC, FuenteArial, Brushes.Black, XEncabezado, 20)
    '    e.Graphics.DrawString(O._ReferenciaDomicilio, FuenteArial, Brushes.Black, XEncabezado, 24)

    '    'e.Graphics.DrawLine(Pluma, 10, 34, 10, 55)
    '    'e.Graphics.DrawLine(Pluma, 10, 55, 100, 55)
    '    'e.Graphics.DrawString("Receptor:", Fuente, Brushes.Black, 12, 34)


    '    'Dim Nenter As String = V.Cliente.Nombre
    '    'InsertaEnters(Nenter, 60, 0, 0)
    '    'e.Graphics.DrawString(Nenter, FuenteC, Brushes.Black, 12, 40)

    '    Dim strF As New StringFormat
    '    strF.Alignment = StringAlignment.Near
    '    strF.LineAlignment = StringAlignment.Near
    '    Dim Rec As RectangleF
    '    Rec = New RectangleF(12, 40, 100, 100)
    '    e.Graphics.DrawString(V.Cliente.Nombre, FuenteC, Brushes.Black, Rec, strF)

    '    If V.Cliente.DireccionFiscal = 0 Then
    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, FuenteC, Brushes.Black, 12, 54)
    '    Else
    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, FuenteC, Brushes.Black, 12, 46)
    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, FuenteC, Brushes.Black, 12, 50)
    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, FuenteC, Brushes.Black, 12, 54)
    '    End If
    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)






    '    '    e.Graphics.DrawString(V.Cliente.Nombre, Fuente, Brushes.Black, 12, 42)
    '    '    If V.Cliente.DireccionFiscal = 0 Then
    '    '        e.Graphics.DrawString(V.Cliente.Direccion + " " + V.Cliente.NoExterior + " " + V.Cliente.NoInterior, Fuente, Brushes.Black, 12, 46)
    '    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia + " C.P:" + V.Cliente.CP, Fuente, Brushes.Black, 12, 50)
    '    '        e.Graphics.DrawString(V.Cliente.Ciudad + " " + V.Cliente.Estado, Fuente, Brushes.Black, 12, 54)
    '    '    Else
    '    '        e.Graphics.DrawString(V.Cliente.Direccion2 + " " + V.Cliente.NoExterior2 + " " + V.Cliente.NoInterior2, Fuente, Brushes.Black, 12, 46)
    '    '        e.Graphics.DrawString("Col: " + V.Cliente.Colonia2 + " C.P:" + V.Cliente.CP2, Fuente, Brushes.Black, 12, 50)
    '    '        e.Graphics.DrawString(V.Cliente.Ciudad2 + " " + V.Cliente.Estado2, Fuente, Brushes.Black, 12, 54)
    '    '    End If
    '    '    e.Graphics.DrawString(V.Cliente.RFC, Fuente, Brushes.Black, 90, 54)


    '    e.Graphics.DrawString(V.Serie + V.Folio.ToString, Fuente, Brushes.Black, 163, 28)
    '    'e.Graphics.DrawString("No. Aprobación:", Fuente, Brushes.Black, 130, 16)
    '    e.Graphics.DrawString(O._noAprobacion + " " + O._yearAprobacion, Fuente, Brushes.Black, 163, 37)
    '    'e.Graphics.DrawString("Año Aprobación:  ", Fuente, Brushes.Black, 130, 24)
    '    'e.Graphics.DrawString(O._yearAprobacion, Fuente, Brushes.Black, 130, 28)
    '    'e.Graphics.DrawString("No. Certificado: ", Fuente, Brushes.Black, 130, 32)
    '    e.Graphics.DrawString(en.Seriex509, Fuente, Brushes.Black, 163, 44)
    '    'e.Graphics.DrawString("Fecha:", Fuente, Brushes.Black, 155, 36)
    '    e.Graphics.DrawString(Replace(V.Fecha, "/", "-") + " " + V.Hora, Fuente, Brushes.Black, 163, 52)
    '    'e.Graphics.DrawString(V.Hora, FuenteC, Brushes.Black, 187, 32)


    '    'e.Graphics.DrawString("Cant.", Fuente, Brushes.Black, 12, 60)
    '    'e.Graphics.DrawString("Unidad", Fuente, Brushes.Black, 25, 60)
    '    'e.Graphics.DrawString("Descripción", Fuente, Brushes.Black, 42, 60)
    '    'e.Graphics.DrawString("P. Unitario", Fuente, Brushes.Black, 130, 60)
    '    'e.Graphics.DrawString("Importe", Fuente, Brushes.Black, 170, 60)
    '    Dim DR As MySql.Data.MySqlClient.MySqlDataReader
    '    Dim VI As New dbVentasInventario(MySqlcon)
    '    DR = VI.ConsultaReader(idVenta)
    '    Dim CadenaB As String
    '    Dim Y As Integer = 70
    '    Dim YB As Integer
    '    While DR.Read
    '        'e.Graphics.DrawString(DR("clave"), Fuente, Brushes.Black, 10, Y)
    '        'e.Graphics.DrawString(DR("tipocantidad"), Fuente, Brushes.Black, 30, Y)

    '        YB = Y
    '        CadenaB = DR("descripcion")
    '        Y = InsertaEnters(CadenaB, 50, Y, 4)
    '        Rec = New RectangleF(32, YB, 120, 200)
    '        e.Graphics.DrawString(DR("descripcion"), Fuente, Brushes.Black, Rec, strF)
    '        'e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 32, YB)
    '        e.Graphics.DrawString(Format(DR("cantidad"), "#,##0.00").PadLeft(8), Fuente, Brushes.Black, 10, YB)
    '        e.Graphics.DrawString(Format(DR("precio") / DR("cantidad"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 146, YB)
    '        e.Graphics.DrawString(Format(DR("precio"), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, YB)
    '        Y += 6
    '    End While
    '    DR.Close()

    '    Y = 170
    '    e.Graphics.DrawString("Sub total:", FuenteC, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.Subtototal, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '    If V.IdConversion <> 2 Then
    '        e.Graphics.DrawString("El importe de la presente factura deberá ser pagada de acuerdo a la cotización del dólar " + vbCrLf + "frente al peso vigente al día de su pago.", FuenteArialC, Brushes.Black, 8, 158)
    '    End If
    '    '“El importe de la presente factura deberá ser pagada de acuerdo A la cotización del dólar frente al peso vigente al día de su pago”
    '    Dim Ivas As New Collection
    '    Dim IvasImporte As New Collection
    '    DR = V.DaIvas(idVenta)
    '    Dim IAnt As Double
    '    While DR.Read
    '        If Ivas.Contains(DR("iva").ToString) = False Then
    '            Ivas.Add(DR("iva"), DR("iva").ToString)
    '        End If
    '        If IvasImporte.Contains(DR("iva").ToString) = False Then
    '            IvasImporte.Add(DR("precio") * (DR("iva") / 100), DR("iva").ToString)
    '        Else
    '            IAnt = IvasImporte(DR("iva").ToString)
    '            IvasImporte.Remove(DR("iva").ToString)
    '            IvasImporte.Add(IAnt + (DR("precio") * (DR("iva") / 100)), DR("iva").ToString)
    '        End If
    '    End While
    '    DR.Close()
    '    Y += 4
    '    For Each I As Double In Ivas
    '        'If I <> 0 Then
    '        e.Graphics.DrawString("Iva " + Format(I, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(IvasImporte(I.ToString), "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '        'End If
    '    Next
    '    If V.ISR <> 0 Then
    '        e.Graphics.DrawString("ISR " + Format(V.ISR, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalISR, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    If V.IvaRetenido <> 0 Then
    '        e.Graphics.DrawString("IVARet. " + Format(V.IvaRetenido, "#0.00") + "%:", FuenteC, Brushes.White, 153, Y)
    '        e.Graphics.DrawString(Format(V.TotalIvaRetenido, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)
    '        Y += 4
    '    End If
    '    e.Graphics.DrawString("Total: ", Fuente, Brushes.White, 153, Y)
    '    e.Graphics.DrawString(Format(V.TotalVenta, "$#,###,##0.00").PadLeft(13), Fuente, Brushes.Black, 178, Y)

    '    Dim f As New PaseLetras

    '    Y = 175

    '    If V.IdConversion = 2 Then
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.MN) + " M.N."
    '    Else
    '        CadenaB = f.PASELETRAS(System.Math.Round(V.TotalVenta, 2), MONEDAS.USD) + " U.S.D"
    '    End If

    '    YB = Y
    '    Y = InsertaEnters(CadenaB, 63, Y, 4)
    '    e.Graphics.DrawString(CadenaB, Fuente, Brushes.Black, 10, 175)

    '    Y = 198
    '    YB = Y
    '    CadenaB = Cadena
    '    Y = InsertaEnters(CadenaB, 105, Y, 4)
    '    e.Graphics.DrawString(CadenaB, FuenteC, Brushes.Black, 10, YB)

    '    Dim SelloB As String
    '    Y = 238
    '    SelloB = Sello
    '    YB = Y
    '    Y = InsertaEnters(SelloB, 105, Y, 4)

    '    e.Graphics.DrawString(SelloB, FuenteC, Brushes.Black, 10, Y)

    'End Sub
    '----------------Termina Version DAniel---------------------------------------------------------------
    '-------------------------------------------------------------------------------------------------------

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        'If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        Dim op As New dbOpciones(MySqlcon)
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        CadenaOriginal()
        'PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
        'PrintDocument1.Print()
        'End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        'If ConsultaOn Then
        '    If IsNumeric(TextBox6.Text) And IsNumeric(TextBox10.Text) Then
        '        If IDsMonedas.Valor(ComboBox1.SelectedIndex) = 2 Then
        '            TextBox12.Text = CStr(PrecioU * CDbl(TextBox10.Text))
        '            'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
        '        Else
        '            TextBox12.Text = CStr(PrecioU / CDbl(TextBox10.Text))
        '            'TextBox6.Text = CStr(CDbl(TextBox6.Text) / CDbl(TextBox10.Text))
        '        End If
        '    End If
        'End If
    End Sub
    Private Function InsertaEnters(ByRef Cadena As String, ByVal CadaCuantos As Integer, ByVal Y As Integer, ByVal AumentoY As Integer) As Integer
        Dim C As Integer
        Dim SigLimite As Integer
        C = 0
        SigLimite = CadaCuantos
        While C < Cadena.Length
            If C >= SigLimite Then
                Cadena = Cadena.Insert(SigLimite, vbCrLf)
                SigLimite += CadaCuantos
                Y += AumentoY
            End If
            C += CadaCuantos
        End While
        Return Y
    End Function

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        'If IdInventario <> 0 Or IdVariante <> 0 Then
        If IsNumeric(TextBox12.Text) Then
            PrecioU = CDbl(TextBox12.Text)
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
        'End If
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        If ConsultaOn Then
            If IsNumeric(TextBox12.Text) And IsNumeric(TextBox9.Text) Then
                If PrecioBase <> 0 Then
                    TextBox12.Text = CStr(PrecioBase - (PrecioBase * CDbl(TextBox9.Text) / 100))
                Else
                    TextBox12.Text = CStr(PrecioU - (PrecioU * CDbl(TextBox9.Text) / 100))
                End If
                'TextBox6.Text = CStr(CDbl(TextBox6.Text) * CDbl(TextBox10.Text))
            End If
        End If
    End Sub


    Private Sub DGDetalles_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellContentClick

    End Sub

    Private Sub DateTimePicker1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox9.KeyDown, TextBox8.KeyDown, TextBox7.KeyDown, TextBox6.KeyDown, TextBox5.KeyDown, TextBox4.KeyDown, TextBox2.KeyDown, TextBox13.KeyDown, TextBox12.KeyDown, TextBox11.KeyDown, TextBox10.KeyDown, TextBox1.KeyDown, DateTimePicker1.KeyDown, ComboBox8.KeyDown, ComboBox4.KeyDown, ComboBox3.KeyDown, ComboBox2.KeyDown, ComboBox1.KeyDown, cmbConcepto.KeyDown, Button9.KeyDown, Button8.KeyDown, Button7.KeyDown, Button5.KeyDown, Button4.KeyDown, Button3.KeyDown, Button2.KeyDown, Button15.KeyDown, Button14.KeyDown, Button13.KeyDown, Button12.KeyDown, Button11.KeyDown, Button10.KeyDown, Button1.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send("{Tab}")
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim et As New frmVentasTextoExtra(TextBox14.Text, 1000, False)
        If et.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox14.Text = et.Texto
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbNotasdeCargoCompras(MySqlcon)
                V.ActualizaComentario(idNota, TextBox14.Text)
                PopUp("Guardado", 90)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(OpenFileDialog1.FileName)
                TextBox16.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, GlobalNombreApp)
            End Try
        End If
    End Sub
    Private Sub GeneraPoliza()
        Try
            Dim Op As New dbOpciones(MySqlcon)
            If Op.IntegrarContabilidad = 1 And GlobalconIntegracion Then
                Dim M As New dbContabilidadMascaras(MySqlcon)
                Dim V As New dbNotasdeCreditoCompras(idNota, MySqlcon)
                Dim Canceladas As Byte = 0
                Dim credito As Byte = 2
                Dim cuantas As Integer
                If V.Estado = Estados.Cancelada Then
                    Canceladas = 1
                End If
                'Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
                'If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                '    credito = 0
                'Else
                '    credito = 1
                'End If
                cuantas = M.CuantasHay(7, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(7, Canceladas, credito)
                    Else
                        Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 7)
                        f.ShowDialog()
                        If f.DialogResult = Windows.Forms.DialogResult.OK Then
                            M.ID = f.IdMascara
                        Else
                            Exit Sub
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    If Canceladas = 0 Then
                        GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    Else
                        GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    End If
                    GP.GeneraPolizaGeneral(V.ID, V.IdProveedor, 1, 0, 0, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    Else
                        PopUp("Póliza generada.", 90)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
End Class