Public Class frmDevolucionesCompras
    Dim IdsVariantes As New elemento
    Dim idDevolucion As Integer
    Dim IDsMonedas As New elemento
    Dim IDsMonedas2 As New elemento
    Dim idProveedor As Integer
    Dim IdInventario As Integer
    Dim IdDetalle As Integer
    Dim IdsAlmacenes As New elemento
    Dim CantAnt As Double
    Dim ConsultaOn As Boolean = False
    Dim ManejaSeries As Byte
    Dim IdAlmacen As Integer
    Dim Estado As Byte
    Dim FolioAnt As String
    Dim IdVariante As Integer
    Dim IdServicio As Integer
    Dim PrecioU As Double
    Dim IdForma As Integer
    Dim Tabla As New DataTable
    Dim IdsSucursales As New elemento
    Dim idsFormasDePago As New elemento
    Dim SerieAnt As String
    Dim Cadena As String
    Dim Sello As String
    Dim Isr As Double
    Dim IvaRetenido As Double
    Dim PrecioBase As Double
    Dim IdCompra As Integer
    Dim IdRemision As Integer
    Dim Modo As Integer
    Dim strClienteClave As String
    Dim IdDocumento As Integer
    Dim Tipo As Byte
    Dim TipoDeCambio As Double
    Dim EsdeCredito As Boolean
    Dim Credito As Double
    Dim ImpDoc As ImprimirDocumento
    Dim Op As dbOpciones
    Dim cadenaIDS As String = ""
    Dim PorLotes As Byte
    Dim Aduana As Byte
    Dim AlmacenStr As String
    Public Sub New(ByVal pidDevolucion As Integer, ByVal pidDocumento As Integer, ByVal pModo As Integer, ByVal pClienteClave As String, ByVal pTipo As Byte)

        ' This call is required by the Windows Form Designer.

        InitializeComponent()
        idDevolucion = pidDevolucion
        Modo = pModo
        strClienteClave = pClienteClave
        IdDocumento = pidDocumento
        Tipo = pTipo
        If Tipo = 2 Then
            IdRemision = pidDocumento
            IdCompra = 0
        Else
            IdRemision = 0
            IdCompra = pidDocumento
        End If
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmVentasN_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta devolución no ha sido guardada. ¿Cerrar la ventana de todas maneras? Los datos se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idDevolucion)
                Dim C As New dbDevoluciones(idDevolucion, MySqlcon)
                C.RegresaInventario(idDevolucion)
                C.Eliminar(idDevolucion)
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
        Tabla.Columns.Add("TipoR", S.GetType)
        Tabla.Columns.Add("Extra", S.GetType)
        Tabla.Columns.Add("Cantidad", D.GetType)
        Tabla.Columns.Add("Código", S.GetType)
        Tabla.Columns.Add("Descripción", S.GetType)
        Tabla.Columns.Add("Precio U.", S.GetType)
        Tabla.Columns.Add("Importe", S.GetType)
        Tabla.Columns.Add("Moneda", S.GetType)
        LlenaCombos("tblsucursales", ComboBox3, "nombre", "nombret", "idsucursal", IdsSucursales)

        'LlenaCombos("tblformasdepago", ComboBox4, "nombre", "nombret", "idforma", idsFormasDePago, , , "idforma")
        LlenaCombos("tblmonedas", ComboBox1, "nombre", "nombrem", "idmoneda", IDsMonedas, "idmoneda>1")
        LlenaCombos("tblmonedas", ComboBox2, "nombre", "nombrem", "idmoneda", IDsMonedas2, "idmoneda>1")
        Op = New dbOpciones(MySqlcon)
        ImpDoc = New ImprimirDocumento
        op.RutaXMLEgresos = op.DaRutaXMLCompras
        If op.RutaXMLEgresos <> "" Then OpenFileDialog1.InitialDirectory = op.RutaXMLEgresos
        ConsultaOn = True
        ComboBox3.SelectedIndex = IdsSucursales.Busca(GlobalIdSucursalDefault)
        If Modo = 0 Then
            If idDevolucion = 0 Then
                Nuevo()
            Else
                LlenaDatosVenta()
                NuevoConcepto()
            End If
        Else

            Try
                'Dim Forma As New frmBuscaDocumentoVenta(1)
                'If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Nuevo()
                Dim V As New dbDevolucionesCompras(idDevolucion, MySqlcon)
                If Estado = 0 Then
                    '0 cotizacion
                    '1 pedido
                    '2 remision
                    '3 ventas
                    If Tipo = 2 Then
                        Dim Cr As New dbComprasRemisiones(IdRemision, MySqlcon)
                        TextBox1.Text = Cr.Proveedor.Clave
                        'IdRemision = Forma.Id
                        TipoDeCambio = 0
                        IdCompra = 0
                        IdForma = 0
                    Else
                        Dim Cv As New dbCompras(IdCompra, MySqlcon)
                        TextBox1.Text = Cv.Proveedor.Clave
                        'IdVenta = Forma.Id
                        TipoDeCambio = Cv.TipodeCambio
                        IdRemision = 0
                        IdForma = Cv.Idforma
                        Dim FP As New dbFormasdePago(Cv.Idforma, MySqlcon)
                        If FP.Tipo = dbFormasdePago.Tipos.Credito Then
                            EsdeCredito = True
                            Credito = Cv.Credito
                        Else
                            EsdeCredito = False
                            Credito = 0
                        End If
                    End If
                    Guardar()
                    If Estado <> 0 Then
                        V.AgregarDetallesReferencia(idDevolucion, IdDocumento, Tipo)
                        ConsultaDetalles()
                    End If
                Else
                    V.AgregarDetallesReferencia(idDevolucion, IdDocumento, Tipo)
                    ConsultaDetalles()
                End If
                NuevoConcepto()
                'End If
                seleccionarArticulos()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try
        End If
    End Sub

    Private Sub SacaTotal()
        Try
            If ConsultaOn Then
                Dim T As Double
                Dim Iva As Double
                Dim V As New dbDevolucionesCompras(MySqlcon)
                T = V.DaTotal(idDevolucion, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim O As New dbOpciones(MySqlcon)

                'If CheckBox1.Checked Then
                Iva = V.TotalIva
                Label12.Text = Format(V.Subtototal, "#,##0.00")
                Label13.Text = Format(V.TotalIva, "#,##0.00")
                Label14.Text = Format(V.TotalVenta, "#,##0.00")
                'Else
                'Iva = T * (O.Imp / 100)
                'Label12.Text = System.Math.Round(T, 2).ToString
                'Label13.Text = System.Math.Round(Iva, 2).ToString
                'Label14.Text = Format(System.Math.Round(T + Iva, 2), "#,###0.00")
                'End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Private Sub Nuevo()
        DateTimePicker1.Value = Date.Now
        TextBox1.Text = ""
        FolioAnt = 0
        Label11.Text = "Documento que afecta:"
        idDevolucion = 0
        IdForma = 0
        DGDetalles.DataSource = Nothing
        Button2.Enabled = False
        Button13.Enabled = False
        Button1.Enabled = False
        Button20.Enabled = False
        Button23.Enabled = False
        Button14.Enabled = False
        TextBox15.Text = ""
        CheckBox1.Checked = False
        TextBox14.Text = ""
        TextBox10.Enabled = True
        TextBox11.Enabled = True
        TextBox2.BackColor = Color.FromKnownColor(KnownColor.Window)
        Label17.Visible = False
        Estado = Estados.Inicio
        LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
        Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
        'TextBox11.Text = S.Serie
        ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
        'Dim V As New dbDevoluciones(MySqlcon)
        'Dim Sf As New dbSucursalesFolios(MySqlcon)
        'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Devolucionn, 1)
        'TextBox11.Text = Sf.Serie
        'TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        'Dim CM As New dbMonedasConversiones(1, MySqlcon)
        'TextBox10.Text = CM.Cantidad.ToString
        TextBox2.Text = ""
        Label12.Text = "0"
        Label13.Text = "0"
        Label14.Text = "0"
        TextBox15.Text = ""
        CheckBox1.Checked = False
        'Panel1.Enabled = True
        Panel2.Enabled = True
        SerieAnt = ""
        Button2.Enabled = True
        Label24.Visible = False
        Dim Sf As New dbSucursalesFolios(MySqlcon)
        Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasDevoluciones, 0)
        TextBox11.Text = Sf.Serie
        Dim V As New dbDevolucionesCompras(MySqlcon)
        TextBox10.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
        If CInt(TextBox10.Text) < Sf.FolioInicial Then
            TextBox10.Text = Sf.FolioInicial.ToString
        End If
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
                    'If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Then
                    '    If c.Credito > 0 Then
                    '        ComboBox4.SelectedIndex = 1
                    '    Else
                    '        ComboBox4.SelectedIndex = 0
                    '    End If
                    'End If
                    idProveedor = c.ID
                    'Isr = c.ISR
                    'IvaRetenido = c.IvaRetenido
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
            Dim C As New dbDevolucionesCompras(idDevolucion, MySqlcon)

            Dim Desglozar As Byte
            If IsNumeric(TextBox10.Text) Then
                If C.ChecaFolioRepetido(CInt(TextBox10.Text), idProveedor, TextBox11.Text) And pEstado = Estados.Guardada Then
                    'If pEstado = Estados.Guardada Then TextBox2.Text = C.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
                    MensajeError += "Folio Repetido"
                End If
            Else
                MensajeError = "Debe indicar un folio."
            End If

            'If FolioAnt <> TextBox2.Text Then
            
            'End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesAlta, PermisosN.Secciones.Compras) = False And pEstado = Estados.Guardada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesCancelar, PermisosN.Secciones.Compras) = False And pEstado = Estados.Cancelada Then
                MensajeError += " No tiene permiso para realizar esta operación."
            End If
            If MensajeError = "" Then
                If CheckBox1.Checked Then
                    Desglozar = 1
                Else
                    Desglozar = 0
                End If
                'Dim O As New dbOpciones(MySqlcon)
                C.DaTotal(idDevolucion, IDsMonedas2.Valor(ComboBox2.SelectedIndex))
                'Dim Sf As New dbSucursalesFolios(MySqlcon)
                'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Devolucionn, 1)
                'Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                C.Modificar(idDevolucion, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, 0, pEstado, IdForma, 0, TipoDeCambio, IDsMonedas2.Valor(ComboBox2.SelectedIndex), C.Subtototal, C.TotalVenta, idProveedor, TextBox14.Text, TextBox11.Text, CInt(TextBox10.Text), TextBox15.Text)
                Dim VP As New dbComprasPagos(MySqlcon)
                If pEstado = Estados.Cancelada Then
                    C.RegresaInventario(idDevolucion)
                    C.Aplicar(idDevolucion, C.Credito, False)
                    If IdCompra <> 0 Then
                        VP.CancelarPagosxDocumento(idDevolucion, 2, idProveedor, Estados.Cancelada)
                    End If
                End If
                If pEstado = Estados.Guardada Then
                    If EsdeCredito Then
                        If IdCompra <> 0 Then
                            If Credito < C.TotalVenta Then
                                VP.Guardar(C.idCompra, C.TotalVenta - Credito, Format(DateTimePicker1.Value, "yyyy/MM/dd"), "Abono por Devolución: " + TextBox11.Text + TextBox2.Text, idProveedor, idDevolucion, 2, 0, TipoDeCambio, IDsMonedas2.Valor(ComboBox2.SelectedIndex), 0, 0, 5)
                                C.Aplicar(idDevolucion, C.TotalVenta - Credito, True)
                            Else
                                VP.Guardar(C.idCompra, C.TotalVenta, Format(DateTimePicker1.Value, "yyyy/MM/dd"), "Abono por Devolución" + TextBox11.Text + TextBox2.Text, idProveedor, idDevolucion, 2, 0, TipoDeCambio, IDsMonedas2.Valor(ComboBox2.SelectedIndex), 0, 0, 5)
                                C.Aplicar(idDevolucion, C.TotalVenta, True)
                            End If
                        End If
                    Else
                        If C.Fecha = Format(Date.Now, "yyyy/MM/dd") Then
                            C.Aplicar(idDevolucion, C.TotalVenta, False)
                        Else

                        End If
                    End If
                    C.ModificaInventario(idDevolucion)
                    Imprimir(idDevolucion)
                End If
                'CadenaOriginal()
                If (pEstado = Estados.Guardada Or pEstado = Estados.Cancelada) And IdCompra <> 0 Then
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
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesAlta, PermisosN.Secciones.Compras) = True Then
                If idProveedor <> 0 Then
                    Dim C As New dbDevolucionesCompras(MySqlcon)
                    Dim Desglozar As Byte
                    If CheckBox1.Checked Then
                        Desglozar = 1
                    Else
                        Desglozar = 0
                    End If
                    'If TextBox2.Text = "" Then
                    '    MsgBox("Debe indicar un folio", MsgBoxStyle.Critical, NombreApp)
                    '    Exit Sub
                    'End If
                    'If C.ChecaFolioRepetido(TextBox2.Text, idProveedor) Then
                    '    TextBox2.BackColor = Color.FromArgb(250, 150, 150)
                    '    Label17.Visible = True
                    '    FolioAnt = ""
                    '    Exit Sub
                    'Else
                    '    FolioAnt = TextBox2.Text
                    'End If
                    'Dim O As New dbOpciones(MySqlcon)
                    'Dim CM As New dbMonedasConversiones(MySqlcon)
                    'CM.Modificar(1, CDbl(TextBox10.Text))
                    'Dim Sf As New dbSucursalesFolios(MySqlcon)
                    'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Devolucionn, 1)
                    'Dim Sc As New dbSucursalesCertificados(Sf.IdCertificado, MySqlcon)
                    ComboBox2.SelectedIndex = IDsMonedas2.Busca(IDsMonedas.Valor(ComboBox1.SelectedIndex))
                    'C.DaTotal(idDevolucion, IDsMonedas2.Valor(ComboBox2.SelectedIndex))

                    C.Guardar(idProveedor, Format(DateTimePicker1.Value, "yyyy/MM/dd"), TextBox2.Text, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), IdForma, TipoDeCambio, IDsMonedas2.Valor(ComboBox2.SelectedIndex), IdCompra, IdRemision, TextBox11.Text, CInt(TextBox10.Text), TextBox15.Text)
                    idDevolucion = C.ID
                    Label11.Text = "Documento que afecta: " + C.DaDatosDocumento(idDevolucion)
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
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        BuscaArticulo()
    End Sub
    Private Sub BuscaArticulo()
        Try
            If ConsultaOn Then
                Dim p As New dbInventario(MySqlcon)
                If p.BuscaArticulo(TextBox3.Text, 1, "") Then
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
        Dim C As New dbDevolucionesCompras(idDevolucion, MySqlcon)
        ConsultaOn = False
        ComboBox3.SelectedIndex = IdsSucursales.Busca(C.IdSucursal)
        ConsultaOn = True
        TextBox2.Text = C.Folio
        FolioAnt = C.Folio
        TextBox1.Text = C.Proveedor.Clave
        Estado = C.Estado
        TextBox8.Text = C.Iva.ToString
        'TextBox11.Text = C.Serie
        IdCompra = C.idCompra
        IdRemision = C.IdRemision
        TipoDeCambio = C.TipodeCambio
        TextBox14.Text = C.Comentario
        TextBox15.Text = C.Foliocfdi
        'TextBox10.Text = C.TipodeCambio.ToString
        'If C.Desglosar = 1 Then
        '    CheckBox1.Checked = True
        'Else
        '    CheckBox1.Checked = False
        'End If
        Button2.Enabled = True
        DateTimePicker1.Value = C.Fecha
        'ComboBox4.SelectedIndex = idsFormasDePago.Busca(C.IdFormadePago)
        ComboBox2.SelectedIndex = IDsMonedas2.Busca(C.IdConversion)
        Label11.Text = "Documento que afecta: " + C.DaDatosDocumento(idDevolucion)
        
        TextBox10.Text = C.Folioi.ToString
        TextBox11.Text = C.Serie
        LlenaDatosDetalles()
        Select Case Estado
            Case Estados.Cancelada
                Label24.Visible = True
                Label24.Text = "Cancelada"
                Label24.ForeColor = Color.Red
                Button13.Enabled = False
                'Panel1.Enabled = False
                Panel2.Enabled = False
                Button2.Enabled = False
                TextBox10.Enabled = False
                TextBox11.Enabled = False
            Case Estados.Guardada
                Label24.Visible = True
                Label24.Text = "Guardada"
                Button13.Enabled = True
                Button2.Enabled = False
                Label24.ForeColor = Color.FromArgb(50, 255, 50)
                'Panel1.Enabled = False
                Panel2.Enabled = False
                TextBox10.Enabled = False
                TextBox11.Enabled = False
            Case Else
                Label24.Visible = False
                Button13.Enabled = True
                TextBox10.Enabled = True
                TextBox11.Enabled = True
                'Panel1.Enabled = True
                Panel2.Enabled = True
                Button1.Enabled = True
                Button14.Enabled = True
                Button2.Enabled = True
        End Select
    End Sub
    Private Sub LlenaDatosDetalles()
        'Panel1.Visible = True
        ConsultaDetalles()
    End Sub
    Private Sub ConsultaDetalles()
        Try

            Tabla.Rows.Clear()
            Dim T As MySql.Data.MySqlClient.MySqlDataReader
            Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
            T = CD.ConsultaReader(idDevolucion)
            While T.Read
                If T("cantidad") <> 0 Then
                    'If T("idinventario") > 1 Then
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), Format(T("precio") / T("cantidad"), "0.00"), Format(T("precio"), "0.00"), T("abreviatura"))
                    'Else
                    '   Tabla.Rows.Add(T("iddetalle"), "P", "", T("cantidad"), T("pclave"), T("descripcion"), T("precio"), T("abreviatura"))
                    'End If
                Else
                    Tabla.Rows.Add(T("iddetalle"), "A", "", T("cantidad"), T("clave"), T("descripcion"), "0.00", Format(T("precio"), "0.00"), T("abreviatura"))
                End If
            End While
            T.Close()
            'Dim VP As New dbVentasProductos(MySqlcon)
            'T = VP.ConsultaReader(idVenta)
            'While T.Read
            '    Tabla.Rows.Add(T("idventasproducto"), "P", "", T("cantidad"), T("clave"), T("descripcion"), T("precio"), T("abreviatura"))
            'End While
            'T.Close()
            'Dim VS As New dbVentasServicios(MySqlcon)
            'T = VS.ConsultaReader(idVenta)
            'While T.Read
            '    Tabla.Rows.Add(T("idventasservicio"), "S", "", T("cantidad"), T("folio"), T("descripcion"), T("precio"), T("abreviatura"))
            'End While
            'T.Close()
            DGDetalles.DataSource = Tabla
            DGDetalles.Columns(0).Visible = False
            DGDetalles.Columns(1).Visible = False
            DGDetalles.Columns(2).Visible = False
            DGDetalles.Columns(4).ReadOnly = True
            DGDetalles.Columns(5).ReadOnly = True
            DGDetalles.Columns(6).ReadOnly = True
            DGDetalles.Columns(7).ReadOnly = True
            DGDetalles.Columns(8).ReadOnly = True
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
            SacaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub
    Private Sub NuevoConcepto()
        IdInventario = 0
        IdVariante = 0
        IdServicio = 0
        'IdVenta = 0
        'IdRemision = 0
        cmbVariante.Visible = False
        TextBox12.Text = "0"
        TextBox3.Text = ""


        TextBox4.Text = ""
        TextBox5.Text = "0"
        TextBox9.Text = "0"
        TextBox6.Text = "0"
        PrecioBase = 0
        cmbVariante.Visible = False
        Button12.Visible = False
        Button9.Enabled = False
        ComboBox1.SelectedIndex = IDsMonedas.Busca(GlobalIdMoneda)
        Button4.Text = "Modificar Concepto"
        Button4.Enabled = False
        TextBox5.Focus()
    End Sub
    Private Sub AgregaArticulo()
        Try
            Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
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
                If CInt(TextBox6.Text) <= 0 Then
                    MsgError += " El costo debe ser un valor mayor a 0."
                    HayError = True
                End If
            End If
            If HayError = False Then
                If Button4.Text = "Agregar Concepto" Then
                    CD.Guardar(idDevolucion, IdInventario, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, IdsAlmacenes.Valor(ComboBox8.SelectedIndex), CDbl(TextBox8.Text), CDbl(TextBox9.Text), IdVariante, IdServicio)
                        If ManejaSeries <> 0 Then
                            If CD.NuevoConcepto Then
                                Dim F As New frmVentasAsignaSeries(IdInventario, IdCompra, IdRemision, CInt(TextBox5.Text))
                                F.ShowDialog()
                            Else
                                Dim F As New frmVentasAsignaSeries(IdInventario, IdCompra, IdRemision, CD.Cantidad)
                                F.ShowDialog()
                            End If
                        End If

                        ConsultaDetalles()
                        'NuevoConcepto()
                        'PopUp("Artículo agregado", 90)
                    Else
                        CD.Modificar(IdDetalle, CDbl(TextBox5.Text), CDbl(TextBox6.Text), IDsMonedas.Valor(ComboBox1.SelectedIndex), TextBox4.Text, CDbl(TextBox8.Text), CDbl(TextBox9.Text))

                        ConsultaDetalles()
                        'NuevoConcepto()
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
        'If (PermisosCompras And CULng((Math.Pow(2, perCompras.Devoluciones + 2)))) <> 0 Then
        If Estado = 0 Then
            Guardar()
        End If
        If Estado <> 0 Then
            If IdInventario <> 0 Or IdVariante <> 0 Or IdServicio <> 0 Then AgregaArticulo()
        End If
        'Else
        'MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
        'TextBox3.Focus()
        'End If
    End Sub

    Private Sub DGDetalles_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellClick
        IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
        LlenaDatosDetallesA()
    End Sub
    Private Sub LlenaDatosDetallesA()
        Try

            'IdDetalle = DGDetalles.Item(0, DGDetalles.CurrentCell.RowIndex).Value
            Dim CD As New dbDevolucionesComprasDetalles(IdDetalle, MySqlcon)

            TextBox5.Text = CD.Cantidad.ToString
            TextBox8.Text = CD.Iva.ToString
            TextBox9.Text = CD.Descuento.ToString
            IdInventario = CD.Idinventario
            IdVariante = CD.idVariante
            If IdInventario > 1 Then
                ConsultaOn = False
                TextBox3.Text = CD.Inventario.Clave
                ConsultaOn = True
                IdVariante = 0
                cmbVariante.Visible = False
                PorLotes = CD.Inventario.PorLotes
                Aduana = CD.Inventario.Aduana
                If PorLotes = 1 Then
                    Button20.Enabled = True
                Else
                    Button20.Enabled = False
                End If
                If Aduana = 1 Then
                    Button23.Enabled = True
                Else
                    Button23.Enabled = False
                End If
            End If

            PrecioU = CD.Precio / CD.Cantidad
            PrecioBase = PrecioU
            TextBox12.Text = PrecioU.ToString
            cmbVariante.Visible = False
            CantAnt = CD.Cantidad
            IdAlmacen = CD.IdAlmacen
            Dim A As New dbAlmacenes(IdAlmacen, MySqlcon)
            AlmacenStr = A.Nombre
            TextBox6.Text = CD.Precio.ToString
            TextBox4.Text = CD.Descripcion
            Button4.Text = "Modificar Concepto"
            If CD.Inventario.ManejaSeries = 1 Then
                Button12.Visible = True
            Else
                Button12.Visible = False
            End If
            Button4.Enabled = True
            If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then Button9.Enabled = True
            'cmbtipoarticulo.Text = "A"
            ComboBox1.SelectedIndex = IDsMonedas.Busca(CD.IdMoneda)


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub


    Private Sub DGCompras_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        LlenaDatosVenta()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If Estado = Estados.SinGuardar Then
            If MsgBox("Esta devolución no ha sido guardada. ¿Desea iniciar una nueva devolución? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idDevolucion)
                Dim C As New dbDevolucionesCompras(idDevolucion, MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                '    Dim Cliente As New dbClientes(MySqlcon)
                '    Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                C.RegresaInventario(idDevolucion)
                C.Eliminar(idDevolucion)
                Nuevo()
                BuscarDocumento()
            End If
        Else
            Nuevo()
            BuscarDocumento()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        NuevoConcepto()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Try
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesAlta, PermisosN.Secciones.Compras) = True Then
                If MsgBox("¿Desea eliminar este concepto de la devolución?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                    Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
                    CD.Eliminar(IdDetalle)
                    'Dim S As New dbInventarioSeries(MySqlcon)
                    'S.QuitarSeriesAventaxArticulo(IdInventario, idDevolucion)
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
            TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion + " " + B.Cliente.NoExterior + " " + B.Cliente.Ciudad + " " + B.Cliente.CP
            'Else
            '    TextBox7.Text = B.Cliente.Nombre + vbCrLf + B.Cliente.RFC + vbCrLf + B.Cliente.Direccion2 + " " + B.Cliente.NoExterior2 + " " + B.Cliente.Ciudad2 + " " + B.Cliente.CP2
            'End If
            TextBox13.Text = "Límite: " + Format(B.Proveedor.LimiteCredito, "#,##0.00") + vbCrLf + "Días: " + B.Proveedor.DiasCredito.ToString ' + vbCrLf + "Saldo: " + Format(B.Cliente.Saldo, "#,##0.00")
            'If Estado <> Estados.Guardada Or Estado <> Estados.Cancelada Then
            '    If B.Cliente.Credito > 0 Then
            '        ComboBox4.SelectedIndex = 1
            '    Else
            '        ComboBox4.SelectedIndex = 0
            '    End If
            'End If
            idProveedor = B.Cliente.ID
            Isr = B.Cliente.ISR
            IvaRetenido = B.Cliente.IvaRetenido
            ConsultaOn = False
            TextBox1.Text = B.Cliente.Clave
            ConsultaOn = True
            TextBox5.Focus()
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim TipodeBusqueda As Integer
        TipodeBusqueda = frmBuscador.TipoDeBusqueda.ArticuloInv
        Dim B As New frmBuscador(TipodeBusqueda, 0, False, True, False)
        B.ShowDialog()
        If B.DialogResult = Windows.Forms.DialogResult.OK Then
            Select Case B.Tipo
                Case "I"
                    LlenaDatosArticulo(B.Inventario)
                    TextBox12.Focus()
            End Select

        End If
    End Sub
    Private Sub LlenaDatosArticulo(ByVal Articulo As dbInventario)
        'Dim a As dbInventarioPrecios
        'Dim Cant As Double
        'TextBox4.Text = Articulo.Nombre
        'a = Articulo.DaPrecioDefault
        'If IsNumeric(TextBox5.Text) Then
        '    Cant = CDbl(TextBox5.Text)
        'Else
        '    TextBox5.Text = "1"
        '    Cant = 1
        'End If
        'cmbVariante.Visible = False
        'PrecioU = a.Precio
        'PrecioBase = a.Precio
        'TextBox12.Text = a.Precio.ToString
        'ManejaSeries = Articulo.ManejaSeries
        'TextBox6.Text = Cant * PrecioU
        'TextBox8.Text = Articulo.Iva.ToString
        'cmbVariante.Visible = False
        ''cmbtipoarticulo.Text = "A"
        'IdInventario = Articulo.ID
        'If ManejaSeries = 0 Then
        '    Button12.Visible = False
        'Else
        '    Button12.Visible = True
        'End If
        'ConsultaOn = False
        'TextBox3.Text = Articulo.Clave
        'ComboBox1.SelectedIndex = IDsMonedas.Busca(a.IdMoneda)
        'ConsultaOn = True
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
        Dim f As New frmDevolucionesComprasConsulta(ModosDeBusqueda.Secundario)
        f.ShowDialog()
        If f.DialogResult = Windows.Forms.DialogResult.OK Then
            idDevolucion = f.IdDevolucion
            LlenaDatosVenta()
            NuevoConcepto()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If MsgBox("¿Eliminar esta devolución?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            If GlobalPermisos.ChecaPermiso(PermisosN.Compras.DevolucionesAlta, PermisosN.Secciones.Compras) = True Then
                'Dim S As New dbInventarioSeries(MySqlcon)
                'S.QuitaSeriesAVenta(idDevolucion)
                Dim C As New dbDevolucionesCompras(idDevolucion, MySqlcon)
                'If Estado = Estados.Guardada And C.Credito = 0 Then
                'Dim Cliente As New dbClientes(MySqlcon)
                'Cliente.ModificaSaldo(idCliente, C.TotalaPagar, 1)
                'End If
                C.RegresaInventario(idDevolucion)
                Dim VP As New dbComprasPagos(MySqlcon)
                VP.CancelarPagosxDocumento(idDevolucion, 2, idProveedor, Estados.Cancelada)
                C.Eliminar(idDevolucion)
                PopUp("Factura Eliminada", 90)
                Nuevo()
            Else
                MsgBox("No tiene permiso para realizar esta operación.", MsgBoxStyle.Critical, GlobalNombreApp)
            End If
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim F As New frmVentasAsignaSeries(IdInventario, IdCompra, IdRemision, CDbl(TextBox5.Text))
        F.ShowDialog()
        F.Dispose()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Modificar(Estados.Guardada)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        If MsgBox("¿Cancelar esta devolución?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
            Modificar(Estados.Cancelada)
            If MsgBox("¿Desea imprimir la devolución?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Imprimir(idDevolucion)
            End If
        End If
    End Sub

    Private Sub Imprimir(pIdDevolucion As Integer)
        Try
            Dim Compra As New dbDevolucionesCompras(pIdDevolucion, MySqlcon)
            ImpDoc.IdSucursal = Compra.idSucursal
            ImpDoc.TipoDocumento = TiposDocumentos.CompraDevolucion
            ImpDoc.TipoDocumentoT = TiposDocumentos.CompraDevolucion + 1000
            ImpDoc.TipoDocumentoImp = TiposDocumentos.CompraDevolucion
            ImpDoc.TipoRuta = dbSucursalesArchivos.TipoRutas.OtrosPDF
            ImpDoc.Inicializar()
            LlenaNodosImpresion(pIdDevolucion)
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\")
            IO.Directory.CreateDirectory(ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM") + "\")
            If Op._NoRutas = "0" Then
                ImpDoc.RutaPDF = ImpDoc.RutaPDF + "\" + Format(DateTimePicker1.Value, "yyyy") + "\" + Format(DateTimePicker1.Value, "MM")
            End If
            ImpDoc.GuardaSettingsBullzip(ImpDoc.RutaPDF)
            PrintDocument1.PrinterSettings.PrinterName = ImpDoc.Impresora
            PrintDocument1.DocumentName = "DEV DE COMPRA " + Compra.Serie + Compra.Folioi.ToString("0000")
            PrintDocument1.Print()
        Catch ex As Exception
            MsgBox("Error al imprimir: " + ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    Public Sub LlenaNodosImpresion(pIdDevolucion As Integer)
        Dim Cot As New dbDevolucionesCompras(pIdDevolucion, MySqlcon)
        Dim Suc As New dbSucursales(Cot.IdSucursal, MySqlcon)
        Dim Prov As New dbproveedores(Cot.Proveedor.ID, MySqlcon)
        'Dim firmaChequeRecibido As String = "Firma cheque recibido"
        Cot.DaTotal(pIdDevolucion, Cot.IdConversion)
        ImpDoc.ImpND.Add(New NodoImpresionN("", "foliocfdi", Cot.Foliocfdi, 0), "foliocfdi")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docqueafecta", Cot.DaDatosDocumento(pIdDevolucion), 0), "docqueafecta")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "referencia", Cot.Folio, 0), "referencia")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFolio", Cot.Folioi.ToString("0000"), 0), "docFolio")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "serie", Cot.Serie, 0), "serie")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docFecha", Cot.Fecha, 0), "docFecha")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docSubTotal", Format(Cot.Subtototal, Op._formatoSubtotal).PadLeft(Op.EspacioSubtotal), 0), "docSubTotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "docTotal", Format(Cot.TotalVenta, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "docTotal")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalieps", Format(Cot.TotalIEPS, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "doctotalieps")
        ImpDoc.ImpND.Add(New NodoImpresionN("", "doctotalivaret", Format(Cot.TotalIvaretenidoCon, Op._formatoTotal).PadLeft(Op.Espaciototal), 0), "doctotalivaret")
        Dim TotalconLetra As String
        Dim CL As New CLetras
        If Cot.TotalVenta >= 0 Then
            TotalconLetra = CL.LetrasM(Cot.TotalVenta, Cot.IdConversion, GlobalIdiomaLetras)
        Else
            TotalconLetra = "MENOS " + CL.LetrasM(Cot.TotalVenta * -1, Cot.IdConversion, GlobalIdiomaLetras)
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
        Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
        T = CD.ConsultaReader(pIdDevolucion)
        While T.Read
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artCant", Format(T("cantidad"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "artCant" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "descripcion", T("descripcion"), 0), "descripcion" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "artPrecioU", Format(T("precio") / T("cantidad"), Op._formatoImporte).PadLeft(Op.EspacioImporte), 0), "artPrecioU" + Format(ImpDoc.CuantosRenglones, "000"))
            ImpDoc.ImpNDD.Add(New NodoImpresionN("", "provImporte", Format(T("precio"), Op._formatocantidad).PadLeft(Op.EspacioCantidad), 0), "provImporte" + Format(ImpDoc.CuantosRenglones, "000"))
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
        DR = Cot.DaIvas(pIdDevolucion)
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


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ConsultaOn Then
            LlenaCombos("tblalmacenes", ComboBox8, "nombre", "nombret", "idalmacen", IdsAlmacenes, "idalmacen<>1 and idsucursal=" + IdsSucursales.Valor(ComboBox3.SelectedIndex).ToString)
            Dim S As New dbSucursales(IdsSucursales.Valor(ComboBox3.SelectedIndex), MySqlcon)
            ComboBox8.SelectedIndex = IdsAlmacenes.Busca(S.idAlmacen)
            Dim Sf As New dbSucursalesFolios(MySqlcon)
            Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.ComprasDevoluciones, 0)
            TextBox11.Text = Sf.Serie
            Dim V As New dbDevolucionesCompras(MySqlcon)
            TextBox10.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
            If CInt(TextBox10.Text) < Sf.FolioInicial Then
                TextBox10.Text = Sf.FolioInicial.ToString
            End If
        End If
        'TextBox11.Text = S.Serie
        'Dim V As New dbDevoluciones(MySqlcon)
        'Dim Sf As New dbSucursalesFolios(MySqlcon)
        'Sf.BuscaFolios(IdsSucursales.Valor(ComboBox3.SelectedIndex), dbSucursalesFolios.TipoDocumentos.Devolucionn, 1)
        'TextBox11.Text = Sf.Serie
        'TextBox2.Text = V.DaNuevoFolio(TextBox11.Text, IdsSucursales.Valor(ComboBox3.SelectedIndex)).ToString
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If IdInventario <> 0 Or IdVariante <> 0 Then
            If IsNumeric(TextBox5.Text) Then
                TextBox6.Text = CDbl(PrecioU * CDbl(TextBox5.Text))
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click


     

        BuscarDocumento()
    End Sub
    Private Sub BuscarDocumento()
        Try
            Dim Forma As New frmBuscaDocumentoVenta(2, True, 0, IdsSucursales.Valor(ComboBox3.SelectedIndex), 1, False, True, True, 0, False, "", False)
            If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If Estado = Estados.SinGuardar Then
                    If MsgBox("Esta devolución no ha sido guardada. ¿Desea iniciar una nueva devolución? Los datos actuales se perderán.", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                        Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
                        For i As Integer = 0 To DGDetalles.RowCount() - 1
                            CD.Eliminar(Integer.Parse(DGDetalles.Rows(i).Cells("Id").Value.ToString))
                        Next

                        Dim V As New dbDevolucionesCompras(Forma.id(0), MySqlcon)
                        If Estado = 0 Then
                            '0 cotizacion
                            '1 pedido
                            '2 remision
                            '3 ventas
                            Select Case Forma.Tipo
                                Case 2
                                    IdRemision = Forma.id(0)
                                    Dim Cr As New dbComprasRemisiones(Forma.id(0), MySqlcon)
                                    TextBox1.Text = Cr.Proveedor.Clave
                                    IdRemision = Forma.id(0)
                                    TipoDeCambio = 0 'Cr.TipodeCambio
                                    IdCompra = 0
                                    IdForma = 0
                                Case 3
                                    IdCompra = Forma.id(0)
                                    Dim Cv As New dbCompras(Forma.id(0), MySqlcon)
                                    TextBox1.Text = Cv.Proveedor.Clave
                                    IdCompra = Forma.id(0)
                                    TipoDeCambio = Cv.TipodeCambio
                                    IdRemision = 0
                                    IdForma = Cv.Idforma
                                    Dim FP As New dbFormasdePago(Cv.Idforma, MySqlcon)
                                    If FP.Tipo = dbFormasdePago.Tipos.Credito Then
                                        EsdeCredito = True
                                        Credito = Cv.Credito
                                    Else
                                        EsdeCredito = False
                                        Credito = 0
                                    End If
                            End Select

                            Guardar()
                            If Estado <> 0 Then
                                IdDocumento = Forma.id(0)
                                V.AgregarDetallesReferencia(idDevolucion, Forma.id(0), Forma.Tipo)
                                ConsultaDetalles()
                            End If
                        Else
                            IdDocumento = Forma.id(0)
                            V.AgregarDetallesReferencia(idDevolucion, Forma.id(0), Forma.Tipo)
                            ConsultaDetalles()
                        End If
                        NuevoConcepto()
                        

                      
                    End If
                Else
                    'Estado guardado
                    Dim V As New dbDevolucionesCompras(Forma.id(0), MySqlcon)
                    If Estado = 0 Then
                        '0 cotizacion
                        '1 pedido
                        '2 remision
                        '3 ventas
                        Select Case Forma.Tipo
                            Case 2
                                IdRemision = Forma.id(0)
                                Dim Cr As New dbComprasRemisiones(Forma.id(0), MySqlcon)
                                TextBox1.Text = Cr.Proveedor.Clave
                                IdRemision = Forma.id(0)
                                TipoDeCambio = 0 'Cr.TipodeCambio
                                IdCompra = 0
                                IdForma = 0
                            Case 3
                                IdCompra = Forma.id(0)
                                Dim Cv As New dbCompras(Forma.id(0), MySqlcon)
                                TextBox1.Text = Cv.Proveedor.Clave
                                IdCompra = Forma.id(0)
                                TipoDeCambio = Cv.TipodeCambio
                                IdRemision = 0
                                IdForma = Cv.Idforma
                                Dim FP As New dbFormasdePago(Cv.Idforma, MySqlcon)
                                If FP.Tipo = dbFormasdePago.Tipos.Credito Then
                                    EsdeCredito = True
                                    Credito = Cv.Credito
                                Else
                                    EsdeCredito = False
                                    Credito = 0
                                End If
                        End Select

                        Guardar()
                        If Estado <> 0 Then
                            IdDocumento = Forma.id(0)
                            V.AgregarDetallesReferencia(idDevolucion, Forma.id(0), Forma.Tipo)
                            ConsultaDetalles()
                        End If
                    Else
                        IdDocumento = Forma.id(0)
                        V.AgregarDetallesReferencia(idDevolucion, Forma.id(0), Forma.Tipo)
                        ConsultaDetalles()
                    End If
                    NuevoConcepto()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try
    End Sub
    'Version Normal
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
        'If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
        'Dim op As New dbOpciones(MySqlcon)
        If Op.NoImpSinGuardar = 1 And Estado < 3 Then
            MsgBox("No se puede imprimir un documento sin guardar.", MsgBoxStyle.Information, GlobalNombreApp)
            Exit Sub
        End If
        Imprimir(idDevolucion)
        'PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
        'PrintDocument1.Print()
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

    Private Sub DGDetalles_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DGDetalles.CellValidating

    End Sub

    Private Sub DGDetalles_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDetalles.CellValueChanged
        If Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
            If IsNumeric(DGDetalles.Item(e.ColumnIndex, e.RowIndex).Value) Then
                IdDetalle = DGDetalles.Item(0, e.RowIndex).Value
                LlenaDatosDetallesA()
                TextBox5.Text = DGDetalles.Item(e.ColumnIndex, e.RowIndex).Value
                'If Estado <> 0 Then
                AgregaArticulo()
                ' End If
            End If
        End If
    End Sub


    Private Sub DGDetalles_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGDetalles.KeyDown
        If e.KeyCode = Keys.Delete Then
            If IdDetalle <> 0 And Estado <> Estados.Guardada And Estado <> Estados.Cancelada Then
                Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
                CD.Eliminar(IdDetalle)
                IdDetalle = 0
                ConsultaDetalles()
            End If
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            If MsgBox("¿Guardar los cambios del comentario?", MsgBoxStyle.YesNo, GlobalNombreApp) = MsgBoxResult.Yes Then
                Dim V As New dbDevolucionesCompras(MySqlcon)
                V.ActualizaComentario(idDevolucion, TextBox14.Text)
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

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged

    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        seleccionarArticulos()
    End Sub
    Private Sub seleccionarArticulos()
        'llenaDatosDocumento()

        Dim dev As New frmDevolucionesElegirC(idDevolucion)
        If dev.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'Tomar los datos
            'BuscarDocumento()
            cadenaIDS = dev.IDs
            llenaDatosSeleccionados()
        End If
    End Sub
    Private Sub llenaDatosSeleccionados()
        ConsultaDetalles()
        Dim id As String = ""
        If cadenaIDS <> "" Then
            For i As Integer = 0 To cadenaIDS.Length() - 1

                If cadenaIDS.Chars(i) = "," Then
                    Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
                    CD.Eliminar(Integer.Parse(id))
                    ConsultaDetalles()
                    NuevoConcepto()
                    id = ""
                Else
                    id += cadenaIDS.Chars(i)
                End If

            Next
        End If



    End Sub
    Private Sub llenaDatosDocumento()

        'Eliminar datos Todos
        'Dim CD As New dbDevolucionesComprasDetalles(MySqlcon)
        'For i As Integer = 0 To DGDetalles.RowCount() - 1
        '    CD.Eliminar(Integer.Parse(DGDetalles.Rows(i).Cells("Id").Value.ToString))
        'Next



        If Modo = 0 Then
            'If idDevolucion = 0 Then
            '    Nuevo()
            'Else
            '    LlenaDatosVenta()
            '    NuevoConcepto()
            'End If
        Else

            Try
                'Dim Forma As New frmBuscaDocumentoVenta(1)
                ''If Forma.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Dim V As New dbDevolucionesCompras(idDevolucion, MySqlcon)
                'If Estado = 0 Then
                '    '0 cotizacion
                '    '1 pedido
                '    '2 remision
                '    '3 ventas
                '    If Tipo = 2 Then
                '        Dim Cr As New dbComprasRemisiones(IdRemision, MySqlcon)
                '        TextBox1.Text = Cr.Proveedor.Clave
                '        'IdRemision = Forma.Id
                '        TipoDeCambio = 0
                '        IdCompra = 0
                '    Else
                '        Dim Cv As New dbCompras(IdCompra, MySqlcon)
                '        TextBox1.Text = Cv.Proveedor.Clave
                '        'IdVenta = Forma.Id
                '        TipoDeCambio = Cv.TipodeCambio
                '        IdRemision = 0
                '        Dim FP As New dbFormasdePago(Cv.Idforma, MySqlcon)
                '        If FP.Tipo = dbFormasdePago.Tipos.Credito Then
                '            EsdeCredito = True
                '            Credito = Cv.Credito
                '        Else
                '            EsdeCredito = False
                '            Credito = 0
                '        End If
                '    End If
                '    Guardar()
                '    If Estado <> 0 Then
                '        V.EliminarDetalles(idDevolucion)
                '        V.AgregarDetallesReferencia(idDevolucion, IdDocumento, Tipo)
                '        ConsultaDetalles()
                '    End If
                'Else
                '    'V.EliminarDetalles(idDevolucion)
                '    V.AgregarDetallesReferencia(idDevolucion, IdDocumento, Tipo)
                '    ConsultaDetalles()
                'End If
                'NuevoConcepto()
                ''End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
            End Try


        End If

    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try
                Dim xmldoc As New Xml.XmlDocument
                xmldoc.Load(OpenFileDialog1.FileName)
                TextBox15.Text = xmldoc.Item("cfdi:Comprobante").Item("cfdi:Complemento").Item("tfd:TimbreFiscalDigital").Attributes("UUID").Value
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
                Dim V As New dbDevolucionesCompras(idDevolucion, MySqlcon)
                Dim Canceladas As Byte = 0
                Dim credito As Byte
                Dim cuantas As Integer
                If V.Estado = Estados.Cancelada Then
                    Canceladas = 1
                End If
                Dim FP As New dbFormasdePago(V.IdFormadePago, MySqlcon)
                If FP.Tipo = dbFormasdePago.Tipos.Contado Or FP.Tipo = dbFormasdePago.Tipos.Parcialidad Then
                    credito = 0
                Else
                    credito = 1
                End If
                cuantas = M.CuantasHay(3, Canceladas, credito)
                If cuantas > 0 Then
                    If cuantas = 1 Then
                        M.ID = M.DaMascaraActiva(3, Canceladas, credito)
                    Else
                        cuantas = M.CuantasHay(3, Canceladas, 3)
                        If cuantas = 1 Then
                            M.ID = M.DaMascaraActiva(3, Canceladas, 3)
                        Else
                            Dim f As New frmContabilidadMascarasConsulta(ModosDeBusqueda.Secundario, False, 3)
                            f.ShowDialog()
                            If f.DialogResult = Windows.Forms.DialogResult.OK Then
                                M.ID = f.IdMascara
                            Else
                                Exit Sub
                            End If
                        End If
                    End If
                    M.LlenaDatos()
                    Dim GP As dbContabilidadGeneraPolizas
                    If Canceladas = 0 Then
                        GP = New dbContabilidadGeneraPolizas(M, V.Fecha, V.Fecha, V.Fecha)
                    Else
                        GP = New dbContabilidadGeneraPolizas(M, V.FechaCancelado, V.FechaCancelado, V.FechaCancelado)
                    End If
                    GP.GeneraPolizaGeneral(V.ID, V.IdProvedor, 0, 0, 0, 0, 0)
                    If GlobalPermisos.ChecaPermiso(PermisosN.Contabilidad.VerPolizasGeneradas, PermisosN.Secciones.Contabilidad) = True Then
                        If GP.Exito Then
                            Dim frmp As New frmContabilidadPolizasN(GP.IdPoliza)
                            frmp.ShowDialog()
                            frmp.Dispose()
                        Else
                            MsgBox("No se generó la póliza", MsgBoxStyle.Information, GlobalNombreApp)
                        End If
                    Else
                        PopUp("Póliza Generada", 60)
                    End If
                End If
                End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, GlobalNombreApp)
        End Try


    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If PorLotes = 1 Then
            Dim Doc As Integer
            Dim QueDoc As Byte
            If IdCompra <> 0 Then
                Doc = IdCompra
                QueDoc = 1
            Else
                Doc = IdRemision
                QueDoc = 0
            End If
            Dim F As New frmInventarioLotes(0, 0, 0, 0, CDbl(DGDetalles.Item(3, DGDetalles.CurrentCell.RowIndex).Value), IdInventario, 1, 0, IdDetalle, 0, IdAlmacen, AlmacenStr, 0, Doc, QueDoc)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If Aduana = 1 Then
            Dim Doc As Integer
            Dim QueDoc As Byte
            If IdCompra <> 0 Then
                Doc = IdCompra
                QueDoc = 1
            Else
                Doc = IdRemision
                QueDoc = 0
            End If
            Dim F As New frmInventarioAduana(0, 0, 0, 0, CDbl(DGDetalles.Item(3, DGDetalles.CurrentCell.RowIndex).Value), IdInventario, 1, IdDetalle, 0, 0, IdAlmacen, AlmacenStr, 0, Doc, QueDoc)
            F.ShowDialog()
            F.Dispose()
        End If
    End Sub
End Class